using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newshore.Technical.Domain.Commands.Flights;
using Newshore.Technical.Domain.Commands.JourneyFlights;
using Newshore.Technical.Domain.Commands.Journeys;
using Newshore.Technical.Domain.Commands.Transports;
using Newshore.Technical.Domain.Interfaces;
using Newshore.Technical.Domain.Queries.Flights;
using Newshore.Technical.Domain.Queries.Journeys;
using Newshore.Technical.Domain.Queries.Transports;
using Newshore.Technical.Domain.ResponseModels;
using Newshore.Technical.Transverse.Dto;
using Newshore.Technical.Transverse.Utils;
using Newtonsoft.Json;

namespace Newshore.Technical.Domain.Domain
{
    public class JourneyDomain : IJourneyDomain
    {        
        private readonly IConfiguration? _configuration;
        private readonly IMediator _mediator;
        private readonly string _apiJourneysUrl = string.Empty;
        private readonly int _maxFlightCount = 0;
        private List<JourneyApiResponseDto>? _apiJourneys;
        private readonly ILogger<JourneyDomain> _logger;
          
        public JourneyDomain(IMediator mediator, IConfiguration configuration, ILogger<JourneyDomain> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _mediator = mediator;
            _apiJourneysUrl = _configuration?["AppSettings:JourneyApi"]?.ToString() ?? string.Empty;
            _maxFlightCount = Convert.ToInt32(_configuration?["AppSettings:GetMaxFlightCount"] ?? "4");                        
        }     

        public async Task<List<JourneyDto>?> FindItinerariesFromOriginToDestination(string origin, string destination)
        {                        
            _logger.Log(LogLevel.Information, $"JourneyDomain - FindItinerariesFromOriginToDestination - Success - origin: {origin} destination: {destination}");
            List<JourneyDto>? result;
            
            try
            {
                result = await CheckJourneyExists(origin, destination);
                if (result == null)
                {
                    throw new Exception("Solicitud no pudo ser procesada. No fue posible calcular la ruta");
                }                
                _logger.Log(LogLevel.Information, "JourneyDomain - CheckJourneyExists - Success");
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("FindItinerariesFromOriginToDestination - Domain - Error -> Origin: {@origin}, Destination: {@destination}", ex, origin, destination);
                throw;
            }
            return result;
        }        
                                       
        private void SaveJourneyInfoFromApi(ref JourneyDto journey) 
        {            
            _logger.Log(LogLevel.Information, $"JourneyDomain - SaveJourneyInfoFromApi, Success - Journey: {journey}");
            try
            {
                JourneyResponse journeyInfo = _mediator.Send(new CreateJourneyService() { Destination = journey.Destination, Origin = journey.Origin, Price = journey.Price, IsDirectFlight = journey.IsDirectFlight, IsRoundTripFlight = journey.IsRoundTripFlight }).Result;
                if (journeyInfo != null)
                {
                    journey.Id = journeyInfo.Id;
                    journeyInfo.Flights = new();
                    journey.Flights.ForEach(flight =>
                    {
                        TransportResponse transportInfo = _mediator.Send(new CreateTransportService() { FlightCarrier = flight.Transport.FlightCarrier, FlightNumber = flight.Transport.FlightNumber }).Result;
                        if (transportInfo != null)
                        {
                            flight.Id = flight.Id;
                            FlightResponse flightInfo = _mediator.Send(new CreateFlightService() { Destination = flight.Destination, Origin = flight.Origin, Price = flight.Price, TransportId = transportInfo.Id }).Result;
                            if (flightInfo != null)
                            {
                                flight.Id = flightInfo.Id;
                                flight.Transport.Id = transportInfo.Id;
                                journeyInfo.Flights.Add(flightInfo);
                                _mediator.Send(new CreateJourneyFlightService() { FlightId = flightInfo.Id, JourneyId = journeyInfo.Id });
                            }
                        }
                    });
                    _logger.Log(LogLevel.Information, $"JourneyDomain - SaveJourneyInfoFromApi, Success - Journey: {journey}");                    
                }
                else
                {                    
                    _logger.Log(LogLevel.Warning, $"JourneyDomain - SaveJourneyInfoFromApi - Success - Journey: {@journey} - Journey no saved on database", journey);
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Domain Save journey info from api -- Start --> Journey: {@journey}", ex, journey);
            }
        }

        private async Task<string> GetJourneyInfoFromApi()
        {            
            _logger.Log(LogLevel.Information, $"JourneyDomain - GetJourneyInfoFromApi - Success - Start");
            string result = string.Empty;
            try
            {
                using (HttpClient client = new())
                {
                    HttpResponseMessage response = await client.GetAsync(_apiJourneysUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                }
                _logger.Log(LogLevel.Information, $"JourneyDomain - GetJourneyInfoFromApi - Success");                
            }
            catch (Exception ex) 
            {
                LogUtils.WriteErrorLog("Domain Get Journey Info From Api -- Error", ex);
                throw;
            }
            return result;
        }

        private List<JourneyDto> CalculateJourney(string origin, string destination)
        {            
            _logger.Log(LogLevel.Information, $"JourneyDomain - CalculateJourney - Success - Origin:{origin} Destination:{destination}");
            List<JourneyDto> result = new();
            try
            {
                List<List<JourneyApiResponseDto>> allApiJourneys = new();
                List<List<JourneyApiResponseDto>>? apiJourneys = GetApiJourneysByOriginAndDestination(origin, destination);
                if (apiJourneys != null && apiJourneys.Any())
                {
                    allApiJourneys.AddRange(apiJourneys);
                    List<List<JourneyApiResponseDto>>? apiReturnJourneys = GetApiJourneysByOriginAndDestination(destination, origin);
                    apiJourneys.ForEach(apiJourney =>
                    {
                        if (apiReturnJourneys != null && apiReturnJourneys.Any())
                        {
                            apiReturnJourneys.ForEach(apiReturnJourney =>
                            {
                                List<JourneyApiResponseDto> totalJourney = new();
                                totalJourney.AddRange(apiJourney);
                                totalJourney.AddRange(apiReturnJourney);
                                allApiJourneys.Add(totalJourney);
                            });
                        }
                        else
                        {                            
                            _logger.Log(LogLevel.Warning, "JourneyDomain - CalculateJourney - Success - return journeys not found");
                        }
                    });
                }
                else
                {
                    _logger.Log(LogLevel.Warning, "JourneyDomain - CalculateJourney - Success - return journeys not found");                    
                }

                if (allApiJourneys != null && allApiJourneys.Any())
                {
                    allApiJourneys.ForEach(apiJourney =>
                    {
                        result.Add(GetJourneyDto(apiJourney, origin, destination));
                    });
                }
                _logger.Log(LogLevel.Information, "JourneyDomain - CalculateJourney -Success");                
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("DOMAIN Calculate Journey -- Error", ex);
                throw;
            }
            return result;
        }

        private JourneyDto GetJourneyDto(List<JourneyApiResponseDto> flights, string origin, string destination)
        {
            int flightsCount = flights?.Count ?? 0;
            _logger.Log(LogLevel.Information, $"JourneyDomain - GetJourneyDto, Success: origin: {origin} destination: {destination}");
            try
            {
                double price = 0;
                JourneyDto journey = new()
                {
                    Origin = origin,
                    Destination = destination,
                    IsDirectFlight = flights != null && flights.Any() ? flights.Count == 1 || (flights.Count == 2 && flights.First().Origin == flights.Last().Destination) : false,
                    IsRoundTripFlight = flights != null && flights.Any() ? flights.First().Origin == flights.Last().Destination : false,
                    Flights = new()
                };
                if (flights != null && flights.Any())
                {
                    flights.ForEach(flight =>
                    {
                        journey.Flights.Add(new FlightDto()
                        {
                            Origin = flight.Origin,
                            Destination = flight.Destination,
                            Price = flight.Price,
                            Transport = new() { FlightCarrier = flight.FlightCarrier, FlightNumber = flight.FlightNumber }
                        });

                        price += flight.Price;
                    });
                }
                else
                {
                    _logger.Log(LogLevel.Warning, $"JourneyDomain - GetJourneyDto - Success - Origin: {origin}, Destination: {destination}, flights count: {flightsCount} -- Journey not contains flights");
                }

                journey.Price = price;
                _logger.Log(LogLevel.Information, $"JourneyDomain - GetJourneyDto - Success --> Origin: {origin}, Destination: {destination}, flights count: {flightsCount}");
                return journey;
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Domain Get Journey Dto -- Error --> Origin: {origin}, Destination: {destination}, flights count: {flightsCount}", ex, origin, destination, flightsCount);
                throw;
            }
        }

        private List<List<JourneyApiResponseDto>>? GetApiJourneysByOriginAndDestination(string origin, string destination)
        {
            _logger.Log(LogLevel.Information, $"JourneyDomain - GetApiJourneysByOriginAndDestination - Origin:{origin} Destinations:{destination}");            
            List<List<JourneyApiResponseDto>>? result = new();
            if (_apiJourneys != null && _apiJourneys.Any())
            {
                List<JourneyApiResponseDto> found = _apiJourneys.Where(j => j.Origin == origin).ToList();
                if (found != null && found.Any())
                {
                    foreach (JourneyApiResponseDto flight in found)
                    {
                        List<JourneyApiResponseDto> journey = new() { flight };
                        if (flight.Destination != destination)
                        {
                            journey.AddRange(GetFlights(journey, flight.Destination, destination, ref result));
                        }
                    }
                }
                else
                {
                    _logger.Log(LogLevel.Warning, $"JourneyDomain - GetApiJourneysByOriginAndDestination - Origin:{origin} Destinations:{destination} - API journeys with origin {origin} not found");                    
                }
            }
            else
            {
                _logger.Log(LogLevel.Warning, $"JourneyDomain - GetApiJourneysByOriginAndDestination - Origin:{origin} Destinations:{destination} - API journeys not found.");                
            }
            _logger.Log(LogLevel.Information, $"JourneyDomain - GetApiJourneysByOriginAndDestination - Success - Origin: {origin}, Destination: {destination}");            
            return result;
        }

        private List<JourneyApiResponseDto> GetFlights(List<JourneyApiResponseDto> currentJourney, string origin, string destination, ref List<List<JourneyApiResponseDto>> fullJourneys)
        {
            List<JourneyApiResponseDto> result = new();
            List<string> currentOrigins = currentJourney.Select(j => j.Origin).ToList();            
            _logger.Log(LogLevel.Information, $"JourneyDomain - GetFlights - Success - Origin: {origin}, Destination: {destination}, Current origins: {@currentOrigins}", origin, destination, currentOrigins);

            if (_apiJourneys != null && _apiJourneys.Any())
            {
                List<JourneyApiResponseDto> found = _apiJourneys.Where(j => j.Origin == origin && !currentOrigins.Contains(j.Destination)).ToList();

                if (found != null && found.Any())
                {
                    foreach (JourneyApiResponseDto flight in found)
                    {
                        result.AddRange(currentJourney);
                        result.Add(flight);

                        if (flight.Destination != destination)
                        {
                            List<JourneyApiResponseDto> addFlights = GetFlights(result, flight.Destination, destination, ref fullJourneys);
                            if (addFlights == null || !addFlights.Any())
                            {
                                if (result.Last().Destination == destination)
                                {
                                    fullJourneys.Add(result);
                                }
                                result.Clear();
                            }
                            else
                            {
                                if (!result.Any())
                                {
                                    result.AddRange(currentJourney);
                                }
                            }
                        }
                        else
                        {
                            List<JourneyApiResponseDto> journeyToAdd = new();
                            journeyToAdd.AddRange(result);
                            if (journeyToAdd.Count <= _maxFlightCount)
                            {
                                fullJourneys.Add(journeyToAdd);
                            }
                            result.Clear();
                        }
                    }
                }
                else
                {
                    _logger.Log(LogLevel.Warning, $"JourneyDomain - GetFlights - Success - Origin: {origin}, Destination: {destination}, Current origins: {@currentOrigins} -- API Journeys with origin {origin} not found.", origin, destination, currentOrigins, origin);                    
                }
            }
            else
            {
                _logger.Log(LogLevel.Warning, $"JourneyDomain - GetFlights - Success - Origin: {origin}, Destination: {destination}, Current origins: {@currentOrigins}", origin, destination, currentOrigins);                
            }

            return result;

        }

        private JourneyDto GetJourneyDto(JourneyResponse journeyInfo)
        {
            _logger.Log(LogLevel.Information, $"JourneyDomain - GetJourneyDto - Success - Journey info: {journeyInfo}");
            JourneyDto result = new();

            try
            {
                result = new()
                {
                    Destination = journeyInfo.Destination,
                    Id = journeyInfo.Id,
                    Origin = journeyInfo.Origin,
                    Price = journeyInfo.Price,
                    IsDirectFlight = journeyInfo.IsDirectFlight,
                    IsRoundTripFlight = journeyInfo.IsRoundTripFlight,
                    Flights = new()
                };

                if (journeyInfo.Flights != null && journeyInfo.Flights.Any())
                {
                    journeyInfo.Flights.ForEach(flight =>
                    {
                        result.Flights.Add(new()
                        {
                            Id = flight.Id,
                            Destination = flight.Destination,
                            Origin = flight.Origin,
                            Price = flight.Price,
                            Transport = flight.Transport != null ? new() { Id = flight.TransportId, FlightCarrier = flight.Transport.FlightCarrier, FlightNumber = flight.Transport.FlightNumber } : new()
                        });
                    });
                }
                else
                {
                    _logger.Log(LogLevel.Warning, $"JourneyDomain - GetJourneyDto - Success - Journey not contains flights: {journeyInfo}");
                }

                _logger.Log(LogLevel.Information, $"JourneyDomain - GetJourneyDto, Success - Journey info: {journeyInfo}");
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("GetJourneyDto - Domain, Error - Journey info: {@journeyInfo}", ex, journeyInfo);
                throw;
            }

            return result;
        }

        private List<JourneyDto>? GetApiJourney(string origin, string destination)
        {
            _logger.Log(LogLevel.Information, $"JourneyDomain - GetApiJourney, Success: origin: {origin} destination: {destination}");
            List<JourneyDto>? result = null;

            try
            {
                if (_apiJourneys != null && _apiJourneys.Any())
                {
                    result = CalculateJourney(origin, destination);
                    if (result != null && result.Any())
                    {
                        result.ForEach(journey => SaveJourneyInfoFromApi(ref journey));
                    }
                }
                else
                {
                    _apiJourneys = JsonConvert.DeserializeObject<List<JourneyApiResponseDto>>(GetJourneyInfoFromApi().Result);
                    result = GetApiJourney(origin, destination);
                }
                _logger.Log(LogLevel.Information, $"JourneyDomain - GetApiJourney, Request Success - origin: {origin} destination: {destination}");
                return result;
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Domain Get Api Journey -- Error --> Origin: {@origin}, Destination: {@destination}", ex, origin, destination);
            }

            return result;
        }

        private async Task<List<JourneyDto>?> CheckJourneyExists(string origin, string destination)
        {
            _logger.Log(LogLevel.Information, $"JourneyDomain - CheckJourneyExists, Success: origin: {origin} destination: {destination}");
            List<JourneyDto>? result = null;

            try
            {
                List<JourneyResponse>? journeyList = _mediator.Send(new GetJourneyListByPlacesService() { Origin = origin, Destination = destination }).Result;
                if (journeyList != null && journeyList.Any())
                {
                    result = new();
                    journeyList.ForEach(journey =>
                    {
                        List<FlightResponse>? flightList = _mediator.Send(new GetFlightListByJourneyService() { JourneyId = journey.Id }).Result;
                        if (flightList != null && flightList.Any())
                        {
                            journey.Flights = new();
                            flightList.ForEach(flight =>
                            {
                                flight.Transport = _mediator.Send(new GetTransportByIdService() { Id = flight.TransportId }).Result;
                                journey.Flights.Add(flight);
                            });
                        }
                        result.Add(GetJourneyDto(journey));
                    });
                }
                else
                {
                    result = GetApiJourney(origin, destination);
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("JourneyDomain - Get Exists Journey -- Error --> Origin: {@origin}, Destination: {@destination}", ex, origin, destination);
                throw;
            }
            return result;
        }

    }
}

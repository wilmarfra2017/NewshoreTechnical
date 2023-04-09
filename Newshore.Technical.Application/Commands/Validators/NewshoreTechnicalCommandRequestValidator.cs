using FluentValidation;

namespace Newshore.Technical.Application.Commands.Validators
{
    public sealed class NewshoreTechnicalCommandRequestValidator : AbstractValidator<NewshoreTechnicalCommandRequest>
    {

        public NewshoreTechnicalCommandRequestValidator()
        {
            RuleFor(request => request.journeys.Origin)
                .NotEmpty()
                .WithMessage("El campo Origen no puede estar vacio");

            RuleFor(request => request.journeys.Destination)
                .NotEmpty()
                .WithMessage("El campo Destino no puede estar vacio");
        }

    }
}

# Prueba Técnica Newshore
Wilmar Francisco Martinez

# Objetivo
Microservicio que se encarga de calcular las rutas de viaje de acuerdo a la documentación de la prueba técnica

Se implementó microservicio con API expuesta para ver la ruta calculada de acuerdo a los parametros Origen y Destino enviados al microservicio.

# Instalación
1. Clonar repositorio
2. Crear desde **SQL Server Management Studio** Base de Datos: **Newshore.Technical.DataBase**
3. En la base de datos anterior ejecutar el Script **Tablas.sql** ubicado en: **NewshoreTechnical/Base de datos/Tablas.sql**
4. Ajustar cadena de conexión a Base de datos en archivo **appsettings.json**  ubicado en la capa Api del microservicio: **Newshore.Technical.Api**

# Implementación realizada
1. Base de datos de SQL Server
2. Microservicio en .NET Core 6 con capa de servicio (API), capa de aplicacion (CQRS), capa de dominio (entities, interfaces y services), 
capa de infraestructura (finders y repositories (persistencia a la base de datos SQL)), capa transversal (Dtos y Utils)

# Estructura del Microservicio:

- Arquitectura Hexagonal
- Manejo DDD (Domain Driven Design)
- Inyección de dependencias (Principio SOLID)
- MediatR para ejecución de querys y commands (CQRS)
- Manejo de Entity FrameworkCore
- Logs para excepcion de errores con SeriLog (manejo de Utils)
- Logs de aplicación con Microsoft.Extensions.Logging y SeriLog
- Implementación de pruebas unitarias con XUnit

3. Implementación de CORS (Cross-origin resource sharing): En la linea 52 del archivo **Program.cs** ubicado en la capa Api del microservicio: **Newshore.Technical.Api**
ajustar el origen es decir: **(URL de Aplicacion Front)**

**La implementacion de CORS la realicé, para que el proyecto basico de Front que construí, pueda realizar peticiones a la API expuesta por el Microservicio** en caso
que se desee probar en conjunto el back con el front


**Nota:** 
En archivo **appsettings.json** se configura la opción de **rutas multiples y de retorno**
y tambien se configura la cantidad maxima de viajes a utilizar en cada ruta (Usé el doble de este valor en las rutas de ida y vuelta)

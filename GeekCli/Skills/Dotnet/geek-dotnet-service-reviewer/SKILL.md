---
name: geek-dotnet-service-reviewer
description: Revisa interfaces y servicios .NET DB y MC, sus métodos de búsqueda, el manejo de resultados vacíos, el flujo de cache y su registro en la API.
---

# Revisar servicios .NET

Revisa archivos existentes sin modificarlos. Acepta una solución `.sln` o `.slnx`, un proyecto `*Services`, una carpeta de servicios o un archivo C#. Excluye `bin`, `obj` y archivos generados. Identifica interfaces y clases por sus declaraciones, no solo por el nombre del archivo. Si una solución contiene varios proyectos `*Services`, revisa todos; si un archivo declara varias clases de servicio, identifica cada una por separado. Informa el alcance buscado cuando no encuentres servicios; no declares correcta una revisión vacía.

Usa la skill vecina [`geek-dotnet-service`](../geek-dotnet-service/SKILL.md) y sus plantillas como referencias. Resuelve los contratos reales mediante el código del proyecto, sus dependencias o las clases base disponibles. No conviertas un comportamiento particular de una plantilla en una regla para todos los servicios.

## Contratos y registro

- Comprueba que la interfaz, `*DBService` y, para Entities, `*MCService` utilicen tipos, contratos y clases base coherentes con el modelo y su alcance.
- Comprueba que `*DBService` use el repositorio de lectura, la transformación a DTO y el recurso correspondiente. Para una View, verifica su contrato de solo lectura; no exijas `*MCService`, repositorio de escritura ni operaciones CRUD.
- Compara las operaciones propias declaradas en la interfaz con sus implementaciones DB y MC. Ten en cuenta los métodos heredados antes de declarar que falta una operación.
- Cuando pueda identificarse la API correspondiente, revisa el registro en `AddServices`: una Entity debe registrar el DB service y decorarlo con el MC service; una View debe registrar su DB service.
- En una revisión limitada a un archivo, no apliques controles que requieran inspeccionar todo el proyecto.

## Búsquedas en DBService

Revisa cada método de búsqueda implementado en `*DBService`, tanto si devuelve una colección como si devuelve un solo registro.

- Sigue el resultado devuelto por el repositorio de lectura. Antes de devolver una respuesta sin registros, el método debe lanzar `EntityException`. Para una colección, comprueba si está vacía; para un resultado individual, comprueba si no existe, según el contrato real del repositorio.
- Comprueba que la excepción use el mensaje del recurso adecuado al criterio de búsqueda.
- Comprueba los parámetros definidos en ese mensaje. Según el criterio y el texto del recurso, deben sustituirse por los valores de la búsqueda mediante `ReplaceCorporationInMessage`, `ReplaceCompanyInMessage` y `ReplaceValueInMessage`.
- No infieras los parámetros requeridos solo por el nombre del método. Consulta el recurso, el criterio de búsqueda y las llamadas efectivas.
- Si la búsqueda se implementa en una clase base, verifica su comportamiento únicamente cuando la implementación esté disponible. Si no lo está, informa el control como no verificable.

## Búsquedas en MCService

- Para cada búsqueda de colecciones implementada en `*MCService`, sigue el flujo completo: consultar primero la cache; devolver el resultado cuando exista; en caso contrario, consultar `I*Service`, guardar los valores obtenidos en la cache y devolverlos.
- Para una búsqueda de un solo registro, comprueba que se delegue directamente a `I*Service`, sin una consulta previa a la cache.
- Sigue los métodos auxiliares y los contratos heredados cuando sea necesario. No declares correcto el flujo basándote solo en que aparecen referencias a la cache y al servicio.
- La excepción de `*DBService` ante una búsqueda sin registros impide que el flujo MC guarde un resultado vacío. Señala el defecto en el punto donde se confirma y evita duplicar hallazgos por la misma causa.

## Códigos de hallazgo

| Código | Hallazgo |
| --- | --- |
| `GDSR001` | No se encontró una clase o interfaz de servicio en el alcance seleccionado. |
| `GDSR002` | La interfaz, el DB service o el MC service contradice el contrato, modelo o alcance comprobable. |
| `GDSR003` | Una operación propia de la interfaz no tiene la implementación DB o MC que le corresponde. |
| `GDSR004` | El registro comprobable en `AddServices` es incorrecto o está ausente. |
| `GDSR005` | Una búsqueda DB puede devolver un resultado sin registros sin lanzar `EntityException`. |
| `GDSR006` | La excepción usa un recurso incorrecto o no sustituye correctamente los parámetros del mensaje. |
| `GDSR007` | Una búsqueda MC de colecciones incumple el flujo de consulta, retorno, carga y guardado en cache. |
| `GDSR008` | Una búsqueda MC de un solo registro consulta la cache en vez de delegar directamente a `I*Service`. |

Reporta únicamente hallazgos sustentados por declaraciones y flujo de ejecución verificables. Si falta un contrato, recurso o implementación necesario para decidir, indica qué evidencia falta y marca ese control como no verificable. Evita hallazgos duplicados por un mismo defecto.

## Informe

Escribe el informe en el idioma del usuario. Comienza con una tabla de controles, estado y cantidad de hallazgos. Usa `Correcto`, `Hallazgos`, `No verificable` u `Omitido` cuando el usuario escriba en español, y traduce los estados a su idioma en otros casos; usa `—` si no puede determinarse una cantidad.

Enumera cada hallazgo como `GDSRnnn ruta:línea:columna — evidencia y corrección sugerida`, omitiendo coordenadas no disponibles. Agrupa los hallazgos por servicio. Termina con el alcance revisado, la cantidad de interfaces, DB services y MC services examinados, los hallazgos por código y el resultado general. No declares un resultado general correcto si algún control aplicable tiene hallazgos o sigue sin verificarse.

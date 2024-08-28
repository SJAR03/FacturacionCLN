# FacturacionCLN

## Prueba Técnica - Backend

Este proyecto es una API RESTful construida con ASP.NET Core para la gestión de facturación. Utiliza .NET 8.0 y está diseñado para funcionar con Microsoft SQL Server.

## Importante

Favor, también leer el otro archivo llamado Detalles.md (https://github.com/SJAR03/FacturacionCLN/blob/master/Detalles.md)

## Requisitos

- **ASP.NET Core Web API**
- **.NET 8.0**
- **Visual Studio 2022**
- **SQL Server**

## Paquetes NuGet

- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`

## Configuración de entity framework

Para configurar entity framework core, sigue estos pasos en la Consola del administrador de paquetes:

  1. **Crear y actualizar base de datos**  
  Ejecuta el siguiente comando para aplicar las migraciones y crear/actualizar la base de datos:
  ```bash
  Update-Database
  ```
  Si esta usando Visual Studio 2022, dirigirse a la pestaña de "View", luego "Other Windows" y donde dice "Package Manager Console". Ejecutar el comando en esa terminal (debe tener instalados los paquetes NuGet mencionados anteriormente)
  ![image](https://github.com/user-attachments/assets/9a1caac1-ad7f-4c01-ac19-dcc13fbe8f17)
  ![image](https://github.com/user-attachments/assets/a86de013-9bf4-42c3-988b-dbac059c269e)

## Reglas de Negocio
- Precisión de Decimales: Los precios y totales deben tener solo 2 decimales.
- Conversión de Precios: Los precios en córdobas y dólares deben estar correctamente convertidos. La conversión se basa en el precio en córdobas como referencia. En caso de ingresar solo precio córdoba, se hace la conversión del precio dólar automatico, viceversa si solo se ingresa el precio dólar.
- Tasa de Cambio: No se aceptan tasas de cambio negativas.

## Reglas API
- Detallado en el archivo de Detalles.md (https://github.com/SJAR03/FacturacionCLN/blob/master/Detalles.md)

## Cómo Levantar el Proyecto

1. **Clonar el Repositorio**  
   Clona el repositorio en tu máquina local:
  ```bash
  git clone <url-del-repositorio>
  ```
2. **Restaurar Paquetes**  
Navega al directorio del proyecto y restaura los paquetes NuGet:
  ```bash
  dotnet restore
  ```
3. **Construir el Proyecto**  
Construye el proyecto para asegurarte de que todo esté en orden:
  ```bash
  dotnet build
  ```
4. **Ejecutar el Proyecto**  
Ejecuta el proyecto en modo desarrollo:
  ```bash
  dotnet run
  ```
5. **Probar la API**  
- Utiliza una herramienta como Postman para probar los endpoints de la API en `http://localhost:7293` (o el puerto especificado en la configuración).
- Los pasos del 2 al 4 pueden saltarse y realizar el proceso directamente desde las opciones de Visual Studio 2022

### Notas adicionales

- Asegurarse de tener SQL Server en tu entorno de desarrollo o configurar la cadena de conexión adecuadamente en `appsettings.json`. (Solo cambiar el servidor al que apunta, el usuario y contraseña) Las pruebas hechas para probar esta API fueron hechas en un servidor local
- La base de datos se inicializa con una migración inicial, asegurarse de ejecutar el comando de migración después de clonar el repositorio.

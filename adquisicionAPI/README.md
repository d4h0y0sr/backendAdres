# AdquisicionAPI

AdquisicionAPI es una API RESTful desarrollada en .NET 9.0 para gestionar adquisiciones y su historial. Incluye funcionalidades para crear, actualizar, y consultar adquisiciones, así como registrar cambios en un historial.

## Características

- Gestión de adquisiciones con propiedades como presupuesto, valor unitario y valor total.
- Registro automático de cambios en el historial mediante triggers en la base de datos.
- Documentación de API generada con Swagger.
- Configuración de base de datos con Entity Framework Core.

## Requisitos

- .NET 9.0 SDK
- SQL Server 2019
- Visual Studio Code o cualquier IDE compatible con .NET

## Configuración

1. Clona este repositorio:
   ```bash
   git clone <URL_DEL_REPOSITORIO>
   cd adquisicionAPI
   ```
2. Configura la cadena de conexión en appsettings.json:
    ```
    {
    "ConnectionStrings": {
        "DefaultConnection": "Server=<SERVIDOR>;Database=<NOMBRE_BD>;User Id=<USUARIO>;Password=<CONTRASEÑA>;"
    }
    }
    ```
3. Restaura las dependencias:
    ```
    dotnet restore
    ```
4. Aplica las migraciones a la base de datos:
    ```
    dotnet ef database update
    ```
5. Ejecuta el proyecto:
    ```
    dotnet run
    ```    

## Endpoints
La documentación de los endpoints está disponible en Swagger. Una vez que la API esté en ejecución, accede a:
```
https://localhost:<PUERTO>/swagger
```
### Estructura del Proyecto

adquisicionAPI/
├── controllers/         # Controladores de la API
├── data/                # Configuración de Entity Framework y DbContext
├── models/              # Modelos de datos
├── Migrations/          # Migraciones de la base de datos
├── SqlScripts/          # Scripts SQL adicionales
├── [Program.cs]         # Configuración principal de la aplicación
├── [appsettings.json]   # Configuración de la aplicación

# Licencia
Este proyecto está bajo la licencia MIT.
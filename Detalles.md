# Detalles

## Clientes CRUD

- **GET** /api/Clientes: Devuelve todos los clientes
- **POST** /api/Clientes: Permite crear un nuevo cliente
  Usando la estructura siguiente,
```json
{
  "codigo": "420",
  "nombre": "Juan",
  "direccion": "De canal 10, 1 cuadra al lago",
  "telefono": "82647854",
  "email": "juan@example.com",
  "codigoPais": "505"
}
```
- **GET** /api/Clientes/{id}: Devuelve cliente por id
- **PUT** /api/Clientes/{id}: Permite modificar un cliente especifico por id
  Usando la estructura siguiente (se necesita especificar el id),
  
```json
{
  "id": 4,
  "codigo": "420",
  "nombre": "Juan Perez",
  "direccion": "De canal 10, 1 cuadra al lago",
  "telefono": "82647854",
  "email": "juan@example.com",
  "codigoPais": "505"
}
```

- **DELETE** /api/Clientes/{id}: Permite eliminar un cliente especifico por id
- **GET** /api/Clientes/search: Permite buscar clientes por coincidencias de nombre y codigo.

## Tasa cambio CRUD

- **GET** /api/TasaCambios: Devuelve todas las tasas de cambio
- **POST** /api/TasaCambios: Permite crear una nueva tasa de cambio, la tasa no puede ser negativa y debe ir en formato "yyyy-MM-d"
  Usando la estructura siguiente,
```json
{
  "tasa": 36.62,
  "fecha": "2024-08-28"
}
```

- **GET** /api/TasaCambios/{id}: Devuelve la tasa de cambio por id
- **PUT** /api/TasaCambios/{id}: Permite modificar una tasa de cambio por especifico por id
  Usando la estructura siguiente,
```json
{
  "id": 7,
  "tasa": 36.75,
  "fecha": "2024-09-03"
}
```
- **DELETE** /api/TasaCambios/{id}: Permite eliminar una tasa de cambio en especifico por id
- **GET** /api/TasaCambios/diaria: Devuelve la tasa de cambio del día actual
- **GET** /api/TasaCambios/mes/{year}/{month}: Devuelve la tasa de cambio por mes, se debe pasar el año y mes especifico

## Productos CRUD

- **GET** /api/Productos: Devuelve todos los productos
- **POST** /api/Productos: Permite crear un nuevo producto
  1) Si solamente se facilita el precioCordoba, el API calcula el precioDolar usando la tasa de cambio del día
  2) Si solamente se facilita el precioDolar, el API calcula el precioCordoba usando la tasa de cambio del día
  3) Si se facilitan ambos precios, el API valida que la conversión sea correcta, usando como base el precioCordoba y la tasa de cambio del día
```json
{
  "sku": "string",
  "descripcion": "string",
  "precioCordoba": 0,
  "precioDolar": 0,
  "activo": true
}
```

- **GET** /api/Productos/{id}: Devuelve productos por id
- **PUT** /api/Productos/{id}: Permite modificar un producto especifico por id
  Usando la estructura siguiente (se necesita especificar el id),
```json
{
  "id": 0,
  "sku": "string",
  "descripcion": "string",
  "precioCordoba": 0,
  "precioDolar": 0,
  "activo": true
}
```
- **DELETE** /api/Producto/{id}: Permite eliminar un producto especifico por id
- **GET** /api/Producto/search: Permite buscar productos por coincidencias de sku y descripcion.

## Factura CRUD

- **GET** /api/Facturas: Devuelve todas las facturas
- **GET** /api/Facturas/buscar/{criterio}: Permite buscar facturas por coincidencias de id de factura y id de cliente (solamente 1 criterio a la vez, por cuestión de tiempo, hizo falta agregar buscar por ambos parametros por separados)
- **POST** /api/Facturas: Permite crear una nueva factura junto a sus detalles
La estructura original seria la siguiente:
```json
{
  "idFactura": 0,
  "idCliente": 0,
  "nombreCliente": "string",
  "fecha": "2024-08-28T14:17:40.405Z",
  "subTotalCordoba": 0,
  "ivaCordoba": 0,
  "montoTotalCordoba": 0,
  "subTotalDolar": 0,
  "ivaDolar": 0,
  "montoTotalDolar": 0,
  "detallesFactura": [
  {
      "idProducto": 0,
      "cantidad": 0,
      "precioUnitarioCordoba": 0,
      "precioUnitarioDolar": 0
    }
  ]
}
```

Sin embargo, para facturar a un cliente y agregar productos que se compran, se debe usar la siguiente,
```json
{
  "idCliente": 0,
  "fecha": "2024-08-28",
  "detallesFactura": [
    {
      "idProducto": 0,
      "cantidad": 0
    }
  ]
}
```

Para agregar varios productos, solamente se añaden nuevos elementos de productos seguidos de 1 coma,
```json
{
  "idCliente": 0,
  "fecha": "2024-08-28",
  "detallesFactura": [
    {
      "idProducto": 1,
      "cantidad": 3
    },
    {
      "idProducto": 2,
      "cantidad": 1
    }
  ]
}
```

*El calculo de los montos totales e IVA tanto en cordobas como en dolar, se calculan en base a la cantidad de productos y el precio unitario que el producto tiene registrado en la base de datos en ese momento.*
- a



## Comentario finales
El API proporcionada por mi persona cumple con todos los requisitos solicitados según mi propio criterio y abstración del problema a resolver.

Sin embargo, por cuestiones de tiempo, no logré finalizar el codigo completo a como a mi me gustaría, aplicando una estructura más ordenada y siguiendo las mejores prácticas (por ejemplo, el CRUD de clientes y tasa de cambio si me dio tiempo a refactorizar, pero en cambio factura, el componente más pesado, no tuve tiempo de hacerlo, pero aun con todo y eso, el componente cumple con las funcionalidades requeridas).

La base de datos se crea directamente usando update-database y aplicando las migraciones de entity framework desde la consola de administrador de paquetes, como lo muestro en el README.md. Recordar cambiar la conexion a la base de datos en el appsettings.json. Igualmente, proporciono un archivo llamadp schemaCLN.sql para crear el esquema de la base de datos directamente.

Ademas, recordar que es necesario instalar los paquetes de entity framework detallados en el README.md

Asimismo, facilitare en el repositorio, un pequeño archivo llamado dataCLN.sql, que inserta unos cuantos registros de meramente prueba para la base de datos, se debe ejecutar despues de realizar el update-database o de haber creado la base de datos directamente.

Utilice ASP NET CORE con la versión 8 ed .NET, por lo cual necesitaran el SDK, en caso de no tenerlo ya instalado, deben descargarlo e instalar para poder levantar el proyecto.

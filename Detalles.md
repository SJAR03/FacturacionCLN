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





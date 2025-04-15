Dejo en el proyecto la coleccion a ejecutar. 

1) Deben asegurarse que las url coincidan.

2) ejecutar las migraciones de la base de datos, cambiar la cadena de conexion para que coincida con las pruebas a ejecutar.

3) Implementacion:

  A)El flujo consiste en generar un user nuevo con DNI como input.

- **Crear Usuario**
  - Método: POST
  - Ruta: `/Users`
  - Cuerpo de la solicitud:
    ```json
    {
      "DNI": "12345678A"
    }
    ```
  - Respuesta exitosa:
    ```json
    {
      "userId": "guid-del-usuario"
    }
    ```
  
  B)Luego crear una lista de product para tener un buen pool para probar.

- **Crear Producto**
  - Método: POST
  - Ruta: `/Products`
  - Cuerpo de la solicitud:
    ```json
    {
      "name": "Nombre del Producto",
      "price": 99.99
    }
    ```
  - Respuesta exitosa:
    ```json
    {
      "productId": "guid-del-producto"
    }
    ```

  C)Luego crear un cart para empezar una compra

- **Crear Carrito**
  - Método: POST
  - Ruta: `/Carts`
  - Cuerpo de la solicitud:
    ```json
    {
      "DNI": "12345678A"
    }
    ```
  - Respuesta exitosa:
    ```json
    {
      "cartId": "guid-del-carrito"
    }
    ```

  D)Para concluir se puede agregar productos.

  - **Agregar Producto al Carrito**
  - Método: POST
  - Ruta: `/Carts/AddProduct`
  - Cuerpo de la solicitud:
    ```json
    {
      "cartId": "guid-del-carrito",
      "productId": "guid-del-producto"
    }
    ```
  - Respuesta exitosa:
    ```json
    {
      "id": "guid-del-carrito",
      "products": [
        {
          "name": "Producto 1",
          "price": 99.99
        }
      ],
      "total": 99.99
    }
    ```

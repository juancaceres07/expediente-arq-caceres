Detecciones SOLID - Variante A Comedor Universitario


S-- (Single Responsibility): `GestorDePedidos` `ProcesarPedido`  Tiene demasiadas responsabilidades: calcula precios, guarda en BD, imprime el vale y envia correos. 
O-- (Open/Closed): `GestorDePedidos` `ProcesarPedido` Usa un `switch` para determinar el precio segun el tipo de menu. Si agregamos un nuevo menu obliga a modificar el codigo existente. 
D-- (Dependency Inversion):`GestorDePedidos` `ProcesarPedido` Instancia directamente las clases concretas (`new BaseDeDatosComedor()` y `new CorreoUniversitario()`) en lugar de depender de interfaces. 
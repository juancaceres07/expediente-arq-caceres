S-- GestorDePedidos.ProcesarPedido:  El método hace de todo: calcula total, guarda en BD, imprime y envía correo. 

O-- GestorDePedidos.ProcesarPedido : Tiene un switch para el tipo de cliente. Si llega un cliente nuevo, hay que editar el código.

L / I-- IEmpleadoDeFarmacia y Cajero : Cajero implementa una interfaz con cosas que no hace y solo lanza NotSupportedException. 

D-- GestorDePedidos.ProcesarPedido : Instancia las clases directas con new (BaseDeDatosMySql y CorreoSmtp) en lugar de usar interfaces.




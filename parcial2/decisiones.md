Parcial 2 - JUAN ANTONIO CÁCERES RAMIREZ

Situacion 1
Patron - Observer
Porque cuando vence una membresia hay que avisar a varios lados al mismo tiempo. Al no usar Observer, cada vez que al dueño se le ocurra ingresar un nuevo aviso, tendria que modificar el codigo principal de socios para agregar otra llamada. Eso terminaria modificando el modulo y haciendo que cualquier cambio rompa lo que ya funcionaba.

Situación 2
Patron - Strategy
Porque la forma de cobrar cambia segun la hora y todo esta en un if/else copiado en dos partes. Al no aplicar Strategy, cada vez que el dueño cambie los precios por temporada tendría que buscar y modificar esos ifs en varios archivos corriendo el riesgo de equivocarme en un lado y cobrar mal en otro.

Situación 3
Patrón - Adapter
Porque el sistema de pagos externo habla otro idioma que no coincide con nuestro sistema. Si no uso Adapter, tendria que adaptar todo el codigo al formato del proveedor. Y si el año que viene cambiamos el cobro de comisiones, tendria que rehacer el sistema casi desde cero en vez de solo cambiar el adaptador.

---

principo SOLID
Principio de Abierto/Cerrado 
En solucion.cs, la clase `CalculadorDeTarifa` usa la interfaz `ITarifaEstrategia`. Si mañana el dueño pide una nueva tarifa para feriados, no tengo que tocar ni modificar la clase `CalculadorDeTarifa`, solo crearia una nueva clase con la nueva regla y listo.
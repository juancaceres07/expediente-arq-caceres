¿Por qué NO apliqué el patrón Singleton?

El patrón Singleton obliga a que exista una sola copia de una clase en todo el programa. Aunque en algunos casos sirve, en este sistema de gestión de docentes (SISEP) no conviene usarlo.

Razones:
1. Cada docente tiene su propio CI, sueldo y bonos. Si usara una sola instancia compartida para procesar a todos los datos de un profesor podrían sobrescribirse o mezclarse con los de otro si se consultan al mismo tiempo.

2. En el trabajo anterior (H2) separé las clases mediante interfaces (`ICalculadorBono`, `INotificadorBoleta`, etc.) El patrón Singleton conecta el código de forma muy rígida lo que haría complicado cambiar una pieza después sin arruinar el resto.

Conclusión: Es mejor instanciar cada objeto cuando se necesita, garantizando que la información de cada docente se maneje de forma limpia, independiente y sin errores.


 Aplicación de Patrón de Diseño

1. Requerimiento del Comedor: Cuando un pedido queda preparado el estudiante debe recibir un aviso.
2. Patrón Aplicado: Observer.
3. Justificacion: Con el patron: Notifica de forma automatica cuando el `Pedido` pasa a "preparado", sin acoplar la clase Pedido con la logica de envio de notificaciones.

Sin el patron: La clase Pedido tendria que saber como enviar cada notificacion por su cuenta. Si mañana se quiere avisar tambien por una app móvil, habria que modificar directamente el codigo de Pedido pro cambiando el diseño.

4. Diseño 

public interface IObservador
{
    void Notificar(string mensaje);
}

public class EstudianteObservador : IObservador
{
    private string _nombre;
    public EstudianteObservador(string nombre) => _nombre = nombre;

    public void Notificar(string mensaje) 
        => Console.WriteLine($"Aviso a {_nombre}: {mensaje}");
}

public class Pedido
{
    private List<IObservador> _observadores = new();
    public string Estado { get; private set; } = "Solicitado";

    public void AgregarObservador(IObservador obs) => _observadores.Add(obs);

    public void CambiarEstado(string nuevoEstado)
    {
        Estado = nuevoEstado;
        if (Estado == "preparado")
        {
            foreach (var obs in _observadores)
            {
                obs.Notificar("Tu pedido está listo y preparado para retirar.");
            }
        }
    }
}
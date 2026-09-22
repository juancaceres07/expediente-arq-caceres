// Refactor: JUAN ANTONIO CÁCERES RAMIREZ

namespace Integradora.Comedor;

public interface IBaseDeDatos
{
    void GuardarPedido(string estudiante, string menu, int cantidad, decimal total);
}

public interface INotificador
{
    void Enviar(string mensaje);
}

public class BaseDeDatosComedor : IBaseDeDatos
{
    public void GuardarPedido(string estudiante, string menu, int cantidad, decimal total)
        => Console.WriteLine($"[BD] INSERT INTO pedidos VALUES ('{estudiante}', '{menu}', {cantidad}, {total})");
}

public class CorreoUniversitario : INotificador
{
    public void Enviar(string mensaje) => Console.WriteLine($"[CORREO] {mensaje}");
}

public class GestorDePedidos
{
    private readonly IBaseDeDatos _bd;
    private readonly INotificador _notificador;

    public GestorDePedidos(IBaseDeDatos bd, INotificador notificador)
    {
        _bd = bd;
        _notificador = notificador;
    }

    public void ProcesarPedido(string estudiante, string tipoMenu, int cantidad)
    {
        decimal precioBase = tipoMenu switch
        {
            "vegetariano" => 14,
            "beca" => 5,
            _ => 12
        };

        decimal total = precioBase * cantidad;

        _bd.GuardarPedido(estudiante, tipoMenu, cantidad, total);

        Console.WriteLine("----- VALE DE COMEDOR -----");
        Console.WriteLine($"{estudiante}: {cantidad} x menú {tipoMenu}");
        Console.WriteLine($"TOTAL: {total:0.00} Bs");

        _notificador.Enviar($"Pedido registrado: {cantidad} x {tipoMenu}, {estudiante}");
    }
}

public static class Demo
{
    public static void Correr()
    {
        IBaseDeDatos bd = new BaseDeDatosComedor();
        INotificador correo = new CorreoUniversitario();

        var gestor = new GestorDePedidos(bd, correo);
        gestor.ProcesarPedido("Noelia", "vegetariano", 2);
    }
}
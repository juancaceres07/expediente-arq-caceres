// Refactor: JUAN ANTONIO CÁCERES RAMIREZ 

namespace Parcial1.Farmacia;

// interfaces separads por roles
public interface IPedido
{
    void RegistrarPedido(string medicamento, int cantidad);
}

public interface IControlado
{
    void AutorizarVentaControlada(string medicamento);
    void VerLibroDeControlados();
}

public interface IPrecio
{
    void AjustarPrecio(string medicamento, decimal nuevoPrecio);
}

//el cajero
public class Cajero : IPedido
{
    public void RegistrarPedido(string medicamento, int cantidad)
    {
        Console.WriteLine($"[CAJA] Pedido: {cantidad} x {medicamento}");
    }
}

// el farmaceutico 
public class Farmaceutico : IPedido, IControlado, IPrecio
{
    public void RegistrarPedido(string medicamento, int cantidad)
    {
        Console.WriteLine($"[FARM] Pedido: {cantidad} x {medicamento}");
    }

    public void AutorizarVentaControlada(string medicamento)
    {
        Console.WriteLine($"[FARM] Venta controlada de {medicamento} autorizada");
    }

    public void AjustarPrecio(string medicamento, decimal nuevoPrecio)
    {
        Console.WriteLine($"[FARM] {medicamento} ahora cuesta {nuevoPrecio:0.00} Bs");
    }

    public void VerLibroDeControlados()
    {
        Console.WriteLine("[FARM] Libro de medicamentos controlados");
    }
}

// interfaces para BD y correo
public interface IPedidoRepositorio
{
    void GuardarPedido(string cliente, string medicamento, int cantidad, decimal total);
}

public interface IEmail
{
    void Enviar(string mensaje);
}

public class BaseDeDatosMySql : IPedidoRepositorio
{
    public void GuardarPedido(string cliente, string medicamento, int cantidad, decimal total)
    {
        Console.WriteLine($"[MYSQL] INSERT INTO pedidos VALUES ('{cliente}', '{medicamento}', {cantidad}, {total})");
    }
}

public class CorreoSmtp : IEmail
{
    public void Enviar(string mensaje)
    {
        Console.WriteLine($"[SMTP] {mensaje}");
    }
}

public class GestorDePedidos
{
    private IPedidoRepositorio repo;
    private IEmail email;

    public GestorDePedidos(IPedidoRepositorio repo, IEmail email)
    {
        this.repo = repo;
        this.email = email;
    }

    public void ProcesarPedido(string cliente, string tipoCliente, string medicamento, int cantidad, decimal precioUnitario)
    {
        decimal total = cantidad * precioUnitario;

        decimal descuento = 0;
        if (tipoCliente == "asegurado")
        {
            descuento = total * 0.20m;
        }
        else if (tipoCliente == "convenio")
        {
            descuento = total * 0.10m;
        }

        decimal totalFinal = total - descuento;

        repo.GuardarPedido(cliente, medicamento, cantidad, totalFinal);

        Console.WriteLine("----- COMPROBANTE -----");
        Console.WriteLine($"{cantidad} x {medicamento}");
        Console.WriteLine($"Cliente: {cliente} ({tipoCliente})");
        Console.WriteLine($"TOTAL: {totalFinal:0.00} Bs");

        email.Enviar($"Su pedido de {medicamento} fue registrado, {cliente}");
    }
}
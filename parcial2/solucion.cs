// Solucion: JUAN ANTONIO CÁCERES RAMIREZ

namespace Parcial2.Gimnasio;


public interface ITarifaEstrategia
{
    decimal CalcularPrecio(decimal precioBase, int horas);
}


public class TarifaManana : ITarifaEstrategia
{
    public decimal CalcularPrecio(decimal precioBase, int horas)
    {
        return precioBase * horas;
    }
}

public class TarifaNoche : ITarifaEstrategia
{
    public decimal CalcularPrecio(decimal precioBase, int horas)
    {
        return (precioBase * horas) * 1.20m;
    }
}

public class CalculadorDeTarifa
{
    private ITarifaEstrategia tarifa;

    public CalculadorDeTarifa(ITarifaEstrategia tarifa)
    {
        this.tarifa = tarifa;
    }

    public decimal ObtenerTotal(decimal precioBase, int horas)
    {
        return tarifa.CalcularPrecio(precioBase, horas);
    }
}

public class Program
{
    public static void Main()
    {
        decimal precioHora = 20.00m; 

        var cobroManana = new CalculadorDeTarifa(new TarifaManana());
        Console.WriteLine($"Total mañana: {cobroManana.ObtenerTotal(precioHora, 2)} Bs");

        var cobroNoche = new CalculadorDeTarifa(new TarifaNoche());
        Console.WriteLine($"Total noche: {cobroNoche.ObtenerTotal(precioHora, 2)} Bs");
    }
}
using System;

namespace SISEP.Decorator
{
    public interface ILiquidablePorItem
    {
        double CalcularMonto();
        string ObtenerDetalle();
    }

    public class LiquidacionBase : ILiquidablePorItem
    {
        private readonly double _haberBasico;

        public LiquidacionBase(double haberBasico)
        {
            _haberBasico = haberBasico;
        }

        public double CalcularMonto() => _haberBasico;
        public string ObtenerDetalle() => "Sueldo Básico";
    }

    public abstract class DecoradorBono : ILiquidablePorItem
    {
        protected ILiquidablePorItem _componente;

        public DecoradorBono(ILiquidablePorItem componente)
        {
            _componente = componente;
        }

        public virtual double CalcularMonto() => _componente.CalcularMonto();
        public virtual string ObtenerDetalle() => _componente.ObtenerDetalle();
    }

    public class DecoradorZonaFrontera : DecoradorBono
    {
        public DecoradorZonaFrontera(ILiquidablePorItem componente) : base(componente) { }

        public override double CalcularMonto() => base.CalcularMonto() + 500;
        public override string ObtenerDetalle() => base.ObtenerDetalle() + " + Bono Zona Frontera/Rural";
    }

    class Program
    {
        static void Main(string[] args)
        {
            ILiquidablePorItem liquidacion = new LiquidacionBase(5000);
            liquidacion = new DecoradorZonaFrontera(liquidacion);

            Console.WriteLine($"[DECORATOR] Detalle: {liquidacion.ObtenerDetalle()}");
            Console.WriteLine($"[DECORATOR] Total a pagar: {liquidacion.CalcularMonto()} BS.");
        }
    }
}
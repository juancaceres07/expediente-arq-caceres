using System;

namespace SISEP.Strategy
{
        public abstract class BeneficiarioDocenteBase
    {
        public string DocumentoCI { get; set; }
        public string Rda { get; set; }
        public string NombreCompleto { get; set; }
        public double HaberBasico { get; set; }
    }

    public class DocenteTitular : BeneficiarioDocenteBase { }
    public class DocenteInterino : BeneficiarioDocenteBase { }

    
    public interface ICalculadorBono
    {
        double CalcularMontoTotal(double haberBasico);
    }

    public class CalculadorBonoTitular : ICalculadorBono
    {
        public double CalcularMontoTotal(double haberBasico)
        {
            return haberBasico + (haberBasico * 0.20); 
        }
    }

    public class CalculadorBonoInterino : ICalculadorBono
    {
        public double CalcularMontoTotal(double haberBasico)
        {
            return haberBasico; 
    }

    public class ServicioNegocioDocente
    {
        private ICalculadorBono _calculadorBono;

        public void EstablecerEstrategia(ICalculadorBono calculador)
        {
            _calculadorBono = calculador;
        }

        public double ProcesarCálculo(BeneficiarioDocenteBase docente)
        {
            return _calculadorBono.CalcularMontoTotal(docente.HaberBasico);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DocenteTitular titular = new DocenteTitular { NombreCompleto = "Juan Pérez", HaberBasico = 5000 };
            DocenteInterino interino = new DocenteInterino { NombreCompleto = "María López", HaberBasico = 5000 };

            ServicioNegocioDocente servicio = new ServicioNegocioDocente();

            servicio.EstablecerEstrategia(new CalculadorBonoTitular());
            Console.WriteLine($"[STRATEGY] {titular.NombreCompleto} (Titular): {servicio.ProcesarCálculo(titular)} BS.");

            servicio.EstablecerEstrategia(new CalculadorBonoInterino());
            Console.WriteLine($"[STRATEGY] {interino.NombreCompleto} (Interino): {servicio.ProcesarCálculo(interino)} BS.");
        }
    }
    }
}
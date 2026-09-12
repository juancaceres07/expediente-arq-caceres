using System;

namespace SISEP.Builder
{
    public interface ICalculadorBono 
    { 
        double CalcularBono(object docente); 
    }

    public interface INotificadorBoleta 
    { 
        void EnviarBoleta(string docenteId, string archivo); 
    }

    public class DocenteRepositoryData { }

    // implementacion
    public class CalculadorBonoTitular : ICalculadorBono
    {
        public double CalcularBono(object docente) => 1500.0;
    }

    public class EnviadorCorreoReal : INotificadorBoleta
    {
        public void EnviarBoleta(string docenteId, string archivo) => 
            Console.WriteLine($"[CORREO] Boleta enviada al docente {docenteId}.");
    }

    

    public class ServicioNegocioDocente
    {
        public ICalculadorBono CalculadorBono { get; set; }
        public INotificadorBoleta Notificador { get; set; }
        public DocenteRepositoryData Repositorio { get; set; }

        public void Procesar()
        {
            Console.WriteLine("[BUILDER] ServicioNegocioDocente construido y configurado correctamente.");
        }
    }

    // patron builder
    public class ConstructorServicioDocente
    {
        private readonly ServicioNegocioDocente _servicio = new ServicioNegocioDocente();

        public ConstructorServicioDocente ConCalculadorBono(ICalculadorBono calculador)
        {
            _servicio.CalculadorBono = calculador;
            return this;
        }

        public ConstructorServicioDocente ConNotificador(INotificadorBoleta notificador)
        {
            _servicio.Notificador = notificador;
            return this;
        }

        public ConstructorServicioDocente ConRepositorio(DocenteRepositoryData repositorio)
        {
            _servicio.Repositorio = repositorio;
            return this;
        }

        public ServicioNegocioDocente Construir() => _servicio;
    }

    

    class Program
    {
        static void Main(string[] args)
        {
            
            ServicioNegocioDocente servicio = new ConstructorServicioDocente()
                .ConCalculadorBono(new CalculadorBonoTitular())
                .ConNotificador(new EnviadorCorreoReal())
                .ConRepositorio(new DocenteRepositoryData())
                .Construir();

            servicio.Procesar();
        }
    }
}
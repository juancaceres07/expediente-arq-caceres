using System;
using System.Collections.Generic;

namespace SISEP.Final
{
    // BASE 
    
    public abstract class BeneficiarioDocenteBase
    {
        public string DocumentoCI { get; set; }
        public string Rda { get; set; }
        public string NombreCompleto { get; set; }
        public double HaberBasico { get; set; }
    }

    public class DocenteTitular : BeneficiarioDocenteBase { }
    public class DocenteInterino : BeneficiarioDocenteBase { }

    // STRATEGY 
    
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
    }

    
    // OBSERVER 
    
    public interface INotificadorBoleta
    {
        void EnviarBoleta(string docenteId, string mensaje);
    }

    public class EnviadorCorreoReal : INotificadorBoleta
    {
        public void EnviarBoleta(string docenteId, string mensaje)
        {
            Console.WriteLine($"[NOTIFICACIÓN CORREO]: {mensaje} para el RDA {docenteId}");
        }
    }

    public class EnviadorSistemaAudit : INotificadorBoleta
    {
        public void EnviarBoleta(string docenteId, string mensaje)
        {
            Console.WriteLine($"[NOTIFICACIÓN AUDITORÍA]: Registrado envío para RDA {docenteId}");
        }
    }

    // FUSIÓN
        public class ServicioNegocioDocente
    {
        private ICalculadorBono _calculadorBono;
        private readonly List<INotificadorBoleta> _notificadores = new List<INotificadorBoleta>();

        public void EstablecerEstrategia(ICalculadorBono calculador)
        {
            _calculadorBono = calculador;
        }

        public void AgregarNotificador(INotificadorBoleta notificador)
        {
            _notificadores.Add(notificador);
        }

        public bool ValidarRequisitos(BeneficiarioDocenteBase docente)
        {
            return !string.IsNullOrEmpty(docente.Rda);
        }

        public void ProcesarPlanillaDocente(BeneficiarioDocenteBase docente)
        {
            if (!ValidarRequisitos(docente))
            {
                Console.WriteLine($"[ERROR] Docente {docente.NombreCompleto} no cuenta con RDA válido.");
                return;
            }

          
            double totalCalculado = _calculadorBono.CalcularMontoTotal(docente.HaberBasico);

            string detalle = $"Boleta de {docente.NombreCompleto} procesada exitosamente. Total Líquido: {totalCalculado} BS.";

            
            foreach (var notificador in _notificadores)
            {
                notificador.EnviarBoleta(docente.Rda, detalle);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA SISEP - FUSIÓN DE PATRONES (STRATEGY + OBSERVER) ===\n");

            ServicioNegocioDocente servicio = new ServicioNegocioDocente();
            servicio.AgregarNotificador(new EnviadorCorreoReal());
            servicio.AgregarNotificador(new EnviadorSistemaAudit());

            
            DocenteTitular titular = new DocenteTitular
            {
                DocumentoCI = "123456",
                Rda = "RDA-99",
                NombreCompleto = "Juan Pérez",
                HaberBasico = 5000.00
            };

            Console.WriteLine("--- Procesando Docente Titular ---");
            servicio.EstablecerEstrategia(new CalculadorBonoTitular());
            servicio.ProcesarPlanillaDocente(titular);

            Console.WriteLine();

            
            DocenteInterino interino = new DocenteInterino
            {
                DocumentoCI = "789012",
                Rda = "RDA-105",
                NombreCompleto = "María López",
                HaberBasico = 5000.00
            };

            Console.WriteLine("--- Procesando Docente Interino ---");
            servicio.EstablecerEstrategia(new CalculadorBonoInterino());
            servicio.ProcesarPlanillaDocente(interino);
        }
    }
}
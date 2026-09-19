using System;
using System.Collections.Generic;

namespace SISEP.Observer
{
    public abstract class BeneficiarioDocenteBase
    {
        public string DocumentoCI { get; set; }
        public string Rda { get; set; }
        public string NombreCompleto { get; set; }
    }

    public class DocenteTitular : BeneficiarioDocenteBase { }

    
    public interface INotificadorBoleta
    {
        void EnviarBoleta(string docenteId, string mensaje);
    }

    public class EnviadorCorreoReal : INotificadorBoleta
    {
        public void EnviarBoleta(string docenteId, string mensaje)
        {
            Console.WriteLine($"[CORREO ENVIADO] Docente ID: {docenteId} | Mensaje: {mensaje}");
        }
    }

    public class EnviadorSistemaAudit : INotificadorBoleta
    {
        public void EnviarBoleta(string docenteId, string mensaje)
        {
            Console.WriteLine($"[AUDITORÍA REGISTRADA] Registro para ID: {docenteId}");
        }
    }

    public class ServicioNegocioDocente
    {
        private readonly List<INotificadorBoleta> _notificadores = new List<INotificadorBoleta>();

        public void AgregarNotificador(INotificadorBoleta notificador)
        {
            _notificadores.Add(notificador);
        }

        public void EmitirBoleta(BeneficiarioDocenteBase docente)
        {
            string mensaje = $"Boleta de haber generada para {docente.NombreCompleto}";
            foreach (var obs in _notificadores)
            {
                obs.EnviarBoleta(docente.Rda, mensaje);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DocenteTitular docente = new DocenteTitular { Rda = "RDA-99", NombreCompleto = "Juan Pérez" };

            ServicioNegocioDocente servicio = new ServicioNegocioDocente();
            servicio.AgregarNotificador(new EnviadorCorreoReal());
            servicio.AgregarNotificador(new EnviadorSistemaAudit());

            servicio.EmitirBoleta(docente);
        }
    }
}
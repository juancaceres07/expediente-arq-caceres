using System;

namespace SISEP.Base
{
    public abstract class BeneficiarioDocenteBase
    {
        public string DocumentoCI { get; set; }
        public string Rda { get; set; }
        public string NombreCompleto { get; set; }

        public virtual void ConsultarRDA()
        {
            Console.WriteLine($"[BASE] Consultando RDA: {Rda} de {NombreCompleto}");
        }
    }

    public class DocenteTitular : BeneficiarioDocenteBase { }
    public class DocenteInterino : BeneficiarioDocenteBase { }

    public class ServicioNegocioDocente
    {
        public bool ValidarRequisitos(BeneficiarioDocenteBase docente)
        {
            return !string.IsNullOrEmpty(docente.Rda);
        }

        public string CalcularEstadoLaboral(BeneficiarioDocenteBase docente)
        {
            return "ACTIVO";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DocenteTitular docente = new DocenteTitular 
            { 
                DocumentoCI = "123456", 
                Rda = "RDA-99", 
                NombreCompleto = "Juan Pérez" 
            };
            
            ServicioNegocioDocente servicio = new ServicioNegocioDocente();
            Console.WriteLine($"[BASE] Validación de {docente.NombreCompleto}: {servicio.ValidarRequisitos(docente)}");
        }
    }
}
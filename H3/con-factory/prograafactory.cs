using System;

namespace SISEP.Factory
{
    
    public abstract class BeneficiarioDocenteBase
    {
        public string DocumentoCI { get; set; }
        public string Rda { get; set; }
        public string NombreCompleto { get; set; }
    }

    public class DocenteTitular : BeneficiarioDocenteBase { }
    public class DocenteInterino : BeneficiarioDocenteBase { }

    //patron facory

    public abstract class CreadorDocente
    {
        public abstract BeneficiarioDocenteBase CrearDocente(string ci, string rda, string nombre);
    }

    public class CreadorDocenteTitular : CreadorDocente
    {
        public override BeneficiarioDocenteBase CrearDocente(string ci, string rda, string nombre) =>
            new DocenteTitular { DocumentoCI = ci, Rda = rda, NombreCompleto = nombre };
    }

    public class CreadorDocenteInterino : CreadorDocente
    {
        public override BeneficiarioDocenteBase CrearDocente(string ci, string rda, string nombre) =>
            new DocenteInterino { DocumentoCI = ci, Rda = rda, NombreCompleto = nombre };
    }


    class Program
    {
        static void Main(string[] args)
        {
            // Usamos el creador específico para instanciar al titular
            CreadorDocente creador = new CreadorDocenteTitular();
            BeneficiarioDocenteBase docente = creador.CrearDocente("654321", "RDA-88", "María López");
            
            Console.WriteLine($"[FACTORY] Docente registrado: {docente.NombreCompleto} (CI: {docente.DocumentoCI})");
        }
    }
}
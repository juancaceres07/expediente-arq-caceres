using System;

namespace SISEP.Adapter
{
    // ministerio
    public class ServicioRDAMinisterioLegado
    {
        public string ObtenerRespuestaXML(string ci)
        {
            return $"<respuesta><ci>{ci}</ci><estado>VALIDO</estado></respuesta>";
        }
    }

    // contratop or el sisep
    public interface ILectorPlanilla
    {
        bool ConsultarEstadoPlanilla(string ci);
    }


    // patron Adapter
    public class AdaptadorRDA : ILectorPlanilla
    {
        private readonly ServicioRDAMinisterioLegado _servicioLegado;

        public AdaptadorRDA(ServicioRDAMinisterioLegado servicioLegado)
        {
            _servicioLegado = servicioLegado;
        }

        public bool ConsultarEstadoPlanilla(string ci)
        {
           
            string respuestaXml = _servicioLegado.ObtenerRespuestaXML(ci);
            
            return respuestaXml.Contains("VALIDO");
        }
    }

    // entrada
    class Program
    {
        static void Main(string[] args)
        {
            ServicioRDAMinisterioLegado servicioExterno = new ServicioRDAMinisterioLegado();

            ILectorPlanilla lector = new AdaptadorRDA(servicioExterno);

            bool esValido = lector.ConsultarEstadoPlanilla("123456");

            Console.WriteLine($"[ADAPTER] Consulta realizada con éxito a través de ILectorPlanilla: Estado Válido = {esValido}");
        }
    }
}
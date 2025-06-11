using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministracionSanatorio
{
    using System.Collections.Generic;

namespace AdministracionSanatorio
{
    public class Paciente
    {
        public string DocumentoIdentidad { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string ObraSocial { get; set; }
        public decimal? MontoCobertura { get; set; }
        public List<IntervencionPaciente> Intervenciones { get; private set; } = new List<IntervencionPaciente>();

        public Paciente(string documentoIdentidad, string nombre, string apellido, string telefono)
        {
            DocumentoIdentidad = documentoIdentidad;
            Nombre = nombre;
            Telefono = telefono;
            Apellido = apellido;
            ObraSocial = null;
            MontoCobertura = null;
        }

        public Paciente(string documentoIdentidad, string nombre, string apellido, string telefono, 
                      string obraSocial, decimal montoCobertura) 
            : this(documentoIdentidad, nombre, apellido,  telefono)
        {
            ObraSocial = obraSocial;
            MontoCobertura = montoCobertura;
        }

            public Paciente()
            {
            }

            public Paciente(string documentoIdentidad, string nombre, string apellido, string telefono, int v) : this(documentoIdentidad, nombre, apellido, telefono)
            {
            }

            public void AgregarIntervencion(IntervencionPaciente intervencion)
        {
            Intervenciones.Add(intervencion);
        }

        public decimal CalcularTotalPendiente()
        {
            decimal total = 0;
            foreach (var intervencion in Intervenciones)
            {
                if (!intervencion.Pagado)
                {
                    total += intervencion.CalcularCostoFinal();
                }
            }
            return total;
        }
    }
}
}
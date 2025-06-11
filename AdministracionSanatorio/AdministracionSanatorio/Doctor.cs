using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministracionSanatorio
{
    namespace AdministracionSanatorio
    {
        public class Doctor
        {
            public string NombreCompleto { get; set; }
            public string Matricula { get; set; }
            public string Especialidad { get; set; }
            public bool Disponible { get; set; }
            public bool Activo { get; internal set; }

            public Doctor(string nombreCompleto, string matricula, string especialidad, bool disponible)
            {
                NombreCompleto = nombreCompleto;
                Matricula = matricula;
                Especialidad = especialidad;
                Disponible = disponible;
            }

            public override string ToString()
            {
                return $"{NombreCompleto} (Matrícula: {Matricula}) - {Especialidad} - {(Disponible ? "Disponible" : "No disponible")}";
            }
        }
    }
}

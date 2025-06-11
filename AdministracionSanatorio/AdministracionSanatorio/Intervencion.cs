using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::AdministracionSanatorio.AdministracionSanatorio;

namespace AdministracionSanatorio
{
    public abstract class Intervencion
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Especialidad { get; set; }
        public decimal Arancel { get; set; }

        protected Intervencion(string codigo, string descripcion, string especialidad, decimal arancel)
        {
            Codigo = codigo;
            Descripcion = descripcion;
            Especialidad = especialidad;
            Arancel = arancel;
        }

        public abstract decimal CalcularCosto();
    }

    public class IntervencionComun : Intervencion
    {
        public IntervencionComun(string codigo, string descripcion, string especialidad, decimal arancel)
            : base(codigo, descripcion, especialidad, arancel) { }

        public override decimal CalcularCosto()
        {
            return Arancel;
        }
    }

    public class IntervencionAltaComplejidad : Intervencion
    {
        public static decimal PorcentajeAdicional { get; set; } = 15; // 15% por defecto

        public IntervencionAltaComplejidad(string codigo, string descripcion, string especialidad, decimal arancel, double v)
            : base(codigo, descripcion, especialidad, arancel) { }

        public override decimal CalcularCosto()
        {
            return Arancel * (1 + PorcentajeAdicional / 100);
        }
    }

    public class IntervencionPaciente
    {
        public string Id { get; set; }
        public DateTime Fecha { get; set; }
        public Intervencion Intervencion { get; set; }
        public Doctor Medico { get; set; }
        public Paciente Paciente { get; set; }
        public bool Pagado { get; set; }

        public IntervencionPaciente(string id, Intervencion intervencion, Doctor medico, Paciente paciente)
        {
            if (medico.Especialidad != intervencion.Especialidad)
                throw new InvalidOperationException("El médico no tiene la especialidad requerida");

            if (!medico.Disponible)
                throw new InvalidOperationException("El médico no está disponible");

            Id = id;
            Fecha = DateTime.Now.AddDays(7); // Programar para 7 días después
            Intervencion = intervencion;
            Medico = medico;
            Paciente = paciente;
            Pagado = false;
        }

        public decimal CalcularCostoFinal()
        {
            decimal costo = Intervencion.CalcularCosto();

            if (Paciente.ObraSocial != null && Paciente.MontoCobertura.HasValue)
            {
                costo -= Paciente.MontoCobertura.Value;
                if (costo < 0) costo = 0;
            }

            return costo;
        }
    }
}
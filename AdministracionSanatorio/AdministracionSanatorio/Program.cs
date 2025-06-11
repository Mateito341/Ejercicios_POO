using System;

namespace AdministracionSanatorio
{
    using System;
    using System.Linq;

    namespace AdministracionSanatorio
    {
        class Program
        {
            private static Hospital hospital = new Hospital();

            static void Main(string[] args)
            {
                bool salir = false;
                while (!salir)
                {
                    MostrarMenu();
                    string opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            DarAltaPaciente();
                            break;
                        case "2":
                            ListarPacientes();
                            break;
                        case "3":
                            ListarIntervenciones();
                            break;
                        case "4":
                            AsignarIntervencion();
                            break;
                        case "5":
                            CalcularCostosPaciente();
                            break;
                        case "6":
                            GenerarReportePendientes();
                            break;
                        case "7":
                            salir = true;
                            break;
                        default:
                            Console.WriteLine("Opción no válida. Intente nuevamente.");
                            break;
                    }

                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }

            private static void MostrarMenu()
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE GESTIÓN HOSPITALARIA ===");
                Console.WriteLine("1. Dar de alta un nuevo paciente");
                Console.WriteLine("2. Listar pacientes");
                Console.WriteLine("3. Listar intervenciones disponibles");
                Console.WriteLine("4. Asignar intervención a paciente");
                Console.WriteLine("5. Calcular costos de paciente");
                Console.WriteLine("6. Reporte de pagos pendientes");
                Console.WriteLine("7. Salir");
                Console.Write("Seleccione una opción: ");
            }

            private static void DarAltaPaciente()
            {
                Console.Clear();
                Console.WriteLine("=== Alta de nuevo paciente ===");

                Paciente paciente = new Paciente();

                Console.Write("DNI: ");
                paciente.DocumentoIdentidad = Console.ReadLine();

                Console.Write("Nombre: ");
                paciente.Nombre = Console.ReadLine();

                Console.Write("Apellido: ");
                paciente.Apellido = Console.ReadLine();

                Console.Write("Teléfono: ");
                paciente.Telefono = Console.ReadLine();

                Console.Write("¿Tiene obra social? (S/N): ");
                bool tieneObraSocial = Console.ReadLine().Trim().ToUpper() == "S";

                if (tieneObraSocial)
                {
                    Console.Write("Nombre de la obra social: ");
                    paciente.ObraSocial = Console.ReadLine();

                    Console.Write("Monto de cobertura: ");
                    decimal cobertura;
                    decimal.TryParse(Console.ReadLine(), out cobertura);
                    paciente.MontoCobertura = cobertura;
                }
                else
                {
                    paciente.ObraSocial = null;
                    paciente.MontoCobertura = 0;
                }

                hospital.Pacientes.Add(paciente);

                Console.WriteLine("\nPaciente registrado con éxito.");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }


            private static void ListarPacientes()
            {
                Console.Clear();
                Console.WriteLine("=== LISTADO DE PACIENTES ===");
                foreach (var paciente in hospital.Pacientes)
                {
                    string obraSocial = paciente.ObraSocial ?? "Sin obra social";
                    Console.WriteLine($"{paciente.DocumentoIdentidad} - {paciente.Nombre} {paciente.Apellido} - Tel: {paciente.Telefono} - OS: {obraSocial}");
                }
            }

            private static void ListarIntervenciones()
            {
                Console.Clear();
                Console.WriteLine("=== INTERVENCIONES DISPONIBLES ===");
                foreach (var intervencion in hospital.Intervenciones)
                {
                    string tipo = intervencion is IntervencionAltaComplejidad ? "Alta complejidad" : "Común";
                    Console.WriteLine($"{intervencion.Codigo} - {intervencion.Descripcion} - {intervencion.Especialidad} - {tipo} - ${intervencion.CalcularCosto()}");
                }
            }

            private static void AsignarIntervencion()
            {
                Console.Clear();
                Console.WriteLine("=== ASIGNAR INTERVENCIÓN ===");

                // Pedir DNI y buscar paciente
                Console.Write("DNI del paciente: ");
                string dni = Console.ReadLine();
                var paciente = hospital.Pacientes.FirstOrDefault(p => p.DocumentoIdentidad == dni);
                if (paciente == null)
                {
                    Console.WriteLine("Paciente no encontrado");
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine("\nIntervenciones disponibles:");
                foreach (var interv in hospital.Intervenciones)
                {
                    string tipo = interv is IntervencionAltaComplejidad ? "Alta complejidad" : "Común";
                    Console.WriteLine($"{interv.Codigo} - {interv.Descripcion} - Especialidad: {interv.Especialidad} - Costo base: ${interv.CalcularCosto()}");
                }

                Console.Write("\nCódigo de intervención: ");
                string codigo = Console.ReadLine();
                var intervencion = hospital.Intervenciones.FirstOrDefault(i => i.Codigo == codigo);
                if (intervencion == null)
                {
                    Console.WriteLine("Intervención no encontrada");
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    return;
                }

                var medicosEspecialidad = hospital.Doctores.Where(d => d.Especialidad == intervencion.Especialidad).ToList();
                if (!medicosEspecialidad.Any())
                {
                    Console.WriteLine($"No hay médicos disponibles para la especialidad {intervencion.Especialidad}");
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"\nMédicos disponibles en {intervencion.Especialidad}:");
                foreach (var doc in medicosEspecialidad)
                {
                    string activo = doc.Activo ? "Activo" : "No activo";
                    Console.WriteLine($"{doc.Matricula} - {doc.NombreCompleto}");
                }

                Console.Write("\nMatrícula del médico: ");
                string matricula = Console.ReadLine();
                var medico = medicosEspecialidad.FirstOrDefault(d => d.Matricula == matricula);
                if (medico == null)
                {
                    Console.WriteLine("Médico no encontrado o no pertenece a la especialidad de la intervención");
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    return;
                }

                try
                {
                    string nuevoId = Guid.NewGuid().ToString().Substring(0, 8);
                    var intervencionPaciente = new IntervencionPaciente(nuevoId, intervencion, medico, paciente);
                    paciente.AgregarIntervencion(intervencionPaciente);
                    Console.WriteLine("Intervención asignada con éxito!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }


            private static void CalcularCostosPaciente()
            {
                Console.Clear();
                Console.WriteLine("=== CALCULAR COSTOS ===");

                Console.Write("DNI del paciente: ");
                string dni = Console.ReadLine();
                var paciente = hospital.Pacientes.FirstOrDefault(p => p.DocumentoIdentidad == dni);
                if (paciente == null)
                {
                    Console.WriteLine("Paciente no encontrado");
                    return;
                }

                Console.WriteLine($"\nTotal pendiente: {paciente.CalcularTotalPendiente():C}");

                Console.WriteLine("\nDetalle de intervenciones:");
                foreach (var intervencion in paciente.Intervenciones)
                {
                    string estado = intervencion.Pagado ? "Pagado" : "Pendiente";
                    Console.WriteLine($"{intervencion.Id} - {intervencion.Intervencion.Descripcion} - " +
                                    $"{intervencion.Fecha.ToShortDateString()} - " +
                                    $"{intervencion.Medico.NombreCompleto} - " +
                                    $"{intervencion.CalcularCostoFinal():C} - {estado}");
                }
            }

            private static void GenerarReportePendientes()
            {
                Console.Clear();
                Console.WriteLine("=== REPORTE DE PAGOS PENDIENTES ===");

                bool hayPendientes = false;
                foreach (var paciente in hospital.Pacientes)
                {
                    var pendientes = paciente.Intervenciones.Where(i => !i.Pagado).ToList();
                    if (pendientes.Any())
                    {
                        hayPendientes = true;
                        Console.WriteLine($"\nPaciente: {paciente.Nombre} {paciente.Apellido} ({paciente.DocumentoIdentidad})");
                        Console.WriteLine($"Obra social: {paciente.ObraSocial ?? "Ninguna"}");

                        foreach (var intervencion in pendientes)
                        {
                            Console.WriteLine($"  - {intervencion.Intervencion.Descripcion} con {intervencion.Medico.NombreCompleto} " +
                                            $"el {intervencion.Fecha.ToShortDateString()}: {intervencion.CalcularCostoFinal():C}");
                        }
                        Console.WriteLine($"Total pendiente: {paciente.CalcularTotalPendiente():C}");
                    }
                }

                if (!hayPendientes)
                {
                    Console.WriteLine("No hay pagos pendientes.");
                }
            }
        }
    }
}
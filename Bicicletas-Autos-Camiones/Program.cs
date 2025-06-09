using System;

namespace Bicicletas_Autos_Camiones
{
    internal class Program
    {
        // clase base vehiculo
        public class Vehiculo
        {
            private int velocidad;
            private int distancia;

            public Vehiculo(int velocidad)
            {
                this.velocidad = velocidad;
                this.distancia = 0;
            }

            public void Mover(int segundos)
            {
                distancia += velocidad * segundos;
            }

            public int Posicion()
            {
                return distancia;
            }

            public void Reiniciar()
            {
                distancia = 0;
            }

            // Método virtual para obtener el tipo de vehículo
            public virtual string Tipo()
            {
                return "Vehículo";
            }
        }

        // Clase Bicicleta
        public class Bicicleta : Vehiculo
        {
            public Bicicleta() : base(10) { }

            public override string Tipo()
            {
                return "Bicicleta";
            }
        }

        // Clase Camion
        public class Camion : Vehiculo
        {
            public Camion() : base(30) { }

            public override string Tipo()
            {
                return "Camión";
            }
        }

        // Clase Auto
        public class Auto : Vehiculo
        {
            public Auto(int velocidad) : base(velocidad) { }

            public override string Tipo()
            {
                return "Auto";
            }
        }

        // Clase Carrera
        public class Carrera
        {
            private Vehiculo v1;
            private Vehiculo v2;
            private int duracion;

            public Carrera(Vehiculo v1, Vehiculo v2, int duracion)
            {
                this.v1 = v1;
                this.v2 = v2;
                this.duracion = duracion;
            }

            public void Iniciar()
            {
                v1.Mover(duracion);
                v2.Mover(duracion);

                Console.WriteLine("\nResultado de la carrera:");
                Console.WriteLine($"{v1.Tipo()} llegó a {v1.Posicion()} metros.");
                Console.WriteLine($"{v2.Tipo()} llegó a {v2.Posicion()} metros.");

                if (v1.Posicion() > v2.Posicion())
                    Console.WriteLine($"Gana el {v1.Tipo()}!");
                else if (v2.Posicion() > v1.Posicion())
                    Console.WriteLine($"Gana el {v2.Tipo()}!");
                else
                    Console.WriteLine("¡Empate!");
            }
        }

        // Método principal
        static void Main(string[] args)
        {
            Auto fiat = new Auto(45);         // El auto se mueve a 45 m/s
            Bicicleta bici = new Bicicleta(); // Bicicleta a 10 m/s
            Camion camion = new Camion();     // Camión a 30 m/s

            // Prueba con movimientos individuales
            bici.Mover(20);
            Console.WriteLine($"Posición de la bicicleta: {bici.Posicion()} metros");

            bici.Mover(10);
            Console.WriteLine($"Nueva posición de la bicicleta: {bici.Posicion()} metros");

            // Carrera entre el auto y el camión
            Carrera carrera = new Carrera(fiat, camion, 10);
            carrera.Iniciar();
        }
    }
}

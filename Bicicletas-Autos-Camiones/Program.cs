using System;

namespace Bicicletas_Autos_Camiones
{
    internal class Program
    {
        // Clase base Vehiculo
        public class Vehiculo
        {
            // Atributos protegidos para acceso desde clases derivadas
            private int velocidad;
            private int distancia;

            // Constructor
            public Vehiculo(int velocidad)
            {
                this.velocidad = velocidad;
                this.distancia = 0;
            }

            // Métodos
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
        }

        // clase bici
        public class Bicicleta : Vehiculo
        {
            //constructor
            public Bicicleta() : base(10) // velocidad por defecto: 10
            {
            }
        }

        // clase camion
        public class Camion : Vehiculo
        {
            public Camion() : base(30) // velocidad por defecto: 30
            {
            }
        }

        // clase auto
        public class Auto : Vehiculo
        {
            public Auto(int velocidad) : base(velocidad) // puede recibir velocidad personalizada
            {
            }
        }

        static void Main(string[] args)
        {
            Auto fiat = new Auto(45); // El auto se mueve a 45 mts por segundo 
            Bicicleta bici = new Bicicleta();
            Camion camion = new Camion();

            bici.Mover(20); // Mover la bicicleta durante 20 segundos 
            Console.WriteLine($"Posición de la bicicleta: {bici.Posicion()} metros");

            bici.Mover(10); // Mover la bicicleta durante otros 10 segundos más 
            Console.WriteLine($"Nueva posición de la bicicleta: {bici.Posicion()} metros");
        }
    }
}

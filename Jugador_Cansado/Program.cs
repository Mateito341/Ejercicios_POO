using System;

namespace Jugador_Cansado
{
    internal class Program
    {
        public class Jugador
        {
            // Atributos
            protected int minutosRecorridos;
            protected int capacidadMaxima;

            // Constructor
            public Jugador(int minutosRecorridos, int capacidadMaxima)
            {
                this.minutosRecorridos = minutosRecorridos;
                this.capacidadMaxima = capacidadMaxima;
            }

            // Métodos
            public bool Correr(int minutos)
            {
                if ((minutosRecorridos + minutos) <= capacidadMaxima)
                {
                    minutosRecorridos += minutos;
                    return true;
                }
                return false;
            }

            public bool Cansado()
            {
                return minutosRecorridos >= capacidadMaxima;
            }

            public void Descansar(int minutos)
            {
                minutosRecorridos -= minutos;
                if (minutosRecorridos < 0)
                {
                    minutosRecorridos = 0;
                }
            }
        }

        public class Amateur : Jugador
        {
            public Amateur(int minutosRecorridos) : base(minutosRecorridos, 20)
            {
            }
        }

        public class Profesional : Jugador
        {
            public Profesional(int minutosRecorridos) : base(minutosRecorridos, 40)
            {
            }
        }

        static void Main(string[] args)
        {
            Profesional miProfesional = new Profesional(0);
            Amateur miAmateur = new Amateur(0);

            Console.WriteLine("¿Profesional corrió 10 minutos? " + miProfesional.Correr(10));
            Console.WriteLine("¿Está cansado? " + miProfesional.Cansado());

            Console.WriteLine("¿Amateur corrió 25 minutos? " + miAmateur.Correr(25)); // Excede su capacidad
            Console.WriteLine("¿Está cansado? " + miAmateur.Cansado());

            miAmateur.Descansar(5);
            Console.WriteLine("Descansó 5 minutos. ¿Ahora puede correr 15? " + miAmateur.Correr(15));
        }
    }
}

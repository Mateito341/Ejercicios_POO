using System;

namespace Jugador_Cansado
{
    internal class Program
    {
        public class Jugador
        {
            //atributos
            private int minutosRecorridos;

            //contructor

            //metodos
            public bool correr(int minutos);
            public bool cansado();
            public void descansar(int minutos);

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}

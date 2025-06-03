using System;
using static Cronometro.Program;

namespace Cronometro
{
    internal class Program
    {
        public class cronometro
        {
            // atributos
            private int segundos;
            private int minutos;

            // constructor
            public cronometro(int segundoss, int minutoss)
            {
                segundos = segundoss;
                minutos = minutoss;
            }

            // métodos
            public void reiniciar()
            {
                segundos = 0;
                minutos = 0;
            }

            public void incrementar()
            {
                segundos++;
                if (segundos == 60)
                {
                    segundos = 0;
                    minutos++;
                }
            }

            public string mostrarTiempo()
            {
                return $"Tiempo transcurrido: {minutos:D2}:{segundos:D2}";
            }
        }

        static void Main(string[] args)
        {
            // Crear un cronometro de x segundos y x minutos
            cronometro miCronometro = new cronometro(0, 0);
            Console.WriteLine(miCronometro.mostrarTiempo());

            for(int i = 0; i < 5000; i++) {
                // Esto se repite 5000 veces. 
                // Incrementar el valor del objeto aca
                miCronometro.incrementar();
            }
            Console.WriteLine(miCronometro.mostrarTiempo());
        }
    }
}

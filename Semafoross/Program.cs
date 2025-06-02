using System;
namespace Semafoross
{
    public class Semaforo
    {
        // Atributos
        private string colorActual;
        private string colorAnterior;
        private int tiempoTranscurrido;
        private bool intermitente;

        // Constructor
        public Semaforo(string colorInicial)
        {
            colorActual = colorInicial;
            tiempoTranscurrido = 0;
            intermitente = false;
        }

        // Métodos
        public void PasoDelTiempo(int segundos)
        {
            // Si está en modo intermitente, alternar cada segundo
            if (intermitente)
            {
                for (int i = 0; i < segundos; i++)
                {
                    tiempoTranscurrido++;
                    // Alternar entre "amarillo" y "apagado" cada segundo
                    if (tiempoTranscurrido % 2 == 1)
                        colorActual = "Amarillo";
                    else
                        colorActual = "Apagado";
                }
                return;
            }

            // Funcionamiento normal - procesar todos los segundos uno por uno
            for (int i = 0; i < segundos; i++)
            {
                tiempoTranscurrido++;

                switch (colorActual)
                {
                    case "Verde":
                        if (tiempoTranscurrido >= 20)
                        {
                            colorActual = "Amarillo";
                            tiempoTranscurrido = 0;
                            colorAnterior = "Verde";
                        }
                        break;

                    case "Amarillo":
                        if (tiempoTranscurrido >= 2)
                        {
                            colorActual = (colorAnterior == "Verde") ? "Rojo" : "Verde";
                            tiempoTranscurrido = 0;
                            colorAnterior = colorActual;
                        }
                        break;

                    case "Rojo":
                        if (tiempoTranscurrido >= 30)
                        {
                            colorActual = "Amarillo";
                            tiempoTranscurrido = 0;
                            colorAnterior = "Rojo";
                        }
                        break;
                }
            }
        }

        // Métodos para modo intermitente
        public void PonerEnIntermitente()
        {
            intermitente = true;
            tiempoTranscurrido = 0;
        }

        public void SacarDeIntermitente()
        {
            intermitente = false;
            tiempoTranscurrido = 0;
        }

        // Método básico
        public string MostrarColor()
        {
            return colorActual;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Ejemplo de uso
            Semaforo miSemaforo = new Semaforo("Rojo");
            Console.WriteLine($"Color inicial: {miSemaforo.MostrarColor()}");

            // Simular paso de X segundos
            miSemaforo.PasoDelTiempo(31);
            Console.WriteLine($"Después de 31 segundos: {miSemaforo.MostrarColor()}");

            // Modo intermitente
            miSemaforo.PonerEnIntermitente();
            miSemaforo.PasoDelTiempo(3);
            Console.WriteLine($"En intermitente: {miSemaforo.MostrarColor()}");
        }
    }
}
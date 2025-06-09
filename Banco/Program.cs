using System;

namespace Banco
{
    internal class Program
    {

        public class cuentaBancaria
        {
            //atributos
            private int numeroCuenta;
            private int saldo;
            private string titular;

            //constructor
            public cuentaBancaria(int numeroCuenta, string titular, int saldoInicial = 0)
            {
                this.numeroCuenta = numeroCuenta;
                this.titular = titular;
                this.saldo = saldoInicial;
            }

            //metodos
            public int obtenerSaldo()
            {
                return saldo;
            }

            public void modificarSaldo(int cantidad)
            {
                saldo = cantidad;
            }
        }

        public class banco
        {

        }
        static void Main(string[] args)
        {
            // Crear una cuenta bancaria
            cuentaBancaria cuenta = new cuentaBancaria(123456, "Juan Perez", 1000);

            // Mostrar el saldo inicial
            Console.WriteLine($"Saldo inicial de la cuenta {cuenta.obtenerSaldo()}");

            //Modificar el saldo
            cuenta.modificarSaldo(1500);
            // Mostrar el saldo modificado
            Console.WriteLine($"Saldo modificado de la cuenta {cuenta.obtenerSaldo()}");


        }
    }
}

using System;

namespace Banco
{
    internal class Program
    {
        public class CuentaBancaria
        {
            //atributos
            private int numeroCuenta;
            private int saldo;
            private string titular;

            //constructor
            public CuentaBancaria(int numeroCuenta, string titular, int saldoInicial = 0)
            {
                this.numeroCuenta = numeroCuenta;
                this.titular = titular;
                this.saldo = saldoInicial;
            }

            //metodos
            public int ObtenerSaldo()
            {
                return saldo;
            }

            public void ModificarSaldo(int cantidad)
            {
                saldo = cantidad;
            }
        }

        public class Banco
        {
            //depositar
            public void Depositar(CuentaBancaria cuenta, int cantidad)
            {
                if (cantidad > 0)
                {
                    cuenta.ModificarSaldo(cuenta.ObtenerSaldo() + cantidad);
                }
                else
                {
                    Console.WriteLine("Cantidad a depositar debe ser positiva.");
                }
            }

            //extraer
            public void Extraer(CuentaBancaria cuenta, int cantidad)
            {
                if (cantidad > 0 && cantidad <= cuenta.ObtenerSaldo())
                {
                    cuenta.ModificarSaldo(cuenta.ObtenerSaldo() - cantidad);
                }
                else
                {
                    Console.WriteLine("Cantidad a extraer debe ser positiva y no puede exceder el saldo actual.");
                }
            }

            //transferir
            public bool Transferir(CuentaBancaria cuentaOrigen, CuentaBancaria cuentaDestino, int cantidad)
            {
                if (cantidad > 0 && cantidad <= cuentaOrigen.ObtenerSaldo())
                {
                    cuentaOrigen.ModificarSaldo(cuentaOrigen.ObtenerSaldo() - cantidad);
                    cuentaDestino.ModificarSaldo(cuentaDestino.ObtenerSaldo() + cantidad);
                    return true;
                }
                return false;
            }
        }

        static void Main(string[] args)
        {
            // Crear una cuenta bancaria
            CuentaBancaria cuenta = new CuentaBancaria(123456, "Juan Perez", 1000);

            // Mostrar el saldo inicial
            Console.WriteLine($"Saldo inicial de la cuenta {cuenta.ObtenerSaldo()}");

            //Modificar el saldo
            cuenta.ModificarSaldo(1500);
            // Mostrar el saldo modificado
            Console.WriteLine($"Saldo modificado de la cuenta {cuenta.ObtenerSaldo()}");

            // Crear un banco
            Banco banco = new Banco();

            // Depositar dinero en la cuenta
            banco.Depositar(cuenta, 500);
            Console.WriteLine($"Saldo después del depósito: {cuenta.ObtenerSaldo()}");

            // Extraer dinero de la cuenta
            banco.Extraer(cuenta, 300);
            Console.WriteLine($"Saldo después de la extracción: {cuenta.ObtenerSaldo()}");

            //Transferir dinero a otra cuenta
            CuentaBancaria cuentaDestino = new CuentaBancaria(654321, "Maria Lopez", 2000);
            bool transferenciaExitosa = banco.Transferir(cuenta, cuentaDestino, 200);
            if (transferenciaExitosa)
            {
                Console.WriteLine($"Saldo de la cuenta origen después de la transferencia: {cuenta.ObtenerSaldo()}");
                Console.WriteLine($"Saldo de la cuenta destino después de la transferencia: {cuentaDestino.ObtenerSaldo()}");
            }
            else
            {
                Console.WriteLine("Transferencia fallida. Verifique los saldos.");
            }
        }
    }
}
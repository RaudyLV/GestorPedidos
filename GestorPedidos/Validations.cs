using System;

namespace GestorDeValidaciones
{
    public class PagoFactory
    {
        public static Pago CrearPago(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    return new PagoTarjeta();
                default:
                    Console.WriteLine("ERROR: Elija una opcion valida!");
                    return null;
            }
        }
    }

    public class ValidarPago
    {
        public bool Validar(double monto, int dineroRecibido)
        {
            if (dineroRecibido <= 0)
            {
                Console.WriteLine("ERROR: El dinero no puede ser 0. ");
                return false;
            }
            else if (dineroRecibido < monto)
            {
                Console.WriteLine($"Dinero insuficiente, faltan ${monto - dineroRecibido}  pesos");
                return false;
            }
            else if (dineroRecibido > monto)
            {
                Console.WriteLine($"Gracias por su compra! Devuelta de {dineroRecibido - monto} pesos");
                return true;
            }
            Console.WriteLine("Gracias por su compra vuelva pronto! :)");
            return true;
        }
    }
}

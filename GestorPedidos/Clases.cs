using GestorDeValidaciones;
using GestorInterfaces;
using System;
using System.Collections.Generic;
using System.Threading;

//Clase principal
public class Pedidos
{
    public int Id { get; set; }
    public int CompraId { get; set; }
    public List<Compra> Compras { get; set; }
}
public class Compra
{
    public int Id { get; set; }
    public DateTime FechaDeCompra { get; set; }
    public int CantidadDeProductos { get; set; }
    public int TotalPrecio { get; set; }
    public int ProductoId { get; set; } //LLAVE FORANEA
}
public class Categorias
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int CantidadProductos { get; set; }
}
public class Producto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Cantidad { get; set; }
    public int Precio { get; set; }
    public int CategoriaId { get; set; } // "LLAVE FORANEA"
}
public class Empleado
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public virtual int Sueldo => 20000;
    public string Departamento { get; set; }
    public string Rol {  get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int CompraId { get; set; }
}

public class Administrador : Empleado
{
    public override int Sueldo => 40000; 
}

public class Director : Empleado
{
    public override int Sueldo => 70000; 
}

public abstract class Pago
{
    protected ValidarPago validarPago = new ValidarPago();

    public int MenuPagar()
    {
        Console.Clear();
        Console.WriteLine("Elija un metodo de pago: ");
        Console.WriteLine("1. Tarjeta");
        Console.WriteLine("2. PayPal");
        Console.Write("Elija un metodo: ");
        return int.Parse(Console.ReadLine());
    }
    public abstract void Pagar(int monto);
}

public class PagoTarjeta : Pago
{
    private readonly Dictionary<int, ITarjeta> _tarjetas = new Dictionary<int, ITarjeta>
        {
            {1, new BancoBHD()},
            {2, new BancoPopular() }
        };

    private int PagoTarjetaMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Elija el banco de la tarjeta: ");
            Console.WriteLine("1. BHD");
            Console.WriteLine("2. Popular");
            Console.Write("\nElija un banco: ");
            if (int.TryParse(Console.ReadLine(), out int opcion) && opcion < 3)
            {
                return opcion;
            }
            Console.WriteLine("\nOpcion invalida. Vuelva a intentarlo!");
            Console.WriteLine("Pulse cualquier tecla para volver");
            Console.ReadKey();
        }
    }
    public override void Pagar(int monto)
    {
        int opcion = PagoTarjetaMenu();

        Console.Write($"\nMonto a pagar ({monto}): ");
        int dineroRecibido = int.Parse(Console.ReadLine());

        _tarjetas[opcion].ValidarTransferencia();
        validarPago.Validar(monto, dineroRecibido);
    }
}

public class BancoBHD : ITarjeta
{
    public void ValidarTransferencia()
    {
        Console.WriteLine("Haciendo transferencia del BHD...");
        Thread.Sleep(4000);
    }
}

public class BancoPopular : ITarjeta
{
    public void ValidarTransferencia()
    {
        Console.WriteLine("Haciendo transferencia del Popular...");
        Thread.Sleep(4000);
    }
}
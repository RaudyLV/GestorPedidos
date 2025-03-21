using GestorDeRepositorios;
using GestorDeServicios;
using GestorDeValidaciones;
using GestorInterfaces;
using System;
class Factura
{
    static void Main()
    {
        ProductRepository productRepository = new ProductRepository();
        CategoryRepository categoryRepository = new CategoryRepository();

        //PRINCIPAL
        ProductService productService = new ProductService(productRepository, categoryRepository);

        ICompraRepository compraRepository = new CompraRepository();
        Pago pago = new PagoTarjeta();

        //PRINCIPAL
        CompraService compraService = new CompraService(compraRepository, productService, pago);


        PedidosRepository pedidosRepository = new PedidosRepository();

        //PRINCIPAL
        PedidoService pedidoService = new PedidoService(pedidosRepository, compraService, productService);

        EmpleadosRepository empleadosRepository = new EmpleadosRepository();

        //Principal
        EmpleadoService empleadoService = new EmpleadoService(empleadosRepository);

        int opcion;
        do
        {
            Console.Clear();
            Console.WriteLine("1. Realizar un pedido");
            //Console.WriteLine("2. Ver productos");
            Console.Write("Elija una opcion: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    pedidoService.EjecutarMenuPedidos();
                    break;
                case 2:

                    break;
                default:
                    Console.WriteLine("Elija una opcion valida!");
                    break;
            }

            Console.ReadKey();
        } while (opcion != 5);
    }


}



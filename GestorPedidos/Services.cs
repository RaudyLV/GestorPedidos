
using System;
using System.Collections.Generic;
using System.Linq;
using GestorDeRepositorios;
using GestorDeValidaciones;
using GestorInterfaces;

namespace GestorDeServicios
{
    public class EmpleadoService
    {
        private readonly EmpleadosRepository _empleadosRepository;

        public EmpleadoService(EmpleadosRepository empleadosRepository)
        {
            _empleadosRepository = empleadosRepository;
        }

        private int MenuEmpleado()
        {
            Console.Clear();
            Console.WriteLine("-----------MENU EMPLEADO-----------");
            Console.WriteLine("1. Ver informacion personal");
            Console.WriteLine("2. Cambiar informacion personal");
            return int.Parse(Console.ReadLine());
        }

        public virtual void EjecutarMenuEmpleados()
        {
            int opcion = MenuEmpleado();

            switch (opcion)
            {
                case 1:
                    MostrarInfoPersonal();
                    break;
                case 2:
                    UpdateInfo();
                    break;

                default:
                    Console.WriteLine("\nElija una opcion valida!");
                    break;
            }
        }

        public void MostrarInfoPersonal()
        {
            Empleado empleado = new Empleado();

            Console.WriteLine("--------------INFORMACION PERSONAL-----------");
            Console.WriteLine($"Nombre: {empleado.Nombre}");
            Console.WriteLine($"Departamento: {empleado.Departamento}");
            Console.WriteLine($"Sueldo: {empleado.Sueldo}");
            Console.WriteLine($"Rol: {empleado.Rol}");
            Console.WriteLine($"Email: {empleado.Email}");
            Console.WriteLine($"Password: {empleado.Password}");
        }

        public void UpdateInfo()
        {

        }
    }

    public class AdministradorService : EmpleadoService
    {
        public AdministradorService(EmpleadosRepository empleadosRepository) : base(empleadosRepository)
        {

        }

        public int MenuAdministrador()
        {
            Console.WriteLine("3. Agregar productos");
            Console.WriteLine("4. Eliminar producto");
            Console.WriteLine("5. Actualizar Categoria");
            Console.WriteLine("6. Buscar Producto");
            Console.WriteLine("7. Ver todos los productos");
            Console.WriteLine("8. Agregar categoria");
            Console.WriteLine("9. Eliminar categoria");
            Console.WriteLine("10. Actualizar categoria");
            return int.Parse(Console.ReadLine());
        }

        public override void EjecutarMenuEmpleados()
        {
            base.EjecutarMenuEmpleados();

            int opcion = MenuAdministrador();

            switch (opcion)
            {
                default:
                    Console.WriteLine("\nElija una opcion valida!");
                    break;
            }
            Console.WriteLine("Pulse cualquier tecla para volver...");
        }

    }

    public class DirectorService : EmpleadoService
    {
        public DirectorService(EmpleadosRepository empleadosRepository) : base(empleadosRepository)
        {
        }

        public int MenuAdministrador()
        {
            Console.WriteLine("3. Agregar empleado");
            Console.WriteLine("4. Despedir empleado ");
            Console.WriteLine("5. Buscar empleado");
            Console.WriteLine("6. Ver todos los empleados");
            return int.Parse(Console.ReadLine());
        }

        public override void EjecutarMenuEmpleados()
        {
            base.EjecutarMenuEmpleados();

            int opcion = MenuAdministrador();

            switch (opcion)
            {
                default:
                    Console.WriteLine("\nElija una opcion valida!");
                    break;
            }
            Console.WriteLine("Pulse cualquier tecla para volver...");
        }
}
public class PedidoService
    {
        private readonly IPedidosRepository _pedidosRepository;
        private readonly CompraService _compraService;
        private readonly ProductService _productService;
        public PedidoService(IPedidosRepository pedidosRepository, CompraService compraService,
            ProductService productService)
        {
            _pedidosRepository = pedidosRepository;
            _compraService = compraService;
            _productService = productService;
        }

        public int MenuDePedidos()
        {
            Console.Clear();
            Console.WriteLine("------BIENVENIDO A PAPOLO COLMADO------");
            Console.WriteLine("\n-- QUE QUIERE HACER HOY? ");
            Console.WriteLine("1. Hacer un pedido.");
            Console.WriteLine("2. Ver productos comprados");
            Console.Write("\nElija una opcion valida: ");
            return int.Parse(Console.ReadLine());
        }

        public void EjecutarMenuPedidos()
        {
            int opcion = MenuDePedidos();

            switch (opcion)
            {
                case 1:
                    AddPedidos();
                    break;
                case 2:
                    MostrarPedidosHechos();
                    break;
                default:
                    Console.WriteLine("ERROR: Opcion invalida!");
                    break;
            }
        }

        private void AddPedidos()
        {
            ProcesarPedidos();

            var listaDeCompras = new List<Compra>();
            foreach (var compra in _compraService.GetAllCompras())
            {
                listaDeCompras.Add(new Compra { Id = listaDeCompras.Count + 1, ProductoId = compra.ProductoId });
            }

            var newPedido = new Pedidos
            {
                Id = _pedidosRepository.GetAllPedidos().Count() + 1,
                Compras = listaDeCompras,
            };

            _pedidosRepository.AddPedido(newPedido);
        }

        private void MostrarPedidosHechos()
        {
            var comprasDeProductos = from compras in _compraService.GetAllCompras()
                                     join producto in _productService.GetAllProductos()
                                     on compras.ProductoId equals producto.Id
                                     select new
                                     {
                                         compras.Id,
                                         compras.CantidadDeProductos,
                                         compras.TotalPrecio,
                                         compras.FechaDeCompra,
                                         producto.Name,
                                         producto.Precio

                                     };

            if (!comprasDeProductos.Any())
            {
                Console.WriteLine("\nERROR: Usted no ha hecho ningun pedido");
            }

            else
            {
                Console.Clear();
                Console.WriteLine("------------------DETALLES DEL PEDIDO----------------------");
                foreach (var compra in comprasDeProductos)
                {
                    Console.WriteLine($"~Nombre del producto: {compra.Name}");
                    Console.WriteLine($"~Precio: ${compra.Precio} pesos");
                    Console.WriteLine($"~Cantidad comprada: {compra.CantidadDeProductos}");
                    Console.WriteLine($"~Fecha de compra: {compra.FechaDeCompra}");
                    Console.WriteLine("------------------------------------------------------");
                    Console.WriteLine();
                }

            }
            Console.Write("Pulse cualquier tecla para seguir...");
        }

        // --------------------------AUX METHODS-----------------------------
        private void ProcesarPedidos()
        {
            Console.Clear();
            _productService.ShowProductsWithCategory();

            Console.Write("Escriba el ID del producto que quiere: ");
            int productoId = int.Parse(Console.ReadLine());

            if (productoId > _productService.GetAllProductos().Count)
            {
                Console.WriteLine("\nERROR: ID invalido!");
                Console.Write("Pulse cualquier tecla para volver...");
                return;
            }

            Console.Write("Cantidad: ");
            int cantidad = int.Parse(Console.ReadLine());

            _compraService.Comprar(productoId, cantidad);
        }
    }
    public class CompraService
    {
        private readonly ICompraRepository _compraRepository;
        private readonly ProductService _productService;
        private readonly Pago _metodoDePago;
        public CompraService(ICompraRepository compraRepository, ProductService productService,
         Pago metodoDePago)
        {
            _compraRepository = compraRepository;
            _productService = productService;
            _metodoDePago = metodoDePago;
        }

        public void Comprar(int productoId, int cantidad)
        {
            var producto = _productService.GetById(productoId);

            if (producto.Cantidad < cantidad)
            {
                Console.WriteLine("\nERROR: No hay suficientes en stock :( .");
                Console.Write("\n~Pulse cualquier tecla para volver...");
                return;
            }

            int opcion = _metodoDePago.MenuPagar();

            var pago = PagoFactory.CrearPago(opcion);

            int totalPrecio = producto.Precio * cantidad;

            if (pago != null)
            {
                pago.Pagar(totalPrecio);
            }

            var compra = new Compra
            {
                Id = _compraRepository.GetAllCompras().Count() + 1,
                ProductoId = productoId,
                CantidadDeProductos = cantidad,
                TotalPrecio = totalPrecio,
                FechaDeCompra = DateTime.Now
            };

            producto.Cantidad -= cantidad;
            _productService.UpdateProduct(producto);

            _compraRepository.AddCompra(compra);
        }
        public List<Compra> GetAllCompras() => _compraRepository.GetAllCompras();
    }
    public class ProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public void AddProduct(Producto product)
        {
            Console.WriteLine("Digite el nombre del productoId: ");
            product.Name = Console.ReadLine();

            var findProduct = _repository.GetAllProducts()
                                         .FirstOrDefault(p => p.Name
                                         .Equals(product.Name, StringComparison.CurrentCultureIgnoreCase));

            if (findProduct != null)
            {
                findProduct.Cantidad++;
                Console.WriteLine($"Producto: {product.Name} agregado exitosamente");
                _repository.UpdateProduct(findProduct);

            }
            else
            {
                Console.WriteLine("Digite el precio del productoId: ");
                product.Precio = int.Parse(Console.ReadLine());

                product.Cantidad++;
                _repository.AddProduct(product);
            }
            Console.WriteLine("Pulse cualquier tecla para seguir...");
        }
        public void DeleteProduct(int id)
        {
            _repository.DeleteProduct(id);
        }
        public void UpdateProduct(Producto producto)
        {
            _repository.UpdateProduct(producto);

            Console.WriteLine("\nPulse cualquier tecla para seguir...");
        }
        public void BuscarProducto()
        {
            Console.WriteLine("Producto a buscar: ");
            string productoNombre = Console.ReadLine();

            var existingProducts = _repository.GetAllProducts().FirstOrDefault(p => p.Name
                                                               .Equals(productoNombre, StringComparison.CurrentCultureIgnoreCase));

            if (existingProducts is null)
            {
                Console.WriteLine("ERROR: Producto inexistente!");
                return;
            }

            Console.WriteLine($"Nombre: {existingProducts.Name}" + "\n" +
                              $"Precio: {existingProducts.Precio}" + "\n" +
                              $"Cantidad: {existingProducts.Cantidad}" + "\n");

            Console.WriteLine("Pulse cualquier tecla para seguir...");
        }
        public Producto GetById(int id)
        {
            var product = _repository.GetProductById(id);

            return product;
        }
        public List<Producto> GetAllProductos() => _repository.GetAllProducts();
        public void ShowProductsWithCategory()
        {
            var productosConCategoria = from producto in _repository.GetAllProducts()
                                        join categoria in _categoryRepository.GetAllCategories()
                                        on producto.CategoriaId equals categoria.Id
                                        select new
                                        {
                                            categoria.Nombre,
                                            producto.Id,
                                            producto.Name,
                                            producto.Precio,
                                            producto.Cantidad
                                        };

            if (!productosConCategoria.Any())
            {
                Console.WriteLine("Stock vacio, vuelva mas tarde.");
            }

            Console.WriteLine("----INVENTARIO----");
            foreach (var producto in productosConCategoria)
            {
                Console.WriteLine($"~Categoria: {producto.Nombre}");//nombre de categoria
                Console.WriteLine($"ID del productoId: {producto.Id}");
                Console.WriteLine($"Nombre del productoId: {producto.Name}");
                Console.WriteLine($"Precio: {producto.Precio} pesos");
                if (producto.Cantidad == 0)
                {
                    Console.WriteLine("~Out of stock!");
                }
                else
                {
                    Console.WriteLine($"Cantidad en Stock: {producto.Cantidad}");
                }
                Console.WriteLine();
            }
        }
    }
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void AddCategory(Categorias categoria)
        {
            Console.WriteLine("Digite el nombre de la categoria: ");
            categoria.Nombre = Console.ReadLine();

            var findCategory = _categoryRepository.GetAllCategories().FirstOrDefault(c => c.Nombre
                                                                       .Equals(categoria.Nombre, StringComparison.CurrentCultureIgnoreCase));

            if (findCategory != null)
            {
                Console.WriteLine("ERROR: La categoria ya existe!");
            }

            _categoryRepository.AddCategory(categoria);

            Console.WriteLine("Pulse cualquier tecla para volver...");
        }
        public void DeleteCategory(int id)
        {
            var findCategory = _categoryRepository.GetCategoryById(id);

            if (findCategory is null)
            {
                Console.WriteLine("ERROR: Categoria inexistente");
            }

            _categoryRepository.DeleteCategory(id);

            Console.WriteLine("Pulse cualquier tecla para volver...");
        }
        public void UpdateCategory(Categorias categoria)
        {
            Console.WriteLine("Que categoria quiere buscar? ");
            categoria.Nombre = Console.ReadLine().Trim();

            var findCategory = _categoryRepository.GetAllCategories().FirstOrDefault(c => c.Nombre
                                                         .Equals(categoria.Nombre, StringComparison.CurrentCultureIgnoreCase));

            if (findCategory is null)
            {
                Console.WriteLine("No hay categoria con ese nombre.");
            }
        }
        public void FindCategory(string nombre)
        {
            Console.WriteLine("Que categoria quiere buscar? ");
            nombre = Console.ReadLine().Trim();

            var findCategory = _categoryRepository.GetAllCategories().FirstOrDefault(c => c.Nombre
                                                                     .Equals(nombre, StringComparison.CurrentCultureIgnoreCase));

            if (findCategory is null)
            {
                Console.WriteLine("No hay categoria con ese nombre.");
            }

            Console.WriteLine($"\nCategoria: {findCategory.Nombre}" + "\n" +
                              $"Cantidad de productos: {findCategory.CantidadProductos}");

            Console.WriteLine("Pulse cualquier tecla para volver...");
        }
        public Categorias GetCategoryById(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);

            if (category is null)
            {
                Console.WriteLine("Producto inexistente.");
                return category;
            }
            return category;
        }
        public List<Categorias> GetAllCategories() => _categoryRepository.GetAllCategories();
    }
}

using GestorInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestorDeRepositorios
{
    public class EmpleadosRepository : IEmpleadosRepository
    {
        private readonly Dictionary<int, Empleado> _empleados = new Dictionary<int, Empleado>
        {
            {1, new Empleado{ Id = 1, Nombre = "Raudy", Departamento = "IT", Rol = "Director", Email = "Raudy@gmail.com", Password = "123"  } },
            {2, new Empleado{ Id = 2, Nombre = "Jose", Departamento = "Logistica", Rol = "Administrador", Email = "Jose@gmail.com", Password = "1234"  } },
        };

        public void AgrearEmpleado(Empleado empleado) => _empleados.Add(empleado.Id, empleado);
        public void DespedirEmpleado(int id) => _empleados.Remove(id);
        public List<Empleado> GetAllEmpleados() => _empleados.Values.ToList();
        public Empleado GetEmpleadoById(int id)
        {
            var empleado = _empleados.FirstOrDefault(e => e.Key == id).Value;

            return empleado;
        }

        public void UpdateEmpleado(Empleado empleado)
        {
            var buscarEmpleado = _empleados.FirstOrDefault(e => e.Key == empleado.Id).Value;
            if( buscarEmpleado is null)
            {
                Console.WriteLine("ERROR: Empleado inexistente");
            }

            buscarEmpleado.Nombre = empleado.Nombre;
            buscarEmpleado.Rol = empleado.Rol;
            buscarEmpleado.Email = empleado.Email;
            buscarEmpleado.Password = empleado.Password;
        }
    }
    public class PedidosRepository : IPedidosRepository
    {
        private readonly List<Pedidos> _pedidos = new List<Pedidos>();

        public void AddPedido(Pedidos pedido) => _pedidos.Add(pedido);
        public List<Pedidos> GetAllPedidos() => _pedidos;

        public Pedidos GetPedidoById(int id)
        {
            var pedido = _pedidos.FirstOrDefault(x => x.Id == id);

            return pedido;
        }
    }
    public class CompraRepository : ICompraRepository
    {
        private readonly List<Compra> _compras = new List<Compra>();
        public void AddCompra(Compra compra) => _compras.Add(compra);
        public List<Compra> GetAllCompras() => _compras;
    }
    public class ProductRepository : IProductRepository
    {
        private readonly List<Producto> _products = new List<Producto> // "BASE DE DATOS"
        {
            new Producto{Id = 1, Name = "Super Hot Dog", Cantidad = 4, Precio = 400, CategoriaId = 4},
            new Producto{Id = 2,Name = "Yaroa mixta",Cantidad= 3,Precio= 500 , CategoriaId = 3},
            new Producto{ Id = 3, Name = "Sandwich", Cantidad = 3, Precio = 500 , CategoriaId = 1},
            new Producto{Id = 4, Name = "Burrito supremo", Cantidad = 1, Precio = 500 , CategoriaId = 2}
        };
        public void AddProduct(Producto product) => _products.Add(product);
        public void DeleteProduct(int id)
        {
            var findProduct = _products.FirstOrDefault(x => x.Id == id);
            if(findProduct == null)
            {
                Console.WriteLine("ERROR: Producto no existente!");
                return;
            }

            _products.Remove(findProduct);
        }
        public List<Producto> GetAllProducts() => _products;
        public Producto GetProductById(int id)
        {
            var findProduct = _products.FirstOrDefault(x => x.Id == id);
            
            return findProduct;
        }
        public void UpdateProduct(Producto product)
        {
            var findProduct = _products.FirstOrDefault(x => x.Id == product.Id);
            if (findProduct is null)
            {
                Console.WriteLine("ERROR: Producto inexistente! ");
            }

            findProduct.Name = product.Name;
            findProduct.Precio = product.Precio;
            findProduct.Cantidad = product.Cantidad;
        }
    }
    public class CategoryRepository : ICategoryRepository
    {
        private readonly List<Categorias> _categories = new List<Categorias>
        {
            new Categorias{ Id = 1, Nombre = "Sandwich"},
            new Categorias{ Id = 2, Nombre = "Burritos" },
            new Categorias{ Id = 3, Nombre = "Yaroas" },
            new Categorias{Id = 4, Nombre = "Hot Dogs"}
        };

        public void AddCategory(Categorias categoria)
        {
            _categories.Add(categoria);
        }

        public void DeleteCategory(int id)
        {
            var findCategory = _categories.Find(x => x.Id == id);

            if (findCategory is null) return;

            _categories.Remove(findCategory);
        }

        public List<Categorias> GetAllCategories()
        {
            return _categories;
        }

        public Categorias GetCategoryById(int id)
        {
            var findCategory = _categories.Find(x => x.Id == id);

            return findCategory;
        }

        public void UpdateCategory(Categorias categoria)
        {
            var findCategory = _categories.FirstOrDefault(c => c.Id == categoria.Id);
            if (findCategory is null) return;

            findCategory.Nombre = categoria.Nombre;
            findCategory.CantidadProductos = categoria.CantidadProductos;
        }
    }


}

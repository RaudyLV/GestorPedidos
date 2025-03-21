using System.Collections.Generic;

namespace GestorInterfaces
{
    public interface ICategoryRepository
    {
        List<Categorias> GetAllCategories();
        Categorias GetCategoryById(int id);
        void AddCategory(Categorias product);
        void UpdateCategory(Categorias product);
        void DeleteCategory(int id);
    }
    public interface IProductRepository
    {
        List<Producto> GetAllProducts();
        Producto GetProductById(int id);
        void AddProduct(Producto product);
        void UpdateProduct(Producto product);
        void DeleteProduct(int id);
    }
    public interface IPedidosRepository
    {
        void AddPedido(Pedidos pedido);
        List<Pedidos> GetAllPedidos();
        Pedidos GetPedidoById(int id);
    }
    public interface ICompraRepository
    {
        void AddCompra(Compra compra);
        List<Compra> GetAllCompras();
    }
    public interface IEmpleadosRepository
    {
        void AgrearEmpleado(Empleado empleado);
        void DespedirEmpleado(int id);
        void UpdateEmpleado(Empleado empleado);
        Empleado GetEmpleadoById(int id);
        List<Empleado> GetAllEmpleados();
    }
    public interface ITarjeta
    {
        void ValidarTransferencia();
    }
}

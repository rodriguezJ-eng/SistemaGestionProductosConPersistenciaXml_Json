using System;
public interface IProductoRepository
{
    void Agregar(Producto producto);
    Producto ObtenerPorCodigo(string codigo);
    void Actualizar(Producto producto);
    void Eliminar(string codigo);
    List<Producto> ObtenerTodos();
}

public class GestorProductos
{
    private readonly IProductoRepository _repositorio;

    public GestorProductos(IProductoRepository repositorio)
    {
        _repositorio = repositorio;
    }

    // 1. Agregar producto
    public string AgregarProducto(string codigo, string nombre, decimal precio)
    {
        try
        {
            Producto producto = new Producto(codigo, nombre, precio);
            _repositorio.Agregar(producto);
            return $" Producto '{nombre}' agregado correctamente.";
        }
        catch (Exception ex)
        {
            return $" Error: {ex.Message}";
        }
    }

    // 2. Buscar producto por código
    public string BuscarProducto(string codigo)
    {
        try
        {
            Producto p = _repositorio.ObtenerPorCodigo(codigo);
            return $" Encontrado → {p}";
        }
        catch (Exception ex)
        {
            return $" Error: {ex.Message}";
        }
    }

    // 3. Actualizar producto
    public string ActualizarProducto(string codigo, string nuevoNombre, decimal nuevoPrecio)
    {
        try
        {
            Producto actualizado = new Producto(codigo, nuevoNombre, nuevoPrecio);
            _repositorio.Actualizar(actualizado);
            return $" Producto '{codigo}' actualizado correctamente.";
        }
        catch (Exception ex)
        {
            return $" Error: {ex.Message}";
        }
    }

    // 4. Eliminar producto
    public string EliminarProducto(string codigo)
    {
        try
        {
            _repositorio.Eliminar(codigo);
            return $" Producto con código '{codigo}' eliminado correctamente.";
        }
        catch (Exception ex)
        {
            return $" Error: {ex.Message}";
        }
    }

    // 5. Listar productos
    public List<Producto> ListarProductos()
    {
        return _repositorio.ObtenerTodos();
    }

    // 6. Filtrar productos por criterio usando LINQ
    public List<Producto> FiltrarProductos(decimal valor, string criterio)
    {
        List<Producto> todos = _repositorio.ObtenerTodos();
        return criterio switch
        {
            ">" => todos.Where(p => p.Precio > valor).ToList(),
            "<" => todos.Where(p => p.Precio < valor).ToList(),
            "=" => todos.Where(p => p.Precio == valor).ToList(),
            _ => new List<Producto>()
        };
    }

    // 7. Ordenar productos de menor a mayor precio con LINQ
    public List<Producto> OrdenarProductos()
    {
        return _repositorio.ObtenerTodos()
            .OrderBy(p => p.Precio)
            .ToList();
    }

    // 8. Total de productos
    public int TotalProductos()
    {
        return _repositorio.ObtenerTodos().Count;
    }
}

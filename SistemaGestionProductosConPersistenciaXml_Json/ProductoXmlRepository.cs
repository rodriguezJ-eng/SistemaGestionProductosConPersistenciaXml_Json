using System.Xml.Serialization;

public class ProductoXmlRepository : IProductoRepository
{
    private readonly string _archivo;

    public ProductoXmlRepository(string archivo)
    {
        _archivo = archivo;
        if (!File.Exists(_archivo))
            GuardarArchivo(new List<Producto>());
    }

    private List<Producto> LeerArchivo()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Producto>));
        using FileStream fs = new(_archivo, FileMode.Open);
        return (List<Producto>)serializer.Deserialize(fs)!;
    }

    private void GuardarArchivo(List<Producto> productos)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Producto>));
            using FileStream fs = new(_archivo, FileMode.Create);
            serializer.Serialize(fs, productos);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new IOException("No tiene permisos para escribir el archivo.", ex);
        }
    }

    public void Agregar(Producto producto)
    {
        List<Producto> productos = LeerArchivo();
        if (productos.Any(p => p.Codigo == producto.Codigo))
            throw new InvalidOperationException(
                $"Ya existe un producto con código '{producto.Codigo}'.");
        productos.Add(producto);
        GuardarArchivo(productos);
    }

    public Producto ObtenerPorCodigo(string codigo)
    {
        return LeerArchivo()
            .FirstOrDefault(p => p.Codigo == codigo)
            ?? throw new ArgumentException(
                $"No existe un producto con código '{codigo}'.");
    }

    public void Actualizar(Producto producto)
    {
        List<Producto> productos = LeerArchivo();
        Producto existente = productos.FirstOrDefault(p => p.Codigo == producto.Codigo)
            ?? throw new ArgumentException(
                $"No existe un producto con código '{producto.Codigo}'.");
        existente.Nombre = producto.Nombre;
        existente.Precio = producto.Precio;
        GuardarArchivo(productos);
    }

    public void Eliminar(string codigo)
    {
        List<Producto> productos = LeerArchivo();
        Producto producto = productos.FirstOrDefault(p => p.Codigo == codigo)
            ?? throw new ArgumentException(
                $"No existe un producto con código '{codigo}'.");
        productos.Remove(producto);
        GuardarArchivo(productos);
    }

    public List<Producto> ObtenerTodos()
    {
        return LeerArchivo();
    }
}

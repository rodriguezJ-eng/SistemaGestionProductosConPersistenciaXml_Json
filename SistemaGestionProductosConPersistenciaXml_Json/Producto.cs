using System.Xml.Serialization;

public class Producto
{
    private string _codigo = string.Empty;
    private string _nombre = string.Empty;
    private decimal _precio;

    // Constructor sin parámetros requerido por XmlSerializer
    public Producto() { }

    public Producto(string codigo, string nombre, decimal precio)
    {
        Codigo = codigo;
        Nombre = nombre;
        Precio = precio;
    }

    
    public string Codigo
    {
        get => _codigo;
        set
        {
            if (int.TryParse(value, out int numero) && numero < 0)
                throw new ArgumentException("El código no puede ser un número negativo.");

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El código no puede estar vacío.");

            _codigo = value.Trim();
        }
    }

   
    public string Nombre
    {
        get => _nombre;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El nombre no puede estar vacío.");
            _nombre = value.Trim();
        }
    }

    
    public decimal Precio
    {
        get => _precio;
        set
        {
            if (value <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero.");
            _precio = value;
        }
    }

    public override string ToString()
    {
        return $"[{Codigo}] {Nombre} - ${Precio:F2}";
    }
}


IProductoRepository repositorio = new ProductoXmlRepository("productos.xml");
GestorProductos gestor = new GestorProductos (repositorio);

bool ejecutando = true;

while (ejecutando)
{
    MostrarMenu();
    string opcion = Console.ReadLine()?.Trim() ?? "";

    switch (opcion)
    {
        case "1": Agregar(); break;
        case "2": Buscar(); break;
        case "3": Actualizar(); break;
        case "4": Eliminar(); break;
        case "5": Listar(); break;
        case "6": Filtrar(); break;
        case "7": Ordenar(); break;
        case "8": Total(); break;
        case "9":
            Console.WriteLine("\n Saliendo del sistema. ¡Hasta pronto!");
            ejecutando = false;
            break;
        default:
            Console.WriteLine("\n Opción inválida. Intente de nuevo.");
            break;
    }

    if (ejecutando)
    {
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
        Console.Clear();
    }
}

static void MostrarMenu()
{
    Console.WriteLine("╔══════════════════════════════════╗");
    Console.WriteLine("║    === GESTOR DE PRODUCTOS ===   ║");
    Console.WriteLine("╠══════════════════════════════════╣");
    Console.WriteLine("║  1. Agregar producto             ║");
    Console.WriteLine("║  2. Buscar producto              ║");
    Console.WriteLine("║  3. Actualizar producto          ║");
    Console.WriteLine("║  4. Eliminar producto            ║");
    Console.WriteLine("║  5. Listar productos             ║");
    Console.WriteLine("║  6. Filtrar productos            ║");
    Console.WriteLine("║  7. Ordenar productos            ║");
    Console.WriteLine("║  8. Mostrar total de productos   ║");
    Console.WriteLine("║  9. Salir                        ║");
    Console.WriteLine("╚══════════════════════════════════╝");
    Console.Write("Seleccione una opción: ");
}


void Agregar()
{
    Console.WriteLine("\n--- AGREGAR PRODUCTO ---");
    string codigo = LeerTexto("Ingrese el código del producto: ");
    string nombre = LeerTexto("Ingrese el nombre del producto: ");
    decimal precio = LeerDecimal("Ingrese el precio del producto: ");
    Console.WriteLine(gestor.AgregarProducto(codigo, nombre, precio));
}

void Buscar()
{
    Console.WriteLine("\n--- BUSCAR PRODUCTO ---");
    string codigo = LeerTexto("Ingrese el código del producto: ");
    Console.WriteLine(gestor.BuscarProducto(codigo));
}

void Actualizar()
{
    Console.WriteLine("\n--- ACTUALIZAR PRODUCTO ---");
    string codigo = LeerTexto("Ingrese el código del producto a actualizar: ");

    var busquedaResultado = gestor.BuscarProducto(codigo);

    if(busquedaResultado.Contains("Error"))
    {
        Console.WriteLine($"\n No se puede actualizar: El producto con código '{codigo} no existe.");
    }

    string nuevoNombre = LeerTexto("Ingrese el nuevo nombre: ");
    decimal nuevoPrecio = LeerDecimal("Ingrese el nuevo precio: ");
    Console.WriteLine(gestor.ActualizarProducto(codigo, nuevoNombre, nuevoPrecio));
}

void Eliminar()
{
    Console.WriteLine("\n--- ELIMINAR PRODUCTO ---");
    string codigo = LeerTexto("Ingrese el código del producto a eliminar: ");
    Console.WriteLine(gestor.EliminarProducto(codigo));
}

void Listar()
{
    Console.WriteLine("\n--- LISTA DE PRODUCTOS ---");
    var lista = gestor.ListarProductos();
    if (lista.Count == 0)
    {
        Console.WriteLine(" No hay productos registrados.");
        return;
    }
    for (int i = 0; i < lista.Count; i++)
        Console.WriteLine($"  {i + 1}. {lista[i]}");
}

void Filtrar()
{
    Console.WriteLine("\n--- FILTRAR PRODUCTOS ---");

    Console.WriteLine("Seleccione el criterio:");
    Console.WriteLine(" 1. Menor que (<)");
    Console.WriteLine(" 2. Mayor que (>)");
    Console.WriteLine(" 3. Igual que (=)");
    Console.Write("Elija una opción (1-3): ");
    string criterio = Console.ReadLine()?.Trim() ?? "";

    if (criterio != "1" && criterio != "2" && criterio != "3")
    {
        Console.WriteLine(" Opción inválida. Debe elegir 1, 2 o 3.");
        return;
    }

    decimal valor = LeerDecimal("Ingrese el valor de referencia: ");

    var resultados = gestor.FiltrarProductos(valor, criterio);

    string simboloCriterio = criterio switch 
    { 
        "1" => "<",
        "2" => ">", 
        "3" => "=", 
        _ => "" 
    };

    Console.WriteLine($"\n Productos con precio {simboloCriterio} ${valor:F2}:");

    if (resultados.Count == 0)
        Console.WriteLine(" No se encontraron productos.");
    else
        resultados.ForEach(p => Console.WriteLine($" {p}"));
}

void Ordenar()
{
    Console.WriteLine("\n--- PRODUCTOS ORDENADOS (menor a mayor precio) ---");
    var lista = gestor.OrdenarProductos();
    if (lista.Count == 0)
    {
        Console.WriteLine(" No hay productos registrados.");
        return;
    }
    for (int i = 0; i < lista.Count; i++)
        Console.WriteLine($"  {i + 1}. {lista[i]}");
}

void Total()
{
    Console.WriteLine("\n--- TOTAL DE PRODUCTOS ---");
    Console.WriteLine($"  Total de productos registrados: {gestor.TotalProductos()}");
}

static string LeerTexto(string mensaje)
{
    while (true)
    {
        Console.Write(mensaje);
        string entrada = Console.ReadLine()?.Trim() ?? "";
        if (!string.IsNullOrWhiteSpace(entrada))
            return entrada;
        Console.WriteLine(" El valor no puede estar vacío.");
    }
}

static decimal LeerDecimal(string mensaje)
{
    while (true)
    {
        Console.Write(mensaje);
        string entrada = Console.ReadLine()?.Trim() ?? "";
        if (decimal.TryParse(entrada, out decimal valor))
            return valor;
        Console.WriteLine(" Valor inválido. Ingrese un número válido.");
    }
}
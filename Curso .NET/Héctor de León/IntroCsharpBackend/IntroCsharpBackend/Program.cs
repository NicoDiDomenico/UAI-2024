//////// Sección 2 - Introducción a C#

// Crear objeto usando la forma completa
Console.WriteLine("Trabajando con Clases y Objetos");
Sale venta1 = new Sale(100);
Console.WriteLine(venta1.GetInfo());

// Crear objeto usando var
var venta2 = new Sale(200);
Console.WriteLine(venta2.GetInfo());

// Crear objeto usando la sintaxis corta (C# 9+)
Sale venta3 = new(300);
Console.WriteLine(venta3.GetInfo());

// Modificar propiedad (porque tiene set)
venta1.Total = 150;
Console.WriteLine($"Nuevo total venta1: {venta1.Total}");

// Aplicar descuento usando un método público
venta1.AplicarDescuento();
Console.WriteLine($"Total venta1 con descuento: {venta1.Total}");

Console.WriteLine();
Console.WriteLine("Trabajando con Herencias");
var venta = new SaleWithTaxes(100, 2);
Console.WriteLine(venta.GetInfo()); // Virtual-Override --> Sobreescritura
Console.WriteLine(venta.GetInfo("Fin.")); // Sobrecarga
venta.AplicarDescuento();
Console.WriteLine($"Total venta con descuento: {venta.Total}");
Console.WriteLine(venta.GetInfoSaleWithTaxes());


//// 8. Creación de objetos
public class Sale
{
    private decimal _descuento;

    // protected lo veremos mas adelante
    
    public decimal Total { get; set; }

    public Sale(decimal total)
    {
        Total = total;
        _descuento = 5;
    }

    public virtual string GetInfo()
    {
        return "El total es: " + Total;
    }

    private void CalcularDescuento()
    {
        Total = Total - _descuento;
    }

    public void AplicarDescuento()
    {
        CalcularDescuento();
    }
}
//// 9. Herencia
class SaleWithTaxes : Sale
{
    public decimal Tax {  get; set; } 
    public SaleWithTaxes(decimal total, decimal tax) : base(total)
    {
        Tax = tax;
    }

    public string GetInfoSaleWithTaxes()
    {
        var totalFinal = Total + Tax;
        return "El total con impuesto es: " + totalFinal;
    }
    // Sobrescritura (overriding) --> Polimorfismo 1
    public override string GetInfo()
    {

        return "El total es: " + Total + ", y el impuesto es: " + Tax;
    }

    // Sobrecarga(overloading) --> Polimorfismo 2
    public string GetInfo(string mensaje)
    {

        return "El total es: " + Total + ", y el impuesto es: " + Tax + ". " + mensaje;
    }

    /// Duda Futuro --> ¿Y cómo hago si quiero usar el getInfo() del Padre y alguno del hijo? ya que al hacer overriding no puedo acceder al del Padre
}
////////
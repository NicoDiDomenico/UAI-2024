using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Backend.Modelos
{
    public class Brand
    {
        [Key] // Clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incremental
        public int BrandID { get; set; }
        public string Name { get; set; }
    }

    /*
    [Attributes]
    Los atributos en C# son una forma de agregar metadatos (información adicional) al código: clases, propiedades, métodos, parámetros, etc.
    Esos metadatos no cambian directamente el comportamiento del código por sí mismos.
    Pero el framework (ejemplo: ASP.NET Core, Entity Framework) o el runtime los interpreta y actúa en consecuencia.
    */
}

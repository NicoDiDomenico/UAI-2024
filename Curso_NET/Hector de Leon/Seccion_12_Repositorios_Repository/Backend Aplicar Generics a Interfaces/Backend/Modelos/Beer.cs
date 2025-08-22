using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Modelos
{
    public class Beer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BeerID { get; set; } // Clave primaria
        public string Name { get; set; } // Nombre de la cerveza

        [Column(TypeName = "decimal(18,2)")] // Con los decimal u otros tipos de datos se tendrá que aclarar en la etiqueta Column con cuantos decimales despues de la ',' se quiere trabajar en la BD  
        public decimal Alcohol { get; set; } // Contenido de alcohol en porcentaje

        public int BrandID { get; set; } // Clave primaria
        [ForeignKey("BrandID")] // Clave foránea que referencia a la marca
        public virtual Brand Brand { get; set; } // Relación con la marca
                                                 // La palabra clave virtual le dice a Entity Framework que esa propiedad puede ser sobreescrita dinámicamente (en tiempo de ejecución) mediante una técnica llamada Lazy Loading.
    }
}

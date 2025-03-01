using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion.Utilidades
{
    public class OpcionCombo // La clase OpcionCombo es un modelo simple que se usa para representar elementos en un ComboBox (lista desplegable).
    {
        public string Texto { get; set; } // Texto: Representa el texto que se va a mostrar en la lista desplegable del ComboBox.
        public object Valor { get; set; } // Valor: Representa el valor asociado al texto, que generalmente se usa para realizar operaciones lógicas o consultas a la base de datos. Como es de tipo object, puede almacenar cualquier tipo de dato (números, strings, etc.).
        public object DescripcionPermiso { get; set; }
    }
}

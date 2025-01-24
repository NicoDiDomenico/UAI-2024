using System;
using System.Collections.Concurrent;
using System.Threading.Channels;
using System.Timers;
using System.Xml;

namespace AprendiendoGenericos
{
    class Program
    {
        static void Main(string[] args)
        {
            // Almacenando objetos tipo String
            AlmacenObjetos<String> archivos = new AlmacenObjetos<String>(3); // Cuando instanciamos una clase que es generica debemos especificar en la instanciacion el tipo que vamos a instanciar + <T>

            archivos.agregar("Juan");

            archivos.agregar("Elena");

            archivos.agregar("Antonio");
                        
            String nombrePersona = archivos.getElement(2); 

            Console.WriteLine(nombrePersona);

            // Almacenando objetos tipo Empleado
            AlmacenObjetos<Empleado> empleados = new AlmacenObjetos<Empleado>(2);

            empleados.agregar(new Empleado(1500));

            empleados.agregar(new Empleado(2000));

            Console.WriteLine(empleados.getElement(1).getSalario());

        }
    }

    // Al reemplazar Object por <T> (generico) le estamos diciendo a C# oye esta clase va a manejar cualquier tipo de objeto (String, Empleado, Date, File, etc)
    class AlmacenObjetos<T>
    {
        private T[] datosElemento; // Dentro de este array podemos almacenar cualquier tipo de objeto

        private int i = 0;

        public AlmacenObjetos(int z) 
        {
            datosElemento = new T[z];
        }

        public void agregar( T obj) // Este metodo va a poder agregar cualquier tipo de objeto que se pase por parámetros
        {
            datosElemento[i] = obj;

            i++;
        }

        public T getElement(int i) // este geter va a devolver cualquier tipo de objeto
        { 
            return datosElemento[i];
        }
    }

    class Empleado
    {
        private double salario;

        public Empleado(double salario)
        {
            this.salario = salario;
        }

        public double getSalario()
        {
            return this.salario;
        }
    }
}
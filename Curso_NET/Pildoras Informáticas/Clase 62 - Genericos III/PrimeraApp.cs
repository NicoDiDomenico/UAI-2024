using System;
using System.Collections.Concurrent;
using System.Threading.Channels;
using System.Timers;
using System.Xml;

namespace GenericosRestricciones
{
    class Program
    {
        static void Main(string[] args)
        {
            AlmacenEmpleados <Director> empleados = new AlmacenEmpleados<Director>(3);

            empleados.agregar(new Director(4500));

            empleados.agregar(new Director(3500));

            empleados.agregar(new Director(2500));

            /*AlmacenEmpleados<Estudiante> estudiantes = new AlmacenEmpleados<Estudiante>(3);*/ // No me va a dejar porque no cumple con la restriccion de la interfaz para <T>

        }
    }
    public interface IParaEmpleados
    {
        double getSalario();
    }
    class Estudiante
    {
        private double edad; // Variable privada para almacenar la edad del estudiante.

        public Estudiante(double edad)
        {
            this.edad = edad; // Asignar el valor del parámetro al campo privado "edad".
        }

        public double getEdad()
        {
            return edad; // Devolver el valor de la edad.
        }
    }

    // Al reemplazar Object por <T> (generico) le estamos diciendo a C# oye esta clase va a manejar cualquier tipo de objeto (String, Empleado, Date, File, etc)
    class AlmacenEmpleados<T> where T : IParaEmpleados // Los objetos de esta clase se veran obligados a implementar con esta interfaz
    {
        private T[] datosEmpleados; // Dentro de este array podemos almacenar cualquier tipo de objeto

        private int i = 0;

        public AlmacenEmpleados(int z) 
        {
            datosEmpleados = new T[z];
        }

        public void agregar( T obj) // Este metodo va a poder agregar cualquier tipo de objeto que se pase por parámetros
        {
            datosEmpleados[i] = obj;

            i++;
        }

        public T getElement(int i) // este geter va a devolver cualquier tipo de objeto
        { 
            return datosEmpleados[i];
        }
    }

    class Director : IParaEmpleados
    {
        public Director(double salario)
        {
            this.salario = salario;
        }

        private double salario;

        public double getSalario()
        {
            return salario;
        }
    }
    class Secretaria : IParaEmpleados
    {
        public Secretaria(double salario)
        {
            this.salario = salario;
        }

        private double salario;

        public double getSalario()
        {
            return salario;
        }
    }

    class Electricista : IParaEmpleados
    {
        public Electricista(double salario)
        {
            this.salario = salario;
        }

        private double salario;

        public double getSalario()
        {
            return salario;
        }
    }
}
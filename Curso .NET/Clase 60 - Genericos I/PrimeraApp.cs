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
            AlmacenObjetos archivos = new AlmacenObjetos(4);

            archivos.agregar("Juan");

            archivos.agregar("Elena");

            archivos.agregar("Antonio");
                        
            String nombrePersona = (String)archivos.getElement(2); // tenemos que hacer un casting para que el error desaparezca

            Console.WriteLine(nombrePersona);

            // Almacenando objetos tipo Empleado
            Empleado unEmpleado = new Empleado(1500);

            archivos.agregar(unEmpleado);

            /* String nombrePersona2 = (String)archivos.getElement(3); */ // Notar como no da error en timepo de compilacion pero si de ejecucion --> Los genericos resuelvene sto

            Empleado unaPersona = (Empleado)archivos.getElement(3);

            Console.WriteLine(unaPersona.getSalario());
        }
    }

    class AlmacenObjetos
    {
        private Object[] datosElemento;

        private int i = 0;

        public AlmacenObjetos(int z) 
        {
            datosElemento = new Object[z];
        }

        public void agregar(Object obj)
        {
            datosElemento[i] = obj;

            i++;
        }

        public Object getElement(int i) 
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
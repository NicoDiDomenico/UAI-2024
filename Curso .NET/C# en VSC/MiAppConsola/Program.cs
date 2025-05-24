using System;

namespace contextoClases
{
    class HolaMundo
    {
        static void Main(string[] args)
        {
            // Comentario
            string palabra;
            string nombre = "Nico";

            palabra = "Hola " + nombre;

            Console.WriteLine("Hola Mundo. " + palabra);
            /*
            Comentario Ampliado,

            */

            Heroe.MostrarDescripcion();

            Heroe unHeroe = new Heroe("Lord Nico", "Espada", "Hielo");

            unHeroe.MostrarDatos();
        }
    }

    public class Heroe
    {
        private string nombreCompleto, arma, poder;

        public Heroe(string nom, string arm, string pod)
        {
            nombreCompleto = nom;
            arma = arm;
            poder = pod;
        }

        public static void MostrarDescripcion()
        {
            Console.WriteLine("Definiendo un Heroe un heroe");
        }

        public void MostrarDatos()
        {
            Console.WriteLine(nombreCompleto, arma, poder);
        }

    }
}

/*
============================== INSTALACIÓN DE C# EN VISUAL STUDIO CODE ==============================

1. INSTALAR .NET SDK:
   - Ir a: https://dotnet.microsoft.com/download
   - Descargar e instalar el SDK más reciente para tu sistema operativo.
   - Verificar la instalación con el comando en la terminal:
        dotnet --version

2. INSTALAR VISUAL STUDIO CODE:
   - Ir a: https://code.visualstudio.com/
   - Descargar e instalar VS Code.

3. INSTALAR EXTENSIÓN DE C# EN VS CODE:
   - Abrir Visual Studio Code.
   - Ir a la pestaña de extensiones (ícono de cuadrado o Ctrl+Shift+X).
   - Buscar e instalar la extensión oficial llamada "C#" (de Microsoft).

4. CREAR UN NUEVO PROYECTO C#:
   - Abrir una terminal y ejecutar:
        dotnet new console -n MiProyecto
        cd MiProyecto
        code .
   - Esto crea una aplicación de consola C#, entra en la carpeta y la abre en VS Code.

5. COMPILAR Y EJECUTAR:
   - Usar el comando:
        dotnet run

======================================================================================================
*/

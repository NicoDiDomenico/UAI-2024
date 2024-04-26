import os
import shutil

path = 'texto_eliminable.txt'
try:
    os.remove(path) #Esta funicon no elimina carpeta vacia.
    # shutil.rmtree(path) --> Cuidado, elimina el directorio y sus archivos dentro.
    print('Archivo eliminado')
except FileNotFoundError:
    print('No se encontró el archivo')

path2 = 'carpeta_eliminable.txt'
try:
    os.rmdir(path2) # Eliminar directorio
    print('Carpeta eliminada')
except FileNotFoundError:
    print('No se encontró la carpeta')
except PermissionError:
    print('No tenes permiso para eliminarla!!')
except OSError:
    print('No puedes eliminar eso usando esa funcion')
else:
    #En una estructura try-except, el bloque else se ejecutará solo si no se ha producido ninguna excepción dentro del bloque try. Esto significa que si ningún error ocurre durante la ejecución del código dentro del try, el bloque else se ejecutará inmediatamente después.
    print('Carpeta eliminada')
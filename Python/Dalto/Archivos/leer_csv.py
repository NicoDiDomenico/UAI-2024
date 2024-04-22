## CSV - Valores Separados con Comas
# Los archivos CSV son un tipo de documento en formato abierto sencillo para representar 
# datos en forma de tabla, en las que las columnas se separan por comas y las filas por 
# saltos de línea.

import csv # El código utilizará la biblioteca csv de Python para leer y mostrar el contenido de un archivo CSV

with open('Archivos\\datos.csv','r') as archivo:
    reader = csv.reader(archivo) #  La línea reader = csv.reader(archivo) se encarga de crear un objeto csv.reader 
                                 # que puede leer las líneas del archivo CSV proporcionado (archivo en este caso).
    for row in reader:
        print(row) 
        
# Para el análisis de datos vamos a usar la libreria pandas, no la csv. 
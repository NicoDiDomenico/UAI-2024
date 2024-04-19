# Archivos - Introducción
archivo = open('Archivos\\texto_de_nico.txt', encoding = 'UTF-8') # El encoding permite que se puedan utilizar todos los carácteres especiales
print(archivo.read())
print()

# Otra forma de leer el archivo completo:
archivo_sin_leer = open('Archivos\\texto_de_nico.txt', encoding = 'UTF-8') # 1er paso abrirlo
#archivo = archivo_sin_leer.read() # 2do paso leerlo
#print(archivo) # 3er paso mostrarlo (opcional)

# Leer linea por linea - OJO CON LA RAM
#lineas = archivo_sin_leer.readlines() # Para usar el .readlines() no se tiene que haber leido el archivo anteriormente
#print(lineas) # Devuelve un arreglo de lineas

# Leer una sola linea
linea = archivo_sin_leer.readline(2)
print(linea)
# Si no pongo nada: leer la primera
# Si pongo 0: lee todo (funciona como un .readlines())
# Si pongo un nro. entero como 1, 2, 3, ...: leer según la cantidad de carácteres ingresada como argumento

# Cerrar el archivo
archivo_sin_leer.close() # Ya no podemos volver a leerlo a no ser que se vuelva a abrir.
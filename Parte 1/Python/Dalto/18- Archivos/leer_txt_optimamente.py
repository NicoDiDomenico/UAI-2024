# Lo que buscaremos es que se cierre automaticamente una vez usado
# Anteriormente lo haciamos asi:
archivo_sin_leer = open('texto_de_nico.txt', encoding = 'UTF-8') # 1er paso abrirlo
archivo = archivo_sin_leer.read() # 2do paso leerlo
print(archivo) # 3er paso mostrarlo (opcional)
archivo_sin_leer.close()

print()
# With - USAR ESTA FORMA!!
# Podemos leerlo y cerrarlo optimamente de la siguiente forma: 
try:
    with open('texto_de_nico.txt', encoding = 'UTF-8') as file:
        print(file.read())
        print(file.closed)  # Devuelve True si efectivamente se cerró
except FileNotFoundError:
    print('El archivo no fue encontrado.')


#   En este caso, estás utilizando la declaración with, que automáticamente se encarga de cerrar el archivo 
# una vez que se sale del bloque de código. No necesitas llamar a archivo_sin_leer.close() explícitamente. 
# Este enfoque es más seguro y más conciso, ya que garantiza que el archivo se cierre correctamente incluso 
# si ocurre una excepción dentro del bloque with.

# Otra forma:
print()
with open('texto_de_nico.txt','r',encoding = 'UTF-8') as archivo:
    for linea in archivo:
        print(linea)

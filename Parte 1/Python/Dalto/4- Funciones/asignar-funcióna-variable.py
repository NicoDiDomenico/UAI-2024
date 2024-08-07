# 52
def hola():
    print('Hola')

hola() # Son los () lo que llama a la funcion
print(hola) # Notar que al imprimir la funcion nos muestra la direccion de memoria
h = hola # ahora h y hola apuntan a la misma direccion de memoria
h() # Ahora h tiene la direccion que al agregar () nos lleva a la misma funcion que hola()

# Ejemplo practico
decir = print
decir('Sorprendente')
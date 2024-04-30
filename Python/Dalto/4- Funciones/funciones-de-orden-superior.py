# 53
# Estas son funciones que pueden hace sdos cosas:
# - Aceptan una funcion como argumente. funcion(func)
# - Devuelven una funcion como resultado. def funcion():
#                                            return func
# En python esto es totalmente valido porque se tratan a las funciones como objetos

# Ejemplo 1
def hablarAlto(texto):
    return texto.upper()

def hablarBajo(texto):
    return texto.lower()

def hola(func):
    texto = func('Hola')
    print(texto)

hola(hablarAlto) # notar como usamos la func sin (), ya que de esta manera trabajamos con su direccion de memoria
hola(hablarBajo)

# Ejemplo 2
def divisor(x):
    def dividendo(y):
        return y / x
    return dividendo

divide = divisor(2)
print(divide(10))
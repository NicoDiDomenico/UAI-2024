# Las funciones se crean con def 
# 4- Funciones simples
'''
def saludar():
    print('Buenos dias amiguito!!!')

print()
saludar()
print()
'''
'''
# 4- Funciones compuestas
def saludar(nombre, sexo):
    sexo = sexo.lower()
    if sexo == 'mujer':
        adjetivo = 'maestra'
    elif sexo == 'hombre':
        adjetivo = 'maestro'
    else:
        adjetivo = 'crack'
    print(f'Buenos dias {nombre}!!! Cómo estás {adjetivo}?')

saludar(nombre = 'Nico', sexo = 'Hombre') # Usando argumentos de PALABRA CLAVE
saludar('Nicol', 'Mujer')   # Usando argumentos de POSICIONALES
saludar('Nique', 'no obinario porque soy retrasado')
'''
'''
# Return - Crear una función que nos retorne valores
def generar_contrasena_aleatoria(num):
    obtener_primer_numero = str(num)
    primer_numero = int(obtener_primer_numero[0])
    n = primer_número  
    contrasena_base = 'abcdefghij'
    c = contrasena_base
    contrasena = c[n-2]+c[n-1]+c[n-4]+c[n-3]+c[n]+c[n-5]+c[n-7]+c[n]+c[n-3]
    return contrasena, primer_numero

n = float(input('Ingrese un número: '))
n = round(n)
contrasena, num = generar_contraseña_aleatoria(n) # Desempaquetado
print(f'Tu contraseña es: {contrasena}')
print(f'Para generarla usamos: {num}')
'''
'''
# Funciones anidadas:
# Transformar cualquier numero en entero positivo:
# MAL
num = input('Ingresa un Numero: ')
num = float(num)
num = abs(num)
num = float(num)
num = round(num)
print(num)
# BIEN - Usamos funciones anidadas
print(round(abs(float(input('Ingresa un Numero: ')))))
'''
# Ámbito de las variables
# - Es la region en la que una variable es reconocida. Una variable solo está disponible desde dentro de la región en la que se crea.
nombre = 'Di Domenico' # Variable global
def mostrar_nombre():
    nombre = 'Nico' # Variable Local - ámbito local, solo está disponible dentro de esta función
    print(nombre) # Regla LEGB - Prioridad de Variables: locales, cercanas, global integradas

mostrar_nombre()
print(nombre)





# Las funciones se crean con def 
# Funciones simples 
''' 
def saludar():
    print('Buenos dias amiguito!!!')

print()    
saludar()
print()
'''

# Funciones compuestas
'''
def saludar(nombre, sexo):
    sexo = sexo.lower()
    if sexo == 'mujer':
        adjetivo = 'maestra'
    elif sexo == 'hombre':
        adjetivo = 'maestro'
    else:
        adjetivo = 'crack'
    print(f'Buenos dias {nombre}!!! Cómo estás {adjetivo}?')

saludar('Nico', 'Hombre')    
saludar('Nicol', 'Mujer')    
saludar('Nique', 'no obinario porque soy retrasado')
'''

# Crear una función que nos retorne valores

def generar_contraseña_aleatoria(num):
    obtener_primer_número = str(num)
    primer_número = int(obtener_primer_número[0])
    n = primer_número  
    contraseña_base = 'abcdefghij'
    c = contraseña_base
    contraseña = c[n-2]+c[n-1]+c[n-4]+c[n-3]+c[n]+c[n-5]+c[n-7]+c[n]+c[n-3]
    return contraseña, primer_número 

n = float(input('Ingrese un número: '))
n = round(n)
contraseña, num = generar_contraseña_aleatoria(n) # Desempaquetado
print(f'Tu contraseña es: {contraseña}')
print(f'Para generarla usamos: {num}')






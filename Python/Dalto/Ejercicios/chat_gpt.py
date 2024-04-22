#1 Imprime "Hola, mundo" en la consola.
print('Hola, mundo', end= '\n\n')

#2 Realiza operaciones aritméticas simples (suma, resta, multiplicación, división).
a = float(input('Ingrese un número(a): '))
b = float(input('Ingrese un número(b): '))
sum = a + b
res = a - b
mul = a * b
div = a / b 
print(f'La suma de a y b es: {sum}')
print(f'La resta de a y b es: {res}')
print(f'La multiplicación de a y b es: {mul}')
print(f'La división de a y b es: {div}', end= '\n\n')

#3 Escribe un programa que calcule el área de un círculo. Puedes usar la fórmula π.r^2.
import math 
'''
La línea de código import math en Python significa que estás importando el módulo
math en tu programa. El módulo math proporciona funciones matemáticas estándar, 
como funciones trigonométricas, logarítmicas, exponenciales, funciones para trabajar
con números complejos, y constantes matemáticas como π (pi) y e.
'''
print('Área de un círculo...')
r = float(input('Ingrese el radio de su círculo(r): '))
area_circulo = math.pi * r**2
print(f'El área es: {area_circulo}', end= '\n\n')

#4 Crea un programa que determine si un número es positivo, negativo o cero.
while True:
    num = float(input('Ingrese un número: '))
    
    if num != 0:
        if num > 0:
            print('El número es positivo.')
        else:
            print('El número es negativo.')
    else:
        print('El número ingresado es 0')
    
    rta = input('¿Quiere ingresar otro número? (Si/No): ')
    rta = rta.lower()

    if rta != 'si':
        break  # Sale del bucle si la respuesta no es 'si'
    
#Otra forma
num = float(input('Ingrese un número: '))
if num > 0:
    print('El número es negativo.')
elif num > 0:
    print('El número es positivo') 
else:
    print('El número es 0') 
    
#5 Imprime los primeros 5 números naturales usando un bucle while.
contador = 0
posicion = 1
print('Los primeros 5 números naturales son...')
while contador <= 4:
    print(f'[{posicion}]: {contador} ')
    contador += 1
    posicion += 1
    
#6 Crea una lista de nombres y luego imprime cada nombre en la lista.
lista_nombres = ['Nico', 'Bruno', 'Ivan', 'Osvaldo', 'Roy']
print('La lista es...')
print(lista_nombres)
#Otra forma
print('La lista es...')
print(lista_nombres[0])
print(lista_nombres[1])
print(lista_nombres[2])
print(lista_nombres[3])
print(lista_nombres[4])
#La forma correcta seria con un for, no se hacerlo todavia

#7 Define una función que tome dos parámetros y devuelva su suma.

#Plus
type(9)
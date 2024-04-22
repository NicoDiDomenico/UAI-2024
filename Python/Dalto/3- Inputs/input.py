# Pedirle un dato al usuario
nombre = input('Ingrese nombre: ') # input siempre devuelve STR
print(f'Tu nombre es: {nombre}')
print('Tu nombre es: ' + str(nombre))
print()

numero = int(input('Ingrese numero: '))
print(f'El número ingresado es: {numero}')
print('El número ingresado es: ' + str(numero))
print()

decimal= float(input('Ingrese decimal: '))
print(f'El decimal ingresado es: {decimal}')
print(f'El decimal ingresado es: ' + str(decimal))
print()

# Ej. de como funciona imput con str y con int
print('STR*2')
numero1 = input('Flaco dame un número: ')
print(f'Tu edad es: {numero1*2}\n') # se multiplica el caracter x2 => ej. a*2 = aa (dos veces a)

print('INT*2')
numero2 = int(numero1)
print(f'Tu edad es: {numero2*2}\n') # al ser entero se hace la multiplicacion convencional
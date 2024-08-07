# While - mientras la condicion se cumpla el bucle se va a seguir ejecutando (vuelta tras vuelta se ejecuta la condición)

# Bucles infinitos
#1
# while 1==1:
#    print("Estoy cansado Jefe :c")
#2
# while True:
# print('infinito')

nombre = ""
while not nombre or len(nombre) == 0:
    nombre = input('Ingrese su nombre: ')
print('Hola ' + nombre)

contador = 0
while contador < 10:
    contador += 1
    print(f'El valor del contador es {contador}')
    # contador += 1
    
print('El while llegó a su fin.')
# Creando los datos
datos_lista = ['Nico', 'Di Domenico',1000000] 
datos_tupla = ('Nico', 'Di Domenico',1000000)
datos_conjunto = {'Nico', 'Di Domenico',1000000}

# Desempaquetado -  acción de extraer los elementos de una estructura de datos iterable, como una lista o una tupla, y asignarlos a variables individuales. 
nombre, apellido, suscriptores = datos_lista
nombre2, apellido2, suscriptores2 = datos_tupla
nombre3, apellido3, suscriptores3 = datos_conjunto

# Mostrando resultado
print('Lista:')
print(nombre)
print(apellido)
print(suscriptores)

print('\nTupla:')
print(nombre2)
print(apellido2)
print(suscriptores2)

print('\nConjunto:')
print(nombre3)
print(apellido3)
print(suscriptores3)
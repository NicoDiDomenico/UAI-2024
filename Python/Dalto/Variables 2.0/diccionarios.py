# Creando diccionarios con dict()
diccionario = dict(nombre = 'Lionel', apellido = 'Messi')
print(f'Creando diccionarios con dict(): {diccionario}\n')

# Se pueden poner tuplas como clave ya que son hasheables
dic_con_tupla = {
    (0,1,2,3) : 'Lionel'
}
print(f'Se pueden poner tuplas como clave ya que son hasheables: {dic_con_tupla}')

# Como las claves de los diccionarios tienen que ser hasheables(inmutables) puedo usar conjuntos mediante frozenset()
conjunto = set(['Hola', True, 1])
dic_con_frozenset = {
    frozenset(conjunto) : 'Lionel'
}
print(f'Con frozenset() puedo usar conjuntos como Claves: {dic_con_frozenset}\n')

# Creando diccionarios con fromkeys
# Crear llaves sin valor
dicc_fromkeys1 = dict.fromkeys(['Legajo', 'Nombre', 'Apellido'])
print(dicc_fromkeys1)
print('\n')
# Crear llaves con el mismo valor 
dicc_fromkeys2 = dict.fromkeys('ABC', '-')
print(dicc_fromkeys2)
dicc_fromkeys3 = dict.fromkeys(['Legajo', 'Nombre', 'Apellido'], '-')
print(dicc_fromkeys3)
print('\n')

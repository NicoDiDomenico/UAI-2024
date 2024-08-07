# Datasos
definoDeEstaFormaLasVariable = 7 #CamelCase
defino_de_esta_forma_las_variables = 7 #Snake_case (python recomienda esta forma)

# Conceptos
'''
1°) nom = (estoy DECLARANDO la variable)
2°) nom = 'Nico' (estoy DEFINIENDO la variable)
3°) nom = 'Lucas' (estoy REDEFINIENDO la variable)
'''
nom = 'Nico'
print('Hola me llamo ' + nom)
print(f'La variable es del tipo: {type(nom)}')

num = 10
num += 1
print(num)

print()
num = 10
num -= 1
print(num)

# Concatenar
# Forma 1:
nombre = 'Juan'
saludo = 'Hola' + ' ' + 'como ' + 'estas ' + nombre
print(saludo)


# Forma 2:
saludo2 = f"Hola como estas {nombre}"

# Esto es una expresión dentro de la f-string. 
# Lo que está dentro de las llaves {} es evaluado y su resultado es 
# convertido a una cadena y se coloca en ese lugar en la cadena. 
print(saludo2)

edad = 26
edad += 1
palabra1 = f"Mi edad es {edad}"
print(palabra1)

# print mostrara el texto del mismo tipo:
print(f'Tu edad es:' + str(edad))

# Operador de pertenencia in | not in
print("ed" in palabra1)
print('ed' not in palabra1)

# Float
altura = 180.5
print('Tu altura es ' + str(altura))

# Boolean
humano = False
print(humano)
print(type(humano))
print("Eres un humano: " + str(humano))

# Asignacion multiple
# 1
nombre, edad, atractivo = 'Alex', 24, True

print(nombre)
print(edad)
print(atractivo)

# 2
# MAL
'''
nombre1 = 10
nombre2 = 10
nombre3 = 10
nombre4 = 10
'''
# Bien
nombre1 = nombre2 = nombre3 = nombre4 = 10

print(nombre1)
print(nombre2)
print(nombre3)
print(nombre4)




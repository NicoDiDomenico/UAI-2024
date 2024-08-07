#List - No es hasheable, son mutables, lo que significa que puedes cambiar su contenido (agregar, eliminar o modificar elementos) después de su creación.
lista = ["Estoy aprendiendo python", True, 26, 1.75]
print(lista, end="\n\n")
print(lista[0])
print(lista[1])
print(lista[2])
print(lista[3])

#Tupla - hasheable, son inmutables, lo que significa que no puedes cambiar su contenido después de su creación. Ahorra memoria
tupla = ("Estoy aprendiendo python", True, 26, 1.75)
print(lista, end="\n\n")
print(lista[0])
print(lista[1])
print(lista[2])
print(lista[3])

#Set - No es hasheable, se puede redefinir pero no modificar sus elementos, o sea son son mutables, pero los elementos dentro de un conjunto deben ser únicos y no se pueden indexar, es decir, no se puede acceder a elementos individuales mediante un índice numérico.
conjunto = {"Estoy aprendiendo python", True, 26, 1.75}
print(conjunto)
#print(conjunto[1]) (no se accede a elementos por indice, no almacena datos duplicados)

#Dict - Los diccionarios son mutables y se utilizan para almacenar pares clave-valor. Cada clave en un diccionario debe ser única. Las claves en un diccionario deben ser hasheables.
diccionario ={
    'Descripcion' : "Estoy aprendiendo python",
    'Estudia' : True,
    'Edad' : 26,
    'Altura' : 1.75
}    #Key    : Value
print(f"Edad: {diccionario['Edad']}") # Forma 1, si usas 'Edad' entonces poner el f-string entre " " y viceversa
print('Edad:',diccionario['Edad']) # Forma 2

# Definicion de Hasheable
'''
Capacidad de un objeto de ser utilizado como clave en una tabla hash. Un objeto hashable en Python debe tener dos 
propiedades clave:

Inmutabilidad: El objeto no puede cambiar su estado después de ser creado. Es decir, no se pueden realizar 
modificaciones en el objeto.

Hash constante: El objeto debe tener un valor hash que permanezca constante durante su vida útil. El valor hash 
es un número entero que se calcula a partir del contenido del objeto y se utiliza para indexar la posición del 
objeto en la tabla hash. En resumen cada dato tiene un único valor hash que se utilizará como indice para 
asignar un espacio en memoria, por lo tanto si borras el espacio de memoria con ese indice borras el valor del dato 
'''

# Recordar:
# Hasheable -> Inmutable (sus datos no se pueden modificar)
# No Hasheable -> Mutable (sus datos se pueden modificar)


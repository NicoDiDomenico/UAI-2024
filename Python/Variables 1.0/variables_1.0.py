nom = 'Nico'
print(nom)

#Conceptos
'''
1°) nom = (estoy DECLARANDO la variable)
2°) nom = 'Nico' (estoy DEFINIENDO la variable)
3°) nom = 'Lucas' (estoy REDEFINIENDO la variable)
'''

num = 10
num += 1
print(num)

print()
num = 10
num -= 1
print(num)

#Concatenar
nombre = 'Juan'
saludo = 'Hola' + ' ' + 'como ' + 'estas ' + nombre
print(saludo)
#Mejor forma
saludo2 = f"Hola como estas {nombre}" 
# Esto es una expresión dentro de la f-string. 
# Lo que está dentro de las llaves {} es evaluado y su resultado es 
# convertido a una cadena y se coloca en ese lugar en la cadena. 
print(saludo2)

edad = 26 
palabra1 = f"Mi edad es {edad}" 
#del palabra1 #(borra datos )
print(palabra1)

#Operador de pertenencia in | not in
print("ed" in palabra1)
print('ed' not in palabra1)

#Datasos
definoDeEstaFormaLasVariable = 7 #CamelCase 
defino_de_esta_forma_las_variables = 7 #Snake_case (python recomienda esta forma)
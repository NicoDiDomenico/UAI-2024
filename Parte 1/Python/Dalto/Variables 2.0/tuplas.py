# Creando tupla con tuple()
lista = list(['Hola', 12, 7])
nueva_tupla = tuple(lista) # Como parámetro va una lista
# nueva_tupla = tuple(['Hola', 12, 7])
print(nueva_tupla)

# Otra forma de crearla
nueva_tupla2 = 'dato1','dato2' 
print(nueva_tupla2)
nueva_tupla3 = 'único_dato', # notar que si quiero crear una tupla de esta forma y cn un solo dato debo poner una ',' al final porque sino es un string
print(nueva_tupla3) 
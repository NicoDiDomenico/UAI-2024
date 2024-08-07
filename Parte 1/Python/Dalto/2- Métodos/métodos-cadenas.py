
cadena1 = 'Hola soy Nicolás'
cadena2 = 'Bienvenido crack'
cadena3 = 'vIVA LA LIBERTAD CARAJO'
lista = [1,'Pepe', 'Juan', 20.3]
cadena4 = '7'
cadena5 = 'fjkj'
cadena6 = 'dsfsdf45465'
cadena7 = 'hola'
cadena8 = 'chau'
cadena9 = 'Buenas tardes, soy Nicolás Di Domenico, me duele la cabeza.'

# FUNCIÓN
# Dir - devuelve la lista de atributos válidos del objeto pasado 
print('DIR:')
rta = dir(cadena1) # --> dir es una función 
print(rta)
print()

# MÉTODOS DE CONVERSION
# UPPER - convierte a mayúscula
print('UPPER:')
print(f'{cadena1.upper()}\n') # --> upper es un método
# LOWER - convierte a minúscula
print('LOWER:')
print(f'{cadena1.lower()}\n')
# CAPITALIZE - primera en mayúscula y el resto minúscula
print('CAPITALIZE:')
print(f'{cadena3.capitalize()}\n')

# MÉTODOS DE BÚSQUEDA
# FIND - método encuentra la primera aparición del valor especificado, sino devuelve 1
print('FIND:')
metodo_find = cadena1.find('soy') # Tiene que ser str
print(f'{metodo_find}\n')
# INDEX - método encuentra la primera aparición del valor especificado, sino devuelve una excepción
print('INDEX:')
metodo_index = cadena1.index('soy') # Tiene que ser str
print(f'{metodo_index}\n') #La diferencia con find es que el primero devuelve un -1 si no encuentra nada, y este último un error

# MÉTODOS DE CONSULTA
# ISDIGIT - Solo retorna True para cadenas que contienen exclusivamente dígitos del 0 al 9
print('ISDIGIT:')
metodo_digit = cadena6.isdigit()
print(f'{metodo_digit}\n')
# ISNUMERIC - devuelve True si todos los caracteres en la cadena son caracteres numéricos, incluidos los dígitos y otros caracteres que representan números, como números romanos, números fraccionarios, subíndices y superíndices.
print('ISNUMERIC:')
metodo_numerico = cadena4.isnumeric()
print(f'{metodo_numerico}\n') # Lo interesante es que si bien es str, puede darse cuenta que el str está compuesto por un número
# ISALPHA - si son letras alfébetica devuelve true 
print('ISALPHA:')
metodo_alfa = cadena5.isalpha()
print(f'{metodo_alfa}\n') 
# ISALNUM - verificar si una cadena está formada por caracteres alfanuméricos
print('ISALNUM:')
metodo_alnum = cadena6.isalnum()
print(f'{metodo_alnum}\n')

# COUNT - devuelve el número de ocurrencias de una subcadena en la cadena dada
print('COUNT:')
buscar_coincidencias = cadena6.count('f')
print(f'{buscar_coincidencias}\n') 
# LEN - cuenta los caracteres de una cadena 
print('LEN:')
metodo_len = len(cadena6) # en este caso es una función
print(f'{metodo_len}\n')  # sirve con arreglos para devolver la can. max de los mismos

# ENDSWITH - verifica si una cadena comienza con... 
print('ENDSWITH:')
termina_con = cadena2.endswith("ck") # en este caso es una función
print(f'{termina_con}\n') 

# STARSWITH - verifica si una cadena termina con...  
print('STARSWITH:')
empieza_con = cadena2.startswith('B') # en este caso es una función
print(f'{empieza_con}\n') 

# REPLACE - reemplaza un valor por otro
print('REPLACE:')
print(cadena7)
print(cadena7.replace('hola','Hola compañero'))
print(cadena7.replace('h','H'))
print()

# SPLIT - separa por el parámetro dado
print('SPLIT:')
cadena_separada = cadena9.split(',') 
print(len(cadena_separada))
print(f'{cadena_separada[0]}')
print(f'{cadena_separada[1]}')  
print(f'{cadena_separada[2]}') 
print(f'{cadena_separada}\n') # notar que devuelve un arreglo
print()

# Caracteristica útil:
nombre = 'Nico'
print(nombre*4)
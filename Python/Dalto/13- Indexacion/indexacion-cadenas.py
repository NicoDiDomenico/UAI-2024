nombre = 'Nico Di Domenico'
print(len(nombre[0:4]))
if nombre[0:4] == 'Nico':
    print('Te llamas Nicolás')

print()
if nombre[0].islower():
    print('Tu nombre comienza con minuscula')
else:
    print('Tu nombre comienza con mayuscula')

print()
if nombre[0].islower():
    nombre = nombre.capitalize()
print(nombre)

primer_nombre = nombre[:4].upper()
apellido = nombre[5:].lower()
ultimo_caracter = nombre[-1]

print(primer_nombre)
print(apellido)
print(ultimo_caracter)


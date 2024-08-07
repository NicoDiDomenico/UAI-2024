#    Tenemos 2 listas, una con nombre y  otra  con apellido. Tenemos que escribir los datos en un archivo de 
# texto de forma óptima con un for.

# Lista Nombres
nombres_lista = ['Nico', 'Juan', 'Pepe', 'Rodrigo', 'Emilia']

# Lista Apellidos
apellidos_lista = ['Rodriguez', 'Sarate', 'Costagura', 'Romano', 'Mancineli']

# Registramos en un txt la información solicitada
with open('ArchivosProblemasResueltos\\lista_nombre.txt','w', encoding = 'UTF-8') as archivo:
    archivo.write('Lista de nombres:\n')
    for n in nombres_lista:
        archivo.write(f'- {n}\n')
        
with open('ArchivosProblemasResueltos\\lista_apellido.txt','w', encoding = 'UTF-8') as archivo:
    archivo.write('Lista de apellidos:\n')
    for a in apellidos_lista:
        archivo.write(f'- {a}\n')
        
# Forma óptima:
with open('ArchivosProblemasResueltos\\lista_nombre_apellido.txt','w', encoding = 'UTF-8') as archivo:
    archivo.write('Lista de nombres y apellidos:\n')
    for tupla in zip(nombres_lista,apellidos_lista):
        archivo.write(f'- {tupla[1]}, {tupla[0]} \n')
        
# Optimizacion ultimate ninja 3 storm
with open('ArchivosProblemasResueltos\\lista_nombre_apellido2.txt','w', encoding = 'UTF-8') as archivo:
    archivo.write('Lista de nombres y apellidos:\n')
    [archivo.write(f'- {tupla[1]}, {tupla[0]} \n') for tupla in zip(nombres_lista,apellidos_lista)]
# - el for de una sola linea lleva [] y se escribe de manera inversa sin los ':'
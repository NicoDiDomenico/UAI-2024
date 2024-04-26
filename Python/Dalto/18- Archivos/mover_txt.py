import os

origen = 'texto_portable.txt'
destino = 'C:\\Users\\Nicol\Desktop\\texto_portable.txt'

try:
    if os.path.exists(destino):
        print('Ya hay un archivo en este destino')
    else:
        os.replace(origen, destino)
        print(origen + ' fue movido')
except FileNotFoundError:
    print(origen + ' no fue encontrado')

# Esto mismo tambien se puede hacer con un directorio


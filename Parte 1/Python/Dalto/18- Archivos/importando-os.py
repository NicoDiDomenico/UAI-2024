import os

path = 'C:\\Users\\Nicol\Desktop\\UAI\Cursada 2024\Python\Dalto\Archivos\\test.txt'

if os.path.exists(path):
    print('Esa ubicacion existe')
    if os.path.isfile(path):
        print('Es un archivo')
    elif os.path.isdir(path):
        print('Es un directorio')
else:
    print('Esa ubicacion no existe')
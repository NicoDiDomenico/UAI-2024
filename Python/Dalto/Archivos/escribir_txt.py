# Con el permiso 'w' si no encuentra el archivo lo crea, si lo encuentra lo sobreescribe.
# .write - Escribe una linea en el architivo.txt
with open('Archivos\\texto_de_nico.txt', 'w',encoding = 'UTF-8') as archivo:
    archivo.write('NASHEEEEEEEEEEEEEEEE1')
    archivo.write('\nNASHEEEEEEEEEEEEEEEE2\n')

# .writelines - Escribe varias lineas en el architivo.txt
with open('Archivos\\texto_de_nico.txt', 'w',encoding = 'UTF-8') as archivo:
    archivo.writelines(['NASHEEEEEEEEEEEEEEEE3','\nNDEAAAAH'])
    archivo.writelines(['\nNDEAAAAAAAAAAAAAAH','\nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\n\n'])
    

    



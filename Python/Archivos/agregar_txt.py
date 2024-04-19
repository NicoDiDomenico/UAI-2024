# Con el permiso 'a' si no encuentra el archivo lo crea, si lo encuentra lo agrega.
# .write - Escribe una linea en el architivo.txt
with open('Archivos\\texto_de_nico.txt', 'a',encoding = 'UTF-8') as archivo:
    archivo.write('NASHEEEEEEEEEEEEEEEE1')
    archivo.write('\nNASHEEEEEEEEEEEEEEEE2\n')

# .writelines - Escribe varias lineas en el architivo.txt
with open('Archivos\\texto_de_nico.txt', 'a',encoding = 'UTF-8') as archivo:
    archivo.writelines(['NASHEEEEEEEEEEEEEEEE3','\nNDEAAAAH'])
    archivo.writelines(['\nNDEAAAAAAAAAAAAAAH','\nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\n\n'])

# with + for 
with open('Archivos\\texto_de_nico.txt','a', encoding = 'UTF-8') as archivo:
    for i in range(10):
        archivo.write(f'Linea {i+1}\n')
        
#  Notar como con 'a' al estar continuamente ejecutando el programa siempre se va a ir agregando y nunca
# sobrescribiendo.
 
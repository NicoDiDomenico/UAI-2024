# Con el permiso 'a' si no encuentra el archivo lo crea, si lo encuentra lo agrega.
# .write - Escribe una linea en el architivo.txt
with open('text.txt', 'a', encoding = 'UTF-8') as archivo:
    archivo.write('Texto agreagado.\n')

# .writelines - Escribe varias lineas en el architivo.txt
with open('text.txt', 'a',encoding = 'UTF-8') as archivo:
    archivo.writelines(['Texto agreagado 1, ', 'texto agreagado 2.\n'])
    archivo.writelines(['Texto agreagado 3, ', 'texto agreagado 4.\n'])

# with + for 
with open('text.txt','a', encoding = 'UTF-8') as archivo:
    for i in range(10):
        archivo.write(f'Linea {i+1}\n')
        
#  Notar como con 'a' al estar continuamente ejecutando el programa siempre se va a ir agregando y nunca
# sobrescribiendo.
 
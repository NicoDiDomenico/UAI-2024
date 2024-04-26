# Con el permiso 'w' si no encuentra el archivo lo crea, si lo encuentra lo sobreescribe.
# .write -  Escribe una linea en el architivo.txt
texto = 'Hola\nUsando w\nQue tenga un buen dia\n'
with open('text.txt', 'w', encoding = 'UTF-8') as archivo:
    archivo.write(texto)


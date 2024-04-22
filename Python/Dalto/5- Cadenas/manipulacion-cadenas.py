nombre = "Alex Smith"
primer_nombre = nombre[0:4] #[inclusivo,exclusivo]
apellido = nombre[5:]
nombre_cada_dos = nombre[0:10:2]
nombre_invertido = nombre[::-1]

# Otro forma de recortar una cadena de caracteres
website = "http://www.google.com"
slice = slice(11,-4)
sitio = website[slice]

print(primer_nombre)
print(apellido)
print(nombre_cada_dos)
print(nombre_invertido)
print(sitio)


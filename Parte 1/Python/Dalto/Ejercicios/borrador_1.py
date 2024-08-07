def obtener_articulo(fruta):
    articulos = {"manzana": "una", "plátano": "un", "uva": "una", "arándano": "un"}

    # Convierte la fruta a minúsculas para evitar problemas con mayúsculas/minúsculas
    fruta = fruta.lower()

    # Verifica si la fruta está en el diccionario
    if fruta in articulos:
        # Imprime el resultado con el artículo correcto
        print(f"Es {articulos[fruta].capitalize()} {fruta}")
    else:
        print("No conozco esa fruta")

# Ingresa la lista de frutas
frutas = ["manzana", "plátano", "uva", "arándano", "pera"]

# Itera sobre la lista de frutas e imprime el resultado para cada una
for fruta in frutas:
    obtener_articulo(fruta)

print('----------------------------------')

def obtener_articulo(fruta):
    articulos = {"manzana": "una", "plátano": "un", "uva": "una", "arándano": "un"}

    # Convierte la fruta a minúsculas para evitar problemas con mayúsculas/minúsculas
    fruta = fruta.lower()

    # Verifica si la fruta está en el diccionario
    if fruta in articulos:
        # Imprime el resultado con el artículo correcto
        print(f"Es {articulos[fruta].capitalize()} {fruta}")
    else:
        print("No conozco esa fruta")

# Solicita al usuario ingresar una lista de frutas separadas por comas
frutas_ingresadas = input("Ingresa una lista de frutas separadas por comas: ")
frutas_lista = [fruta.strip() for fruta in frutas_ingresadas.split(',')]

# Itera sobre la lista de frutas e imprime el resultado para cada una
for fruta in frutas_lista:
    obtener_articulo(fruta)

# Basico Listas
comidas = ['Pizzas', 'Empanadas', 'Hamburguesas', 'Tacos']
print(comidas)
print(comidas[0])
comidas[0] = 'Helado'
print(comidas)

print('Lista:')
for x in comidas:
    print(x)

print()
# Listas 2D
bebidas = ['Café', 'Soda', 'Té']
cena = ['Pizzas', 'Empanadas', 'Hamburguesas']
postres = ['Pastel', 'Helado']

comida = [bebidas, cena, postres]

print(comida)
print(type(comida))

print('\nBebidas:')
print(comida[0][2])
print(comida[0][1])
print(comida[0][0])

print('\nCena:')
print(comida[1][2])
print(comida[1][1])
print(comida[1][0])

print('\nPostres:')
print(comida[2][1])
print(comida[2][0])
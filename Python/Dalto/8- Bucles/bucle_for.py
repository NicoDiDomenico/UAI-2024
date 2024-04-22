import time

for x in range(5):
    print(x)

print()

# Establecemos un piso
for x in range(1, 5): # [INLUSIVO|EXCLUSIVO]
    print(x)

print()

# Se itera cada 2
for x in range(0, 10, 2):
    print(x)

print()

# Podemos recorrer cada caracter de la cadena
# nombre = input("Ingrese tu nombre: ")
nombre = 'Nico'
for n in nombre:
    print(n)

print()

# Programa que simula una cuenta regresiva - usamos el modulo import
for r in range(10, 0, -1): # EL -1 PERMITE ir hacia atras
    print(r)
    time.sleep(1)

print('DESPEGUE')
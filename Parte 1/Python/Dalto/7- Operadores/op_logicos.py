# AND (&)
r1 = True & True
r2 = True & False
r3 = False & True
r4 = False & False
'devuelve True si ambas condiciones son verdaderas'

# OR (|) 
r5 = True | True
r6 = True | False
r7 = False | True
r8 = False | False
'devuelve True si alguna de las condiciones es verdadera'

# NOT 
r9 = not True
r10 = not False
'cambia la condicion'

print(r1)
print(r2)
print(r3)
print(r4)
print()
print(r5)
print(r6)
print(r7)
print(r8)
print()
print(r9)
print(r10)
print()

# Ejercicio
temperatura = int(input("¿Cual es la temperatura afuera? "))
if temperatura >= 0 and temperatura <= 30:
    print("Es un lindo dia")
elif temperatura < 0 or temperatura > 30:
    print("Ta feo :C")

# Lo negamos --> Se mostrará lo contrario
if not(temperatura >= 0 and temperatura <= 30):
    print("Es un lindo dia")
elif not(temperatura < 0 or temperatura > 30):
    print("Ta feo :C")
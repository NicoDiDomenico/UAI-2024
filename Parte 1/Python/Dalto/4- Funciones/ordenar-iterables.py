# 55
estudiantes = ('Juan', 'Alex', 'Jorge', 'Lucy', 'Pedro')
print(type(estudiantes))
#estudiante.sort -> no funca
estudiantes2 = sorted(estudiantes, reverse=True)
print(type(estudiantes2))
for i in estudiantes2:
    print(i)

print()
# Ordenar tuplas de tuplas:
estudiantes_tupla = (('Juan', 'F', 60),
                     ('Alex', 'A', 24),
                     ('Jorge', 'D', 33),
                     ('Lucy', 'B', 19),
                     ('Pedro', 'C', 43))

edad = lambda tupla:tupla[1]
#estudiantes.sort(key=edad)
estudiantes2 = sorted(estudiantes_tupla, key=edad)

for i in estudiantes2:
    print(i)
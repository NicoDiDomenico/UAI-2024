#49
class Coche:
    color = None

class Moto:
    color = None

# Funcion independiente, no es un método de de la clase coche
def cambiar_color(unVehiculo, color): # notar que unVehiculo es un objeto (no importa el nombre)
    unVehiculo.color = color

coche1 = Coche()
coche2 = Coche()
coche3 = Coche()
moto1 = Moto()

cambiar_color(coche1, 'Rojo')
cambiar_color(coche2, 'Azul')
cambiar_color(coche3, 'Blanco')
cambiar_color(moto1, 'Negro')

print(coche1.color)
print(coche2.color)
print(coche3.color)
print(moto1.color)
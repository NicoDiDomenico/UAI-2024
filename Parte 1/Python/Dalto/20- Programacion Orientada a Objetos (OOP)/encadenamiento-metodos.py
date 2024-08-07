# El Encadenamiento de metodos se utiliza para llevar a varios metodos secuencialmente y cada llamada realiza una accion en el mismo objeto y devuelve self
class Coche:
    def encender(self):
        print("has arrancado el motor")
        print(type(self))
        return self # Es importante usar return self para el encadenamiento de metodos

    def conducir(self):
        print("estás conduciendo el coche")
        return self

    def frenar(self):
        print("estás pisando los frenos")
        return self

    def apagar(self):
        print("has apagado el motor")
        return self

coche = Coche()

coche.encender()
coche.conducir()
print()
# metodo secuencial -> Encadenamiento de metodos
coche.encender().conducir()
# (coche.encender()).conducir()
# (self).conducir()
# (coche).conducir()
# coche.conducir()
coche.frenar().apagar()

print()

coche.encender().conducir().frenar().apagar()

print()

coche.encender()\
    .conducir()\
    .frenar()\
    .apagar()
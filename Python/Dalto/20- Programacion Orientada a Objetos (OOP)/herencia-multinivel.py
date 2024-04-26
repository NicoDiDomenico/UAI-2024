# Este es un concepto en el que una clase derivada, tambien conocida como clase hija, hereda de otra clase derivada.

class Organismo:
    vivo = True

class Animal(Organismo):
    def comer(self):
        print('Este animal está comiendo')

class Perro(Animal):
    def ladrar(self):
        print('Este perro está ladrando')

unPerro = Perro()
print(unPerro.vivo)
unPerro.comer()
unPerro.ladrar()
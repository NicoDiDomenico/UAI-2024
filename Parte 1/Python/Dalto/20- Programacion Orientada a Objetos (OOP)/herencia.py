# Las clases hijas heredan atributos y metodos  de las clases padres
class Animal:
    vivo = True

    def comer(self):
        print('Está comiendo!')
    def dormir(self):
        print('Está durmiendo!')

class Conejo(Animal): # Al pasa el nombre de la clase padre definimos que Conejo es su clase hija
    def correr(self):
        print('Corriendo!')
class Pez(Animal):
    def nadar(self):
        print('Nadando!')
class Halcon(Animal):
    def volar(self):
        print('Volando!')

# Defino los objetos
unConejo = Conejo()
unPez = Pez()
unHalcon = Halcon()

print(unHalcon.vivo)
unHalcon.dormir()
unPez.comer()

unPez.comer()
unPez.nadar()

unConejo.dormir()
unConejo.correr()
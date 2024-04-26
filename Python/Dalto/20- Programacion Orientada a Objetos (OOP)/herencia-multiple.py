# La herencia multiple es el concepto en el que una calse hija se deriva de mas de una clase padre.
class Presa:
    def huir(self):
        print('Este animal huye')

class Depredador:
    def cazar(self):
        print('Este animal está cazando')

class Conejo(Presa):
    pass

class Halcon(Depredador):
    pass
class Pez(Presa, Depredador):
    pass

unConejo = Conejo()
unHalcon = Halcon()
unPez = Pez()

unConejo.huir()
unHalcon.cazar()
print()
unPez.huir()
unPez.cazar()

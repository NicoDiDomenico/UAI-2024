# La anulacion de metodos es la capacidad de un lenguaje OOP para permitir que una subclase, tambien conocida como clase hija, proporcione una implementacion especifica de un metodo que ya he proporcionado por uno de sus padres.

class Animal:
    vivo = True

    def comer(self): # queremos anular comer para conejo
        print('Está comiendo!')

class Conejo(Animal):
    def comer(self): # Entonces usamos el mismo método
        print('Está comiendo zanahoria!') # Pero cambiamos su codigo para que haga una funcionalidad especifica.

unConejo = Conejo()

unConejo.comer()
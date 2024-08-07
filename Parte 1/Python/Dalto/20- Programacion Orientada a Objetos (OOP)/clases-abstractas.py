#48
# Las clases abstractas (fantasma) evitan que un usuario cree un objeto de esa clase.

from abc import ABC, abstractmethod # obligatorio para usar clases abstractas

class Vehiculo(ABC): # No queremos que el usuario haga uso de esta clase, ya que es muy generica, y queremos que, por ejemplo, se haga un auto o una moto => la vamos a convertir en una clase abstracta.
    @abstractmethod
    def ir(self):
        pass

class Coche(Vehiculo):
    def ir(self): # Asi anulamos el ir(self) de la calse padre
        print('Conduces Auto')

class Motocicleta(Vehiculo):
    def ir(self):
        print('Conduces Moto')

#vehiculo = Vehiculo()
coche = Coche()
moto = Motocicleta()

#vehiculo.ir()
coche.ir()
moto.ir()
#47
# La funcion super se utiliza para acceder a los metodos de una clase padre y devuelve un objeto temporal de la clase padre cuando se usa.
'''
# INEFICIENTE:
class Rectangulo:
    pass

class Cuadrado(Rectangulo):
    def __init__(self, alto, ancho):
        self.alto = alto
        self.ancho = ancho

class Cubo(Rectangulo):
    def __init__(self, alto, ancho, largo):
        self.largo = largo
        self.ancho = ancho
        self. largo = largo
'''
# EFICIENTE
# usamos la funcion super() para traer el metodo __init__ de la clase padre
class Rectangulo:
    def __init__(self, alto, ancho):
        self.alto = alto
        self.ancho = ancho

class Cuadrado(Rectangulo):
    def __init__(self, alto, ancho):
        super().__init__(alto, ancho)

    def area(self):
        return self.alto * self.ancho
                # El self es necesario apara que el metodo sepa con que objeto estamos tarbajando, ya que pdoriamos tener cuadrado1 y cuadrado2.
class Cubo(Rectangulo):
    def __init__(self, alto, ancho, largo):
        super().__init__(alto, ancho)
        self. largo = largo

    def volumen(self):
        return self.alto * self.ancho * self.largo

cuadrado1 = Cuadrado(3, 3)
cuadrado2 = Cuadrado(2, 6)
cubo = Cubo(3, 3, 3)

print(cuadrado1.area())
print(cuadrado2.area())
print()
print(cubo.volumen())

# En resumen la funcion super permite que una clase hija acceda a los metodos de la clase padre
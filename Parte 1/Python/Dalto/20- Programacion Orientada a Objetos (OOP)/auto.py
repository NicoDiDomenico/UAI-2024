class Auto:
    # Variable de Clase
    ruedas = 4

    # Metodo contructor - contiene las Variable de Instancia
    #  __init__(self, ...) Este metodo actua como un constructor y crea objetos para nosotros
    def __init__(self, marca, modelo, ano, color):
        self.marca = marca
        self.modelo = modelo
        self.ano = ano
        self.color = color
        self.__ruedas = 4
            # Para encapsular (atributos privados)usamos '__': self.ruedas = 4

    # Metodo de Clase - NO LO VIMOS
    def obtener_ruedas(cls):
        #print(cls.ruedas)
        return cls.ruedas

    # Metodo de Instancia
    #En Python, self es una convención utilizada dentro de las funciones de una clase para referirse al objeto en sí mismo. Cuando defines una funcion dentro de una clase, debes incluir self como el primer parámetro de la función. Esto permite que la función acceda y manipule los atributos y funciones del objeto en cuestión.
    def encendido(self):
        print('El auto está Encendido!')

    def apagado(self):
        print('El auto está Apagado!')
        # Metodo privado: print('El coche tiene ', self.__ruedas, ' ruedas.')

    def __ruedas_priv(self):
        print('El coche tiene ', self.__ruedas, ' ruedas.')


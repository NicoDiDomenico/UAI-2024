class Auto:
    # Variable de Clase
    ruedas = 4

    # Variable de Instancia
    def __init__(self, marca, modelo, ano, color): #  __init__(self, ...) Este metodo actua cono un constructor y crea objetos para nosotros
        self.marca = marca
        self.modelo = modelo
        self.ano = ano
        self.color = color

    # Metodo de Clase
    def obtener_ruedas(cls):
        #print(cls.ruedas)
        return cls.ruedas

    # Metodo de Instancia
    #En Python, self es una convención utilizada dentro de las funciones de una clase para referirse al objeto en sí mismo. Cuando defines una funcion dentro de una clase, debes incluir self como el primer parámetro de la función. Esto permite que la función acceda y manipule los atributos y funciones del objeto en cuestión.
    def encendido(self):
        print('El auto está Encendido!')

    def apagado(self):
        print('El auto está Apagado!')
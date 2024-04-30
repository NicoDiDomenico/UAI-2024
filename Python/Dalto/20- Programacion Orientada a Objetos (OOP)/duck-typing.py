# 50
# Las clases de un objeto es menos importante que los metodos y atributos que eesa clase podria tener
# No importa la clase real de un objeto, siempre y cuando tenga los metodos y atributos necesarios
# Este concepto de Duck Typing está basado en la frase: "Si camina como un pato y hace cuac como un pato, entonces debe ser un pato"

class Pato:

    def caminar(self):
        print('Este Pato está caminando')

    def hablar(self):
        print('Este Pato está haciendo cuac')


class Gallina:

    def caminar(self):
        print('Esta Gallina está caminando')

    def hablar(self):
        print('Esta Gallina está cacareando')

class Persona:
    def atrapar(self, pato):
        pato.caminar()# Notar que si el argumento es el objeto Gallina -> me manda a la clase gallina, por mas que lo pensé para el onjeto pato.
        pato.hablar()
        print('Lo atrapaste')

pato = Pato()
gallina = Gallina()
persona = Persona()
print()
persona.atrapar(pato)
persona.atrapar(gallina)
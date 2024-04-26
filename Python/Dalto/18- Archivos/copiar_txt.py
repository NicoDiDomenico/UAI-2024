import shutil
# Trae 3 funciones basicas:
# copyfile() - copiará el contenido de un archivo a otro
# copy() - = a copyfile() + copiar permiso, destinoo y directorios
# copy2() - = a copy() + copiar los metodos del archivo, incluyendo la fecha de creacion y modificacion

shutil.copyfile('texto_origen.txt', 'texto_destino.txt')
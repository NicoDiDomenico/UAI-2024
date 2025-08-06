### **¿Qué es REST?**

**REST** (Representational State Transfer) es un estilo de arquitectura para diseñar sistemas distribuidos, como aplicaciones web. Fue propuesto por Roy Fielding en su tesis doctoral en el año 2000. REST define un conjunto de principios que deben seguirse para crear servicios web escalables, mantenibles y eficientes.

**Características Clave de REST:**

1. **Recursos:** En REST, todo se considera un recurso, como usuarios, productos, pedidos, etc. Cada recurso tiene una representación y se identifica de manera única mediante una URL (Uniform Resource Locator).

2. **Métodos HTTP:** REST utiliza los métodos estándar de HTTP para realizar operaciones sobre los recursos:
   - **GET:** Para recuperar un recurso.
   - **POST:** Para crear un nuevo recurso.
   - **PUT:** Para actualizar un recurso existente.
   - **DELETE:** Para eliminar un recurso.

3. **Estateless (Sin Estado):** En una arquitectura REST, cada solicitud del cliente al servidor debe contener toda la información necesaria para ser procesada. El servidor no almacena ningún estado de las solicitudes anteriores.

4. **Uso de Formatos Estándar:** Aunque REST no está vinculado a ningún formato de datos específico, el formato JSON (JavaScript Object Notation) es el más comúnmente utilizado debido a su simplicidad y legibilidad tanto para humanos como para máquinas.

5. **Escalabilidad:** Debido a su naturaleza sin estado y uso de estándares abiertos, REST es altamente escalable, lo que lo convierte en una opción popular para aplicaciones web de gran tamaño.

### **¿Qué es una API?**

**API** (Application Programming Interface) es un conjunto de definiciones y protocolos que permiten a diferentes aplicaciones o servicios comunicarse entre sí. Una API define las reglas y los métodos que las aplicaciones pueden usar para interactuar con un sistema, servicio o recurso.

**Características Clave de una API:**

1. **Interfaz Definida:** Una API proporciona una interfaz clara y documentada para que otras aplicaciones puedan interactuar con ella sin necesidad de conocer la implementación interna.

2. **Abstracta:** Las APIs permiten que los desarrolladores utilicen funcionalidades sin tener que entender o ver el código subyacente que las implementa.

3. **Reutilizable:** Las APIs están diseñadas para ser reutilizables en múltiples aplicaciones, lo que permite a los desarrolladores integrar funcionalidades de manera eficiente.

4. **Protocolos y Estándares:** Las APIs pueden utilizar diversos protocolos de comunicación como HTTP, SOAP, o gRPC, y pueden devolver datos en diferentes formatos, como JSON, XML, etc.

### **Unión de Ambos Términos: ¿Qué es una REST API?**

**REST API** es una API que sigue los principios de la arquitectura REST. En una REST API, los recursos de una aplicación se exponen a través de URLs, y las operaciones sobre esos recursos se realizan utilizando los métodos HTTP estándar como GET, POST, PUT y DELETE. 

### **Características de una REST API:**

1. **Recursos Expuestos en URLs:** Cada recurso en la API tiene su propia URL única. Por ejemplo, en una REST API para gestionar usuarios, la URL para acceder a un usuario específico podría ser `/users/123`, donde `123` es el ID del usuario.

2. **Operaciones a Través de Métodos HTTP:** Los diferentes métodos HTTP se utilizan para interactuar con los recursos:
   - **GET /users/123:** Recupera la información del usuario con ID 123.
   - **POST /users:** Crea un nuevo usuario.
   - **PUT /users/123:** Actualiza la información del usuario con ID 123.
   - **DELETE /users/123:** Elimina el usuario con ID 123.

3. **Sin Estado:** Cada solicitud a la API es independiente de las demás. El servidor no necesita recordar solicitudes anteriores; toda la información necesaria se envía en cada solicitud.

4. **Uso de JSON:** Aunque no es obligatorio, muchas REST APIs utilizan JSON como formato para enviar y recibir datos debido a su simplicidad y compatibilidad con JavaScript.

### **Ejemplo Práctico de una REST API:**

Imaginemos que tienes una aplicación para gestionar una librería:

- **GET /books:** Recupera una lista de todos los libros.
- **GET /books/1:** Recupera la información del libro con ID 1.
- **POST /books:** Crea un nuevo libro.
- **PUT /books/1:** Actualiza la información del libro con ID 1.
- **DELETE /books/1:** Elimina el libro con ID 1.

En este ejemplo, cada uno de estos endpoints sigue los principios de REST, y la API en su conjunto es lo que llamamos una **REST API**.

### **Resumen:**
- **REST** es un estilo arquitectónico para diseñar servicios web que son escalables, simples y usan los protocolos estándar como HTTP.
- **API** es una interfaz que permite que diferentes aplicaciones se comuniquen entre sí.
- Una **REST API** es una API que sigue los principios de REST, exponiendo recursos a través de URLs y utilizando métodos HTTP para interactuar con esos recursos. Es un enfoque común y eficiente para crear servicios web que pueden ser consumidos por diversas aplicaciones.
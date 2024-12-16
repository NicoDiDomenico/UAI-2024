"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.JWT_TOKEN = exports.MONGODB_URI = exports.PORT = void 0;
/* Acá configuro y accedo a las variables de entorno:
- Se lee de las variables de entorno.
- Se pone a disposición del resto del proyecto como constante (JWT_TOKEN).
*/
/*
// Me pide que agregue esto:
import dotenv from "dotenv";
dotenv.config();
*/
exports.PORT = Number(process.env.PORT) || 3001;
// PORT = 3000 --> Define en qué puerto el backend escuchará las solicitudes. En este caso, es el puerto 3000. --> Esta es la dirección para conectarse a una base de datos en MongoDB Atlas (una base de datos en la nube).
exports.MONGODB_URI = process.env.MONGODB_URI;
// MONGODB_URI = 'mongodb+srv://agusalbo2024:ferchu123@test.bmscycf.mongodb.net/' --> URL especial que contiene toda la información necesaria para que tu aplicación pueda conectarse a una base de datos, nos la dio MongoDb Atlas.
exports.JWT_TOKEN = String(process.env.JWT_TOKEN) || 'secret_token';
// JWT_TOKEN = '123' --> Secreto generado por el servidor y que solo conoce él
/*
JWT:
Cuando el usuario inicia sesión, el backend genera un JWT (JSON Web Token) que incluye tres partes: el Header, el Payload y la Firma. La Firma se crea utilizando el Header y el Payload combinados con un secreto (JWT_TOKEN), conocido únicamente por el backend. Este token se envía al cliente, quien lo utilizará para acceder a recursos protegidos, enviándolo en los headers de las solicitudes. El backend valida el token comprobando que la Firma sea auténtica y no haya sido alterada (para eso la vuelve a generar con el secreto y la compara con la que vino en el Req), garantizando así la seguridad y el acceso controlado a las distintas partes de la aplicación.

Ejemplo práctico del proceso de generación:
    Header: {"alg": "HS256", "typ": "JWT"}
    Payload: {"id": "123", "role": "user", "exp": 1700000000}
    Firma: HMACSHA256(base64(Header) + "." + base64(Payload), JWT_TOKEN)// JWT_TOKEN es el secreto
    
    ¿Qué viaja realmente?
    El token completo (Header, Payload y Firma juntos) viaja como un string compacto en este formato codificado. No viaja un JSON crudo, sino su versión Base64Url codificada.

    Token resultante --> eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEyMyIsInJvbGUiOiJ1c2VyIn0.abc123signature // Esto es lo que se nvia en el header de la Req para hacer las validaciones
*/ 

"use strict";
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    var desc = Object.getOwnPropertyDescriptor(m, k);
    if (!desc || ("get" in desc ? !m.__esModule : desc.writable || desc.configurable)) {
      desc = { enumerable: true, get: function() { return m[k]; } };
    }
    Object.defineProperty(o, k2, desc);
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __exportStar = (this && this.__exportStar) || function(m, exports) {
    for (var p in m) if (p !== "default" && !Object.prototype.hasOwnProperty.call(exports, p)) __createBinding(exports, m, p);
};
Object.defineProperty(exports, "__esModule", { value: true });
__exportStar(require("./todo.routes"), exports); /* "Todo lo que está exportado desde todo.routes.ts será reexportado por este archivo (index.ts)." */
__exportStar(require("./user.routes"), exports);
/*
El archivo index.ts actúa como un punto central de exportación para todos los módulos de rutas (todo.routes.ts y user.routes.ts) dentro del directorio routes.

En TypeScript (o JavaScript), un archivo llamado index.ts dentro de un directorio tiene un uso especial:
- Cuando importas el directorio, el compilador automáticamente busca el archivo index.ts como punto de entrada.
*/
/*
Si no existiera el archivo index.ts, tendrías que importar cada archivo de rutas manualmente:
    import { todoRouter } from './routes/todo.routes';
    import { userRouter } from './routes/user.routes';


Con index.ts, puedes consolidar todas las exportaciones en un solo lugar:
    // Archivo index.ts
    export * from './todo.routes';
    export * from './user.routes';

Ahora solo necesitas importar desde el directorio routes:
    import { todoRouter, userRouter } from './routes';
*/ 

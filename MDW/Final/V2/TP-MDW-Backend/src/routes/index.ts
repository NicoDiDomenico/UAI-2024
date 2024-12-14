export * from './todo.routes'; /* "Todo lo que está exportado desde todo.routes.ts será reexportado por este archivo (index.ts)." */
export * from './user.routes';

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
import React from "react";
/* Este contexto proporciona un valor por defecto que  puede ser consumido por cualquier componente 
dentro  del árbol de componentes que utilice este contexto. */
const ValorContext = React.createContext("Valor por defecto"); 
/* El valor por defecto se usa cuando no hay un proveedor de contexto en el árbol de componentes */
export default ValorContext;
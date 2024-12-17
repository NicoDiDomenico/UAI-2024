import { FC } from "react"; // // FC (Functional Component) define el tipo del componente.
/* 
FC significa Functional Component y es una manera de tipar componentes en TypeScript.
Este tipo permite definir las propiedades (props) que recibe el componente.
*/
import { MdOutlinePushPin } from "react-icons/md"; // Iconos de Material Design.
import { MdCreate, MdDelete } from "react-icons/md"; // Iconos de Material Design.
/* 
MdOutlinePushPin: Para mostrar si una nota está fijada.
MdCreate: Para editar una nota.
MdDelete: Para eliminar una nota.
*/

interface NoteCardProps {
  title: string;
  date: string;
  description: string;
  tags: string[];
  isPinned: boolean;
  onEdit(): void; // Función que se ejecuta al editar la nota
  onDelete(): void; // Función que se ejecuta al eliminar la nota
  onPinNote(): void; // Función que se ejecuta al fijar o desfijar la nota
}

const NoteCard: FC<NoteCardProps> = ({
  title,
  date,
  description,
  tags,
  isPinned,
  onEdit,
  onDelete,
  onPinNote,
}) => {
  return (
    /* Contenedor principal del NoteCard */
    <div className="border rounded p-4 bg-white hover:shadow-xl duration-500 transition-all ease-in-out">
      {/* Primera fila: título, fecha y botón de pin */}
      <div className="flex items-center justify-between">
        <div>
          <h6 className="text-sm font-medium">{title}</h6>
          <span className="text-sm text-slate-500">{date}</span>
        </div>

        <MdOutlinePushPin
          className={`icon-btn ${isPinned ? "text-primary" : "text-slate-300"}`}
          onClick={
            onPinNote
          } /* Ejecuta la función onPinNote (proporcionada por el padre) cuando se hace clic. */
        />
      </div>

      {/* Segunda fila: Contenido (descripción) de la nota */}
      <p className="text-xs text-slate-600 mt-2">{description?.slice(0, 60)}</p>
      {/*El operador `?.` (encadenamiento opcional) se asegura de que no se produzca un error si `description` es `null` o `undefined`, 
      retornando `undefined` de forma segura. Si `description` tiene un valor, el método `slice(0, 60)` extrae los primeros 60 
      caracteres de la cadena (o menos si la cadena tiene menos de 60 caracteres), limitando la longitud de la cadena mostrada 
      sin causar errores si el valor de `description` no está definido. */}

      {/* Tercera fila: etiquetas (tags) y botones de acción de editar y borrar nota */}
      <div className="flex items-center justify-between mt-2">
        {/* Tags */}
        <div className="text-xs text-slate-500">
          {tags.map((tag) => `#${tag} `)}{" "}
          {/* generamos una cadena de tags: tags = ['SED', 'SOS'], se muestra: #SED #SOS */}
        </div>

        {/* Botones de Editar y Eliminar */}
        <div className="flex items-center gap-2">
          <MdCreate
            className="icon-btn hover:text-green-600 duration-150"
            onClick={onEdit} // Ejecuta la función onEdit proporcionada como prop.
          />

          <MdDelete
            className="icon-btn hover:text-red-500 duration-150"
            onClick={
              onDelete
            } /* Ejecuta la función onDelete proporcionada como prop.*/
          />
        </div>
      </div>
    </div>
  );
};

export default NoteCard;

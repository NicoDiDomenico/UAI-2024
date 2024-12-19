import { FC, useState } from "react";
import { MdAdd, MdClose } from "react-icons/md";

interface TagInputProps {
  tags: string[];
  setTags(tag: string[]): void;
}

const TagInput: FC<TagInputProps> = ({ tags, setTags }) => {
  const [inputValue, setInputValue] = useState("");
  /* 
  inputValue:
    Es el texto que el usuario está escribiendo en el input para agregar una nueva etiqueta.
    Se actualiza dinámicamente cada vez que el usuario escribe en el input (con onChange).
  setInputValue:
    Es la función que actualiza el estado inputValue con el texto ingresado por el usuario.
  */

  const addNewTag = () => {
    if (inputValue.trim() !== "") {
      // Si el valor ingresado no está vacío
      setTags([...tags, inputValue.trim()]);
      /* Actualiza el estado tags agregando la nueva etiqueta.
      [...tags]: Copia el array actual de etiquetas.
      inputValue.trim(): Agrega la nueva etiqueta (sin espacios innecesarios). */
      setInputValue(""); // Limpia el input después de agregar
    }
  };

  const handleKeyDown = (e: React.KeyboardEvent) => {
    // e: React.KeyboardEvent - Recibe un evento de teclado. Este tipo asegura que solo se pueda usar en eventos relacionados con el teclado.
    if (e.key === "Enter") {
      // Si el usuario presiona la tecla Enter
      addNewTag(); // Agrega la etiqueta llamando a `addNewTag` (la funcion de arriba)
    }
  };

  const handleRemoveTag = (tagToRemove: string) => {
    // tagToRemove --> Recibe la etiqueta que el usuario quiere eliminar.
    setTags(tags.filter((tag) => tag !== tagToRemove));
    /* 
    tags.filter((tag) => tag !== tagToRemove):
      - Crea un nuevo array que excluye la etiqueta seleccionada.
      - tag !== tagToRemove: Filtra todas las etiquetas que no coincidan con la etiqueta que queremos eliminar.
    setTags(...):
      - Actualiza el estado tags con el array filtrado, eliminando la etiqueta seleccionada.
    */
  };

  return (
    <div>
      {/* 1. Listado de Etiquetas */}
      {tags?.length > 0 && (
        <div className="flex items-center gap-2 flex-wrap mt-2">
          {/* Mostrar cada uno de los tags: */}
          {tags.map((tag, index) => (
            <span
              key={index}
              className="flex items-center gap-2 text-base text-slate-900 bg-slate-100 px-3 py-1 rounded"
            >
              # {tag}
              {/* Eliminar Tag: */}
              <button
                onClick={() => {
                  handleRemoveTag(tag);
                }}
              >
                <MdClose />
              </button>
            </span>
          ))}
        </div>
      )}

      {/* 2. Input para agregar etiquetas */}
      <div className="flex items-center gap-4 mt-3">
        {/* Campo de texto para escribir una nueva etiqueta */}
        <input
          type="text" // Define que este input es para texto.
          className="text-sm bg-transparent px-3 py-2 rounded outline-none"
          placeholder="Agregar etiquetas..." // Texto que se muestra cuando el campo está vacío.
          onChange={(e) => setInputValue(e.target.value)}
          /* 
            onChange: 
            - Evento que se dispara cada vez que el usuario escribe algo en el campo.
            - e.target.value: Captura el valor actual ingresado por el usuario.
            - setInputValue: Actualiza el estado "inputValue" con el texto ingresado.
          */
          onKeyDown={handleKeyDown}
          /* 
            onKeyDown: 
            - Evento que se dispara cuando el usuario presiona una tecla.
            - Si la tecla presionada es "Enter", se llama a la función "addNewTag" para agregar la etiqueta.
          */
          value={inputValue}
          /* 
            value: 
            - El valor actual del input, controlado por el estado "inputValue".
            - Este enlace asegura que el input siempre refleje el estado más reciente.
          */
        />

        {/* Botón para agregar etiquetas */}
        <button
          className="group w-8 h-8 flex items-center justify-center rounded border border-blue-700 hover:bg-blue-700 duration-150 "
          onClick={addNewTag}
          /* 
            onClick: 
            - Evento que se dispara al hacer clic en el botón.
            - Llama a la función "addNewTag" para agregar la etiqueta ingresada en el input.
          */
        >
          <MdAdd className="text-2xl text-blue-700 group-hover:text-white" />
        </button>
      </div>
    </div>
  );
};

export default TagInput;

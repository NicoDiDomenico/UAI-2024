import { FC, useState } from "react";
import TagInput from "./TagInput";
import { MdClose } from "react-icons/md";
import { INote } from "../pages/Home/Home";
import axiosInstance from "../utils/axiosInstance";
import { notifyError, notifySuccess } from "../utils/toastFunctions";
import { AxiosError } from "axios";

interface AddEditNotesProps {
  setOpenModal: (value: {
    isShown: boolean;
    type: "add" | "edit";
    data: INote | null;
  }) => void;
  type: "add" | "edit";
  getAllNotes: () => void;
  noteDetails: INote | null;
}

const AddEditNotes: FC<AddEditNotesProps> = ({
  setOpenModal,
  type,
  getAllNotes,
  noteDetails,
}) => {
  const [title, setTitle] = useState(noteDetails?.title || ""); // Título de la nota
  /* Si noteDetails existe y tiene un valor en title, usa ese valor(modo --> type:"edit"). 
     Si no existe o title está vacío, usa una cadena vacía "" como valor inicial" (modo --> type:"add"). */
  const [description, setDescription] = useState(
    noteDetails?.description || ""
  ); // Descripción de la nota
  // Misma logica pero con la descripcion:
  /* Modo Editar: description = "Descripción de ejemplo".
     Modo Agregar: description = "".  */
  const [tags, setTags] = useState<string[]>(noteDetails?.tags || []); // Etiquetas de la nota
  /* Misma logica pero con tags que ahora es un arreglo de string:
      Si noteDetails existe y tiene tags, se usa ese array Resultado inicial: tags = ["etiqueta1", "etiqueta2"].
      Si noteDetails es null o tags no está definido, se inicializa con un array vacío, Resultado inicial: tags = [].
  */
  const [error, setError] = useState<string | null>(
    null
  ); /* Estado para manejar y mostrar errores en el formulario. */

  // 1) Conecta con el back para guardar la nota (crear una nueva) - type add
  const createNote = async () => {
    try {
      const res = await axiosInstance.post("/todos/", {
        title,
        description,
        tags,
      }); // Envía una petición POST con el título, descripción y etiquetas al backend.

      getAllNotes(); // Refresca la lista de notas.
      setOpenModal({ isShown: false, data: null, type: "add" }); // Cierra el modal.
      notifySuccess(res.data.message);
    } catch (error) {
      if (error instanceof AxiosError) {
        notifyError(error.response?.data.message);
      }
    }
  };

  //2) Conecta con el back para edita la nota - type edit
  const editNote = async (id: string) => {
    try {
      const res = await axiosInstance.put(`/todos/${id}`, {
        // traigo la nota actualizada pero para mi es al dope
        title,
        description,
        tags,
      }); // Envía una petición PUT para actualizar la nota específica con el ID.

      getAllNotes(); // Refresca la lista de notas.
      setOpenModal({ isShown: false, data: null, type: "add" }); // Cierra el modal.
      notifySuccess(res.data.message); // Muestra un mensaje de éxito.
    } catch (error) {
      if (error instanceof AxiosError) {
        notifyError(error.response?.data.message);
      }
    }
  };

  /* Validación y ejecución de 1) add o 2) edit */
  const handleAddNote = () => {
    /* handleAddNote está en el tsx de la 4ta fila  */
    if (!title) {
      setError("Se debe proporcionar un titulo");
      return;
    }
    if (!description) {
      setError("Se debe proporcionar una description");
      return;
    }
    setError(""); // Limpia los errores previos.
    if (type === "edit") {
      editNote(noteDetails?.id as string); // Si es "edit", llama a editNote.
    } else {
      createNote(); // Si es "add", llama a createNote.
    }
  };

  return (
    <div className="relative">
      {/* Botón de Cierre (Cerrar Modal) */}
      <button
        className="group w-10 h-10 rounded-full flex items-center justify-center absolute -top-3 -right-3 hover:bg-slate-500 duration-150"
        onClick={
          () =>
            setOpenModal({
              isShown: false,
              data: null,
              type: "add",
            }) /* isShown: false: El modal se oculta. */
        }
      >
        <MdClose className="text-xl text-slate-400 group-hover:text-slate-50 duration-150" />
      </button>

      {/* Primera fila: Input para el título */}
      <div className="flex flex-col gap-2">
        <label className="input-label" htmlFor="title">
          TITULO
        </label>
        <input
          type="text"
          className="text-2xl text-slate-950"
          placeholder="Ir al gimnasio a las 5"
          id="title"
          value={title} // toma el valor del titulo que traje en la nota
          onChange={(e) => setTitle(e.target.value)}
          /* 
          - onChange es un evento de React que se dispara cada vez que el valor de un input (campo de texto, textarea, etc.) cambia. Se activa cada vez que el usuario escribe algo en el campo.
            Por ejemplo, si escribo "Hola", el evento onChange se disparará cada vez que agregue una letra:
              Primer cambio: "H"
              Segundo cambio: "Ho"
              Tercer cambio: "Hol"
              Cuarto cambio: "Hola"
          - (e): Es el objeto del evento que contiene información sobre el input.
          - e.target.value: Obtiene el valor actual del input (lo que el usuario está escribiendo).
          - setTitle(e.target.value): Actualiza el estado "title" con el valor actual del input.
          - Resultado: El input siempre refleja el estado más actualizado de "title" y se sincroniza en tiempo real.
          */
        />
      </div>

      {/* Segunda fila: Textarea para la descripción */}
      <div className="flex flex-col gap-2 mt-4">
        <label className="input-label">DESCRIPCIÓN</label>
        <textarea
          className="text-sm text-slate-950 outline-none bg-slate-50 rounded p-2 resize-none"
          placeholder="Descripción"
          rows={10}
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          /* Lo mismo que title pero usando el hook useState para el estado description */
        />
      </div>

      {/* Tercera fila: Componente TagInput para etiquetas */}
      <div className="mt-3">
        <label className="input-label">ETIQUETAS</label>
        <TagInput tags={tags} setTags={setTags} />
        {/* 
          TagInput: Es un componente personalizado que permite agregar y manejar etiquetas (tags).
          tags={tags}: Se le pasa el estado "tags" como prop, que almacena las etiquetas actuales.
          setTags={setTags}: Se pasa la función setTags para actualizar el estado "tags" cuando se agregan o eliminan etiquetas.
          Resultado: Permite al usuario agregar, visualizar y borrar etiquetas dinámicamente.
        */}
      </div>

      {/* Mensaje de error condicional */}
      {error && <p className="error-msg">{error}</p>}

      {/* Cuarta fila: Botón para agregar o actualizar la nota */}
      <button
        className="btn-primary font-medium mt-5 p-3"
        onClick={handleAddNote}
      >
        {type === "add" ? "AGREGAR" : "ACTUALIZAR"}
        {/*
          type === "add" ? "AGREGAR" : "ACTUALIZAR":
          - Si el tipo del modal es "add", el botón muestra "AGREGAR".
          - Si el tipo del modal es "edit", el botón muestra "ACTUALIZAR".
          onClick={handleAddNote}: Al hacer clic, se ejecuta la función handleAddNote.
          Resultado:
            - Si "type" es "add", se ejecuta la función createNote (crear una nueva nota).
            - Si "type" es "edit", se ejecuta la función editNote (actualizar la nota existente).
        */}
      </button>
    </div>
  );
};

export default AddEditNotes;

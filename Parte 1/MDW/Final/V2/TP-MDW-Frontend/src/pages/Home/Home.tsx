import { MdAdd } from "react-icons/md";
import Navbar from "../../components/Navbar";
import NoteCard from "../../components/NoteCard";
import AddEditNotes from "../../components/AddEditNotes";
import { useEffect, useState } from "react";
import Modal from "react-modal";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../contexts/AuthContext";
import { notifyError, notifySuccess } from "../../utils/toastFunctions";
import axiosInstance from "../../utils/axiosInstance";
import { SpinnerCircularFixed } from "spinners-react";
import { AxiosError } from "axios";

export interface INote {
  completed: boolean;
  description: string;
  isPinned: boolean;
  tags: string[];
  title: string;
  id: string;
  date: string;
}

interface INoteRaw {
  completed: boolean;
  createdOn: string;
  description: string;
  isPinned: boolean;
  tags: string[];
  title: string;
  user: string;
  __v: number;
  _id: string;
}

interface IOpenModal {
  isShown: boolean;
  type: "add" | "edit";
  data: INote | null;
}

const Home = () => {
  const navigate = useNavigate(); // Esta línea usa el hook useNavigate de React Router, que te permite redirigir al usuario programáticamente.
  /* navigate: Es una función que puedes llamar para cambiar la URL de la aplicación.  */
  const { user } = useAuth(); // Esta línea usa un hook personalizado llamado useAuth para obtener el valor actual del usuario desde el contexto AuthContext.

  const [loading, setLoading] = useState(false);
  const [notes, setNotes] = useState<INote[]>([]);
  const [openModal, setOpenModal] = useState<IOpenModal>({
    isShown: false, // Indica si el modal debe mostrarse o no. --> Esto acriva el componente Modal de react-modal
    type: "add", // Tipo de acción: "add" (agregar) o "edit" (editar).
    data: null, // Contiene los datos de la nota cuando se edita.
  });

  /* Se ejecuta después de que el componente se haya renderizado, En este caso, se ejecuta cada vez que user cambia o cuando es la primera vez, gracias a la dependencia [user]. */
  useEffect(() => {
    if (!user) navigate("/login");
    // Si no hay un usuario, React automáticamente redirige a /login.
    else getAllNotes(); // Si hay usuario, carga las notas.
  }, [user]);

  //Traer todas las notas del usuario
  const getAllNotes = async () => {
    try {
      setLoading(true);
      const res = await axiosInstance.get("/todos/");

      const notesDb = res.data.todos as INoteRaw[];

      const formattedNotes: INote[] = notesDb.map((note) => ({
        completed: note.completed,
        description: note.description,
        id: note._id,
        isPinned: note.isPinned,
        tags: note.tags,
        title: note.title,
        date: new Intl.DateTimeFormat("es-ES", {
          day: "2-digit",
          month: "2-digit",
          year: "numeric",
        }).format(new Date(note.createdOn)),
      }));

      setNotes(formattedNotes);
    } catch (error) {
      notifyError(
        "Error al traer notas de la base de datos, intente nuevamente"
      );
    } finally {
      setLoading(false);
    }
  };

  //Buscar nota con el buscador
  const searchNote = async (query: string) => {
    try {
      setLoading(true); // esto es para mostrar el SpinnerCircularFixed
      const res = await axiosInstance.get("/todos/search", {
        params: {
          query,
        } /* Envía el parámetro "query" al backend como parte de la URL. */,
      });

      const notesDb = res.data.todos as INoteRaw[]; // Recupera las notas del backend.

      const formattedNotes: INote[] = notesDb.map((note) => ({
        completed: note.completed,
        description: note.description,
        id: note._id,
        isPinned: note.isPinned,
        tags: note.tags,
        title: note.title,
        date: new Intl.DateTimeFormat("es-ES", {
          day: "2-digit",
          month: "2-digit",
          year: "numeric",
        }).format(new Date(note.createdOn)),
      }));

      setNotes(formattedNotes);
    } catch (error) {
      console.log(error);
      notifyError(
        "Error al traer notas de la base de datos, intente nuevamente"
      );
    } finally {
      setLoading(false);
    }
  };

  //Limpia el buscador
  const handleClearSearch = () => {
    getAllNotes();
    /* 
    Llama a la función que trae todas las notas desde el backend.
    Es una "limpieza" del buscador porque al volver a obtener todas las notas, se elimina cualquier filtro o búsqueda aplicada.
    */
  };

  //Logica para borrar una nota
  const onDelete = async (id: string) => {
    try {
      const confirm = window.confirm("Seguro que desea borrar la nota?");
      if (confirm) {
        const res = await axiosInstance.delete(`/todos/${id}`); // Elimina la nota en el backend --> todoRouter.delete("/:id", checkAuth, deleteTodo);

        notifySuccess(res.data.message);
        getAllNotes(); // Vuelve a cargar todas las notas después de eliminar.
      }
    } catch (error) {
      if (error instanceof AxiosError) {
        notifyError(error.response?.data.message); // Notificación en caso de error.
      }
    }
  };

  //Abrir modal con los datos de la nota a editar / COMO HACE PARA DESPUES LA LOGICA DEL MODAL (ACTUALIZAR, AGREGAR ETIQUETAS, ETC..)
  const handleEdit = (noteDetails: INote) => {
    setOpenModal({ data: noteDetails, isShown: true, type: "edit" });
    /* Actualiza el estado openModal con:
        isShown: true → Abre el modal.
        type: "edit" → El modal está en modo editar.
        data: noteDetails → Guarda los datos actuales de la nota para poder editarlos. */
  };

  //Fijar o desfijar una nota
  const updateIsPinned = async (id: string, isPinned: boolean) => {
    try {
      console.log(id, isPinned); // quitar
      await axiosInstance.put(`/todos/pin-todo/${id}`, {
        isPinned: !isPinned, // Invierte el valor actual de isPinned.
      });

      getAllNotes(); // Recarga todas las notas. (por eso creo que es al dope traer del servidor la nota modificada)
      setOpenModal({ isShown: false, data: null, type: "add" }); // Cierra el modal si estaba abierto.
    } catch (error) {
      if (error instanceof AxiosError) {
        notifyError(error.response?.data.message);
      }
    }
  };

  return (
    <Navbar searchNote={searchNote} handleClearSearch={handleClearSearch}>
      {/* Mostar el Spinner hasta que se obtengan las notas de la api */}
      <div className="container mx-auto">
        {loading && (
          <SpinnerCircularFixed className="mx-auto" color="#2B85FF" />
        )}

        {/* Cuando la api respodne puede que no hayan notas, entonces renderiza <p></p> */}
        {notes?.length === 0 && loading === false && (
          <p className="text-xl text-center">No hay ninguna nota cargada!</p>
        )}

        {/* El componente NoteCard se está renderizando dentro de un map que recorre el array de notes */}
        <div className="grid grid-cols-3 gap-4 mt-8 justify-start">
          {notes.map(
            (
              item,
              index // El resultado de map es un nuevo arreglo de elementos JSX que React puede renderizar.
            ) => (
              <NoteCard
                key={index} // Propiedad clave única para cada tarjeta.
                title={item.title} // Título de la nota.
                date={item.date} // Fecha formateada de creación de la nota.
                description={item.description} // Descripción de la nota.
                isPinned={item.isPinned} // Indica si la nota está fijada.
                tags={item.tags} // Array de etiquetas de la nota.
                onDelete={() => {
                  onDelete(item.id); // Llama a la lógica para borrar la nota con su id, pasar item.id como para metro es clave para despues usarla como params en la url
                }}
                /* 2) Editar una nota */
                onEdit={() => handleEdit(item)} // Abre el modal de tipo edit
                onPinNote={() => {
                  updateIsPinned(item.id, item.isPinned); // Fija o desfija la nota.
                }}
              />
            )
          )}
        </div>
      </div>

      {/* 1) Agregar una nota */}
      <button
        className="w-16 h-16 flex items-center justify-center rounded-2xl bg-primary hover:bg-blue-600 duration-150 absolute right-10 bottom-10 "
        onClick={() => {
          /* Cuando haces clic en el botón con el + azul (que está en la parte inferior derecha de la pantalla), ejecuta la función setOpenModal. */
          setOpenModal({ isShown: true, type: "add", data: null });
          /* isShown: true → Abre el modal.
          type: "add" → El modal está en modo agregar.
          data: null → No hay datos porque es una nueva nota. */
        }}
      >
        <MdAdd className="text-[32px] text-white" />
      </button>

      {/* El modal se abre en dos casos principales: 1) Agregar una nota y 2) Editar una nota */}
      {/* El modal se renderiza usando la librería react-modal */}
      <Modal
        isOpen={openModal.isShown} // Muestra el modal si isShown es true.
        onRequestClose={() => {}} // Aquí puedes cerrar el modal manualmente (no implementado aún).
        style={{
          overlay: {
            backgroundColor: "rgba(0,0,0,0.2)",
          },
        }}
        contentLabel=""
        className="w-[40%] max-h-3/4 bg-white rounded-md mx-auto mt-14 p-5 overflow-hidden"
      >
        {/* Es el componente principal dentro del modal que maneja tanto la lógica de agregar como de editar una nota. */}
        <AddEditNotes
          setOpenModal={setOpenModal} // Pasa la función para cambiar el estado
          type={openModal.type} // Pasa el tipo de acción (add o edit).
          getAllNotes={getAllNotes} // Pasa la función para recargar las notas.
          noteDetails={openModal.data} // Pasa los datos de la nota.
        />
      </Modal>
    </Navbar>
  );
};

export default Home;

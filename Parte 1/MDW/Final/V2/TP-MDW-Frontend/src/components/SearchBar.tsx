import { FC } from "react";
import { FaMagnifyingGlass } from "react-icons/fa6";
import { IoMdClose } from "react-icons/io";

interface SearchBarProps {
  value: string; // Valor actual del input de búsqueda.
  onChange(event: React.ChangeEvent<HTMLInputElement>): void; // Se ejecuta al escribir en el input.
  handleSearch(): void; // Se ejecuta al hacer clic en el botón de búsqueda (lupa).
  onClearSearch(): void; // Se ejecuta al hacer clic en el botón "X" para limpiar el input.
}

const SearchBar: FC<SearchBarProps> = ({
  value,
  onChange,
  handleSearch,
  onClearSearch,
}) => {
  return (
    <div className="w-80 flex items-center px-4 bg-slate-100 rounded-md">
      {/* Input de búsqueda */}
      <input
        type="text"
        placeholder="Buscar nota..."
        className="w-full text-sm bg-transparent py-[11px] outline-none"
        onChange={onChange} // Llama a la función pasada como prop para actualizar el valor, esto cambia el estado, al hacer esto sirve para cuando busque la palabra correcta(SearchQuery) cuando haca clic en la lupa
        value={value} // Muestra el valor actual del input.
      />

      {/* Botón "X" para limpiar */}
      {value && (
        <IoMdClose
          className="text-xl text-slate-500 cursor-pointer hover:text-black duration-150 mr-4"
          onClick={onClearSearch}
        />
      )}

      {/* Botón de búsqueda (lupa) */}
      <FaMagnifyingGlass
        className="text-slate-400 cursor-pointer hover:text-black  duration-150"
        onClick={handleSearch}
      />
    </div>
  );
};

export default SearchBar;

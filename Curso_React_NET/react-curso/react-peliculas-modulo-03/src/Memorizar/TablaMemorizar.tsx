import React, { memo, useCallback } from "react";
import type Persona from "../persona.model";
import FilaMemorizar from "./FilaMemorizar";
import { ErrorBoundary } from "react-error-boundary";

const TablaMemorizar = memo(function TablaMemorizar() {

  console.log("Se renderiza la tabla");

  const personasFuente: Persona[] = [
    { id: 1, nombre: 'Felipe', departamento: 'Ingeniería' },
    { id: 2, nombre: 'Claudia', departamento: 'Recursos Humanos' },
    { id: 3, nombre: 'Roberto', departamento: 'Contabilidad' },
    { id: 4, nombre: 'Francisca', departamento: 'Contabilidad' },
    { id: 5, nombre: 'José', departamento: 'Operaciones' },
    { id: 6, nombre: 'Estephany', departamento: 'Ingeniería' },
    { id: 7, nombre: 'Norberto', departamento: 'Recursos Humanos' },
  ];

  const [personas, setPersonas] = React.useState(personasFuente);

  const removerPersona = useCallback((persona: Persona) => {
    setPersonas((estadoActual) =>
      estadoActual.filter((p) => p.id !== persona.id)
    );
  }, []);

  return (
    <table>
      <thead>
        <tr>
          <th>Nombre</th>
          <th>Departamento</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        {personas.map(p => 
            <ErrorBoundary key={p.id} fallback={<>
                <tr>
                    <td colSpan={3} style={{color: 'red'}}>--Error: {p.nombre}</td>
                </tr>
            </>}>
                <FilaMemorizar persona={p} remover={removerPersona} />
            </ErrorBoundary>
        )}
      </tbody>
    </table>
  );
});

export default TablaMemorizar;

import { useState, useEffect } from 'react';
import { Plus, Search, Edit, Trash2 } from 'lucide-react';
import { useMemberStore } from '../../store/memberStore';
import { format } from 'date-fns';
import toast from 'react-hot-toast';

export default function Members() {
  const { members, loading, fetchMembers, deleteMember } = useMemberStore();
  const [searchTerm, setSearchTerm] = useState('');

  useEffect(() => {
    fetchMembers();
  }, [fetchMembers]);

  const filteredMembers = members.filter((member) =>
    `${member.firstName} ${member.lastName} ${member.email}`
      .toLowerCase()
      .includes(searchTerm.toLowerCase())
  );

  const handleDelete = async (id) => {
    if (window.confirm('¿Estás seguro de eliminar este miembro?')) {
      try {
        await deleteMember(id);
        toast.success('Miembro eliminado correctamente');
      } catch (error) {
        toast.error('Error al eliminar el miembro');
      }
    }
  };

  return (
    <div>
      <div className="flex justify-between items-center mb-8">
        <h1 className="text-3xl font-bold text-gray-800">Miembros</h1>
        <button className="btn-primary flex items-center gap-2">
          <Plus size={20} />
          Nuevo Miembro
        </button>
      </div>

      <div className="card mb-6">
        <div className="relative">
          <Search className="absolute left-3 top-3 text-gray-400" size={20} />
          <input
            type="text"
            placeholder="Buscar miembros..."
            className="input-field pl-10"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </div>
      </div>

      <div className="card">
        {loading ? (
          <div className="text-center py-8">Cargando...</div>
        ) : (
          <div className="overflow-x-auto">
            <table className="w-full">
              <thead>
                <tr className="border-b">
                  <th className="text-left py-3 px-4">Nombre</th>
                  <th className="text-left py-3 px-4">Email</th>
                  <th className="text-left py-3 px-4">Teléfono</th>
                  <th className="text-left py-3 px-4">Fecha de Registro</th>
                  <th className="text-left py-3 px-4">Estado</th>
                  <th className="text-center py-3 px-4">Acciones</th>
                </tr>
              </thead>
              <tbody>
                {filteredMembers.map((member) => (
                  <tr key={member.id} className="border-b hover:bg-gray-50">
                    <td className="py-3 px-4">
                      {member.firstName} {member.lastName}
                    </td>
                    <td className="py-3 px-4">{member.email}</td>
                    <td className="py-3 px-4">{member.phone}</td>
                    <td className="py-3 px-4">
                      {format(new Date(member.memberSince), 'dd/MM/yyyy')}
                    </td>
                    <td className="py-3 px-4">
                      <span
                        className={`px-2 py-1 rounded-full text-xs font-medium ${
                          member.isActive
                            ? 'bg-green-100 text-green-800'
                            : 'bg-red-100 text-red-800'
                        }`}
                      >
                        {member.isActive ? 'Activo' : 'Inactivo'}
                      </span>
                    </td>
                    <td className="py-3 px-4">
                      <div className="flex justify-center gap-2">
                        <button className="p-2 hover:bg-gray-100 rounded">
                          <Edit size={16} className="text-primary-600" />
                        </button>
                        <button
                          onClick={() => handleDelete(member.id)}
                          className="p-2 hover:bg-gray-100 rounded"
                        >
                          <Trash2 size={16} className="text-red-600" />
                        </button>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </div>
  );
}

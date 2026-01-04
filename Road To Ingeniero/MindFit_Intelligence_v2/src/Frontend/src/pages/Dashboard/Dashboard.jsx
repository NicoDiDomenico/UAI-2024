import { Users, Calendar, DollarSign, TrendingUp } from 'lucide-react';

export default function Dashboard() {
  const stats = [
    { title: 'Miembros Activos', value: '256', icon: Users, color: 'bg-blue-500' },
    { title: 'Clases Hoy', value: '12', icon: Calendar, color: 'bg-green-500' },
    { title: 'Ingresos Mes', value: '$45,680', icon: DollarSign, color: 'bg-yellow-500' },
    { title: 'Crecimiento', value: '+15%', icon: TrendingUp, color: 'bg-purple-500' },
  ];

  return (
    <div>
      <h1 className="text-3xl font-bold text-gray-800 mb-8">Dashboard</h1>
      
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
        {stats.map((stat, index) => (
          <div key={index} className="card">
            <div className="flex items-center justify-between">
              <div>
                <p className="text-gray-500 text-sm mb-1">{stat.title}</p>
                <p className="text-2xl font-bold text-gray-800">{stat.value}</p>
              </div>
              <div className={`${stat.color} p-3 rounded-lg`}>
                <stat.icon size={24} className="text-white" />
              </div>
            </div>
          </div>
        ))}
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <div className="card">
          <h2 className="text-xl font-semibold mb-4">Próximas Clases</h2>
          <div className="space-y-3">
            {[1, 2, 3].map((i) => (
              <div key={i} className="flex items-center justify-between p-3 bg-gray-50 rounded-lg">
                <div>
                  <p className="font-medium">Yoga Matutino</p>
                  <p className="text-sm text-gray-500">08:00 AM - Sala A</p>
                </div>
                <span className="text-sm text-primary-600 font-medium">15/20</span>
              </div>
            ))}
          </div>
        </div>

        <div className="card">
          <h2 className="text-xl font-semibold mb-4">Membresías por Vencer</h2>
          <div className="space-y-3">
            {[1, 2, 3].map((i) => (
              <div key={i} className="flex items-center justify-between p-3 bg-gray-50 rounded-lg">
                <div>
                  <p className="font-medium">Juan Pérez</p>
                  <p className="text-sm text-gray-500">Plan Premium</p>
                </div>
                <span className="text-sm text-red-600 font-medium">5 días</span>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}

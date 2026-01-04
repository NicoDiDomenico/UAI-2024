import { NavLink } from 'react-router-dom';
import { Home, Users, Calendar, DollarSign, CreditCard, Dumbbell } from 'lucide-react';

const navItems = [
  { to: '/', icon: Home, label: 'Dashboard' },
  { to: '/members', icon: Users, label: 'Miembros' },
  { to: '/classes', icon: Calendar, label: 'Clases' },
  { to: '/trainers', icon: Dumbbell, label: 'Entrenadores' },
  { to: '/membership-plans', icon: CreditCard, label: 'Planes' },
  { to: '/payments', icon: DollarSign, label: 'Pagos' },
];

export default function Sidebar() {
  return (
    <aside className="fixed left-0 top-16 h-[calc(100vh-4rem)] w-64 bg-white shadow-lg">
      <nav className="p-4 space-y-2">
        {navItems.map(({ to, icon: Icon, label }) => (
          <NavLink
            key={to}
            to={to}
            end={to === '/'}
            className={({ isActive }) =>
              `flex items-center gap-3 px-4 py-3 rounded-lg transition-colors ${
                isActive
                  ? 'bg-primary-600 text-white'
                  : 'text-gray-700 hover:bg-gray-100'
              }`
            }
          >
            <Icon size={20} />
            <span className="font-medium">{label}</span>
          </NavLink>
        ))}
      </nav>
    </aside>
  );
}

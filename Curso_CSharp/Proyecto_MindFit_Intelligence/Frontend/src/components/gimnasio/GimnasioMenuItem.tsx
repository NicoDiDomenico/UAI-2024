import { Link } from 'react-router-dom'
import type { GimnasioMenuItemConfig } from './gimnasioMenuConfig'

interface GimnasioMenuItemProps {
  item: GimnasioMenuItemConfig
}

export function GimnasioMenuItem({ item }: GimnasioMenuItemProps) {
  return (
    <Link className="gimnasio-menu-item" to={item.path}>
      <span className="gimnasio-menu-item__icon" aria-hidden="true">
        {item.iconLabel}
      </span>
      <span className="gimnasio-menu-item__label">{item.label}</span>
      <span className="gimnasio-menu-item__description">{item.description}</span>
    </Link>
  )
}

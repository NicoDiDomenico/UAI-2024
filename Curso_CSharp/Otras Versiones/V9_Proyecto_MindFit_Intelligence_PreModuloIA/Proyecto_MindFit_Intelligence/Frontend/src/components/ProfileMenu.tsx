import { useEffect, useRef, useState } from 'react'

interface ProfileMenuProps {
  onLogout: () => void
}

export function ProfileMenu({ onLogout }: ProfileMenuProps) {
  const [isOpen, setIsOpen] = useState(false)
  const menuRef = useRef<HTMLDivElement>(null)
  const triggerRef = useRef<HTMLButtonElement>(null)

  useEffect(() => {
    if (!isOpen) {
      return
    }

    function handlePointerDown(event: PointerEvent) {
      if (!menuRef.current?.contains(event.target as Node)) {
        setIsOpen(false)
      }
    }

    function handleKeyDown(event: KeyboardEvent) {
      if (event.key === 'Escape') {
        setIsOpen(false)
        triggerRef.current?.focus()
      }
    }

    document.addEventListener('pointerdown', handlePointerDown)
    document.addEventListener('keydown', handleKeyDown)

    return () => {
      document.removeEventListener('pointerdown', handlePointerDown)
      document.removeEventListener('keydown', handleKeyDown)
    }
  }, [isOpen])

  function handleLogout() {
    setIsOpen(false)
    onLogout()
  }

  return (
    <div className="workspace-profile" ref={menuRef}>
      <button
        ref={triggerRef}
        className="workspace-profile__trigger"
        type="button"
        aria-label="Abrir menu de perfil"
        aria-haspopup="menu"
        aria-expanded={isOpen}
        onClick={() => setIsOpen((current) => !current)}
      >
        <svg viewBox="0 0 24 24" aria-hidden="true">
          <circle cx="8.5" cy="6.5" r="3.5" />
          <path d="M2.75 19.5c.35-4 2.45-6 6.25-6 1.35 0 2.5.25 3.4.75" />
          <circle cx="17.5" cy="17.5" r="2.75" />
          <path d="M17.5 12.75v2M17.5 20.25v2M12.75 17.5h2M20.25 17.5h2M14.15 14.15l1.4 1.4M19.45 19.45l1.4 1.4M20.85 14.15l-1.4 1.4M15.55 19.45l-1.4 1.4" />
        </svg>
      </button>

      {isOpen ? (
        <div className="workspace-profile__menu" role="menu" aria-label="Menu de perfil">
          <button
            className="workspace-profile__logout"
            type="button"
            role="menuitem"
            onClick={handleLogout}
          >
            Cerrar sesion
          </button>
        </div>
      ) : null}
    </div>
  )
}

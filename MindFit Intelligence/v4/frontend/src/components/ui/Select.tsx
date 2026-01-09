import type { SelectHTMLAttributes } from "react";

interface SelectProps extends SelectHTMLAttributes<HTMLSelectElement> {
  label?: string;
  options: Array<{ value: string | number; label: string }>;
}

export const Select = ({
  label,
  options,
  className = "",
  ...props
}: SelectProps) => {
  return (
    <div className="input-group">
      {label && <label className="input-label">{label}</label>}
      <select className={`input ${className}`} {...props}>
        <option value="">-- Seleccionar --</option>
        {options.map((option) => (
          <option key={option.value} value={option.value}>
            {option.label}
          </option>
        ))}
      </select>
    </div>
  );
};

import React, { useState, useEffect, useRef } from 'react';
import ReactDOM from 'react-dom';

const ComboBox = ({
  label,
  options = [],
  selected,
  onChange,
  placeholder = "Select an option",
  error,
  className = '',
  onDropdownToggle,
  size = 'medium',
}) => {
  const [isOpen, setIsOpen] = useState(false);
  const [dropdownPosition, setDropdownPosition] = useState({ top: 0, left: 0 });
  const comboRef = useRef(null);

  const handleSelect = (option) => {
    onChange(option.id);
    setIsOpen(false);
  };

  useEffect(() => {
    if (onDropdownToggle) onDropdownToggle(isOpen);

    if (isOpen && comboRef.current) {
      const rect = comboRef.current.getBoundingClientRect();
      setDropdownPosition({ top: rect.bottom, left: rect.left });
    }
  }, [isOpen, onDropdownToggle]);

  const sizeStyles = {
    small: 'p-1 text-sm',
    medium: 'p-2 text-base',
    large: 'p-3 text-lg',
  };

  return (
    <div className="relative" ref={comboRef}>
      {label && (
        <label className="block text-sm font-medium text-gray-700 mb-1">
          {label}
        </label>
      )}
      <div
        className={`block w-full ${className} ${sizeStyles[size]} border rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 ${error ? 'border-red-500' : 'border-gray-300'}`}
        onClick={() => setIsOpen(!isOpen)}
      >
        <div className="flex">
          {selected ? (
            <span className="inline-block text-gray-900">
              {options.find(option => option.id === selected)?.name}
            </span>
          ) : (
            <span className="text-gray-400">{placeholder}</span>
          )}
        </div>
      </div>
      {isOpen &&
        ReactDOM.createPortal(
          <div
            className="absolute z-50 bg-white border border-gray-300 rounded-md shadow-lg max-h-48 overflow-y-auto"
            style={{
              top: dropdownPosition.top,
              left: dropdownPosition.left,
              minWidth: '200px',
              width: 'auto',
            }}
          >
            {options.map((option) => (
              <div
                key={option.id}
                className={`cursor-pointer ${sizeStyles[size]} ${selected === option.id ? 'bg-blue-100 text-blue-700' : 'hover:bg-gray-100'}`}
                onClick={() => handleSelect(option)}
              >
                {option.name}
              </div>
            ))}
          </div>,
          document.body
        )}
      {error && <p className="mt-1 text-sm text-red-600">{error}</p>}
    </div>
  );
};

export default ComboBox;
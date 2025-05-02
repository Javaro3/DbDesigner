import React, { useState, useEffect } from 'react';

const MultiComboBox = ({
  label,
  options = [],
  selected = [],
  onChange,
  placeholder = "Select options",
  error,
  className = '',
  onDropdownToggle,
  size = 'medium',
}) => {
  const [isOpen, setIsOpen] = useState(false);

  const handleSelect = (option) => {
    const newSelected = selected.includes(option.id)
      ? selected.filter((item) => item !== option.id)
      : [...selected, option.id];
    onChange(newSelected);
  };

  useEffect(() => {
    if (onDropdownToggle) onDropdownToggle(isOpen);
  }, [isOpen, onDropdownToggle]);

  const sizeStyles = {
    small: 'p-1 text-sm',
    medium: 'p-2 text-base',
    large: 'p-3 text-lg',
  };

  return (
    <div className="relative">
      {label && (
        <label className="block text-sm font-medium text-gray-700 mb-1">
          {label}
        </label>
      )}
      <div
        className={`block w-full ${className} ${sizeStyles[size]} border rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 ${error ? 'border-red-500' : 'border-gray-300'}`}
        onClick={() => setIsOpen(!isOpen)}
      >
        <div className="flex flex-wrap gap-1">
          {selected.length > 0 ? (
            selected.map((selectedId) => {
              const selectedOption = options.find(option => option.id === selectedId);
              return selectedOption ? (
                <span
                  key={selectedId}
                  className="inline-block bg-blue-100 text-blue-700 px-2 py-1 rounded-full text-xs"
                >
                  {selectedOption.name}
                </span>
              ) : null;
            })
          ) : (
            <span className="text-gray-400">{placeholder}</span>
          )}
        </div>
      </div>
      {isOpen && (
        <div className="absolute z-10 w-full bg-white border border-gray-300 rounded-md mt-1 shadow-lg max-h-48 overflow-y-auto">
          {options.map((option) => (
            <div
              key={option.id}
              className={`cursor-pointer ${sizeStyles[size]} ${selected.includes(option.id) ? 'bg-blue-100 text-blue-700' : 'hover:bg-gray-100'}`}
              onClick={() => handleSelect(option)}
            >
              {option.name}
            </div>
          ))}
        </div>
      )}
      {error && <p className="mt-1 text-sm text-red-600">{error}</p>}
    </div>
  );
};

export default MultiComboBox;
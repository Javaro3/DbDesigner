import React from 'react';

const Checkbox = ({
  label,
  checked,
  onChange,
  error,
  className = '',
  disabled = false,
}) => {
  return (
    <div className={`flex items-center ${className}`}>
      <input
        type="checkbox"
        checked={checked}
        onChange={onChange}
        disabled={disabled}
        className={`h-5 w-5 text-blue-500 border-gray-300 rounded focus:ring-6 focus:ring-blue-500 ${
          disabled ? 'bg-gray-200 cursor-not-allowed' : ''
        } ${error ? 'border-red-500' : ''}`}
      />
      {label && (
        <label
          className={`ml-2 text-sm ${
            disabled ? 'text-gray-500' : 'text-gray-700'
          }`}
        >
          {label}
        </label>
      )}
      {error && <p className="mt-1 text-sm text-red-600">{error}</p>}
    </div>
  );
};

export default Checkbox;
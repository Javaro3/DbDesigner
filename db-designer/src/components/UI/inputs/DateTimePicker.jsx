import React, { useState } from 'react';

const DateTimePicker = ({
  label,
  selectedDateTime,
  onChange,
  placeholder = "Select date and time",
  error,
  className = '',
}) => {
  const [dateTimeValue, setDateTimeValue] = useState(selectedDateTime || '');

  const handleChange = (e) => {
    const newDateTime = e.target.value;
    setDateTimeValue(newDateTime);
    onChange(newDateTime);
  };

  return (
    <div className={`relative ${className}`}>
      {label && (
        <label className="block text-sm font-medium text-gray-700 mb-1">
          {label}
        </label>
      )}
      <input
        type="datetime-local"
        value={dateTimeValue}
        onChange={handleChange}
        placeholder={placeholder}
        className={`block w-full p-2 border rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 ${error ? 'border-red-500' : 'border-gray-300'}`}
      />
      {error && <p className="mt-1 text-sm text-red-600">{error}</p>}
    </div>
  );
};

export default DateTimePicker;
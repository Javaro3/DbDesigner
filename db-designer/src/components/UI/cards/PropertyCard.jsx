import React, { useState } from 'react';
import ComboBox from '../inputs/ComboBox';
import Input from '../inputs/Input';
import Button from '../buttons/Button';

const PropertyCard = ({ model, properties, setPropertyError, onDelete, onPropertyChange, propertyCombobox, updateProperty }) => {
  const [isSupportsParameter, setIsSupportsParameter] = useState(propertyCombobox.find((t) => t.id === model.propertyId)?.hasParams);

  const handlePropertyChange = async (propertyId) => {
    const prevPropertyId = model.propertyId;
    if (properties.some(x => x.propertyId === propertyId)){
      if (propertyId != prevPropertyId)
        setPropertyError('This property is already exist');
      return;
    }

    model.propertyId = propertyId;
    setIsSupportsParameter(propertyCombobox.find((t) => t.id === model.propertyId)?.hasParams);
    
    await updateProperty(model, prevPropertyId);
    onPropertyChange(model);
  };

  const handlePropertyParamsChange = (e) => {
    model.propertyParams = e.target.value;
    onPropertyChange(model);
  };

  return (
    <div className="bg-gray-50 rounded p-1 my-1 shadow-md relative flex items-start">
      <div className="flex-1">
        <ComboBox
          options={propertyCombobox}
          selected={model.propertyId}
          onChange={handlePropertyChange}
          size="small"
          placeholder="Select a property"
          className="w-full mb-2"/>

        <Input
          disabled={!isSupportsParameter}
          size="small"
          onChange={handlePropertyParamsChange}
          onBlur={() => updateProperty(model, model.propertyId)}
          placeholder="Enter parameter"
          className="w-full"/>
      </div>

      <Button onClick={onDelete} variant="danger" size="small" className="ml-2 my-5">
        <i className="fa-solid fa-trash"></i>
      </Button>
    </div>
  );
};

export default PropertyCard;
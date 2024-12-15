import React, { useState } from 'react';
import Input from '../inputs/Input';
import Button from '../buttons/Button';
import ComboBox from '../inputs/ComboBox';
import PropertyCard from './PropertyCard';
import { deleteById, update } from '../../../utils/apiHelper';
import { addPropertyToColumn, deletePropertyFromColumn } from '../../../services/columnPropertyService';

const ColumnCard = ({ model, onDelete, onColumnChange, sqlTypeCombobox, propertyCombobox }) => {
  const [isExpanded, setIsExpanded] = useState(true);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [modalPosition, setModalPosition] = useState({ top: 0, left: 0 });
  const [propertyError, setPropertyError] = useState('');
  const [isSqlTypeParamsDisabled, setIsSqlTypeParamsDisabled] = useState(!sqlTypeCombobox.find(t => t.id === model.sqlTypeId)?.hasParams ?? true);

  const handleToggleExpand = () => setIsExpanded(!isExpanded);

  const handleDelete = async () => {
    onDelete(model.id);
    await deleteById('Column', model.id);
  }

  const handleModalToggle = (e) => {
    const rect = e.currentTarget.getBoundingClientRect();
    setModalPosition({top: rect.top, left: rect.right});
    setIsModalVisible(!isModalVisible);
  };

  const handleNameChange = (e) => {
    model.name = e.target.value;
    onColumnChange(model);
  };

  const handleDescriptionChange = (e) => {
    model.description = e.target.value;
    onColumnChange(model);
  };

  const handleSqlTypeChange = async (e) => {
    setIsSqlTypeParamsDisabled(!sqlTypeCombobox.find((t) => t.id == e).hasParams);
    model.sqlTypeId = e
    onColumnChange(model);
    await updateColumn();
  };

  const handleSqlTypeParamsChange = (e) => {
    model.sqlTypeParams = e.target.value;
    onColumnChange(model);
  };

  const updateColumn = async () => {
    const column = {
      id: model.id,
      name: model.name,
      description: model.description,
      sqlTypeId: model.sqlTypeId,
      sqlTypeParams: model.sqlTypeParams
    };

    await update('Column', column);
  }

  const updateProperty = async (property, prevPropertyId) => {
    const columnProperty = {
      propertyId: property.propertyId,
      columnId: model.id,
      propertyParams: property.propertyParams,
      prevPropertyId: prevPropertyId
    };

    await update('ColumnProperty', columnProperty);
  }

  const addProperty = async () => {
    if (model.properties.length === propertyCombobox.length){
      setPropertyError('The column already has all properties');
      return;
    }
    setPropertyError('');
    
    const newProperty = {
      propertyId: propertyCombobox.find(e => !model.properties.map(x => x.propertyId).includes(e.id)).id,
      propertyParams: ''
    };

    await addPropertyToColumn(newProperty, model.id);
    model.properties.push(newProperty);
    onColumnChange(model);
  };

  const onPropertyDelete = async(propertyId) => {
    setPropertyError('');
    const newProperties = model.properties.filter(e => e.propertyId != propertyId);
    model.properties = newProperties;
    await deletePropertyFromColumn({propertyId: propertyId, columnId: model.id});
    
    onColumnChange(model);
  };

  const handlePropertyChange = (newProperty) => {
    setPropertyError('');
    const newProperties = model.properties.filter(property => property.propertyId == newProperty.propertyId && !model.properties.includes(newProperty.propertyId) ? newProperty : property);
    model.properties = newProperties;
    onColumnChange(model);
  };

  return (
    <div className="flex items-center bg-gray-50 rounded m-1">
      <div className="flex flex-col space-y-2 flex-1 m-1">
        <Input value={model.name} onChange={handleNameChange} onBlur={updateColumn} placeholder="Name" size='small' className="w-full"/>
        <ComboBox selected={model.sqlTypeId} options={sqlTypeCombobox} onChange={handleSqlTypeChange} size='small' className="w-full"/>
        <Input value={model.description} onChange={handleDescriptionChange} onBlur={updateColumn} placeholder="Description" size='small' className="w-full"/>
      </div>

      <div className="flex flex-col space-y-2 m-1">
        <Button onClick={handleModalToggle} variant="primary" size="small">
          <i className="fa-solid fa-cog"></i>
        </Button>
        <Button onClick={() => handleDelete(model.id)} variant="danger" size="small">
          <i className="fa-solid fa-trash"></i>
        </Button>
      </div>

      {isModalVisible && (
        <div className="absolute bg-white shadow-lg rounded p-4 z-10 border w-72" style={{ top: `${modalPosition.top}px`, left: `${modalPosition.left}px` }}>
          
          <Input className='w-full' value={model.sqlTypeParams} onChange={handleSqlTypeParamsChange} onBlur={updateColumn} placeholder='Sql Type Params' size='small' disabled={isSqlTypeParamsDisabled}/>
          
          <Button size='small' className='my-1 mr-1' onClick={addProperty}>
            <i className="fa-solid fa-plus"></i>
          </Button>
          <Button onClick={handleToggleExpand} variant="secondary" size="small">
              {isExpanded
                ? <i className="fa-solid fa-square-minus"></i>
                : <i className="fa-solid fa-caret-down"></i>}
          </Button>

          <div className="p-1 bg-gray-300 rounded shadow">
            {isExpanded && (
              model.properties.length === 0
                ? (<></>)
                : (model.properties.map((property, i) => (
                  <PropertyCard
                    key={i}
                    model={property}
                    properties={model.properties}
                    setPropertyError={setPropertyError}
                    onPropertyChange={handlePropertyChange}
                    onDelete={() => onPropertyDelete(property.propertyId)}
                    propertyCombobox={propertyCombobox}
                    updateProperty={updateProperty} />
            ))))}
            {propertyError && <p className="mt-1 text-sm text-red-600">{propertyError}</p>}
          </div>

        </div>
      )}
    </div>
  );
};

export default ColumnCard;
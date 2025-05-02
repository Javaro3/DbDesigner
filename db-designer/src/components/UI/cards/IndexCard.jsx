import React from 'react';
import Input from '../inputs/Input';
import Button from '../buttons/Button';
import ComboBox from '../inputs/ComboBox';
import MultiComboBox from '../inputs/MultiComboBox';
import { update } from '../../../utils/apiHelper';

const IndexCard = ({ model, onDelete, onIndexChange, indexTypeCombobox, columnCombobox }) => {
  const handleDelete = () => onDelete(model.id);

  const handleDescriptionChange = (e) => {
    model.description = e.target.value;
    onIndexChange(model);
  };

  const handleIndexTypeChange = async (e) => {
    model.indexTypeId = e;
    onIndexChange(model);
    await updateIndex()
  };

  const handleColumnChange = async (e) => {
    model.columns = e;
    onIndexChange(model);
    await updateIndex();
  };

  const updateIndex = async () => {
    const index = {
      id: model.id,
      description: model.description,
      indexTypeId: model.indexTypeId,
      columns: model.columns
    };

    await update('Index', index);
  }

  return (
    <div className="flex items-center bg-gray-50 rounded m-1 p-1">
      <div className="flex-1">
        <ComboBox
          options={indexTypeCombobox}
          selected={model.indexTypeId}
          onChange={handleIndexTypeChange}
          size="small"
          placeholder="Select Index Type"
          className="w-full mb-2"/>

        <MultiComboBox
          options={columnCombobox}
          selected={model.columns}
          size="small"
          onChange={handleColumnChange}
          placeholder="Select columns"
          className="w-full mb-2"/>

        <Input
          value={model.description}
          size="small"
          onChange={handleDescriptionChange}
          onBlur={updateIndex}
          placeholder="Description"
          className="w-full"/>
      </div>

      <Button onClick={() => handleDelete(model.id)} variant="danger" size="small" className="ml-2 my-5">
        <i className="fa-solid fa-trash"></i>
      </Button>
    </div>
  );
};

export default IndexCard;
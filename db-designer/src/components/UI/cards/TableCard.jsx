import React, { useState } from 'react';
import Input from '../inputs/Input';
import Button from '../buttons/Button';
import ColumnCard from './ColumnCard';
import IndexCard from './IndexCard';
import { deleteById, update } from '../../../utils/apiHelper';
import { addColumnToTable } from '../../../services/columnService';
import { addIndexToTable } from '../../../services/indexService';

const TableCard = ({ node, onModelChange, onDelete }) => {
  const [isExpanded, setIsExpanded] = useState(true);
  const [isMenuVisible, setIsMenuVisible] = useState(false);
  const [menuPosition, setMenuPosition] = useState({ top: 0, left: 0 });
  
  const handleToggleExpand = () => setIsExpanded(!isExpanded);

  const handleDelete = () => onDelete(node.id);

  const handleToggleMenu = (e) => {
    const rect = e.currentTarget.getBoundingClientRect();
    setMenuPosition({top: rect.bottom, left: rect.left});
    setIsMenuVisible(!isMenuVisible);
  };

  const handleNameChange = (e) => {
    node.name = e.target.value;
    onModelChange(node);
  }

  const handleDescriptionChange = (e) => {
    node.description = e.target.value;
    onModelChange(node);
  }

  const updateTable = async () => {
    const table = {
      id: node.id,
      name: node.name,
      description: node.description
    };

    await update('Table', table);
  }

  const addColumn = async () => {
    const newColumn = await addColumnToTable({id: 0, sqlTypeId: node.sqlTypeCombobox[0].id}, node.id);
    node.columns.push(newColumn);
    onModelChange(node);
  };

  const onColumnDelete = (id) => {
    const newColumns = node.columns.filter(e => e.id != id);
    node.columns = newColumns;
    
    onModelChange(node);
  };

  const onIndexDelete = async (id) => {
    const newIndexes = node.indexes.filter(e => e.id != id);
    node.indexes = newIndexes;
    await deleteById('Index', id);
    onModelChange(node);
  };

  const handleColumnChange = (newColumn) => {
    const newColumns = node.columns.filter(column => column.id == newColumn.id ? newColumn : column);
    node.columns = newColumns;
    onModelChange(node);
  };

  const handleIndexChange = (newIndex) => {
    const newIndexes = node.indexes.filter(index => index.id == newIndex.id ? newIndex : index);
    node.indexes = newIndexes;
    onModelChange(node);
  };

  const addIndex = async () => {
    const newIndex = await addIndexToTable({
      id: 0,
      description: '',
      indexTypeId: node.indexTypeCombobox[0].id,
      columns: node.columns.length !== 0 ? [node.columns[0].id] : [] 
    });

    node.indexes.push(newIndex);
    onModelChange(node);
  };

  return (
    <div className="p-4 mb-2 bg-white rounded shadow">
      <div className="flex items-center justify-between flex-wrap">
        <h2 className="text-lg font-semibold w-full sm:w-auto">{node.name}</h2>
        <div className="flex space-x-2 mt-2 sm:mt-0">
          <Button onClick={handleToggleMenu} variant="primary" size="small">
            <i className="fa-solid fa-plus"></i>
          </Button>
          <Button onClick={handleDelete} variant="danger" size="small">
            <i className="fa-solid fa-trash"></i>
          </Button>
          <Button onClick={handleToggleExpand} variant="secondary" size="small">
            {isExpanded
              ? <i className="fa-solid fa-square-minus"></i>
              : <i className="fa-solid fa-caret-down"></i>}
          </Button>
        </div>
      </div>

      {isMenuVisible && (
        <div 
          className="absolute bg-white shadow-lg rounded p-2 z-10 border"
          style={{ top: `${menuPosition.top}px`, left: `${menuPosition.left}px` }}>
          <Button size="small" onClick={addColumn} className="w-full mb-2">
            <i className="fa-solid fa-table-columns"></i>
          </Button>
          <Button size="small" onClick={addIndex} className="w-full">
            <i className="fa-solid fa-indent"></i>
          </Button>
        </div>
      )}

      {isExpanded && (
        <div className="mt-4">
          <Input value={node.name} onChange={handleNameChange} onBlur={updateTable} placeholder="Name" className="mb-2"/>
          <Input value={node.description} onChange={handleDescriptionChange} onBlur={updateTable} placeholder="Description" size='small' className="mb-4"/>

          <div className="mt-2 p-1 bg-gray-300 rounded shadow">
            {node.columns.length === 0 ? (
              <></>
            ) : (
              node.columns.map((column, i) => (
                <ColumnCard
                  key={i}
                  model={column}
                  onDelete={() => onColumnDelete(column.id)}
                  onColumnChange={handleColumnChange}
                  sqlTypeCombobox={node.sqlTypeCombobox}
                  propertyCombobox={node.propertyCombobox}
                />
              ))
            )}
          </div>

          <div className="mt-2 p-1 bg-gray-300 rounded shadow">
            {node.indexes.length === 0 ? (
              <></>
            ) : (
              node.indexes.map((index, i) => (
                <IndexCard
                  key={i}
                  model={index}
                  onDelete={() => onIndexDelete(index.id)}
                  onIndexChange={handleIndexChange}
                  indexTypeCombobox={node.indexTypeCombobox}
                  columnCombobox={node.columns}
                />
              ))
            )}
          </div>
        </div>
      )}
    </div>
  );
};

export default TableCard;
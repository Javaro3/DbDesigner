import React, { useState } from 'react';
import { getSmoothStepPath, getEdgeCenter } from 'react-flow-renderer';
import ComboBox from '../inputs/ComboBox';
import Button from '../buttons/Button';
import { update } from '../../../utils/apiHelper';

const TableEdge = ({
  id,
  sourceX, sourceY,
  targetX, targetY,
  sourcePosition, targetPosition,
  style = {},
  data, markerEnd
}) => {
  const [isExpanded, setIsExpanded] = useState(true);
  const edgePath = getSmoothStepPath({sourceX, sourceY, sourcePosition, targetPosition, targetX, targetY});
  const [edgeCenterX, edgeCenterY] = getEdgeCenter({sourceX, sourceY, targetX, targetY});

  const handleToggleExpand = () => setIsExpanded(!isExpanded);

  const handleOnActionChange = async (e, action) => {
    if (action === 'onDelete'){
      data.onDelete = e;
    } else {
      data.onUpdate = e;
    }
    await update('Relation', {
      id: id,
      sourceColumnId: data.sourceColumn, 
      targetColumnId: data.targetColumn, 
      onDeleteId: data.onDelete, 
      onUpdateId: data.onUpdate});
   
    data.onEdgeChange(id, data);
  };

  const handleDelete = () => {
    data.onEdgeDelete(id);
  };

  return (
    <>
      <path
        id={id}
        style={{
          ...style,
          strokeWidth: 3,
          stroke: 'rgb(59 130 246)',
        }}
        className="react-flow__edge-path"
        d={edgePath}
        markerEnd={markerEnd}
      />
      <foreignObject
        x={edgeCenterX - 150}
        y={edgeCenterY - 100}
        width={300}
        height={200}
        requiredExtensions="http://www.w3.org/1999/xhtml"
      >
        <div className="p-2 mb-2 bg-gray-100 rounded shadow">
          <div className="flex items-center justify-between flex-wrap">
            <h2 className="text-lg font-semibold w-full sm:w-auto">{`${data.source.column.name} → ${data.target.column.name}`}</h2>
            <div className="flex space-x-2 mt-2 sm:mt-0">
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

          {isExpanded && (
            <div className="mt-4">
              <ComboBox
                selected={data.onDelete}
                options={data.relationActionCombobox}
                onChange={(e) => handleOnActionChange(e, 'onDelete')}
                placeholder="On Delete" size='small' className='mb-2'/>
              <ComboBox
                selected={data.onUpdate} 
                options={data.relationActionCombobox} 
                onChange={(e) => handleOnActionChange(e, 'onUpdate')}
                placeholder="On Update" size='small' className='mb-2'/>
            </div>
          )}
        </div>
      </foreignObject>
    </>
  );
};

export default TableEdge;
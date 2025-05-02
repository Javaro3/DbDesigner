import React from 'react';
import { Handle, Position } from 'react-flow-renderer';

const TableNode = ({ data }) => {
  return (
    <div className="bg-gray-800 text-white items-center rounded-lg shadow-lg p-2 w-72">
      <h3 className="text-xl font-bold mb-1">{data.name}</h3>
      <p className="text-sm text-gray-400 mb-2">{data.description}</p>

      <div className="mt-3">
        <h4 className="font-semibold text-lg mb-1 text-white">Columns</h4>
        <div className="space-y-3">
          {data.columns.length > 0 ? (
            data.columns.map((column) => {
              const sqlType = data.sqlTypeCombobox.find((t) => t.id === column.sqlTypeId);
              const sqlTypeText = sqlType ? `${sqlType.name}${sqlType.hasParams ? `(${column.sqlTypeParams})` : ''}` : 'Unknown';
              const handleData = JSON.stringify({ table: {id: data.id, name: data.name}, column: {id: column.id, name: column.name} });
              return (
                <div key={column.id} className="bg-gray-700 p-2 rounded-lg shadow-md flex items-center justify-between relative">
                  <Handle
                    type='source'
                    id={handleData}
                    position={Position.Right}
                    style={{
                      top: '30%',
                      position: 'absolute',
                      width: '16px',
                      height: '16px',
                      borderRadius: '4px',
                      background: 'rgb(59 130 246)',
                      right: '-16px'
                    }}
                  />

                  <Handle
                    type='target'
                    id={handleData}
                    position={Position.Right}
                    style={{
                      top: '70%',
                      position: 'absolute',
                      width: '16px',
                      height: '16px',
                      borderRadius: '4px',
                      background: 'rgb(239 68 68)',
                      right: '-16px'
                    }}
                  />

                  <div>
                    <div className="text-base font-bold text-white">{column.name} - {sqlTypeText}</div>
                    {column.description && (
                      <div className="text-sm text-gray-400 mt-1">
                        {column.description}
                      </div>
                    )}  
                    {column.properties && column.properties.length > 0 && (
                      <div className="mt-1 text-xs text-gray-300">
                        <span className="font-semibold">Properties: </span>
                        {column.properties.map((property, i) => {
                          const propertyInfo = data.propertyCombobox.find((t) => t.id === property.propertyId);
                          const propertyText = propertyInfo ? `${propertyInfo.name}${propertyInfo.hasParams ? `: ${property.propertyParams}` : ''}` : 'Unknown';
                          return (
                            <span key={i}>
                              {propertyText}{i < column.properties.length - 1 ? ', ' : ''}
                            </span>
                          );
                        })}
                      </div>
                    )}
                  </div>
                </div>
              );
            })
          ) : (
            <div className="text-sm text-gray-500">No columns available</div>
          )}
        </div>
      </div>

      <div className="mt-3">
        <h4 className="font-semibold text-lg mb-1 text-white">Indexes:</h4>
        <div className="space-y-3">
          {data.indexes.length > 0 ? (
            data.indexes.map((index, i) => {
              const indexType = data.indexTypeCombobox.find((i) => i.id === index.indexTypeId);
              const indexTypeName = indexType ? indexType.name : 'Unknown';

              return (
                <div key={i} className="bg-gray-700 p-2 rounded-lg shadow-md">
                  <div className="text-base font-bold text-white">
                    {indexTypeName}
                  </div>
                  {index.description && (
                    <div className="text-sm text-gray-400 mt-1">
                      {index.description}
                    </div>
                  )}
                  {index.columns && index.columns.length > 0 && (
                    <div className="mt-1 text-xs text-gray-300">
                      <span className="font-semibold">Columns: </span>
                      {index.columns.map((column, columnIndex) => (
                        <span key={columnIndex}>
                          {data.columns.find((e) => e.id === column)?.name}
                          {columnIndex < index.columns.length - 1 ? ', ' : ''}
                        </span>
                      ))}
                    </div>
                  )}
                </div>
              );
            })
          ) : (
            <div className="text-sm text-gray-500">No indexes available</div>
          )}
        </div>
      </div>
    </div>
  );
};

export default TableNode;
import React, { useEffect, useState, useMemo, useCallback } from 'react';
import TableNode from '../../components/UI/flow/TableNode';
import TableCard from '../../components/UI/cards/TableCard';
import GenerateCard from '../../components/UI/cards/GenerateCard';
import Button from '../../components/UI/buttons/Button';
import { deleteById, getForCombobox } from '../../utils/apiHelper';
import { getSqlTypeForComboboxByDatabase } from '../../services/sqlTypeService';
import { getIndexTypeForComboboxByDatabase } from '../../services/indexTypeService';
import ReactFlow, { Background, Controls, ReactFlowProvider, addEdge, useEdgesState, useNodesState } from 'react-flow-renderer';
import TableEdge from '../../components/UI/flow/TableEdge';
import Loader from '../../components/UI/loaders/Loader';
import { useParams } from 'react-router-dom';
import { getForDiagram } from '../../services/projectService';
import { addTableToProject } from '../../services/tableService';
import { addRelationToProject } from '../../services/relationService';
import Modal from '../../components/UI/modal/Modal';


export default function ProjectDiagram() {
  const { id } = useParams();
  const [dataBaseId, setDataBaseId] = useState(0);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [nodes, setNodes, onNodesChange] = useNodesState([]);
  const [edges, setEdges, onEdgesChange] = useEdgesState([]);
  const [sqlTypeCombobox, setSqlTypeCombobox] = useState([]);
  const [propertyCombobox, setPropertyCombobox] = useState([]);
  const [indexTypeCombobox, setIndexTypeCombobox] = useState([]);
  let relationActionCombobox = [];
  const [loading, setLoading] = useState(true);
  
  const nodeTypes = useMemo(() => ({ tableNode: TableNode }), []);
  const edgeTypes = useMemo(() => ({ tableEdge: TableEdge }), []);

  const onEdgeDelete = async (id) => {
    await deleteById('Relation', id);
    setEdges((prevEdges) => prevEdges.filter((edge) => edge.id !== id));
  };

  const onEdgeChange = (id, changedEdge) => {
    setEdges((prevEdges) =>
      prevEdges.map((edge) => {
        if (edge.id === id) {
          return {
            ...edge,
            data: { ...changedEdge },
          };
        }
        return edge;
      })
    );
  };

  useEffect(() => {
    const fetchComboboxes = async () => {
      const project = await getForDiagram(id);
      setDataBaseId(project.dataBase.id);
      const sqlTypes = await getSqlTypeForComboboxByDatabase(project.dataBase.id);
      const properties = await getForCombobox('Property');
      const indexTypes = await getIndexTypeForComboboxByDatabase(project.dataBase.id);
      const relationActions = await getForCombobox('RelationAction');
      setSqlTypeCombobox(sqlTypes);
      setPropertyCombobox(properties);
      setIndexTypeCombobox(indexTypes);
      relationActionCombobox = relationActions;

      const initNodes = project.tables.map(table => {
        return {
          id: String(table.id),
          type: 'tableNode',
          data: {
            id: table.id,
            name: table.name,
            description: table.description,
            columns: table.columns.map(column =>{
              return {
                id: column.id,
                name: column.name,
                description: column.description,
                sqlTypeId: column.sqlTypeId,
                sqlTypeParams: column.sqlTypeParams,
                properties: column.properties.map(property => {
                  return {
                    propertyId: property.propertyId,
                    propertyParams: property.propertyParams
                  }
                })
              }
            }),
            indexes: table.indexes.map(index => {
              return {
                id: index.id,
                description: index.description,
                indexTypeId: index.indexTypeId,
                columns: index.columns
              }
            }),
            sqlTypeCombobox: sqlTypes,
            propertyCombobox: properties,
            indexTypeCombobox: indexTypes,
          },
          position: { x: 250, y: 0 },
        }
      });
      setNodes(initNodes);

      const initEdges = project.relations.map(relation => {
        const findNodeByColumnId = (columnId) => {
          return initNodes.find((node) =>
            node.data.columns.some((column) => column.id === columnId)
          );
        };
        
        const sourceNode = findNodeByColumnId(relation.sourceColumn.id);
        const targetNode = findNodeByColumnId(relation.targetColumn.id);
        
        const sourceHandle = {
          table: {
            id: sourceNode.data.id,
            name: sourceNode.data.name,
          },
          column: {
            id: relation.sourceColumn.id,
            name: relation.sourceColumn.name,
          }
        };

        const targetHandle = {
          table: {
            id: targetNode.data.id,
            name: targetNode.data.name,
          },
          column: {
            id: relation.targetColumn.id,
            name: relation.targetColumn.name,
          }
        };
                
        return {
          id: String(relation.id),
          source: String(sourceHandle.table.id),
          target: String(targetHandle.table.id),
          sourceHandle: JSON.stringify(sourceHandle),
          targetHandle: JSON.stringify(targetHandle),
          type: 'tableEdge',
          data: {
            source: sourceHandle,
            target: targetHandle, 
            onEdgeDelete: onEdgeDelete,
            onEdgeChange: onEdgeChange,
            sourceColumn: relation.sourceColumn.id,
            targetColumn: relation.targetColumn.id,
            onDelete: relation.onDeleteId,
            onUpdate: relation.onUpdateId,
            relationActionCombobox: relationActions
          }
        }
      });
      setEdges(initEdges);
      setLoading(false);
    };

    fetchComboboxes();
  }, []);

  const addNode = async () => {
    const newTable = await addTableToProject({id: 0, name: '', description: ''}, id);

    const node = {
      id: String(newTable.id),
      type: 'tableNode',
      data: {
        id: newTable.id,
        name: '',
        description: '',
        columns: [],
        indexes: [],
        sqlTypeCombobox: sqlTypeCombobox,
        propertyCombobox: propertyCombobox,
        indexTypeCombobox: indexTypeCombobox,
      },
      position: { x: 250, y: 0 },
    }; 
    setNodes((prevNodes) => [...prevNodes, node]);
  };

  const onNodeDelete = async (id) => {
    await deleteById('Table', id);
    setNodes((prevNodes) => prevNodes.filter((node) => node.data.id !== id));
    setEdges((prevEdges) => prevEdges.filter((edge) => edge.data.source.table.id !== id && edge.data.target.table.id !== id));
  };

  const onNodeChange = (changedTable) => {
    setNodes((prevNodes) =>
      prevNodes.map((node) => {
        if (node.data.id === changedTable.id) {
          return {
            ...node,
            data: { ...changedTable },
          };
        }
        return node;
      })
    );

    setEdges((prevEdges) => prevEdges.filter((edge) => {
      return (changedTable.id == edge.data.source.table.id && 
              changedTable.columns.some(e => e.id === edge.data.source.column.id)) || 
             (changedTable.id == edge.data.target.table.id &&
              changedTable.columns.some(e => e.id === edge.data.target.column.id)) ||
             (changedTable.id != edge.data.source.table.id && changedTable.id != edge.data.target.table.id);
    }));

    setEdges((prevEdges) => prevEdges.map((edge) => {
      if (edge.data.source.table.id === changedTable.id){
        edge.data.source.column.name = changedTable.columns.find((c) => c.id == edge.data.source.column.id).name;
      }
      if (edge.data.target.table.id === changedTable.id){
        edge.data.target.column.name = changedTable.columns.find((c) => c.id == edge.data.target.column.id).name;
      }
      edge.sourceHandle = JSON.stringify(edge.data.source);
      edge.targetHandle = JSON.stringify(edge.data.target);
      return edge;
    }));
  };

  const onConnect = useCallback(async (connection) => {
    const sourceHandle = JSON.parse(connection.sourceHandle);
    const targetHandle = JSON.parse(connection.targetHandle);

    const relation = {sourceColumnId: sourceHandle.column.id, targetColumnId: targetHandle.column.id, onDeleteId: 1, onUpdateId: 1};
    const newRelation = await addRelationToProject(relation);

    const edge = {...connection,
      id: String(newRelation.id),
      type: 'tableEdge',
      source: String(sourceHandle.table.id),
      target: String(targetHandle.table.id),
      data: {
        source: sourceHandle,
        target: targetHandle, 
        onEdgeDelete: onEdgeDelete,
        onEdgeChange: onEdgeChange,
        sourceColumn: sourceHandle.column.id,
        targetColumn: targetHandle.column.id,
        onDelete: 1,
        onUpdate: 1,
        relationActionCombobox: relationActionCombobox
      }
    };

    setEdges(prevEdges => addEdge(edge, prevEdges));
  }, [])

  function onSave() {
    setIsModalOpen(true);
  }

  return (
    <ReactFlowProvider>
      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} size="large">
          <GenerateCard
            projectId={id}
            dataBaseId={dataBaseId}
            tables={nodes.map(e => e.data)}/>
      </Modal>
      <div className="flex" style={{ height: '95vh' }}>
        <div className="w-1/4 bg-gray-200 p-3 flex flex-col items-center">
          <div className="flex justify-between w-full gap-2">
            <Button onClick={addNode} className="w-1/2" disabled={loading}>
              {loading ? (<Loader/>) : (<i class="fa-solid fa-plus"></i>)}
            </Button>
            <Button onClick={onSave} className="w-1/2" disabled={loading}>
              {loading ? (<Loader/>) : (<i class="fa-solid fa-sliders"></i>)}
            </Button>
          </div>

          <div className="w-full pt-3 overflow-y-auto">
            {nodes.map((node) => (
              <TableCard
                key={node.data.id}
                node={node.data}
                onModelChange={onNodeChange}
                onDelete={onNodeDelete}
              />
            ))}
          </div>
        </div>

        <div className="w-3/4 bg-white">
          {!loading && (
            <ReactFlow
              nodes={nodes}
              edges={edges}
              nodeTypes={nodeTypes}
              edgeTypes={edgeTypes} 
              onNodesChange={onNodesChange}
              onEdgesChange={onEdgesChange}
              onConnect={onConnect}
              fitView>
              <Controls showInteractive={false} />
              <Background color="rgb(59 130 246)" size={1} gap={12} />
            </ReactFlow>
          )}
        </div>
      </div>
    </ReactFlowProvider>
  );
};
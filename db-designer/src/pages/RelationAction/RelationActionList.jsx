import React, { useEffect, useState, useCallback } from 'react';
import { DataGrid, GridDeleteIcon, GridAddIcon } from '@mui/x-data-grid';
import { deleteById, getAll } from '../../utils/apiHelper';
import { useNavigate } from 'react-router-dom';
import { IconButton } from '@mui/material';

export default function RelationActionList() {
  const navigate = useNavigate();
  const [rowData, setRowData] = useState([]);
  const [sortModel, setSortModel] = useState([]);
  const [filterModel, setFilterModel] = useState({ name: '', description: '' });
  const [totalCount, setTotalCount] = useState(0);
  const [pageSize, setPageSize] = useState(100);
  const [currentPage, setCurrentPage] = useState(0);
  const [loading, setLoading] = useState(false);

  const fetchData = useCallback(async () => {
    setLoading(true);
    const model = {
      PageNumber: currentPage + 1,
      PageSize: pageSize,
      ...(sortModel.length > 0 && {
        SortField: {
          Field: sortModel[0].field,
          Direction: sortModel[0].sort,
        },
      }),
      Name: filterModel.name,
      Description: filterModel.description,
    };
    const result = await getAll('RelationAction', model);
    setRowData(result.data);
    setTotalCount(result.totalCount);
    setLoading(false);
  }, [currentPage, pageSize, sortModel, filterModel]);

  const onDeleteHandle = useCallback(
    async (id) => {
      const status = await deleteById('RelationAction', id);
      if (status === 200) fetchData();
    }, [fetchData]
  );

  const columns = [
    {
      field: 'id',
      headerName: 'ID',
      flex: 1,
      filterable: false,
      renderCell: (params) => (
        <a href={`/editRelationAction/${params.value}`} rel="noopener noreferrer">
          {params.value}
        </a>
      ),
    },
    {
      field: 'name',
      headerName: 'Name',
      flex: 5,
    },
    {
      field: 'description',
      headerName: 'Description',
      flex: 10,
    },
    {
      field: 'actions',
      headerName: 'Actions',
      flex: 1,
      sortable: false,
      filterable: false,
      renderCell: (params) => (
        <IconButton onClick={() => onDeleteHandle(params.row.id)}>
          <GridDeleteIcon />
        </IconButton>
      ),
    },
  ];

  useEffect(() => {
    fetchData();
  }, [fetchData, currentPage, pageSize, sortModel, filterModel]);

  return (
    <div style={{ height: '92vh', width: '100%' }}>
      <div className="flex justify-between items-center m-1">  
          <GridAddIcon onClick={() => navigate('/editRelationAction/0')}/>
      </div>
      <DataGrid
        rows={rowData}
        columns={columns}
        loading={loading}
        pageSize={pageSize}
        rowCount={totalCount}
        disableSelectionOnClick
        pagination
        pageSizeOptions={[5, 10, 20, 50, 100]}
        paginationMode="server"
        sortingMode="server"
        filterMode="server"
        onPaginationModelChange={(page) => {setPageSize(page.pageSize); setCurrentPage(page.page);}}
        onSortModelChange={(newSortModel) => setSortModel(newSortModel)}
        onFilterModelChange={(newFilterModel) => {
          const name = newFilterModel.items.find((item) => item.field === 'name')?.value || '';
          const description = newFilterModel.items.find((item) => item.field === 'description')?.value || '';
          setFilterModel({ name, description });
        }}
      />
    </div>
  );
}
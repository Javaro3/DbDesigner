import React, { useEffect, useState, useCallback } from 'react';
import { DataGrid, GridDeleteIcon, GridAddIcon } from '@mui/x-data-grid';
import { deleteById, getAll } from '../../utils/apiHelper';
import { useNavigate } from 'react-router-dom';
import { IconButton } from '@mui/material';
import EntityFilter from '../../components/UI/gridFilters/EntityFilter';
import BooleanFilter from '../../components/UI/gridFilters/BooleanFilter';

export default function PropertyList() {
  const navigate = useNavigate();
  const [rowData, setRowData] = useState([]);
  const [sortModel, setSortModel] = useState([]);
  const [filterModel, setFilterModel] = useState({ name: '', description: '', hasParams: null, dataBases: [] });
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
      DataBases: filterModel.dataBases,
      ...(filterModel.hasParams !== null && { hasParams: filterModel.hasParams })
    };
    const result = await getAll('Property', model);
    setRowData(result.data);
    setTotalCount(result.totalCount);
    setLoading(false);
  }, [currentPage, pageSize, sortModel, filterModel]);

  const onDeleteHandle = useCallback(
    async (id) => {
      const status = await deleteById('Property', id);
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
        <a href={`/editProperty/${params.value}`} rel="noopener noreferrer">
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
      field: 'hasParams',
      headerName: 'Has Params',
      flex: 5,
      filterOperators: [
        {
          label: 'HasParams',
          value: 'HasParams',
          InputComponent: BooleanFilter
        },
      ],
      renderCell: (params) => (
        <>{params.row.hasParams ? "Yes" : "No"}</>
      ),
    },
    {
      field: 'dataBase',
      headerName: 'DataBase',
      flex: 5,
      sortable: false,
      filterOperators: [
        {
          label: 'DataBase',
          value: 'DataBase',
          InputComponent: EntityFilter
        },
      ],
      renderCell: (params) => (
        <>{params.row.dataBase.name}</>
      ),
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
          <GridAddIcon onClick={() => navigate('/editProperty/0')}/>
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
          const dataBases = newFilterModel.items.find((item) => item.field === "dataBase")?.value || [];
          let hasParams = newFilterModel.items.find((item) => item.field === "hasParams")?.value || [];
          hasParams = hasParams.length > 0 ? hasParams[0] : null;
          setFilterModel({ name, description, hasParams, dataBases });
        }}
      />
    </div>
  );
}
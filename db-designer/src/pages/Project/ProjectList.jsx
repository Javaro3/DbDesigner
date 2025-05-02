import React, { useEffect, useState, useCallback } from 'react';
import { DataGrid, GridDeleteIcon, GridAddIcon } from '@mui/x-data-grid';
import { deleteById, getAll } from '../../utils/apiHelper';
import { useNavigate } from 'react-router-dom';
import { IconButton } from '@mui/material';
import { format } from 'date-fns';
import EntityFilter from '../../components/UI/gridFilters/EntityFilter';
import { useAuth } from '../../hooks/AuthContext';
import AccountTreeIcon from '@mui/icons-material/AccountTree';
import { download } from '../../services/projectService';

export default function ProjectList() {
  const navigate = useNavigate();
  const { getUserId } = useAuth();
  const [rowData, setRowData] = useState([]);
  const [sortModel, setSortModel] = useState([]);
  const [filterModel, setFilterModel] = useState({ name: '', description: '', dataBases: [] });
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
      User: getUserId() 
    };
    const result = await getAll('Project', model);
    setRowData(result.data);
    setTotalCount(result.totalCount);
    setLoading(false);
  }, [currentPage, pageSize, sortModel, filterModel]);

  const onDeleteHandle = useCallback(
    async (id) => {
      const status = await deleteById('Project', id);
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
        <a href={`/editProject/${params.value}`} rel="noopener noreferrer">
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
      field: 'createdOn',
      filterable: false,
      headerName: 'Created On',
      renderCell: (params) => (
        <>{format(new Date(params.row.createdOn), 'dd MMM yyyy, HH:mm')}</>
      ),
      flex: 10,
    },
    {
      field: 'dataBase',
      headerName: 'DataBase',
      flex: 10,
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
      headerName: '',
      flex: 4,
      sortable: false,
      filterable: false,
      renderCell: (params) => (
        <div>
          <IconButton onClick={() => navigate(`/editProjectDiagram/${params.row.id}`)}>
            <AccountTreeIcon />
          </IconButton>
          <IconButton onClick={() => download(params.row.id)}>
            <i className="fa-solid fa-download"></i>
          </IconButton>
          <IconButton onClick={() => onDeleteHandle(params.row.id)}>
            <GridDeleteIcon />
          </IconButton>
        </div>
      ),
    },
  ];

  useEffect(() => {
    fetchData();
  }, [fetchData, currentPage, pageSize, sortModel, filterModel]);

  return (
    <div style={{ height: '92vh', width: '100%' }}>
      <div className="flex justify-between items-center m-1">  
          <GridAddIcon onClick={() => navigate('/editProject/0')}/>
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
          setFilterModel({ name, description, dataBases });
        }}
      />
    </div>
  );
}
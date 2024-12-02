import React, { useEffect, useState, useCallback } from 'react';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { useNavigate } from 'react-router-dom';
import { getForCombobox, getAll, deleteById } from '../../utils/apiHelper';
import TextFilter from '../../components/UI/gridFilters/TextFilter';
import MultiSelectFilter from '../../components/UI/gridFilters/MultiboxFilter';
import DateTimeRangeFilter from '../../components/UI/gridFilters/DateTimeRangeFilter';
import Button from '../../components/UI/buttons/Button';

export default function ProjectList() {
  const navigate = useNavigate();
  const [rowData, setRowData] = useState([]);
  const [sortModel, setSortModel] = useState([]);
  const [filterModel, setFilterModel] = useState({ name: '', description: '', createdOnFrom: null, createdOnTo: null, dataBases: [], users: [] });
  const [totalCount, setTotalCount] = useState(0);
  const [pageSize, setPageSize] = useState(20);
  const [currentPage, setCurrentPage] = useState(1);
  const [databaseOptions, setDatabaseOptions] = useState([]);
  const [userOptions, setUserOptions] = useState([]);
  const [columnDefs, setColumnDefs] = useState([]);

  const fetchData = useCallback(async () => {
    const model = {
      PageNumber: currentPage,
      PageSize: pageSize,
      SortField: sortModel,
      Name: filterModel.name,
      Description: filterModel.description,
      DataBases: filterModel.dataBases,
      Users: filterModel.users,
      ...(filterModel.createdOnFrom && { CreatedOnFrom: filterModel.createdOnFrom }),
      ...(filterModel.createdOnTo && { CreatedOnTo: filterModel.createdOnTo })
    };
    const result = await getAll('Project', model);
    setRowData(result.data);
    setTotalCount(result.totalCount);
  }, [pageSize, sortModel, filterModel]);

  const onDeleteHandle = useCallback(async (id) => {
    const status = await deleteById('Project', id);
    if (status == 200)
      fetchData();
  }, [fetchData]);

  const onOpenDiagramHandle = useCallback(async (id) => {
    navigate(`/editProjectDiagram/${id}`);
  }, []);

  const onDownloadHandle = useCallback(async (id) => {
    console.log(`${id} Downloading... `);
  }, []);

  const fetchUserOptions = useCallback(async () => {
    const options = await getForCombobox('User');
    setUserOptions(options);
  }, []);

  const fetchDatabaseOptions = useCallback(async () => {
    const options = await getForCombobox('DataBase');
    setDatabaseOptions(options);
  }, []);

  useEffect(() => {
    setColumnDefs([
      {
        field: 'id', 
        filter: false, 
        flex: 2,
        cellRenderer: (params) => {
          const id = params.value;
          return (
            <a href={`/editProject/${id}`} rel="noopener noreferrer">
              {id}
            </a>
          );
        },
      },
      { 
        field: 'name', 
        filter: TextFilter, 
        flex: 4
      },
      { 
        field: 'description', 
        filter: TextFilter, 
        flex: 8
      },
      {
        field: 'createdOn', 
        filter: DateTimeRangeFilter, 
        flex: 8,
        valueFormatter: (params) => new Date(params.data.createdOn).toLocaleString()
      },
      {
        field: 'dataBases', 
        filter: MultiSelectFilter, 
        filterParams: { options: databaseOptions }, 
        sortable: false, 
        flex: 5,
        valueFormatter: (params) => params.data.dataBase.name
      },
      {
        field: 'users', 
        filter: MultiSelectFilter, 
        filterParams: { options: userOptions }, 
        sortable: false, 
        flex: 8,
        valueFormatter: (params) => params.data.users.map(e => e.name).join(', ')
      },
      {
        headerName: "",
        field: "",
        sortable: false,
        flex: 2,
        cellRenderer: (params) => (
          <Button size='small' className='w-full' onClick={() => onDeleteHandle(params.node.data.id)}>
            <i className="fa-solid fa-trash"></i>
          </Button>
        )
      },
      {
        headerName: "",
        field: "",
        sortable: false,
        flex: 2,
        cellRenderer: (params) => (
          <Button size='small' className='w-full' onClick={() => onOpenDiagramHandle(params.node.data.id)}>
            <i className="fa-solid fa-diagram-project"></i>
          </Button>
        )
      },
      {
        headerName: "",
        field: "",
        sortable: false,
        flex: 2,
        cellRenderer: (params) => (
          <Button size='small' className='w-full' onClick={() => onDownloadHandle(params.node.data.id)}>
            <i className="fa-solid fa-download"></i>
          </Button>
        )
      }
    ]);
  }, [userOptions, databaseOptions]);

  const onSortChanged = useCallback((params) => {
    const sortColumn = params.columns.filter(e => e.sort)[0];
    if (sortColumn) {
      setSortModel([{ field: sortColumn.colId, direction: sortColumn.sort }]);
    } else {
      setSortModel([]);
    }
  }, []);

  const onPaginationChanged = useCallback((params) => {
    const newPage = params.api.paginationGetCurrentPage() + 1;
    if (newPage !== currentPage) {
      setCurrentPage(newPage);
    }
  }, [currentPage]);

  const onFilterChanged = useCallback((params) => {
    const filters = params.api.getFilterModel();
    setFilterModel({ 
      name: filters.name || '', 
      description: filters.description || '',
      createdOnFrom: filters?.createdOn?.fromTime ? filters.createdOn.fromTime : null,
      createdOnTo: filters?.createdOn?.toTime ? filters.createdOn.toTime : null,
      users: filters.users || [],
      dataBases: filters.dataBases || [] 
    });
  }, []);

  useEffect(() => {
    fetchData();
  }, [fetchData, currentPage, sortModel]);

  useEffect(() => {
    fetchUserOptions();
  }, [fetchUserOptions]);

  useEffect(() => {
    fetchDatabaseOptions();
  }, [fetchDatabaseOptions]);

  return (
    <div className="ag-theme-alpine" style={{ height: '90vh', width: '100%' }}>
      <div className="flex justify-between items-center m-1">
        <Button onClick={() => navigate('/editProject/0')} className="px-4 py-2 bg-blue-500 text-white rounded-md">
          <i className="fa-solid fa-plus"></i>
        </Button>
      </div>
      <AgGridReact
        columnDefs={columnDefs}
        rowData={rowData}
        onSortChanged={onSortChanged}
        onPaginationChanged={onPaginationChanged}
        onFilterChanged={onFilterChanged}
        pagination={true}
        paginationPageSize={pageSize}
        paginationPageSizeSelector={[1, 5, 10, 20, 50, 100]}
        onPaginationPageSizeChanged={(newPageSize) => setPageSize(newPageSize)}
      />
    </div>
  );
}
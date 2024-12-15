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

export default function UserList() {
  const navigate = useNavigate();
  const [rowData, setRowData] = useState([]);
  const [sortModel, setSortModel] = useState([]);
  const [filterModel, setFilterModel] = useState({ name: '', email: '', createdOnFrom: null, createdOnTo: null, roles: [] });
  const [totalCount, setTotalCount] = useState(0);
  const [pageSize, setPageSize] = useState(20);
  const [currentPage, setCurrentPage] = useState(1);
  const [roleOptions, setRoleOptions] = useState([]);
  const [columnDefs, setColumnDefs] = useState([]);

  const fetchData = useCallback(async () => {
    const model = {
      PageNumber: currentPage,
      PageSize: pageSize,
      SortField: sortModel,
      Name: filterModel.name,
      Email: filterModel.email,
      Roles: filterModel.roles,
      ...(filterModel.createdOnFrom && { CreatedOnFrom: filterModel.createdOnFrom }),
      ...(filterModel.createdOnTo && { CreatedOnTo: filterModel.createdOnTo })
    };
    const result = await getAll('User', model);
    setRowData(result.data);
    setTotalCount(result.totalCount);
  }, [pageSize, sortModel, filterModel]);

  const onDeleteHandle = useCallback(async (id) => {
    const status = await deleteById('User', id);
    if (status == 200)
      fetchData();
  }, [fetchData]);

  const fetchRoleOptions = useCallback(async () => {
    const options = await getForCombobox('Role');
    setRoleOptions(options);
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
            <a href={`/editUser/${id}`} rel="noopener noreferrer">
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
        field: 'email', 
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
        field: 'roles', 
        filter: MultiSelectFilter, 
        filterParams: { options: roleOptions }, 
        sortable: false, 
        flex: 8,
        valueFormatter: (params) => params.data.roles.map(e => e.name).join(', ')
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
      }
    ]);
  }, [roleOptions]);

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
      email: filters.email || '',
      createdOnFrom: filters?.createdOn?.fromTime ? filters.createdOn.fromTime : null,
      createdOnTo: filters?.createdOn?.toTime ? filters.createdOn.toTime : null,
      roles: filters.roles || [] 
    });
  }, []);

  useEffect(() => {
    fetchData();
  }, [fetchData, currentPage, sortModel]);

  useEffect(() => {
    fetchRoleOptions();
  }, [fetchRoleOptions]);

  return (
    <div className="ag-theme-alpine" style={{ height: '90vh', width: '100%' }}>
      <div className="flex justify-between items-center m-1">
        <Button onClick={() => navigate('/addUser')} className="px-4 py-2 bg-blue-500 text-white rounded-md">
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
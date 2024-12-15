import React, { useEffect, useState, useCallback } from 'react';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import TextFilter from '../../components/UI/gridFilters/TextFilter';
import { API_BASE_URL, getForCombobox, getAll, deleteById } from '../../utils/apiHelper';
import MultiSelectFilter from '../../components/UI/gridFilters/MultiboxFilter';
import Button from '../../components/UI/buttons/Button';
import { useNavigate } from 'react-router-dom';

export default function DataBaseList() {
  const navigate = useNavigate();
  const [rowData, setRowData] = useState([]);
  const [sortModel, setSortModel] = useState([]);
  const [filterModel, setFilterModel] = useState({name: '', description: '', indexTypes: []});
  const [indexTypesOptions, setIndexTypesOptions] = useState([]);
  const [totalCount, setTotalCount] = useState(0);
  const [pageSize, setPageSize] = useState(20);
  const [currentPage, setCurrentPage] = useState(1);
  const [columnDefs, setColumnDefs] = useState([]);

  const fetchData = useCallback(async () => {
    const model = {
      PageNumber: currentPage,
      PageSize: pageSize,
      SortField: sortModel,
      Name: filterModel.name,
      Description: filterModel.description,
      indexTypes: filterModel.indexTypes
    };
    const result = await getAll('DataBase', model);
    setRowData(result.data);
    setTotalCount(result.totalCount);
  }, [pageSize, sortModel, filterModel]);

  const onDeleteHandle = useCallback(async (id) => {
    const status = await deleteById('DataBase', id);
    if (status == 200)
      fetchData();
  }, [fetchData]);

  const fetchIndexTypesOptions = useCallback(async () => {
    const options = await getForCombobox('IndexType');
    setIndexTypesOptions(options);
  }, []);

  useEffect(() => {
    setColumnDefs([
    { 
      field: 'id',
      cellRenderer: (params) => {
        const id = params.value;
        return (
          <a href={`/editDatabase/${id}`} rel="noopener noreferrer">
            {id}
          </a>
        );
      },
      filter: false,
      flex: 1
    },
    { 
      field: 'name', 
      filter: TextFilter, 
      flex: 2 
    },
    { 
      field: 'description',
      filter: TextFilter,
      flex: 8 
    },
    {
      field: 'indexTypes', 
      filter: MultiSelectFilter,
      filterParams: { options: indexTypesOptions },
      sortable: false,
      flex: 5,
      valueFormatter: (params) => params.data.indexTypes.map(e => e.name).join(', ')
    },
    {
      field: 'image',
      filter: false,
      sortable: false,
      flex: 2,
      cellRenderer: (params) => {
        const image = params.value;
        return (
          <img height={64} width={64} src={`${API_BASE_URL}/Image/${image}`} />
        );
      }
    },
    {
      headerName: "",
      field: "",
      sortable: false,
      flex: 1,
      cellRenderer: (params) => (
        <Button size='small' className='w-full' onClick={() => onDeleteHandle(params.node.data.id)}>
          <i className="fa-solid fa-trash"></i>
        </Button>
      )
    }
  ]);
  }, [indexTypesOptions]);

  const onSortChanged = useCallback((params) => {
    const sortColumn = params.columns.filter(e => e.sort)[0];
    if (sortColumn) {
      setSortModel({ field: sortColumn.colId, direction: sortColumn.sort });
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
      indexTypes: filters.indexTypes || [] 
    });
  }, []);

  useEffect(() => {
    fetchData(currentPage);
  }, [fetchData, currentPage, sortModel]);

  useEffect(() => {
    fetchIndexTypesOptions();
  }, [fetchIndexTypesOptions]);

  const getRowHeight = useCallback(() => 70, []);

  return (
    <div className="ag-theme-alpine" style={{ height: '90vh', width: '100%' }}>
      <div className="flex justify-between items-center m-1">
        <Button onClick={() => navigate('/editDatabase/0')} className="px-4 py-2 bg-blue-500 text-white rounded-md">
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
        getRowHeight={getRowHeight}
        paginationPageSize={pageSize}
        paginationPageSizeSelector={[1, 5, 10, 20, 50, 100]}
        onPaginationPageSizeChanged={(newPageSize) => setPageSize(newPageSize)}
      />
    </div>
  );
}
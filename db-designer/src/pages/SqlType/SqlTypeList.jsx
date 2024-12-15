import React, { useEffect, useState, useCallback } from 'react';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { useNavigate } from 'react-router-dom';
import { getForCombobox, getAll, deleteById } from '../../utils/apiHelper';
import TextFilter from '../../components/UI/gridFilters/TextFilter';
import MultiSelectFilter from '../../components/UI/gridFilters/MultiboxFilter';
import Button from '../../components/UI/buttons/Button';
import ComboBoxFilter from '../../components/UI/gridFilters/ComboBoxFilter';

export default function SqlTypeList() {
  const navigate = useNavigate();
  const [rowData, setRowData] = useState([]);
  const [sortModel, setSortModel] = useState([]);
  const [filterModel, setFilterModel] = useState({ name: '', description: '', hasParams: null, databases: [], languageTypes: [] });
  const [totalCount, setTotalCount] = useState(0);
  const [pageSize, setPageSize] = useState(20);
  const [currentPage, setCurrentPage] = useState(1);
  const [databaseOptions, setDatabaseOptions] = useState([]);
  const [languageTypeOptions, setLanguageTypeOptions] = useState([]);
  const [columnDefs, setColumnDefs] = useState([]);

  const fetchData = useCallback(async () => {
    const model = {
      PageNumber: currentPage,
      PageSize: pageSize,
      SortField: sortModel,
      Name: filterModel.name,
      Description: filterModel.description,
      Databases: filterModel.databases,
      LanguageTypes: filterModel.languageTypes,
      ...(filterModel.hasParams !== null && { hasParams: filterModel.hasParams })
    };
    const result = await getAll('SqlType', model);
    setRowData(result.data);
    setTotalCount(result.totalCount);
  }, [pageSize, sortModel, filterModel]);

  const onDeleteHandle = useCallback(async (id) => {
    const status = await deleteById('SqlType', id);
    if (status == 200)
      fetchData();
  }, [fetchData]);

  const fetchDatabaseOptions = useCallback(async () => {
    const options = await getForCombobox('DataBase');
    setDatabaseOptions(options);
  }, []);

  const fetchLanguageTypeOptions = useCallback(async () => {
    const options = await getForCombobox('LanguageType');
    setLanguageTypeOptions(options);
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
            <a href={`/editSqlType/${id}`} rel="noopener noreferrer">
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
        field: 'hasParams', 
        filter: ComboBoxFilter, 
        filterParams: { options: [{id: 0, name: 'All'}, {id: 1, name: 'Yes'}, {id: 2, name: 'No'}] }, 
        sortable: false, 
        flex: 4,
        valueFormatter: (params) => params.data.hasParams ? 'Yes' : 'No'
      },
      {
        field: 'databases', 
        filter: MultiSelectFilter, 
        filterParams: { options: databaseOptions }, 
        sortable: false, 
        flex: 8,
        valueFormatter: (params) => params.data.dataBases.map(e => e.name).join(', ')
      },
      {
        field: 'languageTypes', 
        filter: MultiSelectFilter, 
        filterParams: { options: languageTypeOptions }, 
        sortable: false, 
        flex: 8,
        valueFormatter: (params) => params.data.languageTypes.map(e => e.name).join(', ')
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
  }, [databaseOptions, languageTypeOptions]);

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
      hasParams: filters.hasParams == 0
      ? null
      : filters.hasParams == 1,
      databases: filters.databases || [],
      languageTypes: filters.languageTypes || [],
    });
  }, []);

  useEffect(() => {
    fetchData();
  }, [fetchData, currentPage, sortModel]);

  useEffect(() => {
    fetchDatabaseOptions();
  }, [fetchDatabaseOptions]);

  useEffect(() => {
    fetchLanguageTypeOptions();
  }, [fetchLanguageTypeOptions]);

  return (
    <div className="ag-theme-alpine" style={{ height: '90vh', width: '100%' }}>
      <div className="flex justify-between items-center m-1">
        <Button onClick={() => navigate('/editSqlType/0')} className="px-4 py-2 bg-blue-500 text-white rounded-md">
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
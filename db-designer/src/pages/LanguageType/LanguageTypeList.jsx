import React, { useEffect, useState, useCallback } from 'react';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { useNavigate } from 'react-router-dom';
import { getForCombobox, getAll, deleteById } from '../../utils/apiHelper';
import TextFilter from '../../components/UI/gridFilters/TextFilter';
import MultiSelectFilter from '../../components/UI/gridFilters/MultiboxFilter';
import Button from '../../components/UI/buttons/Button';

export default function LanguageTypeList() {
  const navigate = useNavigate();
  const [rowData, setRowData] = useState([]);
  const [sortModel, setSortModel] = useState({});
  const [filterModel, setFilterModel] = useState({ name: '', description: '', languages: [], sqlTypes: [] });
  const [totalCount, setTotalCount] = useState(0);
  const [pageSize, setPageSize] = useState(20);
  const [currentPage, setCurrentPage] = useState(1);
  const [languageOptions, setLanguageOptions] = useState([]);
  const [sqlTypeOptions, setSqlTypeOptions] = useState([]);
  const [columnDefs, setColumnDefs] = useState([]);

  const fetchData = useCallback(async () => {
    const model = {
      PageNumber: currentPage,
      PageSize: pageSize,
      SortField: sortModel,
      Name: filterModel.name,
      Description: filterModel.description,
      Languages: filterModel.languages,
      SqlTypes: filterModel.sqlTypes,
    };
    const result = await getAll('LanguageType', model);
    setRowData(result.data);
    setTotalCount(result.totalCount);
  }, [pageSize, sortModel, filterModel]);

  const onDeleteHandle = useCallback(async (id) => {
    const status = await deleteById('LanguageType', id);
    if (status == 200)
      fetchData();
  }, [fetchData]);

  const fetchLanguageOptions = useCallback(async () => {
    const options = await getForCombobox('Language');
    setLanguageOptions(options);
  }, []);

  const fetchSqlTypeOptions = useCallback(async () => {
    const options = await getForCombobox('SqlType');
    setSqlTypeOptions(options);
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
            <a href={`/editLanguageType/${id}`} rel="noopener noreferrer">
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
        field: 'language', 
        filter: MultiSelectFilter, 
        filterParams: { options: languageOptions }, 
        sortable: false, 
        flex: 8,
        valueFormatter: (params) => params.data.language.name
      },
      {
        field: 'sqlTypes', 
        filter: MultiSelectFilter, 
        filterParams: { options: sqlTypeOptions }, 
        sortable: false, 
        flex: 8,
        valueFormatter: (params) => params.data.sqlTypes.map(e => e.name).join(', ')
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
  }, [languageOptions, sqlTypeOptions]);

  const onSortChanged = useCallback((params) => {
    const sortColumn = params.columns.filter(e => e.sort)[0];
    if (sortColumn) {
      setSortModel({ field: sortColumn.colId, direction: sortColumn.sort });
    } else {
      setSortModel({});
    }
  }, []);

  const onPaginationChanged = useCallback((params) => {
    const newPage = params.api.paginationGetCurrentPage() + 1;
    const newPageSize = params.api.paginationGetPageSize();
    if (newPage !== currentPage) {
      setCurrentPage(newPage);
    }
    if(pageSize !== newPageSize){
      setPageSize(newPageSize);
    }
  }, [currentPage]);

  const onFilterChanged = useCallback((params) => {
    const filters = params.api.getFilterModel();
    setFilterModel({ 
      name: filters.name || '', 
      description: filters.description || '',
      languages: filters.language || [],
      sqlTypes: filters.sqlType || [],
    });
  }, []);

  useEffect(() => {
    fetchData();
  }, [fetchData, currentPage, sortModel]);

  useEffect(() => {
    fetchLanguageOptions();
  }, [fetchLanguageOptions]);

  useEffect(() => {
    fetchSqlTypeOptions();
  }, [fetchSqlTypeOptions]);

  return (
    <div className="ag-theme-alpine" style={{ height: '90vh', width: '100%' }}>
      <div className="flex justify-between items-center m-1">
        <Button onClick={() => navigate('/editLanguageType/0')} className="px-4 py-2 bg-blue-500 text-white rounded-md">
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
      />
    </div>
  );
}
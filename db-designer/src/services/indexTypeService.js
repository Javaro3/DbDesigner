import { API_BASE_URL, toQueryString } from "../utils/apiHelper";

export const getIndexTypeForComboboxByDatabase = async (dataBaseId) => {
  const queryString = toQueryString({dataBaseId});

  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/IndexType/get-combobox-by-database?${queryString}`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    }
    });
  return response.json();
};
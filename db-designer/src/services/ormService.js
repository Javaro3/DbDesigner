import { API_BASE_URL, toQueryString } from "../utils/apiHelper";

export const getOrmForComboboxByLanguage = async (languageId) => {
  const queryString = toQueryString({languageId});

  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/Orm/get-combobox-by-language?${queryString}`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    }
    });
  return response.json();
};
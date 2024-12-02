import { API_BASE_URL } from "../utils/apiHelper";

export const addColumnToTable = async (column, tableId) => {
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/Column/add-to-table`, {
    method: 'POST',
    headers: { 
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify({column, tableId}),
  });
  if (!response.ok) {
    const errorData = await response.json();
    const errorMessage = errorData.message;
    throw new Error(errorMessage);
  }
  return response.json();
};
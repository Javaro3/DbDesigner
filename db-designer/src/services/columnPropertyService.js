import { API_BASE_URL } from "../utils/apiHelper";

export const addPropertyToColumn = async (columnProperty, columnId) => {
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/ColumnProperty/add-to-column`, {
    method: 'POST',
    headers: { 
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify({columnProperty, columnId}),
  });
  if (!response.ok) {
    const errorData = await response.json();
    const errorMessage = errorData.message;
    throw new Error(errorMessage);
  }
};

export const deletePropertyFromColumn = async (columnProperty) => {
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/ColumnProperty/delete`, {
    method: 'DELETE',
    headers: { 
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify(columnProperty),
  });
  if (!response.ok) {
    const errorData = await response.json();
    const errorMessage = errorData.message;
    throw new Error(errorMessage);
  }
};
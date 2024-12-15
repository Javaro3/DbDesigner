import { API_BASE_URL } from "../utils/apiHelper";

export const addIndexToTable = async (index) => {
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/Index/add-to-table`, {
    method: 'POST',
    headers: { 
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify(index),
  });
  if (!response.ok) {
    const errorData = await response.json();
    const errorMessage = errorData.message;
    throw new Error(errorMessage);
  }
  return response.json();
};
import { API_BASE_URL } from "../utils/apiHelper";

export const addTableToProject = async (table, projectId) => {
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/Table/add-to-project`, {
    method: 'POST',
    headers: { 
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify({table, projectId}),
  });
  if (!response.ok) {
    const errorData = await response.json();
    const errorMessage = errorData.message;
    throw new Error(errorMessage);
  }
  return response.json();
};
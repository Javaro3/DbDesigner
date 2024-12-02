import { API_BASE_URL } from "../utils/apiHelper";

export const addRelationToProject = async (relation) => {
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/Relation/add-to-project`, {
    method: 'POST',
    headers: { 
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify(relation),
  });
  if (!response.ok) {
    const errorData = await response.json();
    const errorMessage = errorData.message;
    throw new Error(errorMessage);
  }
  return response.json();
};
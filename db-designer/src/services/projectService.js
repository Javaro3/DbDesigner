import { API_BASE_URL, saveFile, toQueryString } from "../utils/apiHelper";

export const getForDiagram = async (id) => {
  const queryString = toQueryString({id});

  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/Project/get-for-diagram?${queryString}`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    }
    });
  return response.json();
};

export const generate = async (generateModel, fileName) => {
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/Project/generate`, {
    method: 'POST',
    headers: { 
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`,
    },
    body: JSON.stringify(generateModel),
  });

  if (!response.ok) {
    const errorData = await response.json();
    const errorMessage = errorData.message || 'Failed to generate file';
    throw new Error(errorMessage);
  }

  saveFile(response, fileName);
};

export const download = async (projectId, fileName) => {
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/Project/download/${projectId}`, {
    method: 'GET',
    headers: { 
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`,
    }
  });

  if (!response.ok) {
    const errorData = await response.json();
    const errorMessage = errorData.message || 'Failed to generate file';
    throw new Error(errorMessage);
  }

  saveFile(response, fileName);
};
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

export const generateScript = async (projectId) => {
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/Project/generate-script/${projectId}`, {
    method: 'GET',
    headers: { 
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`,
    }
  });
  return response.json();
};

export const scriptIsValid = async (model) => {
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/Project/script-is-valid`, {
    method: 'POST',
    headers: { 
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify(model)
  });
  return response.json();
};

export const downloadScript = async (projectId) => {
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/Project/download-script/${projectId}`, {
    method: 'GET',
    headers: { 
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`,
    }
  });

  if (!response.ok) {
    const errorData = await response.json();
    const errorMessage = errorData.message || 'Failed to download file';
    throw new Error(errorMessage);
  }

  saveFile(response, 'script.sql');
};

export const generateDalAndTestData = async (model) => {
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/Project/generate-dal`, {
    method: 'POST',
    headers: { 
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify(model)
  });
  return response.json();
};

export const download = async (projectId, fileName = 'result') => {
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
    const errorMessage = errorData.message || 'Failed to download file';
    throw new Error(errorMessage);
  }

  saveFile(response, fileName);
};
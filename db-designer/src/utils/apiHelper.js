export const API_BASE_URL = 'https://localhost:7058';

export function toQueryString(params) {
  return Object.entries(params)
    .flatMap(([key, value]) => {
      if (Array.isArray(value)) {
        return value.map(subValue => `${encodeURIComponent(key)}=${encodeURIComponent(subValue)}`);
      }
      if (typeof value === 'object' && value !== null) {
        return Object.entries(value)
          .map(([subKey, subValue]) => `${encodeURIComponent(key + '.' + subKey)}=${encodeURIComponent(subValue)}`);
      }
      return `${encodeURIComponent(key)}=${encodeURIComponent(value)}`;
    })
    .join('&');
}

export async function saveFile(response, fileName) {
  const blob = await response.blob();
  const contentDisposition = response.headers.get('Content-Disposition');
    if (contentDisposition) {
      const match = contentDisposition.match(/filename="(.+)"/);
      if (match && match[1]) {
        fileName = match[1];
      }
    }

    const url = URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = url;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    link.remove();
    URL.revokeObjectURL(url);
}

export const getForCombobox = async (controller) => {
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/${controller}/get-combobox`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    }
    });
  return response.json();
};

export const get = async (controller, id) => {
  const queryString = toQueryString({id});
  
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/${controller}/get?${queryString}`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    }
    });
  return response.json();
};

export const update = async (controller, model) => {
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/${controller}/update`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify(model)
    });
  return response.status;
};

export const getAll = async (controller, model) => {
  const queryString = toQueryString(model);
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/${controller}/get-all?${queryString}`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    }
    });
  return response.json();
};

export const deleteById = async (controller, id) => {
  const queryString = toQueryString({id: id});
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/${controller}/delete?${queryString}`, {
    method: 'DELETE',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    }
    });
  return response.status;
};
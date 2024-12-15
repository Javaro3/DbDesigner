import { API_BASE_URL } from "../utils/apiHelper";

export const create = async (model) => {
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/User/add`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify(model)
    });
  return response.status;
};

export const getForComboboxWithoutCurrent = async () => {
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/User/get-combobox-without-current`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    }
    });
  return response.json();
};
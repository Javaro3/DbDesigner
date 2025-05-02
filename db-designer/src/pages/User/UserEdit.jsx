import React, { useEffect, useState, useCallback } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Loader from '../../components/UI/loaders/Loader';
import Input from '../../components/UI/inputs/Input';
import Button from '../../components/UI/buttons/Button';
import MultiComboBox from '../../components/UI/inputs/MultiComboBox';
import { get, getForCombobox, update } from '../../utils/apiHelper';
import { validateEmail, validateEmpty } from '../../utils/validators';

export default function UserEdit() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [user, setUser] = useState({ name: '', email: '', roles: [] });
  const [loading, setLoading] = useState(true);
  const [nameError, setNameError] = useState('');
  const [emailError, setEmailError] = useState('');
  const [rolesError, setRolesError] = useState('');
  const [roleOptions, setRoleOptions] = useState([]);

  const fetchUser = useCallback(async () => {
    try {
      const result = await get('User', id);
      setUser(result);
      setLoading(false);
    } catch (error) {
      setLoading(false);
    }
  }, [id]);

  const fetchRole = useCallback(async () => {
    try {
      const result = await getForCombobox('Role');
      setRoleOptions(result);
    } catch (error) {
      console.error("Error fetching index types:", error);
    }
  }, []);

  useEffect(() => {
    fetchUser();
    fetchRole();
  }, [fetchUser, fetchRole]);

  const handleNameChange = (e) => {
    const newName = e.target.value;
    const validationError = validateEmpty(newName, 'Name');
    setNameError(validationError);
    setUser((prev) => ({
      ...prev,
      name: newName,
    }));
  };

  const handleEmailChange = (e) => {
    const newEmail = e.target.value;
    const validationError = validateEmail(newEmail);
    setEmailError(validationError);
    setUser((prev) => ({
      ...prev,
      email: newEmail,
    }));
  };

  const handleRoleChange = (selectedOptions) => {
    const newRoles = roleOptions.filter(e => selectedOptions.includes(e.id));
    const validationError = validateEmpty(newRoles.length !== 0, 'Roles');
    setRolesError(validationError);
    setUser((prev) => ({
      ...prev,
      roles: newRoles,
    }));
  };

  const handleSave = async () => {
    try {
      if (nameError || emailError || rolesError)
        return;

      await update('User', user);
      navigate('/users');
    } catch (error) {
      console.error("Error updating language:", error);
    }
  };

  return (
    <div className="max-w-lg mx-auto p-6 bg-white rounded-lg shadow-md mt-10">
      <h2 className="text-2xl font-semibold text-gray-700 mb-6">Edit User</h2>
      {loading ? (
        <Loader />
      ) : (
        <form className="space-y-4">
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Name:</label>
            <Input
              value={user.name}
              onChange={handleNameChange}
              error={nameError}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Email:</label>
            <Input
              value={user.email}
              onChange={handleEmailChange}
              error={emailError}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Roles:</label>
            <MultiComboBox
              options={roleOptions}
              selected={user.roles.map(e => e.id)}
              onChange={handleRoleChange}
              error={rolesError}
            />
          </div>
          <div className="flex justify-end space-x-4 mt-6">
            <Button onClick={handleSave}>
              <i className="fa-solid fa-floppy-disk"></i>
            </Button>
            <Button variant="secondary" onClick={() => navigate('/users')}>
              <i className="fa-solid fa-xmark"></i>
            </Button>
          </div>
        </form>
      )}
    </div>
  );
}
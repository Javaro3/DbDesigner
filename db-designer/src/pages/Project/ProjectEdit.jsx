import React, { useEffect, useState, useCallback } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Loader from '../../components/UI/loaders/Loader';
import Input from '../../components/UI/inputs/Input';
import Button from '../../components/UI/buttons/Button';
import { get, getForCombobox, update } from '../../utils/apiHelper';
import ComboBox from '../../components/UI/inputs/ComboBox';
import MultiComboBox from '../../components/UI/inputs/MultiComboBox';
import { validateEmpty } from '../../utils/validators';
import { getForComboboxWithoutCurrent } from '../../services/userService';

export default function ProjectEdit() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [project, setProject] = useState({ name: '', description: '', dataBase: null, users: [] });
  const [loading, setLoading] = useState(true);
  const [nameError, setNameError] = useState('');
  const [databaseError, setDatabaseError] = useState('');
  const [databaseOptions, setDatabaseOptions] = useState(0);
  const [usersOptions, setUsersOptions] = useState([]);

  const fetchProject = useCallback(async () => {
    try {
      const result = await get('Project', id);
      setProject(result);
      setLoading(false);
    } catch (error) {
      setLoading(false);
    }
  }, [id]);

  const fetchDatabase = useCallback(async () => {
    try {
      const result = await getForCombobox('DataBase');
      setDatabaseOptions(result);
    } catch (error) {
      console.error("Error fetching index types:", error);
    }
  }, []);

  const fetchUser = useCallback(async () => {
    try {
      const result = await getForComboboxWithoutCurrent();
      setUsersOptions(result);
    } catch (error) {
      console.error("Error fetching index types:", error);
    }
  }, []);

  useEffect(() => {
    fetchProject();
    fetchDatabase();
    fetchUser();
  }, [fetchProject, fetchDatabase, fetchUser]);

  const handleNameChange = (e) => {
    const newName = e.target.value;
    const validationError = validateEmpty(newName, 'Name');
    setNameError(validationError);
    setProject((prev) => ({
      ...prev,
      name: newName,
    }));
  };

  const handleDescriptionChange = (e) => {
    setProject((prev) => ({
      ...prev,
      description: e.target.value,
    }));
  };

  const handleDataBaseChange = (selectedOptions) => {
    const newDatabase = { id: selectedOptions };
    const validationError = validateEmpty(newDatabase?.id, 'Database');
    setDatabaseError(validationError);
    setProject((prev) => ({
      ...prev,
      dataBase: newDatabase,
    }));
  };

  const handleUserChange = (selectedOptions) => {
    const newUsers = usersOptions.filter(e => selectedOptions.includes(e.id));
    setProject((prev) => ({
      ...prev,
      users: newUsers,
    }));
  };

  const handleSave = async () => {
    try {
      if (nameError || databaseError)
        return;

      await update('Project', project);
      navigate('/home');
    } catch (error) {
      console.error("Error updating property:", error);
    }
  };

  return (
    <div className="max-w-lg mx-auto p-6 bg-white rounded-lg shadow-md mt-10">
      <h2 className="text-2xl font-semibold text-gray-700 mb-6">{id == 0 ? 'Add' : 'Edit'} Project</h2>
      {loading ? (
        <Loader />
      ) : (
        <form className="space-y-4">
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Name:</label>
            <Input
              value={project.name}
              onChange={handleNameChange}
              error={nameError}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Description:</label>
            <Input
              value={project.description}
              onChange={handleDescriptionChange}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Database:</label>
            <ComboBox
              options={databaseOptions}
              selected={project.dataBase?.id}
              onChange={handleDataBaseChange}
              placeholder="Select value"
              error={databaseError}
            />
          </div>
          {id != 0
          ? (
            <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Users:</label>
            <MultiComboBox
              options={usersOptions}
              selected={project?.users?.map(e => e.id)}
              onChange={handleUserChange}
            />
          </div>
          )
          : (<></>)}
          <div className="flex justify-end space-x-4 mt-6">
            <Button onClick={handleSave}>
              <i className="fa-solid fa-floppy-disk"></i>
            </Button>
            <Button variant="secondary" onClick={() => navigate('/home')}>
              <i className="fa-solid fa-xmark"></i>
            </Button>
          </div>
        </form>
      )}
    </div>
  );
}
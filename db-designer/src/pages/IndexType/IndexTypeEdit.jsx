import React, { useEffect, useState, useCallback } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Loader from '../../components/UI/loaders/Loader';
import Input from '../../components/UI/inputs/Input';
import Button from '../../components/UI/buttons/Button';
import MultiComboBox from '../../components/UI/inputs/MultiComboBox';
import { get, getForCombobox, update } from '../../utils/apiHelper';
import { validateEmpty } from '../../utils/validators';

export default function IndexTypeEdit() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [indexType, setIndexType] = useState({ name: '', description: '', dataBases: [], image: null });
  const [nameError, setNameError] = useState('');
  const [databasesError, setDatabasesError] = useState('');
  const [loading, setLoading] = useState(true);
  const [databaseOptions, setDatabaseOptions] = useState([]);

  const fetchDatabases = useCallback(async () => {
    try {
      const result = await get('IndexType', id);
      setIndexType(result);
      setLoading(false);
    } catch (error) {
      setLoading(false);
    }
  }, [id]);

  const fetchIndexType = useCallback(async () => {
    try {
      const result = await getForCombobox('DataBase');
      setDatabaseOptions(result);
    } catch (error) {
      console.error("Error fetching index types:", error);
    }
  }, []);

  useEffect(() => {
    fetchIndexType();
    fetchDatabases();
  }, [fetchIndexType, fetchDatabases]);

  const handleNameChange = (e) => {
    const newName = e.target.value;
    const validationError = validateEmpty(newName, 'Name');
    setNameError(validationError);
    setIndexType((prev) => ({
      ...prev,
      name: e.target.value,
    }));
  };

  const handleDescriptionChange = (e) => {
    setIndexType((prev) => ({
      ...prev,
      description: e.target.value,
    }));
  };

  const handleDatabasesChange = (selectedOptions) => {
    const newDatabases = databaseOptions.filter(e => selectedOptions.includes(e.id));
    const validationError = validateEmpty(newDatabases.length !== 0, 'Databases');
    setDatabasesError(validationError);
    setIndexType((prev) => ({
      ...prev,
      dataBases: newDatabases,
    }));
  };

  const handleSave = async () => {
    try {
      if (nameError || databasesError)
        return;

      await update('IndexType', indexType);
      navigate('/indexTypes');
    } catch (error) {
      console.error("Error updating database:", error);
    }
  };

  return (
    <div className="max-w-lg mx-auto p-6 bg-white rounded-lg shadow-md mt-10">
      <h2 className="text-2xl font-semibold text-gray-700 mb-6">Edit IndexType</h2>
      {loading ? (
        <Loader />
      ) : (
        <form className="space-y-4">
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Name:</label>
            <Input
              value={indexType.name}
              onChange={handleNameChange}
              error={nameError}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Description:</label>
            <Input
              value={indexType.description}
              onChange={handleDescriptionChange}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Databases:</label>
            <MultiComboBox
              options={databaseOptions}
              selected={indexType.dataBases.map(e => e.id)}
              onChange={handleDatabasesChange}
              error={databasesError}
            />
          </div>
          <div className="flex justify-end space-x-4 mt-6">
            <Button onClick={handleSave}>
              <i className="fa-solid fa-floppy-disk"></i>
            </Button>
            <Button variant="secondary" onClick={() => navigate('/indexTypes')}>
              <i className="fa-solid fa-xmark"></i>
            </Button>
          </div>
        </form>
      )}
    </div>
  );
}
import React, { useEffect, useState, useCallback } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Loader from '../../components/UI/loaders/Loader';
import Input from '../../components/UI/inputs/Input';
import Button from '../../components/UI/buttons/Button';
import MultiComboBox from '../../components/UI/inputs/MultiComboBox';
import { get, getForCombobox, update } from '../../utils/apiHelper';
import { validateEmpty } from '../../utils/validators';

export default function DataBaseEdit() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [database, setDatabase] = useState({ name: '', description: '', indexTypes: [], image: null });
  const [nameError, setNameError] = useState('');
  const [indexTypesError, setIndexTypesError] = useState('');
  const [loading, setLoading] = useState(true);
  const [indexTypeOptions, setIndexTypeOptions] = useState([]);

  const fetchDatabase = useCallback(async () => {
    try {
      const result = await get('DataBase', id);
      setDatabase(result);
      setLoading(false);
    } catch (error) {
      setLoading(false);
    }
  }, [id]);

  const fetchIndexTypes = useCallback(async () => {
    try {
      const result = await getForCombobox('IndexType');
      setIndexTypeOptions(result);
    } catch (error) {
      console.error("Error fetching index types:", error);
    }
  }, []);

  useEffect(() => {
    fetchDatabase();
    fetchIndexTypes();
  }, [fetchDatabase, fetchIndexTypes]);

  const handleNameChange = (e) => {
    const newName = e.target.value;
    const validationError = validateEmpty(newName, 'Name');
    setNameError(validationError);
    setDatabase((prev) => ({
      ...prev,
      name: e.target.value,
    }));
  };

  const handleDescriptionChange = (e) => {
    setDatabase((prev) => ({
      ...prev,
      description: e.target.value,
    }));
  };

  const handleIndexTypesChange = (selectedOptions) => {
    const newIndexTypes = indexTypeOptions.filter(e => selectedOptions.includes(e.id));
    const validationError = validateEmpty(newIndexTypes.length !== 0, 'Index Types');
    setIndexTypesError(validationError);
    setDatabase((prev) => ({
      ...prev,
      indexTypes: newIndexTypes,
    }));
  };

  const handleSave = async () => {
    try {
      if (nameError || indexTypesError)
        return;

      await update('DataBase', database);
      navigate('/databases');
    } catch (error) {
      console.error("Error updating database:", error);
    }
  };

  return (
    <div className="max-w-lg mx-auto p-6 bg-white rounded-lg shadow-md mt-10">
      <h2 className="text-2xl font-semibold text-gray-700 mb-6">Edit Database</h2>
      {loading ? (
        <Loader />
      ) : (
        <form className="space-y-4">
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Name:</label>
            <Input
              value={database.name}
              onChange={handleNameChange}
              error={nameError}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Description:</label>
            <Input
              value={database.description}
              onChange={handleDescriptionChange}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Index Types:</label>
            <MultiComboBox
              options={indexTypeOptions}
              selected={database.indexTypes.map(e => e.id)}
              onChange={handleIndexTypesChange}
              error={indexTypesError}
            />
          </div>
          <div className="flex justify-end space-x-4 mt-6">
            <Button onClick={handleSave}>
              <i className="fa-solid fa-floppy-disk"></i>
            </Button>
            <Button variant="secondary" onClick={() => navigate('/databases')}>
              <i className="fa-solid fa-xmark"></i>
            </Button>
          </div>
        </form>
      )}
    </div>
  );
}
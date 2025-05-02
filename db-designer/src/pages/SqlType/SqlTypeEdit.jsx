import React, { useEffect, useState, useCallback } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Loader from '../../components/UI/loaders/Loader';
import Input from '../../components/UI/inputs/Input';
import Button from '../../components/UI/buttons/Button';
import { get, getForCombobox, update } from '../../utils/apiHelper';
import ComboBox from '../../components/UI/inputs/ComboBox';
import { validateEmpty } from '../../utils/validators';

export default function SqlTypeEdit() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [sqlType, setSqlType] = useState({ name: '', description: '', hasParams: false, dataBase: null });
  const [loading, setLoading] = useState(true);
  const [nameError, setNameError] = useState('');
  const [dataBaseOptions, setDataBaseOptions] = useState([]);
  const [dataBaseError, setDataBaseError] = useState('');

  const fetchSqlType = useCallback(async () => {
    try {
      const result = await get('SqlType', id);
      setSqlType(result);
      setLoading(false);
    } catch (error) {
      setLoading(false);
    }
  }, [id]);

  const fetchDataBase = useCallback(async () => {
      try {
        const result = await getForCombobox('DataBase');
        setDataBaseOptions(result);
      } catch (error) {
        console.error("Error fetching index types:", error);
      }
    }, []);

  useEffect(() => {
    fetchSqlType();
    fetchDataBase();
  }, [fetchSqlType, fetchDataBase]);

  const handleNameChange = (e) => {
    const newName = e.target.value;
    const validationError = validateEmpty(newName, 'Name');
    setNameError(validationError);
    setSqlType((prev) => ({
      ...prev,
      name: newName,
    }));
  };

  const handleDescriptionChange = (e) => {
    setSqlType((prev) => ({
      ...prev,
      description: e.target.value,
    }));
  };

  const handleHasParamsChange = (e) => {
    setSqlType((prev) => ({
      ...prev,
      hasParams: e == 2,
    }));
  };

  const handleDataBaseChange = (selectedOptions) => {
      const validationError = validateEmpty(selectedOptions, 'DataBase');
      setDataBaseError(validationError);
      setSqlType((prev) => ({
        ...prev,
        dataBase: {id: selectedOptions},
      }));
    };

  const handleSave = async () => {
    try {
      if (nameError)
        return;

      await update('SqlType', sqlType);
      navigate('/sqlTypes');
    } catch (error) {
      console.error("Error updating sqlType:", error);
    }
  };

  return (
    <div className="max-w-lg mx-auto p-6 bg-white rounded-lg shadow-md mt-10">
      <h2 className="text-2xl font-semibold text-gray-700 mb-6">{id != 0? 'Edit Sql Type' : 'Add Sql Type'}</h2>
      {loading ? (
        <Loader />
      ) : (
        <form className="space-y-4">
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Name:</label>
            <Input
              value={sqlType.name}
              onChange={handleNameChange}
              error={nameError}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Description:</label>
            <Input
              value={sqlType.description}
              onChange={handleDescriptionChange}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Has Params:</label>
            <ComboBox
              options={[{id: 2, name: 'Yes'}, {id: 1, name: 'No'}]}
              selected={sqlType.hasParams ? 2 : 1}
              onChange={handleHasParamsChange}
              placeholder="Select value"
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">DataBase:</label>
            <ComboBox
              options={dataBaseOptions}
              selected={sqlType.dataBase?.id}
              onChange={handleDataBaseChange}
              error={dataBaseError}
            />
          </div>
          <div className="flex justify-end space-x-4 mt-6">
            <Button onClick={handleSave}>
              <i className="fa-solid fa-floppy-disk"></i>
            </Button>
            <Button variant="secondary" onClick={() => navigate('/sqlTypes')}>
              <i className="fa-solid fa-xmark"></i>
            </Button>
          </div>
        </form>
      )}
    </div>
  );
}
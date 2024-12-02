import React, { useEffect, useState, useCallback } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Loader from '../../components/UI/loaders/Loader';
import Input from '../../components/UI/inputs/Input';
import Button from '../../components/UI/buttons/Button';
import { get, getForCombobox, update } from '../../utils/apiHelper';
import ComboBox from '../../components/UI/inputs/ComboBox';
import { validateEmpty } from '../../utils/validators';
import MultiComboBox from '../../components/UI/inputs/MultiComboBox';

export default function LanguageTypeEdit() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [languageType, setLanguageType] = useState({ name: '', description: '', language: null, sqlTypes: [] });
  const [loading, setLoading] = useState(true);
  const [nameError, setNameError] = useState('');
  const [languageError, setLanguageError] = useState('');
  const [sqlTypeError, setSqlTypeError] = useState('');
  const [languageOptions, setLanguageOptions] = useState([]);
  const [sqlTypeOptions, setSqlTypeOptions] = useState([]);

  const fetchLanguageType = useCallback(async () => {
    try {
      const result = await get('LanguageType', id);
      setLanguageType(result);
      setLoading(false);
    } catch (error) {
      setLoading(false);
    }
  }, [id]);

  const fetchLanguage = useCallback(async () => {
    try {
      const result = await getForCombobox('Language');
      setLanguageOptions(result);
    } catch (error) {
      console.error("Error fetching index types:", error);
    }
  }, []);

  const fetchSqlType = useCallback(async () => {
    try {
      const result = await getForCombobox('SqlType');
      setSqlTypeOptions(result);
    } catch (error) {
      console.error("Error fetching index types:", error);
    }
  }, []);

  useEffect(() => {
    fetchLanguageType();
    fetchLanguage();
    fetchSqlType();
  }, [fetchLanguageType, fetchLanguage, fetchSqlType]);

  const handleNameChange = (e) => {
    const newName = e.target.value;
    const validationError = validateEmpty(newName, 'Name');
    setNameError(validationError);
    setLanguageType((prev) => ({
      ...prev,
      name: e.target.value,
    }));
  };

  const handleDescriptionChange = (e) => {
    setLanguageType((prev) => ({
      ...prev,
      description: e.target.value,
    }));
  };

  const handleLanguageChange = (selectedOptions) => {
    const newLanguage = { id: selectedOptions };
    const validationError = validateEmpty(newLanguage?.id, 'Language');
    setLanguageError(validationError);
    setLanguageType((prev) => ({
      ...prev,
      language: newLanguage,
    }));
  };

  const handleSqlTypeChange = (selectedOptions) => {
    const newSqlTypes = sqlTypeOptions.filter(e => selectedOptions.includes(e.id));
    const validationError = validateEmpty(newSqlTypes?.length !== 0, 'Sql Types');
    setSqlTypeError(validationError);
    setLanguageType((prev) => ({
      ...prev,
      sqlTypes: newSqlTypes,
    }));
  };

  const handleSave = async () => {
    try {
      if (nameError || languageError || sqlTypeError)
        return;

      await update('LanguageType', languageType);
      navigate('/languageTypes');
    } catch (error) {
      console.error("Error updating property:", error);
    }
  };

  return (
    <div className="max-w-lg mx-auto p-6 bg-white rounded-lg shadow-md mt-10">
      <h2 className="text-2xl font-semibold text-gray-700 mb-6">{id == 0 ? 'Add' : 'Edit'} Language Type</h2>
      {loading ? (
        <Loader />
      ) : (
        <form className="space-y-4">
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Name:</label>
            <Input
              value={languageType.name}
              onChange={handleNameChange}
              error={nameError}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Description:</label>
            <Input
              value={languageType.description}
              onChange={handleDescriptionChange}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Language:</label>
            <ComboBox
              options={languageOptions}
              selected={languageType.language?.id}
              onChange={handleLanguageChange}
              error={languageError}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Sql Type:</label>
            <MultiComboBox
              options={sqlTypeOptions}
              selected={languageType.sqlTypes?.map(e => e.id)}
              onChange={handleSqlTypeChange}
              error={sqlTypeError}
            />
          </div>
          <div className="flex justify-end space-x-4 mt-6">
            <Button onClick={handleSave}>
              <i className="fa-solid fa-floppy-disk"></i>
            </Button>
            <Button variant="secondary" onClick={() => navigate('/languageTypes')}>
              <i className="fa-solid fa-xmark"></i>
            </Button>
          </div>
        </form>
      )}
    </div>
  );
}
import React, { useEffect, useState, useCallback } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Loader from '../../components/UI/loaders/Loader';
import Input from '../../components/UI/inputs/Input';
import Button from '../../components/UI/buttons/Button';
import MultiComboBox from '../../components/UI/inputs/MultiComboBox';
import { get, getForCombobox, update } from '../../utils/apiHelper';
import { validateEmpty } from '../../utils/validators';

export default function OrmEdit() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [orm, setOrm] = useState({ name: '', description: '', languages: [] });
  const [nameError, setNameError] = useState('');
  const [languagesError, setLanguagesError] = useState('');
  const [loading, setLoading] = useState(true);
  const [languageOptions, setLanguageOptions] = useState([]);

  const fetchOrm = useCallback(async () => {
    try {
      const result = await get('Orm', id);
      setOrm(result);
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

  useEffect(() => {
    fetchLanguage();
    fetchOrm();
  }, [fetchLanguage, fetchOrm]);

  const handleNameChange = (e) => {
    const newName = e.target.value;
    const validationError = validateEmpty(newName, 'Name');
    setNameError(validationError);
    setOrm((prev) => ({
      ...prev,
      name: newName,
    }));
  };

  const handleDescriptionChange = (e) => {
    setOrm((prev) => ({
      ...prev,
      description: e.target.value,
    }));
  };

  const handleLanguagesChange = (selectedOptions) => {
    const newLanguages = languageOptions.filter(e => selectedOptions.includes(e.id));
    const validationError = validateEmpty(newLanguages.length !== 0, 'Languages');
    setLanguagesError(validationError);
    setOrm((prev) => ({
      ...prev,
      languages: newLanguages,
    }));
  };

  const handleSave = async () => {
    try {
      if (nameError || languagesError)
        return;

      await update('Orm', orm);
      navigate('/orms');
    } catch (error) {
      console.error("Error updating language:", error);
    }
  };

  return (
    <div className="max-w-lg mx-auto p-6 bg-white rounded-lg shadow-md mt-10">
      <h2 className="text-2xl font-semibold text-gray-700 mb-6">Edit Orm</h2>
      {loading ? (
        <Loader />
      ) : (
        <form className="space-y-4">
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Name:</label>
            <Input
              value={orm.name}
              onChange={handleNameChange}
              error={nameError}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Description:</label>
            <Input
              value={orm.description}
              onChange={handleDescriptionChange}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Languages:</label>
            <MultiComboBox
              options={languageOptions}
              selected={orm.languages.map(e => e.id)}
              onChange={handleLanguagesChange}
              error={languagesError}
            />
          </div>
          <div className="flex justify-end space-x-4 mt-6">
            <Button onClick={handleSave}>
              <i className="fa-solid fa-floppy-disk"></i>
            </Button>
            <Button variant="secondary" onClick={() => navigate('/orms')}>
              <i className="fa-solid fa-xmark"></i>
            </Button>
          </div>
        </form>
      )}
    </div>
  );
}
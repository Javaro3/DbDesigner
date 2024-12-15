import React, { useEffect, useState, useCallback } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Loader from '../../components/UI/loaders/Loader';
import Input from '../../components/UI/inputs/Input';
import Button from '../../components/UI/buttons/Button';
import MultiComboBox from '../../components/UI/inputs/MultiComboBox';
import { get, getForCombobox, update } from '../../utils/apiHelper';
import { validateEmpty } from '../../utils/validators';

export default function LanguageEdit() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [language, setLanguage] = useState({ name: '', description: '', orms: [] });
  const [nameError, setNameError] = useState('');
  const [ormsError, setOrmsError] = useState('');
  const [loading, setLoading] = useState(true);
  const [ormOptions, setOrmOptions] = useState([]);

  const fetchLanguage = useCallback(async () => {
    try {
      const result = await get('Language', id);
      setLanguage(result);
      setLoading(false);
    } catch (error) {
      setLoading(false);
    }
  }, [id]);

  const fetchOrm = useCallback(async () => {
    try {
      const result = await getForCombobox('Orm');
      setOrmOptions(result);
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
    setLanguage((prev) => ({
      ...prev,
      name: e.target.value,
    }));
  };

  const handleDescriptionChange = (e) => {
    setLanguage((prev) => ({
      ...prev,
      description: e.target.value,
    }));
  };

  const handleOrmsChange = (selectedOptions) => {
    const newOrms = ormOptions.filter(e => selectedOptions.includes(e.id));
    const validationError = validateEmpty(newOrms.length !== 0, 'Orms');
    setOrmsError(validationError);
    setLanguage((prev) => ({
      ...prev,
      orms: newOrms,
    }));
  };

  const handleSave = async () => {
    try {
      if (nameError || ormsError)
        return;

      await update('Language', language);
      navigate('/languages');
    } catch (error) {
      console.error("Error updating language:", error);
    }
  };

  return (
    <div className="max-w-lg mx-auto p-6 bg-white rounded-lg shadow-md mt-10">
      <h2 className="text-2xl font-semibold text-gray-700 mb-6">Edit Language</h2>
      {loading ? (
        <Loader />
      ) : (
        <form className="space-y-4">
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Name:</label>
            <Input
              value={language.name}
              onChange={handleNameChange}
              error={nameError}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Description:</label>
            <Input
              value={language.description}
              onChange={handleDescriptionChange}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Orms:</label>
            <MultiComboBox
              options={ormOptions}
              selected={language.orms.map(e => e.id)}
              onChange={handleOrmsChange}
              error={ormsError}
            />
          </div>
          <div className="flex justify-end space-x-4 mt-6">
            <Button onClick={handleSave}>
              <i className="fa-solid fa-floppy-disk"></i>
            </Button>
            <Button variant="secondary" onClick={() => navigate('/languages')}>
              <i className="fa-solid fa-xmark"></i>
            </Button>
          </div>
        </form>
      )}
    </div>
  );
}
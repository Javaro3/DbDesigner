import React, { useEffect, useState, useCallback } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Loader from '../../components/UI/loaders/Loader';
import Input from '../../components/UI/inputs/Input';
import Button from '../../components/UI/buttons/Button';
import { get, update } from '../../utils/apiHelper';
import ComboBox from '../../components/UI/inputs/ComboBox';
import { validateEmpty } from '../../utils/validators';

export default function PropertyEdit() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [property, setProperty] = useState({ name: '', description: '', hasParams: false });
  const [loading, setLoading] = useState(true);
  const [nameError, setNameError] = useState('');

  const fetchProperty = useCallback(async () => {
    try {
      const result = await get('Property', id);
      setProperty(result);
      setLoading(false);
    } catch (error) {
      setLoading(false);
    }
  }, [id]);

  useEffect(() => {
    fetchProperty();
  }, [fetchProperty]);

  const handleNameChange = (e) => {
    const newName = e.target.value;
    const validationError = validateEmpty(newName, 'Name');
    setNameError(validationError);
    setProperty((prev) => ({
      ...prev,
      name: newName,
    }));
  };

  const handleDescriptionChange = (e) => {
    setProperty((prev) => ({
      ...prev,
      description: e.target.value,
    }));
  };

  const handleHasParamsChange = (e) => {
    setProperty((prev) => ({
      ...prev,
      hasParams: e == 2,
    }));
  };

  const handleSave = async () => {
    try {
      if (nameError)
        return;

      await update('Property', property);
      navigate('/properties');
    } catch (error) {
      console.error("Error updating property:", error);
    }
  };

  return (
    <div className="max-w-lg mx-auto p-6 bg-white rounded-lg shadow-md mt-10">
      <h2 className="text-2xl font-semibold text-gray-700 mb-6">Edit Property</h2>
      {loading ? (
        <Loader />
      ) : (
        <form className="space-y-4">
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Name:</label>
            <Input
              value={property.name}
              onChange={handleNameChange}
              error={nameError}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Description:</label>
            <Input
              value={property.description}
              onChange={handleDescriptionChange}
            />
          </div>
          <div className="flex flex-col">
            <label className="text-gray-600 font-medium mb-2">Has Params:</label>
            <ComboBox
              options={[{id: 2, name: 'Yes'}, {id: 1, name: 'No'}]}
              selected={property.hasParams ? 2 : 1}
              onChange={handleHasParamsChange}
              placeholder="Select value"
            />
          </div>
          <div className="flex justify-end space-x-4 mt-6">
            <Button onClick={handleSave}>
              <i className="fa-solid fa-floppy-disk"></i>
            </Button>
            <Button variant="secondary" onClick={() => navigate('/properties')}>
              <i className="fa-solid fa-xmark"></i>
            </Button>
          </div>
        </form>
      )}
    </div>
  );
}
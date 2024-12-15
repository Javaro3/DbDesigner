import React, { useEffect, useState, useCallback } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Loader from '../../components/UI/loaders/Loader';
import Input from '../../components/UI/inputs/Input';
import Button from '../../components/UI/buttons/Button';
import { get, update } from '../../utils/apiHelper';
import { validateEmpty } from '../../utils/validators';


export default function RelationActionEdit() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [relationAction, setRelationAction] = useState({ name: '', description: '' });
  const [loading, setLoading] = useState(true);
  const [nameError, setNameError] = useState('');

  const fetchRelationAction = useCallback(async () => {
    try {
      const result = await get('RelationAction', id);
      setRelationAction(result);
      setLoading(false);
    } catch (error) {
      setLoading(false);
    }
  }, [id]);

  useEffect(() => {
    fetchRelationAction();
  }, [fetchRelationAction]);

  const handleNameChange = (e) => {
    const newName = e.target.value;
    const validationError = validateEmpty(newName, 'Name');
    setNameError(validationError);
    setRelationAction((prev) => ({
      ...prev,
      name: newName,
    }));
  };

  const handleDescriptionChange = (e) => {
    setRelationAction((prev) => ({
      ...prev,
      description: e.target.value,
    }));
  };

  const handleSave = async () => {
    try {
      if (nameError)
        return;

      await update('RelationAction', relationAction);
      navigate('/relationActions');
    } catch (error) {
      console.error("Error updating architecture:", error);
    }
  };

  return (
    <div className="max-w-lg mx-auto p-6 bg-white rounded-lg shadow-md mt-10">
      <h2 className="text-2xl font-semibold text-gray-700 mb-6">Edit Relation Action</h2>
      {loading ? (
        <Loader />
      ) : (
        <form className="space-y-4">
          <div className="flex flex-col">
            <label htmlFor="name" className="text-gray-600 font-medium mb-2">Name:</label>
            <Input
              value={relationAction.name}
              onChange={handleNameChange}
              error={nameError}
            />
          </div>
          <div className="flex flex-col">
            <label htmlFor="description" className="text-gray-600 font-medium mb-2">Description:</label>
            <Input
              value={relationAction.description}
              onChange={handleDescriptionChange}
            />
          </div>
          <div className="flex justify-end space-x-4 mt-6">
            <Button onClick={handleSave}>
              <i className="fa-solid fa-floppy-disk"></i>
            </Button>
            <Button variant="secondary" onClick={() => navigate('/relationActions')}>
              <i className="fa-solid fa-xmark"></i>
            </Button>
          </div>
        </form>
      )}
    </div>
  );
}
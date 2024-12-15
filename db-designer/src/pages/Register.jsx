import { useState } from 'react';
import { useAuth } from '../hooks/AuthContext';
import Input from '../components/UI/inputs/Input';
import Button from '../components/UI/buttons/Button';
import { useNavigate } from 'react-router-dom';
import { validateEmail, validateEmpty, validatePassword } from '../utils/validators';
import Loader from '../components/UI/loaders/Loader';

export default function Register() {
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const [name, setName] = useState('');
  const [nameError, setNameError] = useState('');

  const [email, setEmail] = useState('');
  const [emailError, setEmailError] = useState('');

  const [password, setPassword] = useState('');
  const [passwordError, setPasswordError] = useState('');
  
  const { register } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (nameError || emailError || passwordError) {
      return;
    }
    setLoading(true);

    try {
      const errorMessage = await register(name, email, password);
      if (!errorMessage) {
        navigate('/login');
      }
      else {
        setError(errorMessage);
      }
    } finally {
      setLoading(false);
    }
  };

  const handleNameChange = (e) => {
    setError('');
    const newName = e.target.value;
    setName(newName);
    const validationError = validateEmpty(newName, 'The name could not be empty');
    setNameError(validationError);
  };

  const handleEmailChange = (e) => {
    setError('');
    const newEmail = e.target.value;
    setEmail(newEmail);
    const validationError = validateEmail(newEmail);
    setEmailError(validationError);
  };

  const handlePasswordChange = (e) => {
    setError('');
    const newPassword = e.target.value;
    setPassword(newPassword);
    const validationError = validatePassword(newPassword);
    setPasswordError(validationError);
  };

  return (
    <div className="flex items-center justify-center min-h-screen bg-gray-100">
      {loading ? (
        <Loader />
      ) : (
        <form onSubmit={handleSubmit} className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4 w-96">
          <h2 className="text-2xl font-bold mb-6 text-center">Register</h2>

          <Input
            label="Name"
            type="text"
            placeholder="Input name"
            value={name}
            onChange={handleNameChange}
            error={nameError}
          />

          <Input
            label="Email"
            type="email"
            placeholder="Input email"
            value={email}
            onChange={handleEmailChange}
            error={emailError}
          />

          <Input
            label="Password"
            type="password"
            placeholder="Input password"
            value={password}
            onChange={handlePasswordChange}
            error={passwordError}
          />

          {error && (
            <p className="mt-4 text-sm text-red-600 text-center">{error}</p>
          )}

          <Button type="submit" className="w-full mt-4">Register</Button>
        </form>
      )}
    </div>
  );
}
import { useState } from 'react';
import { useAuth } from '../hooks/AuthContext';
import Input from '../components/UI/inputs/Input';
import Button from '../components/UI/buttons/Button';
import { useNavigate, Link } from 'react-router-dom';
import { validateEmail, validatePassword } from '../utils/validators';
import GoogleIcon from '@mui/icons-material/Google';
import Loader from '../components/UI/loaders/Loader';

export default function Login() {
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const [email, setEmail] = useState('');
  const [emailError, setEmailError] = useState('');

  const [password, setPassword] = useState('');
  const [passwordError, setPasswordError] = useState('');
  
  const { login, loginWithGoogle } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (emailError || passwordError) {
      return;
    }
    setLoading(true);

    try {
      const errorMessage = await login(email, password);
      if (!errorMessage) {
        navigate('/');
      }
      else {
        setError(errorMessage);
      }
    } finally {
      setLoading(false);
    }
  };

  const handleGoogleLogin = async () => {
    setLoading(true);
    loginWithGoogle();
    setLoading(false);
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
          <h2 className="text-2xl font-bold mb-6 text-center">Login</h2>

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

          <Button type="submit" className="w-full mt-4">
            Login
          </Button>

          <div className="flex items-center my-4">
            <div className="flex-grow border-t border-gray-300"></div>
            <span className="mx-4 text-gray-500">or</span>
            <div className="flex-grow border-t border-gray-300"></div>
          </div>

          <Button 
            onClick={handleGoogleLogin}
            className="w-full flex items-center justify-center gap-2 mb-4"
          >
            <GoogleIcon className="text-xl" />
            Continue with Google
          </Button>

          <div className="text-center mt-4">
            <span className="text-gray-600">Don't have an account? </span>
            <Link 
              to="/register" 
              className="text-blue-600 hover:text-blue-800 font-medium"
            >
              Register
            </Link>
          </div>
        </form>
      )}
    </div>
  );
}
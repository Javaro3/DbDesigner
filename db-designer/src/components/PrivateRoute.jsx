import { useAuth } from '../hooks/AuthContext';
import { Navigate } from 'react-router-dom';

export default function PrivateRoute({ children, allowedRole }) {
  const { token, getUserRole } = useAuth();

  if (!token) {
    return <Navigate to="/login" />;
  }

  const userRole = getUserRole();
  if (allowedRole && !userRole.includes(allowedRole)) {
    return <Navigate to="/login" />;
  }

  return children;
}
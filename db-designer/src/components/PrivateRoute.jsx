import { useAuth } from '../hooks/AuthContext';
import { Navigate } from 'react-router-dom';

export default function PrivateRoute({ children, allowedRole }) {
  const { token, getUserRole } = useAuth();

  if (window.location.href.split("?").length > 1){
    localStorage.setItem('token', window.location.href.split("?")[1]);
    window.location.href = window.location.href.split("?")[0]
  }

  if (!token) {
    return <Navigate to="/login" />;
  }

  const userRole = getUserRole();
  if (allowedRole && !userRole.includes(allowedRole)) {
    return <Navigate to="/login" />;
  }

  return children;
}
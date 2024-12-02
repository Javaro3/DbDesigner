import { createContext, useContext, useState } from 'react';
import * as authService from '../services/authService';
import { jwtDecode } from 'jwt-decode';

const AuthContext = createContext();

export function AuthProvider({ children }) {
  const [token, setToken] = useState(localStorage.getItem('token') || null);

  const login = async (email, password) => {
    try {
      const data = await authService.login(email, password);
      if (data.token) {
        setToken(data.token);
        localStorage.setItem('token', data.token);
      }
    } catch (error) {
      return error.message;
    }
  };

  const register = async (name, email, password) => {
    try {
      await authService.register(name, email, password);
    } catch (error) {
      return error.message;
    }
  };

  const getUserRole = () => {
    if (token) {
      const decodedToken = jwtDecode(token);
      var roles = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
      return roles instanceof Array ? roles : [roles];;
    }
    return null;
  };

  const logout = () => {
    authService.logout();
    setToken(null);
  };

  return (
    <AuthContext.Provider value={{ token, login, register, logout, getUserRole }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
}
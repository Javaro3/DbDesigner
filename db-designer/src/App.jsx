import AppRouter from './router';
import { loader } from '@monaco-editor/react';
import { AuthProvider } from './hooks/AuthContext';

loader.config({
  paths: {
    vs: '/vs',
  },
});

function App() {
  return (
    <AuthProvider>
      <AppRouter />
    </AuthProvider>
  );
}

export default App;
import Header from '../components/Header';
import Footer from '../components/Footer';
import { useAuth } from '../hooks/AuthContext';

export default function MainLayout({ children, showHeader = true, showFooter = true }) {
  const { getUserRole } = useAuth();

  return (
    <div className="flex flex-col min-h-screen">
      {showHeader && <Header roles={getUserRole()} />}
      <main className="flex-grow">
        {children}
      </main>
      {showFooter && <Footer/>}
    </div>
  );
}
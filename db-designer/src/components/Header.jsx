import React from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../hooks/AuthContext';
import Button from '../components/UI/buttons/Button'

export default function Header({ roles }) {
  const { logout } = useAuth();

  const adminLinks = [
    { to: '/architectures', label: 'Architectures' },
    { to: '/databases', label: 'Databases' },
    { to: '/indexTypes', label: 'Index Types' },
    { to: '/relationActions', label: 'Relation Actions' },
    { to: '/languages', label: 'Languages' },
    { to: '/orms', label: 'Orms' },
    { to: '/properties', label: 'Properties'},
    { to: '/roles', label: 'Roles' },
    { to: '/users', label: 'Users' },
    { to: '/sqlTypes', label: 'Sql Types' },
    { to: '/languageTypes', label: 'Language Types' }
  ];
  
  const userLinks = [
  ];

  let links = roles.includes('Administrator') ? adminLinks : userLinks;

  return (
    <header className="bg-blue-500 text-white flex justify-between items-center">
      <div className="text-lg font-bold pl-3">
        <Link to="/home">DB DESINGER</Link>
      </div>

      <nav className="flex gap-4">
        {links.map((link) => (
          <Link key={link.to} to={link.to} className="hover:underline">
            {link.label}
          </Link>
        ))}
      </nav>

      <Button onClick={logout}>Log out</Button>
    </header>
  );
}
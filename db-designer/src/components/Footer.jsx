import React from 'react';
import { Link } from 'react-router-dom';

export default function Footer() {
  return (
    <footer className="bg-gray-800 text-gray-300 mt-auto">
      <div className="container mx-auto flex flex-col md:flex-row justify-between items-center">
        
        <div className="text-lg font-bold mb-4 md:mb-0">
          <Link to="/" className="text-white hover:text-gray-400">
            DB DESIGNER
          </Link>
        </div>

        <div className="flex gap-4">
          <a href="https://twitter.com" target="_blank" rel="noopener noreferrer" className="hover:text-white">
            <i className="fab fa-twitter"></i>
          </a>
          <a href="https://facebook.com" target="_blank" rel="noopener noreferrer" className="hover:text-white">
            <i className="fab fa-facebook"></i>
          </a>
          <a href="https://instagram.com" target="_blank" rel="noopener noreferrer" className="hover:text-white">
            <i className="fab fa-instagram"></i>
          </a>
        </div>

        <div className="text-center text-sm">
          <p>&copy; {new Date().getFullYear()} Db Designer. All rights reserved.</p>
        </div>
      </div>
    </footer>
  );
}
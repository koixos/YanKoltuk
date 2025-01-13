import React from 'react';
import './Navbar.css';
import { useNavigate, useLocation } from 'react-router-dom';

export const Navbar = () => {
  const navigate = useNavigate();
  const location = useLocation();

  const handleClick = () => {
    const role = localStorage.getItem("role");
    if (role === 'Admin') {
      navigate("/admin-dashboard");
      return;
    }
    if (role === 'Manager') {
      navigate("/manager-dashboard");
      return;
    }
    navigate("/");
  };

  const handleLogout = () => {
    localStorage.removeItem("authToken");
    localStorage.removeItem("role");
    localStorage.removeItem("username");
    navigate("/");
  };

  const isLoginPage = location.pathname === '/';

  return (
    <nav className={`navbar navbar-expand-lg fixed-top ${isLoginPage ? 'login-navbar' : ''}`} id='navbar'>
      <div className="container">
        <i className="navbar-brand" onClick={handleClick}>
          <p className='logo'>YAN KOLTUK</p>
          <hr />
        </i>
        {!isLoginPage && (
          <>
            <div className="navbar-right">
              <ul className="navbar-nav ms-auto align-items-center">
                <p className="id-no">Kullanıcı Adı: {localStorage.getItem("username")}</p>
                <li className="nav-item ms-3" id="sign-out-btn">
                  <button
                    type="button"
                    className="btn btn-outline-light"
                    onClick={handleLogout}
                  >
                    <i className="fa-solid fa-arrow-right-from-bracket"></i>
                  </button>
                </li>
              </ul>
            </div>
          </>
        )}
      </div>
    </nav>
  );
};

export default Navbar;

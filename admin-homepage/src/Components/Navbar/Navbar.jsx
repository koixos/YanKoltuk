import React, { useState } from 'react';
import './Navbar.css';

export const Navbar = () => {
  const [menu, setMenu] = useState("#");

  return (
    <nav class="navbar navbar-expand-lg fixed-top" id='navbar'>
      <div class="container">
        <a class="navbar-brand" href="/" onClick={() => {setMenu("home")}}>
          <img
            src="logo192.png"
            alt="Yan Koltuk Logo"
            draggable="false"
            height="45"
          />
        </a>
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <i class="fas fa-bars"></i>
        </button>
        <div class="collapse navbar-collapse" id='navbarSupportedContent'>
          <ul class="navbar-nav ms-auto align-items-center">
            <p className='id-no'>
              Signed in as #ID-NO
            </p>
            {/* <li class="nav-item">
              <a class="nav-link mx-2" href="#!"><i class="fas fa-plus-circle pe-2"></i>Post</a>
            </li>
            <li class="nav-item">
              <a class="nav-link mx-2" href="#!"><i class="fas fa-bell pe-2"></i>Alerts</a>
            </li>
            <li class="nav-item">
              <a class="nav-link mx-2" href="#!"><i class="fas fa-heart pe-2"></i>Trips</a>
            </li> */}
            <li class="nav-item ms-3" id='sign-out-btn'>
              <button type="button" class="btn btn-outline-light">
                <i class="fa-solid fa-arrow-right-from-bracket"></i>
              </button>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  )
}

export default Navbar;
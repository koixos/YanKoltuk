import React, { useEffect, useState } from 'react';
import './Navbar.css';
import { apiUrl } from "../../Services/ApiService";
import axiosInstance from '../../Services/AxiosInstance';

export const Navbar = () => {
  const [managerId, setManagerId] = useState(null);
  const [menu, setMenu] = useState("#");

  const handleLogout = () => {
    localStorage.removeItem("authToken");
    window.location.href = "/";
  };

  useEffect(() => {
    const fetchManagerIdAsync = async () => {
      try {
        const response = await axiosInstance.get(`${apiUrl}/api/manager/manager`);
        setManagerId(response.data.data);
      } catch (e) {
        console.error("Yönetici bilgisi alınamadı: ", e);
      }
    }

    fetchManagerIdAsync();
  }, []);

  return (
    <nav class="navbar navbar-expand-lg fixed-top" id='navbar'>
      <div class="container">
        <a class="navbar-brand" href="/home" onClick={() => {setMenu("home")}}>
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
            {managerId ? <p className='id-no'>
                # Yönetici Kimlik No: {`${managerId}`}
              </p> : <> </>
            }
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
              <button type="button" class="btn btn-outline-light" onClick={handleLogout}>
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
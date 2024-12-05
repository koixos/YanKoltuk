import React, { useState } from "react";
import './Login.css';
import axios from "axios";
import { apiUrl } from "../../Services/ApiService";
import { useNavigate } from 'react-router-dom';

const Login = () => {
  const [Username, setUsername] = useState("");
  const [Password, setPasswd] = useState("");
  const [error, setError] = useState("");

  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post(`${apiUrl}/auth/login`, {
        Username,
        Password,
      });

      const { role, token } = response.data;

      if (response.data) {
        localStorage.setItem("authToken", token);
        localStorage.setItem("role", role);
        localStorage.setItem("username", Username);

        if (role === "Admin")
          navigate("/admin-dashboard");
        else if (role === "Manager")
          navigate("/manager-dashboard");
        else
          navigate("/not-found");
      }
    } catch (err) {
      setError("Giriş başarısız. Bilgilerinizi kontrol ediniz.");
    }
  };

  return (
    <div className="login-container">
      <h2 className="title">Giriş Yap</h2>
      { error && <p className="error">{error}</p>}
      <form className="login-form" onSubmit={handleSubmit}>
        <div className="input-group">
          <input
            type="username"
            placeholder="Kullanıcı Adı"
            value={Username}
            onChange={(e) => setUsername(e.target.value)}
            className="input"
            required
          />
        </div>
        <div className="input-group">
          <input
            type="password"
            placeholder="Şifre"
            value={Password}
            onChange={(e) => setPasswd(e.target.value)}
            className="input"
            required
          />
        </div>
        <div className="forgot-passwd">Şifremi Unuttum</div>
        <button type="submit">
          Giriş Yap
        </button>
      </form>
    </div>
  );
};

export default Login;

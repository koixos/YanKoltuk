import React, { useState } from "react";
import './Login.css';
import axios from "axios";
import { apiUrl } from "../../Services/ApiService";

const Login = () => {
  const [Username, setUsername] = useState("");
  const [Password, setPasswd] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post(`${apiUrl}/api/auth/login`, {
        Username,
        Password,
      });
      localStorage.setItem("authToken", response.data.token);
      window.location.href = "/home";
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
      <p>
        Üye değil misiniz? <a href="/signup" className="signup-link">Kayıt Ol</a>
      </p>
    </div>
  );
};

export default Login;

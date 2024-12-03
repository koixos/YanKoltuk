import React from "react";

const SignUp = () => {
  const styles = {
    container: {
      background: "#ffffff",
      borderRadius: "10px",
      padding: "30px",
      width: "350px",
      textAlign: "center",
      boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)",
      margin: "50px auto",
    },
    title: {
      color: "#0077cc",
      fontSize: "24px",
      fontWeight: "bold",
      marginBottom: "20px",
    },
    form: {
      display: "flex",
      flexDirection: "column",
      alignItems: "center",
    },
    inputGroup: {
      marginBottom: "15px",
      width: "100%",
    },
    input: {
      width: "100%",
      padding: "10px",
      border: "1px solid #0077cc",
      borderRadius: "5px",
      fontSize: "14px",
      boxSizing: "border-box",
    },
    button: {
      background: "#0077cc",
      color: "#ffffff",
      padding: "10px 15px",
      border: "none",
      borderRadius: "5px",
      fontSize: "16px",
      fontWeight: "bold",
      cursor: "pointer",
      width: "100%",
      transition: "background 0.3s",
    },
    link: {
      color: "#0077cc",
      textDecoration: "none",
      fontWeight: "bold",
    },
  };

  return (
    <div style={styles.container}>
      <h2 style={styles.title}>Kayıt Ol</h2>
      <form style={styles.form}>
        <div style={styles.inputGroup}>
          <input
            type="text"
            placeholder="Kullanıcı Adı"
            style={styles.input}
            required
          />
        </div>
        <div style={styles.inputGroup}>
          <input
            type="email"
            placeholder="E-Posta"
            style={styles.input}
            required
          />
        </div>
        <div style={styles.inputGroup}>
          <input
            type="password"
            placeholder="Şifre"
            style={styles.input}
            required
          />
        </div>
        <button type="submit" style={styles.button}>
          Kayıt Ol
        </button>
      </form>
      <p>
        Zaten hesabınız var mı? <a href="/" style={styles.link}>Giriş Yap</a>
      </p>
    </div>
  );
};

export default SignUp;

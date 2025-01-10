import './AddManager.css';
import React, { useState } from 'react';
import { Flip, toast, ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import { useNavigate } from 'react-router-dom';
import axiosInstance from '../../Services/AxiosInstance';

const AddManager = () => {
    const [data, setData] = useState({
        Username: ""
    });
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    const handleChange = (e) => {
        const { name, value } = e.target;
        setData((prevData) => ({ 
            ...prevData, 
            [name]: value,
        }));
    }

    const handleSubmitAsync = async (e) => {
        e.preventDefault();

        try {
            const response = await axiosInstance.post("/admin/addManager", data);
            setPassword(response.data);
            toast.success('Başarıyla kaydedildi.', {
                position: "bottom-right",
                autoClose: 5000,
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                theme: "light",
                transition: Flip,
            });
        } catch (err) {
            toast.error("Yönetici eklenemedi." , {
                position: "bottom-right",
                autoClose: 5000,
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                theme: "light",
                transition: Flip,
            });
        }
    };

    return (
        <div className="add-manager">
            <div className="container" id="add-manager-container">
                <button id='back-btn' className="btn btn-secondary" onClick={() => navigate(-1)}>
                    <i class="fa-solid fa-xmark fa-lg"/>
                </button>
                <form onSubmit={handleSubmitAsync}>
                    <div className="form first">
                        <h3>Yönetici Kayıt</h3>
                        <hr />
                        <div className="details personal">
                            <span className="title">Yönetici Detayları</span>
                            <div className="input-field">
                                <label>Kullanıcı Adı</label>
                                <input 
                                    type="text" 
                                    name='Username'
                                    placeholder="manager1"
                                    value={data.Username}
                                    onChange={handleChange} 
                                    required
                                />
                            </div>
                            <div className="manager-button-container">
                                <button type="submit" className="submit">
                                    <span className="btnText">Gönder</span>
                                    <i className="uil uil-navigator"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                </form>
                {password && (
                    <div className="password-display">
                        <p>Şifre: <strong>{password}</strong></p>
                    </div>
                )}
                <ToastContainer />
            </div>
        </div>
    );
};

export default AddManager;

import './AddManager.css';
import React, { useState } from 'react';
import { toast, ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import { useNavigate } from 'react-router-dom';
import axiosInstance from '../../Services/AxiosInstance';

const AddManager = () => {
    const [data, setData] = useState({
        Username: ""
    });

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
            await axiosInstance.post("/admin/addManager", data);
            toast.success('Başarıyla kaydedildi!', {
                position: "top-center",
                autoClose: 2000,
                hideProgressBar: true,
                closeOnClick: true,
                pauseOnHover: false,
                draggable: true,
                progress: undefined,
            });

            setTimeout(() => {
                navigate('/admin-dashboard');
            }, 1800); 
        } catch (err) {
            toast.error("Hata oluştu " + err.response?.data?.message || "Bilinmeyen bir hata", {
                position: "top-center",
                autoClose: 2000,
                hideProgressBar: true,
                closeOnClick: true,
                pauseOnHover: false,
                draggable: true,
            });
        }
    };

    return (
        <div className="add-service">
            <div className="container" id="add-service-container">
                <header>Yönetici Kayıt</header>
                <form onSubmit={handleSubmitAsync}>
                    <div className="form first">
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
                            <div className="button-container">
                                <button type="submit" className="submit">
                                    <span className="btnText">Submit</span>
                                    <i className="uil uil-navigator"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                </form>
                <ToastContainer />
            </div>
        </div>
    );
};

export default AddManager;
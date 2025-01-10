import './AddService.css';
import React, { useState } from 'react';
import { Flip, toast, ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import { useNavigate } from 'react-router-dom';
import axiosInstance from '../../Services/AxiosInstance';

const AddService = () => {
    const [data, setData] = useState({
        Plate: "",
        Capacity: 0,
        DepartureLocation: "",
        DepartureTime: "",
        DriverIdNo: "",
        DriverName: "",
        DriverPhone: "",
        DriverPhoto: "",
        StewardessIdNo: "",
        StewardessName: "",
        StewardessPhone: "",
        StewardessPhoto: "",
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
            const response = await axiosInstance.post("/manager/addService", data);
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
            toast.error("Servis eklenemedi." , {
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
        <div className="add-service">
            <div className="container" id="add-service-container">
                <button id='back-btn' className="btn btn-secondary" onClick={() => navigate(-1)}>
                    <i class="fa-solid fa-xmark fa-lg"/>
                </button>
                <form onSubmit={handleSubmitAsync}>
                    <div className="form first">
                        <h3>Servis Kayıt</h3>
                        <hr />
                        <div className="details personal">
                            <span className="title">Araç Detayları</span>
                            <div className="fields">
                                <div className="input-field">
                                    <label>Plaka</label>
                                    <input 
                                        type="text" 
                                        name='Plate'
                                        placeholder="34ABC34"
                                        value={data.Plate}
                                        onChange={handleChange} 
                                        required
                                    />
                                </div>

                                <div className="input-field">
                                    <label>Yolcu Kapasitesi</label>
                                    <input 
                                        type="number" 
                                        name='Capacity'
                                        placeholder="20" 
                                        value={data.Capacity}
                                        onChange={handleChange} 
                                        required
                                    />
                                </div>

                                <div className="input-field">
                                    <label>Kalkış Noktası</label>
                                    <input 
                                        type="text" 
                                        name='DepartureLocation'
                                        placeholder="İlçe/mahalle, vb." 
                                        value={data.DepartureLocation}
                                        onChange={handleChange} 
                                        required
                                    />
                                </div>

                                <div className="input-field">
                                    <label>Kalkış Saati</label>
                                    <input 
                                        type="text"
                                        name='DepartureTime'
                                        placeholder="08:30" 
                                        value={data.DepartureTime}
                                        onChange={handleChange} 
                                        required
                                    />
                                </div>
                            </div>
                        </div>

                        <div className="details ID">
                            <span className="title">Personel Detayları</span>

                            <div className="fields">
                                <div className="input-field">
                                    <label>Sürücü T.C. No</label>
                                    <input 
                                        type="number" 
                                        name='DriverIdNo'
                                        placeholder="12345678923" 
                                        value={data.DriverIdNo}
                                        onChange={handleChange} 
                                        required
                                    />
                                </div>

                                <div className="input-field">
                                    <label>Sürücü Ad-Soyad</label>
                                    <input 
                                        type="text" 
                                        name='DriverName'
                                        placeholder="Ali Kaya" 
                                        value={data.DriverName}
                                        onChange={handleChange} 
                                        required
                                    />
                                </div>

                                <div className="input-field">
                                    <label>Sürücü Telefon No</label>
                                    <input 
                                        type="number" 
                                        name='DriverPhone'
                                        placeholder="+905527841212" 
                                        value={data.DriverPhone}
                                        onChange={handleChange} 
                                        required
                                    />
                                </div>

                                <div className="input-field">
                                    <label>Hostes T.C. No</label>
                                    <input 
                                        type="number" 
                                        name='StewardessIdNo'
                                        placeholder="12345678923"
                                        value={data.StewardessIdNo}
                                        onChange={handleChange} 
                                        required
                                    />
                                </div>

                                <div className="input-field">
                                    <label>Hostes Ad-Soyad</label>
                                    <input 
                                        type="text" 
                                        name='StewardessName'
                                        placeholder="Emine Zorlu" 
                                        value={data.StewardessName}
                                        onChange={handleChange} 
                                        required
                                    />
                                </div>

                                <div className="input-field">
                                    <label>Hostes Telefon No</label>
                                    <input 
                                        type="number" 
                                        name='StewardessPhone'
                                        placeholder="+905527841212" 
                                        value={data.StewardessPhone}
                                        onChange={handleChange} 
                                        required
                                    />
                                </div>
                            </div>

                            <div className="button-container">
                                <button type="submit" className="submit">
                                    <span className="btnText">Kaydet</span>
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

export default AddService;
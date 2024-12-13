import './AddService.css';
import React, { useState } from 'react';
import { toast, ToastContainer } from "react-toastify";
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
            await axiosInstance.post("/manager/addService", data);
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
                navigate('/manager-dashboard');
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
                <header>Servis Kayıt</header>
                <form onSubmit={handleSubmitAsync}>
                    <div className="form first">
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
                <ToastContainer />
            </div>
        </div>
    );
};

export default AddService;
import './AddService.css';
import React from 'react';
import { toast, ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import { useNavigate } from 'react-router-dom';

const AddService = () => {
    const navigate = useNavigate();

    const handleSubmit = (e) => {
        e.preventDefault();

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
            navigate('/');
        }, 1500);  // 2-second delay to allow Toastify to display
    };

    return (
        <div className="add-service">
            <div className="container" id="add-service-container">
                <header>Servis Kayıt</header>
                <form onSubmit={handleSubmit}>
                    <div className="form first">
                        <div className="details personal">
                            <span className="title">Araç Detayları</span>

                            <div className="fields">
                                <div className="input-field">
                                    <label>Plaka</label>
                                    <input type="text" placeholder="34ABC34" required />
                                </div>

                                <div className="input-field">
                                    <label>Yolcu Kapasitesi</label>
                                    <input type="number" placeholder="20" required />
                                </div>

                                <div className="input-field">
                                    <label>Kalkış Noktası</label>
                                    <input type="text" placeholder="İlçe/mahalle, vb." required />
                                </div>

                                <div className="input-field">
                                    <label>Kalkış Saati</label>
                                    <input type="text" placeholder="08:30" required />
                                </div>
                            </div>
                        </div>

                        <div className="details ID">
                            <span className="title">Personel Detayları</span>

                            <div className="fields">
                                <div className="input-field">
                                    <label>Sürücü T.C. No</label>
                                    <input type="number" placeholder="12345678923" required />
                                </div>

                                <div className="input-field">
                                    <label>Sürücü Ad-Soyad</label>
                                    <input type="text" placeholder="Ali Kaya" required />
                                </div>

                                <div className="input-field">
                                    <label>Sürücü Telefon No</label>
                                    <input type="number" placeholder="+905527841212" required />
                                </div>

                                <div className="input-field">
                                    <label>Hostes T.C. No</label>
                                    <input type="number" placeholder="12345678923" required />
                                </div>

                                <div className="input-field">
                                    <label>Hostes Ad-Soyad</label>
                                    <input type="text" placeholder="Emine Zorlu" required />
                                </div>

                                <div className="input-field">
                                    <label>Hostes Telefon No</label>
                                    <input type="number" placeholder="+905527841212" required />
                                </div>
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

export default AddService;
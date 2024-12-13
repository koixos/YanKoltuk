import React, { useState } from "react";
import "./ServiceDetail.css";
import { useNavigate } from "react-router-dom";
import { toast, ToastContainer } from "react-toastify";
import axiosInstance from "../../Services/AxiosInstance";

function ServiceDetail({ service, onBack, onEdit }) {
    const [showPopup, setShowPopup] = useState(false);
    const [isEditing, setIsEditing] = useState(false);
    const [updatedService, setUpdatedService] = useState(service);

    const navigate = useNavigate();

    const handleChange = (e) => {
        const { name, value } = e.target;
        setUpdatedService((prevData) => ({ 
            ...prevData, 
            [name]: value,
        }));
    }

    const handleSubmitAsync = async (e) => {
        e.preventDefault();

        const updateData = {
            DriverIdNo: updatedService.driverIdNo,
            DriverName: updatedService.driverName,
            DriverPhone: updatedService.driverPhone,
            StewardessIdNo: updatedService.stewardessIdNo,
            StewardessName: updatedService.stewardessName,
            StewardessPhone: updatedService.stewardessPhone
        };

        try {
            await axiosInstance.put(`/manager/updateService/${service.serviceId}`, updateData);
            toast.success("Servis bilgileri başarıyla güncellendi!");
            setIsEditing(false);

            if (onEdit)
                onEdit({ ...updatedService });
        } catch (err) {
            console.error("Could not update the service: ", err);
            toast.error("Servis bilgileri güncellenemedi.");
        }
    };

    const handleBack = () => {
        return onBack(null);
    };

    const handleEditToggle = () => {
        setIsEditing((prev) => !prev);
    };

    const handleDelete = () => {
        setShowPopup(true);
    };

    const confirmDelete = async (e) => {
        e.preventDefault();
        try {
            await axiosInstance.delete(`/manager/deleteService/${service.serviceId}`);
            toast.success('Servis başarıyla silindi!', {
                position: "top-center",
                autoClose: 2000,
                hideProgressBar: true,
                closeOnClick: true,
                pauseOnHover: false,
                draggable: true,
                progress: undefined,
            });
            closePopup();
        } catch (err) {
            console.error("Could not delete the service: ", err);
            toast.error("Servis silinemedi.");
        }
        
        setTimeout(() => {
            navigate('/manager-dashboard');
        }, 1800); 
    };

    const closePopup = () => {
        setShowPopup(false);
    };

    return (
        <div className="container" id="servicedetail-container">
            <button className="back-btn" onClick={handleBack}>
                <i class="fa-solid fa-arrow-left-long" />
            </button>
            <div className="items" id="servicedetail-details">
                <div className="items-head" id="servicedetail-items-head">
                    <p>Servis Bilgileri</p>
                    <hr />
                    <div className="btn-group">
                        {!isEditing ? (
                            <button className="edit-btn" onClick={handleEditToggle}>
                                <i class="fa-regular fa-pen-to-square" />
                            </button>
                        ) : (
                            <button className="edit-btn" onClick={handleSubmitAsync}>
                                <i class="fa-solid fa-check" />
                            </button>
                        )}
                        <button className="delete-btn" onClick={handleDelete}>
                            <i class="fa-regular fa-trash-can" />
                        </button>
                    </div>
                </div>
                <div className="items-body" id="servicedetail-items-body">
                    <div className="items-body-content" id="servicedetail-items-body-content">
                    <div className="row">
                            <div className="col-sm-3"><h6 className="mb-0">Servis ID:</h6></div>
                            <div className="col-sm-9 text-secondary"> {service.serviceId} </div>
                            <hr />
                        </div>
                        <div className="row">
                            <div className="col-sm-3"><h6 className="mb-0">Plaka:</h6></div>
                            <div className="col-sm-9 text-secondary"> {service.plate} </div>
                            <hr />
                        </div>
                        <div class="row">
                            <div class="col-sm-3"><h6 class="mb-0">Sürücü T.C. :</h6></div>
                            <div class="col-sm-9 text-secondary">
                                {isEditing ? (
                                    <input
                                        type="text"
                                        name="driverIdNo"
                                        value={updatedService.driverIdNo}
                                        onChange={handleChange}
                                    />
                                ) : (
                                    updatedService.driverIdNo
                                )}
                            </div>
                            <hr />
                        </div>
                        <div class="row">
                            <div class="col-sm-3"><h6 class="mb-0">Sürücü Adı:</h6></div>
                            <div class="col-sm-9 text-secondary">
                                {isEditing ? (
                                    <input
                                        type="text"
                                        name="driverName"
                                        value={updatedService.driverName}
                                        onChange={handleChange}
                                    />
                                ) : (
                                    updatedService.driverName
                                )}
                            </div>
                            <hr />
                        </div>
                        <div class="row">
                            <div class="col-sm-3"><h6 class="mb-0">Sürücü Telefon:</h6></div>
                            <div class="col-sm-9 text-secondary"> 
								{isEditing ? (
                                    <input
                                        type="text"
                                        name="driverPhone"
                                        value={updatedService.driverPhone}
                                        onChange={handleChange}
                                    />
                                ) : (
                                    updatedService.driverPhone
                                )}
							</div>
                            <hr />
                        </div>
                        <div class="row">
                            <div class="col-sm-3"><h6 class="mb-0">Hostes T.C. :</h6></div>
                            <div class="col-sm-9 text-secondary">
								{isEditing ? (
                                    <input
                                        type="text"
                                        name="stewardessIdNo"
                                        value={updatedService.stewardessIdNo}
                                        onChange={handleChange}
                                    />
                                ) : (
                                    updatedService.stewardessIdNo
                                )}
							</div>
                            <hr />
                        </div>
                        <div class="row">
                            <div class="col-sm-3"><h6 class="mb-0">Hostes Adı:</h6></div>
                            <div class="col-sm-9 text-secondary">
								{isEditing ? (
                                    <input
                                        type="text"
                                        name="stewardessName"
                                        value={updatedService.stewardessName}
                                        onChange={handleChange}
                                    />
                                ) : (
                                    updatedService.stewardessName
                                )}
							</div>
                            <hr />
                        </div>
                        <div class="row">
                            <div class="col-sm-3"><h6 class="mb-0">Hostes Telefon:</h6></div>
                            <div class="col-sm-9 text-secondary">
								{isEditing ? (
                                    <input
                                        type="text"
                                        name="stewardessPhone"
                                        value={updatedService.stewardessPhone}
                                        onChange={handleChange}
                                    />
                                ) : (
                                    updatedService.stewardessPhone
                                )}
							</div>
                            <hr />
                        </div>
                        <div class="row">
                            <div class="col-sm-3"><h6 class="mb-0">Kapasite:</h6></div>
                            <div class="col-sm-9 text-secondary"> {service.capacity} </div>
                            <hr />
                        </div>
                        <div class="row">
                            <div class="col-sm-3"><h6 class="mb-0">Kalkış Saati:</h6></div>
                            <div class="col-sm-9 text-secondary"> {service.departureTime} </div>
                            <hr />
                        </div>
                        <div className="row">
                            <div className="col-sm-3"><h6 className="mb-0">Kalkış Noktası:</h6></div>
                            <div className="col-sm-9 text-secondary"> {service.departureLocation} </div>
                        </div>
                    </div>
                </div>
            </div>

            <div className="items" id="servicedetail-students">
                <div className="items-head" id="servicedetail-items-head">
                    <p>Kayıtlı Öğrenciler</p>
                    <hr />
                </div>
                <div className="items-body" id="servicedetail-items-body">
                    {service.students && service.students.length > 0 ? (
                        service.students.map((student, i) => (
                            <div 
                                key={i}
                                className="items-body-content"
                                id="servicedetail-items-body-content"
                            >
                                <span> {student.schoolNo} - {student.name} </span>
                                <hr />
                            </div>
                        ))
                    ) : (
                        <p>Kayıtlı öğrenci bulunamadı.</p>
                    )}
                </div>
            </div>

            {showPopup && (
                <div class="popup">
                    <div class="popup-content">
                        <p>{service.plate} plakalı servisi sistemden silmek istediğinize emin misiniz?</p>
                        <button onClick={confirmDelete}>Evet</button>
                        <button onClick={closePopup}>İptal</button>
                    </div>                    
                </div>
            )}

            <ToastContainer />
        </div>
    );
}

export default ServiceDetail;

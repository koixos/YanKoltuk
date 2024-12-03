import React, { useEffect, useState } from "react";
import "./ServiceDetail.css";
import { useNavigate } from "react-router-dom";
import { toast, ToastContainer } from "react-toastify";
import axiosInstance from "../../Services/AxiosInstance";

function ServiceDetail({ service, onBack, onEdit }) {
    const [data, setData] = useState(service || { students: [] });
    const [showPopup, setShowPopup] = useState(false);
    const [isEditing, setIsEditing] = useState(false);
    const [updatedService, setUpdatedService] = useState(service);

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
            const response = await axiosInstance.put(`api/manager/update/${service.serviceId}`, data);
            onEdit(response.data);
        } catch (err) {
            console.error("Could not update the service: ", err);
        }
    };

    const handleBack = () => {
        return onBack(null);
    };

    const handleEditToggle = () => {
		if (isEditing) {
			onEdit(updatedService);
		}
        setIsEditing((prev) => !prev);
    };

    const handleDelete = () => {
        setShowPopup(true);
    };

    const confirmDelete = (e) => {
        e.preventDefault();
        closePopup();
        
        toast.success('Servis başarıyla silindi!', {
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
        }, 1800);  // delay to allow Toastify to display
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
                        <button className="edit-btn" onClick={handleEditToggle}>
                            {!isEditing ? (
                                <i class="fa-regular fa-pen-to-square" />
                            ) : (
                                <i class="fa-solid fa-check" />
                            )}
                        </button>
                        <button className="delete-btn" onClick={handleDelete}>
                            <i class="fa-regular fa-trash-can" />
                        </button>
                    </div>
                </div>
                <div className="items-body" id="servicedetail-items-body">
                    <div className="items-body-content" id="servicedetail-items-body-content">
                        <div className="row">
                            <div className="col-sm-3"><h6 className="mb-0">Plaka:</h6></div>
                            <div className="col-sm-9 text-secondary"> {service.plate} </div>
                            <hr />
                        </div>
                        <div class="row">
                            <div class="col-sm-3"><h6 class="mb-0">Sürücü:</h6></div>
                            <div class="col-sm-9 text-secondary">
                                {isEditing ? (
                                    <input
                                        type="text"
                                        name="name"
                                        value={updatedService.driverName}
                                        //onChange={handleDriverChange}
                                    />
                                ) : (
                                    updatedService.driverName
                                )}
                            </div>
                            <hr />
                        </div>
                        <div class="row">
                            <div class="col-sm-3"><h6 class="mb-0">Sürücü Telefon No:</h6></div>
                            <div class="col-sm-9 text-secondary"> 
								{isEditing ? (
                                    <input
                                        type="text"
                                        name="phone"
                                        value={updatedService.driverPhone}
                                        //onChange={handleDriverChange}
                                    />
                                ) : (
                                    updatedService.driverPhone
                                )}
							</div>
                            <hr />
                        </div>
                        <div class="row">
                            <div class="col-sm-3"><h6 class="mb-0">Hostes:</h6></div>
                            <div class="col-sm-9 text-secondary">
								{isEditing ? (
                                    <input
                                        type="text"
                                        name="name"
                                        value={updatedService.stewardessName}
                                        //onChange={handleStewardessChange}
                                    />
                                ) : (
                                    updatedService.stewardessName
                                )}
							</div>
                            <hr />
                        </div>
                        <div class="row">
                            <div class="col-sm-3"><h6 class="mb-0">Hostes Telefon No:</h6></div>
                            <div class="col-sm-9 text-secondary">
								{isEditing ? (
                                    <input
                                        type="text"
                                        name="phone"
                                        value={updatedService.stewardessPhone}
                                        //onChange={handleStewardessChange}
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

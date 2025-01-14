import React, { useEffect, useState } from "react";
import "./ServiceDetail.css";
import { useNavigate } from "react-router-dom";
import { toast, ToastContainer } from "react-toastify";
import axiosInstance from "../../Services/AxiosInstance";

function ServiceDetail({ service, onEdit }) {
    const [showPopup, setShowPopup] = useState(false);
    const [isEditing, setIsEditing] = useState(false);
    const [updatedService, setUpdatedService] = useState(service);
    const [students, setStudents] = useState([]);

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

    useEffect(() => {
        if (service.studentServices && Array.isArray(service.studentServices.$values)) {
            setStudents(service.studentServices.$values.map(ss => ss.student) || []);
        }        
    }, [service]);


    return (
        <div className="container" id="servicedetail-container">
            <button className='custom-back-btn' onClick={() => navigate(-1)}>
                <i class="fa-solid fa-xmark fa-lg"/>
            </button>
            <div className="items" id="servicedetail-details">
                <div className="items-body" id="servicedetail-items-body">
                    <div className="items-head" id="servicedetail-items-head">
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
                        <p>Servis Bilgileri</p>
                        <hr />
                    </div>
                    <div className="items-body-content" id="servicedetail-items-body-content">
                        <div className="row">
                            <div className="col-sm-3"><h6 className="mb-0">Servis No</h6></div>
                            <div className="col-sm-9"> {service.serviceId} </div>
                            <hr />
                        </div>
                        <div className="row">
                            <div className="col-sm-3"><h6 className="mb-0">Plaka</h6></div>
                            <div className="col-sm-9"> {service.plate} </div>
                            <hr />
                        </div>
                        <div class="row">
                            <div class="col-sm-3"><h6 class="mb-0">Şoför T.C.</h6></div>
                            <div class="col-sm-9">
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
                            <div class="col-sm-3"><h6 class="mb-0">Şoför</h6></div>
                            <div class="col-sm-9">
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
                            <div class="col-sm-3"><h6 class="mb-0">Şoför Telefon</h6></div>
                            <div class="col-sm-9"> 
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
                            <div class="col-sm-3"><h6 class="mb-0">Hostes T.C.</h6></div>
                            <div class="col-sm-9">
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
                            <div class="col-sm-3"><h6 class="mb-0">Hostes</h6></div>
                            <div class="col-sm-9">
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
                            <div class="col-sm-3"><h6 class="mb-0">Hostes Telefon</h6></div>
                            <div class="col-sm-9">
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
                            <div class="col-sm-3"><h6 class="mb-0">Kapasite</h6></div>
                            <div class="col-sm-9"> {service.capacity} </div>
                            <hr />
                        </div>
                        <div class="row">
                            <div class="col-sm-3"><h6 class="mb-0">Kalkış Saati</h6></div>
                            <div class="col-sm-9"> {service.departureTime} </div>
                            <hr />
                        </div>
                        <div className="row">
                            <div className="col-sm-3"><h6 className="mb-0">Kalkış Noktası</h6></div>
                            <div className="col-sm-9"> {service.departureLocation} </div>
                        </div>
                    </div>
                </div>
                <div className="items-body" id="servicedetail-items-body">
                    <div className="items-head" id="servicedetail-items-head">
                        <p>Kayıtlı Öğrenciler</p>
                        <hr />
                    </div>
                    {students && students.length > 0 ? (
                        students.map((student, i) => (
                            <div 
                                key={student.studentId}
                                className="items-body-content"
                                id="servicedetail-items-body-content"
                            >
                                <span>{i + 1}) {student.schoolNo} - {student.name} </span>
                                <hr />
                            </div>
                        ))
                    ) : (
                        <p className="warning">Kayıtlı öğrenci bulunamadı.</p>
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

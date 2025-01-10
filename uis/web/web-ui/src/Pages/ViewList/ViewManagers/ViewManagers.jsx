import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axiosInstance from '../../../Services/AxiosInstance';
import "../ViewList.css";
import { toast, ToastContainer } from 'react-toastify';

const ViewManagers = () => {
    const [showPopup, setShowPopup] = useState(false);
    const [selectedManager, setSelectedManager] = useState(null);
    const [managers, setManagers] = useState([]);

    const navigate = useNavigate();
    
    const handleDelete = () => {
        setShowPopup(true);
    };

    const handleManagerClick = (manager) => {
        setSelectedManager(manager);
    };

    const confirmDelete = async (e) => {
        e.preventDefault();
        try {
            await axiosInstance.delete(`/admin/deleteManager/${selectedManager.managerId}`);
            toast.success('Yönetici başarıyla silindi.', {
                position: "bottom-right",
                closeOnClick: true,
                pauseOnHover: false,
                draggable: true,
            });
            closePopup();
        } catch (err) {
            toast.error("Yönetici silinemedi.", {
                position: "bottom-right",
                closeOnClick: true,
                pauseOnHover: false,
                draggable: true,
            });
        }
        
        setTimeout(() => {
            window.location.reload();
        }, 500); 
    };

    const closePopup = () => {
        setShowPopup(false);
    };

    useEffect(() => {
        const fetchManagersAsync = async () => {
            try {
                const response = await axiosInstance.get("/admin/managers");
                setManagers(response.data.data.$values || []);
            } catch (err) {
                console.error("Could not load managers: ", err);
                setManagers([]);
            }
        };
        fetchManagersAsync();
    }, []);

    return (
        <div class="container" id="viewlist-container">
            <button id='back-btn' className="btn btn-secondary" onClick={() => navigate(-1)}>
                <i class="fa-solid fa-xmark fa-lg"/>
            </button>
            <div class="items" id="viewlist-items">
                <div class="items-head" id="viewlist-items-head">
                    <p>Kayıtlı Yöneticiler</p>
                    <hr/>
                </div>
                <div class="items-body" id="viewlist-items-body">
                    { !managers || managers.length === 0 ? (
                        <p>Kayıtlı Yönetici Bulunamadı</p>
                    ) : (
                            managers.map((item, index) => (
                                <div 
                                    key={item.$id}
                                    class="items-body-content"
                                    id="viewlist-items-body-content"
                                    onClick={() => handleManagerClick(item)}
                                >
                                    <span> {index + 1}) {item.username} (ID: {item.managerId}) </span>
                                        <button
                                            className="delete-btn"
                                            onClick={(e) => handleDelete(e)}
                                        >
                                            <i class="fa-regular fa-trash-can" />
                                        </button>
                                </div>
                            )
                        )
                    )}
                </div>
            </div>

            {showPopup && (
                <div class="popup">
                    <div class="popup-content">
                        <p>{selectedManager.managerId} ID'li yöneticiyi sistemden silmek istediğinize emin misiniz?</p>
                        <button onClick={confirmDelete} className='yes-btn'>Evet</button>
                        <button onClick={closePopup} className='no-btn'>İptal</button>
                    </div>                    
                </div>
            )}

            <ToastContainer />
        </div>
    );
};

export default ViewManagers;

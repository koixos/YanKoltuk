import React, { useEffect, useState } from 'react';
import ServiceDetail from '../../../Components/ServiceDetail/ServiceDetail';
import { useNavigate } from 'react-router-dom';
import axiosInstance from '../../../Services/AxiosInstance';
import "../ViewList.css";

const ViewServices = () => {
    const [selectedService, setSelectedService] = useState(null);
    const [services, setServices] = useState([]);
    const navigate = useNavigate();

    const handleServiceClick = (service) => {
        setSelectedService(service);
        navigate(`/view-services/${service.serviceId}`);
    };

    const handleBack = () => {
        setSelectedService(null);
        navigate('/view-services');
    }

    const handleEdit = (updatedService) => {
        setServices((prevServices) =>
            prevServices.map((service) =>
                service.serviceId === updatedService.serviceId ? updatedService : service
            )
        );
        setSelectedService(updatedService);
    }

    useEffect(() => {
        const fetchServicesAsync = async () => {
            try {
                const response = await axiosInstance.get("/manager/services");
                setServices(response.data.$values || []);
            } catch (err) {
                console.error("Could not load services: ", err);
                setServices([]);
            }
        };
        fetchServicesAsync();
    }, []);

    return (
        <div className="view-services-container">
            {selectedService ? (
                <ServiceDetail
                    service={selectedService}
                    onBack={handleBack}
                    onEdit={handleEdit}
                />
            ) : (
                <div class="container" id="viewlist-container">
                    <div class="items" id="viewlist-items">
                        <button id='back-btn' className="btn btn-secondary" onClick={() => navigate(-1)}>
                            <i class="fa-solid fa-xmark fa-lg"/>
                        </button>
                        <div class="items-head" id="viewlist-items-head">
                            <p>Kayıtlı Servisler</p>
                            <hr />
                        </div>
                        <div class="items-body" id="viewlist-items-body">
                            { !services || services.length === 0 ? (
                                <p>Kayıtlı Servis Bulunamadı</p>
                            ) : (
                                services.map((item, index) => (
                                    <div 
                                        key={item.$id}
                                        onClick={() => handleServiceClick(item)}
                                        class="items-body-content"
                                        id="viewlist-items-body-content"
                                    >
                                        <span> {index + 1}) {item.plate} - {item.driverName} </span>
                                        <i class="fa fa-angle-right"/>
                                    </div>
                                ))
                            )}
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default ViewServices;

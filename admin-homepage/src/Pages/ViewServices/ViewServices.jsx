import React, { useEffect, useState } from 'react';
import ViewList from "../../Components/ViewList/ViewList"
import ServiceDetail from '../../Components/ServiceDetail/ServiceDetail';
import { useNavigate } from 'react-router-dom';
import axiosInstance from '../../Services/AxiosInstance';

const ViewServices = () => {
    const [selectedService, setSelectedService] = useState(null);
    const [services, setServices] = useState();
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
                const response = await axiosInstance.get("api/service/services");
                setServices(response.data);
            } catch (err) {
                console.error("Could not load services: ", err);
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
                <ViewList items={services} onItemClick={handleServiceClick} />
            )}
        </div>
    );
};

export default ViewServices;

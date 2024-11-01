import React, { useState } from 'react';
import ViewList from "../../Components/ViewList/ViewList"
import services from "../../db/serviceDB"
import ServiceDetail from '../../Components/ServiceDetail/ServiceDetail';
import { useNavigate } from 'react-router-dom';

const ViewServices = () => {
    const [selectedService, setSelectedService] = useState(null);
    const [allServices, setAllServices] = useState(services);

    const navigate = useNavigate();

    const handleServiceClick = (service) => {
        setSelectedService(service);
        navigate(`/view-services/${service.id}`);
    };

    const handleBack = () => {
        setSelectedService(null);
        navigate('/view-services');
    }

    const handleEdit = (updatedService) => {
        setAllServices((prevServices) =>
            prevServices.map((service) =>
                service.id === updatedService.id ? updatedService : service
            )
        );

        setSelectedService(updatedService);
    }

    return (
        <div className="view-services-container">
            {selectedService ? (
                <ServiceDetail
                    service={selectedService}
                    onBack={handleBack}
                    onEdit={handleEdit}
                />
            ) : (
                <ViewList items={allServices} onItemClick={handleServiceClick} />
            )}
        </div>
    );
};

export default ViewServices;

import React, { useState } from 'react';
import ViewList from "../../Components/ViewList/ViewList"
import services from "../../db/serviceDB"
import ServiceDetail from '../../Components/ServiceDetail/ServiceDetail';
import { useNavigate } from 'react-router-dom';

const ViewServices = () => {
    const [selectedService, setSelectedService] = useState(null);
    const navigate = useNavigate();

    const handleServiceClick = (service) => {
        setSelectedService(service);
        navigate(`/view-services/${service.id}`);
    };

    const handleBack = () => {
        setSelectedService(null);
        navigate('/view-services');
    }

    return (
        <div className="view-services-container">
            {selectedService ? (
                <ServiceDetail service={selectedService} onBack={handleBack} />
            ) : (
                <ViewList items={services} onItemClick={handleServiceClick} />
            )}
        </div>
    );
};

export default ViewServices;

import React, { useState } from 'react';
import ViewList from "../../Components/ViewList/ViewList"
import services from "../../db/serviceDB"

const ViewServiceLogs = () => {
    return (
        <div className="view-services-container">
            <ViewList items={services}/>
        </div>
    );
};

export default ViewServiceLogs;

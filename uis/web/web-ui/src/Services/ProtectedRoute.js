import React from "react";
import { Navigate } from "react-router-dom";

const ProtectedRoute = ({ children, allowedRoles }) => {
    const token = localStorage.getItem("authToken");
    const userRole = localStorage.getItem("role");
    
    if (!token || !allowedRoles.includes(userRole))
        return <Navigate to="/" />;

    return children;
};

export default ProtectedRoute;
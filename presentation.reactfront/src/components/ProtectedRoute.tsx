import React from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import authService from '../services/authService';

interface ProtectedRouteProps {
    children: React.ReactNode;
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ children }) => {
    const location = useLocation();
    const isAuthenticated = authService.isLoggedIn();

    if (!isAuthenticated) {
        return <Navigate to="/login" state={{ from: location }} replace />;
    }

    return <>{children}</>;
};

export default ProtectedRoute; 
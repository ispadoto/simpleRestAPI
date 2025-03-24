import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Login from './components/auth/Login';
import ProtectedRoute from './components/auth/ProtectedRoute';
import EmployeeComponent from './components/employee/Employee';
import './App.css';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route
          path="/employee"
          element={
            <ProtectedRoute>
              <EmployeeComponent />
            </ProtectedRoute>
          }
        />
        <Route path="/" element={<Navigate to="/login" replace />} />
        {/* Add more routes here */}
      </Routes>
    </Router>
  );
}

export default App;

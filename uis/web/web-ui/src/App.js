import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';

import Navbar from './Components/Navbar/Navbar';
import Footer from './Components/Footer/Footer';

import ProtectedRoute from './Services/ProtectedRoute';
import NotFound from './Pages/NotFound/NotFound';

import ManagerDashboard from './Pages/ManagerDashboard/ManagerDashboard';
import AdminDashboard from './Pages/AdminDashboard/AdminDashboard';

import Login from './Pages/Login/Login';

import AddService from './Pages/AddService/AddService';
import ViewServices from './Pages/ViewList/ViewServices/ViewServices';
import ViewServiceLogs from './Pages/ViewList/ViewServiceLogs/ViewServiceLogs';

import AddManager from './Pages/AddManager/AddManager';
import ViewManagers from './Pages/ViewList/ViewManagers/ViewManagers';

function App() {
  return (
    <div>
      <BrowserRouter>
      <Navbar/>
        <Routes>
          <Route path='/' exact element={<Login/>}/>
          <Route path='/admin-dashboard' element={
              <ProtectedRoute allowedRoles={["Admin"]}>
                <AdminDashboard/>
              </ProtectedRoute>
            }
          />
          <Route path='/add-manager' element={
              <ProtectedRoute allowedRoles={["Admin"]}>
                <AddManager/>
              </ProtectedRoute>
            }
          />
          <Route path='/view-managers' element={
              <ProtectedRoute allowedRoles={["Admin"]}>
                <ViewManagers/>
              </ProtectedRoute>
            }
          />
          <Route path='/manager-dashboard' element={
              <ProtectedRoute allowedRoles={["Manager"]}>
                <ManagerDashboard/>
              </ProtectedRoute>
            }
          />
          <Route path='/add-service' element={
              <ProtectedRoute allowedRoles={["Manager"]}>
                <AddService/>
              </ProtectedRoute>
            }
          />
          <Route path='/view-services' element={
              <ProtectedRoute allowedRoles={["Manager"]}>
                <ViewServices/>
              </ProtectedRoute>
            }
          >
            <Route path=":serviceId" element={
                <ProtectedRoute allowedRoles={["Manager"]}>
                  <ViewServices/>
                </ProtectedRoute>
              }
            />
          </Route>
          <Route path='/service-logs' exact element={
              <ProtectedRoute allowedRoles={["Manager"]}>
                <ViewServiceLogs/>
              </ProtectedRoute>
            }
          />
          <Route path='/not-found' element={<NotFound/>}/>
          <Route path='*' element={<Navigate replace to='/not-found'/>}/>
        </Routes>
        <Footer/>
      </BrowserRouter>
    </div>
  );
}

export default App;
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';

import Navbar from './Components/Navbar/Navbar';
import Footer from './Components/Footer/Footer';

import NotFound from './Pages/NotFound/NotFound';
import Home from './Pages/Home/Home';

import Login from './Pages/Login/Login';
import Signup from './Pages/Signup/Signup';

import AddService from './Pages/AddService/AddService';
import ViewServices from './Pages/ViewServices/ViewServices';
import ViewServiceLogs from './Pages/ViewServiceLogs/ViewServiceLogs';

import ProtectedRoute from './Services/ProtectedRoute';

function App() {
  return (
    <div>
      <BrowserRouter>
      <Navbar/>
        <Routes>
          <Route path='/' exact element={<Login/>}/>
          <Route path='/signup' exact element={<Signup/>}/>
          <Route path='/home' element={
              <ProtectedRoute>
                <Home/>
              </ProtectedRoute>
            }
          />
          <Route path='/add-service' element={
              <ProtectedRoute>
                <AddService/>
              </ProtectedRoute>
            }
          />
          <Route path='/view-services' element={
              <ProtectedRoute>
                <ViewServices/>
              </ProtectedRoute>
            }
          >
            <Route path=":serviceId" element={
                <ProtectedRoute>
                  <ViewServices/>
                </ProtectedRoute>
              }
            />
          </Route>
          <Route path='/service-logs' exact element={
              <ProtectedRoute>
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
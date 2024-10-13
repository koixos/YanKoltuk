import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';

import Navbar from './Components/Navbar/Navbar';
import Footer from './Components/Footer/Footer';

import NotFound from './Pages/NotFound/NotFound';
import Home from './Pages/Home/Home';

import AddService from './Pages/AddService/AddService';
import ViewServices from './Pages/ViewServices/ViewServices';
import ViewServiceLogs from './Pages/ViewServiceLogs/ViewServiceLogs';

function App() {
  return (
    <div>
      <BrowserRouter>
      <Navbar/>
        <Routes>
          <Route path='/' exact element={<Home/>}/>
          <Route path='/add-service' element={<AddService/>}/>
          <Route path='/view-services' element={<ViewServices/>}>
            <Route path=":serviceId" element={<ViewServices />} />
          </Route>
          <Route path='/service-logs' exact element={<ViewServiceLogs/>}/>
          <Route path='/not-found' element={<NotFound/>}/>

          <Route path='*' element={<Navigate replace to='/not-found'/>}/>
        </Routes>
        <Footer/>
      </BrowserRouter>
    </div>
  );
}

export default App;
import React from 'react';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import 'bootstrap/dist/css/bootstrap.min.css';

import NavBar from './Component/Navbar';
import MatterForm from './Component/MatterForm';
import SideBar from './Component/Sidebar';
import MatterView from './Component/MatterView';
const App = () => {
  return (
    <BrowserRouter>
      <NavBar />
      <div className="container mt-3">
        <div className="row">
          <div className="col-md-2 mb-3">
            {/* Left SideBar */}
            <SideBar />
          </div>
          <div className="col-md-10 pt-3">
            <Routes>
            
              <Route path="/MatterForm" element={<MatterForm />} />
              <Route path="/MatterView" element={<MatterView />} />

            </Routes>
          </div>
        </div>
      </div>
    </BrowserRouter>
  );
}

export default App;

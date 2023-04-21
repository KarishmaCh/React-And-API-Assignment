import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { BrowserRouter, Routes, Route, useParams } from "react-router-dom";
import 'bootstrap/dist/css/bootstrap.min.css';
import Dashboard from "./Component/Dashboard";
import NavBar from "./Component/NavBar";

import LeftSideBar from "./Component/LeftSideBar";
import ViewCustomer from "./Component/ViewCustomer";
import AddCustomer from './Component/AddCustomer';
import EditCustomer from "./Component/EditCustomer";
import { Customer } from './Component/EditCustomer';

const App = () => {
  return (
    <BrowserRouter>
      <NavBar />
      <div className="container mt-3">
        <div className="row">
          <div className="col-md-2 mb-3">
            {/* Left SideBar */}
            <LeftSideBar />
          </div>
          <div className="col-md-10 pt-3">

            <Routes>
              <Route path="/" element={<Dashboard />} />

              <Route path="/ViewCustomer" element={<ViewCustomer />} />
              <Route path="/AddCustomer" element={< AddCustomer />} />
              <Route
                path='/edit-customer/:id'
                element={<EditCustomerWrapper />}
              />
            </Routes>

          </div>
        </div>
      </div>
    </BrowserRouter>
  );
}

const EditCustomerWrapper = () => {
  const { id } = useParams();

  const [customer, setCustomer] = useState<Customer | null>(null);

  useEffect(() => {
    axios
      .get(`https://localhost:7249/api/Customers/${id}`)
      .then(response => setCustomer(response.data))
      .catch(error => console.log(error));
  }, [id]);

  if (!customer) {
    return <div>Loading...</div>;
  }

  return <EditCustomer customer={customer} onClose={() => { }} />;
};

export default App;

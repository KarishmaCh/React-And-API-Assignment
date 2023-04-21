import React, { useState, useEffect } from "react";
import axios from "axios";
import "../App.css";
import { Link } from 'react-router-dom';

import EditCustomer from "./EditCustomer";

interface Customer {
    id: number;
    name: string;
    email: string;
    phoneNumber: number;
    location: {
        city: string;
        state: string;
        address: string;
    };
}

const ViewCustomer = () => {
    const [customers, setCustomers] = useState<Customer[] | null>(null);
    const [isLoading, setIsLoading] = useState(true);
    const [showPopup, setShowPopup] = useState(false);
    const [popupType, setPopupType] = useState("");
    const [selectedCustomer, setSelectedCustomer] = useState<Customer | null>(
        null
    );
    const [isDeleteConfirmed, setIsDeleteConfirmed] = useState(false);
    const [isDeleted, setIsDeleted] = useState(false); // new state variable
    const [showEditForm, setShowEditForm] = useState(false);


    useEffect(() => {
        axios
            .get("https://localhost:7249/api/Customers")
            .then((response) => {
                setCustomers(response.data);
                setIsLoading(false);
            })
            .catch((error) => console.log(error));
    }, []);

    return (
        <div className="Main">
            <h1>Customer List</h1>
            {isLoading ? (
                <div className="d-flex justify-content-center">
                    <div className="spinner-border" role="status">
                        <span className="visually-hidden">Loading...</span>
                    </div>
                </div>
            ) : customers && customers.length > 0 ? ( // check if customers exists before rendering
                <>
                    <table className="table">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Phone Number</th>
                                <th>City</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            {customers.map((customer) => (
                                <tr key={customer.id}>
                                    <td
                                        onClick={() => {
                                            setSelectedCustomer(customer);
                                            setPopupType("view");
                                            setShowPopup(true);
                                            setIsDeleteConfirmed(false); // reset confirmation flag
                                            setIsDeleted(false); // reset delete status
                                        }}
                                    >
                                        {customer.name}
                                    </td>
                                    <td>{customer.phoneNumber}</td>
                                    <td>{customer?.location?.city}</td>
                                    <td>
                                        <button
                                            className={`delete-btn ${customer.location ? "disabled-btn" : ""
                                                }`}
                                            disabled={!!customer.location} // disable if location is not null or undefined
                                            onClick={() => {
                                                setSelectedCustomer(customer);
                                                setPopupType("delete");
                                                setShowPopup(true);
                                                setIsDeleteConfirmed(false); // reset confirmation flag
                                                setIsDeleted(false); // reset delete status
                                            }}
                                        >
                                            Delete
                                        </button>
                                        <button className='edit-btn customer-action'>

                                            <Link to={`/edit-customer/${customer.id}`}>Edit</Link>

                                        </button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>

                    {showPopup && selectedCustomer && (
                        <div className="popup">
                            <div className="popup-content">
                                {popupType === "delete" ? (
                                    <>
                                        <h3>Are you sure you want to delete {selectedCustomer.name}?</h3>
                                        <div className="popup-actions">
                                            <button
                                                className="delete-btn"
                                                onClick={() => {
                                                    axios
                                                        .delete(
                                                            `https://localhost:7249/api/Customers/${selectedCustomer.id}`
                                                        )
                                                        .then((response) => {
                                                            const updatedCustomers = customers.filter(
                                                                (cust) => cust.id !== selectedCustomer.id
                                                            );
                                                            setCustomers(updatedCustomers);
                                                            setSelectedCustomer(null);
                                                            setShowPopup(false);
                                                            setIsDeleted(true); // set delete status to true
                                                        })
                                                        .catch((error) => console.log(error));
                                                }}
                                            >
                                                Delete
                                            </button>
                                            <button className='customer-action' onClick={() => setShowPopup(false)}>No</button>
                                        </div>
                                        {isDeleted && !showPopup && (
                                            <p>{selectedCustomer.name} has been successfully deleted.</p>
                                        )}
                                    </>
                                ) : (
                                    <>
                                        <h3>{selectedCustomer.name}</h3>
                                        <p>ID: {selectedCustomer.id}</p>
                                        <p>Email: {selectedCustomer.email}</p>
                                        <p>Phone Number: {selectedCustomer.phoneNumber}</p>
                                        <p>Address: {selectedCustomer?.location?.address}</p>
                                        <p>State: {selectedCustomer?.location?.state}</p>
                                        <p>City: {selectedCustomer?.location?.city}</p>
                                        <button className='customer-action' onClick={() => setShowPopup(false)}>Close</button>
                                    </>
                                )}
                            </div>
                        </div>
                    )}
                </>
            ) : (
                <p>No customers found.</p>
            )}
        </div>
    );
};

export default ViewCustomer;

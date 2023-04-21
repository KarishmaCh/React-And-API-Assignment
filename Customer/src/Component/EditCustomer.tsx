import React, { useState } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import { createBrowserHistory } from 'history';
import { Button, Modal } from 'react-bootstrap';

export interface Customer {
    id: number;
    name: string;
    email: string;
    phoneNumber: string;
    location: {
        city: string;
        state: string;
        address: string;
    };
}

interface EditCustomerProps {
    customer: Customer;
    onClose: () => void;
}

const history = createBrowserHistory();

const EditCustomer = ({ customer, onClose }: EditCustomerProps) => {
    const [name, setName] = useState(customer.name);
    const [email, setEmail] = useState(customer.email);
    const [phoneNumber, setPhoneNumber] = useState(customer.phoneNumber);
    const [city, setCity] = useState(customer.location.city);
    const [state, setState] = useState(customer.location.state);
    const [address, setAddress] = useState(customer.location.address);
    const [showSuccessModal, setShowSuccessModal] = useState(false);

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        const updatedCustomer = {
            id: customer.id,
            name,
            email,
            phoneNumber,
            location: {
                city,
                state,
                address,
            },
        };

        axios
            .put(`https://localhost:7249/api/Customers/${customer.id}`, updatedCustomer)
            .then(() => {
                setShowSuccessModal(true);
            })
            .catch((error) => console.log(error));
    };

    return (
        <div className="Main">
            <h1> Edit Customer </h1>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label htmlFor="name">Name</label>
                    <input type="text" className="form-control" id="name" value={name} onChange={(e) => setName(e.target.value)} />
                </div>
                <div className="form-group">
                    <label htmlFor="email">Email</label>
                    <input type="email" className="form-control" id="email" value={email} onChange={(e) => setEmail(e.target.value)} />
                </div>
                <div className="form-group">
                    <label htmlFor="phone">Phone Number</label>
                    <input type="tel" pattern="[0-9]{10}" className="form-control" id="phone" value={phoneNumber} onChange={(e) => setPhoneNumber(e.target.value)} />
                </div>
                <div className="form-group">
                    <label htmlFor="address">Address</label>
                    <input type="text" className="form-control" id="address" value={address} onChange={(e) => setAddress(e.target.value)} />
                </div>
                <div className="form-group">
                    <label htmlFor="city">City</label>
                    <input type="text" className="form-control" id="city" value={city} onChange={(e) => setCity(e.target.value)} />
                </div>
                <div className="form-group">
                    <label htmlFor="state">State</label>
                    <input type="text" className="form-control" id="state" value={state} onChange={(e) => setState(e.target.value)} />
                </div>
                <button type="submit" className="btn btn-primary">
                    Update
                </button>
                <button type="button" className="btn btn-secondary customer-action" onClick={() => history.back()}>
                    Cancel
                </button>
            </form>

            <Modal show={showSuccessModal} onHide={() => setShowSuccessModal(true)}>
                <Modal.Header closeButton>
                    <Modal.Title>Update Successful</Modal.Title>
                </Modal.Header>
                <Modal.Body>The customer has been successfully updated.</Modal.Body>
                <Modal.Footer>
                    <Button variant="primary" onClick={() => history.back()}>
                        Close
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};

export default EditCustomer;

import React, { useState } from 'react';
import axios, { AxiosError } from 'axios';
import { Link } from 'react-router-dom';
import { createBrowserHistory } from 'history';
import { Modal, Button } from 'react-bootstrap';
interface CustomerData {
    name: string;
    email: string;
    phoneNumber: string;
    location?: {
        id?: number | null;
        address?: string; // Remove the '?' here
        city?: string; // Remove the '?' here
        state?: string;
    };
}

const AddCustomer = () => {
    const [customerData, setCustomerData] = useState<CustomerData>({
        name: '',
        email: '',
        phoneNumber: '',
        location: {
            address: '',
            city: '',
            state: '',
        },
    });

    const history = createBrowserHistory();
    const [showModal, setShowModal] = useState(false);


    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        if (name === 'name' && !/^[a-zA-Z]+$/.test(value)) {
            return;
        }
        setCustomerData((prevState) => ({
            ...prevState,
            [name]: value,
        }));
    };

    const handleCloseModal = () => {
        setShowModal(false);
        history.push('/ViewCustomer');
      };
    const handleLocationChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setCustomerData((prevState) => {
            const newLocation = {
                ...prevState.location,
                [name]: value || '',
            };
            const updatedLocation = newLocation.address && newLocation.city && newLocation.state ? newLocation : null;
            const updatedData: CustomerData = {
                ...prevState,
                location: updatedLocation!,
            };
            return updatedData;
        });
    };

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        if (!customerData.name) {
            alert('Name is required');
            return;
        }

        axios
            .post<CustomerData>('https://localhost:7249/api/Customers', customerData)
            .then((response) => {
                console.log(response.data);

                setShowModal(true);
                history.push('/ViewCustomer');
            })
            .catch((error: AxiosError) => console.log(error));
    };

    return (
        <div className="Main">
        <h1> Edit Customer </h1>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Name</label>
                    <input
                        type="text"
                        className="form-control"
                        name="name"
                        value={customerData.name}
                        onChange={handleChange}
                    />
                </div>
                <div className="form-group">
                    <label>Email</label>
                    <input
                        type="email"
                        className="form-control"
                        name="email"
                        value={customerData.email}
                        onChange={handleChange}
                    />
                </div>
                <div className="form-group">
                    <label>Phone Number</label>
                    <input
                        type="tel"
                        pattern="[0-9]{10}"
                        className="form-control"
                        name="phoneNumber"
                        value={customerData.phoneNumber}
                        onChange={handleChange}
                    />
                </div>
                <hr />
                <h3>Location</h3>
                <div className="form-group">
                    <label>Address</label>
                    <input
                        type="text"
                        className="form-control"
                        name="address"
                        value={customerData?.location?.address!}
                        onChange={handleLocationChange}
                    />
                </div>
                <div className="form-group">
                    <label>City</label>
                    <input
                        type="text"
                        className="form-control"
                        name="city"
                        value={customerData?.location?.city!}
                        onChange={handleLocationChange}
                    />
                </div>
                <div className="form-group">
                    <label>State</label>
                    <input
                        type="text"
                        className="form-control"
                        name="state"
                        value={customerData?.location?.state!}
                        onChange={handleLocationChange}
                    />
                </div>
                <button type="submit" className="btn btn-primary">
                    Add Customer
                </button>
                <button type="button" className="btn btn-secondary customer-action" onClick={() => history.back()}>
                    Cancel
                </button>
            </form>
            <Modal show={showModal} onHide={handleCloseModal}>
                <Modal.Header closeButton>
                    <Modal.Title>Success!</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    Customer added successfully!
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => history.back()}>
                        Close
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};

export default AddCustomer;

import React from 'react';
import { Link } from 'react-router-dom';
import { Navbar, Nav, NavDropdown } from 'react-bootstrap';
import '../App.css';

interface LeftSideBarProps { }

const LeftSideBar: React.FC<LeftSideBarProps> = () => {
    return (
        <Navbar bg="dark" variant="dark" expand="md" className="flex-column fixed-left">
            <div className="sidebar-sticky pt-3">
                <Navbar.Brand as={Link} to="/">
                    Dashboard
                </Navbar.Brand>
                <Nav className="flex-column">

                    <NavDropdown title="Customer">
                        <NavDropdown.Item as={Link} to="/AddCustomer">Add Customer</NavDropdown.Item>
                        <NavDropdown.Item as={Link} to="/ViewCustomer">View Customer</NavDropdown.Item>
                    </NavDropdown>
                </Nav>
            </div>
        </Navbar>
    );
};

export default LeftSideBar;
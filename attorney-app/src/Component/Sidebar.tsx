import React from 'react';
import { Link } from 'react-router-dom';
import { Navbar, Nav, NavDropdown } from 'react-bootstrap';

import '../App.css';

interface SideBarProps { }

const SideBar: React.FC<SideBarProps> = () => {
    return (
        <Navbar bg="dark" variant="dark" expand="md" className="flex-column fixed-left">
            <div className="sidebar-sticky pt-3">
                <Navbar.Brand as={Link} to="/">
                    Dashboard
                </Navbar.Brand>
                <Nav className="flex-column">

                    <NavDropdown title="Matter">
                        <NavDropdown.Item as={Link} to="/MatterForm">Add Matter</NavDropdown.Item>
                        <NavDropdown.Item as={Link} to="/MatterView">View Matter</NavDropdown.Item>
                        <NavDropdown.Item as={Link} to="/MatterViewClient">Client Matters</NavDropdown.Item>
                    </NavDropdown>

                    <NavDropdown title="Invoice">
                      
                        <NavDropdown.Item as={Link} to="/MatterInvoices">Matter Invoices</NavDropdown.Item>
                      
                    </NavDropdown>
                </Nav>
            </div>
        </Navbar>
    );
};

export default SideBar;
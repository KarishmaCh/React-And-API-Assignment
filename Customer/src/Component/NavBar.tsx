import { Link } from "react-router-dom";
import { Navbar, Container, Image, NavDropdown } from 'react-bootstrap';
import '../App.css';





const NavBar = () => {
  return (
    <Navbar bg="light" expand="lg" fixed="top">
      <Container>
        <Navbar.Brand as={Link} to="/" className="mr-auto"><h2> Customer</h2></Navbar.Brand>

        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Navbar.Collapse className="justify-content-end">

            <NavDropdown title={<Image src="https://www.gravatar.com/avatar/2c7d99fe281ecd3bcd65ab915bac6dd5?s=250" roundedCircle style={{ width: '50px', height: '50px' }} />} id="basic-nav-dropdown">
              <NavDropdown.Item href="#">Name</NavDropdown.Item>
              <NavDropdown.Divider />
              <NavDropdown.Item href="#">Logout</NavDropdown.Item>
            </NavDropdown>
          </Navbar.Collapse>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};
export default NavBar;

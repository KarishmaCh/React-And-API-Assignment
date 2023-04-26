import React from 'react';
import { Container, Row, Col, Button } from 'react-bootstrap';


const Dashboard: React.FC = () => {
  
  const missionStatement = 'Our mission is to provide our clients with reliable and effective legal solutions that help them achieve their goals. We pride ourselves on our professionalism, integrity, and dedication to serving our community.';

  return (
    <div className="Main">
    <Container>
      <Row className="justify-content-md-center my-5">
        <Col md="8">
          <h1>Welcome to the Law Form</h1>
          <p>At the Law Form, we’re committed to providing high-quality legal services to our clients. Whether you’re an individual or a business, we’re here to help you achieve your goals.</p>
          <Button variant="primary">Get Started</Button>
        </Col>
      </Row>
    
      <Row>
        <Col md="8">
          <h2>Our Mission</h2>
          <p>{missionStatement}</p>
        </Col>
        <Col md="4">
          <h2>Contact Us</h2>
          <address>
            123 Main Street<br />
            Anytown, USA 12345<br />
            (555) 555-1212<br />
            email@example.com
          </address>
        </Col>
      </Row>
    </Container>
    </div>
  );
};

export default Dashboard;

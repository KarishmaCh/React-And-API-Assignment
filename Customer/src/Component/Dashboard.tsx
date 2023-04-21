import { Container, Row, Col, Image, Card } from 'react-bootstrap';
import image1 from '../Image/image1.jpg'
import image2 from '../Image/image2.jpg'
import image3 from '../Image/image3.jpg'
import image4 from '../Image/image4.jpg'
import image5 from '../Image/image5.jpg'
import '../App.css'

const Dashboard = () => {
  return (
    <div className="Main">
      <Container fluid>
        {/* First row */}
        <Row className="dashboard-row">
          <Col className="p-0">
            <Image src={image1} fluid width="100%" className="custom-img" />
          </Col>
        </Row>

        {/* Second row */}
        <Row className="dashboard-row">
          <Col xs={12} md={6}>
            <h2>Welcome to the Dashboard</h2>
            <p style={{ textAlign: 'justify' }}>Time management is the method of planning and balancing your time between different activities. Good time management helps you to complete a given task in a specific time frame amidst challenges and tight schedules. When you manage your schedule and deadlines, strong time management skills help you better your reputation and move ahead in the workplace.</p>
          </Col>
          <Col xs={12} md={6}>
            <Image src={image2} style={{ width: "538px", height: "206px" }} />
          </Col>
        </Row>


        {/* Third row */}
        <Row className="dashboard-row">
          <Col>
            <h3>Your Recent Activity</h3>
            <p>
              The activity statement provides details of all activity on the customer receivables between two selected dates. This includes all invoices, refunds and payments. Any outstanding balance dated prior to the chosen statement period will appear as a forward balance at the top of the statement. The list is displayed in chronological order and is split by currencies.</p>
          </Col>
        </Row>
        <Row className="dashboard-row">
          <Col xs={12} sm={6} lg={4}>
            <Card style={{ height: "100%" }}>
              <Card.Img variant="top" src={image3} />
              <Card.Body>
                <Card.Title>Task 1</Card.Title>
                <Card.Text>
                  This is a sample task description.
                </Card.Text>
              </Card.Body>
            </Card>
          </Col>
          <Col xs={12} sm={6} lg={4}>
            <Card style={{ height: "100%" }} >
              <Card.Img variant="top" src={image4} />
              <Card.Body>
                <Card.Title>Task 2</Card.Title>
                <Card.Text>
                  This is a sample task description.
                </Card.Text>
              </Card.Body>
            </Card>
          </Col>
          <Col xs={12} sm={6} lg={4}>
            <Card style={{ height: "100%" }}>
              <Card.Img variant="top" src={image5} />
              <Card.Body>
                <Card.Title>Task 3</Card.Title>
                <Card.Text>
                  This is a sample task description.
                </Card.Text>
              </Card.Body>
            </Card>
          </Col>
        </Row>
      </Container>
    </div>
  );
};

export default Dashboard;

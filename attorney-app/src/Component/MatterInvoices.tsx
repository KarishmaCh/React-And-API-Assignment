import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Dropdown, Card } from 'react-bootstrap';

interface MatterInvoicesData {
  id: number;
  name: string;
  amount:number;
  hourlyRate: number;
  hoursWorked: number;
  attorneyId: number;
}

interface MatterData {
  id: number;
  name: string;
}

interface AttorneyData {
  id: number;
  name: string;
}

function MatterInvoices() {
  const [matters, setMatters] = useState<MatterData[]>([]);
  const [selectedMatterId, setSelectedMatterId] = useState<number | null>(null);
  const [matterData, setMatterData] = useState<MatterInvoicesData[]>([]);
  const [attorneyName, setAttorneyName] = useState<string>(''); // Add state variable for attorney name
  const [view, setView] = useState<'table' | 'cards'>('cards');

  useEffect(() => {
    async function fetchMatters() {
      try {
        const response = await axios.get('https://localhost:7036/api/Matter');
        setMatters(response.data);
      } catch (error) {
        console.error(error);
      }
    }

    fetchMatters();
  }, []);

  useEffect(() => {
    async function fetchMatterData() {
      try {
        if (selectedMatterId === null) {
          return;
        }

        const response = await axios.get(`https://localhost:7036/api/Matter/${selectedMatterId}/invoices`);
        setMatterData(response.data);
      } catch (error) {
        console.error(error);
      }
    }

    fetchMatterData();
  }, [selectedMatterId]);

  useEffect(() => {
    async function fetchAttorneyName() {
      try {
        if (matterData.length === 0) {
          return;
        }
        const response = await axios.get(`https://localhost:7036/api/Attorney/${matterData[0].attorneyId}`);
        setAttorneyName(response.data.name);
      } catch (error) {
        console.error(error);
      }
    }

    fetchAttorneyName();
  }, [matterData]);

  return (
    <div className="Main">
      <div className='d-flex justify-content-center mt-8'>
        <Card>
          <Card.Header as="h3">{attorneyName} - {matterData.length > 0 ? matterData[0].name : 'Select a Matter'}</Card.Header> {/* Add attorney name to card header */}
          <Card.Body>
            <Dropdown onSelect={(value) => setSelectedMatterId(Number(value))} className='mb-4' drop="up">
              <Dropdown.Toggle variant="primary" id="dropdown-matter">
                {selectedMatterId ? matters.find(matter => matter.id === selectedMatterId)?.name : 'Select a Matter'}
              </Dropdown.Toggle>
              <Dropdown.Menu>
                {matters.map((matter, index) => (
                  <Dropdown.Item key={index} eventKey={matter.id.toString()}>{matter.name}</Dropdown.Item>
                ))}
              </Dropdown.Menu>
            </Dropdown>

            <button onClick={() => setView(view === 'table' ? 'cards' : 'table')}>
              {view === 'table' ? 'Switch to Cards' : 'Switch to Table'}
            </button>

            {matterData.length > 0 ? (
              <>
                {view === 'table' && (
                  <table>
                    <thead>
                      <tr>
                        <th>Invoice ID</th>
                        <th>Amount</th>
                        <th>Hourly Rate</th>
                        <th>Hours Worked</th>
                      </tr>
                    </thead>
                    <tbody>
                      {matterData.map((invoice) => (
                        <tr key={invoice.id}>
                            
                          <td>{invoice.id}</td>
                          <td>{invoice.amount}</td>
                          <td>${invoice.hourlyRate.toFixed(2)}</td>
                          <td>{invoice.hoursWorked}</td>
                          <td>{attorneyName}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                )}

                {view === 'cards' && (
                  <div className="row">
                    {matterData.map((invoice) => (
                      <div className="col-md-4" key={invoice.id}>
                        <Card>
                          <Card.Body>
                            <Card.Title>Invoice #{invoice.id}</Card.Title>
                            <Card.Text>
                            
                              <strong>Amount:</strong> ${invoice.amount.toFixed(2)}<br />
                              <strong>Hourly Rate:</strong> ${invoice.hourlyRate.toFixed(2)}<br/>
                              <strong>Hours Worked:</strong> {invoice.hoursWorked}<br/>
                              <strong>Attorney:</strong> {attorneyName}<br/>
                            </Card.Text>
                          </Card.Body>
                        </Card>
                      </div>
                    ))}
                  </div>
                )}
              </>
            ) : (
              <Card.Text>No invoices generated for this matter</Card.Text>
            )}
          </Card.Body>
        </Card>
      </div>
    </div>
  );
}

export default MatterInvoices;

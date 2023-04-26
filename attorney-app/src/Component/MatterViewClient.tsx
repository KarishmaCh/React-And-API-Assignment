import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Dropdown } from 'react-bootstrap';

interface Client {
  id: number;
  name: string;
}

interface Matter {
  id: number;
  name: string;
  jurisdictionId: string;
  billingAttorneyId: string;
  responsibleAttorneyId: string;
  createdOn: string;
}

interface Jurisdiction {
  id: string;
  name: string;
}

interface Attorney {
  id: string;
  name: string;
}

function MatterViewClient() {
  const [clients, setClients] = useState<Client[]>([]);
  const [selectedClient, setSelectedClient] = useState<Client | null>(null);
  const [matters, setMatters] = useState<Matter[]>([]);

  const [jurisdictions, setJurisdictions] = useState<{[key: string]: string}>({});
  const [attorneys, setAttorneys] = useState<{[key: string]: string}>({});

  useEffect(() => {
    // Fetch the list of clients from the API
    axios.get("https://localhost:7036/api/Clients").then(response => {
      setClients(response.data);
    }).catch(error => {
      console.error(error);
    });

    // Fetch the list of jurisdictions from the API
    axios.get("https://localhost:7036/api/Jurisdiction").then(response => {
      const allJurisdictions: {[key: string]: string} = {};

      response.data.forEach((jurisdiction: Jurisdiction) => {
        allJurisdictions[jurisdiction.id] = jurisdiction.name;
      });

      setJurisdictions(allJurisdictions);
    }).catch(error => {
      console.error(error);
    });

    // Fetch the list of attorneys from the API
    axios.get("https://localhost:7036/api/Attorney").then(response => {
      const allAttorneys: {[key: string]: string} = {};

      response.data.forEach((attorney: Attorney) => {
        allAttorneys[attorney.id] = attorney.name;
      });

      setAttorneys(allAttorneys);
    }).catch(error => {
      console.error(error);
    });
  }, []);

  useEffect(() => {
    if (selectedClient) {
      // Fetch the matters for the selected client from the API
      axios.get(`https://localhost:7036/api/Matter/${selectedClient.id}/matters`).then(response => {
        setMatters(response.data);
      }).catch(error => {
        console.error(error);
      });
    }
  }, [selectedClient]);

  const handleClientSelect = (client: Client) => {
    setSelectedClient(client);
  };

  return (
    <div className="Main">
      {/* Add a heading */}
      <h1>Client Matters</h1>
      <Dropdown>
        {/* Wrap the client dropdown in a row */}
        <div className="row">
          <div className="col-md-3">
            <Dropdown.Toggle variant="primary" id="client-dropdown">
              {selectedClient ? selectedClient.name : "Select a Client"}
            </Dropdown.Toggle>
            <Dropdown.Menu>
              {clients.map(client => (
                <Dropdown.Item key={client.id} onClick={() => handleClientSelect(client)}>
                  {client.name}
                </Dropdown.Item>
              ))}
            </Dropdown.Menu>
          </div>
        </div>
      </Dropdown>
  
      {selectedClient && (
        matters.length > 0 ? (
          <table className="table table-striped table-bordered mt-4">
            <thead className="thead-dark">
              <tr>
                <th>Name</th>
                <th>Jurisdiction</th>
                <th>Billing Attorney</th>
                <th>Responsible Attorney</th>
                <th>Created On</th>
              </tr>
            </thead>
            <tbody>
              {matters.map(matter => (
                <tr key={matter.id}>
                  <td>{matter.name}</td>
                  <td>{jurisdictions[matter.jurisdictionId]}</td>
                  <td>{attorneys[matter.billingAttorneyId]}</td>
                  <td>{attorneys[matter.responsibleAttorneyId]}</td>
                  <td>{matter.createdOn}</td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <h3>No matters for this client.</h3>
        )
      )}
    </div>
  );
}

export default MatterViewClient;

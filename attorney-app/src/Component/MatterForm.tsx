import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Modal, Button } from 'react-bootstrap';




interface Matter {
  name: string;
  jurisdictionId: number;
  billingAttorneyId: number;
  responsibleAttorneyId: number;
  clientId: number;
  createdOn: Date;
}

interface Client {
  id: number;
  name: string;
}

interface Jurisdiction {
  id: number;
  name: string;
}

interface Attorney {
  id: number;
  name: string;
}

const MatterForm = () => {
  const [matter, setMatter] = useState<Matter>({
    name: '',
    jurisdictionId: 0,
    billingAttorneyId: 0,
    responsibleAttorneyId: 0,
    clientId: 0,
    createdOn: new Date(),
  });



  const [clients, setClients] = useState<Array<Client>>([]);
  const [jurisdictions, setJurisdictions] = useState<Array<Jurisdiction>>([]);
  const [attorneys, setAttorneys] = useState<Array<Attorney>>([]);
  const [disabled, setDisabled] = useState<boolean>(true);
  const [successMessage, setSuccessMessage] = useState<string>('');
  const [showSuccessModal, setShowSuccessModal] = useState<boolean>(false);

  
  useEffect(() => {
    // Fetching clients data
    axios
      .get<Array<Client>>('https://localhost:7036/api/Clients')
      .then((response) => {
        setClients(response.data);
      })
      .catch((error) => {
        console.log(error);
      });

    // Fetching jurisdictions data
    axios
      .get<Array<Jurisdiction>>('https://localhost:7036/api/Jurisdiction')
      .then((response) => {
        setJurisdictions(response.data);
      })
      .catch((error) => {
        console.log(error);
      });

    // Fetching attorneys data
    axios
      .get<Array<Attorney>>('https://localhost:7036/api/Attorney')
      .then((response) => {
        setAttorneys(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);

  useEffect(() => {
    if (
      matter.name &&
      matter.jurisdictionId &&
      matter.billingAttorneyId &&
      matter.responsibleAttorneyId &&
      matter.clientId &&
      matter.createdOn
    ) {
      setDisabled(false);
    } else {
      setDisabled(true);
    }
  }, [matter]);

  const handleChange = (event: React.SyntheticEvent) => {
    const target = event.target as HTMLInputElement;
    setMatter({ ...matter, [target.name]: target.value });
  };

  


  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    axios
      .post('https://localhost:7036/api/Matter', matter)
      .then((response) => {
        console.log(response);
        setShowSuccessModal(true);
        setMatter({
          name: '',
          jurisdictionId: 0,
          billingAttorneyId: 0,
          responsibleAttorneyId: 0,
          clientId: 0,
          createdOn: new Date(),
        });
      })
      .catch((error) => {
        console.error(error);
      });
  };

  return (
    <div className="container mt-5">
    <Modal show={showSuccessModal} onHide={() => setShowSuccessModal(false)}>
      <Modal.Header closeButton>
        <Modal.Title>Success</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        Matter created successfully!
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={() => setShowSuccessModal(false)}>Close</Button>
      </Modal.Footer>
    </Modal>
 
      <div className="row justify-content-center">
        <div className="col-md-8">
          <div className="card shadow-lg border-0 rounded-lg">
            <div className="card-header">
              <h3 className="text-center font-weight-light my-4">
                Create Matter
              </h3>
            </div>
            <div className="card-body">
              <form onSubmit={handleSubmit}>
                <div className="form-group">
                  <label htmlFor="name">Name:</label>
                  <input
  type="text"
  className="form-control"
  id="name"
  name="name"
  value={matter.name}
  onChange={handleChange}
  required
  maxLength={50}
  pattern="[A-Za-z ]+"
/>
                </div>
                <div className="form-group">
                  <label htmlFor="jurisdictionId">Jurisdiction:</label>
                  <select
                    className="form-control"
                    id="jurisdictionId"
                    name="jurisdictionId"
                    value={matter.jurisdictionId}
                    onChange={handleChange}
                    required
                  >
                    <option value="">Please select a jurisdiction</option>
                    {jurisdictions.map((jurisdiction) => (
                      <option key={jurisdiction.id} value={jurisdiction.id}>
                        {jurisdiction.name}
                      </option>
                    ))}
                  </select>
                </div>
                <div className="form-group">
                  <label htmlFor="billingAttorneyId">Billing Attorney:</label>
                  <select
                    className="form-control"
                    id="billingAttorneyId"
                    name="billingAttorneyId"
                    value={matter.billingAttorneyId}
                    onChange={handleChange}
                    required
                  >
                    <option value="">Please select a billing attorney</option>
                    {attorneys.map((attorney) => (
                      <option key={attorney.id} value={attorney.id}>
                        {attorney.name}
                      </option>
                    ))}
                  </select>
                </div>
                <div className="form-group">
                  <label htmlFor="responsibleAttorneyId">
                    Responsible Attorney:
                  </label>
                  <select
                    className="form-control"
                    id="responsibleAttorneyId"
                    name="responsibleAttorneyId"
                    value={matter.responsibleAttorneyId}
                    onChange={handleChange}
                    required
                  >
                    <option value="">
                      Please select a responsible attorney
                    </option>
                    {attorneys.map((attorney) => (
                      <option key={attorney.id} value={attorney.id}>
                        {attorney.name}
                      </option>
                    ))}
                  </select>
                </div>
                <div className="form-group">
                  <label htmlFor="clientId">Client:</label>
                  <select
                    className="form-control"
                    id="clientId"
                    name="clientId"
                    value={matter.clientId}
                    onChange={handleChange}
                    required
                  >
                    <option value="">Please select a client</option>
                    {clients.map((client) => (
                      <option key={client.id} value={client.id}>
                        {client.name}
                      </option>
                    ))}
                  </select>
                </div>
                <div className="form-group">
                  <label htmlFor="createdOn">Created On:</label>
                  <input
                    type="datetime-local"
                    className="form-control"
                    id="createdOn"
                    name="createdOn"
                    value={matter.createdOn.toISOString().slice(0, -8)}
                    onChange={handleChange}
                    required
                  />
                </div>
                <div className="card-footer d-flex justify-content-between">
                <button
        type="button"
        className="btn btn-secondary"
        onClick={() => { window.history.back() }}
      >
        Cancel
      </button>
                
                  <button
                    type="submit"
                    className="btn btn-primary"
                    disabled={disabled}
                    
                  >
                    Submit
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default MatterForm;

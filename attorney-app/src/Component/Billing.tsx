import React, { useState, useEffect } from 'react';
import axios from 'axios';
import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

interface InvoiceData {
  invoiceId: number;
  matterName: string;
  hourlyRate: number;
  hoursWorked: number;
  totalAmount: number;
  createdOn: string;
}

interface Attorney {
  id: number;
  name: string;
}

function Billing() {
  const [attorneys, setAttorneys] = useState<Attorney[]>([]);
  const [selectedAttorneyId, setSelectedAttorneyId] = useState<string | null>(null);
  const [invoiceDataArray, setInvoiceDataArray] = useState<InvoiceData[]>([]);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    axios
      .get('https://localhost:7036/api/Attorney')
      .then(response => {
        setAttorneys(response.data);
      })
      .catch(error => {
        console.error(error);
        setError('Failed to fetch attorneys.');
      });
  }, []);

  const handleSelectedAttorneyChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const attorneyId = event.target.value || null;

    if (!attorneyId) {
      setInvoiceDataArray([]);
      return;
    }

    axios.get(`https://localhost:7036/api/Invoice/api/invoices/billing/${attorneyId}`)
      .then(response => {
        setInvoiceDataArray(response.data);
        setError(null);
      })
      .catch(error => {
        console.error(error);
        setError('Failed to fetch invoice data.');
      });

    setSelectedAttorneyId(attorneyId);
  };

  return (
    <div className='container mt-5'>
      <div className="row">
        <div className="col-12">
          <h3>Billing</h3>
        </div>
        <div className="col-md-4">
          <label htmlFor="attorney" className="form-label">Attorney:</label>
          <select id="attorney" className="form-select" value={selectedAttorneyId ?? ''} onChange={handleSelectedAttorneyChange}>
            <option value="">Select an attorney</option>
            {attorneys.map(attorney => (
              <option key={attorney.id} value={attorney.id.toString()}>
                {attorney.name}
              </option>
            ))}
          </select>
        </div>
        <div className="col-12">
          {error && <div className='alert alert-danger'>{error}</div>}
          {invoiceDataArray.length > 0 && (
            <table className="table mt-3">
              <thead>
                <tr>
                  <th>Matter Name</th>
                  <th>Hourly Rate</th>
                  <th>Hours Worked</th>
                  <th>Total Amount</th>
                  <th>Created On</th>
                </tr>
              </thead>
              <tbody>
                {invoiceDataArray.map(invoiceData => (
                  <tr key={invoiceData.invoiceId}>
                    <td>{invoiceData.matterName}</td>
                    <td>{invoiceData.hourlyRate}</td>
                    <td>{invoiceData.hoursWorked}</td>
                    <td>{invoiceData.totalAmount}</td>
                    <td>{invoiceData.createdOn}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}
        </div>
      </div>
    </div>
  );
}

export default Billing;

import React, { useState, useEffect } from "react";
import axios from "axios";
import { Table, Spinner } from "react-bootstrap";

interface Matter {
  ID: number;
  name: string;
  jurisdictionId: number;
  jurisdictionName: string; // Added property
  billingAttorneyId: number;
  billingAttorneyName: string; // Added property
  responsibleAttorneyId: number;
  responsibleAttorneyName: string; // Added property
  clientId: number;
  clientName: string; // Added property
  createdOn: string;
}

function MatterView() {
  const [matters, setMatters] = useState<Matter[]>([]);
  const [isLoading, setLoading] = useState(true);

  useEffect(() => {
    Promise.all([
      axios.get("https://localhost:7036/api/Matter"),
      axios.get("https://localhost:7036/api/Clients"),
      axios.get("https://localhost:7036/api/Jurisdiction"),
      axios.get("https://localhost:7036/api/Attorney")
    ]).then(
      ([
        mattersResponse,
        clientsResponse,
        jurisdictionResponse,
        attorneyResponse
      ]) => {
        const mattersData = mattersResponse.data as Matter[];
        const clientsData = clientsResponse.data as any[]; // Any type as the actual type is not provided
        const jurisdictionData = jurisdictionResponse.data as any[]; // Any type as the actual type is not provided
        const attorneyData = attorneyResponse.data as any[]; // Any type as the actual type is not provided

        const mattersWithNames = mattersData.map((matter) => ({
          ...matter,
          jurisdictionName:
            jurisdictionData.find((j) => j.id === matter.jurisdictionId)?.name ||
            "",
          billingAttorneyName:
            attorneyData.find((a) => a.id === matter.billingAttorneyId)?.name ||
            "",
          responsibleAttorneyName:
            attorneyData.find((a) => a.id === matter.responsibleAttorneyId)?.name ||
            "",
          clientName: clientsData.find((c) => c.id === matter.clientId)?.name || ""
        }));

        setMatters(mattersWithNames);
        setLoading(false);
      }
    );
  }, []);

  return (
    <div className="Main">
      {isLoading ? (
        <Spinner animation="border" />
      ) : (
        <>
          <Table striped bordered hover>
            <thead>
              <tr>
                <th colSpan={6}>
                  <h2 style={{ textAlign: "center" }}>Matters</h2>
                </th>
              </tr>
              <tr>
                <th>Name</th>
                <th>Jurisdiction</th>
                <th>Billing Attorney</th>
                <th>Responsible Attorney</th>
                <th>Client</th>
                <th>Created On</th>
              </tr>
            </thead>
            <tbody>
              {matters.map((matter) => (
                <tr key={matter.ID}>
                  <td>{matter.name}</td>
                  <td>{matter.jurisdictionName}</td>
                  <td>{matter.billingAttorneyName}</td>
                  <td>{matter.responsibleAttorneyName}</td>
                  <td>{matter.clientName}</td>
                  <td>{matter.createdOn}</td>
                </tr>
              ))}
            </tbody>
          </Table>
        </>
      )}
    </div>
  );
}

export default MatterView;

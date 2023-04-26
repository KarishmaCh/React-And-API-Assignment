import React, { useState, useEffect } from "react";
import axios from "axios";

import "bootstrap/dist/css/bootstrap.min.css";

interface Data {
  id: number;
  matterName: string;
  name: string;
  hourlyRate: number;
  hoursWorked: number;
  totalAmount: number;
  createdOn: number;
}

const BillingShow = () => {
  const [data, setData] = useState<Data[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      const result = await axios.get(
        "https://localhost:7036/api/Invoice/LastWeeksBillingForAllAttorneys"
      );
      setData(result.data);
    };
    fetchData();
  }, []);

  return (
    <div className="Main">
        <h1>Last Week's Billing Table</h1>
    <table className="table">
      <thead>
        <tr>
          <th>Matter Name</th>
          <th>Attorney Name</th>
          <th>Hourly Rate</th>
          <th>Hours Worked</th>
          <th>Total Amount</th>
          <th>Created On</th>
        </tr>
      </thead>
      <tbody>
        {data.map((item) => (
          <tr key={item.id}>
            <td>{item.matterName}</td>
            <td>{item.name}</td>
            <td>{item.hourlyRate}</td>
            <td>{item.hoursWorked}</td>
            <td>{item.totalAmount}</td>
            <td>{item.createdOn}</td>
          </tr>
        ))}
      </tbody>
    </table>
    </div>
  );
};

export default BillingShow;

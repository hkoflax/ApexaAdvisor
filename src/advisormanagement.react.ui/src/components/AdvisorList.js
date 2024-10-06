// src/components/AdvisorList.js
import React, { useState, useEffect } from 'react';
import { getAdvisors, deleteAdvisor } from '../api';
import { Link } from 'react-router-dom';

const AdvisorList = () => {
    const [advisors, setAdvisors] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        loadAdvisors();
    }, []);

    const loadAdvisors = async () => {
        try {
            const response = await getAdvisors();
            setAdvisors(response.data);
            setError(null);  // Clear any previous errors
        } catch (error) {
            setError(error.message);  // Capture both status and body
        }
    };

    const confirmDelete = (id) => {
        if (window.confirm('Are you sure you want to delete this advisor?')) {
            handleDelete(id);
        }
    };

    const handleDelete = async (id) => {
        try {
            await deleteAdvisor(id);
            loadAdvisors(); // Refresh list after deletion
            setError(null);
        } catch (error) {
            setError(error.message);
        }
    };


    return (
        <div className="container mt-5">
            <h2 className="mb-4">Advisors List</h2>

            {/* Display error message if there's an error */}
            {error && <div className="alert alert-danger" role="alert">{error}</div>}

            <Link to="/advisors/create" className="btn btn-primary mb-3">Create New Advisor</Link>

            {advisors.length > 0 ? (
                <table className="table table-striped">
                    <thead>
                        <tr>
                            <th>Full Name</th>
                            <th>SIN</th>
                            <th>Address</th>
                            <th>Phone Number</th>
                            <th>Health Status</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {advisors.map((advisor) => (
                            <tr key={advisor.id}>
                                <td>{advisor.fullName}</td>
                                <td>{advisor.sin}</td>
                                <td>{advisor.address}</td>
                                <td>{advisor.phoneNumber}</td>
                                <td>{advisor.healthStatus}</td>
                                <td>
                                    <Link to={`/advisors/edit/${advisor.id}`} className="btn btn-warning btn-sm me-2">Edit</Link>
                                    <button className="btn btn-danger btn-sm" onClick={() => confirmDelete(advisor.id)}>Delete</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            ) : (
                <p>No advisors found.</p>
            )}
        </div>
    );
};

export default AdvisorList;

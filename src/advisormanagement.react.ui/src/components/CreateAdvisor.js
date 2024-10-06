// src/components/CreateAdvisor.js
import React, { useState } from 'react';
import { createAdvisor } from '../api';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';


const CreateAdvisor = () => {
    const [advisor, setAdvisor] = useState({ fullName: '', sin: '', address: '', phoneNumber: ''});
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    // Helper function to remove empty fields
    const removeEmptyFields = (obj) => {
        return Object.fromEntries(
            Object.entries(obj).filter(([key, value]) => value !== '' && value !== null)
        );
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const cleanedAdvisor = removeEmptyFields(advisor); // Remove empty fields before submission
            await createAdvisor(cleanedAdvisor);
            setError(null);  // Clear previous errors
            navigate('/advisors');  // Redirect to advisor list after creation
        } catch (error) {
            setError(error.message);  // Set the error message
        }
    };

    const handleCancel = () => {
        navigate('/advisors'); // Navigate back to advisor list on cancel
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setAdvisor({ ...advisor, [name]: value }); // Update state for form fields
    };

    return (
        <div className="container mt-5">
            <h2>Create Advisor</h2>

            {/* Display error message if there's an error */}
            {error && <div className="alert alert-danger" role="alert">{error}</div>}

            <form onSubmit={handleSubmit}>
                <div className="mb-3">
                    <label className="form-label">Full Name</label>
                    <input
                        type="text"
                        name="fullName"
                        className="form-control"
                        value={advisor.fullName}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label className="form-label">SIN</label>
                    <input
                        type="text"
                        name="sin"
                        className="form-control"
                        value={advisor.sin}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label className="form-label">Address</label>
                    <input
                        type="text"
                        name="address"
                        className="form-control"
                        value={advisor.address}
                        onChange={handleChange}
                    />
                </div>
                <div className="mb-3">
                    <label className="form-label">Phone Number</label>
                    <input
                        type="text"
                        name="phoneNumber"
                        className="form-control"
                        value={advisor.phoneNumber}
                        onChange={handleChange}
                    />
                </div>

                <button type="submit" className="btn btn-success">Create</button>
                <button type="button" className="btn btn-secondary ms-3" onClick={handleCancel}>Cancel</button>
            </form>
        </div>
    );
};

export default CreateAdvisor;

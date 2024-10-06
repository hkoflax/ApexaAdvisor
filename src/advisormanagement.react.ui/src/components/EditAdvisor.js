// src/components/EditAdvisor.js
import React, { useState, useEffect } from 'react';
import { getAdvisors, updateAdvisor } from '../api';
import { useParams, useNavigate } from 'react-router-dom';

const EditAdvisor = () => {
    const [advisor, setAdvisor] = useState({ id: '', fullName: '', address: '', phoneNumber: '', healthStatus: '' });
    const [error, setError] = useState(null);
    const { id } = useParams();
    const navigate = useNavigate();

    useEffect(() => {
        loadAdvisor();
    }, []);

    const loadAdvisor = async () => {
        try {
            const response = await getAdvisors();
            const advisorData = response.data.find((a) => a.id === id);
            if (advisorData) {
                setAdvisor(advisorData);
                setError(null);
            } else {
                throw new Error("Advisor not found");
            }
        } catch (error) {
            setError(error.message);
        }
    };

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
            await updateAdvisor(advisor.id, cleanedAdvisor);
            setError(null);  // Clear previous errors
            navigate('/advisors');  // Redirect to advisor list after update
        } catch (error) {
            setError(error.message);
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
            <h2>Edit Advisor</h2>

            {error && <div className="alert alert-danger" role="alert">{error}</div>}

            <form onSubmit={handleSubmit}>
                <input type="hidden" value={advisor.id} />

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

                <div className="mb-3">
                    <label className="form-label">Health Status</label>
                    <select
                        name="healthStatus"
                        className="form-control"
                        value={advisor.healthStatus}
                        onChange={handleChange}
                        required
                    >
                        <option value="">Select Health Status</option>
                        <option value="Green">Green</option>
                        <option value="Yellow">Yellow</option>
                        <option value="Red">Red</option>
                    </select>
                </div>

                <button type="submit" className="btn btn-primary">Save Changes</button>
                <button type="button" className="btn btn-secondary ms-3" onClick={handleCancel}>Cancel</button>
            </form>
        </div>
    );
};

export default EditAdvisor;

import axios from 'axios';

const API_URL = 'https://localhost:7016/api/Advisor/';  // Replace with your actual API URL

// Function to handle errors globally (optional)
const handleApiError = (error) => {
    if (error.response) {
        // Server responded with a status code other than 2xx
        const status = error.response.status;
        const message = error.response.data?.message || error.response.statusText;
        const details = error.response.data?.details || JSON.stringify(error.response.data);
        throw new Error(`Error ${status}: ${message} - ${details}`);
    } else if (error.request) {
        // Request was made but no response received
        throw new Error('Network Error: Could not reach the server');
    } else {
        // Something else happened
        throw new Error('An unexpected error occurred');
    }
};

export const getAdvisors = async () => {
    try {
        const response = await axios.get('https://localhost:7016/api/Advisor/GetAdvisorsList');
        return response;
    } catch (error) {
        handleApiError(error);
    }
};

export const createAdvisor = async (advisor) => {
    try {
        const response = await axios.post('https://localhost:7016/api/Advisor/CreateAdvisor', advisor);
        return response;
    } catch (error) {
        handleApiError(error);
    }
};

export const updateAdvisor = async (id, advisor) => {
    try {
        const response = await axios.put(`https://localhost:7016/api/Advisor/UpdateAdvisor`, advisor);
        return response;
    } catch (error) {
        handleApiError(error);
    }
};

export const deleteAdvisor = async (id) => {
    try {
        const response = await axios.delete(`https://localhost:7016/api/Advisor/DeleteAdvisor/${id}`);
        return response;
    } catch (error) {
        handleApiError(error);
    }
};

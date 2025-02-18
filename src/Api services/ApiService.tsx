// src/services/apiService.ts

import axios from 'axios';
import { LoginResponse } from './Api interfaces/Interfaces';
const api = axios.create({
  baseURL: 'https://yourapi.com/api', // Base URL for your API
});

// Request interceptor to include JWT token in the headers for all requests
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token'); // Get token from localStorage
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// Example API calls

// Login endpoint
export const loginUser = async (email: string, password: string): Promise<LoginResponse> => {
  try {
    const response = await api.post('/auth/login', { email, password });
    return response.data;  // response.data will be of type LoginResponse
  } catch (error) {
    throw error;
  }
};
// More API methods can be added here for other endpoints

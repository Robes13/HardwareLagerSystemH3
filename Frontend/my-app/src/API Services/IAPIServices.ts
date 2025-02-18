// src/api/interfaces/Interfaces.ts

export interface User {
    id: number;
    username: string;
    email: string;
    // add other fields as necessary
  }
  
  export interface LoginResponse {
    token: string;
    user: User;
  }
  
  export interface ProfileProps {
    user: User;
  }
  
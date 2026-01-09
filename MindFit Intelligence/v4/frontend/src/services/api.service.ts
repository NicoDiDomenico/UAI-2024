import axios from "axios";
import { Gym, LoginRequest, LoginResponse, AuthUser } from "../types/auth.types";

const API_URL = import.meta.env.VITE_API_URL || "http://localhost:3000/api";

const api = axios.create({
  baseURL: API_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

export const gymService = {
  async getGyms(): Promise<Gym[]> {
    const response = await api.get<{ gyms: Gym[] }>("/gyms");
    return response.data.gyms;
  },
};

export const authService = {
  async login(data: LoginRequest): Promise<LoginResponse> {
    const response = await api.post<LoginResponse>("/auth/login", data);
    return response.data;
  },

  async getCurrentUser(token: string): Promise<AuthUser> {
    const response = await api.get<{ usuario: AuthUser }>("/auth/me", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data.usuario;
  },
};

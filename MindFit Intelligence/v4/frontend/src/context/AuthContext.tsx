import { useState, useEffect } from "react";
import type { ReactNode } from "react";
import type { AuthUser } from "../types/auth.types";
import { authService } from "../services/api.service";
import { AuthContext } from "./auth.context";

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [user, setUser] = useState<AuthUser | null>(null);
  const [token, setToken] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    // Verificar si hay token al cargar
    const storedToken = localStorage.getItem("token");
    if (storedToken) {
      authService
        .getCurrentUser(storedToken)
        .then((userData) => {
          setToken(storedToken);
          setUser(userData);
        })
        .catch(() => {
          localStorage.removeItem("token");
        })
        .finally(() => {
          setIsLoading(false);
        });
    } else {
      // No token, finish loading immediately
      setTimeout(() => setIsLoading(false), 0);
    }
  }, []);

  const login = (newToken: string, newUser: AuthUser) => {
    localStorage.setItem("token", newToken);
    setToken(newToken);
    setUser(newUser);
  };

  const logout = () => {
    localStorage.removeItem("token");
    setToken(null);
    setUser(null);
  };

  return (
    <AuthContext.Provider
      value={{
        user,
        token,
        isAuthenticated: !!token && !!user,
        login,
        logout,
        isLoading,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

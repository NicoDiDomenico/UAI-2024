import { createRoot } from "react-dom/client";
import { AuthProvider } from "./contexts/AuthContext.tsx";
import App from "./App.tsx";

import "./index.css";
import "react-toastify/dist/ReactToastify.css";

createRoot(document.getElementById("root")!).render(
  <AuthProvider>
    <App />
  </AuthProvider>
);
import "dotenv/config";
import express from "express";
import cors from "cors";
import { config } from "./config/env.js";
import authRoutes from "./routes/auth.routes.js";
import gymRoutes from "./routes/gym.routes.js";
import { errorMiddleware } from "./middleware/error.middleware.js";
const app = express();
// Middlewares
app.use(cors({
    origin: config.frontendUrl,
    credentials: true,
}));
app.use(express.json());
// Routes
app.get("/", (_req, res) => {
    res.json({ message: "MindFit Intelligence API v4" });
});
app.use("/api/auth", authRoutes);
app.use("/api/gyms", gymRoutes);
// Error handler
app.use(errorMiddleware);
// Start server
app.listen(config.port, () => {
    console.log(`🚀 Servidor corriendo en http://localhost:${config.port}`);
    console.log(`📊 Frontend permitido: ${config.frontendUrl}`);
});
//# sourceMappingURL=index.js.map
import "dotenv/config";
export const config = {
    port: parseInt(process.env.PORT || "3000", 10),
    jwtSecret: (process.env.JWT_SECRET || "default-secret-change-in-production"),
    jwtExpiration: process.env.JWT_EXPIRATION || "8h",
    frontendUrl: process.env.FRONTEND_URL || "http://localhost:5173",
    databaseUrl: process.env.DATABASE_URL || "",
};
//# sourceMappingURL=env.js.map
import "dotenv/config";
import type { Secret } from "jsonwebtoken";
export declare const config: {
    port: number;
    jwtSecret: Secret;
    jwtExpiration: string;
    frontendUrl: string;
    databaseUrl: string;
};
//# sourceMappingURL=env.d.ts.map
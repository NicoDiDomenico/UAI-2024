declare const _default: () => {
    database: {
        type: "postgres";
        host: string;
        port: number;
        username: string;
        password: string;
        database: string;
        synchronize: boolean;
        logging: boolean;
        autoLoadEntities: boolean;
    };
    jwt: {
        secret: string;
        accessExpiration: string;
        refreshExpiration: string;
    };
    argon2: {
        type: number;
        memoryCost: number;
        timeCost: number;
        parallelism: number;
    };
    redis: {
        host: string;
        port: number;
        password: string | undefined;
        db: number;
    };
    security: {
        lockoutThreshold: number;
        lockoutDuration: number;
        passwordResetExpiration: number;
        permissionsCacheTTL: number;
    };
    cors: {
        origin: string;
        credentials: boolean;
    };
    app: {
        port: number;
        environment: string;
        apiPrefix: string;
    };
};
export default _default;

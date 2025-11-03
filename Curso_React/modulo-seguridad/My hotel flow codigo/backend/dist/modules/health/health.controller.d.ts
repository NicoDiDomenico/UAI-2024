import { HealthCheckService, TypeOrmHealthIndicator, MemoryHealthIndicator } from '@nestjs/terminus';
export declare class HealthController {
    private health;
    private db;
    private memory;
    constructor(health: HealthCheckService, db: TypeOrmHealthIndicator, memory: MemoryHealthIndicator);
    check(): Promise<import("@nestjs/terminus").HealthCheckResult>;
    checkLive(): Promise<import("@nestjs/terminus").HealthCheckResult>;
    checkReady(): Promise<import("@nestjs/terminus").HealthCheckResult>;
}

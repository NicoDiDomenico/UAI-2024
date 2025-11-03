export interface EndpointMetrics {
    totalRequests: number;
    successfulRequests: number;
    failedRequests: number;
    totalResponseTime: number;
    minResponseTime: number;
    maxResponseTime: number;
    lastAccessTime: Date;
}
export interface MetricsData {
    uptime: number;
    totalRequests: number;
    successfulRequests: number;
    failedRequests: number;
    activeRequests: number;
    averageResponseTime: number;
    endpoints: Record<string, EndpointMetrics>;
    systemInfo: {
        memoryUsage: NodeJS.MemoryUsage;
        cpuUsage: NodeJS.CpuUsage;
        nodeVersion: string;
        platform: string;
    };
    timestamp: Date;
}
export declare class MetricsService {
    private startTime;
    private totalRequests;
    private successfulRequests;
    private failedRequests;
    private activeRequests;
    private totalResponseTime;
    private endpoints;
    startRequest(): void;
    endRequest(method: string, path: string, statusCode: number, responseTime: number): void;
    getMetrics(): MetricsData;
    resetMetrics(): void;
}

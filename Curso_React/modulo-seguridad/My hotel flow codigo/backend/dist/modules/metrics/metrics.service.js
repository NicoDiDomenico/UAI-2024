"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.MetricsService = void 0;
const common_1 = require("@nestjs/common");
let MetricsService = class MetricsService {
    startTime = Date.now();
    totalRequests = 0;
    successfulRequests = 0;
    failedRequests = 0;
    activeRequests = 0;
    totalResponseTime = 0;
    endpoints = new Map();
    startRequest() {
        this.activeRequests++;
    }
    endRequest(method, path, statusCode, responseTime) {
        this.activeRequests--;
        this.totalRequests++;
        this.totalResponseTime += responseTime;
        if (statusCode >= 200 && statusCode < 400) {
            this.successfulRequests++;
        }
        else {
            this.failedRequests++;
        }
        const endpointKey = `${method} ${path}`;
        const endpointMetrics = this.endpoints.get(endpointKey) || {
            totalRequests: 0,
            successfulRequests: 0,
            failedRequests: 0,
            totalResponseTime: 0,
            minResponseTime: Infinity,
            maxResponseTime: 0,
            lastAccessTime: new Date(),
        };
        endpointMetrics.totalRequests++;
        endpointMetrics.totalResponseTime += responseTime;
        endpointMetrics.minResponseTime = Math.min(endpointMetrics.minResponseTime, responseTime);
        endpointMetrics.maxResponseTime = Math.max(endpointMetrics.maxResponseTime, responseTime);
        endpointMetrics.lastAccessTime = new Date();
        if (statusCode >= 200 && statusCode < 400) {
            endpointMetrics.successfulRequests++;
        }
        else {
            endpointMetrics.failedRequests++;
        }
        this.endpoints.set(endpointKey, endpointMetrics);
    }
    getMetrics() {
        const uptime = (Date.now() - this.startTime) / 1000;
        const averageResponseTime = this.totalRequests > 0 ? this.totalResponseTime / this.totalRequests : 0;
        const endpointsObj = {};
        this.endpoints.forEach((value, key) => {
            endpointsObj[key] = {
                ...value,
                minResponseTime: value.minResponseTime === Infinity ? 0 : value.minResponseTime,
            };
        });
        return {
            uptime,
            totalRequests: this.totalRequests,
            successfulRequests: this.successfulRequests,
            failedRequests: this.failedRequests,
            activeRequests: this.activeRequests,
            averageResponseTime: Math.round(averageResponseTime * 100) / 100,
            endpoints: endpointsObj,
            systemInfo: {
                memoryUsage: process.memoryUsage(),
                cpuUsage: process.cpuUsage(),
                nodeVersion: process.version,
                platform: process.platform,
            },
            timestamp: new Date(),
        };
    }
    resetMetrics() {
        this.startTime = Date.now();
        this.totalRequests = 0;
        this.successfulRequests = 0;
        this.failedRequests = 0;
        this.activeRequests = 0;
        this.totalResponseTime = 0;
        this.endpoints.clear();
    }
};
exports.MetricsService = MetricsService;
exports.MetricsService = MetricsService = __decorate([
    (0, common_1.Injectable)()
], MetricsService);
//# sourceMappingURL=metrics.service.js.map
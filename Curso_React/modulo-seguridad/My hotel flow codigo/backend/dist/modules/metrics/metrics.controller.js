"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.MetricsController = void 0;
const common_1 = require("@nestjs/common");
const swagger_1 = require("@nestjs/swagger");
const metrics_service_1 = require("./metrics.service");
const public_decorator_1 = require("../../common/decorators/public.decorator");
let MetricsController = class MetricsController {
    metricsService;
    constructor(metricsService) {
        this.metricsService = metricsService;
    }
    getMetrics() {
        return this.metricsService.getMetrics();
    }
};
exports.MetricsController = MetricsController;
__decorate([
    (0, common_1.Get)(),
    (0, public_decorator_1.Public)(),
    (0, swagger_1.ApiOperation)({
        summary: 'Obtener métricas de la API',
        description: 'Retorna estadísticas de uso, rendimiento y estado del sistema',
    }),
    (0, swagger_1.ApiResponse)({
        status: 200,
        description: 'Métricas obtenidas exitosamente',
        schema: {
            type: 'object',
            properties: {
                uptime: {
                    type: 'number',
                    description: 'Tiempo de actividad en segundos',
                },
                totalRequests: {
                    type: 'number',
                    description: 'Total de requests procesadas',
                },
                successfulRequests: {
                    type: 'number',
                    description: 'Requests exitosas (2xx, 3xx)',
                },
                failedRequests: {
                    type: 'number',
                    description: 'Requests fallidas (4xx, 5xx)',
                },
                activeRequests: {
                    type: 'number',
                    description: 'Requests activas en este momento',
                },
                averageResponseTime: {
                    type: 'number',
                    description: 'Tiempo de respuesta promedio en ms',
                },
                endpoints: {
                    type: 'object',
                    description: 'Métricas por endpoint',
                },
                systemInfo: {
                    type: 'object',
                    description: 'Información del sistema',
                },
                timestamp: {
                    type: 'string',
                    format: 'date-time',
                },
            },
        },
    }),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", []),
    __metadata("design:returntype", void 0)
], MetricsController.prototype, "getMetrics", null);
exports.MetricsController = MetricsController = __decorate([
    (0, swagger_1.ApiTags)('Metrics'),
    (0, common_1.Controller)('metrics'),
    __metadata("design:paramtypes", [metrics_service_1.MetricsService])
], MetricsController);
//# sourceMappingURL=metrics.controller.js.map
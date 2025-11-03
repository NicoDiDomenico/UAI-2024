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
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.LoggerService = void 0;
const common_1 = require("@nestjs/common");
const winston_1 = require("winston");
const chalk_1 = __importDefault(require("chalk"));
let LoggerService = class LoggerService {
    logger;
    context;
    constructor(context) {
        this.context = context;
        const customFormat = winston_1.format.printf(({ timestamp, level, message, context, emoji, ...meta }) => {
            const ctx = context && typeof context === 'string'
                ? chalk_1.default.cyan(`[${context}]`)
                : '';
            const ts = typeof timestamp === 'string' ? chalk_1.default.gray(timestamp) : '';
            const levelFormatted = this.formatLevel(typeof level === 'string' ? level : 'info');
            const emojiStr = emoji && typeof emoji === 'string' ? `${emoji} ` : '';
            let metaStr = '';
            if (Object.keys(meta).length > 0) {
                metaStr = '\n' + chalk_1.default.gray(JSON.stringify(meta, null, 2));
            }
            return `${ts} ${levelFormatted} ${ctx} ${emojiStr}${String(message)}${metaStr}`;
        });
        this.logger = (0, winston_1.createLogger)({
            level: process.env.LOG_LEVEL || 'info',
            format: winston_1.format.combine(winston_1.format.timestamp({ format: 'YYYY-MM-DD HH:mm:ss' }), winston_1.format.errors({ stack: true }), customFormat),
            transports: [
                new winston_1.transports.Console({
                    format: winston_1.format.combine(winston_1.format.colorize({ all: false }), customFormat),
                }),
                ...(process.env.NODE_ENV === 'production'
                    ? [
                        new winston_1.transports.File({
                            filename: 'logs/error.log',
                            level: 'error',
                            format: winston_1.format.combine(winston_1.format.uncolorize(), winston_1.format.json()),
                        }),
                        new winston_1.transports.File({
                            filename: 'logs/combined.log',
                            format: winston_1.format.combine(winston_1.format.uncolorize(), winston_1.format.json()),
                        }),
                    ]
                    : []),
            ],
        });
    }
    formatLevel(level) {
        switch (level) {
            case 'error':
                return chalk_1.default.red.bold('ERROR');
            case 'warn':
                return chalk_1.default.yellow.bold(' WARN');
            case 'info':
                return chalk_1.default.blue.bold(' INFO');
            case 'debug':
                return chalk_1.default.magenta.bold('DEBUG');
            case 'verbose':
                return chalk_1.default.gray.bold(' VERB');
            default:
                return level.toUpperCase();
        }
    }
    log(message, context, emoji) {
        this.logger.info(message, { context: context || this.context, emoji });
    }
    error(message, trace, context, emoji = '❌') {
        this.logger.error(message, {
            context: context || this.context,
            emoji,
            trace,
        });
    }
    warn(message, context, emoji = '⚠️') {
        this.logger.warn(message, { context: context || this.context, emoji });
    }
    debug(message, context, emoji = '🔍') {
        this.logger.debug(message, { context: context || this.context, emoji });
    }
    verbose(message, context, emoji = '📝') {
        this.logger.verbose(message, { context: context || this.context, emoji });
    }
    success(message, context) {
        this.log(message, context, '✅');
    }
    start(message, context) {
        this.log(message, context, '🚀');
    }
    database(message, context) {
        this.debug(message, context, '💾');
    }
    auth(message, context) {
        this.log(message, context, '🔐');
    }
    security(message, context) {
        this.warn(message, context, '🛡️');
    }
    http(message, context) {
        this.log(message, context, '🌐');
    }
    user(message, context) {
        this.log(message, context, '👤');
    }
    seed(message, context) {
        this.log(message, context, '🌱');
    }
    cache(message, context) {
        this.debug(message, context, '⚡');
    }
    api(message, context) {
        this.log(message, context, '📡');
    }
};
exports.LoggerService = LoggerService;
exports.LoggerService = LoggerService = __decorate([
    (0, common_1.Injectable)({ scope: 2 }),
    __metadata("design:paramtypes", [String])
], LoggerService);
//# sourceMappingURL=logger.service.js.map
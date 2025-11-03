import { LoggerService as NestLoggerService } from '@nestjs/common';
export declare class LoggerService implements NestLoggerService {
    private logger;
    private context?;
    constructor(context?: string);
    private formatLevel;
    log(message: string, context?: string, emoji?: string): void;
    error(message: string, trace?: string, context?: string, emoji?: string): void;
    warn(message: string, context?: string, emoji?: string): void;
    debug(message: string, context?: string, emoji?: string): void;
    verbose(message: string, context?: string, emoji?: string): void;
    success(message: string, context?: string): void;
    start(message: string, context?: string): void;
    database(message: string, context?: string): void;
    auth(message: string, context?: string): void;
    security(message: string, context?: string): void;
    http(message: string, context?: string): void;
    user(message: string, context?: string): void;
    seed(message: string, context?: string): void;
    cache(message: string, context?: string): void;
    api(message: string, context?: string): void;
}

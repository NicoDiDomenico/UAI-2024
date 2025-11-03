import { CanActivate, ExecutionContext } from '@nestjs/common';
import { Reflector } from '@nestjs/core';
import { AuthorizationService } from '@common/services';
export declare class ActionsGuard implements CanActivate {
    private reflector;
    private authorizationService;
    private readonly logger;
    constructor(reflector: Reflector, authorizationService: AuthorizationService);
    canActivate(context: ExecutionContext): Promise<boolean>;
}

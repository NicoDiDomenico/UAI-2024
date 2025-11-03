import { createParamDecorator, ExecutionContext } from '@nestjs/common';
import { UserEntity } from '@infra/database/entities';
import { Request } from 'express';

/**
 * Request type with authenticated user
 */
interface RequestWithUser extends Request {
  user: UserEntity;
}

/**
 * Decorator para extraer el usuario autenticado del request
 *
 * @example
 * ```typescript
 * @Get('profile')
 * async getProfile(@CurrentUser() user: UserEntity) {
 *   return user;
 * }
 * ```
 */
export const CurrentUser = createParamDecorator(
  (data: unknown, ctx: ExecutionContext): UserEntity => {
    const request = ctx.switchToHttp().getRequest<RequestWithUser>();
    return request.user;
  },
);

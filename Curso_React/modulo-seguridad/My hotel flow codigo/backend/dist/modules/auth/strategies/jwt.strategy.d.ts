import { Strategy } from 'passport-jwt';
import { ConfigService } from '@nestjs/config';
import { TokenService } from '@common/services';
import { UsersService } from '@modules/users/users.service';
export interface JwtPayload {
    sub: number;
    username: string;
    email: string;
    jti: string;
    type: 'access' | 'refresh';
    iat: number;
    exp: number;
}
declare const JwtStrategy_base: new (...args: [opt: import("passport-jwt").StrategyOptionsWithRequest] | [opt: import("passport-jwt").StrategyOptionsWithoutRequest]) => Strategy & {
    validate(...args: any[]): unknown;
};
export declare class JwtStrategy extends JwtStrategy_base {
    private readonly configService;
    private readonly tokenService;
    private readonly usersService;
    private readonly logger;
    constructor(configService: ConfigService, tokenService: TokenService, usersService: UsersService);
    validate(payload: JwtPayload): Promise<import("../../../infra/database/entities").UserEntity>;
}
export {};

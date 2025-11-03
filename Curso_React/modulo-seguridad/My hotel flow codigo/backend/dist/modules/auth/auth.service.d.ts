import { ConfigService } from '@nestjs/config';
import { UsersService } from '@modules/users/users.service';
import { HashService, TokenService, AuthorizationService } from '@common/services';
import { LoginDto, RefreshDto, ChangePasswordDto, RecoverRequestDto, RecoverConfirmDto } from './dto';
import { UserEntity } from '@infra/database/entities';
export interface AuthTokens {
    accessToken: string;
    refreshToken: string;
}
export declare class AuthService {
    private readonly usersService;
    private readonly hashService;
    private readonly tokenService;
    private readonly authorizationService;
    private readonly configService;
    private readonly logger;
    constructor(usersService: UsersService, hashService: HashService, tokenService: TokenService, authorizationService: AuthorizationService, configService: ConfigService);
    validateUser(identity: string, password: string): Promise<UserEntity | null>;
    login(dto: LoginDto): Promise<AuthTokens>;
    refresh(dto: RefreshDto): Promise<AuthTokens>;
    logout(userId: number, accessToken: string, refreshToken: string): Promise<void>;
    changePassword(userId: number, dto: ChangePasswordDto): Promise<void>;
    recoverRequest(dto: RecoverRequestDto): Promise<void>;
    recoverConfirm(dto: RecoverConfirmDto): Promise<void>;
    getMe(userId: number): Promise<UserEntity>;
    getPermissions(userId: number): Promise<string[]>;
}

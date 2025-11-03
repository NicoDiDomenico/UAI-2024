import { AuthService } from './auth.service';
import { LoginDto, RefreshDto, ChangePasswordDto, RecoverRequestDto, RecoverConfirmDto } from './dto';
export declare class AuthController {
    private readonly authService;
    private readonly logger;
    constructor(authService: AuthService);
    login(dto: LoginDto): Promise<import("./auth.service").AuthTokens>;
    refresh(dto: RefreshDto): Promise<import("./auth.service").AuthTokens>;
    logout(req: any, body: {
        refreshToken: string;
    }): Promise<void>;
    changePassword(req: any, dto: ChangePasswordDto): Promise<void>;
    recoverRequest(dto: RecoverRequestDto): Promise<void>;
    recoverConfirm(dto: RecoverConfirmDto): Promise<void>;
    getMe(req: any): Promise<import("../../infra/database/entities").UserEntity>;
    getPermissions(req: any): Promise<string[]>;
}

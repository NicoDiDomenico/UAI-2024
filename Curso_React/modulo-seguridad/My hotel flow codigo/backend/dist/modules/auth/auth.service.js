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
var AuthService_1;
Object.defineProperty(exports, "__esModule", { value: true });
exports.AuthService = void 0;
const common_1 = require("@nestjs/common");
const config_1 = require("@nestjs/config");
const users_service_1 = require("../users/users.service");
const services_1 = require("../../common/services");
const logger_service_1 = require("../../common/logger/logger.service");
const crypto_1 = require("crypto");
let AuthService = AuthService_1 = class AuthService {
    usersService;
    hashService;
    tokenService;
    authorizationService;
    configService;
    logger = new logger_service_1.LoggerService(AuthService_1.name);
    constructor(usersService, hashService, tokenService, authorizationService, configService) {
        this.usersService = usersService;
        this.hashService = hashService;
        this.tokenService = tokenService;
        this.authorizationService = authorizationService;
        this.configService = configService;
    }
    async validateUser(identity, password) {
        let user = await this.usersService.findByEmail(identity);
        if (!user) {
            user = await this.usersService.findByUsername(identity);
        }
        if (!user) {
            return null;
        }
        const isLocked = await this.usersService.isLocked(user.id);
        if (isLocked) {
            this.logger.security(`Login attempt blocked: Account ${identity} is locked`);
            throw new common_1.UnauthorizedException('Account is locked due to too many failed login attempts. Please try again later.');
        }
        if (!user.isActive) {
            this.logger.warn(`Login attempt for inactive account: ${identity}`);
            throw new common_1.UnauthorizedException('Account is disabled');
        }
        const isValidPassword = await this.hashService.verify(user.passwordHash, password);
        if (!isValidPassword) {
            await this.usersService.incrementFailedAttempts(user.id);
            this.logger.warn(`Failed login attempt for user: ${identity} (attempts increased)`);
            return null;
        }
        await this.usersService.resetFailedAttempts(user.id);
        this.logger.debug(`Password verified successfully for: ${identity}`);
        return user;
    }
    async login(dto) {
        const user = await this.validateUser(dto.identity, dto.password);
        if (!user) {
            throw new common_1.UnauthorizedException('Invalid credentials');
        }
        const tokens = await this.tokenService.generateTokenPair(user.id, user.username, user.email);
        this.logger.auth(`Login successful: ${user.username} (${user.email}) - ID: ${user.id}`);
        return tokens;
    }
    async refresh(dto) {
        try {
            const payload = await this.tokenService.verifyToken(dto.refreshToken, 'refresh');
            const isRevoked = await this.tokenService.isRevoked(payload.jti);
            if (isRevoked) {
                throw new common_1.UnauthorizedException('Token has been revoked');
            }
            await this.tokenService.revokeToken(payload.jti, payload.sub, 'refresh', 'Token rotation');
            const user = await this.usersService.findOne(payload.sub);
            if (!user.isActive) {
                this.logger.warn(`Refresh attempt for inactive user: ${payload.username}`);
                throw new common_1.UnauthorizedException('Account is disabled');
            }
            const tokens = await this.tokenService.generateTokenPair(user.id, user.username, user.email);
            this.logger.auth(`Tokens refreshed successfully: ${user.username} (ID: ${user.id})`);
            return tokens;
        }
        catch (error) {
            this.logger.error(`Refresh token failed: ${error.message}`, '');
            throw new common_1.UnauthorizedException('Invalid or expired refresh token');
        }
    }
    async logout(userId, accessToken, refreshToken) {
        try {
            const accessPayload = await this.tokenService.verifyToken(accessToken, 'access');
            await this.tokenService.revokeToken(accessPayload.jti, userId, 'access', 'User logout');
            const refreshPayload = await this.tokenService.verifyToken(refreshToken, 'refresh');
            await this.tokenService.revokeToken(refreshPayload.jti, userId, 'refresh', 'User logout');
            this.logger.auth(`Logout successful: User ID ${userId}`);
        }
        catch (error) {
            this.logger.error(`Logout error: ${error.message}`, '');
        }
    }
    async changePassword(userId, dto) {
        const user = await this.usersService.findOne(userId);
        const isValidPassword = await this.hashService.verify(user.passwordHash, dto.currentPassword);
        if (!isValidPassword) {
            throw new common_1.UnauthorizedException('Current password is incorrect');
        }
        if (dto.currentPassword === dto.newPassword) {
            throw new common_1.BadRequestException('New password must be different from current password');
        }
        await this.usersService.resetPassword(userId, {
            newPassword: dto.newPassword,
        });
        this.logger.log(`User ${userId} changed password`);
    }
    async recoverRequest(dto) {
        const user = await this.usersService.findByEmail(dto.email);
        if (!user) {
            this.logger.warn(`Password recovery requested for non-existent email: ${dto.email}`);
            return;
        }
        const resetToken = (0, crypto_1.randomBytes)(32).toString('hex');
        const resetExpires = new Date(Date.now() + 60 * 60 * 1000);
        user.passwordResetToken = resetToken;
        user.passwordResetExpires = resetExpires;
        await this.usersService['userRepo'].save(user);
        this.logger.log(`Password recovery token for ${user.email}: ${resetToken}`);
    }
    async recoverConfirm(dto) {
        const user = await this.usersService['userRepo'].findOne({
            where: { passwordResetToken: dto.token },
        });
        if (!user) {
            throw new common_1.BadRequestException('Invalid recovery token');
        }
        if (!user.passwordResetExpires || user.passwordResetExpires < new Date()) {
            throw new common_1.BadRequestException('Recovery token has expired');
        }
        await this.usersService.resetPassword(user.id, {
            newPassword: dto.newPassword,
        });
        user.passwordResetToken = undefined;
        user.passwordResetExpires = undefined;
        await this.usersService['userRepo'].save(user);
        this.logger.log(`Password recovered for user ${user.username}`);
    }
    async getMe(userId) {
        return await this.usersService.findOne(userId);
    }
    async getPermissions(userId) {
        const actions = await this.authorizationService.getEffectiveActions(userId);
        return Array.from(actions);
    }
};
exports.AuthService = AuthService;
exports.AuthService = AuthService = AuthService_1 = __decorate([
    (0, common_1.Injectable)(),
    __metadata("design:paramtypes", [users_service_1.UsersService,
        services_1.HashService,
        services_1.TokenService,
        services_1.AuthorizationService,
        config_1.ConfigService])
], AuthService);
//# sourceMappingURL=auth.service.js.map
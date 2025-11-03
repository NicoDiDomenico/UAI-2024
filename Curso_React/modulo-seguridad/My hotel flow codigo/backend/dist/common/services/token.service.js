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
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.TokenService = void 0;
const common_1 = require("@nestjs/common");
const config_1 = require("@nestjs/config");
const jwt_1 = require("@nestjs/jwt");
const typeorm_1 = require("@nestjs/typeorm");
const typeorm_2 = require("typeorm");
const entities_1 = require("../../infra/database/entities");
const uuid_1 = require("uuid");
let TokenService = class TokenService {
    jwtService;
    config;
    revokedTokenRepo;
    constructor(jwtService, config, revokedTokenRepo) {
        this.jwtService = jwtService;
        this.config = config;
        this.revokedTokenRepo = revokedTokenRepo;
    }
    generateTokenPair(userId, username, email) {
        const accessJti = (0, uuid_1.v4)();
        const refreshJti = (0, uuid_1.v4)();
        const accessPayload = {
            sub: userId,
            username,
            email,
            jti: accessJti,
            type: 'access',
        };
        const refreshPayload = {
            sub: userId,
            username,
            email,
            jti: refreshJti,
            type: 'refresh',
        };
        const accessExpiration = this.config.get('jwt.accessExpiration') || '15m';
        const refreshExpiration = this.config.get('jwt.refreshExpiration') || '7d';
        const accessToken = this.jwtService.sign(accessPayload, { expiresIn: accessExpiration });
        const refreshToken = this.jwtService.sign(refreshPayload, { expiresIn: refreshExpiration });
        const expiresIn = this.parseExpirationToSeconds(accessExpiration);
        return {
            accessToken,
            refreshToken,
            expiresIn,
        };
    }
    async verifyToken(token, expectedType) {
        try {
            const payload = this.jwtService.verify(token);
            if (await this.isRevoked(payload.jti)) {
                throw new common_1.UnauthorizedException('Token has been revoked');
            }
            return payload;
        }
        catch (error) {
            if (error.message === 'Token has been revoked') {
                throw error;
            }
            throw new common_1.UnauthorizedException('Invalid or expired token');
        }
    }
    async revokeToken(jti, userId, tokenType, reason, ip) {
        const expiration = tokenType === 'access' ? '15m' : '7d';
        const expiresAt = this.calculateExpirationDate(expiration);
        const revokedToken = this.revokedTokenRepo.create({
            jti,
            userId,
            tokenType,
            reason,
            expiresAt,
            ip,
        });
        await this.revokedTokenRepo.save(revokedToken);
    }
    async isRevoked(jti) {
        const revoked = await this.revokedTokenRepo.findOne({
            where: { jti },
        });
        return revoked !== null;
    }
    async cleanExpiredTokens() {
        const result = await this.revokedTokenRepo
            .createQueryBuilder()
            .delete()
            .where('expiresAt < :now', { now: new Date() })
            .execute();
        return result.affected || 0;
    }
    parseExpirationToSeconds(expiration) {
        const value = parseInt(expiration.slice(0, -1));
        const unit = expiration.slice(-1);
        switch (unit) {
            case 's':
                return value;
            case 'm':
                return value * 60;
            case 'h':
                return value * 3600;
            case 'd':
                return value * 86400;
            default:
                return 900;
        }
    }
    calculateExpirationDate(expiration) {
        const seconds = this.parseExpirationToSeconds(expiration);
        return new Date(Date.now() + seconds * 1000);
    }
};
exports.TokenService = TokenService;
exports.TokenService = TokenService = __decorate([
    (0, common_1.Injectable)(),
    __param(2, (0, typeorm_1.InjectRepository)(entities_1.RevokedTokenEntity)),
    __metadata("design:paramtypes", [jwt_1.JwtService,
        config_1.ConfigService,
        typeorm_2.Repository])
], TokenService);
//# sourceMappingURL=token.service.js.map
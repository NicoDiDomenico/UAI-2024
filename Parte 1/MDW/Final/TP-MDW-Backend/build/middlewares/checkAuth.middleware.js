"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.checkAuth = void 0;
const jsonwebtoken_1 = __importDefault(require("jsonwebtoken"));
const errorHandler_middleware_1 = require("./errorHandler.middleware");
const env_1 = require("../config/env");
// Este si dale bola
// Recibo del front el token, chequeo que tiene adentro, y lo paso al handler
const checkAuth = (req, res, next) => {
    var _a;
    const token = (_a = req.headers.authorization) === null || _a === void 0 ? void 0 : _a.split(" ")[1];
    if (!token)
        return next(new errorHandler_middleware_1.ErrorResponse("Usuario no autorizado", 401));
    try {
        const decodedToken = jsonwebtoken_1.default.verify(token, env_1.JWT_TOKEN);
        req.user = decodedToken.id; // Añade el ID del usuario a req.user --> Se usa en user.controller.ts
        next();
    }
    catch (error) {
        return next(new errorHandler_middleware_1.ErrorResponse("Usuario no autorizado", 401));
    }
};
exports.checkAuth = checkAuth;

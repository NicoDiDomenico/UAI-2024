"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.authenticateToken = void 0;
const jsonwebtoken_1 = __importDefault(require("jsonwebtoken"));
const env_1 = require("../config/env");
// Lo mandamos a otro lado a esto.
const authenticateToken = (req, res, next) => {
    const authHeader = req.headers["authorization"];
    const token = authHeader && authHeader.split(" ")[1];
    if (!token)
        return res.sendStatus(401);
    jsonwebtoken_1.default.verify(token, env_1.JWT_TOKEN, (error, user) => {
        if (error)
            return res.sendStatus(401);
        req.user = user;
    });
};
exports.authenticateToken = authenticateToken;

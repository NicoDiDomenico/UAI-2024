export const errorMiddleware = (err, _req, res, _next) => {
    console.error("Error global:", err);
    res.status(500).json({
        error: "Error interno del servidor",
        message: err.message,
    });
};
//# sourceMappingURL=error.middleware.js.map
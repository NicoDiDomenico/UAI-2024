"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.updateIsPinned = exports.deleteTodo = exports.getTodo = exports.getEveryTodo = exports.editTodo = exports.createTodo = void 0;
const todo_model_1 = require("../models/todo.model");
const errorHandler_middleware_1 = require("../middlewares/errorHandler.middleware");
const createTodo = (req, res, next) => __awaiter(void 0, void 0, void 0, function* () {
    const { title, description, tags } = req.body;
    try {
        const todo = yield todo_model_1.Todo.create({
            title,
            description,
            tags,
            user: req.user,
        });
        res.status(200).json({
            error: false,
            message: "Nota creada correctamente",
            todo,
        });
    }
    catch (error) {
        next(error);
    }
});
exports.createTodo = createTodo;
const editTodo = (req, res, next) => __awaiter(void 0, void 0, void 0, function* () {
    const { title, description, tags, isPinned } = req.body;
    const { id } = req.params;
    try {
        const todo = yield todo_model_1.Todo.findOne({ _id: id, user: req.user });
        if (!todo)
            return next(new errorHandler_middleware_1.ErrorResponse("Nota no encontrada", 404));
        if (title)
            todo.title = title;
        if (description)
            todo.description = description;
        if (tags)
            todo.tags = tags;
        if (isPinned)
            todo.isPinned = isPinned;
        yield todo.save();
        res.status(200).json({
            error: false,
            message: "Nota editada correctamente",
            todo,
        });
    }
    catch (error) {
        next(error);
    }
});
exports.editTodo = editTodo;
const getEveryTodo = (req, res, next) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const todos = yield todo_model_1.Todo.find({
            user: req.user,
        }).sort({
            isPinned: -1,
        });
        res.status(200).json({
            error: false,
            todos,
        });
    }
    catch (error) {
        next(error);
    }
});
exports.getEveryTodo = getEveryTodo;
const getTodo = (req, res, next) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const todo = yield todo_model_1.Todo.findById(req.params.id);
        res.status(200).json({
            error: false,
            todo,
        });
    }
    catch (error) {
        next(error);
    }
});
exports.getTodo = getTodo;
const deleteTodo = (req, res, next) => __awaiter(void 0, void 0, void 0, function* () {
    const id = req.params.id;
    try {
        const todo = yield todo_model_1.Todo.findOne({ _id: id, user: req.user });
        if (!todo)
            return next(new errorHandler_middleware_1.ErrorResponse("Nota no encontrada", 404));
        yield todo_model_1.Todo.deleteOne({ _id: id, user: req.user });
        res.status(200).json({
            error: false,
            message: "Nota eliminada correctamente",
        });
    }
    catch (error) {
        next(error);
    }
});
exports.deleteTodo = deleteTodo;
const updateIsPinned = (req, res, next) => __awaiter(void 0, void 0, void 0, function* () {
    const { isPinned } = req.body;
    const { id } = req.params;
    try {
        const todo = yield todo_model_1.Todo.findOne({ _id: id, user: req.user });
        if (!todo)
            return next(new errorHandler_middleware_1.ErrorResponse("Nota no encontrada", 404));
        if (isPinned)
            todo.isPinned = isPinned || false;
        yield todo.save();
        res.status(200).json({
            error: false,
            message: "Nota editada correctamente",
            todo,
        });
    }
    catch (error) {
        next(error);
    }
});
exports.updateIsPinned = updateIsPinned;

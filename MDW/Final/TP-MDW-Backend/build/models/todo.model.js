"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Todo = void 0;
const mongoose_1 = require("mongoose");
const TodoSchema = new mongoose_1.Schema({
    title: { type: String, required: true },
    description: { type: String },
    completed: { type: Boolean, default: false },
    tags: { type: [String], default: [] },
    isPinned: { type: Boolean, default: false },
    createdOn: { type: Date, default: new Date().getTime() },
    user: { type: mongoose_1.Schema.Types.ObjectId, ref: "User", required: true },
});
exports.Todo = (0, mongoose_1.model)("Todo", TodoSchema);

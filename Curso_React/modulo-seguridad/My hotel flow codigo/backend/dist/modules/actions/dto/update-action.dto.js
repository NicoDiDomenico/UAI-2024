"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UpdateActionDto = void 0;
const swagger_1 = require("@nestjs/swagger");
const create_action_dto_1 = require("./create-action.dto");
class UpdateActionDto extends (0, swagger_1.PartialType)(create_action_dto_1.CreateActionDto) {
}
exports.UpdateActionDto = UpdateActionDto;
//# sourceMappingURL=update-action.dto.js.map
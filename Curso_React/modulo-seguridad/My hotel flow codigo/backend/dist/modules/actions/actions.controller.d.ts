import { ActionsService } from './actions.service';
import { CreateActionDto, UpdateActionDto } from './dto';
export declare class ActionsController {
    private readonly actionsService;
    constructor(actionsService: ActionsService);
    create(createActionDto: CreateActionDto): Promise<import("../../infra/database/entities").ActionEntity>;
    findAll(): Promise<import("../../infra/database/entities").ActionEntity[]>;
    findByArea(area: string): Promise<import("../../infra/database/entities").ActionEntity[]>;
    findOne(id: number): Promise<import("../../infra/database/entities").ActionEntity>;
    update(id: number, updateActionDto: UpdateActionDto): Promise<import("../../infra/database/entities").ActionEntity>;
    remove(id: number): Promise<void>;
    seed(): Promise<{
        message: string;
    }>;
}

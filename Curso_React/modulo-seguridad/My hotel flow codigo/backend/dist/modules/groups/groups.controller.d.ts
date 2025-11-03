import { GroupsService } from './groups.service';
import { CreateGroupDto, UpdateGroupDto, SetGroupActionsDto, SetGroupChildrenDto } from './dto';
export declare class GroupsController {
    private readonly groupsService;
    constructor(groupsService: GroupsService);
    create(createGroupDto: CreateGroupDto): Promise<import("../../infra/database/entities").GroupEntity>;
    findAll(): Promise<import("../../infra/database/entities").GroupEntity[]>;
    findOne(id: number): Promise<import("../../infra/database/entities").GroupEntity>;
    getEffectiveActions(id: number): Promise<{
        actions: string[];
    }>;
    update(id: number, updateGroupDto: UpdateGroupDto): Promise<import("../../infra/database/entities").GroupEntity>;
    remove(id: number): Promise<void>;
    setActions(id: number, dto: SetGroupActionsDto): Promise<import("../../infra/database/entities").GroupEntity>;
    setChildren(id: number, dto: SetGroupChildrenDto): Promise<import("../../infra/database/entities").GroupEntity>;
    seed(): Promise<{
        message: string;
    }>;
}

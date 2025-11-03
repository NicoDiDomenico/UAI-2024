import { Repository } from 'typeorm';
import { GroupEntity } from '@infra/database/entities';
import { ActionsService } from '@modules/actions/actions.service';
import { CreateGroupDto, UpdateGroupDto, SetGroupActionsDto, SetGroupChildrenDto } from './dto';
export declare class GroupsService {
    private readonly groupRepo;
    private readonly actionsService;
    private readonly logger;
    constructor(groupRepo: Repository<GroupEntity>, actionsService: ActionsService);
    create(dto: CreateGroupDto): Promise<GroupEntity>;
    findAll(): Promise<GroupEntity[]>;
    findOne(id: number): Promise<GroupEntity>;
    findByKey(key: string, loadRelations?: boolean): Promise<GroupEntity | null>;
    update(id: number, dto: UpdateGroupDto): Promise<GroupEntity>;
    remove(id: number): Promise<void>;
    setActions(id: number, dto: SetGroupActionsDto): Promise<GroupEntity>;
    setChildren(id: number, dto: SetGroupChildrenDto): Promise<GroupEntity>;
    private wouldCreateCycle;
    getEffectiveActions(groupId: number): Promise<Set<string>>;
    private collectActions;
    seed(): Promise<void>;
    private assignInitialActions;
}

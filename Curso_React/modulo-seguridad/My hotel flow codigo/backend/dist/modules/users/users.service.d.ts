import { Repository } from 'typeorm';
import { UserEntity, ActionEntity } from '@infra/database/entities';
import { HashService } from '@common/services';
import { ActionsService } from '@modules/actions/actions.service';
import { GroupsService } from '@modules/groups/groups.service';
import { CreateUserDto, UpdateUserDto, SetUserGroupsDto, SetUserActionsDto, ResetPasswordDto, FindAllUsersDto } from './dto';
export declare class UsersService {
    private readonly userRepo;
    private readonly hashService;
    private readonly actionsService;
    private readonly groupsService;
    private readonly logger;
    private readonly LOCKOUT_THRESHOLD;
    private readonly LOCKOUT_DURATION;
    constructor(userRepo: Repository<UserEntity>, hashService: HashService, actionsService: ActionsService, groupsService: GroupsService);
    create(dto: CreateUserDto): Promise<UserEntity>;
    findAll(dto: FindAllUsersDto): Promise<{
        data: UserEntity[];
        pagination: {
            page: number;
            limit: number;
            total: number;
            totalPages: number;
            hasNextPage: boolean;
            hasPreviousPage: boolean;
        };
    }>;
    findOne(id: number): Promise<UserEntity>;
    getInheritedActions(userId: number): Promise<ActionEntity[]>;
    findByEmail(email: string): Promise<UserEntity | null>;
    findByUsername(username: string): Promise<UserEntity | null>;
    update(id: number, dto: UpdateUserDto): Promise<UserEntity>;
    remove(id: number): Promise<void>;
    setGroups(id: number, dto: SetUserGroupsDto): Promise<UserEntity>;
    setActions(id: number, dto: SetUserActionsDto): Promise<UserEntity>;
    resetPassword(id: number, dto: ResetPasswordDto): Promise<void>;
    isLocked(userId: number): Promise<boolean>;
    incrementFailedAttempts(userId: number): Promise<void>;
    resetFailedAttempts(userId: number): Promise<void>;
    seed(): Promise<void>;
    private assignInitialGroups;
}

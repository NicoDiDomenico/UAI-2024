import { UsersService } from './users.service';
import { CreateUserDto, UpdateUserDto, SetUserGroupsDto, SetUserActionsDto, ResetPasswordDto, FindAllUsersDto } from './dto';
export declare class UsersController {
    private readonly usersService;
    private readonly logger;
    constructor(usersService: UsersService);
    create(dto: CreateUserDto): Promise<import("../../infra/database/entities").UserEntity>;
    findAll(query: FindAllUsersDto): Promise<{
        data: import("../../infra/database/entities").UserEntity[];
        pagination: {
            page: number;
            limit: number;
            total: number;
            totalPages: number;
            hasNextPage: boolean;
            hasPreviousPage: boolean;
        };
    }>;
    findOne(id: number): Promise<import("../../infra/database/entities").UserEntity>;
    getInheritedActions(id: number): Promise<import("../../infra/database/entities").ActionEntity[]>;
    update(id: number, dto: UpdateUserDto): Promise<import("../../infra/database/entities").UserEntity>;
    remove(id: number): Promise<void>;
    setGroups(id: number, dto: SetUserGroupsDto): Promise<import("../../infra/database/entities").UserEntity>;
    setActions(id: number, dto: SetUserActionsDto): Promise<import("../../infra/database/entities").UserEntity>;
    resetPassword(id: number, dto: ResetPasswordDto): Promise<void>;
    seed(): Promise<void>;
}

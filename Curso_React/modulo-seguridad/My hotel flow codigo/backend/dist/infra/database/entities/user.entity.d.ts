import { GroupEntity } from './group.entity';
import { ActionEntity } from './action.entity';
export declare class UserEntity {
    id: number;
    username: string;
    email: string;
    passwordHash: string;
    fullName?: string;
    role: string;
    isActive: boolean;
    lastLoginAt?: Date;
    failedLoginAttempts: number;
    lockedUntil?: Date;
    groups: GroupEntity[];
    actions: ActionEntity[];
    passwordResetToken?: string;
    passwordResetExpires?: Date;
    createdAt: Date;
    updatedAt: Date;
}

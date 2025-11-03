import { ActionEntity } from './action.entity';
export declare class GroupEntity {
    id: number;
    key: string;
    name: string;
    description?: string;
    actions: ActionEntity[];
    children?: GroupEntity[];
    createdAt: Date;
    updatedAt: Date;
}

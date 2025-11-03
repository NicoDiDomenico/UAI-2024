import { Repository } from 'typeorm';
import { ActionEntity } from '@infra/database/entities';
import { CreateActionDto, UpdateActionDto } from './dto';
export declare class ActionsService {
    private readonly actionRepo;
    private readonly logger;
    constructor(actionRepo: Repository<ActionEntity>);
    create(dto: CreateActionDto): Promise<ActionEntity>;
    findAll(): Promise<ActionEntity[]>;
    findOne(id: number): Promise<ActionEntity>;
    findByKey(key: string): Promise<ActionEntity | null>;
    findByKeys(keys: string[]): Promise<ActionEntity[]>;
    findByArea(area: string): Promise<ActionEntity[]>;
    update(id: number, dto: UpdateActionDto): Promise<ActionEntity>;
    remove(id: number): Promise<void>;
    private extractArea;
    seed(): Promise<void>;
}

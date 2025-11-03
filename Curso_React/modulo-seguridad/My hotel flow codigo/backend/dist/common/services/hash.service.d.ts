import { ConfigService } from '@nestjs/config';
export declare class HashService {
    private config;
    private readonly options;
    constructor(config: ConfigService);
    hash(password: string): Promise<string>;
    verify(hash: string, password: string): Promise<boolean>;
    needsRehash(hash: string): boolean;
}

export declare class GymService {
    getAllGyms(): Promise<{
        gymId: number;
        nombre: string;
    }[]>;
    getGymById(gymId: number): Promise<{
        gymId: number;
        nombre: string;
    } | null>;
}
//# sourceMappingURL=gym.service.d.ts.map
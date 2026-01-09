import { prisma } from "../prisma.js";
export class GymService {
    async getAllGyms() {
        return await prisma.gym.findMany({
            select: {
                gymId: true,
                nombre: true,
            },
            orderBy: {
                nombre: "asc",
            },
        });
    }
    async getGymById(gymId) {
        return await prisma.gym.findUnique({
            where: { gymId },
            select: {
                gymId: true,
                nombre: true,
            },
        });
    }
}
//# sourceMappingURL=gym.service.js.map
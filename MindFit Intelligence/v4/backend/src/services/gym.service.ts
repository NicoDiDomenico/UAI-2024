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

  async getGymById(gymId: number) {
    return await prisma.gym.findUnique({
      where: { gymId },
      select: {
        gymId: true,
        nombre: true,
      },
    });
  }
}

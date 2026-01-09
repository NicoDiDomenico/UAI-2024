import type { Request, Response } from "express";
import { GymService } from "../services/gym.service.js";

const gymService = new GymService();

export const getGyms = async (_req: Request, res: Response) => {
  try {
    const gyms = await gymService.getAllGyms();
    res.json({ gyms });
  } catch (error) {
    console.error("Error al obtener gimnasios:", error);
    res.status(500).json({ error: "Error al obtener gimnasios" });
  }
};

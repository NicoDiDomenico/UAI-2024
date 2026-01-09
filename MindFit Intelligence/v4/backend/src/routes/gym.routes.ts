import { Router } from "express";
import { getGyms } from "../controllers/gym.controller.js";

const router = Router();

router.get("/", getGyms);

export default router;

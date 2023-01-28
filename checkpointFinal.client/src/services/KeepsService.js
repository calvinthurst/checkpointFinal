import { AppState } from "../AppState.js"
import { Keeps } from "../models/Keeps.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class KeepsService {
  async GetKeeps() {
    const res = await api.get("api/keeps")
    AppState.keeps = res.data.map(k => new Keeps(k))
    logger.log(AppState.keeps, "the keeps sir")
  }
}
export const keepsService = new KeepsService()
import { AppState } from "../AppState.js"
import { Keeps } from "../models/Keeps.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class KeepsService {
  async getKeepsByVaultId(vaultId) {
    const res = await api.get('api/vaults/' + vaultId + '/keeps')
    AppState.keeps = res.data.map(k => new Keeps(k))
    logger.log(AppState.keeps)
  }
  async GetKeeps() {
    const res = await api.get("api/keeps")
    AppState.keeps = res.data.map(k => new Keeps(k))
    logger.log(AppState.keeps, "the keeps sir")
  }

  async setKeepActive(id) {
    const res = await api.get('api/keeps/' + id)
    AppState.keep = new Keeps(res.data)
    logger.log(AppState.keep)
  }
  async createKeep(formData) {
    const res = await api.post('api/keeps', formData)
    const keep = new Keeps(res.data)
    AppState.keeps.push(keep)
  }

  async deleteKeep(id) {
    await api.delete('api/keeps/' + id)
    AppState.keeps = AppState.keeps.filter(k => k.id !== id)
    AppState.myKeeps = AppState.myKeeps.filter(k => k.id !== id)
  }
}
export const keepsService = new KeepsService()
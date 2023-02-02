import { AppState } from "../AppState.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {
  async addToVault(vaultId, keepId) {
    const vaultKeep = { vaultId: vaultId, keepId: keepId }
    const res = await api.post('api/vaultkeeps', vaultKeep)
    logger.log(res.data)
    AppState.vaultKeeps.unshift(new VaultKeep(res.data))
    AppState.vaultKeeps = AppState.vaultKeeps
    AppState.keep.kept += 1
  }
  async removeFromVault(vaultKeepId, keepId) {

    debugger
    const res = await api.delete('api/vaultkeeps/' + vaultKeepId)
    AppState.KeepsInVault = AppState.KeepsInVault.filter(vk => vk.id != keepId)
  }
}

export const vaultKeepsService = new VaultKeepsService()
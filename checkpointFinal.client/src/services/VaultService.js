import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultService {

  async getMyVaults() {
    const res = await api.get("account/vaults")
    AppState.myVaults = res.data.map(v => new Vault(v))
    logger.log(AppState.myVaults)
  }
  async activateVault(vaultId) {
    const res = await api.get('api/vaults/' + vaultId)
    AppState.vault = new Vault(res.data)
  }
  async createVault(formData) {
    const res = await api.post('api/vaults', formData)
    AppState.myVaults.push(new Vault(res.data))
  }
  async deleteVault(vaultId) {
    await api.delete('api/vaults/' + vaultId)
    AppState.myVaults = AppState.myVaults.filter(v => v.id !== vaultId)
  }
}

export const vaultService = new VaultService()
import { AppState } from "../AppState.js"
import { Account } from "../models/Account.js"
import { Keeps } from "../models/Keeps.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class ProfileService {
  async getProfile(profileId) {
    const res = await api.get('api/profiles/' + profileId)
    AppState.activeUser = new Account(res.data)
    logger.log(AppState.account)
  }
  async getProfileKeeps(profileId) {
    const res = await api.get('api/profiles/' + profileId + '/keeps')
    AppState.keeps = res.data.map(k => new Keeps(k))
    logger.log(AppState.keeps)
  }
  async getProfileVaults(profileId) {
    const res = await api.get('api/profiles/' + profileId + '/vaults')
    AppState.vaults = res.data.map(v => new Vault(v))
    logger.log(AppState.vaults)
  }
}
export const profileService = new ProfileService()
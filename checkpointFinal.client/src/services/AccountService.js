import { AppState } from '../AppState'
import { Keeps } from "../models/Keeps.js"
import { Vault } from "../models/Vault.js"
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }


  async getMyVaults() {
    const res = await api.get('account/vaults')
    AppState.myVaults = res.data.map(v => new Vault(v))

  }

  async getMyKeeps() {
    const res = await api.get('api/profiles/' + AppState.account.id + '/keeps')
    AppState.myKeeps = res.data.map(k => new Keeps(k))
  }
  async editAccount(newAccount) {
    const res = await api.put('/account', newAccount)
    AppState.account = res.data

  }
}

export const accountService = new AccountService()

import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: null,
  /** @type {import('./models/Keeps.js').Keeps[]} */
  keeps: [],
  vaults: [],
  vaultKeeps: [],
  /** @type {import('./models/Keeps.js').Keeps} */
  keep: null,
  vault: {},
  myVaults: null,
  myKeeps: [],
  activeUser: {}
})

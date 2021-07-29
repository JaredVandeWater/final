import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  keeps: [],
  account: {},
  activeProfile: {},
  activeKeep: {},
  activeVault: {},
  myVaults: [],
  vaults: [],
  activeVK: {}
})

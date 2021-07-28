import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultsService {
  async getAllVaultsByProfileId(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.vaults = res.data
  }

  async getVault(id) {
    const res = await api.get(`api/vaults/${id}`)
    AppState.activeVault = res.data
  }

  async deleteVault(id) {
    const res = await api.delete(`api/vaults/${id}`)
    AppState.vaults = AppState.vaults.filter(v => v.id !== res.data.id)
  }
}

export const vaultsService = new VaultsService()

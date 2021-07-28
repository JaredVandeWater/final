import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultsService {
  async getAllVaultsByProfileId(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.keeps = res.data
  }
}

export const vaultsService = new VaultsService()

import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultsService {
  async getMyVaults(id) {
    const res = await api.get(`api/profile/${id}/vaults`)
    AppState.keeps = res.data
  }

  async getOneKeep(id) {
    const res = await api.get(`api/keeps/${id}`)
    AppState.activeKeep = res.data
  }
}

export const vaultsService = new VaultsService()

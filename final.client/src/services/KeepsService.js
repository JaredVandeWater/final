import { AppState } from '../AppState'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
  }

  async getOneKeep(id) {
    const res = await api.get(`api/keeps/${id}`)
    AppState.activeKeep = res.data
  }

  async deleteKeep(id, site, profId) {
    const res = await api.delete(`api/keeps/${id}`)
    AppState.keeps = AppState.keeps.filter(k => k.id !== res.data.id)
    if (site === 'Profile') {
      this.getAllKeepsByProfileId(profId)
    }
    if (site === 'Home') {
      this.getAllKeeps()
    }
  }

  async getAllKeepsByVaultId(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    AppState.keeps = res.data
  }

  async getAllKeepsByProfileId(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.keeps = res.data
  }

  async createKeep(data) {
    const res = await api.post('api/keeps', data)
    AppState.keeps = [res.data, ...AppState.keeps]
  }

  async addKeeptoVault(data) {
    await api.post('api/vaultKeeps', data)
  }

  async removeVaultKeep(id, vaultId) {
    await api.delete(`api/vaultkeeps/${id}`)
    this.getAllKeepsByVaultId(vaultId)
  }
}

//   async keepCountDown(id){
//     await api.put(`api/`)
//   }
// }

export const keepsService = new KeepsService()

import { AppState } from '../AppState'
import { api } from './AxiosService'

class ProfilesService {
  async getProfile(id) {
    const res = await api.get(`api/profile/${id}`)
    AppState.activeProfile = res.data
  }
}

export const profilesService = new ProfilesService()

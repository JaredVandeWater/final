import { AppState } from '../AppState'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    try {
      const res = await api.get('api/keeps')
      AppState.keeps = res.data
    } catch (error) {

    }
  }
}

export const keepsService = new KeepsService()

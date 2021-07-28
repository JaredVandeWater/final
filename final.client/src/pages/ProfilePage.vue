<template>
  <KeepModal />
  <div v-if="state.profile && state.vaults && state.keeps" class="container-fluid">
    <div class="row fixed-top">
      <Navbar />
    </div>
    <div class="navspacer"></div>
    <div class="row">
      <div class="mx-4">
        <img :src="state.profile.picture" alt="Profile">
      </div>
      <div>
        <h1>{{ state.profile.name }}</h1>
        <h4>Keeps: {{ state.keeps.length }}</h4>
        <h4>Vaults: {{ state.vaults.length }}</h4>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { computed, onMounted } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'
import { profilesService } from '../services/ProfilesService'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'

export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    const state = reactive({
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults)

    })
    onMounted(async() => {
      window.scrollTo({ top: 0 })
      AppState.activeProfile = {}
      AppState.keeps = {}
      try {
        await profilesService.getProfile(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
      try {
        await keepsService.getAllKeepsByProfileId(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
      try {
        await vaultsService.getAllVaultsByProfileId(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      state

    }
  }
}
</script>

<style lang="scss" scoped>
.navspacer{
  min-height: 90px;
}

</style>

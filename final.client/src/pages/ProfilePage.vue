<template>
  <KeepModal />
  <div class="container-fluid">
    <div class="row fixed-top">
      <Navbar />
    </div>
    <div class="navspacer"></div>
    <div v-if="!state.keeps">
      Loading...
    </div>
    <div v-else class="card-columns">
      <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
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
import { useRoute } from 'vue-router'

export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    const state = reactive({
      Profile: computed(() => AppState.activeProfile)
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
    })
    return {
      state

    }
  }
}
</script>

<style lang="scss" scoped>

</style>

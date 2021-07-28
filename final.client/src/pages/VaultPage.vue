<template>
  <div v-if="state.account && state.profile && state.keeps" class="container-fluid">
    <div class="row fixed-top">
      <Navbar />
    </div>
    <div class="navspacer"></div>

    <div class="row m-4">
      <h1> Keeps </h1>
    </div>

    <div class="row">
      <div>
        <h1>{{ state.profile.name }}</h1>
        <h4>Keeps: {{ state.keeps.length }}</h4>
      </div>
    </div>
    <div class="card-columns">
      <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { computed, watchEffect } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'

import { useRoute } from 'vue-router'

export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    const state = reactive({
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      account: computed(() => AppState.account)

    })

    watchEffect(async() => {
      window.scrollTo({ top: 0 })
      AppState.activeProfile = {}
      if (route.name === 'Vault') {
        try {
          AppState.keeps = []
          await keepsService.getAllKeepsByVaultId(route.params.id)
        } catch (error) {
          Pop.toast(error, 'error')
        }
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

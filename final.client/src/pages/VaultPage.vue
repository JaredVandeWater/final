<template>
  <KeepModal />
  <div v-if="state.account && state.profile && state.keeps && state.activeVault" class="container-fluid">
    <div class="row fixed-top">
      <Navbar />
    </div>
    <div class="navspacer"></div>

    <div class="row mx-4 mt-4">
      <h1 class="mb-0">
        {{ state.activeVault.name }}
      </h1>

      <button title="Delete Vault" v-if="state.account.id === state.activeVault.creatorId" @click="deleteVault" class="btn">
        <i class="mdi mdi-delete mdi-24px"></i>
      </button>
    </div>

    <div class="row ml-4">
      <div>
        <h1>{{ state.profile.name }}</h1>
        <h5>Keeps: {{ state.keeps.length }}</h5>
      </div>
    </div>
    <div class="card-columns">
      <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { computed, onMounted, watchEffect } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'

import { useRoute, useRouter } from 'vue-router'
import { vaultsService } from '../services/VaultsService'

export default {
  name: 'Profile',
  setup() {
    const router = useRouter()
    const route = useRoute()
    const state = reactive({
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      account: computed(() => AppState.account),
      activeVault: computed(() => AppState.activeVault)
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
          router.push({ name: 'Home' })
        }
        try {
          AppState.activeVault = {}
          await vaultsService.getVault(route.params.id)
        } catch (error) {

        }
      }
    })

    return {
      state,
      async deleteVault() {
        try {
          if (await Pop.confirm('Are you sure?', "You won't be able to revert this!", 'warning', 'Yes, delete it!')) {
            await vaultsService.deleteVault(route.params.id)
            router.push({ name: 'Profile', params: { id: state.activeVault.creatorId } })
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }

    }
  }
}
</script>

<style lang="scss" scoped>
.navspacer{
  min-height: 90px;
}

</style>

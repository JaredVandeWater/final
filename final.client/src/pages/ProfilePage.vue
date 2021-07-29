<template>
  <div v-if="state.account && state.profile && state.vaults && state.keeps && state.user" class="container-fluid">
    <div class="row fixed-top">
      <Navbar />
    </div>
    <div class="navspacer"></div>
    <div class="row ">
      <div class="mx-4">
        <div class="col">
          <img class="rounded" :src="state.profile.picture" alt="Profile">
        </div>
        <div class="col mt-2 d-flex justify-content-center">
          <button
            title="Logout"
            class="btn btn-outline-danger btn-sm text-uppercase"
            @click="logout"
            v-if="state.user.isAuthenticated"
          >
            Logout
          </button>
        </div>
      </div>
      <div>
        <h1>{{ state.profile.name }}</h1>
        <h4>Keeps: {{ state.keeps.length }}</h4>
        <h4>Vaults: {{ state.vaults.length }}</h4>
      </div>
    </div>
    <div class="row mx-4 mt-3">
      <h1>Vaults</h1>
      <button title="Create Vault" v-if="state.account.id === state.profile.id" data-toggle="modal" data-target="#vaultcreator" class="btn">
        <i class="text-danger mdi mdi-plus mdi-24px"></i>
      </button>
    </div>

    <div class="row m-1">
      <Vault v-for="vault in state.vaults" :key="vault.id" :vault="vault" />
    </div>
    <div class="row m-4">
      <h1>Keeps</h1>
      <button title="Create Keep" v-if="state.account.id === state.profile.id" data-toggle="modal" data-target="#keepcreator" class="btn">
        <i class="text-danger mdi mdi-plus mdi-24px"></i>
      </button>
    </div>
    <div class="card-columns">
      <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
    </div>
    <KeepModal />
    <CreateVaultModal />
    <CreateKeepModal />
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { computed, watch, watchEffect } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'
import { profilesService } from '../services/ProfilesService'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
import { AuthService } from '../services/AuthService'

export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    const state = reactive({
      user: computed(() => AppState.user),
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      account: computed(() => AppState.account)

    })

    watch(() => state.user, async() => {
      try {
        AppState.vaults = []
        await vaultsService.getAllVaultsByProfileId(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })

    watchEffect(async() => {
      window.scrollTo({ top: 0 })
      AppState.activeProfile = {}
      if (route.name === 'Profile') {
        try {
          await profilesService.getProfile(route.params.id)
        } catch (error) {
          Pop.toast(error, 'error')
        }
        try {
          AppState.keeps = []
          await keepsService.getAllKeepsByProfileId(route.params.id)
        } catch (error) {
          Pop.toast(error, 'error')
        }
        try {
          AppState.vaults = []
          await vaultsService.getAllVaultsByProfileId(route.params.id)
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
    })
    return {
      state,
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
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

<template>
  <div @click="setKeep" data-toggle="modal" data-target="#keepModal" class="position-relative card hoverable shadow-lg">
    <img class="card-img-top" :src="state.img" alt="Card image cap">
    <div class="position-absolute titlepos">
      <h5 class="m-0 px-1 text-light">
        {{ state.keepTitle }}
      </h5>
    </div>
    <div v-if="$route.name === 'Home'" class="position-absolute profpos">
      <button class="btn" @click.stop="toProfilePage">
        <img class="rounded-circle creator-pic px-1" :src="state.creator.picture" :alt="state.creator.name">
      </button>
    </div>
    <button v-if="state.account.id === state.vault && $route.name==='Vault'" @click.stop="removeVaultKeep" class="btn remvk position-absolute">
      <i class="mdi mdi-36px mdi-delete-restore"></i>
    </button>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
import { useRoute, useRouter } from 'vue-router'
import Pop from '../utils/Notifier'
import { computed } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService'
export default {
  props: { keep: { type: Object, required: true } },
  setup(props) {
    const route = useRoute()
    const router = useRouter()
    const state = reactive({
      img: props.keep.img,
      creator: props.keep.creator,
      keepTitle: props.keep.name,
      keepId: props.keep.id,
      account: computed(() => AppState.account),
      vault: computed(() => AppState.activeVault.creatorId),
      vaultKeep: computed(() => AppState.activeVK.id)

    })
    return {
      state,
      toProfilePage() {
        try {
          router.push({ name: 'Profile', params: { id: state.creator.id } })
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      async removeVaultKeep() {
        if (route.name === 'Vault') {
          try {
            if (await Pop.confirm('Are you sure?', 'You are about to remove a keep from this vault!', 'warning', 'Remove from Vault')) {
              keepsService.getVK()
              keepsService.removeVaultKeep(state.vaultKeep)
            }
          } catch (error) {
            Pop.toast(error, 'error')
          }
        }
      },
      async setKeep() {
        try {
          AppState.activeKeep = {}
          AppState.myVaults = {}
          await vaultsService.getAllVaultsByProfileId(state.account.id)
          await keepsService.getOneKeep(state.keepId)
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.creator-pic{
  height: 40px;
}
.hoverable{
  cursor: pointer;
}
.titlepos{
  bottom: 10px;
}
.profpos{
  bottom: 10px;
  right: 0;
}
.remvk{
  top: 0;
  right: 0;
  color: red;
}
</style>

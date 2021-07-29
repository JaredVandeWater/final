
<template>
  <!-- Modal -->
  <div
    class="modal fade"
    id="keepModal"
    tabindex="-1"
    role="dialog"
    aria-labelledby="keepModalTitle"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
      <div class="modal-content">
        <div class="modal-body p-0 py-2">
          <div class="container">
            <div class="row" v-if="state.creator && state.myVaults">
              <div class="col-12 col-lg d-flex align-items-center justify-content-center hset">
                <img class="maxw-100" modal :src="state.activeKeep.img" alt="">
              </div>
              <div class="col-12 col-md position-relative">
                <div class="row justify-content-between">
                  <div class="col">
                    <button v-if="state.account.id === state.creator.id" @click="deleteKeep" class="btn">
                      <i class="mdi mdi-delete"></i>
                    </button>
                  </div>
                  <div class="col d-flex justify-content-end">
                    <button type="button" class="btn" data-dismiss="modal">
                      <i class="mdi mdi-close"></i>
                    </button>
                  </div>
                </div>
                <div class="row py-1 pb-2 justify-content-center">
                  <div class="col d-flex justify-content-end">
                    <i class="mdi mdi-eye d-flex"><p class="pl-1">{{ state.activeKeep.views }}</p></i>
                  </div>

                  <div class="col-3 d-flex justify-content-center">
                    <i class="mdi mdi-alpha-k-box-outline d-flex"><p class="pl-1">{{ state.activeKeep.keeps }}</p></i>
                  </div>
                  <div class="col">
                    <i class="mdi mdi-share-variant d-flex"><p class="pl-1">{{ state.activeKeep.shares }}</p></i>
                  </div>
                </div>
                <div class="row justify-content-center mx-2">
                  <h3 class="owr">
                    {{ state.activeKeep.name }}
                  </h3>
                </div>
                <div class="row mx-2">
                  <p class="owr hset2 ">
                    {{ state.activeKeep.description }}
                  </p>
                </div>

                <hr>
                <div class="spacer"></div>
                <div class="position-absolute vaultbuttonrow ">
                  <form>
                    <select @change="addToVault($event.target.value)" class="btn btn-sm btnsz btn-primary">
                      <option value="" selected disabled hidden>
                        Put into Vault
                      </option>
                      <option v-for="vault in state.myVaults" :value="vault.id" :key="vault.id">
                        {{ vault.name }}
                      </option>
                    </select>
                  </form>
                </div>
                <div class="position-absolute namepos">
                  <img class="rounded-circle creator-pic px-1" :src="state.creator.picture" alt="">
                  <small class="pr-2">{{ state.creator.name }}</small>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'
import $ from 'jquery'
import { useRoute } from 'vue-router'

export default {

  setup() {
    const route = useRoute()
    const state = reactive({
      activeKeep: computed(() => AppState.activeKeep),
      creator: computed(() => AppState.activeKeep.creator),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.vaults),
      newVaultKeep: {}

    })
    return {
      state,
      async deleteKeep() {
        try {
          if (await Pop.confirm('Are you sure?', "You won't be able to revert this!", 'warning', 'Yes, delete it!')) {
            await keepsService.deleteKeep(state.activeKeep.id, route.name, route.params.id)
            $('#keepModal').modal('hide')
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      async addToVault(vault) {
        try {
          state.newVaultKeep = {}
          state.newVaultKeep.keepId = state.activeKeep.id
          state.newVaultKeep.vaultId = vault
          console.log(vault)
          console.log(state.newVaultKeep)
          if (state.newVaultKeep.vaultId) {
            await keepsService.addKeeptoVault(state.newVaultKeep)
            $('#keepModal').modal('hide')
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
.maxw-100{
  width: 100%;
  max-height: 40vh;
  min-height: 300px;
  object-fit: contain;
  background-color: black;
}
.vaultbuttonrow{
  bottom: 0;
  left: 0;
}
.trashpos{
  top: 0px;
  left: 100px;
}
.creator-pic{
  width: 35px;
  right: 0;
  bottom: 0;
}
.spacer{
  min-height: 50px;
}
.namepos{
  right: 0;
  bottom: 0;
}
.owr{
  overflow-wrap: anywhere;
}
.hset{
  max-height: 45vh;

}
.hset2{
   max-height: 22vh;
   overflow-y: auto ;
}
.btnsz{
  max-width: 128px;
}
</style>

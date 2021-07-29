
<template>
  <!-- Modal -->
  <div class="modal fade"
       id="vaultcreator"
       tabindex="-1"
       role="dialog"
       aria-labelledby="vaultcreatorLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <form @submit.prevent="createVault" class="modal-content">
        <div class="modal-header">
          <h2 class="modal-title" id="vaultcreatorLabel">
            Create a Vault
          </h2>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <div>
            <div class="form-group">
              <label for="title">Title</label>
              <input v-model="state.newVault.Name"
                     type="text"
                     class="form-control"
                     id="name"
                     placeholder="Title..."
                     required
              >
            </div>
            <div class="form-group">
              <label for="descr">Description</label>
              <input v-model="state.newVault.Description"
                     type="text"
                     class="form-control"
                     id="descr"
                     placeholder="Vault Description..."
                     required
              >
            </div>

            <div class="form-check">
              <input v-model="state.newVault.isPrivate" type="checkbox" class="form-check-input" id="isPrivate">
              <label class="form-check-label" for="isPrivate">Private</label>
            </div>
            <small>Private vaults cannot be seen by others.</small>
          </div>
        </div>
        <div class="modal-footer">
          <button type="submit" class="btn btn-primary">
            Create
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Notifier'
import $ from 'jquery'
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'

export default {

  setup() {
    const state = reactive({
      newVault: {},
      account: computed(() => AppState.account)

    })
    return {
      state,
      async createVault() {
        try {
          await vaultsService.createVault(state.newVault, state.account.id)
          $('#vaultcreator').modal('hide')
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
    }
  }

}
</script>

<style lang="scss" scoped>

</style>

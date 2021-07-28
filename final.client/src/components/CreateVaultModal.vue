
<template>
  <!-- Modal -->
  <div class="modal fade"
       id="keepcreator"
       tabindex="-1"
       role="dialog"
       aria-labelledby="keepcreatorLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h2 class="modal-title" id="keepcreatorLabel">
            Create a Keep
          </h2>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="">
            <div class="form-group">
              <label for="title">Title</label>
              <input type="text" class="form-control" id="title" placeholder="Title...">
            </div>
            <div class="form-group">
              <label for="exampleInputPassword1">Image Url</label>
              <input type="password" class="form-control" id="exampleInputPassword1" placeholder="URL...">
            </div>
            <div class="form-check">
              <input type="checkbox" class="form-check-input" id="isPrivate">
              <label class="form-check-label" for="isPrivate">Private</label>
              <small>Private vaults cannot be seen by others.</small>
            </div>
            <button type="submit" class="btn btn-primary">
              Create
            </button>
          </form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">
            Close
          </button>
          <button type="button" class="btn btn-primary">
            Save changes
          </button>
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
      account: computed(() => AppState.account)

    })
    return {
      state,
      async createKeep() {
        try {
          console.log('create')
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

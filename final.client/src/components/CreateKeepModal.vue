
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
      <form @submit.prevent="createKeep" class="modal-content">
        <div class="modal-header">
          <h2 class="modal-title" id="keepcreatorLabel">
            Create a Keep
          </h2>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <div>
            <div class="form-group">
              <label for="title">Title</label>
              <input v-model="state.newKeep.Name"
                     type="text"
                     class="form-control"
                     id="title"
                     placeholder="Title..."
                     required
              >
            </div>
            <div class="form-group">
              <label for="img">Image Url</label>
              <input v-model="state.newKeep.Img"
                     type="text"
                     class="form-control"
                     id="img"
                     placeholder="URL..."
                     required
              >
            </div>
            <div class="form-group">
              <label for="desc">Description</label>
              <textarea v-model="state.newKeep.Description"
                        class="form-control"
                        id="desc"
                        rows="3"
                        placeholder="Keep Description..."
                        required
              ></textarea>
            </div>
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
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'
import $ from 'jquery'

export default {

  setup() {
    const state = reactive({
      newKeep: {}

    })
    return {
      state,
      async createKeep() {
        try {
          console.log(state.newKeep)
          await keepsService.createKeep(state.newKeep)
          $('#keepcreator').modal('hide')
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
    }
  }

}
</script>

<style lang="scss" scoped>
textarea{
  resize: none;
}
</style>

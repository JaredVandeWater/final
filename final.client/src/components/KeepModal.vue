
<template>
  <!-- Modal -->
  <div class="modal fade"
       id="keepModal"
       tabindex="-1"
       role="dialog"
       aria-labelledby="keepModalTitle"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
      <div class="modal-content">
        <div class="modal-body">
          <div class="container">
            <div class="row">
              <div class="col d-flex align-items-center">
                <img class="maxw-100" modal :src="state.activeKeep.img" alt="">
              </div>
              <div class="col">
                <div class="row justify-content-end">
                  <button type="button" class="btn btn-secondary" data-dismiss="modal">
                    <i class="mdi mdi-close"></i>
                  </button>
                </div>
                <div class="row justify-content-center">
                  Put some data here
                </div>
                <div class="row justify-content-center">
                  <h3>
                    {{ state.activeKeep.name }}
                  </h3>
                </div>
                <div class="row">
                  <p>
                    {{ state.activeKeep.description }}
                  </p>
                </div>
                <hr>
                <div class="rowjustify-content-between">
                </div>
                <div class="dropdown">
                  <button class="btn btn-info dropdown-toggle"
                          type="button"
                          id="dropdownMenuButton"
                          data-toggle="dropdown"
                          aria-haspopup="true"
                          aria-expanded="false"
                  >
                    Add To Vault
                  </button>
                  <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                    <a class="dropdown-item" href="#">Action</a>
                    <a class="dropdown-item" href="#">Another action</a>
                    <a class="dropdown-item" href="#">Something else here</a>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- {{ state.activeKeep }} -->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
export default {

  setup() {
    const state = reactive({
      activeKeep: computed(() => AppState.activeKeep)
    })
    return {
      state
    }
  }

}
</script>

<style lang="scss" scoped>
.maxw-100{
  max-width: 100%;
}
</style>

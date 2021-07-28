
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
        <div class="modal-body p-0 py-2">
          <div class="container">
            <div class="row">
              <div class="col-12 col-md d-flex align-items-center justify-content-center">
                <img class="maxw-100" modal :src="state.activeKeep.img" alt="">
              </div>
              <div class="col-12 col-md position-relative">
                <div class="row justify-content-between">
                  <div class="">
                    <button class="btn">
                      <i class="mdi mdi-delete"></i>
                    </button>
                  </div>
                  <button type="button" class="btn" data-dismiss="modal">
                    <i class="mdi mdi-close"></i>
                  </button>
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
                <div class="position-absolute vaultbuttonrow">
                  <div class="dropdown ">
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
                    </div>
                  </div>
                </div>
                <div>
                  <img v-if="state.creator" class="rounded-circle position-absolute creator-pic px-1" :src="state.creator" alt="">
                  <small v-if="state.creator">{{ state.creator }}</small>
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
      activeKeep: computed(() => AppState.activeKeep),
      creator: computed(() => AppState.activeKeep.creator)

    })
    return {
      state
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
</style>

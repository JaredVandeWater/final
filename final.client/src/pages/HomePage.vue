<template>
  <KeepModal />
  <div class="container-fluid">
    <div class="row fixed-top">
      <Navbar />
    </div>
    <div class="navspacer"></div>
    <div v-if="!state.keeps">
      Loading...
    </div>
    <div v-else class="card-columns">
      <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'
export default {
  name: 'Home',
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keeps)
    })
    onMounted(async() => {
      window.scrollTo({ top: 0 })
      try {
        await keepsService.getAllKeeps()
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      state

    }
  }
}
</script>

<style scoped lang="scss">
.navspacer{
  min-height: 90px;
}

@media (min-width: 34em) {
    .card-columns {
        -webkit-column-count: 2;
        -moz-column-count: 2;
        column-count: 2;
    }
}

@media (min-width: 48em) {
    .card-columns {
        -webkit-column-count: 3;
        -moz-column-count: 3;
        column-count: 3;
    }
}

@media (min-width: 62em) {
    .card-columns {
        -webkit-column-count: 4;
        -moz-column-count: 4;
        column-count: 4;
    }
}

@media (min-width: 75em) {
    .card-columns {
        -webkit-column-count: 5;
        -moz-column-count: 5;
        column-count: 5;
    }
}
</style>

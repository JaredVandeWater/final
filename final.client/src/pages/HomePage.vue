<template>
  <div class="container-fluid">
    <div class="row">
      <Navbar />
    </div>
    <div class="row">
      <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'Home',
  setup() {
    const state = reactive({
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps)
    })
    onMounted(async() => {
      window.scrollTo({ top: 0 })
      try {
        await keepsService.getAllKeeps()
      } catch (error) {
        Notification.toast(error, 'error')
      }
    })
    return {
      state

    }
  }
}
</script>

<style scoped lang="scss">
</style>

<template>
  <div>
    ProfilePage
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { computed, onMounted } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { profileService } from '../services/ProfileService'
export default {
  name: 'Profile',
  setup() {
    const state = reactive({
      Profile: computed(() => AppState.activeProfile)
    })
    onMounted(async() => {
      window.scrollTo({ top: 0 })
      AppState.activeProfile = {}
      try {
        await profileService.getProfile()
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

<style lang="scss" scoped>

</style>

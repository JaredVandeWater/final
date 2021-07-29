<template>
  <div v-if="state.creator" @click="goToVault" class=" m-2 position-relative card hoverable">
    <img class="card-img-top vault" src="https://thumbs.dreamstime.com/b/silver-safe-dial-d-rendering-isolated-white-background-106560450.jpg" alt="VaultImage">
    <div class="position-absolute titlepos">
      <h5 class="m-0 px-1 text-light">
        {{ state.vaultTitle }}
      </h5>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { useRouter } from 'vue-router'
import Pop from '../utils/Notifier'
export default {
  props: { vault: { type: Object, required: true } },
  setup(props) {
    const router = useRouter()
    const state = reactive({
      creator: props.vault.creator,
      vaultTitle: props.vault.name,
      vaultId: props.vault.id

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
      async goToVault() {
        try {
          router.push({ name: 'Vault', params: { id: state.vaultId } })
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
.vault{
  width: 12vw;
  min-width: 150px;
}
</style>

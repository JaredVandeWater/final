<template>
  <div v-if="state.creator" @click="setKeep" data-toggle="modal" data-target="#keepModal" class="position-relative card hoverable">
    <img class="card-img-top" :src="state.img" alt="Card image cap">
    <div class="position-absolute titlepos">
      <h5 class="m-0 px-1 text-light">
        {{ state.keepTitle }}
      </h5>
    </div>
    <div class="position-absolute profpos">
      <button class="btn" @click.stop="toProfilePage">
        <img class="rounded-circle creator-pic px-1" :src="state.creator.picture" :alt="state.creator.name">
      </button>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
import { useRouter } from 'vue-router'
import Pop from '../utils/Notifier'
export default {
  props: { keep: { type: Object, required: true } },
  setup(props) {
    const router = useRouter()
    const state = reactive({
      img: props.keep.img,
      creator: props.keep.creator,
      keepTitle: props.keep.name,
      keepId: props.keep.id

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
      async setKeep() {
        try {
          AppState.activeKeep = {}
          await keepsService.getOneKeep(state.keepId)
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
</style>

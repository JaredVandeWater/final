<template>
  <div class="card">
    <img class="card-img-top" :src="state.img" alt="Card image cap">
  </div>
  <div class="position-relative">
    <img class="rounded-circle creator-pic" :src="state.creator.picture" :alt="state.creator.name">
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
export default {
  props: { keep: { type: Object, required: true } },
  setup(props) {
    const state = reactive({
      img: props.keep.img,
      creator: props.keep.creator

    })
    return {
      state
    }
  }
}
</script>

<style lang="scss" scoped>
.creator-pic{
  height: 40px;
}
</style>

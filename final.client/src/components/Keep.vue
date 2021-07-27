<template>
  <div class="col-6 align-self-center col-md-3">
    <img class="w-100" :src="state.img" alt="Card image cap">
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
export default {
  props: { keep: { type: Object, required: true } },
  setup(props) {
    const state = reactive({
      img: props.keep.img

    })
    return {
      state
    }
  }
}
</script>

<style lang="scss" scoped>

</style>

<template>
  <nav class="navbar min100vw d-flex justify-content-between navbar-expand-lg navbar-dark bg-dark">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img
          alt="logo"
          src="../assets/img/cw-logo.png"
          height="45"
        />
      </div>
    </router-link>

    <div id="navbarText">
      <span class="navbar-text">
        <button
          class="btn btn-outline-primary text-uppercase"
          @click="login"
          v-if="!user.isAuthenticated"
        >
          Login
        </button>

        <button @click="toProfile" class=" d-flex btn btn-light p-0 p-1 " v-else>
          <img class="profpic rounded-circle mx-2" :src="state.account.picture" alt="ProfilePic">

          <p class=" m-0 mr-2 mt-1">{{ state.account.name }}</p>
        </button>
      </span>
    </div>
  </nav>
</template>

<script>
import { AuthService } from '../services/AuthService'
import { AppState } from '../AppState'
import { computed, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { router } from '../router'
export default {
  setup() {
    const router = useRouter()
    const state = reactive({
      account: computed(() => AppState.account)
    })
    return {
      state,
      user: computed(() => AppState.user),

      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      },
      toProfile() {
        router.push({ name: 'Profile', params: { id: state.account.id } })
      }
    }
  }
}
</script>

<style scoped>
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
a:hover {
  text-decoration: none;
}
.nav-link{
  text-transform: uppercase;
}
.nav-item .nav-link.router-link-exact-active{
  color: var(--primary);
}

.min100vw{
  min-width: 100vw;
}
.profpic{
  height: 30px;
}
</style>

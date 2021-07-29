<template>
  <nav class="navbar ncolor min100vw d-flex justify-content-between navbar-expand-lg ">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center mx-4">
        <img
          alt="logo"
          src="../assets/img/keeprLogo.png"
          height="45"
        />
      </div>
    </router-link>

    <div id="navbarText">
      <span class="navbar-text">
        <button
          class="btn btn-outline-danger text-uppercase"
          @click="login"
          v-if="!user.isAuthenticated"
        >
          Login
        </button>

        <button @click="toProfile" class=" d-flex btn krpred  p-0 p-1 shadow-sm " v-else>
          <img class="profpic rounded-circle mx-2" :src="state.account.picture" alt="ProfilePic">

          <p class=" d-md-block d-none  m-0 mr-2 mt-1">{{ state.account.name }}</p>
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
  border-color: white;
  border-style: solid;
  border-width: 1.5px;
}
.krpred{
  background-color: #0000000d;
}
.ncolor{
  background: rgb(223,229,231);
background: linear-gradient(61deg, rgba(223,229,231,1) 0%, rgba(255,221,221,1) 100%);
}
</style>

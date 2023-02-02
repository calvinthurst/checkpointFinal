<template>
  <nav class="navbar navbar-expand-lg  navbar-light bg-light justify-content-between ">
    <div class="col-3  d-flex  justify-content-end">
      <router-link class="navbar-brand  d-flex" :to="{ name: 'Home' }">
        <div title="Home" class="flex-column align-items-center fw-bold">
          HOME </div>
      </router-link>
      <div title="Create Menu" class="dropdown d-none d-md-block justify-content-end" v-if="user != null">
        <a class="btn btn-outline-secondary dropdown-toggle" role="button" data-bs-toggle="dropdown"
          aria-expanded="false">
          Create
        </a>
        <ul class="dropdown-menu">
          <li>
            <div title="Create Keep" class="text-secondary fw-bold selectable mb-2 " data-bs-toggle="modal"
              data-bs-target="#keep-form">
              Create
              Keep</div>
          </li>
          <li>
            <div title="Create Vault" class="text-secondary fw-bold selectable " data-bs-toggle="modal"
              data-bs-target="#vault-form">
              Create
              Vault <i class="mdi mdi-safe"></i></div>
          </li>
        </ul>
      </div>
    </div>


    <router-link class="navbar-brand col-3 text-center " :to="{ name: 'Home' }">
      <div class="flex-column align-items-center fw-bold ">
        Keepers
      </div>
    </router-link>

    <!-- LOGIN COMPONENT HERE -->
    <Login class="col-3 text-center px-3" />
  </nav>
</template>

<script>
import KeepForm from "./KeepForm.vue";
import Login from './Login.vue'
import VaultForm from "./VaultForm.vue";
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  setup() {
    return {
      user: computed(() => AppState.account)
    }
  },
  components: { Login, KeepForm, VaultForm }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.loggin {
  user-select: none;
}

.logo-brand {
  justify-content: center;
}
</style>

<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light px-3 justify-content-between my-3">
    <div class="d-flex align-items-center justify-content-between col-3" v-if="user != null">
      <button class="btn btn-outline-secondary fw-bold " data-bs-toggle="modal" data-bs-target="#keep-form">Create
        Keep</button>
      <Modal id="keep-form" modal-title="Create Keep">
        <KeepForm />
      </Modal>
      <button class="btn btn-outline-secondary fw-bold " data-bs-toggle="modal" data-bs-target="#vault-form">Create
        Vault<i class="mdi mdi-safe"></i></button>
      <Modal id="vault-form" modal-title="Create Vault">
        <VaultForm />
      </Modal>
    </div>
    <div class="col-3 text-light loggin" v-else>KEEPERS</div>
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center fw-bold col-3">
        Keepers
      </div>
    </router-link>

    <!-- LOGIN COMPONENT HERE -->
    <Login class="col-2" />
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
</style>

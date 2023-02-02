<template>
  <nav
    class="navbar navbar-expand-lg d-block d-md-none fixed-bottom footer-button navbar-light bg-light m-0 justify-content-around my-3 d-flex">
    <div class="col-3 m-0 d-flex p-2">
      <div class="dropup dropup-center" v-if="user != null">
        <a class="btn btn-outline-secondary dropdown-toggle" role="button" data-bs-toggle="dropdown"
          aria-expanded="false">
          <i class="mdi mdi-dots-vertical"></i>
        </a>
        <ul class="dropdown-menu m-2">
          <li>
            <div>

              <router-link class="" :to="{ name: 'Home' }">
                <div class="flex-column text-secondary mx-3 fw-bold">
                  Home </div>
              </router-link>
            </div>
          </li>
          <li>
            <div class="text-secondary fw-bold m-3 selectable " data-bs-toggle="modal" data-bs-target="#keep-form">
              Create
              Keep</div>
          </li>
          <li>
            <div class="text-secondary fw-bold m-3 selectable " data-bs-toggle="modal" data-bs-target="#vault-form">
              Create
              Vault <i class="mdi mdi-safe"></i></div>
          </li>
        </ul>
      </div>
    </div>

    <router-link class="navbar-brand text-center col-2 d-flex" :to="{ name: 'Home' }">
      <div class="flex-column align-items-center fw-bold">
        Keepers
      </div>
    </router-link>

    <FooterLogin class="col-3 text-center" />
  </nav>


</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import KeepForm from "./KeepForm.vue";
import VaultForm from "./VaultForm.vue";
export default {
  setup() {
    return {
      user: computed(() => AppState.account)
    };
  },
  components: { KeepForm, VaultForm }
};
</script>


<style lang="scss" scoped>
.footer-button {
  position: fixed;
  transform: translateY(3vh);
}
</style>
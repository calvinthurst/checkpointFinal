<template>
  <div class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <p>{{ account.email }}</p>
  </div>
  <div class="container">
    <button class="btn btn-outline-secondary" data-bs-toggle="modal" data-bs-target="#account-form">Edit
      Account</button>
    <div class="row align-items-center justify-content-center">
      <div class="col-8">
        <img :src="account.coverImg" class="coverImg" alt="">
      </div>
      <div class="col-8 d-flex align-items-center justify-content-center">
        <img :src="account.picture" class="profile-pic rounded-circle" alt="">
        <h1>{{ account.name }}</h1>
      </div>
      <h4>Vaults</h4>
      <div v-for="v in vaults" class="col-3">
        <VaultCard :vault="v" />
      </div>
      <h4>Keeps</h4>
      <div v-for="k in keeps" class="col-3">
        <KeepCard :keep="k" />
      </div>
    </div>
  </div>
  <Modal id="account-form" modal-title="Edit Account">
    <AccountForm />
  </Modal>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import AccountForm from "../components/AccountForm.vue"
import Modal from "../components/Modal.vue"
import { accountService } from "../services/AccountService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
export default {
  setup() {
    async function getMyVaults() {
      try {
        await accountService.getMyVaults();
      }
      catch (error) {
        logger.log(error);
        Pop.error(error);
      }
    }
    async function getMyKeeps() {
      try {
        await accountService.getMyKeeps();
      }
      catch (error) {
        logger.log(error);
        Pop.error(error);
      }
    }
    onMounted(() => {
      getMyKeeps();
      getMyVaults();
    });
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.myKeeps),
      vaults: computed(() => AppState.myVaults)
    };
  },
  components: { Modal, AccountForm }
}
</script>

<style scoped>
img {
  max-width: 100px;
}

.profile-pic {
  max-width: 7vh;
}
</style>

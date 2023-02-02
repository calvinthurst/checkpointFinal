<template>
  <div class="row justify-content-center align-items-center m-0">
    <img :src="account.coverImg" class="coverImg" alt="">
    <div class="col-12 text-center ">
      <h1>Welcome {{ account.name }}</h1>
      <div class="d-flex justify-content-around">
        <img :src="account.picture" class="profile-pic rounded-circle " alt="">
        <p>{{ account.email }}</p>
      </div>
    </div>
    <button class="col-4 col-md-2 btn btn-outline-secondary" data-bs-toggle="modal" data-bs-target="#account-form">Edit
      Account</button>
    <div class="row align-items-center justify-content-center">

      <div class="text-center fs-5 fw-bold">{{ vaults.length }} vaults || {{ keeps.length }} keeps</div>
      <h4 class="ps-4">Vaults</h4>
      <div v-for="v in vaults" class="col-md-3 col-6">
        <VaultCard :vault="v" />
      </div>
      <h4 class="px-3">Keeps</h4>
      <div class="masonry-with-columns">
        <div v-for="k in keeps">
          <KeepCard :keep="k" />
        </div>
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

<style lang="scss" scoped>
img {
  max-width: 100px;
}

.coverImg {
  height: 45vh;
  max-width: 90%;
  object-fit: cover;
  object-position: center;
  border-radius: 10px;
}

.profile-pic {
  max-width: 7vh;

}

body {
  margin: 0;
  padding: 1rem;
}

.masonry-with-columns {
  columns: 6 40vw;
  column-gap: 1rem;

  div {
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
  }



  @for $i from 1 through 200 {
    div:nth-child(#{$i}) {
      $h: (random(45) + 20)+vh;
      height: $h;
      line-height: $h/2;
    }
  }
}

@media screen and (min-width: 600px) {

  .masonry-with-columns {
    columns: 4 20vw;
    column-gap: 1rem;

    div {
      margin: 0 1rem 1rem 0;
      display: inline-block;
      width: 100%;
    }



    @for $i from 1 through 200 {
      div:nth-child(#{$i}) {
        $h: (random(45) + 20)+vh;
        height: $h;
        line-height: $h/2;
      }
    }
  }
}
</style>

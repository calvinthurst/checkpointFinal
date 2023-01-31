<template>
  <section class="row" v-if="keep">
    <img :src="keep.img" class="col-md-6 modal-image" alt="">
    <div class="col-md-6 align-items-around justify-content-center keep">
      <div class="d-flex justify-content-around">
        <p><i class="mdi mdi-eye-outline"></i> {{ keep.views }}</p>
        <p><i class="mdi mdi-safe"></i>{{ keep.kept }}</p>
      </div>
      <h4 class="text-center">{{ keep.name }}</h4>
      <p>{{ keep.description }}</p>
      <div class="d-flex align-items-center justify-content-around">
        <div class="border rounded" v-if="!myVaults">Login to add</div>
        <div class="btn btn-outline-danger" @click="removeFromVault()"
          v-else-if="route.name == 'Vault' && vault.creatorId == isUser.id">Delete</div>
        <div class="d-flex" v-else>
          <VaultDropdown />
        </div>
        <div class="d-flex">
          <RouterLink :to="{
            name: 'Profile', params: { id: keep.creatorId }
          }">
            <img :src="keep.creator.picture" class="profile-pic rounded-circle" alt="" data-bs-dismiss="modal">
          </RouterLink>
          <RouterLink :to="{
            name: 'Profile', params: { id: keep.creatorId }
          }">
            <p data-bs-dismiss="modal">{{ keep.creator.name }}</p>
          </RouterLink>

        </div>
      </div>
    </div>
  </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watchEffect } from 'vue';
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { vaultService } from "../services/VaultService.js";
import { useRoute } from "vue-router";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { Modal } from 'bootstrap';

export default {
  setup() {
    const route = useRoute()
    watchEffect(() => AppState.account)
    return {
      async removeFromVault() {
        try {
          Modal.getOrCreateInstance('#keep-modal').hide()
          await vaultKeepsService.removeFromVault(AppState.keep.vaultKeepId, AppState.keep.id)
        } catch (error) {
          logger.log(error)
          Pop.error(error)
        }
      },
      route,
      keep: computed(() => AppState.keep),
      isUser: computed(() => AppState.account),
      vault: computed(() => AppState.vault),
      myVaults: computed(() => AppState.myVaults)
    }
  }
};
</script>


<style lang="scss" scoped>
.modal-img {
  width: 100%;
  object-fit: contain;
  object-position: center;
  height: 100;
}

.profile-pic {
  width: 7vh;
  object-fit: cover;
  object-position: center;

}

.keep {
  height: auto;
}
</style>
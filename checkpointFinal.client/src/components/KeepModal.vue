<template>
  <div class="d-flex justify-content-end">
    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
  </div>
  <section class="row align-items-between" v-if="keep">

    <div class="col-md-6">

      <img :src="keep.img" class="modal-img rounded" alt="">
    </div>
    <div class="col-md-6 align-items-between">
      <div title="Delete Keep" class="col-12 justify-content-around d-flex"><i @click="deleteKeep()"
          v-if="user?.id == keep.creatorId"
          class="selectable mdi mdi-trash-can-outline text-center delete-btn  text-danger">
        </i> </div>
      <div class="row justify-content-center align-items-around  keep-height">
        <div class="col-12 d-flex justify-content-around">
          <p class="col-2 text-center"><i class="mdi mdi-eye-outline"></i> {{ keep.views }}</p>
          <p class="col-2 text-center"><i class="mdi mdi-safe"></i>{{ keep.kept }}</p>
        </div>
        <h4 class="text-center">{{ keep.name }}</h4>
        <p>{{ keep.description }}</p>
        <div class="row align-items-center justify-content-start">
          <div class="col-12 col-md-6 border rounded" v-if="!myVaults">Login to add</div>
          <div class="col-12 col-md-6 btn btn-outline-danger" @click="removeFromVault()"
            v-else-if="route.name == 'Vault' && vault.creatorId == user.id">Delete</div>
          <div class="col-12 col-md-6" v-else>
            <VaultDropdown />
          </div>
          <div class="col-md-3">
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
import { keepsService } from "../services/KeepsService.js";

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
      async deleteKeep() {
        try {
          if (await Pop.confirm("Are you sure you want to delete this keep?"))
            await keepsService.deleteKeep(AppState.keep.id)
          Modal.getOrCreateInstance('#keep-modal').hide()
        } catch (error) {
          logger.log(error)
          Pop.error(error)
        }
      },
      route,
      keep: computed(() => AppState.keep),
      user: computed(() => AppState.account),
      vault: computed(() => AppState.vault),
      myVaults: computed(() => AppState.myVaults)
    }
  }
};
</script>


<style lang="scss" scoped>
.modal-img {
  width: 100%;
  object-fit: cover;
  object-position: center;
  height: 65vh;
}

.profile-pic {
  max-height: 7vh;
  max-width: 7vh;
  object-fit: cover;
  object-position: center;

}

.keep-height {
  height: 60vh;
}

@media screen and (max-width: 768px) {
  .keep-height {
    max-height: 35vh;
  }
}
</style>
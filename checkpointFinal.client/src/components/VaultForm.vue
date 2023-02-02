<template>
  <form @submit.prevent="createVault()" v-if="account">
    <div class="modal-body row">
      <div class="form-floating mb-3">
        <input v-model="editable.name" required type="text" class="form-control" id="keepName" placeholder="Name...">
        <label for="keepName">Keep Name</label>
      </div>
      <div class="form-floating mb-3">
        <textarea v-model="editable.description" required type="text" class="form-control" id="keepDescription"
          placeholder="Description...">
                </textarea>
        <label for="keepDescription">Description</label>
      </div>
      <div class="form-floating mb-3 col-md-10">
        <input v-model="editable.img" required type="url" class="form-control" id="keepImg" placeholder="Image...">
        <img :src="editable.img" class="modal-img" alt="">
        <label for="keepImg">Keep Image</label>
      </div>
      <div class="form-check col-md-2">
        <input class="form-check-input" type="checkbox" v-model="editable.isPrivate" id="isPrivate">
        <label class="form-check-label" for="isPrivate">
          Private
        </label>
      </div>
    </div>

    <div class="modal-footer">
      <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      <button type="submit" class="btn btn-primary">Save changes</button>
    </div>
  </form>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultService } from "../services/VaultService.js";
import { Modal } from "bootstrap";
export default {
  setup() {
    const editable = ref({})

    return {
      editable,
      async createVault() {
        try {
          await vaultService.createVault(editable.value)
          Modal.getOrCreateInstance('#vault-form').hide()
        } catch (error) {
          logger.log(error)
          Pop.error(error)
        }
      },
      account: computed(() => AppState.user)
    }
  }
};
</script>


<style lang="scss" scoped>
.modal-img {
  max-width: 100%;
  max-height: 30vh;
}
</style>
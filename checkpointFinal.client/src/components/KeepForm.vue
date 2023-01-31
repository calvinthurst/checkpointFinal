<template>
  <form @submit.prevent="createKeep()">
    <div>
      <div class="form-floating mb-3">
        <input v-model="editable.name" required type="text" class="form-control" id="keepName" placeholder="Name...">
        <label for="keepName">Keep Name</label>
      </div>
      <div class="form-floating mb-3">
        <textarea v-model="editable.description" required type="text" class="form-control" id="keepDescription"
          placeholder="Description...">
                </textarea>
        <label for="floatingPassword">Description</label>
      </div>
      <div class="form-floating mb-3">
        <input v-model="editable.img" required type="url" class="form-control" id="keepImg" placeholder="Image...">
        <img :src="editable.img" class="keep-img" alt="" v-if="editable.img">
        <label for="keepImg">Keep Image</label>
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
import { keepsService } from "../services/KeepsService.js";
import { Modal } from "bootstrap";
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createKeep() {
        try {
          await keepsService.createKeep(editable.value)
          Modal.getOrCreateInstance.hide('#keep-form')
        } catch (error) {
          logger.log(error)
          Pop.error(error)
        }
      }
    }
  }
};
</script>


<style lang="scss" scoped>
.keep-img {
  width: 100%;
  height: 30vh;
  object-fit: contain;
}
</style>
<template>
  <div class="border border-rounded"></div>
  <form action="" @submit.prevent="addToVault()" class="d-flex">
    <select class="form-select" aria-label="Default select example" required id="dropdown" v-model="editable">
      <option :value="v.id" v-for="v in myVaults">{{ v.name }}</option>
    </select>
    <button class="btn btn-success">Add</button>
  </form>

</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { Modal } from "bootstrap";
import { useRouter } from 'vue-router';

export default {

  setup() {
    const router = useRouter()
    const editable = ref({})
    return {
      router,
      async addToVault() {
        try {
          await vaultKeepsService.addToVault(editable.value, AppState.keep.id)
          Pop.success("Keep Added to Vault")

        } catch (error) {
          logger.log(error)
          Pop.error(error)
        }
      },
      editable,
      myVaults: computed(() => AppState.myVaults),
    }
  }
};
</script>


<style lang="scss" scoped>

</style>
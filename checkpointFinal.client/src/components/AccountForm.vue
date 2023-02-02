<template>
  <form title="Edit Account" @submit.prevent="editAccount()" class="justify-content-end">
    <div class="form-floating m-3">
      <input title="Name" type="text" class="form-control" id="name" v-model="editable.name" />
      <label for="name">Name</label>
    </div>
    <div class="form-floating m-3">
      <input title="Picture" type="text" class="form-control" id="picture" v-model="editable.picture" />
      <label for="picture">Picture</label>
    </div>
    <div class="form-floating m-3">
      <input title="Cover Image" type="text" class="form-control" id="coverImg" v-model="editable.coverImg" />
      <label for="coverImg">Cover Image</label>
    </div>
    <div class="justify-content-around modal-footer">

      <button type="submit" class="btn btn-outline-primary m-2">Submit</button>
      <button class="btn btn-outline-secondary m-2" data-bs-dismiss="modal">Cancel</button>
    </div>
  </form>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { accountService } from "../services/AccountService.js";
export default {
  setup() {
    const editable = ref({})
    onMounted(() => {
      editable.value = AppState.account
    })
    return {
      editable,
      async editAccount() {
        try {
          await accountService.editAccount(editable.value)
        } catch (error) {
          logger.log(error)
          Pop.error(error)
        }
      },
      account: computed(() => AppState.account)
    }
  }
};
</script>


<style lang="scss" scoped>

</style>
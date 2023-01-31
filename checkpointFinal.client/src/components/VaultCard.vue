<template>
  <div class="d-flex justify-content-end">
    <i @click="deleteVault()" v-if="route?.name == `Account`"
      class="mdi mdi-trash-can-outline text-end delete-btn  text-danger">
    </i>
  </div>
  <RouterLink :to="{
    name: 'Vault', params: { id: vault.id }
  }">
    <p class="coverImg text-light" @click="activateVault()">{{ vault.name }}</p>
  </RouterLink>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from "../models/Vault.js";
import { RouterLink } from "vue-router";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultService } from "../services/VaultService.js";
import { useRoute } from "vue-router";
export default {
  props: {
    vault: {
      type: Vault,
      required: true
    }
  },
  setup(props) {
    const route = useRoute()
    return {
      route,
      coverImg: computed(() => `url(${props.vault.img})`),
      async deleteVault() {
        try {
          if (await Pop.confirm("Are you sure you want to delete this vault?"))
            await vaultService.deleteVault(props.vault.id)
        } catch (error) {
          logger.log(error)
          Pop.error(error)
        }
      },

    };
  },
  components: { RouterLink }
};
</script>


<style lang="scss" scoped>
.coverImg {
  background-image: v-bind(coverImg);
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  height: 30vh;
}

.delete-btn {
  transform: translate(-1vh, 4vh);
  cursor: pointer;
}
</style>
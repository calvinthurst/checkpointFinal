<template>
  <section class="row align-items-center justify-content-center m-0">
    <div v-for="k in keeps" class="col-3">
      <KeepCard :keep="k" />
    </div>
  </section>
</template>

<script>
import { authGuard, authSettled } from "@bcwdev/auth0provider-client";
import { onMounted, computed, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { keepsService } from "../services/KeepsService.js"
import { vaultService } from "../services/VaultService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import Masonry from 'masonry-layout';


export default {
  setup() {
    async function GetKeeps() {
      try {
        await keepsService.GetKeeps();
      }
      catch (error) {
        logger.log(error);
        Pop.error(error);
      }
    }

    onMounted(() => {
      GetKeeps();
    });
    watchEffect(() => {
      AppState.keeps;
    });
    return {
      keeps: computed(() => AppState.keeps)
    };
  },
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }


}
</style>

<template>
  <div class="container">
    <div class="masonry-with-columns">
      <div v-for="k in keeps" class="">
        <KeepCard :keep="k" />
      </div>
    </div>
  </div>

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
body {
  margin: 0;
  padding: 1rem;
}

.masonry-with-columns {
  columns: 2 30vw;
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
    columns: 6 20vw;
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

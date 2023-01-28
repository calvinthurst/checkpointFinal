<template>

  <div class="container">
    <section class="row">
      <div v-for="k in keeps">
        <KeepCard :keep="k" />
      </div>
    </section>

  </div>
</template>

<script>
import { onMounted, computed } from "vue";
import { AppState } from "../AppState.js";
import { keepsService } from "../services/KeepsService.js"
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import KeepCard from "../components/KeepCard.vue";

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
    return {
      keeps: computed(() => { AppState.keeps })
    };
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss">

</style>

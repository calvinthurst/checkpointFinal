<template>
  <div @click="setKeepActive(keep.id)" :title="keep.name"
    class="keep-card row justify-content-between align-items-end mx-0" data-bs-toggle="modal"
    data-bs-target="#keep-modal">
    <div class="bg-keep-creator">
      <div class=" text-light grid-item align-items-center d-flex justify-content-between m-1">
        <div class="fs-5 fw-bold  title-bg">{{ keep.name }}</div>
        <img v-if="route.name != 'Account'" :src="keep.creator.picture" :title="keep.creator.name"
          class="profile-pic d-none d-md-block rounded-circle m-1" alt="">
      </div>
    </div>
  </div>

  <Modal id="keep-modal" modal-title=Keep>
    <KeepModal />
  </Modal>


</template>


<script>
import { Keeps } from "../models/Keeps.js";
import { computed } from "@vue/reactivity";
import Modal from "./Modal.vue";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
import { onMounted } from "vue";
import { AppState } from "../AppState.js";
import { useRoute } from "vue-router";
export default {
  props: {
    keep: {
      type: Keeps,
      required: false
    }
  },
  setup(props) {
    const route = useRoute()
    return {
      route,
      async setKeepActive(id) {
        try {
          await keepsService.setKeepActive(id)
          AppState.keep.vaultKeepId = props.keep.vaultKeepId
        } catch (error) {
          logger.log(error)
          Pop.error(error)
        }
      },
      async deleteKeep() {
        try {
          if (await Pop.confirm("Are you sure you want to delete this keep?"))
            await keepsService.deleteKeep(props.keep.id)
        } catch (error) {
          logger.log(error)
          Pop.error(error)
        }
      },
      watchEffect: onMounted(() => {
        AppState.keep = props.keep
      }),

      coverImg: computed(() => `url(${props.keep.img})`)
    };
  },
  components: { Modal }
};
</script>


<style lang="scss" scoped>
.keep-card {
  background-image: v-bind(coverImg);
  background-repeat: no-repeat;
  background-size: cover;
  background-position: center;
  width: 100%;
  height: inherit;
}

.delete-btn {
  cursor: pointer;
}

.bg-keep-creator {
  background-color: rgba(0, 0, 0, 0.322);
}

.profile-pic {
  max-width: 5vh;
  max-height: 5vh;
  border-radius: 50%;
  object-fit: cover;
  object-position: center;
}
</style>
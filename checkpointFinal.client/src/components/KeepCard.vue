<template>
  <div class="d-flex justify-content-end">
    <i @click="deleteKeep()" v-if="route?.name == `Account`"
      class="mdi mdi-trash-can-outline text-end delete-btn  text-danger">
    </i>
  </div>
  <div @click="setKeepActive(keep.id)" class="keep-card text-light grid-item " data-bs-toggle="modal"
    data-bs-target="#keep-modal">
    <h1>
      {{ keep.name }}
    </h1>
  </div>

  <Modal id="keep-modal" :modal-title="keep.name">
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

      coverImg: computed(() => `url(${props.keep.img})`)
    };
  },
  components: { Modal }
};
</script>


<style lang="scss" scoped>
.keep-card {
  background-image: v-bind(coverImg);
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  height: 30vh;
}

.modal-img {
  width: 100%;
  object-fit: contain;
  object-position: center;
  height: 100;
}

.delete-btn {
  transform: translate(-1vh, 4vh);
  cursor: pointer;
}
</style>
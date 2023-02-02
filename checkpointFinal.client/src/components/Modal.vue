<template>
  <div class="modal fade" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable modal-fullscreen-down modal-xl">
      <div class="modal-content modal-body">
        <div class="modal-header sticky-top bg-white">
          <slot name="modal-header">
            <h1 class="modal-title fs-5" id="exampleModalLabel">{{ modalTitle }}</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </slot>
        </div>

        <slot></slot>

      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
  props: {
    modalTitle: {
      type: String,
      required: false,
      default: 'Close'
    },
  },
  setup() {
    return {}
  }
};
</script>


<style lang="scss" scoped>
@media screen and (max-width: 768px) {
  .modal-fullscreen-down {
    max-width: 100%;
    width: 100%;
    height: 100%;
    margin: 0;
  }

  .bg-white {
    background-color: white;
    transform: translateY(-4vh);
    border: none;
  }
}

.bg-white {
  background-color: white;
  border: none;
}
</style>
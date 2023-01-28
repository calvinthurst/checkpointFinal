<template>
  <div>
    {{ keep.name }}
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keeps } from "../models/Keeps.js";
export default {
  props: {
    keep: {
      type: Keeps,
      required: true
    }
  },
  setup(props) {
    return {}
  }
};
</script>


<style lang="scss" scoped>
body {
  margin: 0;
  padding: 1rem;
}

.masonry-with-columns {
  columns: 6 200px;
  column-gap: 1rem;

  div {
    width: 150px;
    background: #EC985A;
    color: white;
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
    text-align: center;
    font-family: system-ui;
    font-weight: 900;
    font-size: 2rem;
  }

  @for $i from 1 through 36 {
    div:nth-child(#{$i}) {
      $h: (random(400) + 100)+px;
      height: $h;
      line-height: $h;
    }
  }
}
</style>
<template>
  <div class="row align-items-center justify-content-center m-0">
    <img :src="profile.coverImg" class="coverImg" alt="">
    <div class="col-8 d-flex align-items-center justify-content-center">
      <img :src="profile.picture" class="profile-pic rounded-circle" alt="">
      <h1>{{ profile.name }}</h1>
    </div>
    <div class="row align-items-center justify-content-center p-2">

      <div class="text-center fs-5 fw-bold">{{ vaults.length }} vaults || {{ keeps.length }} keeps</div>
      <h4 class="ps-4">Vaults</h4>
      <div v-for="v in vaults" class="col-md-3 col-6 p-2">
        <VaultCard :vault="v" />
      </div>
      <h4 class="px-3">Keeps</h4>
      <div class="masonry-with-columns">
        <div v-for="k in keeps">
          <KeepCard :keep="k" />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { profileService } from "../services/ProfileService.js";
import { useRoute } from "vue-router";
export default {
  setup() {
    const route = useRoute()
    async function getProfile() {
      try {
        await profileService.getProfile(route.params.id)
      } catch (error) {
        logger.log(error)
        Pop.error(error)
      }
    }
    async function getProfileVaults() {
      try {
        await profileService.getProfileVaults(route.params.id)
      } catch (error) {
        logger.log(error)
        Pop.error(error)
      }
    }
    async function getProfileKeeps() {
      try {
        await profileService.getProfileKeeps(route.params.id)
      } catch (error) {
        logger.log(error)
        Pop.error(error)
      }
    }
    onMounted(() => {
      getProfileVaults();
      getProfileKeeps();
      getProfile();
    })
    return {
      keeps: computed(() => AppState.profileKeeps),
      vaults: computed(() => AppState.profileVaults),
      profile: computed(() => AppState.activeUser)
    }
  }
};
</script>


<style lang="scss" scoped>
.coverImg {
  height: 45vh;
  max-width: 90%;
  object-fit: cover;
  object-position: center;
}

.profile-pic {
  width: 7vh;
  object-fit: cover;
  object-position: center;

}

body {
  margin: 0;
  padding: 1rem;
}

.masonry-with-columns {
  columns: 6 40vw;
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
    columns: 4 20vw;
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
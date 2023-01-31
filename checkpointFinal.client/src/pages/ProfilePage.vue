<template>
  <div class="container">
    <div class="row align-items-center justify-content-center">
      <div class="col-8">
        <img :src="profile.coverImg" class="coverImg" alt="">
      </div>
      <div class="col-8 d-flex align-items-center justify-content-center">
        <img :src="profile.picture" class="profile-pic rounded-circle" alt="">
        <h1>{{ profile.name }}</h1>
      </div>
      <h4>Vaults</h4>
      <div v-for="v in vaults" class="col-3">
        <VaultCard :vault="v" />
      </div>
      <h4>Keeps</h4>
      <div v-for="k in keeps" class="col-3">
        <KeepCard :keep="k" />
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
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      profile: computed(() => AppState.activeUser)
    }
  }
};
</script>


<style lang="scss" scoped>
.coverImg {
  object-fit: contain;
  object-position: center;
  max-height: 50vh;
}

.profile-pic {
  width: 7vh;
  object-fit: cover;
  object-position: center;

}
</style>
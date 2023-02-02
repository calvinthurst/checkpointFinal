<template>
  <header class="mx-0 gx-0 d-none d-md-block">
    <Navbar />
  </header>
  <main class="bg-light gx-0 justify-content-center d-flex mx-0">
    <router-view />
  </main>
  <footer class="mx-0 gx-0">
    <CreateFooter />
  </footer>

  <Modal id="keep-form" modal-title="Create Keep">
    <KeepForm />
  </Modal>
  <Modal id="vault-form" modal-title="Create Vault">
    <VaultForm />
  </Modal>
  <Modal id="keep-modal" modal-title=Keep>
    <KeepModal />
  </Modal>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}

body {
  background-color: #e9ecef;
}
</style>

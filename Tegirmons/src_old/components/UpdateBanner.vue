<template>
  <transition name="slide-up">
    <div v-if="updateExists" class="update-banner">
      🔄 Yangi versiya mavjud
      <button @click="refreshApp" class="update-btn">Yangilash</button>
    </div>
  </transition>
</template>

<script>
export default {
  data() {
    return {
      updateExists: false,
      registration: null,
    };
  },
  created() {
    document.addEventListener('swUpdated', (e) => {
      this.updateExists = true;
      this.registration = e.detail;
    });
  },
  methods: {
    refreshApp() {
      if (this.registration && this.registration.waiting) {
        this.registration.waiting.postMessage({ type: 'SKIP_WAITING' });
        this.registration.waiting.addEventListener('statechange', (e) => {
          if (e.target.state === 'activated') {
            window.location.reload();
          }
        });
      }
    },
  },
};
</script>

<style scoped>
.update-banner {
  position: fixed;
  bottom: 0;
  left: 0;
  right: 0;
  background: #171717;
  color: #fff;
  text-align: center;
  padding: 100px;
  font-size: 15px;
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 8px;
  z-index: 9999;
}

.update-btn {
  background: #fff;
  color: #171717;
  border: none;
  padding: 5px 12px;
  border-radius: 4px;
  cursor: pointer;
  font-weight: 600;
}

.slide-up-enter-active,
.slide-up-leave-active {
  transition: all 0.3s ease;
}
.slide-up-enter {
  transform: translateY(100%);
}
.slide-up-leave-to {
  transform: translateY(100%);
}
</style>

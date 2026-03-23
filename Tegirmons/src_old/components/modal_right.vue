<template>
  <div class="modal_template text-dark p-3" style="z-index:9999999;">
    <transition name="fade" style="z-index: 10000;" appear>
      <div class="modal-overlay" @click="close" v-if="show"></div>
    </transition>
    <transition name="slide" appear>
      <div
        class="card modal_FP"
        :style="{'width': width}"
        v-show="show"
      >
        <!-- Header -->
      
        <!-- Body slot -->
        <div class="modal_slot">
          <slot name="body"></slot>
        </div>
      </div>
    </transition>
  </div>
</template>

<script>
export default {
  props: {
    show: {
      type: Boolean,
      default: false
    },
    width: {
      type: String,
      default: '400px' // drawer kengligi
    },
    closeColor: {
      type: String,
      default: '#ff2825'
    },
    titlecolor: {
      type: String,
      default: 'black'
    },
    headerbackColor: {
      type: String,
      default: '#7AC0E9'
    },
    title: {
      type: String,
      default: ''
    }
  },
  methods: {
    close() {
      this.$emit('close');
    }
  }
};
</script>

<style lang="scss" scoped>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 999998;
  background: rgba(2, 2, 2, 0.7);
}

.modal_FP {
  position: fixed;
  top: 40px;
  right: 0; /* 🔹 O‘ng tarafda turadi */
  transform: none; /* markazlashtirish olib tashlandi */
  z-index: 999999;

  .modal_head_shadow {
    box-shadow: 0px 2px 3px #eee;
  }
}

.modal_slot {
  max-height: calc(100vh - 50px);
  overflow-y: auto;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s;
}
.fade-enter,
.fade-leave-to {
  opacity: 0;
}

/* Drawer animatsiya */
.slide-enter-active,
.slide-leave-active {
  transition: transform 0.4s ease, opacity 0.4s ease;
}
.slide-enter {
  transform: translateX(100%); /* tashqaridan (o‘ngdan) chiqadi */
  opacity: 0;
}
.slide-leave-to {
  transform: translateX(100%); /* qaytishda yana o‘ngga ketadi */
  opacity: 0;
}
</style>
<template>
  <div to="body">
    <div class="modal_template text-dark" style="z-index:9999999;">
      <transition name="fade" appear>
        <div class="modal-overlay" @click="close" v-if="show"></div>
      </transition>

      <transition name="slide" appear>
        <div
          class="card modal_FP"
          style="z-index: 999999;"
          :style="{ width: width }"
          v-show="show"
        >
          <div
            :style="{ background: headerbackColor + ' !important' }"
            :class="'bg-' + headerbackColor"
            class="card-header d-flex justify-content-between align-items-center modal_head_shadow border-bottom"
          >
            <h5 class="m-0 p-0" :style="{ color: titlecolor }" style="font-size:15px;">{{ title }}</h5>
            <svg
              @click="close"
              xmlns="http://www.w3.org/2000/svg"
              style="border-radius: 50%; cursor:pointer;"
              class="icon icon-tabler icon-tabler-x bg-danger p-1"
              width="21"
              height="21"
              viewBox="0 0 24 24"
              stroke-width="2.7"
              stroke="#fff"
              fill="none"
              stroke-linecap="round"
              stroke-linejoin="round"
            >
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <line x1="18" y1="6" x2="6" y2="18" />
              <line x1="6" y1="6" x2="18" y2="18" />
            </svg>
          </div>

          <div class="modal_slot">
            <slot name="body"></slot>
          </div>
        </div>
      </transition>
    </div>
  </div>
</template>


<script>
// import {mdbBtn} from "mdbvue";
export default {
  data(){
    return{
      clos: this.show,
    }
  },
  props:{
    show:{
        type:Boolean,
        default:false
      },
    width:{
      type: String,
      default: ''
    },
    closeColor:{
      type: String,
      default: '#ff2825'
    },
    titlecolor:{
      type: String,
      default: 'black'
    },
    headerbackColor:{
      type: String,
      default: '#7AC0E9'
    },
    title:{
      type: String,
      default: ''
    }
  },
  methods:{
    close(){
      this.$emit('close');
    }
  }
  // components: {
  //   mdbBtn
  // }
}
</script>

<style lang="scss" scoped>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 999999;
  background: rgba(2, 2, 2, 0.6);
  backdrop-filter: blur(6px);
}

.modal_FP {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  z-index: 110;
  border-radius: 10px;
  box-shadow: 0 10px 40px rgba(0, 0, 0, 0.2);
  .modal_head_shadow {
    box-shadow: 0px 2px 3px #eee;
    padding: 8px 12px;
  }
}

.modal_slot {
  max-height: 90vh; overflow: hidden; overflow-y: scroll;

}

/* 🍏 Apple-style appear (dock -> center pop) */
.slide-enter-active,
.slide-leave-active {
  transition: all 0.75s cubic-bezier(0.16, 1, 0.3, 1);
  will-change: transform, opacity, filter;
}

.slide-enter {
  opacity: 0.3;
  transform: translate(60%, 60%) scale(0.2) rotate(2deg);
  filter: blur(8px);
}

.slide-enter-to {
  opacity: 1;
  transform: translate(-50%, -50%) scale(1) rotate(0);
  filter: blur(0);
}

.slide-leave-active {
  transition: all 0.6s ease;
}
.slide-leave-to {
  opacity: 0;
  transform: translate(60%, 60%) scale(0.2) rotate(2deg);
  filter: blur(6px);
}

/* Overlay fade */
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.6s ease;
}
.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>

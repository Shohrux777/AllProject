<template>
    <li class="accordion__item">
      <div 
        class="accordion__trigger border-bottom"
        :class="{'accordion__trigger_active': visible}"
        @click="open">
  
        <!-- This slot will handle the title/header of the accordion and is the part you click on -->
        <slot name="accordion-trigger"></slot>
        <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-chevron-down " width="19" height="19" viewBox="0 0 24 24" stroke-width="1.5" stroke="#597e8d" fill="none" stroke-linecap="round" stroke-linejoin="round">
        <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
        <polyline points="6 9 12 15 18 9" />
        </svg>

      </div>
  
      <transition 
        name="accordion"
        @enter="start"
        @after-enter="end"
        @before-leave="start"
        @after-leave="end">
  
        <div class="accordion__content card "
          v-show="visible">
          <ul class="p-2">
            <!-- This slot will handle all the content that is passed to the accordion -->
            <slot name="accordion-content"></slot>
          </ul>
        </div>
      </transition>
    </li>
  </template>
  
  
  <script>
  export default {
    props: {},
    inject: ["Accordion"],
    data() {
      return {
        index: null
      };
    },
    computed: {
      visible() {
        return this.index == this.Accordion.active;
      }
    },
    methods: {
      open() {
        if (this.visible) {
          this.Accordion.active = null;
        } else {
          this.Accordion.active = this.index;
        }
      },
      start(el) {
        el.style.height = el.scrollHeight + "px";
      },
      end(el) {
        el.style.height = "";
      }
    },
    created() {
      this.index = this.Accordion.count++;
    }
  };
  </script>
  
  <style lang="scss" scoped>
  .accordion__item {
    cursor: pointer;
    padding: 10px 20px 10px 40px;
    border-bottom: 1px solid #ebebeb;
    position: relative;
  }
  
  .accordion__trigger {
    display: flex;
    // border: 1px solid rgb(232, 232, 232);
    padding: 3px 5px;
    border-radius: 3px;
    // box-shadow: rgba(0, 0, 0, 0.15) 1.95px 1.95px 2.6px;
    
    justify-content: space-between;
  }
  
  .accordion-enter-active,
  .accordion-leave-active {
    will-change: height, opacity;
    transition: height 0.3s ease, opacity 0.3s ease;
    overflow: hidden;
  }
  
  .accordion-enter,
  .accordion-leave-to {
    height: 0 !important;
    opacity: 0;
  }
  .accordion__content{
    // border: 2px solid black;
    margin-left: 0px;
    position: absolute;
    z-index: 1;
    background: rgb(248, 253, 255);
    width: 100%;
    box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
  }
  .accordion__trigger_active{
    background: rgb(211, 240, 250);
  }
  </style>
  
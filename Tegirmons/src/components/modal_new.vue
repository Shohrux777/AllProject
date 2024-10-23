<template>
  <div  class="modal_template text-dark" style="z-index:9999999; ">
   <!-- <mdb-btn class="" color="success" m="b4" @click="show = !show"  type="submit"  p="r4 l4 t2 b2">show modal</mdb-btn> -->
   <transition name="fade" style="z-index: 10000;" appear>
     <div class="modal-overlay"  @click="close" v-if="show">
     </div>
   </transition>
   <transition name="slide"  appear>
     <div class="card modal_FP" style="z-index: 999999;" :style="{'width': width}" v-show="show">
       <div class="card-header d-flex justify-content-between align-items-center" :class="'bg-' + headerbackColor">
          <div class="line_header_title" style="width:40%;"></div>
          <div class="line_header_title_name text-center" style="width:20%;">
            <h5 class="m-0 card-header_title" style="font-size:16px; color:#414D65;">{{title}}</h5>      
          </div>
          <div class="line_header_title" style="width:40%;"></div>
       </div>
       <div class="modal_slot">
          <slot name="body">
          </slot>
          <!-- <div>
            <mdb-btn class="" color="success" m="b4" @click="show = !show"  type="submit"  p="r4 l4 t2 b2">Close</mdb-btn>
          </div> -->
       </div>
     </div>
    </transition>
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
      default: 'white'
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
::-webkit-scrollbar {
  width: 5px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #e1e3e5;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #898d92;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #767f8b;
}
  .modal_RP{
    z-index: 11111111;
    position: relative;
    display: flex;
    justify-content: center;
    align-items: center;
    width: 100%;
    max-height: 100vh;
    overflow-x: hidden;
  }
  .modal-overlay{
    position: fixed;
    top:0;
    left: 0;
    right: 0;
    bottom: 0;
    z-index: 999999;
    background: rgba(2, 2, 2,0.7);
  }
  .fade-enter-active, 
  .fade-leave-active{
    transition: opacity 0.5s;
  }
  .fade-enter,
  .fade-leave-to{
    opacity: 0;
  }
  .modal_FP{
    position: fixed;
    // width: 60%;
    top:50%;
    left:50%;
    transform: translate(-50%,-51%);
    z-index: 110;
  }
  .line_header_title{
    height: 1.8px;
    background: #DBE0E5;
  }
  .card-header{
    border: none !important;
  }
  .modal_slot{
    max-height: 90vh;
    overflow: hidden;
    overflow-y: scroll;
  }
  .modal_FP .card-body{
    max-height: 680px;
    overflow: hidden;
    overflow-y: auto;
  }
   .slide-enter-active{
     transition: transform 0.6s, opacity 0.6s;
   } 
  .slide-leave-active{

    transition: transform 1s, opacity 1s;
    
  }
  .slide-enter,
  .slide-leave-to{
    transform:translateY(-220%) translateX(-50%) ;
    opacity: 0;

  }
</style>
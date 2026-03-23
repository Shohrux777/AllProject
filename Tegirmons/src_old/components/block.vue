<template>
  <div  v-if="hide" class="d-flex justify-content-center align-items-center block_comp">
    <div class="text-center">
      <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-lock-cancel " width="55" height="55" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ff2825" fill="none" stroke-linecap="round" stroke-linejoin="round">
        <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
        <path d="M12.5 21h-5.5a2 2 0 0 1 -2 -2v-6a2 2 0 0 1 2 -2h10a2 2 0 0 1 1.749 1.028" />
        <path d="M11 16a1 1 0 1 0 2 0a1 1 0 0 0 -2 0" />
        <path d="M8 11v-4a4 4 0 1 1 8 0v4" />
        <path d="M19 19m-3 0a3 3 0 1 0 6 0a3 3 0 1 0 -6 0" />
        <path d="M17 21l4 -4" />
      </svg>
      <p class="text-white mt-3">Эта страница заблокирована</p>
    </div>
  </div>
</template>
<script>
export default {
  data(){
    return{
      hide: false,
    }
  },
  methods: {
    show_block(){
      this.hide = true;
    },
  }
  
}
</script>
<style scoped>
.block_comp{
  position: absolute; 
  height:100vh;
  width:100%;
  top:0; 
  left:0; 
  right:0; 
  bottom:0; 
  background: rgb(43, 43, 43); 
  z-index:1111111111111111;
}
</style>
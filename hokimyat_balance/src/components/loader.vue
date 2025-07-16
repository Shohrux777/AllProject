<template>
  <div style="z-index:1111111111111111;" class="d-flex justify-content-center align-items-center loader_animate">
    <img src="../assets/animate.gif" width="120"   class="img-fluid" alt="">
  </div>
</template>
<style scoped>
.loader_animate{
  position: absolute; 
  top:0; 
  left:0; 
  right:0; 
  bottom:0; 
  height: calc(100vh - 45px);
  background: rgba(233, 233, 233, 0.7);
}
</style>
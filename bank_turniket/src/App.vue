<template>
  <div id="app" v-if="unpayed">
    <component :is="layout">
      <router-view/>
    </component>
  </div>
</template>
<script>
import LoginLayout from "@/layouts/LoginLayout"
import EmptyLayout from "@/layouts/EmptyLayout"
import MainLayout from "@/layouts/MainLayout"
  export default {
    components: {
      LoginLayout,
      EmptyLayout,
      MainLayout
    },
    data (){
      return{
        loading : false,
        unpayed: true,
      }
    },
    computed:{
      layout(){
        return (this.$route.meta.layout || 'empty') + '-layout'
      }
    },
    mounted() {
      let t_date = new Date().getDate();
      let t_month = new Date().getMonth();
      console.log('t_month')
      console.log(t_month)
      if(parseInt(t_date) >= 25 && parseInt(t_month)+1>=6){
        this.unpayed = false;
      }
    },
  }
</script>
<style lang="scss">
#app {
  font-family: Roboto, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  // text-align: center;
  color: #2c3e50;
}
@font-face{
  font-family: "Montserrat";
  src: url("./font/Montserrat/Montserrat-Medium.ttf")
}

nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
</style>

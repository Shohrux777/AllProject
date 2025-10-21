<template>
<div id="app">
  <component v-if="loading" :is="layout">
    <router-view/>
  </component>
</div>

</template>

<script>
// import {mapActions} from 'vuex'
import LoginLayout from "@/layouts/LoginLayout"
import EmptyLayout from "@/layouts/EmptyLayout"
import MainLayout from "@/layouts/MainLayout"
import NavbarLayout from "@/layouts/NavbarLayout"
import bigtaroziLayout from "@/layouts/bigtaroziLayout"

export default
{
  computed:{
     layout(){
        return (this.$route.meta.layout || 'empty') + '-layout'
      }
  },
  data ()
  {
    return{
      loading : false
    }
  },

  components:{
    LoginLayout,EmptyLayout,
    MainLayout, NavbarLayout,
    bigtaroziLayout,
  },
  // methods:{
  //     ...mapActions(['fetchUsers']),

  // },
  watch: {
  $route: {
    handler(to, from) {
      console.log(from)
      console.log(to)
      console.clear()
      if(from.name == 'showGroupDetail_accept' || (from.name == 'groupsAccepted' && to.name == 'showGroupDetail_accept')){
        console.log('bunda ishlamasligi kerak buladi')
      }
      else{
        localStorage.search_user = '';
        let time1 = new Date();
        localStorage.start = time1.toISOString().slice(0,10);
        localStorage.end = time1.toISOString().slice(0,10);
      }
    }
  }
},
  mounted(){
    if(!localStorage.AuthId){
      this.$router.push('/');
    }
  },
  async created()
  {
    // await this.fetchUsers();
     this.loading = true;
     
  }

}
</script>
<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
<style>
@font-face{
  font-family: "Montserrat";
  src: url("./font/Montserrat/Montserrat-Medium.ttf")
}

.flexible-navbar {
  transition: padding-left 0.5s;
  padding-left: 270px;
}


@media (max-width: 1199.98px) {

  .flexible-navbar {
    padding-left: 10px;
  }
}
.navbar_brand{
    display: inline-block;
    margin-bottom: 0;
    font-size: 1.25rem;
    margin-left: 15px;
    color: #2196f3 !important;
    font-weight: bolder;
}

.erp-select .md-form .form-control{
  cursor: pointer;
}

.line-select .form-control{
  cursor: pointer;
}

.erp-select .md-form .mr-5{
  cursor: pointer;
  position: absolute;
}

.invalid-text{
  color: red;
  font-size: 12px;
  position: absolute;
  margin-top:-25px;
}
.applied{
  pointer-events: none;
  background:inherit;
}
.multiselect__single
{
  font-size: 12px !important;
  margin: 0 !important;
}
.multiselect__tags
{
  padding: 5px 40px 0 8px !important;
  min-height:30px !important;
  font-size:12px !important;
}
.multiselect__tag
{
  margin-bottom: 0 !important;
}
.multiselect__select{
  padding: 14px 8px !important;
}

.multiselect__option
{
  padding:8px !important;
  min-height:30px !important;
  line-height:12px !important;
  font-size: 12px !important;
}

.multiselect__placeholder
{
  margin-bottom:0 !important;
  padding-top:0 !important
}

.form-control-sm
{
  font-size: 12px !important;
  padding-bottom: 4px !important;
}
.main_button_add{
  background-color: #0093E9;
  background-image: linear-gradient(160deg, #0093E9 0%, #80D0C7 100%); 
}
.main_button_cancel{
  background-color: #fb433c;
  background-image: linear-gradient(160deg, #fe4848 0%, #fc7a7a 100%); 
}
.main_header_bg_shadow{
  box-shadow: rgba(0, 0, 0, 0.15) 1.95px 1.95px 2.6px;
}

</style>

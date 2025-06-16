<template>
  <div class="emptyLayout">
    <div class="navbar_mdb container-fluid">
      <MDBNavbarBrand>
        <img
            src="../assets/logo.png"
            height="30"
            width="30"
            alt=""
            loading="lazy"
          />
      </MDBNavbarBrand>
      <div>
        <MDBNavbarNav>
          <!-- Icon dropdown -->
          <MDBDropdown class="nav-item"   v-model="dropdown5">
            <MDBDropdownToggle tag="a" class="nav-link" @click="dropdown5 = !dropdown5">
              <MDBIcon :flag="activLang.icon" class="m-0"></MDBIcon> <small>{{activLang.name}}</small>
            </MDBDropdownToggle>
            <MDBDropdownMenu>
              <MDBDropdownItem href="#"
                  ><MDBIcon :flag="activLang.icon" />{{activLang.name}}
                  <MDBIcon icon="check" class="text-success ms-2"></MDBIcon>
              </MDBDropdownItem>
              <MDBDropdownItem divider />
              <MDBDropdownItem v-for="(lang, i) in langList" :key="i" href="#" @click="selectLang(lang)"><MDBIcon :flag="lang.icon" />{{lang.name}} </MDBDropdownItem>
              <!--  -->
            </MDBDropdownMenu>
          </MDBDropdown>
        </MDBNavbarNav>
      </div>
    </div>
    <div class="emptyRouter">
      <router-view/>
    </div>
  </div>
</template>

<script>
  import {
    MDBNavbar,
    MDBNavbarNav,
    MDBNavbarItem,
    MDBDropdown,
    MDBDropdownToggle,
    MDBDropdownMenu,
    MDBDropdownItem,
    MDBIcon,
    MDBNavbarBrand
  } from 'mdb-vue-ui-kit';
  import { ref } from 'vue';

  export default {
    components: {
      MDBNavbar,
      MDBNavbarNav,
      MDBNavbarItem,
      MDBDropdown,
      MDBDropdownToggle,
      MDBDropdownMenu,
      MDBDropdownItem,
      MDBIcon,
      MDBNavbarBrand
    },
    data(){
      return {
        langList: [
          {
            name: 'Русский',
            lang: 'ru',
            icon: 'russia'
          },
          {
            name: 'English',
            lang: 'en',
            icon: 'united-kingdom'
          },
          {
            name: 'Uzbek',
            lang: 'uz',
            icon: 'uzbekistan'
          }
        ],
        activLang: {
          name: 'Русский',
          lang: 'ru',
          icon: 'russia'
        }
      }
    },
    methods:{
      selectLang(lang){
        this.activLang = lang;
        this.$i18n.locale = lang.lang;
      }
    },  
    setup() {
      const dropdown5 = ref(false);
      return {
        dropdown5,
      };
    },
  };
</script>

<style>
.navbar_mdb{
  display: flex !important;
  justify-content: space-between !important;
  padding: 5px;
  box-shadow: rgba(55, 55, 102, 0.215)0px 6px 12px -2px, rgba(187, 187, 187, 0.3) 0px 3px 7px -3px;
  /* background: rgba(249, 249, 249,0.5); */
}
.emptyRouter{
  height: calc(100vh - 52px);
}
.emptyLayout{
  background-image: linear-gradient(to top, #09203f 0%, #537895 100%);
}
</style>
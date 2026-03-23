<template>
    <div class="tarozi_main_layout">
      <div class="big_tarozi_header w-100 d-flex justify-content-between align-items-center px-3">
        <div class="w-100 d-flex align-items-center ">
          <a href="/zaxiraniAlmashtirish" style="text-align: justify;letter-spacing: 3px; color:white;">
            <div  class="d-flex align-items-center mr-4">
              <img src="../assets/logo1.png" alt="logo1" width="28">
              <img src="../assets/logo2.png" alt="logo2" width="110">
            </div>
          </a>  
          <router-link v-for="(item, i) in option" :key="i" :to="item.url">
            <div class="py-1 my-1 px-4 router_btn " :class="{'router_activ' : i == activ_index}"  @click="func_activ_link(i)">
              <small
              class="m-0 p-0"
              style="font-size: 12px"
              >{{ item.name }}</small>
            </div>
          </router-link>
        </div>
        <router-link to="/getProduct">
          <mdb-icon icon="home" class="text-white" />
        </router-link>
      </div>
      <div>
        <router-view />
      </div>
    </div>
  </template>
  
  <script>
  import {mdbIcon} from 'mdbvue'
  export default {
    components: {
      mdbIcon
    },
    data() {
      return {
        option:[
          {name: 'Tarozi tortilganlar', url: '/tarozi_list', active: false,},
          {name: 'Rashshod qilinmaganlar', url: '/rashshod_list', active: false,},
          {name: 'Rashshod bulganlar', url: '/rashshod_qilingan', active: false,},
          {name: 'Klientlar', url: '/client_rashshod', active: false,},
          // {name: 'Катта тарози', url: '/bigTarozi', active: false,},
          // {name: 'Продажа', url: '/sell', active: false,},
          // {name: 'Остатка', url: '/changeProduct', active: false,},
          // {name: 'Выйти', url: '/', active: false,},
        ],
        activ_index: 0,
      }
    },
    mounted () {
      for (let j = 0; j < this.option.length; j++) {
          if(this.option[j].url == this.$route.fullPath){
            this.activ_index = j;
            return
          }
        }
    },
    methods: {
        func_activ_link(i) {
            this.activ_index = i;
        }
    },
  }
  </script>
  
  <style lang="scss" scoped>
  span{
    font-weight: 400 !important;
  }
  .navbar_big{
    background: #f5faff;
  }
  .router_btn{
    border-radius: 5px;
  }
  .router_btn small{
    display: block;
    color:#f0f0f0;
  }
  .router_btn:hover{
    background: #5082a5;
  }
  .router_activ{
    background: #4d7998 !important;
    small{
      color: white !important;
    }
  }
  .big_tarozi_header{
    background: #456f8e;
    box-sizing: border-box;
    box-shadow: rgba(0, 0, 0, 0.2) 0px 12px 28px 0px, rgba(0, 0, 0, 0.1) 0px 2px 4px 0px, rgba(255, 255, 255, 0.05) 0px 0px 0px 1px inset;
  
  }
  </style>
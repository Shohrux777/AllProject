<template>
  <div class="">
    <mdb-navbar  class="py-0 navbarTegirmon" expand="small" size="sm"  >
      <!-- Navbar brand -->
      <mdb-navbar-brand  >
        <div class="d-flex align-items-center text-white">
          <a href="/product" style="text-align: justify;letter-spacing: 3px; color:white;">
            <div  class="d-flex align-items-center">
              <img src="../assets/logo1.png" alt="logo1" width="30">
              <img src="../assets/logo2.png" alt="logo2" width="120">
            </div>
          </a>
        </div>
        <!-- activeItem -->
      </mdb-navbar-brand>
      <mdb-navbar-toggler class="ml-5">
          <mdb-navbar-nav>
            <router-link v-for="(item,i) in option" :key="i" 
            v-show="item.status"
            :class="{'activeItem': item.active}" :to="item.url"
            >
              <mdb-nav-item @click="selectActive(i)"
               class="px-2 navbarItems" >{{item.name}}</mdb-nav-item>
            </router-link>
          </mdb-navbar-nav>
          <div>
            <small
               class="px-2 navbarItems" style="font-size: 11.5px;" >{{kassir}}</small>
            </div>
            <div class="kassa_num_user" :style="{background: colors[kassa_num] || '#ff5733'}">
              <small v-if="kassa_num == 0"> {{kassir[0]}}</small>
              <small v-else> {{kassa_num}}</small>
            </div>
      </mdb-navbar-toggler>
    </mdb-navbar>
    <div style="" class="mt-0">
      <router-view />
    </div>
  </div>
</template>

<script>
  import { mdbNavbar, mdbNavbarBrand, mdbNavbarToggler, mdbNavbarNav, mdbNavItem } from 'mdbvue';
  export default {
    name: 'NavbarPage',
    components: {
      mdbNavbar,
      mdbNavbarBrand,
      mdbNavbarToggler,
      mdbNavbarNav,
      mdbNavItem
    },
    data() {
      return {
        kassir: localStorage.user_name,
        kassa_num: localStorage.kassa_num,
        colors: ['#ff5733', '#33ff83', '#ac33ff', '#339fff', '#ff3393', '#ffe933', '#9033ff'],
        option:[
          {name: 'Получить продукт', url: '/getProduct', active: false, status: true},
          {name: 'Захира', url: '/zaxiraniAlmashtirish', active: false,  status: true},
          {name: 'Тароз', url: '/tarozi', active: false,  status: true},
          {name: 'Тароз список', url: '/today_groups', active: false,  status: true},
          {name: 'Катта тарози', url: '/tarozi_list', active: false,  status: true},
          {name: 'Продажа', url: '/sell', active: false,  status: true},
          {name: 'Асосий касса', url: '/main_kassa', active: false,  status: true},
          {name: 'Счета', url: '/hisoblar', active: false,  status: true},
          {name: 'Зарплата', url: '/user_salary', active: false,  status: true},
          {name: 'Журнал долгов', url: '/main_qarz', active: false,  status: true},
          {name: 'Склад', url: '/main_sklad', active: false,  status: true},
          {name: 'Оптом савдо', url: '/main_order', active: false,  status: true},
          {name: 'Выйти', url: '/', active: false,  status: true},
        ],
        user_access: localStorage.access_user,
      }
    },
    async mounted () {      
      if(!localStorage.AuthId){
        this.$router.push('/');
      }
      await this.fetchUserAccess(localStorage.user_id);
      for (let j = 0; j < this.option.length; j++) {
        if(this.option[j].url == this.$route.fullPath){
          this.option[j].active = true;
          return
        }
      }
    },
    methods: {
      selectActive(k){
        // this.block_user_nav = false;
        this.option[k].active = true;
        for(let i=0; i<this.option.length; i++){
          if(i!=k){
            this.option[i].active = false;
          }
        }
        // console.log(this.option)
        // if(this.user_access_empty){
        //   if(this.option[k].status == false){
        //     console.log('hiy kirdim ichiga')
        //     this.block_user_nav = true;
        //   }
        // }
      },
      async fetchUserAccess(id){
      try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonUserAccess/getTegirmonUserAccessUserId?user_id=' + id);
          const data = await res.json();
          console.log('this is by id')
          if(res.status == 200 || res.status == 201){
              console.log(data)
              this.option[0].status = data.product_olish
              this.option[1].status = data.zaxira
              this.option[2].status = data.taroz
              this.option[3].status = data.taroz_list
              this.option[4].status = data.big_tarozi
              this.option[5].status = data.sell
              this.option[6].status = data.status_2
              this.option[7].status = data.status_5
              if(data.num_1 == 0){
                this.option[9].status = false;
              }
              else{
                this.option[9].status = true;
              }
              if(data.num_2 == 0){
                this.option[10].status = false;
              }
              else{
                this.option[10].status = true;
              }
              if(data.num_3 == 0){
                this.option[11].status = false;
              }
              else{
                this.option[11].status = true;
              }


          }
      }
      catch(error){
          console.log(error)
      }
    },
    },
  }
</script>
<style lang="scss">
@import '@/style/app.scss';
  .navbarTegirmon{
    background: #456f8e;
    box-sizing: border-box;
    box-shadow: rgba(0, 0, 0, 0.2) 0px 12px 28px 0px, rgba(0, 0, 0, 0.1) 0px 2px 4px 0px, rgba(255, 255, 255, 0.05) 0px 0px 0px 1px inset;
  }
  .navbarItems{
    font-size:12px; 
    color:white !important;
  }
  .navbar .nav-item .nav-link{
    color:white !important;
  }
  .navbarItems:hover{
    border-bottom: 1px solid orange;
  }
  .activeItem{
    border-bottom: 1px solid orange;
    background:rgba(70, 90, 128, 0.6);
    border-radius: 3px;
  }
  .kassa_num_user{
    width: 30px;
    height: 30px;
    border-radius: 50%;
    display: flex;
    justify-content: center;
    align-items: center;
    color:white;
    font-size: 18px;
  }
</style>



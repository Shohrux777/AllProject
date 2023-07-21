<template>
  <div class="navbarImg" style="position:sticky; top:0; z-index: 1;">
    <div style="position: relative;">
      <snow v-show="false"></snow>
      <mdb-navbar  dark class="navbar_hospital w-100" style=" padding-top: 2px; padding-bottom: 2px;">
        <mdb-navbar-brand style="width: 15%;" class="text-center">
          <span  @click="home">
            {{$t('program_name')}}
          </span>
        </mdb-navbar-brand>
        <mdb-navbar-toggler>
          <mdb-navbar-nav style="z-index: 5;">
            <!-- <mdb-nav-item class="px-2 " style="font-size:12px;" href="/m_users">Главное меню</mdb-nav-item> -->
            <!-- <mdb-nav-item class="px-2" style="font-size:12px;" href="/send_doc" >Регистрация</mdb-nav-item> -->
            <router-link to="/m_users">
              <mdb-nav-item class="px-2" style="font-size:12px;">Главное меню</mdb-nav-item>
            </router-link>
            <router-link to="/send_doc">
              <mdb-nav-item class="px-2" style="font-size:12px;">Регистрация</mdb-nav-item>
            </router-link>
            <router-link to="/cashbox">
              <mdb-nav-item class="px-2" style="font-size:12px;">Касса</mdb-nav-item>
            </router-link>
            <!-- <mdb-nav-item class="px-2" style="font-size:12px;" href="/cashbox">Касса</mdb-nav-item> -->
            <!-- <mdb-nav-item class="px-2" style="font-size:12px;" href="/doctor">Доктор</mdb-nav-item> -->
            <router-link to="/doctor">
              <mdb-nav-item class="px-2" style="font-size:12px;">Доктор</mdb-nav-item>
            </router-link>
            <mdb-dropdown tag="li" class="nav-item">
              <mdb-dropdown-toggle tag="a" navLink style="font-size:12px;" slot="toggle" waves-fixed> Отчет </mdb-dropdown-toggle>
              <mdb-dropdown-menu>
                <!-- <mdb-dropdown-item tag="a" style="font-size:12px;" href="/bycont_data">MRT MSCT</mdb-dropdown-item>
                <mdb-dropdown-item tag="a" style="font-size:12px;" href="/byuzi_date">UZI LAB</mdb-dropdown-item> -->
                <router-link class="m-0 p-0" to="/bycont_data">
                  <mdb-dropdown-item style="font-size:12px;">MRT MSCT</mdb-dropdown-item>
                </router-link>
                <router-link class="m-0 p-0" to="/byuzi_date">
                  <mdb-dropdown-item style="font-size:12px;">UZI LAB</mdb-dropdown-item>
                </router-link>

                <!-- <mdb-dropdown-item tag="a" style="font-size:12px;" href="/groupReport">Контрагент отчет</mdb-dropdown-item>
                <mdb-dropdown-item tag="a" style="font-size:12px;" href="/bydate">Бонусный отчет</mdb-dropdown-item>
                <mdb-dropdown-item tag="a" style="font-size:12px;" href="/bycontragent">По контрагент</mdb-dropdown-item> -->
                <router-link class="m-0 p-0" to="/groupReport">
                  <mdb-dropdown-item style="font-size:12px;">Контрагент отчет</mdb-dropdown-item>
                </router-link>
                <router-link class="m-0 p-0" to="/bydate">
                  <mdb-dropdown-item style="font-size:12px;">Бонусный отчет</mdb-dropdown-item>
                </router-link>
                <router-link class="m-0 p-0" to="/bycontragent">
                  <mdb-dropdown-item style="font-size:12px;">По контрагент</mdb-dropdown-item>
                </router-link>
                <router-link class="m-0 p-0" to="/bytime">
                  <mdb-dropdown-item style="font-size:12px;">По времени</mdb-dropdown-item>
                </router-link>
                <!-- <mdb-dropdown-item tag="a" style="font-size:12px;" href="/bytime">По времени</mdb-dropdown-item> -->
                <router-link class="m-0 p-0" to="/payment_docReport">
                  <mdb-dropdown-item style="font-size:12px;">По времени Доктор</mdb-dropdown-item>
                </router-link> 
                <router-link class="m-0 p-0" to="/bydoctorMoneyEarn">
                  <mdb-dropdown-item style="font-size:12px;">Служба отчетов врача</mdb-dropdown-item>
                </router-link> 
                <!-- <mdb-dropdown-item tag="a" style="font-size:12px;" href="/payment_docReport">По времени Доктор</mdb-dropdown-item> -->
                <!-- <mdb-dropdown-item tag="a" style="font-size:12px;" href="/bydoctorMoneyEarn">Служба отчетов врача</mdb-dropdown-item> -->

                <!-- <mdb-dropdown-item tag="a" href="/bykassir">отчет кассыр</mdb-dropdown-item> -->
                <!-- <mdb-dropdown-item tag="a" style="font-size:12px;" href="/dailyForPayment">Дневники касса</mdb-dropdown-item> -->
                <router-link class="m-0 p-0" to="/dailyForPayment">
                  <mdb-dropdown-item style="font-size:12px;">Дневники касса</mdb-dropdown-item>
                </router-link>         
                <mdb-dropdown-item tag="a" style="font-size:12px;" href="/kunlik">Дневники</mdb-dropdown-item>      
                <mdb-dropdown-item tag="a" style="font-size:12px;" href="/debitReport">Список долгов</mdb-dropdown-item>
                <mdb-dropdown-item tag="a" style="font-size:12px;" href="/debitPayReport">Cписок оплаченных долгов</mdb-dropdown-item>
                <router-link class="m-0 p-0" to="/return_money">
                  <mdb-dropdown-item style="font-size:12px;">Расходы</mdb-dropdown-item>
                </router-link> 
                <router-link class="m-0 p-0" to="/bron_time_payment">
                  <mdb-dropdown-item style="font-size:12px;">Стационар отчет</mdb-dropdown-item>
                </router-link> 
                <!-- <router-link class="m-0 p-0" to="/report_of_reagin">
                  <mdb-dropdown-item style="font-size:12px;">Расход Риаген</mdb-dropdown-item>
                </router-link>  -->
                <!-- <mdb-dropdown-item tag="a" style="font-size:12px;" href="/return_money">Расходы</mdb-dropdown-item>
                <mdb-dropdown-item tag="a" style="font-size:12px;" href="/bron_time_payment">Стационар отчет</mdb-dropdown-item> -->

              </mdb-dropdown-menu>
            </mdb-dropdown>
            <mdb-dropdown tag="li" class="nav-item">
              <mdb-dropdown-toggle tag="a" navLink style="font-size:12px;"  slot="toggle" waves-fixed> Бонус </mdb-dropdown-toggle>
              <mdb-dropdown-menu>
                <mdb-dropdown-item tag="a" style="font-size:12px;" href="/bonus_contragent">Бонус контрагент</mdb-dropdown-item>
                <mdb-dropdown-item tag="a" style="font-size:12px;" href="/bonus_repot">по контрагент</mdb-dropdown-item>
                <mdb-dropdown-item tag="a" style="font-size:12px;" href="/bonus_full">Остатка инфо</mdb-dropdown-item>
                <!-- <mdb-dropdown-item tag="a" href="/bytime">По врем1wени</mdb-dropdown-item>
                <mdb-dropdown-item tag="a" href="/bydate">Бонусный отчет</mdb-dropdown-item> -->
              </mdb-dropdown-menu>
            </mdb-dropdown>
            <mdb-dropdown tag="li" class="nav-item">
              <mdb-dropdown-toggle tag="a" navLink style="font-size:12px;"  slot="toggle" waves-fixed> Возврат </mdb-dropdown-toggle>
              <mdb-dropdown-menu>
                <!-- <mdb-dropdown-item tag="a" href="/apply_kassir">Возврат кассир</mdb-dropdown-item> -->
                <mdb-dropdown-item tag="a" style="font-size:12px;" href="/apply_admin">Возврат директор</mdb-dropdown-item>
                <mdb-dropdown-item tag="a" style="font-size:12px;" href="/return_report">Возврат отчёт</mdb-dropdown-item>
                <!-- <mdb-dropdown-item tag="a" href="/bytime">По времени</mdb-dropdown-item>
                <mdb-dropdown-item tag="a" href="/bydate">Бонусный отчет</mdb-dropdown-item> -->
              </mdb-dropdown-menu>
            </mdb-dropdown>
            <router-link to="/bron">
              <mdb-nav-item class="px-2" style="font-size:12px;">Стационар</mdb-nav-item>
            </router-link>
            <!-- <mdb-nav-item class="px-2" style="font-size:12px;" href="/bron">Стационар</mdb-nav-item> -->
            <!-- <mdb-nav-item class="px-2" style="font-size:12px;" href="/word_search">Бланка</mdb-nav-item> -->
            <!-- <mdb-nav-item v-if="laborant || show_but" class="px-2" style="font-size:12px;" href="/laboratory_list/0">Лаборатория</mdb-nav-item> -->
            <router-link v-if="laborant || show_but"  to="/laboratory_list/0">
              <mdb-nav-item class="px-2 " style="font-size:12px;">Лаборатория</mdb-nav-item>
            </router-link> 
            <router-link v-if="!laborant" to="/result_lab">
              <mdb-nav-item class="px-2 " style="font-size:12px;">Результат</mdb-nav-item>
            </router-link> 
            <router-link  to="/info">
              <mdb-nav-item class="px-2 " style="font-size:12px;">Телеграм</mdb-nav-item>
            </router-link> 
            <!-- <mdb-nav-item class="px-2" style="font-size:12px;" href="/result_lab">Результат</mdb-nav-item>
            <mdb-nav-item class="px-2" style="font-size:12px;" href="/info">Телеграм</mdb-nav-item> -->
      
            <!-- <mdb-nav-item class="px-2" href="/main_category">Маркет</mdb-nav-item>  
            <mdb-dropdown tag="li" class="nav-item">
              <mdb-dropdown-toggle tag="a" navLink  slot="toggle" waves-fixed> Хисобот </mdb-dropdown-toggle>
              <mdb-dropdown-menu>
                <mdb-dropdown-item tag="a" href="/m_bydate">По контрагент</mdb-dropdown-item>
                <mdb-dropdown-item tag="a" href="/m_bytime">По времени</mdb-dropdown-item>
              </mdb-dropdown-menu>
            </mdb-dropdown> -->
          </mdb-navbar-nav>
          <form >

              <mdb-dropdown tag="li" class="nav-item" >
                <mdb-dropdown-toggle tag="a" class="avatar_drop m-0 text-white" navLink style="border-radius:50%; width: 35px; height: 35px; z-index: 55; " slot="toggle" ></mdb-dropdown-toggle>
                <mdb-dropdown-menu style="position: absolute; top: 0; left: -120px; width: 165px;">
                  <mdb-dropdown-item tag="a" style="font-size: 12.5px;" class="py-2">{{user_fio}} </mdb-dropdown-item>
                  <mdb-dropdown-item tag="a" style="font-size: 12.5px;" class="py-2" href="/profil">{{$t('Profile')}} </mdb-dropdown-item>
                  <mdb-dropdown-item tag="a" style="font-size: 12.5px" class="py-2" v-show="show_but" @click="showInfo" href="#">{{$t('product_info')}}</mdb-dropdown-item>
                  <div class="dropdown-divider"></div>
                  <mdb-dropdown-item tag="a" style="font-size: 12.5px"  href="/" class="m-0 py-2 px-2">{{$t('Log out')}}</mdb-dropdown-item>
                  <!-- <mdb-dropdown-item>Something else here</mdb-dropdown-item> -->
                </mdb-dropdown-menu>
              </mdb-dropdown>
      
            <!-- <div class="avatar_drop animated bounce"   style="width: 40px; height: 40px; position: relative;">
                <div @click="dropFunc" style="width: 40px; height: 40px;">
                  <img src=""  class="img-avatar img-fluid rounded-circle" alt="">
                </div>
                <i class="fas fa-circle mr-1 text-success " style="position: absolute; bottom: 0; right:-1.5px; font-size: 10px;"></i>
                <div v-show="show" style="position: absolute; top: 40px; right:-5px; width: 170px;" class="">
                    <mdb-dropdown-menu style="z-index: 468499999999999999">
                    <mdb-dropdown-item>Action</mdb-dropdown-item>
                    <mdb-dropdown-item>Another action</mdb-dropdown-item>
                    <mdb-dropdown-item>Something else here</mdb-dropdown-item>
                    <div class="dropdown-divider"></div>
                    <mdb-dropdown-item>Log out</mdb-dropdown-item>
                  </mdb-dropdown-menu>
                </div>
            </div> -->
          </form>
        </mdb-navbar-toggler>
      </mdb-navbar>
    </div>
    <mdb-modal  :show="showmin"  @close="showmin = false" size="lg" danger>
      <mdb-modal-header>
        <mdb-modal-title style="font-weight:  500;">{{$t('Last_info_medicine')}}</mdb-modal-title>
      </mdb-modal-header>
      <mdb-modal-body>
        <minDrag />
      </mdb-modal-body>
    </mdb-modal>

    <div v-show="false">
      <div class="d-flex justify-content-center align-items-center InfoIcoFixed shadow" v-if="laborant" @click="infoBoolen = !infoBoolen">
        <mdb-icon icon="info-circle" class="text-white" style="font-size: 27px;" />
      </div>
      <div :class="{'infoLab': infoBoolen, 'infoLabNo': !infoBoolen}">
        <div class="d-flex justify-content-center align-items-center InfoIcoFixed shadow" v-if="laborant" @click="infoBoolen = !infoBoolen">
          <mdb-icon icon="info-circle" class="text-white" style="font-size: 27px;" />
        </div> 
        <labInfo/>
      </div>
    </div>
  </div>
</template>
<script>
import labInfo from '../views/laboratory/labInfo.vue'
import minDrag from '../views/hospital/minDragList'
  import { mdbNavbar, mdbNavbarBrand, mdbNavbarToggler, mdbNavbarNav, mdbNavItem, mdbIcon,
    mdbDropdown, mdbDropdownItem, mdbDropdownMenu, mdbDropdownToggle, mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, } from 'mdbvue';
    import {mapActions, mapGetters} from 'vuex'
import Snow from './wnd_for_add/snow.vue';
  export default {
    name: 'NavbarPage',
    components: {
      mdbNavbar,
      mdbNavbarBrand,
      mdbNavbarToggler,
      mdbNavbarNav, mdbDropdown, 
      mdbDropdownItem,
      mdbDropdownMenu, mdbDropdownToggle, mdbIcon,
      mdbNavItem, mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, minDrag, labInfo,
      Snow
    },
    data(){
      return {
        show: false,
        showmin: false,
        num: 0,
        image: '',
        show_but: false,
        laborant: false,
        infoBoolen: false,
        user_fio: localStorage.docName
      }
    },
    async mounted(){
      if(localStorage.AuthId == null){
         this.$router.push('/')
      }
      if(localStorage.Type == 0){
        this.show_but = true 
      }
      if(localStorage.Type == 3){
        this.laborant = true;
      }
      await this.fetch_min_drag_list();
      this.image = localStorage.Image
      if(this.get_min_drag_list_for_admin.length == 0){
        this.$store.state.info_product = false;
      }
       this.showmin = this.$store.state.info_product;
      // console.log('dasdas')
      // console.log(this.image)
      // console.log(localStorage.Image)
    },
    computed: mapGetters(['get_min_drag_list_for_admin']),
    methods: {
      ...mapActions(['fetch_min_drag_list']),
      home(){
        console.log('dsd')
        this.$router.push('/m_users')
      },
      showInfo(){
        this.$store.state.info_product = true;
        this.showmin = true;
      },
      dropFunc(){
        this.num = 0
        this.show = !this.show;
        document.addEventListener("click", this.add_fun);
      },
      add_fun (e){
        if(this.num!=0){
          if(e.target.closest(".down_list")) return
          this.show = false;
          document.removeEventListener("click", this.add_fun)
        }
        this.num++
    }
    }
  }
</script>
<style scoped lang="scss">
.avatar_list{
  // z-index: 5;
  a{
    padding: 5px 15px;
    font-size: 13px;
    color: black;
    &:hover{
      background-color: rgba(96, 125, 139, 0.15);
    }
  }
}
.active{
  background: red;
}
.avatar_drop{
  cursor: pointer;
  background-image: url('../assets/icon/medic.png');
    background-position: right;
    background-repeat: no-repeat;
    background-size: cover;
    border-radius: 50%;
}
.navbar_hospital{
  
  background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgba(0,107,141,1) 0%, rgba(0,69,91,1) 90% );
  li{
    font-size: 13px;
  }
  span{
    font-size: 18px;
  }
}
.infoLab{
  position: fixed;
  width: 100%;
  height: 100vh;
  right:0;
  background: white;
  z-index: 1111111111;
  transition: 0.5s;
  overflow: hidden;
  overflow-y: scroll;
}
.infoLabNo{
  transition: 0.5s;
  position: fixed;
  width: 100%;
  height: 100vh;
  right:-100%;
  // transform: translateX(-100%);
  background: white;
  z-index: 1111111111;
}
.InfoIcoFixed{
  position:fixed;
  z-index: 11111;
  right:0; 
  top:200px; 
  width: 50px; 
  height:50px; 
  cursor:pointer;
  background-image: linear-gradient( 102.4deg,  rgba(253,189,85,1) 7.8%, rgba(249,131,255,1) 100.3% );;
  border-top-left-radius:5px; 
  border-bottom-left-radius:5px;
}

.list-unstyled{
  background: red;
  left:-500px
}

</style>
<template>
  <div>
    <div class="d-flex allContent">
      <div class=" border-right" :class="{'leftmenu': show_title, 'backleftmenu': !show_title }">
        <div class="d-flex main_left_shadow pb-1 pt-1 mb-0">
          <svg v-if="show_title" xmlns="http://www.w3.org/2000/svg" @click="backMenu(!show_title)" class="icon icon-tabler icon-tabler-chevron-left leftdown" 
            style="cursor:pointer;" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="#fff" fill="none" stroke-linecap="round" stroke-linejoin="round">
            <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
            <polyline points="15 6 9 12 15 18" />
          </svg>
          <svg v-else xmlns="http://www.w3.org/2000/svg" @click="backMenu(!show_title)" class="icon icon-tabler icon-tabler-menu-2 backleftdown"  style="cursor:pointer;" width="26" height="26" viewBox="0 0 24 24" stroke-width="1.5" stroke="#00abfb" fill="none" stroke-linecap="round" stroke-linejoin="round">
            <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
            <line x1="4" y1="6" x2="20" y2="6" />
            <line x1="4" y1="12" x2="20" y2="12" />
            <line x1="4" y1="18" x2="20" y2="18" />
          </svg>
          <div  class="d-flex align-items-center" v-if="show_title">
            <img src="../assets/logo1.png" alt="logo1" width="40">
            <img src="../assets/logo2.png" alt="logo2" width="130">
          </div>
        </div>
        <div class="menu_list">
          <div class="" v-for="(link,index) in links" :key="index" style="cursor: pointer;">
            <div @click="update_down(index)">
              <router-link
              tag="li" custom v-slot="{ navigate }"
              :to="link.url"
              :class="{'active_link': link.view}"
              class="text-dark d-flex py-2 menuitem"
              >
              <!-- <MDBIcon style="color: red; margin-right: 10px; padding-left: 15px;" icon="camera-retro" />
              Xodimlar -->
              <li @click="navigate" role="link" >
                <div  class="icon_div" style="width:13%;">
                  <mdb-icon :style="{color: link.color}" :class="{'ico': show_title, 'backico': !show_title }" :icon="link.icon" />
                </div>
                <div v-if="show_title" class="d-flex justify-content-between" style="width:85%; text-align:left">
                  <p  style="padding-left: 15px; font-size: 12.5px;"  class="m-0 text-white">{{$t(link.title)}}</p>
                  <svg v-if="link.down_list.length!=0" :class="{round:link.view}" xmlns="http://www.w3.org/2000/svg" class="icon mr-3 icon-tabler icon-tabler-chevron-right" style="margin-top: 3.5px;" width="13" height="13" viewBox="0 0 24 24" stroke-width="2" stroke="#999" fill="none" stroke-linecap="round" stroke-linejoin="round">
                  <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                  <polyline points="9 6 15 12 9 18" />
                </svg>
                </div>
              </li>
              </router-link>
            </div>
            <!-- dropdown -->
            <div v-if="show_title">
              <div v-show="link.view">
                <div v-for="(list,i) in link.down_list" :key="i" @click="update_down_click(i)">
                  
                  <router-link
                    tag="li" custom v-slot="{ navigate }"
                    :to="list.url"
                    :class="{'active_drop_link': list.dview}"
                    active-class="active_drop_link"
                    class="drop_down justify-content-flex-start d-flex align-items-center list-group-item-action pointer w-100"
                    > 
                    <li @click="navigate" role="link" >
                      <a class="m-0 p-0 pl-2 d-flex text-white" style="font-size:11.5px;" >
                        <div class="icon_div ml-4">
                        <mdb-icon :icon="list.icon" :style="{color: link.color}"  class="mr-3 icon_color"/>
                      </div>
                        {{$t(list.title)}}</a>
                    </li>
                  </router-link>
                </div>
              </div>
            </div>
          <!-- /dropdown -->
          </div>
        </div>
      </div>
      <div class="main  bg-white" >
        <router-view></router-view>
      </div>
    </div>
  </div>
</template>

<script>
  import { mdbIcon } from 'mdbvue';
  // import { ref } from 'vue';


  export default {
    components: {
      mdbIcon,
    },
    mounted() {
      this.show_title = localStorage.sidebar;
      console.log(this.show_title)
      console.log(localStorage.sidebar)
      this.name = localStorage.Name;
      for (let j = 0; j < this.links.length; j++) {
        if(this.links[j].url == this.$route.fullPath){
          this.links[j].view = true;
          return
        }else{
          if(this.links[j].down_list.length > 0){
            for (let k = 0; k < this.links[j].down_list.length; k++){
              if(this.links[j].down_list[k].url == this.$route.fullPath){
                this.links[j].down_list[k].dview = true;
                this.links[j].view = true;
                this.indexMain = j;
                return
              }
            }
          }
        }
      }
    },
    data() {
      return {
        name: 'sidebar',
        status_acces: false,
        show_title: localStorage.sidebar,
        indexMain: -1,
        links: [
            { title: "main", icon: 'home', url: '/getProduct', view: false, color: '#c4cbcb', down_list:[] },
            { title: "product", icon: 'shopping-bag', url: '/product', view: false, color: '#c4cbcb', down_list:[] },
            { title: "measurment", icon: 'balance-scale', url: '/measurment', view: false, color: '#c4cbcb', down_list:[] },
            { title: "changing", icon: 'drafting-compass', url: '/changing', view: false, color: '#c4cbcb', down_list:[] },
            { title: "buy", icon: 'truck-loading', url: '/buy', view: false, color: '#c4cbcb', down_list:[] },
            { title: "client", icon: 'user-shield', url: '/client', view: false, color: '#c4cbcb', down_list:[] },
            // { title: "client_controler", icon: 'users', url: '/client_controler', view: false, color: '#c4cbcb', down_list:[] },
            { title: "user", icon: 'user-tie', url: '/user', view: false, color: '#c4cbcb', down_list:[] },
            { title: "kassa", icon: 'cash-register', url: '/kassa', view: false, color: '#c4cbcb', down_list:[] },
            { title: "hisob", icon: 'wallet', url: '/hisob', view: false, color: '#c4cbcb', down_list:[] },
            { title: "report", icon: 'calendar-plus', url: '', view: false, color: '#c4cbcb', down_list:[
               { title: "usedProductList", url: '/usedProductList', dview: false, color: '#c4cbcb', down_list:[] },
               { title: "getProduct", url: '/getProduct_report', dview: false, color: '#c4cbcb', down_list:[] },

               { title: "zaxiraSend", url: '/zaxiraSend', dview: false, color: '#c4cbcb', down_list:[] },
               { title: "zaxiraList", url: '/zaxiraList', dview: false, color: '#c4cbcb', down_list:[] },

               { title: "changeWithoutReg", url: '/changeWithoutReg', dview: false, color: '#c4cbcb', down_list:[] },
               { title: "millUndo", url: '/millUndo', dview: false, color: '#c4cbcb', down_list:[] },
              
            ] },
            { title: "worker_rasxods", icon: 'users', url: '', view: false, color: '#c4cbcb', down_list:[
              { title: "worker", url: '/worker', view: false, color: '#c4cbcb', down_list:[] },
              { title: "worker_rasxod", url: '/worker_rasxod', dview: false, color: '#c4cbcb', down_list:[] },
            ] },
            { title: "setting", icon: 'cog', url: '', view: false, color: '#c4cbcb', down_list:[
              { title: "company", icon: 'landmark', url: '/company', view: false, color: '#c4cbcb', down_list:[] },
              { title: "department", icon: 'box', url: '/department', view: false, color: '#c4cbcb', down_list:[] },
              { title: "contragent", icon: 'bell', url: '/contragent', view: false, color: '#c4cbcb', down_list:[] },
              { title: "district", icon: 'crutch', url: '/district', view: false, color: '#c4cbcb', down_list:[] },
            ] },
            { title: "face_control", icon: 'users', url: '', view: false, color: '#c4cbcb', down_list:[
              { title: "Dashboard", url: '/dashboard', dview: false, color: '#c4cbcb', down_list:[] },
              { title: "salary_report", url: '/salary_report', dview: false, color: '#c4cbcb', down_list:[] },
              { title: "worker", url: '/userFace', dview: false, color: '#c4cbcb', down_list:[] },
              { title: "devices_info", url: '/door', view: false, color: '#c4cbcb', down_list:[] },
              { title: "device_door", url: '/doors', view: false, color: '#c4cbcb', down_list:[] },
              { title: "salary", url: '/salary', view: false, color: '#c4cbcb', down_list:[] },
            ] },
            { title: "logout", icon: 'sign-in-alt', url: '/', view: false, color: '#c4cbcb', down_list:[] },
        ],
      }
    },
    created() {
        this.$root.$refs.sidebar = this;
    },
    methods: {
      update_down: function (i) {
        this.indexMain = i;
        for (let j = 0; j < this.links.length; j++) {
          if (j !== i) {
            this.links[j].view = false
            if(this.links[j].down_list.length > 0) {
              for (let k = 0; k < this.links[j].down_list.length; k++) {
                  this.links[j].down_list[k].dview = false
              }
            }
          }
        }
        this.links[i].view = true
      },
      update_down_click: function (i) {
        for (let j = 0; j < this.links[this.indexMain].down_list.length; j++) {
          if (j !== i) {
            this.links[this.indexMain].down_list[j].dview = false
          }
        }
        this.links[this.indexMain].down_list[i].dview = true
       // console.log(this.links[this.indexMain].down_list[i].dview)
      },
      backMenu(a){
        this.show_title = a
        localStorage.sidebar = this.show_title
        console.log(localStorage.sidebar)

      }
    }
  };
</script>

<style lang="scss">
*{
  font-family: 'Montserrat', sans-serif;
}
.main{
  background: #eee;

}
.leftmenu{
  width: 290px;
  position: sticky;
  top: 0px;
  height: 100vh;
  overflow: hidden;
  overflow-y: auto;
  padding: 2px 0px;
  background: #2F3C4E;
  font-size: 13px;
}
.backleftmenu{
  background: #2F3C4E;

}
.smallleftmenu{
  width: 50px;
  position: sticky;
  top: 0px;
  height: 100vh;
  overflow: hidden;
  overflow-y: auto;
  padding: 2px 0px;
  background: #1B3446;
  font-size: 13px;
}
.main{
  width: 100%;
}
.allContent{
  min-height: 100vh;
}
.active_link{
  background: rgba(113, 184, 255, 0.3);
  p{
    color: #fff;
  }
  .ico{
    color: #fff;
  }
}
.active_drop_link{
  background: rgba(131, 190, 250, 0.25);
  p{
    color: #fff;
  }
  .ico{
    color: #fff;
  }
}
.menuitem:hover{
  background: rgba(113, 184, 255, 0.2);
  p{
    color: #fff;
  }
  .ico{
    color: #fff;
  }
}
.snipper{
  display: flex;
  justify-content: center;
  background-color: rgb(255, 255, 255);
}
.activetext{
  cursor: pointer;
  a{
    color: #fff;
  }
  i{
    color: #fff;
  }
}
.drop_down{
  color: black;
  padding: 5px 0;
  position: relative;
  justify-content: space-between;
  a{
    flex: 1;
    color: rgb(70, 70, 70);
    font-size: 13.4px;
    font-family: 'Montserrat', sans-serif;
    font-weight: 100;
  }
  &:hover{
    background: rgba(113, 184, 255, 0.1);
    a{
      color: #fff;
    }
    i{
      color: #fff;
    }
    transition: background-color 0.2s;
    color: #fff;
  }
}
.round{
  transform: rotate(90deg);
  transition: all 0.5s;
}
.back_round{
  transition: all 0.5s;
}
.ico{
  padding-left: 15px;
}
.backico{
  padding-left: 17px;
}
.leftdown{
  margin: 10px 6px;
  padding: 2px;
}
.backleftdown{
  margin: 10px 12px;
  padding: 1px;
}
.menu_list{
  padding-top: 10px;
  height: calc(100vh - 57px);
  overflow: hidden;
  overflow-y: scroll;
}
::-webkit-scrollbar {
  width: 5px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #2F3C4E;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #697d9c;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #212d3d;
}
.main_left_shadow{
  box-shadow: rgba(0, 0, 0, 0.07) 0px 1px 2px, rgba(0, 0, 0, 0.07) 0px 2px 4px, rgba(0, 0, 0, 0.07) 0px 4px 8px, rgba(0, 0, 0, 0.07) 0px 8px 16px, rgba(0, 0, 0, 0.07) 0px 16px 32px, rgba(0, 0, 0, 0.07) 0px 32px 64px;
}
</style>
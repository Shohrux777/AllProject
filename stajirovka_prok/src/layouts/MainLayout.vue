<template>
  <div>
    <div class="d-flex allContent">
      <div class="border-right" :class="{'leftmenu': show_title, 'smallleftmenu': !show_title }">
        <div class="d-flex pb-1 mb-2" v-if="show_title">
          <div  
            style="padding: 6.5px 10px;"
            class="ml-3 w-100 shadow d-flex align-items-center justify-content-start">
            <!-- <span style="font-size: 22px;" class="mb-1 text-white">Маркет</span> -->
            <img
                src="../assets/logo.png"
                height="35"
                width="35"
                alt=""
                loading="lazy"
              />
              <span class="text-white ms-2" style="font-size: 15px;">Ходимларни ўқитиш</span>
          </div>
        </div>
        <div class="smallIcon shadow d-flex align-items-center justify-content-center" v-if="!show_title">
            <img
                src="../assets/logo.png"
                height="30"
                width="30"
                alt=""
                loading="lazy"
              />
        </div>
        <div class="" v-for="(link,index) in links" :key="index" >
          <div @click="update_down(index)" v-if="link.role == role || link.role == ''">
            <router-link
            :to="link.url"
            :class="{'active_link': link.view}"
            class="text-dark d-flex py-2 menuitem"
            >
              <div  class="icon_div" style="width:15%;">
                <MDBIcon :style="{color: link.color}" :class="{'ico': show_title, 'backico': !show_title }" :icon="link.icon" />
              </div>

              <div v-if="show_title" class="d-flex justify-content-between" style="width:85%; text-align:left">

                <p  style="padding-left: 5px; font-size: 12px; position: relative;" class="m-0 text-white">{{$t(link.title)}}
                  <span v-if="link.url == '/answers_list'" style="right:-28px; top:2px; background:#DCA333; color:black;" class="position-absolute  translate-middle badge rounded-pill">
                    {{ count }}
                    <span class="visually-hidden">unread messages</span>
                  </span>
                </p>

                <svg v-if="link.down_list.length!=0" :class="{'round':link.view}" xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-chevron-right" 
                  style="margin-top: 3.5px; margin-right: 8px !important;" width="13" height="13" viewBox="0 0 24 24" stroke-width="2" stroke="#999" fill="none" stroke-linecap="round" stroke-linejoin="round">
                  <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                  <polyline points="9 6 15 12 9 18" />
                </svg>

              </div>
            </router-link>
          </div>
          <!-- dropdown -->
          <div v-if="show_title">
            <div v-show="link.view">
              <div v-for="(list,i) in link.down_list" :key="i" @click="update_down_click(i)">
                <router-link
                  :to="list.url"
                  :class="{'active_drop_link': list.dview}"
                  active-class="active_drop_link"
                  class="drop_down justify-content-flex-start d-flex align-items-center list-group-item-action pointer w-100"
                > 
                  <a class=" d-flex text-white">
                    <div class="icon_div ml-4">
                      <MDBIcon :icon="list.icon" :style="{color: link.color}"  class="mr-3 icon_color"/>
                    </div>
                    {{$t(list.title)}}
                  </a>
                </router-link>
              </div>
            </div>
          </div>
        <!-- /dropdown -->
        </div>
      </div>
      <div class="main bg-white">
        <div style="position: sticky; top:0px;">
          <MDBNavbar expand="lg" class="d-flex justify-content-between navbar_bg p-0 m-0" container>
            <MDBNavbarNav class="mb-2 py-2 mb-lg-0" >
              <MDBNavbarBrand @click="backMenu(!show_title)" style="cursor:pointer;">
                <MDBIcon icon="bars" iconStyle="fas" class="cl_white" size="1x"></MDBIcon>
              </MDBNavbarBrand>
            </MDBNavbarNav>

            <div>
              <MDBNavbarNav class="mb-2 mb-lg-0 ">
                <MDBNavbarItem class="d-sm-flex align-items-sm-center">
                  
                  <img
                    :src="hostname2 + user_image ? hostname2 + user_image : 'https://mdbootstrap.com/img/new/avatars/8.jpg'"
                    class="rounded-circle"
                    height="22"
                    alt=""
                    loading="lazy"
                  />
                  <span class="d-none d-sm-block ms-1 cl_white " style="font-size: 12px;">{{ user_name }}</span>
                </MDBNavbarItem>
                
                <MDBNavbarItem >
                  <MDBDropdown class="nav-item "   v-model="dropdown5">
                    <MDBDropdownToggle tag="a" class="nav-link" @click="dropdown5 = !dropdown5">
                      <MDBIcon :flag="activLang.icon" class="m-0"></MDBIcon>
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
                </MDBNavbarItem>
              </MDBNavbarNav>
            </div>
          </MDBNavbar>
          <span class="creater_database">Самарқанд вил. Прократура</span>
        </div>
        <div class="main_router_page">
          <router-view></router-view>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import {
    MDBNavbar,
    MDBNavbarToggler,
    MDBNavbarBrand,
    MDBNavbarItem,
    MDBNavbarNav,
    MDBDropdown,
    MDBDropdownItem,
    MDBDropdownMenu,
    MDBDropdownToggle,
    MDBIcon,
    MDBBadge, } from 'mdb-vue-ui-kit';
  import { ref } from 'vue';
  export default {
    components: {
      MDBNavbar,
      MDBNavbarToggler,
      MDBNavbarBrand,
      MDBNavbarItem,
      MDBNavbarNav,
      MDBDropdown,
      MDBDropdownItem,
      MDBDropdownMenu,
      MDBDropdownToggle,
      MDBIcon,
      MDBBadge,
    },
    setup() {
      const dropdown5 = ref(false);
      return {
        dropdown5,
      };
    },
    async mounted() {
      console.log(this.role)
      let time1 = new Date();
      console.log(time1)
      this.Start_time = time1.toISOString().slice(0,10);
      setInterval(await this.fetchblockTime, 60000);
      // console.log(localStorage.sidebar)
      // this.name = localStorage.Name;
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
        Start_time: null,
        count: localStorage.getItem('pending_count'),
        loading: false,
        reportList: [],
        show_title: true,
        indexMain: -1,
        role: localStorage.getItem('role'),
        user_name: localStorage.getItem('user_name'),
        user_image: localStorage.getItem('user_image'),
        hostname2: this.$store.state.hostname2,
        langList: [
          {
            name: 'Узбек',
            lang: 'uz',
            icon: 'uzbekistan'
          },
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
          
        ],
        activLang: {
          name: 'Узбек',
          lang: 'uz',
          icon: 'uzbekistan'
        },
        links: [

            // ............... for Bank ....................
            // { title: 'test', icon: 'house-damage', url: '/test', view: false, color: '#ddd', 
            //   down_list:[]
            // },
            { title: 'dashboard', icon: 'house-damage', url: '/dashboard', view: false, color: '#ddd', role:'', count: false,
              down_list:[]
            },
            { title: 'answers_list', icon: 'landmark', url: '/answers_list', view: false, color: '#ddd', count: true, down_list:[], role:'admin' },

            { title: 'Топшириклар', icon: 'landmark', url: '/user_topshiriqlar', view: false, color: '#ddd',  down_list:[], role:'user', count: false, },
          
            { title: 'user', icon: 'user', url: '/user', view: false, color: '#ddd', role:'admin', count: false,
              down_list:[]
            },
            { title: 'task', icon: 'landmark', url: '/task', view: false, color: '#ddd', role:'admin', count: false,
              down_list:[]
            },
            // { title: 'menu_setting', icon: 'tools', url: '', view: false, color: '#ddd', down_list:[
            //     { title: "company", url: '/', dview: false, color: '#ddd', down_list:[] },
            // ] },
            { title: "logout", icon: 'sign-out-alt', url: '/', view: false, color: '#ddd', down_list:[], role:'', count: false, },


            // ...............End for MArket..................

        ],
      }
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
        // localStorage.sidebar = this.show_title
        // console.log(localStorage.sidebar)
      },
      selectLang(lang){
        this.activLang = lang;
        this.$i18n.locale = lang.lang;
      },
      
      async fetchblockTime(){
          let day = new Date();
          console.log('day.getDay()')
          console.log(day.getDay())
          console.log(day.getHours())
          console.log(day.getMinutes())

          if(day.getDay() != 0){
              if(day.getHours() == 22 && day.getMinutes()>0 && day.getMinutes()<6){
                await this.submit();
                for(let i=0; i<this.reportList.length; i++){
                  await this.fetchBlockUsers(this.reportList[i].userid);
                }
              }
          }
      },
      async fetchBlockUsers(id){
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getBlokUsersListForAllDoorsWithUpdateGr?user_id_list=" + id);
          const data = await response.json();
          this.loading = false;
          console.log(data)
          // this.reportList = data.items_list;
        }
        catch(error){
          this.loading = false;
          console.log(error)
        }
      }
    }
  };
</script>

<style lang="scss">
*{
  font-family: 'Montserrat', sans-serif;
}
::-webkit-scrollbar {
  width: 5.5px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #f1f1f1;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #313f48b0;
  border-radius: 2px;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #313F48;
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
  background: #1D282D;
  font-size: 14px;
}
.leftmenu p{
  color:#c9c9c9 !important;
}
.leftmenu .ico{
  color:#c9c9c9 !important;
}
.backleftmenu{
  background: #1D282D;
}
.smallleftmenu{
  width: 50px;
  position: sticky;
  top: 0px;
  height: 100vh;
  overflow: hidden;
  overflow-y: auto;
  padding: 0px 0px;
  background: #1D282D;
  font-size: 14px;
}
.main{
  width: 100%;
}
.allContent{
  min-height: 100vh;
}
.active_link{
  position: relative;
  background: #1B2628;
  p{
    color: #fff !important;
  }
  .ico{
    color: #fff !important;
  }
}
.active_link::before{
  content: '';
  position: absolute;
  width: 3px;
  height: 100%;
  background: green;
  left:0;
  top:0;
}
.active_drop_link{
  background: #1B2628;
  p{
    color: #fff !important;
  }
  .ico{
    color: #fff !important;
  }
}
.menuitem:hover{
  background: #1B2628;
  p{
    color: #fff !important;
  }
  .ico{
    color: #fff !important;
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
    font-size: 12px;
    font-family: 'Montserrat', sans-serif;
    font-weight: 100;
    margin-left: 15px !important;
    color:#c9c9c9 !important;
  }
  &:hover{
    background: rgba(19, 29, 40, 0.274) !important;
    a{
      color: #fff !important;
    }
    i{
      color: #fff !important;
    }
    transition: background-color 0.2s;
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
  font-size: 12px;
}
.backico{
  padding-left: 17px;
  font-size: 15px;
}
.leftdown{
  margin: 10px 8px;
  padding: 2px;
}
.backleftdown{
  margin: 10px 12px;
  padding: 1px;
}
.navbar_bg{
  background: #313F48;
}
.cl_white{
  color:white;
}
.cl_white:hover{
  color:rgb(192, 192, 192);
}
.smallIcon{
  height: 45px;
  background: #1D282D;
}
.main_router_page{
  height: calc(100vh - 46px);
}
.creater_database{
  position: fixed;
  bottom: 4px;
  left: 4px;
  font-size: 12px;
  color:#f0efef;
}
</style>
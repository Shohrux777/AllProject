<template>
  <div>
    <navbar/>
    <div class="d-flex allContent" >
      <div class="leftmenuReg">
        <div class="backgroundColor">
          <div class="" v-for="(link,index) in links" :key="index" @click="funcActive(index)">
            <!-- :class="{'active_linkReg': link.view}" -->
            <router-link
            tag="li" custom v-slot="{ navigate }"
            :to="link.url"
            :class="{'active_linkReg': index == itter}"
            @click ="update_down(index)"
            style="font-size:12.2px;"
            class="text-dark d-flex py-2 menuitemReg"
            >
            <!-- <MDBIcon style="color: red; margin-right: 10px; padding-left: 15px;" icon="camera-retro" />
            Xodimlar -->
            <li @click="navigate" role="link">
              <div class="icon_div" style="width:13%;">
                <div class="pl-2" v-html="icons[index].ico"></div>
              </div>
              <div style="width:86%; text-align:left">
                <p  style="padding-left: 10px;" class="m-0">{{$t(link.title)}}</p>
              </div>
            </li>
            
            </router-link>
          </div>
        </div>
      </div>
      <div class="mainReg" >
        <router-view></router-view>
      </div>
    </div>
  </div>
</template>

<script>
  // import { mdbIcon } from 'mdbvue';
  // import { ref } from 'vue';
  import navbar from '../components/navbar'


  export default {
    components: {
       navbar
    },
    mounted() {
      this.name = localStorage.Name;

      for (let j = 0; j < this.links.length; j++) {
        if(this.links[j].url == this.$route.fullPath){
          this.itter = j;
          return
        }
      }
    },
    // setup() {

    //   const collapse1 = ref(false);
    //   const dropdown1 = ref(false);
    //   const dropdown6 = ref(false);

    //   return {
    //     collapse1,
    //     dropdown1,
    //     dropdown6,
    //   }
    // },
    data() {
      return {
        name: '',
        itter: -1,
        links: [
          // { title: 'Работника', icon: 'user-md', url: '/users', view: true, color: '#20B2AA' },
          { title: 'user', icon: 'doc25.png', url: '/m_users', view: true, color: '#ddd' },
          // { title: "Болезни", icon: 'biohazard', url: '/kasallik', view: false, color: '#00FF00' },
          { title: "contragent", icon: 'doc22.png', url: '/contragent', view: false, color: '#ddd' },
          { title: "patient", icon: 'doc28.png', url: '/client', view: false, color:'#ddd' },
          { title: "type_service", icon: 'ambulance', url: '/service_type', view: false, color:'#ddd' },
          { title: "Company", icon: 'building', url: '/company', view: false, color:'#ddd' },
          { title: "department", icon: 'clinic-medical', url: '/depart', view: false, color: '#ddd' },
          { title: "rooms", icon: 'hospital-alt', url: '/rooms', view: false, color: '#ddd' },
          { title: "position", icon: 'clipboard', url: '/position', view: false, color: '#ddd' },

          { title: "province", icon: 'map-marker-alt', url: '/province', view: false, color: '#ddd' },
          { title: "district", icon: 'globe', url: '/district', view: false, color:'#ddd' },

          { title: 'bronRoom', icon: 'hospital-symbol', url: '/bronRoom', view: false, color: '#ddd' },
          { title: 'priceTypeRoom', icon: 'dollar-sign', url: '/priceTypeRoom', view: false, color: '#ddd' },

          
          { title: "drag", icon: 'capsules', url: '/medicine', view: false, color:'#ddd' },
          // { title: "min drag", icon: 'capsules', url: '/medicineMin', view: false, color:'#ddd' },
          { title: "transit", icon: 'file-invoice', url: '/medicineMin', view: false, color: '#ddd' },
          { title: 'agent', icon: 'address-book', url: '/agent', view: false, color: '#ddd' },
          { title: 'package_service', icon: 'address-book', url: '/package', view: false, color: '#ddd' },
          // { title: "num_service", icon: 'diagnoses', url: '/number_into', view: false, color: '#ddd' },
          // { title: "type_patient", icon: 'users', url: '/patient_type', view: false, color:'#ddd' },
          { title: "service_group_show_doc", icon: 'eye-slash', url: '/ServiceGroup_show_doc', view: false, color:'#ddd' },
          // { title: 'bronBeds', icon: 'user', url: '/bronBeds', view: false, color: '#ddd' },
          // { title: 'bronRoomAddBeds', icon: 'user', url: '/bronRoomAddBeds', view: false, color: '#ddd' },
          { title: 'Active service', icon: 'server', url: '/activeServiceRemove', view: false, color: '#ddd' },
          { title: 'otchrit', icon: 'sort-numeric-up', url: '/ochred_view', view: false, color: '#ddd' },

          
          


          // { title: "Диаграмма", icon: 'chart-line', url: '/chart', view: false, color: '#FFA07A'  },
          // { title: "Датчик", icon: 'atom', url: '/sensor', view: false, color: '#800080' },
          // { title: "Цветь", icon: 'palette', url: '/addcolor', view: false, color: '#20B2AA' },
          // { title: "Датчик и Цветь", icon: 'tablet', url: '/scr_add', view: false, color: '#FF7F50' },
          // { title: "Datchik_Add", icon: 'heartbeat', url: '/datchikAdd', view: false, color:'#FFA500' },


          // { title: 'm_product', icon: 'clipboard', url: '/m_product', view: false, color: '#4B0082' },
          // { title: "add_category", icon: 'box', url: '/m_product_group', view: false, color: '#FFA07A' },
          // { title: "product_and category", icon: 'compress-arrows-alt', url: '/m_product_conf', view: false, color: '#40E0D0' },
          // { title: 'limit', icon: 'chart-bar', url: '/limit', view: false, color: '#9370DB' },
          // { title: 'currency', icon: 'hand-holding-usd', url: '/currency', view: false, color: '#40E0D0' },
        ],
        icons: [
          {ico: 
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-user userWidth"  viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
            <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
            <circle cx="12" cy="7" r="4" />
            <path d="M6 21v-2a4 4 0 0 1 4 -4h4a4 4 0 0 1 4 4v2" />
          </svg>`
          },
          {ico: 
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-users" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <circle cx="9" cy="7" r="4" />
              <path d="M3 21v-2a4 4 0 0 1 4 -4h4a4 4 0 0 1 4 4v2" />
              <path d="M16 3.13a4 4 0 0 1 0 7.75" />
              <path d="M21 21v-2a4 4 0 0 0 -3 -3.85" />
            </svg>`
          },
          {ico:
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-user-search" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <circle cx="12" cy="7" r="4" />
              <path d="M6 21v-2a4 4 0 0 1 4 -4h1" />
              <circle cx="16.5" cy="17.5" r="2.5" />
              <path d="M18.5 19.5l2.5 2.5" />
            </svg>`
          },
          {ico:
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-brand-slack" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <path d="M12 12v-6a2 2 0 0 1 4 0v6m0 -2a2 2 0 1 1 2 2h-6" />
              <path d="M12 12h6a2 2 0 0 1 0 4h-6m2 0a2 2 0 1 1 -2 2v-6" />
              <path d="M12 12v6a2 2 0 0 1 -4 0v-6m0 2a2 2 0 1 1 -2 -2h6" />
              <path d="M12 12h-6a2 2 0 0 1 0 -4h6m-2 0a2 2 0 1 1 2 -2v6" />
            </svg>`
          },
          {ico:
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-building-castle" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <path d="M15 19v-2a3 3 0 0 0 -6 0v2a1 1 0 0 1 -1 1h-4a1 1 0 0 1 -1 -1v-14h4v3h3v-3h4v3h3v-3h4v14a1 1 0 0 1 -1 1h-4a1 1 0 0 1 -1 -1z" />
              <line x1="3" y1="11" x2="21" y2="11" />
            </svg>`
          },
          {ico:
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-building-warehouse" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <path d="M3 21v-13l9 -4l9 4v13" />
              <path d="M13 13h4v8h-10v-6h6" />
              <path d="M13 21v-9a1 1 0 0 0 -1 -1h-2a1 1 0 0 0 -1 1v3" />
            </svg>`
          },
          {ico:
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-building-cottage" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <line x1="3" y1="21" x2="21" y2="21" />
              <path d="M4 21v-11l2.5 -4.5l5.5 -2.5l5.5 2.5l2.5 4.5v11" />
              <circle cx="12" cy="9" r="2" />
              <path d="M9 21v-5a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v5" />
            </svg>`
          },
          {ico:
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-clipboard-list" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <path d="M9 5h-2a2 2 0 0 0 -2 2v12a2 2 0 0 0 2 2h10a2 2 0 0 0 2 -2v-12a2 2 0 0 0 -2 -2h-2" />
              <rect x="9" y="3" width="6" height="4" rx="2" />
              <line x1="9" y1="12" x2="9.01" y2="12" />
              <line x1="13" y1="12" x2="15" y2="12" />
              <line x1="9" y1="16" x2="9.01" y2="16" />
              <line x1="13" y1="16" x2="15" y2="16" />
            </svg>`
          },
          {ico:
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-map-pin" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <circle cx="12" cy="11" r="3" />
              <path d="M17.657 16.657l-4.243 4.243a2 2 0 0 1 -2.827 0l-4.244 -4.243a8 8 0 1 1 11.314 0z" />
            </svg>`
          },
          {ico:
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-map-pins" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <path d="M10.828 9.828a4 4 0 1 0 -5.656 0l2.828 2.829l2.828 -2.829z" />
              <line x1="8" y1="7" x2="8" y2="7.01" />
              <path d="M18.828 17.828a4 4 0 1 0 -5.656 0l2.828 2.829l2.828 -2.829z" />
              <line x1="16" y1="15" x2="16" y2="15.01" />
            </svg>`
          },
          {ico:
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-bed" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <path d="M3 7v11m0 -4h18m0 4v-8a2 2 0 0 0 -2 -2h-8v6" />
              <circle cx="7" cy="10" r="1" />
            </svg>`
          },
          {ico:
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-receipt-2" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <path d="M5 21v-16a2 2 0 0 1 2 -2h10a2 2 0 0 1 2 2v16l-3 -2l-2 2l-2 -2l-2 2l-2 -2l-3 2" />
              <path d="M14 8h-2.5a1.5 1.5 0 0 0 0 3h1a1.5 1.5 0 0 1 0 3h-2.5m2 0v1.5m0 -9v1.5" />
            </svg>`
          },
          {ico:
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-pill" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                <path d="M4.5 12.5l8 -8a4.94 4.94 0 0 1 7 7l-8 8a4.94 4.94 0 0 1 -7 -7" />
                <line x1="8.5" y1="8.5" x2="15.5" y2="15.5" />
              </svg>`
          },
          {ico:
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-shopping-cart-discount" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                <circle cx="6" cy="19" r="2" />
                <circle cx="17" cy="19" r="2" />
                <path d="M17 17h-11v-14h-2" />
                <path d="M20 6l-1 7h-13" />
                <path d="M10 10l6 -6" />
                <circle cx="10.5" cy="4.5" r=".5" />
                <circle cx="15.5" cy="9.5" r=".5" />
              </svg>`
          },
          {ico:
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-brand-deviantart" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                <path d="M18 3v4l-3.857 6h3.857v4h-6.429l-2.571 4h-3v-4l3.857 -6h-3.857v-4h6.429l2.571 -4z" />
              </svg>`
          },
          {ico:
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-sitemap" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
            <path d="M3 15m0 2a2 2 0 0 1 2 -2h2a2 2 0 0 1 2 2v2a2 2 0 0 1 -2 2h-2a2 2 0 0 1 -2 -2z" />
              <path d="M15 15m0 2a2 2 0 0 1 2 -2h2a2 2 0 0 1 2 2v2a2 2 0 0 1 -2 2h-2a2 2 0 0 1 -2 -2z" />
              <path d="M9 3m0 2a2 2 0 0 1 2 -2h2a2 2 0 0 1 2 2v2a2 2 0 0 1 -2 2h-2a2 2 0 0 1 -2 -2z" />
              <path d="M6 15v-1a2 2 0 0 1 2 -2h8a2 2 0 0 1 2 2v1" />
              <path d="M12 9l0 3" />
            </svg>`
          },  
          {ico:
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-pinned-off" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <line x1="3" y1="3" x2="21" y2="21" />
              <path d="M15 4.5l-3.249 3.249m-2.57 1.433l-2.181 .818l-1.5 1.5l7 7l1.5 -1.5l.82 -2.186m1.43 -2.563l3.25 -3.251" />
              <line x1="9" y1="15" x2="4.5" y2="19.5" />
              <line x1="14.5" y1="4" x2="20" y2="9.5" />
            </svg>`
          },
          {ico:
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-stairs-up" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <path d="M4 20h4v-4h4v-4h4v-4h4" />
              <path d="M4 11l7 -7v4m-4 -4h4" />
            </svg>`
          },
          {ico:
            `<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-circle-3" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <path d="M12 12a2 2 0 1 0 -2 -2" />
              <path d="M10 14a2 2 0 1 0 2 -2" />
              <circle cx="12" cy="12" r="9" />
            </svg>`
          },
        ]
      }
    },
    methods: {
      update_down: function (i) {
        for (let j = 0; j < this.links.length; j++) {
          if (j !== i) {
            this.links[j].view = false
          }
        }
        this.links[i].view = !this.links[i].view
      },
      funcActive(i){
        this.itter  = i
      },
      getImgUrl(pic) {
        return require('../assets/icon/'+pic)
      }
    }
  };
</script>

<style lang="scss">
*{
  font-family: 'Montserrat', sans-serif;
}
.mainReg{
  background: #eee;

}
.userWidth{
  width: 20px; height: 20px;
}
.leftmenuReg{
  width: 15.5%;
  position: sticky;
  top: 0px;
  height: calc(100vh - 45px);
  overflow: hidden;
  overflow-y: auto;
  font-size: 14px;
  position: sticky;
  top: 45px;

}
.backgroundColor{
  background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgba(0,107,141,1) 0%, rgba(0,69,91,1) 90% );
  height: 100%;
  padding: 2px 0px;
}
.mainReg{
  width: 84.5%;
}
.allContent{
  min-height: 100vh;
  
}
.active_linkReg{
  background: rgba(113, 184, 255, 0.3);
  border-left: 3px solid orange;
  p{
    color: white;
  }
  .ico{
    color: white;
  }
}
.menuitemReg:hover{
  background: rgba(113, 184, 255, 0.2);
  p{
    color: white;
  }
  .ico{
    color: white;
  }
}
.menuitemReg{
  p{
    color: white;
    font-size: 12px;

  }
  .ico{
    color: white;
  }
}
.snipper{
  display: flex;
  justify-content: center;
  background-color: rgb(255, 255, 255);
}
</style>
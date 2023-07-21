<template>
  <div>
    <navbar/>
    <div class="d-flex allContent_dynamic">
      <div class="leftmenudynamic border-right">
        <div class="" v-for="(item,i) in get_lab_main_list" :key="i+100" @click="chooseFunc(item.id, item)">
          <!-- :class="{'active_link_dynamic': link.view}" -->
          <div
          class="text-dark  d-flex py-2 menuitem border-bottom" :class="{'active_link_dynamic': item.id == lab_main_id}"

          >
          <!-- <MDBIcon style="color: red; margin-right: 10px; padding-left: 15px;" icon="camera-retro" />
          Xodimlar -->
          <div style="width:100%; text-align:left">
            <p  style="padding-left: 15px; font-size:12.5px;" class="m-0">{{item.name}}</p>
          </div>
         
          </div>
        </div>
        <div class="" v-for="(link,index) in links" :key="index" @click="funcActive(index)">
          <!-- :class="{'active_link_dynamic': link.view}" -->
          <router-link
          tag="li" custom v-slot="{ navigate }"
          :to="link.url" 
          :class="{'active_link_dynamic': index == itter && lab_main_id == null}"
          
          class="text-dark d-flex py-2 menuitem border-bottom"
          >
          <!-- <MDBIcon style="color: red; margin-right: 10px; padding-left: 15px;" icon="camera-retro" />
          Xodimlar -->
          <li @click="navigate" role="link">
             <div style="width:100%; text-align:left">
              <p  style="padding-left: 15px; font-size:12.5px;" class="m-0">{{$t(link.title)}}</p>
            </div>
          </li>
         
          </router-link>
        </div>
        <div class="" >
          <router-link
          tag="li" custom v-slot="{ navigate }"
          to="/Laboratoriya_add" 
          class="text-dark d-flex py-2 menuitem border-bottom bg-success"
          >
         <li @click="navigate" role="link">
             <div style="width:100%; text-align:left">
              <p  style="padding-left: 15px; font-size:12.5px;" class="m-0 text-white">{{$t('add_laboratory')}}</p>
            </div>
          </li>
          </router-link>
        </div>
      </div>
      <div class="mainAnalizdynamic px-1 py-1" v-if="lab_main_id">
        <div v-if="show_lab_add">
            <labAddResult :main_name_title = "lab_main_item" :main_id = "lab_main_id" />
        </div>
        <div v-else-if="lab_update">
            <labUpdate :main_name_title = "lab_main_item" :main_id = "lab_main_id" :id="lab_result_id"  />
        </div>
        <div v-else> 
          <labTable :main_id="lab_main_id" :main_name_title="lab_main_item" @opened="openLabAdd" @update="updateLab"/>
        </div>
      </div>
      <div class="mainAnalizdynamic px-1 py-1" v-else>
        <router-view></router-view>
      </div>
      
    </div>
  </div>
</template>

<script>
  // import { ref } from 'vue';
  import labAddResult from '../views/analysis/dynamic/labResultDynamicAdd.vue'
  import labUpdate from '../views/analysis/dynamic/labUpdate.vue'
  import jsonLabMenu from '../json/Sergile.json'
  import navbar from '../components/navbar'
  import {mapActions, mapGetters} from 'vuex'
  import labTable from '../views/analysis/dynamic/labTable_andAdd.vue'
  export default {
    components: {
       navbar, labTable,
       labAddResult, labUpdate
    },
    data() {
      return {
        name: '',
        itter: -1,
        links: jsonLabMenu,
        lab_main_id: null,
        lab_main_item: {},
        show_lab_add: false,
        lab_update: false,
        pathLab : this.$route.fullPath,
        router_id: this.$route.params.id,
        lab_result_id: null,
      }
    },
    async mounted() {
      // console.log('jsonLabMenu')
      localStorage.labBack = false;
      this.name = localStorage.Name;
      await this.fetch_lab_main_list();
      if(this.router_id == 0){
        this.lab_main_id = this.get_lab_main_list[0].id;
        this.lab_main_item = this.get_lab_main_list[0];
        this.show_lab_add = false;
        localStorage.labBack = false;
        this.$router.push('/laboratory_list/' + this.get_lab_main_list[0].id)
      }
      else{
        // this.show_lab_add = localStorage.labBack;
        // console.log('this.$route.fullPath')
        // console.log(this.pathLab)
        for(let i=0; i<this.get_lab_main_list.length; i++){
          if(this.get_lab_main_list[i].id == this.router_id){
            this.lab_main_id = this.get_lab_main_list[i].id;
            this.lab_main_item = this.get_lab_main_list[i];
            break;
          }
        }
      }

      for (let j = 0; j < this.links.length; j++) {
        if(this.links[j].url == this.$route.fullPath){
          this.itter = j;
          return
        }
      }
      
    },
    computed: mapGetters(['get_lab_main_list']),
    methods: {
      ...mapActions(['fetch_lab_main_list']),
      update_down: function (i) {
        for (let j = 0; j < this.links.length; j++) {
          if (j !== i) {
            this.links[j].view = false
          }
        }
        this.links[i].view = !this.links[i].view
      },
      funcActive(i){
        this.itter  = i;
        this.lab_main_id = null;
      },
      chooseFunc(id,title){
        this.lab_main_id = id;
        this.lab_main_item = title;
        this.show_lab_add = false;
        this.lab_update = false;
        localStorage.labBack = false;
        if(this.$route.params.id != id){
          this.$router.push('/laboratory_list/' + id)
        } 

      },
      openLabAdd(){
        this.show_lab_add = true;
      },
      updateLab(id){
        console.log('dasdasd')
        this.lab_result_id = id;
        this.lab_update = true;
      }
    }
  };
</script>

<style lang="scss">
*{
  font-family: 'Montserrat', sans-serif;
}
.mainAnalizdynamic{
  background:#ddd ;
}
.leftmenudynamic{
  width: 18%;
  position: sticky;
  top: 0px;
  height: calc(100vh - 50px);
  overflow: hidden;
  overflow-y: auto;
  padding: 2px 0px;
  background: #fff;
  font-size: 14px;
}
.mainAnalizdynamic{
  width: 82%;
}
.allContent_dynamic{
  min-height: calc(100vh - 50px);
}
.active_link_dynamic{
  border-left: 3px solid rgb(25, 0, 255);
  background: rgba(113, 184, 255, 0.3);
  p{
    color: #1266F1;
  }
  .ico{
    color: #1266F1;
  }
}
.menuitem:hover{
  background: rgba(113, 184, 255, 0.2);
  p{
    color: #1266F1;
  }
  .ico{
    color: #1266F1;
  }
}
.snipper{
  display: flex;
  justify-content: center;
  background-color: rgb(255, 255, 255);
}
</style>
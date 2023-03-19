<template>
    <div class="deparment_page">
      <navbar :title = "$t('user')" @add="addDept"/>
      <div class="mainpage">
          <mdbtabled
           :options="get_user_list"
           ></mdbtabled>
      </div>
      <MDBModal
          id="exampleModal"
          tabindex="-1"
          labelledby="exampleModalLabel"
          v-model="exampleModal"
          staticBackdrop
          size="lg"
      >
          <MDBModalHeader style="background: #475a65;" class="text-white px-3 py-2">
              <MDBModalTitle id="exampleModalLabel"> {{$t('user')}} </MDBModalTitle>
          </MDBModalHeader>
          <MDBModalBody>
              <dept_add @close="exampleModal = false"></dept_add>
          </MDBModalBody>
          
      </MDBModal>
    </div>
  </template>
  
  <script>
    import {
      MDBBtn,
      MDBBadge,
      MDBIcon,
      MDBModal,
      MDBModalHeader,
      MDBModalTitle,
      MDBModalBody,
    } from 'mdb-vue-ui-kit';
    import { ref } from 'vue';
    import mdbtabled from '@/components/mdbtable.vue'
    import navbar from '@/components/navbar.vue'
    import dept_add from './user_add.vue'
    import {mapActions, mapGetters} from 'vuex'
    export default {
      setup() {
        const exampleModal = ref(false);
        return {
          exampleModal,
        };
      },
      components: {
          MDBBtn,
          MDBBadge,
          MDBIcon,
          MDBModal,
          MDBModalHeader,
          MDBModalTitle,
          MDBModalBody,
          navbar,
          dept_add,
          mdbtabled
      },
      data(){
          return{
              show_dept: false,
          }
      },
      async mounted(){
          await this.fetch_user();
          console.log(this.get_user_list)
          console.log('this.get_user_list')
      },
      computed: mapGetters(['get_user_list']),
      methods:{
          ...mapActions(['fetch_user']),
          addDept(){
              console.log('dept')
              this.show_dept = true;
              this.exampleModal = true;
          }
      }
  
    };
  </script>
  
  <style>
  .mainpage{
      height: calc(100vh - 85px);
      overflow-y: scroll;
      padding:25px 15px;
  }
  .main_table{
      padding: 15px;
      box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
      border-radius: 5px;
  }
  .table_header{
      /* position:sticky; 
      top:-25px;
      z-index: 2; */
      background:#e2e2e2;
      
  }
  th,td{
      padding: 5px !important;
      font-size: 12.5px;
  }
  </style>
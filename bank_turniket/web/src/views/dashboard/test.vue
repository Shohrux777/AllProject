<template>
    <div class="deparment_page">
      <navbar :title = "$t('dashboard')" @add="addDept" :added_status = "added_status"/>
      <div class="mainpage">
          <div class="main_table ">
              <p>{{ data_list }}</p>
          </div>
          <MDBBtn style="font-size: 11px;" @click="submit_add" color="success">Refresh api</MDBBtn>

      </div>
    </div>
  </template>
  
  <script>
    import {
      
      MDBBtn,
      MDBBadge,
      MDBIcon,
      
    } from 'mdb-vue-ui-kit';
    import { ref } from 'vue';
    import navbar from '@/components/navbar.vue'
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
          
          navbar,
      },
      data(){
          return{
              show_dept: false,
              added_status: false,
              data_list: [],
          }
      },
      methods:{

          async submit_add(){
            try{
              const requestOptions = {
              method: "GET",
              headers: { "Content-Type" : "application/json" },
              };
              const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes", requestOptions);
              const data = await response.json();
              console.log(data)
              this.data_list = data;
             
            }
            catch{
              console.log('error catch')
            }
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
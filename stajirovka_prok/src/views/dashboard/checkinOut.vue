<template>
    <div class="deparment_page">
      <navbar :title = "$t('dashboard')" @add="addDept" :added_status = "added_status"/>
      <div class="mainpage ">
          <div class="dashboart_header ">
            <div class="row">
              <div class="col-3 card" style="cursor:pointer;">
                <div class="dashboard_card_item p-3 d-flex">
                  <div class="dashboard_card_item_left">
                    <div class="icon_box bg-info card ">
                      <i class="fas fa-users text-white"></i>
                    </div>
                    <small >Xodimlar</small>
                  </div>
                  <div class="dashboard_card_item_right">
                    <small>25</small>
                  </div>
                  
                  
                </div>
              </div>
            </div>
          </div>
      </div>
    </div>
  </template>
  
  <script>
    import { ref } from 'vue';
    import navbar from '@/components/navbar.vue'
    import {mapActions, mapGetters} from 'vuex'
    export default {
      setup() {
        const exampleModal = ref(false);
        return {
          exampleModal,
        };
      },
      components: {
          navbar
      },
      data(){
        return{
          show_dept: false,
          added_status: false,
        }
      },
      async mounted(){
        setInterval(this.fetch_dashboard_list, 3000);
          await this.fetch_dashboard_list();
          console.log(this.get_dashboard_list)
          console.log('this.get_dashboard_list')
      },
      computed: mapGetters(['get_dashboard_list']),
      methods:{
          ...mapActions(['fetch_dashboard_list']),
          addDept(){
            console.log('dept')
            this.show_dept = true;
            this.exampleModal = true;
          }
      }
    };
  </script>
  
  <style lang="scss">
  .mainpage{
      height: calc(100vh - 85px);
      overflow-y: scroll;
      padding: 25px 30px;
      background: #F3F5F9;
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
  .icon_box{
    width: 40px;
    height: 40px;
    display: flex;
    align-items: center;
    justify-content: center;
  }
  .dashboard_card_item_left{
    width: 80%;
    display: flex;
    small{
      font-size: 20px;
    }
  }
  .dashboard_card_item_right{
    width: 20%;
    display: flex;
    align-items: center;
    justify-content: end;
    small{
      font-size: 27px;
    }
  }
  </style>
<template>
    <div class="deparment_page" style="position:relative;">
      <navbar :title = "$t('dashboard')" @add="addDept" :added_status = "added_status"/>

      <loader v-if="loading"/>
      <div v-else class="mainpage ">
          <div class="dashboart_header ">
            <div class="row">
              <div class="col-3 " style="cursor:pointer;" v-if="role == 'admin'">
                <div class="card">
                  <div class="dashboard_card_item  p-3 d-flex">
                    <div class="dashboard_card_item_left">
                      <div class="icon_box bg-secondary card">
                        <i class="fas fa-users text-white"></i>
                      </div>
                      <small >Ходимлар</small>
                    </div>
                    <div class="dashboard_card_item_right">
                      <small class="text-secondary">{{get_user_list.length}}</small>
                    </div>
                  </div>
                </div>
              </div>
              
              <div class="col-3 " style="cursor:pointer;">
                <div class="card">
                  <div class="dashboard_card_item  p-3 d-flex">
                    <div class="dashboard_card_item_left">
                      <div class="icon_box bg-primary card">
                        <i class="fas fa-list-check text-white"></i>
                      </div>
                      <small >Топшириқлар</small>
                    </div>
                    <div class="dashboard_card_item_right">
                      <small class="text-primary">83</small>
                    </div>
                  </div>
                </div>
              </div>
              
              <div class="col-3 " style="cursor:pointer;">
                <div class="card">
                  <div class="dashboard_card_item  p-3 d-flex">
                    <div class="dashboard_card_item_left">
                      <div class="icon_box bg-success card ">
                        <i class="fas fa-check text-white" ></i>
                      </div>
                      <small >Қабул қилинган</small>
                    </div>
                    <div class="dashboard_card_item_right">
                      <small class="text-success">{{accept_count}}</small>
                    </div>
                  </div>
                </div>
              </div>

              <div class="col-3 " style="cursor:pointer;">
                <div class="card">
                  <div class="dashboard_card_item  p-3 d-flex">
                    <div class="dashboard_card_item_left">
                      <div class="icon_box bg-danger card ">
                        <i class="fas fa-xmark text-white"></i>
                      </div>
                      <small >Рад этилган</small>
                    </div>
                    <div class="dashboard_card_item_right">
                      <small class="text-danger">{{ignore_count}}</small>
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-6 mt-3 " v-if="role == 'admin'" style="cursor:pointer;">
                <div class="card">
                  <div class="dashboard_card_item  p-3 d-flex">
                    <div class="dashboard_card_item_left">
                      <div class="icon_box bg-info card ">
                        <i class="fas fa-chart-pie text-white"></i>
                      </div>
                      <div class="w-100">
                        <small >Ходимлар умумий кўрсатгичи</small>
                        <div class=" range_component mx-3 mt-1">
                          <div :class="{'foiz_0': get_user_all_foiz == 0 || get_user_all_foiz == null, 'foiz_56': get_user_all_foiz < 56 && get_user_all_foiz > 0, 'foiz_71': get_user_all_foiz < 71 && get_user_all_foiz > 56,  'foiz_95': get_user_all_foiz < 95 && get_user_all_foiz >71, 'foiz_100': get_user_all_foiz>=95}" 
                          style=" height:4px;" :style="{ width: (get_user_all_foiz ? get_user_all_foiz : 0) + '%' }"></div>
                        </div>
                      </div>
                    </div>
                    <div class="dashboard_card_item_right">
                      <small class="text-info">{{get_user_all_foiz}}%</small>
                    </div>
                  </div>
                </div>
              </div>
              
              <div class=""  :class="{'col-6 mt-3': role == 'admin', 'col-3': role == 'user'}" style="cursor:pointer;">
                <div class="card">
                  <div class="dashboard_card_item  p-3 d-flex">
                    <div class="dashboard_card_item_left">
                      <div class="icon_box bg-warning card ">
                        <i class="fas fa-clock text-white"></i>
                      </div>
                      <small >Тасдиқлашда</small>
                    </div>
                    <div class="dashboard_card_item_right">
                      <small class="text-warning">{{pending_count}}</small>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="dashboart_header mt-3">
            <div class="row">
              
              
             
            </div>
          </div>

          <div class="dashboart_header card p-3 mt-3">
            <div>
              <p class="m-0" style="font-style:italic; font-size: 15px; color:#757580;">Ходимлар статистикаси</p>
            </div>
            <div>
              <table class="table align-middle mb-0 bg-white">
                <thead class="bg-light">
                  <tr>
                    <th width="550">ФИО</th>
                    <!-- <th>Лавозим</th> -->
                    <th >Статус</th>
                    <th width="300">Кўрсатгич</th>
                    <th v-if="role == 'admin'" width="100">Кўриш</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(user,i) in get_user_list.rows" :key="i">
                    <td>
                      <div class="d-flex align-items-center">
                        <img
                            src="@/assets/pic1.png"
                            alt=""
                            style="width: 45px; height: 45px"
                            class="rounded-circle"
                            />
                        <div class="ms-3">
                          <p class="fw-bold mb-1">{{user.name}}</p>
                          <p class="text-muted mb-0">{{user.full_name}}</p>
                        </div>
                      </div>
                    </td>
                    
                    <td>
                      <span class="badge badge-success rounded-pill d-inline">Active</span>
                    </td>
                    <td>
                      <div class="w-100 d-flex align-items-center">
                        <div class="range_item_table">
                          <div class=" range_component ">
                            <div :class="{'foiz_0': user.foiz == 0 || user.foiz == null, 'foiz_56': user.foiz < 56 && user.foiz > 0, 'foiz_71': user.foiz < 71 && user.foiz > 56,  'foiz_95': user.foiz < 95 && user.foiz >71, 'foiz_100': user.foiz>=95}" 
                              style=" height:4px;" :style="{ width: (user.foiz ? user.foiz : 0) + '%' }"></div>
                          </div>
                        </div>
                        <div class="range_item_procent">
                          <small v-if="user.foiz>0" class="mx-2" style="font-weight:bold;">{{user.foiz}}%</small>
                          <small v-else class="mx-2" style="font-weight:bold;">0 %</small>
                        </div>
                      </div>
                    </td>
                    <td v-if="role == 'admin'">
                      <button type="button" @click="link_router" class="btn btn-link btn-sm btn-rounded">
                        Ochish
                      </button>
                    </td>
                  </tr>
                </tbody>
              </table>

            </div>
          </div>

      </div>
    </div>
  </template>
  
  <script>
    import { ref } from 'vue';
    import Loader from '@/components/loader.vue';
    import navbar from '@/components/navbar.vue';
    import {mapActions, mapGetters} from 'vuex';
    export default {
      setup() {
        const exampleModal = ref(false);
        return {
          exampleModal,
        };
      },
      components: {
          navbar,
          Loader
      },
      data(){
        return{
          loading: false,
          show_dept: false,
          added_status: false,
          accept_count: localStorage.getItem('accept_count'),
          ignore_count: localStorage.getItem('ignore_count'),
          pending_count: localStorage.getItem('pending_count'),
          role: localStorage.getItem('role'),
          user_id: localStorage.getItem('user_id'),

        }
      },
      async mounted(){
        this.loading = true;
        if(this.role == 'admin'){
          await this.fetch_status_answers(0);
          await this.fetch_status_answers(1);
          await this.fetch_status_answers(2);
          this.accept_count = localStorage.getItem('accept_count');
          this.ignore_count = localStorage.getItem('ignore_count');
          this.pending_count = localStorage.getItem('pending_count');
        }
        else{
          await this.fetch_user_task_answers(this.user_id);
          this.accept_count = this.get_count_user_answer_incorrect_list;
          this.ignore_count = this.get_count_user_answer_accept_list;
          this.pending_count = this.get_count_user_answer_pending_list;
        }
        await this.fetch_user();
        console.log(this.get_user_list)
        this.loading = false;

        
      },
      computed: mapGetters(['get_dashboard_list','get_user_all_foiz','get_user_list','get_count_user_answer_pending_list', 'get_count_user_answer_accept_list', 'get_count_user_answer_incorrect_list']),
      methods:{
          ...mapActions(['fetch_status_answers', 'fetch_user_task_answers','fetch_user']),
          addDept(){
            console.log('dept')
            this.show_dept = true;
            this.exampleModal = true;
          },
          link_router(){
            this.$router.push({ name: 'topshiriqlar' });
          }
      }
    };
  </script>
  
  <style>
  .mainpage{
      height: calc(100vh - 85px);
      overflow-y: scroll;
      padding: 25px 30px !important;
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
    
  }
  .dashboard_card_item_left small{
    color: rgb(93, 93, 94);
    margin: 0 13px;
    font-size: 16px;
  }
  .dashboard_card_item_right{
    width: 20%;
    display: flex;
    align-items: center;
    justify-content: flex-end;
    
  }
  .dashboard_card_item_right small{
    font-weight: bold;
    font-size: 26px;
  }
  .range_component{
    height: 4px;
    background: #ebebeb;
    border-radius: 7px;
  }
  .range_item_table{
    width: 80%;
  }
  .range_item_procent{
    width: 20%;
  }
  tr{
    cursor:pointer;
  }
  .foiz_0{
    background: yellow;
  }
  .foiz_56{
    background: rgb(255, 42, 42);
  }
  .foiz_71{
    background: rgb(254, 121, 121);
  }
  .foiz_95{
    background: rgb(255, 225, 30);
  }
  .foiz_100{
    background: rgb(32, 164, 14);
  }
  
  
  </style>
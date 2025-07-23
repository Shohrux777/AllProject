<template>
    <div class="deparment_page" style="position:relative;">
      <navbar :title = "$t('dashboard')" :added_status = "added_status"/>

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
                      <small >Ташкилотлар</small>
                    </div>
                    <div class="dashboard_card_item_right">
                      <small v-if="get_user_list.rows.length" class="text-secondary">{{get_user_list.rows.length}}</small>
                    </div>
                  </div>
                </div>
              </div>
              
              <!-- <div class="col-3 " style="cursor:pointer;" @click="task_show = !taskshow">
                <div class="card">
                  <div class="dashboard_card_item  p-3 d-flex">
                    <div class="dashboard_card_item_left">
                      <div class="icon_box bg-primary card">
                        <i class="fas fa-list-check text-white"></i>
                      </div>
                      <small >Топшириқлар</small>
                    </div>
                    <div class="dashboard_card_item_right">
                      <small v-if="get_all_task_count" class="text-primary">{{get_all_task_count}}</small>
                    </div>
                  </div>
                </div>
              </div> -->
              
              <div class="col-3 " style="cursor:pointer;">
                <div class="card" @click="showAll_Answer(1)">
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
                <div class="card" @click="showAll_Answer(2)">
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
              <div class=""  :class="{'col-3 ': role == 'admin', 'col-3': role == 'user'}" style="cursor:pointer;">
                <div class="card" @click="showAll_Answer(0)">
                  <div class="dashboard_card_item  p-3 d-flex">
                    <div class="dashboard_card_item_left">
                      <div class="icon_box bg-warning card ">
                        <i class="fas fa-clock text-white"></i>
                      </div>
                      <small>Тасдиқлашда</small>
                    </div>
                    <div class="dashboard_card_item_right">
                      <small class="text-warning">{{pending_count}}</small>
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-3 " v-if="role == 'user'" style="cursor:pointer;">
                <div class="card">
                  <div class="dashboard_card_item  p-3 d-flex">
                    <div class="dashboard_card_item_left">
                      <div class="icon_box bg-info card ">
                        <i class="fas fa-chart-pie text-white"></i>
                      </div>
                      <div class="w-100">
                        <small style="font-size: 12px;">Ташкилот умумий кўрсатгичи</small>
                        <div class=" range_component mx-3 mt-1">
                          <div :class="{'foiz_0': get_user_all_foiz == 0 || get_user_all_foiz == null, 'foiz_56': get_user_all_foiz < 56 && get_user_all_foiz > 0, 'foiz_71': get_user_all_foiz < 71 && get_user_all_foiz > 56,  'foiz_95': get_user_all_foiz < 95 && get_user_all_foiz >71, 'foiz_100': get_user_all_foiz>=95}" 
                          style=" height:4px;" :style="{ width: (get_user_all_foiz ? get_user_all_foiz : 0) + '%' }"></div>
                        </div>
                      </div>
                    </div>
                    <div class="dashboard_card_item_right">
                      <small class="text-info" style="font-size: 17px;">{{get_user_all_foiz}}%</small>
                    </div>
                  </div>
                </div>
              </div>
              

              <div class="col-6 mt-3 " v-if="role == 'admin' || role == 'user'" style="cursor:pointer;">
                <Circle_progress label="Бажариш кўрсаткичи" :qty="xodim_soni" :activ_qty="xodim_kelmagan" progress_bg="progress_bg_danger"/>
              </div>


              <div class="col-6 mt-3 " v-if="role == 'admin'" style="cursor:pointer;">
                <div class="card">
                  <div class="dashboard_card_item  p-3 d-flex">
                    <div class="dashboard_card_item_left">
                      <div class="icon_box bg-info card ">
                        <i class="fas fa-chart-pie text-white"></i>
                      </div>
                      <div class="w-100">
                        <small >Ташкилотлар умумий кўрсатгичи</small>
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
                    <th width="450">Ташкилотлар</th>
                    <th>Телифон рақами</th>
                    <th >Статус</th>
                    <th width="350">Кўрсатгич</th>
                    <th v-if="role == 'admin'" width="100">Кўриш</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(user,i) in get_user_list.rows" :key="i">
                    <td>
                      <div class="d-flex align-items-center">
                        <img
                            :src="hostname2 + user.image_base64"
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
                      <div class="ms-3">
                          <p class=" mb-1">{{user.mobile_phone}}</p>
                          <p class="text-muted mb-0">{{user.phone}}</p>
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
                          <small v-if="user.foiz>0" class="mx-2" style="font-weight:bold;">{{user.foiz}}/20 000</small>
                          <small v-else class="mx-2" style="font-weight:bold;">0 / 20 000</small>
                        </div>
                      </div>
                    </td>
                    <td v-if="role == 'admin'">
                      <button type="button" @click="link_router(user.id)" class="btn btn-link btn-sm btn-rounded">
                        Очиш
                      </button>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
      </div>
      <ModalTask  :show="task_show" classes="bg_dolg" closeColor="white" titlecolor="white" 
        title="Топшириқлар" @close="task_show = false" width="90%">
        <template v-slot:body>
          <div>
            <div class="card mt-3 px-3 pt-2 pb-1 w-100 " :class="{'alert-info': task_item_index == i}" style="cursor:pointer" v-for="(task,i) in get_task_list.rows" :key="i"  @click="task_item_show(i)">
              <div>
                <div class="d-flex justify-content-between">
                  <p class="m-0 " style="font-size: 15px; color:#293142;">{{task.name}}</p>
                </div>
                <div class="d-flex justify-content-between">
                  <small class="mx-2" style="font-style:italic; font-size: 12px; color:#67748A;">{{task.note}}</small>
                  <p class="m-0 text-success" style="font-size: 14px; font-weight:bold"> <span class="text-primary">{{task.count}}</span></p>
                </div>
              </div>
              <div class="mt-2" v-if="task_item_index == i" >
                <div class="mt-3 need_sendFile" v-html="task.description"></div>
              </div>
            </div>
          </div>
        </template>
      </ModalTask>

      <ModalTask  :show="answer_show" classes="bg_dolg" closeColor="white" titlecolor="white" 
        title="Берилган жавоблар" @close="answer_show = false" width="90%">
        <template v-slot:body>
          <div>
            <div class="card hover px-3 mt-2 pt-2 pb-1 mb-3" style="cursor:pointer" v-for="(answer,index) in answer_type_all_list" :key="index">
              <div>
                <div class="d-flex justify-content-between">
                  <p class="m-0 " style="font-size: 14.5px; color:#293142;">{{answer.task.name}}</p>
                  <!-- <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 17 May 2025</small> -->
                </div>
              </div>
              <div class="d-flex justify-content-between  mt-4 mb-1">
                <div>
                  <div class="px-3  " style="background: #4795EF; border-radius: 15px;" >
                    <small style=" font-size: 12px; color:white;">Берилган жавоб</small>
                  </div>
                  <small class="px-2" style=" font-size: 12px; color:#67748A;">
                    {{ answer.created_at.slice(8,10) }}  {{ oylar[answer.created_at.slice(5,7)] }}  {{ answer.created_at.slice(0,4) }} {{ answer.created_at.slice(11,16) }}
                  </small>
                </div>
                
                <div class="" style="text-align: right;">
                  <p class="m-0" style=" font-size: 13px; color:black;">{{answer.user.name}}</p>
                  <p class="m-0" style=" font-size: 12px; color:#67748A;">{{answer.user.full_name}}</p>
                </div>
              </div>

              <div class="border-start px-3 mt-3">
                <p class="m-0"  style=" font-size: 13px;">
                  {{answer.description}}
                </p>
              </div>

              <div class="mt-3">
                <div class="row"> 
                  <div class="col-6" @click="pdf_status = !pdf_status">
                    <div class="file-card w-100">
                      <div class="file-icon">
                        <span style="font-size: 14px;">PDF</span>
                        </div>
                      <div class="file-info">
                        <div class="file-name">{{answer.answer}}</div>
                        <div class="file-meta">
                          {{ answer.created_at.slice(8,10) }}  {{ oylar[answer.created_at.slice(5,7)] }}  {{ answer.created_at.slice(0,4) }} {{ answer.created_at.slice(11,16) }}
                        </div>
                      </div>
                      <div class="file-actions">
                        <i class="fa-regular fa-eye"></i>
                        <!-- <i class="far fa-circle-down"></i> -->
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <hr>
              <div class="d-flex justify-content-between  mt-4 mb-1">
                <div>
                  <div class="px-3 bg-success" style="border-radius: 15px;" v-if="type_status == 1">
                    <small style=" font-size: 12px; color:white;">Қабул қилинди</small>
                  </div>
                  <div class="px-3 bg-warning" style="border-radius: 15px;" v-else-if="type_status == 0">
                    <small style=" font-size: 12px; color:white;">Тасдиқлашда</small>
                  </div>
                  <div class="px-3 bg-danger" style="border-radius: 15px;" v-else-if="type_status == 2">
                    <small style=" font-size: 12px; color:white;">Рад этилган</small>
                  </div>
                  <small class="px-2" style=" font-size: 12px; color:#67748A;" v-if="type_status != 0">
                    {{ answer.updated_at.slice(8,10) }}  {{ oylar[answer.updated_at.slice(5,7)] }}  {{ answer.updated_at.slice(0,4) }} {{ answer.updated_at.slice(11,16) }}

                  </small>
                </div>
                
                <div class="" style="text-align: right;">
                  <p class="m-0" style=" font-size: 13px; color:black;">Админ</p>
                  <p class="m-0" style=" font-size: 12px; color:#67748A;">Сам. вил. Кадир бўлим бошлиғи</p>
                </div>
              </div>

              <div class="border-start px-3 mt-2 mb-4">
                <p class="m-0"  style=" font-size: 13px;" v-if="type_status != 0">
                  {{answer.note}}
                </p>
              </div>

              <div class="pdf_class_reader" v-show="pdf_status">
                <i class="fas fa-xmark pdf_class_reader_close" @click="pdf_status = !pdf_status"></i>
                <iframe
                  :src="hostname + answer.answer"
                  width="95%"
                  style="border: none;  height: 95vh !important"
                ></iframe>
              </div>
            </div>
          </div>
        </template>
      </ModalTask>
    </div>
  </template>
  
  <script>
    import { ref } from 'vue';
    import Loader from '@/components/loader.vue';
    import navbar from '@/components/navbar.vue';
    import ModalTask from '@/components/modal.vue';

    import {mapActions, mapGetters} from 'vuex';
import Circle_progress from './circle_progress.vue';
    export default {
      setup() {
        const exampleModal = ref(false);
        return {
          exampleModal,
        };
      },
      components: {
          navbar,
          Loader,
          ModalTask,
          Circle_progress
      },
      data(){
        return{
          task_show: false,
          answer_show: false,
          pdf_status: false,
          type_status: 1,
          loading: false,
          show_dept: false,
          added_status: false,
          hostname : this.$store.state.hostname1,
          hostname2: this.$store.state.hostname2,
          accept_count: localStorage.getItem('accept_count'),
          ignore_count: localStorage.getItem('ignore_count'),
          pending_count: localStorage.getItem('pending_count'),
          role: localStorage.getItem('role'),
          user_id: localStorage.getItem('user_id'),
          task_item_index: -1,
          answer_type_all_list: [],
          oylar:{
              "01": "Янв",
              "02": "Фев",
              "03": "Мар",
              "04": "Апр",
              "05": "Май",
              "06": "Июн",
              "07": "Июл",
              "08": "Авг",
              "09": "Сен",
              "10": "Окт",
              "11": "Ноя",
              "12": "Дек"
            },
          xodim_soni: 150000,
          xodim_kelmagan: 20000,

        }
      },
      async mounted(){
        console.clear();
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
          this.accept_count = this.get_count_user_answer_accept_list;
          this.ignore_count = this.get_count_user_answer_incorrect_list;
          this.pending_count = this.get_count_user_answer_pending_list;
        }
        await this.fetch_user();
        this.loading = false;
        await this.fetch_task();
        console.log('get_task_list',this.get_task_list)
        this.loading = false;

        
      },
      computed: mapGetters(['get_dashboard_list','get_user_all_foiz','get_user_list','get_count_user_answer_pending_list',
       'get_count_user_answer_accept_list', 'get_count_user_answer_incorrect_list', 'get_all_task_count', 'get_task_list',
      'get_answer_accept_list', 'get_answer_pending_list', 'get_answer_incorrect_list',
    'get_user_answer_pending_list', 'get_user_answer_accept_list', 'get_user_answer_incorrect_list']),
      methods:{
          ...mapActions(['fetch_status_answers', 'fetch_user_task_answers','fetch_user', 'fetch_task', ]),
          task_item_show(index){
            this.task_item_index = index;
          },
          async showAll_Answer(type){
            this.type_status = type;
            this.answer_show = true;
            if(type == 0){
              if(this.role == 'admin'){
                this.answer_type_all_list = this.get_answer_pending_list;
              }
              else{
                this.answer_type_all_list = this.get_user_answer_pending_list;
              }
            }
            else if(type == 1){
              if(this.role == 'admin'){
                this.answer_type_all_list = this.get_answer_accept_list;

              }
              else{
                this.answer_type_all_list = this.get_user_answer_accept_list;
                
              }
            }
            else{
              if(this.role == 'admin'){
                this.answer_type_all_list = this.get_answer_incorrect_list;

              }
              else{
                this.answer_type_all_list = this.get_user_answer_incorrect_list;
              }
            }
            console.log('this.answer_type_all_list', this.answer_type_all_list)
          },
          link_router(user_id){
            this.$router.push('/topshiriqlar/' + user_id);
          }
      }
    };
  </script>
  
  <style scoped>
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
    margin: 0 15px;
    font-size: 13px;
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
    width: 70%;
  }
  .range_item_procent{
    width: 30%;
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
  .need_sendFile{
    font-size: 13px;
  }
  .need_sendFile p{
    margin: 0;
    font-size: 14px;
  }
  .file-card {
      display: flex;
      align-items: center;
      background: #fff;
      border: 1px solid #e6e6e6;
      border-radius: 8px;
      padding: 10px 14px;
      width: 420px;
      box-shadow: 0 2px 5px rgba(199, 199, 199, 0.05);
    }

    .file-icon {
      background: #d32f2f;
      color: white;
      font-weight: bold;
      width: 55px;
      height: 55px;
      display: flex;
      align-items: center;
      justify-content: center;
      border-radius: 7px;
      margin-right: 15px;
    }

    .file-info {
      flex: 1;
    }

    .file-name {
      font-weight: 600;
      font-size: 12.5px;
    }

    .file-meta {
      font-size: 12px;
      color: #666;
    }

    .file-actions {
      display: flex;
      gap: 12px;
      margin-left: 10px;
    }

    .file-actions i {
      color: #7a7a7a;
      cursor: pointer;
      transition: color 0.3s;
      font-size: 18px;
    }

    .file-actions i:hover {
      color: #000;
    }
    .pdf_class_reader{
      background: rgba(72, 72, 72, 0.85);
      display: flex;
      justify-content: center;
      align-items: center;
      position: fixed; top:0; right:0; bottom:0; left:0;
    }
    .pdf_class_reader_close{
      position: absolute;
      right: 15px;
      top:5px;
      color:white;
      font-size: 20px;
      cursor:pointer;
    }
  </style>
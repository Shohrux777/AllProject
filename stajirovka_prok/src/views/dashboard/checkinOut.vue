<template>
    <div class="deparment_page">
      <navbar :title = "$t('dashboard')" @add="addDept" :added_status = "added_status"/>
      <div class="mainpage ">
          <div class="dashboart_header ">
            <div class="row">
              <div class="col-3 " style="cursor:pointer;">
                <div class="card">
                  <div class="dashboard_card_item  p-3 d-flex">
                    <div class="dashboard_card_item_left">
                      <div class="icon_box bg-secondary card ">
                        <i class="fas fa-users text-white"></i>
                      </div>
                      <small >Ходимлар</small>
                    </div>
                    <div class="dashboard_card_item_right">
                      <small class="text-secondary">25</small>
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
                      <small class="text-success">37</small>
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
                      <small class="text-danger">5</small>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="dashboart_header mt-3">
            <div class="row">
              <div class="col-6 " style="cursor:pointer;">
                <div class="card">
                  <div class="dashboard_card_item  p-3 d-flex">
                    <div class="dashboard_card_item_left">
                      <div class="icon_box bg-info card ">
                        <i class="fas fa-chart-pie text-white"></i>
                      </div>
                      <div class="w-100">
                        <small >Ходимлар умумий кўрсатгичи</small>
                        <div class=" range_component mx-3 mt-1">
                          <div style="width:72%; height:4px; background: #3DA152;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="dashboard_card_item_right">
                      <small class="text-info">72%</small>
                    </div>
                  </div>
                </div>
              </div>
              
              <div class="col-6 " style="cursor:pointer;">
                <div class="card">
                  <div class="dashboard_card_item  p-3 d-flex">
                    <div class="dashboard_card_item_left">
                      <div class="icon_box bg-warning card ">
                        <i class="fas fa-clock text-white"></i>
                      </div>
                      <small >Тасдиқлашда</small>
                    </div>
                    <div class="dashboard_card_item_right">
                      <small class="text-warning">18</small>
                    </div>
                  </div>
                </div>
              </div>
              
             
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
                    <th>Лавозим</th>
                    <th >Статус</th>
                    <th width="300">Кўрсатгич</th>
                    <th width="100">Кўриш</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td>
                      <div class="d-flex align-items-center">
                        <img
                            src="@/assets/pic1.png"
                            alt=""
                            style="width: 45px; height: 45px"
                            class="rounded-circle"
                            />
                        <div class="ms-3">
                          <p class="fw-bold mb-1">Rahimov Dilmurod Ravshanovich</p>
                          <p class="text-muted mb-0">john.doe@gmail.com</p>
                        </div>
                      </div>
                    </td>
                    <td>
                      <p class="fw-normal mb-1">Prokror</p>
                      <p class="text-muted mb-0">IT department</p>
                    </td>
                    <td>
                      <span class="badge badge-success rounded-pill d-inline">Active</span>
                    </td>
                    <td>
                      <div class="w-100 d-flex align-items-center">
                        <div class="range_item_table">
                          <div class=" range_component ">
                            <div style="width:75%; height:4px; background: #3DA152;"></div>
                          </div>
                        </div>
                        <div class="range_item_procent">
                          <small class="mx-2" style="font-weight:bold;">75%</small>
                        </div>
                      </div>
                    </td>
                    <td>
                      <button type="button" @click="link_router" class="btn btn-link btn-sm btn-rounded">
                        Ochish
                      </button>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <div class="d-flex align-items-center">
                        <img
                            src="@/assets/pic2.png"
                            class="rounded-circle"
                            alt=""
                            style="width: 45px; height: 45px"
                            />
                        <div class="ms-3">
                          <p class="fw-bold mb-1">Yusupov Baxrom Uktamovich</p>
                          <p class="text-muted mb-0">Payariq tuman</p>
                        </div>
                      </div>
                    </td>
                    <td>
                      <p class="fw-normal mb-1">Consultant</p>
                      <p class="text-muted mb-0">Finance</p>
                    </td>
                    <td>
                      <span class="badge badge-primary rounded-pill d-inline"
                            >Onboarding</span
                        >
                    </td>
                    <td>
                      <div class="w-100 d-flex align-items-center">
                        <div class="range_item_table">
                          <div class=" range_component ">
                            <div style="width:23%; height:4px; background: red;"></div>
                          </div>
                        </div>
                        <div class="range_item_procent">
                          <small class="mx-2" style="font-weight:bold;">23%</small>
                        </div>
                      </div>
                    </td>
                    <td>
                      <button
                              type="button"
                              class="btn btn-link btn-rounded btn-sm fw-bold"
                              data-mdb-ripple-color="dark"
                              @click="link_router"
                              >
                        Ochish
                      </button>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <div class="d-flex align-items-center">
                        <img
                            src="https://cdn1.img.sputniknews.uz/img/07e7/0b/07/40747524_348:142:876:670_1920x0_80_0_0_01c3c794650c050cba2312626a52c04c.jpg"
                            class="rounded-circle"
                            alt=""
                            style="width: 45px; height: 45px"
                            />
                        <div class="ms-3">
                          <p class="fw-bold mb-1">Xamidov Uktam Tursunovich</p>
                          <p class="text-muted mb-0">Ishtixon tuman</p>
                        </div>
                      </div>
                    </td>
                    <td>
                      <p class="fw-normal mb-1">Prokror</p>
                      <p class="text-muted mb-0">Mutaxassis</p>
                    </td>
                    <td>
                      <span class="badge badge-warning rounded-pill d-inline">Awaiting</span>
                    </td>
                    <td>
                      <div class="w-100 d-flex align-items-center">
                        <div class="range_item_table">
                          <div class=" range_component ">
                            <div style="width:46%; height:4px; background: #1F282D;"></div>
                          </div>
                        </div>
                        <div class="range_item_procent">
                          <small class="mx-2" style="font-weight:bold;">46%</small>
                        </div>
                      </div>
                    </td>
                    <td>
                      <button
                              type="button"
                              class="btn btn-link btn-rounded btn-sm fw-bold"
                              data-mdb-ripple-color="dark"
                              @click="link_router"
                              >
                        Ochish
                      </button>
                    </td>
                  </tr>
                                    <tr>
                    <td>
                      <div class="d-flex align-items-center">
                        <img
                            src="@/assets/pic1.png"
                            alt=""
                            style="width: 45px; height: 45px"
                            class="rounded-circle"
                            />
                        <div class="ms-3">
                          <p class="fw-bold mb-1">Rahimov Dilmurod Ravshanovich</p>
                          <p class="text-muted mb-0">john.doe@gmail.com</p>
                        </div>
                      </div>
                    </td>
                    <td>
                      <p class="fw-normal mb-1">Prokror</p>
                      <p class="text-muted mb-0">IT department</p>
                    </td>
                    <td>
                      <span class="badge badge-success rounded-pill d-inline">Active</span>
                    </td>
                    <td>
                      <div class="w-100 d-flex align-items-center">
                        <div class="range_item_table">
                          <div class=" range_component ">
                            <div style="width:82%; height:4px; background: #3DA152;"></div>
                          </div>
                        </div>
                        <div class="range_item_procent">
                          <small class="mx-2" style="font-weight:bold;">82%</small>
                        </div>
                      </div>
                    </td>
                    <td>
                      <button type="button" class="btn btn-link btn-sm btn-rounded" @click="link_router">
                        Ochish
                      </button>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <div class="d-flex align-items-center">
                        <img
                            src="@/assets/pic2.png"
                            class="rounded-circle"
                            alt=""
                            style="width: 45px; height: 45px"
                            />
                        <div class="ms-3">
                          <p class="fw-bold mb-1">Yusupov Baxrom Uktamovich</p>
                          <p class="text-muted mb-0">Payariq tuman</p>
                        </div>
                      </div>
                    </td>
                    <td>
                      <p class="fw-normal mb-1">Consultant</p>
                      <p class="text-muted mb-0">Finance</p>
                    </td>
                    <td>
                      <span class="badge badge-primary rounded-pill d-inline"
                            >Onboarding</span
                        >
                    </td>
                    <td>
                      <div class="w-100 d-flex align-items-center">
                        <div class="range_item_table">
                          <div class=" range_component ">
                            <div style="width:28%; height:4px; background: red;"></div>
                          </div>
                        </div>
                        <div class="range_item_procent">
                          <small class="mx-2" style="font-weight:bold;">28%</small>
                        </div>
                      </div>
                    </td>
                    <td>
                      <button
                              type="button"
                              class="btn btn-link btn-rounded btn-sm fw-bold"
                              data-mdb-ripple-color="dark"
                              @click="link_router"
                              >
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
          navbar
      },
      data(){
        return{
          show_dept: false,
          added_status: false,
        }
      },
      computed: mapGetters(['get_dashboard_list']),
      methods:{
          ...mapActions(['fetch_dashboard_list']),
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
    font-size: 19px;
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
  </style>
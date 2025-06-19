<template>
  <div class="deparment_page" style="position: relative;">
    <navbar title = "Топшириқлар"  :added_status = "added_status"/>
      <loader v-if="loading"/>

    <div v-else class="example">
      <div class="example_cont">
        <div class="w-100">
          <div class="row">
            <div class="col-6">
              <div class="d-flex align-items-center">
                <img
                  :src="hostname2 + get_user_data.image_base64"
                  class="rounded"
                  alt=""
                  style="width: 120px; height: 120px"
                  />
                <div class="ms-3">
                  <p class="fw-bold mb-0" style="font-size: 15px;">{{get_user_data.name}}</p>
                  <!-- <p class="text-muted mb-0" style="font-size: 12px;">
                    <span style="font-weight:bold; margin-right:7px;">Manzil:</span>Samarqand viloyat Ishtixon tuman</p> -->
                  <p class="text-muted mb-0" style="font-size: 12px;">
                    <span style="font-weight:bold; margin-right:7px;">Лавозими:</span>{{get_user_data.full_name}}</p>
                  <p class="text-muted mb-0" style="font-size: 12px;">
                    <span style="font-weight:bold; margin-right:7px;">Телифон номер:</span>{{get_user_data.mobile_phone}}</p>
                  <p class="text-muted mb-0" style="font-size: 12px;">
                    <span style="font-weight:bold; margin-right:7px;">IP телифон:</span>{{get_user_data.phone}}</p>
                  <p class="text-muted mb-0" style="font-size: 12px;">
                    <span style="font-weight:bold; margin-right:7px;">ЖШШИР:</span>{{get_user_data.passport}}</p>
                </div>
              </div>
            </div>
            <div class="col-6">
              <div class="text-end">
                <div class="user_answers d-flex align-items-center w-100">
                  <div class="text-muted mx-2" style="width:20%; font-size: 15px;">
                    Топшириқлар
                  </div>
                  <div style="width:40%;">
                    <div class=" range_component rounded">
                      <div class="rounded bg-primary" style="width:100%; height:10px;"></div>
                    </div>
                  </div>
                  <div  class="text-primary mx-2" style="font-size: 16px; font-weight:bold;">
                    {{get_all_task_count}}
                  </div>
                </div>

                <div class="user_answers d-flex align-items-center w-100">
                  <div  class="text-muted mx-2" style="width:20%; font-size: 15px;">
                    Бажарилган
                  </div>
                  <div style="width:40%;" >
                    <div class=" range_component rounded">
                      <div class="rounded bg-success" 
                      :style="{ width: (parseInt(get_count_user_answer_accept_list*100/get_all_task_count) ? parseInt(get_count_user_answer_accept_list*100/get_all_task_count) : 0) + '%' }"
                      style=" height:10px;" ></div>
                    </div>
                  </div>
                  <div  class="text-success mx-2 " style="font-size: 16px; font-weight:bold;">
                    {{get_count_user_answer_accept_list}}
                  </div>
                </div>
                <div class="user_answers d-flex align-items-center w-100">
                  <div  class="text-muted mx-2" style="width:20%; font-size: 15px;">
                    Тасдиқлашда
                  </div>
                  <div style="width:40%;" >
                    <div class=" range_component rounded">
                      <div class="rounded bg-warning" style=" height:10px;"
                      :style="{ width: (parseInt(get_count_user_answer_pending_list*100/get_all_task_count) ? parseInt(get_count_user_answer_pending_list*100/get_all_task_count) : 0) + '%' }" ></div>
                    </div>
                  </div>
                  <div  class="text-warning mx-2 " style="font-size: 16px; font-weight:bold;">
                    {{get_count_user_answer_pending_list}}
                  </div>
                </div>

                <div class="user_answers d-flex align-items-center w-100">
                  <div  class="text-muted mx-2" style="width:20%; font-size: 15px;">
                    Бажарилмаган
                  </div>
                  <div style="width:40%;" >
                    <div class=" range_component rounded">
                      <div class="rounded bg-danger" 
                      style="height:10px;"
                      :style="{ width: (parseInt((get_all_task_count-get_count_user_answer_accept_list)*100/get_all_task_count) ? parseInt((get_all_task_count-get_count_user_answer_accept_list)*100/get_all_task_count) : 0) + '%' }"
                      ></div>
                    </div>
                  </div>
                  <div  class="text-danger mx-2 " style="font-size: 16px; font-weight:bold;">
                    {{get_all_task_count-get_count_user_answer_accept_list}}
                  </div>
                </div>
              </div>
            </div>
          </div>
          
        </div>        
      </div>
      <div class="example_topshiriqlar">
        <div>
            <p class="m-0 " style="font-style:italic; font-size: 15px; color:#757580;">Топшириқлар</p>
          </div>
           

        <div class="card mt-2 px-3 pt-2 pb-1 w-100" style="cursor:pointer" v-for="(task,i) in task_list" :key="i">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 15px; color:#293142;">{{task.name}}</p>
              <small v-if="task.date" class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">
                Охирги фаол: {{ task.date.slice(8,10) }}  {{ oylar[task.date.slice(5,7)] }}  {{ task.date.slice(0,4) }}
              </small>
            </div>
            <div class="d-flex justify-content-between">
              <small class="mx-2" style="font-style:italic; font-size: 12px; color:#67748A;">{{task.note}}</small>
              <p class="m-0 text-success" style="font-size: 14px; font-weight:bold">{{task.accept}} / <span class="text-primary">{{task.count}}</span></p>
            </div>
          </div>
          <div class="w-100" style="height: 6px;" >
            <TotalTask :totalTasks="task.count" :doneCount="task.accept"  :pendingCount="task.pending"/>
          </div>
          <div class="d-flex justify-content-end mt-2 mb-1" @click="routeAnswers(task.id)">
            <div class="px-4 rounded alert-info" >
              <small style=" font-size: 12px; color:#67748A; font-weight:600;">Батафсил</small>
            </div>
          </div>
        </div>

        <!-- <div class="card mt-2 px-3 pt-2 pb-1 w-100" style="cursor:pointer">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 15px; color:#293142;">ІІІ. Терговга қадар текширув, суриштирув ва дастлабки тергов соҳаларида:</p>
              <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 22 Aprel 2025</small>
            </div>
            <div class="d-flex justify-content-between">
              <small class="mx-2" style="font-style:italic; font-size: 12px; color:#67748A;">Topshiriq yuzasidan kamida o'n beshta ish yuklash va bitta elektiv o'quv kurs.</small>
              <p class="m-0 text-success" style="font-size: 14px; font-weight:bold">2 / <span class="text-primary">16</span></p>
            </div>
          </div>
          <div class="d-flex w-100 bg-secondary" style="height: 6px;">
            <div class="border bg-success" style="width: 6.25%;"></div>
            <div class="border bg-success" style="width: 6.25%;"></div>
            <div class="border bg-warning" style="width: 6.25%;"></div>
            <div class="border bg-warning" style="width: 6.25%;"></div>
            <div class="border bg-warning" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
            <div class="border bg-secondary" style="width: 6.25%;"></div>
          </div>
          <div class="d-flex justify-content-end mt-2 mb-1"  @click="$router.push('/answers_user')">
            <div class="px-4 rounded alert-info" >
              <small style=" font-size: 12px; color:#67748A; font-weight:600;">Batafsil</small>
            </div>
          </div>
        </div>

        <div class="card mt-2 px-3 pt-2 pb-1 w-100" style="cursor:pointer">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 15px; color:#293142;">IV. Ички ишлар органларида қонунлар ижроси устидан назорат соҳасида:</p>
              <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 22 Aprel 2025</small>
            </div>
            <div class="d-flex justify-content-between">
              <small class="mx-2" style="font-style:italic; font-size: 12px; color:#67748A;">Topshiriq yuzasidan kamida beshta ish yuklash va bitta elektiv o'quv kurs.</small>
              <p class="m-0 text-success" style="font-size: 14px; font-weight:bold">2 / <span class="text-primary">6</span></p>
            </div>
          </div>
          <div class="d-flex w-100 bg-secondary" style="height: 6px;">
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>

          </div>
          <div class="d-flex justify-content-end mt-2 mb-1">
            <div class="px-4 rounded alert-info" >
              <small style=" font-size: 12px; color:#67748A; font-weight:600;">Batafsil</small>
            </div>
          </div>
        </div>

        <div class="card mt-2 px-3 pt-2 pb-1 w-100" style="cursor:pointer">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 15px; color:#293142;">V. Судларда ишлар кўрилишида прокурор ваколатини таъминлаш соҳасида:</p>
              <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 22 Aprel 2025</small>
            </div>
              <div class="d-flex justify-content-between">
              <small class="mx-2" style="font-style:italic; font-size: 12px; color:#67748A;">Topshiriq yuzasidan kamida beshta ish yuklash va bitta elektiv o'quv kurs.</small>
              <p class="m-0 text-success" style="font-size: 14px; font-weight:bold">2 / <span class="text-primary">6</span></p>
            </div>
          </div>
          <div class="d-flex w-100 bg-secondary" style="height: 6px;">
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>

          </div>
          <div class="d-flex justify-content-end mt-2 mb-1">
            <div class="px-4 rounded alert-info" >
              <small style=" font-size: 12px; color:#67748A; font-weight:600;">Batafsil</small>
            </div>
          </div>
        </div>

        <div class="card mt-2 px-3 pt-2 pb-1 w-100" style="cursor:pointer">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 15px; color:#293142;">VI. Суд ҳужжатлари ва бошқа органлар ҳужжатларини ижро этишда, қамоққа олинганларни сақлашда ва жиноий жазоларнинг ижро этилишида қонунларга риоя этилиши устидан назорат соҳасида:</p>
              <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 22 Aprel 2025</small>
            </div>
              <div class="d-flex justify-content-between">
              <small class="mx-2" style="font-style:italic; font-size: 12px; color:#67748A;">Topshiriq yuzasidan kamida beshta ish yuklash va bitta elektiv o'quv kurs.</small>
              <p class="m-0 text-success" style="font-size: 14px; font-weight:bold">2 / <span class="text-primary">12</span></p>
            </div>
          </div>
          <div class="d-flex w-100 bg-secondary" style="height: 6px;">
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-warning" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>

          </div>
          <div class="d-flex justify-content-end mt-2 mb-1">
            <div class="px-4 rounded alert-info" >
              <small style=" font-size: 12px; color:#67748A; font-weight:600;">Batafsil</small>
            </div>
          </div>
        </div>

        <div class="card mt-2 px-3 pt-2 pb-1 w-100" style="cursor:pointer">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 15px; color:#293142;">VII. Жисмоний ва юридик шахсларнинг мурожаатларини кўриб чиқиш соҳасида:</p>
              <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 22 Aprel 2025</small>
            </div>
              <div class="d-flex justify-content-between">
              <small class="mx-2" style="font-style:italic; font-size: 12px; color:#67748A;">Topshiriq yuzasidan kamida beshta ish yuklash va bitta elektiv o'quv kurs.</small>
              <p class="m-0 text-success" style="font-size: 14px; font-weight:bold">1 / <span class="text-primary">10</span></p>
            </div>
          </div>
          <div class="d-flex w-100 bg-secondary" style="height: 6px;">
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>

          </div>
          <div class="d-flex justify-content-end mt-2 mb-1">
            <div class="px-4 rounded alert-info" >
              <small style=" font-size: 12px; color:#67748A; font-weight:600;">Batafsil</small>
            </div>
          </div>
        </div>

        <div class="card mt-2 px-3 pt-2 pb-1 w-100" style="cursor:pointer">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 15px; color:#293142;">VIII. Қонунчиликни тарғиб қилиш, жамиятда ҳуқуқий саводхонликни ошириш бўйича:</p>
              <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 22 Aprel 2025</small>
            </div>
              <div class="d-flex justify-content-between">
              <small class="mx-2" style="font-style:italic; font-size: 12px; color:#67748A;">Topshiriq yuzasidan kamida beshta ish yuklash va bitta elektiv o'quv kurs.</small>
              <p class="m-0 text-success" style="font-size: 14px; font-weight:bold">8 / <span class="text-primary">10</span></p>
            </div>
          </div>
          <div class="d-flex w-100 bg-secondary" style="height: 5px;">
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-success" style="width: 16.666%;"></div>
            <div class="border bg-warning" style="width: 16.666%;"></div>
            <div class="border bg-secondary" style="width: 16.666%;"></div>

          </div>
          <div class="d-flex justify-content-end mt-2 mb-1">
            <div class="px-4 rounded alert-info" >
              <small style=" font-size: 12px; color:#67748A; font-weight:600;">Batafsil</small>
            </div>
          </div>
        </div> -->
      </div>
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
    import Loader from '@/components/loader.vue';

  import navbar from '@/components/navbar.vue'
import TotalTask from '@/components/totalTask.vue';

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
        navbar,
        TotalTask,
        Loader
    },
    data(){
        return{
          loading: false,
            show_dept: false,
            added_status:false,
            user_id: this.$route.params.id,
            hostname2: this.$store.state.hostname2,
            task_list: [],
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
            }
        }
    },
    async mounted(){
      this.loading = true;
        await this.fetch_user_id(this.user_id);
        await this.fetch_task();
        await this.fetch_user_task_answers(this.user_id);
        this.task_list = [];
        for(let i=0; i<this.get_task_list.rows.length; i++){
          let task = {
            id: this.get_task_list.rows[i].id,
            count : this.get_task_list.rows[i].count,
            name: this.get_task_list.rows[i].name,
            note: this.get_task_list.rows[i].note,
            description: this.get_task_list.rows[i].description,
            accept: 0,
            pending: 0,
            date: '',
          }
          for(let j=0; j<this.get_user_answer_list.length; j++){
            if(this.get_task_list.rows[i].id == this.get_user_answer_list[j].task_id){
              if(this.get_user_answer_list[j].status == 1){
                task.accept ++;
                task.date = this.get_user_answer_list[j].created_at
              }
              else if(this.get_user_answer_list[j].status == 0){
                task.date = this.get_user_answer_list[j].created_at
                task.pending ++;
              }
            }
          }
          this.task_list.push(task);
        }
      this.loading = false;


    },
    computed: mapGetters(['get_user_data', 'get_all_task_count', 'get_task_list','get_user_answer_list', 
    'get_count_user_answer_pending_list', 'get_count_user_answer_accept_list', 'get_count_user_answer_incorrect_list']),
    methods:{
        ...mapActions(['fetch_user_id', 'fetch_task', 'fetch_user_task_answers']),
        routeAnswers(task_id){
          localStorage.setItem('task_user_Id', this.user_id);
          this.$router.push('/answers_user/' +task_id)
        },
        selectData(data){
          console.log('select_date')
          console.log(data.userid)
          this.select_data = data
        },
        async deleteFunc(){
            try{
                console.log('deleteFunc')
                const requestOptions = {
                    method : "delete",
                };
                const response = await fetch(this.$store.state.hostname + "/SkudOyliks/" + this.select_data.id, requestOptions);
                const data = await response.json();
                console.log('data')
                console.log(data)
                console.log(response)
                if(response.status == 200 || response.status == 201){
                    await this.fetch_Salary();
                }
            }
            catch(error){
                console.log(error)
            }
          },
          async editFunc(){
            this.show_dept = true;
            this.exampleModal = true;
            console.log('editFunc')
          }
    }

  };
</script>

<style>
.example{
    height: calc(100vh - 85px);
    overflow-y: scroll;
    background: #F3F5F9;
}
.example_topshiriqlar{
  padding: 15px 20px !important;
}
.example_cont{
  padding: 10px 20px !important;
  background: white;
}
.range_component{
  height: 10px;
  background: #ebebeb;
  border-radius: 7px;
}
</style>
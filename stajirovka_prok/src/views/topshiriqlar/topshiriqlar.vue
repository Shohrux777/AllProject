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
                    <span style="font-weight:bold; margin-right:7px;">ЖШШИР:</span>{{get_user_data.passport}}
                  </p>
                  <p class="mb-0" @click="forwardArxiv" v-if="get_user_data.foiz>=95">
                    <span style="margin-right:7px;" class="forward_to_arxiv">
                      Ходимни архивга ўтказиш
                      <i class="fas fa-share" ></i>
                    </span>
                  </p>
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
                    {{alltask_count}}
                  </div>
                </div>

                <div class="user_answers d-flex align-items-center w-100">
                  <div  class="text-muted mx-2" style="width:20%; font-size: 15px;">
                    Бажарилган
                  </div>
                  <div style="width:40%;" >
                    <div class=" range_component rounded">
                      <div class="rounded bg-success" 
                      :style="{ width: (parseInt(get_count_user_answer_accept_list*100/alltask_count) ? parseInt(get_count_user_answer_accept_list*100/alltask_count) : 0) + '%' }"
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
                      :style="{ width: (parseInt(get_count_user_answer_pending_list*100/alltask_count) ? parseInt(get_count_user_answer_pending_list*100/alltask_count) : 0) + '%' }" ></div>
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
                      :style="{ width: (parseInt((alltask_count-get_count_user_answer_accept_list)*100/alltask_count) ? parseInt((alltask_count-get_count_user_answer_accept_list)*100/alltask_count) : 0) + '%' }"
                      ></div>
                    </div>
                  </div>
                  <div  class="text-danger mx-2 " style="font-size: 16px; font-weight:bold;">
                    {{alltask_count-get_count_user_answer_accept_list}}
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
           

        <div class="card mt-2 px-3 pt-2 pb-1 w-100" style="cursor:pointer" v-for="(task,i) in task_list" :key="i" :class="{'blink-red': task.status }">
          <div > 
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 15px; color:#293142;">{{task.name}}</p>
              <small v-if="task.status" class="mx-2 text-danger" style="font-style:italic; font-size: 12px; color:#67748A;">
                Топшириқ бажариш муддати: {{ task_date }} 
              </small>
              <small v-else-if="task.date" class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">
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
  import axios from 'axios';
  import navbar from '@/components/navbar.vue'
  import TotalTask from '@/components/totalTask.vue';

  import {mapActions, mapGetters} from 'vuex'
// import task from '@/store/moduls/task';
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
            alltask_count: 0,
            task_date: '',
            isNearDeadline: false,
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
        console.log(this.get_user_data);
        
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
            status: false,
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

        this.alltask_count = this.get_all_task_count;
        if(this.get_user_data.task_info){
            const arrayBack = this.get_user_data.task_info.split(' | ');
            this.task_list[4].count = arrayBack.length * 10;
            this.alltask_count = (this.alltask_count - 20) + arrayBack.length * 10
        }
        if(this.get_user_data.dead_line){
          if (!this.get_user_data.dead_line) return;
          this.task_date = this.get_user_data.dead_line.slice(0,10);
          const today = new Date();
          const deadline = new Date(this.get_user_data.dead_line);
          // Farqni millisekundlarda olib, kunlarga o‘tkazamiz
          const diffTime = deadline - today;
          const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

          // 5 kun yoki kam qolganda true qiladi
          this.isNearDeadline = diffDays <= 5 && diffDays >= 0;
          this.task_list[this.task_list.length-1].status = this.isNearDeadline;

        }
        console.log('this.task_list', this.isNearDeadline)

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
        async forwardArxiv(){
          try {
          let nol = "0";
          const now = new Date()
          const isoString = now.toISOString().slice(0, 19).replace('T', ' ')
          console.log('isoString',isoString)
          const token = localStorage.getItem('auth_token'); // login paytida saqlangan token
          const formData = new FormData();
          formData.append('name', this.get_user_data.name);
          formData.append('full_name', this.get_user_data.full_name);
          formData.append('email', this.get_user_data.email);
          formData.append('password', this.get_user_data.password);
          formData.append('phone', this.get_user_data.phone);
          formData.append('mobile_phone', this.get_user_data.mobile_phone);
          formData.append('description', this.get_user_data.description);
          formData.append('passport', this.get_user_data.passport);
          formData.append('image_base64', this.get_user_data.image_base64); // Fayl (PDF yoki JPG)
          formData.append('task_count', this.get_user_data.task_count);
          formData.append('task_id', this.get_user_data.task_id);
          formData.append('task_info', this.get_user_data.task_info);
          formData.append('dead_line', this.get_user_data.dead_line);
          formData.append('status', nol);
          formData.append('id', this.get_user_data.id);
          formData.append('date_end', isoString);

          const response = await axios.post(this.$store.state.hostname + '/api/by_user/', formData, {
            headers: {
              'Content-Type': 'multipart/form-data',
              'Authorization': `Bearer ${token}` // Agar token kerak bo‘lsa
            }
          })
            console.log('Yaratildi:', response.data);
            if(response.status == 200 || response.status == 201){
              // this.$emit('close')
              // await this.fetch_user();
            }
            // this.img_str = response.data.image_base64;
            alert("Foydalanuvchi arxivga o'tkazildi!");
          } catch (error) {
            console.error('Xatolik:', error.response?.data || error.message);
            alert("Xatolik yuz berdi!");
          }
        },
        
        async arxivList(){
          const token = localStorage.getItem('auth_token') // <<< bu yerga tokenni yozing

          try {
            const response = await axios.get(this.$store.state.hostname + '/api/by_year/', {
              params: {
                year: '2025'
              },
              headers: {
                Authorization: `Bearer ${token}`
              }
            })

            this.data = response.data
            console.log(response.data)
            console.log(response)
          } catch (err) {
            this.error = err.response?.data?.message || err.message
          } finally {
            this.loading = false
          }
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
.forward_to_arxiv{
  font-size: 11.5px;
  padding: 4px 5px;
  border-radius: 3px;
  border: 1px solid #76a1ff;
  cursor: pointer;
}
.forward_to_arxiv:hover{
  background: #588dff;
  color:white;
}
.forward_to_arxiv:hover i{
  color:white;
}
.forward_to_arxiv i{
  color: #588dff;
}
.blink-red {
  border: 2px solid red;
  animation: blinkRed 1s infinite;
  padding: 10px;
  border-radius: 6px;
}

@keyframes blinkRed {
  0%, 100% { box-shadow: 0 0 10px rgb(253, 69, 69); }
  50% { box-shadow: 0 0 0px rgb(255, 113, 113); }
}
</style>
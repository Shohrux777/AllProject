<template>
  <div class="deparment_page" style="position: relative;">
    <navbar title = "Топшириқлар"  :added_status = "added_status"/>
      <loader v-if="loading"/>

    <div v-else class="example">
      
      <div class="example_topshiriqlar">
        <div>
            <p class="m-0 " style="font-style:italic; font-size: 15px; color:#757580;">Topshiriqlar</p>
          </div>
           

        <div class="card mt-2 px-3 pt-2 pb-1 w-100" style="cursor:pointer" v-for="(task,i) in task_list" :key="i" :class="{'blink-red': task.status }">
          <div>
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
          <div class="d-flex justify-content-end mt-2 mb-1" @click="$router.push('/answers_userID/' + task.id)">
            <div class="px-4 rounded alert-info" >
              <small style=" font-size: 12px; color:#67748A; font-weight:600;">Batafsil</small>
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
            user_id: localStorage.getItem('user_id'),
            hostname2: this.$store.state.hostname2,
            task_date: '',
            isNearDeadline: false,
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
        if(this.get_user_data.task_info){
            const arrayBack = this.get_user_data.task_info.split(' | ');
            this.task_list[4].count = arrayBack.length * 10;
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
        console.log('this.task_list', this.task_list)
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
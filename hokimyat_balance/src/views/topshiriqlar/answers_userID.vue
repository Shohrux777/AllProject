<template>
  <div class="deparment_page" style="position: relative;">
    <navbar :title = "$t('topshiriqlar')"  :added_status = "added_status"/>
    <loader v-if="loading"/>
    <div v-else class="example">
      <div class="example_cont">
        <div class="w-100 d-flex justify-content-between">
          <div>
            <p class="m-0 pt-1" style="font-size: 14px;">
              Қарздорликни ёпиш
            </p>
          </div>
          <div class="d-flex" style="cursor:pointer;">
            <div  class="px-1 py-1 pb-2 mx-1" :class="{'active_border_bottom': type_status == 3}" @click="type_status=3">
              <p class="m-0 " style="font-size: 13px; color:#293142;">Ҳужжатлар<span class="badge rounded-pill" style="background:#F4AA79;" >{{ task_info.count }}</span></p>
            </div>
            <div  class="px-1 py-1 pb-2 mx-1" :class="{'active_border_bottom': type_status == 1}" @click="func_accept" >
              <p class="m-0 " style="font-size: 13px; color:#293142;" 
              >Бажарилган<span class="badge rounded-pill " style="background:#4EC264;">{{user_accept_answer.length}}</span></p>
            </div>
            <div  class="px-1 py-1 pb-2 mx-1" @click="func_pending"
              :class="{'active_border_bottom': type_status == 0}">
              <p class="m-0 " style="font-size: 13px; color:#293142;" >Тасдиқлаш<span class="badge rounded-pill" style="background:#6968EC;">{{user_pending_answer.length}}</span></p>
            </div>
            <div  class="px-1 py-1 pb-2 mx-1" @click="func_incorrect"
              :class="{'active_border_bottom': type_status == 2}">
              <p class="m-0 " style="font-size: 13px; color:#293142;" >Рад етилган<span class="badge rounded-pill" style="background:red;">{{user_incorrect_answer.length}}</span></p>
            </div>
          </div>
          
        </div>
      </div>
      <div class="example_topshiriqlar" v-if="type_status==3">
        <div class="card mt-2 px-3 pt-3 pb-0" style="cursor:pointer">
          <div>
            <div class="">
              <p class="m-0 " style="font-size: 14.5px; color:#293142;">Ҳужжатларни киритиш</p>
              <small class="mx-2" style="font-style:italic; font-size: 12.5px; color:#67748A;">
                Қарздорлик буйича керакли ҳужжатлар билан сўммани киритинг.
              </small>
              <div class="mt-3 need_sendFile" v-html="task_info.description">
              </div>
            </div>
          </div>
          <div class="px-2" v-if="task_id == 12 && task_date">
            <div class="my-1">
              <p class="m-0 p-0 text-danger" style="font-size: 13px;"> <i class="fas fa-calendar-day  text-primary" style="color:green"></i> 
                Топшириқ бажариш муддати: <span style="font-weight: bold;">{{ task_date }}</span>
              </p>
            </div>
            <div>
              
            </div>
          </div>
          <div class="px-2 pb-2" v-if="task_id == 8">
              <div class="custom-control custom-switch d-flex align-items-center" v-for="(row,i) in sudlar" :key="i">
                <p class="m-0 p-0" style="font-size: 13px;"> <i class="fas fa-square-check mx-2 text-succes" style="color:green"></i> {{row}}</p>
              </div>
            </div>
          <hr class="m-0 mb-2">
          <div class="mt-0">
            <div class="">
              <label for="input_summa" style="font-size: 12.5px;" class="ms-1 mb-0">Сўмма</label>
              <input type="number" class="form-control " v-model="summa" id="input_summa" style="font-size: 14px;">


              <label for="summary" style="font-size: 12.5px;" class="ms-1 mb-0 mt-1">Қисқача мазмуни *</label>
              <textarea id="summary" v-model="description" class="p-2" style="font-size: 14px;" placeholder="Қисқача мазмуни"></textarea>

              <div class="upload-container">
                <label for="file-upload" style="font-size: 13px;">
                  <img class="upload-icon" src="https://img.icons8.com/material-rounded/24/6a5acd/attach.png"/>
                  Yuklash
                </label>
                <input type="file" id="file-upload"  accept=".pdf" @change="handleFileChange"/>
                <span v-if="file" style="font-size: 12px;  color: #6b7280;" class="mb-2 ms-3">{{file.name}}</span>
                <span v-else style="font-size: 12px;  color: #6b7280;" class="mb-2 ms-3">Илтимос, асословчи файлларни бириктиринг!</span>
              </div>
            </div>
          </div>
          <div class="mt-0" v-if="file">
            <div class="row">
              <div class="col-6">
                <div class="file-card w-100" @click="pdf_status = !pdf_status">
                  <div class="file-icon">
                    <span style="font-size: 14px;">PDF</span>
                    </div>
                  <div class="file-info">
                    <div class="file-name">{{file.name}}</div>
                    <div class="file-meta">{{size}}<br>{{file.date}}</div>
                  </div>
                  <div class="file-actions">
                    <i class="fa-regular fa-eye"></i>
                    <i class="far fa-circle-down"></i>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="d-flex justify-content-end mt-3 mb-1 border-top pt-3 pb-2">
              <div class="px-3  py-1" style="background: #4795EF; border-radius: 5px;" @click="submitAnswer">
                <small style="font-size: 13px; color:white;" >Сақлаш</small>
              </div>
          </div>
          
        </div>
      </div>
      <div v-else class="example_topshiriqlar">
        <div class="card px-3 mt-3 pt-2 pb-1 mb-5" style="cursor:pointer" v-for="(answer,index) in user_answer_list" :key="index">
          <div>
            <div class="d-flex justify-content-between">
              <p class="m-0 " style="font-size: 14.5px; color:#293142;">{{answer.taskinfo.name}}</p>
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
              <p class="m-0" style=" font-size: 13px; color:black;">{{answer.userinfo.name}}</p>
              <p class="m-0" style=" font-size: 12px; color:#67748A;">{{answer.userinfo.full_name}}</p>
            </div>
          </div>

          <div class="border-start px-3 mt-3">
            <p class="m-0"  style=" font-size: 13px;">
              {{answer.description}}
            </p>
          </div>

          <div class="mt-3">
            <div class="row"> 
              <div class="col-6" @click="pdf_status2 = !pdf_status2">
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
          <div class="pdf_class_reader" v-show="pdf_status2">
            <i class="fas fa-xmark pdf_class_reader_close" @click="pdf_status2 = !pdf_status2"></i>
            <iframe 
              :src="hostname + answer.answer"
              width="95%"
              style="border: none;  height: 95vh !important"
            ></iframe>
          </div>
        </div>

      </div>
    </div>
    <div class="pdf_class_reader" v-show="pdf_status">
      <i class="fas fa-xmark pdf_class_reader_close" @click="pdf_status = !pdf_status"></i>
      <iframe 
        :src="pdfUrl"
        width="95%"
        style="border: none;  height: 95vh !important"
      ></iframe>
    </div>
    <alert ref="message"></alert>
  </div>
</template>

<script>
  import {
    MDBBtn,
    MDBBadge,
    MDBIcon,
    
  } from 'mdb-vue-ui-kit';
  import { ref } from 'vue';
  import axios from 'axios';
  import alert from '@/components/alert.vue';
  import navbar from '@/components/navbar.vue';
  import {mapActions, mapGetters} from 'vuex';
import Loader from '@/components/loader.vue';
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
      alert,
      Loader
    },
    data(){
        return{
            loading: false,
            pdf_status: false,
            pdf_status2: false,
            hostname : this.$store.state.hostname1,
            show_dept: false,
            added_status:false,
            file: null,
            description: '',
            task_id: this.$route.params.id,
            task_info: {},
            user: localStorage.getItem('user'),
            size: '',
            type_status: 3,
            user_answer_list: [],
            user_accept_answer: [],
            user_pending_answer: [],
            user_incorrect_answer: [],
            pdfUrl: null,
            task_date: '',
            summa:null,
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
            sudlar: []
            
        }
    },
    async mounted(){
      // this.loading = true;
      // await this.fetch_user_task();
      
      // const user_id = localStorage.getItem('user_id');
      // await this.fetch_user_id(user_id);
      // this.loading = false;

      // if(this.get_user_data.task_info && this.task_id == 8){
      //     this.sudlar = this.get_user_data.task_info.split(' | ');
      //     this.task_info.count = this.sudlar.length * 10;
      // }
      // if(this.get_user_data.dead_line && this.task_id == 12){
      //   this.task_date = this.get_user_data.dead_line.slice(0,10)
      // }
      // await this.fetch_user_all_answers()
      // console.log('get answer list',this.get_user_answer_list);
      
      
    },
    computed: mapGetters(['get_salary_list', 'get_user_answer_list', 'get_answer_user_task_status', 'get_user_data']),
    methods:{
        ...mapActions([ 'fetch_user_task_answers_status', 'fetch_user_id']),
        async func_accept(){
          this.type_status = 1;
          this.user_answer_list = this.user_accept_answer;
        },
        async func_pending(){
          this.type_status = 0;
          this.user_answer_list = this.user_pending_answer;

        },
        async func_incorrect(){
          this.type_status = 2;
          this.user_answer_list = this.user_incorrect_answer;

        },
        async fetch_user_all_answers(){
          const user_id = localStorage.getItem('user_id');

          await this.fetch_user_task_answers_status({user_id: parseInt(user_id),task_id: parseInt(this.task_id),status: 1,})
          this.user_accept_answer = this.get_answer_user_task_status;
          await this.fetch_user_task_answers_status({user_id: parseInt(user_id),task_id: parseInt(this.task_id),status: 0,})
          this.user_pending_answer = this.get_answer_user_task_status;
          await this.fetch_user_task_answers_status({user_id: parseInt(user_id),task_id: parseInt(this.task_id),status: 2,})
          this.user_incorrect_answer = this.get_answer_user_task_status;

          this.user_answer_list = this.user_accept_answer;
        },

        async fetch_user_task(){
            try {
              const token = localStorage.getItem('auth_token'); // login paytida saqlangan token
              const response = await axios.get(
                this.$store.state.hostname + '/api/tasks/' + this.task_id,
                {
                  headers: {
                    Authorization: `Bearer ${token}`,
                    Accept: 'application/json',
                    'ngrok-skip-browser-warning': '69420'
                  }
                }
              );
              // console.log('task', response);
              if(response.status == 200 || response.status == 201){
                this.task_info = response.data;
              }
            } catch (error) {
              // console.error('Xatolik:', error.response?.data || error.message);
              alert("Xatolik yuz berdi!");
            }
        },
        handleFileChange(event) {
          this.file = event.target.files[0];
          this.formatFileSize(this.file.size);
          if (this.file && this.file.type === "application/pdf") {
            this.pdfUrl = URL.createObjectURL(this.file);
          } else {
            alert("Faqat PDF fayl yuklang!");
          }
        },

        formatFileSize(size) {
          if (size < 1024) this.size = size + ' B';
          else if (size < 1024 * 1024) this.size = (size / 1024).toFixed(2) + ' KB';
          else this.size = (size / (1024 * 1024)).toFixed(2) + ' MB';
        },

        submitAnswer() {
          const user_id = localStorage.getItem('user_id'); // login paytida saqlangan token
          // console.log(user_id)
          const formData = new FormData();
          formData.append('user_id', user_id);
          formData.append('task_id', this.task_id);
          formData.append('status', 0);
          formData.append('type', 1);
          // formData.append('count', 1);
          // formData.append('note', 'answer');
          formData.append('description', this.description);
          formData.append('answer', this.file); // Fayl (PDF yoki JPG)
          const token = localStorage.getItem('auth_token'); // login paytida saqlangan token
          this.loading = true;
          axios.post(this.$store.state.hostname + '/api/answertasks', formData, {
            headers: {
              'Content-Type': 'multipart/form-data',
              'Authorization': `Bearer ${token}` // Agar token kerak bo‘lsa
            }
          })
          .then(response => {
            console.log('Yuborildi:', response.data);
            this.loading = false;
            this.$refs.message.success('Added_successfully');
            this.file = null;
            this.description = '';
          })
          .catch(error => {
            this.loading = false;
            console.error('Xatolik:', error.response?.data || error.message);
          });
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
  padding: 6px 30px 0px 30px !important;
  background: white;
}
.range_component{
  height: 10px;
  background: #ebebeb;
  border-radius: 7px;
}

.form-container {
      background-color: #fff;
      padding: 25px 30px;
      border-radius: 12px;
      box-shadow: 0 6px 20px rgba(0, 0, 0, 0.08);
      max-width: 700px;
      margin: auto;
    }

    label {
      display: block;
      font-weight: 600;
      margin-bottom: 10px;
      font-size: 15px;
      color: #333;
    }

    textarea {
      width: 100%;
      height: 120px;
      padding: 15px;
      border: 1px solid #d0d5dd;
      border-radius: 8px;
      font-size: 14px;
      resize: vertical;
      outline: none;
      transition: border-color 0.3s;
    }

    textarea:focus {
      border-color: #6366f1;
      box-shadow: 0 0 0 3px rgba(99, 102, 241, 0.2);
    }

    .upload-container {
      display: flex;
      align-items: center;
      margin-top: 15px;
    }

    .upload-container label {
      display: inline-flex;
      align-items: center;
      color: #6a5acd;
      cursor: pointer;
      font-size: 14px;
      gap: 6px;
    }

    .upload-container input {
      display: none;
    }

    .note {
      margin-left: 10px;
      font-size: 13px;
      color: #6b7280;
    }

    .upload-icon {
      width: 18px;
      height: 18px;
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
    .active_border_bottom{
      border-bottom:1px solid blue;
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
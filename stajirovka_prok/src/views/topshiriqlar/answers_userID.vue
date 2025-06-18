<template>
  <div class="deparment_page">
    <navbar :title = "$t('topshiriqlar')"  :added_status = "added_status"/>
    <div class="example">
      <div class="example_cont">
        <div class="w-100 d-flex justify-content-between">
          <div>
            <p class="m-0 pt-1" style="font-size: 14px;">
              {{task_info.name}}
            </p>
          </div>
          <div class="d-flex" style="cursor:pointer;">
            <div style="border-bottom:1px solid blue;" class="px-1 py-1 pb-2 mx-1" >
              <p class="m-0 " style="font-size: 13px; color:#293142;">Топшириқлар<span class="badge rounded-pill" style="background:#F4AA79;">{{ task_info.count }}</span></p>
            </div>
            <div  class="px-1 py-1 pb-2 mx-1" @click="$router.push('/bajarilgan')">
              <p class="m-0 " style="font-size: 13px; color:#293142;">Бажарилган<span class="badge rounded-pill " style="background:#4EC264;">3</span></p>
            </div>
            <div  class="px-1 py-1 pb-2 mx-1">
              <p class="m-0 " style="font-size: 13px; color:#293142;">Тасдиқлаш<span class="badge rounded-pill" style="background:#6968EC;">1</span></p>
            </div>
            <div  class="px-1 py-1 pb-2 mx-1">
              <p class="m-0 " style="font-size: 13px; color:#293142;">Бажарилмоқда<span class="badge rounded-pill" style="background:#5082F2;">12</span></p>
            </div>
          </div>
          
        </div>
      </div>
      <div class="example_topshiriqlar">
        <div class="card mt-2 px-3 pt-3 pb-0" style="cursor:pointer">
          <div>
            <div class="">
              <p class="m-0 " style="font-size: 14.5px; color:#293142;">{{task_info.name}}</p>
              <small class="mx-2" style="font-style:italic; font-size: 12.5px; color:#67748A;">
                {{task_info.note}}
              </small>
              <div class="mt-3 need_sendFile" v-html="task_info.description">
              </div>
            </div>
          </div>
          <hr class="m-0 mb-2">
          <div class="mt-0">
            <div class="">
              <label for="summary" style="font-size: 12.5px;" class="ms-1 mb-0">Қисқача мазмуни *</label>
              <textarea id="summary" v-model="description" class="p-2" style="font-size: 12px;" placeholder="Қисқача мазмуни"></textarea>

              <div class="upload-container">
                <label for="file-upload" style="font-size: 12px;">
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
                <div class="file-card w-100">
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
    import axios from 'axios';
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
        MDBBtn,
        MDBBadge,
        MDBIcon,
        navbar,
       
    },
    data(){
        return{
            show_dept: false,
            added_status:false,
            file: null,
            description: '',
            task_id: this.$route.params.id,
            task_info: {},
            user: localStorage.getItem('user'),
            size: '',
        }
    },
    async mounted(){
      await this.fetch_user_task();

      const user_id = localStorage.getItem('user_id'); // login paytida saqlangan user id
      await this.fetch_user_task_answers(user_id)
      console.log('get answer list',this.get_user_answer_list);
    },
    computed: mapGetters(['get_salary_list', 'get_user_answer_list']),
    methods:{
        ...mapActions(['fetch_Salary', 'fetch_user_task_answers']),
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
              console.log('task', response);
              if(response.status == 200 || response.status == 201){
                this.task_info = response.data;
              }
            } catch (error) {
              console.error('Xatolik:', error.response?.data || error.message);
              alert("Xatolik yuz berdi!");
            }
        },
        handleFileChange(event) {
          this.file = event.target.files[0];
          this.formatFileSize(this.file.size);
        },

        formatFileSize(size) {
          if (size < 1024) this.size = size + ' B';
          else if (size < 1024 * 1024) this.size = (size / 1024).toFixed(2) + ' KB';
          else this.size = (size / (1024 * 1024)).toFixed(2) + ' MB';
        },

        submitAnswer() {
          const user_id = localStorage.getItem('user_id'); // login paytida saqlangan token
          console.log(user_id)
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
          axios.post(this.$store.state.hostname + '/api/answertasks', formData, {
            headers: {
              'Content-Type': 'multipart/form-data',
              'Authorization': `Bearer ${token}` // Agar token kerak bo‘lsa
            }
          })
          .then(response => {
            console.log('Yuborildi:', response.data);
          })
          .catch(error => {
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
</style>
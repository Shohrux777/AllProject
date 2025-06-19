<template>
  <div class="deparment_page" style="position:relative;">
    <navbar title = "Берилган жавоблар" :added_status = "added_status"/>
    <loader v-if="loading"/>
    <div v-else class="w-100 d-flex ">
      <div style="width:27%;" class="border-end answer_list_left">
        <div class="text-center" v-if="get_answer_pending_list.length==0">
          <span style="font-size: 13px;">Берилган жавоблар мавжуд емас</span>
        </div>
        <div class="border-bottom pb-1" :class="{'alert-primary': i==active_item}"
          v-for="item, i in get_answer_pending_list" :key="i" @click="Select_answer(item, i)">
          <div class="d-flex justify-content-between w-100 px-1 py-2">
            <div style="width:83%;" class="px-2">
              <p class="m-0 mb-2 task_name_pending" style="font-size: 13px; height:60px "><span style="font-weight:bold;"></span> {{item.task.name}}</p>
              <span class="m-0 border rounded" style="font-size: 12px; padding: 6px 10px;"><span style="font-weight:bold;">{{item.user.name}}</span> </span>
            </div>
            <div style="width:12%;">
              <p class="m-0" style="font-size: 13px;">{{ item.created_at.slice(8,10) }} <br> {{ oylar[item.created_at.slice(5,7)] }} <br> {{ item.created_at.slice(0,4) }}</p>
            </div>
          </div>
        </div>
      </div>
      <div style="width:80%">
        <div class="example" v-if="select_answer">
          <div class="example_topshiriqlar">
            <div class="card mt-2 px-3 pt-2 pb-1" style="cursor:pointer">
              <div>
                <div class="d-flex justify-content-between">
                  <p class="m-0 " style="font-size: 14.5px; color:#293142;">{{select_answer.task.name}}</p>
                  <!-- <small class="mx-2" style="font-style:italic; font-size: 11px; color:#67748A;">Last activ: 17 May 2025</small> -->
                </div>
              </div>
              <div class="d-flex justify-content-between  mt-4 mb-1">
                <div>
                  <div class="px-3  " style="background: #4795EF; border-radius: 15px;" >
                    <small style=" font-size: 12px; color:white;">Берилган жавоб</small>
                  </div>
                  <small class="px-2" style=" font-size: 12px; color:#67748A;">{{ select_answer.created_at.slice(8,10) }}  {{ oylar[select_answer.created_at.slice(5,7)] }}  {{ select_answer.created_at.slice(0,4) }} {{ select_answer.created_at.slice(11,16) }} </small>
                </div>
                
                <div class="" style="text-align: right;">
                  <p class="m-0" style=" font-size: 13px; color:black;">{{select_answer.user.name}}</p>
                  <p class="m-0" style=" font-size: 12px; color:#67748A;">{{select_answer.user.full_name}}</p>
                </div>
              </div>

              <div class="border-start px-3 mt-3">
                <p class="m-0"  style=" font-size: 13px;">
                  {{select_answer.description}}
                </p>
              </div>

              <div class="mt-3">
                <div class="row">
                  <div class="col-6">
                    <div class="file-card w-100" @click="pdf_status = !pdf_status">
                      <div class="file-icon">
                        <span style="font-size: 14px;">PDF</span>
                        </div>
                      <div class="file-info">
                        <div class="file-name">{{select_answer.answer}}</div>
                        <div class="file-meta">{{ select_answer.created_at.slice(8,10) }}  {{ oylar[select_answer.created_at.slice(5,7)] }}  {{ select_answer.created_at.slice(0,4) }} {{ select_answer.created_at.slice(11,16) }}</div>
                      </div>
                      <div class="file-actions">
                        <i class="fa-regular fa-eye"></i>
                        <!-- <i class="far fa-circle-down"></i> -->
                      </div>
                    </div>
                  </div>

                </div>
              </div>

              <div class="d-flex justify-content-end mt-3 mb-1 border-top pt-3 pb-2" v-if="show_accept">
                <div class="px-3  py-1" style="background: #4795EF; border-radius: 5px;" @click="accept_answer">
                    <small style=" font-size: 13px; color:white;" >Қабул қилиш</small>
                  </div>
                <div class="px-3 py-1  mx-2" style="background: #d32f2f; border-radius: 5px;" @click="ignore_answer">
                    <small style=" font-size: 13px; color:white;" >Рад етиш</small>
                  </div>
              </div>
              <div class="mt-3 mb-1 border-top" v-else>
                <div class="px-3 my-2" style="background: #4795EF; border-radius: 15px; display:inline-block;" v-if="accept_status == 1">
                  <small style=" font-size: 12px; color:white;">Қабул қилиш</small>
                </div>
                <div class="px-3 my-2" style="background: #4795EF; border-radius: 15px; display:inline-block;" v-else>
                  <small style=" font-size: 12px; color:white;">Рад етиш</small>
                </div>
                <!-- <label for="summary" style="font-size: 12.5px;" class="ms-1 mb-0">Қисқача мазмуни *</label> -->
                <textarea id="summary" v-model="note" class="p-2" style="font-size: 12px;" placeholder="Қисқача мазмуни"></textarea>
                <div class="d-flex justify-content-end mt-3 mb-1 border-top pt-3 pb-2">
                  <div class="px-3  py-1" style="background: #d32f2f; border-radius: 5px;" @click="show_accept = !show_accept">
                    <small style=" font-size: 13px; color:white;">Бекор қилиш</small>
                  </div>
                  <div class="px-3  py-1 ms-2" style="background: #4795EF; border-radius: 5px;" @click="submit_accept_answer">
                    <small style=" font-size: 13px; color:white;">Сақлаш</small>
                  </div>
                </div>
              </div>
            </div>



            <div class="pdf_class_reader" v-show="pdf_status">
              <i class="fas fa-xmark pdf_class_reader_close" @click="pdf_status = !pdf_status"></i>
              <iframe 
                :src="hostname  + select_answer.answer"
                width="95%"
                style="border: none;  height: 95vh !important"
              ></iframe>
            </div>
            
            <!-- <VuePdf src="https://6e76-92-63-205-177.ngrok-free.app/storage/answers/68510d3c2139c_1750142268.pdf"  /> -->
            <!-- <vue-pdf-app style="height: 100vh;" pdf="https://6e76-92-63-205-177.ngrok-free.app/storage/answers/68510d3c2139c_1750142268.pdf"></vue-pdf-app> -->
          </div>
        </div>
      </div>
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
  import navbar from '@/components/navbar.vue'
  import {mapActions, mapGetters} from 'vuex'
  import Loader from '@/components/loader.vue';
  import alert from '@/components/alert.vue';

  import axios from 'axios'
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
        Loader,
        alert
        // VuePdfApp
    },
    data(){
        return{
            loading: false,
            show_dept: false,
            added_status:false,
            active_item: 0,
            select_answer: null,
            show_accept: true,
            accept_status: 0,
            hostname : this.$store.state.hostname1,
            pdf_status: false,
            note: '',
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
        await this.fetch_status_answers(0);
        console.log(this.get_answer_pending_list)
        this.active_item = 0;
        this.select_answer = this.get_answer_pending_list[0];
      this.loading = false;

        console.log('this.get_answer_pending_list')
    },
    computed: mapGetters(['get_answer_pending_list']),
    methods:{
        ...mapActions(['fetch_status_answers']),
        async Select_answer(data, index){
          this.active_item = index;
          this.select_answer = data;
          this.show_accept = true;
          this.accept_status = 0;
          console.log(data);
        },
        async ignore_answer(){
          this.show_accept = false;
          this.accept_status = 2;
        },
        async accept_answer(){
          this.show_accept = false;
          this.accept_status = 1;
        },

        async submit_accept_answer(){
          this.loading = true;
          const token = localStorage.getItem('auth_token') // agar token kerak bo‘lsa
          const payload = {
            status: parseInt(this.accept_status),
            note: this.note,
            id: this.select_answer.id,
            user_id: this.select_answer.user.id,
            task_id: this.select_answer.task.id,
          }

          try {
            const response = await axios.post(
              this.$store.state.hostname + `/api/calculate-foiz/`,
              payload,
              {
                headers: {
                  Authorization: `Bearer ${token}` // agar kerak bo‘lsa
                }
              }
            )

            console.log('Yuborildi ✅:', response.data)
            await this.fetch_status_answers(0);
            this.show_accept = true;
            if(this.get_answer_pending_list.length > 0){
              this.active_item = 0;
              this.select_answer = this.get_answer_pending_list[0];
            }
            else{
              this.select_answer = null;
            }
            this.loading = false;
            // alert('Muvaffaqiyatli saqlandi')
            this.$refs.message.success('Added_successfully');

          } catch (error) {
            this.loading = false;
            console.error('Xatolik ❌:', error.response?.data || error.message)
            alert('Xatolik yuz berdi!')
          }
        },

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
  padding: 10px 30px !important;
  background: white;
}
.range_component{
  height: 10px;
  background: #ebebeb;
  border-radius: 7px;
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
    .task_name_pending{
      display: -webkit-box;
      -webkit-line-clamp: 3;      /* Nechta qator ko‘rsatiladi */
      -webkit-box-orient: vertical;
      overflow: hidden;
      text-overflow: ellipsis;
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
    .answer_list_left{
      cursor:pointer; 
      height: calc(100vh - 85px) !important;
      overflow: hidden;
      overflow-y: scroll;
    }
</style>
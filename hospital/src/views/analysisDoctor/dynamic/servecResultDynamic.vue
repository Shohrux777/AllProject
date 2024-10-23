<template>
    <div class="result_lab result_lab22">
        <div class="container-fluid mb-0 bg-white shadow result_lab_header" style="position:sticky; top:0; z-index:1;">
          <div class="row px-1 py-0">
            <div class="col-3">
              <mdb-input :label="$t('patient')" size="sm" class="m-0 p-0 header_input" outline v-model="patient_name" />
            </div>
            <div class="col-2 ">
              <mdb-input :label="$t('analiz_id')" size="sm" class="m-0 p-0 header_input" outline v-model="analiz_id" />
            </div>
            <div class="col-2 ">
              <mdb-input :label="$t('patient_id')" size="sm" class="m-0 p-0 header_input" outline v-model="patient_id" @keydown.enter.native="choosePatientID(patient_id)"/>
            </div>
            <div class="col-5">
              <div class="d-flex justify-content-end align-items-center h-100">
                <router-link to="/doctor">
                  <div class="lab_update_icon lab_update_color_home">
                    <mdb-icon icon="home" color="white" />
                  </div>
                </router-link>
                <!-- <router-link to="/paymentsGroupLab">
                  <div class="lab_update_icon lab_update_color_analiz">
                    <mdb-icon fab color="white" icon="cloudsmith" />
                  </div>
                </router-link> -->
                <div class="lab_update_icon lab_update_color" @click="click_lab_update">
                  <mdb-icon icon="flask" color="white" />
                </div>
                
                <div class="lab_update_icon lab_update_color_user">
                  <mdb-icon icon="user" color="white" />
                </div>
              </div>
            </div>
          </div>
          <!-- <div style="position:absolute; left: 10px; top:10px;">
            <router-link to="/doctor">
                <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-home-2" width="27" height="27" viewBox="0 0 24 24" stroke-width="1.5" stroke="#00abfb" fill="none" stroke-linecap="round" stroke-linejoin="round">
                <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                <polyline points="5 12 3 12 12 3 21 12 19 12" />
                <path d="M5 12v7a2 2 0 0 0 2 2h10a2 2 0 0 0 2 -2v-7" />
                <rect x="10" y="12" width="4" height="4" />
                </svg>
            </router-link>
          </div> -->
        </div>
      <loader v-if="loading" />
      <div v-else class="result_lists container-fluid">
        <div class="row" v-if="get_client_info.fio">
            <div class="col-6 mt-4 ">
                <div class="card  px-3 py-2">
                    <div class=" patient_anketa">
                        <div class="patient_info">
                        <p class="text_content_border">{{$t('fio')}} <span>{{get_client_info.fio}}</span></p>
                        <p class="text_content_border">{{$t('bornDate')}} <span>{{patient_born}}</span></p>
                        <!-- <p class="text_content_border">{{$t('phone')}} <span>{{get_client_info.phoneNumber}}</span></p> -->
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-6 mt-4 ">
                <div class="card  px-3 py-2">
                    <div class=" patient_anketa">
                        <div class="patient_info " >
                        <p class="text_content_border" v-if="get_client_info.polType==1">{{$t('pol')}} <span>Мужчина</span></p>
                        <p class="text_content_border" v-else>{{$t('pol')}} <span>Женщина</span></p>
                        <p class="text_content_border">{{$t('phone')}} <span>{{get_client_info.phoneNumber}}</span></p>
                        <!-- <p class="text_content_border">{{$t('passportSerialNumber')}} <span>{{get_client_info.passportSerialNumber}}</span></p>
                        <p class="text_content_border">{{$t('district')}} <span>{{patient_district}}</span></p> -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div v-if="!lab_main_id && !lab_result_update_show">
          <div class="row mt-2 " v-if="get_service_patientId.length">
              <div class="col-3 px-3 mt-2" v-for="(service,i) in get_service_patientId" :key="i">
                  <div class="text-center service_item_hover card py-2 px-1" @click="chooseService(service)"
                      :class="{'bg-danger text-white': !service.finishPayment}">
                      <small style="font-size:12px;">{{ service.serviceName }}</small>
                  </div>
              </div>
          </div>
          <div class="row mt-3" v-else>
              <div class="col-12 px-2 ">
                  <div class="text-center card py-2 px-1">
                      <small style="font-size:12px;">Laboratoriya topilmadi.</small>
                  </div>
              </div>
          </div>
        </div>
        <div class="container-fluid mt-3 card" v-if="choosenListName.length && !lab_result_update_show">
          <div  class="text-center py-1 px-1" style="position:relative;">
              <small style="font-size:15px;">Kiritiladigan analizlar</small>
              <div 
                active-class="active_link"
                @click ="SelectFile"
                class="text-white d-flex pl-2 px-3 border-bottom menuitem dropLink justify-content-center" 
                style="font-size:12px; padding: 5px 0; background: #2196F3;"
                >
                <span>Выберите файл</span>
                <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-cloud-download ml-2" width="17" height="17" viewBox="0 0 24 24" stroke-width="2.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <path d="M19 18a3.5 3.5 0 0 0 0 -7h-1a5 4.5 0 0 0 -11 -2a4.6 4.4 0 0 0 -2.1 8.4" />
                    <line x1="12" y1="13" x2="12" y2="22" />
                    <polyline points="9 19 12 22 15 19" />
                </svg>
              </div>
            <div 
                active-class="active_link"
                @click ="close_result"
                class="text-white d-flex pl-2 px-3 border-bottom menuitem closeLink justify-content-center" 
                style="font-size:12px; padding: 5px 0; background: #ff4949;"
                >
                <span>Закрить</span>
                <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-circle-x ml-2" width="18" height="18" viewBox="0 0 24 24" stroke-width="2.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                  <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                  <path d="M12 12m-9 0a9 9 0 1 0 18 0a9 9 0 1 0 -18 0" />
                  <path d="M10 10l4 4m0 -4l-4 4" />
                </svg>
            </div>
          </div>
          <div class="row mt-2 mb-2" >
              <div class="col-3 px-2 " v-for="(item,index) in choosenListName" :key="index">
                  <div class="text-center  card py-2 px-1" :class="{'bg_active text-white': item.status}"
                  @click="chooseItem(index)">
                      <small style="font-size:12px;">{{ item.name }}</small>
                  </div>
              </div>
          </div>
          <div class="mt-2" v-if="lab_main_id && !lab_result_update_show">
              <labAddResult :main_name_title = "lab_main_item" :main_id = "lab_main_id" @close="getActiveService"
              :patient_name = "patient_name" :patient_id="active_patient_id" :patient_born="patient_born"/>
          </div>
        </div>
        <div class="update_lab_result mt-3" v-if="lab_result_update_show">
          <labUpdateResult :main_name_title="lab_result_data.hospitalAnalizDynamicmain" :main_id="lab_result_data.hospitalAnalizDynamicmainId" 
            :patient_id="lab_result_data.patientsId" :patient_name="lab_result_data.name" 
            :patient_born="lab_result_data.patients.bornDate.slice(0,10)" :lab_result_props="lab_result_data" 
            :lab_pic_id= "lab_pic_id"/>
        </div>
      </div>


      <!-- File select  -->
      <div class="dropFile" v-if="fileSelect_show">
        <div class="close_file" @click="fileSelect_show = false">
          <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-x" width="17" height="17" viewBox="0 0 24 24" stroke-width="2.5" stroke="#ff2825" fill="none" stroke-linecap="round" stroke-linejoin="round">
          <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
          <line x1="18" y1="6" x2="6" y2="18" />
          <line x1="6" y1="6" x2="18" y2="18" />
          </svg>
        </div>
        <dropFile  :patient="get_client_info" @close="fetchFiles"/>
      </div>
      <!-- File select  -->


      <!-- Laboratory list result  -->
      <div :class="{'Lab_update_show_yes': lab_show_update, 'Lab_update_show_no': !lab_show_update}">
        <div class="Lab_update_show_item border-top" v-for="(item,i) in get_accept_lab_list" :key="i" @click="click_lab_result(item)">
          <div class="item_icons_time d-flex">
            <div class="w-50 times">
              <small>{{ item.date_time_reg.slice(0,10)}}
                <span class="ml-1 font-weight-bold">{{ item.date_time_reg.slice(11,16) }}</span>
              </small>
            </div>
            <div class="w-50 icons">
              <div class="icon_item text-right">
                <mdb-icon far icon="chart-bar" class="cursor_p" style="font-size: 12px;" color="info"/>
                <mdb-icon far icon="edit"  style="font-size: 12px;" class="ml-2 cursor_p" color="success"/>
              </div>
            </div>
          </div>
          <div class="d-flex lab_text_item">
            <mdb-icon icon="flask" style="font-size: 12px;" color="primary" />
            <p>{{item.analiz_name}}</p>
          </div>
        </div>
        <div 
          @click="lab_show_update = !lab_show_update"
          :class="{'Lab_update_show_yes_icon': lab_show_update, 'Lab_update_show_no_icon': !lab_show_update}">
          <mdb-icon style="color:#203146;" icon="chevron-left" />
        </div>
      </div>  
      <!-- Laboratory list result  -->


      <!-- Show entered result  -->
      <result_shov @close="result_show_close" @update="result_update_close"
        v-show="lab_picture_url_show" :picture="lab_picture_url">
      </result_shov>
      <!-- Show entered result  -->

      <Toast ref="message"></Toast>
    </div>
  </template>
  
  <script>
  import result_shov from '../../../components/analysis/result_shov.vue'
    import dropFile from '../../../components/hospital/drop.vue'
    import { mdbInput, mdbIcon } from 'mdbvue';
    import {mapActions, mapGetters, mapMutations} from 'vuex'
    import labAddResult from './labResultDynamicAdd_Id.vue'
    import labUpdateResult from './labResultDynamicUpdate_Id.vue'
  export default {
    components: {
      mdbInput,
      mdbIcon,
      labAddResult,
      dropFile,
      result_shov,
      labUpdateResult
    },
    data() {
      return {
        hostname1: this.$store.state.hostname1,
        search: '',
        searchshow: false,
        loading: false,
        admin: true,
        patient_id:null,
        patient_name:'',
        patient_born:null,
        choosenListId: [],
        choosenListName: [],
        lab_main_item: null,
        lab_main_id: null,
        patient_district: '',
        active_patient_id: null,
        fileSelect_show: false,
        analiz_id: null,
        lab_show_update: false,
        lab_picture_url: '',
        lab_picture_url_show: false,
        lab_item_data_pic: {},
        lab_result_data: null,
        lab_result_update_show: false,
        lab_pic_id: null,
      }
    },
    async mounted() {
      // this.$nextTick
      this.choosenListId = [];
      if(localStorage.finish_serv_Lab.length){
        const myArray = localStorage.finish_serv_Lab.split(",");
        console.log(myArray)
        for(let i=0; i<myArray.length; i++){
            console.log(parseInt(myArray[i]))
            let a = {
              id: parseInt(myArray[i])
            }
            this.choosenListId.push(a)
        }
        await this.fetchAll_choosenList(this.choosenListId)
      }
      

      this.patient_id = localStorage.client_id_Lab;
      this.active_patient_id = parseInt(localStorage.client_id_Lab);
      if(this.patient_id != null && this.patient_id != ''){
        await this.choosePatientID(localStorage.client_id_Lab)
      } 
    },
      computed: mapGetters(['get_service_patientId', 'get_client_info', 'get_accept_lab_list']),
    methods: {
        ...mapActions(['fetch_service_patientId','fetch_client_info', 'fetch_accept_lab_list']),
        ...mapMutations(['updatePatentResults']),
        async close_result(){
          this.lab_main_id = null;
          this.choosenListId = [];
          this.choosenListName = [];

        },
        async choosePatientID(id){
            if(this.patient_id == null || this.patient_id == ''){return;}
            localStorage.client_id_Lab = id;
            this.active_patient_id = parseInt(id);
            this.lab_main_id = null;
            this.choosenListId = [];
            this.choosenListName = [];
          try{
            this.loading = true;
            await this.fetch_client_info(id);
            console.log('this.get_client_info')
            console.log(this.get_client_info);
            this.patient_name = this.get_client_info.fio;
            this.patient_born = this.get_client_info.bornDate.slice(0,10);
            this.patient_district = this.get_client_info.districts.name;
            await this.fetch_service_patientId(id);
            this.loading = false;
          }
          catch{
            this.loading = false;
            this.$refs.message.success('not_found')
          }
        },

        async chooseService(item){
            console.log(item)
            // bu tulov qilmagan bulsa tanlab bulmasligi uchun
            // || item.finishPayment == false
            if(item.serviceType.link_str3 == null || item.serviceType.link_str3 ==''){
                return;
            }
            this.lab_main_id= null;
            this.choosenListId = [];
            this.choosenListName = [];
            for(let i = 0; i<this.get_service_patientId.length; i++){
              // bu tulov qilmagan bulsa tanlab bulmasligi uchun
              // && this.get_service_patientId[i].finishPayment
                if(item.serviceType.link_str3 == this.get_service_patientId[i].serviceType.link_str3){
                    let a = {
                      id: this.get_service_patientId[i].id,
                      link_id: this.get_service_patientId[i].serviceType.link_str3,
                      name: this.get_service_patientId[i].serviceName,
                      status: true,
                    }
                    this.choosenListName.push(a)
                }
            }
            await this.fetchMainId(parseInt(item.serviceType.link_str3));
            this.lab_main_id = parseInt(item.serviceType.link_str3);

            console.log(this.choosenListName)
            console.log(this.choosenListId)
        },
        async fetchMainId(id){
            try{
                this.loading = true;
                const res = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicmains/' + id)
                const dataArr = await res.json();
                console.log('dataArr')
                console.log(dataArr)
                this.lab_main_item = dataArr;
                this.loading = false;
            }
            catch{
                this.loading = false;
                this.$refs.message.error('not_found')
            }
        },
        async chooseItem(i){
            // console.log(item)
            this.choosenListName[i].status = !this.choosenListName[i].status;
        },
        async getActiveService(){
            let list_id = [];
            localStorage.finish_serv_Lab = [];
            for(let i=0; i<this.choosenListName.length; i++){
                if(this.choosenListName[i].status == true){
                    list_id.push(this.choosenListName[i].id)
                }
            }
            localStorage.finish_serv_Lab = list_id
        },

    async fetchAll_choosenList(list){
      const requestOptions = {
        method: "POST",
        headers : { "Content-Type" : "application/json" },
        body : JSON.stringify(list)
      };
      const response = await fetch(this.$store.state.hostname + "/Payments/finishPaymentsAllFromDoktr", requestOptions);
      const data = await response.json();
      console.log('data all')
      console.log(data)
      if(data[0].id)
      {
        this.$refs.message.success('Added_successfully');
        localStorage.finish_serv_Lab = [];
      }
      else{
        // this.modal_info = "Error server";
        // this.modal_status = true;
      }
    },
    SelectFile(){
      this.fileSelect_show = true;
    },
    async fetchFiles(list){
      console.log('list')
      console.log(list.data)
      this.fileSelect_show = false;
      this.loading = true;
        for(let i=0; i<list.data.file_names_list.length; i++){
            console.log(list.data.file_names_list[i])
            await this.send(list.data.file_names_list[i], list.file_name, list.patient)
        }
      this.loading = false;
    //   await this.getShablon(list.patient);
    },
    async send(link, file_name, patient_id){
        // console.log(this.base64)
        const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
        "patientRecipeTitle": file_name,
        "patinetRecipeStr": 'images/' + link,
        "id": 0,
        "patientsId": patient_id,
        "doctorId": localStorage.docId,
        })
        };
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + '/PatientRecipeByDoctors', requestOptions)
        const data = await response.json()
        if(data.id){
            this.$refs.message.success('Added_successfully_file');
            this.loading = false;
        }
        else{
        this.loading = false;
        return false;
        }
    },
        
        async debitPatientId(id){
          try{
            this.loading = true;
            const res = await fetch(this.$store.state.hostname + '/HospitalPatientDolg/getPaginationByPatientId?page=0&size=100&patient_id=' + id)
            const dataArr = await res.json();
            console.log('dataArr')
            console.log(dataArr)
            if(dataArr.items_list.length>0){
              this.Updatedebit(dataArr.items_list[0].real_qty);
            }
            else{
              var emptyNol = 0;
              this.Updatedebit(emptyNol);
  
            }
            this.loading = false;
  
          }
          catch{
            this.loading = false;
            this.$refs.message.success('not_found')
          }
        },
        async click_lab_update(){
          if(this.patient_id != null && this.patient_id != ''){
            await this.fetch_accept_lab_list(localStorage.client_id_Lab)
          }
          this.lab_show_update = !this.lab_show_update
        },
        async click_lab_result(data){
          console.log(data)
          this.lab_item_data_pic = data;
          this.lab_picture_url_show = true;
          this.lab_show_update = false;
          this.lab_picture_url = data.analiz_file_name;
        },
        async result_show_close(){
          this.lab_picture_url_show = false;
          this.lab_show_update = true;
        },
        async result_update_close(){
          console.log('update');
          this.lab_picture_url_show = false;
          this.lab_result_data = {};
          this.lab_main_id = null;
          this.choosenListId = [];
          this.choosenListName = [];
          await this.fetch_lab_result(parseInt(this.lab_item_data_pic.doctor_name))
          this.lab_pic_id = parseInt(this.lab_item_data_pic.id);
          this.lab_result_update_show = true;
        },
        async fetch_lab_result(id){
          try{
            this.loading = true;
            const res = await fetch(this.$store.state.hostname + '/HospitalAnalizDynamicResults/' + id)
            const data = await res.json();
            console.log(data)
            this.loading = false;
            if(res.status == 200 || res.status == 201){
              this.lab_result_data = data;
              this.$refs.message.success('Added_successfully');
            }
          }
          catch{
            this.loading = false;
            this.$refs.message.warning('not_found')
          }
        }
    },
  
  }
  </script>
  
  <style lang="scss" scoped>
    @import "@/scss/service_result.scss";
  </style>
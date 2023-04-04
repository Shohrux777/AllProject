<template>
    <div class="result_lab result_lab22">
        <div class="d-flex justify-content-center container-fluid mb-0 bg-white shadow" style="position:sticky; top:0; z-index:1;">
          <div class="w-25 px-3 ">
            <mdb-input label="ID" size="sm" class="m-0 p-0 mt-3 mb-3" v-model="patient_id" @keydown.enter.native="choosePatientID(patient_id)"/>
          </div>
          <div style="position:absolute; left: 10px; top:20px;">
            <router-link to="/doctor">
                <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-home-2" width="27" height="27" viewBox="0 0 24 24" stroke-width="1.5" stroke="#00abfb" fill="none" stroke-linecap="round" stroke-linejoin="round">
                <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                <polyline points="5 12 3 12 12 3 21 12 19 12" />
                <path d="M5 12v7a2 2 0 0 0 2 2h10a2 2 0 0 0 2 -2v-7" />
                <rect x="10" y="12" width="4" height="4" />
                </svg>
            </router-link>
          </div>
        </div>
        
      <div>
        
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
                        <p class="text_content_border">{{$t('phone')}} <span>{{get_client_info.phoneNumber}}</span></p>
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
                        <p class="text_content_border">{{$t('passportSerialNumber')}} <span>{{get_client_info.passportSerialNumber}}</span></p>
                        <p class="text_content_border">{{$t('district')}} <span>{{patient_district}}</span></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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
        <div class="container-fluid mt-3 card" v-if="choosenListName.length">
            <div class="text-center py-1 px-1" style="position:relative;">
                <small style="font-size:15px;">Kiritiladigan analizlar</small>
                <div 
                    active-class="active_link"
                    @click ="SelectFile"
                    class="text-white d-flex pl-2 px-3 border-bottom menuitem dropLink justify-content-center" 
                    style="font-size:12.6px; padding: 6px 0; background: #2196F3;"
                    >
                    <span>Выберите файл</span>
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-cloud-download ml-2" width="17" height="17" viewBox="0 0 24 24" stroke-width="2.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                        <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                        <path d="M19 18a3.5 3.5 0 0 0 0 -7h-1a5 4.5 0 0 0 -11 -2a4.6 4.4 0 0 0 -2.1 8.4" />
                        <line x1="12" y1="13" x2="12" y2="22" />
                        <polyline points="9 19 12 22 15 19" />
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
            <div class="mt-2" v-if="lab_main_id">
                <labAddResult :main_name_title = "lab_main_item" :main_id = "lab_main_id" @close="getActiveService"
                :patient_name = "patient_name" :patient_id="active_patient_id" :patient_born="patient_born"/>
            </div>
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
      <Toast ref="message"></Toast>
    </div>
  </template>
  
  <script>
    import dropFile from '../../../components/hospital/drop.vue'
    import { mdbInput,  } from 'mdbvue';
    import {mapActions, mapGetters, mapMutations} from 'vuex'
    import labAddResult from './labResultDynamicAdd_Id.vue'
  export default {
    components: {
      mdbInput, 
      labAddResult,
      dropFile
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

      }
    },
    async mounted() {
      // this.$nextTick
      this.choosenListId = []
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
      computed: mapGetters(['get_service_patientId', 'get_client_info']),
    methods: {
        ...mapActions(['fetch_service_patientId','fetch_client_info']),
        ...mapMutations(['updatePatentResults']),
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
    },
  
  }
  </script>
  
  <style>
  .result_lab22{
    min-height: 100vh;
    background-image: linear-gradient( 109.6deg,  rgba(156,252,248,1) 11.2%, rgba(110,123,251,1) 91.1% );
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    background-attachment: fixed;
    /* background-image: radial-gradient( circle farthest-corner at 1.3% 2.8%,  rgba(239,249,249,1) 0%, rgba(182,199,226,1) 100.2% ); */
  }
  .text_font{
    color:rgb(2, 47, 47);
  }
  .searchBarcodeHover:hover{
      background: rgb(233, 233, 233);
      color:rgb(84, 143, 251);
      cursor: pointer;
    }
  
  .bgorange{
    background: rgb(255, 255, 255);
    box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;
  }
  .bggreen{
    background: rgb(255, 235, 226);
  
  }
  .patient_box{
    max-height:70vh;
    overflow:hidden;
    overflow-y:scroll;
  }
  .service_item_hover:hover{
    cursor:pointer;
    color:white;
    font-weight:bold;
    background-image: radial-gradient( circle farthest-corner at 22.4% 21.7%,  rgba(4,189,228,1) 0%, rgba(2,83,185,1) 100.2% );
  }
  .bg_active{
    cursor:pointer;
    color:white;
    font-weight:bold;
    background-image: radial-gradient( circle farthest-corner at 22.4% 21.7%,  rgba(4,189,228,1) 0%, rgba(2,83,185,1) 100.2% );
  }
  .text_content_border {
        display: flex;
        justify-content: space-between;
        border-bottom: 1px dashed #ddd;
        color:rgb(117, 117, 117);
        font-style: italic;
        font-size: 13px;
    }
    .text_content_border span{
        font-weight: bold;
        color:rgb(44, 33, 83)
    }
    .dropFile{
  position: fixed;
  top:0;
  bottom:0;
  right: 0;
  left: 0;
  background: rgba(23, 23, 23, 0.7);
  display: flex;
  align-items: center;
}
.close_file{
    position: fixed;
    top: 50px;
    right: 10px;
    background: #fff;
    border-radius: 50%;
    width: 27px;
    height: 27px;
    display: flex;
    align-items: center;
    justify-content: center;
    cursor:pointer;
}
.bg_gradiunt{
  background: linear-gradient(to right, rgb(242, 112, 156), rgb(255, 148, 114));
  color:white;
}
.menuitems {
  cursor: pointer;
}
.menuitems:hover{
    background: linear-gradient(to right, rgb(247, 95, 146), rgb(255, 132, 95));
  }
.dropLink{
    position:absolute; 
    right:-10px;
    top:3px;
}
  </style>
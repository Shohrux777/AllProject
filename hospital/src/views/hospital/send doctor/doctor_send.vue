<template>
  <div class="simple_send_to_doctor">
    <div v-if="showSending" class=" shadow" :class="{'InfoIcoFixedOpenService_list': showSending, 'InfoIcoFixedService_list': !showSending}" >
      <div class="w-100 px-2 py-0" v-for="(item,i) in ServiceTypesCount" :key="i">
        <div class="d-flex justify-content-between align-items-center">
          <span class="text-white" style="font-size: 15px;">{{ i+1 }}. {{item.name}} ({{ item.price }})</span>
          <span class="text-danger" style="font-size: 18px;" @click="ServiceTypesCount.splice(i,1)">x</span>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-6"> 
        <div class="px-2 mt-2" style="position: relative;">
          <lineSelect
            :options="get_contragent_list.rows" 
            :searchshow="true"
            @select="contragent_func"
            :selected="contragent_name"
            :label="$t('contragent')"
            
          />
          <small v-if="$v.contragent_name.$dirty && contragent_id == null"  class="invalid-text" style="margin-top:-4px; margin-left:5px;" >
            {{$t('select_item')}}
          </small>
        </div>
          
        <!-- <div class="col-12">
          
        </div> -->
      </div>
      <div class="col-6 text-center border-bottom py-2">
        <h5 class="text-primary pr-3 m-0">{{patient.name}}</h5>
      </div>
    </div>
    <loader v-if="loading"/>
    <div v-else class="row allSize">
      <div class="col-6 border-right doctor_list_simple">
        <div class="doctor_list bg-white pl-2">
          <!-- <small v-if="$v.doc_name.$dirty && user_id == null" class="text-danger pt-4 pl-5" >
            {{$t('select_item')}}
          </small> -->

          <div  v-for="(doc,i) in get_doctor_list_by_casher.rows" :key="i"
            @click="show_serv(i,doc.DocAuthId,doc.doctorAuth.users.id, doc.fio, )"  
            class="item px-3" :class="{ 'activeUser' : active_el == i }"
          >
            <div class="d-flex align-items-center">
              <div class="user_photo user_photo_back" >
                <img :src="doc.doctorAuth.users.image"  style=" overflow: none; " class="img-fluid" alt="">
              </div>
              <div class="px-3 doctor_fullname">
                <p class="m-0 p-0" style="font-size:15px;">{{doc.fio}}</p>
                <p class="m-0 rang" style="font-size:10px;">{{doc.doctorAuth.users.phoneNumber}}</p>
              </div>    
            </div>
          </div>
        </div>
      </div>
      <div class="col-6 pr-0 ">
        <div class="service_type_simple">
          <mdb-input type="text" class="p-0 m-0 bg-white" size="sm"
            style="position: sticky; top:0; z-index:11;" placeholder="Поиск" ref="doc_search" v-model="search" outline/>
          <div class="row mt-2">
            <div v-for="(servic,i) in filteredList" :key="i" class="col-12">
              <div class="d-flex align-items-center">
                <div>
                  <input type="checkbox" style="width: 17px; height: 17px;" :value="servic.serviceType.id" v-model="checkedCategories[servic.serviceType.id]" :id="servic.serviceType.name" @click="check_service($event, servic.serviceType.name, servic.serviceType.price)" />
                </div>
                <div class="pb-2 px-2">
                  <label style="font-weight:500; font-size:13.5px"  :for="servic.serviceType.name" class="m-0">{{servic.serviceType.name}} ({{servic.serviceType.price}})</label>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="send_button text-right" v-show="!finish_button">
          <mdb-btn  color="primary" @click="send_doctor_from_doc" style="font-size:10px"  p="r4 l4 t2 b2">{{$t('Send')}}</mdb-btn>  
        </div>
      </div>
      
    </div>
   
    <Toast ref="message"></Toast>
    <AlertError ref="msg"></AlertError>
   <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal = "modal_status= false"/>
  </div>
</template>

<script>
import {mdbBtn, mdbInput} from 'mdbvue'
import lineSelect from "../../../components/lineSelect.vue";
import { required } from 'vuelidate/lib/validators'
import { mapActions, mapGetters } from 'vuex'
export default {
components:{
  mdbBtn, lineSelect, mdbInput
},
validations: {
  contragent_name: {required},
},
data() {
  return {
    modal_info : '',
    modal_status: false,
    loading: false,
    active_el: -1,
    auth_id: null,
    user_id: null,
    doc_name: '',
    finish_button: false,

    checkedCategories: [],  
    ServiceTypesCount: [],

    contragent_name: '',
    contragent_id: null,
    OcheredServiceList: [],
    genereted: '',
    check_ochret_status: false, 
    ocherd_list: [],
    search: '',
    filterDataList: [],
  }
},
async mounted() {
  if(localStorage.cont_name == ''){
    await this.contragent_get_not_doctor();
  }
  await this.fetch_get_doctor_list(localStorage.AuthId)
  await this.fetch_contragent()
  this.contragent_name = localStorage.cont_name;
  this.contragent_id = localStorage.cont_id;
  console.log(this.contragent_id)
},
props:{
  patient: {
    type: Object,
    default() {
      return {}
    }
  },
  patientId: {
    type: Number,
    default: null,
  },
  showSending: {
    type: Boolean,
    default: false,
  }
},
computed: { 
  ...mapGetters(['get_doctor_list_by_casher', 'get_user_service_list', 'get_contragent_list']),
  filteredList: function(){
    if(this.search)
    {
      return this.get_user_service_list.filter((item)=>{
        return this.search.toLowerCase().split(' ').every(v => item.serviceType.name.toLowerCase().includes(v))
      })
    }else
    {
      return this.get_user_service_list;
    }
  }
},
methods: {
  ...mapActions(['fetch_get_doctor_list', 'fetch_users_service_list', 'fetch_contragent']),
  async contragent_get_not_doctor(){
    try{
      const response = await fetch(this.$store.state.hostname + "/Contragents/1",);
      const data = await response.json();
      if(response.status == 200 || response.status == 201){
        localStorage.cont_name = data.name;
        localStorage.cont_id = data.id;
      }
    }
    catch(error){
      console.log(error)
    }
  },
  contragent_func(option){
  console.log(this.patient)
    this.contragent_name = option.data.name;
    this.contragent_id = option.data.id;
    localStorage.cont_name = option.data.name;
    localStorage.cont_id = option.data.id;

  },
  show_serv(i,authId, UserId, name){
    this.active_el = i
    this.auth_id = authId;
    this.user_id = UserId;
    this.doc_name = name;
    this.fetch_users_service_list(UserId)
    // this.fetch_get_patient_list_Doc_Id(authId)
    console.log(this.get_patient_list_doc_id)
    this.$refs.doc_search.focus();
  },
  check_service: function(e, serv_name, narx) {
      this.serviceCheck = false;
      if (e.target.checked){
        console.log(e.target.value)
        console.log(serv_name)
        let s = {
          id: e.target.value,
          name: serv_name,
          price: narx,
          authId: this.auth_id,
          user_Id: this.user_id,
          doc_name: this.doc_name,
          talon: 0,
        }
        this.ServiceTypesCount.push(s);
        // this.summa = 0;
        // this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
        // for(let a=0; a<this.ServiceTypesCount.length; a++){
        //   this.summa += this.ServiceTypesCount[a].price
        //   this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
        // }
      }
      else{
        // console.log(e.target.value)
        for(let i=0; i<this.ServiceTypesCount.length; i++){
          if(e.target.value == this.ServiceTypesCount[i].id){
            this.ServiceTypesCount.splice(i, 1);
            // this.summa = 0
            // this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
            // for(let a=0; a<this.ServiceTypesCount.length; a++){
            //   this.summa += this.ServiceTypesCount[a].price
            //   this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
            
            // }
            return
          }
        }
      }
      console.log(this.ServiceTypesCount);
      this.$refs.doc_search.focus();
    },

  async send_doctor_from_doc(){
  this.finish_button = true;
  console.log(this.contragent_id)
  this.OcheredServiceList = []
  this.OcheredServiceList.push(this.ServiceTypesCount[0])
  if(this.ServiceTypesCount.length == 0){
    this.finish_button = false;
    this.$refs.message.warning('please_fill');
    return false;
  }
  for(let i=0; i<this.ServiceTypesCount.length; i++){
    var sN=0
    for(let j=0; j<this.OcheredServiceList.length; j++){
      if(this.ServiceTypesCount[i].authId == this.OcheredServiceList[j].authId){
        sN++;
      }
    }
    if(sN == 0){
      this.OcheredServiceList.push(this.ServiceTypesCount[i])
    }
  }
  
  for(let k=0; k<this.OcheredServiceList.length; k++){
    await this.check_ochred(this.patient.patient_id, this.OcheredServiceList[k].user_Id, this.OcheredServiceList[k].authId, k)
  }
  console.log('this.OcheredServiceList')
  console.log(this.OcheredServiceList)
    if(this.$v.$invalid || this.contragent_id == null)
    {
      this.$v.$touch();
      this.$refs.message.warning('please_fill');
      this.finish_button = false;
      return false;
    }

    console.log(this.ServiceTypesCount)

    var jsonData = []
    for(let i=0; i<this.ServiceTypesCount.length; i++){
      var a = {
        serviceTypeId: null,
        contragentId: null,
        serviceName: '',
        patientsId: null,
        summ: 0,
        name: '',
        authorizationId: null,
        creatorAuthId: null,
        discount_card_qty: 'none',
        discount_real_qty: 0,
      }
      a.discount_card_qty = this.ServiceTypesCount[i].doc_name
      a.discount_real_qty = parseInt(this.ServiceTypesCount[i].talon)
      a.serviceTypeId = this.ServiceTypesCount[i].id;
      a.contragentId = this.contragent_id;
      a.serviceName =  this.ServiceTypesCount[i].name;
      a.patientsId =  this.patient.patient_id;
      a.summ = this.ServiceTypesCount[i].price;
      a.name = '';
      a.authorizationId = this.ServiceTypesCount[i].authId;  // this.auth_id
      a.creatorAuthId = localStorage.AuthId;
      jsonData.push(a);
    }
    console.log(jsonData)
    const requestOptions = {
      method: "POST",
      headers: { "Content-Type" : "application/json" },
      body: JSON.stringify(jsonData)
    };
    console.log(requestOptions)
    this.loading = true;
    const response = await fetch(this.$store.state.hostname + "/Payments/addPayments", requestOptions);
    const data = await response.json();
    this.loading = false;
    console.log('data')
    console.log(data)
    if(data.length){
      for(let l=0; l<this.OcheredServiceList.length; l++){
        if(this.OcheredServiceList[l].status == true){
          await this.ochred(this.patient.patient_id, this.OcheredServiceList[l].user_Id, this.OcheredServiceList[l].authId, this.OcheredServiceList[l].generete)
        }
      }
      this.ServiceTypesCount = [];
      this.checkedCategories = [];
      this.OcheredServiceList = [];
      this.finish_button = false;
      this.$emit('closed')
      this.$refs.message.success('Added_successfully')
    }
    this.finish_button = false;
    // if()
  },
  async ochred(patient_id,doc_id, auth_id, generete){
      console.log('prosto ocheredga kirdi')
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "patientsId" : patient_id,
          "ochred_name_aout_genereted": generete,
          "id" : 0,
          "usersId": doc_id,
          "authorizationId": auth_id
        })
      };
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/HospitalOchreds", requestOptions);
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          console.log(data, 'jsonini oldim')
          // let import_data = {
          //   doc_name : data.users.fio,
          //   client_name: data.patients.fio,
          //   born_date: data.patients.bornDate.slice(0,10),
          //   phone_number: data.patients.phoneNumber,
          //   number: data.ochred_name_aout_genereted
          // }
          // this.ochred_add_check(import_data)

          // this.show_otchert = true;
          // this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
          const data = await response.text();
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
      }
    },
    async fetchGenereted(doc_id){
      let newdate = new Date().toISOString().slice(0,10);
      console.log(newdate)
      let b_date  = newdate + 'T00:00:35.000Z';
      let e_date  = newdate + 'T23:59:35.000Z';
      console.log(b_date)
      console.log(e_date)
      console.log(this.user_id)
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/HospitalOchreds/getPaginationByDoktorIdAndDatesOxirgisiniOlish?user_id=" + doc_id + '&b_date=' + b_date + '&e_date='+ e_date);
        // this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          console.log(data ,  'sadasd data open Generete ga utdi')
          if(data.items_list.length == 0){
            this.genereted = '1'
          }
          else{
          console.log(data ,  'else Generete ga utdi')

            let test = parseInt(data.items_list[0].ochred_name_aout_genereted) + 1;
            this.genereted = test.toString();
            console.log(this.genereted)
          }
          // this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
          const data = await response.text();
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
      }
    },
    async check_ochred(patient_id,doc_id, auth_id, index){
      this.check_ochret_status = false;
      console.log(auth_id)
      let newdate = new Date().toISOString().slice(0,10);
      console.log(newdate)
      let b_date  = newdate + 'T00:00:35.000Z';
      let e_date  = newdate + 'T23:59:35.000Z';
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/HospitalOchreds/getPaginationByDoktorIdAndPatientIdDates?page=0&size=100&user_id=" + doc_id + '&patient_id=' + patient_id + '&b_date=' + b_date + '&e_date=' + e_date);
        // this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          console.log(data, 'jsonini ckeck ochret oldim')
          if(data.items_list.length == 0){
            console.log('add_new')
            await this.fetchGenereted(doc_id);
            this.OcheredServiceList[index].status = true;
            this.OcheredServiceList[index].generete = this.genereted;
            this.check_ochret_status = true;
          }
          else{
            this.genereted = data.items_list[0].ochred_name_aout_genereted;
            this.OcheredServiceList[index].status = false;
            this.OcheredServiceList[index].generete = this.genereted;
            console.log(data, 'check_Ochredda_else_ichiga kirdi')
            // let import_data = {
            //   doc_name : data.items_list[0].users.FIO,
            //   client_name: data.items_list[0].patients.FIO,
            //   born_date: data.items_list[0].patients.BornDate.slice(0,10),
            //   phone_number: data.items_list[0].patients.PhoneNumber,
            //   number: data.items_list[0].ochred_name_aout_genereted
            // }
            // this.ochred_add_check(import_data)

            // this.show_otchert = true;
            // this.$refs.message.success('Added_successfully')
          }
          console.log('this.genereted')
          console.log(this.genereted)
          for(let i=0; i<this.ServiceTypesCount.length; i++){
            if(this.ServiceTypesCount[i].authId == auth_id){
              this.ServiceTypesCount[i].talon = this.genereted;
            }
          }
          return true;
        }
        else{
          const data = await response.text();
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
      }
    },
    async fetch_ocherd_list(){
      let newdate = new Date().toISOString().slice(0,10);
        console.log(newdate)
        let b_date  = newdate + 'T00:00:35.000Z';
        let e_date  = newdate + 'T23:59:35.000Z';
        console.log(b_date)
        console.log(e_date)
        console.log(this.user_id)
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/HospitalOchreds/getPaginationByDoktorIdAndDates?page=0&size=1000&user_id=" + this.user_id + '&b_date=' + b_date + '&e_date='+ e_date);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            console.log(data ,  'sadasd data open')

            this.ocherd_list = data.items_list;
            
            // this.$refs.message.success('Added_successfully')
            return true;
          }
          else{
            const data = await response.text();
            this.modal_info = data;
            this.modal_status = true;
            return false;
          }
        }
        catch{
          this.loading = false;
          this.modal_info = this.$i18n.t('network_ne_connect'); 
          this.modal_status = true;
        }
    }
},
}
</script>

<style lang="scss">
.simple_send_to_doctor{
  border-left: 2px solid rgb(86, 199, 255);
  position: relative;
}
.allSize{
  width: 100%;
  height: calc(100vh - 60px);
    overflow: hidden;
    
  .doctor_list_simple{
    height: calc(100vh - 100px);
    overflow: hidden;
    overflow-y: scroll;
  }
  .service_type_simple{
    position: relative;
    height: calc(100vh - 135px);
    overflow: hidden;
    overflow-y: scroll;
  }
}
.doctor_list .item{
  .user_photo{
    background-color: #fff;
      border-radius: 50%;
      width: 45px !important;
      height: 45px !important;
      overflow: hidden;
      

    // img{
    //   border-radius: 50%;
    //   overflow: hidden;          
    // }
  }
  width:100%;
  box-shadow: 2px 2px 8px rgb(224, 224, 224), -1px -1px 2px rgb(224, 224, 224);
  height: 62px;
  margin: 5px auto;
  display: flex;
  align-items: center;
  justify-content: space-between;
  
  // background-color: #fff;
  transition: all 0.5s ease-in-out;
  .rang{
    color: rgb(66, 167, 255);
    font-weight: bold;
  }
  &:hover{
    cursor: pointer;
    box-shadow: 2px 2px 5px rgb(224, 224, 224);
    background-color: rgb(215, 242, 255);
    transform: translate(6px, 0px);
    transition: all 0.1s ease-in-out;
  }
}
.doctor_list .add_icon{
  border-radius: 50%;
  &:hover{
    box-shadow: 2px 2px 12px rgb(197, 197, 197), -1px -1px 10px rgb(224, 224, 224);
  }
}
.doctor_list .user_photo_back{
  background-image: url('../../../assets/doc_icon.jpg');
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  }
.doctor_list .activeUser{
  cursor: pointer;
  box-shadow: 2px 2px 5px rgb(224, 224, 224);
  background-color: rgb(179, 230, 255);
  transform: translate(6px, 0px);
  transition: all 0.1s ease-in-out;
}
.wrap_chip{
  box-shadow: 0.5px 0.5px 1px rgb(225, 225, 225), -1px -1px 5px rgb(224, 224, 224);
  font-size: 13.5px;
  cursor: pointer;
  &:hover{
    // box-shadow: 2px 2px 10px rgb(193, 181, 251), -1px -1px 3px rgb(224, 224, 224);
    // border-bottom: 1px solid green;
    background-color: rgb(145, 231, 255);
  }
}
.picture_background{
  background-image: url('../../../assets/doc_list_icon.jpg');
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
}
.InfoIcoFixedService_list{
  transition: 0.5s;
  position:fixed;
  z-index: 11111;
  right:-350px; 
  top:0px; 
  width: 350px; 
  height:420px; 
  cursor:pointer;
  background-image: radial-gradient( circle farthest-corner at 12.3% 19.3%,  rgba(85,88,218,1) 0%, rgba(95,209,249,1) 100.2% );
  border-top-left-radius:5px; 
  border-bottom-left-radius:5px;
}
.InfoIcoFixedOpenService_list{
  transition: 0.5s;
  position:fixed;
  z-index: 11111;
  right:50%; 
  bottom:0px; 
  width: 350px; 
  height:420px; 
  cursor:pointer;
  background-image: radial-gradient( circle farthest-corner at 12.3% 19.3%,  rgba(85,88,218,1) 0%, rgba(95,209,249,1) 100.2% );
  border-top-left-radius:5px; 
  border-bottom-left-radius:5px;
  overflow: hidden;
  overflow-y:scroll;
}
</style>
<template>
  <div class="result_lab result_lab22">
    <div class="d-flex justify-content-start container-fluid mb-4 bg-white">
       <div class="w-75 px-4" style=" position:relative;">
          <mdb-input label="Поиск" v-model="search" ref="searchPatientByResult"  @input="search_func"/>
          <div style="position:absolute; top: 70px; left:0px; right:0; z-index: 1000;" class="border patient_box shadow pt-1 " v-if="searchshow">
            <div class="bg-white " v-for="(item,i) in get_patient_list_last.rows" :key="i" >
              <div class="item searchBarcodeHover border-bottom py-1" @click="choosePatientID(item.id)">
                <div class="d-flex justify-content-between px-2">
                  <h5 class="text_font m-0 ml-1 text-primary">{{item.fio}}</h5>
                  <small style="color:red; font-size:11px;">{{item.registratedDate.slice(0,10) }} <small class="text-primary">{{item.registratedDate.slice(11,16)}}</small> </small>
                </div>
                <div class="d-flex px-2">
                  <div style="width:220px;" class="ml-1">
                    <small style=" font-size:11px;" >Тел: <span class="textdanger"> {{item.phoneNumber}}</span></small>
                  </div>
                  <div style="width:220px;">
                    <small style=" font-size:11px;" class="ml-4">Паспорт: <span class="textdanger"> {{item.passportSerialNumber}}</span></small>
                  </div>
                  <div style="width:220px;">
                    <small style=" font-size:11px;" class="ml-4">Дата рожд: <span class="textdanger"> {{item.bornDate.slice(0,10)}}</span></small>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="w-25 px-3">
          <mdb-input label="ID" v-model="patient_id" @keydown.enter.native="choosePatientID(patient_id)"/>
        </div>
      </div>
      
    <div>
      <div v-if="debitResultPatient[0].real_qty>0" class="container d-flex justify-content-center border-bottom">
        <h3 class="text-danger ">Долг: {{debitResultPatient[0].real_qty}}</h3>
      </div>
      <div class="bg-white mx-5 my-2 px-4 py-3 rounded" v-show="get_service_pay_list.length">
        <div class="TablePatientDocId mt-1  ">
          <table class="myTable px-3">
            <thead class="bg_table_header" style="position: sticky; top:-17px; z-index: 1;">
              <tr class="header">
                <th>{{$t('serviceName')}}</th>
                <th>{{$t('summ')}}</th>
                <th>{{$t('discount_persantage_qty')}}</th>
                <th>{{$t('discount_qty')}}</th>
                <th>{{$t('payedDate')}}</th>
                <th width="150">{{$t('paid')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in get_service_pay_list" :key="rowIndex" class="bg_table_tr">
                <td> <span >{{row.serviceName}}</span> </td>
                <td> <span >{{row.summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{row.discount_persantage_qty}} %</span> </td>
                <!-- <td> <span >{{row.discount_qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td> -->
                <td> <span >{{row.discount_qty}}</span> </td>
                <td> <span >{{row.payedDate.slice(0,10)}}</span> </td>
                <td>
                  <mdb-badge v-show="row.finishPayment === true" style="padding: 2px 8px; font-size: 11px;" pill color="success">{{$t("payed")}}</mdb-badge>
                  <mdb-badge v-show="row.finishPayment === false" style="padding: 2px 8px; font-size: 11px;" pill color="danger">{{$t('unpayed')}}</mdb-badge>
                </td>
                
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      
    </div>
    <loader v-if="loading" />
    <div v-else class="result_lists container-fluid d-flex">
      <div class="w-75 mx-4">
        <div class=" itemResult border p-3 mt-5 rounded bgorange " v-for="(item,i) in result_list" :key="i">
          <div class="d-flex justify-content-center border-bottom">
            <h5 style="color:#4B515D;">Дата результата: <span class=" ml-1" style="color:#FF8800;"> {{item.date_time_reg.slice(0,10)}} <span class="ml-2"></span>  {{item.date_time_reg.slice(11,19)}}</span></h5>
          </div>
          <div class="d-flex justify-content-between border-bottom">
            <h4  style="color:#4B515D;" class="m-0 py-2">Пациент: <span  style="color:#0d47a1;" class=" ml-1">{{item.patient_name}}</span></h4>
            <h5  style="color:#4B515D;" class="m-0 py-2">Врач: <span style="color:#0d47a1;" class=" ml-1">{{item.creator_doctor_name}}</span></h5>
          </div>
          <div class="d-flex justify-content-center border-bottom py-3" style="position:relative;">
            <span style="color:#757575; font-size:25px;">Анализ: <span class=" ml-1" style="color:#1b5e20;"> {{item.analiz_name}}</span></span>
            <div style="position:absolute; top:20px; right:5px;">
            <mdb-btn @click="printed(item.id)" color="primary"  class="m-0 py-2 px-3 ml-5 mt-1" style="font-size: 10px;" >Распечатать</mdb-btn>
            </div>
            <!-- :disabled="admin" -->
          </div>
          <table  class="w-100">
            <thead >
              <tr >
                <th class="px-2 border-bottom border-left">Тел</th>
                <th class="px-2 border-bottom border-left">Паспорт</th>
                <th class="px-2 border-bottom border-left border-right">Дата рождения</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td class="px-2 border-bottom border-left">{{item.patients.PhoneNumber}}</td>
                <td class="px-2 border-bottom border-left">{{item.patients.PassportSerialNumber}}</td>
                <td class="px-2 border-bottom border-left border-right">{{item.patients.BornDate.slice(0,10)}}</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      

      <!-- <h1>Xulosalar</h1> -->
      <div class="w-25 mx-4">
        <div class=" itemResult border p-3 mt-5 rounded bggreen" v-for="(list) in shablonLists" :key="list.patient_name">
          <div class="d-flex justify-content-center border-bottom">
            <!-- <h5 style="color:#4B515D;">Дата результата: <span class=" ml-1" style="color:#FF8800;"> {{list.registratedDate.slice(0,10)}} <span class="ml-2"></span>  {{list.date_time_reg.slice(11,19)}}</span></h5> -->
          </div>
          <div class="d-flex justify-content-center border-bottom">
            <h5  style="color:#4B515D;" class="m-0 py-2">Пациент: <span  style="color:#0d47a1;" class=" ml-1">{{list.patients.fio}}</span></h5>
          </div>
          <div class="d-flex justify-content-center border-bottom">
            <h5  style="color:#4B515D;" class="m-0 py-2">Врач: <span style="color:#0d47a1;" class=" ml-1">{{list.users.fio}}</span></h5>
          </div>
          <div class="d-flex justify-content-center  py-3" style="position:relative;">
            <span style="color:#757575; font-size:22px;">Ташхиc: <span class=" ml-1" style="color:#1b5e20;"> {{list.patientRecipeTitle}}</span></span>
            
          </div>
          <div class="d-flex justify-content-end border-bottom pb-2 mb-2">
              <router-link v-if="list.patinetRecipeStr.slice(0,4) == 'data'" 
                class="bg-primary text-white py-2 px-3 rounded" style="font-size:12.5px;" 
                :to="`/result_xulosa/${list.id}`">Открыть
              </router-link>

              <a v-else class="bg-primary text-white py-2 px-3 rounded" style="font-size:12.5px;"
                :href="hostname1 + list.patinetRecipeStr" target="_blank">Открыть</a>
            <!-- <mdb-btn @click="printedTash(list.id)"  color="primary" class="m-0 py-2 px-3 ml-5 mt-1" style="font-size: 10px;" >Распечатать</mdb-btn> -->
            </div>
          <table  class="w-100">
            <thead >
              <tr >
                <th class="px-2 border-bottom border-left">Тел</th>
                <!-- <th class="px-2 border-bottom border-left">Паспорт</th> -->
                <th class="px-2 border-bottom border-left border-right">Дата рождения</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td class="px-2 border-bottom border-left">{{list.patients.phoneNumber}}</td>
                <!-- <td class="px-2 border-bottom border-left">{{list.patients.passportSerialNumber}}</td> -->
                <td class="px-2 border-bottom border-left border-right">{{list.patients.bornDate.slice(0,10)}}</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      
    </div>
    <Toast ref="message"></Toast>
  </div>
</template>

<script>
  import { mdbInput, mdbBtn, mdbBadge } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'

export default {
  components: {
    mdbInput, mdbBtn, mdbBadge
  },
  data() {
    return {
      hostname1: this.$store.state.hostname1,

      search: '',
      searchshow: false,
      loading: false,
      admin: true,
      patient_id:null,
    }
  },
  async mounted() {
    // this.$nextTick
    this.$refs.searchPatientByResult.focus()
    await this.fetch_service_pay_list(0)
    if(localStorage.Type == 0 || localStorage.Type == 5){
      this.admin = false;
    }
  },
    computed: mapGetters(['get_client_list', 'get_patient_list_last', 'result_list', 'shablonLists', 'debitResultPatient', 'get_service_pay_list']),
  methods: {
      ...mapActions(['fetch_client', 'fetch_patient_list_last', 'fetch_search_patient_name', 'fetch_service_pay_list']),
      ...mapMutations(['updatePatentResults', 'UpdateshablonLists', 'Updatedebit']),
    async search_func(){
        if(this.search == ''){
          // this.fetch_patient_list_last()
          this.searchshow = false;
        }
        else{
          await this.fetch_search_patient_name(this.search);
          var list = [];
          this.updatePatentResults(list);
          this.UpdateshablonLists(list);
          let emptyNol = 0;
          this.Updatedebit(emptyNol);
          console.log(this.get_patient_list_last)
          this.searchshow = true;
        }

      },
      async choosePatientID(id){
        console.log(id)
        await this.fetch_service_pay_list(id)
        console.log('this.get_service_pay_list')
        console.log(this.get_service_pay_list)
        this.searchshow = false;
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + '/HospitalPatientAnalizHistory/getPaginationByPatient?page=0&size=500&patient_id=' + id)
          const data = await response.json();
          console.log(data)
          this.updatePatentResults(data.items_list);
          await this.debitPatientId(id);
          await this.shablonPatientId(id);
          
          this.loading = false;

        }
        catch{
          this.loading = false;
          this.$refs.message.success('not_found')
        }

      },
      printed(id){
        this.$router.push('/result_lab/' + id)
      },
      printedTash(id){
        this.$router.push('/result_xulosa/' + id)

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
      async shablonPatientId(id){
        try{
          this.loading = true;
          
          const resp = await fetch(this.$store.state.hostname + '/PatientRecipeByDoctors/getPatientRecipeByPatientId?patientid=' + id)
          const dataStr = await resp.json();
          console.log('dataStr')
          console.log(dataStr)
          this.UpdateshablonLists(dataStr);
          this.loading = false;
          

        }
        catch{
          this.loading = false;
          this.$refs.message.success('not_found')
        }
      }
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
</style>
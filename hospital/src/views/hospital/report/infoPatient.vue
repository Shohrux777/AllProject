<template>
  <div class="fullInfoPatient_send_doc bg_choose_patient_send_doc">
    <loader v-if="loading"/>
    <div class="p-4">
      <div class="mb-2 pt-2 px-4 pb-1 " style="border-radius:5px; position:relative;">
        <form @submit.prevent="submit">
          <div class="row border-bottom py-2">
            <div class="col-7">
              <lineSelect
                  class="mt-2 ml-3"
                  :options="get_patient_client_list" 
                  :searchshow="true"
                  @select="selectPatient"
                  :selected="patient_name"
                  :label="$t('patient list')"
              />
            </div>
            <div class="col-5">
              <div class="d-flex w-100 mt-2">
                <input basic class="mb-2 w-100 border rounded px-3" v-model="patientId" v-on:keyup.enter="IdFunc" style="height:32px; outline: none;" placeholder="Ид" />
              <mdb-btn color="success" @click="IdFunc" style="width: 100px; height: 32px;" size="sm" group slot="append" >Поиск</mdb-btn>
              </div>
            </div>
          </div>
        </form>
        <div class="container-fluid text-right">
          <p class="text-white font-weight-bold mt-2 mb-0" style="font-size: 22px;">Баланс: {{ (bron_dolg + moneyInfo.debit + moneyInfo.nopayed).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} сум </p>
        </div>
        <div class="row" v-if="patientInfo.fio">
          <div class="col-6  mt-2 patient_anketa">
            <div class="card px-3 py-3 patient_info">
              <p class="text_content_border">{{$t('unpayed')}} <span>{{moneyInfo.nopayed}} сум</span></p>
              <p class="text_content_border">{{$t('debit')}} услуга<span class="text-danger">{{moneyInfo.debit}} сум</span></p>
              <p class="text_content_border">Стационар долг<span>{{bron_dolg}} сум</span></p>
              <p class="text_content_border">Общий долг<span>{{bron_dolg + moneyInfo.debit + moneyInfo.nopayed}} сум</span></p>

              <p class="text_content_border">{{$t('cash')}} <span class="text-success">{{moneyInfo.cash}} сум</span></p>
                  <p class="text_content_border">{{$t('card')}} <span class="text-info">{{moneyInfo.card}} сум</span></p>

                  <p class="text_content_border">{{$t('payed')}} <span>{{moneyInfo.payed}} сум</span></p>                  
                  <!-- <p class="text_content_border">Скидка <span>0%</span></p> -->
                  
            </div>
          </div>
          <div class="col-6 mt-2 ">
            <div class="card  px-3 py-2">
              <div class=" patient_anketa">
                <div class="patient_img">
                  <img  v-if="patientInfo.polType==1" src="../../../assets/avatar4.jpg" class="img-fluid" alt="">
                  <img v-else src="../../../assets/woman1.png" class="img-fluid" alt="">

                </div>
                <div class="patient_info " >
                  <p class="text_content_border">{{$t('fio')}} <span>{{patientInfo.fio}}</span></p>
                  <p class="text_content_border">{{$t('phone')}} <span>{{patientInfo.phoneNumber}}</span></p>
                  <p class="text_content_border">{{$t('passportSerialNumber')}} <span>{{patientInfo.passportSerialNumber}}</span></p>
                  <p class="text_content_border" v-if="patientInfo.polType==1">{{$t('pol')}} <span>Мужчина</span></p>
                  <p class="text_content_border" v-else>{{$t('pol')}} <span>Женщина</span></p>
                  <p class="text_content_border">{{$t('bornDate')}} <span>{{patientInfo.bornDate.slice(0,10)}}</span></p>
                  <p class="text_content_border">{{$t('district')}} <span>{{patientInfo.districts.name}}</span></p>
                  <p class="text_content_border">{{$t('address')}} <span>{{patientInfo.address}}</span></p>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-6">
            <div class="item_list_patient_bron_info bg-white p-2 mt-2 w-100" v-for="(item, i) in bron_list" :key="i">
              <!-- {{ get_bron_list_patient_id }} -->
              <div class="d-flex w-100">
                <div style="width:40%" >
                  <div class="alert-info rounded text-center">
                    <small class="font-weight-bold">Нач:</small>
                    <small class="ml-2">{{ item.begin_date_bron.slice(0,10) }}</small>
                  </div>
                </div>
                <div style="width:40%" >
                  <div class="alert-info rounded text-center">
                    <small class="font-weight-bold">Окон:</small>
                    <small class="ml-2">{{ item.end_date_bron.slice(0,10) }}</small>
                  </div>
                </div>
                <div style="width:20%" >
                  <div class="alert-info rounded text-center">
                    <small class="font-weight-bold">День:</small>
                    <small class="ml-2">{{ item.reserved_number_1 }}</small>
                  </div>
                </div>
              </div>
              <div class="pay_need_payed_patient d-flex w-100  ">
                <div style="width:33%">
                  <div class="alert-success rounded text-center">
                    <small class="font-weight-bold">Опл:</small>
                    <small class="ml-2">{{ item.payed_summ }}</small>
                  </div>
                </div>
                <div style="width:33%">
                  <div class="alert-danger rounded text-center">
                    <small class="font-weight-bold">Неопл:</small>
                    <small class="ml-2">{{ item.need_payed_summ }}</small>
                  </div>
                </div>
                <div style="width:34%">
                  <div class="alert-primary rounded text-center">
                    <small class="font-weight-bold">День:</small>
                    <small class="ml-2">{{ item.price_for_one_day }}</small>
                  </div>
                </div>
              </div>
              <div class="text-right">
                <mdb-btn v-show="item.need_payed_summ == 0 && item.finish_payment == false"  color="primary" @click="finish_patient_payment(item)" class="my-0"  style="font-size: 9px;" p="r4 l4 t1 b1">{{$t('finish')}}</mdb-btn>  
                <mdb-btn v-show="item.payed_summ == 0"  color="danger" @click="del_patient_payment(item)" class="my-0"  style="font-size: 9px;" p="r4 l4 t1 b1">{{$t('Delete')}}</mdb-btn>  
              </div>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-12 mt-4" v-for="(item,i) in doctorPaymentInfo" :key="i">
            <div class="card px-3 py-2">
              <div class="d-flex justify-content-between border_dashed">
                <h6 class="pb-3 pt-2 m-0 text-primary">{{item.doctor}}</h6>
                <h6 class="pb-3 pt-2 m-0 text-primary">{{item.doctorPhone}}</h6>
              </div>
              <table class="myTableFullInfo_send_doc_info">
                <thead>
                  <tr class="header ">
                    <th  width="40" class="text-left">№</th>
                    <th width="200">{{$t('patient_name')}}</th>
                    <th>{{$t('contragent_name')}}</th>
                    <th>{{$t('service_name')}}</th>
                    <th >{{$t('service_price')}}</th>
                    <th >{{$t('paymentInCash')}}</th>
                    <th >{{$t('paymentInCard')}}</th>
                    <th >{{$t('regisdate')}}</th>
                    <th >{{$t('payed')}}</th>
                    <th >{{$t('finish')}}</th>
                    <th class="text-center">{{$t('change_doctor')}}</th>

                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(row,rowIndex) in item.item" :key="rowIndex">
                    <td> <span >{{rowIndex+1}}</span> </td>
                    <td> <span >{{row.patients.FIO}}</span> </td>
                    <td> <span >{{row.contragent.Name}}</span> </td>
                    <td> <span >{{row.ServiceName}}</span> </td>
                    <td> <span >{{row.Summ}}</span> </td>
                    <td> <span :class="{'text-danger': row.PaymentInCash == 0, 'text-success': row.PaymentInCash != 0}">{{row.PaymentInCash}}</span> </td>
                    <td> <span :class="{'text-danger': row.PaymentInCard == 0, 'text-success': row.PaymentInCard != 0}">{{row.PaymentInCard}}</span> </td>
                    <td> <p class="m-0">{{row.RegistratedDate.slice(0,10)}}</p> <p class="m-0">{{row.RegistratedDate.slice(11,19)}}</p> </td>

                    <td>
                      <mdb-badge v-show="row.FinishPayment === true" style="padding: 2px 8px;" pill color="success">{{$t("payed")}}</mdb-badge>
                      <mdb-badge v-show="row.FinishPayment === false" style="padding: 2px 8px;" pill color="danger">{{$t('unpayed')}}</mdb-badge>
                    </td>
                    <td>
                      <mdb-badge v-show="row.Finish === true" style="padding: 2px 8px;" pill color="success">{{$t("проверил")}}</mdb-badge>
                      <mdb-badge v-show="row.Finish === false" style="padding: 2px 8px;" pill color="danger">{{$t('непроверенный')}}</mdb-badge>
                    </td>
                    <td v-show="row.Finish === false" class="text-center" style="cursor:pointer;"  @click="change_doctor(row)">
                      <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-replace" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#00abfb" fill="none" stroke-linecap="round" stroke-linejoin="round">
                        <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                        <rect x="3" y="3" width="6" height="6" rx="1" />
                        <rect x="15" y="15" width="6" height="6" rx="1" />
                        <path d="M21 11v-3a2 2 0 0 0 -2 -2h-6l3 3m0 -6l-3 3" />
                        <path d="M3 13v3a2 2 0 0 0 2 2h6l-3 -3m0 6l3 -3" />
                      </svg>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>

        
      </div>
    </div>

    <mdb-modal  :show="change_doc_patient_id"  @close="change_doc_patient_id = false" size="md" info>
      <mdb-modal-header>
        <mdb-modal-title style="font-weight: 500;">{{$t('change_doctor')}}</mdb-modal-title>
      </mdb-modal-header>
      <mdb-modal-body>
        <div class="d-flex justify-content-center">
          <div style="position: relative; margin-top: 0px;" class="w-100"> 
            <lineSelectAuth
            :options="auth_user_list"
            :searchshow="true"
            @select="selectcontragent"
            :selected="contragent_name"
            :label="$t('contragent')"
            />
          </div>
        </div>
        <div class="px-3 row mt-2 text-center">
          <p class="my-2 text-primary">{{this.patient_data.patients.FIO}}</p>
          <p class="my-2 text-danger">{{this.patient_data.ServiceName}}</p>
        </div>
        <div class="d-flex justify-content-end pt-2 border-top mt-3">
          <mdb-btn color="info" @click="change_func" size="sm">{{$t('change_doctor')}}</mdb-btn>
        </div>
      </mdb-modal-body>
    </mdb-modal>

    
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>
  // import VueHtml2pdf from 'vue-html2pdf'
  import lineSelectAuth from "../../../components/lineSelectAuth.vue";

  import lineSelect from "../../../components/hospital/cashUserSelectForAnalysis.vue";
  // import DatePicker from 'vue2-datepicker';
  // import districtAdd from "../../../components/new_prog_add/district_add"
  import {  mdbBadge, mdbBtn, mdbModal, mdbModalBody, mdbModalHeader,mdbModalTitle} from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBadge, lineSelectAuth,
      lineSelect,
      mdbBtn, mdbModalHeader, mdbModal, mdbModalBody,mdbModalTitle
    },
    data(){
      return{
        show: false,
        admin:false,
        snipper: true,
        loading:false,
        editData: {},
        modal_info : '',
        modal_status: false,
        contragent_name: '',
        contragent_id: 0,
        Start_time: null,
        End_time: null,
        patient_name: '',
        patient_id: null,
        get_payment_list: [],
        index: null,
        service_id: null,
        day: '',
        qty: 0,
        summ: 0,
        card: 0,
        cash: 0,
        patientId: null,


        patientInfo: {},
        moneyInfo: {
          cash:0,
          card: 0,
          debit: 0,
          payed:0,
          nopayed:0,
          summ:0,
        },
        doctorPaymentInfo: [
          
        ],
        change_doc_patient_id: false,
        patient_data: {patients:{FIO:''}},
        dolg_list: [],
        bron_list: [],
        bron_dolg: 0,
      }
    },
    async mounted(){
      if(localStorage.Type == 0){
        this.admin = true;
      }
      this.fetch_patient_client();
      this.fetch_service_group();
      await this.fetch_auth_list();
    },
    computed: mapGetters(['get_client_info','get_contragent_list', 'get_report_by_data_time',
       'get_report_by_time_card_cash', 'get_service_group_list', 'get_patient_client_list', 'auth_user_list']),
    methods: {
      ...mapActions(['fetch_contragent', 'fetch_report_by_data_time', 'fetch_service_group', 'fetch_patient_client', 
        'fetch_client_info', 'fetch_auth_list']),
      ...mapMutations(['district_row_delete']),

      async finish_patient_payment(item){
        try{
          const res = await fetch(this.$store.state.hostname + '/HospitalBronRoomPayments/finishPayment?id=' + item.id);
          const res_data = await res.json();
          console.log('res_data')
          console.log(res_data)
          if(res_data.id){
              this.$refs.message.success('Successfully_added')
              await this.fetchPatientIdBron();
          }
        }
        catch(error){
          console.log(error)
        }
        
      },
      async del_patient_payment(item){
        const requestOptions = {
              method : "delete",
            };
            const response = await fetch(this.$store.state.hostname + "/HospitalBronRoomPayments/" + item.id, requestOptions);
            const data = await response.json();
            // console.log(data)
            if(data.id)
            {
              this.$refs.message.success('Successfully_removed')
              await this.fetchPatientIdBron();
            }
      },
      print(){
        this.$refs.listlar.generatePdf()
      },
      change_doctor(data){
        console.log(data)
        this.patient_data = data;
        this.change_doc_patient_id = true;
      },
      selectcontragent(option){
        console.log(option)
        this.contragent_name = option.data.users.fio;
        this.contragent_id = option.data.id;
      },
      async change_func(){
        try{
          const response = await fetch(this.$store.state.hostname + "/Payments/updateDoktorPatientAndSaveUpdater?payment_id=" +  this.patient_data.Id + "&doctor_auth_id=" + this.contragent_id + "&updater_auth_id=" + localStorage.AuthId);
          const data = await response.json();
          console.log(data)
          if(data.id){
            this.$refs.message.success('Added_successfully')
            this.IdFunc();
            this.change_doc_patient_id = false;
          }
        }
        catch{
          console.log('Oshibka')
        }

      },
     


      async selectPatient(option){
        this.loading = true;
        console.log(option)
        this.patientInfo = option.data
        this.patient_name = option.data.fio
        this.patient_id = option.data.id
        this.patientId = option.data.id
        this.moneyInfo = {
          cash:0,
          card: 0,
          debit: 0,
          payed:0,
          nopayed:0,
          summ:0,
        }

        try{
          const response = await fetch(this.$store.state.hostname + "/HospitalPatientDolgPaymentInfoes/getPaginationByPatientId?page=0&size=1000&patient_id=" +  this.patient_id);
          const data = await response.json();
          console.log('debit')
            console.log(data)
          if(response.status == 200 || response.status == 201)
          {
            this.dolg_list = data.items_list;
            this.moneyInfo.debit = data.items_list.reduce((total, item) => {
                return total + parseInt(item.dolg_summ)
            }, 0);
          }
          else{
            this.moneyInfo.debit = 0
            this.modal_info = data.detail + "    (" + data.routine + ")";
            this.modal_status = true;
          }
        }
        catch{
          this.moneyInfo.debit = 0
          console.log("Ошибка долг");
        }

        const response = await fetch(this.$store.state.hostname + "/Payments/getPaginationByPatientId?page=0&size=400&patient_id=" + option.data.id);
        const data = await response.json();
        console.log('payment')
        var test = data
        console.log(test.items_list)

        this.get_payment_list = data.items_list;

        this.moneyInfo.summ = 0;
        this.moneyInfo.cash = 0;
        this.moneyInfo.card = 0;
        for(let i = 0; i < this.get_payment_list.length; i++) {
          this.moneyInfo.summ += this.get_payment_list[i].Summ
          this.moneyInfo.card += this.get_payment_list[i].PaymentInCard
          this.moneyInfo.cash += this.get_payment_list[i].PaymentInCash
          if(this.get_payment_list[i].FinishPayment == false){
            this.moneyInfo.nopayed += this.get_payment_list[i].Summ
          }
          else{
            this.moneyInfo.payed += (this.get_payment_list[i].PaymentInCard + this.get_payment_list[i].PaymentInCash);
          }
          // this.summa += this.get_report_by_data[i].summa;
        }
        console.log(this.moneyInfo)

        this.doctorPaymentInfo = []
        for(let i=0; i<this.get_payment_list.length; i++){
          let newItem = {
            doctor: '',
            doctorId: null,
            doctorPhone: '',
            item: []
          }
          this.doctorPaymentInfo.push(newItem);
          this.doctorPaymentInfo[i].doctor = this.get_payment_list[i].authorization.users.FIO;
          this.doctorPaymentInfo[i].doctorId = this.get_payment_list[i].authorization.users.Id;
          this.doctorPaymentInfo[i].doctorPhone = this.get_payment_list[i].authorization.users.PhoneNumber;
          this.doctorPaymentInfo[i].item.push(this.get_payment_list[i]);

          for(let j=i+1; j<this.get_payment_list.length; j++){
            if(this.get_payment_list[i].AuthorizationId == this.get_payment_list[j].AuthorizationId){
              this.doctorPaymentInfo[i].item.push(this.get_payment_list[j]);
              this.get_payment_list.splice(j,1)
              j--;
            }
          }
        }
        await this.fetchPatientIdBron();
        this.loading = false;
        console.log(this.doctorPaymentInfo)
      },
      async fetchPatientIdBron(){
        try{
          const response = await fetch(this.$store.state.hostname + "/HospitalBronRoomPayments/getPaginationByPatientIdYangi?page=0&size=100&patient_id=" +  this.patient_id);
          const data = await response.json();
          console.log('debit')
            console.log(data)
          if(response.status == 200 || response.status == 201)
          {
            this.bron_list = data.items_list;
            this.bron_dolg = data.items_list.reduce((total, item) => {
                return total + parseInt(item.need_payed_summ)
            }, 0);
            this.moneyInfo.cash += data.items_list.reduce((total, item) => {
                return total + parseInt(item.cash_summ)
            }, 0);
            this.moneyInfo.card += data.items_list.reduce((total, item) => {
                return total + parseInt(item.card_summ)
            }, 0);
            this.moneyInfo.summ += data.items_list.reduce((total, item) => {
                return total + parseInt(item.payed_summ)
            }, 0);
          }
          else{
            this.bron_dolg = 0
            this.modal_info = data.detail + "    (" + data.routine + ")";
            this.modal_status = true;
          }
        }
        catch{
          this.bron_dolg = 0
          console.log("Ошибка долг");
        }
      },
      async IdFunc(){
        console.log('this.patientId')
        console.log(this.patientId)
        await this.fetch_client_info(this.patientId);
        this.patient_id = this.patientId;
        this.patientInfo = this.get_client_info
        this.patient_name = this.get_client_info.fio;
        await this.selectPatient({data:this.get_client_info})
        // this.moneyInfo = {
        //   cash:0,
        //   card: 0,
        //   debit: 0,
        //   payed:0,
        //   nopayed:0,
        //   summ:0,
        // }
        // try{
        //   const response = await fetch(this.$store.state.hostname + "/HospitalPatientDolg/getPaginationByPatientId?page=0&size=100&patient_id=" +  this.patient_id);
        //   const data = await response.json();
        //   if(data.items_list[0].qty)
        //   {
            
        //     // this.alert_text = localizeFilter('Successfully_removed');
        //     // this.$refs.message.success('Added_successfully')
        //     this.moneyInfo.debit = data.items_list[0].real_qty;
        //   }
        //   else{
        //     this.moneyInfo.debit = 0
        //     this.modal_info = data.detail + "    (" + data.routine + ")";
        //     this.modal_status = true;
        //   }
        // }
        // catch{
        //   this.moneyInfo.debit = 0
        //   console.log("Ошибка долг");
        // }

        // const response = await fetch(this.$store.state.hostname + "/Payments/getPaginationByPatientId?page=0&size=400&patient_id=" + this.patientId);
        // const data = await response.json();
        // console.log('payment')
        // console.log(data)
        // var test = data
        // console.log(test.items_list)

        // this.get_payment_list = data.items_list;

        // this.moneyInfo.summ = 0;
        // this.moneyInfo.cash = 0;
        // this.moneyInfo.card = 0;
        // for(let i = 0; i < this.get_payment_list.length; i++) {
        //   this.moneyInfo.summ += this.get_payment_list[i].Summ
        //   this.moneyInfo.card += this.get_payment_list[i].PaymentInCard
        //   this.moneyInfo.cash += this.get_payment_list[i].PaymentInCash
        //   if(this.get_payment_list[i].FinishPayment == false){
        //     this.moneyInfo.nopayed += this.get_payment_list[i].Summ
        //   }
        //   else{
        //     this.moneyInfo.payed += (this.get_payment_list[i].PaymentInCard + this.get_payment_list[i].PaymentInCash);
        //   }
        //   // this.summa += this.get_report_by_data[i].summa;
        // }
        // console.log(this.moneyInfo)

        // this.doctorPaymentInfo = []
        // for(let i=0; i<this.get_payment_list.length; i++){
        //   let newItem = {
        //     doctor: '',
        //     doctorId: null,
        //     doctorPhone: '',
        //     item: []
        //   }
        //   this.doctorPaymentInfo.push(newItem);
        //   this.doctorPaymentInfo[i].doctor = this.get_payment_list[i].authorization.users.FIO;
        //   this.doctorPaymentInfo[i].doctorId = this.get_payment_list[i].authorization.users.Id;
        //   this.doctorPaymentInfo[i].doctorPhone = this.get_payment_list[i].authorization.users.PhoneNumber;
        //   this.doctorPaymentInfo[i].item.push(this.get_payment_list[i]);

        //   for(let j=i+1; j<this.get_payment_list.length; j++){
        //     if(this.get_payment_list[i].AuthorizationId == this.get_payment_list[j].AuthorizationId){
        //       this.doctorPaymentInfo[i].item.push(this.get_payment_list[j]);
        //       this.get_payment_list.splice(j,1)
        //       j--;
        //     }
        //   }
        // }

        // this.loading = false;
        // console.log(this.doctorPaymentInfo)
      }
    },
  };
</script>

<style lang="scss">
.fullInfoPatient_send_doc{
  min-height: 100vh;
  background: rgb(248, 248, 248);
}
.search_item{
  position: relative;
}
.search_title{
  position: absolute;
  top: -25px;
  left: 35px;
  color:orange
}

.add{
  position: fixed;
  background: rgba(0, 0, 0, 0.4);
  height: 100vh;
  top:0;
  width:85%;
}

.addxizmat{
  width: 470px;
  // height: 120px;
  background: #fff;
  position: relative;
  z-index: 5000;
}
.showing{
  display: none;
}
.timePicer{
  position: relative;
  margin-top: -10px;
  .timeLabel{
    position: absolute;
    font-size: 12px;
    background-color: #fff;
    padding: 1px 3px;
    z-index: 1;
    left: 6px;
    top: -1px;
  }
  .dayLabel{
    position: absolute;
    font-size: 12px;
    background-color: #fff;
    padding: 0px 3px;
    z-index: 1;
    left: 6px;
    top: -8px;
  }
}
  .myTableFullInfo_send_doc_info {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; overflow-x:auto
}
.myTableFullInfo_send_doc_info th{
  font-weight: 600;
  font-size:11px;
}
.myTableFullInfo_send_doc_info td{
  font-size:11.5px;
}
.myTableFullInfo_send_doc_info th, .myTableFullInfo_send_doc_info td {
  text-align: left;
  padding: 8px;
}

.myTableFullInfo_send_doc_info tr {
  border-bottom: 1px dashed rgb(240, 240, 240);
}

.myTableFullInfo_send_doc_info tr.header, .myTableFullInfo_send_doc_info tr:hover {
  // background-color: #f1f1f1;
}
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}



.patient_anketa{
  display: flex;
  width: 100%;
  align-items: center;
  

  .patient_img{
    width: 200px;
  }
  .patient_info{
    width: 100%;
    padding: 10px 15px 10px 25px;

    .text_content_border{
      display: flex;
      justify-content: space-between;
      border-bottom: 1px dashed #ddd;
      color:rgb(117, 117, 117);
      font-style: italic;
      font-size: 13px;
      span{
        font-weight: bold;
        color:rgb(44, 33, 83)
      }
    }
  }
}
.border_dashed{
  border-bottom: 1px dashed #ddd;
}
.bg_choose_patient_send_doc{
  background-image: linear-gradient( 109.6deg,  rgba(156,252,248,1) 11.2%, rgba(110,123,251,1) 91.1% );
}
.item_list_patient_bron_info{
  box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
}
</style>
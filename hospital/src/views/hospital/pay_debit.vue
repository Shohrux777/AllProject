<template>
  <div class="bg-white">
   <loader v-if="loading"/>
    <div class="p-4">
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
        <form @submit.prevent="submit">
          <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
            <div class="title w-75 row align-items-center">
              <div class="col-4 m-0 pr-0">
                <mdb-input type="text" v-model="search" @input="submit" size="sm" :label="$t('search_here')" outline></mdb-input>
              </div>
              <div class="col-1 m-0 pl-0">
                <mdb-btn type="submit" color="primary py-2 px-4"  style="font-size:10px;" >
                  {{$t('search')}}
                </mdb-btn>
              </div>
            </div>
            <div class="plus">
              <mdb-btn @click="$router.back()" color="primary py-2 px-4"  style="font-size:10px;" >
                {{$t('back')}}
              </mdb-btn>
            </div>
          </div>
        </form>
        <div class="newTableDebit">
          <div class="TablePatientDocId p-3">
            <table class="myTabledebit">
              <thead>
                <tr class="header ">
                  <th  width="40" class="text-left">№</th>
                  <th width="200">{{$t('patient_name')}}</th>
                  <th >{{$t('id')}}</th>
                  <th>{{$t('phone')}}</th>
                  <th>{{$t('debit')}}</th>
                  <th  width="160">{{$t('Action')}}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,rowIndex) in filteredList" :key="rowIndex">
                  <td> <small style="font-size: 12.5px;">{{rowIndex+1}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.fio}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.patient_id}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.phone}}</small> </td>
                  <td> <small style="font-size: 12.5px;" class="text-danger">{{row.dolg.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                  <td  class="text-center d-flex align-items-center">
                      <i class="fas fa-eye  mask waves-effect m-0 mr-2 pl-2 text-warning" v-on:click="showPatientIdDebitPayment(row.patient_id)" :data-row="rowIndex"></i>
                      <mdb-btn @click="payedDebitPayment(row)" color="primary py-1 px-3"  style="font-size:10px;" >
                        {{$t('pay')}}
                      </mdb-btn>
                  </td>
                </tr>
                
              </tbody>
            </table>
          </div>
        </div>
        <div class="TablePatientDocIds p-1 m-0">
          <anyTable
          :datasource="m_contragent"
          @pay="pay_debit"
          @showed = "showed"
          :debit="debit"
          />
        </div>
      </div>
    </div>


    <!-- <div :class="{'showing':show}">
      <div class="add d-flex justify-content-center align-items-center" >  
        <districtAdd/>
      </div>
    </div> -->
      <mdb-modal  :show="pay_debit_show"  @close="pay_debit_show = false" size="md"  light>
        <mdb-modal-header>
          <mdb-modal-title style="font-weight:  500;">Долг платить</mdb-modal-title>
        </mdb-modal-header>
        <mdb-modal-body>
          <div class="container-fluid">
            <div class="row">
              <div class="col-12 text-center">
                <span class="text-primary" style="font-size: 22px;">{{ dolg_info_patient.fio }}</span>
              </div>
              <div class="col-12 text-center mb-2" v-if="dolg_info_patient.dolg">
                <span class="text-danger" style="font-size: 18px;">{{ dolg_info_patient.dolg.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span>
              </div>
              <div class="col-12">
                <mdb-input type="number" class="m-0 p-0" v-model="debit_pay_cash" size="md" :label="$t('cash')" outline></mdb-input>
              </div>
              <div class="col-12 mt-3">
                <mdb-input type="number" class="m-0 p-0" v-model="debit_pay_card" size="md" :label="$t('card')" outline></mdb-input>
              </div>
              <div class="col-12 mt-2 pt-1 text-right border-top">
                <mdb-btn @click="payedDebit_summa" color="success py-2 px-5"  style="font-size:10px;" >
                  {{$t('pay')}}
                </mdb-btn>  
              </div>
            </div>
          </div>
        </mdb-modal-body>
      </mdb-modal>
      
      <ModalUser  :show="show" headerbackColor="danger" closeColor="white" titlecolor="white" 
        :title="$t('debit')" @close="show = false" width="80%">
        <template v-slot:body>
          <div class="TablePatientDocId p-3">
            <table class="myTabledebit">
              <thead>
                <tr class="header ">
                  <th  width="40" class="text-left">№</th>
                  <th width="200">{{$t('patient_name')}}</th>
                  <th >{{$t('service_name')}}</th>
                  <th >{{$t('doctor_name')}}</th>
                  <th >{{$t('summa')}}</th>
                  <th>{{$t('payed')}}</th>
                  <th>{{$t('debit')}}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,rowIndex) in debit_list_patient_id" :key="rowIndex">
                  <td> <small style="font-size: 12.5px;">{{rowIndex+1}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.patient_name}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.service_name}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.payments.discount_card_qty}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.payments.Summ}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.payments.PaymentInCash + row.payments.PaymentInCard}}</small> </td>
                  <td> <small style="font-size: 12.5px;" class="text-danger">{{row.payments.dolg_summ}}</small> </td>
                </tr>
              </tbody>
            </table>
          </div>
        </template>
      </ModalUser>
       <ModalUser  :show="debit_show" headerbackColor="success" closeColor="white" titlecolor="white" 
        :title="$t('debit')" @close="debit_show = false" width="500px">
        <template v-slot:body>
          <debit :option="summa" :summa="summaString" :patientName ="patientName" :patient="patient_id" @close="close_paydebit" />
        </template>
      </ModalUser>
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>
  import anyTable from "../../components/ContragentTable"
import { required } from 'vuelidate/lib/validators'
import debit from "./payedDebit.vue";
import ModalUser from '../../components/modal.vue'

  // import lineSelect from "../../components/lineSelect.vue";
  // import districtAdd from "../../components/new_prog_add/district_add"
  import { mdbBtn, mdbInput,  mdbModal, mdbModalHeader,  mdbModalTitle, mdbModalBody,   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, 
      mdbInput,anyTable, ModalUser, debit
    },
    data(){
      return{
        debit: true,
        debit_show: false,
        patient_id: null,
        summa: 0,
        summaString: '',
        patientName: '',
        search:'',

        show: false,
        snipper: true,
        editData: {},
        modal_info : '',
        modal_status: false,
        contragent_name: '',
        contragent_id: null,
        Start_time: new Date(),
        End_time: new Date(),
        price: null,
        reason: '',
        loading: false,
        bonus_contragent_list: [],
        showen_Auth: false,
        m_contragent :{
          rows: [],
          columns: ['patient_name', 'real_qty', 'created_date_time'],
          col : []
        },
        debit_new_list: [],
        debit_list_patient_id: [],
        pay_debit_show: false,
        debit_pay_cash: 0,
        debit_pay_card: 0,
        dolg_info_patient: {},
      }
    },
    validations: {
      price: {required},
      contragent_name : {required}
    },
    async mounted(){
      this.refresh();
      await this.newRefresh();
      if(localStorage.AuthId === 0){
        this.showen_Auth = true
        console.log(localStorage.Auth)
      } 
        this.fetch_contragent()
    },
    computed: {
      ...mapGetters(['get_contragent_list', 'get_report_by_data_time', 
        'get_report_by_time_card_cash','get_pagination']),
      filteredList: function () {
      if (this.search) {
        return this.debit_new_list.filter((item) => {
          return this.search.toLowerCase().split(' ').every(v => item.fio.toLowerCase().includes(v))
        })
      } else {
        return this.debit_new_list
      }
    }
    },
    methods: {
      ...mapActions(['fetch_contragent', 'fetch_report_by_data_time']),
      ...mapMutations(['district_row_delete', 'update_pagination_first']),
      async showPatientIdDebitPayment(id){
        try{
          const res = await fetch(this.$store.state.hostname + '/HospitalPatientDolgPaymentInfoes/getPaginationByPatientId?page=0&size=100&patient_id=' + id);
          const res_data = await res.json();
          console.log('res_data_patient_id_list')
          console.log(res_data.items_list)
          this.debit_list_patient_id = res_data.items_list;
          this.show = true;
        }
        catch{
          console.log('server error')
        }
      },
      async payedDebitPayment(data){
        this.dolg_info_patient = data;
        this.pay_debit_show = true;
      },
      async payedDebit_summa(){
        if(this.debit_pay_cash == null || this.debit_pay_cash == ''){
          this.debit_pay_cash = 0;
        }
        if(this.debit_pay_card == null || this.debit_pay_card == ''){
          this.debit_pay_card = 0;
        }
        if((parseInt(this.debit_pay_cash) + parseInt(this.debit_pay_card))>parseInt(this.dolg_info_patient.dolg) || (parseInt(this.debit_pay_cash) + parseInt(this.debit_pay_card)) == 0){
          console.log('return')
          return;
        }
        else{
          try{
            const respon = await fetch(this.$store.state.hostname + '/HospitalPatientDolgPaymentInfoes/getPaginationByPatientIdQarzdorlikTolash?page=0&size=1000&patient_id=' + this.dolg_info_patient.patient_id + '&cash=' + this.debit_pay_cash + '&card=' + this.debit_pay_card)
            const data = await respon.json()
            if(respon.status == 200 || respon.status == 201){
              console.log(data)
              this.pay_debit_show = false;
              await this.newRefresh();
              this.search = '1';
              this.search = '';
              this.debit_pay_card = 0;
              this.debit_pay_cash = 0;
            }
          }
          catch(error){
            console.log(error)
          }
        }
      },
      async newRefresh(){
        try{
          const res = await fetch(this.$store.state.hostname + '/HospitalPatientDolgPaymentInfoes/getPaginationQarzdorlik?page=0&size=1000');
          const res_data = await res.json();
          console.log('res_data')
          console.log(res_data.items_list)
          this.debit_new_list = res_data.items_list;
        }
        catch{
          console.log('server error')
        }
      },
      cls_wnd(){
        this.price =  null;
        this.reason =  '';
        this.contragent_name = '';
        this.contragent_id = null;
      },
      async close_paydebit(){
        this.debit_show = false;
        await this.$router.push('/checkDebit')
        this.$root.$refs.checkDebit.printed()
      },
      
      async refresh(){
        const res = await fetch(this.$store.state.hostname + '/HospitalPatientDolg/getPagination?page=0&size=1000');
        const res_data = await res.json();
        console.log('res_data_old_dolg')
        this.m_contragent.rows = [];
        console.log(res_data)
        for(let i=0; i<res_data.items_list.length; i++){
          if(new Date(res_data.items_list[i].created_date_time) < new Date('2023-09-24T17:16:08.157324')){
            this.m_contragent.rows.push(res_data.items_list[i]);
          }
        }
        this.loading = false;
      },
      pay_debit(option){
        console.log('option')
        console.log(option)
        this.patient_id = option.id;
        this.summa = option.real_qty;
        this.patientName = option.patient_name;
        this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
        this.debit_show = true;
      },
      showed(id){
        this.$router.push('/debitdetiels/' +id)
      },

      add(){
        this.show =! this.show
        this.editData = {};
      },
      print(){
        this.$refs.listlar.generatePdf()
      },
      async deleteRow(index,del_data){
          const requestOptions = {
            method : "delete",
          };
          const response = await fetch(this.$store.state.hostname + "/ContragentAdditionalPaymentBefohands/" + del_data.id, requestOptions);
          const data = await response.json();
          this.bonus_contragent_list.splice(parseInt(index), 1);
          if(data)
          {
            // this.alert_text = localizeFilter('Successfully_removed');
            this.$refs.message.error('Successfully_removed')
            // this.m_product_row_delete(index);
          }
          else{
            this.modal_info = data.detail + "    (" + data.routine + ")";
            this.modal_status = true;
          }
      },
      async submit(){
        if(this.search == ''){
          this.refresh();
        }else{
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + '/HospitalPatientDolg/getPaginationByPatientFio?page=0&size=100&patient_name=' + this.search)
            const data = await response.json()
            this.loading = false;
            console.log(data)
            if(data.items_count){
              this.m_contragent.rows = [];
              console.log(data)
              for(let i=0; i<data.items_list.length; i++){
                if(new Date(data.items_list[i].created_date_time) < new Date('2023-09-24T17:16:08.157324')){
                  this.m_contragent.rows.push(data.items_list[i]);
                }
              }
              this.$refs.message.success('Added_successfully')
            }
            else{
              this.$refs.message.error('not_found_patient')
              this.m_contragent.rows = [];
              // this.modal_info = data.detail + "    (" + data.routine + ")";
              // this.modal_status = true;
              return false;
            } 
          }
          catch{
            this.$refs.message.error('not_found_patient')
              this.m_contragent.rows = [];
          }
          
        }
        
        
        
      },

    },
  };
</script>

<style scoped>
@import "../../scss/tableAll.scss";
</style>
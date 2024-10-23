<template>
  <div class="bg-white">
    <loader v-if="loading"/>
    <div class="p-4">
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
        <form @submit.prevent="submit">
          <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
            <div class="title w-75 row align-items-center">
               <h5 class="m-0">{{ $t('debit') }} лист</h5>
            </div>
            <div class="plus">
              <mdb-btn @click="print" color="info py-2 px-4"  style="font-size:10px;" >
                {{$t('print')}}
              </mdb-btn>
              <mdb-btn type="submit" v-show="false" color="primary py-2 px-4" style="font-size:10px;"  >
                {{$t('apply')}}
              </mdb-btn>
            </div>
          </div>
        </form>
        <div class="TablePatientDocId p-3">
          <table class="myTable">
            <thead>
              <tr class="header ">
                <th  width="40" class="text-left">№</th>
                <th width="200">{{$t('patient_name')}}</th>
                <th>{{$t('id')}}</th>
                <th>{{$t('phoneNumber')}}</th>
                <th >{{$t('summ_dolg')}}</th>
                <th width="120">{{$t('Action')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in get_payment_list" :key="rowIndex">
                <td> <span >{{rowIndex+1}}</span> </td>
                <td> <span >{{row.fio}}</span> </td>
                <td> <span >{{row.patient_id}}</span> </td>
                <td> <span >{{row.phone}}</span> </td>
                <td> <span >{{row.dolg}}</span> </td>
                <td> <mdb-btn @click="showPatientIdDebitPayment(row.patient_id)" color="info py-1 px-4"  style="font-size:10px;" >
                {{$t('details')}}
              </mdb-btn></td>
              </tr>
              <tr >
                
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>

     <vue-html2pdf ref='listlar'
        :show-layout="false"
        :float-layout="true"
        :enable-download="false"
        :preview-modal="true"
        :paginate-elements-by-height="1600"
        filename="hee hee"
        :pdf-quality="2"
        :manual-pagination="false"
        pdf-format="a4"
        pdf-orientation="landscape"
        pdf-content-width="100%"
        @hasStartedGeneration="hasStartedGeneration()"
        @hasGenerated="hasGenerated($event)"
      >
      <div slot="pdf-content">
       <div class="TablePatientDocId p-3">
          <table class="myTable">
            <thead>
              <tr class="header ">
                <th  width="40" class="text-left">№</th>
                <th width="200">{{$t('patient_name')}}</th>
                <th>{{$t('real_qty')}}</th>
                <th>{{$t('summ_dolg')}}</th>
                <th >{{$t('date')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in get_payment_list" :key="rowIndex">
                <td> <span >{{rowIndex+1}}</span> </td>
                <td> <span >{{row.patient_name}}</span> </td>
                <td> <span >{{row.real_qty}}</span> </td>
                <td> <span >{{row.summ_dolg}}</span> </td>
                <td> <span >{{row.created_date_time}}</span> </td>
                
              </tr>
              <tr >
                
              </tr>
            </tbody>
          </table>
        </div>
      </div>

    </vue-html2pdf>

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
                <th>{{$t('dolg_date')}}</th>
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
                <td> <small style="font-size: 12.5px;">{{row.dolgDate.slice(0,10)}}</small> </td>
              </tr>
            </tbody>
          </table>
        </div>
      </template>
    </ModalUser>
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>
  import VueHtml2pdf from 'vue-html2pdf'
  // import DatePicker from 'vue2-datepicker';
  // import RegSelect from '../../../components/RegSelect.vue'
  // import districtAdd from "../../../components/new_prog_add/district_add"
  import { mdbBtn, } from 'mdbvue';
import ModalUser from '../../../components/modal.vue'
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      VueHtml2pdf,
      ModalUser
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
        group_name: '',
        group_id: null,
        get_payment_list: [],
        index: null,
        service_id: null,
        day: '',
        qty: 0,
        summ: 0,
        card: 0,
        cash: 0,
        debit_list_patient_id: [],
      }
    },
    async mounted(){
      if(localStorage.Type == 0){
        this.admin = true;
      }
      {
        this.loading = true;
        // let time1 = new Date();
        // this.Start_time = time1.toISOString().slice(0,10); 
        // this.End_time = time1.toISOString().slice(0,10);
        // let a = this.Start_time + 'T00:00:35.000Z' ;
        // let b = this.End_time + 'T23:59:59.000Z';
        try{
          const resp = await fetch(this.$store.state.hostname + '/HospitalPatientDolgPaymentInfoes/getPaginationQarzdorlik?page=0&size=1000')
          const dataStr = await resp.json();
          console.log('dataStr')
          console.log(dataStr)
          this.get_payment_list = dataStr.items_list
        }
        catch(error){
          console.log(error)
        }
        

        this.loading = false;
      }

        this.fetch_service_group()
    },
    computed: mapGetters(['get_contragent_list', 'get_report_by_data_time', 'get_report_by_time_card_cash', 'get_service_group_list']),
    methods: {
      ...mapActions(['fetch_contragent', 'fetch_report_by_data_time', 'fetch_service_group']),
      ...mapMutations(['district_row_delete']),
      async showPatientIdDebitPayment(id){
        try{
          const res = await fetch(this.$store.state.hostname + '/HospitalPatientDolgPaymentInfoes/getPaginationByPatientId?page=0&size=100&patient_id=' + id);
          const res_data = await res.json();
          console.log('res_data_patient_id_list');
          console.log(res_data.items_list);
          this.debit_list_patient_id = res_data.items_list;
          this.show = true;
        }
        catch{
          console.log('server error')
        }
      },
      add(){
        this.show =! this.show
        this.editData = {};
      },
      print(){
        this.$refs.listlar.generatePdf()
      },
      showed(id){
        this.$router.push('/debitdetiels/' +id)
      },
      deleteRow(data, index){
        console.log(data)
        this.index = index
        this.service_id = data.id
        this.show = true;
      },
    },
  };
</script>

<style lang="scss" scoped>
  @import "../../../scss/tableAll.scss";
</style>
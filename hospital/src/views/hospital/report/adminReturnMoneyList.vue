<template>
  <div class="bg-white">
    <loader v-if="loading"/>
    <div class="p-4">
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
        <form >
          <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
            <div class="title w-75 row align-items-center">
              <div class="d-flex w-50 px-3">
                <input-icon style="width: 100%; height:30px;" v-model="search" @input="searchClick" :placeholder="$t('search_here')"></input-icon>
                <mdb-btn class="mr-1 ml-0 mt-0  py-1 px-3"  
                style="font-size: 9px; height:28px; width:90px; margin-top: 1px !important;" 
                color="info"  
                @click="searchClick()" 
                  size="sm">{{$t('search')}}
                </mdb-btn>
              </div>
              <!-- <div v-if="!admin" class="col-5">
                <div style="position: relative; margin-top: 40px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -8px; color: #757575;">
                    {{$t('start_time')}}
                  </small>
                  <mdb-input type="date"  v-model="Start_time" outline/>
                </div>
              </div> -->
            </div>
            <div class="plus">
              <mdb-btn @click="print" color="info py-2 px-4" style="font-size:10px;"  >
                {{$t('print')}}
              </mdb-btn>
              <mdb-btn @click="apply"  :disabled="admin" color="primary py-2 px-4" style="font-size:10px;"  >
                {{$t('apply')}}
              </mdb-btn>
            </div>
          </div>
        </form>
        <div class="TablePatientDocIdApply p-3">
          <table class="myTable">
            <thead>
              <tr class="header ">
                <th width="60">№</th>
                <th >{{$t('bemor')}}</th>
                <th width="230">{{$t('contragent')}}</th>
                <th>{{$t('service')}}</th>
                <th >{{$t('date')}}</th>
                <th >{{$t('card')}}</th>
                <th >{{$t('cash')}}</th>
                <th >{{$t('payed')}}</th>
                <th >{{$t('finish')}}</th>
                <th >{{$t('Action')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in order_list" :key="rowIndex">
                <td>{{rowIndex+1}}</td>
                <td> <span >{{row.patients.FIO}}</span> </td>

                <td> <span>{{row.discount_card_qty}}</span></td>
                <td> <span >{{row.ServiceName}}</span> </td>
                <td> <span >{{row.PayedDate.slice(0,10)}}  | {{row.PayedDate.slice(11,16)}}</span> </td>
                <td> <span :class="{'text-success': row.PaymentInCard>0}">{{row.PaymentInCard}}</span> </td>
                <td> <span :class="{'text-success': row.PaymentInCash>0}" >{{row.PaymentInCash}}</span> </td>
                <td>
                  <mdb-badge v-show="row.FinishPayment === true" style="padding: 2px 8px;" pill color="success">{{$t("payed")}}</mdb-badge>
                  <mdb-badge v-show="row.FinishPayment === false" style="padding: 2px 8px;" pill color="danger">{{$t('unpayed')}}</mdb-badge>
                </td>
                <td>
                  <mdb-badge v-show="row.Finish === true" style="padding: 2px 8px;" pill color="success">{{$t("проверил")}}</mdb-badge>
                  <mdb-badge v-show="row.Finish === false" style="padding: 2px 8px;" pill color="danger">{{$t('непроверенный')}}</mdb-badge>
                </td>
                <td> 
                  <mdb-btn @click="remove(row.Id)" style="font-size:9px;" color="danger py-1 px-2">возврат</mdb-btn>  
                </td>
              </tr>
              <tr >
                <td> <span class="text-primary">Общий</span> </td>
                <td> <span ></span> </td>
                
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>


    <!-- <div :class="{'showing':show}">
      <div class="add d-flex justify-content-center align-items-center" >  
        <districtAdd/>
      </div>
    </div> -->
      <mdb-modal  :show="show"  @close="show = false"  light>
        <mdb-modal-header>
          <mdb-modal-title style="font-weight:  500;">{{$t('Добавить Район')}}</mdb-modal-title>
        </mdb-modal-header>
        <mdb-modal-body>
          <districtAdd :options="editData"/>
        </mdb-modal-body>
      </mdb-modal>

      <mdb-modal :show="confirm" @close="confirm = false" size="sm" class="text-center" danger>
        <mdb-modal-header center :close="false">
          <p class="heading">{{$t('Are_you_sure')}}</p>
        </mdb-modal-header>
        <mdb-modal-body>
          <span>Ты хочешь вернуть деньги?</span>
        </mdb-modal-body>
        <mdb-modal-footer center>
          <mdb-btn outline="danger" @click="promise">{{$t('Yes')}}</mdb-btn>
          <mdb-btn color="danger" @click="confirm = false">{{$t('No')}}</mdb-btn>
        </mdb-modal-footer>
      </mdb-modal>
      <mdb-modal :show="confirmP" @close="confirmP = false" size="sm" class="text-center" info>
        <mdb-modal-header center :close="false">
          <p class="heading">{{$t('Are_you_sure')}}</p>
        </mdb-modal-header>
        <mdb-modal-body>
          <span>Вы обязательно закроете все операции, потому что после этого вы не сможете все вернуть?</span>
        </mdb-modal-body>
        <mdb-modal-footer center>
          <mdb-btn outline="primary" @click="promiseP">{{$t('Yes')}}</mdb-btn>
          <mdb-btn color="primary" @click="confirmP = false">{{$t('No')}}</mdb-btn>
        </mdb-modal-footer>
      </mdb-modal>

    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>
  // import VueHtml2pdf from 'vue-html2pdf'
  // import DatePicker from 'vue2-datepicker';
  import districtAdd from "../../../components/new_prog_add/district_add"
  import inputIcon from "../../../components/inputIcon"
  import { mdbBtn, mdbBadge,  mdbModal, mdbModalHeader,  mdbModalTitle, mdbModalBody, mdbModalFooter  } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,mdbBadge,
      mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, mdbModalFooter,
      districtAdd, inputIcon
    },
    data(){
      return{
        show: false,
        snipper: true,
        editData: {},
        modal_info : '',
        modal_status: false,
        contragent_name: '',
        contragent_id: 0,
        Start_time: null,
        End_time: null,
        admin: true,
        kunlik_report_lists: [],
        cach_order_list: [],
        order_list: [],
        loading: false,
        confirm: false,
        payment: null,
        confirmP: false,
        search: '',

      }
    },
    async mounted(){
       
        this.refresh();
        // await this.fetch_report_by_data(a)
        // await this.fetch_contragent()
        // console.log(this.get_report_by_data)
        this.refresh();
        if(localStorage.Type == 0){
          this.admin = false;
        }
    },
    computed: mapGetters(['get_contragent_list', 'get_report_by_data', 'get_report_by_time_card_cash']),
    methods: {
      ...mapActions(['fetch_contragent', 'fetch_report_by_data']),
      ...mapMutations(['dibet_delite_cont']),

      add(){
        this.show =! this.show
        this.editData = {};
      },
      print(){
        this.$refs.listlar.generatePdf()
      },
      async refresh(){
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/Payments/getPaginationAllUnfinishedPaymentsListForAdmin?page=0&size=150");
        const data = await response.json();
        console.log(data)
        this.loading = false;
        this.kunlik_report_lists = data.items_list;
        console.log('this.kunlik_report_lists')
        console.log(this.kunlik_report_lists)
        this.order_list = this.kunlik_report_lists;
      },
      async searchClick(){
        this.order_list = this.kunlik_report_lists;
        this.loading = true;
        if(this.search != ''){
          try{
            const response = await fetch(this.$store.state.hostname + '/Payments/getPaginationAllUnfinishedPaymentsListForAdminWithName?page=0&size=150&fio=' + this.search)
            const data = await response.json()
            console.log(data.items_list)
            this.order_list = data.items_list;
          }
          catch(error){
            console.log(error)
          }
        }
        else{
          this.order_list = this.kunlik_report_lists;
        }
        this.loading = false;
      },
      async promise(){
         this.loading = true;
          const response = await fetch(this.$store.state.hostname + '/Payments/getReturnAlreadyPaidMoneyToBackClient?paymentId=' + this.payment + '&deletorAuthId=' + localStorage.AuthId)
          const data = await response.json()
          console.log(data)
          this.refresh();
          this.loading = false;
          this.confirm = false;
      },
      async promiseP(){
        const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify(this.kunlik_report_lists)
          };
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + '/Payments/priminitPaymnetsListAsDoctor', requestOptions)
          const data = await response.json()
          console.log(data)
          this.loading = false;
          if(data){
            this.refresh();
            this.$refs.message.success('Added_successfully')
          }
          else{
            this.$refs.message.error('Network ne connect')
          }
          this.confirmP = false;
      },
      async remove(payment){
        console.log('fun')
         this.payment = payment;
         this.confirm = true;
        console.log(payment)
      },
      async apply(){
        this.confirmP = true;
        console.log('apply')
      },
      async deleteRow(i, del_data){
        const requestOptions = {
            method : "delete",
          };
          this.loading =true;
          const response = await fetch(this.$store.state.hostname + "/HospitalContragentDebitPaymentReports/" + del_data.id, requestOptions);
          const data = await response.json();
          this.loading = false;

          if(data)
          {
            // this.alert_text = localizeFilter('Successfully_removed');
            this.$refs.message.error('Successfully_removed')
            this.dibet_delite_cont(i);
          }
          else{
            this.modal_info = data.detail + "    (" + data.routine + ")";
            this.modal_status = true;
          }
      },
      async submit(){
        console.log(this.Start_time)
      },

    },
  };
</script>

<style lang="scss" scoped>
  @import "../../../scss/tableAll.scss";
</style>
<template>
  <div class="bg-white">
    <loader v-if="loading"/>
    <div class="p-4">
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
        <form @submit.prevent="submit">
          <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
            <div class="title w-75 row align-items-center">
               <div class="col-2">
                <div style="position: relative; margin-top: 25px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -12px; color: #757575;">
                    {{$t('start_time')}}
                  </small>
                  <mdb-input type="date" size="sm"  v-model="Start_time" outline/>
                </div>
              </div>
              <div class="col-2">
                <div style="position: relative; margin-top: 25px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -12px; color: #757575;">
                    {{$t('end_time')}}
                  </small>
                  <mdb-input type="date" size="sm"  v-model="End_time" outline/>
                </div>
              </div>
              <div class="col-4">
                <RegSelect_casher
                  class="mt-0"
                  label="Кассир"
                  @select="users_func"
                  :options="get_auth_user_limit"
                  :selected="user_name"
                  :searching="true"
                />
              </div>
              <div class="col-4">
                <RegSelect
                :label="$t('service_group')"
                @select="select_service_Group"
                :options = "get_service_group_list" 
                :selected="group_name"
                class="mb-3"
               />
              </div>
              
            </div>
            <div class="plus d-flex">
              <mdb-btn @click="allService" color="indigo py-2 px-4"  style="font-size:10px;" >
                {{$t('all_service')}}
              </mdb-btn>
              <mdb-btn type="submit" color="primary py-2 px-4" style="font-size:10px;"  >
                {{$t('apply')}}
              </mdb-btn>
              <div v-show="false">
                <download-excel
                  class="bg-info rounded px-2"
                  style="margin-top:6px; padding: 3.5px 5px;"
                  :data = "get_report_by_data_time_excel"
                  :fields = "json_fields"
                  name="Service.xls">
                  <small class="text-white ml-1" style="font-size: 12px;">
                      <mdb-icon icon="file-excel" class="mr-1"></mdb-icon>
                      Excel
                  </small>
                </download-excel>
              </div>
            </div>
          </div>
        </form>
        <div class="row mt-3 border-bottom pb-3">
          
          <div class="col-2">
              <div class="qty borderSolder py-2">
                  <span class="ml-3">{{$t('service_qty')}}</span>
                  <div class="text-right px-3 mt-1">
                      <p>{{qty}}</p>
                  </div>
              </div>
          </div>
          <div class="col-2">
              <div class="qty borderSolder py-2">
                  <span class="ml-3">{{$t('summ_service')}}</span>
                  <div class="text-right px-3 mt-1">
                      <p>{{summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                  </div>
              </div>
          </div>
          <div class="col-2">
              <div class="qty borderSolder py-2">
                  <span class="ml-3">{{$t('cash')}}</span>
                  <div class="text-right px-3 mt-1">
                      <p>{{cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                  </div>
              </div>
          </div>
          <div class="col-2">
              <div class="qty borderSolder py-2">
                  <span class="ml-3">{{$t('card')}}</span>
                  <div class="text-right px-3 mt-1">
                      <p>{{card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                  </div>
              </div>
          </div>
          <div class="col-2">
              <div class="qty borderSolder py-2">
                  <span class="ml-3">{{$t('discount_qty')}}</span>
                  <div class="text-right px-3 mt-1">
                      <p>{{(discount_qty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                  </div>
              </div>
          </div>
          <div class="col-2">
              <div class="qty borderSolder py-2">
                  <span class="ml-3">{{$t('summ_payed')}}</span>
                  <div class="text-right px-3 mt-1">
                      <p>{{(card+cash).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                  </div>
              </div>
          </div>
        </div>
        <div class="TablePatientDocId p-3">
          <table class="myTable">
            <thead>
              <tr class="header ">
                <th  width="40" class="text-left">№</th>
                <th width="200">{{$t('patient_name')}}</th>
                <th>{{$t('contragent_name')}}</th>
                <th>{{$t('service_name')}}</th>
                <th >{{$t('service_price')}}</th>
                <th >{{$t('paymentInCash')}}</th>
                <th >{{$t('paymentInCard')}}</th>
                <th>{{$t('discount_persantage_qty')}}</th>
                <th>{{$t('discount_qty')}}</th>
                <th>{{$t('debit')}}</th>
                <th >{{$t('regisdate')}}</th>
                <th >{{$t('payed')}}</th>
                <th >{{$t('finish')}}</th>
                <th v-show="admin" width="70">{{$t('Action')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in get_payment_list" :key="rowIndex" :class="{'alert-danger': row.dolg_summ>0, 'alert-success': row.dolg_status}">
                <td> <span >{{rowIndex+1}}</span> </td>
                <td> <span >{{row.patient_name}}</span> </td>
                <td> <span >{{row.discount_card_qty}}</span></td>
                <!-- <td> <span >{{row.contragent_name}}</span></td> -->
                <td> <span >{{row.service_name}}</span> </td>
                <td> <span >{{row.service_price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span :class="{'text-danger': row.paymentInCash == 0, 'text-success': row.paymentInCash != 0}">{{row.paymentInCash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span :class="{'text-danger': row.paymentInCard == 0, 'text-success': row.paymentInCard != 0}">{{row.paymentInCard.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{row.discount_persantage_qty}} %</span> </td>
                <td > <span v-if="row.discount_qty != null && row.discount_qty != ''">{{row.discount_qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td > <span v-if="row.dolg_summ != null && row.dolg_summ != ''">{{row.dolg_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{row.regisdate.slice(0,10)}}</span> 
                  <span class="ml-1" style="font-size: 12px;"> | {{row.regisdate.slice(11,16)}}</span>
                </td>
                <td>
                  <mdb-badge v-show="row.payed === true" style="padding: 2px 8px;" pill color="success">{{$t("payed")}}</mdb-badge>
                  <mdb-badge v-show="row.payed === false" style="padding: 2px 8px;" pill color="danger">{{$t('unpayed')}}</mdb-badge>
                </td>
                <td>
                  <mdb-badge v-show="row.finish === true" style="padding: 2px 8px;" pill color="success">{{$t("проверил")}}</mdb-badge>
                  <mdb-badge v-show="row.finish === false" style="padding: 2px 8px;" pill color="danger">{{$t('непроверенный')}}</mdb-badge>
                </td>
                <td v-show="admin" class="text-center">
                    <i class="fas fa-trash delIcon mask waves-effect m-0 pl-2" v-on:click="deleteRow(row,rowIndex)" :data-row="rowIndex"></i>
                </td>
              </tr>
              <tr >
                <td> <span class="text-primary">Общий</span> </td>
                <td> <span ></span> </td>
                <!-- <td> <span ></span> </td> -->
                <td> <span class="text-primary">{{qty}}</span></td>
                <td> <span class="text-danger">{{summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span class="text-danger">{{cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span class="text-danger">{{card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td></td>
                <td><span class="text-danger">{{discount_qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span></td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
      <mdb-modal :show="show" @close="show = false" size="sm" class="text-center" danger>
        <mdb-modal-header center :close="false">
          <p class="heading">{{$t('Are_you_sure')}}</p>
        </mdb-modal-header>
        <mdb-modal-body>
          <mdb-icon icon="times" size="4x" class="animated rotateIn"/>
        </mdb-modal-body>
        <mdb-modal-footer center>
          <mdb-btn outline="danger" @click="promiseService">{{$t('Yes')}}</mdb-btn>
          <mdb-btn color="danger" @click="show = false">{{$t('No')}}</mdb-btn>
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
  import RegSelect from '../../../components/RegSelect.vue'
  import RegSelect_casher from '../../../components/hospital/UserlineSelect.vue'

  // import districtAdd from "../../../components/new_prog_add/district_add"
  import { mdbBtn, mdbInput, mdbIcon,  mdbModal, mdbModalHeader, mdbBadge,  mdbModalBody,mdbModalFooter   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      mdbModal, mdbModalHeader, mdbIcon, mdbModalBody, mdbModalFooter,
      mdbBadge, mdbInput,
      RegSelect, RegSelect_casher
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
        payment_data: null,
        day: '',
        qty: 0,
        summ: 0,
        card: 0,
        cash: 0,
        discount_qty: 0,
        json_fields: {
          'Имя пациента': 'patient_name',
          'Имя врача': 'discount_card_qty',
          'Услуги': 'serviceGroup',
          'Сумма': 'summ',

        },
        user_name: '',
        user_id: 0,
      }
    },
    async mounted(){
      if(localStorage.Type == 0){
        this.admin = true;
      }
      {
        await this.fetch_auth_list();
        let time1 = new Date();
        this.Start_time = time1.toISOString().slice(0,10); 
        this.End_time = time1.toISOString().slice(0,10);
        let a = this.Start_time + 'T00:00:35.000Z' ;
        let b = this.End_time + 'T23:59:59.000Z';
        let c = {
          time1: '',
          time2: '',
          contId: this.user_id
        }
        c.time1 = a;
        c.time2 = b;
        this.loading = true;
        await this.fetch_report_by_kassir_data_time(c)
        // await this.fetch_report_by_data_time_excel(c)
        this.get_payment_list = this.get_report_by_data_time_kassir.rows
        this.qty = this.get_report_by_time_card_cash_kassir.qty
        this.summ = this.get_report_by_time_card_cash_kassir.summ;
        this.card = this.get_report_by_time_card_cash_kassir.card;
        this.cash = this.get_report_by_time_card_cash_kassir.cash;
        this.discount_qty = this.get_report_by_time_card_cash_kassir.discount_qty
        this.loading = false;
        
      }

        this.fetch_service_group()
    },
    computed: mapGetters(['get_contragent_list', 'get_report_by_data_time_kassir', 'get_report_by_time_card_cash_kassir',
     'get_service_group_list', 'get_report_by_data_time_excel', 'get_auth_user_limit']),
    methods: {
      ...mapActions(['fetch_contragent', 'fetch_report_by_kassir_data_time', 'fetch_service_group',
        'fetch_report_by_data_time_excel', 'fetch_auth_list']),
      ...mapMutations(['district_row_delete']),
      async users_func(options){
        console.log(options)
        this.user_name = options.data.fio
        this.user_id = options.data.Authid
      },
      add(){
        this.show =! this.show
        this.editData = {};
      },
      print(){
        this.$refs.listlar.generatePdf()
      },
      deleteRow(data, index){
        console.log('data')
        console.log(data)
        this.index = index;
        this.service_id = data.id;
        this.payment_data = data;
        this.show = true;
      },
      async promiseService(){
        // const requestOptions = {
        //   method: "POST",
        //   headers : { "Content-Type" : "application/json" },
        //   body : JSON.stringify({
        //     "deleted_user_name": localStorage.docName,
        //     "name" : this.payment_data.discount_card_qty,
        //     "serviceName": this.payment_data.service_name,
        //     "patientName": this.payment_data.patient_name,
        //     "serviceTypeId": this.payment_data.serviceTypeId,
        //     "finish": this.payment_data.finish,
        //     "authorizationId": this.payment_data.authorizationId,
        //     "contragentId": this.payment_data.contragentId,
        //     "registratedDate": this.payment_data.regisdate,
        //     "finishPayment": this.payment_data.payed,
        //     "payedDate": this.payment_data.payedDate,
        //     "dletedAuthId": localStorage.AuthId,

        //   })
        // };
        // this.loading = true;
        // const response = await fetch(this.$store.state.hostname + "/PaymentsDeleteds", requestOptions);
        // const data = await response.json();
        // this.loading = false;
        // if(response.status == 200 || response.status == 201){
        //   console.log(data);
        //   await this.PaymentDelete();
        // }
        await this.PaymentDelete();

      },
      async PaymentDelete(){
        const requestOptions = {
          method : "delete",
        };
        const response = await fetch(this.$store.state.hostname + "/Payments/" + this.service_id, requestOptions);
        const data = await response.json();
        console.log(data)
        if(data.id)
        {
          this.$refs.message.success('Successfully_removed')
          this.get_payment_list.splice(this.index, 1);
          this.show = false;
        }
      },
      select_service_Group(option){
        this.group_name = option.name;
        this.group_id = option.id;
        this.summ = 0;
        this.qty = 0;
        this.cash = 0;
        this.card = 0;
        this.discount_qty = 0;
        this.get_payment_list = []
        for(let i = 0; i < this.get_report_by_data_time_kassir.rows.length; i++) {
          if(this.get_report_by_data_time_kassir.rows[i].serviceGroup == this.group_name){
            this.get_payment_list.push(this.get_report_by_data_time_kassir.rows[i])
            this.summ += this.get_report_by_data_time_kassir.rows[i].service_price
            this.card += this.get_report_by_data_time_kassir.rows[i].paymentInCard
            this.cash += this.get_report_by_data_time_kassir.rows[i].paymentInCash;
            this.discount_qty += this.get_report_by_data_time_kassir.rows[i].discount_qty;
            // this.summa += this.get_report_by_data[i].summa;
          }
        }
            this.qty = this.get_payment_list.length
      },
      async allService(){
        let a = {
          time1: "2021-09-01T09:15:28.886Z",
          time2: new Date(),
          contId: this.user_id
        }
        a.time1 = this.Start_time + 'T00:00:35.000Z';
        a.time2 = this.End_time + 'T23:59:59.000Z';
        this.loading = true;
        await this.fetch_report_by_kassir_data_time(a);
        // await this.fetch_report_by_data_time_excel(a)
        this.get_payment_list = [];
        this.get_payment_list = this.get_report_by_data_time_kassir.rows;
        this.qty = this.get_report_by_time_card_cash_kassir.qty;
        this.summ = this.get_report_by_time_card_cash_kassir.summ;
        this.card = this.get_report_by_time_card_cash_kassir.card
        this.cash = this.get_report_by_time_card_cash_kassir.cash
        this.discount_qty = this.get_report_by_time_card_cash_kassir.discount_qty

        this.loading = false;
        this.group_name = '';
        this.group_id = null;
      },
      async submit(){
        let a = {
          time1: "2021-09-01T09:15:28.886Z",
          time2: new Date(),
          contId: this.user_id
        }
        a.time1 = this.Start_time + 'T00:00:35.000Z';
        a.time2 = this.End_time + 'T23:59:59.000Z';
        this.loading = true;
        await this.fetch_report_by_kassir_data_time(a);
        // await this.fetch_report_by_data_time_excel(a)
        this.get_payment_list = [];
        if(this.group_name == ''){
          this.get_payment_list = this.get_report_by_data_time_kassir.rows
          this.qty = this.get_report_by_time_card_cash_kassir.qty
          this.summ = this.get_report_by_time_card_cash_kassir.summ;
          this.card = this.get_report_by_time_card_cash_kassir.card
          this.cash = this.get_report_by_time_card_cash_kassir.cash
          this.discount_qty = this.get_report_by_time_card_cash_kassir.discount_qty
          this.loading = false;
        }
        else{
          this.get_payment_list = []
              this.summ = 0;
              this.card = 0;
              this.cash = 0;
              this.discount_qty = 0;
          for(let i = 0; i < this.get_report_by_data_time_kassir.rows.length; i++) {
            if(this.get_report_by_data_time_kassir.rows[i].serviceGroup == this.group_name){
              this.get_payment_list.push(this.get_report_by_data_time_kassir.rows[i])
              this.summ += this.get_report_by_data_time_kassir.rows[i].service_price
              this.card += this.get_report_by_data_time_kassir.rows[i].paymentInCard
              this.cash += this.get_report_by_data_time_kassir.rows[i].paymentInCash
              this.discount_qty += this.get_report_by_data_time_kassir.rows[i].discount_qty
              this.loading = false;
              // this.summa += this.get_report_by_data[i].summa;
            }
          }
              this.qty = this.get_payment_list.length
        }
        console.log(this.get_payment_list)
        
      },

    },
  };
</script>

<style lang="scss" scoped>
  @import "../../../scss/tableAll.scss";
</style>
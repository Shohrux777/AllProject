<template>
  <div class="cashbox bg-white">
    <check v-show="check_show" @close="closeCheck" :indexItem="checkIndex" @closeNext = "closeNext" ref="checkP"/>
    <div v-show="!check_show" class="cash-header  pt-2">
      <div class="cashbox_page border-bottom pb-3 px-3">
        <div class="row ">
          <div class="col-3 pl-3 pr-0">
            <lineSelect
              class="mt-1 ml-0 mr-3"
              :options="get_unpay_patient_list"
              :searchshow="true"
              @select="selectPatient"
              :selected="patient_name"
              :label="$t('patient list')"
            />
            <small class="mt-1 " style="color:#67676C;">
              Последние 5 пациентов
            </small>
            <div class=" last_unpay_patient">
              <div class="choosePatientLast px-1" v-for="option in get_unpay_patient_list" :key="option.id" 
                  :class="{'activ_last_patient': activ_id == option.id}"
                  @click="selectOption(option)"
              >
                <div class="d-flex align-items-center">
                  <div class="picture_back_last">
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-user" width="30" height="30" viewBox="0 0 24 24" stroke-width="1" stroke="#fff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                      <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                      <circle cx="12" cy="7" r="4" />
                      <path d="M6 21v-2a4 4 0 0 1 4 -4h4a4 4 0 0 1 4 4v2" />
                    </svg>
                  </div>
                  <div class="ml-2">
                    <h6 style="font-size: 13px;" class="m-0 p-0">{{option.fio}}</h6>
                    <span style="font-size:11px; padding-top:-15px;" class="text-primary">{{option.phoneNumber}}</span>
                  </div>
                </div>
              </div>
            </div>
            
          </div>
          <div class="col-4 px-2 pl-4 mt-1">
            <div class="summa_content">
              <div class="w-100">
                <div class="qty borderSolder py-2">
                  <span class="ml-3">Пациент</span>
                  <div class="text-right px-3 mt-1">
                    <p>{{ patient_name }}</p>
                  </div>
                </div>
              </div>
              <div class="d-flex">
                <div class="w-50">
                  <div class="qty borderSolder py-2">
                    <span class="ml-3">{{$t('summ')}}</span>
                    <div class="text-right px-3 mt-1">
                      <p>{{summaString}}</p>
                    </div>
                  </div>
                </div>
                <div class="w-50">
                  <div class="qty borderSolder py-2">
                    <span class="ml-3">{{$t('discount')}}</span>
                    <div class="text-right px-3 mt-1">
                      <p>{{ skidkaString }}</p>
                    </div>
                  </div>
                </div>
              </div>
              
            </div>
          </div>
          <div class="col-5 px-4 ">
            <div class="row">
              <div class="col-4 mt-1 px-1">
                <div class="btn_pay bg-success" @click="payCash">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-cash" width="24" height="24" viewBox="0 0 24 24" stroke-width="1.2" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <rect x="7" y="9" width="14" height="10" rx="2" />
                    <circle cx="14" cy="14" r="2" />
                    <path d="M17 9v-2a2 2 0 0 0 -2 -2h-10a2 2 0 0 0 -2 2v6a2 2 0 0 0 2 2h2" />
                  </svg>
                  <small class="ml-1">
                    {{$t('cash')}}
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1" @click="payCard">
                <div class="btn_pay bg-primary">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-credit-card" width="22" height="22" viewBox="0 0 24 24" stroke-width="1" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <rect x="3" y="5" width="18" height="14" rx="3" />
                    <line x1="3" y1="10" x2="21" y2="10" />
                    <line x1="7" y1="15" x2="7.01" y2="15" />
                    <line x1="11" y1="15" x2="13" y2="15" />
                  </svg>
                  <small class="ml-1">
                    {{$t('card')}}
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1">
                <div class="btn_pay bg_dolg" @click="debit">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-cash-banknote-off" width="22" height="22" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <path d="M9.88 9.878a3 3 0 1 0 4.242 4.243m.58 -3.425a3.012 3.012 0 0 0 -1.412 -1.405" />
                    <path d="M10 6h9a2 2 0 0 1 2 2v8c0 .294 -.064 .574 -.178 .825m-2.822 1.175h-13a2 2 0 0 1 -2 -2v-8a2 2 0 0 1 2 -2h1" />
                    <line x1="18" y1="12" x2="18.01" y2="12" />
                    <line x1="6" y1="12" x2="6.01" y2="12" />
                    <line x1="3" y1="3" x2="21" y2="21" />
                  </svg>
                  <small class="ml-1">
                    {{$t('debit')}}
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1">
                <div class="btn_pay bg-info" @click="infocash">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-coin" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <circle cx="12" cy="12" r="9" />
                    <path d="M14.8 9a2 2 0 0 0 -1.8 -1h-2a2 2 0 0 0 0 4h2a2 2 0 0 1 0 4h-2a2 2 0 0 1 -1.8 -1" />
                    <path d="M12 6v2m0 8v2" />
                  </svg>
                  <small class="ml-1">
                    Инфо касса
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1">
                <div class="btn_pay bg_rasxod" @click="returnMoney">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-registered" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <circle cx="12" cy="12" r="9" />
                    <path d="M9 16v-8h4a2 2 0 0 1 0 4h-4m3 0l3 4" />
                  </svg>
                  <small class="ml-1">
                    {{$t('returnMoney')}}
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1">
                <div class="btn_pay bg_return" @click="returnPatient">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-receipt-refund" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <path d="M5 21v-16a2 2 0 0 1 2 -2h10a2 2 0 0 1 2 2v16l-3 -2l-2 2l-2 -2l-2 2l-2 -2l-3 2" />
                    <path d="M15 14v-2a2 2 0 0 0 -2 -2h-4l2 -2m0 4l-2 -2" />
                  </svg>
                  <small class="ml-1">
                    Возврат
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1">
                <div class="btn_pay bg_bron" @click="$router.push('/not_payed_list')">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-bed" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <path d="M3 7v11m0 -4h18m0 4v-8a2 2 0 0 0 -2 -2h-8v6" />
                    <circle cx="7" cy="10" r="1" />
                  </svg>
                  <small class="ml-1">
                    Стационар
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1">
                <div class="btn_pay bg_paydebit" @click="debit_pay">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-wallet" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <path d="M17 8v-3a1 1 0 0 0 -1 -1h-10a2 2 0 0 0 0 4h12a1 1 0 0 1 1 1v3m0 4v3a1 1 0 0 1 -1 1h-12a2 2 0 0 1 -2 -2v-12" />
                    <path d="M20 12v4h-4a2 2 0 0 1 0 -4h4" />
                  </svg>
                  <small class="ml-1">
                    {{$t('pay_debit')}}
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1">
                <div class="btn_pay bg-primary" >
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-chart-infographic" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <circle cx="7" cy="7" r="4" />
                    <path d="M7 3v4h4" />
                    <line x1="9" y1="17" x2="9" y2="21" />
                    <line x1="17" y1="14" x2="17" y2="21" />
                    <line x1="13" y1="13" x2="13" y2="21" />
                    <line x1="21" y1="12" x2="21" y2="21" />
                  </svg>
                  <small class="ml-1">
                    Статистика
                  </small>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      
      <div class="cash-table ">
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
                <th width="150">{{$t('payed')}}</th>
                <th width="80">{{$t('Action')}}</th>
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
                  <mdb-badge v-show="row.finishPayment === true" style="padding: 2px 8px;" pill color="success">{{$t("payed")}}</mdb-badge>
                  <mdb-badge v-show="row.finishPayment === false" style="padding: 2px 8px;" pill color="danger">{{$t('unpayed')}}</mdb-badge>
                </td>
                <td>
                  <mdb-btn @click="payOneDevice(row.id, row.summ, row.serviceName,row)"  color="success" class="m-0 p-0" style="font-size: 8px;"  p="r3 l3 t1 b1">{{$t('pay')}}</mdb-btn>  
                </td>
                <td class="text-center">
                  <!-- <i class="fas fa-pen editIcon mask waves-effect t m-0 pr-2" disabled  :data-row="rowIndex"></i> -->
                  <i class="fas fa-trash delIcon mask waves-effect m-0 pl-2" disabled @click="delService(row, rowIndex)" :data-row="rowIndex"></i>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
    <mdb-modal :show="confirm" @close="confirm = false" size="sm" class="text-center" success>
        <mdb-modal-header center :close="false">
          <p class="heading">{{$t('Are_you_sure')}}</p>
        </mdb-modal-header>
        <mdb-modal-body>
          <mdb-icon icon="check" size="4x" class="animated rotateIn"/>
        </mdb-modal-body>
        <mdb-modal-footer center>
          <mdb-btn color="success" @click="promise">{{$t('Yes')}}</mdb-btn>
          <mdb-btn color="danger" @click="confirm = false">{{$t('No')}}</mdb-btn>
        </mdb-modal-footer>
    </mdb-modal>
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
    <ModalUser  :show="service_show" headerbackColor="info"  closeColor="white" titlecolor="black" 
    :title="$t('Service pay')" @close="service_show = false" width="500px">
        <template v-slot:body>
          <cashpay :option="service_list" @close="payed_close" />
        </template>
      </ModalUser>
      <ModalUser  :show="return_show"  classes="bg_rasxod" closeColor="white"  titlecolor="white" 
        :title="$t('returnMoney')" @close="return_show = false" width="60%">
        <template v-slot:body>
          <returnMoney :option="service_list" @close="payed_close" />
        </template>
      </ModalUser>
      <ModalUser  :show="debit_show" classes="bg_dolg" closeColor="white" titlecolor="white" 
        :title="$t('debit')" @close="debit_show = false" width="500px">
        <template v-slot:body>
          <debit :option="summa" :summa="summaString" :patient="patient_id" @close="debit_close" />
        </template>
      </ModalUser>
    <Toast ref="message"></Toast>
    
  </div>
</template>

<script>
import {mapActions, mapGetters, mapMutations} from 'vuex'

import {mdbBtn,  mdbBadge,  mdbModal, mdbModalHeader, mdbModalBody, mdbModalFooter, mdbIcon} from 'mdbvue'
import ModalUser from '../../components/modal.vue'
import lineSelect from "../../components/hospital/cashUserSelect.vue";
import cashpay from "./cashpaydevice.vue";
import debit from "./debit.vue";
import returnMoney from "./returnMoney.vue";
import check from "./check.vue"
// import DatePicker from 'vue2-datepicker';
// import 'vue2-datepicker/index.css';
export default {
  components: {
    lineSelect, mdbBtn, mdbBadge, ModalUser, cashpay, check,
     mdbModal, mdbModalHeader, mdbModalBody, mdbModalFooter, mdbIcon, returnMoney, debit
  },
  data(){
    return{
      enterSumma: null,
      zdachi:0,
      zdachiString:'0',
      currency: '',
      summaString: '0',


      patient_name: '...',
      patient_id: null,
      time1: null,
      card_code: '',
      service_show: false,
      service_list: {},
      confirm: false,
      cash: false,
      show: false,
      service_id: null,
      index: null,
      return_show: false,
      debit_show: false,
      
      summ: 0,
      CashMoney: null,
      CardMoney: null,
      ServiceSumma: 0,
      ServicName: '',
      ServicePayId: null,
      serviceList_check: [],

      auth_qty: 0,
      patient_id_for_ochred: null,
      user_id_for_ochred: null,

      check_show: false,
      cashOption: null,
      checkIndex: 0,

      activ_id : -1,
      skidkaString: '0',
      skidka: 0,
    }
  },
  computed: mapGetters(['get_unpay_patient_list', 'get_service_pay_list', 'summa', 'get_check_print_list', 'get_code_patient', 'get_skidka']),
  mounted(){
    this.fetch_unpayed_patient();
    setInterval(this.fetch_unpayed_patient, 4000);
    console.log(this.get_service_pay_list)
  },
  methods: {
    ...mapActions(['fetch_unpayed_patient', 'fetch_service_pay_list', 'fetch_get_code']),
    ...mapMutations(['update_patient_name', 'Update_check_data', 'updateDebit', 'UpdatecheckInfo']),
    debit(){
      this.Update_check_data(this.get_service_pay_list);
      this.debit_show = true;
      console.log('dasdas')
    },
    updatePatients(){
      this.fetch_unpayed_patient();
    },
    debit_pay(){
      this.$router.push('/pay_debit')
    },
    async closeCheck(){
      this.check_show = false;
      this.patient_name = '...';
      this.patient_id = null;
      this.summaString = '0';
      this.skidkaString = '0';
      this.skidka = 0;
      this.checkIndex = 0;
      console.log('this.cashOption')
      console.log(this.cashOption)
      if(this.get_service_pay_list.length > 1){
        await this.selectOption(this.cashOption);
      }
    },
    closeNext(){
      this.check_show = false;
      this.checkIndex ++;
      this.check_show = true;
      this.$refs.checkP.printed()
    },
    
    async selectPatient(option){
      console.log('option');
      console.log(option);
      this.cashOption = option.data;
      this.update_patient_name(option.data.fio)
      this.patient_name = option.data.fio;
      this.patient_id = option.data.id;
      this.activ_id = option.data.id;
      this.patient_id_for_ochred = option.data.id;
      await this.fetch_service_pay_list(option.data.id);
      this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
      this.skidkaString = this.get_skidka.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
      // this.$nextTick(function () {
      //   this.$refs.enterSumma.focus();
      //   this.enterSumma = '';
      // })
      console.log(this.get_service_pay_list)
    },
    async selectOption(option){
      console.log('option');
      console.log(option);
      this.cashOption = option;
      this.update_patient_name(option.fio)
      this.patient_name = option.fio;
      this.patient_id = option.id;
      this.activ_id = option.id;
      this.patient_id_for_ochred = option.id;
      await this.fetch_service_pay_list(option.id);
      this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
      this.skidkaString = this.get_skidka.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
      
    },
    funcCurrency(n){
      console.log(n)
       this.enterSumma = n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, " ")
       var temp = ''
       for(let i=0; i<this.enterSumma.length; i++){
        if(this.enterSumma[i] != ' '){
          temp += this.enterSumma[i];
        }
       }

        this.currency = parseInt(temp);
        this.zdachi = parseFloat(this.summa)-this.currency;
        this.zdachiString = this.zdachi.toFixed().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
    },

    returnMoney(){
      this.return_show = true;
    },
    returnPatient(){
      this.$router.push('/apply_admin');
    },
    async payed_close(){
      this.service_show = false
      this.fetch_service_pay_list(this.patient_id);
      this.check_show = true;
      this.$refs.checkP.printed()
      // await this.$router.push('/check')
      // this.$root.$refs.check.printed()
    },
    async debit_close(){
      this.debit_show = false
      await this.fetch_service_pay_list(this.patient_id);
      this.check_show = true;
      this.$refs.checkP.printed();

      // await this.$router.push('/check')
      // this.$root.$refs.check.printed()
    },
    async promise(){
      this.confirm = false;
      if(this.cash === true){
        this.Update_check_data(this.get_service_pay_list);
        const respon = await fetch(this.$store.state.hostname + '/Payments/payPaymentsAllCardOrCash?PatientId=' + this.patient_id + '&Card=false')
        const data = await respon.json()
        this.$store.state.cashPay_card = true;
        console.log('shuni izlayabmzn');
        console.log(data);
        if(data[0].id){
          console.log('data[0]')
          console.log(data[0])
          await this.fetch_get_code(data[0].patientsId)
          this.UpdatecheckInfo({check:data[0].patientsId, cash: this.summa, card: 0, code:this.get_code_patient})
          var sumdebit = 0;
          this.updateDebit(sumdebit)
          // this.$refs.message.success('Added_successfully')
          await this.fetch_service_pay_list(this.patient_id);
          await this.fetch_unpayed_patient();
          // await this.$router.push('/check')
          // this.$root.$refs.check.printed()
          this.check_show = true;
          this.$refs.checkP.printed()
        }
        else{
          this.$refs.message.error('Summa xato kiritilgan')
        }
      }
      else{
        const respon = await fetch(this.$store.state.hostname + '/Payments/payPaymentsAllCardOrCash?PatientId=' + this.patient_id + '&Card=true')
        const data = await respon.json()
        this.Update_check_data(this.get_service_pay_list);
        this.$store.state.cashPay_card = false;
        console.log(data);
        if(data[0].id){
          console.log('data[0]')
          console.log(data[0])
          await this.fetch_get_code(data[0].patientsId)
          this.UpdatecheckInfo({check:data[0].patientsId, cash: 0, card: this.summa, code:this.get_code_patient})
          var sumdebits = 0;
          this.updateDebit(sumdebits)
          // this.$refs.message.success('Added_successfully')
          await this.fetch_service_pay_list(this.patient_id);
          await this.fetch_unpayed_patient();
          this.check_show = true;
          this.$refs.checkP.printed()
          // await this.$router.push('/check')
          // this.$root.$refs.check.printed()

        }
        else{
          this.$refs.message.error('Summa xato kiritilgan')
        }
      }
    },
    async promiseService(){
      const requestOptions = {
            method : "delete",
          };
          const response = await fetch(this.$store.state.hostname + "/Payments/" + this.service_id, requestOptions);
          const data = await response.json();
          console.log(data)
          if(data.id)
          {
            this.$refs.message.error('Successfully_removed')
            this.show = false;
            if(this.auth_qty == 1){
              await this.fetchNextOchred();
            }
            await this.fetch_service_pay_list(this.patient_id);
            this.fetch_unpayed_patient();
            this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
            this.skidkaString = this.get_skidka.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
            if(this.get_service_pay_list.length == 0){
              this.patient_name = '';
              this.patient_id = null;
              this.summaString = '';
            }
          }
    },
    async delService(data, index){
      console.log(data)
      this.index = index
      this.service_id = data.id
      this.show = true;
      this.patient_id_for_ochred = data.patientsId;

      this.auth_qty = 0;
      for(let i=0; i<this.get_service_pay_list.length; i++){
        if(data.authorizationId == this.get_service_pay_list[i].authorizationId){
          this.auth_qty ++;
        }
      }
      console.log(this.auth_qty)
      if(this.auth_qty == 1){
        await this.fetchAuthForUserId(data.authorizationId);
      }
    },
    async fetchAuthForUserId(id){
      try{
        const response = await fetch(this.$store.state.hostname + "/Authorization/" + id);
        const data = await response.json();
        console.log(data)
        if(response.status ==200 || response.status ==201){
          this.user_id_for_ochred = data.usersId;
        }
      }
      catch{
        console.log('error')
      }
      
    },
    async fetchNextOchred(){
      let newdate = new Date().toISOString().slice(0,10);
      console.log(newdate)
      let b_date  = newdate + 'T00:00:35.000Z';
      let e_date  = newdate + 'T23:59:35.000Z';
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/HospitalOchreds/acceptGetPaginationByDoktorIdAndPatientIdDates?page=0&size=100&user_id=" + this.user_id_for_ochred + '&patient_id=' + this.patient_id_for_ochred + '&b_date=' + b_date + '&e_date=' + e_date);
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          console.log(data, 'jsonini ckeck ochret oldim')
          this.$refs.message.success('Added_successfully')
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
    infocash(){
      // this.$router.push('/dailyForPayment')
      this.$router.push('/kunlikkassa')

    },
    async payCash(){
      this.confirm = true;
      this.cash = true;
      
    },
      async payCard(){
        this.confirm = true;
        this.cash = false;
      },
    //   printCheck () {
    //   const prtHtml = document.getElementById('print').innerHTML
    //   let stylesHtml = ''
    //   for (const node of [...document.querySelectorAll('link[rel="stylesheet"], style')]) {
    //     stylesHtml += node.outerHTML
    //   }
    //   const WinPrint = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=0,scrollbars=0,status=0')
    //   WinPrint.document.write(`<!DOCTYPE html>
    //       <html>
    //         <head>
    //           ${stylesHtml}
    //         </head>
    //         <body>
    //           <div >
    //             ${prtHtml}
    //           </div>
    //         </body>
    //       </html>`)
    //   WinPrint.document.close()
    //   WinPrint.focus()
    //   WinPrint.print()
    //   setTimeout(function () {
    //     WinPrint.close()
    //   }, 100)
    // },

    payOneDevice(id, summ, serviceName,row){
      this.service_show = true;
      console.log(id, summ, serviceName, row)
      var a = {};
      a= row;
      this.serviceList_check = [];
      this.serviceList_check.push(a);
      this.service_list = a;
      console.log(this.service_list)
      this.ServiceSumma = summ;
      this.ServicName = serviceName;
      this.ServicePayId = id
    },
  }


}
</script>

<style lang="scss" scoped>
// @import "../../scss/index.scss";
@font-face{
  font-family: "Ubuntu";
  src: url("../../font/Ubuntu/Ubuntu-Bold.ttf")
}

.paymentCheckPrintCheck{
  font-weight: bold;
  font-family: 'Ubuntu';
}
span, th, td, a, p{
 font-weight: bold;
 font-family: 'Ubuntu';
}
.TablePatientDocId{
    height: calc(100vh - 320px);
    overflow: hidden;
    overflow-y: auto;
    // border: 1px solid #ddd;
  }
  .myTable {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; overflow-x:auto
}
.myTable th{
  font-weight: 600;
  font-size:12px;
}
.myTable td{
  font-size:13px;
}
.myTable th, .myTable td {
  text-align: left;
  padding: 9px;
}

.myTable tr {
  border-bottom: 1px solid rgb(240, 240, 240);
}

.myTable tr.header, .myTable tr:hover {
  // background-color: #f1f1f1;
}

.editIcon{
  color:#01b348;
  font-size: 13px;

}
.editIcon:hover{
color: #000;
}

.delIcon:hover{
color: #000;
}
.delIcon{color: rgb(251, 70, 70);
  font-size: 13px;
}
.summa_content{
  .borderSolder{
    border: 0.5px dashed #D0D3D8;

    span{
      color:#67676C;
      font-size: 21px;
    }
    p{
      color:#525255;
      font-weight:bold;
      font-size: 23px;
      margin:0;
      padding:0;
    }
  }
}
.btn_pay{
  height: 45px;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
  text-transform: uppercase;
  small{
    font-size: 11px;
    color:white;
  }
  &:hover{
    box-shadow: rgba(0, 0, 0, 0.1) 0px 20px 25px -5px, rgba(0, 0, 0, 0.04) 0px 10px 10px -5px;

    small{
      transform: scale(1.01);
    }
  }
}
.choosePatientLast{
    padding: 5px 0px 5px 0px;
    margin: 0;
  }
.picture_back_last{
    width: 35px;
    height: 35px;
    display: flex;
    justify-content: center;
    align-items: center;
    border-radius: 50%;
    background-image: radial-gradient( circle farthest-corner at 12.3% 19.3%,  rgba(85,88,218,1) 0%, rgba(95,209,249,1) 100.2% );
  }

.choosePatientLast:hover{
    background: #e7e7e7;
  }
.bg_rasxod{
  background: linear-gradient(to right, rgb(242, 112, 156), rgb(255, 148, 114));
}
.bg_return{
  background-image: radial-gradient( circle farthest-corner at 10.2% 55.8%,  rgba(252,37,103,1) 0%, rgba(250,38,151,1) 46.2%, rgba(186,8,181,1) 90.1% );
}
.bg_dolg{
  background-image: linear-gradient( 171.8deg,  rgba(5,111,146,1) 13.5%, rgba(6,57,84,1) 78.6% );
}
.bg_bron{
  background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgba(0,152,155,1) 0.1%, rgba(0,94,120,1) 94.2% );
}
.bg_paydebit{
  background-image: linear-gradient( 83.2deg,  rgba(150,93,233,1) 10.8%, rgba(99,88,238,1) 94.3% );
}
.last_unpay_patient{
  height: 150px;
  overflow-y: scroll;
}
.cashbox_page{
  box-shadow: 0px 2px 5px rgb(238, 238, 238);
}
.activ_last_patient{
  position: relative;
  background-image: radial-gradient( circle farthest-corner at 1.3% 2.8%,  rgba(239,249,249,1) 0%, rgb(214, 228, 251) 100.2% );
}
.activ_last_patient::before{
  content: '';
  position: absolute;
  width: 2.5px;
  height: 100%;
  left: 0;
  top:0;
  background-image: linear-gradient(to right, rgb(182, 244, 146), rgb(51, 139, 147));
}
.bg_table_tr:hover{
  background-image: radial-gradient( circle farthest-corner at 1.3% 2.8%,  rgb(207, 219, 238) 100.2%, rgb(198, 214, 241) 100.2% );
}
.bg_table_header{
  background-image: linear-gradient( 65.9deg,  rgba(85,228,224,1) 5.5%, rgba(75,68,224,0.74) 54.2%, rgba(64,198,238,1) 55.2%, rgba(177,36,224,1) 98.4% );
  th,td{
    color:white;  
  }
  padding: 50px !important;
}
// </style>
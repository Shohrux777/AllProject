<template>
  <div class="bg-white">
    <check v-show="check_show" @close="close_service_check" :indexItem="checkIndex" @closeNext = "closeNext" ref="checkPrint_show"/>
    <div v-show="!check_show">
      <loader v-if="loading"/>
      <div class="p-4">
        <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
          <form @submit.prevent="submit">
            <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
              <div class="title w-75 row align-items-center">
                <div class="col-4">
                  <div style="position: relative; margin-top: 25px;"> 
                    <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -12px; color: #757575;">
                      {{$t('start_time')}}
                    </small>
                    <mdb-input type="date" size="sm"  v-model="Start_time" outline/>
                  </div>
                </div>
                <div class="col-4">
                  <div style="position: relative; margin-top: 25px;"> 
                    <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -12px; color: #757575;">
                      {{$t('end_time')}}
                    </small>
                    <mdb-input type="date" size="sm"  v-model="End_time" outline/>
                  </div>
                </div>
              </div>
              <div class="plus d-flex">
                <mdb-btn type="submit" color="primary py-2 px-4" style="font-size:10px;"  >
                  {{$t('apply')}}
                </mdb-btn>
                <mdb-btn v-show="false" @click="print" color="info py-2 px-4"  style="font-size:10px;" >
                  {{$t('print')}}
                </mdb-btn>
                <div v-show="false">
                  <download-excel
                    :data = "get_payment_payed_history_excel"
                    :fields = "json_fields"
                    name="Стационар.xls">
                    <mdb-btn color="info py-2 px-4"  style="font-size:10px;" >
                      <mdb-icon icon="file-excel" class="mr-1"></mdb-icon>
                      Excel
                    </mdb-btn>
                  </download-excel>
                </div>
              </div>
            </div>
          </form>
          <div class="TablePatientDocId p-3">
            <table class="myTable">
              <thead>
                <tr class="header ">
                  <th  width="40" class="text-left">№</th>
                  <th width="70">{{$t('id')}}</th>
                  <th width="300">{{$t('patient_name')}}</th>
                  <th>{{$t('name')}}</th>
                  <th>Услуги</th>
                  <th>{{$t('summ')}}</th>
                  <th>{{$t('payedDate')}}</th>
                  <th width="180">{{$t('Action')}}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,rowIndex) in get_payment_list" :key="rowIndex" >
                  <td> <span >{{rowIndex+1}}</span> </td>
                  <td><span class="font-weight-bold">{{ row[0].patientsId }}</span></td>
                  <td><span>{{ row[0].patients.fio }}</span></td>
                  <td><span>{{ row[0].patientName }}</span></td>
                  <td>
                    <span>{{ row.length }}</span>
                  </td>
                  <td>
                    <span class="text-success ">
                      {{ row.reduce((acc, score) => acc + parseInt(score.paymentInCard)  + parseInt(score.paymentInCash), 0).toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
                    </span>
                  </td>
                  <td>
                    <span>{{ row[0].payedDate.slice(0,10) }}</span>
                    <span class="ml-2">{{ row[0].payedDate.slice(11,19) }}</span>
                  </td>
                  <td>
                    <mdb-btn outline="warning" class="m-0 px-2 py-1" @click="show_item(row)" style="font-size: 10px;">{{$t('show')}}</mdb-btn>
                    <mdb-btn outline="primary" class="m-0 px-2 py-1 ml-2" @click="check_print(row)" style="font-size: 10px;">Чек</mdb-btn>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
    <ModalUser  :show="show_item_see" classes="bg_dolg" closeColor="white" titlecolor="white" 
        title="Чек" @close="show_item_see = false" width="80%">
        <template v-slot:body>
          <div class="p-2">
            <div class="TablePatientDocId p-3">
              <table class="myTable">
                <thead>
                  <tr class="header ">
                    <th  width="40" class="text-left">№</th>
                    <th width="70">{{$t('id')}}</th>
                    <th width="300">{{$t('patient_name')}}</th>
                    <th>{{$t('doctor_name')}}</th>
                    <th>{{$t('name')}}</th>
                    <th>{{$t('service_name')}}</th>
                    <th>{{$t('cash')}}</th>
                    <th>{{$t('card')}}</th>
                    <th>{{$t('payedDate')}}</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(row,i) in payment_item_data" :key="i" >
                    <td> <span >{{i+1}}</span> </td>
                    <td><span class="font-weight-bold">{{ row.patientsId }}</span></td>
                    <td><span>{{ row.patients.fio }}</span></td>
                    <td><span>{{ row.discount_card_qty }}</span></td>
                    <td><span>{{ row.patientName }}</span></td>
                    <td>
                      <span>{{ row.serviceName }}</span>
                    </td>
                    <td>
                      <span class="text-success ">
                        {{ row.paymentInCash.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
                      </span>
                    </td>
                    <td>
                      <span class="text-success ">
                        {{ row.paymentInCard.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
                      </span>
                    </td>
                    <td>
                      <span>{{ row.payedDate.slice(0,10) }}</span>
                      <span class="ml-2">{{ row.payedDate.slice(11,19) }}</span>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </template>
      </ModalUser>
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>
  import { mdbBtn, mdbInput, mdbIcon,  } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import 'vue2-datepicker/index.css';
  import ModalUser from '../../../components/modal.vue'
  import check from "../check.vue"
  export default {
    components: {
      mdbBtn, ModalUser, 
      mdbIcon, check,
      mdbInput
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
        discount_qty: 0,
        json_fields: {
          'Имя пациента': 'hospitalBronRoomPayments.reserved_name_1',
          'Имя врача': 'hospitalBronRoomPayments.reserved_name_3',
          'День': 'hospitalBronRoomPayments.reserved_number_1',
          'Сумма': 'summ',
        },  
        payment_item_data: [],
        show_item_see: false,
        check_show: false,
        checkIndex: 0,
        contragent_name_for_check: '',
      }
    },
    async mounted(){
      if(localStorage.Type == 0){
        this.admin = true;
      }
      {
        let time1 = new Date();
        this.Start_time = time1.toISOString().slice(0,10); 
        this.End_time = time1.toISOString().slice(0,10);
        let a = this.Start_time + 'T00:00:35.000Z' ;
        let b = this.End_time + 'T23:59:50.000Z';
        let c = {
          time1: '',
          time2: '',
        }
        c.time1 = a;
        c.time2 = b;
        this.loading = true;
        await this.fetch_report_by_data_time_check(c);
        this.get_payment_list = this.get_check_payment_list
        this.loading = false;
      }
    },
    computed: mapGetters(['get_contragent_list', 'get_report_by_data_time', 'get_report_by_time_card_cash',
     'get_check_payment_list', 'get_bron_cash_card', 'get_payment_payed_history_excel']),
    methods: {
      ...mapActions(['fetch_contragent', 'fetch_report_by_data_time_check', 'fetch_service_group', ]),
      ...mapMutations(['district_row_delete', 'Update_check_data','Update_cashbox_patient_info', 
        'update_patient_name', 'updateDebit', 'UpdatecheckInfo']),
      show_item(data){
        console.log(data)
        this.payment_item_data = data;
        this.show_item_see = true;
      },
      async check_print(data){
        if(data.length>0){
          await this.contragentId_forName(data[0].contragentId);
        }
        this.Update_cashbox_patient_info({name: data[0].patients.fio, id:data[0].patients.id})
        this.update_patient_name({name: data[0].patients.fio, born:data[0].patients.bornDate.slice(0,4), address:data[0].patients.address, contragent: this.contragent_name_for_check})
        this.updateDebit(data.reduce((acc, item) => acc + parseInt(item.dolg_summ), 0))
        this.UpdatecheckInfo({check:data[0].patients.id, cash: 0, card: 0, code:data[0].patients.workAddress})
        this.payment_item_data = data;
        this.Update_check_data(data);
        this.check_show = true;
        this.$refs.checkPrint_show.printed()
      },
      async close_service_check(){
        this.check_show = false;
        this.checkIndex = 0;
      },
      closeNext(){
        this.check_show = false;
        this.checkIndex ++;
        this.check_show = true;
        this.$refs.checkPrint_show.printed()
      },
      async contragentId_forName(id){
      console.log(id)
      try{
        const response = await fetch(this.$store.state.hostname + "/Contragents/" + id);
        const data = await response.json();
        console.log(data)
        if(response.status ==200 || response.status ==201){
          this.contragent_name_for_check = data.name;
        }
      }
      catch{
        console.log('error')
      }

    },
      add(){
        this.show =! this.show
        this.editData = {};
      },
      print(){
        window.print();
      },
      deleteRow(data, index){
        console.log(data)
        this.index = index
        this.service_id = data.id
        this.show = true;
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
            this.$refs.message.success('Successfully_removed')
            this.get_payment_list.splice(this.index, 1);
            this.show = false;
          }
      },
      async submit(){
        let a = {
          time1: "2021-09-01T09:15:28.886Z",
          time2: new Date(),
        }
        console.log('this.Start_time')
        console.log(this.Start_time)
        console.log(this.End_time)
        a.time1 = this.Start_time + 'T00:00:35.000Z';
        a.time2 = this.End_time + 'T23:59:50.000Z';
        this.loading = true;
        await this.fetch_report_by_data_time_check(a);
        this.get_payment_list = [];
        this.get_payment_list = this.get_check_payment_list
        this.loading = false;
      },

    },
  };
</script>

<style lang="scss" scoped>
  @import "../../../scss/tableAll.scss";
</style>
<template>
  <div >
    <checkbroncheck v-if="checkbron" @closeCheck="closeCheck"/>
   <div  v-else class="bg-white">
      <loader v-if="loading"/>
      <div class="py-0">
        <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
          <form @submit.prevent="submit">
            <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
              <div class="title row align-items-center" style="width: 100%;">
                <div class="col-4">
                  <mdb-input type="text" v-model="search" size="sm" :label="$t('search_here')" outline></mdb-input>
                </div>
                <div class="col-1 m-0 p-0">
                  <mdb-btn type="submit" color="primary py-2 px-4"  style="font-size:9px;" >
                    {{$t('search')}}
                  </mdb-btn>
                </div>
                <div class="col-7">
                  <mdb-btn @click="needpayedNotZero" color="danger py-2 px-4"  style="font-size:9px;" >
                    {{$t('need_payed')}}
                  </mdb-btn>
                  <mdb-btn @click="refresh" color="info py-2 px-4"  style="font-size:9px;" >
                    {{$t('all_patient')}}
                  </mdb-btn>
                  <mdb-btn @click="SrogiBugungilarLekinTolamagan" color="secondary py-2 px-4"  style="font-size:9px;" >
                    {{$t('bugun_srogi_kelganlar')}}
                  </mdb-btn>
                  <mdb-btn @click="SrogiOtganlar" color="warning py-2 px-4"   style="font-size:9px;" >
                    {{$t('srogi_utganlar')}}
                  </mdb-btn>
                </div>
              </div>
              <div class="plus" style="width: 5%;">
                <mdb-btn @click="$router.back()" color="primary py-2 px-4"  style="font-size:9px;" >
                  {{$t('back')}}
                </mdb-btn>
              </div>
            </div>
          </form>
          <div class="TablePatientBron mt-2">
              <table class="myTable">
                <thead class="rasxod_table_header">
                  <tr class="header">
                    <th width="40">№</th>
                    <th width="250">{{$t('patient')}}</th>
                    <th>Врач</th>
                    <th>{{$t('Room')}}</th>
                    <th >Комнаты</th>
                    <th width="50">День</th>
                    <th >{{$t('Start_time')}}</th>
                    <th >{{$t('End_time')}}</th>
                    <th >Цена за один день</th>
                    <th >Платная сумма</th>
                    <th >Возврат</th>
                    <th >Не оплатет сумма</th>
                    <th style="width: 200px;" class="text-end">{{$t('Action')}}</th>
                  </tr>
                </thead>
                <tbody class="rasxod_body">
                  <tr v-for="(row,rowIndex) in filteredList" :key="rowIndex" 
                     class="hover_bron_item">
                     <!-- :class="{'past_bron_payment': new Date(row.end_date_bron)<Today_time}" -->
                    <td> <span >{{rowIndex+1}}</span> </td>
                    <td> <span >{{row.reserved_name_1}}</span> </td>
                    <td> <span >{{row.users.FIO}}</span> </td>  
                    <td> <span >{{row.reserved_name_2}}</span> </td>
                    <td> <span>{{row.reserved_name_5}}</span> </td>   
                    <td> <span class="font-weight-bold">{{row.reserved_number_1}}</span> </td>   
                    <td> <span >{{row.begin_date_bron.slice(0,10)}}</span> </td>   
                    <td> <span >{{row.end_date_bron.slice(0,10)}}</span> </td>   
                    <td> <span >{{row.price_for_one_day.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>   
                    <td> <span class="text-success">{{row.payed_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>   
                    <td> <span class="text-secondary">{{row.reserved_number_db_5.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td> 
                    <td> <span class="text-danger font-weight-bold"> {{row.need_payed_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>   
                    <td class="text-end"> 
                      <mdb-btn color="warning" @click="check_show_prinent(row)" style="font-size: 9px;" class="px-2 py-1 ml-0">чек</mdb-btn> 
                      <mdb-btn color="success" @click="payed_payment(row)" style="font-size: 9px;" class="px-2 py-1 ml-0">{{$t('pay')}}</mdb-btn> 
                      <!-- <mdb-btn color="primary" v-show="row.need_payed_summ == 0 && row.finish_payment == false" @click="fetch_finish_payment(row.id)" :disabled="row.need_payed_summ != 0" style="font-size: 9px;" class="px-2 py-1 ml-0">{{$t('finish')}}</mdb-btn>  -->
                      <!-- <mdb-btn v-show="row.payed_summ == 0"  color="danger" @click="del_patient_payment(row)" class="my-0"  style="font-size: 9px;" p="r4 l4 t1 b1">{{$t('Delete')}}</mdb-btn>   -->
                      <mdb-btn v-show="row.payed_summ > 0"  color="secondary" @click="vozvrat_patient_payment(row)" class="my-0"  style="font-size: 9px;" p="r4 l4 t1 b1">Возврат</mdb-btn>  
                    </td>
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
          <!-- <anyTable
            :datasource="m_contragent"
            @page="page"
            @size="size"
            @pay="pay_debit"
            :debit="debit"
            /> -->
        </div>

      </vue-html2pdf>

        <ModalUser  :show="payment_bron_show" classes="bg_dolg" closeColor="white" titlecolor="white" 
          :title="$t('pay')" @close="payment_bron_show = false" width="600px">
          <template v-slot:body>
            <pay_cash :option = "payment_data" @close="payed_close"/>
          </template>
        </ModalUser>
        <ModalUser  :show="vozvrat_show" classes="bg_dolg" closeColor="white" titlecolor="white" 
          :title="$t('vozvrat')" @close="vozvrat_show = false" width="600px">
          <template v-slot:body>
            <div class="container-fluid">
              <div class="row">
                <div class="col-12 mb-1 border-bottom d-flex justify-content-between">
                  <span>{{$t('patient')}}</span>
                  <span>{{payment_data.reserved_name_1}}</span>
                </div>
                <div class="col-12 mb-1 border-bottom d-flex justify-content-between">
                  <span>День</span>
                  <span class="font-weight-bold">{{payment_data.reserved_number_1}}</span>
                </div>
                <div class="col-12 mb-1 border-bottom d-flex justify-content-between">
                  <span>Цена за один день</span>
                  <span>{{payment_data.price_for_one_day}}</span>
                </div>
                <div class="col-12 mb-1 border-bottom d-flex justify-content-between">
                  <span>Платная сумма</span>
                  <span class="text-success">{{payment_data.payed_summ}}</span>
                </div>
                <div class="col-12 mb-1 border-bottom d-flex justify-content-between">
                  <span>Не оплатет сумма</span>
                  <span class="text-danger">{{payment_data.need_payed_summ}}</span>
                </div>
                <div class="col-12 mb-1 border-bottom mt-4">
                  <mdb-input type="text" class="m-0 p-0" ref="vozvrat_days_ref" v-model="vozvrat_day" size="sm" :label="$t('vozvrat_days')" outline></mdb-input>
                </div>
                <div class="col-12 mb-1 border-bottom mt-3">
                  <mdb-input type="text" class="m-0 p-0 " v-model="vozvrat_sum" size="sm" :label="$t('vozvrat_sum')" outline></mdb-input>
                </div>
                <div class="col-12 text-right pt-2 mt-2 border-top">
                  <mdb-btn @click="func_vozvrat(payment_data.id)" color="primary py-2 px-4"  style="font-size:10px;" >
                    {{$t('save')}}
                  </mdb-btn>
                </div>
              </div>
            </div>
          </template>
        </ModalUser>
   </div>
   <Toast ref="message"></Toast>
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>
import checkbroncheck from './checkbron.vue'
import VueHtml2pdf from 'vue-html2pdf'
import { required } from 'vuelidate/lib/validators'
import ModalUser from '../../components/modal.vue'
import pay_cash from './pay_cash.vue'
  // import lineSelect from "../../components/lineSelect.vue";
  import { mdbBtn, mdbInput,   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      VueHtml2pdf,
      mdbInput, 
      ModalUser,
      pay_cash,
      checkbroncheck
    },
    data(){
      return{

        payment_bron_show: false,
        payment_data: {},

        vozvrat_show: false,
        vozvrat_day: null,
        vozvrat_sum: null,

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
        Today_time:new Date(),
        price: null,
        reason: '',
        loading: false,
        bonus_contragent_list: [],
        showen_Auth: false,
        checkbron: false,
        m_contragent :{
          rows: [],
          columns: ['patient_name', 'real_qty', 'created_date_time'],
          col : []
        },
      }
    },
    validations: {
      price: {required},
      contragent_name : {required}
    },
    async mounted(){
      this.needpayedNotZero();
      if(localStorage.AuthId === 0){
        this.showen_Auth = true
        console.log(localStorage.Auth)
      } 
    },
    computed:{
      ...mapGetters(['get_contragent_list', 'get_report_by_data_time', 'get_report_by_time_card_cash','get_pagination']),
      filteredList: function(){
          if(this.search)
          {
            return this.m_contragent.rows.filter((item)=>{
            console.log(item.reserved_name_1)
            console.log(this.search)
              return this.search.toLowerCase().split(' ').every(v => item.reserved_name_1.toLowerCase().includes(v))
            })
          }else
          {
            return this.m_contragent.rows;
          }
        }
    },
    methods: {
      ...mapActions(['fetch_contragent', 'fetch_report_by_data_time']),
      ...mapMutations(['district_row_delete', 'update_pagination_first', 'updatebronCheck']),
      cls_wnd(){
        this.price =  null;
        this.reason =  '';
        this.contragent_name = '';
        this.contragent_id = null;
      },
      payed_payment(item){
        console.log('item test');
        this.payment_bron_show = true;
        this.payment_data = item;
      },
      check_show_prinent(data){
        console.log('data items', data)
        this.updatebronCheck(data);
        setTimeout(() => {
          this.checkbron = true;
            
        }, 200);
      },
      async payed_close(){
        this.checkbron = true;
        this.payment_bron_show = false;
        await this.refresh();
        // this.$refs.message.success('Added_sly');
      },
      async closeCheck(){
        this.checkbron = false;
        this.payment_bron_show = false;
        await this.refresh();
      },
      async refresh(){
        const res = await fetch(this.$store.state.hostname + '/HospitalBronRoomPayments/getPaginationOnlyFinishFalse?page=0&size=400');
        const res_data = await res.json();
        console.log('res_data')
        console.log(res_data)
        this.m_contragent.rows = res_data.items_list;
        this.loading = false;
      },
      async needpayedNotZero(){
        const res = await fetch(this.$store.state.hostname + '/HospitalBronRoomPayments/getPaginationNeedpayedSummNotZero?page=0&size=1000');
        const res_data = await res.json();
        console.log('res_data')
        console.log(res_data)
        this.m_contragent.rows = res_data.items_list;
        this.loading = false;
      },
      async SrogiOtganlar(){
        const res = await fetch(this.$store.state.hostname + '/HospitalBronRoomPayments/getPaginationSrogiOtganlarBugungiSanaBoyichaTolovTugamaganOxirigacha?page=0&size=1000');
        const res_data = await res.json();
        console.log('res_data')
        console.log(res_data)
        this.m_contragent.rows = res_data.items_list;
        this.loading = false;
      },
      async SrogiBugungilarLekinTolamagan(){
        const res = await fetch(this.$store.state.hostname + '/HospitalBronRoomPayments/getPaginationSrogiBugungilarLekinTolamaganlar?page=0&size=1000');
        const res_data = await res.json();
        console.log('res_data')
        console.log(res_data)
        this.m_contragent.rows = res_data.items_list;
        this.loading = false;
      },
      async fetch_finish_payment(id){
        const res = await fetch(this.$store.state.hostname + '/HospitalBronRoomPayments/finishPayment?id=' + id);
        const res_data = await res.json();
        console.log('res_data')
        console.log(res_data)
        if(res.status == 200 || res.status == 201){
          this.refresh();
          this.$refs.message.success('Bemor xonadan chiqarildi');

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
              this.$refs.message.success('sly_removed')
              this.refresh();
            }
      },
      async vozvrat_patient_payment(item){
        console.log(item);
        this.vozvrat_show = true;
        this.payment_data = item;
        this.$refs.vozvrat_days_ref.focus();
      },
      async func_vozvrat(id){
        const res = await fetch(this.$store.state.hostname + '/HospitalBronRoomPayments/addPaymentVozvrat?payment_id=' + id + '&vozvrat_sum=' + this.vozvrat_sum + '&days=' + this.vozvrat_day + '&pay_auth=' + localStorage.AuthId);
        const res_data = await res.json();
        console.log('res_data')
        console.log(res_data)
        if(res.status == 200 || res.status == 201){
          this.refresh();
          this.vozvrat_show = false;
          this.$refs.message.success('Added_successfully')
        }
      },
      print(){
        this.$refs.listlar.generatePdf()
      },
    },
  };
</script>
<style scoped>
  @import "../../scss/tableAll.scss";
</style>

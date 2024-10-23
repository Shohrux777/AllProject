<template>
  <div >
   
   <div class="bg-white">
      <loader v-if="loading"/>
      <div class="py-0">
        <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
          <form @submit.prevent="submit">
            <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
              <div class="title w-75 row align-items-center">
                <div class="col-4">
                  <mdb-input type="text" v-model="search" size="sm" :label="$t('search_here')" outline></mdb-input>
                </div>
                <div class="col-1 m-0 p-0">
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
          <div class="TablePatientBron mt-2">
              <table class="myTable">
                <thead class="rasxod_table_header">
                  <tr class="header">
                    <th width="40">№</th>
                    <th width="270">{{$t('patient')}}</th>
                    <th>{{$t('Room')}}</th>
                    <th>Врач</th>
                    <th width="50">День</th>
                    <th >{{$t('Start_time')}}</th>
                    <th >{{$t('End_time')}}</th>
                    <th >Цена за один день</th>
                    <th >Платная сумма</th>
                    <th >Возврат</th>
                    <th >Не оплатет сумма</th>
                  </tr>
                </thead>
                <tbody class="rasxod_body">
                  <tr v-for="(row,rowIndex) in filteredList" :key="rowIndex" 
                     class="hover_bron_item">
                     <!-- :class="{'past_bron_payment': new Date(row.end_date_bron)<Today_time}" -->
                    <td> <span >{{rowIndex+1}}</span> </td>
                    <td> <span >{{row.reserved_name_1}}</span> </td>
                    <td> <span >{{row.reserved_name_2}}</span> </td>
                    <td> <span >{{row.users.FIO}}</span> </td>
                    <td> <span class="font-weight-bold">{{row.reserved_number_1}}</span> </td>   
                    <td> <span >{{row.begin_date_bron.slice(0,10)}}</span> </td>   
                    <td> <span >{{row.end_date_bron.slice(0,10)}}</span> </td>   
                    <td> <span >{{row.price_for_one_day.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>   
                    <td> <span class="text-success">{{row.payed_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>   
                    <td> <span class="text-success">{{row.reserved_number_db_5.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>   
                    <td> <span class="text-danger font-weight-bold"> {{row.need_payed_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>   
                  </tr>
                </tbody>
              </table>
            </div>
        </div>
      </div>
   </div>
   <Toast ref="message"></Toast>
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>
</template>

<script>

import { required } from 'vuelidate/lib/validators'
// import pay_cash from './pay_cash.vue'
  // import lineSelect from "../../components/lineSelect.vue";
  import { mdbBtn, mdbInput,   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      mdbInput, 

    },
    data(){
      return{

        payment_bron_show: false,
        payment_data: {},
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
      this.refresh();
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
     
     
      async closeCheck(){
        this.checkbron = false;
        this.payment_bron_show = false;
        await this.refresh();
      },
      async refresh(){
        this.m_contragent.rows = [];
        const res = await fetch(this.$store.state.hostname + '/HospitalBronRoomPayments/getPaginationOnlyFinishFalse?page=0&size=400');
        const res_data = await res.json();
        console.log('res_data')
        console.log(res_data)
        for(let i=0; i<res_data.items_list.length; i++){
          if(res_data.items_list[i].UsersId == localStorage.docId){
            this.m_contragent.rows.push(res_data.items_list[i])
          }
        }
        this.loading = false;
      },
     
    },
  };
</script>

<style scoped>
  @import "../../scss/tableAll.scss";
</style>
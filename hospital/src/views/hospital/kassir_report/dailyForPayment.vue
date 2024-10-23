<template>
  <div class="bg-white">
    <loader v-if="loading"/>
    <div class="">
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
        <div class="border-bottom">
          <h5 class="mx-1 pb-0 ">Дневники касса</h5>
        </div>
        <form @submit.prevent="submit">
          <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center ">
            <div class="title w-75 row align-items-center">
              <div  class="col-3">
                <div style="position: relative; margin-top: 30px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -11px; color: #757575;">
                    {{$t('start_time')}}
                  </small>
                  <mdb-input type="date" size="sm" v-model="Start_time" outline/>
                </div>
              </div>
              <div  class="col-3" >
                <div style="position: relative; margin-top: 30px;" v-show="admin"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -11px; color: #757575;">
                    {{$t('end_time')}}
                  </small>
                  <mdb-input type="date" size="sm" v-model="End_time" outline/>
                </div>
              </div>
              <div  class="col-4" >
                <RegSelect_casher
                  class="mt-0"
                  label="Кассир"
                  @select="users_func"
                  :options="get_auth_user_limit"
                  :selected="user_name"
                  :searching="true"
                />
              </div>
            </div>
            <div class="plus">
              <mdb-btn @click="print" color="info py-2 px-3"  style="font-size:10px;" >
                <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-printer mr-0" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                  <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                  <path d="M17 17h2a2 2 0 0 0 2 -2v-4a2 2 0 0 0 -2 -2h-14a2 2 0 0 0 -2 2v4a2 2 0 0 0 2 2h2" />
                  <path d="M17 9v-4a2 2 0 0 0 -2 -2h-6a2 2 0 0 0 -2 2v4" />
                  <rect x="7" y="13" width="10" height="8" rx="2" />
                </svg>
                {{$t('print')}}
              </mdb-btn>
              <mdb-btn v-show="admin" type="submit" color="primary py-2 px-3" style="font-size:10px;"  >
                <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-check" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                  <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                  <path d="M5 12l5 5l10 -10" />
                </svg>
                {{$t('apply')}}
              </mdb-btn>
            </div>
          </div>
        </form>
        <div class="all_price border-bottom pb-2"> 
          <div class="row mt-2">
            <div class="pr-0" style="width:20%;">
              <div class="price_all_item card">
                <div class="qty borderSolder py-2">
                    <span class="ml-3">Общий ( Нал + Плас)</span>
                    <div class="text-right px-3 mt-1">
                      <p>{{(card + cash + get_bron_cash_card.cash + get_bron_cash_card.card - get_payed_date_vozvrat_summa_kassir - get_money).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</p>
                    </div>
                  </div>
              </div>
            </div>
            <div class="pr-0" style="width:20%;">
              <div class="price_all_item card">
                <div class="qty borderSolder py-2">
                    <span class="ml-3">{{$t('cash')}}</span>
                    <div class="text-right px-3 mt-1">
                      <p>{{(cash + get_bron_cash_card.cash - get_payed_date_vozvrat_summa_kassir).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</p>
                    </div>
                  </div>
              </div>
            </div>
            <div class="pr-0" style="width:20%;">
              <div class="price_all_item card">
                <div class="qty borderSolder py-2">
                    <span class="ml-3">{{$t('card')}}</span>
                    <div class="text-right px-3 mt-1">
                      <p>{{(card + get_bron_cash_card.card).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</p>
                    </div>
                  </div>
              </div>
            </div>
            
            <div class="pr-0" style="width:20%;">
              <div class="price_all_item card">
                <div class="qty borderSolder py-2">
                    <span class="ml-3">РACXOДЫ </span>
                    <div class="text-right px-3 mt-1">
                      <p>{{get_money.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</p>
                    </div>
                  </div>
              </div>
            </div>
            <div class="" style="width:20%;">
               <div class="price_all_item card">
                <div class="qty borderSolder py-2">
                    <span class="ml-3">В кассе есть деньги</span>
                    <div class="text-right px-3 mt-1">
                      <p>{{(cash + get_bron_cash_card.cash - get_money - get_payed_date_vozvrat_summa_kassir).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</p>
                    </div>
                  </div>
              </div>
            </div>
          </div>
        </div>
        <div class="TablePatientDocIdset mt-1">
          <table class="myTableDailery">
            <thead class="bg_header_dalery">
              <tr class="header text-white ">
                <th >{{$t('service_group')}}</th>
                <!-- <th >{{$t('user')}}</th> -->
                <th >{{$t('date')}}</th>
                <th >{{$t('cash')}}</th>
                <th >{{$t('card')}}</th>
                <th>{{$t('qty')}}</th>
                <td> <span class="text-white">Общий</span> </td>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in get_daily_report_list_kassir" :key="rowIndex" class="myTableDailery_item">
                <td> <span >{{row.service_group}}</span> </td>
                <td> <span >{{row.date}}</span> </td>
                <td> <span >{{row.cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{row.card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{row.qty}}</span> </td>
                <td> <span >{{(row.cash + row.card).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
              </tr>
              
            </tbody>
            <tbody>
              <tr v-for="(item,itemIndex) in kunlik_report_list" :key="itemIndex"  class="myTableDailery_item"  
                :class="{ 'text-danger': item.hospitalServiceTypeGroupName == 'RETURNED',
               'text-warning': item.hospitalServiceTypeGroupName == 'REJECTED',
               'text-secondary': item.hospitalServiceTypeGroupName == 'VOZVRAT' }"
               v-show="item.hospitalServiceTypeGroupName == 'RETURNED' || item.hospitalServiceTypeGroupName == 'REJECTED'
                || item.hospitalServiceTypeGroupName == 'VOZVRAT'">
                <td>
                  <span v-if = "item.hospitalServiceTypeGroupName == 'RETURNED'" >РACXOДЫ</span> 
                  <span v-else-if = "item.hospitalServiceTypeGroupName == 'REJECTED'" >ОТКАЗ</span> 
                  <span v-else-if = "item.hospitalServiceTypeGroupName == 'VOZVRAT'" >Общий возвырат</span> 
                  <span v-else >{{item.hospitalServiceTypeGroupName}}</span>
                </td>
                <!-- <td> <span >{{item.authorization.users.fio}}</span> </td> -->
                <td> <span >{{item.createdDateTime.slice(0,10)}}</span> </td>
                <td> <span >{{item.cashSumm.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{item.cardSumm.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{item.count}}</span> </td>
                <td> <span >{{(item.cashSumm + item.cardSumm).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
              </tr>
              <tr v-if="rasxod_list_month.length" class="myTableDailery_item">
                <td>
                  <span class="text-danger">РACXOДЫ</span>
                </td>
                <td> <span >{{rasxod_list_month[0].registratedDate.slice(0,10)}}</span> </td>
                <td> <span class="text-danger">{{get_money.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >0</span> </td>
                <td> <span ></span> </td>
                <td> <span class="text-danger">{{get_money.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
              </tr>
              <tr v-if="get_payed_date_vozvrat_list_kassir.length" class="myTableDailery_item">
                <td>
                  <span class="text-danger">Прошлые возврат</span>
                </td>
                <td> <span v-if="get_payed_date_vozvrat_list_kassir[0].payedDate">{{get_payed_date_vozvrat_list_kassir[0].payedDate.slice(0,10)}}</span> </td>
                <td> <span class="text-danger">{{get_payed_date_vozvrat_summa_kassir.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >0</span> </td>
                <td> <span ></span> </td>
                <td> <span class="text-danger">{{get_payed_date_vozvrat_summa_kassir.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
              </tr>
              <tr class="myTableDailery_item">
                <td> <span class="text-success">Общий</span> </td>
                <td> <span ></span></td>
                <!-- <td> <span ></span></td> -->
                <td> <span  class="text-success">{{(cash).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span></td>
                <td> <span  class="text-success">{{card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span></td>
                <td> <span  class="text-success">{{qtys}}</span></td>
                <td> <span  class="text-success">{{(cash + card).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span></td>
              </tr>
              <!-- <tr >
                <td> <span class="text-success"></span>{{$t('cash')}} </td>
                <td> <span  class="text-primary">{{cash}} сум </span></td>
              </tr>
               <tr >
                <td> <span class="text-success"></span>{{$t('card')}} </td>
                <td> <span  class="text-primary">{{card}} сум </span></td>
              </tr>
              <tr >
                <td> <span class="text-success"></span>РACXOДЫ </td>
                <td> <span  class="text-primary">{{get_money}} сум </span></td>
              </tr>
              <tr >
                <td> <span class="text-success"></span>В кассе есть деньги </td>
                <td> <span  class="text-primary">{{cash - get_money}} сум </span></td>
              </tr> -->
            </tbody>
          </table>
        </div>

        <div class="TablePatientDocIdset mt-1">
          <table class="myTableDailery">
            <thead class="bg_header_dalery">
              <tr class="header text-white ">
                <th >{{$t('service_group')}}</th>
                <!-- <th >{{$t('user')}}</th> -->
                <th >{{$t('date')}}</th>
                <th >{{$t('cash')}}</th>
                <th >{{$t('card')}}</th>
                <th>{{$t('qty')}}</th>
                <td> <span class="text-white">Общий</span> </td>
              </tr>
            </thead>
            <tbody>
              <tr class="myTableDailery_item">
                <td> <span >Стационар</span> </td>
                <td> <span >{{this.Start_time}}</span> </td>
                <td> <span >{{get_bron_cash_card.cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{get_bron_cash_card.card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{get_bron_cash_card.qty}}</span> </td>
                <td> <span >{{(get_bron_cash_card.cash + get_bron_cash_card.card).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
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
         <div class="TablePatientDocIdset p-3">
          <table class="myTableDailery">
            <thead>
              <tr class="header ">
                <th >{{$t('service_group')}}</th>
                <!-- <th >{{$t('user')}}</th> -->
                <th >{{$t('date')}}</th>
                <th >{{$t('cash')}}</th>
                <th >{{$t('card')}}</th>
                <th>{{$t('qty')}}</th>
                <td> <span class="text-primary">Общий</span> </td>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in get_daily_report_list_kassir" :key="rowIndex" >
                <td> <span >{{row.service_group}}</span> </td>
                <td> <span >{{row.date}}</span> </td>
                <td> <span >{{row.cash}}</span> </td>
                <td> <span >{{row.card}}</span> </td>
                <td> <span >{{row.qty}}</span> </td>
                <td> <span >{{row.cash + row.card}}</span> </td>
              </tr>
              
            </tbody>
            <tbody>
              <tr v-for="(item,itemIndex) in kunlik_report_list" :key="itemIndex"   
              :class="{ 'text-danger': item.hospitalServiceTypeGroupName == 'RETURNED',
               'text-warning': item.hospitalServiceTypeGroupName == 'REJECTED',
               'text-secondary': item.hospitalServiceTypeGroupName == 'VOZVRAT' }"
               v-show="item.hospitalServiceTypeGroupName == 'RETURNED' || item.hospitalServiceTypeGroupName == 'REJECTED'
                || item.hospitalServiceTypeGroupName == 'VOZVRAT'">
                <td> 
                  <span v-if = "item.hospitalServiceTypeGroupName == 'RETURNED'" >РACXOДЫ</span> 
                  <span v-else-if = "item.hospitalServiceTypeGroupName == 'REJECTED'" >ОТКАЗ</span> 
                  <span v-else-if = "item.hospitalServiceTypeGroupName == 'VOZVRAT'" >Возвырат</span> 
                  <span v-else >{{item.hospitalServiceTypeGroupName}}</span> 
                </td>
                <!-- <td> <span >{{item.authorization.users.fio}}</span> </td> -->
                <td> <span >{{item.createdDateTime.slice(0,10)}}</span> </td>
                <td> <span >{{item.cashSumm}}</span> </td>
                <td> <span >{{item.cardSumm}}</span> </td>
                <td> <span >{{item.count}}</span> </td>
                <td> <span >{{item.cashSumm + item.cardSumm}}</span> </td>
              </tr>
              <tr >
                <td> <span class="text-success">Общий</span> </td>
                <td> <span ></span></td>
                <!-- <td> <span ></span></td> -->
                <td> <span  class="text-success">{{cash - get_money}}</span></td>
                <td> <span  class="text-success">{{card}}</span></td>
                <td> <span  class="text-success">{{qtys}}</span></td>
                <td> <span  class="text-success">{{cash + card - get_money}}</span></td>
              </tr>
              <tr >
                <td> <span class="text-success"></span>{{$t('cash')}} </td>
                <td> <span  class="text-primary">{{cash}} сум </span></td>
              </tr>
               <tr >
                <td> <span class="text-success"></span>{{$t('card')}} </td>
                <td> <span  class="text-primary">{{card}} сум </span></td>
              </tr>
              <tr >
                <td> <span class="text-success"></span>РACXOДЫ </td>
                <td> <span  class="text-primary">{{get_money}} сум </span></td>
              </tr>
              <tr >
                <td> <span class="text-success"></span>В кассе есть деньги </td>
                <td> <span  class="text-primary">{{cash - get_money}} сум </span></td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>

    </vue-html2pdf>

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
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>
  import VueHtml2pdf from 'vue-html2pdf'
  import RegSelect_casher from '../../../components/hospital/UserlineSelect.vue'
  import districtAdd from "../../../components/new_prog_add/district_add"
  import { mdbBtn, mdbInput,  mdbModal, mdbModalHeader,  mdbModalTitle, mdbModalBody,   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, mdbInput,
      districtAdd,
      VueHtml2pdf,
      RegSelect_casher
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
        admin: false,
        kunlik_report_list: [],
        loading: false,
        cash: 0,
        card: 0,
        qtys: 0,
        allmoney: 0,
        get_money: 0,
        rasxod_list_month: [],

        user_name: '',
        user_id: localStorage.AuthId,
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
        await this.fetch_report_by_kassir_data_time(c);
        await this.fetch_report_by_bron_payed(c);
        await this.fetch_payed_date_vozvrat_list_kassir(c);
        this.loading = false;
        console.log(this.get_daily_report_list_kassir)
        this.cash = 0;
        this.card = 0;
        this.qtys = 0;
        this.get_daily_report_list_kassir.forEach((item)=>{
          this.cash += item.cash
          this.card += item.card
          this.qtys += item.qty
        })
      }
      {
          this.kunlik_report_list = [];
          let timed1 = this.Start_time + 'T00:00:35.000Z'
          let timed2 = this.End_time + 'T23:59:35.000Z'
          const responsed = await fetch(this.$store.state.hostname + '/ReturnMoneys/getReturnMoneyListBeatwenDateTimeAndKassirId?beginDate=' + timed1 + 
          '&endDate=' + timed2 + '&kassirId=' + this.user_id)
          const dataJson = await responsed.json()
          console.log('dataList items')
          console.log(dataJson)
          this.get_money = 0;
          for(var i = 0; i < dataJson.length; i++) {
            this.get_money += dataJson[i].price;
          }
          this.rasxod_list_month = dataJson
          console.log(dataJson)
        }

        
        // console.log(this.get_report_by_data)
    },
    computed: mapGetters(['get_contragent_list', 'get_report_by_data', 'get_report_by_time_card_cash',
     'get_report_by_data_time','get_daily_report_list_kassir', 'get_bron_cash_card', 'get_payed_date_vozvrat_summa_kassir',
      'get_payed_date_vozvrat_list_kassir', 'get_auth_user_limit', 'get_payed_date_vozvrat_summa_kassir_obshey']),
    methods: {
      ...mapActions(['fetch_contragent', 'fetch_report_by_data', 'fetch_report_by_kassir_data_time', 
        'fetch_report_by_bron_payed', 'fetch_payed_date_vozvrat_list_kassir', 'fetch_auth_list']),
      ...mapMutations(['dibet_delite_cont']),
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
          this.loading = true;
          {

          let a = this.Start_time + 'T00:00:00.000Z' ;    
          let b = this.End_time + 'T23:59:59.000Z';
          let c = {
            time1: '',
            time2: '',
            contId: this.user_id
          }
          c.time1 = a;
          c.time2 = b;
          this.loading = true;
          await this.fetch_report_by_kassir_data_time(c);
          await this.fetch_report_by_bron_payed(c);
          await this.fetch_payed_date_vozvrat_list_kassir(c);
          this.loading = false;
          console.log(this.get_daily_report_list_kassir)
          this.cash = 0;
          this.card = 0;
          this.qtys = 0;
          this.get_daily_report_list_kassir.forEach((item)=>{
            this.cash += item.cash
            this.card += item.card
            this.qtys += item.qty
        })
        }
        {
          this.kunlik_report_list = [];
          let timed1 = this.Start_time + 'T00:00:35.000Z'
          let timed2 = this.End_time + 'T23:59:35.000Z'
          const responsed = await fetch(this.$store.state.hostname + '/ReturnMoneys/getReturnMoneyListBeatwenDateTimeAndKassirId?beginDate=' + timed1 + 
          '&endDate=' + timed2 + '&kassirId=' + this.user_id)
          const dataJson = await responsed.json()
          console.log('dataList items')
          console.log(dataJson)
          this.get_money = 0;
          for(var i = 0; i < dataJson.length; i++) {
            this.get_money += dataJson[i].price;
          }
          this.rasxod_list_month = dataJson
          console.log(dataJson)
        }

      },

    },
  };
</script>

<style lang="scss" scoped>
  @import "../../../scss/tableAll.scss";
</style>

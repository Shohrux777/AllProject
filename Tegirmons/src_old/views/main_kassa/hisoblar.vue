<template>
  <div class="bg-white">
    <loader v-if="loading"/>
    <div v-else class="main_kassa_">
      <div class="p-2" style="border-radius:5px; position:relative;">
        <form @submit.prevent="submit">
          <div style="height: 45px;" class="d-flex justify-content-between border-bottom pb-1 align-items-center  ">
            <div class="title w-75 row align-items-center">
               <div class="col-3">
                <div style="position: relative; margin-top: 25px;"> 
                  <!-- <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
                    {{$t('start_time')}}
                  </small> -->
                  <mdb-input type="date" size="sm"  v-model="Start_time" outline/>
                </div>
              </div>
              <div class="col-3">
                <div style="position: relative; margin-top: 25px;">
                  <!-- <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
                    {{$t('end_time')}}
                  </small> -->
                  <mdb-input type="date" size="sm"   v-model="End_time" outline/>
                </div>
              </div>
              <div class="col-2 p-0">
                <mdb-btn @click="apply" color="primary py-2 px-3" style="font-size:9px;">
                  <mdb-icon style="font-size:9.5px;" icon="check" class="m-0 p-0 mr-1" />
                  {{$t('apply')}}
                </mdb-btn>
              </div>
              
            </div>
            <div class="plus d-flex">
              <mdb-btn v-show="false" @click="$router.push('/main_kassa_report')" color="secondary py-2 px-3" style="font-size:9px;">
                <mdb-icon style="font-size:9.5px;" icon="calendar" class="m-0 p-0 mr-1" />
                {{$t('report')}}
              </mdb-btn>

              <download-excel
                  class="bg-info rounded px-2"
                  style="margin-top:6px; cursor:pointer; height: 29px;"
                  :data = "get_payment_list"
                  :fields = "json_fields"
                  :before-generate = "startDownload"
                  :before-finish   = "finishDownload"
                  name="Ежедневные_отчет.xls">
                  <small class="text-white ml-0" style="font-size: 12px;">
                      <mdb-icon icon="file-excel" class="m-0 p-0 mr-1"></mdb-icon>
                      Excel
                  </small>
              </download-excel>


              <div class="select_hisob_info d-flex align-items-center">
                <div class="kassa_num_user mr-2">
                  <small> {{hisob_name[0]}}</small>
                </div>
                <small>{{hisob_name}}</small>
                <div class="hisob_info_list">
                  <p v-for="(item,i) in hisob_list_access" :key="i" @click="select_hisob_info(item)">
                    {{ item.name }}
                  </p>
                </div>
              </div>
            </div>
          </div>
        </form>
        <div class="header_info_main_kassa  container-fluid mt-0">
          <div class="row">
            <div class="col-6 m-0 pl-1 main_kassa_asosiy_summa_depart">
              <div class=" pt-1 pb-1 px-3">
                <div class="border-bottom">
                  <span style="color:#8697BA; font-style: italic; font-size: 14px;">Kun boshida kassada bulgan summa
                  </span>
                </div>
                <div class="d-flex justify-content-start mt-2 mb-2">
                  <div class="main_kassa_hisoblar main_kassa_bg">
                    <small>
                      Наличные 
                    </small>
                    <p class="summ_title">
                      {{ yest_day_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
                    </p>
                  </div>
                  <div class="main_kassa_hisoblar ml-3 main_kassa_bg">
                    <small>
                      Доллор 
                    </small>
                    <p class="summ_title">
                      {{ yest_day_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} $
                    </p>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-6 m-0 pl-1 main_kassa_asosiy_summa_depart">
              <div class=" pt-1 pb-1 px-3">
                <div class="border-bottom">
                  <span style="color:#8697BA; font-style: italic; font-size: 14px;">Kun oxirida kassada bulgan summa
                  </span>
                </div>
                <div class="d-flex justify-content-start mt-2 mb-2">
                  <div class="main_kassa_hisoblar">
                    <small>
                      Наличные 
                    </small>
                    <p class="summ_title ">
                      {{ today_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
                    </p>
                    <!-- <div class="d-flex justify-content-between">
                      <small class="summ_title_diff">
                        <i class="fa-solid fa-arrow-trend-up"></i>
                        50 000 000
                      </small>
                    </div> -->
                  </div>
                  <div class="main_kassa_hisoblar ml-3">
                    <small>
                      Доллор 
                    </small>
                    <p class="summ_title">
                      {{today_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} $
                    </p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="worker_name_search_main_kassa container-fluid mt-2 mb-0">
          <div class="row">
            <div class="col-4 m-0 pl-1">
              <div style="position:relative">
                <input type="text"  placeholder="Сотрудник" class="input_style form-control" 
                  @input="search_worker_name" v-model="worker_name_str" 
                  style="height: 32px;">
                <!-- <small style="position:absolute; top:-17px; left: 0px; font-size: 11px; color: rgb(62, 62, 139);" class="font-weight-bold px-2 py-0">Сотрудник</small> -->
              </div>
            </div>
            <div class="col-4 m-0 pl-1">
              <div style="position:relative">
                <input type="text"  placeholder="Cумма" class="input_style form-control" 
                  @keyup="funcAll_summ_str($event.target.value)" v-model="all_summ_str" 
                  style="height: 32px;">
                <!-- <small style="position:absolute; top:-17px; left: 0px; font-size: 11px; color: rgb(62, 62, 139);" class="font-weight-bold px-2 py-0">Сотрудник</small> -->
              </div>
            </div>
            <div class="col-4 m-0 pl-1">
              <div style="position:relative">
                <input type="text"  placeholder="Примечание" class="input_style form-control" 
                  @input="search_note" v-model="note_str" 
                  style="height: 32px;">
                <!-- <small style="position:absolute; top:-17px; left: 0px; font-size: 11px; color: rgb(62, 62, 139);" class="font-weight-bold px-2 py-0">Сотрудник</small> -->
              </div>
            </div>
          </div>
        </div>
        <div class="workermainKassaTable p-0 pt-2 px-1">
          <table class="mainKassaTable">
            <thead style="background-color: #C4DEE4; position:sticky; top: -10px;">
              <tr class="header">
                <th  width="40" class="text-left">№</th>
                <th >{{$t('worker')}}</th>
                <!-- <th >{{$t('born_date')}}</th> -->
                <th >{{$t('summ')}}</th>
                <th >UZS</th>
                <th >USD</th>
                <th width="90">Курс</th>
                <th >Кассир</th>
                <th >{{$t('date')}}</th>
                <th >{{$t('note')}}</th>

              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in get_payment_list" :key="rowIndex" 
                :class="{'text_success': row.status_rasxod == 1, 'text_danger': row.status_rasxod == 0}"
                style="cursor:pointer;"
                @click="showRasxodItem(row)">
                <td> <small style="font-size: 11.5px;">{{rowIndex+1}}</small> </td>
                <td> <small style="font-size: 11.5px;">{{row.worker_name}}</small> </td>
                <!-- <td> <small style="font-size: 11.5px;" v-if="row.passport_number">{{row.passport_number.slice(8,10) + '-' + row.passport_number.slice(5,7) + '-' + row.passport_number.slice(0,4)}}</small> </td>  -->
                <td> <small style="font-size: 11.5px;" :class="{'text-success': row.status_rasxod == 1, 'text-danger': row.status_rasxod == 0}">{{row.all_summ_string}}</small> </td>
                <td v-if="row.status_rasxod == 1"> <small style="font-size: 11.5px;" class="text-success">{{row.image_url}}</small> </td>
                <td v-else> <small style="font-size: 11.5px;" class="text-danger">{{row.reserve}}</small> </td>
                <td> <small style="font-size: 11.5px;" :class="{'text-success': row.status_rasxod == 1, 'text-danger': row.status_rasxod == 0}">{{row.dollor_string}}</small> </td>
                <td> <small style="font-size: 11.5px;" v-if="row.kurs">{{row.kurs.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                <td> <small style="font-size: 11.5px;">{{row.addiotionala_information}}</small> </td>
                <td> <small style="font-size: 11.5px;" v-if="row.created_date">{{row.created_date.slice(8,10) + '-' + row.created_date.slice(5,7) + '-' + row.created_date.slice(0,4)}}</small> <small class="ml-2">{{row.created_date.slice(11,16)}}</small> </td>
                <td> <small style="font-size: 11.5px;">{{row.note}}</small> </td>
              </tr>
            </tbody>
          </table>
        </div>
        <div class="main_kassa_btns border-top px-2 d-flex justify-content-between">
          <div class="main_statis_ d-flex">
            <div class="main_statis_item prixod_item mt-2 mr-1">
              <small class="small_static">
                Наличные
                <mdb-icon style="font-size:11px; position:absolute; right: -12px; top:0px;" icon="sort-down" class="text-success" />
              </small>
              <p class="m-0 p_static">
                {{ prixod_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
              </p>
            </div>
            <div class="main_statis_item prixod_item mt-2 mr-1">
              <small class="small_static">
                Доллор
                <mdb-icon style="font-size:11px; position:absolute; right: -12px; top:0px;" icon="sort-down" class="text-success" />
              </small>
              <p class="m-0 p_static">
                {{ prixod_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} $
              </p>
            </div>
            <div class="main_statis_item rasxod_item mt-2 mr-1 ml-3">
              <small class="small_static">
                Наличные
                <mdb-icon style="font-size:11px; position:absolute; right: -12px; top:4px;" icon="sort-up" class="text-danger" />
              </small>
              <p class="m-0 p_static">
                {{ rasxod_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
              </p>
            </div>
            <div class="main_statis_item rasxod_item mt-2 mr-1">
              <small class="small_static">
                Доллор
                <mdb-icon style="font-size:11px; position:absolute; right: -12px; top:4px;" icon="sort-up" class="text-danger" />
              </small>
              <p class="m-0 p_static">
                {{ rasxod_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} $
              </p>
            </div>
          </div>
          <div class="d-flex justify-content-end">
            <div class="main_kassa_btn bg_col_info" v-show="false" @click="show_send_savdo_kassa">
              <small>Савдо кассага пул кучириш</small>
            </div>
            <div class="main_kassa_btn bg_col_blue" @click="pul_olib_qolish = !pul_olib_qolish">
              <small>Получать деньги</small>
            </div>
            <div class="main_kassa_btn bg_col_red px-4" @click="rasxod_show = !rasxod_show">
              <small>Расходъ</small>
            </div>
          </div>
        </div>
      </div>
    </div>


    <modal-train  :show="show_rasxod_item" headerbackColor="white"  titlecolor="black" title="Ежедневные инфо" 
        @close="show_rasxod_item = false" width="32%">
          <template v-slot:body>
            <info-rasxod @close="show_rasxod_item = false" v-if="show_rasxod_item"  :option="show_rasxod_data" >
            </info-rasxod>
          </template>
    </modal-train>

    <modal-train  :show="rasxod_show" headerbackColor="#fc4640"  titlecolor="black" :title="$t('rasxod') + ' (' + hisob_name + ' )'" 
      @close="rasxod_show = false" width="65%">
      <template v-slot:body>
        <rasxod @close="closeRasxod" ref="rasxodWorkerSum" :hisob_id="parseInt(hisob_id)" :hisob_name="hisob_name">
        </rasxod>
      </template>
    </modal-train>

  <modal-train  :show="pul_olib_qolish" headerbackColor="#009587"  titlecolor="black" :title="$t('pul_olish') + ' (' + hisob_name + ' )'" 
    @close="pul_olib_qolish = false" width="65%">
      <template v-slot:body>
        <chiqarPulOlish @close="closePulChiqish" :hisob_id="parseInt(hisob_id)" :hisob_name="hisob_name"  ref="prixodWorkerSum">
        </chiqarPulOlish>
      </template>
  </modal-train>


  <modal-train  :show="send_kassa_status" headerbackColor="info"  titlecolor="black" title="Savdo kassaga pul o'tkazish" 
    @close="send_kassa_status = false" width="30%">
      <template v-slot:body>
        <send-main-kassa @close="closeSendKassa" ref="savdo_kassa_send">
        </send-main-kassa>
      </template>
  </modal-train>
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status = false"/>
      <block ref="blocked"></block>
  </div>
  

</template>

<script>
  import rasxod from './hisoblarRasxod.vue'
  import chiqarPulOlish from './hisoblarPrixod.vue'

  import infoRasxod from "../report/workerRasxod_info.vue"
  import { mdbBtn, mdbBadge, mdbInput, mdbIcon } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  import SendMainKassa from './sendMainKassa.vue';
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      mdbInput, 
      mdbBadge,
      mdbIcon,
      infoRasxod,
      rasxod,
      chiqarPulOlish,
      SendMainKassa
    },
    data(){
      return{
        hisob_name: 'S',
        hisob_id: null,
        rasxod_show: false,
        pul_olib_qolish: false,
        send_kassa_status: false,

        yest_day_cash: 0,
        yest_day_dollor: 0,
        yest_day_date: '',
        today_cash: 0,
        today_dollor: 0,

        show_rasxod_item: false,
        show_rasxod_data: {},
        admin:false,
        loading:false,
        modal_info : '',
        modal_status: false,

        Start_time: null,
        End_time: null,
        get_payment_list: [],
        worker_name: '',

        rasxod: 0,
        prixod: 0,
        all_summ_rasxod: 0,
        dollor_rasxod: 0,
        all_summ_prixod: 0,
        dollor_prixod: 0,

        all_summ_diff: 0,
        rasxod_diff: 0,
        dollor_diff: 0,

        worker_name_str: '',
        all_summ_str: '',
        note_str: '',

        prixod_cash: 0,
        prixod_dollor: 0,

        rasxod_cash: 0,
        rasxod_dollor: 0,

        hisob_list_access: [],

        json_fields: {
          'Сотрудник': 'worker_name',
          'Дата	': {
            field: "created_date",
            callback: (value) => {
              if(value)
                return  value.slice(8,10) + '-' + value.slice(5,7) + '-' + value.slice(0,4) + '  ' + value.slice(11,16);
              else return ``;
            },
          },
          
          'Приход':{
            callback: (value) => {
              if(value.status_rasxod>0){
                return value.all_summ_string;
              }
              else {
                return ``;
              }
            },
          },
          'Расходь':{
            callback: (value) => {
              if(value.status_rasxod == 0){
                return value.all_summ_string;
              }
              else {
                return ``;
              }
            },
          },
          'UZS': {
            callback: (value) => {
              if(value.status_rasxod>0){
                return value.image_url;
              }
              else if(value.status_rasxod == 0){
                return value.reserve;
              }
              else if(value.status_rasxod == -1){
                return value.reserve;
              }
              else {
                return ``;
              }
            },
          },
          'USD':'dollor_string',
          'Курс': 'kurs',
          'Кассир': 'addiotionala_information',
          'Примечание': 'note',
        },

      }
    },
    async mounted(){
        let time1 = new Date();
        this.Start_time = time1.toISOString().slice(0,10);
        this.End_time = time1.toISOString().slice(0,10);
        await this.fetchUserHisobAccess();

        this.loading = true;
        // await this.fetchHisob();
        let count_access = 0
        for(let j=0; j<this.hisob_list_access.length; j++){
          if(this.hisob_list_access[j].id == localStorage.hisob_Id){
            count_access ++;
          }
        }
        if(localStorage.hisob_Id && count_access>0){
          this.hisob_name = localStorage.hisob_Name;
          this.hisob_id = localStorage.hisob_Id;
        }
        else{
          if(this.hisob_list_access.length>0){
            this.hisob_name = this.hisob_list_access[0].name;
            this.hisob_id = this.hisob_list_access[0].id;
            localStorage.hisob_Name = this.hisob_list_access[0].name;
            localStorage.hisob_Id = this.hisob_list_access[0].id;
          }
        }
        await this.fetchHisob_info(this.hisob_id);
          this.yest_day_cash = this.hisob_info.before_cash;
          this.yest_day_dollor = this.hisob_info.before_dollor;
          this.today_cash = this.hisob_info.cash;
          this.today_dollor = this.hisob_info.dollor;
          await this.fetchPrixodRasxodList();
          await this.fetchTodayPrixodKassa();
          await this.fetchTodayRasxodKassa();
        this.loading = false;
    },
   
    computed: mapGetters(['get_contragent_list', 'allWorker', 'allHisob', 'hisob_info']),
    methods: {
      ...mapActions(['fetch_contragent', 'fetchWorker', 'fetchHisob', 'fetchHisob_info']),
      ...mapMutations(['district_row_delete',]),
      async fetchUserHisobAccess(){
        try{
            const res = await fetch(this.$store.state.hostname + '/TegirmonUserHisobAccess/GetHisoblarByUserId?user_id=' + localStorage.user_id);
            const data = await res.json();
            if(res.status == 200 || res.status == 201){
                this.hisob_list_access = data;
                if(data.length == 0){
                  this.$refs.blocked.show_block();
                }
            }
        }
        catch(error){
            console.log(error)
            // this.$refs.blocked.show_block();
        }
      },
      async select_hisob_info(hisob_data){
        this.hisob_name = hisob_data.name;
        this.hisob_id = hisob_data.id;
        localStorage.hisob_Name = hisob_data.name;
        localStorage.hisob_Id = hisob_data.id;
        await this.fetchHisob_info(hisob_data.id);
          this.yest_day_cash = this.hisob_info.before_cash;
          this.yest_day_dollor = this.hisob_info.before_dollor;
          this.today_cash = this.hisob_info.cash;
          this.today_dollor = this.hisob_info.dollor;
        await this.apply();
      },

      async closePulChiqish(){
        this.pul_olib_qolish = false;
        await this.apply();
      },
      async closeRasxod(){
        this.rasxod_show = false;
        await this.apply();
      },

      // bu qismi hozircha kerak emas
      async closeSendKassa(){
        this.send_kassa_status = false;
        await this.apply();
      },
      async show_send_savdo_kassa(){
        this.$refs.savdo_kassa_send.clw_cl();
        this.send_kassa_status = true;
      },
      // bu qismi hozircha kerak emas

      // vaqt buyiha hisobot info oldingi kun
      async fetchStartBeforeHisobot(){
        this.yest_day_cash = 0;
        this.yest_day_dollor = 0;
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.Start_time + 'T23:59:35.000Z';
        try{
          // this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonHisoblarInfo/getPaginationByBeatWeenTwoDateKassaInfoHisobID?page=0&size=10&begin_date=" + b_date + "&end_date=" + e_date + "&hisob_id=" + this.hisob_id);
          // this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            this.yest_day_cash += data.items_list[0].before_cash;
            this.yest_day_dollor += data.items_list[0].before_dollor;
            console.log(data ,  'before')
          }
        }
        catch(error){
          console.log(error);
        }
      },
      async fetchEndLastHisobot(){
        this.today_cash = 0;
        this.today_dollor = 0;
        let b_date  = this.End_time + 'T00:00:35.000Z';
        let e_date  = this.End_time + 'T23:59:35.000Z';
        try{
          // this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonHisoblarInfo/getPaginationByBeatWeenTwoDateKassaInfoHisobID?page=0&size=10&begin_date=" + b_date + "&end_date=" + e_date + "&hisob_id=" + this.hisob_id);
          // this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            this.today_cash += data.items_list[0].cash;
            this.today_dollor += data.items_list[0].dollor;
            console.log(data ,  'last')
          }
        }
        catch(error){
          console.log(error);
        }
      },

      async fetchPrixodRasxodList(){
        this.get_payment_list = [];
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.End_time + 'T23:59:35.000Z';
        try{
          // this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonHisoblarRasxod/getPaginationWorkerListByDate?page=0&size=100000&begin_date=" + b_date + "&end_date=" + e_date + "&hisob_id=" + this.hisob_id);
          // this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            console.log(data ,  'sadasd data open')
            this.get_payment_list = data.items_list;
            // this.$refs.message.success('Added_successfully')
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
      async fetchTodayPrixodKassa(){
        this.prixod_cash = 0;
        this.prixod_dollor = 0;
        this.all_summ_prixod = 0;
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.End_time + 'T23:59:35.000Z';
        try{
          // this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonHisoblarRasxod/getHisoblarPrixodSumma?begin_date=" + b_date + "&end_date=" + e_date + "&hisob_id=" + this.hisob_id);
          // this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            this.prixod_cash += data[0].cash;
            this.prixod_dollor += data[0].dollor;
            this.all_summ_prixod += data[0].all_summ;
            console.log(data ,  'today prixod data 22')
          }
        }
        catch(error){
          console.log(error);
        }
      },
      async fetchTodayRasxodKassa(){
        this.rasxod_cash = 0;
        this.rasxod_dollor = 0;
        this.all_summ_rasxod = 0;
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.End_time + 'T23:59:35.000Z';
        try{
          // this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonHisoblarRasxod/getHisoblarRasxodSumma?begin_date=" + b_date + "&end_date=" + e_date + "&hisob_id=" + this.hisob_id);
          // this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            this.rasxod_cash += data[0].cash;
            this.rasxod_dollor += data[0].dollor;
            this.all_summ_rasxod += data[0].all_summ;
            console.log(data ,  'today rasxod data')
          }
        }
        catch(error){
          console.log(error);
        }
      },

      async funcAll_summ_str(n){
        var tols = ''
        for(let i=0; i<n.length; i++){
          if(n[i] != ' '){
            tols += n[i];
          }
        }
        this.all_summ_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        if(this.all_summ_str == null || this.all_summ_str == ''){
          await this.apply();
          return;
        }
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.End_time + 'T23:59:35.000Z';
        try{
          const response = await fetch(this.$store.state.hostname + "/TegirmonHisoblarRasxod/getPaginationByAll_summ?page=0&size=10000&begin_date=" + b_date + "&end_date=" + e_date + '&all_summ_string=' + this.all_summ_str + "&hisob_id=" + this.hisob_id);
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            // console.log(data ,  'sadasd data open')
            this.get_payment_list = data.items_list;
          }
        }
        catch(error){
          console.log(error)
        }
      },
      async search_worker_name(){
        if(this.worker_name_str == null || this.worker_name_str == ''){
          await this.apply();
          return;
        }
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.End_time + 'T23:59:35.000Z';
        try{
          const response = await fetch(this.$store.state.hostname + "/TegirmonHisoblarRasxod/getPaginationByWorker_name?page=0&size=10000&begin_date=" + b_date + "&end_date=" + e_date + '&worker_name_str=' + this.worker_name_str + "&hisob_id=" + this.hisob_id);
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            // console.log(data ,  'sadasd data open')
            this.get_payment_list = data.items_list;
          }
        }
        catch(error){
          console.log(error)
        }
      },
      async search_note(){
        if(this.note_str == null || this.note_str == ''){
          await this.apply();
          return;
        }
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.End_time + 'T23:59:35.000Z';
        try{
          const response = await fetch(this.$store.state.hostname + "/TegirmonHisoblarRasxod/getPaginationByNote?page=0&size=1000&begin_date=" + b_date + "&end_date=" + e_date + '&note_str=' + this.note_str + "&hisob_id=" + this.hisob_id);
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            // console.log(data ,  'sadasd data open')
            this.get_payment_list = data.items_list;
          }
        }
        catch(error){
          console.log(error)
        }
      },
     


      async showRasxodItem(data){
        this.show_rasxod_item = true;
        this.show_rasxod_data = data;
        console.log(data, 'test uchun')
      },
      async apply(){
        this.loading = true;
        await this.fetchStartBeforeHisobot();
        await this.fetchEndLastHisobot();
          await this.fetchPrixodRasxodList();
          await this.fetchTodayPrixodKassa();
          await this.fetchTodayRasxodKassa();
        this.loading = false;
        return;
        this.today_cash = 0;
        this.today_dollor = 0;
        this.rasxod = 0;
        this.prixod = 0;
        this.all_summ_rasxod = 0;
        this.dollor_rasxod = 0;
        this.all_summ_prixod = 0;
        this.dollor_prixod = 0;
        this.worker_name = '';

        this.all_summ_diff = 0;
        this.rasxod_diff = 0;
        this.dollor_diff = 0;
        
      },
      


      


      // excelga olishda bajariladigan funcsiya
      async startDownload(){
        let temp = {
          worker_name: '',
          all_summ_string : '',
          status_rasxod: -2,
          image_url: '',
          reserve: '',
          dollor_string: '',
          kurs: null,
          created_date: '',
          addiotionala_information: '',
          note: '',
        }
        this.get_payment_list.push(temp);
        let temp2 = {
          worker_name : 'Получать деньги',
          status_rasxod : '1',
          all_summ_string : this.all_summ_prixod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
          image_url : this.prixod_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
          reserve : '',
          dollor_string : this.prixod_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
          kurs: null,
          created_date: '',
          addiotionala_information: '',
          note: '',
        }
        this.get_payment_list.push(temp2);
        let temp1 = {
          worker_name : 'Расходъ',
          status_rasxod : '0',
          all_summ_string : this.all_summ_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
          image_url : '',
          reserve : this.rasxod_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
          dollor_string : this.rasxod_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
          kurs: null,
          created_date: '',
          addiotionala_information: ' ',
          note: '',
        }
        this.get_payment_list.push(temp1);
        let temp3 = {
          worker_name : 'Разница',
          status_rasxod : '0',
          all_summ_string : (this.all_summ_prixod - this.all_summ_rasxod).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
          image_url : '',
          reserve : (this.prixod_cash - this.rasxod_cash).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
          dollor_string : (this.prixod_dollor - this.rasxod_dollor).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
          kurs: null,
          created_date: '',
          addiotionala_information: ' ',
          note: '',
        }
        this.get_payment_list.push(temp3);
      },
      async finishDownload(){
        this.get_payment_list.splice(this.get_payment_list.length-1,1)
        this.get_payment_list.splice(this.get_payment_list.length-1,1)
        this.get_payment_list.splice(this.get_payment_list.length-1,1)
        this.get_payment_list.splice(this.get_payment_list.length-1,1)
      },


    },
  };
</script>

<style lang="scss" scoped>

::-webkit-scrollbar {
  width: 5px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #dfdfdf;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #abbdd7;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #212d3d;
}

.select_hisob_info{
  position: relative;
  height: 30px;
  margin-left: 10px;
  margin-top: 6px;
  padding: 8px 2px;
  z-index: 100;
  border-radius: 2px;
  &:hover .hisob_info_list{
    display: inline-block;
  }
}
.hisob_info_list{
  position: absolute;
  top: 30px;
  display: none;
  right: 0;
  width: 170px;
  padding: 5px 0;
  background: #fcfcfc;
  box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;
  border-radius: 3px;
  p{
    font-size: 12px;
    margin:0;
    padding: 4px 8px;
    cursor: pointer;

    &:hover{
      background: #c4d3e7;
    }
  }
}
.kassa_num_user{
  width: 30px;
  height: 30px;
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;
  color:rgb(255, 255, 255);
  font-size: 18px;
  background: #009587;
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
  .mainKassaTable {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; 
  overflow-x:auto
}
.mainKassaTable th{
  font-weight: 500;
  font-size:11px;
}
.mainKassaTable td{
  font-size:10px;
}
.mainKassaTable th, .mainKassaTable td {
  text-align: left;
  padding: 5px;
}

.mainKassaTable tr {
  border-bottom: 1px solid rgb(240, 240, 240);
}
.mainKassaTable tr {
  &:hover{
    background: #afd1fd !important;
  }
}
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
.text_danger_sum{
  font-size: 12px; 
  color:#E7515A;
}
.text_success_sum{
  font-size: 12px; 
  color:#009587;
}
.input_style{
  font-size: 13px !important;
  color: rgb(51, 51, 85) !important;
}
.main_kassa_{
  min-height: 100%;
  background: #fff;
}
.main_kassa_hisoblar{
  width:48%; 
  padding: 10px 20px;
  border-radius: 7px;
  background-image: radial-gradient( circle 502px at 2.7% 23.7%,  rgba(142,5,254,1) 0%, rgba(33,250,214,1) 99.6% );

  small{
    color:#f8f8ff;
    font-size: 14px;

  }
  .summ_title{
    color:#ffffff;
    font-size: 18px;
    text-align: right;
    padding-bottom: 0px;
    margin-bottom: 0px;
    margin-top: 5px;
  }
  .summ_title_diff{
    color:#ffffff;
    font-size: 14px;
    text-align: right;
    padding-bottom: 0px;
    margin-bottom: 0px;
    margin-top: 10px;
  }
}
.main_kassa_bg{
  //background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgba(239,87,87,0.74) 0%, rgba(245,123,13,0.74) 90.2% );
  background-image: linear-gradient( 90deg,  rgba(255,157,129,1) 24.3%, rgb(255, 110, 78) 78.3% );
}
.header_info_main_kassa{
  height: 120px;
}
.worker_name_search_main_kassa{
  height: 35px;
}
.workermainKassaTable{
  height: calc(100vh - 320px);
  overflow: none;
  overflow-y: scroll;
}

.main_kassa_btn{
  display: flex;
  justify-content: center;
  align-items: center;
  height: 40px;
  padding: 0 15px;
  color:#3a4b52;
  //background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgba(0,152,155,1) 0.1%, rgba(0,94,120,1) 94.2% );
  border-radius: 4px;
  cursor: pointer;
  margin-right: 7px;
  margin-top: 10px;
  small{
    font-size: 12px;
  }
}
.bg_col_blue{
  //border: 1.5px solid #009587;
  background: #009587;   
  color:white;

  &:hover{
    color:white;
    background: #009587;  
  }
  
}
.bg_col_red{
  //border: 1.5px solid #ff504a;
  background: #ff504a; 
  color:white;

  &:hover{
    color:white;
    background: #ff504a;  
  }
}
.bg_col_info{
  //border: 1.5px solid #4aaeff;
  background: #4ab1ff;
  color:white;

  &:hover{
    color:white;
    background: #4ab1ff;  
  }
}

.main_statis_item{
  box-shadow: rgba(17, 12, 46, 0.15) 0px 48px 100px 0px;
  border-radius: 4px;
  width: 11vw;
  .small_static{
    position:relative;
    margin-left: 5px;
    font-size: 11px;
    font-style: italic;
  }
  .p_static{
    text-align: right;
    font-size: 13px;
    padding:  0 5px;
  }
}
.prixod_item{
  background: hsla(152, 100%, 50%, 1);

  background: linear-gradient(90deg, hsla(152, 100%, 50%, 1) 0%, hsla(186, 100%, 69%, 1) 100%);

  background: -moz-linear-gradient(90deg, hsla(152, 100%, 50%, 1) 0%, hsla(186, 100%, 69%, 1) 100%);

  background: -webkit-linear-gradient(90deg, hsla(152, 100%, 50%, 1) 0%, hsla(186, 100%, 69%, 1) 100%);

  //filter: progid: DXImageTransform.Microsoft.gradient( startColorstr="#00FF87", endColorstr="#60EFFF", GradientType=1 );
}
.rasxod_item{
  background: hsla(0, 100%, 67%, 1);

  background: linear-gradient(90deg, hsla(0, 100%, 67%, 1) 0%, hsla(0, 100%, 89%, 1) 100%);
  
  background: -moz-linear-gradient(90deg, hsla(0, 100%, 67%, 1) 0%, hsla(0, 100%, 89%, 1) 100%);
  
  background: -webkit-linear-gradient(90deg, hsla(0, 100%, 67%, 1) 0%, hsla(0, 100%, 89%, 1) 100%);
  
 // filter: progid: DXImageTransform.Microsoft.gradient( startColorstr="#FF5858", endColorstr="#FFC8C8", GradientType=1 );
}

.text_success{
  background: #e6faeb;
}
.text_danger{
  background: #ffebeb;
}
</style>
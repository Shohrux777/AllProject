<template>
  <div class="bg-white">
    <div class="p-1" >
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
        <form @submit.prevent="submit">
          <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
            <div class="title w-75 row align-items-center">
               <div class="col-3">
                <div style="position: relative; margin-top: 25px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
                    {{$t('start_time')}}
                  </small>
                  <mdb-input type="date" size="sm"  v-model="Start_time" outline/>
                </div>
              </div>
              <div class="col-3">
                <div style="position: relative; margin-top: 25px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
                    {{$t('end_time')}}
                  </small>
                  <mdb-input type="date" size="sm"   v-model="End_time" outline/>
                </div>
              </div>
              <div class="col-5">
                <div class="">
                  <erpSelect
                  :options="allWorker.rows"
                  @select="selectOptionWorker"
                  :selected="worker_name"
                  size="sm"
                  class="bg-white"
                  style="margin-top:8px;"
                  url="/TegirmonWorker/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
                  label="Поиск сотрудник"
                  />
                  <!-- <small style="position:absolute; top:-8px; left:10px; font-size: 11px; color: rgb(62, 62, 139);" class="font-weight-bold px-2 py-0">Поиск сотрудник</small> -->
                  
                </div>
              </div>
              
            </div>
            <div class="plus d-flex">
              <mdb-btn @click="apply" color="primary py-2 px-3" style="font-size:9px;">
                <mdb-icon style="font-size:9.5px;" icon="check" class="m-0 p-0 mr-1" />
                {{$t('apply')}}
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
            </div>
          </div>
        </form>
        <div class="header_info_rasxod container-fluid mt-2">
          <div class="row">
            <div class="col-4 m-0 pl-1">
              <div class="border rounded pt-1 pb-1 px-3">
                <div class="border-bottom">
                  <span style="color:#0E1726;">Получать деньги
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-arrow-down" width="17" height="17" viewBox="0 0 24 24" stroke-width="3" stroke="#00b341" fill="none" stroke-linecap="round" stroke-linejoin="round">
                      <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                      <path d="M12 5l0 14" />
                      <path d="M18 13l-6 6" />
                      <path d="M6 13l6 6" />
                    </svg>
                  </span>
                  <p class="mt-0 text-right mb-1" style="font-size: 20px;">	{{all_summ_prixod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} <small>сум</small></p>
                </div>
                <div class="d-flex">
                  <div style="width: 50%" class="text-center">
                    <span style="font-size: 10px;">UZS</span>
                    <p class="m-0 font-weight-bold" style="font-size: 12px; color:#009587;">	{{prixod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} <small>сум</small></p>
                  </div>
                  <div style="width: 50%"  class="text-center">
                    <span style="font-size: 10px;">USD</span>
                    <p class="m-0 font-weight-bold" style="font-size: 12px; color:#009587;">	{{dollor_prixod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} <small>$</small></p>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-4 m-0 pl-1">
              <div class="border rounded pt-1 pb-1 px-3">
                <div class="border-bottom">
                  <span style="color:#0E1726;">Расходъ
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-arrow-narrow-up" width="17" height="17" viewBox="0 0 24 24" stroke-width="3" stroke="#ff2825" fill="none" stroke-linecap="round" stroke-linejoin="round">
                      <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                      <path d="M12 5l0 14" />
                      <path d="M16 9l-4 -4" />
                      <path d="M8 9l4 -4" />
                    </svg>
                  </span>
                  <p class="mt-0 text-right mb-1" style="font-size: 20px;">	{{all_summ_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} <small>сум</small></p>
                </div>
                <div class="d-flex">
                  <div style="width: 50%" class="text-center">
                    <span style="font-size: 10px;">UZS</span>
                    <p class="m-0 font-weight-bold" style="font-size: 12px; color:#E7515A;">	{{rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} <small>сум</small></p>
                  </div>
                  <div style="width: 50%"  class="text-center">
                    <span style="font-size: 10px;">USD</span>
                    <p class="m-0 font-weight-bold" style="font-size: 12px; color:#E7515A;">	{{dollor_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} <small>$</small></p>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-4 m-0 pl-1">
              <div class="border rounded pt-1 pb-1 px-3">
                <div class="border-bottom">
                  <span style="color:#0E1726;" v-if="all_summ_diff<0"> Разница 
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-arrow-narrow-up" width="17" height="17" viewBox="0 0 24 24" stroke-width="3" stroke="#ff2825" fill="none" stroke-linecap="round" stroke-linejoin="round">
                      <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                      <path d="M12 5l0 14" />
                      <path d="M16 9l-4 -4" />
                      <path d="M8 9l4 -4" />
                    </svg>
                  </span>
                  <span style="color:#0E1726;" v-else> Разница
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-arrow-down" width="17" height="17" viewBox="0 0 24 24" stroke-width="3" stroke="#00b341" fill="none" stroke-linecap="round" stroke-linejoin="round">
                      <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                      <path d="M12 5l0 14" />
                      <path d="M18 13l-6 6" />
                      <path d="M6 13l6 6" />
                    </svg>
                  </span>
                  <p class="mt-0 text-right mb-1" style="font-size: 20px !important;"
                  :class="{'text_success_sum': all_summ_diff>=0, 'text_danger_sum': all_summ_diff<0}"
                  >	{{all_summ_diff.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} <small>сум</small></p>
                </div>
                <div class="d-flex">
                  <div style="width: 50%" class="text-center">
                    <span style="font-size: 10px;">UZS</span>
                    <p class="m-0 font-weight-bold" :class="{'text_success_sum': rasxod_diff>=0, 'text_danger_sum': rasxod_diff<0}"
                    >	{{rasxod_diff.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} <small>сум</small></p>
                  </div>
                  <div style="width: 50%"  class="text-center">
                    <span style="font-size: 10px;">USD</span>
                    <p class="m-0 font-weight-bold" :class="{'text_success_sum': dollor_diff>=0, 'text_danger_sum': dollor_diff<0}"
                      >	{{dollor_diff.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} <small>$</small></p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="worker_name_search container-fluid mt-2 mb-0">
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
        <div class="workerRasxodTable p-0 pt-2 px-1">
          <table class="rasxodTable">
            <thead style="background-color: #C4DEE4;">
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
              <tr v-for="(row,rowIndex) in get_payment_list" :key="rowIndex" style="cursor:pointer;"
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
          <mdb-btn outline="danger" >{{$t('Yes')}}</mdb-btn>
          <mdb-btn color="danger" @click="show = false">{{$t('No')}}</mdb-btn>
        </mdb-modal-footer>
    </mdb-modal>
    <modal-train  :show="show_rasxod_item" headerbackColor="white"  titlecolor="black" title="Ежедневные инфо" 
        @close="show_rasxod_item = false" width="32%">
          <template v-slot:body>
            <info-rasxod @close="show_rasxod_item = false" v-if="show_rasxod_item"  :option="show_rasxod_data" >
            </info-rasxod>
          </template>
      </modal-train>
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status = false"/>
  </div>
</template>

<script>
  import erpSelect from "../../components/erpSelectFioSearch.vue";
  import infoRasxod from "../report/workerRasxod_info.vue"
  import { mdbBtn, mdbBadge, mdbInput, mdbIcon,  mdbModal, mdbModalHeader,   mdbModalBody,mdbModalFooter   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      mdbModal, mdbModalHeader, mdbIcon, mdbModalBody, mdbModalFooter,
       mdbInput, mdbBadge,
       erpSelect, infoRasxod
    },
    data(){
      return{
        user_id: 0,
        show_rasxod_item: false,
        show_rasxod_data: {},
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
        await this.fetchWorker();
        await this.apply();
    },
   
    computed: mapGetters(['get_contragent_list', 'allWorker']),
    methods: {
      ...mapActions(['fetch_contragent', 'fetchWorker']),
      ...mapMutations(['district_row_delete',]),
      async showRasxodItem(data){
        this.show_rasxod_item = true;
        this.show_rasxod_data = data;
        console.log(data, 'test uchun')
      },
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
          image_url : this.prixod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
          reserve : '',
          dollor_string : this.dollor_prixod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
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
          reserve : this.rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
          dollor_string : this.dollor_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
          kurs: null,
          created_date: '',
          addiotionala_information: ' ',
          note: '',
        }
        this.get_payment_list.push(temp1);
        let temp3 = {
          worker_name : 'Разница',
          status_rasxod : '0',
          all_summ_string : this.all_summ_diff.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
          image_url : '',
          reserve : this.rasxod_diff.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
          dollor_string : this.dollor_diff.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
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
      async apply(){
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
        // console.log('dasdas')
        // let newdate = new Date().toISOString().slice(0,10);
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.End_time + 'T23:59:35.000Z';
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonMainKassaRasxod/getPaginationWorkerListByDate?page=0&size=100000&begin_date=" + b_date + "&end_date=" + e_date);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            console.log(data ,  'sadasd data open')

            this.get_payment_list = data.items_list;
             for(let i=0; i<data.items_list.length; i++){
              this.rasxod += parseFloat(data.items_list[i].rasxod);
              this.prixod += parseFloat(data.items_list[i].prixod);
              if(data.items_list[i].status_rasxod == 0){
                this.all_summ_rasxod += parseFloat(data.items_list[i].all_summ);
                this.dollor_rasxod += parseFloat(data.items_list[i].dollor);
              }
              else{
                this.all_summ_prixod += parseFloat(data.items_list[i].all_summ);
                this.dollor_prixod += parseFloat(data.items_list[i].dollor);
              }
            }
            this.all_summ_diff = this.all_summ_prixod - this.all_summ_rasxod;
            this.rasxod_diff = this.prixod - this.rasxod;
            this.dollor_diff = this.dollor_prixod - this.dollor_rasxod;
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
      async selectOptionWorker(option){
        console.log(option)
        this.rasxod = 0;
        this.prixod = 0;
        this.all_summ_rasxod = 0;
        this.dollor_rasxod = 0;
        this.all_summ_prixod = 0;
        this.dollor_prixod = 0;
        this.worker_name = option.fio;

        this.worker_name_str = '';
        this.all_summ_str = '';
        this.note_str = '';

        this.all_summ_diff = 0;
        this.rasxod_diff = 0;
        this.dollor_diff = 0;
        // console.log('dasdas')
        // let newdate = new Date().toISOString().slice(0,10);
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.End_time + 'T23:59:35.000Z';
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonMainKassaRasxod/getPaginationWorkerListByWorkerId?page=0&size=10000&begin_date=" + b_date + "&end_date=" + e_date + '&worker_id=' + option.id);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            console.log(data ,  'sadasd data open')

            this.get_payment_list = data.items_list;
             for(let i=0; i<data.items_list.length; i++){
              this.rasxod += parseFloat(data.items_list[i].rasxod);
              this.prixod += parseFloat(data.items_list[i].prixod);
              if(data.items_list[i].status_rasxod == 0){
                this.all_summ_rasxod += parseFloat(data.items_list[i].all_summ);
                this.dollor_rasxod += parseFloat(data.items_list[i].dollor);
              }
              else{
                this.all_summ_prixod += parseFloat(data.items_list[i].all_summ);
                this.dollor_prixod += parseFloat(data.items_list[i].dollor);
              }
            }
            this.all_summ_diff = this.all_summ_prixod - this.all_summ_rasxod;
            this.rasxod_diff = this.prixod - this.rasxod;
            this.dollor_diff = this.dollor_prixod - this.dollor_rasxod;
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
          const response = await fetch(this.$store.state.hostname + "/TegirmonMainKassaRasxod/getPaginationByAll_summ?page=0&size=10000&begin_date=" + b_date + "&end_date=" + e_date + '&all_summ_string=' + this.all_summ_str);
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
          const response = await fetch(this.$store.state.hostname + "/TegirmonMainKassaRasxod/getPaginationByWorker_name?page=0&size=10000&begin_date=" + b_date + "&end_date=" + e_date + '&worker_name_str=' + this.worker_name_str);
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
          const response = await fetch(this.$store.state.hostname + "/TegirmonMainKassaRasxod/getPaginationByNote?page=0&size=1000&begin_date=" + b_date + "&end_date=" + e_date + '&note_str=' + this.note_str);
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
      }
      
      
    },
  };
</script>

<style lang="scss">


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
.workerRasxodTable{
    // height: 400px;
    // overflow: hidden;
    // overflow-y: auto;
    // border: 1px solid #ddd;
  }
  .rasxodTable {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; 
  overflow-x:auto
}
.rasxodTable th{
  font-weight: 500;
  font-size:12px;
}
.rasxodTable td{
  font-size:10px;
}
.rasxodTable th, .rasxodTable td {
  text-align: left;
  padding: 5px;
}

.rasxodTable tr {
  border-bottom: 1px solid rgb(240, 240, 240);
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
  font-size: 13.5px !important;
  color: rgb(0, 0, 85) !important;
}
</style>
<template>
  <div class="zaxira"> 
    <div class="getProduct">
      <div class="container-fluid">
        <div class="row px-3 py-2 d-flex align-items-center main_header_bg_new" :class="{'bg_red_color': oshibka_client == 1}">
          <div class="col-3">
            <div class="">
              <erpSelect
              :options="get_user_list.rows"
              @select="selectOptionUser"
              :selected="user_name"
              size="sm"
              class="bg-white"
              style="margin-top:8px;"
              url="/SkudMyUserinfoes/getPaginationSearchByIsmOrPassportSerailNumberOrMobilePhoneNumberFaceUser?page=0&size=100&fio_or_serial_number="
              :label="$t('select_item')"
              />
              <small style="position:absolute; top:-8px; left:10px; font-size: 11px; color: rgb(62, 62, 139);" class="font-weight-bold px-2 py-0">Поиск клиент</small>
              <!-- <small v-if="$v.user_name.$dirty && user_id == null" class="invalid-text mt-0 ml-2" >
                {{$t('select_item')}}
              </small> -->
            </div>
          </div>
          <!-- <div class="col-3">
            <mdb-input class="m-0 p-0" v-model="passport_number" size="md"  outline  group type="text" validate error="wrong" success="right"/>
            <small style="position:absolute; top:-7px; left:20px; font-size: 11px;" class="bg-white px-2 py-0">{{$t('passport_number')}}</small>
        
          </div> -->
          <div class="col-3">
            <input-search KeyName="ism"  @select="selectClientPassport" :label="$t('passport_number')" :selected="passport_number"
              url="/SkudMyUserinfoes/getPaginationSearchByIsmOrPassportSerailNumberOrMobilePhoneNumberFaceUser?page=0&size=100&fio_or_serial_number="
              ref="search_client" placeholder="AA12345678"  style="height:32px;">
            </input-search>
          </div>
  
          <div class="col-3">
            <input-search-year KeyName="ism"  @select="selectClientBorn" type="text" :label="$t('born_date')" :selected="born_date" placeholder="дд-мм-гггг"
              url="/SkudMyUserinfoes/getPaginationSearchByIsmOrPassportSerailNumberOrMobilePhoneNumberFaceUser?page=0&size=100&fio_or_serial_number="
              ref="search_client_born_date" style="height:32px;">
            </input-search-year>
          </div>
          <div class="col-3">
            <div class="d-flex justify-content-end">
              <div style="width:180px" class="mr-2" v-if="user_id">
                <div class="main_kassa_btn m-0 bg_col_info1" @click="show_user_reports">
                  <small>Ежемесячный отчет</small>
                </div>
              </div>
              <div style="width:150px">
                <div class="main_kassa_btn m-0 bg_col_info1" @click="$router.push('/kunlik_hisobot')">
                  <small>Ежедневный отчет</small>
                </div>
              </div>
            </div>
            
            
          </div>
  
        </div>
      </div>
      <div v-if="user_id == null">
        <Kunlik_hisobot  :comp_status="true"/>
      </div>
      <div v-else class="mainWorkChange">
        <div class="d-flex justify-content-between px-2">
            <div class="w-100 p-2">
                <div class="row">
                  <div class="col-5">
                    <div class="card" v-if="client_info.ism">
                      <div class="d-flex w-100">
                        <div class="user_img" v-if="client_info.image_url">
                          <img :src="hostname + client_info.image_url" alt="">
                        </div>
                        <div class="user_info_selected pt-4 px-3">
                          <h5 class="font-weight-bold" style="font-size: 14.5px;">{{client_info.ism}}</h5>
                          <p class="m-0 pt-1"><span class=" pr-1">ID:</span>  {{client_info.userid}}</p>
                          <p class="m-0"><span class=" pr-1">Номер паспорта:</span>  {{client_info.passport}}</p>
                          <p class="m-0 "><span class=" pr-1">Дата рождения:</span>  {{client_info.born_date}}</p>
                          <p class="m-0 "><span class=" pr-1">Тел:</span>  {{client_info.phone_number}}</p>
                        </div>
                      </div>
                      <div class="ishlagan_puli d-flex align-items-center justify-content-between py-2 px-5 border-top">
                          <h5 class="font-weight-bold m-0" style="font-size: 17px;">Balance :</h5>
                          <h5 v-if="user_oylik_info.length>0" class="font-weight-bold ml-3 m-0 text-primary" style="font-size: 25px;">{{ user_oylik_info[0].sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')  }} сўм</h5>
                          <h5 v-else class="font-weight-bold ml-3 m-0 text-primary" style="font-size: 25px;">0 сўм</h5>
                      </div>
                      <div class="ishlagan_puli d-flex align-items-center py-1 px-4 border-top">
                          <!-- <mdb-btn color="success" class="py-2 px-3" style="font-size: 10px;">
                            <i class="fas fa-download"></i>
                            Приход
                          </mdb-btn>
                          <mdb-btn color="danger" class="py-2 px-3" style="font-size: 10px;">
                            <i class="fas fa-share-square"></i>
                            Расход
                          </mdb-btn> -->
                          <div class="main_kassa_btn bg_col_blue" @click="pul_olib_qolish = !pul_olib_qolish">
                            <small>Получать деньги</small>
                          </div>
                          <div class="main_kassa_btn bg_col_red px-4" @click="rasxod_show = !rasxod_show">
                            <small>Расходъ</small>
                          </div>
                      </div>
                    </div>
<!-- v-if="oylik_data.reserved_value>1" -->
                    <div class="card mt-3" >
                      <div class="d-flex w-100">
                        <div class="user_info_selected pt-1 px-2 w-100">
                          <p class="m-0 pt-1 text-center">
                            <!-- <small style="font-size: 15px;" class="pr-1">Oylik turi:</small> -->
                            <small style="font-size: 17px; font-weight: bold;" class="pl-2">{{oylik_name}}</small> 
                          </p>
                          <div class="row container">
                            <div class="col-6 py-1 px-2 card rounded mt-2"  v-if="oylik_data.reserved_value == 1">
                              <small style="font-size: 15px; " class="pr-1">Kunlik summa:</small>
                              <div class="text-right">
                                <small style="font-size: 18px; font-weight: bold;" class="pl-2">{{oylik_value.toFixed(0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small>
                              </div>
                            </div>
                            <div v-else class="col-6 p-1">
                              <div class="py-1 px-2 border rounded mt-2">
                                <small style="font-size: 15px; color:dimgrey;" class="pr-1">Oylik summa:</small>
                                <div class="text-right">
                                  <small style="font-size: 18px; font-weight: bold; color:dimgrey;" class="pl-2">{{oylik_value.toFixed(0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small>
                                </div>
                              </div>
                            </div>
                            <div class="col-6 p-1" v-if="oylik_dollor">
                              <div class="py-1 px-2 border rounded mt-2">
                                <small style="font-size: 15px; color:dimgrey;" class="pr-1">Oylik dollorda:</small>
                                <div class="text-right">
                                  <small style="font-size: 18px; font-weight: bold; color:dimgrey;" class="pl-2">{{oylik_dollor.toFixed(0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small>
                                </div>
                              </div>
                            </div>
                          </div>
                          
                          <div class="row container">
                            
                            <div  class="col-6 p-1">
                              <div class="py-1 px-2 border rounded mt-1 mb-2">
                                <small style="font-size: 15px; color:dimgrey;" class="pr-1">Ish kunlari soni:</small>
                                <div class="text-right">
                                  <small style="font-size: 18px; font-weight: bold; color:dimgrey;" class="ml-2 px-2 alert-warning rounded">{{daysInSelectedMonth - 4}}</small>
                                </div>
                              </div>
                            </div>

                            <div class="col-6 p-1">
                              <div class="py-1 px-2 border rounded mt-1 mb-2">
                                <small style="font-size: 15px; color:dimgrey;" class="pr-1">Ishlagan kunlari soni:</small>
                                <div class="text-right">
                                  <small style="font-size: 18px; font-weight: bold; color:dimgrey;" class="ml-2 px-2 alert-success rounded">{{user_ishlagan_kunlari}}</small>
                                </div>
                              </div>
                            </div>
                          </div>
                          
                          
                        </div>
                      </div>
                      <div class="ishlagan_puli d-flex align-items-center justify-content-between py-2 px-5 border-top">
                          <h5 class="font-weight-bold m-0" style="font-size: 17px;">Hisoblangan summa :</h5>
                          <h5 v-if="user_hisoblangan_sum>0" class="font-weight-bold ml-3 m-0 text-primary" style="font-size: 25px;">{{ user_hisoblangan_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')  }} сўм</h5>
                          <h5 v-else class="font-weight-bold ml-3 m-0 text-primary" style="font-size: 25px;">0 сўм</h5>
                      </div>
                      
                    </div>
                  </div>
                  <!-- <camera/> -->
                  <div class="col-7">
                    <User_vaqt_info :client_info="client_info" @update_calendar="update_calendar" ref="user_vaqt_info_comp"/>
                  </div>
                </div>
            </div>
            <div >
                <calendar @choose_day="handleChooseDay1" @change_date="handleChooseDate" ref="user_salary_calendar" class="card mt-2"/>
            </div>
        </div>
      </div>

      
      
        <div class="photo w-100 mt-4 d-flex justify-content-center " v-if="image_url_str">
          <img :src="hostname + image_url_str" width="240" alt="" class="shadow border rounded">
        </div>
        
        <webcam  v-show="showPhoto" @getPhotosub="takePhoto"/>

      <hr v-if="user_id" class="mt-4 mb-3 gradint"/>
        <!-- <loaderFixed v-if="loading"/> -->
      <div v-if="user_id">
        <div class="px-0 userSalaryTable">
          <loader-table v-if="loading"/>
          <table v-else class="myTableuserSalaryList mt-2">
            <thead>
              <tr class="header py-3 info_client_header">
                <th  width="40" class="text-left">№</th>
                <th width="40" >ID</th>
                <th>{{$t('client_name')}}</th>
                <th>Kirish</th>
                <th>Chiqish</th>
                <th>Vaqt</th>
                <th>Summa</th>
                <th>Cтатус</th>
                <th>{{$t('note')}}</th>
                <!-- <th width="80" class="text-center">{{$t('Action')}}</th> -->
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in user_rasxod_prixod_list" :key="rowIndex" @click="selectInvoiceItem(row)" 
              :class="{'zero_item': row.sum == 0}">
                <td> <small >{{rowIndex+1}}</small> </td>
                <td> <small >{{row.userid}}</small> </td>
                <td> <small >{{client_info.ism}}</small> </td>
                <td> 
                  <small v-if="row.K_date">{{row.K_date.slice(8,10) + '-' + row.K_date.slice(5,7) + '-' + row.K_date.slice(0,4)}}</small> 
                  <small v-if="row.K_date" class="ml-2">{{row.K_date.slice(11,16)}}</small> 
                </td>
                <td> 
                  <small v-if="row.created_date">{{row.created_date.slice(8,10) + '-' + row.created_date.slice(5,7) + '-' + row.created_date.slice(0,4)}}</small> 
                  <small v-if="row.created_date" class="ml-2">{{row.created_date.slice(11,16)}}</small> 
                </td>
                <td> <small >{{row.work_time}}</small> </td>
                <td v-if="row.sum"> <span :class="{'text-success': row.num == 1, 'text-danger': row.num == 2, 'status_btn_cl': row.num == 3}" >{{row.sum.toFixed(0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>  
                <td  v-else> <span class="text-success">{{row.sum}}</span></td>
                <td v-if="row.num == 1"> <span class="bg-success px-3 text-white rounded" style="padding: 1px 6px;">Приход</span> </td>  
                <td v-else-if="row.num == 3"> <span class="status_btn_bg px-3 text-white rounded" style="padding: 1px 6px;">Получыть </span> </td>  
                <td  v-else> <span class="bg-danger px-3 text-white rounded" style="padding: 2px 10px;">Расход</span></td>
                
                <td> <small >{{row.note}}</small> </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
    <modal-train  :show="rasxod_show" headerbackColor="#fc4640"  titlecolor="black" :title="$t('rasxod')" 
        @close="rasxod_show = false" width="35%">
        <template v-slot:body>
          <rasxod @close="closeRasxod" :client_info="client_info"  ref="rasxodWorkerSum">
          </rasxod>
        </template>
      </modal-train>
      <modal-train  :show="pul_olib_qolish" headerbackColor="#009587"  titlecolor="black" :title="$t('pul_olish')" 
        @close="pul_olib_qolish = false" width="35%">
          <template v-slot:body>
            <chiqarPulOlish @close="closePulChiqish" :client_info="client_info" ref="prixodWorkerSum">
            </chiqarPulOlish>
          </template>
      </modal-train>
      <modal-train  :show="hisobot_show" headerbackColor="#009587"  titlecolor="black" :title="$t('Oylik Hisobot')" 
        @close="hisobot_show = false" width="75%">
          <template v-slot:body >
            <Oylik_hisobot :user_id = "user_id" :selected_date="select_month" ref="user_monthly_report"/>
          </template>
      </modal-train>
      <Toast ref="message"></Toast>

    <massage_box :hide="modal_status" :detail_info="modal_info"
    :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>
    <Alert ref="alert"></Alert> 
  </div>
</template>

<script>
import webcam from '../webcam/webcam_Add.vue'
import { mdbInput, mdbIcon,  mdbBtn } from "mdbvue"
// import { required } from 'vuelidate/lib/validators';
import {mapActions,mapGetters, mapMutations} from 'vuex';
import erpSelect from "../../components/erpSelectIsmSearch.vue";
import InputSearch from '../../components/inputSearch';
import calendar from './calendar.vue';

import inputSearchYear from '../../components/inputSearchYear';
import Camera from './avtoCamera.vue';
  import rasxod from './rasxod.vue'
  import chiqarPulOlish from './chiqarPulOlish.vue'
import Kunlik_hisobot from './kunlik_hisobot.vue';
import User_vaqt_info from './user_vaqt_info.vue';
import Oylik_hisobot from './oylik_hisobot.vue';



export default {
data(){
    return{
      hostname: this.$store.state.server_ip,
      modal_info: '',
      modal_status: false,  
      loading: false,
      loading_table: false,

      rasxod_show: false,
      pul_olib_qolish: false,
      hisobot_show: false,
      
      allList: [],

      showPhoto: false,
      image_url_str: '',

      passport_number: '',
      born_date: '',
      phone_number: '',
      user_name: '',
      user_id:null,

      usercheck_in_out: [],
      user_oylik_info: [],

      
      client_info_show: false,
      client_info: {},

      oshibka_client: 0,
      davernost_client_id: 0,
      show_check_get: false,

      show_check_zaxira_invoice: false,
      edit_davernost: false,
      edit_davernost_obj: {},
      parol_tg: '',
      useQty: 0,
      TgString: '',
      TgStringSumma: '',
      ostatka_qty_real: 0,
      check_number: 0,
      product_status: false,
      main_product_measure: '',
      user_rasxod_prixod_list: [],

      oylik_name: '',
      oylik_value: 0,
      oylik_dollor: 0,
      oylik_data: {},
      select_month: '',
      choosen_day: '',
      user_hisoblangan_sum: 0,
      user_ishlagan_kunlari: 0,
      month_days: 0,

      dollor_kurs: 0,
      dollor_kurs_qty: 0,

    }
  },
  components: {
    mdbIcon,
    mdbBtn, 
    mdbInput, 
    webcam,
    erpSelect,
    InputSearch,
    inputSearchYear,
    calendar,
    Camera,
    rasxod,
    chiqarPulOlish,
    Kunlik_hisobot,
    User_vaqt_info,
    Oylik_hisobot
  },
//   validations: {
      
//     },
    async mounted() {
      await this.fetch_user();
      await this.nbuKurs();
      let today = new Date();
      this.select_month = today.toISOString().slice(0, 10);
    },
   computed: {...mapGetters(['get_user_list',]),
    daysInSelectedMonth() {
      const date = new Date(this.select_month);
      const year = date.getFullYear();
      const month = date.getMonth(); // 0-based
      return new Date(year, month + 1, 0).getDate();
    },
   },
    
  methods: {
    ...mapActions(['fetch_user',]),
    ...mapMutations(['check_invoice_zaxira']),


    async show_user_reports(){
      this.hisobot_show = !this.hisobot_show;
      this.$refs.user_monthly_report.fetchUserOylikReport();
    },

    async get_user_rasxod_prixod_list(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonUserIshlaganPuli/getPaginationUserTulovList?page=0&size=1000&userid=" + this.user_id);
        const data = await response.json();
        console.log('rasxod_prixod',data.items_list)
        if(response.status == 200 || response.status == 201){
          this.user_rasxod_prixod_list = data.items_list;
        }
        else{
          this.user_rasxod_prixod_list = [];
        }
      }
      catch(error){
        this.user_rasxod_prixod_list = [];
        this.$refs.message.error("Foydalanuvchida ma'lumot topilmadi");
        console.log(error)
      }
    },
    async closeRasxod(){
      this.rasxod_show = false;
      await this.get_user_rasxod_prixod_list();
      await this.fetchuseroylik()

    },
    async closePulChiqish(){
      this.pul_olib_qolish = false;
      await this.get_user_rasxod_prixod_list();
      await this.fetchuseroylik()

    },
    async handleChooseDay1(data){
      this.choosen_day = data;
      console.log('bu yerga keldi choosen_day', data)
      this.$refs.user_vaqt_info_comp.handleChooseDay(data);
    },
    async handleChooseDate(data){
      this.select_month = data;
      await this.fetchUserIshlaganVaqti();
    },
    async fetchUserIshlaganPuli(){
      this.user_hisoblangan_sum = 0;
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonUserIshlaganPuli/getUserWorkedDaysReports?page=0&size=100&userid=" + this.user_id + '&month=' + this.select_month);
        const data = await response.json();
        console.log('ishlagan pul list',data)
        if(response.status == 200 || response.status == 201){
          this.user_ishlagan_puli = data.items_list;
          if(this.oylik_data.reserved_value == 1){
            this.user_hisoblangan_sum = this.user_ishlagan_puli.reduce((summa, item)=>{
              return summa + item.sum;
            }, 0)
          }
          console.log(this.user_hisoblangan_sum);
        }
        else{
          this.user_ishlagan_puli = [];
        }
      }
      catch(error){
        this.user_ishlagan_puli = [];
        // this.$refs.message.error("Foydalanuvchida " + formatted + " ushbu kun uchun ma'lumot topilmadi");
        console.log(error)
      }
    },
    async fetchUserIshlaganVaqti(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonUserIshlaganVaqt/getUserWorkedDays?page=0&size=200&userid=" + this.user_id + '&month=' + this.select_month);
        const data = await response.json();
        console.log('ishlagan vaqti list',data)
        if(response.status == 200 || response.status == 201){
          this.user_ishlagan_kunlari = data.items_list.length;
          if(this.oylik_data.reserved_value >1){ 
            let oy_days = this.daysInSelectedMonth;
            console.log('oy_days', oy_days)
            this.user_hisoblangan_sum = (this.user_ishlagan_kunlari * (this.oylik_value / (oy_days-4))).toFixed();
          }
          
        }
        else{
          this.user_ishlagan_puli = [];
        }
      }
      catch(error){
        this.user_ishlagan_puli = [];
        // this.$refs.message.error("Foydalanuvchida " + formatted + " ushbu kun uchun ma'lumot topilmadi");
        console.log(error)
      }
    },


    async fetchuseroylik(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonUserOylik/getPagination?page=0&size=10&userid=" + this.user_id);
        const data = await response.json();
        console.log('kirdi chiqdi',data)
        if(response.status == 200 || response.status == 201){
          this.user_oylik_info = data.items_list;
        }
        else{
          this.user_oylik_info = [];
        }
      }
      catch(error){
        this.user_oylik_info = [];
        // this.$refs.message.error("Foydalanuvchida " + formatted + " ushbu kun uchun ma'lumot topilmadi");
        console.log(error)
      }
    },
    

    //kerak
    async selectInvoiceItem(data){
      console.log(data);
    },

    //kerak
    takePhoto(img){
      this.image_url_str = img;
      this.showPhoto = false;
    },
    //kerak
    async selectClientPassport(option){
      this.client_info = option;
       this.user_name = option.ism;
      this.user_id = option.userid;
      await this.fetchuseroylik();
      this.$refs.user_salary_calendar.update_user_salary(option.userid);
      await this.get_user_rasxod_prixod_list()
        this.$refs.user_vaqt_info_comp.changeUser();
      console.log(option)
      console.log(option)
      if(option.res_badgenumber){
        await this.fetch_oylikId(option.res_badgenumber)
      }
      await this.fetchUserIshlaganVaqti();
      
    },

    async selectClientBorn(option){
      this.client_info = option;
      this.user_name = option.ism;
      this.user_id = option.userid;
      await this.fetchuseroylik();
      this.$refs.user_salary_calendar.update_user_salary(option.userid);
      await this.get_user_rasxod_prixod_list()
        this.$refs.user_vaqt_info_comp.changeUser();
      console.log(option)
      if(option.res_badgenumber){
        await this.fetch_oylikId(option.res_badgenumber)
      }
      await this.fetchUserIshlaganVaqti();
    },
    //kerak
    async selectOptionUser(option){
        console.log(option)
        this.user_name = option.ism;
        this.user_id = option.userid;
        this.client_info = option;
        await this.fetchuseroylik();
        this.$refs.user_salary_calendar.update_user_salary(option.userid);
        await this.get_user_rasxod_prixod_list()
        this.$refs.user_vaqt_info_comp.changeUser();
        if(option.res_badgenumber){
          await this.fetch_oylikId(option.res_badgenumber)
        }
      await this.fetchUserIshlaganVaqti();
    },
    async fetch_oylikId(oylik_id){
        try{
          const res = await fetch(this.$store.state.hostname + '/SkudOyliks/' + oylik_id);
          const data = await res.json();
          if(res.status == 200 || res.status == 201){
            console.log(data)
            this.oylik_name = data.name;
            this.oylik_value = data.value;
            this.oylik_data = data; 
            this.oylik_dollor = data.reserved_value2;
            if(data.reserved_value2 && this.dollor_kurs_qty){
              this.oylik_value = data.reserved_value2 * this.dollor_kurs_qty;
            }
            if(data.reserved_value == 1){
              await this.fetchUserIshlaganPuli();
            }
          }
        }
        catch(error){
            console.log(error)
        }
      },
    async update_calendar(){
      this.$refs.user_salary_calendar.update_user_salary(option.userid);
    },
    getPhoto(){
      this.showPhoto = true;
    },
    async nbuKurs(){
      try{
        const response = await fetch("https://cbu.uz/uz/arkhiv-kursov-valyut/json/");
        const data = await response.json();
        console.log('json valyuta')
        console.log(data)
        if(data.length>0){
          this.dollor_kurs = parseInt(data[0].Rate).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
          this.dollor_kurs_qty = parseInt(data[0].Rate);
        }
      }
      catch(error){
        console.log(error)
      }
    },
  },
}
</script>

<style lang="scss" scoped>

::-webkit-scrollbar {
  width: 5.5px;
  height: 2px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #f1f1f1;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #888;
  border-radius: 2px;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #555;
}
/* ! for scroll */
  .product_name_price{
    height: 55px;
  }
  .pro_name_color{
    color: #757575;
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
  padding: 10px;
}

.myTable tr {
  border-bottom: 1px solid rgb(240, 240, 240);
}

.myTable tr.header, .myTable tr:hover {
  // background-color: #f1f1f1;
}
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
.userSalaryTable{
  height: 300px;
  overflow: hidden;
  overflow-y: scroll;
  // border: 1px solid #757575;
}
.myTableuserSalaryList {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 16px;
  max-height:80px; overflow-x:auto
}
.myTableuserSalaryList th{
  font-weight: 600;
  font-size:11px;
}
.myTableuserSalaryList td{
  font-size:11.5px;
  
}
.myTableuserSalaryList tbody td small{
  font-size:11px;
}
.myTableuserSalaryList td {
  text-align: left;
  padding: 3px 10px;
}
.myTableuserSalaryList th{
  text-align: left;
  padding: 5px 10px;
}

.myTableuserSalaryList tr {
  border-bottom: 1px dashed rgb(240, 240, 240);
  &:hover{
    background: #afd1fd;
  }
}

.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
.editIcon{
  color:#01b348;
  font-size: 13px;
}
.mainWorkChange{
  // border: 1px solid black;
  min-height: calc(100vh - 500px);
}
.hover_info{
  display: none;
}

.border_dashed{
  border-bottom: 1px dashed #ddd;
}
.bg_red_color{
  background: #ff937d !important;
}
.bg_danger{
  background: #fd4925 !important;
}
.info_client_header{
  background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgb(200, 220, 230) 0%, rgb(196, 223, 228) 90.1% );
}
.zero_item{
  background: linear-gradient(to right, #ff698c, #ff6246);
}
.rounded{
  border-radius: 48%;
}
.user_info_card{
  height: 450px;
  overflow: hidden;
  overflow-y: scroll;
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
  padding: 7px;
}

.myTable tr {
  border-bottom: 1px solid rgb(240, 240, 240);
}
.user_img img{
  width: 100%;
  height: 150px;
}
.user_img{
  padding: 5px;
  cursor: pointer;
}
.user_info_selected p{
  color: #595961;
  font-size: 14px;
}
.main_kassa_btn{
  display: flex;
  justify-content: center;
  align-items: center;
  height: 34px;
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
.bg_col_info1{
  //border: 1.5px solid #009587;
  background: #006b95;   
  color:white;

  &:hover{
    color:white;
    background: #006b95;  
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
.status_btn_bg{
  background: #369387 !important;
}
.status_btn_cl{
  color: #369387 !important;
}
</style>
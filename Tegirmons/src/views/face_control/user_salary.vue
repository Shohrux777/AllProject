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
            <input-search  @select="selectClientPassport" :label="$t('passport_number')" :selected="passport_number"
              url="/SkudMyUserinfoes/getPaginationSearchByIsmOrPassportSerailNumberOrMobilePhoneNumberFaceUser?page=0&size=100&fio_or_serial_number="
              ref="search_client" placeholder="AA12345678"  style="height:32px;">
            </input-search>
          </div>
  
          <div class="col-3">
            <input-search-year  @select="selectClientBorn" type="text" :label="$t('born_date')" :selected="born_date" placeholder="дд-мм-гггг"
              url="/TegirmonClient/getPaginationByBornDate?page=0&size=100&born_date_str="
              ref="search_client_born_date" style="height:32px;">
            </input-search-year>
          </div>
          <div class="col-3">
            <input-search  @select="selectClientPhone" :label="$t('note')" :selected="phone_number"
              url="/TegirmonClient/getPaginationByNote?page=0&size=100&note_str="
              ref="search_client_phone" style="height:32px;" placeholder="1234">
            </input-search>
          </div>
  
        </div>
      </div>
      <div class="mainWorkChange">
        <div class="d-flex justify-content-between px-2">
            <div class="w-100 p-2">
                <div class="row">
                  <div class="col-5">
                    <div class="card w-75  py-2" v-if="client_info.ism">
                      <div class=" px-3" >
                        <p class="m-0" style="font-size: 14.5px;">
                          {{client_info.ism}}
                        </p>
                      </div>
                      <div class="text-right px-2 mt-2"  style="font-style: italic;" v-if="user_oylik_info.length>0">
                        <p class="m-0" style="font-size: 18px;">
                          {{user_oylik_info[0].sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} сўм
                        </p>
                      </div>
                      <div v-else class="text-right px-2 mt-2"  style="font-style: italic;" >
                        <p class="m-0" style="font-size: 18px;">
                          0 сўм
                        </p>
                      </div>
                    </div>
                  </div>
                  <camera/>
                  <div class="col-7" v-if="usercheck_in_out.length>0">
                    <div class="card user_info_card p-2">
                      <div class="user_info_header border-bottom px-2">
                        <p class="m-0" style="font-size: 14px;"
                          v-if="client_info">{{ client_info.ism }} ({{ choosen_day_date }} ) 
                          <span class="text-success">{{ choosen_day.hours }}</span></p>
                      </div>
                      <div class="border-bottom text-center px-3 alert-success" style="font-style: italic;">
                        <p class="m-0" style="font-size: 13px;">
                          Pul tushgan vaqtlari
                        </p>
                      </div>
                      <div v-if="user_ishlagan_puli_list.length>0">
                        <div class="TablePatientDocId p-0">
                          <table class="myTableuserSalaryList">
                            <thead class=" rounded" style="background:#C8DDE3">
                                <tr class="header">
                                  <th width="40">№</th>
                                  <th >Kirish</th>
                                  <th >Chiqish</th>
                                  <th >Vaqt</th>
                                  <th >Summa</th>
                              </tr>
                              </thead>
                            <tbody>
                              <tr v-for="(row, index) in user_ishlagan_puli_list" :key="index">
                                  <td >
                                    {{ index+1 }}
                                  </td>
                                  <td>
                                    {{row.k_date.slice(0,10)}} <span>{{ row.k_date.slice(11,16) }}</span>
                                  </td>
                                  <td>
                                    {{row.created_date.slice(0,10)}} <span>{{ row.created_date.slice(11,16) }}</span>
                                  </td>
                                  <td>{{row.work_time}}</td>
                                  <td>{{row.sum.toFixed()}}</td>
                              </tr>
                              </tbody>
                          </table>
                        </div>
                      </div>
                      <div v-else class=" text-center px-3" style="font-style: italic;">
                        <p class="m-0 text-danger" style="font-size: 13px;">
                          Ma'lumot topilmadi!
                        </p>
                      </div>
                      <div class=" text-center px-3 mt-2 alert-success border-bottom"  style="font-style: italic;">
                        <p class="m-0 " style="font-size: 13px;">
                          Xodimning kirdi chiqdi ma'lumotlari
                        </p>
                      </div>
                      <div v-if="usercheck_in_out.length>0">
                        <div class="TablePatientDocId p-0">
                          <table class="myTableuserSalaryList">
                            <thead class=" rounded" style="background:#C8DDE3">
                                <tr class="header">
                                  <th width="40">№</th>
                                  <th width="400">FIO</th>
                                  <th >Vaqt</th>
                                  <th >Status</th>
                              </tr>
                            </thead>
                            <tbody>
                              <tr v-for="(row, index) in usercheck_in_out" :key="index">
                                  <td >
                                    {{ index+1 }}
                                  </td>
                                  <td v-if="row.userinfo">
                                    {{ row.userinfo.ism }}
                                  </td>
                                  <td>
                                    {{row.sana.slice(0,10)}} <span>{{ row.checktime }}</span>
                                  </td>
                                  <td v-if="row.checktype == 'K'" class="text-success">Kirish</td>
                                  <td v-else class="text-primary">Chiqish</td>
                              </tr>
                            </tbody>
                          </table>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
            </div>
            <div >
                <calendar @choose_day="handleChooseDay" ref="user_salary_calendar" class="card mt-2"/>
            </div>
        </div>
      </div>

      
      
        <div class="photo w-100 mt-4 d-flex justify-content-center " v-if="image_url_str">
          <img :src="hostname + image_url_str" width="240" alt="" class="shadow border rounded">
        </div>
        
        <webcam  v-show="showPhoto" @getPhotosub="takePhoto"/>

      <hr class="mt-4 mb-3 gradint"/>
        <!-- <loaderFixed v-if="loading"/> -->
    

      <div>
        <div class="px-0 userSalaryTable">
          <loader-table v-if="loading"/>
          <table v-else class="myTableuserSalaryList mt-2">
            <thead>
              <tr class="header py-3 info_client_header">
                <th  width="40" class="text-left">№</th>
                <th width="40" >{{$t('id')}}</th>
                <th>{{$t('client_name')}}</th>
                <th>{{$t('passport_number')}}</th>
                <th>{{$t('product')}}</th>
                <th>{{$t('measure')}}</th>
                <th>{{$t('client_name')}}</th>
                <th width="100">{{$t('photo')}}</th>
                <th>{{$t('date')}}</th>
                <th >{{$t('ostatka')}}</th>
                <!-- <th width="80" class="text-center">{{$t('Action')}}</th> -->
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in allList" :key="rowIndex" @click="selectInvoiceItem(row)" :class="{'zero_item': row.qty_real == 0}">
                <td> <small >{{rowIndex+1}}</small> </td>
                <td> <small >{{row.check_number}}</small> </td>
                <td> <small >{{row.client.fio}}</small> </td>
                <td> <small >{{row.client.passport_number}}</small> </td>
                <td> <small >{{row.product.name}}</small></td>
                <td>
                  <small v-if="row.status_inv_type_name == 'INVOICE_BUGDOY_ZAXIRADAN_NARSALARGA_ALMASHTRISH'"><mdb-icon class="mr-2 text-danger" fas icon="caret-up"></mdb-icon></small> 
                  <small v-if="row.status_inv_type_name == 'INVOICE_BUGDOY_NARSALARGA_ALMASHTRISH_UCHUN_ZAXIRAGA_OLIB_QOLISH'"> <mdb-icon fas class="mr-2 text-success" icon="caret-down"></mdb-icon></small> 
                  <small v-if="row.qty_real">{{row.qty_real.toFixed(1).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> 
                  <small v-else>{{row.qty_real.toFixed(1)}}</small> 
                  <span v-if="row.status_inv_type_name == 'INVOICE_BUGDOY_ZAXIRADAN_NARSALARGA_ALMASHTRISH'">
                    <small v-if="row.summ != 0 "><mdb-icon icon="exchange-alt" class="ml-2" /> <mdb-icon class="ml-2" icon="dollar-sign" /></small>
                  </span>
                </td>
                <td><small>{{row.user_name}}</small></td>
                <td>
                  <img v-show="row.qty_real != 0" :src="hostname + row.image_str_url" class="rounded" width="50" alt="">
                </td>
                <td> 
                  <small >{{row.updated_date_time.slice(8,10) + '-' + row.updated_date_time.slice(5,7) + '-' + row.updated_date_time.slice(0,4)}}</small> 
                  <small class="ml-2">{{row.updated_date_time.slice(11,16)}}</small> 
                </td>
 
                <td v-if="row.credit_sum"> <span class="text-success" >{{row.credit_sum.toFixed(1).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>  
                <td  v-else> <span class="text-success">{{row.credit_sum.toFixed(1)}}</span> </td>  
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      
      <Toast ref="message"></Toast>
    </div>
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

export default {
data(){
    return{
      hostname: this.$store.state.server_ip,
      modal_info: '',
      modal_status: false,  
      loading: false,
      loading_table: false,
      
      allList: [],

      showPhoto: false,
      image_url_str: '',

      passport_number: '',
      born_date: '',
      phone_number: '',
      user_name: '',
      user_id:null,

      user_ishlagan_puli_list: [],
      usercheck_in_out: [],
      user_oylik_info: [],
      choosen_day: {},
      choosen_day_date: '',

      
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
    Camera
  },
//   validations: {
      
//     },
    async mounted() {
      await this.fetch_user();
    },
   computed: mapGetters(['get_user_list', ]),
    
  methods: {
    ...mapActions(['fetch_user',]),
    ...mapMutations(['check_invoice_zaxira']),

    async handleChooseDay(data){
      this.choosen_day = data;
      if(!this.user_id){
        this.$refs.message.error('not_found_user');
        return;
      };
      const formatted = data.date.getFullYear() + "-" +
      String(data.date.getMonth() + 1).padStart(2, '0') + "-" +
      String(data.date.getDate()).padStart(2, '0');

      this.choosen_day_date = formatted;
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonUserIshlaganPuli/getUserSalaryByDay?userid=" + this.user_id + '&date=' + formatted);
        const data = await response.json();
        console.log('puli',data)
        if(response.status == 200 || response.status == 201){
          this.user_ishlagan_puli_list = data;
        }
        else{
          this.user_ishlagan_puli_list = [];
        }
      }
      catch(error){
        this.user_ishlagan_puli_list = [];
        this.$refs.message.error("Foydalanuvchida " + formatted + " ushbu kun uchun ma'lumot topilmadi");
        console.log(error)
      }

      await this.getCheckInOut(formatted);

      console.log(formatted)
    },

    async getCheckInOut(date){
      try{
        const response = await fetch(this.$store.state.hostname + "/SkudMyCheckinouts/getPaginationGetByDateTimeByUserID?page=0&size=100&date=" + date + '&user_id=' + this.user_id);
        const data = await response.json();
        console.log('kirdi chiqdi',data)
        if(response.status == 200 || response.status == 201){
          this.usercheck_in_out = data.items_list;
        }
        else{
          this.usercheck_in_out = [];
        }
      }
      catch(error){
        this.usercheck_in_out = [];
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
      console.log(option)
    },

    async selectClientPhone(option){
      console.log(option)

    },
    async selectClientBorn(option){
      console.log(option)

    },
    //kerak
    async selectOptionUser(option){
        console.log(option)
        this.user_name = option.ism;
        this.user_id = option.userid;
        this.client_info = option;
        this.user_ishlagan_puli_list = [];
        await this.fetchuseroylik();
        this.$refs.user_salary_calendar.update_user_salary(option.userid);
    },

    getPhoto(){
      this.showPhoto = true;
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
</style>
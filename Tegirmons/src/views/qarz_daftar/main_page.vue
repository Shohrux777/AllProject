<template>
  <div class="zaxira"> 
    <div class="getProduct">
      <div class="container-fluid">
        <div class="row px-3 py-2 d-flex align-items-center main_header_bg_new" :class="{'bg_red_color': oshibka_client == 1}">
          <div class="col-3">
            <div class="">
              <erpSelect
              :options="client_list"
              @select="selectOptionUser"
              :selected="user_name"
              size="sm"
              class="bg-white"
              style="margin-top:8px;"
              url="/TegirmonQarzdorlikUser/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
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
            <input-search KeyName="fio"  @select="selectClientPassport" :label="$t('passport_number')" :selected="passport_number"
              url="/TegirmonQarzdorlikUser/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
              ref="search_client" placeholder="AA12345678"  style="height:32px;">
            </input-search>
          </div>
  
          <div class="col-3">
            <input-search-year KeyName="fio"  @select="selectClientBorn" type="text" :label="$t('born_date')" :selected="born_date" placeholder="дд-мм-гггг"
              url="/TegirmonQarzdorlikUser/getPaginationByBornDate?page=0&size=100&born_date_str="
              ref="search_client_born_date" style="height:32px;">
            </input-search-year>
          </div>
          <div class="col-3">
            <div class="d-flex justify-content-end">
              <div  @click="refresh_skudUser" class="mr-2">
                <div class="main_kassa_btn m-0 bg_col_info" >
                  <i style="font-size: 12px;" class="fas fa-redo"></i>
                </div>
              </div>
              <div  @click="show_user_list" class="mr-2">
                <div class="main_kassa_btn m-0 bg_col_info">
                  <small>Список должников</small>
                </div>
              </div>
              <div style="width:170px" @click="add_user_dolg">
                <div class="main_kassa_btn m-0 bg_col_green" >
                  <small>Добавить должника</small>
                </div>
              </div>
            </div>
          </div>
  
        </div>
      </div>
      
      <div class="mainWorkChange">
        <div class="d-flex justify-content-between px-2">
            <div class="w-100 p-2">
                <div class="row equal-height">
                  <div class="col-5" >
                    <div class="card" v-if="client_info.fio" :class="{'alert-danger': client_info.sum>0 || client_info.dollor>0, 'alert-success': client_info.sum<0 || client_info.dollor<0}">
                      <div class="d-flex w-100">
                        <div class="user_img" v-if="client_info.image_url">
                          <img :src="hostname + client_info.image_url" alt="" @click="$imageModal.open(hostname + client_info.image_url)">
                        </div>
                        <div class="user_info_selected pt-4 px-3">
                          <h5 class="font-weight-bold" style="font-size: 14.5px;">{{client_info.fio}}</h5>
                          <p class="m-0 pt-1"><span class=" pr-1">ID:</span>  {{client_info.id}}</p>
                          <p class="m-0"><span class=" pr-1">Номер паспорта:</span>  {{client_info.passport_number}}</p>
                          <p class="m-0 "><span class=" pr-1">Дата рождения:</span>  {{born_date}}</p>
                          <p class="m-0 "><span class=" pr-1">Тел:</span>  {{client_info.phone_number}}</p>
                        </div>
                      </div>
                      <div class="ishlagan_puli d-flex align-items-center justify-content-between py-2 px-5 " style="border-top: 1px solid grey">
                          <h5 class="font-weight-bold m-0" style="font-size: 17px;">Долг :</h5>
                          <h5  class="font-weight-bold ml-3 m-0" style="font-size: 25px; color: brown;">{{ client_info.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')  }} сўм</h5>
                          <h5  class="font-weight-bold ml-3 m-0" style="font-size: 25px; color: brown;">{{client_info.dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} $</h5>
                      </div>
                      <div class="ishlagan_puli d-flex align-items-center py-1 px-4 " style="border-top: 1px solid grey">
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
                  </div>
                  <!-- <camera/> -->


                </div>
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
                <th width="40" >ID</th>
                <th>{{$t('client_name')}}</th>
                <th>Sum</th>
                <th>Dollor</th>
                <th>Kurs</th>
                <th>{{ $t('status') }}</th>
                <th>{{$t('date')}}</th>
                <th>{{$t('note')}}</th>
                <th>{{ $t('ostatka') }}</th>
                <th>{{ $t('ostatka') }} $</th>
                <th>Кассир</th>

                <!-- <th width="80" class="text-center">{{$t('Action')}}</th> -->
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in user_rasxod_prixod_list" :key="rowIndex" @click="selectInvoiceItem(row)" 
              :class="{'zero_item': row.sum == 0 && row.dollor == 0}">
                <td> <small >{{rowIndex+1}}</small> </td>
                <td> <small >{{row.TegirmonQarzUserId}}</small> </td>
                <td> <small >{{row.worker_name}}</small> </td>
                <td> <small >{{row.sum_str}}</small></td>
                <td> <small >{{row.dollor_string}}</small></td>
                <td> <small >{{row.kurs.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small></td>

                <td v-if="row.status_rasxod == 1"> <span class="status_btn_bg px-2 text-white rounded" style="padding: 1px 6px; font-size: 10px;">Получыть </span> </td>  
                <td  v-else-if="row.status_rasxod == 0"> <span class="bg-danger px-2 text-white rounded" style="padding: 2px 10px; font-size: 10px;">Расход</span></td>
                
                <td>
                  <small v-if="row.created_date">{{row.created_date.slice(8,10) + '-' + row.created_date.slice(5,7) + '-' + row.created_date.slice(0,4)}}</small> 
                  <small v-if="row.created_date" class="ml-2">{{row.created_date.slice(11,16)}}</small>
                </td>
                <td> <small >{{row.note}}</small></td>
                <td> <small :class="{'text-danger': row.reserve_val_1>0}" >{{row.reserve_val_1.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small></td>
                <td> <small :class="{'text-danger': row.reserve_val_2>0}" >{{row.reserve_val_2.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small></td>

                <td> <small >{{row.addiotionala_information}}</small></td>

              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
    <modal-train  :show="dolg_user_show" headerbackColor="#369387"  titlecolor="black" title="Добавить должника" 
        @close="dolg_user_show = false" width="75%">
        <template v-slot:body>
          <add_dolg_user @close_data="addedUserSelect"   ref="addQarzUser">
          </add_dolg_user>
        </template>
      </modal-train>

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
      <modal-train  :show="showUserList" headerbackColor="#4ab1ff"  titlecolor="black" title="Список должников" 
        @close="showUserList = false" width="85%">
          <template v-slot:body>
            <user_list  ref="userQarzdorList" >
            </user_list>
          </template>
      </modal-train>

      <modal-train  :show="show_debt_info" :headerbackColor="rasxod_color"  titlecolor="black" title="Долг инфо" 
        @close="show_debt_info = false" width="32%">
          <template v-slot:body>
            <info_rasxod @close="show_debt_info = false" v-if="show_debt_info"  :option="debt_data" >
            </info_rasxod>
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
import erpSelect from "../../components/erpSelectFioSearch.vue";
import InputSearch from '../../components/inputSearch';
import inputSearchYear from '../../components/inputSearchYear';
import add_dolg_user from './add_dolg_user.vue'
import rasxod from './rasxod.vue'
import chiqarPulOlish from './chiqarPulOlish.vue'
import user_list from './user_list.vue'
import info_rasxod from './rasxod_info.vue'

export default {
data(){
    return{
      hostname: this.$store.state.server_ip,
      modal_info: '',
      modal_status: false,  
      loading: false,
      loading_table: false,
      showUserList: false,

      dolg_user_show: false,
      client_list: [],
      rasxod_show: false,
      pul_olib_qolish: false,
      user_rasxod_prixod_list: [],

      client_info_show: false,
      client_info: {},




      
      hisobot_show: false,
      payment_show: false,
      
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

    
      oshibka_client: 0,
      
      show_debt_info: false,
      debt_info: {},
      rasxod_color: '#64B0FB',
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
    add_dolg_user,
    rasxod,
    chiqarPulOlish,
    user_list,
    info_rasxod
  },
//   validations: {
      
//     },
    async mounted() {
      await this.fetchClient();
      // await this.nbuKurs();
      // let today = new Date();
      // this.select_month = today.toISOString().slice(0, 10);
    },
   computed: {...mapGetters(['get_user_list',]),
    
   },
    
  methods: {
    ...mapActions(['fetch_user',]),
    ...mapMutations(['check_invoice_zaxira']),
    // xodimni olgan yoki bergan pullarini kurish
    async selectInvoiceItem(data){
      this.show_debt_info = true;
      this.debt_data = data;
      if(this.debt_data.status_rasxod == 1){
        this.rasxod_color = "#369387";
      }
      else{
      this.rasxod_color = "#ff504a";
      }
    },

    // skud userdan qarzdolikuserga malumotlarni utkazish
    async refresh_skudUser(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonQarzdorlikUser/transfer-from-skud", { method: 'POST' });
        console.log(response)
        const text = await response.text();
        if(response.status == 201 || response.status == 200)
        {
          this.$refs.message.success(text);
          return true;
        }
        else{
          this.modal_info = text;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        // this.client_list = [];
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
    },

    async fetchClient(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonQarzdorlikUser/getPagination?page=0&size=100");
        const data = await response.json();
        if(response.status == 201 || response.status == 200)
        {
          console.log(data)
          this.client_list = data.items_list;
          return true;
        }
        else{
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.client_list = [];
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
    },
    async fetchUserPrixodRasxod(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonQarzUserRasxod/getPaginationUserId?page=0&size=200&userid=" + this.client_info.id);
        const data = await response.json();
        if(response.status == 201 || response.status == 200)
        {
          this.user_rasxod_prixod_list = data.items_list;
          return true;
        }
        else{
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        // this.client_list = [];
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
    },

    async show_user_list(){
      this.showUserList = !this.showUserList;
      this.$refs.userQarzdorList.fetchUserOylikReport();
    },
    async add_user_dolg(){
      this.dolg_user_show = !this.dolg_user_show;
      this.$refs.addQarzUser.MountedFunc();
    },
    async closeRasxod(data){
      this.client_info = data;
      this.rasxod_show = !this.rasxod_show;
      await this.fetchUserPrixodRasxod();
    },
    async closePulChiqish(data){
      this.client_info = data;
      this.pul_olib_qolish = !this.pul_olib_qolish;
      await this.fetchUserPrixodRasxod();

    },

    async selectOptionUser(option){
      console.log(option)
      this.client_info = option;
      this.user_name = option.fio;
      this.passport_number= option.passport_number;
      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
      await this.fetchUserPrixodRasxod();
    },
    async selectClientPassport(option){
      console.log(option)
      this.client_info = option;
      this.user_name = option.fio;
      this.passport_number= option.passport_number;
      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
      await this.fetchUserPrixodRasxod();

    },
    async selectClientBorn(option){
      console.log(option)
      this.client_info = option;
      this.user_name = option.fio;
      this.passport_number= option.passport_number;

      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
      await this.fetchUserPrixodRasxod();

    },
    // 
    async addedUserSelect(option){
      this.client_info = option;
      this.user_name = option.fio;
      this.passport_number= option.passport_number;
      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
      this.dolg_user_show = false;
      await this.fetchUserPrixodRasxod();
    },


    // data ni 2025-08-08 kurinishida qilib beradigan funksiya
    formatLocalDateToIsoWithoutTimezone(date) {
      const pad = (n) => n.toString().padStart(2, '0');
      const year = date.getFullYear();
      const month = pad(date.getMonth() + 1); // JS'da oy 0-based
      const day = pad(date.getDate());
      // const hours = pad(date.getHours());
      // const minutes = pad(date.getMinutes());
      // const seconds = pad(date.getSeconds());

      return `${year}-${month}-${day}`;
    },

     //kerak
    takePhoto(img){
      this.image_url_str = img;
      this.showPhoto = false;
    },
    //kerak


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
  height: 290px;
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
  min-height: calc(100vh - 450px);
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
.bg_col_green{
  //border: 1.5px solid #009587;
  background: #03b15f;   
  color:white;

  &:hover{
    color:white;
    background: #009563;  
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
.oylik_hisob_payed{
  position: absolute;
  top: 0; 
  right: -580px;
  width: 550px;
  z-index: 11;
}
.oylik_input{
  width: 100%;
  height: 30px;
  border-radius: 4px;
  padding: 1px 7px;
  font-size: 14px;
  text-align: right;
  border: 1px solid #d3d3d3;
  outline: none;
}
.oylik_input_red{
  color: red;
}
.oylik_input_green{
  color: green;
}
.oylik_input_blue{
  color:#006b95;
  
}
.oylik_input:focus{
  border: 1px solid #a5a5a5;
}
.oylik_input:disabled{
  background: white;
}
.payment_info{
  font-style: 17px;
  border-bottom: 1px dashed #757575;
  small{
    color: #4e4a4a;
     font-style: 17px;
  }
}



.equal-height {
    display: flex;
    align-items: stretch;
}
</style>
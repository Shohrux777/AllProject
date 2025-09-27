<template>
  <div class="zaxira"> 
    <div class="getProduct">
      <div class="container-fluid">
        <div class="row px-3 py-1 d-flex align-items-center main_header_bg_new" :class="{'bg_red_color': oshibka_client == 1}">
          <div class="col-3">
            <div class="">
              <erpSelect
              :options="client_list"
              @select="selectOptionUser"
              :selected="user_name"
              size="sm"
              class="bg-white"
              style="margin-top:8px;"
              url="/TegirmonOrderClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
              :label="$t('select_item')"
              />
              <!-- <small style="position:absolute; top:-8px; left:10px; font-size: 11px; color: rgb(62, 62, 139);" class="font-weight-bold px-2 py-0">Поиск клиент</small> -->
              
            </div>
          </div>
          <!-- <div class="col-3">
            <mdb-input class="m-0 p-0" v-model="passport_number" size="md"  outline  group type="text" validate error="wrong" success="right"/>
            <small style="position:absolute; top:-7px; left:20px; font-size: 11px;" class="bg-white px-2 py-0">{{$t('passport_number')}}</small>
        
          </div> -->
          <div class="col-3">
            <input-search KeyName="fio"  @select="selectClientPassport"  :selected="passport_number"
              url="/TegirmonOrderClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
              ref="search_client" placeholder="AA12345678"  style="height:32px;">
            </input-search>
          </div>
          <div class="col-3">
            <input-search-phone KeyName="fio"  @select="selectClientPassport"  :selected="phone_number"
              url="/TegirmonOrderClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
              ref="search_client" placeholder="(99) ###-##-##"  style="height:32px;">
            </input-search-phone>
          </div>
  
          <div class="col-3" v-show="false">
            <input-search-year KeyName="fio"  @select="selectClientBorn" type="text"  :selected="born_date" placeholder="дд-мм-гггг"
              url="/TegirmonOrderClient/getPaginationByBornDate?page=0&size=100&born_date_str="
              ref="search_client_born_date" style="height:32px;">
            </input-search-year>
          </div>
          <div class="col-3">
            <div class="d-flex justify-content-end">
              <!-- <div  @click="refresh_skudUser" class="mr-2">
                <div class="main_kassa_btn m-0 bg_col_info" >
                  <i style="font-size: 12px;" class="fas fa-redo"></i>
                </div>
              </div> -->
              <!-- <div  @click="show_user_list" class="mr-2">
                <div class="main_kassa_btn m-0 bg_col_info">
                  <small>Список должников</small>
                </div>
              </div> -->
              <div style="width:130px" class="mr-2" @click="add_order_status = !add_order_status; order_id = 0;">
                <div class="main_kassa_btn m-0 bg_col_blue">
                  <small>Добавить заказ</small>
                </div>
              </div>
              <div style="width:150px" @click="add_user_dolg">
                <div class="main_kassa_btn m-0 bg_col_green" >
                  <small>Добавить клиент</small>
                </div>
              </div>
            </div>
          </div>
  
        </div>
      </div>
      
      <div class="mainWorkChange ">
        <!-- Client tanlansa balance chiqadi -->
        <div class=" px-2" v-if="client_info.fio">
          <div class="w-100 p-2">
            <div class="row equal-height px-3" >
              <div class="col-2 p-1 balance" style="position: relative;">
                <div class="card py-1 pt-2 px-2 alert-success ">
                  <span style="font-size: 13.5px;">Balance UZS</span>
                  <span class="text-right" style="font-size: 19px;">{{client_info.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span>
                </div>
                <div style="position: absolute; bottom: -50px;" class="balance_btn w-100">
                  <div class="d-flex justify-content-end px-2">
                    <div class="main_kassa_btn_sml bg_col_blue" @click="pul_olib_qolish = !pul_olib_qolish">
                      <small>Получать деньги</small>
                    </div>
                    <div class="main_kassa_btn_sml bg_col_red px-4" @click="rasxod_show = !rasxod_show">
                      <small>Расходъ</small>
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-2 p-1 balance">
                <div class="card py-1 pt-2 px-2 alert-warning">
                  <span style="font-size: 13.5px;">Balance USD</span>
                  <span class="text-right" style="font-size: 19px;">{{client_info.dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} </span>
                </div>
                <div style="position: absolute; bottom: -50px;" class="balance_btn w-100">
                  <div class="d-flex justify-content-end px-2">
                    <div class="main_kassa_btn_sml bg_col_blue" @click="pul_olib_qolish = !pul_olib_qolish">
                      <small>Получать деньги</small>
                    </div>
                    <div class="main_kassa_btn_sml bg_col_red px-4" @click="rasxod_show = !rasxod_show">
                      <small>Расходъ</small>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- Asosiy zakazlar va kalendar chiqadi -->
        <div class="main_order">
          <div class="order_list p-2" v-if="!add_order_status">
            <div class="p-2 card order_list_display">
              <div class="mb-2">
                <div class="order_main_head d-flex">
                  <span v-for="(item,i) in order_link" :key="i" :class="{'bg-white text-dark': active_link == i}" 
                    @click="toggleLink(i)">
                    {{item.name}}
                    <mdb-badge class="ml-1" style="padding: 3px 8px; font-size: 11px;" pill :style="{ background: item.color + '!important' }">{{item.qty}}</mdb-badge>
                  </span>
                </div>
              </div>
              <table class="w-full border w-100 myTableuserSalaryList">
                <thead>
                  <tr class="bg-gray-200 header py-3 info_client_header ">
                    <th style="padding: 6px 10px;" class="text-left" width="40">№</th>
                    <th style="padding: 6px 10px;" class="text-left">{{$t('date')}}</th>
                    <th style="padding: 6px 10px;" class="text-left">{{$t('client_name')}}</th>
                    <th style="padding: 6px 10px;" class="text-left">{{$t('shafyor_name')}}</th>
                    <th style="padding: 6px 10px;" class="text-left">{{$t('car_number')}}</th>
                    <th style="padding: 6px 10px;" class="text-left">Оплата</th>
                    <th style="padding: 6px 10px;" class="text-left">{{$t('sum')}}</th>
                    <th style="padding: 6px 10px;" class="text-left">{{$t('dollor')}}</th>
                    <th style="padding: 6px 10px;" class="text-left">{{$t('items')}}</th>
                    <th style="padding: 6px 10px;" class="text-left">Загрузка</th>
                    <th style="padding: 6px 10px;" class="text-left">{{$t('status_')}}</th>
                    <th style="padding: 6px 10px;" class="text-left" width="90">{{$t('Action')}}</th>

                    <!-- <th style="padding: 6px 10px;" class="text-left">{{$t('user_name')}}</th> -->
                  </tr>
                </thead>
                
                <tbody>
                  <template v-for="(order, index) in order_list">
                    <!-- Asosiy order qatori -->
                    <tr
                      :key="order.id"
                      class="cursor-pointer hover:bg-gray-100"
                      :class="{'alert-success': order.isClosed}"
                    >
                      <td class="p-2">{{ index+1 }}</td>
                      <td class="p-2">{{ order.pickUpDate.slice(8,10) }}-{{ order.pickUpDate.slice(5,7) }}-{{ order.pickUpDate.slice(0,4) }}</td>
                      <td class="p-2">{{ order.client_name }}</td>
                      <td class="p-2 clickbtn" >
                        <span v-if="order.shafyor_name">{{ order.shafyor_name }}</span>
                        <span v-else class="text-danger" @click="toggleShafyorInfo(order, false)">???</span>
                      </td>
                      <td class="p-2 clickbtn">
                        <span v-if=" order.car_nomer" >{{  order.car_nomer }}</span>
                        <span v-else class="text-danger" @click="toggleShafyorInfo(order, false)">???</span>
                      </td>
                      <td class="p-2" v-if="order.isPaid" @click="payOrder(order)">
                        <span class="paid">
                          Оплачено
                        </span>
                      </td>
                      <td class="p-2 " v-else  @click="payOrder(order)">
                        <span v-if="order.pay_progress>0" class="nopaidprogress">
                          <span class="load_progress_text">{{order.pay_progress}} %</span> .
                          <small class="progress_change " :style="{ width: order.pay_progress + '%' }"></small>
                        </span>
                        <span v-else class="nopaid">
                          Не оплачено
                        </span>
                      </td>
                      <td class="p-2">{{ order.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</td>
                      <td class="p-2">{{ order.dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</td>
                      <td class="p-2" style="cursor:pointer;" 
                        @click="toggleOrder(index)">{{ order.item_list.length }}  товары
                      </td>
                      <td class="p-2" v-if="order.isLoaded" @click="toggleShafyorInfo(order, true)">
                        <span class="paid">
                          Загружен
                        </span>
                      </td>
                      <td class="p-2 " v-else  @click="toggleShafyorInfo(order, true)">
                        <span v-if="order.load_progress>0" class="nopaidprogress">
                          <span class="load_progress_text">{{order.load_progress}} %</span> .
                          <small class="progress_change " :style="{ width: order.load_progress + '%' }"></small>
                        </span>
                        <span v-else class="nopaid" >
                          Не загружен
                        </span>
                      </td>
                      <td class="p-2" >
                        <div class="order_status">
                          <span>{{ order.paid_status }}</span>
                        </div>
                      </td>
                      <td class="p-2 text-center">
                        <i class="fas fa-pen iconPen mask waves-effect  m-0 pr-3" @click="edit_order(order)"></i>
                        <i class="fa fa-cog iconCog mask waves-effect  m-0 " @click="printReceipt"></i>
                      </td>
                    </tr>

                    <!-- Order items qatori -->
                    <tr v-if="expandedOrder === index" :key="order.id + '-items'">
                      <td colspan="12" class="px-1">
                        <table class="w-100  item_table_shadow rounded my-2 px-2">
                          <thead>
                            <tr class="bg-gray-100 info_client_header1">
                              <th class=" text-left">{{ $t('product') }}</th>
                              <th class=" text-left">Оптом цена</th>
                              <th class=" text-left">{{$t('qty')}}</th>
                              <th class=" text-left">{{$t('price')}}</th>
                              <th class=" text-left">{{$t('sum')}}</th>
                              <th class=" text-left">Валюта</th>
                            </tr>
                          </thead>
                          <tbody>
                            <tr v-for="item in order.item_list" :key="item.id">
                              <td class="p-2">{{ item.product.name }}</td>
                              <td class="p-2">{{ item.sum_str }}</td>
                              
                              <td class="p-2">{{ item.qty }}</td>
                              <td class="p-2">{{ item.price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</td>

                              <td class="p-2">{{ item.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</td>
                              <td class="p-2">
                                <span v-if="item.price_type == 1">UZS</span>
                                <span v-else>USD</span>
                              </td>
                            </tr>
                          </tbody>
                        </table>
                      </td>
                    </tr>
                  </template>
                </tbody>
              </table>
            </div>
          </div>
          <div class="order_add order_list p-2" v-else>
            <div class="p-2 card order_list_display">
              <order_Add :client_info="client_info" :id="order_id" @close="closeOrderAdd" :choosen_day="choosen_day"/>
            </div>
          </div>
          <div class="order_calendar">
            <calendar @choose_day="handleChooseDay1" 
              :client_info="client_info" 
              @change_date="handleChooseDate" 
              ref="user_salary_calendar" 
              class="card mt-2 ml-2"/>
            <div class="order_products mt-3 ml-2 ">
                <div class="px-0 userSalaryTable" :class="user_id ? 'userSalaryTableClient' : 'userSalaryTable'">
                <loader-table v-if="loading"/>
                <table v-else class="myTableuserSalaryList">
                  <thead>
                    <tr class="header py-3 info_client_header">
                      <th  width="30" class="text-left">№</th>
                      <th width="150">{{$t('product')}}</th>
                      <th>{{$t('qty')}}</th>
                      <th>{{$t('sklad')}}</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for="(row,rowIndex) in order_products_list" :key="rowIndex"
                    :class="{'zero_item': row.stockQty <= 0}">
                      <td> <small >{{rowIndex+1}}</small> </td>
                      <td> <small >{{row.productName}}</small> </td>
                      <td> <small >{{row.totalQty}} {{ row.unitMeasurmentName }}</small> </td>
                      <td> 
                        <small v-if="row.stockQty <= 0">Нет склад</small>
                        <small v-else-if="row.stockQty < row.totalQty" class="text-danger">{{row.stockQty}} {{ row.unitMeasurmentName }}</small>
                        <small v-else class="text-success">{{row.stockQty}} {{ row.unitMeasurmentName }}</small>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      </div>

      
      
        <div class="photo w-100 mt-4 d-flex justify-content-center " v-if="image_url_str">
          <img :src="hostname + image_url_str" width="240" alt="" class="shadow border rounded">
        </div>
        
      <webcam  v-show="showPhoto" @getPhotosub="takePhoto"/>

      <!-- <hr class="mt-4 mb-3 gradint"/> -->
        <!-- <loaderFixed v-if="loading"/> -->
    </div>
     <modal-train  :show="dolg_user_show" headerbackColor="#369387"  titlecolor="black" title="Добавить клиент" 
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
      <!-- <modal-train  :show="showUserList" headerbackColor="#4ab1ff"  titlecolor="black" title="Список должников" 
        @close="showUserList = false" width="85%">
          <template v-slot:body>
            <user_list  ref="userQarzdorList" >
            </user_list>
          </template>
      </modal-train> -->

      <modal-train  :show="show_debt_info" :headerbackColor="rasxod_color"  titlecolor="black" title="Долг инфо" 
        @close="show_debt_info = false" width="32%">
          <template v-slot:body>
            <info_rasxod @close="show_debt_info = false" v-if="show_debt_info"  :option="debt_data" >
            </info_rasxod>
          </template>
      </modal-train>
      <modal-train  :show="show_shafyor_info" headerbackColor="#369387"  titlecolor="black" title="Инфо водителя" 
        @close="show_shafyor_info = false" :width="loaded_width+'%'">
          <template v-slot:body>
            <Loaded_component  :order_info="select_order" @close="close_loaded" ref="loadedComp" :loaded_status="loaded_status"/>
          </template>
      </modal-train>
       <pay v-show="payshow" :client_info="pay_client_info" @close="closePay" 
         />
        <!-- @print="printChek()" -->
      
      <Toast ref="message"></Toast>

    <massage_box :hide="modal_status" :detail_info="modal_info"
    :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>
    <Alert ref="alert"></Alert>
  </div>
</template>

<script>
import calendar from './calendar.vue';
import webcam from '../webcam/webcam_Add.vue'
import { mdbInput, mdbIcon,  mdbBtn, mdbBadge } from "mdbvue"
// import { required } from 'vuelidate/lib/validators';
import {mapActions,mapGetters, mapMutations} from 'vuex';
import erpSelect from "../../components/erpSelectFioSearch.vue";
import InputSearch from '../../components/inputSearchPasport';
import InputSearchPhone from '../../components/inputSearchPhone';
import inputSearchYear from '../../components/inputSearchYear';
import add_dolg_user from './add_client.vue'
import rasxod from './rasxod.vue'
import chiqarPulOlish from './chiqarPulOlish.vue'
// import user_list from './user_list.vue'
import info_rasxod from './rasxod_info.vue'
import order_Add from './order_Add.vue';
import Loaded_component from './loaded_component.vue';
import Pay from './pay.vue';

export default {
data(){
    return{
      hostname: this.$store.state.server_ip,
      modal_info: '',
      modal_status: false,
      loading: false,
      loading_table: false,
      showUserList: false,
      show_shafyor_info: false,

      dolg_user_show: false,
      client_list: [],
      rasxod_show: false,
      pul_olib_qolish: false,
      user_rasxod_prixod_list: [],

      client_info_show: false,
      client_info: {},
      pay_client_info: {},
      add_order_status: false,
      order_id: 0,

      
      hisobot_show: false,
      payment_show: false,
      
      allList: [],

      showPhoto: false,
      image_url_str: '',

      passport_number: '',
      born_date: '',
      phone_number: '',
      user_name: '',
      user_id: 0,

      usercheck_in_out: [],
      user_oylik_info: [],

    
      oshibka_client: 0,
      
      show_debt_info: false,
      debt_info: {},
      rasxod_color: '#64B0FB',

      choosen_day: '',
      order_list: [],
      order_products_list: [],
      expandedOrder: null,
      select_order: {},
      loaded_status: false,
      loaded_width: '35',
      active_link: localStorage.order_page,
      order_link: [
        {
          id: 0,
          name: 'Hammasi',
          color: '#ED792C',
          qty: 0,
        },
        {
          id: 1,
          name: 'Yopilmagan zakazlar',
          color: '#E25049',
          qty: 0,
        },
        {
          id: 2,
          name: "To'lov qilganlar",
          color: "#A45CF2",
          qty: 0,
        },
        {
          id: 3,
          name: "Yuklanganlar",
          color: "#EBA12E",
          qty: 0,
        },
        {
          id: 3,
          name: "Yopilgan zakazlar",
          color: '#4EC264',
          qty: 0,
        },
        {
          id: 4,
          name: "Puli kassaga kelmaganlar",
          color: '#4EE244',
          qty: 0,
        },
      ],
      payshow: false,
      old_paid_not_deliver_cassa: [],
    }
  },
  components: {
    mdbIcon,
    mdbBtn, 
    mdbInput,
    mdbBadge,
    webcam,
    erpSelect,
    InputSearch,
    inputSearchYear,
    InputSearchPhone,
    add_dolg_user,
    rasxod,
    chiqarPulOlish,
    info_rasxod,
    calendar,
    order_Add,
    Loaded_component,
    Pay
  },
//   validations: {
      
//     },
    async mounted() {
      await this.fetchOldDayNotClosed();
      await this.fetchOrderPaidNotCassa();
      await this.fetchClient();
      let time1 = new Date();
      this.choosen_day = time1.toISOString().slice(0,10); 
      await this.fetchAllOrderList();
      await this.fetchAllOrderStatusNumber();
      await this.fetchAllOrderProductsList();
      // await this.nbuKurs();
      if(!localStorage.dollor_kurs){
        await this.nbuKurs();
      }
      if(localStorage.order_page>=0){
        this.active_link = localStorage.order_page;
      }
      else{
        localStorage.order_page = 0;
      }
      if(localStorage.order_page == 5){
        this.order_list = this.old_paid_not_deliver_cassa;
      }
      // let today = new Date();
      // this.select_month = today.toISOString().slice(0, 10);
    },
   computed: {...mapGetters(['get_user_list',]),
    
   },
    
  methods: {
    ...mapActions(['fetch_user',]),
    ...mapMutations(['check_invoice_zaxira']),
    printReceipt() {
      console.log('printed')
      
    },
    async payOrder(order){
      console.log(order)
      if(order.client){
        this.pay_client_info = order.client;
      }
      else{
        this.pay_client_info = {
          fio:"",
          id:null,
          sum:0,
          dollor:0,
        }
      }
      this.payshow = true;
      this.$root.$refs.order_payed.changingEnter({sum:order.remaining_sum, dollor: order.remaining_usd, order: order});

    },
    async closePay(index){
      this.payshow = false;
      if(index == 1){
        await this.fetchAllOrderList();
        await this.fetchOrderPaidNotCassa();
        await this.fetchAllOrderStatusNumber();
      }
    },
    toggleOrder(index) {
      this.expandedOrder = this.expandedOrder === index ? null : index;
    },
    async closeOrderAdd(){
      this.add_order_status = !this.add_order_status;
      await this.fetchAllOrderList();
      await this.fetchAllOrderStatusNumber();
      await this.fetchAllOrderProductsList();
    },
    async toggleShafyorInfo(order, status){
      this.select_order = order;
      this.loaded_status = status;
      if(status){
        this.loaded_width = '65';
      }
      else{
        this.loaded_width = '35';
      }
      this.show_shafyor_info = true;
      this.$refs.loadedComp.fetchMounted(this.select_order);
    },
    async close_loaded(){
      this.show_shafyor_info = false;
      await this.fetchAllOrderList();
      await this.fetchAllOrderStatusNumber();
      await this.fetchAllOrderProductsList();
    },
    async toggleLink(index){
      this.active_link = index;
      localStorage.order_page = index;
      await this.fetchAllOrderList();
      if(index == 5){
        this.order_list = this.old_paid_not_deliver_cassa;
      }
    },

    async fetchAllOrderList(){
      let temp_link = '/TegirmonOrder/getByDate?date=';
      if(localStorage.order_page == 0){
        temp_link = '/TegirmonOrder/getByDate?date=';
      }
      else if(localStorage.order_page == 1){
        temp_link = '/TegirmonOrder/getByDateNotClosed?date=';
      }
      else if(localStorage.order_page == 2){
        temp_link = '/TegirmonOrder/getByDateNotClosedButPaid?date=';
      }
      else if(localStorage.order_page == 3){
        temp_link = '/TegirmonOrder/getByDateNotClosedButLoaded?date=';
      }
      else{
        temp_link = '/TegirmonOrder/getByDateClosedOrder?date=';
      }
      try{
        const response = await fetch(this.$store.state.hostname + temp_link + this.choosen_day + '&clientId=' + this.user_id);
        const data = await response.json();
        console.log('data_list order',data)
        if(response.status == 201 || response.status == 200)
        {
          this.order_list = data;
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

    async fetchAllOrderStatusNumber(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonOrder/getOrderStatsByDate?date=" + this.choosen_day + '&clientId=' + this.user_id);
        const data = await response.json();
        console.log('data_list order',data)
        if(response.status == 201 || response.status == 200)
        {
          this.order_link[0].qty = data.totalCount;
          this.order_link[1].qty = data.notClosedCount;
          this.order_link[2].qty = data.notClosedPaidCount;
          this.order_link[3].qty = data.notClosedLoadedCount;
          this.order_link[4].qty = data.closedCount;
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

    // bu oldingi kunlarda pullari tulangan lekin puli pochtada kassaga yetib kelmaganlarni kursatadi
    async fetchOrderPaidNotCassa(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonOrder/getLoadedUnpaidOrders");
        const data = await response.json();
        console.log('data_list order',data)
        if(response.status == 201 || response.status == 200)
        {
          this.old_paid_not_deliver_cassa = data;
          this.order_link[5].qty = data.length;
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

    // oldingi kunlarda yopilmay qolgan zakazlarni bugunga utkazish 10 kun oldinni tekshiradi
    async fetchOldDayNotClosed(){
      fetch(this.$store.state.hostname + '/TegirmonOrder/checkAndUpdateOldOrders', { // API manzili
        method: 'POST',          // POST bo'lishi shart
        headers: {
          'Content-Type': 'application/json'
        },
        // agar API hech qanday body qabul qilmasa, body shart emas:
        // body: JSON.stringify({ ... })
      })
    .then(res => {
      if (!res.ok) {
        throw new Error('Server error');
      }
      return res.json();
    })
    .then(data => {
      console.log('Yangilangan eski zakazlar:', data);
      // data – C# API qaytargan ro‘yxat bo‘ladi
    })
    .catch(err => {
      console.error(err);
    });
    },

    async fetchAllOrderProductsList(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonOrder/GetOrderSummaryByDate?date=" + this.choosen_day + '&clientId=' + this.user_id);
        const data = await response.json();
        console.log('data_list order products',data)
        if(response.status == 201 || response.status == 200)
        {
          this.order_products_list = data;
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

    // xodimni olgan yoki bergan pullarini kurish
    async handleChooseDay1(data){
      console.log(data)
      const day = String(data.date.getDate()).padStart(2, "0");
      const month = String(data.date.getMonth() + 1).padStart(2, "0");
      const year = data.date.getFullYear();
      this.choosen_day = `${year}-${month}-${day}`;
      await this.fetchAllOrderList();
      await this.fetchAllOrderStatusNumber();
      await this.fetchAllOrderProductsList();
      // this.$refs.user_vaqt_info_comp.handleChooseDay(data);
    },
    async handleChooseDate(data){
      console.log(data)
      const day = String(data.date.getDate()).padStart(2, "0");
      const month = String(data.date.getMonth() + 1).padStart(2, "0");
      const year = data.date.getFullYear();

      this.choosen_day = `${year}-${month}-${day}`;
    },
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

    // async show_user_list(){
    //   this.showUserList = !this.showUserList;
    //   this.$refs.userQarzdorList.fetchUserOylikReport();
    // },
    async add_user_dolg(){
      this.dolg_user_show = !this.dolg_user_show;
      this.$refs.addQarzUser.MountedFunc();
    },
    async closeRasxod(data){
      this.client_info = data;
      this.rasxod_show = !this.rasxod_show;
      // await this.fetchUserPrixodRasxod();
    },
    async closePulChiqish(data){
      this.client_info = data;
      this.pul_olib_qolish = !this.pul_olib_qolish;
      // await this.fetchUserPrixodRasxod();

    },
    async fetchClient(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonOrderClient/getPagination?page=0&size=100");
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
    async selectOptionUser(option){
      console.log(option)
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number= option.passport_number;
      this.phone_number= option.phone_number;

      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
      // await this.fetchUserPrixodRasxod();
      await this.fetchAllOrderList();
      await this.fetchAllOrderStatusNumber();
      await this.fetchAllOrderProductsList();
      
    },
    async selectClientPassport(option){
      console.log(option)
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number= option.passport_number;
      this.phone_number= option.phone_number;
      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
      // await this.fetchUserPrixodRasxod();
      await this.fetchAllOrderList();
      await this.fetchAllOrderStatusNumber();
      await this.fetchAllOrderProductsList();


    },
    async selectClientBorn(option){
      console.log(option)
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number= option.passport_number;
      this.phone_number= option.phone_number;

      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
      // await this.fetchUserPrixodRasxod();
      await this.fetchAllOrderList();
      await this.fetchAllOrderStatusNumber();
      await this.fetchAllOrderProductsList();

    },
    // 
    async addedUserSelect(option){
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number= option.passport_number;
      this.phone_number= option.phone_number;

      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
      this.dolg_user_show = false;
      // await this.fetchUserPrixodRasxod();
      await this.fetchAllOrderList();
      await this.fetchAllOrderStatusNumber();
      await this.fetchAllOrderProductsList();
    },
    async edit_order(order){
      this.order_id = order.id;
      this.add_order_status = true;
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
          localStorage.dollor_kurs = parseInt(data[0].Rate);
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
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
.userSalaryTable{
  max-height: 480px;
  overflow: hidden;
  overflow-y: scroll;
  scrollbar-width: none;
  border-radius: 3px;
  padding-bottom: 5px;
  // border: 1px solid #757575;
}
.userSalaryTableClient{
  max-height: 400px;
  overflow: hidden;
  overflow-y: scroll;
  scrollbar-width: none;
  border-radius: 3px;
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
  // border-right: 1px solid rgb(219, 219, 219);
}
.myTableuserSalaryList th{
  text-align: left;
  padding: 5px 10px;

}

.myTableuserSalaryList tr {
  border-bottom: 1px solid rgb(219, 219, 219);
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
  min-height: calc(100vh - 120px);
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
.info_client_header1{
  background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgb(255, 224, 194) 0%, rgb(253, 205, 187) 90.1% ) !important;

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
  margin-top: 0px;
  small{
    font-size: 12px;
  }
}
.main_kassa_btn_sml{
  display: flex;
  justify-content: center;
  align-items: center;
  height: 30px;
  padding: 0 15px;
  color:#3a4b52;
  border-radius: 4px;
  cursor: pointer;
  margin-right: 7px;
  margin-top: 0px;
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
  border-bottom: 1px solid #757575;
  small{
    color: #4e4a4a;
     font-style: 17px;
  }
}
.client_balance{
  width: 12.5%;
  margin: 5px 0px;
}


.equal-height {
    display: flex;
    align-items: stretch;
}
.balance_btn{
  display: none;
  background: #eeeeee;
  height: 50px;
  padding-top: 7px;
  border-top-left-radius: 5px;
  border-bottom-left-radius: 5px;
  border-bottom-right-radius: 5px;
  z-index: 1111;

}
.balance:hover{
  cursor: pointer;
  .balance_btn{
    display: inline-block;
  }
}

.main_order{
  display: flex;
  width: 100%;
}
.order_list{
  width: calc(100vw - 370px);
}
.order_calendar{
  width: 350px;
}
.order_list_display{
  overflow: hidden;
  overflow-y: scroll;
  max-height: calc(100vh - 130px);
}
.order_main_head{
  border-radius: 5px;
  padding: 3px;
  background: #e7e7e7;
  display: inline-block;
  span{
    color: #3a4b52;
    border-radius: 5px;
    cursor: pointer;
    display: inline-block;
    font-size: 13px;
    padding: 5px 13px;
    &:hover{
      background: white;
    }
  }
}
.paid{
  width: 90%;
  text-align: center;
  color: #4c7419;
  font-weight: bold;
  border-radius: 4px;
  cursor: pointer;
  display: inline-block;
  font-size: 10px;
  padding: 2px 5px;
  border: 1px solid #86c732;
  background: #ebfdd5;
  font-weight: 600;
}
.nopaid{
  width: 90%;
  text-align: center;
  color: #fd6a6a;
  border-radius: 4px;
  cursor: pointer;
  display: inline-block;
  font-size: 10px;
  padding: 2px 5px;
  border: 1px solid #fd6a6a;
  background: #fff6f6;
  font-weight: 600;
}
.nopaidprogress{
  position: relative;
  width: 90%;
  text-align: center;
  color: #fff6f6;
  border-radius: 4px;
  cursor: pointer;
  display: inline-block;
  font-size: 10px;
  padding: 2px 5px;
  border: 1px solid #22b653;
  background: #fff6f6;
  font-weight: 600;
}
.load_progress_text{
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
  z-index: 111 !important;
  color: rgb(19, 62, 75);
}
.progress_change{
  position: absolute;
  left: 0;
  top: 0;
  bottom: 0;
  background: repeating-linear-gradient(
    60deg,
    #22b653,
    #28cc5f 20px
  );
  background-size: 40px 40px;
  animation: move 3s linear infinite;
  transition: width 2s ease;
  z-index: 1;
  border-radius: 3px;
}
@keyframes move {
  from {
    background-position: 0 0;
  }
  to {
    background-position: 40px 0px;
  }
}
.item_table_shadow{
  box-shadow: rgba(60, 64, 67, 0.3) 0px 1px 2px 0px, rgba(60, 64, 67, 0.15) 0px 1px 3px 1px;
  border-radius: 10px;
}
.iconCog{
  color: #828288;
}
.iconPen{
  color: #828288;
}
.clickbtn{
  cursor: pointer;
}
.order_status span {
  display: inline-block;    /* yoki block */
  width: 120px;             /* maksimal kenglik */
  white-space: nowrap;      /* yangi qatorga tushmasin */
  overflow: hidden;         /* tashqarisini yashir */
  text-overflow: ellipsis;  /* uch nuqta qo'y */
}
.order_products{
  border-radius: 3px;
  width: 350px;
  box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
}
</style>
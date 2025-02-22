<template>
  <div style="position:relative;">
    <checkzaxira v-if="show_check" @close="show_check = false"/>
    <div v-else>
      <loader v-if="loading" />
      <div v-else class="getProduct mt-0">
        <!-- Search qismi -->
        <div class="container-fluid">
          <div class="row d-flex align-items-center px-3 mb-0 pt-2 main_header_bg_new border rounded shadow" :class="{'bg_red_color': oshibka_client == 1}">
            <div class="col-3">
              <div class="m-0 p-0 pt-2">
                <erpSelect
                :options="allClient.rows"
                @select="selectOptionClient"
                :selected="client_name"
                size="sm"
                class="bg-white"
                url="/TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
                :label="$t('select_item')"
                />
                <small class="font-weight-bold px-2 py-0 header_input_title">{{$t('client')}}</small>
              </div>
            </div>
            <!-- <div class="col-3">
              <mdb-input class="m-0 p-0" v-model="passport_number" size="md"  outline  group type="text" validate error="wrong" success="right"/>
              <small style="position:absolute; top:-7px; left:20px; font-size: 11px;" class="bg-white px-2 py-0">{{$t('passport_number')}}</small>
          
            </div> -->
            <div class="col-3">
              <input-search  @select="selectClientPassport" :label="$t('passport_number')" :selected="passport_number" placeholder="AB12345678"
                url="/TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
                ref="search_client"  style="height:32px;">
              </input-search>
            </div>
  
            <div class="col-3">
              <input-search-year  @select="selectClientBorn" type="text" :label="$t('born_date')" :selected="born_date" placeholder="дд-мм-гггг"
                url="/TegirmonClient/getPaginationByBornDate?page=0&size=100&born_date_str="
                ref="search_client_born_date" style="height:32px;">
              </input-search-year>
            </div>
            <div class="col-3">
              <input-search  @select="selectClientPhone" :label="$t('note')" :selected="phone_number" placeholder="1234"
                url="/TegirmonClient/getPaginationByNote?page=0&size=100&note_str="
                ref="search_client_phone" style="height:32px;">
              </input-search>
            </div>
            <div class="ostatka px-3 mt-3 pb-2" style="position:relative;" v-if="OstatkaList.length>0">
              <small style="position: absolute; top:-17px; left:20px; font-size: 12px; font-weight: bold; color: rgb(62, 62, 139);">
                Клиент остатки
              </small>
              <table class="myTableDebitorList bg-white" >
                <thead>
                  <tr class="header py-3" style="background: #afd1fd;">
                    <th  width="40" class="text-left">№</th>
                    <th width="260">{{$t('client_name')}}</th>
                    <th>{{$t('passport_number')}}</th>
                    <th>{{$t('phone_number')}}</th>
                    <th>{{$t('note')}}</th>
                    <th>{{$t('product')}}</th>
                    <th>{{$t('measure')}}</th>
                    <th>{{$t('date')}}</th>
                  
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(row,index) in OstatkaList" :key="index" @click="selectMainClient(row)">
                    <td> <span >{{index+1}}</span> </td>
                    <td> <span >{{row.client.fio}}</span> </td>
                    <td> <span >{{row.client.passport_number}}</span> </td>
                    <td> <span >{{row.client.phone_number}}</span> </td>
                    <td> <span >{{row.client.note}}</span> </td>
                    <td> <span >{{row.product.name}}</span> </td>
                    <td> <span class="text-success">{{row.real_qty.toFixed(1)}}</span> </td>
                    
                    <td> <span >{{row.updated_date_time.slice(8,10) + '-' + row.updated_date_time.slice(5,7) + '-' + row.updated_date_time.slice(0,4)}}</span> <span class="ml-2">{{row.updated_date_time.slice(11,16)}}</span> </td>
                    
                    <!-- <td> <span >{{row.lessons_cout}}</span> </td> -->
                  
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
        <!-- Search qismi -->

        <!-- <div class="row mx-3" v-if="user_to_zaxira.client_new_name">
          <div class="col-3">
            <mdb-input class="m-0 p-0" disabled :value="user_to_zaxira.client_new_name" size="sm"  outline  group type="text" validate error="wrong" success="right"/>
          </div>
          <div class="col-3">
            <mdb-input class="m-0 p-0" disabled :value="user_to_zaxira.product_name" size="sm"  outline  group type="text" validate error="wrong" success="right"/>
          </div>
          <div class="col-3">
            <mdb-input class="m-0 p-0" disabled :value="user_to_zaxira.real_qty" size="sm"  outline  group type="text" validate error="wrong" success="right"/>
          </div>
          <div class="col-3">
            <mdb-btn    @click="deleteInvoice(user_to_zaxira)" color="danger"  style="font-size: 8.5px; margin-top: -2px;"
                p="r3 l3 t2 b2"> <mdb-icon fas class="mr-1"  icon="trash"></mdb-icon>  {{$t('trash')}}
              </mdb-btn>
          </div>
        </div> -->
        <div class="container-fluid">
          <div class="row mb-2">
            <div class="col-12">
              <div class="d-flex justify-content-end">
                <div>
                  <small
                    v-if="$v.user_name.$dirty && user_id == null"
                    class="text-danger pt-2 ml-2"
                    style="margin-top: 35px;"
                  >
                    {{ $t("select_client_or_add") }}
                  </small>
                </div>
                <div style=" padding-top: 3px" class="text-center">
                  <mdb-btn
                    color="success"
                    @click="addClientGet"
                    type="submit"
                    style="font-size: 9px"
                    p="r4 l4 t2 b2"
                    m="t0 b0"
                  >
                    {{ $t("add_clients") }}</mdb-btn
                  >
                </div>
                <div v-show="user_id" style="padding-top: 2px" class="text-center">
                  <mdb-btn
                    class="mr-1 px-3 py-2 text-white"
                    style="font-size: 10.3px"
                    color="secondary"
                    darkWaves
                    tag="a"
                    floating
                    size="sm"
                    @click="davernist_show = true"
                    >{{ $t("add_davernis") }}</mdb-btn
                  >
                </div>
                <div v-show="user_id" style="padding-top: 2px" class="text-center">
                  <mdb-btn
                    class="mr-1 px-3 py-2 text-white"
                    style="font-size: 10.3px"
                    color="info"
                    darkWaves
                    tag="a"
                    floating
                    size="sm"
                    @click="infoClient"
                    >{{ $t("info_client") }}
                  </mdb-btn>
                </div>
                <div v-show="user_id" style="padding-top: 2px" class="text-center">
                   <!-- <mdb-btn
                      class="mr-1 px-3 py-2 text-white"
                      style="font-size: 12px"
                      color="primary"
                      darkWaves
                      tag="a"
                      floating
                      size="sm"
                      @click="show_davernis_info = true"
                      >{{ $t("show_davernis") }}</mdb-btn
                    > -->
                </div>
                
              </div>
            </div>
          </div>
        </div>

        <div v-if="!client_id" style="height: 180px;"></div>
        <div v-show="client_id" class="get_product_add_p row border pt-3 pb-2 rounded mx-3 mt-0 pt-0" :class="{'applied': dataItem}">
          <div class="col-4">
            <erpSelectAdd
                :options="all_product_t.rows"
                @select="selectOptionPro"
                :selected="product_name"
                :label="$t('client')"
              />
              <small
                style="position: absolute; top: -7px; left: 20px; font-size: 11px"
                class="bg-white px-2 py-0"
                >{{ $t("product") }}</small
              >
              <small
                v-if="$v.product_name.$dirty && product_id == null"
                class="invalid-text  ml-2"
                style="margin-top: -8px;"
              >
                {{ $t("select_item") }}
              </small>
          </div>
          <!-- <div class="col-1">

          </div> -->
          <div class="col-3 mt-0">
            <input type="text" v-model="measureString"  @keyup="funcMeasure($event.target.value)"   
              class="form-control border mt-0" :disabled="measure_status" style="border:none; outline:none;  height:38px;" >
            <small
              style="position: absolute; top: -8px; left: 20px; font-size: 11px"
              class="bg-white px-2 py-0"
              >{{ $t("measure") }}</small
            >
          </div>
          
          <div class="col-3">
            <mdb-input
              class="m-0 p-0"
              v-model="note"
              size="md"
              outline
              group
              type="text"
              validate
              error="wrong"
              success="right"
            />
            <small
              style="position: absolute; top: -7px; left: 20px; font-size: 11px"
              class="bg-white px-2 py-0"
              >{{ $t("note") }}</small
            >
            <!-- <small
              class="invalid-text pt-4"
              style="margin-left: 5px"
              v-if="$v.summ.$dirty && !$v.summ.required"
            >
              {{ $t("name_invalid_text") }}
            </small> -->
          </div>
          <div class="col-2">
            
            <input type="text" v-model="product_priceString"  @keyup="funcProductSumma($event.target.value)"   
              class="form-control border mt-0" style="border:none; outline:none;  height:38px;" >
            <small
              style="position: absolute; top: -8px; left: 20px; font-size: 11px"
              class="bg-white px-2 py-0"
              >{{ $t("product_price") }}</small
            >
          </div>
          <div class="col-3 mt-2">
            <input type="text" v-model="uzs_summaString" v-on:click.capture="clickUzs" @keyup="funcUzsSumma($event.target.value)"   
              class="form-control border mt-0" style="border:none; outline:none;  height:33px;" >
            <small
              style="position: absolute; top: -8px; left: 20px; font-size: 11px"
              class="bg-white px-2 py-0"
              >UZS</small
            >
          </div>
          <div class="col-3 mt-2" >
            <input type="text" v-model="dollor_summaString" :disabled="measure_status" v-on:click.capture="clickDollor"  @keyup="funcDollorSumma($event.target.value)"   
              class="form-control border mt-0" style="border:none; outline:none;  height:33px;" >
            <small
              style="position: absolute; top: -8px; left: 20px; font-size: 11px"
              class="bg-white px-2 py-0"
              >Доллор</small
            >
          </div>

          <div class="col-3 mt-2" >
            <input type="text"  v-model="kurs_summaString" :disabled="measure_status"  @keyup="funcKursSumma($event.target.value)"   
              class="form-control border mt-0" style="border:none; outline:none;  height:33px;" >
            <small
              style="position: absolute; top: -8px; left: 20px; font-size: 11px"
              class="bg-white px-2 py-0"
              >Курс</small
            >
          </div>
          
          <div class="col-3 mt-2">
            <erpSelectHisob
              :options="allHisob.rows"
              @select="selectOptionHisob"
              :selected="hisob_name"
              :label="$t('hisob')"
              size="sm"
            />
          </div>
          <div class="col-3">
            <p style="font-size: 14.5px; font-style: italic;" class="pb-0 mb-0 font-weight-bold">Общие сумма : 
              <small style="font-size: 14.5px; font-style: italic;">{{all_summaString}}</small>
            </p>
          </div>
          <div class="col-3" v-if="!measure_status">
            <p v-if="diff_summa>0" style="font-size: 14.5px; font-style: italic;" class="pb-0 mb-0 font-weight-bold text-danger"> Не хватить : 
              <small style="font-size: 14.5px; font-style: italic;">{{diff_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small>
            </p>
            <p v-else-if="diff_summa==0" style="font-size: 14.5px; font-style: italic;" class="pb-0 mb-0 font-weight-bold text-info"> 
            </p>
            <p v-else style="font-size: 14.5px; font-style: italic;" class="pb-0 mb-0 font-weight-bold text-success"> Больше денег : 
              <small style="font-size: 14.5px; font-style: italic;">{{diff_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small>
            </p>
          </div>
          <div class="col-12">
            <div class="photo w-100 d-flex justify-content-center rounded" v-if="image_url_str">
              <img :src="hostname + image_url_str" width="120" alt="" class="shadow border rounded">
            </div>
          </div>
          
        </div>
        
          <webcam  v-show="showPhoto" @getPhotosub="takePhoto"/>


        <!-- <hr class="mt-4 mb-3 gradint" /> -->
        <div v-if="client_id" class="d-flex justify-content-end w-100">
          <mdb-btn
            class="mr-3 px-3 py-2 mt-2 text-white"
            style="font-size: 12px"
            color="success"
            darkWaves
            tag="a"
            floating
            @click="getPhoto"
            size="sm"
            >{{ $t("photo") }}</mdb-btn
          >
          <mdb-btn
            v-if="image_url_str"
            class="mr-3 px-3 py-2 mt-2 text-white"
            style="font-size: 12px"
            color="success"
            darkWaves
            tag="a"
            floating
            @click="save"
            size="sm"
            >{{ $t("save") }}</mdb-btn
          >
        </div>

        <div>
          <clientAdd @select="searchClientFromComponent" ref="addclients"/>
        </div>


        <div class="getProductStyle">
          <div class="px-0 mt-3">
            <loader v-if="loading"/>
            <table class="myTableDebitorList">
              <thead>
                <tr class="header py-3 table_header_bg">
                  <th  width="40" class="text-left">№</th>
                  <th>{{$t('client_name')}}</th>
                  <th>{{$t('product')}}</th>
                  <th>{{$t('measure')}}</th>
                  <th>{{$t('note')}}</th>
                  <th>{{$t('date')}}</th>
                  <th >{{$t('ostatka')}}</th>
                  <!-- <th width="80" class="text-center">{{$t('Action')}}</th> -->
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,rowIndex) in comenList" :key="rowIndex" @click="editRow(row)">
                  <td> <span >{{rowIndex+1}}</span> </td>
                  <td> <span >{{row.client.fio}}</span> </td>
                  <td> <span >{{row.product.name}}</span> </td>
                  <td> <span >{{row.qty_real}}</span> </td>
                  <td> <span >{{row.note}}</span> </td>
                  
                  <td> <span >{{row.updated_date_time.slice(8,10) + '-' + row.updated_date_time.slice(5,7) + '-' + row.updated_date_time.slice(0,4)}}</span> <span class="ml-2">{{row.updated_date_time.slice(11,16)}}</span> </td>
                  <td> <span class="text-success">{{row.credit_sum.toFixed(1)}}</span> </td>
                  <!-- <td class="text-center">
                    <i class="fas fa-pen editIcon mask waves-effect  m-0 pr-2"  @click="editRow" :data-row="rowIndex"></i>
                  </td> -->
                </tr>
              </tbody>
            </table>
          </div>
        </div>
        <modal-train v-if="davernist_show" :show="davernist_show" headerbackColor="white"  titlecolor="black" :title="$t('add_davernis')" 
          @close="davernist_show = false" width="60%">
            <template v-slot:body>
              <add-davernis @close="davernist_show = false"  :client_id="user_id" >
              </add-davernis>
            </template>
        </modal-train>
        <modal-train  :show="show_davernis_info" headerbackColor="white"  titlecolor="black" :title="$t('show_davernis')" 
          @close="show_davernis_info = false" width="85%">
            <template v-slot:body>
              <info-davernis @close="show_davernis_info = false"  :option="davernis_info" >
              </info-davernis>
            </template>
        </modal-train>
        
        <modal-train  :show="add_client_show" headerbackColor="white"  titlecolor="black" :title="$t('add_clients')" 
          @close="add_client_show = false" width="85%">
            <template v-slot:body>
              <addClientModal @close="add_client_show = false"  @close_data="close_clientAdd" ref="addClientref">
              </addClientModal>
            </template>
        </modal-train>
        <modal-train  :show="invoice_show_input" headerbackColor="white"  titlecolor="black" :title="$t('show_davernis')" 
          @close="invoice_show_input = false" width="85%">
            <template v-slot:body>
              <info-invoice-get @close="invoice_show_input = false" @printed="printedCheck" v-if="invoice_show_input"  :option="invoice_list" >
              </info-invoice-get>
            </template>
        </modal-train>
        <modal-train  :show="client_info_show" headerbackColor="white"  titlecolor="black" :title="$t('show_davernis')" 
          @close="client_info_show = false" width="95%">
            <template v-slot:body>
              <info-client @close="client_info_show = false" v-if="client_info_show"  :option="client_info" >
              </info-client>
            </template>
        </modal-train>
      </div>
    </div>
    <block ref="blocked"></block>
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>
    <Toast ref="message"></Toast>
    <Alert ref="alert"></Alert> 
  </div>
</template>

<script>
import { mdbInput, mdbBtn, mdbIcon,
  mdbTbl,
  mdbTblHead,
  mdbTblBody,
  mdbBtnGroup,
  mdbDropdown,
  mdbDropdownItem,
  mdbDropdownMenu,

  } from "mdbvue";
import checkzaxira from '../sell/checkGetProduct.vue'
import addClientModal from '../client/addClientModal.vue';
import clientAdd from './clientAddForGetProduct.vue';
import erpSelect from "../../components/erpSelectFioSearch.vue";
import erpSelectAdd from "../../components/erpSelectAdd.vue";
import lineSelect from "../../components/lineSelect.vue";
import InputSearch from '../../components/inputSearch';
import InputSearchYear from '../../components/inputSearchYear';
import addDavernis from './addDavernis.vue'
import infoDavernis from './infoDavernis.vue'
import webcam from '../webcam/webcam_Add.vue'
import infoInvoiceGet from './infoInvoiceGet.vue'
import infoClient from '../client/clientInfo.vue'
import erpSelectHisob from "../../components/erpSelect.vue";




import { required } from "vuelidate/lib/validators";
import {mapActions,mapGetters, mapMutations} from 'vuex'

export default {
  data() {
    return {
      davernist_show : false,
      show_davernis_info: false,
      davernis_info: {},
      invoice_show_input:false,
      invoice_list: [],

      add_client_show: false,
      showPhoto: false,
      image_url_str: '',
      hostname: this.$store.state.server_ip,


      modal_info: "",
      modal_status: false,
      loading: false,
      id: this.$route.params.id,

      allow: 'allow',
      dataItem: false,
      itter: 1,
      
      name: "",
      company_name: '',
      company_id: null,
      user_name: "",
      user_id: null,
      product_name: '', 
      product_id: null,
      indexDis: -1,
      indexClintGroup: -1,
      summ: 0,
      dolg_summ: 0,
      credit_sum: 0,
      note: '',
      today_date: '',

      measure: 0,
      measureString: '',

      uzs_summa: 0,
      uzs_summaString: '',
      
      dollor_summa: 0,
      dollor_summaString: '',
      
      all_summa: 0,
      all_summaString: '0',
      
      product_price: 0,
      product_priceString: '',
      
      kurs_summa: 0,
      kurs_summaString: '',

      diff_summa: 0,

      measure_status: false,
      hisob_name: '',
      hisob_id: 0,

      comenList: [],
      OstatkaList: [],

      client_name: '',
      client_id: null,
      passport_number: '',
      phone_number: '',
      born_date: '',
      parol_tg: '',

      client_info_show: false,
      client_info: {},
      oshibka_client: 0,
      real_time_ostatk: 0,
      allList: [],
      check_number: 1,



      show_check: false,
    };
  },
  components: {
    mdbIcon,
    addDavernis,
    infoDavernis,
    mdbInput,
    mdbBtn,
    mdbTbl,
    mdbTblHead,
    mdbTblBody,
    mdbBtnGroup,
    mdbDropdown,
    mdbDropdownItem,
    mdbDropdownMenu,
    erpSelect,
    erpSelectAdd,
    clientAdd, webcam,
    lineSelect, 
    InputSearch, 
    addClientModal,
    infoInvoiceGet,
    infoClient,
    checkzaxira,
    InputSearchYear,
    erpSelectHisob
  },
  validations: {
    user_name: {
      required,
    },
    product_name: {
      required,
    },
  },
  async mounted(){
      if(localStorage.AuthId){
        await this.fetchUserAccess(localStorage.user_id);
      }
      else{
        console.log('AuthId topilmadi');
        this.$router.push('/');
      }

      await this.fetchClient();
      await this.fetch_product_t();
      await this.fetchHisob();
      console.log('allHisob', this.allHisob)
      await this.fetchGetZaxiraList();
      let today = new Date().toISOString();
      this.today_date = today.slice(0,10);
      await this.nbuKurs();
      if(localStorage.dollor_kurs){
        this.kurs_summa = parseFloat(localStorage.dollor_kurs);
        this.kurs_summaString =  this.kurs_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
      }
      
      // console.log(this.allClient)
    },
   computed: mapGetters(['all_district_t', 'all_client_controler', 'allClient','allHisob',
      'user_kassa_list', 'all_contragent_t', 'allCompany', 'all_product_t', 'user_to_zaxira']),
  methods: {
    ...mapActions(['fetch_district_t', 'fetch_client_controler', 'fetchClient', 'fetch_contragent_t', 
      'fetchHisob', 'fetchKassa_userId', 'fetchCompany', 'fetch_product_t']),
    ...mapMutations(['clearZaxiraList', 'get_ostatka_check_for_get', 'get_invoice_for_invoice']),
    async nbuKurs(){
      try{
        const response = await fetch("https://cbu.uz/uz/arkhiv-kursov-valyut/json/");
        const data = await response.json();
        console.log('json valyuta')
        console.log(data)
        if(data.length>0){
          localStorage.dollor_kurs = parseInt(data[0].Rate);
        }
      }
      catch(error){
        localStorage.dollor_kurs = 0;
        console.log(error);
      }
    },
    selectOptionHisob(option){
      console.log('hisob_name', option)
      this.hisob_name = option.name;
      this.hisob_id = option.id;
    },
    async close_clientAdd(data){
      // console.log('new client data');
      // console.log(data);
      this.add_client_show = false;
      this.user_name = data.fio;
      this.user_id = data.id;
      this.check_number = 1;
      await this.selectOptionClient(data)
    },
    infoClient(){
      this.client_info_show = true;
    },
    takePhoto(img){
      // console.log('img')
      // console.log(img)
      this.image_url_str = img;
      this.showPhoto = false;
      // console.log('this.showPhoto')
      // console.log(this.showPhoto)
    },
    getPhoto(){
      this.showPhoto = true;
    },
    addClientGet(){
      this.add_client_show = true; 
      this.$refs.addClientref.MountedFunc();
    },
    async selectClientPassport(option){
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.passport_number = option.passport_number
      this.phone_number = option.note;
      this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      this.fetchOstatka(option.id);
      this.davernis_info = option;
      this.oshibka_client = option.auth_user_updator_id;
      if(option.car_number == null){
        await this.updateClientPass(option);
      }
      else{
        this.parol_tg = option.car_number;
      }
      // console.log(option)
    },
    async selectClientPhone(option){
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.passport_number = option.passport_number;
      this.phone_number = option.note;
      this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      this.oshibka_client = option.auth_user_updator_id;
      this.fetchOstatka(option.id);
      this.davernis_info = option;
      if(option.car_number == null){
        await this.updateClientPass(option);
      }
      else{
        this.parol_tg = option.car_number;
      }

    },
    async selectClientBorn(option){
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.passport_number = option.passport_number
      this.phone_number = option.note;
      this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      this.oshibka_client = option.auth_user_updator_id;
      this.fetchOstatka(option.id);
      this.davernis_info = option;
      if(option.car_number == null){
        await this.updateClientPass(option);
      }
      else{
        this.parol_tg = option.car_number;
      }

    },

    async selectOptionClient(option){
      this.client_info = option;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number = option.passport_number
      this.phone_number = option.note;
      this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      this.oshibka_client = option.auth_user_updator_id;
      this.fetchOstatka(option.id);
      this.davernis_info = option;
      if(option.car_number == null){
        await this.updateClientPass(option);
      }
      else{
        this.parol_tg = option.car_number;
      }
    },
    async selectOptionUser(option) {
      // console.log('option main client')
      // console.log(option)
      this.client_info = option;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number = option.passport_number
      this.phone_number = option.note;
      this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      this.oshibka_client = option.auth_user_updator_id;
      this.fetchOstatka(option.id);
      this.davernis_info = option;
      if(option.car_number == null){
        await this.updateClientPass(option);
      }
      else{
        this.parol_tg = option.car_number;
      }
    },
    async searchClientFromComponent(option){
      // console.log(option)
      this.client_info = option;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number = option.passport_number;
      this.phone_number = option.note;
      this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      this.oshibka_client = option.auth_user_updator_id;
      this.fetchOstatka(option.id);
      this.davernis_info = option;
      if(option.car_number == null){
        await this.updateClientPass(option);
      }
      else{
        this.parol_tg = option.car_number;
      }
    },
    selectMainClient(option){
      this.davernis_info = option;
      this.user_name = option.client.fio;
      this.user_id = option.client.id;
      // this.oshibka_client = option.auth_user_updator_id;
    },
    fetchPassword_client(length) {
      var result = '';
      var characters  = 'abcdefghijklmnopqrstuvwxyz123456789';
      var charactersLength = characters.length;
      for ( var i = 0; i < length; i++ ) {
          result += characters.charAt(Math.floor(Math.random() * charactersLength));
      }
      return result;
    },
    async updateClientPass(data){
      // console.log('update qilish uchun option')
      // console.log(data)
      var car_number = await this.fetchPassword_client(5);
      // console.log(car_number)
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "fio" : data.fio,
          "adddress" : data.adddress,
          "passport_number" : data.passport_number,
          "phone_number": data.phone_number,
          "addiotionala_information": data.addiotionala_information,
          "tegirmonClientGroupid" : data.TegirmonClientGroupid,
          "note" : data.note,
          "image_url": data.image_url,
          "passport_image_base_64": data.passport_image_base_64,
          "passport_image_url": data.passport_image_url,
          "car_number": car_number,
          "id" : data.id,
          "auth_user_updator_id": data.auth_user_updator_id
        })
      };
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonClient", requestOptions);
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const tryjson = await response.json();
          // console.log('tryjson')
          // console.log(tryjson)
          this.parol_tg = tryjson.car_number;
          this.$refs.message.success('Added_successfully')
          // console.log('fire' + i)
        }
        else{
          const data = await response.text();
          this.modal_info = data;
          this.modal_status = true;
          
        }
      }
      catch{
        this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
      }
    },
    async fetchOstatka(id){
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonClientOstatkas/getPaginationGetByClientClientIdList?page=0&size=100&client_id=" + id);
        const data = await response.json();
        // console.log('weqeqw')
        // console.log(data)
        // this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          this.OstatkaList = data.items_list;
          this.fetchOldList(id);
          // console.log('data.items_list')
          // console.log(data.items_list)
          this.get_ostatka_check_for_get(data.items_list);
          // this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
          // const data = await response.text();
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        // this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
    },
    async fetchOldList(id){
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/getPaginationByClientId?page=0&size=10000&client_id=" + id);
        const data = await response.json();
        // this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          this.allList = data.items_list;
          return true;
        }
        else{
          // const data = await response.text();
          this.modal_info = this.$i18n.t('network_ne_connect');
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

    
    selectOptionPro(option){
      console.log(option)
      this.product_name = option.name;
      this.product_id = option.id;
      this.product_price = option.price;
      this.product_priceString = option.price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      if(option.auth_user_creator_id == 1){
        this.measure_status = true;
        this.measure = 0;
        this.measureString = '';
        this.dollor_summa = 0;
        this.dollor_summaString = '';
        this.all_summa = 0;
        this.all_summaString = 0;
      }
      else{
        this.measure_status = false;
      }
    },
    selectOption(option) {
      // console.log(option)
      this.company_name = option.name;
      this.company_id = option.id;
    },
    addClient(){
      this.$refs.addclients.openClientAdd();
    },
    onChangeCount(data, i){
      this.datasource.rows[i].real_qty = data
    },
    allowInvoice(){
      // console.log(this.datasource.rows)
      if(this.$v.$invalid)
      {
        this.$v.$touch();
        this.$refs.message.warning('please_fill')
        return;
      }
      this.itter++
      
      if(this.itter%2==0){
        this.dataItem = true;
        this.allow = 'prev_apply'
        if(this.datasource.rows.length == 0){
          this.addRow();
        }
      }
      else{
        this.dataItem = false;
        this.allow = 'allow'
      }
      
      // console.log('review')
    },
    async editRow(data){
      let select_data_id = data.id;
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/getAnyInvoiceFullInfoById?invoice_id=" + select_data_id);
        const data = await response.json();
        // console.log('weqeqw')
        // console.log(data)
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          this.invoice_list = data;
          this.invoice_show_input = true;
          this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
          this.modal_info = this.$i18n.t('network_ne_connect');
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
    
    async save(){
      if(!this.uzs_summa){
        this.uzs_summa = 0;
        this.uzs_summaString = '0'
      }
      if(!this.dollor_summa){
        this.dollor_summa = 0;
        this.dollor_summaString = '0';
      }
      if(this.dollor_summa != 0 || this.uzs_summa != 0){
        await this.fetchKassa_userId(localStorage.user_id);
        if(this.user_kassa_list.length){
          localStorage.kassa_id = this.user_kassa_list[0].id;
          localStorage.kassa_num = this.user_kassa_list[0].num_1;
        }
        else{
          this.$refs.alert.error('Bu foydalanuvchi kassaga biriktirilmagan, unda savdo qilish huquqi yuq !');
          localStorage.kassa_id = 0;
          localStorage.kassa_num = 0;
          return;
        }
      }
      if(this.measure_status){
        this.measure = this.uzs_summa;
        this.measureString = this.measure.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
        this.all_summa = this.measure;
        this.all_summaString = this.measureString;
      }
      if(!this.measure_status && (this.dollor_summa!=0 || this.uzs_summa!=0) && (this.diff_summa < -1 || this.diff_summa > 1) ){
        this.$refs.alert.error('Olinayotgan pul summasi xato yana bir tekshirib kuring !');
        return;
      }
      // console.log('this.OstatkaList')
      var real_time_ostatka = parseFloat(this.measure);
      for(let i=0; i<this.OstatkaList.length; i++){
        if(this.OstatkaList[i].TegirmonProductid == this.product_id){
          real_time_ostatka += parseFloat(this.OstatkaList[i].real_qty)
        }
      }

      this.check_number = 1;
      if(this.allList.length>0){
        if(this.allList[0].check_number != undefined ){
          if(parseInt(this.allList[0].check_number) == 0)
          {
            this.check_number = parseInt(this.allList[1].check_number) + 1;
          }
          else{
            this.check_number = parseInt(this.allList[0].check_number) + 1;
          }
        }
      }

      // console.log(real_time_ostatka)
      this.real_time_ostatk = real_time_ostatka
      if(this.$v.$invalid)
      {
        this.$v.$touch();
        this.$refs.message.warning('please_fill')
        return false;
      }
      let note_str = '';
      if(this.uzs_summa>0){
        note_str = this.note + '( Клиент: ' + this.user_name + ' )';
      }
      else{
        note_str = this.note;
      }
      const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "note" : note_str,
            "tegirmonClientid" : this.user_id,
            "summ" : this.summ,
            "tegirmonProductid" : this.product_id,
            "qty_real" : this.measure,
            "image_str_url" : this.image_url_str,
            "id" : 0,
            "tegirmonAuthid": localStorage.AuthId,
            "check_number": this.check_number,
            "user_name": this.client_name,
            "hisob_name": this.hisob_name,
            "hisob_id": this.hisob_id,
            "poluchit_summa": this.uzs_summa,
            "poluchit_summa_str": this.uzs_summaString,

            "pol_dollor_summa": this.dollor_summa,
            "pol_dollor_sum_str": this.dollor_summaString,
            "pol_kurs_sum": this.kurs_summa,
            "pol_kurs_sum_str": this.kurs_summaString,
            "pol_all_sum": this.all_summa,
            "pol_all_sum_str": this.all_summaString,
            "pol_product_price": this.product_price,
            
            "auth_user_name": localStorage.user_name,
            "kassa_id": localStorage.kassa_id,
            "credit_sum": real_time_ostatka,
          })
        };
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/postInvoiceBugdoyniZaxiragaOlibQolishClientdan", requestOptions);
          const data = await response.json();
          // console.log(data)
          this.loading = false;
          // console.log(response.status)
          if(response.status == 201 || response.status == 200)
          {
            this.fetchSendTgMessage(this.client_name, this.product_name, this.user_id, this.measure);
            await this.fetchGetZaxiraList();
            await this.fetchOstatka(this.user_id);
            await this.selectInvoiceItem(data);
            this.measure = 0;
            this.measureString = '';
            this.user_name = '';
            this.user_id = null;
            this.product_name = '';
            this.product_id = null;
            this.note = '';
            this.image_url_str = '';
            this.real_time_ostatk = 0;
            this.uzs_summa = null;
            this.uzs_summaString = '';
            this.dollor_summa = null;
            this.dollor_summaString = '';
            this.diff_summa = 0;
            this.all_summa = 0;
            this.all_summaString = '';
            this.hisob_name = '';
            this.hisob_id = 0;
            this.$refs.message.success('Added_successfully')
            // console.log('fire' )
          }
          else{
            this.modal_info = data;
            this.modal_status = true;
          }
        }
        catch{
          this.loading = false;
          this.modal_info = this.$i18n.t('network_ne_connect'); 
          this.modal_status = true;
        }
    },
    async fetchSendTgMessage(fio, product, client_id, qty){
      var message = fio + ' hisobingizga ' + qty + 'kg ' + product +  '  qo`shildi.✅'  + '  %0A' + 'Hisobingizdagi qoldiq ' + product + ' = ' + this.real_time_ostatk.toFixed(1) + ' kg'
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonClientOstatkas/sendMessageToTegirmonClient?message_str=" + message +"&client_id=" + client_id);
        const data = await response.json();
        // console.log('weqeqw')
        console.log(data)
        // this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          return true;
        }
        else{
          this.modal_info = this.$i18n.t('network_ne_connect');
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
    async selectInvoiceItem(data){
      // console.log(data)
      // console.log(data.id)
      let select_invoice_id = data.id;
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/getAnyInvoiceFullInfoById?invoice_id=" + select_invoice_id);
        const data = await response.json();
        // console.log('weqeqw')
        // console.log(data)
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
            this.get_invoice_for_invoice(data)
            this.show_check = true;
          this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
          this.modal_info = this.$i18n.t('network_ne_connect');
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
    async printedCheck(option){
      this.invoice_show_input = false;
      await this.fetchOstatka(option.client_id);
      await this.selectInvoiceItem(option);
      // console.log(option);
    },

    async fetchGetZaxiraList(){
      let today = new Date().toISOString()
      this.today_date = today.slice(0,10)
      // console.log(this.allClient)
      let a = this.today_date + 'T00:00:00.000Z';
      let b = this.today_date + 'T23:59:00.000Z';
      try{
          const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/getPaginationZahiragaOlibQolinganTovarlarListilarListi?page=0&size=120&b_date=" + a + '&e_date=' + b);
          const data = await response.json();
          // console.log('data')
          // console.log(data)
          if(response.status == 201 || response.status == 200)
          {
            this.comenList = data.items_list;
            // this.$refs.message.success('Successfully_removed')
          }
          else{
            this.modal_info = this.$i18n.t('network_ne_connect'); 
            this.modal_status = true;
          }
        }
        catch{
          this.modal_info = this.$i18n.t('network_ne_connect'); 
            this.modal_status = true;
        }
    },

    async deleteInvoice(item){
      // console.log(item);
      try{
        const requestOptions = {
          method : "delete",
        };
        const response = await fetch(this.$store.state.hostname + "/TegirmonTortilganBugdoyRoyxatiGroupDetails/" + item.group_detail_id, requestOptions);
        const data = await response.text();

        if(response.status == 201 || response.status == 200)
        {
          this.$refs.message.success('Successfully_removed')
        }
        else{
          this.modal_info = data;
          this.modal_status = true;
        }
      }
      catch{
        this.modal_info = data;
        this.modal_status = true;
      }
      try{
        const requestOptions = {
          method : "delete",
        };
        const response = await fetch(this.$store.state.hostname + "/TegirmonTortilganBugdoyRoyxatis/" + item.ruyxat_id, requestOptions);
        const data = await response.text();

        if(response.status == 201 || response.status == 200)
        {
          this.$refs.message.success('Successfully_removed')
        }
        else{
          this.modal_info = data;
          this.modal_status = true;
        }
      }
      catch{
        this.modal_info = data;
        this.modal_status = true;
      }
      try{
        const requestOptions = {
          method : "delete",
        };
        const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/" + item.invoice_id, requestOptions);
        const data = await response.text();

        if(response.status == 201 || response.status == 200)
        {
          this.$refs.message.success('Successfully_removed')
        }
        else{
          this.modal_info = data;
          this.modal_status = true;
        }
      }
      catch{
        this.modal_info = data;
        this.modal_status = true;
      }


      this.clearZaxiraList();
      this.$router.back();  
    },

    async fetchUserAccess(id){
      try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonUserAccess/getTegirmonUserAccessUserId?user_id=' + id);
          const data = await res.json();
          
          if(res.status == 200 || res.status == 201){
            console.log('this is by id access user')
            console.log(res)
            if(data.product_olish == false){
              this.$refs.blocked.show_block();
            }
          }
      }
      catch(error){
          console.log(error)
      }
    },
    funcMeasure(n){
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
        tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9'){
        tols = tols.slice(0,tols.length-1)
       }
       this.measureString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.measureString.length; i++){
        if(this.measureString[i] != ' '){
          temp += this.measureString[i];
        }
       }
      this.measure = parseFloat(temp);
      this.all_summa = this.measure * this.product_price;
      this.all_summaString = this.all_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      this.diff_summa = this.all_summa - (this.uzs_summa + this.dollor_summa*this.kurs_summa);
    },

    funcUzsSumma(n){
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
        tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9'){
        tols = tols.slice(0,tols.length-1)
       }
       this.uzs_summaString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.uzs_summaString.length; i++){
        if(this.uzs_summaString[i] != ' '){
          temp += this.uzs_summaString[i];
        }
       }
      this.uzs_summa = parseFloat(temp);
      if(!this.uzs_summa){
        this.uzs_summa = 0;
      }
      this.all_summa = this.measure * this.product_price;
      this.all_summaString = this.all_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      this.diff_summa = this.all_summa - (this.uzs_summa + this.dollor_summa*this.kurs_summa);
    },

    clickUzs(){
      console.log('hiy')
      if(!this.dollor_summa){
        this.dollor_summa = 0;
        this.dollor_summaString = '';
      }
      
      this.uzs_summa = (parseFloat(this.all_summa) - parseFloat(this.kurs_summa * this.dollor_summa));
      this.uzs_summaString = this.uzs_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

      this.all_summa = this.measure * this.product_price;
      this.all_summaString = this.all_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      this.diff_summa = this.all_summa - (this.uzs_summa + this.dollor_summa*this.kurs_summa);
    },
    
    funcDollorSumma(n){
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
        tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9'){
        tols = tols.slice(0,tols.length-1)
       }
       this.dollor_summaString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.dollor_summaString.length; i++){
        if(this.dollor_summaString[i] != ' '){
          temp += this.dollor_summaString[i];
        }
       }
      this.dollor_summa = parseFloat(temp);

      if(!this.dollor_summa){
        this.dollor_summa = 0;
      }
      this.all_summa = this.measure * this.product_price;
      this.all_summaString = this.all_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      this.diff_summa = this.all_summa - (this.uzs_summa + this.dollor_summa*this.kurs_summa);
    },
    clickDollor(){
      console.log('hiy')
      if(!this.uzs_summa){
        this.uzs_summa = 0;
        this.uzs_summaString = '';
      }
        this.dollor_summa = (parseFloat(this.all_summa - this.uzs_summa)/parseFloat(this.kurs_summa)).toFixed();
        this.dollor_summaString = this.dollor_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      
      this.all_summa = this.measure * this.product_price;
      this.all_summaString = this.all_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      this.diff_summa = this.all_summa - (this.uzs_summa + this.dollor_summa*this.kurs_summa);
    },
    funcProductSumma(n){
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
        tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9'){
        tols = tols.slice(0,tols.length-1)
       }
       this.product_priceString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.product_priceString.length; i++){
        if(this.product_priceString[i] != ' '){
          temp += this.product_priceString[i];
        }
       }
      this.product_price = parseFloat(temp);
      this.all_summa = this.measure * this.product_price;
      this.all_summaString = this.all_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      this.diff_summa = this.all_summa - (this.uzs_summa + this.dollor_summa*this.kurs_summa);
    },
    funcKursSumma(n){
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
        tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9'){
        tols = tols.slice(0,tols.length-1)
       }
       this.kurs_summaString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.kurs_summaString.length; i++){
        if(this.kurs_summaString[i] != ' '){
          temp += this.kurs_summaString[i];
        }
       }
      this.kurs_summa = parseFloat(temp);

      this.diff_summa = this.all_summa - (this.uzs_summa + this.dollor_summa*this.kurs_summa);

    },
  },
};
</script>

<style lang="scss">
.applied {
  pointer-events: none;
}
.fixed-column {
  overflow-x: scroll;
  /* overflow-x: hidden; */

  margin-left: 100px;
  padding-bottom: 200px;
}

.fixed-column th,
.fixed-column td {
  white-space: nowrap;
}

.fixed-column tr th:first-child,
.fixed-column tr td:first-child {
  position: absolute;
  width: 50px;
  top: auto;
  margin-left: -50px;
  margin-top: 0;
}
.delIcon:hover {
  color: #000;
}
.delIcon {
  color: red;
}
.gradint {
  background: #8e2de2; /* fallback for old browsers */
  background: -webkit-linear-gradient(
    to right,
    #4a00e0,
    #8e2de2
  ); /* Chrome 10-25, Safari 5.1-6 */
  background: linear-gradient(
    to right,
    #4a00e0,
    #8e2de2
  ); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
}

.myTableDebitorList {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; overflow-x:auto
}
.myTableDebitorList th{
  font-weight: 600;
  font-size:11px;
}
.myTableDebitorList td{
  font-size:11px;
  
}
.myTableDebitorList td {
  text-align: left;
  padding: 4px 10px;
}
.myTableDebitorList th{
  text-align: left;
  padding: 5px 10px;
}

.myTableDebitorList tr {
  border-bottom: 1px dashed rgb(240, 240, 240);
  &:hover{
    background: #afd1fd;
  }
}

.myTableDebitorList tr.header, .myTableDebitorList tr:hover {
  // background-color: #f1f1f1;
}
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
.editIcon{
  color:#01b348;
  font-size: 13px;
}
.getProductStyle{
  height: 280px;
  overflow: hidden;
  overflow-y: scroll;
}
.getProduct{
  min-height: calc(100vh - 670px);
}
.bg_red_color{
  background: #ff937d !important;
}
.get_product_add_p{
  background: #fbfcff;
}
.header_input_title{
  position:absolute; 
  top:-8px; 
  left:15px; 
  font-size: 11px; 
  color: rgb(62, 62, 139);
}
</style>
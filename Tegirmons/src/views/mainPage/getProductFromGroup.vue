<template>
  <div>
    <checkzaxira v-if="show_check" @close="show_check = false"/>
    <div v-else>
      <loader v-if="loading" />
      <div v-else class="getProduct  mt-0">
        <!-- Search qismi -->
        <div class="container-fluid">
          <div class="row d-flex align-items-center px-3 mb-0 pt-2 main_header_bg_new border rounded shadow" :class="{'bg_red_color': oshibka_client == 1}">
            <div class="col-3">
              <div class="m-0 p-0 pt-2" >
                <erpSelect
                :options="allClient.rows"
                @select="selectOptionClient"
                :selected="client_name"
                size="sm"
                class="bg-white"
                url="/TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
                :label="$t('client')"
                />
                <small style="position:absolute; top:-8px; left:15px; font-size: 11px; color: rgb(62, 62, 139);" 
                  class="font-weight-bold px-2 py-0">{{$t('client')}}</small>
              </div>
            </div>


            <!-- <div class="col-3">
              <mdb-input class="m-0 p-0" v-model="passport_number" size="md"  outline  group type="text" validate error="wrong" success="right"/>
              <small style="position:absolute; top:-7px; left:20px; font-size: 11px;" class="bg-white px-2 py-0">{{$t('passport_number')}}</small>
          
            </div> -->
            <div class="col-3">
              <input-search  @select="selectClientPassport" :label="$t('passport_number')" :selected="passport_number"
                url="/TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
                ref="search_client"  style="height:32px;" placeholder="AA1234567">
              </input-search>
            </div>

            <div class="col-3">
              <input-search-year  @select="selectClientBorn" type="text" :label="$t('born_date')" :selected="born_date" placeholder="дд-мм-гггг"
                url="/TegirmonClient/getPaginationByBornDate?page=0&size=100&born_date_str="
                ref="search_client_born_date" style="height:32px;" >
              </input-search-year>
            </div>
            <div class="col-3">
              <input-search  @select="selectClientPhone" :label="$t('note')" :selected="phone_number"
                url="/TegirmonClient/getPaginationByNote?page=0&size=100&note_str="
                ref="search_client_phone" style="height:32px;" placeholder="1234">
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
                    <th>{{$t('client_name')}}</th>
                    <th>{{$t('passport_number')}}</th>
                    <th>{{$t('phone_number')}}</th>
                    <th>{{$t('note')}}</th>
                    <th>{{$t('product')}}</th>
                    <th>{{$t('measure')}}</th>
                    <th>{{$t('date')}}</th>
                    <!-- <th >{{$t('lessons_cout')}}</th> -->
                  
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
                    <td> <span >{{row.updated_date_time.slice(0,10)}}</span> <span class="ml-2">{{row.updated_date_time.slice(11,16)}}</span> </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>


        <!-- Search qismi -->
        <div class="row mx-3 mt-3 shadow pt-2 mb-3" :class="[{'applied': dataItem}, { 'alert_success': user_name, 'alert_danger' : !user_name  }]">
          <div class="col-6">
            <div class="d-flex">
              <!-- <erpSelect
                :options="allClient.rows"
                @select="selectOptionUser"
                :selected="user_name"
                url="/TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
                :label="$t('client')"
              />
              <small
                style="position: absolute; top: -7px; left: 20px; font-size: 11px"
                class="bg-white px-2 py-0"
                >{{ $t("client") }}</small
              > -->
              <!-- <small
                v-if="$v.user_name.$dirty && user_id == null"
                class="invalid-text ml-2"
                style="margin-top: 35px;"
              >
                {{ $t("select_item") }}
              </small> -->
              <mdb-input class="m-0 p-0 w-100" placeholder="Выбирать клиент" disabled :value="user_name" size="sm"  outline  group type="text" validate error="wrong" success="right"/>

              <div style="width: 170px; padding-top: 0px" class="text-center">
                <mdb-btn
                  color="success"
                  @click="add_client_show = true"
                  type="submit"
                  style="font-size: 9px"
                  p="r4 l4 t2 b2"
                  m="t0 b0"
                >
                  {{ $t("add") }}</mdb-btn
                >
              </div>
            </div>
          </div>
          <div class="col-3" v-show="false">
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
          <div class="col-6 mt-0" v-show="user_id">
            <mdb-btn
              class="mr-1 text-white"
              style="font-size: 10px"
              color="success"
              darkWaves
              tag="a"
              floating
              size="sm"
              p="r4 l4 t2 b2"
              m="t0 b0"
              @click="davernist_show = true"
              >{{ $t("add_davernis") }}</mdb-btn
            >
            <mdb-btn
              class="mr-1 text-white"
              style="font-size: 10px"
              color="primary"
              darkWaves
              tag="a"
              floating
              size="sm"
              p="r4 l4 t2 b2"
              m="t0 b0"
              @click="infoClient"
              >{{ $t("info_client") }}</mdb-btn
            >
          </div>
        </div>

        <!-- Search qismi -->



        <div class="row mx-3 back_ground_zaxira shadow" v-if="user_to_zaxira.client_new_name">
          <div class="col-3">
            <mdb-input class="m-0 p-0" disabled :value="user_to_zaxira.client_new_name" size="sm"  outline  group type="text" validate error="wrong" success="right"/>
          </div>
          <div class="col-3">
            <mdb-input class="m-0 p-0" disabled :value="user_to_zaxira.product_name" size="sm"  outline  group type="text" validate error="wrong" success="right"/>
          </div>
          <div class="col-3 dynamic_measure">
            <mdb-input class="m-0 p-0" disabled :value="(user_to_zaxira.real_qty - parseFloat(user_to_zaxira.check_qty) - parseFloat(user_to_zaxira.zaxira_qty)).toFixed(1)" size="sm"  outline  group type="text" validate error="wrong" success="right"/>
            <div class="abs_measure " v-show="measure_type">
              <mdb-input class="m-0 p-0"  v-model="measure" size="sm"  outline  group type="text" validate error="wrong" success="right"/>
            </div>
            <div class="abs_icon" @click="change_measure_update">
              <mdb-icon fas class="mr-1 text-primary"  icon="arrow-alt-circle-down"></mdb-icon>
            </div>
          </div>
          <div class="col-3">
            <mdb-input
              class="m-0 p-0"
              v-model="note"
              size="sm"
              outline
              group
              type="text"
              validate
              error="wrong"
              success="right"
            />
            <small
              style="position: absolute; top: -10px; left: 20px; font-size: 11px"
              class="back_ground_zaxira px-2 py-0"
              >{{ $t("note") }}</small
            >
            <!-- <mdb-btn @click="deleteInvoice(user_to_zaxira)" color="danger"  style="font-size: 8.5px; margin-top: -2px;"
                p="r3 l3 t2 b2"> <mdb-icon fas class="mr-1"  icon="trash"></mdb-icon>  {{$t('trash')}}
              </mdb-btn> -->
          </div>
        </div>

        
        <div class="photo w-100 mt-4 ml-5 d-flex justify-content-start " v-if="image_url_str">
          <img :src="hostname + image_url_str" width="240" alt="" class="shadow border">
        </div>
        <webcam  v-show="showPhoto" @getPhotosub="takePhoto"/>

        <hr class="mt-4 mb-3 gradint" />
        <div class="d-flex justify-content-end">
          <mdb-btn
            class="mr-2 text-white"
            style="font-size: 10px"
            color="success"
            darkWaves 
            tag="a"
            floating
            p="r4 l4 t2 b2"
            m="t0 b0"
            @click="getPhoto"
            size="sm"
            >{{ $t("photo") }}</mdb-btn
          >
          <mdb-btn
            v-if="image_url_str"
            class="mr-1 text-white"
            style="font-size: 10px"
            color="success"
            darkWaves
            tag="a"
            floating
            p="r4 l4 t2 b2"
            m="t0 b0"
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
                <tr class="header py-3" style="background: #ffbda9;">
                  <th  width="40" class="text-left">№</th>
                  <th>{{$t('client_name')}}</th>
                  <th>{{$t('product')}}</th>
                  <th>{{$t('measure')}}</th>
                  <th>{{$t('note')}}</th>
                  <th>{{$t('date')}}</th>
                  <th>{{$t('ostatka')}}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,rowIndex) in comenList" :key="rowIndex" @click="editRow(row)">
                  <td> <span >{{rowIndex+1}}</span> </td>
                  <td> <span >{{row.client.fio}}</span> </td>
                  <td> <span >{{row.product.name}}</span> </td>
                  <td> <span >{{row.qty_real}}</span> </td>
                  <td> <span >{{row.note}}</span> </td>
                  
                  <td> <span >{{row.updated_date_time.slice(0,10)}}</span> <span class="ml-2">{{row.updated_date_time.slice(11,16)}}</span> </td>
                  <td> <span class="text-success">{{row.credit_sum.toFixed(1)}}</span> </td>
                  
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
        <modal-train  :show="add_client_show" headerbackColor="white"  titlecolor="black" :title="$t('show_davernis')" 
          @close="add_client_show = false" width="90%">
            <template v-slot:body>
              <addClientModal @close="add_client_show = false"  @close_data="close_clientAdd" >
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
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>
    <Toast ref="message"></Toast>
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
import checkzaxira from '../sell/checkGetProduct'
import clientAdd from './clientAddForGetProduct.vue'
import erpSelect from "../../components/erpSelectFioSearch.vue";
import erpSelectAdd from "../../components/erpSelectAdd.vue";
import lineSelect from "../../components/lineSelect.vue";
import InputSearch from '../../components/inputSearch'
import addDavernis from './addDavernis.vue'
import infoDavernis from './infoDavernis.vue'
import addClientModal from '../client/addClientModal'
import webcam from '../webcam/webcam_Add.vue'
import infoInvoiceGet from './infoInvoiceGet.vue'
import infoClient from '../client/clientInfo.vue'
import { required } from "vuelidate/lib/validators";
import {mapActions,mapGetters, mapMutations} from 'vuex'
import InputSearchYear from '../../components/inputSearchYear';
export default {
  data() {
    return {
      modal_info: "",
      modal_status: false,
      loading: false,
      id: this.$route.params.id,
      measure_type: false,

      davernist_show : false,
      show_davernis_info: false,
      davernis_info: {},

      invoice_show_input:false,
      invoice_list: [],

      add_client_show: false,
      showPhoto: false,
      image_url_str: '',
      hostname: this.$store.state.server_ip,


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

      measure: null,
      comenList: [],
      OstatkaList: [],

      client_name: '',
      client_id: null,
      passport_number: '',
      phone_number: '',
      born_date: '',

      client_info_show: false,
      client_info: {},

      oshibka_client: 0,

      show_check: false,
      import_invoice_id: '',

      allList: [],
      check_number: 1,
    };
  },
  components: {
    mdbIcon,
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
    clientAdd,
    lineSelect, InputSearch,
    addDavernis,
    infoDavernis,
    addClientModal,
    webcam,
    infoInvoiceGet,
    infoClient,
    checkzaxira,
    InputSearchYear
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
      // await this.fetch_district_t();
      // await this.fetch_client_controler();
      await this.fetchClient();
      await this.fetch_product_t();
      await this.fetchGetZaxiraList();
      let today = new Date().toISOString()
      this.today_date = today.slice(0,10)
      // console.log(this.allClient)
    },
   computed: mapGetters(['all_district_t', 'all_client_controler', 'allClient', 'all_contragent_t', 'allCompany', 'all_product_t', 'user_to_zaxira']),
  methods: {
    ...mapActions(['fetch_district_t', 'fetch_client_controler', 'fetchClient', 'fetch_contragent_t', 'fetchCompany', 'fetch_product_t']),
    ...mapMutations(['clearZaxiraList', 'get_ostatka_check_for_get', 'get_invoice_for_invoice']),
    change_measure_update(){
      this.measure_type = !this.measure_type;
      this.measure = null;
    },
    close_clientAdd(data){
      this.add_client_show = false;
      console.log(data)
      this.user_name = data.fio;
      this.user_id = data.id;
      this.client_info = data;
      this.client_name = data.fio;
      this.client_id = data.id;
      this.passport_number = data.passport_number
      this.phone_number = data.note;
      this.born_date = data.addiotionala_information;
      this.oshibka_client = data.auth_user_updator_id;
      this.fetchOstatka(data.id);
    },
    infoClient(){
      this.client_info_show = true;
    },
    takePhoto(img){
      console.log('img')
      console.log(img)
      this.image_url_str = img;
      this.showPhoto = false;
      console.log('this.showPhoto')
      console.log(this.showPhoto)
    },
    getPhoto(){
      this.showPhoto = true;
    },
    async selectClientPassport(option){
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.passport_number = option.passport_number
      this.phone_number = option.note;
      this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);;
      this.oshibka_client = option.auth_user_updator_id;
      this.fetchOstatka(option.id);
      console.log(option)
    },
    async selectClientPhone(option){
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.passport_number = option.passport_number;
      this.phone_number = option.note;
      this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);;
      this.oshibka_client = option.auth_user_updator_id;
      this.fetchOstatka(option.id);

    },
    async selectClientBorn(option){
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.passport_number = option.passport_number
      this.phone_number = option.note;
      this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);;
      this.oshibka_client = option.auth_user_updator_id;
      this.fetchOstatka(option.id);

    },

    async selectOptionClient(option){
      this.client_info = option;
      console.log(option)
      this.client_name = option.fio;
      this.client_id = option.id;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number = option.passport_number
      this.phone_number = option.note;
      this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);;
      this.oshibka_client = option.auth_user_updator_id;
      this.fetchOstatka(option.id);
    },
    searchClientFromComponent(option){
      this.client_info = option;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number = option.passport_number
      this.phone_number = option.note;
      this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);;
      this.oshibka_client = option.auth_user_updator_id;
      this.fetchOstatka(option.id);
    },
    selectMainClient(option){
      this.user_name = option.client.fio;
      this.user_id = option.client.id;
    },
    async fetchOstatka(id){
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonClientOstatkas/getPaginationGetByClientClientIdList?page=0&size=100&client_id=" + id);
        const data = await response.json();
        console.log('weqeqw')
        console.log(data)
        // this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          this.OstatkaList = data.items_list;
          this.fetchOldList(id);
          console.log('data.items_list')
          console.log(data.items_list)
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
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/getPaginationByClientId?page=0&size=10000&client_id=" + id);
        const data = await response.json();
        this.loading = false;
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







    selectOptionUser(option) {
      console.log(option)
      this.user_name = option.fio;
      this.user_id = option.id;
    },
    selectOptionPro(option){
      console.log(option)
      this.product_name = option.name;
      this.product_id = option.id;
    },
    selectOption(option) {
      console.log(option)
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
      console.log(this.datasource.rows)
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
      
      console.log('review')
    },
    async editRow(data){
      let select_data_id = data.id;
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/getAnyInvoiceFullInfoById?invoice_id=" + select_data_id);
        const data = await response.json();
        console.log('weqeqw')
        console.log(data)
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
      // console.log(this.user_to_zaxira)
      if(this.measure == null || this.measure == '' || this.measure == 0){
        this.measure = this.user_to_zaxira.real_qty - parseFloat(this.user_to_zaxira.check_qty) - parseFloat(this.user_to_zaxira.zaxira_qty);
      }
      else{
        if(this.measure > this.user_to_zaxira.real_qty - parseFloat(this.user_to_zaxira.check_qty) - parseFloat(this.user_to_zaxira.zaxira_qty)){
          this.$refs.message.error('measure_more')
          return;
        }
      }

      this.product_name = this.user_to_zaxira.product_name;
      this.product_id = this.user_to_zaxira.product_id;
      var real_time_ostatka = parseFloat(this.measure);
      
      for(let i=0; i<this.OstatkaList.length; i++){
        if(this.OstatkaList[i].TegirmonProductid == this.user_to_zaxira.product_id){
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
      if(this.$v.$invalid)
      {
        this.$v.$touch();
        this.$refs.message.warning('please_fill')
        return false;
      }
      const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "note" : this.note,
            "tegirmonClientid" : this.user_id,
            "tegirmonAuthid": localStorage.AuthId,
            "summ" : this.summ,
            "tegirmonProductid" : this.product_id,
            "image_str_url" : this.image_url_str,
            "qty_real" : parseFloat(this.measure).toFixed(1),
            "user_name": this.client_name,
            "credit_sum": real_time_ostatka,
            "check_number": this.check_number,
            "id" : 0,
          })
        };
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/postInvoiceBugdoyniZaxiragaOlibQolishClientdan", requestOptions);
          const data = await response.json();
          console.log(data)
          this.loading = false;
            console.log('dasdas')
            console.log(response.status)
          if(response.status == 201 || response.status == 200)
          {
            await this.fetchGetZaxiraList();
            await this.fetchOstatka(this.user_id);
            await this.selectInvoiceItem(data);
            localStorage.ImportInvoiceId = data.id;
            await this.fetch_groupDetails_add_invoice(data.id)
            this.measure = 0;
            this.user_name = '';
            this.user_id = null;
            this.product_name = '';
            this.product_id = null;
            this.note = '';
            this.image_url_str = '';
            // this.$refs.message.success('Added_successfully')
            console.log('fire' )
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
    async fetch_groupDetails_add_invoice(invoice_id){
      let details_name_temp = this.user_to_zaxira.details_name + invoice_id.toString() + ','
      const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "tegirmonTortilganBugdoyRoyxatiGroupid": this.user_to_zaxira.group_id,
            "tegirmonTortilganBugdoyRoyxatiid": this.user_to_zaxira.ruyxat_id,
            "tegirmonInvoiceid": this.user_to_zaxira.invoice_id,
            "id" : this.user_to_zaxira.group_detail_id,
            "note": (parseFloat(this.measure) + parseFloat(this.user_to_zaxira.zaxira_qty)).toString(),
            "reverced_note_id": invoice_id,
            "name": details_name_temp
            // "name": this.shafyor_name,
            
            // "auth_user_creator_id": localStorage.AuthId,
            // "auth_user_updator_id": localStorage.AuthId,
          })
        };
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonTortilganBugdoyRoyxatiGroupDetails", requestOptions);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            console.log('hiy spring group details ga yozdi tekshirish kerak')
            this.$router.back();
          }
        }
        catch(error){
          console.log(error)
        }
    },
    async selectInvoiceItem(data){
      console.log(data)
      console.log(data.id)
      let select_invoice_id = data.id;
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/getAnyInvoiceFullInfoById?invoice_id=" + select_invoice_id);
        const data = await response.json();
        console.log('weqeqw')
        console.log(data)
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
      console.log(option);
    },

    async fetchGetZaxiraList(){
      let today = new Date().toISOString()
      this.today_date = today.slice(0,10)
      console.log(this.allClient)
      let a = this.today_date + 'T00:00:00.000Z';
      let b = this.today_date + 'T23:59:00.000Z';
      try{
          const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/getPaginationZahiragaOlibQolinganTovarlarListilarListi?page=0&size=120&b_date=" + a + '&e_date=' + b);
          const data = await response.json();
          console.log('data')
          console.log(data)
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
    // async writeGroupInvoice(){
    //   if(localStorage.ImportInvoiceId != null){
    //     localStorage.car_import_name += localStorage.ImportInvoiceId + ',';
    //   }
    //   else{
    //     return;
    //   }
    //   const requestOptions = {
    //     method : "POST",
    //     headers: { "Content-Type" : "application/json" },
    //     body: JSON.stringify({
    //       "shafyor_name": localStorage.car_shafyor_name,
    //       "car_number": localStorage.car_number,
    //       "note": localStorage.car_note,
    //       "name": localStorage.car_import_name,
    //       "id": localStorage.car_group_id
    //     })
    //   };
    //   try{
    //     this.loading = true;
    //     const response = await fetch(this.$store.state.hostname + "/TegirmonTortilganBugdoyRoyxatiGroups", requestOptions);
    //     this.loading = false;
    //     if(response.status == 201 || response.status == 200)
    //     {
    //       const tre = await response.json();
    //       console.log('tre')
    //       console.log(tre)
    //       this.$refs.message.success('Added_successfully')
    //       return true;
    //     }
    //     else{
    //       const data = await response.text();
    //       this.modal_info = data;
    //       this.modal_status = true;
    //       return false;
    //     }
    //   }
    //   catch{
    //     this.loading = false;
    //     this.modal_info = this.$i18n.t('network_ne_connect'); 
    //     this.modal_status = true;
    //   }
    // },

    // async deleteInvoice(item){
    //   await this.writeGroupInvoice();
    //   console.log(item);
    //   try{
    //     const requestOptions = {
    //       method : "delete",
    //     };
    //     const response = await fetch(this.$store.state.hostname + "/TegirmonTortilganBugdoyRoyxatiGroupDetails/" + item.group_detail_id, requestOptions);
    //     const data = await response.text();

    //     if(response.status == 201 || response.status == 200)
    //     {
    //       this.$refs.message.success('Successfully_removed')
    //     }
    //     else{
    //       this.modal_info = data;
    //       this.modal_status = true;
    //     }
    //   }
    //   catch{
    //     this.modal_info = data;
    //     this.modal_status = true;
    //   }
    //   try{
    //     const requestOptions = {
    //       method : "delete",
    //     };
    //     const response = await fetch(this.$store.state.hostname + "/TegirmonTortilganBugdoyRoyxatis/" + item.ruyxat_id, requestOptions);
    //     const data = await response.text();

    //     if(response.status == 201 || response.status == 200)
    //     {
    //       this.$refs.message.success('Successfully_removed')
    //     }
    //     else{
    //       this.modal_info = data;
    //       this.modal_status = true;
    //     }
    //   }
    //   catch{
    //     this.modal_info = data;
    //     this.modal_status = true;
    //   }
    //   try{
    //     const requestOptions = {
    //       method : "delete",
    //     };
    //     const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/" + item.invoice_id, requestOptions);
    //     const data = await response.text();

    //     if(response.status == 201 || response.status == 200)
    //     {
    //       this.$refs.message.success('Successfully_removed')
    //     }
    //     else{
    //       this.modal_info = data;
    //       this.modal_status = true;
    //     }
    //   }
    //   catch{
    //     this.modal_info = data;
    //     this.modal_status = true;
    //   }


    //   this.clearZaxiraList();
    //   this.$router.back();  
    // }

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
  padding: 7px 10px;
}
.myTableDebitorList th{
  text-align: left;
  padding: 6px 10px;
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
  height: 250px;
  overflow: hidden;
  overflow-y: scroll;
}
.getProduct{
  min-height: calc(100vh - 670px);
}
.back_ground_zaxira{
  padding-top: 7px;
  background: #dbffe9;
}
.bg_red_color{
  background: #ff937d !important;
}
.alert_danger{
  background: #ffb6b6 !important;
}
.alert_success{
  background: #d4fde4;
}
.dynamic_measure{
  position: relative;
}
.abs_measure{
  position: absolute;
  top: 30px;
  background: white;
  left: 15px;
  width: 92%;
}
.abs_icon{
  position: absolute;
  top: 0px;
  right: -8px;
  background: #d4fde4;
  cursor:pointer;
}
</style>
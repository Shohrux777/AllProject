<template>
  <div>
    <loader v-if="loading"/>
    <div class="border-bottom alert-success">
      <router-link to="/buy">
         <h5 class="m-0 ml-3 d-flex" style="padding: 16px 0px">
           <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-chevron-left mr-2" width="24" height="24" viewBox="0 0 24 24" stroke-width="2.5" stroke="#007BFF" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
              <polyline points="15 6 9 12 15 18" />
            </svg>
            {{$t('new_buy')}}</h5>
      </router-link>
    </div>

    <div class="changing_add px-3 pt-4 pb-5">
      <div class="row mb-3">
        <div class="col-3">
          <input class="m-0 form-control" style="height:32px; font-size: 13px;" v-model="user_name" size="md"  outline  group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
            class="bg-white px-2 py-0"
            >{{ $t("user_name") }}</small
          >
        </div>
        <div class="col-3">
          <erpSelectSklad
              :options="allSklad.rows"
              @select="selectOptionSklad"
              :selected="sklad_name"
              :label="$t('sklad')"
              size="sm"
            />
            <small
              class="invalid-text"
              style="margin-left: 10px; margin-top: -5px; font-size: 12px;"
              v-if="$v.sklad_name.$dirty && !$v.sklad_name.required"
            >
              {{ $t("Select_sklad") }}
            </small>
        </div>
        <div class="col-3">
          <erpSelect
            :options="client_list"
            @select="selectOptionUser"
            :selected="contragent_name"
            KeyName = "name"
            size="sm"
            class="bg-white"
            url="/TegirmonContragent/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
            :label="$t('select_item')"
            />
            <small style="position:absolute; top:-9px; left:20px; font-size: 11px; color: rgb(62, 62, 139);" class="bg-white px-2 py-0">Выбирать контрагент</small>
        </div>
        <div class="col-3">
            <input class="m-0 form-control"  style="height:32px; font-size: 13px;" v-model="note" size="md"  outline  group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -11px; left: 20px; font-size: 11px"
            class="bg-white px-2 py-0"
            >{{ $t("note") }}</small
          >
        </div>
      </div>
      <div class="row mb-3">
        <div class="col-3">
          <input class="m-0 form-control" disabled style="height:32px; font-size: 13px;" :value="all_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')" size="md"  outline  group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
            class="bg-white px-2 py-0"
            >{{ $t("summ") }} сўм</small
          >
        </div>
        <div class="col-2">
          <input class="m-0 form-control" disabled style="height:32px; font-size: 13px;" :value="all_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')" size="md"  outline  group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
            class="bg-white px-2 py-0"
            >{{ $t("summ") }} {{ $t("dollor") }}</small
          >
        </div>
        <div class="col-2">
          <input class="m-0 form-control"  style="height:32px; font-size: 13px;" v-model="kurs_str" @input="changeKurs($event.target.value)"
          size="md"  outline  group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
            class="bg-white px-2 py-0"
            >Курс</small
          >
        </div>

        <div class="col-3">
          <input class="m-0 form-control" style="height:32px; font-size: 13px;" v-model="pay_sum_str" @input="changePaySum($event.target.value)"
           size="md"  outline  group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
            class="bg-white px-2 py-0"
            >Берилган сўм</small
          >
        </div>
        <div class="col-2">
          <input class="m-0 form-control" style="height:32px; font-size: 13px;" @input="changePayDollor($event.target.value)"
          v-model="pay_dollor_str" size="md"  outline  group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
            class="bg-white px-2 py-0"
            >Берилган {{ $t("dollor") }}</small
          >
        </div>
      </div>

      <form @submit.prevent="submit">
        <div class="d-flex w-100">
          <div class=" " >
            <mdb-btn
              class="mr-1"
              style="font-size: 12px"
              outline="orange"
              darkWaves
              tag="a"
              floating
              @click="addRow"
              icon="plus"
              size="sm"
              >{{ $t("Add_product") }}</mdb-btn
            >

            <!-- <mdb-btn-group style="margin-top: -14px">
              <mdb-dropdown>
                <mdb-btn
                  style="font-size: 12px"
                  outline="primary"
                  darkWaves
                  tag="a"
                  floating
                  icon="bars"
                  size="sm"
                  slot="toggle"
                ></mdb-btn>
                <mdb-dropdown-menu>
                  <mdb-dropdown-item
                    style="font-size: 12px"
                    >{{ $t("") }}</mdb-dropdown-item
                  >
                  <mdb-dropdown-item style="font-size:12px"  >{{'Export_to_PDF'|locolize}}</mdb-dropdown-item>
                </mdb-dropdown-menu>
              </mdb-dropdown>
            </mdb-btn-group> -->
          </div>
          
        </div>
        
        <div class="d_table">
          <mdb-tbl class="fixed-column">
            <mdb-tbl-head class="m-0 p-0">
              <tr>
                <td></td>
                <!-- product -->
                <td style="width:500px; font-size: 13px">
                  {{ $t(datasource.columns[0]) }}
                </td>
                <td
                  style=" font-size: 13px"
                >
                  {{ $t(datasource.columns[1]) }}
                </td>
                <td
                  style=" font-size: 13px"
                >
                  {{ $t(datasource.columns[2]) }}
                </td>
                <td
                  style=" font-size: 13px"
                >
                  {{ $t(datasource.columns[3]) }}
                </td>
                <td
                  style=" font-size: 13px"
                >
                  {{ $t(datasource.columns[4]) }}
                </td>
                <td
                  style=" font-size: 13px"
                >
                  {{ $t(datasource.columns[5]) }}
                </td>
                <td
                  style=" font-size: 13px"
                >
                  {{ $t(datasource.columns[6]) }}
                </td>
               
                
                <!-- ed_izm -->
              </tr>
            </mdb-tbl-head>
            <mdb-tbl-body>
              <tr
                scope="row"
                v-for="(row, rowIndex) in datasource.rows"
                :key="rowIndex"
      
              >
                <th>
                  <div class="d-flex">
                    <i
                      class="
                        fas
                        fa-trash
                        delIcon
                        mask
                        waves-effect
                        m-0
                        pl-2

                      "
                      @click="datasource.rows.splice(parseInt(rowIndex), 1)"
                    ></i>
                  </div>
                </th>
                <th>
                  <lineSelect
                    :options="all_product_t.rows"
                    :row_index="rowIndex"
                    :searchshow="true"
                    @select="selectproduct"
                    :selected="row.product_name"
                  
                    :label="$t('product')"
                  />
                  <small
                    class="invalid-text"
                    style="
                      margin: 0;
                      margin-top: 26px;
                      position: relative;
                    "
                    v-if="
                      $v.datasource.rows.$each[rowIndex].product_name
                        .$dirty &&
                      !$v.datasource.rows.$each[rowIndex].product_name
                        .required
                    "
                  >
                    {{ $t("Select_product") }}
                  </small>
                </th>

                <th>
                  <input
                    v-model="row.qty_str"
                    type="text"
                    class="form-control form-control-sm"
                    @input="changeSumma($event.target.value,rowIndex)"
                  />
                  <small
                    class="invalid-text"
                    style="
                      margin: 0;
                      margin-top: 26px;
                      position: relative;
                    "
                    v-if="($v.datasource.rows.$each[rowIndex].qty.$dirty &&
                        !$v.datasource.rows.$each[rowIndex].qty.required)
                    "
                  >
                    {{ $t("minValue") }}
                  </small>
                </th>
                <th>
                  <input
                    @input="changeBuySum($event.target.value,rowIndex)"
                    v-model="row.buy_sum_str"
                    type="text"
                    class="form-control form-control-sm"
                  />
                </th>
                <th>
                  <input
                    :value="row.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')"
                    disabled
                    type="text"
                    class="form-control form-control-sm"
                  />
                </th>
                <th>
                  <input
                    @input="changeBuyDollor($event.target.value,rowIndex)"
                    v-model="row.buy_dollor_str"
                    type="text"
                    class="form-control form-control-sm"
                  />
                </th>
                <th>
                  <input
                    :value="row.all_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')"
                    disabled
                    type="text"
                    class="form-control form-control-sm"
                  />
                </th>
                <th>
                  <input
                    @input="changeSellPrice($event.target.value,rowIndex)"

                    v-model="row.product_priceString"
                    type="text"
                    class="form-control form-control-sm"
                  />
                </th>
              </tr>
            </mdb-tbl-body>
          </mdb-tbl>
          
        </div>
         
         
        
        <div class="blue-gradient">
          <hr class="mt-1 mb-0"/>
        </div>
        <mdb-row class="mt-4">
          <mdb-col col="12">
            <div class="mt-0 text-right">
              <mdb-btn color="success"  type="submit" style="font-size: 10.5px"
                p="r4 l4 t2 b2">
                <mdb-icon  />{{$t('add')}}
              </mdb-btn>
            </div>
          </mdb-col>
        </mdb-row>
        
      </form>
      <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>

      <Toast ref="message"></Toast>
    </div>
  </div>
</template>

<script>
import { mdbInput, mdbRow, mdbCol, mdbIcon, mdbBtn,  mdbTbl,
  mdbTblHead,
  mdbTblBody,
   mdbBtnGroup,
  mdbDropdown,
  mdbDropdownItem,
  mdbDropdownMenu,} from "mdbvue"
import { required } from 'vuelidate/lib/validators'
import lineSelect from "../../components/lineSelect.vue";
import {mapActions,mapGetters} from 'vuex'
// import { all } from "core-js/fn/promise";
import erpSelectSklad from "../../components/erpSelect.vue";
import erpSelect from "../../components/erpSelectFioSearch.vue";

export default {
  naem: "changingAdd",
  
  data(){
    return{
      modal_info: '',
      modal_status: false,
      loading: false,
      id: this.$route.params.id,
      name: '',
      phone_number: '',

      product_name: '',
      product_id: null,
      subproduct_name: '',
      subproduct_id: null,

      user_name: localStorage.user_name,

      all_sum: 0,
      all_dollor: 0,
      kurs_str:'',
      kurs:0,
      pay_sum: 0,
      pay_sum_str: '',
      pay_dollor: 0,
      pay_dollor_str: '',
      
      note: '',


      producKg: 1,
      subproducKg: 0,
      percanteg: 0,
      datasource: {
        rows: [],
        // real_time
        columns: [
          "product",
          "kg_ves",
          "buy_sum",
          "all_buy_sum",
          "buy_dollor",
          "all_buy_dollor",
          "price",
        ],
      },
      sklad_name: '',
      sklad_id: null,

      client_list: [],
      contragent_name: '',
      contragent_id: null
    }
  },
  components: {
    mdbInput, mdbRow, mdbCol, mdbIcon, mdbBtn, mdbTbl, mdbBtnGroup,
    mdbDropdown, 
    mdbDropdownItem,
    mdbDropdownMenu,
    mdbTblHead,
    mdbTblBody,
    lineSelect,
    erpSelectSklad,
    erpSelect
  },
  validations: {
    datasource: {
      rows: {
        $each: {
          qty: {  required },
          product_name: { required },
        },
      },
    },
    sklad_name: {
      required
    }
  },
  async created()
  {
    if(this.id > 0)
    {
      const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getAnyInvoiceFullInfoById?invoice_id=' + this.id);
      const data = await res.json();
      console.log('data');
      console.log(data);
      this.user_name = data.user_name;
      this.all_sum = data.summ;
      this.note = data.note;
      for(let i=0; i<data.item_list.length; i++){
        this.datasource.rows.push({
        tegirmonProductid: 0,
        product_name: "",
        qty: 0,
        qty_str: '',
        
        buy_dollor: 0,
        buy_dollor_str: '',
        all_sum:0, // dollor sotib olingan tovar narxi summasi

        buy_sum:0,
        buy_sum_str: '',
        sum: 0, // bu sumda sotib olingan tovar summasi va narxi

        product_price: 0,
        product_priceString: '',
        id: 0,
        auth_user_updator_id: 1,
        });

        this.datasource.rows[i].tegirmonProductid = data.item_list[i].tegirmonProductid;
        this.datasource.rows[i].product_name = data.item_list[i].product.name;
        this.datasource.rows[i].price = data.item_list[i].product.price;
        this.datasource.rows[i].qty = data.item_list[i].qty;
        this.datasource.rows[i].qty_str = data.item_list[i].qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

        this.datasource.rows[i].buy_dollor = data.item_list[i].buy_dollor;
        this.datasource.rows[i].buy_dollor_str = data.item_list[i].buy_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.datasource.rows[i].all_sum = data.item_list[i].buy_dollor * data.item_list[i].qty;

        this.datasource.rows[i].buy_sum = data.item_list[i].buy_sum;
        this.datasource.rows[i].buy_sum_str = data.item_list[i].buy_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.datasource.rows[i].sum = data.item_list[i].buy_sum * data.item_list[i].qty;

        this.datasource.rows[i].product_price = data.item_list[i].product_price;
        this.datasource.rows[i].product_priceString = data.item_list[i].product_priceString;


        this.datasource.rows[i].id = data.item_list[i].id;  

      }
      console.log(this.datasource.rows)
    }
  },
  async mounted() {
    await this.fetch_product_t();
    await this.fetchSklad();
    await this.fetchClient();
    if(localStorage.dollor_kurs){
      this.kurs = localStorage.dollor_kurs;
      this.kurs_str = this.kurs.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
    }
    else{
      await this.nbuKurs();
    }
  },
  computed: mapGetters([ 'allSklad', 'all_product_t',]),
  methods:{
    ...mapActions(['fetchSklad', 'fetch_product_t']),
    async fetchClient(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonContragent/getPagination?page=0&size=100");
        const data = await response.json();
        if(response.status == 201 || response.status == 200)
        {
          console.log(data)
          this.client_list = data.items_list;
          return true;
        }
        else
        {
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
      this.contragent_name = option.name;
      this.contragent_id = option.id;
    },
    cls_wnd()
    {
      
    },
    async selectOptionSklad(option){
      this.sklad_name = option.name;
      this.sklad_id = option.id;
    },
    selectOptionProduct(option){
      this.product_name = option.name;
      this.product_id = option.id;0
    },
    selectproduct(option) {
      console.log(option);
      this.datasource.rows[option.row].product_name = option.data.name;
      this.datasource.rows[option.row].tegirmonProductid = option.data.id;
      this.datasource.rows[option.row].product_price = option.data.price;
      this.datasource.rows[option.row].product_priceString = option.data.price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      // this.datasource.rows[option.row].price = option.data.price;
    },
     addRow(){
      this.datasource.rows.unshift({
        tegirmonProductid: 0,
        product_name: "",
        qty: 0,
        qty_str: '',
        
        buy_dollor: 0,
        buy_dollor_str: '',
        all_sum:0, // dollor sotib olingan tovar narxi summasi

        buy_sum:0,
        buy_sum_str: '',
        sum: 0, // bu sumda sotib olingan tovar summasi va narxi

        product_price: 0,
        product_priceString: '',
        id: 0,
        auth_user_updator_id: 1,
      });
      console.log(this.datasource)
    },
    changeSumma(n,i){
      var tols = '';
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' 
        && tols[tols.length-1] != '4' && tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' 
        && tols[tols.length-1] != '8' && tols[tols.length-1] != '9' && tols[tols.length-1] != '.'){
        tols = tols.slice(0,tols.length-1)
       }
       this.datasource.rows[i].qty_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

       var temp = ''
       for(let j=0; j<this.datasource.rows[i].qty_str.length; j++){
        if(this.datasource.rows[i].qty_str[j] != ' '){
          temp += this.datasource.rows[i].qty_str[j];
        }
       }
      if(temp == ''){
        this.datasource.rows[i].qty = 0
      }
      else{
        this.datasource.rows[i].qty = parseFloat(temp);
      }
     
      this.datasource.rows[i].sum = parseFloat(this.datasource.rows[i].buy_sum) * parseFloat(this.datasource.rows[i].qty) 
      this.datasource.rows[i].all_sum = parseFloat(this.datasource.rows[i].buy_dollor) * parseFloat(this.datasource.rows[i].qty) 
      this.all_sum = 0;
      this.all_dollor = 0;
      for(let j=0; j<this.datasource.rows.length; j++){
        this.all_sum +=  this.datasource.rows[j].sum;
        this.all_dollor +=  this.datasource.rows[j].all_sum;
      }

    },
    changeBuySum(n,i){
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
        tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9' && tols[tols.length-1] != '.'){
        tols = tols.slice(0,tols.length-1)
       }
       this.datasource.rows[i].buy_sum_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

       var temp = ''
       for(let j=0; j<this.datasource.rows[i].buy_sum_str.length; j++){
        if(this.datasource.rows[i].buy_sum_str[j] != ' '){
          temp += this.datasource.rows[i].buy_sum_str[j];
        }
       }
       if(temp == ''){
          this.datasource.rows[i].buy_sum = 0
        }
        else{
          this.datasource.rows[i].buy_sum = parseFloat(temp);
        }
      
     
      this.datasource.rows[i].sum = parseFloat(this.datasource.rows[i].buy_sum) * parseFloat(this.datasource.rows[i].qty);
      this.all_sum = 0;
      for(let j=0; j<this.datasource.rows.length; j++){
        this.all_sum +=  this.datasource.rows[j].sum;
      }
      this.datasource.rows[i].all_sum = 0;
      this.datasource.rows[i].buy_dollor = 0;
      this.datasource.rows[i].buy_dollor_str = '';
    },

    changeBuyDollor(n,i){
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
        tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9' && tols[tols.length-1] != '.'){
        tols = tols.slice(0,tols.length-1)
       }
       this.datasource.rows[i].buy_dollor_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

       var temp = ''
       for(let j=0; j<this.datasource.rows[i].buy_dollor_str.length; j++){
        if(this.datasource.rows[i].buy_dollor_str[j] != ' '){
          temp += this.datasource.rows[i].buy_dollor_str[j];
        }
       }
       if(temp == ''){
          this.datasource.rows[i].buy_dollor = 0
        }
        else{
          this.datasource.rows[i].buy_dollor = parseFloat(temp);
        }
      
     
      this.datasource.rows[i].all_sum = parseFloat(this.datasource.rows[i].buy_dollor) * parseFloat(this.datasource.rows[i].qty) 
      this.all_dollor = 0;
      for(let j=0; j<this.datasource.rows.length; j++){
        this.all_dollor +=  this.datasource.rows[j].all_sum;
      }
      this.datasource.rows[i].sum = 0;
      this.datasource.rows[i].buy_sum = 0;
      this.datasource.rows[i].buy_sum_str = '';

    },

    changePayDollor(n){
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
        tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9' && tols[tols.length-1] != '.'){
        tols = tols.slice(0,tols.length-1)
       }
       this.pay_dollor_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

       var temp = ''
       for(let j=0; j<this.pay_dollor_str.length; j++){
        if(this.pay_dollor_str[j] != ' '){
          temp += this.pay_dollor_str[j];
        }
       }
       if(temp == ''){
          this.pay_dollor = 0
        }
        else{
          this.pay_dollor = parseFloat(temp);
        }

    },
    changeKurs(n){
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
        tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9' && tols[tols.length-1] != '.'){
        tols = tols.slice(0,tols.length-1)
       }
       this.kurs_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

       var temp = ''
       for(let j=0; j<this.kurs_str.length; j++){
        if(this.kurs_str[j] != ' '){
          temp += this.kurs_str[j];
        }
       }
       if(temp == ''){
          this.kurs = 0
        }
        else{
          this.kurs = parseFloat(temp);
          localStorage.dollor_kurs = this.kurs;
        }

    },
    changePaySum(n){
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
        tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9' && tols[tols.length-1] != '.'){
        tols = tols.slice(0,tols.length-1)
       }
       this.pay_sum_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

       var temp = ''
       for(let j=0; j<this.pay_sum_str.length; j++){
        if(this.pay_sum_str[j] != ' '){
          temp += this.pay_sum_str[j];
        }
       }
       if(temp == ''){
          this.pay_sum = 0
        }
        else{
          this.pay_sum = parseFloat(temp);
        }

    },

    changeSellPrice(n,i){
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
        tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9' && tols[tols.length-1] != '.'){
        tols = tols.slice(0,tols.length-1)
       }
       this.datasource.rows[i].product_priceString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

       var temp = ''
       for(let j=0; j<this.datasource.rows[i].product_priceString.length; j++){
        if(this.datasource.rows[i].product_priceString[j] != ' '){
          temp += this.datasource.rows[i].product_priceString[j];
        }
       }
       if(temp == ''){
          this.datasource.rows[i].buy_dollor = 0
        }
        else{
          this.datasource.rows[i].product_price = parseFloat(temp);
        }
    },

    async nbuKurs(){
      try{
        const response = await fetch("https://cbu.uz/uz/arkhiv-kursov-valyut/json/");
        const data = await response.json();
        console.log('json valyuta')
        console.log(data)
        if(data.length>0){
          this.kurs_str = parseInt(data[0].Rate).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
          this.kurs = parseInt(data[0].Rate);
          localStorage.dollor_kurs = parseInt(data[0].Rate);
        }
      }
      catch(error){
        console.log(error)
      }
    },




    save_data :  async function(){
        // this.all_sum = this.datasource.rows.reduce((total, item)=>total + item.sum, 0)
        
         if(this.$v.$invalid )
        {
          this.$v.$touch();
          this.$refs.message.warning('please_fill')
          return false;
        }
        const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
              // "tegirmonProductid" : this.product_id,
              "summ": this.all_sum,
              "pol_all_sum": this.all_dollor,
              "note": this.note,
              "user_name": this.user_name,
              "item_list": this.datasource.rows,
              "tegirmonSkladid": this.sklad_id,
              "prixod_status": 1,   // prixod bulsa 1 // rasxod bulsa 2
              "id" : this.id,
              "tegirmonAuthid": localStorage.AuthId,
              "pol_kurs_sum": this.kurs,
              "pol_kurs_sum_str": this.kurs_str,
              "pol_dollor_summa": this.pay_dollor,
              "poluchit_summa": this.pay_sum,
              "tegirmonContragentid": this.contragent_id
            })
          };
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/addTovarlardiPrixodQilish", requestOptions);
            const data = await response.json();
            console.log('data')
            console.log(data)
            this.loading = false;
            if(response.status == 201 || response.status == 200)
            {
              this.$refs.message.success('Added_successfully')
              return true;
            }
            else{
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
    async submit()
      {
        if(await this.save_data())
        {
          this.$store.state.alert = true;
          this.$router.push("/buy");
        }

      },
  }
}
</script>

<style lang="scss" scoped >
td{
  // margin: 0px !important;
  padding: 5px 15px !important;
}
.delIcon:hover {
  color: #000;
}
.delIcon {
  padding-top: 8px;
  color: red;
}
</style>

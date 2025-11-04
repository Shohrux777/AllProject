<template>
  <div>
    <loader v-if="loading"/>
    <div class="px-3">
      <h6 class="m-0 text-primary" style="font-style: italic; font-size: 15px;">Добавить заказ <span v-if="id>0">№ {{ id }}</span></h6>
    </div>
    <div class="changing_add px-3 pt-2 pb-2 ">
      <div class="row  alert-info pt-3 pb-2 mb-2 rounded">
        <div class="col-3">
          <input class="m-0 form-control" disabled
            style="height:32px; font-size: 13px;" :value="client.fio"
            type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
            class="bg-white px-2 py-0"
            >{{ $t("client_name") }}</small
          >
        </div>
        <div class="col-3">
          <input class="m-0 form-control"
            style="height:32px; font-size: 13px;" v-model="shafyor_name"
            type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
            class="bg-white px-2 py-0"
            >{{ $t("name") }}</small
          >
        </div>
        <!-- <div class="col-3">
          <input class="m-0 form-control" v-mask="'##A###AA'" placeholder="01A123AA"
            style="height:32px; font-size: 13px;" v-model="car_nomer" 
            type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
            class="bg-white px-2 py-0"
            >{{ $t("car_number") }}</small
          >
        </div> -->
        <div class="col-3">
            <input class="m-0 form-control"  style="height:32px; font-size: 13px;" v-model="note" size="md"  outline  group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -11px; left: 20px; font-size: 11px"
            class="bg-white px-2 py-0"
            >{{ $t("note") }}</small
          >
        </div>
        <div class="col-3">
          <input class="m-0 form-control"
            style="height:32px; font-size: 13px;" v-model="choosen_day" 
            type="date" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
            class="bg-white px-2 py-0"
            >{{ $t("date") }}</small
          >
        </div>
        
        <div class="col-3 mt-3">
          <input class="m-0 form-control" disabled style="height:32px; font-size: 13px;" :value="all_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')" size="md"  outline  group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
            class="bg-white px-2 py-0"
            >{{ $t("summ") }}</small
          >
        </div>
        <div class="col-3 mt-3">
          <input class="m-0 form-control" disabled style="height:32px; font-size: 13px;" :value="all_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')" size="md"  outline  group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
            class="bg-white px-2 py-0"
            >{{ $t("dollor") }}</small
          >
        </div>
      </div>

      <form @submit.prevent="submit">
        <div class="d-flex w-100">
          <div class=" ">
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
          </div>
          
        </div>
        
        <div class="d_table">
          <mdb-tbl class="fixed-column" style="min-height: 260px;">
            <mdb-tbl-head class="m-0 p-0">
              <tr>
                <td></td>
                <!-- product -->
                <td style="width:450px; font-size: 13px">
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
               
                
                <!-- ed_izm -->
              </tr>
            </mdb-tbl-head>
            <mdb-tbl-body>
              <tr
                scope="row"
                v-for="(row, rowIndex) in datasource.rows"
                :key="rowIndex"
                :class="{'alert-success': row.zaxira}"
      
              >
                <th>
                  <i v-if="id == 0 || (id>0 && row.zaxira == false)"
                    class="
                      fas
                      fa-trash
                      delIcon
                      mask
                      waves-effect
                      m-0
                      pl-2
                    "
                    @click="deleteRow(rowIndex)"
                  ></i>
                </th>
                <th :class="{'applied': row.zaxira}">
                  <lineSelect
                    :options="all_product_skladId.rows"
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

                <th :class="{'applied': row.zaxira}">
                  <lineSelect
                    :options="row.price_list"
                    :row_index="rowIndex"
                    :searchshow="true"
                    @select="selectprice"
                    :selected="row.sum_str"
                  
                    :label="$t('price')"
                  />
                </th>

                <th style="position: relative;"> 
                  <input
                    v-model="row.qty_str"
                    type="text"
                    class="form-control form-control-sm"
                    @input="changeSumma($event.target.value,rowIndex)"
                    @click="button_qty(rowIndex)"
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
                  <small 
                  class="invalid-text"
                    style="
                      margin: 0;
                      margin-top: 26px;
                      position: relative;
                    "
                    v-else-if="$v.datasource.rows.$each[rowIndex].qty.$dirty &&
                    !$v.datasource.rows.$each[rowIndex].qty.minValue">
                    Миқдор 1 дан кам бўлмаслиги керак!
                  </small>
                  <small 
                  class=" text-right"
                    style="
                      margin: 0;
                      bottom: 16px;
                      right: 15px;
                      font-size: 13px;
                      color:red;
                      position: absolute;
                      cursor:pointer;
                    "
                    @click="clientZaxiraAllQty(rowIndex)"
                    v-if="row.zaxira">
                    ⬆️ {{row.all_qty}}
                  </small>
                </th>
                <th style="position: relative;" :class="{'applied': row.zaxira}">
                  <input
                    v-model="row.price_str"
                    @input="changePrice($event.target.value,rowIndex)"
                    @click="button_price(rowIndex)"
                    type="text"
                    class="form-control form-control-sm"
                  />
                  <small v-if="row.price_type == 2" style="position: absolute; right: 15px; top:18px; font-size: 13px;"> USD </small>
                  <small v-else style="position: absolute; right: 15px; top:18px; font-size: 13px;"> UZS </small>
                </th>
                <th style="position: relative;">
                  <input
                    :value="row.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')"
                    disabled
                    type="text"
                    class="form-control form-control-sm"
                  />
                  <small v-if="row.price_type == 2" style="position: absolute; right: 15px; top:18px; font-size: 13px;"> USD </small>
                  <small v-else style="position: absolute; right: 15px; top:18px; font-size: 13px;"> UZS </small>
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
import { required, minValue  } from 'vuelidate/lib/validators'
import lineSelect from "../../components/lineSelect.vue";
import {mapActions,mapGetters} from 'vuex'
// import { all } from "core-js/fn/promise";


export default {
  name: "changingAdd",
  
  data(){
    return{
      modal_info: '',
      modal_status: false,
      loading: false,
      name: '',
      phone_number: '',

      shafyor_name: '',
      car_nomer: '',

      user_name: localStorage.user_name,
      all_sum: 0,
      all_dollor: 0,
      note: '',

      producKg: 1,
      subproducKg: 0,
      percanteg: 0,
      datasource: {
        rows: [],
        // real_time
        columns: [
          "product",
          "price_list",
          "kg_ves",
          "price",
          "summ"
        ],
      },
      sklad_name: '',
      sklad_id: 3,
      client: {
        fio: '',
        id:null,
      },
      client_zaxira_list: [],
      order_data:{},
      load_name: '',
      loadedAt: new Date(),
      kassir_name: '',
      paidAt: new Date(),
    }
  },
  props: {
    client_info:
    {
      type: Object,
      default(){
        return {
          fio: '',
          id:null,
        }
      }
    },
    product:
    {
      type: Object,
      default(){
        return {}
      }
    },
    choosen_day: {
      type: String,
      default: ''
    },
    id:{
      type:Number,
      default:0
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
  },
  validations: {
    datasource: {
      rows: {
        $each: {
          qty: {  required, minValue: minValue(1) },
          product_name: { required },
        },
      },
    },
  },
  watch: {
    client_info: {
      handler(newVal) {
        // ✅ client_info o'zgargan payt zaxira bo'lgan product borligini tekshiramiz
        if (this.datasource.rows.length){
          if (this.datasource.rows.some(r => r.zaxira === true)) {
            if(this.client.id == newVal.id){
              console.log("salom bir biriga teng");
            }
            else{
              this.$emit('close');
            }
          }
          else{
            if((this.order_data.isPaid || this.order_data.pay_progress > 0) && newVal.id != this.client.id){
              this.$emit('close');
            }
          }
        }

        if (newVal) {
          // ma'lumot bor
          this.client = newVal;
        } else {
          // null yoki undefined bo‘lsa bo‘sh obyekt
          this.client = { fio: '', id:null };
        }
      },
      deep: true,
    },
    product: {
      handler(newVal) {
        // yangi product keldi — shu yerda reaksiya qilasiz
        console.log("Yangi product:", newVal);
        this.add_zaxira_product(newVal)
      },
      deep: true,
      immediate: true
    }
  },
  async created()
  {
    this.client = this.client_info;
    if(this.id > 0)
    {
      const res = await fetch(this.$store.state.hostname + '/TegirmonOrder/' + this.id);
      const data = await res.json();
      console.log('data invoice_malumot');
      console.log(data);
      this.order_data = data;
      this.user_name = data.user_name;
      this.all_sum = data.sum;
      this.all_dollor = data.dollor;
      this.shafyor_name = data.shafyor_name;
      this.car_nomer = data.car_nomer;
      this.note = data.note;
      this.load_name =  data.load_name;
      this.loadedAt =  data.loadedAt;
      this.kassir_name =  data.kassir_name;
      this.paidAt =  data.paidAt;
      if (data.client) {
        this.client = data.client;
        await this.fetchClientZaxiraList(data.tegirmonOrderClientid);
        this.$emit('updateClient', this.client)
      } else {
        // null yoki undefined bo‘lsa bo‘sh obyekt
        this.client = { fio: '', id:null };
      }

      for(let i=0; i<data.item_list.length; i++){
        this.datasource.rows.push({
          tegirmonProductid: 0,
          product_name: "",
          qty: 0,
          qty_str: '0',
          price: 0,
          price_str: '0',
          price_type: 0,
          price_qty: 0,
          price_list: [],
          sum_str: '', // bu optim price nomi
          sum:0,
          id: 0,
          auth_user_updator_id: localStorage.AuthId,
          new_qty: 0,
          old_qty: 0,
          all_qty: 0,
          zaxira: false,
        });
        this.datasource.rows[i].tegirmonProductid = data.item_list[i].tegirmonProductid;
        this.datasource.rows[i].product_name = data.item_list[i].product.name;
        this.datasource.rows[i].qty = data.item_list[i].qty;
        this.datasource.rows[i].qty_str = data.item_list[i].qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.datasource.rows[i].sum = data.item_list[i].sum;

        this.datasource.rows[i].price = data.item_list[i].price;
        this.datasource.rows[i].price_str = data.item_list[i].price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.datasource.rows[i].price_type = data.item_list[i].price_type;
        this.datasource.rows[i].price_qty = data.item_list[i].price_qty;
        this.datasource.rows[i].sum_str = data.item_list[i].sum_str;
        this.datasource.rows[i].zaxira = data.item_list[i].zaxira;
        if(this.datasource.rows[i].zaxira){
          this.datasource.rows[i].old_qty = data.item_list[i].qty;
          for(let j=0; j<this.client_zaxira_list.length; j++){
            if(this.client_zaxira_list[j].tegirmonProductid == data.item_list[i].tegirmonProductid){
              this.datasource.rows[i].all_qty = data.item_list[i].qty + this.client_zaxira_list[j].qty;

            }
          }
        }
      }
      console.log(this.datasource.rows)
    }
  },
  async mounted() {
    await this.fetch_product_skladId(2);
    await this.fetchSklad();
  },
  computed: mapGetters([ 'allSklad', 'all_product_skladId',]),
  methods:{
    ...mapActions(['fetchSklad', 'fetch_product_skladId']),
    cls_wnd()
    {
      
    },
    async fetchClientZaxiraList(client_id){
      try {
        // 🔹 Backend API manzili
        const response = await fetch(
          this.$store.state.hostname + '/TegirmonOrderClientProductProduct/getClientProducts?client_id=' + client_id,
          {
            method: 'GET',
            headers: {
              'Content-Type': 'application/json',
            },
          }
        );

        if (!response.ok) {
          const errText = await response.text();
          this.client_zaxira_list = [];
          throw new Error(errText || 'Server xatosi');
        }

        // 🔹 Backenddan kelgan JSON javobni o‘qish
        const data = await response.json();
        console.log('mahsulotlar', data)
        // 🔹 Ma’lumotni tekshirish
        if (Array.isArray(data) && data.length > 0) {
          this.client_zaxira_list = data; // masalan, jadvalda ko‘rsatish uchun
          this.$refs.message.success('Mahsulotlar yuklandi');
        } else {
          this.client_zaxira_list = [];
          this.$refs.message.warning('Bu client uchun mahsulot topilmadi');
        }
      } catch (err) {
        console.error('Xatolik:', err);
        this.client_zaxira_list = [];
        // this.$refs.message.error('Server bilan bog‘lanishda muammo: ' + err.message);
      }
    },
    add_zaxira_product(product){
      console.log('product', product);
      for(let i=0; i<this.datasource.rows.length; i++){
        if(this.datasource.rows[i].zaxira == true && this.datasource.rows[i].tegirmonProductid == product.tegirmonProductid){
          this.$refs.message.warning('Bu mahsulot bor. Faqat sonini uzgartira olasiz')
          return;
        }
      }
      if(product != null){
        this.datasource.rows.unshift({
          tegirmonProductid: product.tegirmonProductid,
          product_name: product.product.name,
          qty: 0,
          qty_str: '0',
          price: 0,
          price_str: '0',
          price_type: 0,
          price_qty: 1,
          price_list: [],
          sum_str: '', // bu optim price nomi
          sum:0,
          id: 0,
          auth_user_updator_id: localStorage.AuthId,
          new_qty: 0,
          old_qty: 0,
          all_qty: product.qty,
          zaxira: true,
        });
      }
      
    },

    selectproduct(option) {
      console.log(option);
      this.datasource.rows[option.row].product_name = option.data.name;
      this.datasource.rows[option.row].tegirmonProductid = option.data.id;
      if(option.data.productPrices.length==0){
        this.datasource.rows[option.row].price = 0;
        this.datasource.rows[option.row].price_str = '0';
        this.datasource.rows[option.row].sum_str = '';
        this.datasource.rows[option.row].price_qty = 0;
        this.datasource.rows[option.row].price_type = 0;
        this.datasource.rows[option.row].price_list = [];
      }
      else{
        this.datasource.rows[option.row].price = option.data.productPrices[0].price;
        this.datasource.rows[option.row].price_str = option.data.productPrices[0].price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.datasource.rows[option.row].sum_str = option.data.productPrices[0].name;
        this.datasource.rows[option.row].price_qty = option.data.productPrices[0].qty;
        this.datasource.rows[option.row].price_type = option.data.productPrices[0].type;
        this.datasource.rows[option.row].price_list = option.data.productPrices;
      }
    },
    selectprice(option){
      this.datasource.rows[option.row].price = option.data.price;
      this.datasource.rows[option.row].price_str = option.data.price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      this.datasource.rows[option.row].sum_str = option.data.name;
      this.datasource.rows[option.row].price_qty = option.data.qty;
      this.datasource.rows[option.row].price_type = option.data.type;
      console.log(option);
    },
    addRow(){
      this.datasource.rows.unshift({
        tegirmonProductid: 0,
        product_name: "",
        qty: 0,
        qty_str: '0',
        price: 0,
        price_str: '0',
        price_type: 0,
        price_qty: 0,
        price_list: [],
        sum_str: '', // bu optim price nomi
        sum:0,
        id: 0,
        auth_user_updator_id: localStorage.AuthId,
        new_qty: 0,
        old_qty: 0,
        all_qty: 0,
        zaxira: false,
      });
      console.log(this.datasource)
    },
    deleteRow(rowIndex){
      this.datasource.rows.splice(parseInt(rowIndex), 1);
      this.all_sum = 0;
      this.all_dollor = 0;
      for(let j=0; j<this.datasource.rows.length; j++){
        if(this.datasource.rows[j].price_type == 1){
          this.all_sum += parseFloat(this.datasource.rows[j].sum);
        }
        else if(this.datasource.rows[j].price_type == 2){
          this.all_dollor += parseFloat(this.datasource.rows[j].sum);
        }
      }
    },
    

    save_data :  async function(){
      // console.log(this.datasource.rows); return;
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
              "sum": this.all_sum,
              "dollor": this.all_dollor,
              "remaining_sum" : this.all_sum,
              "remaining_usd" : this.all_dollor,
              "shafyor_name": this.shafyor_name,
              "car_nomer": this.car_nomer,
              "note": this.note,
              "user_name": this.user_name,
              "client_name": this.client.fio,
              "tegirmonOrderClientid": this.client.id,
              "item_list": this.datasource.rows,
              "tegirmonSkladid": this.sklad_id,
              "pickUpDate": this.choosen_day + 'T10:00:00',
              "paid_status": this.id == 0 ? "Zakaz yaratildi" : "Zakaz o'zgartirildi",
              "load_name": this.load_name,
              "loadedAt": this.loadedAt,
              "kassir_name": this.kassir_name,
              "paidAt": this.paidAt,
              "id" : this.id,
              "tegirmonAuthid": localStorage.AuthId
            })
          };
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/TegirmonOrder", requestOptions);
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
          this.$emit('close');
          
        }

      },

    changeSumma(n,i){
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
       this.datasource.rows[i].qty_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

       var temp = ''
       for(let j=0; j<this.datasource.rows[i].qty_str.length; j++){
        if(this.datasource.rows[i].qty_str[j] != ' '){
          temp += this.datasource.rows[i].qty_str[j];
        }
       }
       if(temp == ''){
        this.datasource.rows[i].qty = 0;
       }
       else{
        if(this.datasource.rows[i].zaxira == true){
          if(parseFloat(temp)> this.datasource.rows[i].all_qty){
            this.datasource.rows[i].qty = this.datasource.rows[i].all_qty;
            this.datasource.rows[i].qty_str = this.datasource.rows[i].qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          }
          else{
            this.datasource.rows[i].qty = parseFloat(temp);
            this.datasource.rows[i].qty_str = this.datasource.rows[i].qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          }
          this.datasource.rows[i].new_qty = this.datasource.rows[i].qty - this.datasource.rows[i].old_qty;
        }
        else{
          this.datasource.rows[i].qty = parseFloat(temp);
        }
       }
      this.datasource.rows[i].sum = ((parseFloat(this.datasource.rows[i].qty) * parseFloat(this.datasource.rows[i].price)) / parseFloat(this.datasource.rows[i].price_qty)).toFixed()
      this.all_sum = 0;
      this.all_dollor = 0;
      for(let j=0; j<this.datasource.rows.length; j++){
        if(this.datasource.rows[j].price_type == 1){
          this.all_sum += parseFloat(this.datasource.rows[j].sum);
        }
        else if(this.datasource.rows[j].price_type == 2){
          this.all_dollor += parseFloat(this.datasource.rows[j].sum);
        }
      }
    },
    clientZaxiraAllQty(i){
      if(this.datasource.rows[i].zaxira == true){
          this.datasource.rows[i].qty = this.datasource.rows[i].all_qty;
          this.datasource.rows[i].qty_str = this.datasource.rows[i].qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          this.datasource.rows[i].new_qty = this.datasource.rows[i].qty - this.datasource.rows[i].old_qty;
        }
    },
    button_qty(i){
      if(this.datasource.rows[i].qty_str == '0'){
        this.datasource.rows[i].qty_str = '';
      }
    },

    changePrice(n,i){
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
       this.datasource.rows[i].price_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

       var temp = ''
       for(let j=0; j<this.datasource.rows[i].price_str.length; j++){
        if(this.datasource.rows[i].price_str[j] != ' '){
          temp += this.datasource.rows[i].price_str[j];
        }
       }
       if(temp == ''){
        this.datasource.rows[i].price = 0;
       }
       else{
        this.datasource.rows[i].price = parseFloat(temp);
       }
      this.datasource.rows[i].sum = ((parseFloat(this.datasource.rows[i].qty) * parseFloat(this.datasource.rows[i].price)) / parseFloat(this.datasource.rows[i].price_qty)).toFixed()
      this.all_sum = 0;
      this.all_dollor = 0;
      for(let j=0; j<this.datasource.rows.length; j++){
        if(this.datasource.rows[j].price_type == 1){
          this.all_sum += parseFloat(this.datasource.rows[j].sum);
        }
        else if(this.datasource.rows[j].price_type == 2){
          this.all_dollor += parseFloat(this.datasource.rows[j].sum);
        }
      }
      
    },
    button_price(i){
      if(this.datasource.rows[i].price_str == '0'){
        this.datasource.rows[i].price_str = '';
      }
    },
  }
}
</script>

<style lang="scss" scoped >
td{
  // margin: 0px !important;
  padding: 0px 15px !important;
}
th{
  // margin: 0px !important;
  padding: 12px 5px !important;
}
.delIcon:hover {
  color: #000;
}
.delIcon {
  padding-top: 8px;
  color: red;
}
input:disabled{
  background: rgb(250, 253, 255);
}
input::placeholder {
  color: rgb(179, 179, 179);       /* rang */
  opacity: 1;       /* brauzer default shaffoflikni olib tashlash */
}
</style>

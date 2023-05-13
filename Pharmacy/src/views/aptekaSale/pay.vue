<template>
  <div class="addProductQtyPayed" >
    <div class="p-4 d-flex">
      <!-- v-show="false" -->
      <div class="client_list_pay" >
        <div class="px-2 d-flex">
          <mdb-input label="Поиск клиента" class="p-0 m-0 mt-3 w-100" size="sm" v-model="search_client" />
          <mdb-btn style="width:170px; height: 32px; font-size: 9px;" 
          class="px-2 py-0 mt-3" color="primary" @click="client_add_activ = true">
          {{$t('client_add')}}</mdb-btn>
        </div>
        <div class="client_add" v-show="client_add_activ">
          <client_add @close="closeClient"/>
        </div>
        <div class="client_list border">
          <div class="d-flex justify-content-between border py-2 px-3 client_item" 
            v-for="(item,i) in client_list" :key="i"
            @click="activ_client(i, item)" :class="{'client_activ_index': activ_index == i}">
            <span style="font-size: 13px;">{{ item.fio }}</span>
            <span style="font-size: 11px;">{{ item.phone_number }}</span>
          </div>
        </div>
      </div>
      <div class="acceptBoxPayed px-3 py-2 text-center">
        <div class="client_info border-bottom">
          <div class="text-center">
            <span style="font-size: 18px;">{{ client_name }}</span>
          </div>
        </div>
        <h2 class="font-weight-bold">
          {{summaString}}
        </h2>
        <input type="text" v-model="enterSumma" v-on:keyup.13 = "payed" pattern="^\$\d{1,3}(,\d{3})*(\.\d+)?$" @keyup="funcCurrency($event.target.value)" ref="enterSumma"  class="form-control form-control-md border mt-2" style="border:none; outline:none;font-weight:bold;" >
        <div class="text-right pt-2">
          <p class="m-0 p-0 zdachi text-danger">{{zdachiString}}</p>
        </div>
        <div class="d-flex mt-2">
          <div class="skidka " style="width:35%">
            <div style="position:relative;">
              <input type="text" v-model="persantage_discount" disabled class="form-control  border mt-2 text-right" style="border:none; outline:none;font-weight:bold; height:30px;" >
              <small style="position:absolute; top:-16px; left:3px;">
                {{$t('skidka')}}
              </small>
            </div>
            <div style="position:relative;">
              <input type="text" v-model="summaString" disabled class="form-control  border mt-3 text-right" style="border:none; outline:none;font-weight:bold; height:30px;" >
              <small style="position:absolute; top:-16px; left:3px;">
                {{$t('Total')}}
              </small> 
            </div>
            <div style="margin-top:15px;">
              <mdb-btn color="info" style="font-size:11px; padding: 6px 0;"   class="m-0  mt-1 w-100" > {{$t('print')}}</mdb-btn>
              <mdb-btn color="success" style="font-size:11px; padding: 6px 0;" :disabled="parseFloat(defaultSum.toFixed(2)) != summa"  @click="payed" class="m-0  mt-1 w-100" > {{'ok'}}</mdb-btn>
              <mdb-btn color="danger" style="font-size:11px; padding: 6px 0;"  @click="close" class="m-0  mt-1 w-100" > {{$t('cancel')}}</mdb-btn>

            </div>
          </div>
          <div class="price ml-4 "  style="width:65%; position:relative;">
            <div style="position:relative;">
              <input type="text" v-model="cashInString"  v-on:keyup.13 = "payed" @keyup="funcCash($event.target.value)" v-on:click.capture="getNol" ref="cashIn"  
              class="form-control  border mt-2 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
              <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                {{$t('cash')}}
              </small> 
            </div> 
            <div style="position:relative;">
              <input type="text" v-model="uzcardInString" v-on:keyup.13 = "payed" @keyup="funcUzcard($event.target.value)" v-on:click.capture="uzcardNol"  ref="uzcardIn" 
              class="form-control  border mt-3 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;">
              <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                {{'Uzcard'}}
              </small> 
            </div>
            <div style="position:relative;">
              <input type="text" v-model="humoInString" v-on:keyup.13 = "payed" @keyup="funcHumo($event.target.value)" v-on:click.capture="humoNol"  ref="humoIn" 
              class="form-control  border mt-3 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
              <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                {{'Humo'}}
              </small> 
            </div>
            <div style="position:relative;">
              <input type="text" v-model="clickInString" v-on:keyup.13 = "payed" @keyup="funcClick($event.target.value)" v-on:click.capture="clickNol"  ref="clickIn" 
              class="form-control  border mt-3 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
              <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold;" class="testing">
                {{$t('skidka')}}
              </small>
            </div>
            <div style="position:relative;">
              <input type="text" v-model="onlineInString" :disabled="client_id == 0" v-on:keyup.13 = "payed" @keyup="funcOnline($event.target.value)" v-on:click.capture="onlineNol"  ref="onlineIn"  
              class="form-control  border mt-3 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
              <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold;" class="testing">
                {{$t('creditd_summ')}}
              </small>
            </div>
            <div class="d-flex justify-content-between mt-2" style="font-size:13.5px;">
              <span v-if="parseFloat(defaultSum.toFixed(2)) > summa" class="text-success"> Больше денег </span>
              <span v-if="parseFloat(defaultSum.toFixed(2)) > summa" class="text-success">{{(parseFloat(defaultSum.toFixed(2))-summa).toFixed(2)}}</span>
            </div>
            <div class="d-flex justify-content-between mt-1" style="font-size:13.5px;">
              <span v-if="parseFloat(defaultSum.toFixed(2)) < summa" class="text-danger"> Не хватить </span>
              <span v-if="parseFloat(defaultSum.toFixed(2)) < summa" class="text-danger">{{(parseFloat(defaultSum.toFixed(2))-summa).toFixed(2)}}</span>
            </div>
          </div>
        </div>
      </div>
    </div>
    
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>

    <Toast ref="message"></Toast>
  </div>
</template>

<script>
import {mapActions, mapGetters, mapMutations} from 'vuex'
import {
  mdbIcon,
  mdbBtn,mdbInput
} from "mdbvue";
import client_add from '../../components/client_add.vue';
export default {
  components: {
    mdbIcon,
    mdbBtn,mdbInput,
    client_add,
  },
  data() {
    return {
      modal_status: false,
      modal_info: '',


      persantage_discount: 0,
      enterSumma: null,
      zdachi:0,
      zdachiString:'0',
      currency: '',
      cashIn: 0,
      uzcardIn: 0,
      humoIn: 0,
      clickIn: 0,
      onlineIn: 0,
      totalIn: 0,
      payshow:false,

      defaultSum: 0,
      discountSum: 0,
      discount: 0,


      cashInString: '0',
      uzcardInString: '0',
      humoInString: '0',
      clickInString: '0',
      onlineInString: '0',
      client_id: 0,
      client_name: '',
      search_client: '',
      client_add_activ: false,
      client_list: [],
      activ_index: -1,
    }
  },
  props:{
    summaString:{
      type: String,
      default: '0'
    },
    summa:{
      type: Number,
      default: 0
    }
  },
  created() {
    this.$root.$refs.payed = this;
  },
  async mounted() {
    console.log(this.summa)
    await this.fetchClient();
  },
  computed:{
    ...mapGetters(['allOrderList', 'get_all_summa', 'get_m_categoryIdProduct', 'get_zakaz_product_all_list','get_page_savat', 'get_product_qty', 'AllSummString']),

  }, 
  methods: {
    ...mapActions([  'fetchCategoryIdProduct', 'fetchProductSearchByName']),
    ...mapMutations([ 'clear_order', 'input_change', 'changeSumma', 'update_zakaz_product_all_list', 'select_savat_page', 'add_savat_page', 'del_savat_page', 'updateCheckId', 'getDiscoundAndDolg']),
    async fetchClient(){
      try{
        const res = await fetch(this.$store.state.hostname + '/PosClients/getClientList?page=0&size=50');
        const res_data = await res.json();
        console.log('res_data');
        console.log(res_data);
        this.client_list = res_data.items_list;
      }
      catch{
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
      }
    },
    async closeClient(){
      this.client_add_activ = false;
      await this.fetchClient();
    },
    async activ_client(i, data){
      this.activ_index = i;
      this.client_id = data.id;
      this.client_name = data.fio;
    },
    changingEnter(paytype){

      this.$nextTick(function () {
      this.$refs.enterSumma.focus();
      this.enterSumma = '';
    })
      this.zdachi = '0';


      console.log(this.summa)
      this.cashIn = 0;
      this.uzcardIn = 0;
      this.humoIn = 0;
      this.clickIn = 0;
      this.onlineIn = 0;
      this.cashInString = '0';
      this.uzcardInString = '0';
      this.humoInString = '0';
      this.clickInString = '0';
      this.onlineInString = '0';
      this.defaultSum = this.summa;
      this.zdachiString = '0';
      this.zdachi = 0;
      if(paytype==1){
        this.cashIn = parseFloat(this.summa)
        this.cashInString = new Intl.NumberFormat().format(this.summa)
      }
      else if(paytype==2){
        this.uzcardIn = parseFloat(this.summa)
        this.uzcardInString = new Intl.NumberFormat().format(this.summa)
      }
      else if(paytype==3){
        this.humoIn = parseFloat(this.summa)
        this.humoInString = new Intl.NumberFormat().format(this.summa)

      }
      else if(paytype==4){
        this.clickIn = parseFloat(this.summa)
        this.clickInString = new Intl.NumberFormat().format(this.summa)
      }
      else if(paytype==5){
        this.onlineIn = parseFloat(this.summa)
        this.onlineInString = new Intl.NumberFormat().format(this.summa)
      }
    },

    
    funcCurrency(n){
      console.log(n)
       this.enterSumma = n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, " ")
       var temp = ''
       for(let i=0; i<this.enterSumma.length; i++){
        if(this.enterSumma[i] != ' '){
          temp += this.enterSumma[i];
        }
       }

        this.currency = parseInt(temp);
        this.zdachi = parseFloat(this.summa)-this.currency;
        this.zdachiString = this.zdachi.toFixed().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
    },
    funcCash(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn)
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))
      console.log('this.discountSum')
      console.log(this.discountSum)
      if(this.discountSum == 0){
      console.log('this.discountSum')
      console.log('this.discountSum')

        this.cashIn = 0;
        this.cashInString = ''; 
        n = n[n.length-1];

      }


      console.log(n)
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.cashInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.cashInString.length; i++){
        if(this.cashInString[i] != ' '){
          temp += this.cashInString[i];
        }
       }
      this.cashIn = parseFloat(temp);
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
    },
    funcUzcard(n){
       this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn)
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))
      console.log('this.discountSum')
      console.log(this.discountSum)
      if(this.discountSum == 0){
      console.log('this.discountSum')
      console.log('this.discountSum')

        this.uzcardIn = 0;
        this.uzcardInString = ''; 
        n = n[n.length-1];

      }

      console.log(n)
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.uzcardInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.uzcardInString.length; i++){
        if(this.uzcardInString[i] != ' '){
          temp += this.uzcardInString[i];
        }
       }
      this.uzcardIn = parseFloat(temp);
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
    },
    async funcHumo(n){
       this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn)
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))

      console.log('this.discountSum')
      console.log(this.discountSum)
      this.discountSum = parseInt(this.discountSum)
      if(this.discountSum == 0){
      console.log('this.discountSum')
      console.log('this.discountSum')

        this.humoIn = 0;
        this.humoInString = ''; 
        n = n[n.length-1];

      }

      console.log(n)
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.humoInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.humoInString.length; i++){
        if(this.humoInString[i] != ' '){
          temp += this.humoInString[i];
        }
       }
      this.humoIn = parseFloat(temp);
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
    },
    funcClick(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn)
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))

      console.log('this.discountSum')
      console.log(this.discountSum)
      if(this.discountSum == 0){
      console.log('this.discountSum')
      console.log('this.discountSum')

        this.clickIn = 0;
        this.clickInString = ''; 
        n = n[n.length-1];

      }

      console.log(n)
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.clickInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.clickInString.length; i++){
        if(this.clickInString[i] != ' '){
          temp += this.clickInString[i];
        }
       }
      this.clickIn = parseFloat(temp);
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
    },
    funcOnline(n){

      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn)
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))

      console.log('this.discountSum')
      console.log(this.discount)
      console.log(this.discountSum)
      if(this.discountSum == 0){
      console.log('this.discountSum')
      console.log('this.discountSum')

        this.onlineIn = 0;
        this.onlineInString = ''; 
        n = n[n.length-1];

      }

      console.log(n)
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.onlineInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.onlineInString.length; i++){
        if(this.onlineInString[i] != ' '){
          temp += this.onlineInString[i];
        }
       }
      this.onlineIn = parseFloat(temp);
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
    },
    
    
    async payed(){
      console.log(this.get_zakaz_product_all_list[this.get_page_savat]) 
      try{
      var orderList = [];
      for(let i=0; i<this.get_zakaz_product_all_list[this.get_page_savat].length; i++){
        if(this.get_zakaz_product_all_list[this.get_page_savat][i].contains_number_in_pack<=1){
          let a = {
            product_id: this.get_zakaz_product_all_list[this.get_page_savat][i].product_id,
            qty: parseFloat(this.get_zakaz_product_all_list[this.get_page_savat][i].qty),
            qty_in_pack: parseFloat(this.get_zakaz_product_all_list[this.get_page_savat][i].qty),
            real_company_id: this.get_zakaz_product_all_list[this.get_page_savat][i].real_company_id,
            real_sum: this.get_zakaz_product_all_list[this.get_page_savat][i].real_sum,
            summ: this.get_zakaz_product_all_list[this.get_page_savat][i].summ,
            unit_price: this.get_zakaz_product_all_list[this.get_page_savat][i].unit_price,
            posInvoiceItemid: this.get_zakaz_product_all_list[this.get_page_savat][i].invoiceItemId
          }
          orderList.push(a)
        }
        else{
          let a = {
            product_id: this.get_zakaz_product_all_list[this.get_page_savat][i].product_id,
            qty: parseInt(this.get_zakaz_product_all_list[this.get_page_savat][i].qty) + parseFloat(this.get_zakaz_product_all_list[this.get_page_savat][i].qty_in_pack)/parseFloat(this.get_zakaz_product_all_list[this.get_page_savat][i].contains_number_in_pack),
            qty_in_pack: (parseFloat(this.get_zakaz_product_all_list[this.get_page_savat][i].qty)*parseFloat(this.get_zakaz_product_all_list[this.get_page_savat][i].contains_number_in_pack)) + parseInt(this.get_zakaz_product_all_list[this.get_page_savat][i].qty_in_pack),
            real_company_id: this.get_zakaz_product_all_list[this.get_page_savat][i].real_company_id,
            real_sum: this.get_zakaz_product_all_list[this.get_page_savat][i].real_sum,
            summ: this.get_zakaz_product_all_list[this.get_page_savat][i].summ,
            unit_price: this.get_zakaz_product_all_list[this.get_page_savat][i].unit_price,
            posInvoiceItemid: this.get_zakaz_product_all_list[this.get_page_savat][i].invoiceItemId
          }
          orderList.push(a)
        }
      }
      console.log('orderList')
      console.log(orderList)
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "posAuthorizationid": localStorage.AuthId,
          "summ" : this.summa,
          "real_company_id": localStorage.CompId,
          "card_sum": parseFloat(this.uzcardIn),
          "cash_sum": parseFloat(this.cashIn),
          "payme_sum": 0,
          "discount_summ": parseFloat(this.clickIn),
          "online_sum": parseFloat(this.onlineIn),
          "humo_sum": parseFloat(this.humoIn),
          "mobil_sum": 0,
          "pos_client_id": this.client_id,
          "paymentsList": orderList,
          // "posClientid": 0
        })
      };
      console.log(requestOptions.body)
      // try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/PosChecks", requestOptions);
        const data = await response.json();
        console.log('check')
        console.log(data)
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          console.log(data.id)
          this.getDiscoundAndDolg({skidka:this.clickIn, dolg:this.onlineIn, client_name: this.client_name})
          this.updateCheckId(data.id)
          this.$emit('print')
          this.$emit('close')
          this.$emit('submitSuccess')
          this.client_id = 0;
          this.client_name = '';
          this.activ_index = -1;
          return true;
        }
        else{
          this.modal_info = this.$i18n.t('network_ne_connect');;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
        return false;
      }
    },


    getNol(){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn)
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);

      console.log('this.discountSum')
      console.log(this.discount)
      console.log(this.cashIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa){
        this.cashIn = this.summa;
        this.cashInString = this.cashIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.clickIn = 0;
        this.clickInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';
      console.log('this.cashIn')
      console.log(this.cashIn)

      }
      else if(this.discountSum > 0){
        this.cashIn = parseFloat(this.discountSum.toFixed(2));
        console.log(this.cashIn)
        this.cashInString = this.cashIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }

      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
    },
    uzcardNol(){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn)
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.uzcardIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa){
        this.uzcardIn = this.summa;
        this.uzcardInString = this.uzcardIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.cashIn = 0;
        this.cashInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.clickIn = 0;
        this.clickInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';
      console.log('this.uzcardIn')
      console.log(this.uzcardIn)

      }
      else if(this.discountSum > 0){
        this.uzcardIn = parseFloat(this.discountSum.toFixed(2));
        console.log(this.uzcardIn)
        this.uzcardInString = this.uzcardIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }

      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
    },
    humoNol(){
      // this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.onlineIn)
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa){
        this.humoIn = this.summa;
        this.humoInString = this.humoIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.clickIn = 0;
        this.clickInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';
      console.log('this.humoIn')
      console.log(this.humoIn)

      }
      else if(this.discountSum > 0){
        this.humoIn = parseFloat(this.discountSum.toFixed(2));
        console.log(this.humoIn)
        this.humoInString = this.humoIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
    },
    clickNol(){
      // this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
      this.discount = parseFloat(this.onlineIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn)
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa){
        this.clickIn = this.summa;
        this.clickInString = this.clickIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
       this.humoIn = 0;
       this.humoInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';
      console.log('this.clickIn')
      console.log(this.clickIn)

      }
      else if(this.discountSum > 0){
        this.clickIn = parseFloat(this.discountSum.toFixed(2));
        console.log(this.clickIn)
        this.clickInString = this.clickIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
    },
    onlineNol(){
      // this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn)
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discount)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa){
        this.onlineIn = this.summa;
        this.onlineInString = this.onlineIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
       this.humoIn = 0;
       this.humoInString = '0';
        this.clickIn = 0;
        this.clickInString = '0';
      console.log('this.onlineIn')
      console.log(this.onlineIn)

      }
      else if(this.discountSum > 0){
        this.onlineIn = parseFloat(this.discountSum.toFixed(2));
        console.log(this.onlineIn)
        this.onlineInString = this.onlineIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
    },
    close(){
      this.$emit('close')
    }
  },
}
</script>

<style lang="scss">
.testing{
  color: rgb(82, 82, 82);
}
 .addProductQtyPayed{
    position: fixed;
    display: flex;
    align-items: center;
    justify-content: center;
    top:0;
    left:0;
    width: 100%;
    height: 100vh;
    background: rgba(113, 114, 116, 0.6);
    z-index: 1111;
  }
  .acceptBoxPayed{
    width:510px;
    height: 460px;
    background: snow;
    box-shadow: 3px 2px 5px rgb(129, 129, 129);
    border-radius: 5px;
  }
  .client_info{
    height: 60px;
  }
  .client_list_pay{
    position: relative;
    overflow-y: hidden;
    width:510px;
    height: 460px;
    background: snow;
    box-shadow: 1px 2px 1px rgb(129, 129, 129);
    border-radius: 5px;
    .client_add{
      background: #e7ffe4;
    }
    .client_list{
      height: 360px;
      overflow-y: hidden;
      overflow-y:scroll;
      .client_item{
        &:hover{
          background: #cee2ff;
          cursor:pointer;
        }
      }
      .client_activ_index{
        background: #93c5ff;
        
      }
    }
  }
  .bg_dark{
    background: #202020;
  }
  .bg_danger{
    background: #fb6b6b;
  }
</style>
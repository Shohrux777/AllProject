<template>
  <div class="addProductQtyPayed" >
    <div class="p-4">
      <div class="acceptBoxPayed px-3 py-2 text-center">
        <h2 class="font-weight-bold">
          {{summ_str}}
        </h2>
        <input type="text" v-model="enterSumma" v-on:keyup.13 = "payed" pattern="^\$\d{1,3}(,\d{3})*(\.\d+)?$" @keyup="funcCurrency($event.target.value)" ref="enterSumma"  class="form-control form-control-md border mt-2" style="border:none; outline:none;font-weight:bold;" >
        <div class="text-right pt-2">
          <p class="m-0 p-0 zdachi text-danger">{{zdachiString}}</p>
        </div>
        <div class="d-flex w-100 mt-2">
          <div class="skidka" style="width:25%">
            <div style="position:relative;">
              <input type="text" v-model="persantage_discountString" @keyup="funcPersantege($event.target.value)" v-on:click.capture="getNolSkidka" @blur="funcSkidkaBlue"
                class="form-control  border mt-2 text-right" style="border:none; outline:none;font-weight:bold; height:30px;" >
              <small style="position:absolute; font-size: 11.5px; top:-16px; left:3px;">
                {{$t('skidka')}}
              </small>
            </div>
            <div style="position:relative;">
              <input type="text" v-model="summ_str" disabled class="form-control  border mt-3 text-right" style="border:none; outline:none;font-weight:bold; height:30px;" >
              <small style="position:absolute; font-size: 11.5px; top:-16px; left:3px;">
                {{$t('Total')}}
              </small> 
            </div>
            <div style="position:relative;">
              <input type="text" v-model="dollor_kurs_str" @keyup="funcDolKurs($event.target.value)"
                class="form-control  border mt-3 text-right" style="border:none; outline:none;font-weight:bold; height:30px;" >
              <small style="position:absolute; font-size: 11.5px; top:-16px; left:3px;">
                Курс
              </small> 
            </div>
            <div style="margin-top:15px;">
              <!-- <mdb-btn color="info" style="font-size:11px; padding: 6px 0;"   class="m-0  mt-1 w-100" > {{$t('print')}}</mdb-btn> -->
              <mdb-btn color="success" style="font-size:10px; padding: 6px 0;" :disabled="parseFloat(defaultSum.toFixed(2) - defaultOrtiqchaSum) != summa"  @click="payed"
                class="m-0 py-3 mt-1 w-100" > {{'Платить'}} </mdb-btn>
              <mdb-btn color="danger" style="font-size:10px; padding: 6px 0;"  @click="close" class="m-0  mt-1 w-100" > {{$t('cancel')}}</mdb-btn>
            </div>
          </div>
          <div style="width:75%;">
            <div class="d-flex">
              <div class="price ml-4"  style="width:55%; position:relative;">
                <div style="position:relative;">
                  <input type="text" v-model="cashInString"  v-on:keyup.13 = "payed" @keyup="funcCash($event.target.value)" v-on:click.capture="getNol" @blur="funcAllBlue" ref="cashIn"  
                  class="form-control  border mt-2 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                  <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                    {{$t('cash')}}
                  </small> 
                </div> 
                <div style="position:relative;">
                  <input type="text" v-model="uzcardInString" v-on:keyup.13 = "payed" @keyup="funcUzcard($event.target.value)" v-on:click.capture="uzcardNol" @blur="funcAllBlue"  ref="uzcardIn" 
                  class="form-control  border mt-3 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;">
                  <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                    {{'Uzcard'}}
                  </small>
                </div>
                <div style="position:relative;">
                  <input type="text" v-model="humoInString" v-on:keyup.13 = "payed" @keyup="funcHumo($event.target.value)" v-on:click.capture="humoNol" @blur="funcAllBlue"  ref="humoIn" 
                  class="form-control  border mt-3 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                  <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                    {{'Humo'}}
                  </small>
                </div>
                <div style="position:relative;">
                  <input type="text" v-model="onlineInString" v-on:keyup.13 = "payed" @keyup="funcOnline($event.target.value)" v-on:click.capture="onlineNol" @blur="funcAllBlue" ref="onlineIn"  
                  class="form-control  border mt-3 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                  <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold;" class="testing">
                    {{$t('online')}}
                  </small> 
                </div>
                <div style="position:relative;">
                  <input type="text" v-model="dollorInString" v-on:keyup.13 = "payed" @keyup="funcDollor($event.target.value)" v-on:click.capture="dollorNol" @blur="funcAllBlue" ref="clickIn" 
                  class="form-control  border mt-3 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                  <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold;" class="testing">
                    {{$t('dollor')}}
                  </small> 
                </div>
              </div>
              <div class="price ml-4"  style="width:45%; position:relative;">
                <div style="position:relative;">
                  <input type="text" v-model="paymeInString"  v-on:keyup.13 = "payed" @keyup="funcPayme($event.target.value)" v-on:click.capture="paymeNol" @blur="funcAllBlue" ref="cashIn"  
                  class="form-control  border mt-2 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                  <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                    {{'Payme'}}
                  </small> 
                </div> 
                <div style="position:relative;">
                  <input type="text" v-model="clickedInString" v-on:keyup.13 = "payed" @keyup="funcClicked($event.target.value)" v-on:click.capture="clickedNol" @blur="funcAllBlue" ref="uzcardIn" 
                  class="form-control  border mt-3 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;">
                  <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                    {{'Click'}}
                  </small> 
                </div>
                <div style="position:relative;">
                  <input type="text" v-model="paynetInString" v-on:keyup.13 = "payed" @keyup="funcPaynet($event.target.value)" v-on:click.capture="paynetNol" @blur="funcAllBlue"  ref="humoIn" 
                  class="form-control  border mt-3 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                  <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                    {{'Paynet'}}
                  </small> 
                </div>
                <div style="position:relative;">
                  <input type="text" v-model="uzumInString" v-on:keyup.13 = "payed" @keyup="funcUzum($event.target.value)" v-on:click.capture="uzumNol" @blur="funcAllBlue"  ref="onlineIn"  
                  class="form-control  border mt-3 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                  <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold;" class="testing">
                    {{'UzumPay'}}
                  </small> 
                </div>
                <div style="position:relative;">
                  <input type="text" v-model="clickInString" v-on:keyup.13 = "payed" @keyup="funcClick($event.target.value)" v-on:click.capture="clickNol" @blur="funcAllBlue"  ref="clickIn" 
                  class="form-control  border mt-3 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                  <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold;" class="testing">
                    Пластикка ўтказма
                  </small>
                </div>
              </div>
            </div>
            <div style="width:100%; position:relative;" class="pl-4 mt-2 pb-0 mb-2 ">
              <div class="ml-0 px-2 py-1 border-bottom border-top" 
                :class="{'alert-success': parseFloat(defaultSum.toFixed(2)) > summa, 'alert-danger': parseFloat(defaultSum.toFixed(2)) < summa}">
                <div class="d-flex justify-content-between mt-0" style="font-size:13.5px;">
                  <span v-if="parseFloat(defaultSum.toFixed(2)) > summa" class="text-success"> Больше денег </span>
                  <span v-if="parseFloat(defaultSum.toFixed(2)) > summa" class="text-success">{{(parseFloat(defaultSum.toFixed(2))-summa).toFixed(2)}}</span>
                </div>
                <div class="d-flex justify-content-between mt-0" style="font-size:13.5px;">
                  <span v-if="parseFloat(defaultSum.toFixed(2)) < summa" class="text-danger"> Не хватить </span>
                  <span v-if="parseFloat(defaultSum.toFixed(2)) < summa" class="text-danger">{{(parseFloat(defaultSum.toFixed(2))-summa).toFixed(2)}}</span>
                </div>
              </div>
            </div>
            <div class="d-flex" v-if="parseFloat(defaultSum.toFixed(2)) > summa">
              <div style="width:55%;" class="pl-4">
                <div style="position:relative;">
                  <input type="text" v-model="naqd_returnInString"  v-on:keyup.13 = "payed" @keyup="funcNaqd_return($event.target.value)" v-on:click.capture="naqd_returnNol" @blur="funcAllBlue" ref="cashIn"  
                  class="form-control  border mt-2 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                  <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                    Қайтим нақт
                  </small> 
                </div> 
              </div>
              <div style="width:45%;" class="pl-4">
                <div style="position:relative;">
                  <input type="text" v-model="dollor_returnInString" v-on:keyup.13 = "payed" @keyup="funcDollor_return($event.target.value)" v-on:click.capture="dollor_returnNol" @blur="funcAllBlue"  ref="uzcardIn" 
                  class="form-control  border mt-2 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;">
                  <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                    {{'Қайтим доллор'}}
                  </small> 
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>

    <Toast ref="message"></Toast>
    <Alert ref="alert"></Alert>
  </div>
</template>

<script>
import {mapActions, mapGetters, mapMutations} from 'vuex'
import {
  mdbIcon,
  mdbBtn,mdbInput
} from "mdbvue";
export default {
  components: {
    mdbIcon,
    mdbBtn,mdbInput,
  },
  data() {
    return {
      modal_status: false,
      modal_info: '',
      summ_str : '0',
      summa: 0,


      persantage_discount: 0,
      persantage_discountString: '0',
      persantage_discount_default: 0,
      persantage_discount_diff: 0,
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

      dollorIn: 0,
      dollorInString: '0',
      paymeIn: 0,
      paymeInString: '0',
      clickedIn: 0,
      clickedInString: '0',
      paynetIn: 0,
      paynetInString: '0',
      uzumIn: 0,
      uzumInString: '0',

      dol_convert_Sum: 0,


      naqd_returnInString: '0',
      naqd_returnIn: 0,
      dollor_returnInString: '0',
      dollor_returnIn: 0,
      dollor_returnSumm: 0,

      defaultOrtiqchaSum: 0,

      dollor_kurs: 0,
      dollor_kurs_str: '0',

    }
  },
  props:{
    summaString:{
      type: String,
      default: '0'
    },
    summa_default:{
      type: Number,
      default: 0
    }
  },
  created() {
    this.$root.$refs.payed = this;
  },
  mounted() {
    console.log(this.summa)
  },
  computed:{
    ...mapGetters(['allOrderList', 'get_skidka_summ', 'get_all_summa', 
    'get_m_categoryIdProduct', 'get_zakaz_product_all_list','get_page_savat', 
    'get_product_qty', 'AllSummString','user_kassa_list']),
  },
  methods: {
    ...mapActions([  'fetchCategoryIdProduct', 'fetchProductSearchByName', 'fetchKassa_userId']),
    ...mapMutations([ 'clear_order', 'input_change', 'changeSumma', 'update_zakaz_product_all_list', 'select_savat_page', 'add_savat_page', 'del_savat_page', 
      'updateCheckId', 'change_all_summa_skidka', 'update_check_type']),
    clw_cl(){
      this.naqd_returnInString = '0';
      this.naqd_returnIn = 0;
      this.dollor_returnInString = '0';
      this.dollor_returnIn = 0;
      this.dollor_returnSumm = 0;
      this.defaultOrtiqchaSum = 0;
    },
    changingEnter(paytype){

    this.$nextTick(function () {
      this.$refs.enterSumma.focus();
      this.enterSumma = '';
    })
      this.summ_str = this.summaString;
      this.zdachi = '0';
      this.summa = this.summa_default;
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
      this.dollorIn = 0;
      this.paymeIn = 0;
      this.clickedIn = 0;
      this.paynetIn = 0;
      this.uzumIn = 0;

      this.dollorInString = '0';
      this.paymeInString = '0';
      this.clickedInString = '0';
      this.paynetInString = '0';
      this.uzumInString = '0';

      this.naqd_returnInString = '0';
      this.naqd_returnIn = 0;
      this.dollor_returnInString = '0';
      this.dollor_returnIn = 0;
      this.dollor_returnSumm = 0;
      this.defaultOrtiqchaSum = 0;

      this.dol_convert_Sum = 0;
      this.defaultSum = this.summa_default;
      this.zdachiString = '0';
      this.zdachi = 0;
      this.persantage_discount = this.get_skidka_summ[this.get_page_savat];
      this.persantage_discountString = this.get_skidka_summ[this.get_page_savat].toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      this.persantage_discount_default = parseFloat(this.get_skidka_summ[this.get_page_savat]);
      this.persantage_discount_diff = 0;
      this.dollor_kurs = localStorage.dollor_kurs;
      this.dollor_kurs_str = localStorage.dollor_kurs.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

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
      else if(paytype==6){
        if(this.dollor_kurs){
          this.dollorIn = (parseFloat(this.summa)/parseFloat(this.dollor_kurs)).toFixed(1);
        }
        else{
          this.dollorIn = 0;
        }
        this.dol_convert_Sum = parseFloat(this.summa)
        this.dollorInString = new Intl.NumberFormat().format(this.dollorIn)
      }
      else if(paytype==7){
        this.paymeIn = parseFloat(this.summa)
        this.paymeInString = new Intl.NumberFormat().format(this.summa)
      }
      else if(paytype==8){
        this.clickedIn = parseFloat(this.summa)
        this.clickedInString = new Intl.NumberFormat().format(this.summa)
      }
      else if(paytype==9){
        this.paynetIn = parseFloat(this.summa)
        this.paynetInString = new Intl.NumberFormat().format(this.summa)
      }
      else if(paytype==10){
        this.uzumIn = parseFloat(this.summa)
        this.uzumInString = new Intl.NumberFormat().format(this.summa)
      }
    },

    async payed(){
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
      console.log(this.get_zakaz_product_all_list[this.get_page_savat]) 
      try{
        var orderList = this.get_zakaz_product_all_list[this.get_page_savat].map(item => {
          return {
            // id: item.product_id,
            tegirmonProductid: item.product_id,
            qty: parseFloat(item.qty),
            summa: item.summ,
            profit_summ: item.unit_price,
            auth_user_creator_id: localStorage.AuthId,
          }
      });
      console.log('orderList')
      console.log(orderList)
      let check_temp = {
        "card": parseFloat(this.uzcardIn),
        "cash": parseFloat(this.cashIn) - parseFloat(this.naqd_returnIn),
        "dolg": parseFloat(this.clickIn),
        "online": parseFloat(this.onlineIn),
        "humo": parseFloat(this.humoIn),
        "profit_summ": parseFloat(this.persantage_discount),
        "real_sum": parseFloat(this.dollorIn) - parseFloat(this.dollor_returnIn), // berilgan dollor qaytarilgandan ayirilgani
        "uz_card": parseFloat(this.paymeIn), // paymen orqali tulov
        "perchisleniya": parseFloat(this.clickedIn), // click orqali tulov
        "creadit_payed": parseFloat(this.paynetIn), // paynet orqali tulov
        "rasxod": parseFloat(this.uzumIn), // uzum orqali tulov
        "salary": parseFloat(this.dollor_returnIn), // qaytarilgan dollor
        "srogi_otganlar_uchun_rasxod": parseFloat(this.naqd_returnIn), // qaytarilgan naqd
      }
      this.update_check_type(check_temp);

      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "tegirmonAuthid": localStorage.AuthId,
          "summ" : this.summa,
          "card": parseFloat(this.uzcardIn),
          "cash": parseFloat(this.cashIn) - parseFloat(this.naqd_returnIn),
          "dolg": parseFloat(this.clickIn),
          "online": parseFloat(this.onlineIn),
          "humo": parseFloat(this.humoIn),
          "profit_summ": parseFloat(this.persantage_discount),
          "real_sum": parseFloat(this.dollorIn) - parseFloat(this.dollor_returnIn), // berilgan dollor qaytarilgandan ayirilgani
          "uz_card": parseFloat(this.paymeIn), // paymen orqali tulov
          "perchisleniya": parseFloat(this.clickedIn), // click orqali tulov
          "creadit_payed": parseFloat(this.paynetIn), // paynet orqali tulov
          "rasxod": parseFloat(this.uzumIn), // uzum orqali tulov
          "salary": parseFloat(this.dollor_returnIn), // qaytarilgan dollor
          "srogi_otganlar_uchun_rasxod": parseFloat(this.naqd_returnIn), // qaytarilgan naqd
          "image_url": this.dollor_kurs.toString(),
          "auth_user_updator_id": localStorage.kassa_id,
          "payments": orderList,
          "auth_user_creator_id": 1,
          "image_base_64": localStorage.user_name
          // "uz_card": 0,     for skidka uchun ishlataman
        })
      };
      console.log('requestOptions.body')
      console.log(requestOptions.body)
      // try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonCheck", requestOptions);
        
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          console.log('check')
          console.log(data)
          console.log(data.id)
          this.updateCheckId(data.id)
          this.$emit('print')
          this.$emit('close')
          this.$emit('submitSuccess')
          return true;
        }
        else{
          const text = await response.text();
          this.modal_info = this.$i18n.t(text);
          this.modal_status = true;
          return false;
        }
      }
      catch{
        const text = await response.text();
        this.modal_info = this.$i18n.t(text);
        this.modal_status = true;
        return false;
      }
    },

    // Kalkulator bulimi
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

    // dollor kursini uzgartiri

    funcDolKurs(n){
      // this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      // parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      // this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      // this.discountSum = parseFloat(this.discountSum.toFixed(2))
      
      // if(this.discountSum == 0){

      //   this.cashIn = 0;
      //   this.cashInString = ''; 
      //   n = n[n.length-1];

      // }

      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.dollor_kurs_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.dollor_kurs_str.length; i++){
        if(this.dollor_kurs_str[i] != ' '){
          temp += this.dollor_kurs_str[i];
        }
       }
      this.dollor_kurs = parseFloat(temp);
      localStorage.dollor_kurs = this.dollor_kurs;
      this.dol_convert_Sum = this.dollor_kurs * parseFloat(this.dollorIn);
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
      // dollor_kurs: localStorage.dollor_kurs,
      // dollor_kurs_str: localStorage.dollor_kurs.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
    
    },
    // yozganda hisoblash qismi
    funcCash(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))
      if(this.discountSum == 0){

        this.cashIn = 0;
        this.cashInString = ''; 
        n = n[n.length-1];

      }

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
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcUzcard(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
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
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcHumo(n){
       this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
       parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
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
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcClick(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
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
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcOnline(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
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
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcPayme(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))
      if(this.discountSum == 0){
        this.paymeIn = 0;
        this.paymeInString = ''; 
        n = n[n.length-1];
      }

      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.paymeInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.paymeInString.length; i++){
        if(this.paymeInString[i] != ' '){
          temp += this.paymeInString[i];
        }
       }
      this.paymeIn = parseFloat(temp);
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcClicked(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))

      if(this.discountSum == 0){
        this.clickedIn = 0;
        this.clickedInString = ''; 
        n = n[n.length-1];
      }
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
      }

      this.clickedInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      var temp = ''
      for(let i=0; i<this.clickedInString.length; i++){
        if(this.clickedInString[i] != ' '){
          temp += this.clickedInString[i];
        }
      }
      this.clickedIn = parseFloat(temp);

      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcPaynet(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))

      if(this.discountSum == 0){
        this.paynetIn = 0;
        this.paynetInString = ''; 
        n = n[n.length-1];
      }
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
      }

      this.paynetInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      var temp = ''
      for(let i=0; i<this.paynetInString.length; i++){
        if(this.paynetInString[i] != ' '){
          temp += this.paynetInString[i];
        }
      }
      this.paynetIn = parseFloat(temp);

      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcUzum(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))

      if(this.discountSum == 0){
        this.uzumIn = 0;
        this.uzumInString = ''; 
        n = n[n.length-1];
      }
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
      }

      this.uzumInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      var temp = ''
      for(let i=0; i<this.uzumInString.length; i++){
        if(this.uzumInString[i] != ' '){
          temp += this.uzumInString[i];
        }
      }
      this.uzumIn = parseFloat(temp);

      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcDollor(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))

      if(this.discountSum == 0){
        this.dollorIn = 0;
        this.dollorInString = ''; 
        this.dol_convert_Sum = 0;
        n = n[n.length-1];
      }
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
      }

      this.dollorInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      var temp = ''
      for(let i=0; i<this.dollorInString.length; i++){
        if(this.dollorInString[i] != ' '){
          temp += this.dollorInString[i];
        }
      }
      this.dollorIn = parseFloat(temp);
      this.dol_convert_Sum = parseFloat(this.dollorIn) * parseFloat(this.dollor_kurs);
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },


    // tanlaganda ishlaydigan funksiya
    getNolSkidka(){
      this.discount = parseFloat(this.cashIn) + parseFloat(this.clickIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + parseFloat(this.dol_convert_Sum) + 
      parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa_default) - parseFloat(this.discount);

      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa || 
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa 
      ){
        this.persantage_discount = this.summa_default + this.persantage_discount_default;   
        this.persantage_discountString = this.persantage_discount.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.persantage_discount_diff = parseFloat(this.persantage_discount) - parseFloat(this.persantage_discount_default);
        this.summ_str = '0';
        this.summa = 0;
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.clickIn = 0;
        this.clickInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';
        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = 0;
        this.dol_convert_Sum = 0;

        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = '0';
      }
      else if(this.discountSum > 0){
        this.persantage_discount = parseFloat(this.discountSum.toFixed(2)) + parseFloat(this.persantage_discount_default);
        this.persantage_discountString = this.persantage_discount.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.persantage_discount_diff = parseFloat(this.persantage_discount) - parseFloat(this.persantage_discount_default);
        this.summa = parseFloat(this.summa_default) - parseFloat(this.persantage_discount_diff)
        this.summ_str = this.summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }

      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
      if(this.persantage_discount_diff != 0){
        this.change_all_summa_skidka({skidka: this.persantage_discount, summa: parseFloat(this.summa)})
      }
    },

    funcSkidkaBlue(){
      if(this.persantage_discountString == '' || this.persantage_discountString == null){
        this.persantage_discountString = '0',
        this.persantage_discount = 0;
        this.persantage_discount_diff = parseFloat(this.persantage_discount) - parseFloat(this.persantage_discount_default);
        this.summa = parseFloat(this.summa_default) + parseFloat(this.persantage_discount_default);
        this.summ_str = this.summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }

      if(this.persantage_discount_diff != 0){
        this.change_all_summa_skidka({skidka: this.persantage_discount, summa: parseFloat(this.summa)})
      }
    },

    funcPersantege(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn) + parseFloat(this.persantage_discount);
      this.discountSum = parseFloat(this.summa_default) - parseFloat(this.discount) + parseFloat(this.persantage_discount_default);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))
      if(this.discountSum == 0){

        this.persantage_discount = 0;
        this.persantage_discountString = '';

        n = n[n.length-1];

      }

      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.persantage_discountString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.persantage_discountString.length; i++){
        if(this.persantage_discountString[i] != ' '){
          temp += this.persantage_discountString[i];
        }
       }
       if(temp == '' || temp == null){
        this.persantage_discount = 0;
       }
       else{
        this.persantage_discount = parseFloat(temp);
       }
      this.summa = parseFloat(this.summa_default) + parseFloat(this.persantage_discount_default) - parseFloat(this.persantage_discount);
      this.summ_str = this.summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      this.persantage_discount_diff = parseFloat(this.persantage_discount) - parseFloat(this.persantage_discount_default);
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();

      if(this.persantage_discount_diff != 0){
        this.change_all_summa_skidka({skidka: this.persantage_discount, summa: parseFloat(this.summa)})
      }
    },



    

    getNol(){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + parseFloat(this.dol_convert_Sum) + 
      parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn)
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount)

      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa || 
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa
      ){
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
        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = 0;
        this.dol_convert_Sum = 0;

        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = '0';

        
      console.log('this.cashIn')
      console.log(this.cashIn)
      console.log(this.dol_convert_Sum)

      }
      else if(this.discountSum > 0){
        this.cashIn = parseFloat(this.discountSum.toFixed(2));
        console.log(this.cashIn)
        this.cashInString = this.cashIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }

      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },
    uzcardNol(){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + parseFloat(this.dol_convert_Sum) + 
      parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.uzcardIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa || 
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
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
        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = 0;
        this.dol_convert_Sum = 0;

        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = '0';
      console.log('this.uzcardIn')
      console.log(this.uzcardIn)

      }
      else if(this.discountSum > 0){
        this.uzcardIn = parseFloat(this.discountSum.toFixed(2));
        console.log(this.uzcardIn)
        this.uzcardInString = this.uzcardIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }

      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },
    humoNol(){
      // this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.onlineIn) + parseFloat(this.dol_convert_Sum) + 
      parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa || 
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
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
        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = 0;
        this.dol_convert_Sum = 0;

        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = '0';
      console.log('this.humoIn')
      console.log(this.humoIn)

      }
      else if(this.discountSum > 0){
        this.humoIn = parseFloat(this.discountSum.toFixed(2));
        console.log(this.humoIn)
        this.humoInString = this.humoIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },
    onlineNol(){
      // this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.dol_convert_Sum) + 
      parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discount)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa || 
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
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
        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = 0;
        this.dol_convert_Sum = 0;

        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = '0';
      console.log('this.onlineIn')
      console.log(this.onlineIn)

      }
      else if(this.discountSum > 0){
        this.onlineIn = parseFloat(this.discountSum.toFixed(2));
        console.log(this.onlineIn)
        this.onlineInString = this.onlineIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },
    dollorNol(){
      // this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
      this.discount = parseFloat(this.onlineIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.clickIn) +
      parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa || 
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
        if(this.dollor_kurs){
          this.dollorIn = (parseFloat(this.summa)/parseFloat(this.dollor_kurs)).toFixed(1);
        }
        else{
          this.dollorIn = 0;
        }
        this.dol_convert_Sum = this.summa
        this.dollorInString = new Intl.NumberFormat().format(this.dollorIn)
        this.clickIn = 0;
        this.clickInString = '0';
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = 0;

        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = '0';
        console.log('this.clickIn')
        console.log(this.clickIn)

      }
      else if(this.discountSum > 0){
        this.dol_convert_Sum = parseFloat(this.discountSum.toFixed(2));
        if(this.dollor_kurs){
          this.dollorIn = (parseFloat(this.dol_convert_Sum)/parseFloat(this.dollor_kurs)).toFixed(1);
        }
        else{
          this.dollorIn = 0;
        }
        this.dollorInString = new Intl.NumberFormat().format(this.dollorIn)
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },

    paymeNol(){
      this.discount = parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + parseFloat(this.clickIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa ||
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
        this.clickIn = 0;
        this.clickInString = '0';
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';
        this.dollorIn = 0;
        this.paymeIn = this.summa;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = 0;
        this.dol_convert_Sum = 0;


        this.dollorInString = '0';
        this.paymeInString = this.paymeIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = '0';
      console.log('this.clickIn')
      console.log(this.clickIn)

      }
      else if(this.discountSum > 0){
        this.paymeIn = parseFloat(this.discountSum.toFixed(2));
        this.paymeInString = this.paymeIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },
    clickedNol(){
      this.discount = parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + parseFloat(this.clickIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa ||
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
        this.clickIn = 0;
        this.clickInString = '0';
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';
        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = this.summa;
        this.paynetIn = 0;
        this.uzumIn = 0;
        this.dol_convert_Sum = 0;


        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = this.clickedIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.paynetInString = '0';
        this.uzumInString = '0';
      console.log('this.clickIn')
      console.log(this.clickIn)

      }
      else if(this.discountSum > 0){
        this.clickedIn = parseFloat(this.discountSum.toFixed(2));
        this.clickedInString = this.clickedIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },
    paynetNol(){
      this.discount = parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + parseFloat(this.clickIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa ||
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
        this.clickIn = 0;
        this.clickInString = '0';
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';

        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = this.summa;
        this.uzumIn = 0;

        this.dol_convert_Sum = 0;

        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = this.paynetIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.uzumInString = '0';
      console.log('this.clickIn')
      console.log(this.clickIn)

      }
      else if(this.discountSum > 0){
        this.paynetIn = parseFloat(this.discountSum.toFixed(2));
        this.paynetInString = this.paynetIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },
    uzumNol(){
      this.discount = parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + parseFloat(this.clickIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa ||
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
        this.clickIn = 0;
        this.clickInString = '0';
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';

        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = this.summa;

        this.dol_convert_Sum = 0;

        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = this.uzumIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      console.log('this.clickIn')
      console.log(this.clickIn)

      }
      else if(this.discountSum > 0){
        this.uzumIn = parseFloat(this.discountSum.toFixed(2));
        this.uzumInString = this.uzumIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },

    clickNol(){
      // this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
      this.discount = parseFloat(this.onlineIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.dol_convert_Sum) + 
      parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa ||
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
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
        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = 0;
        this.dol_convert_Sum = 0;

        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = '0';
      console.log('this.clickIn')
      console.log(this.clickIn)

      }
      else if(this.discountSum > 0){
        this.clickIn = parseFloat(this.discountSum.toFixed(2));
        console.log(this.clickIn)
        this.clickInString = this.clickIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },

    funcAllBlue(){
      if(this.cashInString == null || this.cashInString == ''){
        this.cashIn = 0;
        this.cashInString = '0';
      }
      else if(this.onlineInString == null || this.onlineInString == ''){
        this.onlineIn = 0;
        this.onlineInString = '0';
      }
      else if(this.uzcardInString == null || this.uzcardInString == ''){
        this.uzcardIn = 0;
        this.uzcardInString = '0';
      }
      else if(this.humoInString == null || this.humoInString == ''){
        this.humoIn = 0;
        this.humoInString = '0';
      }
      else if(this.clickInString == null || this.clickInString == ''){
        this.clickIn = 0;
        this.clickInString = '0';
      }
      else if(this.paymeInString == null || this.paymeInString == ''){
        this.paymeIn = 0;
        this.paymeInString = '0';
      }
      else if(this.clickedInString == null || this.clickedInString == ''){
        this.clickedIn = 0;
        this.clickedInString = '0';
      }
      else if(this.paynetInString == null || this.paynetInString == ''){
        this.paynetIn = 0;
        this.paynetInString = '0';
      }
      else if(this.uzumInString == null || this.uzumInString == ''){
        this.uzumIn = 0;
        this.uzumInString = '0';
      }
      else if(this.dollorInString == null || this.dollorInString == ''){
        this.dollorIn = 0;
        this.dollorInString = '0';
        this.dol_convert_Sum = 0;
      }
      else if(this.dollor_returnInString == null || this.dollor_returnInString == ''){
        this.dollor_returnIn = 0;
        this.dollor_returnInString = '0';
        this.dollor_returnSumm = 0;
      }
      else if(this.naqd_returnInString == null || this.naqd_returnInString == ''){
        this.naqd_returnIn = 0;
        this.naqd_returnInString = '0';
      }

      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;

      this.defaultOrtiqchaSum = this.naqd_returnIn + this.dollor_returnSumm;
    },

    naqd_returnNol(){
      let ortiqchaSum = parseFloat(this.defaultSum.toFixed(2)) - this.summa;
      // this.dollor_returnSumm = parseFloat(this.dollor_kurs) * this.dollor_returnIn;
      let deffSum = parseFloat(ortiqchaSum) - parseFloat(this.dollor_returnSumm)
      if(this.naqd_returnIn == ortiqchaSum || this.dollor_returnSumm == ortiqchaSum ){
        this.naqd_returnIn = ortiqchaSum;
        this.naqd_returnInString = ortiqchaSum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

        this.dollor_returnIn = 0;
        this.dollor_returnInString = '0';
        this.dollor_returnSumm = 0;
      }
      else if(deffSum > 0){
        this.naqd_returnIn = deffSum;
        this.naqd_returnInString = this.naqd_returnIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultOrtiqchaSum = this.naqd_returnIn + this.dollor_returnSumm;
      // this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      // this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
    },

    dollor_returnNol(){
      let ortiqchaSum = parseFloat(this.defaultSum.toFixed(2)) - this.summa;
      let deffSum = parseFloat(ortiqchaSum) - parseFloat(this.naqd_returnIn);
      if(this.naqd_returnIn == ortiqchaSum || this.dollor_returnSumm == ortiqchaSum ){
        this.naqd_returnIn = 0;
        this.naqd_returnInString = '0';
        let dollor_change = (parseFloat(ortiqchaSum) / parseFloat(this.dollor_kurs)).toFixed(1);
        this.dollor_returnSumm = parseFloat(ortiqchaSum);
        this.dollor_returnIn = dollor_change;
        this.dollor_returnInString = this.dollor_returnIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      else if(deffSum > 0){
        let dollor_change = (parseFloat(deffSum) / parseFloat(this.dollor_kurs)).toFixed(1);
        this.dollor_returnSumm = parseFloat(deffSum);
        this.dollor_returnIn = dollor_change;
        this.dollor_returnInString = this.dollor_returnIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultOrtiqchaSum = this.naqd_returnIn + this.dollor_returnSumm;
      // this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      // this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
    },

    funcNaqd_return(n){
      
      let disCountSum = parseFloat(this.naqd_returnIn) + parseFloat(this.dollor_returnSumm)
      let ortiqchaSum = parseFloat(this.defaultSum.toFixed(2)) - this.summa; // ortiqcha qaytarish kerak bulgan summa

      let deffSum = parseFloat(ortiqchaSum) - parseFloat(disCountSum);

      if(deffSum == 0){
        this.naqd_returnIn = 0;
        this.naqd_returnInString = '0'; 
        n = n[n.length-1];
      }
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
      }

      this.naqd_returnInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      var temp = ''
      for(let i=0; i<this.naqd_returnInString.length; i++){
        if(this.naqd_returnInString[i] != ' '){
          temp += this.naqd_returnInString[i];
        }
      }
      this.naqd_returnIn = parseFloat(temp);
      this.dollor_returnIn = 0;
      this.dollor_returnInString = '0';
      this.dollor_returnSumm = 0;
      this.defaultOrtiqchaSum = this.naqd_returnIn + this.dollor_returnSumm;
    },

    funcDollor_return(n){
      
      let disCountSum = parseFloat(this.naqd_returnIn) + parseFloat(this.dollor_returnSumm)
      let ortiqchaSum = parseFloat(this.defaultSum.toFixed(2)) - this.summa; // ortiqcha qaytarish kerak bulgan summa

      let deffSum = parseFloat(ortiqchaSum) - parseFloat(disCountSum);

      if(deffSum == 0){
        this.dollor_returnIn = 0;
        this.dollor_returnInString = '0';
        this.dollor_returnSumm = 0;
        n = n[n.length-1];
      }
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
      }

      this.dollor_returnInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      var temp = ''
      for(let i=0; i<this.dollor_returnInString.length; i++){
        if(this.dollor_returnInString[i] != ' '){
          temp += this.dollor_returnInString[i];
        }
      }
      this.dollor_returnIn = parseFloat(temp);
      this.dollor_returnSumm = this.dollor_returnIn * parseFloat(this.dollor_kurs);
      
      this.naqd_returnIn = 0;
      this.naqd_returnInString = '0';

      this.defaultOrtiqchaSum = this.naqd_returnIn + this.dollor_returnSumm;
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
    background: rgba(51, 51, 51, 0.75);
    z-index: 1111;
  }
  .acceptBoxPayed{
    width:610px;
    height: 470px;
    background: rgb(252, 252, 255);
    box-shadow: 3px 2px 5px rgb(129, 129, 129);
    border-radius: 5px;
  }
  .bg_dark{
    background: #202020;
  }
  .bg_danger{
    background: #fb6b6b;
  }
</style>
<template>
  <div class="addProductQtyAcceptCash"  v-on:click.self="close">
    <div class="px-0 py-2" >
      <div class="acceptBoxForTegirmon px-3 py-1 pb-4 text-center">
        <div class="depart_title mb-2 mt-2 border-bottom">
          <div style="height: 44px;" class="d-flex justify-content-between border-bottom align-items-center  ">
            <div class="title w-100 row align-items-center">
               <div class="col-3">
                <div style="position: relative; margin-top: 25px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
                    {{$t('start_time')}}
                  </small>
                  <mdb-input type="date" size="sm"  v-model="Start_time" outline/>
                </div>
              </div>
              <div class="col-3">
                <div style="position: relative; margin-top: 25px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
                    {{$t('end_time')}}
                  </small>
                  <mdb-input type="date" size="sm"   v-model="End_time" outline/>
                </div>
              </div>
              <div class="col-6 ">
                <div class="d-flex ">
                  <mdb-btn @click="apply" color="primary py-2 px-3" style="font-size:9px;">
                    <mdb-icon style="font-size:9.5px;" icon="check" class="m-0 p-0 mr-1" />
                    {{$t('apply')}}
                  </mdb-btn>
                </div>
              </div>
            </div>
            
          </div>
        </div>
        <div class="allSummDollor container-fluid border-bottom pb-2">
          <div class="row">
            <div class="col-4 px-1">
              <div class="price_all_item">
                <div class="qty borderSolder rounded text-left py-2">
                  <span class="ml-3 m-0 p-0">Общий сумма</span>
                  <div class="text-right px-3 mt-1">
                    <p>{{ all_total_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} </p>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="price_all_item ">
                <div class="qty borderSolder rounded text-left py-2">
                  <span class="ml-3">Наличные</span>
                  <div class="text-right px-3 mt-1">
                    <p>{{ (all_cash_summa + yest_day_cash).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} сум</p>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="price_all_item  text-left ">
                <div class="qty borderSolder rounded py-2">
                  <span class="ml-3">{{$t('dollor')}}</span>
                  <div class="text-right px-3 mt-1">
                    <p>{{ (all_dollor_summa + yest_day_dollor).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} $</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="depart_title text-center border-bottom">
          <small>SOTUV BO'LIMI</small>
        </div>

        <div class="mainSellSumms container-fluid">
          <div class="row">
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Oбщий савдо суммаси
                </small>
                <small class="summ_title">
                  {{totalIn}}
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('cash')}}
                </small>
                <small class="summ_title">
                  {{cashInString}}
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  UzCard
                </small>
                <small class="summ_title">
                  {{uzcardInString}}
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Humo
                </small>
                <small class="summ_title">
                  {{humoInString}}
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('online')}}
                </small>
                <small class="summ_title">
                  {{onlineInString}}
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('dollor')}}
                </small>
                <small class="summ_title">
                  {{dollorString}} $
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Payme
                </small>
                <small class="summ_title">
                  {{paymeString}}
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Click
                </small>
                <small class="summ_title">
                  {{clickString}}
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Paynet
                </small>
                <small class="summ_title">
                  {{paynetString}}
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  UzumPay
                </small>
                <small class="summ_title">
                  {{uzumString}}
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Пластикка ўтказма
                </small>
                <small class="summ_title">
                  {{clickInString}}
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('skidka')}}
                </small>
                <small class="summ_title">
                  {{skidkaInString}}
                </small>
              </div>
            </div>
          </div>
        </div>

        <div class="depart_title text-center border-bottom border-top mt-2 mb-2">
          <small>RASXOD PRIXOD BO'LIMI</small>
        </div>

        <div class="mainSellSumms container-fluid">
          <div class="row">
            <div class="col-4 px-1" v-show="false">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('rasxod')}}
                </small>
                <small class="summ_title">
                  {{rasxodInString}}
                </small>
              </div>
            </div>
            <div class="col-6 px-1">
              <div class="summ_item_ p-2  py-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('rasxod')}} Наличные
                </small>
                <small class="summ_title">
                  {{rasxod_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
            </div>
            <div class="col-6 px-1">
              <div class="summ_item_ p-2  py-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('rasxod')}} Доллор
                </small>
                <small class="summ_title">
                  {{dollor_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} $
                </small>
              </div>
            </div>
            <div class="col-4 px-1" v-show="false">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('pul_olish')}}
                </small>
                <small class="summ_title">
                  {{chiqarilPulOlishString}} 
                </small>
              </div>
            </div>
            <div class="col-6 px-1">
              <div class="summ_item_ p-2 py-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('pul_olish')}} Наличные
                </small>
                <small class="summ_title">
                  {{prixod_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
            </div>
            <div class="col-6 px-1">
              <div class="summ_item_ p-2  py-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('pul_olish')}} Доллор
                </small>
                <small class="summ_title">
                  {{dollor_prixod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} $
                </small>
              </div>
            </div>
          </div>
        </div>
        
        <div class="depart_title text-center border-bottom border-top mt-2 mb-2">
          <small>MAHSULOTNI PULGA SOTIB OLISH</small>
        </div>

        <div class="mainSellSumms container-fluid">
          <div class="row">
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Общий покупка продукт
                </small>
                <small class="summ_title">
                  {{sotib_olingan_product_summ}}
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Покупка захира продукт
                </small>
                <small class="summ_title">
                  {{sotib_olingan_zaxira_summ}}
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Покупка алмаштириш продукт
                </small>
                <small class="summ_title">
                  {{sotib_olingan_change_summ}}
                </small>
              </div>
            </div>
          </div>
        </div>
        <div class="depart_title text-center border-bottom border-top mt-2 mb-2">
          <small>ASOSIY KASSAGA UTKAZILGAN SUMMA</small>
        </div>

        <div class="mainSellSumms container-fluid">
          <div class="row">
            <div class="col-6 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Наличные
                </small>
                <small class="summ_title">
                  {{main_kassaga_utkaz_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
            </div>
            <div class="col-6 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Доллор
                </small>
                <small class="summ_title">
                  {{main_kassaga_utkaz_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} $
                </small>
              </div>
            </div>
          </div>
        </div>

        <div class="depart_title text-center border-bottom border-top mt-2 mb-2">
          <small>ASOSIY KASSADAN UTKAZILGAN SUMMA</small>
        </div>

        <div class="mainSellSumms container-fluid">
          <div class="row">
            <div class="col-6 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Наличные
                </small>
                <small class="summ_title">
                  {{savdo_kassaga_utkaz_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
            </div>
            <div class="col-6 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Доллор
                </small>
                <small class="summ_title">
                  {{savdo_kassaga_utkaz_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} $
                </small>
              </div>
            </div>
          </div>
        </div>



        <div class="depart_title text-center border-bottom border-top mt-2 mb-2">
          <small>KASSADA OLDINGI KUNDAN QOLGAN SUMMA</small>
        </div>

        <div class="mainSellSumms container-fluid">
          <div class="row">
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Наличные
                </small>
                <small class="summ_title">
                  {{yest_day_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Доллор
                </small>
                <small class="summ_title">
                  {{yest_day_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} $
                </small>
              </div>
            </div>
            <!-- <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Дата
                </small>
                <small class="summ_title">
                  {{today_day_date}}
                </small>
              </div>
            </div> -->
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
  mdbBtn,
  mdbInput
  
} from "mdbvue";
export default {
  components: {
    mdbIcon,
    mdbBtn,
    mdbInput,
    
  },
  data() {
    return {
      send_kassa_status:false,

      modal_status: false,
      modal_info: '',

      allSumma: 0,


      persantage_discount: 0,
      enterSumma: null,
      totalIn: '0',

      cashInString: '0',
      uzcardInString: '0',
      humoInString: '0',
      clickInString: '0',
      onlineInString: '0',
      sell: '0',
      skidkaInString: '0',
      rasxodInString: '0',
      chiqarilPulOlishString: '0',
      all_summ_prixod: 0,
      all_summ_rasxod: 0,

      sotib_olingan_product_summ: '0',
      sotib_olingan_zaxira_summ: '0',
      sotib_olingan_change_summ: '0',

      dollorString: '0',
      paymeString: '0',
      clickString: '0',
      paynetString: '0',
      uzumString: '0',

      qayt_naqd: '0',
      qayt_dollor: '0',

      rasxod_cash: 0,
      prixod_cash: 0,
      dollor_rasxod: 0,
      dollor_prixod: 0,

      all_total_summa: 0,
      all_cash_summa: 0,
      all_dollor_summa: 0,
      
      Start_time: null,
      End_time: null,

      today_day_cash: 0,
      taday_day_dollor: 0,
      today_day_date: '',


      main_kassaga_utkaz_cash: 0,
      main_kassaga_utkaz_dollor: 0,

      savdo_kassaga_utkaz_cash: 0,
      savdo_kassaga_utkaz_dollor: 0,

      yest_day_cash: 0,
      yest_day_dollor: 0,
      yest_day_date: '',

      real_time_status: false,
      last_create_date: '',

    }
  },
 
  created() {
    this.$root.$refs.closeCashs = this;
  },
  mounted() {
    console.log(this.summa)
  },
  computed:{
    ...mapGetters(['allOrderList', 'get_all_summa', 'get_m_categoryIdProduct', 'get_zakaz_product_all_list','get_page_savat', 'get_product_qty', 'AllSummString']),
  }, 
  methods: {
    ...mapActions([  'fetchCategoryIdProduct', 'fetchProductSearchByName']),
    ...mapMutations([ 'clear_order', 'input_change', 'changeSumma', 'update_zakaz_product_all_list', 'select_savat_page', 'add_savat_page', 'del_savat_page', 'updateCheckId']),

    async getAllSumm(){
      let mtime = new Date().toISOString();
      this.Start_time = mtime.slice(0,10);
      this.End_time = mtime.slice(0,10);
      this.clw_cl();
      await this.yesterdayDay();

      await this.getallCassaSumm();
      await this.fetchRasxod();
      await this.getAllInvoiceSumm();
      await this.getAllInvoiceZaxiraSumm();
      await this.getAllInvoiceChangeSumm();
      // await this.getTodayDate();
      // await this.getNextDate();
      await this.main_kassaga_utkazish();
      await this.savdo_kassaga_utkazish();
      // console.log(this.get_zakaz_product_all_list[this.get_page_savat]) 
    },
    async apply(){
      this.clw_cl();
      await this.getallCassaSumm();
      await this.fetchRasxod();
      await this.getAllInvoiceSumm();
      await this.getAllInvoiceZaxiraSumm();
      await this.getAllInvoiceChangeSumm();
      
      // await this.getTodayDate();

      await this.main_kassaga_utkazish();
      await this.savdo_kassaga_utkazish();
      // console.log(this.get_zakaz_product_all_list[this.get_page_savat])
      await this.fetchChooseDaySumma();

    },
    async getallCassaSumm(){
      this.all_total_summa = 0;
      this.all_cash_summa = 0;
      this.all_dollor_summa = 0;
      let b_data = this.Start_time + 'T00:00:35.000Z';
      let e_data = this.End_time + 'T23:59:59.000Z';
      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonCheck/getKassaCurrentRealTegirmonPosition?begin_date=' +  b_data + '&end_date=' + e_data);
        const res_data = await res.json();
        console.log('res_data check uchun data')
        console.log(res_data)
          this.allSumma = res_data[0].summ;
          this.totalIn = res_data[0].summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
          this.all_total_summa += parseFloat(res_data[0].summ);
          this.all_cash_summa += parseFloat(res_data[0].cash);
          this.all_dollor_summa += parseFloat(res_data[0].real_sum);
          this.cashInString = res_data[0].cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
          this.uzcardInString = res_data[0].card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
          this.humoInString = res_data[0].humo.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
          this.onlineInString = res_data[0].online.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
          this.clickInString = res_data[0].dolg.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
          this.skidkaInString = res_data[0].profit_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')

          this.dollorString = res_data[0].real_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          this.paymeString = res_data[0].uz_card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          this.clickString = res_data[0].perchisleniya.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          this.paynetString = res_data[0].creadit_payed.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          this.uzumString = res_data[0].rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

          this.qayt_naqd = res_data[0].srogi_otganlar_uchun_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          this.qayt_dollor = res_data[0].salary.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          
          
          this.sell = res_data[0].for_buy_tovar_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')

        // }
      }
      catch{
        console.log('error check')
        // this.modal_info = this.$i18n.t('network_ne_connect'); 
        // this.modal_status = true;
        return false;
      }
    },
    async getAllInvoiceSumm(){
      let b_data = this.Start_time + 'T00:00:35.000Z';
      let e_data = this.End_time + 'T23:59:59.000Z';
      // console.log(this.get_zakaz_product_all_list[this.get_page_savat]) 
      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getKassaCurrentInvoiceSotibOlingan?begin_date=' +  b_data + '&end_date=' + e_data);
        const res_data = await res.json();
        this.sotib_olingan_product_summ = res_data[0].summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
        this.all_total_summa = parseFloat(this.all_total_summa) - parseFloat(res_data[0].summ);
        this.all_cash_summa = parseFloat(this.all_cash_summa) - parseFloat(res_data[0].summ);
      }
      catch{
        console.log('ERROR')
        // this.modal_info = this.$i18n.t('network_ne_connect'); 
        // this.modal_status = true;
        return false;
      }
    },
    async getAllInvoiceZaxiraSumm(){
      let b_data = this.Start_time + 'T00:00:35.000Z';
      let e_data = this.End_time + 'T23:59:59.000Z';
      // console.log(this.get_zakaz_product_all_list[this.get_page_savat]) 
      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getKassaCurrentZaxiraInvoiceSumm?begin_date=' +  b_data + '&end_date=' + e_data);
        const res_data = await res.json();
        this.sotib_olingan_zaxira_summ = res_data[0].summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
      }
      catch{
        console.log('ERROR')
        // this.modal_info = this.$i18n.t('network_ne_connect'); 
        // this.modal_status = true;
        return false;
      }
    },
    async getAllInvoiceChangeSumm(){
      let b_data = this.Start_time + 'T00:00:35.000Z';
      let e_data = this.End_time + 'T23:59:59.000Z';
      // console.log(this.get_zakaz_product_all_list[this.get_page_savat]) 
      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getKassaCurrentChangeInvoiceSumm?begin_date=' +  b_data + '&end_date=' + e_data);
        const res_data = await res.json();
        this.sotib_olingan_change_summ = res_data[0].summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
      }
      catch{
        console.log('ERROR')
        // this.modal_info = this.$i18n.t('network_ne_connect'); 
        // this.modal_status = true;
        return false;
      }
    },
    async fetchRasxod(){
      this.all_summ_prixod = 0;
      this.all_summ_rasxod = 0;
      this.rasxod_cash = 0;
      this.prixod_cash = 0;
      this.dollor_rasxod = 0;
      this.dollor_prixod = 0;
      let b_date = this.Start_time + 'T00:00:35.000Z';
      let e_date = this.End_time + 'T23:59:59.000Z';
      try{
          // this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonUserRasxod/getPaginationWorkerListByDate?page=0&size=100000&begin_date=" + b_date + "&end_date=" + e_date);
          // this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            for(let i=0; i<data.items_list.length; i++){
              this.rasxod_cash += parseFloat(data.items_list[i].rasxod);
              this.prixod_cash += parseFloat(data.items_list[i].prixod);
              if(data.items_list[i].status_rasxod == 0){
                this.all_summ_rasxod += parseFloat(data.items_list[i].all_summ);
                this.dollor_rasxod += parseFloat(data.items_list[i].dollor);
              }
              else{
                this.all_summ_prixod += parseFloat(data.items_list[i].all_summ);
                this.dollor_prixod += parseFloat(data.items_list[i].dollor);
              }
            }
            this.rasxodInString = this.all_summ_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
            this.chiqarilPulOlishString = this.all_summ_prixod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
            this.all_total_summa = parseFloat(this.all_total_summa) - parseFloat(this.all_summ_rasxod) + parseFloat(+ this.all_summ_prixod); 
            this.all_cash_summa = parseFloat(this.all_cash_summa) + parseFloat(this.prixod_cash) - parseFloat(this.rasxod_cash);
            this.all_dollor_summa = parseFloat(this.all_dollor_summa) + parseFloat(this.dollor_prixod) - parseFloat(this.dollor_rasxod);
            // this.totalIn = (parseFloat(this.allSumma) - this.all_summ_rasxod + this.all_summ_prixod).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
            return true;
          }
          else{
            const data = await response.text();
            this.modal_info = data;
            this.modal_status = true;
            return false;
          }
        }
        catch{
          this.loading = false;
          // this.modal_info = this.$i18n.t('network_ne_connect'); 
          // this.modal_status = true;
          console.log('error rasxod')
        }
    },
    async main_kassaga_utkazish(){
        this.main_kassaga_utkaz_cash = 0;
        this.main_kassaga_utkaz_dollor = 0;
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.End_time + 'T23:59:35.000Z';
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonMainKassaRasxod/getMainKassaSavdoKassadanOtganSumma?begin_date=" + b_date + "&end_date=" + e_date);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            this.main_kassaga_utkaz_cash += data[0].cash;
            this.main_kassaga_utkaz_dollor += data[0].dollor;
            this.all_cash_summa -= data[0].cash;
            this.all_dollor_summa -= data[0].dollor;
            console.log(this.main_kassaga_utkaz_cash ,  'this.main_kassaga_utkaz_cash')

          }
        }
        catch(error){
          console.log(error);
        }
    },
    async savdo_kassaga_utkazish(){
      this.savdo_kassaga_utkaz_cash = 0;
      this.savdo_kassaga_utkaz_dollor = 0;
      let b_date  = this.Start_time + 'T00:00:35.000Z';
      let e_date  = this.End_time + 'T23:59:35.000Z';
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonMainKassaRasxod/getMainKassadanSavdoKassagaOtganSumma?begin_date=" + b_date + "&end_date=" + e_date);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            this.savdo_kassaga_utkaz_cash += data[0].cash;
            this.savdo_kassaga_utkaz_dollor += data[0].dollor;
            this.all_cash_summa += data[0].cash;
            this.all_dollor_summa += data[0].dollor;
            console.log(data ,  'today prixod data')
          }
        }
        catch(error){
          console.log(error);
        }
    },
    async fetchChooseDaySumma(){
      this.yest_day_cash = 0;
      this.yest_day_dollor = 0;
      let b_date  = this.Start_time + 'T00:00:35.000Z';
      let e_date  = this.Start_time + 'T23:59:35.000Z';
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonKassaSotuvQold/getPaginationWorkerListByDate?page=0&size=100&begin_date=" + b_date + "&end_date=" + e_date);
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          this.yest_day_cash += data.items_list[0].cash;
          this.yest_day_dollor += data.items_list[0].dollor;
          console.log(data.items_list)
        }
      }
      catch(e){
        console.log(e, 'error date buyicha kurishda')
      }
    },
    close(){
      this.$emit('close')
    },
    clw_cl(){
      this.real_time_status = false;
      this.last_create_date = '';
      this.allSumma = 0;
      this.persantage_discount = 0;
      this.enterSumma = null;
      this.totalIn = '0';
      this.cashInString = '0';
      this.uzcardInString = '0';
      this.humoInString = '0';
      this.clickInString = '0';
      this.onlineInString = '0';
      this.sell = '0';
      this.skidkaInString = '0';
      this.rasxodInString = '0';
      this.chiqarilPulOlishString = '0';
      this.all_summ_prixod = 0;
      this.all_summ_rasxod = 0;
      this.sotib_olingan_product_summ = '0';
      this.sotib_olingan_zaxira_summ = '0';
      this.sotib_olingan_change_summ = '0';
      this.dollorString = '0';
      this.paymeString = '0';
      this.clickString = '0';
      this.paynetString = '0';
      this.uzumString = '0';
      this.qayt_naqd = '0';
      this.qayt_dollor = '0';
      this.rasxod_cash = 0;
      this.prixod_cash = 0;
      this.dollor_rasxod = 0;
      this.dollor_prixod = 0;
      this.all_total_summa = 0;
      this.all_cash_summa = 0;
      this.all_dollor_summa = 0;
      this.today_day_cash = 0;
      this.taday_day_dollor = 0;
      this.today_day_date = '';
    },
    async submitNextDay(){
      let tomorrow = new Date();
      tomorrow.setDate(tomorrow.getDate()+1);
      console.log(tomorrow.toISOString());
      try{
        const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "tegirmonAuthid": localStorage.AuthId,
          "credit_status": true,
          "auth_user_creator_id": this.next_day_cash,
          "auth_user_updator_id": this.next_day_dollor,
          "created_date_time": tomorrow.toISOString(),
          "create_date": tomorrow.toISOString(),
          "id": this.next_day_id,
        })
      };
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonCheck", requestOptions);
        const data = await response.json();
        console.log('check next date')
        console.log(data)
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          this.$refs.message.success('Added_successfully')
          this.next_day_cash_string = data.auth_user_creator_id.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          this.next_day_cash = data.auth_user_creator_id;
          this.next_day_dollor_string = data.auth_user_updator_id.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          this.next_day_dollor = data.auth_user_updator_id;
          this.next_day_id = data.id;
          return true;
        }
        else{
          this.modal_info = this.$i18n.t('network_ne_connect');;
          this.modal_status = true;
          return false;
        }
      }
      catch(error){
        console.log(error)
      }
      console.log('hiy');
    },
    async getTodayDate(){
      let b_date = this.Start_time + 'T00:00:35.000Z';
      let e_date = this.End_time + 'T23:59:59.000Z';
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonCheck/getPaginationByBeatWeenTwoDateGetNextDay?page=0&size=10&begin_date=" + b_date + "&end_date=" + e_date);
        // this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          console.log(data, 'today date');
          if(data.items_list.length == 0){
            this.today_day_cash = 0;
            this.taday_day_dollor = 0;
            this.today_day_date = '';
          }
          else{
            console.log(data.items_list[0].auth_user_creator_id)
            this.today_day_cash = data.items_list[0].auth_user_creator_id;
            this.taday_day_dollor = data.items_list[0].auth_user_updator_id;
            this.today_day_date = data.items_list[0].create_date.slice(0,10);
            this.all_cash_summa = parseFloat(this.all_cash_summa) + parseFloat(data.items_list[0].auth_user_creator_id);
            this.all_dollor_summa = parseFloat(this.all_dollor_summa) + parseFloat(data.items_list[0].auth_user_updator_id);
          }
          
          return true;
        }
        else{
          return false;
        }
      }
      catch{
        this.loading = false;
        console.log('error rasxod')
      }
    },
    async getNextDate(){
      let tomorrow = new Date();
      tomorrow.setDate(tomorrow.getDate()+1);
      let new_data = tomorrow.toISOString();
      let b_date = new_data.slice(0,10) + 'T00:00:35.000Z';
      let e_date = new_data.slice(0,10) + 'T23:59:59.000Z';
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonCheck/getPaginationByBeatWeenTwoDateGetNextDay?page=0&size=10&begin_date=" + b_date + "&end_date=" + e_date);
        // this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          console.log(data, 'next date');
          if(data.items_list.length == 0){
            this.next_day_cash_string = '0';
            this.next_day_cash = 0;
            this.next_day_dollor_string = '0';
            this.next_day_dollor = 0;
            this.next_day_id = 0;
          }
          else{
            this.next_day_cash_string = data.items_list[0].auth_user_creator_id.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
            this.next_day_cash = data.items_list[0].auth_user_creator_id;
            this.next_day_dollor_string = data.items_list[0].auth_user_updator_id.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
            this.next_day_dollor = data.items_list[0].auth_user_updator_id;
            this.next_day_id = data.items_list[0].id;
          }
          return true;
        }
        else{
          return false;
        }
      }
      catch{
        this.loading = false;
        console.log('error rasxod')
      }
    },



    async yesterdayDay(){
      this.yest_day_cash = 0;
      this.yest_day_dollor = 0;
      this.yest_day_date = '';
      let yesterday = new Date(this.Start_time);
      yesterday.setDate(yesterday.getDate()-29);
      let new_data = yesterday.toISOString();
      let b_date = new_data.slice(0,10) + 'T00:00:35.000Z'
      let e_date  = this.Start_time + 'T23:59:35.000Z';
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonKassaSotuvQold/getPaginationWorkerListByDate?page=0&size=100&begin_date=" + b_date + "&end_date=" + e_date);
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          if(data.items_list.length>0){
            this.yest_day_cash += data.items_list[0].cash;
            this.yest_day_dollor += data.items_list[0].dollor;
            console.log(this.last_create_date, 'this.last_create_date')
            this.yest_day_date = data.items_list[0].created_date.slice(0,10);
            if(this.yest_day_date == this.Start_time){
              console.log('bugungi malumotlar yozilgan')
              return;
            }
            else{
              console.log(this.yest_day_date, 'yest_day_date')
              console.log(this.Start_time, 'yest_day_date')
              let yesterday = new Date(this.Start_time);
              yesterday.setDate(yesterday.getDate()-1);
              let new_data = yesterday.toISOString();
              this.fetch_kecha_kun_hisobot(this.yest_day_date, new_data.slice(0,10)); 
            }
          }
          else{
            console.log('bazada malumot yuqga kirdi');
            this.yest_day_cash = 0;
            this.yest_day_dollor = 0;
            let yesterday = new Date(this.Start_time);
            yesterday.setDate(yesterday.getDate()-1);
            let new_data = yesterday.toISOString();
            this.fetch_kecha_kun_hisobot(new_data.slice(0,10), new_data.slice(0,10));
          }
        }
      }
      catch(error){
        console.log('error kechagi kun malumoti', error)
      }
    },

    async fetch_kecha_kun_hisobot(b_data, e_data){
      console.log(b_data, e_data, 'test uchun')
      await this.getYesterdayallCassaSumm(b_data, e_data);
      await this.fetchYesterdayRasxod(b_data, e_data);
      await this.getYesterdayAllInvoiceSumm(b_data, e_data);
      await this.main_yesterday_kassaga_utkazish(b_data, e_data)
      await this.savdo_yesterday_kassaga_utkazish(b_data, e_data);

      // malumotlarni oxirida qushish bugunga sotuvQoldiqqa
      await this.fetchSubmitAddLastDay();
    },

    async getYesterdayallCassaSumm(last_date, next_date){
      let b_data = last_date + 'T00:00:35.000Z';
      let e_data = next_date + 'T23:59:59.000Z';
      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonCheck/getKassaCurrentRealTegirmonPosition?begin_date=' +  b_data + '&end_date=' + e_data);
        const res_data = await res.json();
          // this.allSumma = res_data[0].summ;
          console.log('sotuv qismi dalniylari', res_data)
          if(res_data[0].cash){
            this.yest_day_cash += parseFloat(res_data[0].cash);
          }
          if(res_data[0].real_sum){
            this.yest_day_dollor += parseFloat(res_data[0].real_sum);
          }
          console.log(this.yest_day_cash, '0')
          console.log(this.yest_day_dollor, '0')
      }
      catch(e){
        console.log(e,'error check')
        return false;
      }
    },
    async fetchYesterdayRasxod(last_date, next_date){
      let b_date = last_date + 'T00:00:35.000Z';
      let e_date = next_date + 'T23:59:59.000Z';
      try{
          const response = await fetch(this.$store.state.hostname + "/TegirmonUserRasxod/getPaginationWorkerListByDate?page=0&size=100000&begin_date=" + b_date + "&end_date=" + e_date);
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            let rasxod_cash = 0;
            let prixod_cash = 0;
            let dollor_rasxod = 0;
            let dollor_prixod = 0;
            for(let i=0; i<data.items_list.length; i++){
              rasxod_cash += parseFloat(data.items_list[i].rasxod);
              prixod_cash += parseFloat(data.items_list[i].prixod);
              if(data.items_list[i].status_rasxod == 0){
                dollor_rasxod += parseFloat(data.items_list[i].dollor);
              }
              else{
                dollor_prixod += parseFloat(data.items_list[i].dollor);
              }
            }
            this.yest_day_cash = parseFloat(this.yest_day_cash) + parseFloat(prixod_cash) - parseFloat(rasxod_cash);
            this.yest_day_dollor = parseFloat(this.yest_day_dollor) + parseFloat(dollor_prixod) - parseFloat(dollor_rasxod);
            console.log(this.yest_day_cash, 'rasxod bulimi')
            console.log(this.yest_day_dollor, 'rasxod dollor')
            return true;
          }
          else{
            // const data = await response.text();
            // this.modal_info = data;
            // this.modal_status = true;
            return false;
          }
        }
        catch{
          this.loading = false;
          console.log('error rasxod')
        }
    },
    async getYesterdayAllInvoiceSumm(last_date, next_date){
      let b_data = last_date + 'T00:00:35.000Z';
      let e_data = next_date + 'T23:59:59.000Z'; 
      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getKassaCurrentInvoiceSotibOlingan?begin_date=' +  b_data + '&end_date=' + e_data);
        const res_data = await res.json();
        console.log(res_data, res, 're_data')
        if(res_data[0].summ){
          this.yest_day_cash = parseFloat(this.yest_day_cash) - parseFloat(res_data[0].summ);
        }
        console.log(this.yest_day_cash, 'invoie cash');
      }
      catch{
        console.log('ERROR Invoice Summa')
        return false;
      }
    },
    async main_yesterday_kassaga_utkazish(last_date, next_date){
        let b_date  = last_date + 'T00:00:35.000Z';
        let e_date  = next_date + 'T23:59:35.000Z';
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonMainKassaRasxod/getMainKassaSavdoKassadanOtganSumma?begin_date=" + b_date + "&end_date=" + e_date);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            if(data[0].cash){
              this.yest_day_cash -= data[0].cash;
            }
            if(data[0].dollor){
              this.yest_day_dollor -= data[0].dollor;
            }
            console.log(this.yest_day_cash, 'kassaga utkazish')
            console.log(this.yest_day_dollor, 'kassaga utkazish')
          }
        }
        catch(error){
          console.log(error, 'errorcha');
        }
    },
    async savdo_yesterday_kassaga_utkazish(last_date, next_date){
      this.savdo_kassaga_utkaz_cash = 0;
      this.savdo_kassaga_utkaz_dollor = 0;
      let b_date  = last_date + 'T00:00:35.000Z';
      let e_date  = next_date + 'T23:59:35.000Z';
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonMainKassaRasxod/getMainKassadanSavdoKassagaOtganSumma?begin_date=" + b_date + "&end_date=" + e_date);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            if(data[0].cash){
              this.yest_day_cash += data[0].cash;
            }
            if(data[0].dollor){
              this.yest_day_dollor += data[0].dollor;
            }
            console.log(this.yest_day_cash, 'savdo kassaga utkazish')
          console.log(this.yest_day_dollor, 'savdo kassaga utkazish')
            console.log(data ,  'today prixod data')
          }
        }
        catch(error){
          console.log(error);
          console.log(error, 'errorcha');
        }
    },



    async fetchSubmitAddLastDay(){
      // if(await this.fetchCurrentDateTime()){
      //   this.modal_info = "Komputerni vaqti xato bulishi tekshirib qayta obnavit qilib kuring";
      //   this.yest_day_cash = 0;
      //   this.yest_day_dollor = 0; 
      //   this.modal_status = true;
      //   return;
      // }
      console.log(this.yest_day_cash, 'qushish')
      console.log(this.yest_day_dollor, '0')
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "cash": this.yest_day_cash,
          "dollor": this.yest_day_dollor,
          "string1": localStorage.user_name,
          "auth_id": localStorage.AuthId,
        })
      };
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonKassaSotuvQold", requestOptions);
        console.log(response)
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          console.log(data, 'tugadi yozildi malumot');
        }
      }
      catch(error){
        console.log(error, 'utgan kundagi malumotni yozish');
      }
    },
    async fetchCurrentDateTime(){
      try{
        const response = await fetch("https://timeapi.io/api/time/current/zone?timeZone=Asia%2FYekaterinburg");
        console.log(response)
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          console.log(data, 'online date time', data.dateTime.slice(0,10));
          if(data.dateTime.slice(0,10) == this.Start_time){
            return false;
          }
          else{
            return true;
          }
        }
        else{
          return false;
        }
      }
      catch(error){
        console.log(error, 'data time olinmadi va catch bulimiga utdi');
        return false;
      }
    }
  },
}
</script>

<style lang="scss">
  .addProductQtyAcceptCash{
    position: fixed;
    display: flex;
    justify-content: center;
    top:0;
    left:0;
    width: 100%;
    height: 100vh;
    background: rgba(19, 19, 49, 0.65);
    z-index: 1111;
    animation-name: example1;
    animation-duration: 0.4s;
  }
  .acceptBoxForTegirmon{
    width:70vw;
    max-height: 100vh;
    overflow-y: scroll;
    background: snow;
    box-shadow: 3px 2px 5px rgb(129, 129, 129);
    border-radius: 5px;
    position: relative;
    animation-name: example;
    animation-duration: 0.4s;
  }
  @keyframes example {
    0%   { left:0px; top:-100px; opacity: 0;}

    100% { left:0px; top:0px; opacity: 1;}
  }
  // @keyframes example1 {
  //   0%   {  opacity: 0;}

  //   100% {  opacity: 1;}
  // }

  .price_all_item .borderSolder{
    //border: 0.5px dashed #D0D3D8;
    background-image: linear-gradient( 65.9deg,  rgba(85,228,224,1) 5.5%, rgba(75,68,224,0.74) 54.2%, rgba(64,198,238,1) 55.2%, rgba(177,36,224,1) 98.4% );

    span{
      color:#26254a;
      font-size: 21px;
      font-weight: 450;
    }
    p{
      color:#ffffff;
      font-weight:bold;
      font-size: 23px;
      margin:0;
      padding:0;
    }
  }
  .depart_title small{
    font-style: italic;
    color: #0600b3;
    font-weight:bold;
  }
  .summ_title{
    font-size: 15px;
    font-style: italic;
    color: #3c3669;
    font-weight: bold;
  }
  .summ_item_{
    border-bottom: 1px dashed rgb(110, 110, 110);
  }
</style>
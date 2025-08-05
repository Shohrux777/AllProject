<template>
    <div class="">
      <div class="px-0 py-0" >
        <div class=" px-3 py-1 pb-4 text-center">
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
                    <mdb-btn @click="show_all_info = !show_all_info" color="info py-2 px-3" style="font-size:9px;">
                      <mdb-icon style="font-size:9.5px;" icon="eye" class="m-0 p-0 mr-1" />
                    </mdb-btn>
                    <mdb-btn v-if="asosiy_kassa_status" @click="$router.push('/main_kassa')" color="success py-2 px-3" style="font-size:9px;">
                      <mdb-icon style="font-size:9.5px;" icon="calendar" class="m-0 p-0 mr-1" />
                      Асосий касса
                    </mdb-btn>
                  </div>
                </div>
              </div>
              
            </div>
          </div>
          <div class="allSummDollor container-fluid border-bottom pb-2">
            <div class="row">
              
              <div class="col-4 px-1">
                <div class="price_all_item ">
                  <div class="qty borderSolder rounded text-left py-2">
                    <span class="ml-3">Наличные</span>
                    <div class="text-right px-3 mt-1">
                      <p>{{ (all_cash_summa).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} сум</p>
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-4 px-1">
                <div class="price_all_item  text-left ">
                  <div class="qty borderSolder rounded py-2">
                    <span class="ml-3">{{$t('dollor')}}</span>
                    <div class="text-right px-3 mt-1">
                      <p>{{ (all_dollor_summa).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} $</p>
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-4 px-1">
                <div class="price_all_item">
                  <div class="qty borderSolder rounded text-left py-2">
                    <span class="ml-3 m-0 p-0">Другой сумма</span>
                    <div class="text-right px-3 mt-1">
                      <p>{{ another_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} </p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div v-show="show_all_info">
            <div class="depart_title text-center border-bottom">
              <small>SOTUV BO'LIMI</small>
            </div>
    
            <div class="mainSellSumms container-fluid">
              <div class="row">
                <div class="col-3 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      Oбщий савдо суммаси
                    </small>
                    <small class="summ_title">
                      {{totalIn}}
                    </small>
                  </div>
                </div>
                <div class="col-3 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      {{$t('cash')}}
                    </small>
                    <small class="summ_title text-success">
                      {{cashInString}}
                    </small>
                  </div>
                </div>
                <div class="col-3 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      UzCard
                    </small>
                    <small class="summ_title">
                      {{uzcardInString}}
                    </small>
                  </div>
                </div>
                <div class="col-3 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      Humo
                    </small>
                    <small class="summ_title">
                      {{humoInString}}
                    </small>
                  </div>
                </div>
                <div class="col-3 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      {{$t('online')}}
                    </small>
                    <small class="summ_title">
                      {{onlineInString}}
                    </small>
                  </div>
                </div>
                <div class="col-3 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      {{$t('dollor')}}
                    </small>
                    <small class="summ_title text-success">
                      {{dollorString}} $
                    </small>
                  </div>
                </div>
                <div class="col-3 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      Payme
                    </small>
                    <small class="summ_title">
                      {{paymeString}}
                    </small>
                  </div>
                </div>
                <div class="col-3 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      Click
                    </small>
                    <small class="summ_title">
                      {{clickString}}
                    </small>
                  </div>
                </div>
                <div class="col-3 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      Paynet
                    </small>
                    <small class="summ_title">
                      {{paynetString}}
                    </small>
                  </div>
                </div>
                <div class="col-3 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      UzumPay
                    </small>
                    <small class="summ_title">
                      {{uzumString}}
                    </small>
                  </div>
                </div>
                <div class="col-3 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      Пластикка ўтказма
                    </small>
                    <small class="summ_title">
                      {{clickInString}}
                    </small>
                  </div>
                </div>
                <div class="col-3 px-1">
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
                      {{all_summ_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                    </small>
                  </div>
                </div>
                <div class="col-3 px-1">
                  <div class="summ_item_ p-2  py-1 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      {{$t('rasxod')}} Наличные
                    </small>
                    <small class="summ_title text-danger">
                      {{rasxod_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                    </small>
                  </div>
                </div>
                <div class="col-3 px-1">
                  <div class="summ_item_ p-2  py-1 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      {{$t('rasxod')}} Доллор
                    </small>
                    <small class="summ_title  text-danger" >
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
                      {{all_summ_prixod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} 
                    </small>
                  </div>
                </div>
                <div class="col-3 px-1">
                  <div class="summ_item_ p-2 py-1 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      {{$t('pul_olish')}} Наличные
                    </small>
                    <small class="summ_title text-success">
                      {{prixod_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                    </small>
                  </div>
                </div>
                <div class="col-3 px-1">
                  <div class="summ_item_ p-2  py-1 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      {{$t('pul_olish')}} Доллор
                    </small>
                    <small class="summ_title text-success">
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
                    <small class="summ_title  text-danger">
                      {{(sotib_olingan_mahsulot_zaxiradan + sotib_olingan_change_product_summ).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                    </small>
                  </div>
                </div>
                <div class="col-4 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      Покупка захира продукт 
                    </small>
                    <small class="summ_title  text-danger">
                      {{sotib_olingan_mahsulot_zaxiradan.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                    </small>
                  </div>
                </div>
                <div class="col-4 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      Покупка алмаштириш продукт
                    </small>
                    <small class="summ_title  text-danger">
                      {{sotib_olingan_change_product_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
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
                    <small class="summ_title  text-danger">
                      {{main_kassaga_utkaz_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                    </small>
                  </div>
                </div>
                <div class="col-6 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      Доллор
                    </small>
                    <small class="summ_title  text-danger">
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
                    <small class="summ_title text-success">
                      {{savdo_kassaga_utkaz_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                    </small>
                  </div>
                </div>
                <div class="col-6 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      Доллор
                    </small>
                    <small class="summ_title text-success">
                      {{savdo_kassaga_utkaz_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} $
                    </small>
                  </div>
                </div>
              </div>
            </div>


            <div class="depart_title text-center border-bottom border-top mt-2 mb-2">
              <small>POLUCHIT PRODUCTDAN OLINGAN SUMMA</small>
            </div>
    
            <div class="mainSellSumms container-fluid">
              <div class="row">
                <div class="col-4 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      Наличные
                    </small>
                    <small class="summ_title">
                      {{poluchit_productdan_olingan_naqd.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                    </small>
                  </div>
                </div>
                <div class="col-4 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      Доллор
                    </small>
                    <small class="summ_title">
                      {{poluchit_productdan_olingan_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                    </small>
                  </div>
                </div>
                <div class="col-4 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      Счета
                    </small>
                    <small class="summ_title">
                      {{poluchit_productdan_hisobga.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
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
                    <small class="summ_title text-success">
                      {{yest_day_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                    </small>
                  </div>
                </div>
                <div class="col-4 px-1">
                  <div class="summ_item_ p-2 d-flex justify-content-between">
                    <small style="font-size: 13px;">
                      Доллор
                    </small>
                    <small class="summ_title text-success">
                      {{yest_day_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} $
                    </small>
                  </div>
                </div>
              </div>
            </div>
          </div>
  
          
        </div>
      </div>

      <div class="px-3 container-fluid">
        <div class="row">
          <div class="col-4" v-for="(row,i) in sortNameList" :key="i">
            <div class="p-2 card mt-3" style="cursor: pointer;" @click="show_kassa_item(row)">
              <div class="kassa_item_header rounded d-flex justify-content-between align-items-center p-1 px-2" :style="{'background': colors[i] || '#ff5733'}">
                <span>
                  {{ row.kassa.name }}
                </span>
                <small>
                  {{ row.kassa.text_1 }}
                </small>
              </div>
              <div>
                <div class="summ_item_ p-2 d-flex justify-content-between">
                  <small style="font-size: 13px;">
                    Наличные
                  </small>
                  <small class="summ_title text-success">
                    {{row.cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} 
                  </small>
                </div>
                <div class="summ_item_ p-2 d-flex justify-content-between">
                  <small style="font-size: 13px;">
                    Доллор
                  </small>
                  <small class="summ_title text-success">
                    {{row.dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} 
                  </small>
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
      <kassa_info_item v-show="closeCash" ref="kassa_info_item_ref"  @close="closeKassa" />
      
    </div>
  </template>
  
  <script>
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  import kassa_info_item from './kassa_info_item.vue';
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
      kassa_info_item
    },
    data() {
      return {
        send_kassa_status:false,
        closeCash: false,

        show_all_info: false,
        colors: ['#ff5733', '#33ff83', '#ac33ff', '#339fff', '#ff3393', '#ff9233', '#9033ff','#ff5733', '#33ff83', '#ac33ff', '#339fff'],
  
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
  
        sotib_olingan_mahsulot_zaxiradan : 0,
        sotib_olingan_change_product_summ : 0,
  
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

        kassa_items_list: [],
  
  
  
        main_kassaga_utkaz_cash: 0,
        main_kassaga_utkaz_dollor: 0,
  
        savdo_kassaga_utkaz_cash: 0,
        savdo_kassaga_utkaz_dollor: 0,
  
        yest_day_cash: 0,
        yest_day_dollor: 0,

        another_summa: 0,
  
        real_time_status: false,
        last_create_date: '',

        kassa_id: 0,

        poluchit_productdan_hisobga: 0,
        poluchit_productdan_olingan_naqd: 0,
        poluchit_productdan_olingan_dollor: 0,

        asosiy_kassa_status: false,
  
      }
    },
   
    // created() {
    //   this.$root.$refs.closeCashs = this;
    // },
    async mounted() {
        let mtime = new Date().toISOString();
        this.Start_time = mtime.slice(0,10);
        this.End_time = mtime.slice(0,10);
        this.clw_cl();
        await this.fetchUserAccess(localStorage.user_id);

        this.kassa_items_list = [];
        await this.fetchKassa();
        for(let i=0; i<this.allKassa.rows.length; i++){
          await this.fetchKassa_info(this.allKassa.rows[i].id);
          this.all_cash_summa += this.user_kassa_info.cash;
          this.all_dollor_summa += this.user_kassa_info.dollor;
          this.yest_day_cash += this.user_kassa_info.before_cash;
          this.yest_day_dollor += this.user_kassa_info.before_dollor;
          this.kassa_items_list.push(await this.user_kassa_info)
        }
        await this.getallCassaSumm();
        
        await this.fetchRasxod();
        
        await this.fetchPrixod();
        
        await this.getAllInvoiceZaxiraSumm();

        await this.getAllInvoiceChangeSumm();

        await this.getAllInvoicePoluchitSumma();
        await this.getAllInvoicePoluchitDollor();

        await this.getAllInvoicePoluchitHisobgaSumma();

        await this.main_kassaga_utkazish();

        await this.savdo_kassaga_utkazish();
    },
    computed:{
      ...mapGetters(['allOrderList', 'get_all_summa', 
      'get_m_categoryIdProduct', 'get_zakaz_product_all_list',
      'get_page_savat', 'get_product_qty', 'AllSummString', 'allKassa', 'user_kassa_info']),
      sortNameList(){
        function compare(a, b) {
          if (a.kassa.id < b.kassa.id)
            return -1;
          if (a.kassa.id > b.kassa.id)
            return 1;
          return 0;
        }
        return this.kassa_items_list.sort(compare);
      },
    }, 
    methods: {
      ...mapActions([  'fetchCategoryIdProduct', 'fetchProductSearchByName', 'fetchKassa', 'fetchKassa_info']),
      ...mapMutations([ 'clear_order', 'input_change', 'changeSumma', 'update_zakaz_product_all_list', 'select_savat_page', 'add_savat_page', 'del_savat_page', 'updateCheckId']),
      async fetchUserAccess(id){
        try{
            const res = await fetch(this.$store.state.hostname + '/TegirmonUserAccess/getTegirmonUserAccessUserId?user_id=' + id);
            const data = await res.json();
            console.log('this is by id')
            if(res.status == 200 || res.status == 201){
              this.asosiy_kassa_status = data.status_2;
            }
          }
          catch(error){
            console.log(error)
          }
        },
      
      async show_kassa_item(data){
        this.closeCash = true;
        this.$refs.kassa_info_item_ref.getAllSumm({kassa_id: data.kassa.id, b_data: this.Start_time, e_data: this.End_time});
      },
      closeKassa(){
        this.closeCash = false;
      },

      async apply(){
        this.clw_cl();
        // await this.next_day_cash_all_summ();
        // await this.before_day_cash_all_summ();
        this.all_cash_summa = 0;
        this.all_dollor_summa = 0;
        this.yest_day_cash = 0;
        this.yest_day_dollor = 0;
        
        this.kassa_items_list = [];
        for(let i=0; i<this.allKassa.rows.length; i++){
          await this.next_day_cash_all_summ(this.allKassa.rows[i].id);
          await this.before_day_cash_all_summ(this.allKassa.rows[i].id); 
        }
        
        await this.getallCassaSumm();
        
        await this.fetchRasxod();
        
        await this.fetchPrixod();
        
        await this.getAllInvoiceZaxiraSumm();

        await this.getAllInvoiceChangeSumm();

        await this.getAllInvoicePoluchitSumma();
        await this.getAllInvoicePoluchitDollor();

        await this.getAllInvoicePoluchitHisobgaSumma();

        await this.main_kassaga_utkazish();

        await this.savdo_kassaga_utkazish();
      },


      // Vaqt oraligida oxirgi obshey summani hisoblash yani tanlangan oxirgi kundi chiqaradi <==
      async next_day_cash_all_summ(kassa_id){
        let b_date  = this.End_time + 'T00:00:35.000Z';
        let e_date  = this.End_time + 'T23:59:35.000Z';
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/TegirmonKassaInfo/getPaginationByBeatWeenTwoDateKassaInfoKassaID?page=0&size=100&begin_date=" + b_date + "&end_date=" + e_date + "&kassa_id=" + kassa_id);
            this.loading = false;
            if(response.status == 201 || response.status == 200)
            {
              const data = await response.json();
              if(data.items_list.length>0){
                this.all_cash_summa += data.items_list[0].cash;
                this.all_dollor_summa += data.items_list[0].dollor;
                this.kassa_items_list.push(data.items_list[0])
              }
            }
          }
          catch(error){
            console.log('8')
            console.log(error);
          }
      },
      // Vaqt oraligida oxirgi obshey summani hisoblash yani tanlangan oxirgi kundi chiqaradi <==

      // Vaqt oraligida boshidagi obshey summani hisoblash yani tanlangan boshidagi kundi chiqaradi <==
      async before_day_cash_all_summ(kassa_id){
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.Start_time + 'T23:59:35.000Z';
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/TegirmonKassaInfo/getPaginationByBeatWeenTwoDateKassaInfoKassaID?page=0&size=100&begin_date=" + b_date + "&end_date=" + e_date + "&kassa_id=" + kassa_id);
            this.loading = false;
            if(response.status == 201 || response.status == 200)
            {
              const data = await response.json();
              if(data.items_list.length>0){
                this.yest_day_cash += data.items_list[0].before_cash;
                this.yest_day_dollor += data.items_list[0].before_dollor;
              }
            }
          }
          catch(error){
            console.log('8')
            console.log(error);
          }
      },
      // Vaqt oraligida boshidagi obshey summani hisoblash yani tanlangan boshidagi kundi chiqaradi <==

      // sotuv bulimida sotuv summasi ==>
      async getallCassaSumm(){
        this.another_summa = 0;
        let b_data = this.Start_time + 'T00:00:35.000Z';
        let e_data = this.End_time + 'T23:59:59.000Z';
        console.log('b_data time malumotlari')
        console.log(b_data)
        console.log(e_data)

        try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonCheck/getKassaCurrentRealTegirmonPosition?begin_date=' +  b_data + '&end_date=' + e_data);
          const res_data = await res.json();
          console.log('res_data check uchun data')
          console.log(res_data)
            if(res_data[0].summ){
              this.allSumma = res_data[0].summ;
              this.totalIn = res_data[0].summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')

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

              this.another_summa = parseFloat(res_data[0].card) + parseFloat(res_data[0].humo) + 
                parseFloat(res_data[0].online) + parseFloat(res_data[0].dolg) + 
                parseFloat(res_data[0].uz_card) + parseFloat(res_data[0].perchisleniya) + 
                parseFloat(res_data[0].creadit_payed) + parseFloat(res_data[0].rasxod);
            }
            
  
            // this.qayt_naqd = res_data[0].srogi_otganlar_uchun_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
            // this.qayt_dollor = res_data[0].salary.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
            
            // this.sell = res_data[0].for_buy_tovar_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
        }
        catch{
          console.log('sotuv bulimi ')
          this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
          return false;
        }
      },
      // sotuv bulimida sotuv summasi <==

      // Rasxod bulimi summalari ==>
      async fetchRasxod(){

        this.all_summ_rasxod = 0;
        this.rasxod_cash = 0;
        this.dollor_rasxod = 0;
        
        let b_date = this.Start_time + 'T00:00:35.000Z';
        let e_date = this.End_time + 'T23:59:59.000Z';
        try{
            const response = await fetch(this.$store.state.hostname + "/TegirmonUserRasxod/getUserRasxodSumma?begin_date=" + b_date + "&end_date=" + e_date);
            if(response.status == 201 || response.status == 200)
            {
              const data = await response.json();
              console.log('rasxod data')
              console.log(data)
              this.all_summ_rasxod += data[0].all_summ;
              this.rasxod_cash += data[0].cash;
              this.dollor_rasxod += data[0].dollor;
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
            console.log('Rasxod bulimi')
            this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
            this.loading = false;
        }
      },
      // Rasxod bulimi summalari <==

      // Prixod bulimi summalari ==>
      async fetchPrixod(){
        this.all_summ_prixod = 0;
        this.prixod_cash = 0;
        this.dollor_prixod = 0;
        
        let b_date = this.Start_time + 'T00:00:35.000Z';
        let e_date = this.End_time + 'T23:59:59.000Z';
        try{
            const response = await fetch(this.$store.state.hostname + "/TegirmonUserRasxod/getUserPrixodSumma?begin_date=" + b_date + "&end_date=" + e_date);
            if(response.status == 201 || response.status == 200)
            {
              const data = await response.json();
              console.log('userPrixod')
              console.log(data)
              this.all_summ_prixod += data[0].all_summ;
              this.prixod_cash += data[0].cash;
              this.dollor_prixod += data[0].dollor;
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
            console.log('Prixod bulimi')
            this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
            this.loading = false;
        }
      },
      // Prixod bulimi summalari <==

      // Zaxiran sotib olingan mahsulot summalari <==
      async getAllInvoiceZaxiraSumm(){
        this.sotib_olingan_mahsulot_zaxiradan = 0;
        let b_data = this.Start_time + 'T00:00:35.000Z';
        let e_data = this.End_time + 'T23:59:59.000Z';
        // console.log(this.get_zakaz_product_all_list[this.get_page_savat]) 
        try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getKassaCurrentZaxiraInvoiceSumm?begin_date=' +  b_data + '&end_date=' + e_data);
          const res_data = await res.json();
          this.sotib_olingan_mahsulot_zaxiradan += res_data[0].summ;
        }
        catch{
          console.log('Invoice bulimi zaxira')
          this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
          return false;
        }
      },
      // Zaxiran sotib olingan mahsulot summalari <==

      // Poluchit productdan olib qolingan naqd pullar <==
      async getAllInvoicePoluchitSumma(){
        this.poluchit_productdan_olingan_naqd = 0;
        let b_data = this.Start_time + 'T00:00:35.000Z';
        let e_data = this.End_time + 'T23:59:59.000Z';
        // console.log(this.get_zakaz_product_all_list[this.get_page_savat]) 
        try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getKassaCurrentPoluchitSumm?begin_date=' +  b_data + '&end_date=' + e_data);
          const res_data = await res.json();
          this.poluchit_productdan_olingan_naqd += res_data[0].summ;
        }
        catch{
          console.log('5')
          this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
          return false;
        }
      },
      // Poluchit productdan olib qolingan naqd pullar <==

      // Poluchit productdan olib qolingan dollor <==
      async getAllInvoicePoluchitDollor(){
        this.poluchit_productdan_olingan_dollor = 0;
        let b_data = this.Start_time + 'T00:00:35.000Z';
        let e_data = this.End_time + 'T23:59:59.000Z';
        // console.log(this.get_zakaz_product_all_list[this.get_page_savat]) 
        try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getKassaCurrentPoluchitDollor?begin_date=' +  b_data + '&end_date=' + e_data);
          const res_data = await res.json();
          this.poluchit_productdan_olingan_dollor += res_data[0].summ;
        }
        catch{
          console.log('5')
          this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
          return false;
        }
      },
      // Poluchit productdan olib qolingan dollor <==

      // Poluchit productdan hisobga utkazilgan pullar <==
      async getAllInvoicePoluchitHisobgaSumma(){
        this.poluchit_productdan_hisobga = 0;
        let b_data = this.Start_time + 'T00:00:35.000Z';
        let e_data = this.End_time + 'T23:59:59.000Z';
        // console.log(this.get_zakaz_product_all_list[this.get_page_savat]) 
        try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getKassaCurrentPoluchitSummHisobId?begin_date=' +  b_data + '&end_date=' + e_data);
          const res_data = await res.json();
          this.poluchit_productdan_hisobga += res_data[0].summ;
        }
        catch{
          console.log('5')
          this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
          return false;
        }
      },
      // Poluchit productdan hisobga utkazilgan pullar <==

      // Srazi almashtirishdan sotib olingan mahsulot summalari <==
      async getAllInvoiceChangeSumm(){
        this.sotib_olingan_change_product_summ = 0;
        let b_data = this.Start_time + 'T00:00:35.000Z';
        let e_data = this.End_time + 'T23:59:59.000Z';
        // console.log(this.get_zakaz_product_all_list[this.get_page_savat]) 
        try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getKassaCurrentChangeInvoiceSumm?begin_date=' +  b_data + '&end_date=' + e_data);
          const res_data = await res.json();
          this.sotib_olingan_change_product_summ += res_data[0].summ
        }
        catch{
          console.log('change invoice')
          this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
          return false;
        }
      },
      // Srazi almashtirishdan sotib olingan mahsulot summalari <==


      // Asosiy kassaga pul utkazmalar summasi <==
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
              console.log(this.main_kassaga_utkaz_cash ,  'this.main_kassaga_utkaz_cash')
            }
          }
          catch(error){
            console.log('main kassaga')
            console.log(error);
          }
      },
      // Asosiy kassaga pul utkazmalar summasi <==


      // Savdo kassaga pul utkazmalar summasi <==
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
              console.log(data ,  'today prixod data')
            }
          }
          catch(error){
            console.log('8')
            console.log(error);
          }
      },
      // Savdo kassaga pul utkazmalar summasi <==




  

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
        this.sotib_olingan_mahsulot_zaxiradan = 0;
        this.sotib_olingan_change_product_summ = 0;
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
      },
      
    },
  }
  </script>
  
  <style lang="scss" >
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
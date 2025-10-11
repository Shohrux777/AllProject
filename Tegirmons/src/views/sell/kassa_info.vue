<template>
    <div  class="addProductQtyAcceptCash"
      :class="{'openShow': openBox, 'closeShow': !openBox }"
      v-on:click.self="close">
      <div class="px-0 py-2" >
        <div class="acceptBoxForTegirmon px-3 py-1 pb-4 text-center" :class="{'acceptBoxForTegirmon': openBox, 'closeBoxForTegirmon': !openBox }">
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
                    {{all_summ_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
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
                    {{all_summ_prixod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} 
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
                    {{(sotib_olingan_mahsulot_zaxiradan + sotib_olingan_change_product_summ).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                  </small>
                </div>
              </div>
              <div class="col-4 px-1">
                <div class="summ_item_ p-2 d-flex justify-content-between">
                  <small style="font-size: 13px;">
                    Покупка захира продукт
                  </small>
                  <small class="summ_title">
                    {{sotib_olingan_mahsulot_zaxiradan.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                  </small>
                </div>
              </div>
              <div class="col-4 px-1">
                <div class="summ_item_ p-2 d-flex justify-content-between">
                  <small style="font-size: 13px;">
                    Покупка алмаштириш продукт
                  </small>
                  <small class="summ_title">
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
            </div>
          </div>


          <div class="depart_title text-center border-bottom">
            <small>OPTOM SAVDO BO'LIMI</small>
          </div>
  
          <div class="mainSellSumms container-fluid">
            <div class="row">
              <div class="col-4 px-1">
                <div class="summ_item_ p-2 d-flex justify-content-between">
                  <small style="font-size: 13px;">
                    Oбщий савдо суммаси
                  </small>
                  <small class="summ_title">
                    {{order_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                  </small>
                </div>
              </div>
              <div class="col-4 px-1">
                <div class="summ_item_ p-2 d-flex justify-content-between">
                  <small style="font-size: 13px;">
                    {{$t('cash')}}
                  </small>
                  <small class="summ_title">
                    {{order_cash_str}}
                  </small>
                </div>
              </div>
              <div class="col-4 px-1">
                <div class="summ_item_ p-2 d-flex justify-content-between">
                  <small style="font-size: 13px;">
                    {{$t('dollor')}}
                  </small>
                  <small class="summ_title">
                    {{order_dollor_str}} $
                  </small>
                </div>
              </div>
              <div class="col-4 px-1">
                <div class="summ_item_ p-2 d-flex justify-content-between">
                  <small style="font-size: 13px;">
                    UzCard
                  </small>
                  <small class="summ_title">
                    {{order_uz_card_str.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                  </small>
                </div>
              </div>
              <div class="col-4 px-1">
                <div class="summ_item_ p-2 d-flex justify-content-between">
                  <small style="font-size: 13px;">
                    Humo
                  </small>
                  <small class="summ_title">
                    {{order_humo_str.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                  </small>
                </div>
              </div>
              
              <div class="col-4 px-1">
                <div class="summ_item_ p-2 d-flex justify-content-between">
                  <small style="font-size: 13px;">
                    Payme
                  </small>
                  <small class="summ_title">
                    {{order_payme_str.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                  </small>
                </div>
              </div>
              <div class="col-4 px-1">
                <div class="summ_item_ p-2 d-flex justify-content-between">
                  <small style="font-size: 13px;">
                    Click
                  </small>
                  <small class="summ_title">
                    {{order_click_str.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                  </small>
                </div>
              </div>
              <div class="col-4 px-1">
                <div class="summ_item_ p-2 d-flex justify-content-between">
                  <small style="font-size: 13px;">
                    Paynet
                  </small>
                  <small class="summ_title">
                    {{order_paynet_str.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                  </small>
                </div>
              </div>
              <div class="col-4 px-1">
                <div class="summ_item_ p-2 d-flex justify-content-between">
                  <small style="font-size: 13px;">
                    UzumPay
                  </small>
                  <small class="summ_title">
                    {{order_uzumpay_str.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                  </small>
                </div>
              </div>
              
              <div class="col-4 px-1">
                <div class="summ_item_ p-2 d-flex justify-content-between">
                  <small style="font-size: 13px;">
                    {{$t('skidka')}}
                  </small>
                  <small class="summ_title">
                    {{order_skidka_str.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
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
  
  
  
        main_kassaga_utkaz_cash: 0,
        main_kassaga_utkaz_dollor: 0,
  
        savdo_kassaga_utkaz_cash: 0,
        savdo_kassaga_utkaz_dollor: 0,
  
        yest_day_cash: 0,
        yest_day_dollor: 0,

        another_summa: 0,
        another_summa_order: 0,
  
        real_time_status: false,
        last_create_date: '',

        kassa_id: 0,

        poluchit_productdan_hisobga: 0,
        poluchit_productdan_olingan_naqd: 0,
        poluchit_productdan_olingan_dollor: 0,

        order_cash_str : 0,
        order_dollor_str : 0,
        order_summ : 0,
        order_humo_str : 0,
        order_uz_card_str : 0,
        order_click_str : 0,
        order_skidka_str : 0,
        order_payme_str : 0,
        order_paynet_str : 0,
        order_uzumpay_str : 0,
        openBox: true,

  
      }
    },
   
    // created() {
    //   this.$root.$refs.closeCashs = this;
    // },
    mounted() {
      console.log(this.summa)
    },
    computed:{
      ...mapGetters(['allOrderList', 'get_all_summa', 'get_m_categoryIdProduct', 'get_zakaz_product_all_list','get_page_savat', 'get_product_qty', 'AllSummString']),
  
    }, 
    methods: {
      ...mapActions([  'fetchCategoryIdProduct', 'fetchProductSearchByName']),
      ...mapMutations([ 'clear_order', 'input_change', 'changeSumma', 'update_zakaz_product_all_list', 'select_savat_page', 'add_savat_page', 'del_savat_page', 'updateCheckId']),
  
      async getAllSumm(kassa_data){
        this.openBox = true;
        console.log(kassa_data);
        this.kassa_id = kassa_data.id;
        let mtime = new Date().toISOString();
        this.Start_time = mtime.slice(0,10);
        this.End_time = mtime.slice(0,10);
        this.clw_cl();
        
        await this.fetchAllKassaSummaTodayInfo(kassa_data.id);
        
        await this.getallCassaSumm(kassa_data.id);
        
        await this.fetchRasxod(kassa_data.id);
        
        await this.fetchPrixod(kassa_data.id);
        
        await this.getAllInvoiceZaxiraSumm(kassa_data.id);

        await this.getAllInvoiceChangeSumm(kassa_data.id);

        await this.getAllInvoicePoluchitSumma(kassa_data.id);
        await this.getAllInvoicePoluchitDollor(kassa_data.id);
        
        await this.getAllInvoicePoluchitHisobgaSumma(kassa_data.id);

        await this.main_kassaga_utkazish(kassa_data.id);

        await this.savdo_kassaga_utkazish(kassa_data.id);

        await this.fetch_order_all_naqd(kassa_data.id);


        await this.fetch_order_all_summa_online(kassa_data.id);

      },


      async apply(){
        this.clw_cl();
        await this.next_day_cash_all_summ(this.kassa_id);
        await this.before_day_cash_all_summ(this.kassa_id);
        
        
        await this.getallCassaSumm(this.kassa_id);
        
        await this.fetchRasxod(this.kassa_id);
        
        await this.fetchPrixod(this.kassa_id);
        
        await this.getAllInvoiceZaxiraSumm(this.kassa_id);

        await this.getAllInvoiceChangeSumm(this.kassa_id);

        await this.getAllInvoicePoluchitSumma(this.kassa_id);
        await this.getAllInvoicePoluchitDollor(this.kassa_id);

        await this.getAllInvoicePoluchitHisobgaSumma(this.kassa_id);

        await this.main_kassaga_utkazish(this.kassa_id);

        await this.savdo_kassaga_utkazish(this.kassa_id);
        await this.fetch_order_all_naqd(this.kassa_id);


        await this.fetch_order_all_summa_online(this.kassa_id);

      },




      
      async fetch_order_all_naqd(kassa_id){
        this.order_summ = 0;
        this.another_summa_order = 0;
        let b_data = this.Start_time + 'T00:00:35.000Z';
        let e_data = this.End_time + 'T23:59:59.000Z';

        try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonOrderCheck/getKassaCurrentRealTegirmonOrderKassaIdNotHisob?begin_date=' +  b_data + '&end_date=' + e_data + '&kassa_id=' + kassa_id);
          const res_data = await res.json();
          console.log('res_data order check hammasi online uchun data')
          console.log(res_data)
            if(res.status == 200 || res.status == 201){
              
              this.order_cash_str = res_data[0].cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
              this.order_dollor_str = res_data[0].dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
              this.order_summ = res_data[0].summ;

              this.order_humo_str = res_data[0].humo;
              this.order_uz_card_str = res_data[0].uz_card;
              this.order_click_str = res_data[0].click;
              this.order_skidka_str = res_data[0].skidka;
              this.order_payme_str = res_data[0].payme;
              this.order_paynet_str = res_data[0].paynet;
              this.order_uzumpay_str = res_data[0].uzumpay;
              
              this.another_summa_order += res_data[0].humo + res_data[0].uz_card + res_data[0].click + 
              res_data[0].payme + res_data[0].paynet + res_data[0].uzumpay;
            }
            // this.qayt_naqd = res_data[0].srogi_otganlar_uchun_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
            // this.qayt_dollor = res_data[0].salary.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
            
            // this.sell = res_data[0].for_buy_tovar_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
        }
        catch{
          console.log('2')
          this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
          return false;
        }
      },

      async fetch_order_all_summa_online(kassa_id){
        
        let b_data = this.Start_time + 'T00:00:35.000Z';
        let e_data = this.End_time + 'T23:59:59.000Z';

        try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonOrderCheck/getKassaCurrentRealTegirmonOrderKassaId?begin_date=' +  b_data + '&end_date=' + e_data + '&kassa_id=' + kassa_id);
          const res_data = await res.json();
          console.log('res_data order check hammasi online uchun data')
          console.log(res_data)
            if(res.status == 200 || res.status == 201){
              this.order_humo_str += res_data[0].humo;
              this.order_uz_card_str += res_data[0].uz_card;
              this.order_click_str += res_data[0].click;
              this.order_skidka_str += res_data[0].skidka;
              this.order_payme_str += res_data[0].payme;
              this.order_paynet_str += res_data[0].paynet;
              this.order_uzumpay_str += res_data[0].uzumpay;

              let temp = 0;
              temp += res_data[0].humo + res_data[0].uz_card + res_data[0].click + 
              res_data[0].payme + res_data[0].paynet + res_data[0].uzumpay

              this.another_summa_order += temp;
              this.order_summ += temp;

              this.another_summa += this.another_summa_order;
            }
        }
        catch{
          console.log('2')
          this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
          return false;
        }
      },





      // Bugungi kun hamma summasi ==>
      async fetchAllKassaSummaTodayInfo(kassa_id){
        this.all_cash_summa = 0;
        this.all_dollor_summa = 0;
        this.yest_day_cash = 0;
        this.yest_day_dollor = 0;
        try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonKassaInfo/getKassaInfoLastKassaId?kassa_id=' + kassa_id);
          const res_data = await res.json();
          this.all_cash_summa = res_data.cash;
          this.all_dollor_summa = res_data.dollor;
          this.yest_day_cash = res_data.before_cash;
          this.yest_day_dollor = res_data.before_dollor;
          console.log('kassa infoni olish kerak')
          console.log(res_data)
        }
        catch{
            console.log('1')
            this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
            return false;
        }
      },
      // Bugungi kun hamma summasi <==


      // Vaqt oraligida oxirgi obshey summani hisoblash yani tanlangan oxirgi kundi chiqaradi <==
      async next_day_cash_all_summ(kassa_id){
        this.all_cash_summa = 0;
        this.all_dollor_summa = 0;
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
                this.all_cash_summa = data.items_list[0].cash;
                this.all_dollor_summa = data.items_list[0].dollor;
              }
              console.log(data ,  'next day data')
            }
          }
          catch(error){
            console.log('8')
            console.log(error);
          }
      },
      // Vaqt oraligida oxirgi obshey summani hisoblash yani tanlangan oxirgi kundi chiqaradi <==

      // Vaqt oraligida oxirgi obshey summani hisoblash yani tanlangan boshidagi bir kun oldingi kundi chiqaradi <==
      async before_day_cash_all_summ(kassa_id){
        this.yest_day_cash = 0;
        this.yest_day_dollor = 0;
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.Start_time + 'T23:59:35.000Z';
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/TegirmonKassaInfo/getPaginationByBeatWeenTwoDateKassaInfoKassaID?page=0&size=100&begin_date=" + b_date + "&end_date=" + e_date + "&kassa_id=" + kassa_id);
            this.loading = false;
            if(response.status == 201 || response.status == 200)
            {
              const data = await response.json();
              console.log(data ,  'before day ')
              if(data.items_list.length>0){
                this.yest_day_cash = data.items_list[0].before_cash;
                this.yest_day_dollor = data.items_list[0].before_dollor;
              }
            }
          }
          catch(error){
            console.log('8')
            console.log(error);
          }
      },
      // Vaqt oraligida oxirgi obshey summani hisoblash yani tanlangan boshidagi bir kun oldingi kundi chiqaradi <==




      // sotuv bulimida sotuv summasi ==>
      async getallCassaSumm(kassa_id){
        this.another_summa = 0;
        let b_data = this.Start_time + 'T00:00:35.000Z';
        let e_data = this.End_time + 'T23:59:59.000Z';
        console.log('b_data time malumotlari')
        console.log(b_data)
        console.log(e_data)
        console.log(kassa_id)

        try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonCheck/getKassaCurrentRealTegirmonPositionKassaId?begin_date=' +  b_data + '&end_date=' + e_data + '&kassa_id=' + kassa_id);
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
          console.log('2')
          this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
          return false;
        }
      },
      // sotuv bulimida sotuv summasi <==

      // Rasxod bulimi summalari ==>
      async fetchRasxod(kassa_id){

        this.all_summ_rasxod = 0;
        this.rasxod_cash = 0;
        this.dollor_rasxod = 0;
        
        let b_date = this.Start_time + 'T00:00:35.000Z';
        let e_date = this.End_time + 'T23:59:59.000Z';
        try{
            const response = await fetch(this.$store.state.hostname + "/TegirmonUserRasxod/getUserRasxodSummaKassaId?begin_date=" + b_date + "&end_date=" + e_date + "&kassa_id=" + kassa_id);
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
            console.log('3')
            this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
            this.loading = false;
        }
      },
      // Rasxod bulimi summalari <==

      // Prixod bulimi summalari ==>
      async fetchPrixod(kassa_id){
        this.all_summ_prixod = 0;
        this.prixod_cash = 0;
        this.dollor_prixod = 0;
        
        let b_date = this.Start_time + 'T00:00:35.000Z';
        let e_date = this.End_time + 'T23:59:59.000Z';
        try{
            const response = await fetch(this.$store.state.hostname + "/TegirmonUserRasxod/getUserPrixodSummaKassaId?begin_date=" + b_date + "&end_date=" + e_date + "&kassa_id=" + kassa_id);
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
            console.log('4')
            this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
            this.loading = false;
        }
      },
      // Prixod bulimi summalari <==

      // Zaxiran sotib olingan mahsulot summalari <==
      async getAllInvoiceZaxiraSumm(kassa_id){
        this.sotib_olingan_mahsulot_zaxiradan = 0;
        let b_data = this.Start_time + 'T00:00:35.000Z';
        let e_data = this.End_time + 'T23:59:59.000Z';
        // console.log(this.get_zakaz_product_all_list[this.get_page_savat]) 
        try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getKassaCurrentZaxiraInvoiceSummKassaId?begin_date=' +  b_data + '&end_date=' + e_data + '&kassa_id=' + kassa_id);
          const res_data = await res.json();
          this.sotib_olingan_mahsulot_zaxiradan += res_data[0].summ;
        }
        catch{
          console.log('5')
          this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
          return false;
        }
      },
      // Zaxiran sotib olingan mahsulot summalari <==


      // Poluchit productdan olib qolingan naqd pullar <==
      async getAllInvoicePoluchitSumma(kassa_id){
        this.poluchit_productdan_olingan_naqd = 0;
        let b_data = this.Start_time + 'T00:00:35.000Z';
        let e_data = this.End_time + 'T23:59:59.000Z';
        // console.log(this.get_zakaz_product_all_list[this.get_page_savat]) 
        try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getKassaCurrentPoluchitSummKassaId?begin_date=' +  b_data + '&end_date=' + e_data + '&kassa_id=' + kassa_id);
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


      // Poluchit productdan olib qolingan dollor  <==
      async getAllInvoicePoluchitDollor(kassa_id){
        this.poluchit_productdan_olingan_dollor = 0;
        let b_data = this.Start_time + 'T00:00:35.000Z';
        let e_data = this.End_time + 'T23:59:59.000Z';
        // console.log(this.get_zakaz_product_all_list[this.get_page_savat]) 
        try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getKassaCurrentPoluchitDollorKassaId?begin_date=' +  b_data + '&end_date=' + e_data + '&kassa_id=' + kassa_id);
          const res_data = await res.json();
          this.poluchit_productdan_olingan_dollor += res_data[0].summ;
        }
        catch{
          console.log('12')
          this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
          return false;
        }
      },
      // Poluchit productdan olib qolingan dollor  <==


      // Poluchit productdan hisobga utkazilgan pullar <==
      async getAllInvoicePoluchitHisobgaSumma(kassa_id){
        this.poluchit_productdan_hisobga = 0;
        let b_data = this.Start_time + 'T00:00:35.000Z';
        let e_data = this.End_time + 'T23:59:59.000Z';
        // console.log(this.get_zakaz_product_all_list[this.get_page_savat]) 
        try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getKassaCurrentPoluchitSummHisobIdKassaId?begin_date=' +  b_data + '&end_date=' + e_data + '&kassa_id=' + kassa_id);
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
      async getAllInvoiceChangeSumm(kassa_id){
        this.sotib_olingan_change_product_summ = 0;
        let b_data = this.Start_time + 'T00:00:35.000Z';
        let e_data = this.End_time + 'T23:59:59.000Z';
        // console.log(this.get_zakaz_product_all_list[this.get_page_savat]) 
        try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getKassaCurrentChangeInvoiceSummKassaId?begin_date=' +  b_data + '&end_date=' + e_data + '&kassa_id=' + kassa_id);
          const res_data = await res.json();
          this.sotib_olingan_change_product_summ += res_data[0].summ
        }
        catch{
          console.log('6')
          this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
          return false;
        }
      },
      // Srazi almashtirishdan sotib olingan mahsulot summalari <==


      // Asosiy kassaga pul utkazmalar summasi <==
      async main_kassaga_utkazish(kassa_id){
          this.main_kassaga_utkaz_cash = 0;
          this.main_kassaga_utkaz_dollor = 0;
          let b_date  = this.Start_time + 'T00:00:35.000Z';
          let e_date  = this.End_time + 'T23:59:35.000Z';
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/TegirmonMainKassaRasxod/getMainKassaSavdoKassadanOtganSummaKassaId?begin_date=" + b_date + "&end_date=" + e_date + "&kassa_id=" + kassa_id);
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
            console.log('7')
            console.log(error);
          }
      },
      // Asosiy kassaga pul utkazmalar summasi <==


      // Savdo kassaga pul utkazmalar summasi <==
      async savdo_kassaga_utkazish(kassa_id){
        this.savdo_kassaga_utkaz_cash = 0;
        this.savdo_kassaga_utkaz_dollor = 0;
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.End_time + 'T23:59:35.000Z';
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/TegirmonMainKassaRasxod/getMainKassadanSavdoKassagaOtganSummaKassaId?begin_date=" + b_date + "&end_date=" + e_date + "&kassa_id=" + kassa_id);
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
        this.openBox = false;
        setTimeout(() => {
          this.$emit('close')
          this.openBox = true;
        }, 500);
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
  
<style lang="scss" scoped>
.addProductQtyAcceptCash {
  position: fixed;
  display: flex;
  justify-content: center;
  align-items: center;
  top: 0;
  left: 0;
  width: 100%;
  height: 100vh;
  background: rgba(19, 19, 49, 0.65);
  backdrop-filter: blur(10px); /* 🔹 Orqa fonni blur qiladi */
  -webkit-backdrop-filter: blur(10px);
  z-index: 1111;
}
.openShow{
  animation: fadeIn 0.5s ease forwards;
}
.closeShow{
  animation: fadeOut 0.5s ease forwards;
}


.acceptBoxForTegirmon {
  width: 70vw;
  max-height: 100vh;
  overflow-y: auto;
  background: snow;
  box-shadow: 3px 2px 10px rgba(0, 0, 0, 0.25);
  border-radius: 10px;
  position: relative;
  transform-origin: bottom right;
  animation: macOpen 0.8s cubic-bezier(0.22, 1, 0.36, 1) forwards;
}
.closeBoxForTegirmon{
  width: 70vw;
  max-height: 100vh;
  overflow-y: auto;
  background: snow;
  box-shadow: 3px 2px 10px rgba(0, 0, 0, 0.25);
  border-radius: 10px;
  position: relative;
  transform-origin: bottom right;
  animation: macClose 0.8s cubic-bezier(0.22, 1, 0.36, 1) forwards;
}

/* 🔹 MacOS uslubidagi chiqish (pastki o‘ngdan markazga kattarish) */
@keyframes macOpen {
  0% {
    transform: scale(0.2) translate(40%, 40%);
    opacity: 0;
  }
  
  100% {
    transform: scale(1) translate(0, 0);
    opacity: 1;
  }
}

/* 🔹 Yopilish effekti uchun (agar ishlatmoqchi bo‘lsang) */
@keyframes macClose {
  0% {
    transform: scale(1) translate(0, 0);
    opacity: 1;
  }
  100% {
    transform: scale(0.2) translate(40%, 40%);
    opacity: 0;
  }
}

@keyframes fadeIn {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}
@keyframes fadeOut {
  from {
    opacity: 1;
  }
  to {
    opacity: 0;
  }
}

/* Qo‘shimcha bezaklar (seniki o‘z holida qoldirildi) */
.price_all_item .borderSolder {
  background-image: linear-gradient(
    65.9deg,
    rgba(85, 228, 224, 1) 5.5%,
    rgba(75, 68, 224, 0.74) 54.2%,
    rgba(64, 198, 238, 1) 55.2%,
    rgba(177, 36, 224, 1) 98.4%
  );

  span {
    color: #26254a;
    font-size: 21px;
    font-weight: 450;
  }
  p {
    color: #ffffff;
    font-weight: bold;
    font-size: 23px;
    margin: 0;
    padding: 0;
  }
}

.depart_title small {
  font-style: italic;
  color: #0600b3;
  font-weight: bold;
}

.summ_title {
  font-size: 15px;
  font-style: italic;
  color: #3c3669;
  font-weight: bold;
}

.summ_item_ {
  border-bottom: 1px dashed rgb(110, 110, 110);
}
</style>

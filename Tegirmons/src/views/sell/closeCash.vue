<template>
  <div class="addProductQtyAcceptCash"  v-on:click.self="close">
    <div class="p-4" style="height: 710px;">
      <div class="acceptBoxForTegirmon px-3 py-2 pb-4 text-center">
        <div class="depart_title text-center border-bottom">
          <small>YAKUNIY HISOBLAR</small>
        </div>
        <div class="allSummDollor container-fluid border-bottom pb-2">
          <div class="row">
            <div class="col-4 px-1">
              <div class="price_all_item ">
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
                    <p>{{ all_cash_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} сум</p>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="price_all_item  text-left ">
                <div class="qty borderSolder rounded py-2">
                  <span class="ml-3">{{$t('dollor')}}</span>
                  <div class="text-right px-3 mt-1">
                    <p>{{ all_dollor_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} $</p>
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
                  {{$t('dolg_summ')}}
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

        <div class="depart_title text-center border-bottom border-top mt-3 mb-2">
          <small>RASXOD PRIXOD BO'LIMI</small>
        </div>

        <div class="mainSellSumms container-fluid">
          <div class="row">
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('rasxod')}}
                </small>
                <small class="summ_title">
                  {{rasxodInString}}
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Наличные
                </small>
                <small class="summ_title">
                  {{rasxod_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Доллор
                </small>
                <small class="summ_title">
                  {{dollor_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} $
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('pul_olish')}}
                </small>
                <small class="summ_title">
                  {{chiqarilPulOlishString}} 
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Наличные
                </small>
                <small class="summ_title">
                  {{prixod_cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
            </div>
            <div class="col-4 px-1">
              <div class="summ_item_ p-2 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Доллор
                </small>
                <small class="summ_title">
                  {{dollor_prixod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} $
                </small>
              </div>
            </div>
          </div>
        </div>
        
        <div class="depart_title text-center border-bottom border-top mt-3 mb-2">
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

        <div style="margin-top:20px;" class="d-flex justify-content-end">
          <mdb-btn color="danger" style="font-size:10px; padding: 6px 0;"  @click="close" class="m-0  mt-1 px-3 ml-1" > {{$t('cancel')}}</mdb-btn>
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
export default {
  components: {
    mdbIcon,
    mdbBtn,mdbInput,
  },
  data() {
    return {
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
      this.all_total_summa = 0;
      this.all_cash_summa = 0;
      this.all_dollor_summa = 0;
      let mtime = new Date().toISOString();
      let b_data = mtime.slice(0,10) + 'T00:00:35.000Z'
      let e_data = mtime.slice(0,10) + 'T23:59:59.000Z'
      // console.log(this.get_zakaz_product_all_list[this.get_page_savat]) 
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

          await this.fetchRasxod();
          await this.getAllInvoiceSumm();
          await this.getAllInvoiceZaxiraSumm();
          await this.getAllInvoiceChangeSumm();
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
      let mtime = new Date().toISOString();
      let b_data = mtime.slice(0,10) + 'T00:00:35.000Z'
      let e_data = mtime.slice(0,10) + 'T23:59:59.000Z'
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
      let mtime = new Date().toISOString();
      let b_data = mtime.slice(0,10) + 'T00:00:35.000Z'
      let e_data = mtime.slice(0,10) + 'T23:59:59.000Z'
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
      let mtime = new Date().toISOString();
      let b_data = mtime.slice(0,10) + 'T00:00:35.000Z'
      let e_data = mtime.slice(0,10) + 'T23:59:59.000Z'
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
      let mtime = new Date().toISOString();
      let b_date = mtime.slice(0,10) + 'T00:00:35.000Z'
      let e_date = mtime.slice(0,10) + 'T23:59:59.000Z'
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
    close(){
      this.$emit('close')
    },
    
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
    height: 90%;
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
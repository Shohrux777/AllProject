<template>
  <div class="check" style="background:#fff; z-index: 1000;">
    <div  class="ckeck_main_pharmacy " style="width:260px !important;">
      <div  style="text-align:center; margin: 10px 0;">
        <h6 class="paymentCheckPrintCheck_main" style="font-weight: bold; font-size:11px;">Bilaz savdo</h6>
      </div>
      <div class="infoHeader">
        <table style="width:100%;">
          <tbody>
            <tr>
              <th class="paymentCheckPrintCheck_main" style="font-weight: bold; font-size:8.5px;">Чек №</th>
              <td class="paymentCheckPrintCheck_main text-right" style="font-weight: bold; font-size:8.5px;">{{CheckId}}</td>
            </tr>
            <tr style="padding-top:10px;">
              <th class="paymentCheckPrintCheck_main" style="font-weight: bold; font-size:8.5px;">Кассир</th>
              <td class="paymentCheckPrintCheck_main text-right" style="font-weight: bold; font-size:8.5px;">{{kassir}}</td>
            </tr>
            <tr style="padding-top:10px;" v-if="get_skidka_dolg.client_name != ''">
              <th class="paymentCheckPrintCheck_main" style="font-weight: bold; font-size:8.5px;">Клиент</th>
              <td class="paymentCheckPrintCheck_main text-right" style="font-weight: bold; font-size:8.5px;">{{get_skidka_dolg.client_name}}</td>
            </tr>
            <tr style="padding-top:10px;"> 
              <th class="paymentCheckPrintCheck_main" style="font-weight: bold; font-size:8.5px; padding-right: 35px;">Напечатан</th>
              <td class="paymentCheckPrintCheck_main text-right" style="font-weight: bold; font-size:8.5px;">{{dateTime}}</td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="infoList">
        <table>
          <thead>
              <tr style="border-bottom: 1px dashed #000;">
                <th  style="font-weight: bold; font-size:8px !important; width: 140px;">{{$t('name')}}</th>
                <th  style="font-weight: bold; font-size:8px !important; width: 55px; text-align: center">{{$t('soni')}}</th>
                <th  style="font-weight: bold; font-size:8px !important; width: 80px; text-align: right">{{$t('price')}}</th>
                <th  style="font-weight: bold; font-size:8px !important; width: 100px; text-align: right">{{$t('total')}}</th>
              </tr>
          </thead>
          <tbody>
  
            <tr v-for="(item,i) in get_zakaz_product_all_list[get_page_savat]" :key="i" style="border-bottom: 1px dashed #000;">
              <td style="font-weight:bold; font-size:8px;">{{item.name}}</td>
              <td v-if="item.contains_number_in_pack>1" style="text-align: center;font-weight:bold; font-size:8px;">{{item.qty + ' / ' + item.qty_in_pack}}</td>
              <td v-else style="text-align: center; font-weight:bold; font-size:8px;">{{item.qty}}</td>
              <td style="text-align: right; font-weight:bold; font-size:8px;">{{item.real_sum}}</td>
              <td style="text-align: right;font-weight:bold; font-size:8px;">{{item.summ}}</td>
            </tr>
          </tbody>
        </table>
      </div>

      <div class="totalInfo" style="border-top: 1px dashed black;">
        <h6 style="margin:0; padding:0; font-weight:bold; font-size:8.5px;">{{$t('total')}}</h6>
        <h5 style="margin:0; padding:0; font-weight:bold; font-size:8.5px;">{{get_all_summa[get_page_savat]}} <span style="font-size:8.5px;">Сум</span></h5>
      </div>
      <div class="totalInfo" style="border-top: 1px dashed black;" v-if="get_skidka_dolg.skidka">
        <h6 style="margin:0; padding:0; font-weight:bold; font-size:8.5px;">{{$t('skidka')}}</h6>
        <h5 style="margin:0; padding:0; font-weight:bold; font-size:8.5px;">{{get_skidka_dolg.skidka}} <span style="font-size:8.5px;">Сум</span></h5>
      </div>
      <div class="totalInfo" style="border-top: 1px dashed black;" v-if="get_skidka_dolg.dolg">
        <h6 style="margin:0; padding:0; font-weight:bold; font-size:8.5px;">{{$t('creditd_summ')}}</h6>
        <h5 style="margin:0; padding:0; font-weight:bold; font-size:8.5px;">{{get_skidka_dolg.dolg}} <span style="font-size:8.5px;">Сум</span></h5>
      </div>

      <div class="totalInfo" style="border-top: 1px dashed black;">
        <h6 style="margin:0; padding:0; font-weight:bold; font-size:8.5px;">К оплате</h6>
        <h4 style="margin:0; padding:0; font-weight:bold; font-size:8.5px;" >{{parseInt(parseFloat(get_all_summa[get_page_savat])-parseFloat(get_skidka_dolg.skidka)-parseFloat(get_skidka_dolg.dolg))}} <span style="font-size:10.5px;">Сум</span></h4>
      </div>

      <div class="d-flex justify-content-center" style="border-top: 1px dashed black;">
        <h6 class="mt-3 text-center" style="margin:0; padding:0; font-weight:bold; font-size:8.5px !important;">Проданный товар обмену и возврату не подлежит.
Спасибо за покупку!!!</h6>
      </div>

      <div style="margin-top:15px; border:1px solid black;"></div>
     
    </div>
  </div>
</template>

<script>
import {mapGetters, mapMutations} from 'vuex'
export default {
  data(){
    return{
      comp_name: localStorage.CompName,
      kassir : localStorage.fio,
      dateTime: new Date().toLocaleDateString(),
      time: new Date().toDateString,
    }
  },
  mounted(){
    window.print();
    // window.onload = function() { window.print(); window.close();}
    this.$emit('close')
    this.clear_order();
  },
  created() {
    this.$root.$refs.check = this;
  },
  computed: mapGetters(['get_zakaz_product_all_list','get_page_savat', 'get_all_summa','get_page_savat', 'CheckId','get_skidka_dolg']),
  methods: {
   ...mapMutations(['clear_order']),
  }
}
</script>

<style lang="scss">
@font-face{
  font-family: "Ubuntu";
  src: url("../../font/Ubuntu/Ubuntu-Bold.ttf")
}
.check {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  background-color: #fff;
  display: flex;
  justify-content: center;

  .ckeck_main_pharmacy{
    width: 260px !important; // 58 lik printerga
    // width: 400px;  80 mm lik printerga
    

    .infoHeader{
      border-bottom: 1px dashed black;
      padding: 0 5px 5px 0px; 
    }
    .infoList{
      padding: 10px 5px 5px 0px; 
    }
    .totalInfo{
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 5px 5px 5px 0px; 

    }
  }
}
.paymentCheckPrintCheck_main{
  font-weight: bold;
  font-family: 'Ubuntu';
}
span, th, td, h4, h5, h6{
 font-weight: bold;
 font-family: 'Ubuntu';
}
.ckeck_main_pharmacy span, th, td, h4, h5, h6{
 font-weight: bold;
 font-family: 'Ubuntu';
}
</style>
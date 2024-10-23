<template>
  <div class="checkPrint paymentCheckPrintCheck" style="z-index: 1000;">
    <div  id="printForm"  class="ckeck_main_sell  border" style="width:96%; height: 100%;">
      <div  style="text-align:center; margin: 10px 0; padding: 5px 7px; font-size:12px;">
        <h6 style="font-size:13.5px; font-weight:bold;" class="paymentCheckPrintCheck">ООО «OQQO'RG'ON TEGIRMON» 
          Таш. обл., Аккурганский р-н, Дустлик КФЙ</h6>
      </div>
      <div class="infoHeader" style="width:100%;">
        <div style="margin: 5px 0px;">
          <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
            <span style="font-size:12px; font-weight:bold;" class="paymentCheckPrintCheck">Кассир </span>
            <span style="font-size: 12.5px; font-weight:bold;" class="paymentCheckPrintCheck" v-if="check_get_invoice.auth != null">{{check_get_invoice.auth.user.fio}}</span>
            <span style="font-size: 12.5px; font-weight:bold;" class="paymentCheckPrintCheck" v-else>{{kassir}}</span>
          </div>
        </div>
        <div style="margin: 5px 0px;">
          <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
            <span style="font-size:12px; font-weight:bold;" class="paymentCheckPrintCheck">Напечатан </span>
            <span style="font-size: 12.5px; font-weight:bold;" class="paymentCheckPrintCheck">{{ check_get_invoice.created_date_time.slice(8,10) + '-' + check_get_invoice.created_date_time.slice(5,7) + '-' + check_get_invoice.created_date_time.slice(0,4)}} <span class="ml-1">{{check_get_invoice.created_date_time.slice(11,16)}}</span></span>
          </div>
        </div>
      </div>
      <div class="infoHeader" style="width:100%;">
        <div style="margin: 5px 0px;" v-show="check_get_invoice.check_number">
          <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
            <span style="font-size:12px; font-weight:bold;" class="paymentCheckPrintCheck">{{$t('check_number')}} </span>
            <span style="font-size: 12.5px; font-weight:bold;" class="paymentCheckPrintCheck">{{check_get_invoice.check_number}} </span>
          </div>
        </div>
        <div style="margin: 5px 0px;">
          <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
            <span style="font-size:12px; font-weight:bold;" class="paymentCheckPrintCheck">{{$t('client')}} </span>
            <span style="font-size: 12.5px; font-weight:bold;" class="paymentCheckPrintCheck">{{check_get_invoice.user_name}} </span>
          </div>
        </div>
        <div style="margin: 5px 0px;">
          <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
            <span style="font-size:12px; font-weight:bold;" class="paymentCheckPrintCheck">{{$t('product')}}</span>
            <span style="font-size: 12.5px; font-weight:bold;" class="paymentCheckPrintCheck">{{check_get_invoice.product.name}} </span>
          </div>
        </div>
        <div style="margin: 5px 0px;">
          <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
            <span style="font-size:12px; font-weight:bold;" class="paymentCheckPrintCheck">{{$t('qty')}} </span>
            <span style="font-size: 12.5px; font-weight:bold;" class="paymentCheckPrintCheck">{{check_get_invoice.qty_real}}  </span>
          </div>
        </div>
      </div>
      <div class="infoList">
        <div  style="text-align:center; margin: 0px 0; padding: 5px 7px; font-size:12.5px;">
          <h6 style="font-size:13.5px; font-weight:bold;" class="paymentCheckPrintCheck">{{$t('ostatka')}}</h6>
        </div>
        <table style=" width:100%;">
          <thead>
              <tr style="border-bottom: 0.5px solid #CED4DA;">
                <th class="paymentCheckPrintCheck" style="font-weight: bold; font-size:12px;  text-align: left">{{$t('product')}}</th>
                <th class="paymentCheckPrintCheck" style="font-weight: bold; font-size:12px; text-align: right">{{$t('qty')}}</th>
              </tr>
          </thead>
          <tbody>
            <tr v-if="check_get_invoice.credit_sum>0" style="border-bottom: 0.5px solid #CED4DA;">
              <td class="paymentCheckPrintCheck" style="font-weight:bold; text-align: left; font-size:12px;">{{check_get_invoice.product.name}}</td>
              <td class="paymentCheckPrintCheck" style="font-weight:bold; text-align: right; font-size:12px;">{{check_get_invoice.credit_sum.toFixed(1)}} кг</td>
            </tr>
            <tr v-else v-for="(item,i) in check_get_ostatka" :key="i" style="border-bottom: 0.5px solid #CED4DA;">
              <td class="paymentCheckPrintCheck" style="font-weight:bold; text-align: left; font-size:12px;">{{item.product.name}}</td>
              <td class="paymentCheckPrintCheck" style="font-weight:bold; text-align: right; font-size:12px;">{{item.qty.toFixed(1)}} {{item.product.unitMeasurment.name}}</td>
            </tr>
          </tbody>
        </table>
      </div>
      
      <div style="border-bottom: 1px dashed black;">
        <!-- ******************************************************************************************** -->
      </div>

      <div style="margin: 10px 10px 0px 10px; display:flex; justify-content:center;">
          <span style="font-size: 13px; font-weight:bold;">****************************************** </span>
      </div>
      <div style="margin: 0px 10px; display:flex; justify-content:center;">
        <span style="font-size: 13px; font-weight:bold;" class="paymentCheckPrintCheck">Иш вақти:  </span>
      </div>
      <div style="margin: 5px 10px; display:flex; justify-content:center;">
        <span style="font-size: 13px; font-weight:bold;" class="paymentCheckPrintCheck"> 07:00 - 19:00</span>
      </div>
      <div style="margin: 0px 10px; display:flex; justify-content:center;">
          <span style="font-size: 13px; font-weight:bold;" class="paymentCheckPrintCheck">Контакты:  </span>
      </div>
      <div style="margin: 5px 10px; display:flex; justify-content:center;">
          <span style="font-size: 13px; font-weight:bold;" class="paymentCheckPrintCheck">+998 97 999-9945 </span>
      </div>
      <div style="margin: 5px 10px; display:flex; justify-content:center;">
        <span style="font-size: 13px; font-weight:bold;" class="paymentCheckPrintCheck">+998 90 979-6666 </span>
      </div>
      <div style="margin: 5px 10px 0px 10px; display:flex; justify-content:center;">
          <span style="font-size: 13px; font-weight:bold;">****************************************** </span>
      </div>
      
      <div class="d-flex justify-content-center mt-2">
          <qr-code :text="value" :size="size" error-level="L"></qr-code>
      </div>
      <div style="display:flex; justify-content: center; padding: 0; padding-top: 8px; margin-bottom: 0px;">
        <span style="font-size: 13px;" class="paymentCheckPrintCheck"> 
            <span style="font-size: 12px;" class="paymentCheckPrintCheck mr-2">Ид:</span>
            {{check_get_invoice.client.id}}
          </span>
          <span style="font-size: 13px; margin-left: 15px;" class="paymentCheckPrintCheck"> 
            <span style="font-size: 12px;" class="paymentCheckPrintCheck mr-2">Парол:</span>
            {{check_get_invoice.client.car_number}}
          </span>
      </div>
      <div style="display:flex; justify-content: center; padding: 0; padding-top: 0px; margin-bottom: 1px;">
      <span style="font-size: 12px;" class="paymentCheckPrintCheck text-center"> 
        для получения остатки через телеграм
      </span>
     </div>
     <div style="display:flex; justify-content: center; padding: 0; padding-top: 0px; margin-bottom: 4px;">
      <span style="font-size: 12px;" class="paymentCheckPrintCheck"> 
        @Oqqorgon_tegirmon_zahira_bot
      </span>
     </div>

      <div style="margin-top:15px; border:1px solid black;"></div>
     
    </div>
  </div>
</template>

<script>
// import Printjs from "print-js";
import {mapGetters, mapMutations} from 'vuex'
export default {
  data(){
    return{
      comp_name: localStorage.CompName,
      kassir : localStorage.user_name,
      dateTime: new Date().toLocaleDateString(),
      time: new Date().toLocaleTimeString(),
      value: 't.me/Oqqorgon_tegirmon_zahira_bot',
      size: 100,
    }
  },
  mounted(){
    window.print();
    this.$emit('close');
    // setTimeout(async() => {
    //     window.print();
    //     this.$emit('close');
    // }, 100);
    this.clear_order();
      // Printjs({
      //   printable: "printForm", //Id to print content 
      //   type: "html",
      //   targetStyles: ['*'],
      //   ignoreElements:['no-print','bc','gb']
      // });
    // window.onload = function() { window.print(); window.close();}
    
  },
  created() {
    this.$root.$refs.check = this;
  },
  computed: mapGetters(['get_zakaz_product_all_list','get_page_savat', 'get_all_summa','check_get_invoice', 'check_get_ostatka']),
  methods: {
   ...mapMutations(['clear_order']),
  }
}
</script>

<style lang="scss" >
@font-face{
  font-family: "Ubuntu";
  src: url("../../font/Ubuntu/Ubuntu-Bold.ttf")
}
.paymentCheckPrintCheck{
  font-family: 'Ubuntu'; 
  font-weight: bold; 
}
span, h6 {
  font-family: 'Ubuntu';
  font-weight: bold; 

}
td, th {
  font-family: 'Ubuntu'; 
  font-weight: bold; 
}
.checkPrint {
  position: fixed;
  top: 0;
  left: 0;
  width: 96%;
  background-color: #fff;
  display: flex;
  justify-content: center;

  .ckeck_main_sell{
    width: 100%;
    height: 100%;

    .infoHeader{
      border-bottom: 1px dashed black;
      padding: 0px 0px 10px 0px; 
    }
    .infoList{
      padding: 10px 0px 15px 0px; 
    }
    .totalInfo{
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 10px 0px 20px 0px; 

    }
  }
}
</style>
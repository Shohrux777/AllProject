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
            <span style="font-size: 12.5px; font-weight:bold;" class="paymentCheckPrintCheck">{{kassir}}</span>
          </div>
        </div>
        <div style="margin: 5px 0px;">
          <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
            <span style="font-size:12px; font-weight:bold;" class="paymentCheckPrintCheck">Напечатан </span>
            <span style="font-size: 12.5px; font-weight:bold;" class="paymentCheckPrintCheck">{{dateTime}} <span class="ml-1">{{time}}</span></span>
          </div>
        </div>
      </div>
      <div class="infoList">
        <table style=" width:100%;">
          <thead>
              <tr style="border-bottom: 0.5px solid #CED4DA;">
                <th class="paymentCheckPrintCheck" style="font-weight: bold; font-size:12px; ">{{$t('name')}}</th>
                <th class="paymentCheckPrintCheck" style="font-weight: bold; font-size:12px;  text-align: left">{{$t('soni')}}</th>
                <th class="paymentCheckPrintCheck" style="font-weight: bold; font-size:12px; text-align: left">{{$t('price')}}</th>
                <th class="paymentCheckPrintCheck" style="font-weight: bold; font-size:12px; text-align: right">{{$t('total')}}</th>
              </tr>
          </thead>
          <tbody>
            <tr v-for="(item,i) in get_zakaz_product_all_list[get_page_savat]" :key="i" style="border-bottom: 0.5px solid #CED4DA;">
              <td class="paymentCheckPrintCheck" style="font-weight:bold; text-align: left; font-size:12px;">{{item.name}}</td>
              <td class="paymentCheckPrintCheck" style="font-weight:bold; text-align: left; font-size:12px;">{{item.qty}}</td>
              <td class="paymentCheckPrintCheck" style="font-weight:bold; text-align: left; font-size:12px;">{{item.real_sum}}</td>
              <td class="paymentCheckPrintCheck" style="font-weight:bold; text-align: right; font-size:12px;">{{item.summ}}</td>
            </tr>
          </tbody>
        </table>
      </div>

      <div style="border-bottom: 1px dashed black;">
        <!-- ******************************************************************************************** -->
      </div>

      <div style="margin: 5px 0px;">
        <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
          <span style="font-size:12px; font-weight:bold;" class="paymentCheckPrintCheck">{{$t('total')}} </span>
          <span style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">{{get_all_summa[get_page_savat]}}</span>
        </div>
      </div>
      <div style="border-bottom: 1px dashed black;">
        <!-- ******************************************************************************************** -->
      </div>
      <div style="margin: 5px 0px;">
        <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
          <span style="font-size: 13px; font-weight:bold;" class="paymentCheckPrintCheck">Итого к оплате: </span>
          <span style="font-size: 13px; font-weight:bold;" class="paymentCheckPrintCheck">{{get_all_summa[get_page_savat]}}</span>
        </div>
      </div>
      <div style="border-bottom: 1px dashed black;">
        <!-- ******************************************************************************************** -->
      </div>

      <div style="margin: 10px 10px 0px 10px; display:flex; justify-content:center;">
          <span style="font-size: 13px; font-weight:bold;">****************************************** </span>
      </div>
      <div style="margin: 0px 10px; display:flex; justify-content:center;">
          <span style="font-size: 13px; font-weight:bold;" class="paymentCheckPrintCheck">Контакты:  </span>
      </div>
      <div style="margin: 5px 10px; display:flex; justify-content:center;">
          <span style="font-size: 13px; font-weight:bold;" class="paymentCheckPrintCheck">+998 97 9999945 </span>
      </div>
     
      <div style="margin: 5px 10px 0px 10px; display:flex; justify-content:center;">
          <span style="font-size: 13px; font-weight:bold;">****************************************** </span>
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
    }
  },
  mounted(){
    window.print();
    this.$emit('close');
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
  computed: mapGetters(['get_zakaz_product_all_list','get_page_savat', 'get_all_summa','get_page_savat', 'CheckId']),
  methods: {
   ...mapMutations(['clear_order']),
  }
}
</script>

<style lang="scss" scoped>
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
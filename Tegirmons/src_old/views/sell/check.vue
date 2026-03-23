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
            <strong style="font-size:12px; font-weight:bold;" class="paymentCheckPrintCheck">Кассир </strong>
            <strong style="font-size: 12.5px; font-weight:bold;" class="paymentCheckPrintCheck">{{kassir}}</strong>
          </div>
        </div>
        <div style="margin: 5px 0px;">
          <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
            <strong style="font-size:12px; font-weight:bold;" class="paymentCheckPrintCheck">Напечатан </strong>
            <strong style="font-size: 12.5px; font-weight:bold;" class="paymentCheckPrintCheck">{{dateTime}} <strong class="ml-1">{{time}}</strong></strong>
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
              <td class="paymentCheckPrintCheck" style="font-weight:bold; text-align: left; font-size:12px;">{{item.real_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</td>
              <td class="paymentCheckPrintCheck" style="font-weight:bold; text-align: right; font-size:12px;">{{item.summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</td>
            </tr>
          </tbody>
        </table>
      </div>

      <div style="border-bottom: 1px dashed black;">
        <!-- ******************************************************************************************** -->
      </div>

      <div style="margin: 5px 0px;">
        <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
          <strong style="font-size:12px; font-weight:bold;" class="paymentCheckPrintCheck">{{$t('total')}} </strong>
          <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">{{(get_all_summa[get_page_savat] || 0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</strong>
        </div>
      </div>
      <div style="border-bottom: 1px dashed black;">
        <!-- ******************************************************************************************** -->
      </div>

      <div v-if="get_check_type.cash" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          {{$t('cash')}}
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{(get_check_type.cash + get_check_type.srogi_otganlar_uchun_rasxod).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="get_check_type.real_sum" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          {{$t('dollor')}}
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{(get_check_type.real_sum + get_check_type.salary).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>

      <div v-if="get_check_type.card" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          UzCard
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{get_check_type.card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="get_check_type.humo" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          Humo
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{get_check_type.humo.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="get_check_type.uz_card" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          Payme
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{get_check_type.uz_card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="get_check_type.perchisleniya" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          Click
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{get_check_type.perchisleniya.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="get_check_type.creadit_payed" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          Paynet
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{get_check_type.creadit_payed.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="get_check_type.rasxod" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          UzumPay
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{get_check_type.rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="get_check_type.online" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          Перечисления
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{get_check_type.online.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="get_check_type.dolg" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          Пластикка ўтказма
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{get_check_type.dolg.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="get_check_type.profit_summ" class=" text-warning d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          {{$t('skidka')}}
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck text-warning">
          {{get_check_type.profit_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>

      <div v-if="get_check_type.srogi_otganlar_uchun_rasxod" class=" text-danger d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;" >
          Қайтим нақт
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck  text-danger">
          {{get_check_type.srogi_otganlar_uchun_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="get_check_type.salary" class=" text-danger d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          Қайтим доллор
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck text-danger text-danger">
          {{get_check_type.salary.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>

      <div style="border-bottom: 1px dashed black;">
        <!-- ******************************************************************************************** -->
      </div>

      <div style="margin: 5px 0px;">
        <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
          <strong style="font-size: 13px; font-weight:bold;" class="paymentCheckPrintCheck">Итого к оплате: </strong>
          <strong style="font-size: 13px; font-weight:bold;" class="paymentCheckPrintCheck">{{(get_all_summa[get_page_savat] || 0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</strong>
        </div>
      </div>
      <div style="border-bottom: 1px dashed black;">
        <!-- ******************************************************************************************** -->
      </div>

      <div style="margin: 10px 10px 0px 10px; display:flex; justify-content:center;">
          <strong style="font-size: 13px; font-weight:bold;">****************************************** </strong>
      </div>
      <div style="margin: 0px 10px; display:flex; justify-content:center;">
          <strong style="font-size: 13px; font-weight:bold;" class="paymentCheckPrintCheck">Контакты:  </strong>
      </div>
      <div style="margin: 5px 10px; display:flex; justify-content:center;">
          <strong style="font-size: 13px; font-weight:bold;" class="paymentCheckPrintCheck">+998 97 9999945 </strong>
      </div>
     
      <div style="margin: 5px 10px 0px 10px; display:flex; justify-content:center;">
          <strong style="font-size: 13px; font-weight:bold;">****************************************** </strong>
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
  computed: mapGetters(['get_zakaz_product_all_list','get_page_savat', 'get_all_summa','get_page_savat', 'CheckId', 'get_check_type']),
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
strong, h6 {
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
<template>
  <div class="checkPrint paymentCheckPrintCheck" style="z-index: 1000;" id="print_load">
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
            <tr v-for="(item,i) in check_data.payments" :key="i" style="border-bottom: 0.5px solid #CED4DA;">
              <td class="paymentCheckPrintCheck" style="font-weight:bold; text-align: left; font-size:12px;">{{item.product.name}}</td>
              <td class="paymentCheckPrintCheck" style="font-weight:bold; text-align: left; font-size:12px;">{{item.qty}}</td>
              <td class="paymentCheckPrintCheck" style="font-weight:bold; text-align: left; font-size:12px;">{{item.product.price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</td>
              <td class="paymentCheckPrintCheck" style="font-weight:bold; text-align: right; font-size:12px;">{{item.summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</td>
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
          <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">{{(check_data.summ || 0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</strong>
        </div>
      </div>

      <div style="border-bottom: 1px dashed black;">
        <!-- ******************************************************************************************** -->
      </div>

      <div v-if="check_data.cash" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          {{$t('cash')}}
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{(check_data.cash + check_data.srogi_otganlar_uchun_rasxod).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="check_data.real_sum" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          {{$t('dollor')}}
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{(check_data.real_sum + check_data.salary).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>

      

      <div v-if="check_data.card" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          UzCard
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{check_data.card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="check_data.humo" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          Humo
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{check_data.humo.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="check_data.uz_card" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          Payme
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{check_data.uz_card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="check_data.perchisleniya" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          Click
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{check_data.perchisleniya.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="check_data.creadit_payed" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          Paynet
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{check_data.creadit_payed.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="check_data.rasxod" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          UzumPay
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{check_data.rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="check_data.online" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          Перечисления
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{check_data.online.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="check_data.dolg" class=" d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          Пластикка ўтказма
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck">
          {{check_data.dolg.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="check_data.profit_summ" class=" text-warning d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          {{$t('skidka')}}
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck text-warning">
          {{check_data.profit_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>

      <div v-if="check_data.srogi_otganlar_uchun_rasxod" class=" text-danger d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;" >
          Қайтим нақт
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck  text-danger">
          {{check_data.srogi_otganlar_uchun_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>
      <div v-if="check_data.salary" class=" text-danger d-flex justify-content-between"  style="border-bottom: 0.5px solid #CED4DA;">
        <strong style="font-size: 12px; font-weight:bold;">
          Қайтим доллор
        </strong>
        <strong style="font-size: 12px; font-weight:bold;" class="paymentCheckPrintCheck text-danger text-danger">
          {{check_data.salary.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
        </strong>
      </div>

      <div style="border-bottom: 1px dashed black;">
        <!-- ******************************************************************************************** -->
      </div>
      <div style="margin: 5px 0px;">
        <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
          <strong style="font-size: 13px; font-weight:bold;" class="paymentCheckPrintCheck">Итого к оплате: </strong>
          <strong style="font-size: 13px; font-weight:bold;" class="paymentCheckPrintCheck">{{(check_data.summ || 0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</strong>
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
      check_data: {},
    }
  },
  mounted(){
    
  },
  created() {
    this.$root.$refs.check = this;
  },
  computed: mapGetters(['get_zakaz_product_all_list','get_page_savat', 'get_all_summa','get_page_savat', 'CheckId']),
  methods: {
   ...mapMutations(['clear_order']),
   async printMounted(data){
    this.check_data = data;
    this.kassir = data.image_base_64;
    this.dateTime = data.create_date.slice(8,10) + '-' + data.create_date.slice(5,7) + '-' + data.create_date.slice(0,4);
    this.time = data.create_date.slice(11,16);
    console.log('this.check_data',this.check_data)
    this.$nextTick(() => {
      this.printDiv();
    });
   },
   printDiv() {
      
        let divContents = document.getElementById("print_load").innerHTML;

        // CSS fayllarini olish
        let styles = "";
        document.querySelectorAll('link[rel="stylesheet"], style').forEach((node) => {
          styles += node.outerHTML;
        });

        // Yangi oynani ochish
        let printWindow = window.open("", "", "height=800,width=1000");

        let html =
          '<html>' +
          '<head>' +
          '<title>Chop etish</title>' +
          styles +
          '<style>' +
          /* A4 format + ichki margin */
          '@page { size: A4; margin: 1mm; }' +

          /* print paytida layoutni cheklash */
          '@media print {' +
          '  html, body { width: 210mm; height: 297mm; margin: 0 0; }' +
          '  #print-container { width: 100%; max-width: 190mm; margin: 0 0; }' +
          '  * { -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; color-adjust: exact !important; }' +
          '}' +
          '</style>' +
          '</head>' +
          '<body>' +
          '<div id="print-container">' +
          divContents +
          '</div>' +
          '<script>window.onafterprint = function() { window.close(); }<\/script>' +
          '</body>' +
          '</html>';

        printWindow.document.open();
        printWindow.document.write(html);
        printWindow.document.close();
        printWindow.focus();
        setTimeout(() => {
          printWindow.print();
        }, 700);
      
        
    }
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
<template>
  <div class="checkPrint" style="z-index: 1000;">
    <div  class="ckeck_main" style="width:96%; height:100%;">
      <div style="text-align:center; margin: 10px 0; padding: 5px 7px; font-size:13.5px;">
        <h6 style="font-size:13.5px; font-weight:bold" class="paymentCheckPrint">ООО «OQQO'RG'ON TEGIRMON» 
          Таш. обл., Аккурганский р-н, Дустлик КФЙ</h6>
      </div>
      <div class="infoHeader" style="width:100%;">
        <div style="margin: 5px 0px;">
          <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
            <span style="font-size:12px; font-weight:bold" class="paymentCheckPrint">Кассир </span>
            <span v-if="zaxiraListInvoice[0].kassir" style="font-size: 13px; font-weight:bold" class="paymentCheckPrint">{{zaxiraListInvoice[0].kassir}}</span>
          </div>
        </div>
        <div style="margin: 5px 0px;">
          <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
            <span style="font-size:12px; font-weight:bold" class="paymentCheckPrint">Напечатан </span>
            <span style="font-size: 13px; font-weight:bold" class="paymentCheckPrint">{{ zaxiraListInvoice[0].date.slice(8,10) + '-' + zaxiraListInvoice[0].date.slice(5,7) + '-' + zaxiraListInvoice[0].date.slice(0,4)}} <span class="ml-1">{{zaxiraListInvoice[0].date.slice(11,16)}}</span></span>
          </div>
        </div>
      </div>
      <div class="client_all_info" v-for="(item, index) in zaxiraListInvoice" :key="index" style=" border-bottom: 1px dashed black;">
        <div class="infoList infoHeader">
          <div v-show="item.check_number" style=" display:flex; flex-wrap: wrap; justify-content: space-between; margin-top:3px;">
            <span style="font-size: 12px; font-weight: bold;">{{$t('check_number')}}:</span>
            <span style="font-size: 12px; font-weight:bold"  class="paymentCheckPrint">{{item.check_number}}</span>
          </div>
          <div  style="display:flex; flex-wrap: wrap; justify-content: space-between; margin-top:3px;">
            <span style="font-size: 12px; font-weight: bold;">{{$t('client_name')}}:</span>
            <span style="font-size: 12px; font-weight:bold"  class="paymentCheckPrint">{{item.client_dv}}</span>
          </div>
          <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between; margin-top:3px;">
            <span style="font-size: 12px; font-weight: bold;">Основной клиент:</span>
            <span style="font-size: 12px; font-weight:bold"  class="paymentCheckPrint">{{item.client_name}}</span>
          </div>
          <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between; margin-top:3px;">
            <span style="font-size: 12px; font-weight: bold;">{{$t('product')}}</span>
            <span style="font-size: 12px; font-weight:bold"  class="paymentCheckPrint">{{item.product_name}}</span>
          </div>
          <!-- <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between; margin-top:3px;">
            <span style="font-size: 14px; font-weight: bold;">{{$t('qty')}}</span>
            <span style="font-size: 14px;">{{item.real_qty}}</span>
          </div> -->
        </div>

        <div style="padding: 5px 0px;" class="infoHeader" v-show="item.sell.sell_price>0">
          <table style=" width:100%;">
            <thead style="">
                <tr style="border-bottom: 0.5px solid #CED4DA;">
                  <th  class="paymentCheckPrint" style="font-weight: bold; font-size: 12px;">{{$t('product')}}</th>
                  <th  class="paymentCheckPrint" style="font-weight: bold; font-size: 12px; text-align: center">{{$t('soni')}}</th>
                  <th  class="paymentCheckPrint" style="font-weight: bold; font-size: 12px; text-align: right">{{$t('price')}}</th>
                </tr>
            </thead>
            <tbody>
    
              <tr style="border-bottom: 0.5px solid #CED4DA;" >
                <td style="font-size: 12px; font-weight:bold"  class="paymentCheckPrint">{{item.sell.product_name}}</td>
                <td style="text-align: center; font-size: 12px; font-weight:bold;"  class="paymentCheckPrint">{{item.sell.qty}} кг</td>
                <td style="text-align: right; font-size: 12px; font-weight:bold;"  class="paymentCheckPrint">{{item.sell.sell_price}}</td>
              </tr>
            </tbody>
          </table>
        </div>

        <div style="padding: 10px 0px;" class="infoHeader">
          <table style=" width:100%;">
            <thead style="">
                <tr style="border-bottom: 0.5px solid #CED4DA; ">
                  <th  class="paymentCheckPrint" style="font-weight: bold; font-size: 12px; " >{{$t('product')}}</th>
                  <th  class="paymentCheckPrint" style="font-weight: bold; font-size: 12px;  text-align: center;">{{$t('soni')}}</th>
                  <th  class="paymentCheckPrint" style="font-weight: bold; font-size: 12px;  text-align: right;">{{item.product_name}} </th>
                </tr>
            </thead>
            <tbody>
              <tr style="border-bottom: 0.5px solid #CED4DA;" v-for="(data,i) in item.changeProduct" :key="i">
                <td  class="paymentCheckPrint" style="font-weight:bold; font-size: 12px;">{{data.product_name}}</td>
                <td  class="paymentCheckPrint" style="font-weight:bold; text-align: center; font-size: 12px;">{{data.qty}}</td>
                <td  class="paymentCheckPrint" style="font-weight:bold; text-align: right; font-size: 12px;">{{data.all_qty.toFixed(1)}} кг</td>
              </tr>
            </tbody>
          </table>
        </div>
        <div class="infoList">
        <div  style="text-align:center; margin: 0px 0; padding: 5px 7px; font-size:12px;">
          <h6 style="font-size:13px; font-weight:bold;" class="paymentCheckPrintCheck">{{$t('ostatka')}}</h6>
        </div>
        <table style=" width:100%;">
          <thead>
              <tr style="border-bottom: 0.5px solid #CED4DA;">
                <th class="paymentCheckPrintCheck" style="font-weight: bold; font-size:12px;  text-align: left">{{$t('product')}}</th>
                <th class="paymentCheckPrintCheck" style="font-weight: bold; font-size:12px; text-align: right">{{$t('qty')}}</th>
              </tr>
          </thead>
          <tbody>
            <tr v-if = "item.ostatka_old>0">
              <td class="paymentCheckPrintCheck" style="font-weight:bold; text-align: left; font-size:12px;">{{item.product_name}}</td>
              <td class="paymentCheckPrintCheck " style="font-weight:bold; text-align: right; font-size:12px;">{{item.ostatka_old.toFixed(1)}} кг</td>
            </tr>
            <tr v-else v-for="(item,i) in check_get_ostatka" :key="i" style="border-bottom: 0.5px solid #CED4DA;">
              <td class="paymentCheckPrintCheck" style="font-weight:bold; text-align: left; font-size:12px;">{{item.product.name}}</td>
              <td class="paymentCheckPrintCheck " style="font-weight:bold; text-align: right; font-size:12px;">{{item.qty.toFixed(1)}} {{item.product.unitMeasurment.name}}</td>
            </tr>
          </tbody>
        </table>
      </div>
      </div>
      
      <div style="margin: 5px 0px;">
        <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
          <span  class="paymentCheckPrint" style="font-size: 12px; font-weight:bold">Итого к оплате: </span>
          <span  class="paymentCheckPrint" style="font-size: 12px; font-weight:bold">{{zaxiraListInvoice[0].sell.sell_price}}</span>
        </div>
      </div>
      <div style="border-bottom: 1px dashed black;">
        <!-- ******************************************************************************************** -->
      </div>
      <div style="margin: 10px 0px 0px 0px; display:flex; justify-content:center;">
          <span  class="paymentCheckPrint" style="font-weight:bold; font-size: 12.5px;">****************************************** </span>
      </div>
      <div style="margin: 0px 0px; display:flex; justify-content:center;">
          <span  class="paymentCheckPrint" style="font-weight:bold; font-size: 12.5px;">Контакты:  </span>
      </div>
      <div style="margin: 5px 0px; display:flex; justify-content:center;">
          <span  class="paymentCheckPrint" style="font-weight:bold; font-size: 12.5px;">+998 97 9999945 </span>
      </div>
      <!-- <div style="margin: 5px 0px; display:flex; justify-content:center;">
          <span  class="paymentCheckPrint" style="font-weight:bold; font-size: 12.5px;">+998 99 6005555 </span>
      </div>
      <div style="margin: 5px 10px; display:flex; justify-content:center;">
          <span style="font-size: 12.5px; font-weight:bold;" class="paymentCheckPrintCheck">+998 97 3855555 </span>
      </div> -->
      <div style="margin: 5px 0px 0px 0px; display:flex; justify-content:center;">
          <span  class="paymentCheckPrint" style="font-weight:bold; font-size: 12.5px;">****************************************** </span>
      </div>

      <div class="d-flex justify-content-center mt-2">
          <qr-code :text="value" :size="size" error-level="L"></qr-code>
      </div>
      <div style="display:flex; justify-content: center; padding: 0; padding-top: 8px; margin-bottom: 0px;">
        <span style="font-size: 13px;" class="paymentCheckPrintCheck"> 
            <span style="font-size: 12px;" class="paymentCheckPrintCheck mr-2">Ид:</span>
            {{zaxiraListInvoice[0].client_id}}
          </span>
          <span style="font-size: 13px; margin-left: 15px;" class="paymentCheckPrintCheck"> 
            <span style="font-size: 12px;" class="paymentCheckPrintCheck mr-2">Парол:</span>
            {{zaxiraListInvoice[0].client_key}}
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
  async mounted(){
    // window.print();
    // this.$emit('close');

    // this.clear_order(); 1
    // await this.writeCash(); 1

    setTimeout(async() => {
        window.print();
        this.$emit('close');
    }, 100);
  },
  created() {
    this.$root.$refs.check = this;
  },
  computed: mapGetters(['get_zakaz_product_all_list','get_page_savat', 'get_all_summa','get_page_savat', 'check_get_ostatka','zaxiraListInvoice','get_ostatka_check_zaxira']),
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
.paymentCheckPrint, .paymentCheckPrintCheck{
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
  overflow-y: scroll;
  background-color: #fff;
  display: flex;
  justify-content: center;

  .ckeck_main{
    width: 270px;
    height:100%;
    

    .infoHeader{
      border-bottom: 1px dashed black;
      padding: 0 0px 10px 0px; 
    }
    .infoList{
      padding: 10px 0px 15px 0px; 
    }
    .totalInfo{
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 10px 0px 15px 0px; 

    }
  }
}
</style>
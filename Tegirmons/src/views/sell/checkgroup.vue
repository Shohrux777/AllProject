<template>
  <div class="check" style="z-index: 1000;">
    <div  class="ckeck_maingroup" style="width:96%; height:100%;">
      <div style="text-align:center; margin: 10px 0; padding: 5px 7px; font-size:12.5px;">
        <h6 style="font-size:13.5px; font-weight:bold;" class="paymentCheckPrint">ООО «OQQO'RG'ON TEGIRMON» 
          Таш. обл., Аккурганский р-н, Дустлик КФЙ</h6>
      </div>
      <div class="infoHeader" style="width:100%;">
        <div style="margin: 5px 0px;" v-if="get_all_check[0].kassir">
          <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
            <span style="font-size:12.5px; font-weight:bold;" class="paymentCheckPrint">Кассир </span>
            <span style="font-size: 13px; font-weight:bold;" class="paymentCheckPrint">{{get_all_check[0].kassir}}</span>
          </div>
        </div>
        <div style="margin: 5px 0px;" v-else>
          <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
            <span style="font-size:12.5px; font-weight:bold;" class="paymentCheckPrint">Кассир </span>
            <span style="font-size: 13px; font-weight:bold;" class="paymentCheckPrint">{{kassir}}</span>
          </div>
        </div>
        <div style="margin: 5px 0px;">
          <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
            <span style="font-size:12.5px; font-weight:bold;" class="paymentCheckPrint">Напечатан </span>
            <span style="font-size: 13px; font-weight:bold;" class="paymentCheckPrint">{{get_all_check[0].date.slice(8,10) + '-' + get_all_check[0].date.slice(5,7) + '-' + get_all_check[0].date.slice(0,4)}} <span class="ml-1">{{get_all_check[0].date.slice(11,16)}}</span></span>
          </div>
        </div>
      </div>
      <div class="client_all_info" v-for="(item, index) in get_all_check" :key="index" style=" border-bottom: 1px dashed black;" v-show="item.sell.sell_price>0  || item.changeProduct.length>0">
        <div class="infoList infoHeader">
          <div class="paymentCheckPrintCheck" style=" display:flex; flex-wrap: wrap; justify-content: space-between; margin-top:3px;">
            <span class="paymentCheckPrintCheck" style="font-size: 12px; font-weight: bold;">{{$t('client_name')}}:</span>
            <span class="paymentCheckPrintCheck" style="font-size: 12px; font-weight:bold;">{{item.client_name}}</span>
          </div>
          <div  class="paymentCheckPrintCheck" style=" display:flex; flex-wrap: wrap; justify-content: space-between; margin-top:3px;">
            <span class="paymentCheckPrintCheck" style="font-size: 12px; font-weight: bold;">{{$t('product')}}</span>
            <span class="paymentCheckPrintCheck" style="font-size: 12px; font-weight:bold;">{{item.product_name}}</span>
          </div>
          <div  class="paymentCheckPrintCheck" style=" display:flex; flex-wrap: wrap; justify-content: space-between; margin-top:3px;">
            <span class="paymentCheckPrintCheck" style="font-size: 12px; font-weight: bold;">{{$t('qty')}}</span>
            <span class="paymentCheckPrintCheck" style="font-size: 12px; font-weight:bold;">{{item.real_qty}}  кг</span>
          </div>
          <div  class="paymentCheckPrintCheck" style=" display:flex; flex-wrap: wrap; justify-content: space-between; margin-top:3px;">
            <span class="paymentCheckPrintCheck" style="font-size: 12px; font-weight: bold;">{{$t('skidka')}}</span>
            <span class="paymentCheckPrintCheck" style="font-size: 12px; font-weight:bold;">{{item.sell.skidka}}  кг</span>
          </div>
        </div>

        <div style="padding: 5px 0px;" class="infoHeader" v-show="item.sell.sell_price>0">
          <table style=" width:100%;">
            <thead style="">
                <tr style="border-bottom: 0.5px solid #CED4DA; ">
                  <th class="paymentCheckPrintCheck" style="font-weight: bold; font-size: 12px; ">{{$t('product')}}</th>
                  <th class="paymentCheckPrintCheck" style="font-weight: bold; font-size: 12px; text-align: center">{{$t('soni')}}</th>
                  <th class="paymentCheckPrintCheck" style="font-weight: bold; font-size: 12px; text-align: right">{{$t('price')}}</th>
                </tr>
            </thead>
            <tbody>
              <tr style="border-bottom: 0.5px solid #CED4DA;" >
                <td class="paymentCheckPrintCheck" style="font-size: 12px; font-weight:bold;">{{item.sell.product_name}}</td>
                <td class="paymentCheckPrintCheck" style="text-align: center; font-size: 12px; font-weight:bold;">{{item.sell.qty}} кг</td>
                <td class="paymentCheckPrintCheck" style="text-align: right; font-size: 12px; font-weight:bold;">{{item.sell.sell_price}}</td>
              </tr>
            </tbody>
          </table>
        </div>

        <div style="padding: 5px 0px;" class=" border_black" v-show="item.changeProduct.length>0">
          <table style=" width:100%;">
            <thead style="">
                <tr style="border-bottom: 0.5px solid #CED4DA; ">
                  <th class="paymentCheckPrintCheck" style="font-weight: bold; font-size: 12px;">{{$t('product')}}</th>
                  <th class="paymentCheckPrintCheck" style="font-weight: bold; font-size: 12px; text-align: center">{{$t('soni')}}</th>
                  <th class="paymentCheckPrintCheck" style="font-weight: bold; font-size: 12px; text-align: right">{{item.product_name}}</th>
                </tr>
            </thead>
            <tbody>
    
              <tr style="border-bottom: 0.5px solid #CED4DA;" v-for="(data,i) in item.changeProduct" :key="i">
                <td class="paymentCheckPrintCheck" style="font-size: 12px; font-weight:bold;">{{data.product_name}}</td>
                <td class="paymentCheckPrintCheck" style="text-align: center; font-size: 12px; font-weight:bold;">{{data.qty}}</td>
                <td class="paymentCheckPrintCheck" style="text-align: right; font-size: 12px; font-weight:bold;">{{data.all_qty}}  кг</td>
              </tr>
            </tbody>
          </table>
        </div>

      </div>
      
      <div style="margin: 5px 0px;">
        <div  style=" display:flex; flex-wrap: wrap; justify-content: space-between;  margin-top:3px;">
          <span style="font-size: 12px; font-weight:bold;">Итого к оплате: </span>
          <span style="font-size: 12px; font-weight:bold;">{{check_all_summ}}</span>
        </div>
      </div>
      <div style="border-bottom: 1px dashed black;">
        <!-- ******************************************************************************************** -->
      </div>
      <div style="margin: 11.5px 0px 0px 0px; display:flex; justify-content:center;">
          <span style="font-size: 13px; font-weight:bold;">****************************************** </span>
      </div>
      <div style="margin: 0px 0px; display:flex; justify-content:center;">
          <span style="font-size: 13px; font-weight:bold;" class="paymentCheckPrintCheck">Контакты:  </span>
      </div>
      <div style="margin: 5px 0px; display:flex; justify-content:center;">
          <span style="font-size: 13px; font-weight:bold;" class="paymentCheckPrintCheck">+998 97 9999945 </span>
      </div>
      <div style="margin: 5px 0px 0px 11.5px; display:flex; justify-content:center;">
          <span style="font-size: 13px; font-weight:bold;">****************************************** </span>
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
    }
  },
  async mounted(){
    window.print();
    // await this.writeCash();
    this.$emit('close');
    // this.clear_order();
  },
  created() {
    this.$root.$refs.check = this;
  },
  computed: mapGetters(['get_zakaz_product_all_list','get_page_savat', 'get_all_summa','get_page_savat', 'CheckId', 'get_all_check','check_all_summ']),
  methods: {
   ...mapMutations(['clear_order']),
   async writeCash(){
     try{
       const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "tegirmonAuthid": localStorage.AuthId,
          "for_buy_tovar_rasxod" : this.check_all_summ,
          "auth_user_updator_id": localStorage.kassa_id,
          // "rasxod": this.group_id,
          // "uz_card": 0,     for skidka uchun ishlataman
        })
      };
      console.log('requestOptions.body')
      console.log(requestOptions.body)
        const response = await fetch(this.$store.state.hostname + "/TegirmonCheck", requestOptions);
        const data = await response.json();
        console.log('check')
        console.log(data)

     }
     catch{
        console.log('error')
     }
   }
  }
}
</script>

<style lang="scss" >
@font-face{
  font-family: "Ubuntu";
  src: url("../../font/Ubuntu/Ubuntu-Bold.ttf")
}
.paymentCheckPrint{
  font-family: 'Ubuntu';
  font-weight: bold;
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
.check {
  position: fixed;
  top: 0;
  left: 0;
  width: 96%;
  background-color: #fff;
  display: flex;
  justify-content: center;

  .ckeck_maingroup{
    width: 270px;
    height: 100%;
  
    .infoHeader{
      border-bottom: 1px dashed black;
      padding: 0 0px 10px 0px; 
    }
    .infoList{
      padding: 5px 0px 5px 0px; 
    }
    .totalInfo{
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 10px 0px 15px 0px; 

    }
  }
}
.border_black{
  font-family: 'Ubuntu';
  font-weight: bold; 
  border-bottom: 2px solid black;
}
</style>
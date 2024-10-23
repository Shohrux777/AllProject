<template>
  <div class="checkPrint paymentCheckPrintCheck" style="z-index: 1000; border:none !important;">
    <div  id="printForm"  class="ckeck_main_sell mt-4 border" style="width:100%; border:none !important; height: 100%; transform: rotate(90deg);">
      <div class="" style="width:100%; border:none !important;">
        <div style="margin: 1px 0px; border:none !important;">
          <div style=" display:flex; flex-wrap: wrap; justify-content: center;  margin-top:0px;">
          </div>
        </div>
        <div style="margin: 1px 0px;">
<div  style=" display:flex; flex-wrap: wrap; justify-content: center; margin-top:0px; width: 200px;">
            <span style="font-size:13px; margin-left: 5px; margin-top: -5px;" class="paymentCheckPrintCheck">#{{ numId }}</span>
          </div>
          <div style=" display:flex; flex-wrap: wrap; justify-content: center; margin-top:0px; width: 200px;">
            <span style="font-size:13px; margin-top: -5px;" class="paymentCheckPrintCheck">{{ patient_name }}</span>
          </div>
        </div>
      </div>
      <div style="margin: 0px 0px; margin-top: 3px; display:flex; justify-content:center; align-items: center; width: 190px;">
        <barcode  width="1" style="border:none !important;" height="25" displayValue="false" marginTop="1" marginBottom="1" v-bind:value="numId">
        </barcode>
      </div>
    </div>
  </div>
</template>

<script>
import VueBarcode from 'vue-barcode';
// import Printjs from "print-js";
import {mapGetters, mapMutations} from 'vuex'
export default {
  components: {
    'barcode': VueBarcode
  },
  data(){
    return{
      comp_name: localStorage.CompName,
      kassir : localStorage.docName,
      dateTime: new Date().toLocaleDateString(),
      time: new Date().toDateString,
      barcodeValue: 123,
    }
  },
  props: {
    numId:{
        type:Number,
        default:null
      },
      patient_name:{
        type: String,
        default: ''
      },
    },
  mounted(){
    setTimeout(() => {
      console.log('hiy')
      window.print();
      this.$emit('close');
    }, 350);
    // this.clear_order();
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
  computed: mapGetters(['get_ochred', 'get_ochredService']),
  methods: {
   ...mapMutations(['clear_order']),
  },
}
</script>

<style lang="scss" >
@font-face{
  font-family: "Ubuntu";
  src: url("../../../font/Ubuntu/Ubuntu-Bold.ttf")
}
.paymentCheckPrintCheck{
  font-family: 'Ubuntu';  
  font-weight: bold;
}
span, h6{
  font-family: 'Ubuntu';  
}
*{
  padding: 0;
  margin: 0;
}
.checkPrint {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  background-color: #fff;
  display: flex;
  justify-content: center;
  margin: 0;
  padding: 0;

  .ckeck_main_sell{
    width: 100%;
    height: 100%;

    .infoHeader{
      padding: 0px 0px 1px 0px; 
    }
    // .infoList{
    //   padding: 10px 0px 15px 0px; 
    // }
    // .totalInfo{
    //   display: flex;
    //   justify-content: space-between;
    //   align-items: center;
    //   padding: 10px 0px 20px 0px; 
    // }
  }
}
</style>
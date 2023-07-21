<template >
  <!-- 58 mm Xprinter     width: 270px; -->
  <!-- 800 mm Xprinter    width: 400px; -->
  <div style="width: 400px; background:#fff;" class="paymentCheckPrintCheck" id="printForm">
    <div class="img" style="text-align:center;">
      <img id="image" width="100%" alt="">
    </div>
    <div>
      <div  style="text-align:center; margin: 10px 0 0 0; padding: 2px 7px; ">
        <!-- <img src="../../assets/iymon.svg" height="80" style="width: 100%;" alt="">
        <h4 style="" class="paymentCheckPrintCheck mt-2 mb-3">IYMON MEDICAL SBJ </h4> -->
        <img src="../../assets/Aider.svg" height="80" style="width: 100%;" alt="">
        <h5 style="" class="paymentCheckPrintCheck mt-2 mb-3">AIDER MED CENTR</h5>
      </div>
     <!-- <div  style="text-align:center; margin-bottom: 12px; padding: 0 10px"  >
        <a  style="font-weight: bold; font-size: 16px;" class="paymentCheckPrintCheck">ISTIQBOL <i class="fas fa-heartbeat"></i> MED CENTER</a>
     </div> -->
     <!-- <div style="display:flex; justify-content: space-between; padding: 0 0px;">
        <span style="font-size: 15.5px;" class="paymentCheckPrintCheck">STIR</span>
        <span style="font-size: 15.5px;" class="paymentCheckPrintCheck">302145331</span>
     </div> -->
     <div style="display:flex; justify-content: space-between; padding: 0 0px;">
        <span style="font-size: 16px;" class="paymentCheckPrintCheck">Дата</span>
        <span style="text-align:right;" class="paymentCheckPrintCheck"><span style="font-size: 16px;" class="paymentCheckPrintCheck">{{time}} <span style="margin-left:3px;">{{minut}}</span></span></span>
     </div>
     <div style="display:flex; justify-content: space-between; padding: 0; padding-top: 5px;">
        <span style="font-size: 16px;" class="paymentCheckPrintCheck">Чек</span>
        <span style="font-size: 16px;" class="paymentCheckPrintCheck">{{checkInfo.checkId}}</span>
     </div>
     <!-- <div style="display:flex; justify-content: space-between; padding: 0 0px; border-bottom: 0.5px solid #000000; padding-bottom: 5px;">
        <span style="font-size: 16px;" class="paymentCheckPrintCheck">CH KKM</span>
        <span style="font-size: 16px;" class="paymentCheckPrintCheck">N500W495220</span>
     </div> -->
     <div style="display:flex; justify-content:space-between; padding: 0 0px; padding-top: 5px;">
        <span style="font-size: 16px;" class="paymentCheckPrintCheck">Кассир:</span>
        <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">{{casher}}</span>
     </div>
     <div style="display:flex; justify-content:space-between; padding: 0 0px;  border-bottom: 1px dashed black; padding-bottom: 5px;">
        <span style="font-size: 16px;" class="paymentCheckPrintCheck">Пациент:</span>
        <span  style="font-size: 16.5px; text-align:right;" class="paymentCheckPrintCheck"><span>{{patientFio}}</span></span>
     </div>

     <div  style="padding: 7px 0; border-bottom: 1px dashed black;">
       <div  style="display:flex; justify-content: space-between; padding: 0 0px;" v-for="(item, i) in get_check_print_list" :key="i">
          <span style="font-size: 18px;" class="paymentCheckPrintCheck">{{i+1}}. {{item.serviceName}} (x{{item.qty}})</span>
          <span style="font-size: 17px;" class="paymentCheckPrintCheck"> ={{item.summ*item.qty}}</span>
       </div>
     </div>
     
     <div  style="display:flex; justify-content: space-between; padding: 5px 0px 0 0px; ">
        <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">К оплате</span>
        <span style="font-size: 17.5px;"   class="paymentCheckPrintCheck">{{get_check_sum }}</span>
     </div>
     <div style="display:flex; justify-content: space-between; padding: 0 0px;">
        <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">Оплачено</span>
        <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">{{parseInt(checkInfo.cash) + parseInt(checkInfo.card)}}</span>
     </div>
     <div v-show="checkInfo.cash!=0"  style="display:flex; justify-content: space-between; padding: 0 0px;">
        <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">Наличи</span>
        <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">{{checkInfo.cash}}</span>
     </div>
     <div v-show="checkInfo.card!=0" style="display:flex; justify-content: space-between; padding: 0 0px 7px 0px;  border-bottom: 1px dashed black;" >
        <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">Пластик</span>
        <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">{{checkInfo.card}}</span>
     </div>

     <!-- <div style="display:flex; justify-content: space-between; padding: 0 0px;">
        <span  style="font-size:12px" class="paymentCheckPrintCheck">QQS</span>
        <span  style="font-size:12px" class="paymentCheckPrintCheck">0</span>
     </div> -->
    </div>
  </div>
</template>

<script>
//import Printjs from "print-js";
import {mapGetters,mapActions} from 'vuex'
export default {
  data(){
    return {
      time: new Date().toISOString().slice(0, 10),
      casher: localStorage.docName,
      cash: null,
      allSumm: 0,
      numberOrder: 1,
      checklist: [],
      minut: new Date().toISOString().slice(11, 19),
    }
  },
  async mounted(){
    // this.casher = localStorage.docName;
    setTimeout(() => {
      console.log('hiy')
      window.print();
      this.$emit('close');
    }, 350);
  },
  created() {
    this.$root.$refs.check = this;
  },
  methods: {
    ...mapActions(['fetch_unpayed_patient']),
    async printed(){
      // await this.fetch_unpayed_patient();
       window.print()
      // Printjs({
      //  printable: "printForm", //Id to print content 
      //  type: "html",
      //  targetStyles: ['*'],
        // ignoreElements:['no-print','bc','gb']
      //});
      console.log('end')

      this.$router.back()
    }
  },

  
  props: {
    options:{
        type: Array,
        default(){
          return []
        }
      },
  },
  watch: {
    options(){
      this.allSumm = 0;
      this.allSumm = this.options.reduce((total, item) => {
          return total + item.summ
      }, 0)
    },
    // get_check_print_list(){
    //   this.checklist = [];
    //   var list = [];
    //   list = this.get_check_print_list
    //   console.log('list')
    //   console.log(list)
    //   for(let i=0; i<list.length; i++){
    //     var check = {  
    //       ServicePayId: null,
    //       serviceName: "",
    //       summ: 0,
    //       qty: 1,
    //     }
    //     check.ServicePayId = list[i].ServicePayId;
    //     check.serviceName = list[i].serviceName;
    //     check.summ = list[i].summ;

    //     for(let j=i+1; j<list.length; j++){
    //       if(list[i].ServicePayId == list[j].ServicePayId){
    //         check.qty += 1;
    //         list.splice(j, 1);
    //       }
    //     }
    //     this.checklist.push(check);
    //   }
    //   console.log('checklist')
    //   console.log(this.checklist)
    //   console.log(this.get_check_print_list)

    // }
  },
  computed: {
    ...mapGetters(['get_check_print_list', 'get_check_sum', 'debit_sum', 'checkInfo']),
    summa(){
      return this.$store.state.cashPay_card
    },
    patientFio(){
      return this.$store.state.patient_name_for_cash
    }
  }
}
</script>

<style  scoped>
/* // @import "../../scss/index.scss"; */
@font-face{
  font-family: "Ubuntu";
  src: url("../../font/Ubuntu/Ubuntu-Bold.ttf")
}
.paymentCheckPrintCheck{
  font-weight: bold;
  font-family: 'Ubuntu';
}
span, th, td, a, p{
 font-weight: bold;
 font-family: 'Ubuntu';
}
</style>
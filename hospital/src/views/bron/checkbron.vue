<template >
    <!-- 58 mm Xprinter     width: 270px; -->
    <!-- 800 mm Xprinter    width: 400px; -->
    <div style="width: 400px; background:#fff;" class="paymentCheckPrintCheck" id="printForm">
      <div class="img" style="text-align:center;">
        <img id="image" width="100%" alt="">
      </div>
      <div>
        <div  style="text-align:center; margin: 0 0 0 0; padding: 2px 7px; ">
          <!-- <img src="../../assets/novo.svg" height="80" style="width: 100%;" alt="">
          <h5 style="" class="paymentCheckPrintCheck mt-2 mb-3">OOO 'ExtresMed'</h5> -->
  
          <!-- <img src="../../assets/doctor.jpg" height="80" style="width: 100%;" alt=""> -->

          <!-- <h5 style="" class="paymentCheckPrintCheck mt-2 mb-3">
            «AVITSENNA PLUS MED SERVIS» МЧЖ
          </h5> -->
          <!-- <img src="../../assets/novo.svg" height="80" style="width: 100%;" alt="">
          <h5 style="" class="paymentCheckPrintCheck mt-2 mb-3">OOO 'NOVO MEDICS' Уратепалик МФЙ. И.Каримов кучаси, 17-уй</h5> -->
        
          <img src="../../assets/iymon.svg" height="80" style="width: 100%;" alt="">
          <h4 style="" class="paymentCheckPrintCheck mt-2 mb-3">IYMON MEDICAL SBJ </h4> 
       
          <!-- <h4 style="" class="paymentCheckPrintCheck mt-2 mb-3">AIDER MED CENTR</h4> -->
        </div>
        <div style="display:flex; justify-content: center; padding: 0; padding-top: 0px; margin-top:-5px; margin-bottom: 7px;">
          <span style="font-size: 24px;" class="paymentCheckPrintCheck text-center"> 
            --- ID: {{ get_bron_check_data.patientsId }} ---
          </span>
       </div>
       <!-- <div  style="text-align:center; margin-bottom: 12px; padding: 0 10px"  >
          <a  style="font-weight: bold; font-size: 16px;" class="paymentCheckPrintCheck">ISTIQBOL <i class="fas fa-heartbeat"></i> MED CENTER</a>
       </div> -->
       <!-- <div style="display:flex; justify-content: space-between; padding: 0 0px;">
          <span style="font-size: 15.5px;" class="paymentCheckPrintCheck">STIR</span>
          <span style="font-size: 15.5px;" class="paymentCheckPrintCheck">302145331</span>
       </div> -->
       <div style="display:flex; justify-content: space-between; padding: 0 0px;">
          <span style="font-size: 16px;" class="paymentCheckPrintCheck">Напечатан</span>
          <span style="text-align:right;" class="paymentCheckPrintCheck"><span style="font-size: 16px;" class="paymentCheckPrintCheck">{{time}} <span style="margin-left:3px;">{{minut}}</span></span></span>
       </div>
       <!-- <div style="display:flex; justify-content: space-between; padding: 0 0px; border-bottom: 0.5px solid #000000; padding-bottom: 5px;">
          <span style="font-size: 16px;" class="paymentCheckPrintCheck">CH KKM</span>
          <span style="font-size: 16px;" class="paymentCheckPrintCheck">N500W495220</span>
       </div> -->
       <div style="display:flex; justify-content:space-between; padding: 0 0px; padding-top: 5px;">
          <span style="font-size: 16px;" class="paymentCheckPrintCheck">Кассир:</span>
          <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">{{casher}}</span>
       </div>
       <div style="display:flex; justify-content:space-between; padding: 0 0px; padding-bottom: 5px;">
          <span style="font-size: 16px;" class="paymentCheckPrintCheck">Пациент:</span>
          <span  style="font-size: 16.5px; text-align:right;" class="paymentCheckPrintCheck">{{get_bron_check_data.reserved_name_1}}</span>
       </div>
       <div style="display:flex; justify-content:space-between; padding: 0 0px; padding-bottom: 5px;">
          <span style="font-size: 16px;" class="paymentCheckPrintCheck">Врач:</span>
          <span  style="font-size: 17px; text-align:right;" class="paymentCheckPrintCheck">{{get_bron_check_data.reserved_name_3}}</span>
       </div>
       <div style="display:flex; justify-content: start; padding: 0; padding-top: 0px;">
        <h4 class="line_one ">-----------------------------------------------------</h4>
       </div>
  
       <div  style="padding: 0px 0px 4px 0px;">
            <div style="display:flex; justify-content:center; padding: 0 0px; padding-top: -5px;">
                <span style="font-size: 19px;" class="paymentCheckPrintCheck">Стационар</span>
            </div>
            <div style="display:flex; justify-content:space-between; padding: 0 0px; padding-top: 5px;">
                <span style="font-size: 16px;" class="paymentCheckPrintCheck">Время начала:</span>
                <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">{{get_bron_check_data.begin_date_bron.slice(0,10)}}</span>
            </div>
            <div style="display:flex; justify-content:space-between; padding: 0 0px; padding-top: 5px;">
                <span style="font-size: 16px;" class="paymentCheckPrintCheck">Время окончания:</span>
                <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">{{get_bron_check_data.end_date_bron.slice(0,10)}}</span>
            </div>
            <div style="display:flex; justify-content:space-between; padding: 0 0px; padding-top: 5px;">
                <span style="font-size: 16px;" class="paymentCheckPrintCheck">День:</span>
                <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">{{get_bron_check_data.reserved_number_1}}</span>
            </div>
       </div>
       <div style="display:flex; justify-content: start; padding: 0; padding-top: 0px;">
        <h4 class="line_one ">-----------------------------------------------------</h4>
       </div>
       
       <div  style="display:flex; justify-content: space-between; padding: 5px 0px 0 0px;">
          <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">К оплате</span>
          <span style="font-size: 17.5px;"   class="paymentCheckPrintCheck">{{get_bron_check_data.need_payed_summ + get_bron_check_data.payed_summ}}</span>
       </div>
       <div style="display:flex; justify-content: space-between; padding: 0 0px;">
          <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">Оплачено</span>
          <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">{{get_bron_check_data.payed_summ}}</span>
          <!-- <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">{{parseInt(checkInfo.cash) + parseInt(checkInfo.card)}}</span> -->
       </div>
       <div style="display:flex; justify-content: space-between; padding: 0 0px;">
          <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">Неоплачено</span>
          <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">{{get_bron_check_data.need_payed_summ}}</span>
          <!-- <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">{{parseInt(checkInfo.cash) + parseInt(checkInfo.card)}}</span> -->
       </div>
       <!-- <div v-show="checkInfo.cash!=0"  style="display:flex; justify-content: space-between; padding: 0 0px;">
          <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">Наличи</span>
          <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">{{get_cashSumm_Array[indexItem].all_sum}}</span>
       </div>
       <div v-show="checkInfo.card!=0" style="display:flex; justify-content: space-between; padding: 0 0px 7px 0px;  border-bottom: 1px dashed black;" >
          <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">Пластик</span>
          <span style="font-size: 16.5px;" class="paymentCheckPrintCheck">{{get_cashSumm_Array[indexItem].all_sum}}</span>
       </div> -->
       <div style="display:flex; justify-content: start; padding: 0; padding-top: 10px;">
        <h4 class="line_one paymentCheckPrintCheck">-----------------------------------------------------</h4>
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
  // import QrcodeVue from 'qrcode.vue'
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
        minut: new Date().toTimeString().slice(0, 8),
        dataItems: [],
        value: 'https://t.me/avitsenna_bot',
        // value: 'https://t.me/novo_medics_bot',
        size: 90,
      }
    },
    async mounted(){
        this.$forceUpdate();
        
        setTimeout(() => {
            window.print();
            this.$emit('closeCheck');
        }, 350);
  
        // setTimeout(() => {
        //   window.print();
        //   if(this.get_check_print_list.length == this.indexItem + 1){
        //     this.$emit('close');
        //   }
        //   else{
        //     this.$emit('closeNext');
        //   }
        // }, 350);
      
    },
    created() {
      this.$root.$refs.check = this;
    },
    methods: {
      ...mapActions(['fetch_unpayed_patient']),
      async printed(){      
        setTimeout(() => {
          if(this.get_check_print_list.length == 0){
            this.$emit('close');
          }
          else{
            window.print();
            
          }
        }, 350);
  
        // window.print()
        // this.$router.back()
      }
    },
  
    
    props: {
      options:{
        type: Array,
        default(){
          return []
        }
      },
      indexItem: {
        type: Number,
        default: 0,
      }
  
    },
    watch: {
      options(){
        this.allSumm = 0;
        this.allSumm = this.options.reduce((total, item) => {
            return total + item.summ
        }, 0)
      },
      
    },
    computed: {
      ...mapGetters(['get_bron_check_data','get_check_print_list', 'get_check_sum', 'debit_sum', 'checkInfo', 'get_cashSumm_Array']),
      summa(){
        return this.$store.state.cashPay_card
      },
      patientFio(){
        return this.$store.state.patient_name_for_cash
      },
      patientBorn(){
        return this.$store.state.patient_born_date_for_cash
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
  .line_one{
    font-weight:bold;
    padding:0 !important;
    margin:0 !important;
    /* text-overflow: ellipsis;  */
    overflow: hidden; 
    white-space: nowrap;
  }
  </style>
<template>
    <div>
      <div class="text-center">
        <h4 class="text-primary mt-0">{{option.reserved_name_1}} ( {{ option.reserved_name_2 }} )</h4>
        <h4 class="text-danger" v-if="option.need_payed_summ">{{option.need_payed_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</h4>
        <mdb-input v-model="CashMoney" :label="$t('cash')" size="md" outline type="number" />
        <mdb-input v-model="CardMoney" :label="$t('card')" size="md" outline type="number" />
      </div>
      <div class="text-right">
        <div @click="payDivece()">
          <mdb-btn color="success"  p="r4 l4 t2 b2" style="font-size:10px;">{{$t('pay')}} </mdb-btn>  
        </div>
      </div>
      <Toast ref="message"></Toast>
      <!-- <div id="print" v-show="false"> 
        <PrintCompanent />
      </div> -->
    </div>
  </template>
  
  <script>
  import {mdbBtn, mdbInput} from 'mdbvue'
  import {mapMutations, mapActions, mapGetters} from 'vuex'
  // import PrintCompanent from './check'
  
  export default {
    components: {
      mdbBtn,mdbInput
    },
    data(){
      return{
        CashMoney: null,
        CardMoney: null,
        database: [],
      }
    },
    computed: mapGetters(['get_code_patient']),
    methods: {
      ...mapActions(['fetch_get_code']),
      ...mapMutations([ 'Update_check_data', 'updateDebit', 'UpdatecheckInfo']),
      async payDivece(){
          if( this.CashMoney == null){
              this.CashMoney = 0;
            }
          if( this.CardMoney == null){
            this.CardMoney = 0;
          }
          const respon = await fetch(this.$store.state.hostname + '/HospitalBronRoomPayments/addPayment?id=' + this.option.id + 
          '&cash=' + this.CashMoney + '&card=' + this.CardMoney)
          const data = await respon.json()
          console.log('nima bor ekan')
          console.log(data)
          
          if(data.id){
            this.$emit('close', );
            this.CardMoney = null;
            this.CashMoney = null;
          }
          else{
            this.$refs.message.error('deffirent_sum')
          }
      },
      
      
    },
    props:{
      option: {
        type: Object,
        default(){
          return {}
        }
      }
    }
  }
  </script>
  
  <style>
  
  </style>
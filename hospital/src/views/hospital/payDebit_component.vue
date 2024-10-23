<template>
  <div>
    <div class="d-flex justify-content-center">
      <div style="width:600px;" class="p-3 shadow">
        <div class="text-center">
          <h4 class="text-primary" v-if="patient_info.id != 0">{{patient_info.fio}}</h4>
          <h4 class="text-danger">{{option.summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</h4>
          <mdb-input v-model="CashMoney" :label="$t('cash')" size="md" outline type="number" />
          <mdb-input v-model="CardMoney" :label="$t('card')" size="md" outline type="number" />
        </div>
        <div class="text-right border-bottom" v-if="btn_show">
          <div @click="payDivece()">
            <mdb-btn color="success"  p="r4 l4 t2 b2" style="font-size:10px;">{{$t('pay')}} </mdb-btn>  
          </div>
        </div>
      </div>
    </div>
    <div>
      <div class="TablePatientDocId p-3">
        <table class="myTabledebit">
          <thead>
            <tr class="header ">
              <th  width="40" class="text-left">№</th>
              <th width="200">{{$t('patient_name')}}</th>
              <th >{{$t('service_name')}}</th>
              <th >{{$t('doctor_name')}}</th>
              <th >{{$t('summa')}}</th>
              <th>{{$t('payed')}}</th>
              <th>{{$t('debit')}}</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(row,rowIndex) in option.list" :key="rowIndex">
              <td> <small style="font-size: 12.5px;">{{rowIndex+1}}</small> </td>
              <td> <small style="font-size: 12.5px;">{{row.patient_name}}</small> </td>
              <td> <small style="font-size: 12.5px;">{{row.service_name}}</small> </td>
              <td> <small style="font-size: 12.5px;">{{row.payments.discount_card_qty}}</small> </td>
              <td> <small style="font-size: 12.5px;">{{row.payments.Summ}}</small> </td>
              <td> <small style="font-size: 12.5px;">{{row.payments.PaymentInCash + row.payments.PaymentInCard}}</small> </td>
              <td> <small style="font-size: 12.5px;" class="text-danger">{{row.payments.dolg_summ}}</small> </td>
            </tr>
          </tbody>
        </table>
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
import {mapMutations} from 'vuex'
// import PrintCompanent from './check'

export default {
  components: {
    mdbBtn,mdbInput
  },
  data(){
    return{
      CashMoney: null,
      CardMoney: null,
      card: false,
      database: [],
      pay_sum: 0,
      dolg: 0,
      btn_show: true,
    }
  },
  methods: {
    ...mapMutations([ 'Update_check_data', 'UpdatePay_debit']),
    async payDivece(){
        this.btn_show = false;
        console.log(this.option)
        if( this.CashMoney == null){
          this.CashMoney = 0;
        }
        if( this.CardMoney == null){
          this.CardMoney = 0;
        }
        if((parseInt(this.CashMoney) + parseInt(this.CardMoney))>parseInt(this.option.summ))
        {
          this.$refs.message.error('summa_wrong')
          this.btn_show = true;
          return false;
        }
        this.pay_sum = parseInt(this.CashMoney) + parseInt(this.CardMoney);
        this.dolg = parseInt(this.option.summ) - parseInt(this.pay_sum)
        const respon = await fetch(this.$store.state.hostname + '/HospitalPatientDolgPaymentInfoes/getPaginationByPatientIdQarzdorlikTolash?page=0&size=1000&patient_id=' + this.patient_info.id + '&cash=' + this.CashMoney + '&card=' + this.CardMoney + '&payed_auth_id=' + localStorage.AuthId)
        const data = await respon.json()
        this.btn_show = true;
        if(respon.status == 201 || respon.status == 200){
          this.$emit('close', {list:data.items_list, pay_sum: this.pay_sum, dolg: this.dolg });
          console.log(data)
          this.CardMoney = null;
          this.CashMoney = null;
          this.btn_show = true;
        }
        
    },
    
    
  },
  props:{
    option: {
      type: Object,
      default(){
        return {}
      }
    },
    patient_info: {
      type: Object,
      default(){
        return {}
      }
    }
  }
}
</script>

<style lang="scss">
.myTabledebit {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; overflow-x:auto
}
.myTabledebit th{
  font-weight: 600;
  font-size:12px;
}
.myTabledebit td{
  font-size:13px;
}
.myTabledebit th, .myTabledebit td {
  text-align: left;
  padding: 3px 5px;
}

.myTabledebit tr {
  border-bottom: 1px solid rgb(240, 240, 240);
}
.myTabledebit tr.header, .myTabledebit tr:hover {
  background-color: #f1f1f1;
}
</style>
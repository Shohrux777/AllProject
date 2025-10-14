<template>
  <div class="info_davernost p-2" v-if="option">
    <div class="row mt-0 border-bottom">
      <div class="col-12 text-center">
        <p class="p-0 m-0 mt-2 font-weight-bold text-success" v-if="option.status_rasxod == 1" style="font-size: 14px;">Получать деньги</p>
        <p class="p-0 m-0 mt-2 font-weight-bold text-danger" v-else style="font-size: 14px;">Расходъ</p>
      </div>
    </div>
    <div class="row mt-1 border-bottom">
      <div class="col-4">
        <p class="p-0 m-0 mt-2 " style="font-size: 13px;">{{$t('worker')}}</p>
      </div>
      <div class="col-8 ">
        <p class="p-0 m-0 mt-2 text-right" style="font-size: 14px;">{{option.client_name}}</p>
      </div>
    </div>
    <div class="row mt-1 border-bottom">
      <div class="col-4">
        <p class="p-0 m-0 mt-2 " style="font-size: 13px;">{{$t('all')}} сумма</p>
      </div>
      <div class="col-8 ">
        <p v-if="option.status_rasxod == 1" class="p-0 m-0 mt-2 text-right text-success" style="font-size: 14px;">{{option.all_summ_string}}</p>
        <p v-else class="p-0 m-0 mt-2 text-right text-danger" style="font-size: 14px;">{{option.all_summ_string}}</p>
      </div>
    </div>

    <div class="row mt-1 border-bottom">
      <div class="col-4">
        <p class="p-0 m-0 mt-2 " style="font-size: 13px;">UZS</p>
      </div>
      <div class="col-8 ">
        <p v-if="option.status_rasxod == 1" class="p-0 m-0 mt-2 text-right" style="font-size: 14px;">{{option.sum_str}}</p>
        <p v-else class="p-0 m-0 mt-2 text-right" style="font-size: 14px;">{{option.sum_str}}</p>
      </div>
    </div>

    <div class="row mt-1 border-bottom">
      <div class="col-4">
        <p class="p-0 m-0 mt-2 " style="font-size: 13px;">USD</p>
      </div>
      <div class="col-8 ">
        <p class="p-0 m-0 mt-2 text-right" style="font-size: 14px;">{{option.dollor_string}}</p>
      </div>
    </div>
    <div class="row mt-1 border-bottom">
      <div class="col-4">
        <p class="p-0 m-0 mt-2 " style="font-size: 13px;">Курс</p>
      </div>
      <div class="col-8">
        <p class="p-0 m-0 mt-2 text-right" style="font-size: 14px;">{{option.kurs}}</p>
      </div>
    </div>
  
    <div class="row mt-1 border-bottom">
      <div class="col-4">
        <p class="p-0 m-0 mt-2" style="font-size: 13px;">{{$t('date')}}</p>
      </div>
      <div class="col-8 ">
        <p class="p-0 m-0 mt-2 text-right" style="font-size: 14px;"><span >{{option.created_date.slice(8,10) + '-' + option.created_date.slice(5,7) + '-' + option.created_date.slice(0,4)}}</span> <span class="ml-2">{{option.created_date.slice(11,16)}}</span></p>
      </div>
    </div>
    <div class="row mt-1 border-bottom">
      <div class="col-4">
        <p class="p-0 m-0 mt-2" style="font-size: 13px;">Кассир</p>
      </div>
      <div class="col-8 ">
        <p class="p-0 m-0 mt-2 text-right" v-if="option.addiotionala_information" style="font-size: 14px;">{{option.addiotionala_information}}</p>
        <p class="p-0 m-0 mt-2 text-right" v-else style="font-size: 14px;">Foydalanuvchi topilmadi</p>
      </div>
    </div>
    <div class="row mt-1 border-bottom">
      <div class="col-4">
        <p class="p-0 m-0 mt-2" style="font-size: 13px;">Счета</p>
      </div>
      <div class="col-8 ">
        <p class="p-0 m-0 mt-2 text-right" v-if="option.bot_id>0" style="font-size: 14px;">{{option.reserve}}</p>
        <p class="p-0 m-0 mt-2 text-right" v-else style="font-size: 14px;">Касса</p>
      </div>
    </div>
    <div class="row mt-1 border-bottom">
      <div class="col-4">
        <p class="p-0 m-0 mt-2" style="font-size: 13px;">{{$t('note')}}</p>
      </div>
      <div class="col-8 ">
        <p class="p-0 m-0 mt-2 text-right" style="font-size: 14px;">{{option.note}}</p>
      </div>
    </div>

    <div class="row mt-1 border-bottom">
      <div class="col-4">
        <p class="p-0 m-0 mt-2" style="font-size: 13px;">{{$t('ostatka')}}</p>
      </div>
      <div class="col-8 " v-if="option.reserve_val_1">
        <p class="p-0 m-0 mt-2 text-right" style="font-size: 14px;">{{option.reserve_val_1.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
      </div>
    </div>
    <div class="row mt-1 border-bottom">
      <div class="col-4">
        <p class="p-0 m-0 mt-2" style="font-size: 13px;">{{$t('ostatka')}} $</p>
      </div>
      <div class="col-8 " v-if="option.reserve_val_2">
        <p class="p-0 m-0 mt-2 text-right" style="font-size: 14px;">{{option.reserve_val_2.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
      </div>
    </div>
    
    
    <div class="row mt-5">
      <div class="col-12 border-bottom text-center">
        <img :src="hostname + option.phone_number"
        style="width:98%" alt=""
        @click="$imageModal.open(hostname + option.phone_number)">
      </div>
    </div>
    <!-- <div class="fixed" style="position:fixed; top: 50px; right:10px;">
      <mdb-btn color="info"  @click="printInfo" style="font-size: 10.5px"
        p="r4 l4 t2 b2">
      <mdb-icon  />{{$t('print')}}</mdb-btn>
    </div> -->
  </div>
</template>

<script>
import {mdbBtn, mdbIcon} from 'mdbvue'
export default {
  components:{
    mdbBtn, mdbIcon
  },
  data() {
    return {
      hostname: this.$store.state.server_ip,
    }
  },
props:{
  option:{
    type: Object,
    default() {
      return {}
    }
  }
},
methods:{
  printInfo(){
    this.$emit('printed', this.option)
  }
}
}
</script>

<style lang="scss" scoped>
.myTableInvoicelist {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; overflow-x:auto
}
.myTableInvoicelist th{
  font-weight: 600;
  font-size:11px;
}
.myTableInvoicelist td{
  font-size:11px;
  
}
.myTableInvoicelist td {
  text-align: left;
  padding: 5px 10px;
}
.myTableInvoicelist th{
  text-align: left;
  padding: 5px 10px;
}

.myTableInvoicelist tr {
  border-bottom: 1px dashed rgb(240, 240, 240);
  &:hover{
    background: #afd1fd;
  }
}

.myTableInvoicelist tr.header, .myTableInvoicelist tr:hover {
  // background-color: #f1f1f1;
}
</style>
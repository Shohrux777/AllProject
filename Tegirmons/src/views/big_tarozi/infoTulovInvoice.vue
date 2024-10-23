<template>
  <div class="infotulovInvoice" v-if="option">
    <div class="client_change_product_items px-3 mb-3 pb-4">
      <div class="row pt-3">
        <div class="col-3">
          <div class="client_ostatka_item">
            <div>
              <p class="product_name_item ">
                {{$t('client')}}
              </p>
            </div>
            <div class="d-flex justify-content-end">
              <p class="product_measure_item_change">
                {{option.text_2}}
              </p>
            </div>
          </div>
        </div>
        <div class="col-3">
          <div class="client_ostatka_item">
            <div>
              <p class="product_name_item ">
                {{option.text_1}}
              </p>
            </div>
            <div class="d-flex justify-content-end">
              <p class="product_measure_item_change">
                {{option.product_measure}} кг
              </p>
            </div>
          </div>
        </div>
        <div class="col-3">
          <div class="client_ostatka_item">
            <div>
              <p class="product_name_item ">
                {{$t('product_price')}}
              </p>
            </div>
            <div v-if="option.payed_summ" class="d-flex justify-content-end">
              <p class="product_measure_item_change">
                {{ (option.payed_summ/option.dolg_summ).toFixed() }} сўм
              </p>
            </div>
            <div v-else class="d-flex justify-content-end">
              <p class="product_measure_item_change">
                {{option.product_price}} сўм
              </p>
            </div>
          </div>
        </div>
        <div class="col-3">
          <div class="client_ostatka_item">
            <div>
              <p class="product_name_item ">
                {{$t('ostatka')}}
              </p>
            </div>
            <div class="d-flex justify-content-end">
              <p class="product_measure_item_change">
                {{option.credit_sum.toFixed(1)}} сўм
              </p>
            </div>
          </div>
        </div>
      </div>
      <div class="row pt-3">
        <div class="col-3">
          <div class="client_ostatka_item">
            <div class="d-flex justify-content-between border-bottom">
              <p class="product_name_item mt-2">
                {{option.text_1}}
              </p>
              <p class="product_measure_item_change">
                {{option.dolg_summ}} кг
              </p>
            </div>
            <div class="d-flex justify-content-center mt-2">
              <small class="product_measure_item">{{option.payed_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small>
              <small class="product_name_item mt-2 ml-2">сўм</small>
            </div>
          </div>
          <div class="client_ostatka_item mt-2">
            <div class="image_change_product">
              <div v-if="option.image_last_pull">
                <img :src="hostname + option.image_last_pull" style="width:100%;" alt="" class="shadow border rounded">
              </div>
              <svg v-else xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-camera" width="40" height="40" viewBox="0 0 24 24" stroke-width="1.5" stroke="#597e8d" fill="none" stroke-linecap="round" stroke-linejoin="round">
                <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                <path d="M5 7h1a2 2 0 0 0 2 -2a1 1 0 0 1 1 -1h6a1 1 0 0 1 1 1a2 2 0 0 0 2 2h1a2 2 0 0 1 2 2v9a2 2 0 0 1 -2 2h-14a2 2 0 0 1 -2 -2v-9a2 2 0 0 1 2 -2" />
                <path d="M9 13a3 3 0 1 0 6 0a3 3 0 0 0 -6 0" />
              </svg>
            </div>
          </div>
        </div>
        <div class="col-9">
          <div class="client_ostatka_item ">
            <div class="row">
              <div class="col-12 px-3">
                <div class="d-flex justify-content-between border-bottom">
                  <p class="product_name_item mt-2">
                    {{option.text_1}}
                  </p>
                  <p class="product_measure_item_change">
                    {{(option.product_measure - option.dolg_summ).toFixed(1)}} кг
                  </p>
                </div>
              </div>
              
            </div>
            <div class="p-2 MainKlientBigTaroziList_tulov_info">
              <table class="myTableMainKlientBigTaroziList ">
                <thead>
                  <tr class="header py-3 info_client_header_change">
                    <th  width="40" class="text-left">№</th>
                    <th>{{$t('product')}}</th>
                    <th>{{$t('measure')}}</th>
                    <th>Исполь. масса</th>
                    <th>Основные продукты</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(row,i) in option.item_list" :key="i">
                    <td>  <span >{{i+1}}</span></td>
                    <td><span > {{row.product.name}}</span></td>
                    <td> <span >{{row.qty}}</span></td>
                    <td> <span >{{(parseFloat(row.qty)/parseFloat(row.real_sum)).toFixed(1)}}</span> </td>
                    <td> <span >{{option.text_1}}</span> </td>
                  </tr>
                </tbody>
              </table>
            </div>
            <!-- <div class="d-flex justify-content-end">
              <div class="product_btn_item_change mt-3">
                <button class="btn_1 mr-1" @click="change_product_cancel()">{{$t('cancel')}}</button>
                <button :disabled="!payed_image" @click="func_change_product_accept()">{{$t('accept')}}</button>
              </div>
            </div> -->
          </div>
        </div>
      </div>
      <div class="row pt-3">
        <div class="col-5">
          <div class="client_ostatka_item">
            <div class="d-flex justify-content-between">
              <small  class="cashsher_info_small "> Кассир</small>
              <small class="cashsher_info_small_text ">{{option.auth_last_pull_name}}</small>
            </div>
            <div class="d-flex justify-content-between">
              <small  class="cashsher_info_small "> {{$t('date')}}</small>
              <small class="cashsher_info_small_text ">
                <span>{{option.date_last_pull.slice(8,10) + '-' + option.date_last_pull.slice(5,7) + '-' + option.date_last_pull.slice(0,4)}}</span> <span class="ml-2">{{option.date_last_pull.slice(11,16)}} </span>
              </small>
            </div>
            
          </div>
        </div>
      </div>
  </div>
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
.infotulovInvoice{
  background: #F1F4F5;
}
.client_change_product_items{
  min-height: 300px;
}
.client_ostatka_item{
  background: white;
  cursor:pointer;
  border-radius: 10px;
  box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
  padding: 10px 15px;
  position: relative;
}
.product_name_item{
  font-weight: 500;
  margin:0;
  margin-top: 5px;
  font-size: 16.5px;
  color:#67748E;
}
.cashsher_info_small{
  margin:0;
  margin-top: 3px;
  font-size: 12.5px;
  color:#818ca2;
}
.cashsher_info_small_text{
  margin:0;
  margin-top: 3px;
  font-size: 12.5px;
  color:#667289;
}
.product_measure_item_change{
  font-weight: 700;
  margin:0;
  margin-top: 7px;
  margin-left: 3px;
  font-size: 18px;
  color:#798496;
}
.MainKlientBigTaroziList_tulov_info{
  min-height: 250px;
  max-height: 250px;
  background: white;
  border-radius: 10px;
  overflow: hidden;
  overflow-y: scroll;
}
.myTableMainKlientBigTaroziList {
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  font-size: 16px;
  max-height:80px; overflow-x:auto
}
.myTableMainKlientBigTaroziList th{
  font-weight: 600;
  color:#9ea6b4;
  font-size:11px;
  background: #d5ecf3;
}
.info_client_header_change th{
  background: #ffc08c !important;
  color:#344767;
}
.myTableMainKlientBigTaroziList td{
  font-size:12px;
  color:#666f82;
  
}
.myTableMainKlientBigTaroziList tbody td small{
  font-size:11px;
}
.myTableMainKlientBigTaroziList td {
  text-align: left;
  padding: 3px 10px;
}
.myTableMainKlientBigTaroziList th{
  text-align: left;
  padding: 5px 10px;
}

.myTableMainKlientBigTaroziList tr {
  border-bottom: 1px dashed rgb(240, 240, 240);
  cursor:pointer;
  &:hover{
    background: #afd1fd;
  }
}
.info_client_header_change th{
  background: #ffc08c !important;
  color:#344767;
}
.product_btn_item_change button{
  outline: none;
  border: 1.4px solid #55ffad;
  border-radius: 6px;
  padding: 4px 12px;
  font-size: 13px;
  cursor:pointer;
  background: white;
  margin-left: 7px;
}
.product_btn_item_change button:hover{
  box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
  background: #55ffad;
  font-weight: bold;
  color: white;
}
.product_btn_item_change .btn_1{
  outline: none;
  border: 1.4px solid #fe735b;
  border-radius: 6px;
  padding: 4px 12px;
  font-size: 13px;
  cursor:pointer;
  background: white;
  margin-left: 7px;
}
.product_btn_item_change .btn_1:hover{
  box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
  background: #fe735b;
  color: white;
}
.image_change_product{
  min-height: 150px;
  display: flex;
  justify-content:center;
  align-items: center;
}
.product_measure_item{
  font-weight: 700;
  margin:0;
  margin-top: 7px;
  margin-left: 3px;
  font-size: 18px;
  color:#344767;
}
</style>
<template>
  <div class="order_info py-2">
    <div class="container-fluid">
      <p class="m-0 mb-1" style="font-size: 18px;">
        Заказ № {{ order_data.id }}
        <span style="font-size: 15px;" class="ml-5" v-if="order_data.create_date">Заказ создан:  
          <span style="color: #595961;" class="ml-1">{{  order_data.create_date.slice(8,10) + '-' + order_data.create_date.slice(5,7) + '-' + order_data.create_date.slice(0,4)}} </span>
           <span style="color: #595961;" class="ml-1">{{order_data.create_date.slice(11,16)}}</span>
        </span>
      </p>
      <div class="row mt-2">
        <div class="col-9">
          <div class="main_order_info d-flex w-100 mb-2">

            <div class="w-50 mr-2 p-2 px-3 card_n user_info_selected bg-white rounded">
              <p class="m-0 w-100 d-flex justify-content-between" v-if="order_data.tegirmonOrderClientid"><span class="title_name pr-1"> {{ $t('client') }}:</span>  <span>{{order_data.client_name}}</span></p>
              <p class="m-0 w-100 d-flex justify-content-between" v-else><span class="title_name pr-1"> {{ $t('client') }}:</span>  <span>{{order_data.shafyor_name}}</span></p>
              <p class="m-0 w-100 mt-1 d-flex justify-content-between" v-if="order_data.item_list"><span class="title_name pr-1">Tовары:</span>  <span>{{order_data.item_list.length }} товары</span></p>
              <p class="m-0 w-100 mt-1 d-flex justify-content-between" ><span class="title_name pr-1"> {{ $t('cash') }}:</span>  <span style="color: #5EB0A0; font-weight: bold;" v-if="order_data.sum">{{order_data.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span></p>
              <p class="m-0 w-100 mt-1 d-flex justify-content-between" ><span class="title_name pr-1"> {{ $t('dollor') }}:</span>  <span style="color: #5EB0A0; font-weight: bold;" v-if="order_data.dollor">{{order_data.dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span></p>
              <p class="m-0 w-100 mt-1 d-flex justify-content-between" ><span class="title_name pr-1"> Заказ создан:</span>  <span>{{order_data.user_name }}</span></p>
            </div>
            <div class="w-50 ml-1 p-2 px-3 card_n user_info_selected rounded bg-white">
              <p class="m-0 w-100 mt-1 d-flex justify-content-between" ><span class="title_name pr-1">Оплата:</span>  
                <span class="paid" v-if="order_data.isPaid">
                  Оплачено
                </span>
                <span v-else-if="order_data.pay_progress>0" class="nopaidprogress">
                  <span class="load_progress_text">{{order_data.pay_progress}} %</span> .
                  <small class="progress_change " :style="{ width: order_data.pay_progress + '%' }"></small>
                </span>
                <span v-else class="nopaid">
                  Не оплачено
                </span>
              </p>

              <p class="m-0 w-100 mt-1 d-flex justify-content-between" ><span class="title_name pr-1">Oператор погрузки:</span>  
                <span v-if="order_data.load_name">{{order_data.load_name }}</span>
                <span v-else>____________</span>
              </p>
              <p class="m-0 w-100 mt-1 d-flex justify-content-between" ><span class="title_name pr-1">Время погрузки:</span>  
                <span v-if="order_data.load_name">{{ 
                  order_data.loadedAt.slice(8,10) + '-' + order_data.loadedAt.slice(5,7) + '-' + order_data.loadedAt.slice(0,4)}}
                  <span  class="ml-1">{{order_data.loadedAt.slice(11,16)}}</span>  
                </span>
                <span v-else>____________</span>
              </p>

              <p class="m-0 w-100 mt-1 d-flex justify-content-between" ><span class="title_name pr-1">Оплата:</span>  
                <span class="paid" v-if="order_data.isLoaded && !order_data.is_loading">
                  Загружен
                </span>
                <span v-else-if="order_data.load_progress>0" class="nopaidprogress">
                  <span class="load_progress_text">{{order_data.load_progress}} %</span> .
                  <small :class="{'progress_change': !order_data.is_loading, 'progress_change_warning': order_data.is_loading}" :style="{ width: order_data.load_progress + '%' }"></small>
                </span>
                <span v-else class="nopaid">
                  Не загружен
                </span>
              </p>

              <p class="m-0 w-100 mt-1 d-flex justify-content-between" ><span class="title_name pr-1">Кассир:</span>  
                <span v-if="order_data.kassir_name">{{order_data.kassir_name }}</span>
                <span v-else>____________</span>
              </p>
              <p class="m-0 w-100 mt-1 d-flex justify-content-between" ><span class="title_name pr-1">Время оплаты:</span>  
                <span v-if="order_data.kassir_name">{{ 
                  order_data.paidAt.slice(8,10) + '-' + order_data.paidAt.slice(5,7) + '-' + order_data.paidAt.slice(0,4)}}
                  <span  class="ml-1">{{order_data.paidAt.slice(11,16)}}</span>  
                </span>
                <span v-else>____________</span>
              </p>
              
              
            </div>
            
          </div>
          <p class="m-0 mb-1" style="font-size: 15px;">
            Заказ товары
          </p>
          <div class="w-100 p-2 px-3 card_n user_info_selected bg-white rounded">
            <table class="w-100  myTableUserOrderInfo rounded my-2 px-2">
              <thead>
                <tr class="bg-gray-100 info_client_header">
                  <th style="width: 250px;" class=" text-left">{{ $t('product') }}</th>
                  <th class=" text-left">Оптом цена</th>
                  <th class=" text-left">{{$t('qty')}}</th>
                  <th class=" text-left">{{$t('price')}}</th>
                  <th class=" text-left">{{$t('sum')}}</th>
                  <th class=" text-left">Валюта</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="item in order_data.item_list" :key="item.id"  :style="`background: ${item.product.shitrix_code}`">
                  <td class="p-2">{{ item.product.name }}</td>
                  <td class="p-2">{{ item.sum_str }}</td>
                  <td class="p-2">{{ item.qty }}</td>
                  <td class="p-2" v-if="item.price">{{ item.price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</td>

                  <td class="p-2" v-if="item.sum">{{ item.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</td>
                  <td class="p-2">
                    <span v-if="item.price_type == 1">UZS</span>
                    <span v-else>USD</span>
                  </td>
                </tr>
                <tr class="alert-secondary">
                  <td class="p-2" colspan="2">Общий</td>
                  <td class="p-2" v-if="order_data.item_list">{{ order_data.item_list.reduce((total, item) => total + Number(item.qty || 0), 0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} шт</td>
                  <td class="p-2" v-if="order_data.item_list">{{ order_data.item_list.reduce((total, item) => total + Number(item.qty * item.product.real_qty || 0), 0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')  }} кг</td>
                  <td class="p-2" colspan="2"></td>
                  
                </tr>
              </tbody>
            </table>
          </div>

          <p class="m-0 mb-1 mt-2" style="font-size: 15px;">
            Инфо водителя
          </p>

          <div class="d-flex w-100">
            <div class="w-50 mb-2 p-2 mr-2 mb-2 card_n user_info_selected ortilgan_mashina rounded" v-for="(item,i) in car_order_list" :key="i"
            :class="{'ortilgan_active': item.is_begin}">
              <div class="row m-0 mb-2 border rounded">
                <div class="col-12  mb-0 px-2">
                  <span style="color:#313335; font-size: 12.5px;">
                    👨‍✈️ {{ $t("shafyor_name") }}:
                  </span>
                  <span style="color: #686A6E; font-size: 12.5px;">{{ item.shafyor_name }}</span>
                </div>
                
                <div class="col-6  mb-0 px-2">
                  <span style="color:#313335; font-size: 12.5px;">
                    🚗 Номер машины:
                  </span>
                  <span style="color: #686A6E; font-size: 12.5px;">{{ item.car_nomer }}</span>
                </div>
                <div class="col-6  mb-0 px-2">
                  <span style="color:#313335; font-size: 12.5px;">
                    📞 Телефон:
                  </span>
                  <span style="color: #686A6E; font-size: 12.5px;">{{ item.phone_number }}</span>
                </div>

                <div class="col-12  mb-0 px-2">
                  <span style="color:#313335; font-size: 12.5px;">
                    🧑‍💻 Oператор погрузки:
                  </span>
                  <span style="color: #686A6E; font-size: 12.5px;">{{ item.user_name }}</span>
                </div>

                <div class="col-6  mb-0 px-2">
                  <span style="color:#313335; font-size: 12.5px;">
                    🏗️ Карачи:
                  </span>
                  <span style="color: #686A6E; font-size: 12.5px;">{{ item.image_url }}</span>
                </div>
                <div class="col-6  mb-0 px-2">
                  <span style="color:#313335; font-size: 12.5px;">
                    ⏰ {{ $t("date") }}:
                  </span>
                  <span style="color: #686A6E; font-size: 12.5px;">{{ item.create_date.slice(8,10) }}-{{ item.create_date.slice(5,7) }}-{{ item.create_date.slice(0,4) }}  {{ item.create_date.slice(11,16) }} </span>
                </div>
                
              </div>
              <div v-if="item.item_list" class="w-100">
                <table class="myTableuserSalaryList w-100">
                    <thead>
                      <tr class="header py-0 info_client_header">
                        <th  width="30" class="text-left">№</th>
                        <th width="250">{{$t('product')}}</th>
                        <th>{{$t('qty')}}</th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr v-for="(row,rowIndex) in item.item_list" :key="row.id"
                      :class="{'zero_item': row.stockQty <= 0,}" :style="`background: ${row.product.shitrix_code}`">
                        <td> <small >{{rowIndex+1}}</small> </td>
                        <td> <small >{{row.product.name}}</small></td>
                        <td> <small >{{row.qty}}</small> </td>
                      </tr>
                    </tbody>
                  </table>
              </div>
            </div>
          </div>

        </div>
        <div class="col-3">
          <div class="orderCheckList " v-if="check_list">
            <div class="check_item card_n bg-white mb-3 p-2" v-for="(item, i) in check_list" :key="i"
            :class="{'alert-warning check_item_after' : !item.isInCashbox}">
              <div class="d-flex justify-content-center">
                <small class="summ_title" style="font-size: 13px;">
                  ЧЕК №{{item.id}}
                </small>
              </div>
              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Oбщий тулов суммаси
                </small>
                <small class="summ_title" style="color: #5EB0A0; font-weight: bold;">
                  + {{item.summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.cash" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;" v-if="item.hisob_id">
                  Перечисления
                </small>
                <small style="font-size: 13px;" v-else>
                  {{$t('cash')}}
                </small>
                <small class="summ_title">
                  {{(item.cash + item.real_sum).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.dollor" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('dollor')}}
                </small>
                <small class="summ_title">
                  {{(item.dollor + item.profit_summ).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>

              <div v-if="item.sum_balance" class=" text-success p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Balance UZS
                </small>
                <small class="summ_title text-success">
                  {{item.sum_balance.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.dollor_balance" class=" text-success p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Balance USD
                </small>
                <small class="summ_title text-success">
                  {{item.dollor_balance.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>

              <div v-if="item.uz_card" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  UzCard
                </small>
                <small class="summ_title">
                  {{item.uz_card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.humo" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Humo
                </small>
                <small class="summ_title">
                  {{item.humo.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.payme" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Payme
                </small>
                <small class="summ_title">
                  {{item.payme.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.click" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Click
                </small>
                <small class="summ_title">
                  {{item.click.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.paynet" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Paynet
                </small>
                <small class="summ_title">
                  {{item.paynet.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.uzumpay" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  UzumPay
                </small>
                <small class="summ_title">
                  {{item.uzumpay.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.skidka" class=" text-warning p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('skidka')}}
                </small>
                <small class="summ_title text-warning">
                  {{item.skidka.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>

              <div v-if="item.real_sum" class=" text-secondary p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Қайтим нақт
                </small>
                <small class="summ_title text-secondary">
                  {{item.real_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.profit_summ" class=" text-secondary p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Қайтим доллор
                </small>
                <small class="summ_title text-secondary">
                  {{item.profit_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>

              <div v-if="item.online" class=" text-secondary p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Қайтим {{' Balance UZS'}}
                </small>
                <small class="summ_title text-secondary">
                  {{item.online.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              
              <div v-if="item.perchisleniya" class=" text-secondary p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Қайтим {{' Balance USD'}}
                </small>
                <small class="summ_title text-secondary">
                  {{item.perchisleniya.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>

              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('hisob')}}
                </small>
                <small class="summ_title" v-if="item.hisob_id" style="font-size: 13px;">
                  {{item.hisob_name}}
                </small>
                <small class="summ_title" v-else style="font-size: 13px;">
                  Касса
                </small>
              </div>
              
              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('note')}}: 
                </small>
                <small class="summ_title text-right" :title="item.status_name" style="font-size: 13px; width: 200px !important;  cursor: pointer;">
                  {{item.status_name}}
                </small>
              </div>
              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Кассир
                </small>
                <small class="summ_title" style="font-size: 13px;">
                  {{item.image_url}}
                </small>
              </div>
              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('date')}}
                </small>
                <small v-if="item.create_date" class="summ_title" style="font-size: 13px;">
                  {{  item.create_date.slice(8,10) + '-' + item.create_date.slice(5,7) + '-' + item.create_date.slice(0,4) }}
                  {{ item.create_date.slice(11,16) }}
                </small>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data(){
    return{
      order_data: {},
      car_order_list: [],
      check_list: [],
    }
  },
  methods:{
    async fetchMounted(order){
      this.order_data = order;
      await this.fetchOrderCarItem(order.id);
      await this.fetchOrderCheck(order.id);
    },
    async fetchOrderCarItem(order_id){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonOrderCar/GetTegirmonOrderCarOrderId?order_id=" + order_id);
        const data = await response.json();
        if(response.status == 201 || response.status == 200)
        {
          this.car_order_list = data;
          return true;
        }
        else{
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        // this.client_list = [];
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
    },
    async fetchOrderCheck(order_id) {
      try {
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonOrderCheck/getPaginationOrderId?page=0&size=100&order_id=" + order_id);
        const data = await response.json();
        this.loading = false;
        if (response.ok) {
          console.log('check_list', data);
          this.check_list = data.items_list || [];
          this.noactiveCheckQty = data.items_list.filter(x => !x.isInCashbox).length;
          return this.check_list; // shu yerda qaytarib qo‘y
        }
      } catch (e) {
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
    },
  }
}
</script>

<style lang="scss" scoped>
.order_info{
  background: #f3f3f3;
}
.user_photo{
  background-color: #fff;
  border-radius: 50%;
  width: 45px;
  height: 45px;
  overflow: hidden;
}
.user_info_selected p{
  color: #595961;
  font-size: 14px;
}
.title_name{
  color: #48484d;

}
.card_n{
  box-shadow: rgba(9, 30, 66, 0.25) 0px 4px 8px -2px, rgba(9, 30, 66, 0.08) 0px 0px 0px 1px;
  border-radius: 5px;
}
.paid{
  width: 130px;
  text-align: center;
  color: #4c7419;
  font-weight: bold;
  border-radius: 4px;
  cursor: pointer;
  display: inline-block;
  font-size: 10px;
  padding: 1px 5px;
  border: 1px solid #86c732;
  background: #ebfdd5;
  font-weight: 600;
}
.nopaid{
  width: 130px;
  text-align: center;
  color: #fd6a6a;
  border-radius: 4px;
  cursor: pointer;
  display: inline-block;
  font-size: 10px;
  padding: 1px 5px;
  border: 1px solid #fd6a6a;
  background: #fff6f6;
  font-weight: 600;
}
.nopaidprogress{
  position: relative;
  width: 130px;
  text-align: center;
  color: #fff6f6;
  border-radius: 4px;
  cursor: pointer;
  display: inline-block;
  font-size: 10px;
  padding: 1px 5px;
  border: 1px solid #22b653;
  background: #fff6f6;
  font-weight: 600;
}
.load_progress_text{
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
  z-index: 111 !important;
  color: rgb(19, 62, 75);
}
.progress_change{
  position: absolute;
  left: 0;
  top: 0;
  bottom: 0;
  background: repeating-linear-gradient(
    60deg,
    #22b653,
    #28cc5f 20px
  );
  background-size: 40px 40px;
  animation: move 3s linear infinite;
  transition: width 2s ease;
  z-index: 1;
  border-radius: 3px;
}
.progress_change_warning{
  position: absolute;
  left: 0;
  top: 0;
  bottom: 0;
  background: repeating-linear-gradient(
    60deg,
    #ffba6c,
    #f7e580 20px
  );
  background-size: 40px 40px;
  animation: move 3s linear infinite;
  transition: width 2s ease;
  z-index: 1;
  border-radius: 3px;
}
@keyframes move {
  from {
    background-position: 0 0;
  }
  to {
    background-position: 40px 0px;
  }
}
.myTableUserOrderInfo {
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  overflow-y: scroll;
  font-size: 16px;
  max-height:80px; overflow-x:auto
}
.myTableUserOrderInfo th{
  font-weight: 600;
  font-size:11px;
}
.myTableUserOrderInfo td{
  font-size:11px;
}
.myTableUserOrderInfo tbody td small{
  font-size:10.5px;
}
.myTableUserOrderInfo td {
  text-align: left;
  padding: 3px 10px;
  // border-right: 1px solid rgb(219, 219, 219);
}
.myTableUserOrderInfo th{
  text-align: left;
  padding: 5px 10px;

}
.myTableorderClientInfo td {
  text-align: left;
  padding: 6px 10px;
  // border-right: 1px solid rgb(219, 219, 219);
}
.myTableUserOrderInfo tr {
  border-bottom: 1px solid rgb(219, 219, 219);
  &:hover{
    background: #afd1fd;
  }
}
.info_client_header{
  background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgb(200, 220, 230) 0%, rgb(196, 223, 228) 90.1% );
}
.ortilgan_mashina{
  background: #ffffff;
}
.ortilgan_active{
  background: #afdbe6;
}
.update_car_active{
  background: #c8d2ff;
}
.summ_title{
  font-size: 14px;
  color: #373738;
}
</style>
<template>
  <div class="addProductQtyPayed" >
    <transition name="slide-down">
      <div class="orderCheckList p-2" v-if="check_list.length">
        <div class="check_item card mb-3 px-3 py-2" v-for="(item, i) in check_list" :key="i"
        :class="{'alert-warning check_item_after' : !item.isInCashbox}">
          <div class="d-flex justify-content-center">
            <small class="summ_title" style="font-size: 13px;">
              ЧЕК №{{item.id}}
            </small>
          </div>
          <div class="summ_item_ p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              Oбщий тулов суммаси
            </small>
            <small class="summ_title ">
              {{item.summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
            </small>
          </div>
          <div v-if="item.cash" class="summ_item_ p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              {{$t('cash')}}
            </small>
            <small class="summ_title">
              {{(item.cash + item.real_sum).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
            </small>
          </div>
          <div v-if="item.dollor" class="summ_item_ p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              {{$t('dollor')}}
            </small>
            <small class="summ_title">
              {{(item.dollor + item.profit_summ).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
            </small>
          </div>

          <div v-if="item.sum_balance" class="summ_item_ text-success p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              Balance UZS
            </small>
            <small class="summ_title text-success">
              {{item.sum_balance.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
            </small>
          </div>
          <div v-if="item.dollor_balance" class="summ_item_ text-success p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              Balance USD
            </small>
            <small class="summ_title text-success">
              {{item.dollor_balance.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
            </small>
          </div>

          <div v-if="item.uz_card" class="summ_item_ p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              UzCard
            </small>
            <small class="summ_title">
              {{item.uz_card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
            </small>
          </div>
          <div v-if="item.humo" class="summ_item_ p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              Humo
            </small>
            <small class="summ_title">
              {{item.humo.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
            </small>
          </div>
          <div v-if="item.payme" class="summ_item_ p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              Payme
            </small>
            <small class="summ_title">
              {{item.payme.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
            </small>
          </div>
          <div v-if="item.click" class="summ_item_ p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              Click
            </small>
            <small class="summ_title">
              {{item.click.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
            </small>
          </div>
          <div v-if="item.paynet" class="summ_item_ p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              Paynet
            </small>
            <small class="summ_title">
              {{item.paynet.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
            </small>
          </div>
          <div v-if="item.uzumpay" class="summ_item_ p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              UzumPay
            </small>
            <small class="summ_title">
              {{item.uzumpay.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
            </small>
          </div>
          <div v-if="item.skidka" class="summ_item_ text-warning p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              {{$t('skidka')}}
            </small>
            <small class="summ_title text-warning">
              {{item.skidka.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
            </small>
          </div>

          <div v-if="item.real_sum" class="summ_item_ text-secondary p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              Қайтим нақт
            </small>
            <small class="summ_title text-secondary">
              {{item.real_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
            </small>
          </div>
          <div v-if="item.profit_summ" class="summ_item_ text-secondary p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              Қайтим доллор
            </small>
            <small class="summ_title text-secondary">
              {{item.profit_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
            </small>
          </div>

          <div v-if="item.online" class="summ_item_ text-secondary p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              Қайтим {{' Balance UZS'}}
            </small>
            <small class="summ_title text-secondary">
              {{item.online.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
            </small>
          </div>
          
          <div v-if="item.perchisleniya" class="summ_item_ text-secondary p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              Қайтим {{' Balance USD'}}
            </small>
            <small class="summ_title text-secondary">
              {{item.perchisleniya.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
            </small>
          </div>

          <div class="summ_item_ p-1 d-flex justify-content-between">
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
          
          <div class="summ_item_ p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              {{$t('note')}}
            </small>
            <small class="summ_title" style="font-size: 13px;">
              {{item.status_name}}
            </small>
          </div>
          <div class="summ_item_ p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              Кассир
            </small>
            <small class="summ_title" style="font-size: 13px;">
              {{item.image_url}}
            </small>
          </div>
          <div class="summ_item_ p-1 d-flex justify-content-between">
            <small style="font-size: 13px;">
              {{$t('date')}}
            </small>
            <small class="summ_title" style="font-size: 13px;">
              {{item.create_date}}
            </small>
          </div>
          <div class="summ_item_ p-1 d-flex justify-content-end" v-if="!item.isInCashbox">
            <mdb-btn color="success" style="font-size:9px; padding: 6px 0; width:100px;"  @click="getMoneyToCash(item)" class="m-0  mt-1" > {{$t('accept')}}</mdb-btn>
          </div>
        </div>
      </div>
    </transition>
    <div class="orderInfoPayment p-2">
      <div class="card">
        <div class="text-center mt-2" v-if="order.client">
          <h6 class="font-weight-bold">
            {{order.client.fio}}
          </h6>
        </div>
        
        <div class="mt-0 px-2 border-bottom mb-1">
          <span style="font-size: 14.5px;" class="ml-2">Общая сумма заказа</span>
        </div>
        <div class="mt-0">
          <div class="container-fluid pb-3 ">
            <div class="row px-2">
              <div class="col-6 pl-0 pr-1 ">
                <div class="order_sum dashed_border text-center rounded px-2 main_kassa_bg">
                  <p class="p-0 m-0 mb-1" style="font-size: 13px;">UZS</p>
                  <span class="order_sum_text text-white">{{ (order.sum || 0).toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span>
                </div>
              </div>
              <div class="col-6 pr-0 pl-1 ">
                <div class="order_sum dashed_border rounded text-center main_kassa_bg">
                  <p class="p-0 m-0 mb-1" style="font-size: 13px;">USD</p>
                  <span class="order_sum_text text-white">{{ (order.dollor || 0).toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="mt-1 px-2 border-bottom mb-1">
          <span style="font-size: 14.5px;" class="ml-2">Оплаченная сумма заказа</span>
        </div>
        <div class="mt-0">
          <div class="container-fluid pb-2">
            <div class="row px-2">
              <div class="col-6 pl-0 pr-1">
                <div class="order_sum dashed_border text-center main_kassa_poluchit rounded px-2">
                  <p class="p-0 m-0 mb-1" style="font-size: 13px;">UZS</p>
                  <span class="order_sum_text text-white">{{ payed_sum.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span>
                </div>
              </div>
              <div class="col-6 pr-0 pl-1">
                <div class="order_sum dashed_border rounded text-center main_kassa_poluchit">
                  <p class="p-0 m-0 mb-1" style="font-size: 13px;">USD</p>
                  <span class="order_sum_text text-white">{{ payed_dollor.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>

      </div>
    </div>

    <div class="p-4" style="position: relative;">
      <div class="acceptBoxPayed px-3 py-2 text-center">
        <h2 class="font-weight-bold">
          {{summ_str}}
        </h2>
        <!-- <input type="text" v-model="enterSumma" v-on:keyup.13 = "payed" pattern="^\$\d{1,3}(,\d{3})*(\.\d+)?$" @keyup="funcCurrency($event.target.value)" ref="enterSumma"  class="form-control form-control-md border mt-2" style="border:none; outline:none;font-weight:bold;" >
        <div class="text-right pt-2">
          <p class="m-0 p-0 zdachi text-danger">{{zdachiString}}</p>
        </div> -->
        <div class="container-fluid pb-3">
          <div class="row ">
            <div class="col-6 pl-0 pr-1">
              <div class="order_sum dashed_border main_kassa_balance rounded px-2">
                <p class="p-0 m-0 mb-1 text-white" style="font-size: 13px;">UZS</p>
                <span class="order_sum_text text-white">{{ order_sum.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span>
              </div>
            </div>
            <div class="col-6 pr-0 pl-1">
              <div class="order_sum dashed_border main_kassa_balance rounded">
                <p class="p-0 m-0 mb-1 text-white" style="font-size: 13px;">USD</p>
                <span class="order_sum_text text-white">{{ order_dollor.toFixed(2).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span>
              </div>
            </div>
          </div>
        </div>

        <div class="d-flex w-100 mt-2">
          <div class="skidka" style="width:25%">
            <div style="position:relative;">
              <input type="text" v-model="persantage_discountString" @keyup="funcPersantege($event.target.value)" v-on:click.capture="getNolSkidka" @blur="funcSkidkaBlue"
                class="form-control  border mt-2 text-right" style="border:none; outline:none;font-weight:bold; height:30px;" >
              <small style="position:absolute; font-size: 11.5px; top:-16px; left:3px;">
                {{$t('skidka')}}
              </small>
            </div>
            <div style="position:relative;">
              <input type="text" v-model="summ_str" disabled class="form-control  border mt-3 text-right" style="border:none; outline:none;font-weight:bold; height:30px;" >
              <small style="position:absolute; font-size: 11.5px; top:-16px; left:3px;">
                {{$t('Total')}}
              </small>
            </div>
            
            <div style="position:relative;">
              <input type="text" v-model="dollor_kurs_str" @keyup="funcDolKurs($event.target.value)"
                class="form-control  border mt-3 text-right" style="border:none; outline:none;font-weight:bold; height:30px;" >
              <small style="position:absolute; font-size: 11.5px; top:-16px; left:3px;">
                Курс
              </small> 
            </div>
            <div style="position:relative; margin-top: 10px;" class="d-flex">
              <span style="font-size: 13.5px; margin-top:2px;" class="ml-2">Pul kassada</span>
              <div class="custom-control custom-switch  pl-5" >
                <input v-model="isInCash" type="checkbox" class="custom-control-input "  id="customSwitch2" checked>
                <label class="custom-control-label status-style" for="customSwitch2" style="cursor:pointer;"></label>
              </div>
            </div>
            <div style="margin-top:15px;">
              <!-- <mdb-btn color="info" style="font-size:11px; padding: 6px 0;"   class="m-0  mt-1 w-100" > {{$t('print')}}</mdb-btn> -->
              <mdb-btn color="success" style="font-size:10px; padding: 6px 0;" :disabled="parseFloat(defaultSum.toFixed(2) - defaultOrtiqchaSum) > summa"  @click="payed"
                class="m-0 py-3 mt-1 w-100" > {{'Платить'}} </mdb-btn>
              <mdb-btn color="danger" style="font-size:10px; padding: 6px 0;"  @click="close" class="m-0  mt-1 w-100" > {{$t('cancel')}}</mdb-btn>
            </div>
          </div>
          <div style="width:75%;">
            <!-- bu naqd va dollor tulovlar uchun -->
            <div class="border rounded ml-4 px-1 pt-2">
              <div class="d-flex">
                <div class="price "  style="width:55%; position:relative;">
                  <div style="position:relative;">
                    <input type="text" v-model="cashInString"  v-on:keyup.13 = "payed" @keyup="funcCash($event.target.value)" v-on:click.capture="getNol" @blur="funcAllBlue" ref="cashIn"  
                    class="form-control  border mt-2 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                    <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                      {{$t('cash')}}
                    </small> 
                  </div> 
                </div>
                <div class="price ml-2"  style="width:45%; position:relative;">
                  <div style="position:relative;">
                    <input type="text" v-model="dollorInString" v-on:keyup.13 = "payed" @keyup="funcDollor($event.target.value)" v-on:click.capture="dollorNol" @blur="funcAllBlue" ref="clickIn" 
                    class="form-control  border mt-2 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                    <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold;" class="testing">
                      {{$t('dollor')}}
                    </small> 
                  </div>
                </div>
              </div>
              <div class="price "  style="width:100%; position:relative;">
                <div style="position:relative;" class="mt-2">
                  <erpSelectHisob
                    :options="allHisob.rows"
                    @select="selectOptionHisob"
                    :selected="hisob_name"
                    :label="$t('hisob')"
                    size="sm"
                  />
                </div>
              </div>
            </div>
            
            <!-- bu online tulovlar uchun -->
            <div class="border rounded ml-4 px-1 py-2 my-2">
              <div class="d-flex">
                <div class="price "  style="width:55%; position:relative;">
                  <div style="position:relative;">
                    <input type="text" v-model="uzcardInString" v-on:keyup.13 = "payed" @keyup="funcUzcard($event.target.value)" v-on:click.capture="uzcardNol" @blur="funcAllBlue"  ref="uzcardIn" 
                    class="form-control  border mt-2 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;">
                    <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                      {{'Uzcard'}}
                    </small>
                  </div>
                  <div style="position:relative;">
                    <input type="text" v-model="humoInString" v-on:keyup.13 = "payed" @keyup="funcHumo($event.target.value)" v-on:click.capture="humoNol" @blur="funcAllBlue"  ref="humoIn" 
                    class="form-control  border mt-3 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                    <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                      {{'Humo'}}
                    </small>
                  </div>
                  <div style="position:relative;">
                    <input type="text" v-model="paymeInString"  v-on:keyup.13 = "payed" @keyup="funcPayme($event.target.value)" v-on:click.capture="paymeNol" @blur="funcAllBlue" ref="cashIn"  
                    class="form-control  border mt-3 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                    <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                      {{'Payme'}}
                    </small> 
                  </div>  
                </div>
                <div class="price ml-2"  style="width:45%; position:relative;">
                  <div style="position:relative;">
                    <input type="text" v-model="clickedInString" v-on:keyup.13 = "payed" @keyup="funcClicked($event.target.value)" v-on:click.capture="clickedNol" @blur="funcAllBlue" ref="uzcardIn" 
                    class="form-control  border mt-2 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;">
                    <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                      {{'Click'}}
                    </small> 
                  </div>
                  <div style="position:relative;">
                    <input type="text" v-model="paynetInString" v-on:keyup.13 = "payed" @keyup="funcPaynet($event.target.value)" v-on:click.capture="paynetNol" @blur="funcAllBlue"  ref="humoIn" 
                    class="form-control  border mt-3 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                    <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                      {{'Paynet'}}
                    </small> 
                  </div>
                  <div style="position:relative;">
                    <input type="text" v-model="uzumInString" v-on:keyup.13 = "payed" @keyup="funcUzum($event.target.value)" v-on:click.capture="uzumNol" @blur="funcAllBlue"  ref="onlineIn"  
                    class="form-control  border mt-3 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                    <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold;" class="testing">
                      {{'UzumPay'}}
                    </small> 
                  </div>
                </div>
              </div>
            </div>

            <!-- bu client balansidan tulov qilish uchun -->
            <div class="border rounded ml-4 px-1 pt-2 my-2" v-show="client_info.id">
              <div class="d-flex">
                <div class="price "  style="width:55%; position:relative;">
                  <div style="position:relative;" >
                    <input type="text" v-model="onlineInString" v-on:keyup.13 = "payed" @keyup="funcOnline($event.target.value)" v-on:click.capture="onlineNol" @blur="funcAllBlue" ref="onlineIn"  
                    class="form-control  border mt-2 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                    <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold;" class="testing">
                      Balance UZS
                    </small> 
                  </div>
                  <div class="text-right">
                    <small class="p-0 text-success">{{ (client_info.sum || '').toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</small>
                  </div>
                </div>
                <div class="price ml-2"  style="width:45%; position:relative;">
                  <div style="position:relative;">
                    <input type="text" v-model="clickInString" v-on:keyup.13 = "payed" @keyup="funcClick($event.target.value)" v-on:click.capture="clickNol" @blur="funcAllBlue"  ref="clickIn" 
                    class="form-control  border mt-2 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                    <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold;" class="testing">
                      Balance USD
                    </small>
                  </div>
                  <div class="text-right" >
                    <small class="p-0 text-success">{{ (client_info.dollor || '').toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</small>
                  </div>
                </div>
              </div>
            </div>



            <div class="d-flex">
              <div class="price ml-4"  style="width:45%; position:relative;">
                
              </div>
            </div>

            <div class="price ml-4">
              <div style="position:relative;">
                <input type="text" v-model="note"
                class="form-control  border mt-3 pr-2" style="border:none; outline:none; height:30px; font-size: 14px;" >
                <small style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold;" class="testing">
                  {{$t('note')}}
                </small> 
              </div>
            </div>

            <div style="width:100%; position:relative;" class="pl-4 mt-2 pb-0 mb-2 ">
              <div class="ml-0 px-2 py-1 border-bottom border-top"
                :class="{'alert-success': parseFloat(defaultSum.toFixed(2)) > summa, 'alert-danger': parseFloat(defaultSum.toFixed(2)) < summa}">
                <div class="d-flex justify-content-between mt-0" style="font-size:13.5px;">
                  <span v-if="parseFloat(defaultSum.toFixed(2)) > summa" class="text-success"> Больше денег </span>
                  <span v-if="parseFloat(defaultSum.toFixed(2)) > summa" class="text-success">{{(parseFloat(defaultSum.toFixed())-summa).toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span>
                </div>
                <div class="d-flex justify-content-between mt-0" style="font-size:13.5px;">
                  <span v-if="parseFloat(defaultSum.toFixed(2)) < summa" class="text-danger"> Не хватить </span>
                  <span v-if="parseFloat(defaultSum.toFixed(2)) < summa" class="text-danger">{{(parseFloat(defaultSum.toFixed())-summa).toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span>
                </div>
              </div>
            </div>
            <div class="d-flex" v-if="parseFloat(defaultSum.toFixed(2)) > summa">
              <div style="width:55%;" class="pl-4">
                <div style="position:relative;">
                  <input type="text" v-model="naqd_returnInString"  v-on:keyup.13 = "payed" @keyup="funcNaqd_return($event.target.value)" v-on:click.capture="naqd_returnNol" @blur="funcAllBlue" ref="cashIn"  
                  class="form-control  border mt-2 text-right pr-2" style="border:none; outline:none;font-weight:bold; height:30px;" >
                  <small v-if="!hisob_status" style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold; " class="testing">
                    Қайтим нақт
                  </small>
                  <small v-else style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold;" class="testing">
                    Қайтим {{' Balance UZS'}}
                  </small> 
                </div>
              </div>
              <div style="width:45%;" class="pl-4">
                <div style="position:relative;">
                  <input type="text" v-model="dollor_returnInString " v-on:keyup.13 = "payed" @keyup="funcDollor_return($event.target.value)" v-on:click.capture="dollor_returnNol" @blur="funcAllBlue"  ref="uzcardIn" 
                  class="form-control  border mt-2 text-right pr-2" style="border:none; outline:none; font-weight:bold; height:30px;">
                  <small v-if="!hisob_status" style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold;" class="testing">
                    {{'Қайтим доллор'}}
                  </small> 
                  <small v-else style="position:absolute; top:-16px; left:3px; font-size:11.5px; font-weight:bold;" class="testing">
                    Қайтим {{' Balance USD'}}
                  </small> 
                </div>
              </div>
            </div>
            <div class="w-100 pl-4 mt-1 d-flex"  v-if="parseFloat(defaultSum.toFixed(2)) > summa && !hisob_name">
                <div class="custom-control custom-switch  pl-5" >
                  <input v-model="hisob_status" type="checkbox" class="custom-control-input "  id="customSwitch201" checked>
                  <label class="custom-control-label status-style" for="customSwitch201" style="cursor:pointer;"></label>
                </div>
                <span style="font-size: 13.5px; margin-top:2px; cursor: pointer;" class="ml-2" @click="hisob_status = !hisob_status">Klient balancega qaytarish.</span>

              </div>
          </div>
        </div>
      </div>
      
    </div>
    
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>

    <Toast ref="message"></Toast>
    <Alert ref="alert"></Alert>

    <mdb-modal :show="confirm" @close="confirm = false" size="sm" class="text-center" success>
      <mdb-modal-header center :close="false">
        <p class="heading">{{$t('Are_you_sure')}}</p>
      </mdb-modal-header>
      <mdb-modal-body>
        <mdb-icon icon="times" size="4x" class="animated rotateIn"/>
      </mdb-modal-body>
      <mdb-modal-footer center>
        <mdb-btn outline="success" @click="promise">{{$t('Yes')}}</mdb-btn>
        <mdb-btn outline="danger" @click="confirm = false">{{$t('No')}}</mdb-btn>
      </mdb-modal-footer>
    </mdb-modal>
  </div>
</template>

<script>
import erpSelectHisob from "../../components/erpSelect.vue";
import {mapActions, mapGetters, mapMutations} from 'vuex'
import {
  mdbIcon,
  mdbBtn,mdbInput,
  mdbModal, mdbModalHeader, mdbModalBody, mdbModalFooter
} from "mdbvue";
export default {
  components: {
    mdbIcon,
    mdbBtn,mdbInput,
    erpSelectHisob,
    mdbModal, mdbModalHeader, mdbModalBody, mdbModalFooter
  },
   props:{
    client_info: {
      type: Object,
      default(){
        return {
          id:null,
          fio:'',
          sum:0,
          dollor:0,
        }
      }
    },
  },
  data() {
    return {
      loading:false,
      modal_status: false,
      modal_info: '',
      summ_str : '0',
      summa: 0,
      confirm: false,
      select_check: {},


      persantage_discount: 0,
      persantage_discountString: '0',
      persantage_discount_default: 0,
      persantage_discount_diff: 0,
      enterSumma: null,
      zdachi:0,
      zdachiString:'0',
      currency: '',
      cashIn: 0,
      uzcardIn: 0,
      humoIn: 0,
      clickIn: 0,
      onlineIn: 0,
      totalIn: 0,
      payshow:false,

      defaultSum: 0,
      discountSum: 0,
      discount: 0,


      cashInString: '0',
      uzcardInString: '0',
      humoInString: '0',
      clickInString: '0',
      onlineInString: '0',

      dollorIn: 0,
      dollorInString: '0',
      paymeIn: 0,
      paymeInString: '0',
      clickedIn: 0,
      clickedInString: '0',
      paynetIn: 0,
      paynetInString: '0',
      uzumIn: 0,
      uzumInString: '0',

      dol_convert_Sum: 0,


      naqd_returnInString: '0',
      naqd_returnIn: 0,
      dollor_returnInString: '0',
      dollor_returnIn: 0,
      dollor_returnSumm: 0,

      defaultOrtiqchaSum: 0,

      dollor_kurs: 0,
      dollor_kurs_str: '0',
      summa_default: 0,
      order: {},
      order_sum:0,
      order_dollor:0,

      order_old_sum: 0,
      order_old_dollor: 0,
      hisob_name: '',
      hisob_id: 0,
      isInCash: true,
      hisob_status: false,
      note: '',
      check_list: [],
      noactiveCheckQty: 0,
      payed_sum: 0,
      payed_dollor: 0,

    }
  },
  created() {
    this.$root.$refs.order_payed = this;
  },
  async mounted() {
    console.log(this.summa)
    await this.fetchHisob();
  },
  computed:{
    ...mapGetters(['allOrderList', 'get_skidka_summ', 'get_all_summa', 
    'get_m_categoryIdProduct', 'get_zakaz_product_all_list','get_page_savat', 
    'get_product_qty', 'AllSummString','user_kassa_list', 'allHisob','hisob_info', 'user_kassa_info']),
  },
  methods: {
    ...mapActions([  'fetchCategoryIdProduct', 'fetchProductSearchByName', 'fetchKassa_userId', ,'fetchHisob', 'fetchHisob_info', 'fetchKassa_info']),
    ...mapMutations([ 'clear_order', 'input_change', 'changeSumma', 'update_zakaz_product_all_list', 'select_savat_page', 'add_savat_page', 'del_savat_page', 
      'updateCheckId', 'change_all_summa_skidka']),
    selectOptionHisob(option){
      this.hisob_name = option.name;
      this.hisob_id = option.id;
      this.hisob_status = true; // balancega qaytaradi
    },
    clw_cl(){
      this.naqd_returnInString = '0';
      this.naqd_returnIn = 0;
      this.dollor_returnInString = '0';
      this.dollor_returnIn = 0;
      this.dollor_returnSumm = 0;
      this.defaultOrtiqchaSum = 0;
    },
    async changingEnter(order_data){
      console.log('order_data')
      this.payed_sum = 0;
      this.payed_dollor = 0;
      this.dollor_kurs = localStorage.dollor_kurs;
      this.dollor_kurs_str = localStorage.dollor_kurs.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      this.order_sum =  0;
      this.order_dollor =  0;
      this.loading = false;
      this.check_list = [];
      this.noactiveCheckQty = 0;
      this.order = order_data.order;
      const checks = await this.fetchOrderCheck();
      await this.$nextTick();
      console.log('checks', checks)
      if(checks && checks.length > 0){
        this.dollor_kurs = checks[0].kurs;
        this.dollor_kurs_str = checks[0].kurs.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        await this.funcNotPaidSumma(order_data.sum,order_data.dollor);
      }
      else{
        this.order_sum =  order_data.sum;
        this.order_dollor =  order_data.dollor;
      }
      console.log('summalar chiqgandagi holati')
      console.log(this.order_sum)
      console.log(this.order_dollor)
      this.summa_default = this.order_sum + (this.order_dollor * this.dollor_kurs);
      this.summa = this.summa_default;
      this.summa = parseFloat(this.summa.toFixed())
      this.summ_str = this.summa_default.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

      this.cashIn = parseFloat(this.order_sum);
      this.cashInString = new Intl.NumberFormat().format(this.order_sum);
      console.log('this.summa_default', this.summa_default);
      if(this.order_dollor){
        this.dollorIn = this.order_dollor;
      }
      else{
        this.dollorIn = 0;
      }
      this.dol_convert_Sum = parseFloat(this.order_dollor * this.dollor_kurs);
      this.dollorInString = new Intl.NumberFormat().format(this.dollorIn);
      
      this.order_old_sum = 0;
      this.order_old_dollor = 0;
      this.zdachi = '0';
      this.uzcardIn = 0;
      this.humoIn = 0;
      this.onlineIn = 0;
      this.uzcardInString = '0';
      this.humoInString = '0';
      this.onlineInString = '0';
      this.paymeIn = 0;
      this.clickedIn = 0;
      this.paynetIn = 0;
      this.uzumIn = 0;

      this.paymeInString = '0';
      this.clickedInString = '0';
      this.paynetInString = '0';
      this.uzumInString = '0';

      this.naqd_returnInString = '0';
      this.naqd_returnIn = 0;
      this.dollor_returnInString = '0';
      this.dollor_returnIn = 0;
      this.dollor_returnSumm = 0;
      this.defaultOrtiqchaSum = 0;

      // balance dollor yozadigan qismi
      this.clickIn = 0;
      this.clickInString = '0';
      this.balance_dollor = 0;

      this.zdachiString = '0';
      this.zdachi = 0;
      this.persantage_discount = 0;
      this.persantage_discountString = '0';
      this.persantage_discount_default = 0;
      this.persantage_discount_diff = 0;

      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      
      this.hisob_name = '';
      this.hisob_id = 0;
      this.confirm = false;
      this.hisob_status = false;
      this.isInCash = true;
    },
    async fetchOrderCheck() {
      try {
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonOrderCheck/getPaginationOrderId?page=0&size=100&order_id=" + this.order.id);
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
    async funcNotPaidSumma(sum,dollor){
      let check_sum = 0;
      let check_dollor = 0;
      let check_skidka = 0;
      console.log('this.check_list', this.check_list)
      for(let i=0; i<this.check_list.length; i++){
        let temp_sum = 0;
        let temp_dollor = 0;
        temp_sum = this.check_list[i].cash + this.check_list[i].uz_card + this.check_list[i].click + this.check_list[i].humo + 
        this.check_list[i].payme + this.check_list[i].paynet + this.check_list[i].uzumpay + this.check_list[i].sum_balance - this.check_list[i].online;
        temp_dollor = this.check_list[i].dollor + this.check_list[i].dollor_balance - this.check_list[i].perchisleniya;
        check_sum += temp_sum;
        check_dollor += temp_dollor;
        check_skidka += this.check_list[i].skidka;
      }

        let diff_sum = 0;
        let diff_dollor = 0;
        let _order_old_sum = 0;
        let _order_old_dollor = 0;
        diff_sum = sum - check_sum - check_skidka;
        diff_dollor = dollor - check_dollor;
        this.payed_sum = check_sum;
        this.payed_dollor = check_dollor;

        let val = 0;
        if(diff_sum>0 && diff_dollor<0){
          val = (-1 * (diff_dollor * parseFloat(this.dollor_kurs)) - diff_sum);
          
          if (val <= 0)
          {
              _order_old_dollor = 0;
              _order_old_sum = (-1 * val).toFixed();
          }
          else
          {
            _order_old_sum = 0;
            _order_old_dollor = (-1 * (val / this.dollor_kurs));
          }
        }
        else if(diff_sum<0 && diff_dollor>0){
          val = (-1 * (diff_sum / this.dollor_kurs)) - diff_dollor;
            if (val <= 0)
            {
                _order_old_sum = 0;
                _order_old_dollor = (-1 * val);
            }
            else
            {
                _order_old_dollor = 0;
                _order_old_sum = (-1 * (val * this.dollor_kurs)).toFixed();
            }
        }
        else{
          _order_old_sum = diff_sum;
          _order_old_dollor = diff_dollor;
        }

        this.order_sum = parseFloat(_order_old_sum);
        this.order_dollor = parseFloat(_order_old_dollor);
        
        console.log('check_sum')
        console.log(_order_old_sum)
        console.log(_order_old_dollor)
    },
    async getMoneyToCash(check){
      console.log(check)
      this.select_check = check;
      // kassaga biriktirilganmi yuqmi tekshirish
      await this.fetchKassa_userId(localStorage.user_id);
      if(this.user_kassa_list.length){
        localStorage.kassa_id = this.user_kassa_list[0].id;
        localStorage.kassa_num = this.user_kassa_list[0].num_1;
        this.confirm = true;
      }
      else{
        this.$refs.alert.error('Bu foydalanuvchi kassaga biriktirilmagan, unda savdo qilish huquqi yuq !');
        localStorage.kassa_id = 0;
        localStorage.kassa_num = 0;
        return;
      }
      console.log('pulni qabul qilinganini bildiradi')
    },
    async promise(){
      this.select_check.isInCashbox = true;
      this.select_check.salary = this.noactiveCheckQty;
      this.select_check.tegirmonAuthid = localStorage.AuthId;
      this.select_check.image_url = localStorage.user_name;
      this.select_check.kassa_id = localStorage.kassa_id;
      console.log(this.select_check)
      try{
        const response = await fetch(`${this.$store.state.hostname}/TegirmonOrderCheck/${this.select_check.id}`, {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(this.select_check)
        });
        if(response.status == 201 || response.status == 200)
        {
          console.log(response);
          this.$emit('close', 1);
          return true;
        }
      }
      catch(error){
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
    },

    // tulovdan keyin qancha ostatka sum va dollor qolayotgani aniqlash
    async fetchHisobOstatkaSumma(){
      this.order_old_sum = 0;
      this.order_old_dollor = 0;
      let all_sum_money = 0;
      let all_dollor_money = 0;
      let diff_sum = 0;
      let diff_dollor = 0;
      all_sum_money = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn - parseFloat(this.naqd_returnIn) + parseFloat(this.persantage_discount);
      all_dollor_money = parseFloat(this.dollorIn) - parseFloat(this.dollor_returnIn);

      diff_sum = this.order_sum - all_sum_money;
      diff_dollor = this.order_dollor - all_dollor_money;
      console.log('sum',diff_sum);
      console.log('dollor',diff_dollor);
      let val = 0;
      if(diff_sum>0 && diff_dollor<0){
        val = (-1 * (diff_dollor * parseFloat(this.dollor_kurs)) - diff_sum);
        
        if (val <= 0)
        {
            this.order_old_dollor = 0;
            this.order_old_sum = -1 * val;
        }
        else
        {
          this.order_old_sum = 0;
          this.order_old_dollor = (-1 * (val / this.dollor_kurs)).toFixed();
        }
      }
      else if(diff_sum<0 && diff_dollor>0){
        val = (-1 * (diff_sum / this.dollor_kurs)) - diff_dollor;
          if (val <= 0)
          {
               this.order_old_sum = 0;
               this.order_old_dollor = (-1 * val).toFixed();
          }
          else
          {
              this.order_old_dollor = 0;
              this.order_old_sum = -1 * (val * this.dollor_kurs);
          }
      }
      else{
        this.order_old_sum = diff_sum;
        this.order_old_dollor = diff_dollor;
      }
      console.log('this.order_old_sum')
      console.log(this.order_old_sum)
      console.log(this.defaultSum)
    },
    
    async payed(){
      let return_sum = 0;
      let return_dollor = 0;
      let return_balance_sum = 0;
      let return_balance_dollor = 0;
      if(this.hisob_status == false){
        return_sum = parseFloat(this.naqd_returnIn);
        return_dollor = parseFloat(this.dollor_returnIn);
      }
      else{
        if((!this.client_info.id) && (this.naqd_returnIn>0 || this.dollor_returnIn>0)){
          this.$refs.alert.error("Bu klient ro'yxatga olinmagan uning hisobi mavjud emas !");
          return;
        }
        return_balance_sum = parseFloat(this.naqd_returnIn); 
        return_balance_dollor = parseFloat(this.dollor_returnIn);
      }
      // !!!!!!!!!! yana nimadir bor edi summa 0 bulsa hech qanday qaytim bulmasa tulov bulmasin
      if(this.defaultSum == 0 && this.naqd_returnIn == 0 && this.dollor_returnIn == 0)
      {
        return;
      }
      
      // !!!!!!!!!! bu yerga kurs yozilayabdimi tekshirish kerak dollor aralashgan bulsa
      
      if(this.isInCash == true && this.note == ''){
        this.note = "To'lov qilindi";
      }
      await this.fetchHisobOstatkaSumma();
      
      // kassaga biriktirilganmi yuqmi tekshirish
      await this.fetchKassa_userId(localStorage.user_id);
      if(this.user_kassa_list.length){
        localStorage.kassa_id = this.user_kassa_list[0].id;
        localStorage.kassa_num = this.user_kassa_list[0].num_1;
      }
      else{
        this.$refs.alert.error('Bu foydalanuvchi kassaga biriktirilmagan, unda savdo qilish huquqi yuq !');
        localStorage.kassa_id = 0;
        localStorage.kassa_num = 0;
        return;
      }

      // !!!!!!!!!! qaytim naqd berganda tekshirish kerak agar kassada qaytimga pul yetmasa puling kam deb chiqarsin
      if(return_sum || return_dollor){
        await this.fetchKassa_info(localStorage.kassa_id);
          if(return_dollor>this.user_kassa_info.dollor){
            this.$refs.alert.error('Kassada Dollor yetarli emas !');
            return;

          }
          else if(return_sum>this.user_kassa_info.cash){
            this.$refs.alert.error('Kassada Naqd pul yetarli emas !');
            return;
          }
      }
      try{
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "tegirmonAuthid": localStorage.AuthId,
          "tegirmonOrderClientid": this.client_info.id,
          "summ" : this.defaultSum, // tulanadigan summa sumda
          "cash": parseFloat(this.cashIn) - parseFloat(return_sum),
          "dollor": parseFloat(this.dollorIn) - parseFloat(return_dollor),
          "uz_card": parseFloat(this.uzcardIn),
          "click":  parseFloat(this.clickedIn), // click orqali tulov,
          "humo": parseFloat(this.humoIn),
          "payme": parseFloat(this.paymeIn), // paymen orqali tulov
          "skidka": parseFloat(this.persantage_discount), // skidka
          "paynet": parseFloat(this.paynetIn), // paynet orqali tulov
          "uzumpay": parseFloat(this.uzumIn), // uzum orqali tulov
          "kurs": parseFloat(this.dollor_kurs),
          "isInCashbox": this.isInCash,
          "TegirmonOrderid": this.order.id,
          "kassa_id": localStorage.kassa_id,
          
          "hisob_id": this.hisob_id,
          "hisob_name": this.hisob_name,

          "status_name": this.note + " << Optom savdo bo'limi >>",
          "image_url": localStorage.user_name,

          "remaining_sum": this.order_old_sum,
          "remaining_usd": this.order_old_dollor,
          "salary": this.noactiveCheckQty,

          "sum_balance": parseFloat(this.onlineIn), // client balance sum
          "dollor_balance": parseFloat(this.balance_dollor), //  client balance dollor

          "real_sum": parseFloat(return_sum), // qaytarilgan naqd
          "profit_summ": parseFloat(return_dollor), // qaytarilgan dollor

          "online": parseFloat(return_balance_sum), // client balance qaytim sum
          "perchisleniya": parseFloat(return_balance_dollor), //  client balance qaytim dollor

          // "uz_card": 0,     for skidka uchun ishlataman
        })
      };
      console.log('requestOptions.body')
      console.log(requestOptions.body)
      // try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonOrderCheck", requestOptions);
        
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          console.log('check')
          console.log(data)
          console.log(data.id)
          // this.$emit('print')
          this.$emit('close',1)
          // this.$emit('submitSuccess')
          return true;
        }
        else{
          const text = await response.text();
          this.modal_info = this.$i18n.t(text);
          this.modal_status = true;
          return false;
        }
      }
      catch{
        const text = await response.text();
        this.modal_info = this.$i18n.t(text);
        this.modal_status = true;
        return false;
      }
    },

    // Kalkulator bulimi
    funcCurrency(n){
      console.log(n)
       this.enterSumma = n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, " ")
       var temp = ''
       for(let i=0; i<this.enterSumma.length; i++){
        if(this.enterSumma[i] != ' '){
          temp += this.enterSumma[i];
        }
       }
        this.currency = parseInt(temp);
        this.zdachi = parseFloat(this.summa)-this.currency;
        this.zdachiString = this.zdachi.toFixed().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
    },

    // dollor kursini uzgartiri

    async funcDolKurs(n){
      // this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      // parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      // this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      // this.discountSum = parseFloat(this.discountSum.toFixed(2))
      
      // if(this.discountSum == 0){

      //   this.cashIn = 0;
      //   this.cashInString = ''; 
      //   n = n[n.length-1];

      // }

      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.dollor_kurs_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.dollor_kurs_str.length; i++){
        if(this.dollor_kurs_str[i] != ' '){
          temp += this.dollor_kurs_str[i];
        }
       }
      this.dollor_kurs = parseFloat(temp);
      localStorage.dollor_kurs = this.dollor_kurs;
      const checks = await this.fetchOrderCheck();
      await this.$nextTick();
      console.log('checks', checks)
      if(checks && checks.length > 0){
        await this.funcNotPaidSumma(this.order.sum,this.order.dollor);
      }
      this.summa_default = this.order_sum + (this.order_dollor * this.dollor_kurs);
      this.summa = this.summa_default;
      this.summ_str = this.summa_default.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

      this.dol_convert_Sum = this.dollor_kurs * parseFloat(this.dollorIn);
      this.clickIn = parseFloat(this.balance_dollor) *  this.dollor_kurs;
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();

      // dollor_kurs: localStorage.dollor_kurs,
      // dollor_kurs_str: localStorage.dollor_kurs.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
    
    },
    // yozganda hisoblash qismi
    funcCash(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))
      if(this.discountSum == 0){

        this.cashIn = 0;
        this.cashInString = ''; 
        n = n[n.length-1];

      }

      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.cashInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.cashInString.length; i++){
        if(this.cashInString[i] != ' '){
          temp += this.cashInString[i];
        }
       }
      this.cashIn = parseFloat(temp);
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcUzcard(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))
      console.log('this.discountSum')
      console.log(this.discountSum)
      if(this.discountSum == 0){
      console.log('this.discountSum')
      console.log('this.discountSum')

        this.uzcardIn = 0;
        this.uzcardInString = ''; 
        n = n[n.length-1];

      }

      console.log(n)
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.uzcardInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.uzcardInString.length; i++){
        if(this.uzcardInString[i] != ' '){
          temp += this.uzcardInString[i];
        }
       }
      this.uzcardIn = parseFloat(temp);
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcHumo(n){
       this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
       parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))

      console.log('this.discountSum')
      console.log(this.discountSum)
      this.discountSum = parseInt(this.discountSum)
      if(this.discountSum == 0){
      console.log('this.discountSum')
      console.log('this.discountSum')

        this.humoIn = 0;
        this.humoInString = ''; 
        n = n[n.length-1];

      }

      console.log(n)
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.humoInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.humoInString.length; i++){
        if(this.humoInString[i] != ' '){
          temp += this.humoInString[i];
        }
       }
      this.humoIn = parseFloat(temp);
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcDollor(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))

      if(this.discountSum == 0){
        this.dollorIn = 0;
        this.dollorInString = ''; 
        this.dol_convert_Sum = 0;
        n = n[n.length-1];
      }
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
      }

      this.dollorInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      var temp = ''
      for(let i=0; i<this.dollorInString.length; i++){
        if(this.dollorInString[i] != ' '){
          temp += this.dollorInString[i];
        }
      }
      this.dollorIn = parseFloat(temp);
      this.dol_convert_Sum = parseFloat(this.dollorIn) * parseFloat(this.dollor_kurs);
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcClick(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2));

      console.log('this.discountSum')
      console.log(this.discountSum)
      if(this.discountSum == 0){
      console.log('this.discountSum')
      console.log('this.discountSum')
        this.balance_dollor = 0;
        this.clickIn = 0;
        this.clickInString = ''; 
        n = n[n.length-1];

      }

      console.log(n)
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.clickInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.clickInString.length; i++){
        if(this.clickInString[i] != ' '){
          temp += this.clickInString[i];
        }
       }
      // this.clickIn = parseFloat(temp);
      this.balance_dollor = parseFloat(temp);
      if(this.client_info.dollor>0){
        if(this.client_info.dollor < this.balance_dollor){
          this.balance_dollor = this.client_info.dollor;
          this.clickInString = this.client_info.dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        }
      }
      else{
        this.balance_dollor = 0;
        this.clickInString = this.balance_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.clickIn = parseFloat(this.balance_dollor) * parseFloat(this.dollor_kurs);
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcOnline(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))

      console.log('this.discountSum')
      console.log(this.discount)
      console.log(this.discountSum)
      if(this.discountSum == 0){
      console.log('this.discountSum')
      console.log('this.discountSum')

        this.onlineIn = 0;
        this.onlineInString = ''; 
        n = n[n.length-1];

      }

      console.log(n)
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.onlineInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.onlineInString.length; i++){
        if(this.onlineInString[i] != ' '){
          temp += this.onlineInString[i];
        }
       }
      this.onlineIn = parseFloat(temp);
      if(this.client_info.sum>0){
        if(this.client_info.sum < this.onlineIn){
          this.onlineIn = this.client_info.sum;
          this.onlineInString = this.client_info.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        }
      }
      else{
        this.onlineIn = 0;
        this.onlineInString = this.onlineIn.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcPayme(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))
      if(this.discountSum == 0){
        this.paymeIn = 0;
        this.paymeInString = ''; 
        n = n[n.length-1];
      }

      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.paymeInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.paymeInString.length; i++){
        if(this.paymeInString[i] != ' '){
          temp += this.paymeInString[i];
        }
       }
      this.paymeIn = parseFloat(temp);
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcClicked(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))

      if(this.discountSum == 0){
        this.clickedIn = 0;
        this.clickedInString = ''; 
        n = n[n.length-1];
      }
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
      }

      this.clickedInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      var temp = ''
      for(let i=0; i<this.clickedInString.length; i++){
        if(this.clickedInString[i] != ' '){
          temp += this.clickedInString[i];
        }
      }
      this.clickedIn = parseFloat(temp);

      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcPaynet(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))

      if(this.discountSum == 0){
        this.paynetIn = 0;
        this.paynetInString = ''; 
        n = n[n.length-1];
      }
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
      }

      this.paynetInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      var temp = ''
      for(let i=0; i<this.paynetInString.length; i++){
        if(this.paynetInString[i] != ' '){
          temp += this.paynetInString[i];
        }
      }
      this.paynetIn = parseFloat(temp);

      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    funcUzum(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn);
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))

      if(this.discountSum == 0){
        this.uzumIn = 0;
        this.uzumInString = ''; 
        n = n[n.length-1];
      }
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
      }

      this.uzumInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      var temp = ''
      for(let i=0; i<this.uzumInString.length; i++){
        if(this.uzumInString[i] != ' '){
          temp += this.uzumInString[i];
        }
      }
      this.uzumIn = parseFloat(temp);

      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();
    },
    


    // tanlaganda ishlaydigan funksiya
    getNolSkidka(){
      this.discount = parseFloat(this.cashIn) + parseFloat(this.clickIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + parseFloat(this.dol_convert_Sum) + 
      parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa_default) - parseFloat(this.discount);
      this.discountSum = parseFloat(this.discountSum.toFixed())

      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa || 
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa 
      ){
        this.persantage_discount = this.summa_default + this.persantage_discount_default;   
        this.persantage_discountString = this.persantage_discount.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.persantage_discount_diff = parseFloat(this.persantage_discount) - parseFloat(this.persantage_discount_default);
        this.summ_str = '0';
        this.summa = 0;
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.clickIn = 0;
        this.clickInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';
        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = 0;
        this.dol_convert_Sum = 0;

        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = '0';
      }
      else if(this.discountSum > 0){
        this.persantage_discount = parseFloat(this.discountSum.toFixed(2)) + parseFloat(this.persantage_discount_default);
        this.persantage_discountString = this.persantage_discount.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.persantage_discount_diff = parseFloat(this.persantage_discount) - parseFloat(this.persantage_discount_default);
        this.summa = parseFloat(this.summa_default) - parseFloat(this.persantage_discount_diff)
        this.summa = parseFloat(this.summa.toFixed())
        this.summ_str = this.summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }

      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
      if(this.persantage_discount_diff != 0){
        this.change_all_summa_skidka({skidka: this.persantage_discount, summa: parseFloat(this.summa)})
      }
    },

    funcSkidkaBlue(){
      if(this.persantage_discountString == '' || this.persantage_discountString == null){
        this.persantage_discountString = '0',
        this.persantage_discount = 0;
        this.persantage_discount_diff = parseFloat(this.persantage_discount) - parseFloat(this.persantage_discount_default);
        this.summa = parseFloat(this.summa_default) + parseFloat(this.persantage_discount_default);
        this.summa = parseFloat(this.summa.toFixed())
        this.summ_str = this.summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }

      if(this.persantage_discount_diff != 0){
        this.change_all_summa_skidka({skidka: this.persantage_discount, summa: parseFloat(this.summa)})
      }
    },

    funcPersantege(n){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn) + parseFloat(this.persantage_discount);
      this.discountSum = parseFloat(this.summa_default) - parseFloat(this.discount) + parseFloat(this.persantage_discount_default);
      this.discountSum = parseFloat(this.discountSum.toFixed(2))
      if(this.discountSum == 0){

        this.persantage_discount = 0;
        this.persantage_discountString = '';

        n = n[n.length-1];

      }

      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }

       this.persantage_discountString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let i=0; i<this.persantage_discountString.length; i++){
        if(this.persantage_discountString[i] != ' '){
          temp += this.persantage_discountString[i];
        }
       }
       if(temp == '' || temp == null){
        this.persantage_discount = 0;
       }
       else{
        this.persantage_discount = parseFloat(temp);
       }
      this.summa = parseFloat(this.summa_default) + parseFloat(this.persantage_discount_default) - parseFloat(this.persantage_discount);
      this.summa = parseFloat(this.summa.toFixed());
      this.summ_str = this.summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      this.persantage_discount_diff = parseFloat(this.persantage_discount) - parseFloat(this.persantage_discount_default);
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;
      this.clw_cl();

      if(this.persantage_discount_diff != 0){
        this.change_all_summa_skidka({skidka: this.persantage_discount, summa: parseFloat(this.summa)})
      }
    },



    

    getNol(){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + parseFloat(this.dol_convert_Sum) + 
      parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn)
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);

      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa || 
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa
      ){
        this.cashIn = this.summa;
        this.cashInString = this.cashIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.clickIn = 0;
        this.clickInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';
        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = 0;
        this.dol_convert_Sum = 0;

        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = '0';

        
      console.log('this.cashIn')
      console.log(this.cashIn)
      console.log(this.dol_convert_Sum)

      }
      else if(this.discountSum > 0){
        this.cashIn = parseFloat(this.discountSum.toFixed(2));
        console.log(this.cashIn)
        this.cashInString = this.cashIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }

      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },
    uzcardNol(){
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + parseFloat(this.dol_convert_Sum) + 
      parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.uzcardIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa || 
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
        this.uzcardIn = this.summa;
        this.uzcardInString = this.uzcardIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.cashIn = 0;
        this.cashInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.clickIn = 0;
        this.clickInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';
        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = 0;
        this.dol_convert_Sum = 0;

        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = '0';
      console.log('this.uzcardIn')
      console.log(this.uzcardIn)

      }
      else if(this.discountSum > 0){
        this.uzcardIn = parseFloat(this.discountSum.toFixed(2));
        console.log(this.uzcardIn)
        this.uzcardInString = this.uzcardIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }

      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },
    humoNol(){
      // this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.onlineIn) + parseFloat(this.dol_convert_Sum) + 
      parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa || 
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
        this.humoIn = this.summa;
        this.humoInString = this.humoIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.clickIn = 0;
        this.clickInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';
        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = 0;
        this.dol_convert_Sum = 0;

        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = '0';
      console.log('this.humoIn')
      console.log(this.humoIn)

      }
      else if(this.discountSum > 0){
        this.humoIn = parseFloat(this.discountSum.toFixed(2));
        console.log(this.humoIn)
        this.humoInString = this.humoIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },
    onlineNol(){
      // this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
      this.discount = parseFloat(this.clickIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.dol_convert_Sum) + 
      parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discount)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa || 
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
        this.onlineIn = this.summa;
        this.onlineInString = this.onlineIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        if(this.client_info.sum > 0){
          if(this.client_info.sum < this.onlineIn){
            this.onlineIn = parseFloat(this.client_info.sum);
            this.onlineInString = this.onlineIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          }
        }
        else{
          this.onlineIn = 0;
          this.onlineInString = this.onlineIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        }
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.clickIn = 0;
        this.clickInString = '0';
        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = 0;
        this.dol_convert_Sum = 0;

        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = '0';
      console.log('this.onlineIn')
      console.log(this.onlineIn)

      }
      else if(this.discountSum > 0){
        this.onlineIn = parseFloat(this.discountSum.toFixed(2));
        console.log(this.onlineIn)
        this.onlineInString = this.onlineIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

        if(this.client_info.sum > 0){
          if(this.client_info.sum < this.onlineIn){
            this.onlineIn = parseFloat(this.client_info.sum);
            this.onlineInString = this.onlineIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          }
        }
        else{
          this.onlineIn = 0;
          this.onlineInString = this.onlineIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        }
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },
    dollorNol(){
      // this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
      this.discount = parseFloat(this.onlineIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.clickIn) +
      parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa || 
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
        if(this.dollor_kurs){
          this.dollorIn = (parseFloat(this.summa)/parseFloat(this.dollor_kurs)).toFixed(1);
        }
        else{
          this.dollorIn = 0;
        }
        this.dol_convert_Sum = this.summa
        this.dollorInString = new Intl.NumberFormat().format(this.dollorIn)
        this.clickIn = 0;
        this.clickInString = '0';
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = 0;

        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = '0';
        console.log('this.clickIn')
        console.log(this.clickIn)

      }
      else if(this.discountSum > 0){
        this.dol_convert_Sum = parseFloat(this.discountSum.toFixed(2));
        if(this.dollor_kurs){
          this.dollorIn = (parseFloat(this.dol_convert_Sum)/parseFloat(this.dollor_kurs)).toFixed(1);
        }
        else{
          this.dollorIn = 0;
        }
        this.dollorInString = new Intl.NumberFormat().format(this.dollorIn)
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },

    paymeNol(){
      this.discount = parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + parseFloat(this.clickIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa ||
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
        this.clickIn = 0;
        this.clickInString = '0';
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';
        this.dollorIn = 0;
        this.paymeIn = this.summa;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = 0;
        this.dol_convert_Sum = 0;


        this.dollorInString = '0';
        this.paymeInString = this.paymeIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = '0';
      console.log('this.clickIn')
      console.log(this.clickIn)

      }
      else if(this.discountSum > 0){
        this.paymeIn = parseFloat(this.discountSum.toFixed(2));
        this.paymeInString = this.paymeIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },
    clickedNol(){
      this.discount = parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + parseFloat(this.clickIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa ||
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
        this.clickIn = 0;
        this.clickInString = '0';
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';
        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = this.summa;
        this.paynetIn = 0;
        this.uzumIn = 0;
        this.dol_convert_Sum = 0;


        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = this.clickedIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.paynetInString = '0';
        this.uzumInString = '0';
      console.log('this.clickIn')
      console.log(this.clickIn)

      }
      else if(this.discountSum > 0){
        this.clickedIn = parseFloat(this.discountSum.toFixed(2));
        this.clickedInString = this.clickedIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },
    paynetNol(){
      this.discount = parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + parseFloat(this.clickIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa ||
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
        this.clickIn = 0;
        this.clickInString = '0';
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';

        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = this.summa;
        this.uzumIn = 0;

        this.dol_convert_Sum = 0;

        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = this.paynetIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        this.uzumInString = '0';
      console.log('this.clickIn')
      console.log(this.clickIn)

      }
      else if(this.discountSum > 0){
        this.paynetIn = parseFloat(this.discountSum.toFixed(2));
        this.paynetInString = this.paynetIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },
    uzumNol(){
      this.discount = parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.onlineIn) + parseFloat(this.clickIn) + 
      parseFloat(this.dol_convert_Sum) + parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa ||
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
        this.clickIn = 0;
        this.clickInString = '0';
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';

        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = this.summa;

        this.dol_convert_Sum = 0;

        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = this.uzumIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      console.log('this.clickIn')
      console.log(this.clickIn)

      }
      else if(this.discountSum > 0){
        this.uzumIn = parseFloat(this.discountSum.toFixed(2));
        this.uzumInString = this.uzumIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },

    clickNol(){
      // this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn;
      this.discount = parseFloat(this.onlineIn) + parseFloat(this.cashIn) + parseFloat(this.uzcardIn) + parseFloat(this.humoIn) + parseFloat(this.dol_convert_Sum) + 
      parseFloat(this.paymeIn) + parseFloat(this.clickedIn) + parseFloat(this.paynetIn) + parseFloat(this.uzumIn); 
      this.discountSum = parseFloat(this.summa) - parseFloat(this.discount);
      console.log('this.discountSum')
      console.log(this.discountSum)
      console.log(this.humoIn)
      if(this.cashIn == this.summa || this.uzcardIn == this.summa || this.humoIn == this.summa || this.clickIn == this.summa || this.onlineIn == this.summa ||
      this.dol_convert_Sum == this.summa || this.paymeIn == this.summa || this.clickedIn == this.summa || this.paynetIn == this.summa || this.uzumIn == this.summa){
        if(this.dollor_kurs){
          this.balance_dollor = (parseFloat(this.summa)/parseFloat(this.dollor_kurs)).toFixed(1);
        }
        else{
          this.balance_dollor = 0;
        }
        this.clickIn = this.summa;
        this.clickInString = this.balance_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        if(this.client_info.dollor > 0){
          if(this.client_info.dollor < this.balance_dollor){
            this.balance_dollor = parseFloat(this.client_info.dollor);
            this.clickIn = this.balance_dollor * parseFloat(this.dollor_kurs);
            this.clickInString = this.balance_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          }
        }
        else{
          this.balance_dollor = 0;
          this.clickIn = 0;
          this.clickInString = this.balance_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        }
        this.cashIn = 0;
        this.cashInString = '0';
        this.uzcardIn = 0;
        this.uzcardInString = '0';
        this.humoIn = 0;
        this.humoInString = '0';
        this.onlineIn = 0;
        this.onlineInString = '0';
        this.dollorIn = 0;
        this.paymeIn = 0;
        this.clickedIn = 0;
        this.paynetIn = 0;
        this.uzumIn = 0;
        this.dol_convert_Sum = 0;

        this.dollorInString = '0';
        this.paymeInString = '0';
        this.clickedInString = '0';
        this.paynetInString = '0';
        this.uzumInString = '0';
      console.log('this.clickIn')
      console.log(this.clickIn)

      }
      else if(this.discountSum > 0){
        this.clickIn = parseFloat(this.discountSum.toFixed(2));
        if(this.dollor_kurs){
          this.balance_dollor = (parseFloat(this.clickIn)/parseFloat(this.dollor_kurs)).toFixed(1);
        }
        else{
          this.balance_dollor = 0;
        }
        this.clickInString = new Intl.NumberFormat().format(this.balance_dollor)

        if(this.client_info.dollor > 0){
          if(this.client_info.dollor < this.balance_dollor){
            this.balance_dollor = parseFloat(this.client_info.dollor);
            this.clickIn = this.balance_dollor * parseFloat(this.dollor_kurs);
            this.clickInString = this.balance_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          }
        }
        else{
          this.balance_dollor = 0;
          this.clickIn = 0;
          this.clickInString = this.balance_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        }
      }
      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
      this.clw_cl();
    },

    funcAllBlue(){
      if(this.cashInString == null || this.cashInString == ''){
        this.cashIn = 0;
        this.cashInString = '0';
      }
      else if(this.onlineInString == null || this.onlineInString == ''){
        this.onlineIn = 0;
        this.onlineInString = '0';
      }
      else if(this.uzcardInString == null || this.uzcardInString == ''){
        this.uzcardIn = 0;
        this.uzcardInString = '0';
      }
      else if(this.humoInString == null || this.humoInString == ''){
        this.humoIn = 0;
        this.humoInString = '0';
      }
      else if(this.clickInString == null || this.clickInString == ''){
        this.clickIn = 0;
        this.clickInString = '0';
      }
      else if(this.paymeInString == null || this.paymeInString == ''){
        this.paymeIn = 0;
        this.paymeInString = '0';
      }
      else if(this.clickedInString == null || this.clickedInString == ''){
        this.clickedIn = 0;
        this.clickedInString = '0';
      }
      else if(this.paynetInString == null || this.paynetInString == ''){
        this.paynetIn = 0;
        this.paynetInString = '0';
      }
      else if(this.uzumInString == null || this.uzumInString == ''){
        this.uzumIn = 0;
        this.uzumInString = '0';
      }
      else if(this.dollorInString == null || this.dollorInString == ''){
        this.dollorIn = 0;
        this.dollorInString = '0';
        this.dol_convert_Sum = 0;
      }
      else if(this.dollor_returnInString == null || this.dollor_returnInString == ''){
        this.dollor_returnIn = 0;
        this.dollor_returnInString = '0';
        this.dollor_returnSumm = 0;
      }
      else if(this.naqd_returnInString == null || this.naqd_returnInString == ''){
        this.naqd_returnIn = 0;
        this.naqd_returnInString = '0';
      }

      this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn + this.dol_convert_Sum;

      this.defaultOrtiqchaSum = this.naqd_returnIn + this.dollor_returnSumm;
    },

    naqd_returnNol(){
      let ortiqchaSum = parseFloat(this.defaultSum.toFixed(2)) - this.summa;
      // this.dollor_returnSumm = parseFloat(this.dollor_kurs) * this.dollor_returnIn;
      let deffSum = parseFloat(ortiqchaSum) - parseFloat(this.dollor_returnSumm)
      if(this.naqd_returnIn == ortiqchaSum || this.dollor_returnSumm == ortiqchaSum ){
        this.naqd_returnIn = ortiqchaSum;
        this.naqd_returnInString = ortiqchaSum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

        this.dollor_returnIn = 0;
        this.dollor_returnInString = '0';
        this.dollor_returnSumm = 0;
      }
      else if(deffSum > 0){
        this.naqd_returnIn = deffSum;
        this.naqd_returnInString = this.naqd_returnIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultOrtiqchaSum = this.naqd_returnIn + this.dollor_returnSumm;
      // this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      // this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
    },

    dollor_returnNol(){
      let ortiqchaSum = parseFloat(this.defaultSum.toFixed(2)) - this.summa;
      let deffSum = parseFloat(ortiqchaSum) - parseFloat(this.naqd_returnIn);
      if(this.naqd_returnIn == ortiqchaSum || this.dollor_returnSumm == ortiqchaSum ){
        this.naqd_returnIn = 0;
        this.naqd_returnInString = '0';
        let dollor_change = (parseFloat(ortiqchaSum) / parseFloat(this.dollor_kurs)).toFixed(1);
        this.dollor_returnSumm = parseFloat(ortiqchaSum);
        this.dollor_returnIn = dollor_change;
        this.dollor_returnInString = this.dollor_returnIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      else if(deffSum > 0){
        let dollor_change = (parseFloat(deffSum) / parseFloat(this.dollor_kurs)).toFixed(1);
        this.dollor_returnSumm = parseFloat(deffSum);
        this.dollor_returnIn = dollor_change;
        this.dollor_returnInString = this.dollor_returnIn.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.defaultOrtiqchaSum = this.naqd_returnIn + this.dollor_returnSumm;
      // this.defaultSum = this.cashIn + this.uzcardIn + this.humoIn + this.clickIn + this.onlineIn + 
      // this.dol_convert_Sum + this.paymeIn + this.clickedIn + this.paynetIn + this.uzumIn;
    },

    funcNaqd_return(n){
      
      let disCountSum = parseFloat(this.naqd_returnIn) + parseFloat(this.dollor_returnSumm)
      let ortiqchaSum = parseFloat(this.defaultSum.toFixed(2)) - this.summa; // ortiqcha qaytarish kerak bulgan summa

      let deffSum = parseFloat(ortiqchaSum) - parseFloat(disCountSum);

      if(deffSum == 0){
        this.naqd_returnIn = 0;
        this.naqd_returnInString = '0'; 
        n = n[n.length-1];
      }
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
      }

      this.naqd_returnInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      var temp = ''
      for(let i=0; i<this.naqd_returnInString.length; i++){
        if(this.naqd_returnInString[i] != ' '){
          temp += this.naqd_returnInString[i];
        }
      }
      this.naqd_returnIn = parseFloat(temp);
      this.dollor_returnIn = 0;
      this.dollor_returnInString = '0';
      this.dollor_returnSumm = 0;
      this.defaultOrtiqchaSum = this.naqd_returnIn + this.dollor_returnSumm;
    },

    funcDollor_return(n){
      
      let disCountSum = parseFloat(this.naqd_returnIn) + parseFloat(this.dollor_returnSumm)
      let ortiqchaSum = parseFloat(this.defaultSum.toFixed(2)) - this.summa; // ortiqcha qaytarish kerak bulgan summa

      let deffSum = parseFloat(ortiqchaSum) - parseFloat(disCountSum);

      if(deffSum == 0){
        this.dollor_returnIn = 0;
        this.dollor_returnInString = '0';
        this.dollor_returnSumm = 0;
        n = n[n.length-1];
      }
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
      }

      this.dollor_returnInString = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      var temp = ''
      for(let i=0; i<this.dollor_returnInString.length; i++){
        if(this.dollor_returnInString[i] != ' '){
          temp += this.dollor_returnInString[i];
        }
      }
      this.dollor_returnIn = parseFloat(temp);
      this.dollor_returnSumm = this.dollor_returnIn * parseFloat(this.dollor_kurs);
      
      this.naqd_returnIn = 0;
      this.naqd_returnInString = '0';

      this.defaultOrtiqchaSum = this.naqd_returnIn + this.dollor_returnSumm;
    },


    close(){
      this.$emit('close')
    }
  },
}
</script>

<style lang="scss" scoped>
.testing{
  color: rgb(82, 82, 82);
}
 .addProductQtyPayed{
    position: fixed;
    display: flex;
    align-items: center;
    justify-content: center;
    top:0;
    left:0;
    width: 100%;
    height: 100vh;
    background: rgba(51, 51, 51, 0.75);
    z-index: 1111;
  }
  .acceptBoxPayed{
    width:660px;
    max-height: 630px;
    background: rgb(252, 252, 255);
    box-shadow: 3px 2px 5px rgb(129, 129, 129);
    border-radius: 5px;
    
  }
  .orderCheckList{
    position: absolute;
    top:0;
    right: 0;
    width:400px;
    height: 100vh;
    background: inherit;
    overflow: hidden;
    overflow-y: scroll;
  }
  .orderInfoPayment{
    position: absolute;
    top:0;
    left: 0;
    width:450px;
    height: 100vh;
  }
  .bg_dark{
    background: #202020;
  }
  .bg_danger{
    background: #fb6b6b;
  }
  .dashed_border{
    border:1px dashed #4d4d4d;
  }
  .order_sum_text{
    color: #4d4d4d;
    font-weight: bold;
    font-size: 17px;
  }
  .summ_title{
    font-size: 15px;
    font-style: italic;
    color: #3c3669;
    font-weight: bold;
  }
  .summ_item_{
    border-bottom: 1px dashed rgb(110, 110, 110);
  }
  .check_item{
    position: relative;
  }
  .check_item_after::after {
  content: "";
  position: absolute;
  top: 6px;                 /* tepaga qarab joylashuvni sozlang */
  right: 6px;               /* o'ngga qarab joylashuvni sozlang */
  width: 10px;
  height: 10px;
  border-radius: 50%;
  background: #ff3b30;      /* qizil rang */
  box-shadow: 0 0 6px rgba(255,59,48,.6);
  animation: badge-blink 1s infinite; /* yonib - o‘chish */
  pointer-events: none;     /* ustiga click o‘tmasin */
}

/* oddiy blink animatsiya */
@keyframes badge-blink {
  0%, 100% { opacity: 1; transform: scale(1); }
  50%      { opacity: 0.15; transform: scale(0.85); }
}
.slide-down-enter-active,
.slide-down-leave-active {
  transition: all 1s ease;
}

.slide-down-enter {
  transform: translateY(-100%);
  opacity: 0;
}

.slide-down-enter-to {
  transform: translateY(0);
  opacity: 1;
}

.slide-down-leave {
  transform: translateY(0);
  opacity: 1;
}

.slide-down-leave-to {
  transform: translateY(-100%);
  opacity: 0;
}
.main_kassa_poluchit{
  background-image: linear-gradient( 90deg,  rgb(117, 207, 165) 24.3%, rgb(93, 236, 177) 78.3% );
}
.main_kassa_bg{
  //background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgba(239,87,87,0.74) 0%, rgba(245,123,13,0.74) 90.2% );
  background-image: linear-gradient( 90deg,  rgba(255,157,129,1) 24.3%, rgb(255, 110, 78) 78.3% );
}
.main_kassa_warning{
  background-image: linear-gradient( 90deg,  rgb(219, 193, 119) 24.3%, rgb(255, 177, 88) 78.3% );
}
.main_kassa_balance{
  background-image: linear-gradient( 90deg,  rgb(255, 110, 110) 24.3%, rgb(236, 93, 129) 78.3% );
}
.hisob_bg{
  background-image: radial-gradient( circle 502px at 2.7% 23.7%,  rgba(142,5,254,1) 0%, rgba(33,250,214,1) 99.6% );
}
</style>
<template>
  <div class="checkList">
    <div class="order_main_head d-flex justify-content-between">
      <div>
          <span v-for="(item,i) in order_link" :key="i" :class="{'bg-active_link': active_link == i}"
          @click="toggleLink(i)">
          {{item.name}}
          <!-- <mdb-badge class="ml-1" style="padding: 3px 8px; font-size: 11px;" pill :style="{ background: item.color + '!important' }">{{item.qty}}</mdb-badge> -->
          </span>
      </div>
    </div>

    <!-- Kun tanlash (hamma bo'limlar uchun umumiy filter) -->
    <div class="container-fluid filter_bg py-1">
      <div class="row">
        <div class="col-3 px-1">
          <input
            type="date"
            v-model="start_time"
            class="form-control form-control-sm"
            @change="onDateChange"
          >
        </div>
        <div class="col-3 px-1" v-if="isAdminAccess">
          <select
            v-model="selected_kassa_id"
            class="form-control form-control-sm"
            @change="onDateChange"
          >
            <option :value="''">Hamma kassalar</option>
            <option
              v-for="kassa in allKassa.rows"
              :key="kassa.id"
              :value="kassa.id"
            >
              {{ kassa.name + " ( " + kassa.text_1 + " ) " || kassa.note || ('Kassa #' + kassa.id) }}
            </option>
          </select>
        </div>
      </div>
    </div>
    <div class="sotuv_checks">

    </div>

    <div class="sotuv_checks container-fluid mt-2" v-if="active_link == 0">
      <div class="row">
        <div class="col-3 px-1"  v-for="(item, i) in sell_check_list" :key="i">
          <div class="check_item card_n bg-white mb-3 p-2">
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
                <small style="font-size: 13px;">
                  {{$t('cash')}}
                </small>
                <small class="summ_title">
                  {{(item.cash + item.srogi_otganlar_uchun_rasxod).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.real_sum" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('dollor')}}
                </small>
                <small class="summ_title">
                  {{(item.real_sum + item.salary).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>

             

              <div v-if="item.card" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  UzCard
                </small>
                <small class="summ_title">
                  {{item.card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
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
              <div v-if="item.uz_card" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Payme
                </small>
                <small class="summ_title">
                  {{item.uz_card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.perchisleniya" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Click
                </small>
                <small class="summ_title">
                  {{item.perchisleniya.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.creadit_payed" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Paynet
                </small>
                <small class="summ_title">
                  {{item.creadit_payed.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.rasxod" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  UzumPay
                </small>
                <small class="summ_title">
                  {{item.rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.online" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Перечисления
                </small>
                <small class="summ_title">
                  {{item.online.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.dolg" class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Пластикка ўтказма
                </small>
                <small class="summ_title">
                  {{item.dolg.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.profit_summ" class=" text-warning p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('skidka')}}
                </small>
                <small class="summ_title text-warning">
                  {{item.profit_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>

              <div v-if="item.srogi_otganlar_uchun_rasxod" class=" text-danger p-1 d-flex justify-content-between">
                <small style="font-size: 13px;" >
                  Қайтим нақт
                </small>
                <small class="summ_title  text-danger">
                  {{item.srogi_otganlar_uchun_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.salary" class=" text-danger p-1 d-flex justify-content-between">
                <small style="font-size: 13px;" >
                  Қайтим доллор
                </small>
                <small class="summ_title text-danger text-danger">
                  {{item.salary.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
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
              
              <!-- <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('note')}}
                </small>
                <small class="summ_title" :title="item.status_name" style="font-size: 13px; width: 100px !important; overflow: hidden; height: 20px; cursor: pointer;">
                  {{item.status_name}}
                </small>
              </div> -->
              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Кассир
                </small>
                <small class="summ_title" style="font-size: 13px;">
                  {{item.image_base_64}}
                </small>
              </div>
              <!-- <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('client')}}
                </small>
                <small class="summ_title" style="font-size: 13px;">
                  {{item.client_name}}
                </small>
              </div> -->
              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('date')}}
                </small>
                <small v-if="item.create_date" class="summ_title" style="font-size: 13px;">
                  {{  item.create_date.slice(8,10) + '-' + item.create_date.slice(5,7) + '-' + item.create_date.slice(0,4) }}
                  {{ item.create_date.slice(11,16) }}
                </small>
              </div>
              <div class="d-flex justify-content-end border-top">
                <mdb-btn v-if="canEdit(item)"  class="m-0 mb-1 mt-2 mr-2"  size="sm" outline="success" style="font-size:9px; height:30px;" 
                  @click="editSellCheck(item)">
                  ✏️ Изменить
                </mdb-btn>
                <mdb-btn  class="m-0 mb-1 mt-2"  size="sm" outline="info" style="font-size:9px; height:30px;" @click="printSellCheckInfo(item)">
                  Печат
                </mdb-btn>
              </div>
            </div>
        </div>
      </div>
    </div>

    <div class="optom_savdo_ckecks container-fluid mt-2" v-else-if="active_link == 1">
      <div class="row">
        <div class="col-3 px-1"  v-for="(item, i) in order_check_list" :key="i">
          <div class="check_item card_n bg-white mb-3 p-2"
            :class="{'alert-warning ' : !item.isInCashbox}">
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
                <small style="font-size: 13px;" >
                  Қайтим нақт
                </small>
                <small class="summ_title  text-danger">
                  {{item.real_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              <div v-if="item.profit_summ" class=" text-danger p-1 d-flex justify-content-between">
                <small style="font-size: 13px;" >
                  Қайтим доллор
                </small>
                <small class="summ_title text-danger text-danger">
                  {{item.profit_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>

              <div v-if="item.online" class=" text-danger p-1 d-flex justify-content-between">
                <small style="font-size: 13px;" >
                  Қайтим {{' Balance UZS'}}
                </small>
                <small class="summ_title text-danger ">
                  {{item.online.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>
              
              <div v-if="item.perchisleniya" class=" text-danger p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Қайтим {{' Balance USD'}}
                </small>
                <small class="summ_title text-danger">
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
                  {{$t('note')}}
                </small>
                <small class="summ_title text-right" :title="item.status_name" style="font-size: 13px; cursor: pointer;">
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
                  Кассир
                </small>
                <small class="summ_title" style="font-size: 13px;">
                  {{item.image_base_64}}
                </small>
              </div>
              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('client')}}
                </small>
                <small class="summ_title" style="font-size: 13px;">
                  {{item.client_name}}
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

    <!-- Расходъ и Получать (TegirmonUserRasxod) -->
    <div class="optom_savdo_ckecks container-fluid mt-2" v-else-if="active_link == 2">
      <div class="row">
        <div class="col-3 px-1"  v-for="(item, i) in user_rasxod_list" :key="i">
          <div class="check_item card_n bg-white mb-3 p-2">
              <div class="d-flex justify-content-center">
                <small class="summ_title" style="font-size: 13px;">
                  {{ item.worker_name || 'Расход/Получать' }}
                </small>
              </div>
              <div class=" p-1 d-flex justify-content-between" v-if="item.rasxod">
                <small style="font-size: 13px;">
                  Расход (UZS)
                </small>
                <small class="summ_title text-danger" style="font-size: 13px;">
                  - {{ item.rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
                </small>
              </div>
              <div class=" p-1 d-flex justify-content-between" v-if="item.prixod">
                <small style="font-size: 13px;">
                  Получать (UZS)
                </small>
                <small class="summ_title text-success" style="font-size: 13px;">
                  + {{ item.prixod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
                </small>
              </div>
              <div class=" p-1 d-flex justify-content-between" v-if="item.dollor">
                <small style="font-size: 13px;">
                  {{$t('dollor')}}
                </small>
                <small class="summ_title text-primary" style="font-size: 13px;">
                  {{ item.dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
                </small>
              </div>
              <div class=" p-1 d-flex justify-content-between" v-if="item.dollor">
                <small style="font-size: 13px;">
                  {{$t('kurs')}}
                </small>
                <small class="summ_title text-primary" style="font-size: 13px;">
                  {{ item.kurs.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}
                </small>
              </div>
              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('note')}}
                </small>
                <small class="summ_title text-right" style="font-size: 13px;">
                  {{ item.note }}
                </small>
              </div>
              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Кассир
                </small>
                <small class="summ_title" style="font-size: 13px;">
                  {{ item.addiotionala_information }}
                </small>
              </div>
              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('date')}}
                </small>
                <small class="summ_title" style="font-size: 13px;">
                  {{  item.created_date.slice(8,10) + '-' + item.created_date.slice(5,7) + '-' + item.created_date.slice(0,4) }}
                  {{ item.created_date.slice(11,16) }}
                </small>
              </div>
              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('image_url')}}
                </small>
                <small class="summ_title text-right" style="font-size: 13px;">
                    <div class="photo d-flex " v-if="item.phone_number" style="margin-top:-3px;">
                      <img :src="hostname + item.phone_number" width="60" alt="" @click="$imageModal.open(hostname + item.phone_number)" class="shadow border rounded">
                    </div>
                </small>
              </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Захира (TegirmonInvoice, zaxiradan narsalarga almashganlar) -->
    <div class="optom_savdo_ckecks container-fluid mt-2" v-else-if="active_link == 3">
      <div class="row">
        <div class="col-3 px-1"  v-for="(item, i) in zaxira_invoice_list" :key="i">
          <div class="check_item card_n bg-white mb-3 p-2">
              <div class="d-flex justify-content-center">
                <small class="summ_title" style="font-size: 13px;">
                  ЗAXИРА №{{item.id}}
                </small>
              </div>
              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Oбщий суммаси (UZS)
                </small>
                <small class="summ_title" style="color: red; font-weight: bold;">
                  - {{item.summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>

              <div class=" p-1 d-flex justify-content-between" v-if="item.product">
                <small style="font-size: 13px;">
                  {{$t('product')}}
                </small>
                <small class="summ_title" style="font-size: 13px;">
                  {{ item.product.name }}
                </small>
              </div>

              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('client')}}
                </small>
                <small class="summ_title text-right" style="font-size: 13px;">
                  {{ item.client_name || (item.client && item.client.fio) || (item.client && item.client.name) || '-' }}
                </small>
              </div>

              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Кассир
                </small>
                <small class="summ_title" style="font-size: 13px;">
                  {{ (item.auth && item.auth.user && item.auth.user.fio) ? item.auth.user.fio : '-' }}
                </small>
              </div>

              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('date')}}
                </small>
                <small v-if="item.created_date_time" class="summ_title" style="font-size: 13px;">
                  {{  item.created_date_time.slice(8,10) + '-' + item.created_date_time.slice(5,7) + '-' + item.created_date_time.slice(0,4) }}
                  {{ item.created_date_time.slice(11,16) }}
                </small>
              </div>
              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('image_url')}}
                </small>
                <small class="summ_title text-right" style="font-size: 13px;">
                    <div class="photo d-flex " v-if="item.image_str_url" style="margin-top:-3px;">
                      <img :src="hostname + item.image_str_url" width="60" alt="" @click="$imageModal.open(hostname + item.image_str_url)" class="shadow border rounded">
                    </div>
                </small>
              </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Тароз список (TegirmonInvoice, bug‘doy narsalarga almashganlar) -->
    <div class="optom_savdo_ckecks container-fluid mt-2" v-else-if="active_link == 4">
      <div class="row">
        <div class="col-3 px-1"  v-for="(item, i) in tarozi_invoice_list" :key="i">
          <div class="check_item card_n bg-white mb-3 p-2">
              <div class="d-flex justify-content-center">
                <small class="summ_title" style="font-size: 13px;">
                  ТАРОЗ №{{item.id}}
                </small>
              </div>
              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Oбщий суммаси (UZS)
                </small>
                <small class="summ_title" style="color: red; font-weight: bold;">
                  - {{item.summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </small>
              </div>

              <div class=" p-1 d-flex justify-content-between" v-if="item.product">
                <small style="font-size: 13px;">
                  {{$t('product')}}
                </small>
                <small class="summ_title" style="font-size: 13px;">
                  {{ item.product.name }}
                </small>
              </div>

              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('client')}}
                </small>
                <small class="summ_title" style="font-size: 13px;">
                  {{ item.client_name || (item.client && item.client.fio) || (item.client && item.client.name) || '-' }}
                </small>
              </div>

              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  Кассир
                </small>
                <small class="summ_title" style="font-size: 13px;">
                  {{ (item.auth && item.auth.user && item.auth.user.fio) ? item.auth.user.fio : '-' }}
                </small>
              </div>

              <div class=" p-1 d-flex justify-content-between">
                <small style="font-size: 13px;">
                  {{$t('date')}}
                </small>
                <small v-if="item.created_date_time" class="summ_title" style="font-size: 13px;">
                  {{  item.created_date_time.slice(8,10) + '-' + item.created_date_time.slice(5,7) + '-' + item.created_date_time.slice(0,4) }}
                  {{ item.created_date_time.slice(11,16) }}
                </small>
              </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Zahira va Taroz список тулov (contragentli checklar) -->
    <div class="optom_savdo_ckecks container-fluid mt-2" v-else-if="active_link == 5">
      <div class="row">
        <div class="col-3 px-1"  v-for="(item, i) in contragent_check_list" :key="i">
          <div class="check_item card_n bg-white mb-3 p-2">
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
                  Кассир
                </small>
                <small class="summ_title" style="font-size: 13px;">
                  {{item.image_base_64}}
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
      <massage_box :hide="modal_status" :detail_info="modal_info"
        :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>

      <Toast ref="message"></Toast>
      <Alert ref="alert"></Alert>
      <checkReply v-show="false" ref="printComponent" />
  </div>
</template>

<script>
import checkReply from './checkReply.vue'
import {mapActions, mapGetters, mapMutations} from 'vuex'
import {
  mdbBtn,
  mdbIcon
} from 'mdbvue'
export default {
  components:{
      mdbBtn,
      mdbIcon,
      checkReply
    },
  data(){
    return{
      hostname: this.$store.state.server_ip,
      modal_status: false,
      modal_info: '',
      active_link: 0,
      order_link: [
        {
          id: 0,
          name: "Sotuv bo'limi",
          color: '#ED792C',
          qty: 0,
        },
        
        {
          id: 1,
          name: 'Optom savdo',
          color: '#E25049',
          qty: 0,
        },
        {
          id: 2,
          name: "Расходъ и Получать",
          color: "#A45CF2",
          qty: 0,
        },
        {
          id: 3,
          name: "Захира",
          color: "#A45CF2",
          qty: 0,
        },
        {
          id: 4,
          name: "Тароз список",
          color: "#A45CF2",
          qty: 0,
        },

        {
          id: 5,
          name: "Захира ва Тароз список тулов",
          color: '#ED792C',
          qty: 0,
        },
        
      ],
      order_check_list: [],
      sell_check_list: [],
      contragent_check_list: [],
      user_rasxod_list: [],
      zaxira_invoice_list: [],
      tarozi_invoice_list: [],
      start_time: '',
      selected_kassa_id: '',
      kassa_id: null,
      kassa_name: '', 
      print_status: false,
      now: Date.now(),
    }
  },
  // async mounted(){
  //   await this.fetchMounted();
  // },
  computed:{
    ...mapGetters(['user_kassa_list', 'allKassa']),
    isAdminAccess () {
      return localStorage.AccessType == 0 || localStorage.AccessType == '0'
    },
    effectiveKassaId () {
      // Admin bo'lsa: tanlangan kassani oladi, tanlanmagan bo'lsa null (hamma kassalar)
      if (this.isAdminAccess) {
        return this.selected_kassa_id || null
      }
      // Oddiy kassir bo'lsa: o'z kassasidan foydalanadi
      return localStorage.kassa_id
    },
    kassaIdParam () {
      // effectiveKassaId null yoki 0 bo'lsa, kassaId parametrini yubormaymiz (hamma kassalar)
      const kassaId = this.effectiveKassaId
      if (!kassaId || kassaId === 0 || kassaId === '0' || kassaId === '') {
        return ''
      }
      return '&kassaId=' + kassaId
    },
    kassaIdParamRasxod () {
      // TegirmonUserRasxod uchun kassa_id parametri
      const kassaId = this.effectiveKassaId
      if (!kassaId || kassaId === 0 || kassaId === '0' || kassaId === '') {
        return ''
      }
      return '&kassa_id=' + kassaId
    },
  },
  methods:{
    ...mapActions([ 'fetchKassa_userId', 'fetchKassa', ]),
    ...mapMutations([ 'add_product_order', 'select_savat_page', 'clear_zakaz_list']),
    canEdit(item) {
      if (!item.create_date) return false;

      // ISO formatdagi vaqtni Date obyektiga aylantirish
      const createdTime = new Date(item.create_date).getTime();

      // Agar sana yaroqsiz bo‘lsa — qaytmaydi
      if (isNaN(createdTime)) return false;

      const diff = this.now - createdTime;
      const tenMinutes = 15 * 60 * 1000; // 15 minut

      // faqat 10 minut ichida bo‘lsa true
      return diff >= 0 && diff <= tenMinutes;
    },
    async printSellCheckInfo(check){
      console.log(check);
      this.$refs.printComponent.printMounted(check);
    },
    async editSellCheck(check){
      this.select_savat_page(0);
      this.clear_zakaz_list(0);
      this.$nextTick(() => {});
      console.log('check', check);
      for(let i=0; i<check.payments.length; i++){
        let temp = {
          inv : check.payments[i].product.inv_accepted_status,
          name: check.payments[i].product.name,
          invoice: check.payments[i].product.id,
          id: check.payments[i].product.id,
          manifacturer_name: check.payments[i].product.code,
          ostatka_qty: 0, // ?
          skidka:(check.payments[i].qty * check.payments[i].product.price) - check.payments[i].summa,
          qty: check.payments[i].qty,
          saledPrice: check.payments[i].product.price,
          real_price_unit_product: check.payments[i].product.price,
          price: check.payments[i].summa,
          unit: 0,
          contains_number_in_pack: 0,
          check_id: check.id
        }
        if(temp.skidka>0){
          temp.saledPrice = temp.saledPrice - (temp.skidka / temp.qty);
        }
        this.add_product_order(temp);
      }
      
      this.$emit('close', check.id);
    },
    async toggleLink(i){
      this.active_link = i;
      // Расходъ и Получать
      if (i === 2) {
        await this.fetchUserRasxod();
      }
      // Захира
      if (i === 3) {
        await this.fetchZaxiraInvoice();
      }
      // Тароз список
      if (i === 4) {
        await this.fetchTaroziInvoice();
      }
      // Захира ва Тароз список тулов
      if (i === 5) {
        await this.fetchContragentChecks();
      }
    },
    async onDateChange () {
      // Sana o'zgarganda HAMMA bo'limlar uchun ma'lumotlarni shu sana bo'yicha yangilaymiz
      await this.fetchSellCheck();
      await this.fetchOptomCheck();
      await this.fetchUserRasxod();
      await this.fetchZaxiraInvoice();
      await this.fetchTaroziInvoice();
      await this.fetchContragentChecks();
    },
    async fetchMounted(){
      this.now = Date.now();
      this.$nextTick(function () {})
      let time1 = new Date();
      this.start_time = time1.toISOString().slice(0,10);
      // Hamma kassalar ro'yxatini olish (admin select uchun)
      await this.fetchKassa();
      console.log('allKassa', this.allKassa);
      await this.fetchKassa_userId(localStorage.user_id);
      if(this.user_kassa_list.length){
        // Oddiy kassir uchun default kassani saqlab qo'yamiz
        if (!this.isAdminAccess) {
          localStorage.kassa_id = this.user_kassa_list[0].id;
          localStorage.kassa_num = this.user_kassa_list[0].num_1;
        }
      }
      else{
        this.$refs.alert.error('Bu foydalanuvchi kassaga biriktirilmagan, unda savdo qilish huquqi yuq !');
        localStorage.kassa_id = 0;
        localStorage.kassa_num = 0;
        return;
      }
      await this.fetchOptomCheck();
      await this.fetchSellCheck();
    },

    async fetchOptomCheck(){
      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonOrderCheck/GetByDateAndKassa?date=' +  this.start_time + this.kassaIdParam);
        const res_data = await res.json();
        console.log('res_data order check hammasi online uchun data')
        console.log(res_data)
          if(res.status == 200 || res.status == 201){
            this.order_check_list = res_data;
          }
      }
      catch(error){
        console.log('order not hisob', error)
        this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
        return false;
      }
    },
    async fetchSellCheck(){
      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonCheck/GetByDateAndKassaSell?date=' +  this.start_time + this.kassaIdParam);
        const res_data = await res.json();
        console.log('res_data sell check')
        console.log(res_data)
          if(res.status == 200 || res.status == 201){
            this.sell_check_list = res_data;
          }
      }
      catch(error){
        console.log('order not hisob', error)
        this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
        return false;
      }
    },
    async fetchContragentChecks() {
      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonCheck/GetByDateAndKassaSellContragent?date=' +  this.start_time + this.kassaIdParam);
        const res_data = await res.json();
        console.log('res_data contragent sell check')
        console.log(res_data)
          if(res.status == 200 || res.status == 201){
            this.contragent_check_list = res_data;
          }
      }
      catch(error){
        console.log('order not hisob', error)
        this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
        return false;
      }
    },
    async fetchUserRasxod() {
      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonUserRasxod/getByDateAndKassa?date=' +  this.start_time + this.kassaIdParamRasxod);
        const res_data = await res.json();
        console.log('res_data user_rasxod')
        console.log(res_data)
          if(res.status == 200 || res.status == 201){
            this.user_rasxod_list = res_data;
          }
      }
      catch(error){
        console.log('order not hisob', error)
        this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
        return false;
      }
    },
    async fetchZaxiraInvoice() {
      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getZaxiraByDateAndKassa?date=' +  this.start_time + this.kassaIdParam);
        const res_data = await res.json();
        console.log('res_data zaxira invoice')
        console.log(res_data)
          if(res.status == 200 || res.status == 201){
            this.zaxira_invoice_list = res_data;
          }
      }
      catch(error){
        console.log('invoice zaxira error', error)
        this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
        return false;
      }
    },
    async fetchTaroziInvoice() {
      try{
        const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getTaroziByDateAndKassa?date=' +  this.start_time + this.kassaIdParam);
        const res_data = await res.json();
        console.log('res_data tarozi invoice')
        console.log(res_data)
          if(res.status == 200 || res.status == 201){
            this.tarozi_invoice_list = res_data;
          }
      }
      catch(error){
        console.log('invoice tarozi error', error)
        this.$refs.alert.error("Serverda uzilish bor. Qayta urinib ko'ring !");
        return false;
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.checkList{
min-height: 600px;
  background: rgba(75, 75, 75, 0.5);
  
}
.order_main_head{
  border-radius: 5px;
  padding: 3px;
  background: #a3cfeb;
  display: inline-block;
  span{
    color: #3a4b52;
    border-radius: 5px;
    cursor: pointer;
    display: inline-block;
    font-size: 13px;
    padding: 5px 13px;
    margin-left: 4px;

    background: rgb(255, 255, 255);
    &:hover{
        background-image: radial-gradient( circle 502px at 2.7% 23.7%,  rgb(198, 129, 255) 0%, rgb(91, 255, 228) 99.6% ) !important;
      color: white !important;
    }
  }
}
.bg-active_link{
  background-image: radial-gradient( circle 502px at 2.7% 23.7%,  rgb(172, 73, 253) 0%, rgba(33,250,214,1) 99.6% ) !important;
  color: white !important;
}
.main_kassa_bg{
  background-image: linear-gradient( 90deg,  rgba(255,157,129,1) 24.3%, rgb(255, 110, 78) 78.3% );
}
.main_kassa_poluchit{
  background-image: linear-gradient( 90deg,  rgb(117, 207, 165) 24.3%, rgb(93, 236, 177) 78.3% );
}
.card_n{
  box-shadow: rgba(9, 30, 66, 0.25) 0px 4px 8px -2px, rgba(9, 30, 66, 0.08) 0px 0px 0px 1px;
  border-radius: 5px;
}
.filter_bg{
  background-image: linear-gradient( 90deg,  rgb(177, 217, 250) 24.3%, rgb(156, 207, 255) 78.3% )
}
</style>
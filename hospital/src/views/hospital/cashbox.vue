<template>
  <div class="cashbox bg-white">
    <check v-show="check_show" @close="close_service_check" :indexItem="checkIndex" @closeNext = "closeNext" ref="checkP"/>
    <checkbroncheck v-if="checkbron" @closeCheck="closeCheck_bron"/>
    <checkDebit v-if="dolg_pay_check" @close="closeCheck_dolg"/>
    <div v-show="!check_show && !checkbron && !dolg_pay_check" class="cash-header  pt-2">
      <div class="cashbox_page border-bottom pb-2 px-3">
        <div class="row ">
          <div class="col-3 pl-3 pr-0">
            <div @click="fetch_all_unpayed_users">
              <lineSelect
                class="mt-1 ml-0 mr-3"
                :options="get_unpay_patient_all_list"
                :searchshow="true"
                @select="selectPatient"
                :selected="patient_name"
                :label="$t('patient list')"
              />
            </div>
            <small class="mt-1 " style="color:#67676C;">
              Последние 5 пациентов
            </small>
            <div class=" last_unpay_patient">
              <div class="choosePatientLast px-1" v-for="option in get_unpay_patient_list_today" :key="option.id" 
                  :class="{'activ_last_patient': activ_id == option.id}"
                  @click="selectOption(option)"
              >
                <div class="d-flex align-items-center">
                  <div class="picture_back_last">
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-user" width="30" height="30" viewBox="0 0 24 24" stroke-width="1" stroke="#fff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                      <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                      <circle cx="12" cy="7" r="4" />
                      <path d="M6 21v-2a4 4 0 0 1 4 -4h4a4 4 0 0 1 4 4v2" />
                    </svg>
                  </div>
                  <div class="ml-2">
                    <h6 style="font-size: 13px;" class="m-0 p-0">{{option.fio}}</h6>
                    <span style="font-size:11px; padding-top:-15px;" class="text-primary">{{option.phoneNumber}}</span>
                  </div>
                </div>
              </div>
            </div>
            
          </div>
          <div class="col-4 px-2 pl-4 mt-1">
            <div class="summa_content">
              <div class="w-100">
                <div class="qty borderSolder py-0">
                  <span class="ml-3">Пациент</span>
                  <div class="text-right px-3 mt-1">
                    <p>{{ get_cashbox_patient_info.patient_name }}</p>
                  </div>
                </div>
              </div>
              <div class="d-flex">
                <div class="w-50">
                  <div class="qty borderSolder py-0">
                    <span class="ml-3">{{$t('summ_service')}}</span>
                    <div class="text-right px-3 mt-0">
                      <p>{{summaString}}</p>
                    </div>
                  </div>
                </div>
                <div class="w-50">
                  <div class="qty borderSolder py-0">
                    <span class="ml-3">Стационар</span>
                    <div class="text-right px-3 mt-0">
                      <p>{{ get_bron_list_patient_id.need_payed.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</p>
                    </div>
                  </div>
                </div>
              </div>
              <div class="d-flex">
                <!-- <div class="w-50">
                  <div class="qty borderSolder py-0">
                    <span class="ml-3">{{$t('discount')}}</span>
                    <div class="text-right px-3 mt-0">
                      <p>{{ skidkaString }}</p>
                    </div>
                  </div>
                </div> -->
                <div class="w-50" @click="pay_debit_cashbox" style="cursor: pointer;">
                  <div class="qty borderSolder py-0">
                    <span class="ml-3">{{$t('debit')}}</span>
                    <div class="text-right px-3 mt-0">
                      <p class="text-danger">{{ get_dolg_patient_id_list.summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</p>
                    </div>
                  </div>
                </div>
                <div class="w-50">
                  <div class="qty borderSolder py-0">
                    <span class="ml-3 text-primary">Общий сумма</span>
                    <div class="text-right px-3 mt-0">
                      <p class=" text-primary">{{(summa + get_bron_list_patient_id.need_payed + get_dolg_patient_id_list.summ).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                    </div>
                  </div>
                </div>
              </div>
              
            </div>
          </div>
          <div class="col-5 px-4">
            <div class="row">
              <div class="col-4 mt-1 px-1">
                <div class="btn_pay bg-success" @click="payCash">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-cash" width="24" height="24" viewBox="0 0 24 24" stroke-width="1.2" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <rect x="7" y="9" width="14" height="10" rx="2" />
                    <circle cx="14" cy="14" r="2" />
                    <path d="M17 9v-2a2 2 0 0 0 -2 -2h-10a2 2 0 0 0 -2 2v6a2 2 0 0 0 2 2h2" />
                  </svg>
                  <small class="ml-1">
                    {{$t('cash')}}
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1" @click="payCard">
                <div class="btn_pay bg-primary">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-credit-card" width="22" height="22" viewBox="0 0 24 24" stroke-width="1" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <rect x="3" y="5" width="18" height="14" rx="3" />
                    <line x1="3" y1="10" x2="21" y2="10" />
                    <line x1="7" y1="15" x2="7.01" y2="15" />
                    <line x1="11" y1="15" x2="13" y2="15" />
                  </svg>
                  <small class="ml-1">
                    {{$t('card')}}
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1">
                <div class="btn_pay bg_dolg" @click="debit">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-cash-banknote-off" width="22" height="22" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <path d="M9.88 9.878a3 3 0 1 0 4.242 4.243m.58 -3.425a3.012 3.012 0 0 0 -1.412 -1.405" />
                    <path d="M10 6h9a2 2 0 0 1 2 2v8c0 .294 -.064 .574 -.178 .825m-2.822 1.175h-13a2 2 0 0 1 -2 -2v-8a2 2 0 0 1 2 -2h1" />
                    <line x1="18" y1="12" x2="18.01" y2="12" />
                    <line x1="6" y1="12" x2="6.01" y2="12" />
                    <line x1="3" y1="3" x2="21" y2="21" />
                  </svg>
                  <small class="ml-1">
                    {{$t('debit')}}
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1">
                <div class="btn_pay bg-info" @click="infocash">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-coin" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <circle cx="12" cy="12" r="9" />
                    <path d="M14.8 9a2 2 0 0 0 -1.8 -1h-2a2 2 0 0 0 0 4h2a2 2 0 0 1 0 4h-2a2 2 0 0 1 -1.8 -1" />
                    <path d="M12 6v2m0 8v2" />
                  </svg>
                  <small class="ml-1">
                    Инфо касса
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1">
                <div class="btn_pay bg_rasxod" @click="returnMoney">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-registered" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <circle cx="12" cy="12" r="9" />
                    <path d="M9 16v-8h4a2 2 0 0 1 0 4h-4m3 0l3 4" />
                  </svg>
                  <small class="ml-1">
                    {{$t('returnMoney')}}
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1">
                <div class="btn_pay bg_return" @click="returnPatient">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-receipt-refund" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <path d="M5 21v-16a2 2 0 0 1 2 -2h10a2 2 0 0 1 2 2v16l-3 -2l-2 2l-2 -2l-2 2l-2 -2l-3 2" />
                    <path d="M15 14v-2a2 2 0 0 0 -2 -2h-4l2 -2m0 4l-2 -2" />
                  </svg>
                  <small class="ml-1">
                    Возврат
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1">
                <div class="btn_pay bg_bron" @click="$router.push('/not_payed_list')">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-bed" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <path d="M3 7v11m0 -4h18m0 4v-8a2 2 0 0 0 -2 -2h-8v6" />
                    <circle cx="7" cy="10" r="1" />
                  </svg>
                  <small class="ml-1">
                    Стационар
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1">
                <div class="btn_pay bg_paydebit" @click="debit_pay">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-wallet" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <path d="M17 8v-3a1 1 0 0 0 -1 -1h-10a2 2 0 0 0 0 4h12a1 1 0 0 1 1 1v3m0 4v3a1 1 0 0 1 -1 1h-12a2 2 0 0 1 -2 -2v-12" />
                    <path d="M20 12v4h-4a2 2 0 0 1 0 -4h4" />
                  </svg>
                  <small class="ml-1">
                    {{$t('pay_debit')}}
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1" @click="$router.push('/check_payment_list')">
                <div class="btn_pay bg-primary" >
                  <!-- <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-chart-infographic" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <circle cx="7" cy="7" r="4" />
                    <path d="M7 3v4h4" />
                    <line x1="9" y1="17" x2="9" y2="21" />
                    <line x1="17" y1="14" x2="17" y2="21" />
                    <line x1="13" y1="13" x2="13" y2="21" />
                    <line x1="21" y1="12" x2="21" y2="21" />
                  </svg> -->
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-clipboard-data" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <path d="M9 5h-2a2 2 0 0 0 -2 2v12a2 2 0 0 0 2 2h10a2 2 0 0 0 2 -2v-12a2 2 0 0 0 -2 -2h-2" />
                    <path d="M9 3m0 2a2 2 0 0 1 2 -2h2a2 2 0 0 1 2 2v0a2 2 0 0 1 -2 2h-2a2 2 0 0 1 -2 -2z" />
                    <path d="M9 17v-4" />
                    <path d="M12 17v-1" />
                    <path d="M15 17v-2" />
                    <path d="M12 17v-1" />
                  </svg>
                  <small class="ml-1">
                    Чек
                  </small>
                </div>
              </div>
              <!-- <div class="col-4 mt-1 px-1" v-show="get_bron_list_patient_id.unpayedlist.length">
                <div class="btn_pay bg-success">
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-bed" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <path d="M3 7v11m0 -4h18m0 4v-8a2 2 0 0 0 -2 -2h-8v6" />
                    <circle cx="7" cy="10" r="1" />
                  </svg>
                  <small class="ml-1">
                    СТАЦ. Наличный
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1" v-show="get_bron_list_patient_id.unpayedlist.length">
                <div class="btn_pay bg-primary" >
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-bed" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <path d="M3 7v11m0 -4h18m0 4v-8a2 2 0 0 0 -2 -2h-8v6" />
                    <circle cx="7" cy="10" r="1" />
                  </svg>
                  <small class="ml-1">
                    СТАЦ. ПЛАС.КАРТА
                  </small>
                </div>
              </div>
              <div class="col-4 mt-1 px-1" v-show="get_bron_list_patient_id.unpayedlist.length">
                <div class="btn_pay bg_dolg" >
                  <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-bed" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                    <path d="M3 7v11m0 -4h18m0 4v-8a2 2 0 0 0 -2 -2h-8v6" />
                    <circle cx="7" cy="10" r="1" />
                  </svg>
                  <small class="ml-1">
                    СТАЦ. ДОЛГ
                  </small>
                </div>
              </div> -->
            </div>
          </div>
        </div>
      </div>
      
      <div class="cash-table ">
        <div class=" mt-1  " :class="{'TablePatientDocId_withoutBron': get_bron_list_patient_id.unpayedlist.length == 0, 'TablePatientDocId': get_bron_list_patient_id.unpayedlist.length>0}">
          <table class="myTablecashboxItem px-3">
            <thead class="bg_table_header" style="position: sticky; top:0px; z-index: 1;">
              <tr class="header">
                <th width="400">{{$t('serviceName')}}</th>
                <th >{{$t('summ')}}</th>
                <th >{{$t('discount_persantage_qty')}}</th>
                <th >{{$t('discount_qty')}}</th>
                <th >{{$t('menu_partner')}}</th>
                <th >{{$t('regis_name')}}</th>
                <th >{{$t('payedDate')}}</th>
                <th width="150">{{$t('paid')}}</th>
                <th width="120">{{$t('payed')}}</th>
                <th width="80">{{$t('Action')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in get_service_pay_list" :key="rowIndex" :class="{'alert-warning': row.discount_persantage_qty>0}" class="bg_table_tr_item">
                <td> <span >{{row.serviceName}}</span> </td>
                <td> <span >{{row.summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{row.discount_persantage_qty.toFixed()}} %</span> </td>
                <!-- <td> <span >{{row.discount_qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td> -->
                <td> <span >{{row.discount_qty}}</span> </td>
                <td> <span style="font-size: 12px;">{{row.discount_card_qty}}</span> </td>
                <td> <span style="font-size: 12px;">{{row.patientName}}</span> </td>
                <td> <span >{{row.payedDate.slice(0,10)}}</span> </td>
                <td>
                  <mdb-badge v-show="row.finishPayment === true" style="padding: 2px 8px;" pill color="success">{{$t("payed")}}</mdb-badge>
                  <mdb-badge v-show="row.finishPayment === false" style="padding: 2px 8px;" pill color="danger">{{$t('unpayed')}}</mdb-badge>
                </td>
                <td>
                  <mdb-btn @click="payOneDevice(row.id, row.summ, row.serviceName,row)"  color="success" class="m-0 p-0" style="font-size: 8px;"  p="r3 l3 t1 b1">{{$t('pay')}}</mdb-btn>  
                </td>
                <td class="text-center">
                  <!-- <i class="fas fa-pen editIcon mask waves-effect t m-0 pr-2" disabled  :data-row="rowIndex"></i> -->
                  <i class="fas fa-trash delIcon mask waves-effect m-0 pl-2" disabled @click="delService(row, rowIndex)" :data-row="rowIndex"></i>
                </td>
              </tr>
              <!-- <tr>
                <td>Общий</td>
                <td></td>
              </tr> -->
            </tbody>
          </table>
        </div>
      </div>
      <div class="startsionar_list_cash_box_bron" v-show="get_bron_list_patient_id.unpayedlist.length">
          <div class="mt-1">
            <table class="myTablecashboxItem px-3">
              <thead class="bg_table_header" style="position: sticky; top: 0; z-index: 1;">
                <tr class="header">
                  <th>{{$t('patient')}}</th>
                  <th >Врач</th>
                  <th >День</th>  
                  <th >Время начала</th>
                  <th >Время окончания</th>
                  <th >Цена за один день</th>
                  <th width="150">Платная сумма</th>
                  <th width="150">Не оплатет сумма</th>
                  <th width="120">{{$t('payed')}}</th>
                  <th width="70">{{$t('Action')}}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,rowIndex) in get_bron_list_patient_id.unpayedlist" :key="rowIndex" 
                  class="bg_table_tr_item" style="cursor:pointer;" @click="payed_bron_payment(row)">
                  <td> <span >{{row.reserved_name_1}}</span> </td>
                  <td> <span >{{row.reserved_name_3}}</span> </td>
                  <td> <span >{{row.reserved_number_1}}</span> </td>
                  <!-- <td> <span >{{row.discount_qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td> -->
                  <td> <span >{{row.begin_date_bron.slice(0,10)}}</span> </td>
                  <td> <span >{{row.end_date_bron.slice(0,10)}}</span> </td>
                  <td> <span >{{row.price_for_one_day.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  <td> <span  class="text-success">{{row.payed_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  <td> <span class="text-danger">{{row.need_payed_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  
                  <td>
                    <mdb-btn @click="payed_bron_payment(row)"  color="success" class="m-0 p-0" style="font-size: 8px;"  p="r3 l3 t1 b1">{{$t('pay')}}</mdb-btn>  
                  </td>
                  <td class="text-center">
                    <!-- <i class="fas fa-pen editIcon mask waves-effect t m-0 pr-2" disabled  :data-row="rowIndex"></i> -->
                    <i class="fas fa-trash delIcon mask waves-effect m-0 pl-2" disabled @click="delService(row, rowIndex)" :data-row="rowIndex"></i>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        <div class="row px-4" v-show="false">
          <div class="item_list_patient_bron_info p-2 mt-2 col-6" v-for="(item, i) in get_bron_list_patient_id.unpayedlist" :key="i">
            <!-- {{ get_bron_list_patient_id }} -->
            <div class="d-flex w-100 " style="cursor:pointer" @click="payed_bron_payment(item)">
              <div style="width:40%" >
                <div class="alert-info rounded text-center">
                  <small class="font-weight-bold">Нач:</small>
                  <small class="ml-2">{{ item.begin_date_bron.slice(0,10) }}</small>
                </div>
              </div>
              <div style="width:40%" >
                <div class="alert-info rounded text-center" >
                  <small class="font-weight-bold">Окон:</small>
                  <small class="ml-2">{{ item.end_date_bron.slice(0,10) }}</small>
                </div>
              </div>
              <div style="width:20%" >
                <div class="alert-info rounded text-center">
                  <small class="font-weight-bold">День:</small>
                  <small class="ml-2">{{ item.reserved_number_1 }}</small>
                </div>
              </div>
            </div>
            <div class="pay_need_payed_patient d-flex w-100  " style="cursor:pointer" @click="payed_bron_payment(item)">
              <div style="width:33%">
                <div class="alert-success rounded text-center">
                  <small class="font-weight-bold">Опл:</small>
                  <small class="ml-2">{{ item.payed_summ }}</small>
                </div>
              </div>
              <div style="width:33%">
                <div class="alert-danger rounded text-center">
                  <small class="font-weight-bold">Неопл:</small>
                  <small class="ml-2">{{ item.need_payed_summ }}</small>
                </div>
              </div>
              <div style="width:34%">
                <div class="alert-primary rounded text-center">
                  <small class="font-weight-bold">День:</small>
                  <small class="ml-2">{{ item.price_for_one_day }}</small>
                </div>
              </div>
            </div>
            <div class="text-right">
              <mdb-btn v-show="item.payed_summ == 0"  color="danger" @click="del_patient_payment(item)" class="my-0"  style="font-size: 9px;" p="r4 l4 t1 b1">{{$t('Delete')}}</mdb-btn>  
            </div>
          </div>
        </div>
      </div>
    </div>
   
    <ModalUser  :show="dolg_pay_show" classes="bg_dolg" closeColor="white" titlecolor="white" 
        :title="$t('debit')" @close="dolg_pay_show = false" width="70%">
        <template v-slot:body>
          <payDebit_component :option = "get_dolg_patient_id_list" :patient_info="get_cashbox_patient_info_selected" @close="close_dolg_pay"/>
        </template>
      </ModalUser>
    <mdb-modal :show="confirm" @close="confirm = false" size="sm" class="text-center" success>
        <mdb-modal-header center :close="false">
          <p class="heading">{{$t('Are_you_sure')}}</p>
        </mdb-modal-header>
        <mdb-modal-body>
          <mdb-icon icon="check" size="4x" class="animated rotateIn"/>
        </mdb-modal-body>
        <mdb-modal-footer center>
          <mdb-btn color="success" @click.once="promise">{{$t('Yes')}}</mdb-btn>
          <mdb-btn color="danger" @click="confirm = false">{{$t('No')}}</mdb-btn>
        </mdb-modal-footer>
    </mdb-modal>
    <mdb-modal :show="show" @close="show = false" size="sm" class="text-center" danger>
        <mdb-modal-header center :close="false">
          <p class="heading">{{$t('Are_you_sure')}}</p>
        </mdb-modal-header>
        <mdb-modal-body>
          <mdb-icon icon="times" size="4x" class="animated rotateIn"/>
        </mdb-modal-body>
        <mdb-modal-footer center>
          <mdb-btn outline="danger" @click="promiseService">{{$t('Yes')}}</mdb-btn>
          <mdb-btn color="danger" @click="show = false">{{$t('No')}}</mdb-btn>
        </mdb-modal-footer>
    </mdb-modal>
    <ModalUser  :show="service_show" headerbackColor="info"  closeColor="white" titlecolor="black" 
    :title="$t('Service pay')" @close="service_show = false" width="500px">
        <template v-slot:body>
          <cashpay :option="service_list" @close="payed_close" />
        </template>
      </ModalUser>
      <ModalUser  :show="return_show"  classes="bg_rasxod" closeColor="white"  titlecolor="white" 
        :title="$t('returnMoney')" @close="return_show = false" width="60%">
        <template v-slot:body>
          <returnMoney :option="service_list" @close="payed_close" />
        </template>
      </ModalUser>
      <ModalUser  :show="debit_show" classes="bg_dolg" closeColor="white" titlecolor="white" 
        :title="$t('debit')" @close="debit_show = false" width="500px">
        <template v-slot:body>
          <debit :option="summa" :summa="summaString" :patient="patient_id" @close="debit_close" />
        </template>
      </ModalUser>
      <ModalUser  :show="payment_bron_show" classes="bg_dolg" closeColor="white" titlecolor="white" 
          :title="$t('bron_patient')" @close="payment_bron_show = false" width="600px">
          <template v-slot:body>
            <pay_cash :option = "payment_data" @close="payed_bron_close"/>
          </template>
        </ModalUser>
    <Toast ref="message"></Toast>
    
  </div>
</template>

<script>
import {mapActions, mapGetters, mapMutations} from 'vuex'

import {mdbBtn,  mdbBadge,  mdbModal, mdbModalHeader, mdbModalBody, mdbModalFooter, mdbIcon} from 'mdbvue'
import ModalUser from '../../components/modal.vue'
import lineSelect from "../../components/hospital/cashUserSelect.vue";
import cashpay from "./cashpaydevice.vue";
import debit from "./debit.vue";
import returnMoney from "./returnMoney.vue";
import check from "./check.vue"
import checkDebit from "./checkDebit.vue"
import pay_cash from '../bron/pay_cash.vue'
import checkbroncheck from '../bron/checkbron.vue'
import payDebit_component from './payDebit_component.vue'
// import DatePicker from 'vue2-datepicker';
// import 'vue2-datepicker/index.css';
export default {
  components: {
    lineSelect, mdbBtn, mdbBadge, ModalUser, cashpay, check,pay_cash, checkbroncheck,
     mdbModal, mdbModalHeader, mdbModalBody, mdbModalFooter, mdbIcon, returnMoney, debit,
     payDebit_component, checkDebit
  },
  data(){
    return{
      enterSumma: null,
      zdachi:0,
      zdachiString:'0',
      currency: '',
      summaString: '0',


      patient_name: '...',
      patient_id: null,
      time1: null,
      card_code: '',
      service_show: false,
      service_list: {},
      confirm: false,
      cash: false,
      show: false,
      service_id: null,
      index: null,
      return_show: false,
      debit_show: false,
      
      summ: 0,
      CashMoney: null,
      CardMoney: null,
      ServiceSumma: 0,
      ServicName: '',
      ServicePayId: null,
      serviceList_check: [],

      auth_qty: 0,
      patient_id_for_ochred: null,
      user_id_for_ochred: null,

      check_show: false,
      cashOption: null,
      checkIndex: 0,

      activ_id : -1,
      skidkaString: '0',
      skidka: 0,
      payment_bron_show : false,
      payment_data : {},
      checkbron: false,
      contragent_name_for_check: '',
      patient_adress_for_check: '',
      dolg_pay_show: false,
      dolg_pay_check: false,
    }
  },
  computed: mapGetters(['get_unpay_patient_list', 'get_service_pay_list', 'summa', 'get_unpay_patient_list_today',
    'get_unpay_patient_all_list', 'get_check_print_list', 'get_code_patient', 'get_skidka', 'get_cashbox_patient_info_selected',
    'get_bron_list_patient_id', 'get_cashbox_patient_info', 'get_client_info', 'get_dolg_patient_id_list']),
  async mounted(){
    await this.fetch_unpayed_patient();
    await this.fetch_all_patient_unpayed_lists();
    setInterval(this.fetch_unpayed_patient, 4000);
    console.log(this.get_service_pay_list);
    console.log('get_cashbox_patient_info_selected')
    console.log(this.get_cashbox_patient_info_selected)
    if(this.get_cashbox_patient_info_selected.id != 0){
      await this.selectOption(this.get_cashbox_patient_info_selected)
    }
  },
  methods: {
    ...mapActions(['fetch_unpayed_patient', 'fetch_service_pay_list', 'fetch_get_code', 'fetch_patient_bron_payment',
     'fetch_all_patient_unpayed_lists', 'fetch_client_info', 'fetch_dolg_patient_id_list']),
    ...mapMutations(['update_patient_name', 'Update_check_data', 'updateDebit', 'UpdatecheckInfo', 
      'Update_cashbox_patient_info', 'UpdateCashbox_patient_info_selected', 'UpdateDolgCheck_data']),
    debit(){
      this.Update_check_data(this.get_service_pay_list);
      this.debit_show = true;
    },
    updatePatients(){
      this.fetch_unpayed_patient();
    },
    debit_pay(){
      this.$router.push('/pay_debit')
    },
    async close_service_check(){
      this.check_show = false;
      this.patient_name = '...';
      this.patient_id = null;
      this.summaString = '0';
      this.skidkaString = '0';
      this.skidka = 0;
      this.checkIndex = 0;
      console.log('this.cashOption')
      console.log(this.cashOption)
      await this.selectOption(this.cashOption);
    },
    closeNext(){
      this.check_show = false;
      this.checkIndex ++;
      this.check_show = true;
      this.$refs.checkP.printed()
    },
    async pay_debit_cashbox(){
      this.dolg_pay_show = true;
    },
    async close_dolg_pay(data){
      this.dolg_pay_show = false;
      this.dolg_pay_check = true;
      this.UpdateDolgCheck_data(data);
      await this.fetch_dolg_patient_id_list(this.get_cashbox_patient_info_selected.id);
      console.log('pay debit check');
      console.log(data)
    },
    async closeCheck_dolg(){
      this.dolg_pay_check = false;
    },
    async fetch_all_unpayed_users(){
      await this.fetch_all_patient_unpayed_lists();
    },
    async selectPatient(option){
      this.Update_cashbox_patient_info({name: option.data.fio, id:option.data.id})
      await this.fetch_client_info(option.data.id);
      this.cashOption = this.get_client_info;
      this.UpdateCashbox_patient_info_selected(this.get_client_info);
      this.patient_name = option.data.fio;
      this.patient_id = option.data.id;
      this.activ_id = option.data.id;
      this.patient_id_for_ochred = option.data.id;
      this.patient_adress_for_check = this.get_client_info.address;
      await this.fetch_service_pay_list(option.data.id);
      this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
      this.skidkaString = this.get_skidka.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
      await this.fetch_patient_bron_payment(option.data.id);
      await this.fetch_dolg_patient_id_list(option.data.id);
      if(this.get_service_pay_list.length>0){
        await this.contragentId_forName(this.get_service_pay_list[0].contragentId);
      }
      this.update_patient_name({name: option.data.fio, born:this.get_client_info.bornDate.slice(0,4), address:this.get_client_info.address, contragent: this.contragent_name_for_check})

    },
    async selectOption(option){
      this.Update_cashbox_patient_info({name: option.fio, id:option.id})
      this.UpdateCashbox_patient_info_selected(option);
      this.cashOption = option;
      this.patient_name = option.fio;
      this.patient_id = option.id;
      this.activ_id = option.id;
      this.patient_id_for_ochred = option.id;
      this.patient_adress_for_check = option.address;
      await this.fetch_service_pay_list(option.id);
      this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
      this.skidkaString = this.get_skidka.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
      await this.fetch_patient_bron_payment(option.id);
      await this.fetch_dolg_patient_id_list(option.id);
      if(this.get_service_pay_list.length>0){
        await this.contragentId_forName(this.get_service_pay_list[0].contragentId);
      }
      this.update_patient_name({name: option.fio, born:option.bornDate.slice(0,4), address:option.address, contragent: this.contragent_name_for_check})

    },

    async contragentId_forName(id){
      console.log(id)
      try{
        const response = await fetch(this.$store.state.hostname + "/Contragents/" + id);
        const data = await response.json();
        console.log(data)
        if(response.status ==200 || response.status ==201){
          this.contragent_name_for_check = data.name;
        }
      }
      catch{
        console.log('error')
      }
      console.log('this.contragent_name_for_check')
      console.log(this.contragent_name_for_check)
      console.log(this.patient_adress_for_check)

    },
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

    returnMoney(){
      this.return_show = true;
    },
    returnPatient(){
      this.$router.push('/apply_admin');
    },
    async payed_close(){
      this.service_show = false
      this.fetch_service_pay_list(this.patient_id);
      this.check_show = true;
      this.$refs.checkP.printed()
      // await this.$router.push('/check')
      // this.$root.$refs.check.printed()
    },
    async debit_close(){
      this.debit_show = false
      await this.fetch_service_pay_list(this.patient_id);
      this.check_show = true;
      this.$refs.checkP.printed();

      // await this.$router.push('/check')
      // this.$root.$refs.check.printed()
    },
    async promise(){
      console.log('localStorage.AuthId')
      console.log(localStorage.AuthId)
      this.confirm = false;
      if(this.cash === true){
        this.Update_check_data(this.get_service_pay_list);
        const respon = await fetch(this.$store.state.hostname + '/Payments/payPaymentsAllCardOrCash?PatientId=' + this.patient_id + '&Card=false&payed_auth_id=' + localStorage.AuthId)
        const data = await respon.json()
        this.$store.state.cashPay_card = true;
        console.log('shuni izlayabmzn');
        console.log(data);
        if(data[0].id){
          console.log('data[0]')
          console.log(data[0])
          await this.fetch_get_code(data[0].patientsId)
          this.UpdatecheckInfo({check:data[0].patientsId, cash: this.summa, card: 0, code:this.get_code_patient})
          var sumdebit = 0;
          this.updateDebit(sumdebit)
          // this.$refs.message.success('Added_successfully')
          await this.fetch_service_pay_list(this.patient_id);
          await this.fetch_unpayed_patient();
          // await this.$router.push('/check')
          // this.$root.$refs.check.printed()
          this.check_show = true;
          this.$refs.checkP.printed()
        }
        else{
          this.$refs.message.error('Summa xato kiritilgan')
        }
      }
      else{
        const respon = await fetch(this.$store.state.hostname + '/Payments/payPaymentsAllCardOrCash?PatientId=' + this.patient_id + '&Card=true&payed_auth_id=' + localStorage.AuthId)
        const data = await respon.json()
        this.Update_check_data(this.get_service_pay_list);
        this.$store.state.cashPay_card = false;
        console.log(data);
        if(data[0].id){
          console.log('data[0]')
          console.log(data[0])
          await this.fetch_get_code(data[0].patientsId)
          this.UpdatecheckInfo({check:data[0].patientsId, cash: 0, card: this.summa, code:this.get_code_patient})
          var sumdebits = 0;
          this.updateDebit(sumdebits)
          // this.$refs.message.success('Added_successfully')
          await this.fetch_service_pay_list(this.patient_id);
          await this.fetch_unpayed_patient();
          this.check_show = true;
          this.$refs.checkP.printed()
          // await this.$router.push('/check')
          // this.$root.$refs.check.printed()

        }
        else{
          this.$refs.message.error('Summa xato kiritilgan')
        }
      }
    },
    async promiseService(){
      const requestOptions = {
            method : "delete",
          };
          const response = await fetch(this.$store.state.hostname + "/Payments/" + this.service_id, requestOptions);
          const data = await response.json();
          console.log(data)
          if(data.id)
          {
            this.$refs.message.error('Successfully_removed')
            this.show = false;
            // if(this.auth_qty == 1){
            //   await this.fetchNextOchred();
            // }
            await this.fetch_service_pay_list(this.patient_id);
            this.fetch_unpayed_patient();
            this.summaString = this.summa.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
            this.skidkaString = this.get_skidka.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
            if(this.get_service_pay_list.length == 0){
              this.patient_name = '';
              this.patient_id = null;
              this.summaString = '';
            }
          }
    },
    async delService(data, index){
      console.log(data)
      this.index = index
      this.service_id = data.id
      this.show = true;
      this.patient_id_for_ochred = data.patientsId;

      this.auth_qty = 0;
      for(let i=0; i<this.get_service_pay_list.length; i++){
        if(data.authorizationId == this.get_service_pay_list[i].authorizationId){
          this.auth_qty ++;
        }
      }
      console.log(this.auth_qty)
      if(this.auth_qty == 1){
        await this.fetchAuthForUserId(data.authorizationId);
      }
    },
    async fetchAuthForUserId(id){
      try{
        const response = await fetch(this.$store.state.hostname + "/Authorization/" + id);
        const data = await response.json();
        console.log(data)
        if(response.status ==200 || response.status ==201){
          this.user_id_for_ochred = data.usersId;
        }
      }
      catch{
        console.log('error')
      }
      
    },
    async fetchNextOchred(){
      let newdate = new Date().toISOString().slice(0,10);
      console.log(newdate)
      let b_date  = newdate + 'T00:00:35.000Z';
      let e_date  = newdate + 'T23:59:35.000Z';
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/HospitalOchreds/acceptGetPaginationByDoktorIdAndPatientIdDates?page=0&size=100&user_id=" + this.user_id_for_ochred + '&patient_id=' + this.patient_id_for_ochred + '&b_date=' + b_date + '&e_date=' + e_date);
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const data = await response.json();
          console.log(data, 'jsonini ckeck ochret oldim')
          this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
          const data = await response.text();
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
      }
    },
    infocash(){
      this.$router.push('/dailyForPayment')
      // this.$router.push('/kunlikkassa')

    },
    async payCash(){
      this.confirm = true;
      this.cash = true;
      
    },
      async payCard(){
        this.confirm = true;
        this.cash = false;
      },
    //   printCheck () {
    //   const prtHtml = document.getElementById('print').innerHTML
    //   let stylesHtml = ''
    //   for (const node of [...document.querySelectorAll('link[rel="stylesheet"], style')]) {
    //     stylesHtml += node.outerHTML
    //   }
    //   const WinPrint = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=0,scrollbars=0,status=0')
    //   WinPrint.document.write(`<!DOCTYPE html>
    //       <html>
    //         <head>
    //           ${stylesHtml}
    //         </head>
    //         <body>
    //           <div >
    //             ${prtHtml}
    //           </div>
    //         </body>
    //       </html>`)
    //   WinPrint.document.close()
    //   WinPrint.focus()
    //   WinPrint.print()
    //   setTimeout(function () {
    //     WinPrint.close()
    //   }, 100)
    // },

    payOneDevice(id, summ, serviceName,row){
      this.service_show = true;
      console.log(id, summ, serviceName, row)
      var a = {};
      a= row;
      this.serviceList_check = [];
      this.serviceList_check.push(a);
      this.service_list = a;
      console.log(this.service_list)
      this.ServiceSumma = summ;
      this.ServicName = serviceName;
      this.ServicePayId = id
    },
    async del_patient_payment(item){
      const requestOptions = {
            method : "delete",
          };
          const response = await fetch(this.$store.state.hostname + "/HospitalBronRoomPayments/" + item.id, requestOptions);
          const data = await response.json();
          // console.log(data)
          if(data.id)
          {
            this.$refs.message.success('Successfully_removed')
            await this.fetch_patient_bron_payment(this.patient_id);
          }
    },
    async payed_bron_payment(item){
      this.payment_bron_show = true;
      this.payment_data = item;
    },
    async payed_bron_close(){
      this.checkbron = true;
      this.payment_bron_show = false;
    },
    async closeCheck_bron(){
      this.checkbron = false;
      this.payment_bron_show = false;
      await this.fetch_patient_bron_payment(this.patient_id);
    },
  }


}
</script>

<style lang="scss" scoped>
// @import "../../scss/index.scss";
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
.startsionar_list_cash_box_bron{
  height: 130px;
  overflow: hidden;
  overflow-y: auto;
  border: 1px solid #ddd;
  position: relative;
}
.TablePatientDocId{
  height: calc(100vh - 410px);
  overflow: hidden;
  overflow-y: auto;
  // border: 1px solid #ddd;
}
.TablePatientDocId_withoutBron{
  height: calc(100vh - 290px);
  overflow: hidden;
  overflow-y: auto;
  // border: 1px solid #ddd;
}

  .myTablecashboxItem {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; overflow-x:auto
}
.myTablecashboxItem th{
  font-weight: 600;
  font-size:12px;
}
.myTablecashboxItem td{
  font-size:13px;
}
.myTablecashboxItem th, .myTablecashboxItem td {
  text-align: left;
  padding: 6px 5px;
}

.myTablecashboxItem tr {
  border-bottom: 1px solid rgb(240, 240, 240);
}

.editIcon{
  color:#01b348;
  font-size: 13px;

}
.editIcon:hover{
color: #000;
}

.delIcon:hover{
color: #000;
}
.delIcon{color: rgb(251, 70, 70);
  font-size: 13px;
}
.summa_content{
  .borderSolder{
    border: 0.5px dashed #D0D3D8;

    span{
      color:#67676C;
      font-size: 18px;
    }
    p{
      color:#525255;
      font-weight:bold;
      font-size: 21px;
      margin:0;
      padding:0;
    }
  }
}
.btn_pay{
  height: 45px;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
  text-transform: uppercase;
  small{
    font-size: 11px;
    color:white;
  }
  &:hover{
    box-shadow: rgba(0, 0, 0, 0.1) 0px 20px 25px -5px, rgba(0, 0, 0, 0.04) 0px 10px 10px -5px;

    small{
      transform: scale(1.01);
    }
  }
}
.choosePatientLast{
    padding: 5px 0px 5px 0px;
    margin: 0;
  }
.picture_back_last{
    width: 35px;
    height: 35px;
    display: flex;
    justify-content: center;
    align-items: center;
    border-radius: 50%;
    background-image: radial-gradient( circle farthest-corner at 12.3% 19.3%,  rgba(85,88,218,1) 0%, rgba(95,209,249,1) 100.2% );
  }

.choosePatientLast:hover{
    background: #e7e7e7;
  }
.bg_rasxod{
  background: linear-gradient(to right, rgb(242, 112, 156), rgb(255, 148, 114));
}
.bg_return{
  background-image: radial-gradient( circle farthest-corner at 10.2% 55.8%,  rgba(252,37,103,1) 0%, rgba(250,38,151,1) 46.2%, rgba(186,8,181,1) 90.1% );
}
.bg_dolg{
  background-image: linear-gradient( 171.8deg,  rgba(5,111,146,1) 13.5%, rgba(6,57,84,1) 78.6% );
}
.bg_bron{
  background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgba(0,152,155,1) 0.1%, rgba(0,94,120,1) 94.2% );
}
.bg_paydebit{
  background-image: linear-gradient( 83.2deg,  rgba(150,93,233,1) 10.8%, rgba(99,88,238,1) 94.3% );
}
.last_unpay_patient{
  height: 150px;
  overflow-y: scroll;
}
.cashbox_page{
  box-shadow: 0px 2px 5px rgb(238, 238, 238);
}
.activ_last_patient{
  position: relative;
  background-image: radial-gradient( circle farthest-corner at 1.3% 2.8%,  rgba(239,249,249,1) 0%, rgb(214, 228, 251) 100.2% );
}
.activ_last_patient::before{
  content: '';
  position: absolute;
  width: 2.5px;
  height: 100%;
  left: 0;
  top:0;
  background-image: linear-gradient(to right, rgb(182, 244, 146), rgb(51, 139, 147));
}
.bg_table_tr_item:hover{
  background-image: radial-gradient( circle farthest-corner at 1.3% 2.8%,  rgb(207, 219, 238) 100.2%, rgb(198, 214, 241) 100.2% );
}
.bg_table_header{
  background-image: linear-gradient( 65.9deg,  rgba(85,228,224,1) 5.5%, rgba(75,68,224,0.74) 54.2%, rgba(64,198,238,1) 55.2%, rgba(177,36,224,1) 98.4% ) !important;
  th,td{
    color:white;  
  }
  padding: 50px !important;
}
.item_list_patient_bron_info{
  box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
}
</style>
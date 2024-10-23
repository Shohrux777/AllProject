<template>
    <div class="bg-white">
      <loader v-if="loading"/>
      <div class="p-1">
        <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
          <div class="header_title_vrach border-bottom mb-2">
            <span style="font-size: 17px;">{{ $t('doctor_report_salary') }}</span>
          </div>
          <form @submit.prevent="submit">
            <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
              <div class="title w-75 row align-items-center">
                 <div class="col-3">
                  <div style="position: relative; margin-top: 25px;">
                    <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -12px; color: #757575;">
                      {{$t('start_time')}}
                    </small>
                    <mdb-input type="date" size="sm" v-model="Start_time" outline/>
                  </div>
                </div>
                <div class="col-3">
                  <div style="position: relative; margin-top: 25px;"> 
                    <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -12px; color: #757575;">
                      {{$t('end_time')}}
                    </small>
                    <mdb-input type="date" size="sm" v-model="End_time" outline/>
                  </div>
                </div>
                <div class="col-4">
                  <div style="position: relative; margin-top: 0px;"> 
                    <lineSelect
                    :options="auth_user_list"
                    :searchshow="true"
                    @select="selectcontragent"
                    :selected="contragent_name"
                    :label="$t('contragent')"
                    />
                  </div>
                </div>
                <div class="col-2 m-0 p-0">
                  <mdb-btn type="submit" color="primary py-2 px-4" style="font-size:9px;">
                    {{$t('apply')}}
                  </mdb-btn>
                </div>
              </div>
              <div class="plus">
                <router-link to="/pay_salary_list">
                  <mdb-btn color="info py-2 px-4"  style="font-size:9px;">
                    {{$t('history_vrach')}}
                  </mdb-btn>
                </router-link>
              </div>
            </div>
          </form>
          <div class="border-bottom">
            <div class="summa_header_doc mt-2">
              <span class="summa_header_title pr-3 py-1">{{ $t('report_service') }}</span>
            </div>
            <div class="d-flex mt-3 pb-0">
              <div style="width:20%;">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">{{$t('service_qty')}}</span>
                      <div class="text-right px-3 mt-1">
                          <p>{{get_report_by_time_card_cash_bydoctorInfo.qty}}</p>
                      </div>
                  </div>
              </div>
              <div style="width:20%;">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">Нал + Плас - Воз</span>
                      <div class="text-right px-3 mt-1">
                          <p>{{(get_report_by_time_card_cash_bydoctorInfo.cash + get_report_by_time_card_cash_bydoctorInfo.card - get_report_by_time_card_cash_bydoctorInfo.vozvrat).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                      </div>
                  </div>
              </div>
              <div style="width:20%;">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">{{$t('cash')}}</span>
                      <div class="text-right px-3 mt-1">
                          <p>{{get_report_by_time_card_cash_bydoctorInfo.cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                      </div>
                  </div>
              </div>
              <div style="width:20%;">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">{{$t('card')}}</span>
                      <div class="text-right px-3 mt-1">
                          <p>{{get_report_by_time_card_cash_bydoctorInfo.card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                      </div>
                  </div>
              </div>
              <div style="width:20%;">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">{{$t('discount_qty')}}</span>
                      <div class="text-right px-3 mt-1">
                          <p>{{get_report_by_time_card_cash_bydoctorInfo.discount_qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                      </div>
                  </div>
              </div>
              <div style="width:20%;">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">Возврат</span>
                      <div class="text-right  px-3 mt-1">
                          <p class="text-danger">{{get_report_by_time_card_cash_bydoctorInfo.vozvrat.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                      </div>
                  </div>
              </div>
            </div>
            <div class="d-flex justify-content-between align-items-center mb-1 mt-3">
              <div class="salary_summa row w-75 ">
                <div class="col-2">
                  <mdb-input type="text" class="m-0 p-0" @input="func_service" label="Процент" size="sm" v-model="service_procent" outline/>
                </div>
                <div class="col-3">
                  <mdb-input type="text" class="m-0 p-0" @input="func_change_input" label="Сумма" size="sm" v-model="service_price" outline/>
                </div>
              </div>
              <small @click="show_service = !show_service" class="cursor_p pr-3 py-1"> <mdb-icon far icon="eye" /> {{ $t('report_service') }}</small>
            </div>
          </div>

          <div class="TablePatientDocId p-0" v-if="show_service">
            <table class="myTable">
              <thead>
                <tr class="header ">
                  <th  width="40" class="text-left">№</th>
                  <th width="200">{{$t('patient_name')}}</th>
                  <!-- <th>{{$t('contragent_name')}}</th> -->
                  <th>{{$t('service_name')}}</th>
                  <th >{{$t('service_price')}}</th>
                  <th >{{$t('paymentInCash')}}</th>
                  <th >{{$t('paymentInCard')}}</th>
                  <th>{{$t('discount_persantage_qty')}}</th>
                  <th>{{$t('discount_qty')}}</th>
                  <th>{{$t('vozvrat')}}</th>
                  <th >{{$t('regisdate')}}</th>
                  <th >{{$t('payed')}}</th>
                  <th >{{$t('finish')}}</th>
                  <!-- <th v-show="admin" width="70">{{$t('Action')}}</th> -->
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,rowIndex) in get_payment_list" :key="rowIndex" :class="{'alert-danger': row.dolg_summ, 'alert-success': row.dolg_status, 'alert-secondary': row.pay_num1>0}">
                  <td> <span >{{rowIndex+1}}</span> </td>
                  <td> <span >{{row.patients.FIO}}</span> </td>
                  <!-- <td> <span >{{row.contragent.Name}}</span> </td> -->
                  <td> <span >{{row.ServiceName}}</span> </td>
                  <td> <span >{{row.Summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  <td> <span :class="{'text-danger': row.PaymentInCash == 0, 'text-success': row.PaymentInCash != 0}">{{row.PaymentInCash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  <td> <span :class="{'text-danger': row.PaymentInCard == 0, 'text-success': row.PaymentInCard != 0}">{{row.PaymentInCard.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  <td> <span >{{row.discount_persantage_qty}} %</span> </td>
                  <td > <span v-if="row.discount_qty != null && row.discount_qty != ''">{{row.discount_qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  <td > 
                    <span v-if="row.pay_num1 != null && row.pay_num1 != ''" class="text-secondary">{{row.pay_num1.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> 
                    <span v-else> 0</span>
                  </td>
                  <td> <span >{{row.RegistratedDate.slice(0,10)}}</span> 
                    <span class="ml-1" style="font-size: 12px;">{{row.RegistratedDate.slice(11,16)}}</span></td>
  
                  <td>
                    <mdb-badge v-show="row.FinishPayment === true" style="padding: 2px 8px;" pill color="success">{{$t("payed")}}</mdb-badge>
                    <mdb-badge v-show="row.FinishPayment === false" style="padding: 2px 8px;" pill color="danger">{{$t('unpayed')}}</mdb-badge>
                  </td>
                  <td>
                    <mdb-badge v-show="row.Finish === true" style="padding: 2px 8px;" pill color="success">{{$t("проверил")}}</mdb-badge>
                    <mdb-badge v-show="row.Finish === false" style="padding: 2px 8px;" pill color="danger">{{$t('непроверенный')}}</mdb-badge>
                  </td>
                  <!-- <td v-show="admin" class="text-center">
                      <i class="fas fa-trash delIcon mask waves-effect m-0 pl-2" v-on:click="deleteRow(row,rowIndex)" :data-row="rowIndex"></i>
                  </td> -->
                </tr>
                <tr >
                  <td> <span class="text-primary">Общий</span> </td>
                  <!-- <td> <span ></span> </td> -->
                  <td> <span ></span> </td>
                  <td> <span class="text-primary">{{get_report_by_time_card_cash_bydoctorInfo.qty}}</span></td>
                  <td> <span class="text-primary">{{get_report_by_time_card_cash_bydoctorInfo.summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  <td> <span class="text-danger">{{get_report_by_time_card_cash_bydoctorInfo.cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  <td> <span class="text-danger">{{get_report_by_time_card_cash_bydoctorInfo.card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  <td></td>
                  <td> <span class="text-danger">{{get_report_by_time_card_cash_bydoctorInfo.discount_qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                </tr>
              </tbody>
            </table>
          </div>

          <!-- operatsion_bemorlar listi  -->
          <div class="border-bottom">
            <div class="summa_header_doc mt-2">
              <span class="summa_header_title pr-3 py-1">{{ $t('report_operatsion') }}</span>
            </div>
            <div class="d-flex mt-3 pb-0">
              <div style="width:20%;">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">{{$t('service_qty')}}</span>
                      <div class="text-right px-3 mt-1">
                          <p>{{get_report_operatsion_service.qty}}</p>
                      </div>
                  </div>
              </div>
              <div style="width:20%;">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">Нал + Плас - Воз</span>
                      <div class="text-right px-3 mt-1">
                          <p>{{(get_report_operatsion_service.cash + get_report_operatsion_service.card - get_report_operatsion_service.vozvrat).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                      </div>
                  </div>
              </div>
              <div style="width:20%;">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">{{$t('cash')}}</span>
                      <div class="text-right px-3 mt-1">
                          <p>{{get_report_operatsion_service.cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                      </div>
                  </div>
              </div>
              <div style="width:20%;">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">{{$t('card')}}</span>
                      <div class="text-right px-3 mt-1">
                          <p>{{get_report_operatsion_service.card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                      </div>
                  </div>
              </div>
              <div style="width:20%;">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">{{$t('discount_qty')}}</span>
                      <div class="text-right px-3 mt-1">
                          <p>{{get_report_operatsion_service.discount_qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                      </div>
                  </div>
              </div>
              <div style="width:20%;">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">Возврат</span>
                      <div class="text-right  px-3 mt-1">
                          <p class="text-danger">{{get_report_operatsion_service.vozvrat.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                      </div>
                  </div>
              </div>
            </div>
            <div class="d-flex justify-content-between align-items-center mb-1 mt-3">
              <div class="salary_summa row w-75 ">
                <div class="col-2">
                  <mdb-input type="text" class="m-0 p-0" @input="func_operatsion" label="Процент" size="sm" v-model="operatsion_procent" outline/>
                </div>
                <div class="col-3">
                  <mdb-input type="text" class="m-0 p-0" @input="func_change_input" label="Сумма" size="sm" v-model="operatsion_price" outline/>
                </div>
              </div>
              <small @click="show_operatsion = !show_operatsion" class="cursor_p pr-3 py-1"> <mdb-icon far icon="eye" /> {{ $t('report_service') }}</small>
            </div>
          </div>
          <div class="TablePatientDocId p-0" v-if="show_operatsion">
            <table class="myTable">
              <thead>
                <tr class="header ">
                  <th  width="40" class="text-left">№</th>
                  <th width="200">{{$t('patient_name')}}</th>
                  <!-- <th>{{$t('contragent_name')}}</th> -->
                  <th>{{$t('service_name')}}</th>
                  <th >{{$t('service_price')}}</th>
                  <th >{{$t('paymentInCash')}}</th>
                  <th >{{$t('paymentInCard')}}</th>
                  <th>{{$t('discount_persantage_qty')}}</th>
                  <th>{{$t('discount_qty')}}</th>
                  <th>{{$t('vozvrat')}}</th>
                  <th >{{$t('regisdate')}}</th>
                  <th >{{$t('payed')}}</th>
                  <th >{{$t('finish')}}</th>
                  <!-- <th v-show="admin" width="70">{{$t('Action')}}</th> -->
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,rowIndex) in get_report_operatsion_service.rows" :key="rowIndex" :class="{'alert-danger': row.dolg_summ, 'alert-success': row.dolg_status, 'alert-secondary': row.pay_num1>0}">
                  <td> <span >{{rowIndex+1}}</span> </td>
                  <td> <span >{{row.patients.FIO}}</span> </td>
                  <!-- <td> <span >{{row.contragent.Name}}</span> </td> -->
                  <td> <span >{{row.ServiceName}}</span> </td>
                  <td> <span >{{row.Summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  <td> <span :class="{'text-danger': row.PaymentInCash == 0, 'text-success': row.PaymentInCash != 0}">{{row.PaymentInCash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  <td> <span :class="{'text-danger': row.PaymentInCard == 0, 'text-success': row.PaymentInCard != 0}">{{row.PaymentInCard.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  <td> <span >{{row.discount_persantage_qty}} %</span> </td>
                  <td> 
                      <span v-if="row.pay_num1 != null && row.pay_num1 != ''" class="text-secondary">{{row.pay_num1.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> 
                      <span v-else> 0</span>
                    </td>
                  <td > <span v-if="row.discount_qty != null && row.discount_qty != ''">{{row.discount_qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  <td> <span >{{row.RegistratedDate.slice(0,10)}}</span> 
                    <span class="ml-1" style="font-size: 12px;">{{row.RegistratedDate.slice(11,16)}}</span></td>
  
                  <td>
                    <mdb-badge v-show="row.FinishPayment === true" style="padding: 2px 8px;" pill color="success">{{$t("payed")}}</mdb-badge>
                    <mdb-badge v-show="row.FinishPayment === false" style="padding: 2px 8px;" pill color="danger">{{$t('unpayed')}}</mdb-badge>
                  </td>
                  <td>
                    <mdb-badge v-show="row.Finish === true" style="padding: 2px 8px;" pill color="success">{{$t("проверил")}}</mdb-badge>
                    <mdb-badge v-show="row.Finish === false" style="padding: 2px 8px;" pill color="danger">{{$t('непроверенный')}}</mdb-badge>
                  </td>
                  <!-- <td v-show="admin" class="text-center">
                      <i class="fas fa-trash delIcon mask waves-effect m-0 pl-2" v-on:click="deleteRow(row,rowIndex)" :data-row="rowIndex"></i>
                  </td> -->
                </tr>
                <tr >
                  <td> <span class="text-primary">Общий</span> </td>
                  <!-- <td> <span ></span> </td> -->
                  <td> <span ></span> </td>
                  <td> <span class="text-primary">{{get_report_operatsion_service.qty}}</span></td>
                  <td> <span class="text-primary">{{get_report_operatsion_service.summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  <td> <span class="text-danger">{{get_report_operatsion_service.cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  <td> <span class="text-danger">{{get_report_operatsion_service.card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  <td></td>
                  <td> <span class="text-danger">{{get_report_operatsion_service.discount_qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                </tr>
              </tbody>
            </table>
          </div>
          

          <!-- operatsion bemorlar list  -->

          <div class="summa_header_doc mt-2">
            <span class="summa_header_title pr-3 py-1">{{ $t('report_bron') }}</span>
          </div>
          <div class="d-flex mt-3 pb-0">
              <!-- <div class="col-2">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">{{$t('patient')}}</span>
                      <div class="text-right px-3 mt-1">
                          <p>{{get_report_operatsion_service.users}}</p>
                      </div>
                  </div>
              </div> -->
              <div style="width:25%;">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">{{$t('service_qty')}}</span>
                      <div class="text-right px-3 mt-1">
                          <p>{{get_payment_payed_history_doc_id.length}}</p>
                      </div>
                  </div>
              </div>
              <div style="width:25%;">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">{{$t('summ_service')}}</span>
                      <div class="text-right px-3 mt-1">
                          <p>{{(get_bron_card_summa+get_bron_cash_summa).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                      </div>
                  </div>
              </div>
              <div style="width:25%;">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">{{$t('cash')}}</span>
                      <div class="text-right px-3 mt-1">
                          <p>{{get_bron_cash_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                      </div>
                  </div>
              </div>
              <div style="width:25%;">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">{{$t('card')}}</span>
                      <div class="text-right px-3 mt-1">
                          <p>{{get_bron_card_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                      </div>
                  </div>
              </div>
              <div style="width:25%;">
                  <div class="qty borderSolder py-2">
                      <span class="ml-3">Долг</span>
                      <div class="text-right px-3 mt-1">
                          <p>{{get_bron_need_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                      </div>
                  </div>
              </div>
            </div>
            <div class="d-flex justify-content-between align-items-center mb-1 mt-3">
              <div class="salary_summa row w-75 ">
                <div class="col-2">
                  <mdb-input type="text" class="m-0 p-0" @input="func_bron" label="Процент" size="sm" v-model="bron_procent" outline/>
                </div>
                <div class="col-3">
                  <mdb-input type="text" class="m-0 p-0" @input="func_change_input" label="Сумма" size="sm" v-model="bron_price" outline/>
                </div>
              </div>
              <small @click="show_bron = !show_bron" class="cursor_p pr-3 py-1"> <mdb-icon far icon="eye" /> {{ $t('report_service') }}</small>
            </div>
          <div class="TablePatientDocId p-0" v-if="show_bron">
          <table class="myTable">
            <thead>
              <tr class="header ">
                <th  width="40" class="text-left">№</th>
                <th width="200">{{$t('patient_name')}}</th>
                <th width="200">{{$t('doctor_name')}}</th>
                <th>День</th>
                <th>{{$t('cash')}}</th>
                <th>{{$t('card')}}</th>
                <th>Деньги на еду</th>
                <th>Осталь. денег</th>
                <th>Возврат</th>
                <th>Неоплачено</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in get_payment_payed_history_doc_id" :key="rowIndex" :class="{'alert-danger' : row.vozvrat_sum}">
                <td> <span >{{rowIndex+1}}</span> </td>
                <td><span>{{ row.hospitalBronRoomPayments.reserved_name_1 }}</span></td>
                <td><span>{{ row.hospitalBronRoomPayments.reserved_name_3 }}</span></td>
                <td><span>{{ row.hospitalBronRoomPayments.reserved_number_1 }}</span></td>
                <td class="alert-success"><span>{{ row.cash_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span></td>
                <td class="alert-success"><span>{{ row.card_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span></td>
                <td><span>{{ row.ovqat_puli.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span></td>
                <td><span>{{ row.qolgan_pul.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span></td>
                <td>
                  <span v-if="row.vozvrat_sum == null">---</span>
                  <span v-else class="font-weight-bold text-secondary">{{ row.vozvrat_sum }}</span>
                </td>
                <td><span class="text-danger font-weight-bold">{{ row.hospitalBronRoomPayments.need_payed_summ }}</span></td>
              </tr>
              <tr >
                <td> <span class="text-primary">Общий</span> </td>
                <td> <span ></span> </td>
                <td> <span ></span> </td>
                <td> <span class="text-primary">{{(get_bron_card_summa+get_bron_cash_summa).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td class="alert-success"> <span class="text-primary">{{get_bron_cash_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td class="alert-success"> <span class="text-primary">{{get_bron_card_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span class="text-primary">{{get_bron_dicId_summa.ovqat_puli.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span class="text-primary">{{get_bron_dicId_summa.qolgan_pul.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span class="text-secondary font-weight-bold">{{get_bron_dicId_summa.vozvrat_sum.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                
                <td> <span class="text-danger font-weight-bold">{{get_bron_need_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>

              </tr>
            </tbody>
          </table>
        </div>
        </div>
      </div>
      <div :class="{'Oylik_update_show_yes': oylik_show, 'Lab_update_show_no': !oylik_show}">
        <div class="text-center mt-2 mb-2 border-bottom">
          <p class="m-0 p-0 mb-1 text-danger" style="font-size: 17px;">{{oylik_doc_name}}</p>
        </div>
        <div class="items_oylik mt-2 p-2 mb-2 border-bottom d-flex justify-content-between">
          <p class="m-0 p-0 mb-1 text-primary" style="font-size: 15px;">Услуга оплате:</p>
          <p class="m-0 p-0 mb-1 text-success font-weight-bold" style="font-size: 15px;">{{service_price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
        </div>
        <div class="items_oylik mt-2 p-2 mb-2 border-bottom d-flex justify-content-between">
          <p class="m-0 p-0 mb-1 text-primary" style="font-size: 15px;">Операцион оплате:</p>
          <p class="m-0 p-0 mb-1 text-success font-weight-bold" style="font-size: 15px;">{{operatsion_price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
        </div>
        <div class="items_oylik mt-2 p-2 mb-2 border-bottom d-flex justify-content-between">
          <p class="m-0 p-0 mb-1 text-primary" style="font-size: 15px;">Стационар оплате:</p>
          <p class="m-0 p-0 mb-1 text-success font-weight-bold" style="font-size: 15px;">{{bron_price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
        </div>
        <div class="items_oylik mt-2 p-2 mb-2 border-bottom d-flex justify-content-between">
          <p class="m-0 p-0 mb-1 text-success" style="font-size: 15px;">Общий оплате:</p>
          <p class="m-0 p-0 mb-1 text-success font-weight-bold" style="font-size: 15px;">{{oylik_all_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
        </div>
        <div>
          <small class="pt-1 m-0">{{$t('note')}}</small>
          <textarea class="form-control" id="exampleFormControlTextarea1" v-model="oylik_note" :rows="number"></textarea>
        </div>
        <div class="mt-2 p-2 mb-2 text-right border-top">
          <mdb-btn @click="add_salary_doc" color="success py-2 px-5" style="font-size:9px;">
            {{$t('pay')}}
          </mdb-btn>
        </div>
        <div 
          @click="oylik_show_icon = !oylik_show_icon; oylik_show = !oylik_show"
          :class="{'Lab_update_oylik_yes_icon': oylik_show_icon, 'Lab_update_oylik_no_icon': !oylik_show_icon}">
          <mdb-icon style="color:#203146;" icon="chevron-left" />
        </div>
      </div>  
  
      
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
      <Toast ref="message"></Toast>
       <massage_box :hide="modal_status" :detail_info="modal_info"
        :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
    </div>
  
  </template>
  
  <script>
  import lineSelect from "../../../components/lineSelectAuth.vue";
    // import VueHtml2pdf from 'vue-html2pdf'
    // import DatePicker from 'vue2-datepicker';
    // import RegSelect from '../../../components/RegSelect.vue'
    // import districtAdd from "../../../components/new_prog_add/district_add"
    import { mdbBtn, mdbInput, mdbIcon,  mdbModal, mdbModalHeader, mdbBadge,  mdbModalBody,mdbModalFooter   } from 'mdbvue';
    import {mapActions, mapGetters, mapMutations} from 'vuex'
    // import 'vue2-datepicker/index.css';
    export default {
      components: {
        mdbBtn,
        mdbModal, mdbModalHeader, mdbIcon, mdbModalBody, mdbModalFooter,
        mdbBadge, mdbInput,
         lineSelect
      },
      data(){
        return{
          number: 3,
          show_service: false,
          show_operatsion: false,
          show_bron: false,
          show: false,
          admin:false,
          snipper: true,
          loading:false,
          editData: {},
          modal_info : '',
          modal_status: false,

          Start_time: null,
          End_time: null,
          group_name: '',
          group_id: null,
          get_payment_list: [],
          index: null,
          service_id: null,
          day: '',
          qty: 0,
          summ: 0,
          card: 0,
          cash: 0,
          contragent_name: '',
          contragent_id: 0,
          user_id: null,
          auth_id: localStorage.AuthId,
          service_procent: 0,
          service_price: 0,
          operatsion_procent: 0,
          operatsion_price: 0,
          bron_procent: 0,
          bron_price: 0,
          oylik_show_icon: false,
          oylik_show: false,
          oylik_all_summa: 0,
          oylik_note: '',
          oylik_begin_date: '',
          oylik_end_date: '',
          oylik_doc_name: '',
          oylik_doc_id: null, 
        }
      },
      async mounted(){
        if(localStorage.Type == 0){
          this.admin = true;
        }
        await this.fetch_auth_list()
        console.log('this.auth_user_list')
        console.log(this.auth_user_list)
        if(this.auth_user_list.length > 0){
            let time1 = new Date();
            this.Start_time = time1.toISOString().slice(0,10); 
            this.End_time = time1.toISOString().slice(0,10);
            this.contragent_id = this.auth_user_list[0].id;
            this.user_id = this.auth_user_list[0].usersId;
            this.contragent_name = this.auth_user_list[0].users.fio;
            let a = this.Start_time + 'T00:00:35.000Z';
            let b = this.End_time + 'T23:59:59.000Z';
            let c = {
                time1: '',
                time2: '',
                auth_id: this.auth_user_list[0].id
            }
            c.time1 = a;
            c.time2 = b;

            let d = {
              time1: '',
              time2: '',
              doc_id: this.auth_user_list[0].usersId
            }
            d.time1 = a;
            d.time2 = b;
            this.loading = true;
            console.log(d)
            await this.fetch_report_by_data_time_by_doctorinfo(c)
            await this.fetch_report_by_bron_payed_doc_id(d)
            this.get_payment_list = this.get_report_by_time_card_cash_bydoctorInfo.rows
            this.loading = false;
        }
      },
      computed: mapGetters(['get_report_by_time_card_cash_bydoctorInfo','get_payment_payed_history_doc_id', 'get_report_operatsion_service',
       'auth_user_list', 'get_report_by_time_card_cash', 'get_service_group_list',
      'get_bron_cash_summa', 'get_bron_card_summa', 'get_bron_need_summa', 'get_bron_dicId_summa',
      'get_payed_date_vozvrat_list_doc_service', 'get_payed_date_vozvrat_list_doc_operatsion']),
      methods: {
        ...mapActions(['fetch_auth_list','fetch_report_by_bron_payed_doc_id', 'fetch_report_by_data_time_by_doctorinfo', 
          'fetch_service_group', 'fetch_payed_date_vozvrat_list_doc']),
        ...mapMutations(['district_row_delete']),
        async func_service(){
          this.service_price = (this.get_report_by_time_card_cash_bydoctorInfo.cash + this.get_report_by_time_card_cash_bydoctorInfo.card - parseInt(this.get_report_by_time_card_cash_bydoctorInfo.vozvrat)) * this.service_procent / 100;
          this.oylik_all_summa = parseInt(this.service_price) + parseInt(this.operatsion_price) + parseInt(this.bron_price);
        },
        async func_operatsion(){
          this.operatsion_price = (this.get_report_operatsion_service.cash + this.get_report_operatsion_service.card - parseInt(this.get_report_operatsion_service.vozvrat)) * this.operatsion_procent / 100;
          this.oylik_all_summa = parseInt(this.service_price) + parseInt(this.operatsion_price) + parseInt(this.bron_price);
          
        },
        async func_bron(){
          this.bron_price = (this.get_bron_cash_summa + this.get_bron_card_summa) * this.bron_procent / 100;
          this.oylik_all_summa = parseInt(this.service_price) + parseInt(this.operatsion_price) + parseInt(this.bron_price);
        },
        async func_change_input(){
          this.oylik_all_summa = parseInt(this.service_price) + parseInt(this.operatsion_price) + parseInt(this.bron_price);
        },
        cl_clw(){
          this.service_procent = 0;
          this.service_price = 0;
          this.operatsion_procent = 0;
          this.operatsion_price = 0;
          this.bron_procent = 0;
          this.bron_price = 0;
          this.oylik_show_icon = false;
          this.oylik_show = false;
          this.oylik_all_summa = 0;
          this.oylik_note = '';
        },
        add(){
          this.show =! this.show
          this.editData = {};
        },
        async selectcontragent(option){
            console.log(option)
            this.contragent_id = option.data.id
            this.contragent_name = option.data.users.fio
            this.user_id = option.data.usersId;
            localStorage.oylik_user_id = option.data.usersId;
        },
        print(){
          this.$refs.listlar.generatePdf()
        },
        deleteRow(data, index){
          console.log(data)
          this.index = index
          this.service_id = data.id
          this.show = true;
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
                this.$refs.message.success('Successfully_removed')
                this.get_payment_list.splice(this.index, 1);
                this.show = false;
              }
        },
        async submit(){
          this.cl_clw();
          this.oylik_begin_date = this.Start_time.toString();
          this.oylik_end_date = this.End_time.toString();
          this.oylik_doc_name = this.contragent_name;
          this.oylik_doc_id = this.user_id;
          let a = {
            time1: "2021-09-01T09:15:28.886Z",
            time2: new Date(),
            auth_id: this.contragent_id
          }
          a.time1 = this.Start_time + 'T00:00:35.000Z';
          a.time2 = this.End_time + 'T23:59:59.000Z';
          let d = {
            time1: '',
            time2: '',
            doc_id: this.user_id
          }
          d.time1 = this.Start_time + 'T00:00:35.000Z';
          d.time2 = this.End_time + 'T23:59:59.000Z';
          this.loading = true;
          await this.fetch_report_by_data_time_by_doctorinfo(a);
          await this.fetch_report_by_bron_payed_doc_id(d)
          await this.fetch_payed_date_vozvrat_list_doc(a);
          this.get_payment_list = [];
          this.get_payment_list = this.get_report_by_time_card_cash_bydoctorInfo.rows
          this.loading = false;
        },
        async add_salary_doc(){
          try{
            const requestOptions = {
              method: "POST",
              headers: { "Content-Type" : "application/json" },
              body: JSON.stringify({
                "usersId": this.oylik_doc_id,
                "authorizationId": this.auth_id,
                "service_percentage": this.service_procent,
                "service_price": this.service_price,
                "starsionar_percentage": this.bron_procent,
                "starsionar_price": this.bron_price,
                "reserved_1_nuber": this.operatsion_procent,
                "reserved_2_nuber": this.operatsion_price,
                "reserved_3_nuber": this.oylik_all_summa,
                "reserved_1": this.oylik_begin_date,
                "reserved_2": this.oylik_end_date,
                "reserved_3": this.oylik_doc_name,
                "reserved_4": this.oylik_note,
                "id": 0
              })  
            };
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/HospitalDoktorOyliks", requestOptions);          
            this.loading = false;
            if(response.status == 200 || response.status == 201){
              this.$refs.message.success('Added_successfully')
              this.cl_clw();
            }
            else{
              this.$refs.message.warning('server_not_working')
            }
          }
          catch{
            this.$refs.message.warning('server_not_working')
          }
        }
      },
    };
  </script>
  
  <style lang="scss">
  @import "@/scss/service_result.scss";
  
  .add{
    position: fixed;
    background: rgba(0, 0, 0, 0.4);
    height: 100vh;
    top:0;
    width:85%;
  }
  
  .addxizmat{
    width: 470px;
    // height: 120px;
    background: #fff;
    position: relative;
    z-index: 5000;
  }
  .showing{
    display: none;
  }
  .timePicer{
    position: relative;
    margin-top: -10px;
    .timeLabel{
      position: absolute;
      font-size: 12px;
      background-color: #fff;
      padding: 1px 3px;
      z-index: 1;
      left: 6px;
      top: -1px;
    }
    .dayLabel{
      position: absolute;
      font-size: 12px;
      background-color: #fff;
      padding: 0px 3px;
      z-index: 1;
      left: 6px;
      top: -8px;
    }
  }
  .TablePatientDocId{
      // height: 400px;
      // overflow: hidden;
      // overflow-y: auto;
      // border: 1px solid #ddd;
    }
    .myTable {
    /* border-collapse: collapse; */
    table-layout:fixed;
    width: 100%;
    overflow: hidden;
    // border: 1px solid #ddd;
    font-size: 18px;
    max-height:80px; overflow-x:auto
  }
  .myTable th{
    font-weight: 600;
    font-size:12px;
  }
  .myTable td{
    font-size:13px;
  }
  .myTable th, .myTable td {
    text-align: left;
    padding: 10px;
  }
  
  .myTable tr {
    border-bottom: 1px solid rgb(240, 240, 240);
  }
  
  .myTable tr.header, .myTable tr:hover {
    // background-color: #f1f1f1;
  }
  .delIcon{
    color: rgb(251, 70, 70);
    font-size: 13px;
  }
.borderSolder{
    border: 0.5px dashed #D0D3D8;

    span{
        color:#67676C;
        font-size: 20px;
    }
    p{
        color:#525255;
        font-weight:bold;
        font-size: 22px;
        margin:0;
        padding:0;
    }
}
.summa_header_title{
  border-bottom: 2px solid #00ace0;
}
  </style>
<template>
  <div class="zaxira"> 
    <div class="getProduct">
      <div class="container-fluid">
        <div class="row px-3 py-1 d-flex align-items-center main_header_bg_new" :class="{'bg_red_color': oshibka_client == 1}">
          <div class="col-3">
            <div class="">
              <erpSelect
              :options="client_list"
              @select="selectOptionUser"
              :selected="user_name"
              size="sm"
              class="bg-white"
              style="margin-top:8px;"
              url="/TegirmonOrderClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
              :label="$t('select_item')"
              />
              <!-- <small style="position:absolute; top:-8px; left:10px; font-size: 11px; color: rgb(62, 62, 139);" class="font-weight-bold px-2 py-0">Поиск клиент</small> -->
            </div>
          </div>
          <!-- <div class="col-3">
            <mdb-input class="m-0 p-0" v-model="passport_number" size="md"  outline  group type="text" validate error="wrong" success="right"/>
            <small style="position:absolute; top:-7px; left:20px; font-size: 11px;" class="bg-white px-2 py-0">{{$t('passport_number')}}</small>
        
          </div> -->
          <div class="col-3">
            <input-search KeyName="fio"  @select="selectClientPassport"  :selected="passport_number"
              url="/TegirmonOrderClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
              ref="search_client" placeholder="AA12345678"  style="height:32px;">
            </input-search>
          </div>
          <div class="col-3">
            <input-search-phone KeyName="fio"  @select="selectClientPassport"  :selected="phone_number"
              url="/TegirmonOrderClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
              ref="search_client" placeholder="(99) ###-##-##"  style="height:32px;">
            </input-search-phone>
          </div>
  
          <div class="col-3" v-show="false">
            <input-search-year KeyName="fio"  @select="selectClientBorn" type="text"  :selected="born_date" placeholder="дд-мм-гггг"
              url="/TegirmonOrderClient/getPaginationByBornDate?page=0&size=100&born_date_str="
              ref="search_client_born_date" style="height:32px;">
            </input-search-year>
          </div>
          <div class="col-3">
            <div class="d-flex justify-content-end">
              <!-- <div  @click="refresh_skudUser" class="mr-2">
                <div class="main_kassa_btn m-0 bg_col_info" >
                  <i style="font-size: 12px;" class="fas fa-redo"></i>
                </div>
              </div> -->
              <!-- <div  @click="show_user_list" class="mr-2">
                <div class="main_kassa_btn m-0 bg_col_info">
                  <small>Список должников</small>
                </div>
              </div> -->
              <div style="width:130px" class="mr-2" @click="openOrderAdd">
                <div class="main_kassa_btn m-0 bg_col_blue">
                  <small>Добавить заказ</small>
                </div>
              </div>
              <div style="width:150px" @click="add_user_dolg">
                <div class="main_kassa_btn m-0 bg_col_green" >
                  <small>Добавить клиент</small>
                </div>
              </div>
            </div>
          </div>
  
        </div>
      </div>
      
      <div class="mainWorkChange ">
        <!-- Client tanlansa balance chiqadi -->
        <div class=" px-2" v-if="client_info.fio">
          <div class="w-100 p-2">
            <div class="row equal-height px-3" >
              <div class="col-2 p-1 " >
                <div class="card py-1 pt-2 px-2 main_kassa_poluchit balance" >
                  <span style="font-size: 13.5px;">Balance UZS</span>
                  <span class="text-right" style="font-size: 19px;">{{client_info.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span>
                  <div  class="balance_btn w-100">
                    <div class="d-flex justify-content-end px-2">
                      <div class="main_kassa_btn_sml bg_col_blue" @click="pul_olib_qolish = !pul_olib_qolish">
                        <small>Получать деньги</small>
                      </div>
                      <div class="main_kassa_btn_sml bg_col_red px-4" @click="rasxod_show = !rasxod_show">
                        <small>Расходъ</small>
                      </div>
                    </div>
                  </div>
                </div>
                
              </div>
              
              <div class="col-2 p-1 ">
                <div class="card py-1 pt-2 px-2 main_kassa_bg balance">
                  <span style="font-size: 13.5px;">Balance USD</span>
                  <span class="text-right" style="font-size: 19px;">{{client_info.dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}💲</span>
                  
                  <div  class="balance_btn w-100">
                    <div class="d-flex justify-content-end px-2">
                      <div class="main_kassa_btn_sml bg_col_blue" @click="pul_olib_qolish = !pul_olib_qolish">
                        <small>Получать деньги</small>
                      </div>
                      <div class="main_kassa_btn_sml bg_col_red px-4" @click="rasxod_show = !rasxod_show">
                        <small>Расходъ</small>
                      </div>
                    </div>
                  </div>
                </div>
                
              </div>

              <div class="col-2 p-1 " v-for="(item, index) in clientProducts" :key="index" v-show="item.qty>0">
                <div class="card py-1 pt-2 px-2 main_kassa_bg balance" :style="{'background': item.product.shitrix_code}">
                  <span style="font-size: 13.5px;">{{item.product.name}}</span>
                  <span class="text-right" style="font-size: 19px;">{{item.qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} {{ item.product.unitMeasurment.name }}</span>
                  
                  <div class="balance_btn w-100">
                    <div class="d-flex justify-content-end px-2">
                      <div class="main_kassa_btn_sml bg_col_blue" @click="sendZaxiraProduct(item)">
                        <small>↩️ Передать из захира</small>
                      </div>
                    </div>
                  </div>
                </div>
                
              </div>
            </div>
          </div>
        </div>
        <!-- Asosiy zakazlar va kalendar chiqadi -->
        <div class="main_order">
          <div class="order_list p-1" v-if="!add_order_status">
            <div  :class="{'order_list_display_notAdd': !user_rasxod_show, 'order_list_display_user': user_rasxod_show}">
              <div class="p-2 card m-1">
                <div class="mb-2">
                  <div class="order_main_head d-flex justify-content-between">
                    <div>
                      <span v-for="(item,i) in order_link" :key="i" :class="{'bg-active_link': active_link == i}" 
                        @click="toggleLink(i)">
                        {{item.name}}
                        <mdb-badge class="ml-1" style="padding: 3px 8px; font-size: 11px;" pill :style="{ background: item.color + '!important' }">{{item.qty}}</mdb-badge>
                      </span>
                    </div>
                    <div>
                      <!-- <span @click="show_checks_info = !show_checks_info">
                        Check
                      </span> -->
                      <div class="d-flex">
                        <div class="main_kassa_btn_sml bg_col_info px-4" style="margin-right: 0px;" @click="func_show_checkList">
                          <small>🪙 Платежи</small>
                        </div>
                        <div class="main_kassa_btn_sml bg_col_blue px-4 ml-2" style="margin-right: 0px;" @click="func_show_cloaded">
                          <small>🚚 Статистика погрузки</small>
                        </div>
                      </div>
                    </div>
                    
                  </div>
                </div>
                <div :class="{'order_table_max_height': !user_rasxod_show, 'order_table_max_height_user': user_rasxod_show}">
                  <table class="w-full border w-100 myTableuserSalaryList ">
                    <thead>
                      <tr class="bg-gray-200 header py-3 info_client_header ">
                        <th style="padding: 6px 10px;" class="text-left" width="50">№</th>
                        <th style="padding: 6px 10px;" class="text-left">{{$t('date')}}</th>
                        <th style="padding: 6px 10px;" class="text-left">{{$t('client_name')}}</th>
                        <th style="padding: 6px 10px;" class="text-left">{{$t('name')}}</th>
                        <th style="padding: 6px 10px;" class="text-left">Оплата</th>
                        <th style="padding: 6px 10px;" class="text-left">{{$t('sum')}}</th>
                        <th style="padding: 6px 10px;" class="text-left">{{$t('dollor')}}</th>
                        <th style="padding: 6px 10px;" class="text-left">{{$t('items')}}</th>
                        <th style="padding: 6px 10px;" class="text-left">Загрузка</th>
                        <th style="padding: 6px 10px;" class="text-left">{{$t('status_')}}</th>
                        <th style="padding: 6px 10px;" class="text-left" width="90">{{$t('Action')}}</th>

                        <!-- <th style="padding: 6px 10px;" class="text-left">{{$t('user_name')}}</th> -->
                      </tr>
                    </thead>
                    <tbody v-for="(order, index) in order_list" :key="order.id">
                        <!-- Asosiy order qatori -->
                        <tr 
                          class=" hover:bg-gray-100" style="cursor:pointer;"
                          :class="{'alert-success': order.isClosed, 'alert-warning': order.is_begin, 'alert-warning': order.is_loading}"
                        >
                          <td @click="funcShowOrderInfo(order)" class="p-2 d-flex">{{ index+1 }}
                            <div class="k-icon" title="KEPAK" 
                            v-if="order.item_list.some(x => x.product && x.product.name && x.product.name.toLowerCase().includes('kepak'))"
                            >
                              <span>K</span>
                            </div>
                          </td>
                          <td @click="funcShowOrderInfo(order)" class="p-2"><span>{{ order.pickUpDate.slice(8,10) }}-{{ order.pickUpDate.slice(5,7) }}-{{ order.pickUpDate.slice(0,4) }}</span></td>
                          <td @click="funcShowOrderInfo(order)" class="p-2">
                            <span
                            >{{ order.client_name }}</span>
                          </td>
                          
                          <td class="p-2 clickbtn" @click="funcShowOrderInfo(order)"
                          >
                            <span v-if="order.shafyor_name">{{ order.shafyor_name }}</span>
                            <span v-else class="text-danger" >???</span>
                          </td>
                          <td class="p-2" v-if="order.isPaid" @click="payOrder(order)">
                            <span class="paid">
                              Оплачено
                            </span>
                          </td>
                          <td class="p-2 " v-else  @click="payOrder(order)">
                            <span v-if="order.pay_progress>0" class="nopaidprogress">
                              <span class="load_progress_text">{{order.pay_progress}} %</span> .
                              <small class="progress_change " :style="{ width: order.pay_progress + '%' }"></small>
                            </span>
                            <span v-else class="nopaid">
                              Не оплачено
                            </span>
                          </td>
                          <td @click="funcShowOrderInfo(order)" class="p-2"><span>{{ order.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span></td>
                          <td @click="funcShowOrderInfo(order)" class="p-2"><span>{{ order.dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span></td>
                          <td class="p-2" style="cursor:pointer;" 
                            @click="toggleOrder(index)"><span>{{ order.item_list.length }}  товары</span>
                          </td>
                          <td class="p-2 " v-if="order.isLoaded && !order.is_loading" @click="toggleShafyorInfo(order, true)">
                            <span class="paid">
                              Загружен
                            </span>
                          </td>
                          <td class="p-2 " v-else  @click="toggleShafyorInfo(order, true)" style="position: relative;">
                            <span v-if="order.load_progress>0" class="nopaidprogress">
                              <span class="load_progress_text">{{order.load_progress}} %</span> .
                              <small :class="{'progress_change': !order.is_loading, 'progress_change_warning': order.is_loading}" :style="{ width: order.load_progress + '%' }"></small>
                            </span>
                            <span v-else class="nopaid">
                              Не загружен
                            </span>
                            <img v-if="order.is_loading" style="position:absolute; top:-4px; left: -50px;" src="../../assets/truck.gif" height="50"  alt="">
                          </td>
                          <td class="p-2" @click="funcShowOrderInfo(order)">
                            <div class="order_status" :title="'Примечание: ' + order.note + '(' + order.paid_status + ')'">
                              <span>{{ order.paid_status }}</span>
                            </div>
                          </td>
                          <td class="p-2 text-center d-flex" style="position: relative !important;">
                            <i class="fas fa-pen iconPen mask waves-effect  m-0 mr-3" @click="edit_order(order)"></i>
                            <i class="fa fa-trash iconCog mask waves-effect  m-0 mr-3" @click="delete_order(order)"></i>
                            <i class="fa fa-cog iconCog mask waves-effect m-0" :class="{ 'fa-spin': activeMenu === index }"
                               @click.stop="toggleMenu(index)">
                            </i>
                            <ul class="dropdown-menu"
                              :class="{'dropdown-menu1': (activeMenu == order_list.length-1 
                              || activeMenu == order_list.length-2) && order_list.length>3}"
                              v-if="activeMenu === index"  ref="menu">
                              <li @click="add_order_tozaxira(order)">📦 Получить из захира</li>
                              <li @click="edit_order(order)">📝 Tahrirlash</li>
                              <li @click="delete_order(order)">🗑️ O‘chirish</li>
                            </ul>
                          </td>
                        </tr>

                        <!-- Order items qatori -->
                        <tr v-if="expandedOrder === index" :key="order.id + '-items'">
                          <td colspan="11" class="px-1">
                            <table class="w-100  item_table_shadow rounded my-2 px-2">
                              <thead>
                                <tr class="bg-gray-100 info_client_header1">
                                  <th class=" text-left">{{ $t('product') }}</th>
                                  <th class=" text-left">Оптом цена</th>
                                  <th class=" text-left">{{$t('qty')}}</th>
                                  <th class=" text-left">{{$t('price')}}</th>
                                  <th class=" text-left">{{$t('sum')}}</th>
                                  <th class=" text-left">Валюта</th>
                                </tr>
                              </thead>
                              <tbody>
                                <tr v-for="item in order.item_list" :key="item.id">
                                  <td class="p-2">{{ item.product.name }}</td>
                                  <td class="p-2">{{ item.sum_str }}</td>
                                  <td class="p-2">{{ item.qty }}</td>
                                  <td class="p-2">{{ item.price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</td>

                                  <td class="p-2">{{ item.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</td>
                                  <td class="p-2">
                                    <span v-if="item.price_type == 1">UZS</span>
                                    <span v-else>USD</span>
                                  </td>
                                </tr>
                              </tbody>
                            </table>
                          </td>
                        </tr>
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
            <div class="user_rasxod_prixod_list p-2 card m-1 mt-2 " v-show="user_rasxod_show">
              <table class="myTableuserSalaryList myTableorderClientInfo">
                <thead>
                  <tr class="header py-3 info_client_header">
                    <th  width="40" class="text-left">№</th>
                    <th width="40" >ID</th>
                    <th>{{$t('client_name')}}</th>
                    <th>Sum</th>
                    <th>Dollor</th>
                    <th>Kurs</th>
                    <th>{{ $t('status') }}</th>
                    <th>{{$t('date')}}</th>
                    <th width="200">{{$t('note')}}</th>
                    <th>{{ $t('ostatka') }}</th>
                    <th>{{ $t('ostatka') }} $</th>
                    <th>Кассир</th>
                    <!-- <th width="80" class="text-center">{{$t('Action')}}</th> -->
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(row,rowIndex) in user_rasxod_prixod_list" :key="rowIndex" @click="selectInvoiceItem(row)"  
                  :class="{'zero_item': row.reserve_val_1 == 0 && row.reserve_val_2 == 0}" style="cursor: pointer;">
                    <td> <small >{{rowIndex+1}}</small> </td>
                    <td> <small >{{row.TegirmonQarzUserId}}</small></td>
                    <td> <small >{{row.client_name}}</small></td>
                    <td> <small >{{row.sum_str}}</small></td>
                    <td> <small >{{row.dollor_string}}</small></td>
                    <td> <small >{{row.kurs.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small></td>

                    <td v-if="row.status_rasxod == 1"> <span class="status_btn_bg px-2 text-white rounded" style="padding: 1px 6px; font-size: 10px;">Получыть </span> </td>  
                    <td  v-else-if="row.status_rasxod == 0"> <span class="bg-danger px-2 text-white rounded" style="padding: 2px 10px; font-size: 10px;">Расход</span></td>

                    <td>
                      <small v-if="row.created_date">{{row.created_date.slice(8,10) + '-' + row.created_date.slice(5,7) + '-' + row.created_date.slice(0,4)}}</small> 
                      <small v-if="row.created_date" class="ml-2">{{row.created_date.slice(11,16)}}</small>
                    </td>
                    <td class="truncate-text" :title="row.note"> <small  >{{row.note}}</small></td>
                    <td> <small :class="{'text-danger': row.reserve_val_1>0}" >{{row.reserve_val_1.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small></td>
                    <td> <small :class="{'text-danger': row.reserve_val_2>0}" >{{row.reserve_val_2.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small></td>

                    <td> <small >{{row.addiotionala_information}}</small></td>

                  </tr>
                </tbody>
              </table>
            </div>
          </div>
          <!--  -->
          
          <div class="order_add order_list p-2" v-else>
            <div class="p-2 card order_list_display">
              <order_Add :client_info="client_info" :id="order_id" @close="closeOrderAdd" @updateClient="order_updateClient"
                :choosen_day="choosen_day" :product="selectZaxiraProduct" />
            </div>
          </div>

          <div class="order_calendar">
            <calendar @choose_day="handleChooseDay1" 
              :client_info="client_info" 
              @change_date="handleChooseDate" 
              ref="user_salary_calendar" 
              class="card mt-2 ml-2"/>
            <div class="order_products mt-3 ml-2 ">
                <div class="px-0 userSalaryTable" :class="user_id ? 'userSalaryTableClient' : 'userSalaryTable'">
                  <loader-table v-if="loading"/>
                  <table v-else class="myTableuserSalaryList bg-white">
                    <thead>
                      <tr class="header py-3 info_client_header">
                        <th width="30" class="text-left">№</th>
                        <th width="150">{{$t('product')}}</th>
                        <th>{{$t('qty')}}</th>
                        <th>{{$t('sklad')}}</th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr v-for="(row,rowIndex) in order_products_list" :key="rowIndex"
                      :class="{'zero_item': row.stockQty <= 0}">
                        <td> <small >{{rowIndex+1}}</small> </td>
                        <td> <small >{{row.productName}}</small> </td>
                        <td> <small >{{row.totalQty}} {{ row.unitMeasurmentName }}</small> </td>
                        <td>
                          <small v-if="row.stockQty <= 0">Нет склад</small>
                          <small v-else-if="row.stockQty < row.totalQty" class="text-danger">{{row.stockQty}} {{ row.unitMeasurmentName }}</small>
                          <small v-else class="text-success">{{row.stockQty}} {{ row.unitMeasurmentName }}</small>
                        </td>
                      </tr>
                    </tbody>
                  </table>
              </div>
            </div>
          </div>
        </div>
      </div>

      
      
        <div class="photo w-100 mt-4 d-flex justify-content-center " v-if="image_url_str">
          <img :src="hostname + image_url_str" width="240" alt="" class="shadow border rounded">
        </div>
        
      <webcam  v-show="showPhoto" @getPhotosub="takePhoto"/>

      <!-- <hr class="mt-4 mb-3 gradint"/> -->
      <!-- <loaderFixed v-if="loading"/> -->
    </div>
     <modal-train  :show="dolg_user_show" headerbackColor="#369387"  titlecolor="black" title="Добавить клиент" 
        @close="dolg_user_show = false" width="75%">
        <template v-slot:body>
          <add_dolg_user @close_data="addedUserSelect"   ref="addQarzUser">
          </add_dolg_user>
        </template>
      </modal-train>

      <modal-train  :show="rasxod_show" headerbackColor="#fc4640"  titlecolor="black" :title="$t('rasxod')"
        @close="rasxod_show = false" width="35%">
        <template v-slot:body>
          <rasxod @close="closeRasxod" :client_info="client_info"  ref="rasxodWorkerSum">
          </rasxod>
        </template>
      </modal-train>
      <modal-train  :show="pul_olib_qolish" headerbackColor="#009587"  titlecolor="black" :title="$t('pul_olish')"
        @close="pul_olib_qolish = false" width="35%">
          <template v-slot:body>
            <chiqarPulOlish @close="closePulChiqish" :client_info="client_info" ref="prixodWorkerSum">
            </chiqarPulOlish>
          </template>
      </modal-train>
      <!-- <modal-train  :show="showUserList" headerbackColor="#4ab1ff"  titlecolor="black" title="Список должников" 
        @close="showUserList = false" width="85%">
          <template v-slot:body>
            <user_list  ref="userQarzdorList" >
            </user_list>
          </template>
      </modal-train> -->

      <modal-train  :show="show_debt_info" :headerbackColor="rasxod_color"  titlecolor="black" title="Долг инфо" 
        @close="show_debt_info = false" width="32%">
          <template v-slot:body>
            <info_rasxod @close="show_debt_info = false" v-if="show_debt_info"  :option="debt_data" >
            </info_rasxod>
          </template>
      </modal-train>
      <modal-train  :show="show_shafyor_info" headerbackColor="#369387"  titlecolor="black" title="Инфо водителя" 
        @close="show_shafyor_info = false" :width="loaded_width+'%'">
          <template v-slot:body>
            <Loaded_component  :order_info="select_order" @close="close_loaded" @loaded_finish="loaded_finish_refresh" ref="loadedComp" :loaded_status="loaded_status"/>
          </template>
      </modal-train>

      <modal-train  :show="show_checks_info" headerbackColor="#64B0FB"  titlecolor="black" title="Оптовые платежи " 
        @close="show_checks_info = false" width="70%">
          <template v-slot:body>
            <check_info :choosen_day="choosen_day" ref="order_checks_info"/>
          </template>ы
      </modal-train>

      <modal-train  :show="show_load_info" headerbackColor="#64B0FB"  titlecolor="black" title="🚚 Статистика погрузки" 
        @close="show_load_info = false" width="70%">
          <template v-slot:body>
            <load_info :choosen_day="choosen_day" ref="order_load_info"/>
          </template>
      </modal-train>

      <modal-train  :show="show_order_info" headerbackColor="#64B0FB"  titlecolor="black" title="📦 Полная информация о заказе" 
        @close="show_order_info = false" width="70%">
          <template v-slot:body>
            <order_info :order_id="order_id" ref="order_full_info"/>
          </template>
      </modal-train>


       <pay v-show="payshow" :client_info="pay_client_info" @close="closePay" 
        />
        <!-- @print="printChek()" -->
      
      <Toast ref="message"></Toast>

    <massage_box :hide="modal_status" :detail_info="modal_info"
    :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>
    <Alert ref="alert"></Alert>
      <mdb-modal :show="show_delete_order" @close="show_delete_order = false" size="sm" class="text-center" danger>
        <mdb-modal-header center :close="false">
          <p class="heading">Удалить заказ?</p>
        </mdb-modal-header>
        <mdb-modal-body>
          <mdb-icon icon="times" size="4x" class="animated rotateIn"/>
        </mdb-modal-body>
        <mdb-modal-footer center>
          <mdb-btn outline="success" @click="accept_delete">{{$t('Yes')}}</mdb-btn>
          <mdb-btn outline="danger" @click="show_delete_order = false">{{$t('No')}}</mdb-btn>
        </mdb-modal-footer>
      </mdb-modal>
      <mdb-modal :show="show_send_zaxira" @close="show_send_zaxira = false" size="sm" class="text-center" success>
        <mdb-modal-header center :close="false">
          <p class="heading">Получить из захира?</p>
        </mdb-modal-header>
        <mdb-modal-body>
          <mdb-icon icon="times" size="4x" class="animated rotateIn"/>
        </mdb-modal-body>
        <mdb-modal-footer center>
          <mdb-btn outline="success" @click="accept_send_zaxira">{{$t('Yes')}}</mdb-btn>
          <mdb-btn outline="danger" @click="show_send_zaxira = false">{{$t('No')}}</mdb-btn>
        </mdb-modal-footer>
      </mdb-modal>
  </div>
</template>

<script>
import calendar from './calendar.vue';
import webcam from '../webcam/webcam_Add.vue'
import { mdbInput, mdbIcon,  mdbBtn, mdbBadge,
  mdbModal, mdbModalHeader, mdbModalBody, mdbModalFooter
 } from "mdbvue"
// import { required } from 'vuelidate/lib/validators';
import {mapActions,mapGetters, mapMutations} from 'vuex';
import erpSelect from "../../components/erpSelectFioSearch.vue";
import InputSearch from '../../components/inputSearchPasport';
import InputSearchPhone from '../../components/inputSearchPhone';
import inputSearchYear from '../../components/inputSearchYear';
import add_dolg_user from './add_client.vue'
import rasxod from './rasxod.vue'
import chiqarPulOlish from './chiqarPulOlish.vue'
// import user_list from './user_list.vue'
import info_rasxod from './rasxod_info.vue'
import order_Add from './order_Add.vue';
import Loaded_component from './loaded_component.vue';
import Pay from './pay.vue';
import check_info from './check_info.vue';
import load_info from './load_info.vue';
import order_info from './order_info.vue';

export default {
data(){
    return{
      hostname: this.$store.state.server_ip,
      modal_info: '',
      modal_status: false,
      loading: false,
      loading_table: false,
      showUserList: false,
      show_shafyor_info: false,

      dolg_user_show: false,
      client_list: [],
      rasxod_show: false,
      pul_olib_qolish: false,
      user_rasxod_prixod_list: [],
      user_rasxod_show: false,

      client_info_show: false,
      client_info: {},
      pay_client_info: {},
      add_order_status: false,
      order_id: 0,

      
      hisobot_show: false,
      payment_show: false,
      
      allList: [],

      showPhoto: false,
      image_url_str: '',

      passport_number: '',
      born_date: '',
      phone_number: '',
      user_name: '',
      user_id: 0,

      usercheck_in_out: [],
      user_oylik_info: [],

    
      oshibka_client: 0,
      
      show_debt_info: false,
      debt_info: {},
      rasxod_color: '#64B0FB',

      choosen_day: '',
      order_list: [],
      order_products_list: [],
      expandedOrder: null,
      select_order: {},
      loaded_status: false,
      loaded_width: '35',
      active_link: localStorage.order_page,
      order_link: [
        {
          id: 0,
          name: 'Hammasi',
          color: '#ED792C',
          qty: 0,
        },
        {
          id: 1,
          name: 'Yopilmagan zakazlar',
          color: '#E25049',
          qty: 0,
        },
        {
          id: 2,
          name: "To'lov qilganlar",
          color: "#A45CF2",
          qty: 0,
        },
        {
          id: 3,
          name: "Yuklanganlar",
          color: "#EBA12E",
          qty: 0,
        },
        {
          id: 3,
          name: "Yopilgan zakazlar",
          color: '#4EC264',
          qty: 0,
        },
        {
          id: 4,
          name: "Puli kassaga kelmaganlar",
          color: '#4EE244',
          qty: 0,
        },
      ],
      payshow: false,
      old_paid_not_deliver_cassa: [],
      show_checks_info: false,
      show_load_info: false,
      show_delete_order:false,
      activeMenu: -1,
      show_send_zaxira: false,
      clientProducts: [],
      selectZaxiraProduct: null,
      show_order_info: false,

    }
  },
  components: {
    mdbIcon,
    mdbBtn, 
    mdbInput,
    mdbBadge,
    webcam,
    erpSelect,
    InputSearch,
    inputSearchYear,
    InputSearchPhone,
    add_dolg_user,
    rasxod,
    chiqarPulOlish,
    info_rasxod,
    calendar,
    order_Add,
    Loaded_component,
    Pay,
    check_info,
    load_info,
    order_info,
    mdbModal, mdbModalHeader, mdbModalBody, mdbModalFooter
  },
//   validations: {
      
//     },
    async mounted() {
      await this.fetchOldDayNotClosed();
      await this.fetchOrderPaidNotCassa();
      await this.fetchClient();
      let time1 = new Date();
      this.choosen_day = time1.toISOString().slice(0,10); 
      await this.fetchAllOrderList();
      await this.fetchAllOrderStatusNumber();
      await this.fetchAllOrderProductsList();
      // await this.nbuKurs();
      if(!localStorage.dollor_kurs){
        await this.nbuKurs();
      }
      if(localStorage.order_page>=0){
        this.active_link = localStorage.order_page;
      }
      else{
        localStorage.order_page = 0;
      }
      if(localStorage.order_page == 5){
        this.order_list = this.old_paid_not_deliver_cassa;
      }
      document.addEventListener('click', this.handleClickOutside)
      // let today = new Date();
      // this.select_month = today.toISOString().slice(0, 10);
    },
    computed: {...mapGetters(['get_user_list',]),
    beforeDestroy() {
      document.removeEventListener('click', this.handleClickOutside)
    }
    
   },
    
  methods: {
    ...mapActions(['fetch_user',]),
    ...mapMutations(['check_invoice_zaxira']),
    async funcShowOrderInfo(order){
      console.log(order)
      this.$refs.order_full_info.fetchMounted(order);
      this.show_order_info = true;
    },
    async sendZaxiraProduct(zaxira_product){
      console.log(zaxira_product);
      this.add_order_status = true;
      this.order_id = 0;
      this.selectZaxiraProduct = zaxira_product;
    },
    toggleMenu(index) {
      console.log(index)
      this.activeMenu = this.activeMenu === index ? null : index;
      console.log(this.activeMenu)
    },
    handleClickOutside(event) {
      // Agar menyu ichiga bosilmagan bo‘lsa — yopamiz
      const menus = this.$refs.menu
      if (!event.target.closest('.dropdown-menu') && !event.target.classList.contains('iconCog')) {
        this.activeMenu = null
      }
    },
    func_show_checkList(){
      this.$refs.order_checks_info.func_mounted();
      this.show_checks_info = !this.show_checks_info;
    },
    func_show_cloaded(){
      this.$refs.order_load_info.func_mounted();
      this.show_load_info = !this.show_load_info;
    },
    printReceipt() {
      console.log('printed')
      
    },
    async payOrder(order){
      console.log(order)
      if(order.client){
        this.pay_client_info = order.client;
      }
      else{
        this.pay_client_info = {
          fio:"",
          id:null,
          sum:0,
          dollor:0,
        }
      }
      this.payshow = true;
      this.$root.$refs.order_payed.changingEnter({sum:order.sum, dollor: order.dollor, order: order});

    },
    async closePay(index){
      this.payshow = false;
      if(index == 1){
        await this.fetchAllOrderList();
        await this.fetchOrderPaidNotCassa();
        await this.fetchAllOrderStatusNumber();
        if(this.client_info.id){
          await this.fetchUserBalanceUpdate(this.client_info.id);
          await this.fetchUserPrixodRasxod();
        }
      }
    },
    async loaded_finish_refresh(){
        await this.fetchAllOrderList();
        await this.fetchAllOrderProductsList();
        await this.fetchAllOrderStatusNumber();
    },
    toggleOrder(index) {
      this.expandedOrder = this.expandedOrder === index ? null : index;
    },
    async fetchUserBalanceUpdate(client_id){
      try{
          const response = await fetch(this.$store.state.hostname + "/TegirmonOrderClient/" + client_id);
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            this.client_info = data;
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
          this.client_list = [];
          this.modal_info = this.$i18n.t('network_ne_connect');
          this.modal_status = true;
        }
    },
    async closeOrderAdd(){
      this.add_order_status = !this.add_order_status;
      this.client_info = {
        id: null,
        fio:'',
      }
      this.user_name = '';
      this.user_id = 0;
      await this.fetchAllOrderList();
      await this.fetchAllOrderStatusNumber();
      await this.fetchAllOrderProductsList();
      this.selectZaxiraProduct = null;
      await this.getClientProducts();
    },
    async openOrderAdd(){
      this.add_order_status = !this.add_order_status;
      this.order_id = 0;
      this.selectZaxiraProduct = null;
    },
    async toggleShafyorInfo(order, status){
      this.select_order = order;
      this.loaded_status = status;
      if(status){
        this.loaded_width = '65';
      }
      else{
        this.loaded_width = '35';
      }
      this.show_shafyor_info = true;
      this.$refs.loadedComp.fetchMounted(this.select_order);
    },
    async close_loaded(){
      this.show_shafyor_info = false;
      await this.fetchAllOrderList();
      await this.fetchAllOrderStatusNumber();
      await this.fetchAllOrderProductsList();
    },
    async toggleLink(index){
      this.active_link = index;
      localStorage.order_page = index;
      await this.fetchAllOrderList();
      if(index == 5){
        this.order_list = this.old_paid_not_deliver_cassa;
      }
    },

    async fetchAllOrderList(){
      let temp_link = '/TegirmonOrder/getByDate?date=';
      if(localStorage.order_page == 0){
        temp_link = '/TegirmonOrder/getByDate?date=';
      }
      else if(localStorage.order_page == 1){
        temp_link = '/TegirmonOrder/getByDateNotClosed?date=';
      }
      else if(localStorage.order_page == 2){
        temp_link = '/TegirmonOrder/getByDateNotClosedButPaid?date=';
      }
      else if(localStorage.order_page == 3){
        temp_link = '/TegirmonOrder/getByDateNotClosedButLoaded?date=';
      }
      else{
        temp_link = '/TegirmonOrder/getByDateClosedOrder?date=';
      }
      try{
        const response = await fetch(this.$store.state.hostname + temp_link + this.choosen_day + '&clientId=' + this.user_id);
        const data = await response.json();
        console.log('data_list order',data)
        if(response.status == 201 || response.status == 200)
        {
          this.order_list = data;
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

    async fetchAllOrderStatusNumber(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonOrder/getOrderStatsByDate?date=" + this.choosen_day + '&clientId=' + this.user_id);
        const data = await response.json();
        console.log('data_list order',data)
        if(response.status == 201 || response.status == 200)
        {
          this.order_link[0].qty = data.totalCount;
          this.order_link[1].qty = data.notClosedCount;
          this.order_link[2].qty = data.notClosedPaidCount;
          this.order_link[3].qty = data.notClosedLoadedCount;
          this.order_link[4].qty = data.closedCount;
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

    // bu oldingi kunlarda pullari tulangan lekin puli pochtada kassaga yetib kelmaganlarni kursatadi
    async fetchOrderPaidNotCassa(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonOrder/getLoadedUnpaidOrders");
        const data = await response.json();
        console.log('data_list order',data)
        if(response.status == 201 || response.status == 200)
        {
          this.old_paid_not_deliver_cassa = data;
          this.order_link[5].qty = data.length;
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

    // oldingi kunlarda yopilmay qolgan zakazlarni bugunga utkazish 10 kun oldinni tekshiradi
    async fetchOldDayNotClosed(){
      fetch(this.$store.state.hostname + '/TegirmonOrder/checkAndUpdateOldOrders', { // API manzili
        method: 'POST',          // POST bo'lishi shart
        headers: {
          'Content-Type': 'application/json'
        },
        // agar API hech qanday body qabul qilmasa, body shart emas:
        // body: JSON.stringify({ ... })
      })
    .then(res => {
      if (!res.ok) {
        throw new Error('Server error');
      }
      return res.json();
    })
    .then(data => {
      console.log('Yangilangan eski zakazlar:', data);
      // data – C# API qaytargan ro‘yxat bo‘ladi
    })
    .catch(err => {
      console.error(err);
    });
    },

    async fetchAllOrderProductsList(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonOrder/GetOrderSummaryByDate?date=" + this.choosen_day + '&clientId=' + this.user_id);
        const data = await response.json();
        console.log('data_list order products',data)
        if(response.status == 201 || response.status == 200)
        {
          this.order_products_list = data;
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

    // xodimni olgan yoki bergan pullarini kurish
    async handleChooseDay1(data){
      console.log(data)
      const day = String(data.date.getDate()).padStart(2, "0");
      const month = String(data.date.getMonth() + 1).padStart(2, "0");
      const year = data.date.getFullYear();
      this.choosen_day = `${year}-${month}-${day}`;
      await this.fetchAllOrderList();
      await this.fetchAllOrderStatusNumber();
      await this.fetchAllOrderProductsList();
      // this.$refs.user_vaqt_info_comp.handleChooseDay(data);
    },
    async handleChooseDate(data){
      console.log(data)
      const day = String(data.date.getDate()).padStart(2, "0");
      const month = String(data.date.getMonth() + 1).padStart(2, "0");
      const year = data.date.getFullYear();

      this.choosen_day = `${year}-${month}-${day}`;
    },
    async selectInvoiceItem(data){
      this.show_debt_info = true;
      this.debt_data = data;
      if(this.debt_data.status_rasxod == 1){
        this.rasxod_color = "#369387";
      }
      else{
      this.rasxod_color = "#ff504a";
      }
    },
    async fetchUserPrixodRasxod(){
      this.user_rasxod_show = true;
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonOrderClientRasxod/getPaginationUserId?page=0&size=100&userid=" + this.client_info.id);
        const data = await response.json();
        if(response.status == 201 || response.status == 200)
        {
          console.log('data.items_list user_info', data.items_list)
          this.user_rasxod_prixod_list = data.items_list;
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

    // async show_user_list(){
    //   this.showUserList = !this.showUserList;
    //   this.$refs.userQarzdorList.fetchUserOylikReport();
    // },
    async add_user_dolg(){
      this.dolg_user_show = !this.dolg_user_show;
      this.$refs.addQarzUser.MountedFunc();
    },
    async closeRasxod(data){
      this.client_info = data;
      this.rasxod_show = !this.rasxod_show;
      await this.fetchUserPrixodRasxod();
    },
    async closePulChiqish(data){
      this.client_info = data;
      this.pul_olib_qolish = !this.pul_olib_qolish;
      await this.fetchUserPrixodRasxod();

    },
    async fetchClient(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonOrderClient/getPagination?page=0&size=100");
        const data = await response.json();
        if(response.status == 201 || response.status == 200)
        {
          console.log(data)
          this.client_list = data.items_list;
          return true;
        }
        else{
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.client_list = [];
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
    },
    async selectOptionUser(option){
      console.log(option)
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number= option.passport_number;
      this.phone_number= option.phone_number;

      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
      await this.fetchUserPrixodRasxod();
      await this.fetchAllOrderList();
      await this.fetchAllOrderStatusNumber();
      await this.fetchAllOrderProductsList();
      await this.getClientProducts();
      
    },
    async selectClientPassport(option){
      console.log(option)
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number= option.passport_number;
      this.phone_number= option.phone_number;
      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
      await this.fetchUserPrixodRasxod();
      await this.fetchAllOrderList();
      await this.fetchAllOrderStatusNumber();
      await this.fetchAllOrderProductsList();
      await this.getClientProducts();

    },
    async selectClientBorn(option){
      console.log(option)
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number= option.passport_number;
      this.phone_number= option.phone_number;

      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
      await this.fetchUserPrixodRasxod();
      await this.fetchAllOrderList();
      await this.fetchAllOrderStatusNumber();
      await this.fetchAllOrderProductsList();
      await this.getClientProducts();

    },
    // 
    async addedUserSelect(option){
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number= option.passport_number;
      this.phone_number= option.phone_number;

      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
      this.dolg_user_show = false;
      await this.fetchUserPrixodRasxod();
      await this.fetchAllOrderList();
      await this.fetchAllOrderStatusNumber();
      await this.fetchAllOrderProductsList();
      await this.getClientProducts();
    },

    // orderni update qilganda klient tanlanmagan bulsa ushani uchib beradi

    async order_updateClient(option){
      this.client_info = option;
      this.user_name = option.fio;
      this.user_id = option.id;
      this.passport_number= option.passport_number;
      this.phone_number= option.phone_number;

      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
      this.dolg_user_show = false;
      await this.getClientProducts();
    },
    async add_order_tozaxira(order){
      console.log(order)
      // this.show_send_zaxira = true; // bu joyni uchirib tagini ochish kerak
      // this.order_id = order.id;
      if(order.isPaid || order.pay_progress == 100){
        this.show_send_zaxira = true;
        this.order_id = order.id;
      }
      else{
        this.$refs.alert.error("Bu zakaz uchun to'liq to'lov qilinmagan !");
      }
    },
    async accept_send_zaxira(){
      console.log(this.order_id);
      try {
        // 🔹 API manzilingizni keraklicha o‘zgartiring
        const response = await fetch(this.$store.state.hostname + '/TegirmonOrder/save-to-client-reserve', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(this.order_id),
        });

        if (!response.ok) {
          const errText = await response.text();
          throw new Error(errText || 'Server xatosi');
        }

        const data = await response.json();

        if (data.success) {
          this.$refs.message.success('Added_successfully')
        } else {
          this.$refs.message.error('Xatolik: ' + (data.message || 'Saqlanmadi'))
        }
      } catch (err) {
        console.error('Xatolik:', err);
        this.$refs.message.error('Server bilan bog‘lanishda muammo: ' + err.message)
      } finally {
        this.activeMenu = null; // menyuni yopish
        this.show_send_zaxira = false;
        await this.fetchAllOrderList();
        await this.fetchAllOrderStatusNumber();
        await this.fetchAllOrderProductsList();
        await this.getClientProducts();
      }
    },

    async getClientProducts() {
      try {
        // 🔹 Backend API manzili
        const response = await fetch(
          this.$store.state.hostname + '/TegirmonOrderClientProductProduct/getClientProducts?client_id=' + this.user_id,
          {
            method: 'GET',
            headers: {
              'Content-Type': 'application/json',
            },
          }
        );

        if (!response.ok) {
          const errText = await response.text();
          this.clientProducts = [];
          throw new Error(errText || 'Server xatosi');
        }

        // 🔹 Backenddan kelgan JSON javobni o‘qish
        const data = await response.json();

        // 🔹 Ma’lumotni tekshirish
        if (Array.isArray(data) && data.length > 0) {
          this.clientProducts = data; // masalan, jadvalda ko‘rsatish uchun
          this.$refs.message.success('Mahsulotlar yuklandi');
        } else {
          this.clientProducts = [];
          this.$refs.message.warning('Bu client uchun mahsulot topilmadi');
        }
      } catch (err) {
        console.error('Xatolik:', err);
        this.clientProducts = [];
        // this.$refs.message.error('Server bilan bog‘lanishda muammo: ' + err.message);
      }
    },


    async edit_order(order){
      this.order_id = order.id;
      this.add_order_status = true;
      this.selectZaxiraProduct = null;
    },
    async delete_order(order){
      if(order.pay_progress == 0){
        this.order_id = order.id;
        this.show_delete_order = true;
      }
      else{
        this.$refs.alert.error("Bu zakaz to'lov qilgan yoki yuk ortilgan!");
      }
    },

    async accept_delete(){
       const requestOptions = {
            method : "delete",
          };
          const response = await fetch(this.$store.state.hostname + "/TegirmonOrder/" + this.order_id, requestOptions);
          const data = await response.text();

          if(response.status == 201 || response.status == 200)
          {
            this.$refs.message.success('Successfully_removed')
          }
          else{
            this.modal_info = data;
            this.modal_status = true;
          }
    },


    // data ni 2025-08-08 kurinishida qilib beradigan funksiya
    formatLocalDateToIsoWithoutTimezone(date) {
      const pad = (n) => n.toString().padStart(2, '0');
      const year = date.getFullYear();
      const month = pad(date.getMonth() + 1); // JS'da oy 0-based
      const day = pad(date.getDate());
      // const hours = pad(date.getHours());
      // const minutes = pad(date.getMinutes());
      // const seconds = pad(date.getSeconds());

      return `${year}-${month}-${day}`;
    },

     //kerak
    takePhoto(img){
      this.image_url_str = img;
      this.showPhoto = false;
    },
    //kerak


    getPhoto(){
      this.showPhoto = true;
    },
    async nbuKurs(){
      try{
        const response = await fetch("https://cbu.uz/uz/arkhiv-kursov-valyut/json/");
        const data = await response.json();
        console.log('json valyuta')
        console.log(data)
        if(data.length>0){
          this.dollor_kurs = parseInt(data[0].Rate).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
          this.dollor_kurs_qty = parseInt(data[0].Rate);
          localStorage.dollor_kurs = parseInt(data[0].Rate);
        }
      }
      catch(error){
        console.log(error)
      }
    },
  },
}
</script>

<style lang="scss" scoped>

::-webkit-scrollbar {
  width: 5.5px;
  height: 2px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #f1f1f1;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #888;
  border-radius: 2px;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #555;
}
/* ! for scroll */
  .product_name_price{
    height: 55px;
  }
  .pro_name_color{
    color: #757575;
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
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
.userSalaryTable{
  min-height: calc(100vh - 700px);
  max-height: calc(100vh - 450px);
  overflow: hidden;
  overflow-y: scroll;
  scrollbar-width: none;
  border-radius: 5px;
  padding-bottom: 5px;
  background: #f7fcff;

  // border: 1px solid #757575;
}
.userSalaryTableClient{
  height: calc(100vh - 530px);
  overflow: hidden;
  overflow-y: scroll;
  scrollbar-width: none;
  border-radius: 5px;
  background: #f7fcff;
  // border: 1px solid #757575;
}
.myTableuserSalaryList {
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  overflow-y: scroll;
  font-size: 16px;
  max-height:80px; overflow-x:auto
}
.myTableuserSalaryList th{
  font-weight: 600;
  font-size:11px;
}
.myTableuserSalaryList td{
  font-size:11.5px;
}
.myTableuserSalaryList tbody td small{
  font-size:11px;
}
.myTableuserSalaryList td {
  text-align: left;
  padding: 3px 10px;
  // border-right: 1px solid rgb(219, 219, 219);
}
.myTableuserSalaryList th{
  text-align: left;
  padding: 5px 10px;

}
.myTableorderClientInfo td {
  text-align: left;
  padding: 6px 10px;
  // border-right: 1px solid rgb(219, 219, 219);
}
.myTableuserSalaryList tr {
  border-bottom: 1px solid rgb(219, 219, 219);
  &:hover{
    background: #afd1fd;
  }
}
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
.editIcon{
  color:#01b348;
  font-size: 13px;
}
.mainWorkChange{
  // border: 1px solid black;
  min-height: calc(100vh - 120px);
}
.hover_info{
  display: none;
}

.border_dashed{
  border-bottom: 1px dashed #ddd;
}
.bg_red_color{
  background: #ff937d !important;
}
.bg_danger{
  background: #fd4925 !important;
}
.info_client_header{
  background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgb(200, 220, 230) 0%, rgb(196, 223, 228) 90.1% );
}
.info_client_header1{
  background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgb(255, 224, 194) 0%, rgb(253, 205, 187) 90.1% ) !important;

}
.zero_item{
  background: linear-gradient(to right, #ff698c, #ff6246);
}
.rounded{
  border-radius: 48%;
}
.user_info_card{
  height: 450px;
  overflow: hidden;
  overflow-y: scroll;
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
  padding: 7px;
}

.myTable tr {
  border-bottom: 1px solid rgb(240, 240, 240);
}
.user_img img{
  width: 100%;
  height: 150px;
}
.user_img{
  padding: 5px;
  cursor: pointer;
}
.user_info_selected p{
  color: #595961;
  font-size: 14px;
}
.main_kassa_btn{
  display: flex;
  justify-content: center;
  align-items: center;
  height: 34px;
  padding: 0 15px;
  color:#3a4b52;
  //background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgba(0,152,155,1) 0.1%, rgba(0,94,120,1) 94.2% );
  border-radius: 4px;
  cursor: pointer;
  margin-right: 7px;
  margin-top: 0px;
  small{
    font-size: 12px;
  }
}
.main_kassa_btn_sml{
  display: flex;
  justify-content: center;
  align-items: center;
  height: 30px;
  padding: 0 15px;
  color:#3a4b52;
  border-radius: 4px;
  cursor: pointer;
  margin-right: 7px;
  margin-top: 0px;
  small{
    font-size: 12px;
  }
}
.bg_col_blue{
  //border: 1.5px solid #009587;
  background: #009587;   
  color:white;

  &:hover{
    color:white;
    background: #009587;  
  }
}
.bg_col_green{
  //border: 1.5px solid #009587;
  background: #03b15f;   
  color:white;

  &:hover{
    color:white;
    background: #009563;  
  }

}
.bg_col_red{
  //border: 1.5px solid #ff504a;
  background: #ff504a; 
  color:white;

  &:hover{
    color:white;
    background: #ff504a;  
  }
}
.bg_col_info{
  //border: 1.5px solid #4aaeff;
  background: #4ab1ff;
  color:white;

  &:hover{
    color:white;
    background: #4ab1ff;  
  }
}
.status_btn_bg{
  background: #369387 !important;
}
.status_btn_cl{
  color: #369387 !important;
}
.oylik_hisob_payed{
  position: absolute;
  top: 0; 
  right: -580px;
  width: 550px;
  z-index: 11;
}
.oylik_input{
  width: 100%;
  height: 30px;
  border-radius: 4px;
  padding: 1px 7px;
  font-size: 14px;
  text-align: right;
  border: 1px solid #d3d3d3;
  outline: none;
}
.oylik_input_red{
  color: red;
}
.oylik_input_green{
  color: green;
}
.oylik_input_blue{
  color:#006b95;
}
.oylik_input:focus{
  border: 1px solid #a5a5a5;
}
.oylik_input:disabled{
  background: white;
}
.payment_info{
  font-style: 17px;
  border-bottom: 1px solid #757575;
  small{
    color: #4e4a4a;
     font-style: 17px;
  }
}
.client_balance{
  width: 12.5%;
  margin: 5px 0px;
}

.equal-height {
    display: flex;
    align-items: stretch;
}
.balance_btn{
  position: absolute;
  bottom: -50px;
  left: 0;
  background: rgba(32, 32, 32, 0.7);
  height: 50px;
  padding-top: 10px;
  border-top-left-radius: 5px;
  border-bottom-left-radius: 5px;
  border-bottom-right-radius: 5px;
  z-index: 1111;
  box-shadow: rgba(0, 0, 0, 0.25) 0px 54px 55px,
              rgba(0, 0, 0, 0.12) 0px -12px 30px,
              rgba(0, 0, 0, 0.12) 0px 4px 6px,
              rgba(0, 0, 0, 0.17) 0px 12px 13px,
              rgba(0, 0, 0, 0.09) 0px -3px 5px;

  opacity: 0;
  max-height: 0;
  transform: translateY(15px);
  pointer-events: none;
  transition: all 0.3s ease;
}

// .balance_btn::before {
//   content: "";
//   position: absolute;
//   inset: 0;
//   backdrop-filter: blur(1px);
//   -webkit-backdrop-filter: blur(1px);
//   z-index: 1;

//   opacity: 0;                 /* boshida ko‘rinmas */
//   transition: opacity 0.3s ease; 
// }

// .balance:hover .balance_btn::before {
//   opacity: 1;                /* hover bo‘lganda sekin chiqadi */
// }

.balance_btn > * {
  position: relative;
  z-index: 2;
}

.balance:hover {
  cursor: pointer;
}

.balance:hover .balance_btn{
  opacity: 1;
  max-height: 100px; /* yetarli bir qiymat */
  transform: translateY(0);
  pointer-events: auto;
}

.main_order{
  display: flex;
  width: 100%;
}
.order_list{
  width: calc(100vw - 370px);
}
.order_calendar{
  width: 350px;
}
.order_list_display{
  overflow: hidden;
  overflow-y: scroll;
  max-height: calc(100vh - 130px);
}
.order_list_display_notAdd{
  overflow: hidden;
  height: calc(100vh - 130px);
}
.order_list_display_user{
  overflow: hidden;
  height: calc(100vh - 530px);
}
.order_table_max_height{
  overflow: hidden;
  overflow-y: scroll;
  max-height: calc(100vh - 200px);
  min-height: 260px;
}
.order_table_max_height_user{
  overflow: hidden;
  overflow-y: scroll;
  max-height: calc(100vh - 600px);
  min-height: 260px;
}
.user_rasxod_prixod_list{
  overflow: hidden;
  overflow-y: scroll;
  height: 325px;
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
.paid{
  width: 90%;
  text-align: center;
  color: #4c7419;
  font-weight: bold;
  border-radius: 4px;
  cursor: pointer;
  display: inline-block;
  font-size: 10px;
  padding: 2px 5px;
  border: 1px solid #86c732;
  background: #ebfdd5;
  font-weight: 600;
}
.nopaid{
  width: 90%;
  text-align: center;
  color: #fd6a6a;
  border-radius: 4px;
  cursor: pointer;
  display: inline-block;
  font-size: 10px;
  padding: 2px 5px;
  border: 1px solid #fd6a6a;
  background: #fff6f6;
  font-weight: 600;
}
.nopaidprogress{
  position: relative;
  width: 90%;
  text-align: center;
  color: #fff6f6;
  border-radius: 4px;
  cursor: pointer;
  display: inline-block;
  font-size: 10px;
  padding: 2px 5px;
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
.item_table_shadow{
  box-shadow: rgba(60, 64, 67, 0.3) 0px 1px 2px 0px, rgba(60, 64, 67, 0.15) 0px 1px 3px 1px;
  border-radius: 10px;
}
.iconCog{
  color: #939399;
  font-size: 11.5px;
}
.iconPen{
  color: #939399;
  font-size: 11.5px;

}
.clickbtn{
  cursor: pointer;
}
.order_status span {
  display: inline-block;    /* yoki block */
  width: 120px;             /* maksimal kenglik */
  white-space: nowrap;      /* yangi qatorga tushmasin */
  overflow: hidden;         /* tashqarisini yashir */
  text-overflow: ellipsis;  /* uch nuqta qo'y */
  cursor: pointer;
}
.order_products{
  border-radius: 3px;
  width: 350px;
  box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
}
.truncate-text {
  max-width: 200px;       /* Matn sig‘adigan kenglik */
  white-space: nowrap;     /* Bir qator bo‘lib tursin */
  overflow: hidden;        /* Sig‘magan qismi yashiring */
  text-overflow: ellipsis; /* … chiqsin */
  cursor: pointer;
}
.dropdown-menu,
.dropdown-menu1 {
  position: absolute !important;
  display: block;
  background: rgb(255, 255, 255);
  border: 1px solid #ccc;
  border-radius: 6px;
  box-shadow: 5px 6px 12px rgba(0,0,0,0.15);
  z-index: 999999 !important;
  min-width: 120px;
  padding: 4px 0;
}

.dropdown-menu {
  top: 28px !important;
  left: -85px;
}
.dropdown-menu1 {
  top: -105px !important;
  left: -85px;
}

.dropdown-menu::before {
  content: "";
  position: absolute;
  top: -6px; /* menyu ustidan chiqadi */
  right: 12px; /* ⚙️ icon qayerda joylashganiga qarab sozlanadi */
  width: 10px;
  height: 10px;
  background: #fff;
  border-top: 1px solid #ccc;
  border-left: 1px solid #ccc;
  transform: rotate(45deg);
  box-shadow: -2px -2px 3px rgba(0,0,0,0.05);
  z-index: 9999;
}
/* 🔹 Tepasidagi kichik uch (pointer) */
.dropdown-menu1::before {
  content: "";
  position: absolute !important;
  top: 94px; /* menyu ustidan chiqadi */
  right: 12px; /* ⚙️ icon qayerda joylashganiga qarab sozlanadi */
  width: 10px;
  height: 10px;
  background: #fff;
  border-bottom: 1px solid #ccc;
  border-right: 1px solid #ccc;
  border-top: 0px solid #ffffff;
  border-left: 0px solid #ffffff;
  transform: rotate(45deg);
  box-shadow: -2px -2px 3px rgba(0,0,0,0.05);
  z-index: 9999;
}
.dropdown-menu li {
  list-style: none;
  padding: 6px 12px;
  cursor: pointer;
  white-space: nowrap;
  font-size: 12px;
  // border-bottom: 1px solid #f3f3f3;
}

.dropdown-menu li:hover {
  background: #9bc6ff;
}
.bottom_menu{
  padding-bottom: 60px !important;
}
.k-icon {
  position: relative;
  display: flex;
  align-items: center;
  justify-content: center;
  width: 20px;
  height: 20px;
  margin-top:-3px;
  margin-right: 4px;
  cursor:pointer;
}

.k-icon span {
  position: absolute;
  font-weight: 700;
  font-size: 15px;
  color: #ff0505;
}
</style>
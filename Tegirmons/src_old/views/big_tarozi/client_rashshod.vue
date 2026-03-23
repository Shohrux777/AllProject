<template>
    <div class="big_tarozi_client_container">
      <div class="big_tarozi_header bg-white pt-2 px-3">
        <div class="row">
          <div class="col-3">
            <div>
              <erpSelect
              :options="client_list_last"
              @select="selectOptionUser"
              :selected="client_name"
              :label="$t('client_select_fio')"
              size="sm"
              class="bg-white"
              style="margin-top:0px;"
              url="/TegirmonClientNews/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
              />
              <!-- <small style="position:absolute; top:-8px; left:10px; font-size: 11px;" class="font-weight-bold px-2 py-0">Поиск клиент</small> -->
            </div>
          </div>
          <div class="col-3">
            <input-search  @select="selectClientPassport" :label="$t('')" :selected="passport_number"
              url="/TegirmonClientNews/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
              ref="search_client" placeholder="AA12345678"  style="height:32px;">
            </input-search>
          </div>
          <div class="col-3">
            <input-search-year  @select="selectClientBorn" type="text" :label="$t('')" :selected="born_date" placeholder="дд-мм-гггг"
              url="/TegirmonClientNews/getPaginationByBornDate?page=0&size=100&born_date_str="
              ref="search_client_born_date" style="height:32px;">
            </input-search-year>
          </div>
          <div class="col-3 text-right">
              <div class="product_btn_item_hisobot justify-content-end mt-0">
                <button @click="$router.push('/big_report')">Хисобот</button>
              </div>
          </div>
        </div>
      </div>
      <loader v-if="loading" />
      <div v-else>
        <div class="client_change_product_items px-3 mb-3" v-if="change_product_photo_status">
            <div class="row pt-3">
              <div class="col-9">
                <div class="client_ostatka_item ">
                  <div class="row">
                    <div class="col-6 px-3">
                      <div class="d-flex justify-content-between border-bottom">
                        <p class="product_name_item mt-2">
                          {{select_product_data.product_name}}
                        </p>
                        <p class="product_measure_item_change">
                          {{(used_all_measure_change_product - used_payed_measure_change_product).toFixed(1)}} кг
                        </p>
                      </div>
                    </div>
                    <div class="col-6 px-3">
                      <div class="d-flex justify-content-between border-bottom">
                        <p class="product_name_item mt-2">
                          Остатка
                        </p>
                        <p class="product_measure_item_change">
                          {{(select_product_data.ostatik_measure - used_all_measure_change_product).toFixed(1)}}
                        </p>
                      </div>
                    </div>
                  </div>
                  <div class="p-2 MainKlientBigTaroziList_main_change">
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
                        <tr v-for="(row,i) in item_list_change" :key="i">
                          <td>  <span >{{i+1}}</span></td>
                          <td><span > {{row.product_name}}</span></td>
                          <td> <span >{{row.qty}}</span></td>
                          <td> <span >{{(parseFloat(row.qty)/parseFloat(row.real_sum)).toFixed(1)}}</span> </td>
                          <td> <span >{{select_product_data.product_name}}</span> </td>
                        </tr>
                      </tbody>
                    </table>
                  </div>
                  <div class="d-flex justify-content-end">
                    <div class="product_btn_item_change mt-3">
                      <button class="btn_1 mr-1" @click="change_product_cancel()">{{$t('cancel')}}</button>
                      <button :disabled="!payed_image" @click="func_change_product_accept()">{{$t('accept')}}</button>
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-3">
                <div class="client_ostatka_item">
                  <div class="d-flex justify-content-between border-bottom">
                    <p class="product_name_item mt-2">
                      {{select_product_data.product_name}}
                    </p>
                    <p class="product_measure_item_change">
                      {{used_payed_measure_change_product}} кг
                    </p>
                  </div>
                  <div class="d-flex justify-content-center mt-2">
                    <small class="product_measure_item">{{used_payed_summ_change_product.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small>
                    <small class="product_name_item mt-2 ml-2">сўм</small>
                  </div>
                </div>
                <div class="client_ostatka_item mt-2">
                  <div class="image_change_product" @click="getPhoto()">
                    <div v-if="payed_image">
                      <img :src="hostname + payed_image" style="width:100%;" alt="" class="shadow border rounded">
                    </div>
                    <svg v-else xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-camera" width="40" height="40" viewBox="0 0 24 24" stroke-width="1.5" stroke="#597e8d" fill="none" stroke-linecap="round" stroke-linejoin="round">
                      <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                      <path d="M5 7h1a2 2 0 0 0 2 -2a1 1 0 0 1 1 -1h6a1 1 0 0 1 1 1a2 2 0 0 0 2 2h1a2 2 0 0 1 2 2v9a2 2 0 0 1 -2 2h-14a2 2 0 0 1 -2 -2v-9a2 2 0 0 1 2 -2" />
                      <path d="M9 13a3 3 0 1 0 6 0a3 3 0 0 0 -6 0" />
                    </svg>
                  </div>
                </div>
              </div>
            </div>
            
        </div>
        <div v-else class="client_rashshod_main_product container-fluid px-3">
          <div class="row pt-3">
            <div class="col-3" v-for="(item,i) in product_list" :key="i" v-show="item.ostatik_measure>1">
              <div class="client_ostatka_item">
                <p class="product_name_item">
                  {{ item.product_name }}
                </p>
                <p class="product_measure_item">
                  {{ item.ostatik_measure.toFixed(1) }} кг
                </p>
                <div class="product_icon_item" :style="{background: color[i]}">
                  <mdb-icon  icon="shopping-bag" class="text-white"/>
                </div>
                <div class="product_btn_item justify-content-end mt-1">
                  <button @click="func_payed(item)">Тулов</button>
                  <button class="btn_1" @click="change_product(item)">{{$t('change_product')}}</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div  class="px-2 py-2">
        <div class="p-2 MainKlientBigTaroziList_main">
          <loader-table v-if="loading_table"/>
          <table v-else class="myTableMainKlientBigTaroziList ">
            <thead>
              <tr class="header py-3 info_client_header">
                <th  width="40" class="text-left">№</th>
                <th>{{$t('client_name')}}</th>
                <th>{{$t('product')}}</th>
                <th>{{$t('qabul')}}</th>
                <th>{{$t('tulov')}}</th>
                <th>{{$t('status_')}}</th>
                <th>{{$t('date')}}</th>
                <th>{{$t('note')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,index) in invoice_list_history" 
                @click="show_client_invoice_item(row.id, row.status_1, row.status_2)"
                :key="index" 
                :class="{'bg_danger': row.status_1 == true && row.status_2 == true}">
                <td>  <span >{{index+1}}</span></td>
                <td><span > {{client_name}}</span></td>
                <td> <span >{{row.text_1}}</span></td>
                <td> <span v-if="row.status_1 == false" class="font-weight-bold">{{row.product_measure}} кг 
                </span> </td>
                <td> <span v-if="row.status_1 == true" class="font-weight-bold">{{row.product_measure}} кг 
                </span> </td>
                <td>
                  <small v-if="row.status_1 == true && row.status_2 == true">
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-refresh-alert" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                      <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                      <path d="M20 11a8.1 8.1 0 0 0 -15.5 -2m-.5 -4v4h4" />
                      <path d="M4 13a8.1 8.1 0 0 0 15.5 2m.5 4v-4h-4" />
                      <path d="M12 9l0 3" />
                      <path d="M12 15l.01 0" />
                    </svg>
                    <small class="ml-1 text-white">Rashshod</small>
                  </small> 
                  <small v-else-if="row.status_1 == true">
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-circle-check" width="25" height="25" viewBox="0 0 24 24" stroke-width="1" stroke="#00b341" fill="none" stroke-linecap="round" stroke-linejoin="round">
                      <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                      <path d="M12 12m-9 0a9 9 0 1 0 18 0a9 9 0 1 0 -18 0" />
                      <path d="M9 12l2 2l4 -4" />
                    </svg>
                    <small class="ml-1">To'lov</small>
                  </small> 
                  <small v-else> 
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-circle-chevron-down" width="25" height="25" viewBox="0 0 24 24" stroke-width="1" stroke="#ff2825" fill="none" stroke-linecap="round" stroke-linejoin="round">
                      <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                      <path d="M15 11l-3 3l-3 -3" />
                      <path d="M12 3a9 9 0 1 0 0 18a9 9 0 0 0 0 -18z" />
                    </svg>
                    <small class="ml-1">Qabul</small>
                  </small> 
                </td>
                <!-- <td> <span >{{row.date_first_pull}}</span> </td> -->
                <td> <span >{{row.date_second_pull.slice(8,10) + '-' + row.date_second_pull.slice(5,7) + '-' + row.date_second_pull.slice(0,4)}}</span> <span class="ml-2">{{row.date_second_pull.slice(11,16)}}</span> </td> 
                <td> <span >{{row.note}}</span> </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      <modal-train  :show="change_pro" headerbackColor="white"  titlecolor="black" :title="$t('change_product')" 
        @close="change_pro = false" width="80%">
          <template v-slot:body>
            <changeProduct @close="change_pro = false" ref="changer_refs_client_big_tarozi" 
              @addNewData="GetChangeProduct" 
              :row="rowIndex" 
              :product_name="select_product_data.product_name"
              :product_qty="select_product_data.ostatik_measure.toString()" 
              :product_id="select_product_data.product_id" 
              :product_price="select_product_data.product_price"
              :skidka_status="skidka_status">
            </changeProduct>
          </template>
      </modal-train>
      <webcam  v-show="showPhoto" @getPhotosub="takePhoto" ref="camera_component"/>
      
      <modal-new  :show="change_pro_1" :title="$t('tulov')" 
        @close="change_pro_1 = false" width="80%">
          <template v-slot:body>
            <pay_client ref="payed_client_comp" @close="close_pay_summ"/>
          </template>
      </modal-new>
      <modal-new  :show="tulov_invoice" :title="$t('history')" 
        @close="tulov_invoice = false" width="80%">
          <template v-slot:body>
            <info-tulov-invoice  :option="select_invoice_item_data"/>
          </template>
      </modal-new>
      <modal-new  :show="qabul_invoice" :title="$t('history')" 
        @close="qabul_invoice = false" width="80%">
          <template v-slot:body>
            <info-qabul-invoice  :option="select_invoice_item_data"/>
          </template>
      </modal-new>
      <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>

      <Toast ref="message"></Toast>
    </div>
  </template>
  
  <script>
  import {mdbInput, mdbBtn, mdbIcon } from 'mdbvue'
  import {mapActions,mapGetters} from 'vuex'
  import webcam from '../webcam/webcam_Add.vue'
  import addClientNew from './new_client_add.vue'
  import erpSelect from "../../components/erpSelectFioSearch.vue";
  import erpSelectProduct from "../../components/erpSelectProduct.vue";
  import InputSearch from '../../components/inputSearch';
  import inputSearchYear from '../../components/inputSearchYear';
  import changeProduct from '../mainPage/changeComponent.vue'
  import pay_client from './pay_client.vue';
  import InfoTulovInvoice from './infoTulovInvoice'
  import InfoQabulInvoice from './infoQabulInvoice'
  export default {
    components: {
      mdbInput, mdbBtn, mdbIcon,
      erpSelectProduct,
      addClientNew,
      erpSelect,
      InputSearch,
      inputSearchYear,
      webcam,
      changeProduct,
      pay_client,
      InfoTulovInvoice,
      InfoQabulInvoice
    },
    data() {
      return {
        hostname: this.$store.state.server_ip,
        search: '',
        modal_info: '',
        modal_status: false,
        loading: false,
        loading_table: false,
        showPhoto: false,
        add_client_show: false,
        change_pro: false,
        confirm: false,
        change_product_photo_status: false,
        rowIndex: 0,
        skidka_status: true,
        itemForChange: {
          product_measure: 0,
        },
        product_list: [
          {
            id:0,
            product_name: '',
            product_id: null,
            product_price: 0,
            product_measure: 0,
            summ: 0,
            ostatik_measure: 0,
          }
        ],
        photo_index: 0,
        client_name: '',
        client_id: null,
        client_info: {},
        passport_number: '',
        born_date: '',
        client_list_last: [],
        id: this.$route.params.id,
        ruyxat_id: 0,
        invoice_id: 0,
        invoice_list_table: [],
        invoice_list_history: [],
        color: ['#7068E4', '#EB503F', '#2DCEB2', '#FB9540'],
        change_pro_1: false,
        select_product_data: {
          ostatik_measure: 0,
          product_id: 0,
          product_measure:0,
          product_name:"Bugdoy",
          product_price: 0,
          summ: 0
        },
        select_product_invoice_list: [],
        invoice_list_selected: [],
        payed_image: '',
        used_all_measure_change_product: 0,
        used_payed_measure_change_product: 0,
        used_payed_summ_change_product:0,
        item_list_change: [],

        select_invoice_item_id: null,
        select_invoice_item_data: {},
        tulov_invoice: false,
        qabul_invoice: false,
        product_price_get_change_product: 0,

      }
    },
    async created()
    {
      if(this.id > 0)
      {
        await this.refresh();
      }
    },
    async mounted () {
      // await this.fetch_product_t();
      await this.fetchClientLast();
    },
    computed: mapGetters(['all_product_t', 'allClient']),
    methods: {
      ...mapActions(['fetch_product_t', 'fetchClient']),
      async func_payed(data){
        // console.log(data)
        this.change_pro_1 = true;
        this.$refs.payed_client_comp.refreshUpdate({client_id: this.client_id, product_id: data.product_id, client_name: this.client_name})
      },
      async close_pay_summ(){
        this.change_pro_1 = false;
        this.$refs.message.success('Added_successfully')
        await this.get_client_invoice(this.client_id);
        await this.get_client_invoice_not_paid(this.client_id);
      },
      async change_product(item){
        console.log(item)
        this.change_pro = true;
        this.select_product_data = item;
        console.log('item')
        this.$refs.changer_refs_client_big_tarozi.updateChanger(
          {
            zaxira_qty: 0,
            product_id: item.product_id,
            qty: item.ostatik_measure,
            skidka: 0,
          }
        );
        await this.fetch_product_invoice();
      },
      async fetch_product_invoice(){
        try{
          // this.loading = true;
          this.loading_table = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonInvoiceNews/getPaginationInvoiceNewNotPaidClientIdAndProductID?page=0&size=1000&client_id=" + this.client_id + '&product_id=' +  this.select_product_data.product_id);
          const data = await response.json();
          this.loading_table = false;
          if(response.status == 201 || response.status == 200)
          {
            this.select_product_invoice_list = data.items_list;
            console.log(data.items_list)
            console.log('this.select_product_invoice_list')
            return true;
          }
          else{
            const data_text = await response.text();
            this.modal_info = data_text;
            this.modal_status = true;
            return false;
          }
        }
        catch{
          // this.loading = false;
          this.modal_info = this.$i18n.t('network_ne_connect');
          this.modal_status = true;
        }
      },
      async change_product_cancel(){
        this.change_product_photo_status = false;
        this.item_list_change = [];
        this.payed_image = '';
      },
      async GetChangeProduct(option){
        this.invoice_list_selected = [];
        this.item_list_change = [];
        console.log('option')
        console.log(option)
        let change_unit_qty = 0;
        let used_measure_product = 0;
        this.change_pro = false;
        let summ_all = 0;
        for(let i = 0; i<option.data.length; i++){
          if(parseFloat(option.data[i].real_qty) > 0 && parseFloat(option.data[i].summ) == 0){
            let item = {
              tegirmonProductid: option.data[i].product_id,
              qty: option.data[i].real_qty,
              real_qty: option.data[i].real_qty,
              sum: option.data[i].summ,
              real_sum: option.data[i].persantage,
              id: option.data[i].id,
              product_name: option.data[i].product_name
            }
            let temp_kg = 0
            temp_kg = (parseFloat(item.qty)/parseFloat(item.real_sum)).toFixed(1);
            used_measure_product += parseFloat(temp_kg);
            this.item_list_change.push(item);
          }
          else if(parseFloat(option.data[i].summ)>0){
            change_unit_qty = option.data[i].real_qty;
          }
          summ_all += parseFloat(option.data[i].summ);
        }
        console.log('this.item_list_change');
        console.log(this.item_list_change);
        console.log('summ_all');
        console.log(summ_all);
        console.log('change_unit_qty');
        console.log(change_unit_qty);
        console.log('used_measure_product')
        this.change_product_photo_status = true;
        console.log(used_measure_product + parseFloat(change_unit_qty));
        this.used_payed_measure_change_product = parseFloat(change_unit_qty);
        this.used_payed_summ_change_product = parseFloat(summ_all);
        if(this.used_payed_summ_change_product>0){
          this.product_price_get_change_product = this.used_payed_summ_change_product/this.used_payed_measure_change_product
        }
        else{
          this.product_price_get_change_product = parseFloat(this.select_product_data.product_price);
        }
        this.used_all_measure_change_product = (used_measure_product + parseFloat(change_unit_qty)).toFixed(1);
        // await this.updateInvoiceGet(this.used_all_measure_change_product);
      },
      async func_change_product_accept(){
        await this.updateInvoiceGet(this.used_all_measure_change_product);
      },
      async updateInvoiceGet(temp_summ){
        for(let i=this.select_product_invoice_list.length-1; i>=0; i--){
          if(temp_summ == 0){
            break;
          }
          else if(temp_summ>=this.select_product_invoice_list[i].credit_sum){
            temp_summ = temp_summ - parseFloat(this.select_product_invoice_list[i].credit_sum);
            let item_inv = {
              index: i,
              invoice_id: this.select_product_invoice_list[i].id,
              status: true,
              ostatka: 0,
              payed_measure: 0,
              payed_summ: 0,
              summ: 0, 
            }
            this.invoice_list_selected.push(item_inv);
          }
          else{
            let item_inv = {
              index: i,
              invoice_id: this.select_product_invoice_list[i].id,
              status: false,
              ostatka: 0,
              payed_summ: 0,
              payed_measure: 0,
              summ: 0,
            }
            item_inv.ostatka = parseFloat(this.select_product_invoice_list[i].credit_sum) - parseFloat(temp_summ);
            let use_payed_summ = 0;
            use_payed_summ = parseFloat(this.select_product_invoice_list[i].product_price)*parseFloat(temp_summ)
            item_inv.summ = parseFloat(this.select_product_invoice_list[i].summ) - parseFloat(use_payed_summ);
            this.invoice_list_selected.push(item_inv);
            temp_summ = 0;
          }
        }
        for(let j=0; j<this.invoice_list_selected.length; j++){
            this.select_product_invoice_list[this.invoice_list_selected[j].index].status_2 = this.invoice_list_selected[j].status;
            this.select_product_invoice_list[this.invoice_list_selected[j].index].credit_sum = this.invoice_list_selected[j].ostatka;
            this.select_product_invoice_list[this.invoice_list_selected[j].index].payed_summ = this.invoice_list_selected[j].payed_summ;
            this.select_product_invoice_list[this.invoice_list_selected[j].index].dolg_summ = this.invoice_list_selected[j].payed_measure;
            this.select_product_invoice_list[this.invoice_list_selected[j].index].summ = this.invoice_list_selected[j].summ;

          await this.updateInvoiceItems(this.invoice_list_selected[j].index, this.select_product_invoice_list[this.invoice_list_selected[j].index].product_price, this.invoice_list_selected[j].summ)
        }
        this.loading = true;
          await this.createNewInvoice();
        this.loading = false;

      },
      async updateInvoiceItems(index, product_price, summ){
        console.log(this.select_product_invoice_list[index])
        const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "shafyor_name" : this.select_product_invoice_list[index].shafyor_name,
            "car_number" : this.select_product_invoice_list[index].car_number,
            "shafyor_tel" : this.select_product_invoice_list[index].shafyor_tel,
            "note": this.select_product_invoice_list[index].note,
            "tegirmonProductid": this.select_product_invoice_list[index].TegirmonProductid,
            "product_price": product_price,
            "text_1" : this.select_product_invoice_list[index].text_1,
            "image_first_pull": this.select_product_invoice_list[index].image_first_pull,
            "image_second_pull": this.select_product_invoice_list[index].image_second_pull,
            "image_last_pull": this.payed_image,
            "all_measure": this.select_product_invoice_list[index].all_measure,
            "discount":  this.select_product_invoice_list[index].discount,
            "car_measure":  this.select_product_invoice_list[index].car_measure,
            "product_measure":  this.select_product_invoice_list[index].product_measure,
            "credit_sum": this.select_product_invoice_list[index].credit_sum,
            "dolg_summ": this.select_product_invoice_list[index].dolg_summ,
            "payed_summ": this.select_product_invoice_list[index].payed_summ,
            "tegirmonClientNewid": this.client_id,
            "summ": summ,
            "text_2": this.select_product_invoice_list[index].text_2,
            "id" : this.select_product_invoice_list[index].id,
            "pull_status": this.select_product_invoice_list[index].pull_status,
            "rashshod_status": this.select_product_invoice_list[index].rashshod_status,
            "finish_status":this.select_product_invoice_list[index].finish_status,
            "auth_first_pull_id": this.select_product_invoice_list[index].auth_first_pull_id,
            "auth_first_pull_name": this.select_product_invoice_list[index].auth_first_pull_name,
            "auth_second_pull_id": this.select_product_invoice_list[index].auth_second_pull_id,
            "auth_second_pull_name": this.select_product_invoice_list[index].auth_second_pull_name,
            "auth_last_pull_id": localStorage.AuthId,
            "auth_last_pull_name": localStorage.user_name,
            "tegirmonAuthid": localStorage.AuthId,
            "qty_real": this.select_product_invoice_list[index].qty_real,
            "send_group_id": this.select_product_invoice_list[index].send_group_id,
            "date_first_pull": this.select_product_invoice_list[index].date_first_pull,
            "date_second_pull": this.select_product_invoice_list[index].date_second_pull,
            "status_2": this.select_product_invoice_list[index].status_2
          })
        };
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonInvoiceNews", requestOptions);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            let data = response.json();
            console.log('data ichiga kirdu');
            console.log(data);
            this.$refs.message.success('Added_successfully')

            return true;
          }
          else{
            console.log('else bulimida');
            return false;
          }
        }
        catch(error){
          console.log('error catch bulimida else bulimida');
          console.log(error);
          return false;
        }
      },
      async createNewInvoice(){
        const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
              "tegirmonProductid": this.select_product_data.product_id,
              "text_1" : this.select_product_invoice_list[0].text_1,
              "product_price": this.product_price_get_change_product,

              "image_first_pull": this.payed_image,
              "image_second_pull": this.payed_image,
              "image_last_pull": this.payed_image,

              "all_measure": this.select_product_data.ostatik_measure,
              "product_measure":  this.used_all_measure_change_product,
              "summ": 0,
              "discount":  0,
              "car_measure":  0,
              "credit_sum": parseFloat(this.select_product_data.ostatik_measure) - parseFloat(this.used_all_measure_change_product), //ostatka
              "dolg_summ": this.used_payed_measure_change_product, // payed measure
              "payed_summ": this.used_payed_summ_change_product,



              "tegirmonClientNewid": this.client_id,
              "text_2": this.client_name,

              "id" : 0,
              "pull_status": true,
              "rashshod_status": true,
              "finish_status":true,
              "auth_first_pull_id": localStorage.AuthId,
              "auth_first_pull_name": localStorage.user_name,
              "auth_second_pull_id": localStorage.AuthId,
              "auth_second_pull_name": localStorage.user_name,
              "auth_last_pull_id": localStorage.AuthId,
              "auth_last_pull_name": localStorage.user_name,
              "tegirmonAuthid": localStorage.AuthId,
              "qty_real": 0, // group qty
              // "send_group_id": this.invoice_list_history[i].send_group_id,
              "status_1": true,
              "item_list": this.item_list_change
            })
          };
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/TegirmonInvoiceNews", requestOptions);
            this.loading = false;
            if(response.status == 201 || response.status == 200)
            {
              let data = response.json();
              console.log(data);
              this.change_product_photo_status = false;
              this.payed_image = '';
              if(parseFloat(this.select_product_data.ostatik_measure) - parseFloat(this.used_all_measure_change_product) < 1){
                console.log('kirdi ichiga nimaga ishlamadi')
                await this.createNewInvoiceNol();
              }
                await this.get_client_invoice(this.client_id);
                await this.get_client_invoice_not_paid(this.client_id);
              return true;
            }
            else{
              return false;
            }
          }
          catch(error){
            console.log(error);
            return false;
          }
      },
      async createNewInvoiceNol(){
        const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
              "tegirmonProductid": this.select_product_data.product_id,
              "text_1" : this.select_product_invoice_list[0].text_1,
              "product_price": this.select_product_data.product_price,
              "all_measure": 0,
              "product_measure":  0,
              "summ": 0,
              "discount":  0,
              "car_measure":  0,
              "credit_sum": 0, //ostatka
              "dolg_summ": 0, // payed measure
              "payed_summ": 0,

              "tegirmonClientNewid": this.client_id,
              "text_2": this.client_name,

              "id" : 0,
              "pull_status": true,
              "rashshod_status": true,
              "finish_status":true,
              "auth_last_pull_id": localStorage.AuthId,
              "auth_last_pull_name": localStorage.user_name,
              "tegirmonAuthid": localStorage.AuthId,
              "qty_real": 0, // group qty
              // "send_group_id": this.invoice_list_history[i].send_group_id,
              "status_1": true,
              "status_2": true,
            })
          };
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/TegirmonInvoiceNews", requestOptions);
            this.loading = false;
            if(response.status == 201 || response.status == 200)
            {
              let data = response.json();
              console.log(data);
              return true;
            }
            else{
              return false;
            }
          }
          catch(error){
            console.log(error);
            return false;
          }
      },
      async fetchClientLast(){
            try{
              // this.loading = true;
              const response = await fetch(this.$store.state.hostname + "/TegirmonClientNews/getPagination?page=0&size=100");
              const data = await response.json();
              if(response.status == 201 || response.status == 200)
              {
                this.client_list_last = data.items_list;
                return true;
              }
              else{
                const data_text = await response.text();
                this.modal_info = data_text;
                this.modal_status = true;
                return false;
              }
            }
            catch{
              // this.loading = false;
              this.modal_info = this.$i18n.t('network_ne_connect');
              this.modal_status = true;
            }
      },
      getPhoto(){
        this.showPhoto = true; 
        this.$refs.camera_component.createCameraElement()
      },
      takePhoto(data){
        this.payed_image = data;
        this.showPhoto = false;
      },

      // Optom klientlarni tanlash bulimi
      async selectOptionUser(option){
        this.client_info = option;
        this.client_name = option.fio;
        this.client_id = option.id;
        this.passport_number = option.passport_number
        await this.get_client_invoice(option.id);
        await this.get_client_invoice_not_paid(option.id);
        this.change_product_photo_status = false;
        if(option.addiotionala_information){
          this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
        }
      },
      async selectClientPassport(option){
        this.client_info = option;
        this.client_name = option.fio;
        this.client_id = option.id;
        this.passport_number = option.passport_number
        await this.get_client_invoice(option.id);
        await this.get_client_invoice_not_paid(option.id);
        this.change_product_photo_status = false;
        if(option.addiotionala_information){
          this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
        }
      },
      async selectClientBorn(option){
        this.client_info = option;
        this.client_name = option.fio;
        this.client_id = option.id;
        this.passport_number = option.passport_number;
        await this.get_client_invoice(option.id);
        await this.get_client_invoice_not_paid(option.id);
        this.change_product_photo_status = false;
        if(option.addiotionala_information){
          this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
        }
      },
      // Optom klientlarni tanlash bulimi

      async get_client_invoice(client_id){
            try{
              // this.loading = true;
              this.loading_table = true;
              const response = await fetch(this.$store.state.hostname + "/TegirmonInvoiceNews/getPaginationInvoiceNewClientId?page=0&size=1000&client_id=" + client_id);
              const data = await response.json();
              this.loading_table = false;
              if(response.status == 201 || response.status == 200)
              {
                this.invoice_list_history = data.items_list;
                console.log(this.invoice_list_history)
                console.log('this.invoice_list_history')
                return true;
              }
              else{
                const data_text = await response.text();
                this.modal_info = data_text;
                this.modal_status = true;
                return false;
              }
            }
            catch{
              // this.loading = false;
              this.modal_info = this.$i18n.t('network_ne_connect');
              this.modal_status = true;
            }
       },
      async get_client_invoice_not_paid(client_id){
            try{
              // this.loading = true;
              const response = await fetch(this.$store.state.hostname + "/TegirmonInvoiceNews/getPaginationInvoiceNewNotPaidClientId?page=0&size=1000&client_id=" + client_id);
              const data = await response.json();
              if(response.status == 201 || response.status == 200)
              {
                this.invoice_list_table = [];
                this.invoice_list = data.items_list;
                console.log('this.invoice_list')
                console.log(this.invoice_list)
                await this.fetch_invoice_list_sort();
                return true;
              }
              else{
                const data_text = await response.text();
                this.modal_info = data_text;
                this.modal_status = true;
                return false;
              }
            }
            catch{
              // this.loading = false;
              this.modal_info = this.$i18n.t('network_ne_connect');
              this.modal_status = true;
            }
       },
       async fetch_invoice_list_sort(){
            let sort_bulgan_list = [];
            for(let i=0; i<this.invoice_list.length; i++){
                let sort_item_list = [];
                this.invoice_list_table.push(this.invoice_list[i])
                sort_item_list.push(this.invoice_list[i]);
                for(let j=i+1; j<this.invoice_list.length; j++){
                    if(this.invoice_list[i].TegirmonProductid == this.invoice_list[j].TegirmonProductid){
                        sort_item_list.push(this.invoice_list[j]);
                        this.invoice_list_table.push(this.invoice_list[j])
                        this.invoice_list.splice(j,1);
                        j--;
                    }
                }
                sort_bulgan_list.push(sort_item_list);
            }
            console.log('sort_bulgan_list')
            this.sort_invoice_list = sort_bulgan_list;
            console.log(sort_bulgan_list)
            await this.refresh_invoice_item();
       },
       async refresh_invoice_item(){
            if(this.sort_invoice_list.length == 0){
              this.product_list = [
                {
                  id:0,
                  product_name: '',
                  product_id: null,
                  product_price: 0,
                  product_measure: 0,
                  summ: 0,
                  ostatik_measure: 0,
                }
              ];
            }
            else{
              this.product_list = [];
            }
            for(let i=0; i<this.sort_invoice_list.length; i++){
                let item_invoice = {
                    product_name: '',
                    product_id: null,
                    product_price: 0,
                    product_measure: 0,
                    summ: 0,
                    ostatik_measure: 0,   //data.credit_sum,
                };
                for(let j=0; j<this.sort_invoice_list[i].length; j++){
                    item_invoice.product_measure += parseFloat(this.sort_invoice_list[i][j].product_measure);
                    item_invoice.ostatik_measure += parseFloat(this.sort_invoice_list[i][j].credit_sum);
                    item_invoice.summ += parseFloat(parseFloat(this.sort_invoice_list[i][j].credit_sum) * parseFloat(this.sort_invoice_list[i][j].product_price));
                    item_invoice.product_id = this.sort_invoice_list[i][j].TegirmonProductid;
                    item_invoice.product_name = this.sort_invoice_list[i][j].text_1;
                    item_invoice.product_price = this.sort_invoice_list[i][j].product_price;
                }
                this.product_list.push(item_invoice);
            }
       },

       async show_client_invoice_item(id, status_1, status_2){
          console.log(id, status_1, status_2);
          this.select_invoice_item_id = id;
          if(status_1){
            this.tulov_invoice = true;
            await this.fetch_invoice_full_info_id(id);
          }
          else if(status_2 && status_1){
            console.log('Rashshod')
          }
          else{
            this.qabul_invoice = true;
            await this.fetch_invoice_full_info_id(id);
          }
       },
       async fetch_invoice_full_info_id(id){
        this.select_invoice_item_data = {};
        console.log(id)
        try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonInvoiceNews/getAnyInvoiceNewFullInfoById?invoice_id=' + id);
          const data = await res.json();
          if(res.status == 200 || res.status == 201){
            console.log('this is by id created')
            console.log(data)   
            this.select_invoice_item_data = data       
          }
        }
      catch(error){
        console.log(error);
      }

       }
    },
  }
  </script>
  
  <style lang="scss" scoped>
  ::-webkit-scrollbar {
    width: 5px;
  }
  
  /* Track */
  ::-webkit-scrollbar-track {
    background: #bac0c6;
  }
  
  /* Handle */
  ::-webkit-scrollbar-thumb {
    background: #898d92;
  }
  
  /* Handle on hover */
  ::-webkit-scrollbar-thumb:hover {
    background: #767f8b;
  }
  .big_tarozi_client_container{
    background: #F1F4F5;
  }
  .big_tarozi_header{
    box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
  }
  .client_rashshod_main_product{
    height: 300px;
  }
  .client_change_product_items{
    min-height: 300px;
  }
  .image_change_product{
    min-height: 150px;
    display: flex;
    justify-content:center;
    align-items: center;
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
  .product_measure_item{
    font-weight: 700;
    margin:0;
    margin-top: 7px;
    margin-left: 3px;
    font-size: 18px;
    color:#344767;
  }
  .product_measure_item_change{
    font-weight: 700;
    margin:0;
    margin-top: 7px;
    margin-left: 3px;
    font-size: 18px;
    color:#798496;
  }
  .product_icon_item{
    position: absolute;
    top: 15px;
    right: 15px;
    background: #7068E4;
    width: 40px;
    height: 40px;
    border-radius: 50%;
    display: flex;
    justify-content: center;
    align-items: center;
  }
  .product_btn_item{
    display:flex;
    align-items: center;
  }
  .product_btn_item button{
    outline: none;
    border: 1.4px solid #2DCEB2;
    border-radius: 6px;
    padding: 4px 12px;
    font-size: 13px;
    cursor:pointer;
    background: white;
    margin-left: 7px;
  }
  .product_btn_item button:hover{
    box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
    background: #2DCEB2;
    font-weight: bold;
    color: white;
  }
  .product_btn_item_hisobot{
    display:flex;
    align-items: center;
  }
  .product_btn_item_hisobot button{
    outline: none;
    border: 1.4px solid #8bd0e9;
    border-radius: 6px;
    padding: 4px 12px;
    font-size: 13px;
    cursor:pointer;
    background: white;
    margin-left: 7px;
  }
  .product_btn_item_hisobot button:hover{
    box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
    background: #8bd0e9;
    font-weight: bold;
    color: white;
  }
  
  .product_btn_item .btn_1{
    outline: none;
    border: 1.4px solid #FB9540;
    border-radius: 6px;
    padding: 4px 12px;
    font-size: 13px;
    cursor:pointer;
    background: white;
    margin-left: 7px;
  }
  .product_btn_item .btn_1:hover{
    box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
    background: #FB9540;
    color: white;
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

  .MainKlientBigTaroziList_main{
    height: calc(100vh - 400px);
    background: white;
    border-radius: 10px;
    overflow: hidden;
    overflow-y: scroll;
    box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
  }
  .MainKlientBigTaroziList_main_change{
    min-height: 100px;
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
  .bg_danger{
    background: rgb(255, 124, 100) !important;
  }
  </style>
<template>
 <div class="mainShowGroup pt-0 px-1">
    <checkgroup v-if="checkShow" @close="closeAllGroup"/>
    <div v-else class="groupList">
      <div class="header_group rounded  px-2 mb-1 py-1">
        <div class="row">
          <div class="col-3">
            <div class="d-flex  align-items-center">
              <small style="font-size: 12px;">Имя водителя: </small>
              <small style="font-size: 12px;" class="ml-4 font-weight-bold">{{group_data.shafyor_name}}</small>
            </div>
          </div>
          <div class="col-3">
            <div class="d-flex  align-items-center">
              <small style="font-size: 12px;">Номер машины: </small>
              <small style="font-size: 12px;" class="ml-4 font-weight-bold">{{group_data.car_number}}</small>
            </div>
          </div>
          <div class="col-3">
            <div class="d-flex  align-items-center">
              <small style="font-size: 12px;">Примечание: </small>
              <small style="font-size: 12px;" class="ml-4 font-weight-bold">{{group_data.note}}</small>
            </div>
          </div>
          <div class="col-3" v-if="group_data.qabul_qilgan_user_name">
            <div class="d-flex  align-items-center">
              <small style="font-size: 12px;">Тарозичи: </small>
              <small style="font-size: 12px;" class="ml-4 font-weight-bold">{{group_data.qabul_qilgan_user_name}}</small>
            </div>
          </div>
        </div>
      </div>
      <div class="container-fluid mb-2" v-if="group_data.qabul_qilgan_user_name">
        <div class="row ">
          <div class="col-6 m-0 px-2">
            <div class="d-flex  align-items-center">
              <small style="font-size: 12px;">Кассир: </small>
              <small style="font-size: 10px;" class="ml-4 mt-1 font-weight-bold">{{group_data.name}}</small>
            </div>
          </div>
          <div class="col-6 m-0 px-2">
            <div class="d-flex  align-items-center justify-content-end">
              <small style="font-size: 12px;">Дата: </small>
              <small style="font-size: 10px;" class="ml-4 mt-1 font-weight-bold">{{group_data.updated_date_time.slice(8,10) + '-' + group_data.updated_date_time.slice(5,7) + '-' + group_data.updated_date_time.slice(0,4)}} <span class="ml-1">{{group_data.updated_date_time.slice(11,16)}}</span></small>
            </div>
          </div>
        </div>
      </div>
      <loader v-if="loading"/>
      <div v-else class="backColor rounded mx-2 mb-4 py-4 px-2 border"
        v-for="(item,indexRow) in invoice_list" :key="indexRow"
        :class="{'bg_danger': (item.check_qty + item.zaxira_qty)  > item.qty+1 || (item.check_qty + item.zaxira_qty) < item.qty-1 }"
        >
        <div class="row border-bottom">
          <div class="col-3 appleid" v-show="false">
            <erpSelectFio
              :options="allClient.rows"
              @select="selectOptionUser"
              :selected="item.client_name"
              :row="indexRow"
              :label="$t('client')"
                url="/TegirmonClient/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
            />
            <small
              style="position: absolute; top: -7px; left: 20px; font-size: 11px"
              class="bg-white px-2 py-0"
              >{{ $t("client") }}</small
            >
          </div>
          <div class="col-3">
            <mdb-input class="m-0 p-0 bg-white" disabled v-model="item.client_new_name" size="sm"  outline  group type="text" validate error="wrong" success="right"/>
              <small
                style="position: absolute; top: -15px; left: 15px; font-size: 11px"
                class="font-weight-bold px-2 py-0"
                >{{ $t("client") }}</small
              >
          </div>
          <div class="col-3">
            <mdb-input class="m-0 p-0 bg-white" disabled v-model="item.product_name" size="sm"   outline  group type="text" validate error="wrong" success="right"/>
              <small
                style="position: absolute; top: -15px; left: 15px; font-size: 11px"
                class="font-weight-bold px-2 py-0"
                >{{ $t("product") }}</small
              >
          </div>
          <div class="col-3">
            <mdb-input class="m-0 p-0 bg-white" disabled v-model="item.qty" size="sm"   outline  group type="number" validate error="wrong" success="right"/>
              <small
                style="position: absolute; top: -15px; left: 15px; font-size: 11px"
                class="font-weight-bold px-2 py-0"
                >Общий масса</small
              >
          </div>
          <div class="col-1">
            <mdb-input class="m-0 p-0 bg-white" disabled v-model="item.skidka" size="sm"   outline  group type="number" validate error="wrong" success="right"/>
              <small
                style="position: absolute; top: -15px; left: 15px; font-size: 11px"
                class="font-weight-bold px-2 py-0"
                >{{$t('skidka')}}</small
              >
          </div>
          <div class="col-2">
            <mdb-input  disabled :value="(item.qty - (item.check_qty + item.zaxira_qty)).toFixed(1)" size="sm"   
              outline  group type="number" validate error="wrong" success="right"
              class="m-0 p-0 bg-white" />
              <small
                style="position: absolute; top: -15px; left: 15px; font-size: 11px"
                class="font-weight-bold px-2 py-0"
                >Остатка масса</small
              >
          </div>
          <div class="col-6 mt-1">
            <mdb-input class="m-0 p-0" v-model="item.note" size="sm" outline group type="text" validate error="wrong" success="right"/>
            <small
              style="position: absolute; top: -10px; left: 20px; font-size: 11px"
              class="bg-white font-weight-bold px-2 py-0"
              >{{ $t("reason") }}</small>
          </div>
          
          <div class="col-6 mt-1">
            <div class="text-right">
              <mdb-btn  v-show="false" @click="sendZaxira(item,indexRow)" color="success" m="r2" style="font-size: 8.5px"
                p="r4 l4 t2 b2"> <mdb-icon fas class="mr-1"  icon="share-square"></mdb-icon>  {{$t('zaxira')}}
              </mdb-btn>
              <mdb-btn :disabled="item.zaxira_qty == item.qty" v-show="item.ruyxat_id>0"  @click="changingAdd(item,indexRow)" color="info" m="r2" style="font-size: 8.5px"
                p="r3 l3 t2 b2"> <mdb-icon fas class="mr-2"  icon="sync"></mdb-icon>  {{$t('changing')}}
              </mdb-btn>
            </div>
          </div>
        </div>

        <!-- Sotilgan mahsulot -->
        <div class="text-center" v-if="item.summ>0">
          <span style="font-size: 12px;">Sotilgan mahsulot</span>
        </div>
        <div class="" v-if="item.summ>0">
          <table class="myTablegroupList">
            <thead>
              <tr class="header py-3" style="background: #ebcaff;">
                <th  width="40" class="text-left">№</th>
                <th>{{$t('product')}}</th>
                <th>{{$t('measure')}}</th>
                <th>{{$t('summ')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td> <span >{{1}}</span> </td>
                <td> <span >{{item.product_name}}</span>  </td>
                <td> <span >{{(item.invoice_note).toFixed(1)}}</span> <span>кг</span></td>
                <td> <span >{{item.summ}}</span> <span>сум</span></td>
              </tr>
            </tbody>
          </table>
        </div>
        <!-- Sotilgan mahsulot -->
        
        <!-- Almashtirilgan mahsulot -->
        <div class="text-center" v-if="item.changeProduct.length>0">
          <span style="font-size: 12px;">Almashtirilgan mahsulot</span>
        </div>
        <div class="row mx-0" v-if="item.changeProduct.length>0">
          <div class="col-12 p-0">
            <table class="myTablegroupList ">
              <thead>
                <tr class="header py-3" style="background: #c7e0ff;">
                  <th  width="40" class="text-left">№</th>
                  <th>{{$t('product')}}</th>
                  <th>{{$t('measure')}}</th>
                  <th>{{$t('summ')}}</th>
                  <th>{{item.product_name}}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,index) in item.changeProduct" :key="index" v-show="row.real_qty != 0"
                  :style="{'background': row.color}">
                  <td> <span >{{index+1}}</span> </td>
                  <td> <span >{{row.product_name}}</span> </td>
                  <td> <span >{{row.real_qty}}</span>  <span>кг</span></td>
                  <td> <span >{{row.summ}}</span> </td>
                  <td> <span >{{row.persantage}}</span>  <span>кг</span></td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
        <!-- Almashtirilgan mahsulot -->

        <!-- Zaxiraga olinganlar -->
        <div class="text-center" v-if="item.zaxira_invoice.length>0">
          <span style="font-size: 12px;">Zaxiraga olinganlar</span>
        </div>
        <div class="row mx-0" v-if="item.zaxira_invoice.length>0">
          <loader-table v-if="loading_table"/>
          <div v-else class="col-12 p-0">
            <table class="myTablegroupList ">
              <thead>
                <tr class="header py-3" style="background: #ceffd3;">
                  <th  width="40" class="text-left">№</th>
                  <th>{{$t('client')}}</th>
                  <th>{{$t('product')}}</th>
                  <th>{{$t('measure')}}</th>
                  <!-- <th>{{$t('summ')}}</th> -->
                  <th>{{$t('user')}}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,i) in item.zaxira_invoice" :key="i" >
                  <td> <span >{{i+1}}</span> </td>
                  <td> <span >{{row.client.fio}}</span> </td>
                  <td> <span>{{item.product_name}}</span> </td>
                  <td> <span >{{row.qty_real}}</span>  <span>кг</span></td>
                  <!-- <td> <span >{{row.summ}}</span> </td> -->
                  <td> 
                    <span v-if="row.tegirmonAuthid">{{row.auth.user.fio}}</span>
                    <span v-else>Foydalanuvchi topilmadi</span>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
        <!-- Zaxiraga olinganlar -->
      </div>

        <!-- Eski zaxira malumotlari -->
      <div class="d-flex justify-content-center mt-3" v-if="zaxiraInvoiceList.length">
        <h4>{{$t('zaxira')}}</h4>
      </div>
      <div class="row py-2 mx-2 mb-3 shadow bg-success" v-for="(item,i) in zaxiraInvoiceList" :key="i+500">
        <div class="col-4 mt-3">
          <mdb-input class="m-0 p-0" :value="item.client.fio" size="md"  outline group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -15px; left: 15px; font-size: 11px"
            class="bg-white px-2 py-0"
            >{{ $t("client") }}</small>
        </div>
        <div class="col-4 mt-3">
          <mdb-input class="m-0 p-0" :value="item.product.name" size="md" disabled outline group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -15px; left: 15px; font-size: 11px"
            class="bg-white px-2 py-0"
            >{{ $t("product") }}</small>
        </div>
        <div class="col-4 mt-3">
          <mdb-input class="m-0 p-0" :value="item.qty_real" size="md" disabled outline group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -15px; left: 15px; font-size: 11px"
            class="bg-white px-2 py-0"
            >{{ $t("qty") }}</small>
        </div>
        <div class="col-4 mt-3">
          <mdb-input class="m-0 p-0" :value="item.note" size="md" disabled outline group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -15px; left: 15px; font-size: 11px"
            class="bg-white px-2 py-0"
            >{{ $t("note") }}</small>
        </div>
        <div class="col-4 mt-3">
          <mdb-input class="m-0 p-0" :value="item.client.passport_number" size="md"  outline group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -15px; left: 15px; font-size: 11px"
            class="bg-white px-2 py-0"
            >{{ $t("client_name") }}</small>
        </div>
        <div class="col-4 mt-3">
          <mdb-input class="m-0 p-0" :value="item.auth.user.fio" size="md" disabled outline group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -15px; left: 15px; font-size: 11px;"
            class="bg-white px-2 py-0"
            >Кассир</small>
        </div>
      </div>
      <!-- Eski zaxira malumotlari -->


      <div class="blue-gradient">
        <hr class="mt-1"/>
      </div>
      <mdb-row class="mt-0 mx-2">
        <mdb-col col="12">
          <div class="mt-0 mr-3 text-right">
            <mdb-btn color="success"  @click="sendAll" style="font-size: 10px"
              p="r4 l4 t2 b2">
            <mdb-icon  />{{$t('check')}}</mdb-btn>
      </div>
        </mdb-col>
      </mdb-row>

      <modal-train  :show="change_pro" headerbackColor="white"  titlecolor="black" :title="$t('change_product')" 
        @close="change_pro = false" width="80%">
          <template v-slot:body>
            <changeProduct @close="change_pro = false" ref="changer" @addNewData="GetChangeProduct" :row="rowIndex" :product_name="itemForChange.product_name"
              :product_qty="itemForChange.qty" :product_id="itemForChange.product_id" :product_price ="itemForChange.product_price">
            </changeProduct>
          </template>
      </modal-train>
      <massage_box :hide="modal_status" :detail_info="modal_info"
        :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>

      <Toast ref="message"></Toast>
    </div>
 </div>
</template>

<script>
import checkgroup from '../sell/checkgroup'
import changeProduct from './changeComponent.vue'
import erpSelectFio from "../../components/erpSelectFioSearchRow.vue";
import erpSelect from "../../components/erpSelectRow.vue";
import {mapActions,mapGetters, mapMutations} from 'vuex'
import {mdbInput, mdbRow, mdbCol, mdbBtn, mdbIcon} from 'mdbvue'
export default {
  components: {
    erpSelect,mdbInput,erpSelectFio,checkgroup,
     mdbRow, mdbCol, mdbBtn,mdbIcon,changeProduct
  },
  data() {
    return {
      id: this.$route.params.id,
      modal_info: '',
      modal_status: false,
      loading: false,
      change_pro: false,
      loading_table: false,

      rowIndex: null,
      itemForChange: {},

      checkShow: false,

      invoice_list: [],
      change_unit_qty: 0,
      group_data: {},
      zaxiraInvoiceList: [],
    }
  },
  async created()
  {
    if(this.id > 0)
    {
      const res = await fetch(this.$store.state.hostname + '/TegirmonTortilganBugdoyRoyxatiGroupDetails/getPaginationByGroupId?page=0&size=1000&group_id=' + this.id);
      const data = await res.json();
      if(data.items_list == 0){
        console.log('this is by id created')
      }
      else{
        await this.fetch_group_data();
        await this.fetchInvoice(data.items_list);
      }
      await this.fetchZaxiraList();

    }
  },
  async mounted(){
    // await this.fetchClient();
  },
  computed: mapGetters([ 'allClient', 'all_product_t']),
  methods: {
    ...mapActions(['fetchClient', 'fetch_product_t']),
    ...mapMutations(['getAllCheck', 'zaxiragaUtkazishList']),
    async fetchZaxiraList(){
      this.zaxiraInvoiceList = [];
      let string = this.group_data.name;
      let arrayList = string.split(",");
      for(let i=0; i<arrayList.length-1; i++){
        let Invoice_Id = parseFloat(arrayList[i])
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/getAnyInvoiceFullInfoById?invoice_id=" + Invoice_Id);
          const data = await response.json();
          console.log('invoice')
          console.log(data)
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            this.zaxiraInvoiceList.push(data);
            // this.$refs.message.success('Added_successfully')
          }
          else{
            this.modal_info = this.$i18n.t('network_ne_connect');
            this.modal_status = true;
          }
        }
        catch{
          this.loading = false;
          this.modal_info = this.$i18n.t('network_ne_connect');
          this.modal_status = true;
        }
      }
      console.log('this.zaxiraInvoiceList')
      console.log(this.zaxiraInvoiceList)
    },
    async fetch_group_data(){
      try{
        const response = await fetch(this.$store.state.hostname + '/TegirmonTortilganBugdoyRoyxatiGroups/' + this.id);
        const data = await response.json();
        console.log('group data')
        console.log(data)
        if(response.status == 200 || response.status == 201){
          this.group_data = data;
        }
      }
      catch(error){
        console.log(error);
      }
    },
    async delGrroupEmpty(){
      if(this.invoice_list.length == 0){
        await this.fetchAcceptGroup();
      }
    },
    sendZaxira(data, index){
      console.log(index)
      this.zaxiragaUtkazishList(data);
      this.$router.push('/getProductFromGroup')
    },
    async fetchInvoice(data){
      this.loading = true;
      console.log('group details all')
      console.log(data)
      this.invoice_list = [];
      for(let i=0; i<data.length;i++){
        let info = {
          client_name: '',
          client_id: 0,
          client_new_name: '',
          product_id: null,
          product_name: '',
          product_price: 0,
          ruyxat_id: 0,
          note: '',
          group_note: '',
          qty: '0',
          real_qty: 0,
          check_qty: 0,
          summ:0,
          invoice_id: 0,
          group_detail_id: 0,
          group_id: this.id,
          skidka: 0,
          zaxira_qty: 0,
          zaxira_id: 0,
          details_name: '',
          details_array: [],
          zaxira_invoice: [],
          changeProduct: [
          ],
          invoice_note: 0,
          kassir: '',
          date: '',
        }
        if(data[i].invoice.note == null || data[i].invoice.note == ''){
          info.invoice_note = 0;
        }
        else{
          info.invoice_note = parseFloat(data[i].invoice.note)
        }
        info.date = this.group_data.updated_date_time;
        info.group_note = data[i].note;
        info.group_detail_id = data[i].id;
        info.ruyxat_id = data[i].TegirmonTortilganBugdoyRoyxatiid;
        info.invoice_id = data[i].TegirmonInvoiceid;
        info.client_new_name = data[i].royxati.client_name;
        info.client_id = data[i].TegirmonClientid;
        if(data[i].TegirmonClientid != null){
          info.client_name = data[i].royxati.client_name;
        }
        if( data[i].note != null && data[i].note != ''){
            info.zaxira_qty = parseFloat(data[i].note);
        }
        if(data[i].reverced_note_id != null && data[i].reverced_note_id != ''){
          info.zaxira_id = data[i].reverced_note_id;
        }
        if(data[i].name != null && data[i].name != ''){
          info.details_name = data[i].name;
          info.details_array = data[i].name.split(",");
        }

        info.product_id = data[i].royxati.TegirmonProductid;
        info.product_name = data[i].royxati.product.name;
        info.product_price = data[i].royxati.product.price;
        info.qty = data[i].royxati.qty;
        info.real_qty = data[i].royxati.real_qty;
        info.note = data[i].royxati.note;
        if(this.group_data.name){
          info.kassir = this.group_data.name;
        }
        else{
          info.kassir = localStorage.user_name;
        }
        //
        let tempInvoiceId = data[i].TegirmonInvoiceid;
        try{
          const resp = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getAnyInvoiceFullInfoById?invoice_id=' + tempInvoiceId);
          const data = await resp.json();
          if(resp.status == 201 || resp.status == 200){
            info.summ = data.summ;
            
            if(data.image_str_url == null || data.image_str_url == ''){
              info.skidka = 0;
              info.check_qty += 0;
            }
            else{
              info.skidka = data.image_str_url;
              info.check_qty += parseFloat(data.image_str_url);
            }
            
            info.changeProduct = [];
            if(data.note == null || data.note == ''){
              info.check_qty += 0;
            }
            else{
              info.check_qty += parseFloat(data.note);
            }
            for(let j=0; j<data.item_list.length; j++){
              let rpoTP = {
                persantage: (data.item_list[j].qty/data.item_list[j].real_sum).toFixed(1),
                product_id: data.item_list[j].tegirmonProductid,
                product_name: data.item_list[j].product.name,
                qty: data.item_list[j].qty,
                real_qty: data.item_list[j].real_qty,
                summ: data.item_list[j].sum,
                color: data.item_list[j].product.shitrix_code,
                id: data.item_list[j].id
              }
              info.check_qty += parseFloat((data.item_list[j].qty/data.item_list[j].real_sum).toFixed(1))
              info.changeProduct.push(rpoTP);
            }
          }
        }
        catch{
          this.modal_info = this.$i18n.t('network_ne_connect');
          this.modal_status = true;
        }
        //

        this.invoice_list.push(info)
        this.getAllCheck(this.invoice_list)
      }
      this.loading = false;
      this.loading_table = true;
      await this.fetch_get_zaxira_invoice();
      this.loading_table = false;
    },

    async fetch_get_zaxira_invoice(){
      console.log('this.invoice_list')
      console.log(this.invoice_list)
      for(let i=0; i<this.invoice_list.length; i++){
        for(let j=0; j<this.invoice_list[i].details_array.length-1; j++){
          try{
            const resp = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getAnyInvoiceFullInfoById?invoice_id=' + parseInt(this.invoice_list[i].details_array[j]));
            const data = await resp.json();
            if(resp.status == 201 || resp.status == 200){
              this.invoice_list[i].zaxira_invoice.push(data);
            }
          }
          catch(error){
            console.log(error)
          }
        }
      }
      console.log('this.invoice_list')
      console.log(this.invoice_list)
    },

    selectOptionUser(option) {
      this.invoice_list[option.row].client_name = option.data.fio;
      this.invoice_list[option.row].client_id = option.data.id;
      this.invoice_list[option.row].client_new_name = option.data.fio;
    },
    selectOptionProduct(option){
      this.invoice_list[option.row].product_name = option.data.name;
      this.invoice_list[option.row].product_id = option.data.id;
      this.invoice_list[option.row].product_price = option.data.price;
    },
    changingAdd(data,i){
      this.change_pro = true;
      this.rowIndex = i;
      this.itemForChange = data;
      this.$refs.changer.updateChanger(data);
      this.itemForChange.qty = this.itemForChange.qty.toString();
    },

    async GetChangeProduct(option){
      this.change_unit_qty = 0;
      this.change_pro = false;
      this.invoice_list[option.index].changeProduct = option.data;
      let summ_all = 0;
      let item_list_change = [];
      let skidkaItem = 0;
      for(let i = 0; i<option.data.length; i++){
        if(parseFloat(option.data[i].skidka) > 0){
          skidkaItem += parseFloat(option.data[i].skidka);
        }
        if(parseFloat(option.data[i].real_qty) > 0 && parseFloat(option.data[i].summ) == 0){
          let item = {
            tegirmonProductid: option.data[i].product_id,
            qty: option.data[i].real_qty,
            real_qty: option.data[i].real_qty,
            sum: option.data[i].summ,
            real_sum: option.data[i].persantage,
            id: option.data[i].id,
            auth_user_creator_id: localStorage.AuthId,
          }
          item_list_change.push(item);
        }
        else if(parseFloat(option.data[i].summ)>0){
          this.change_unit_qty = option.data[i].real_qty;
        }
        summ_all += parseFloat(option.data[i].summ)
      }
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "qty_real": this.invoice_list[this.rowIndex].qty,
          "tegirmonProductid": this.invoice_list[this.rowIndex].product_id,
          "id": 0,
          "item_list": item_list_change,
          "summ": summ_all,
          "note": this.change_unit_qty,
          "tegirmonAuthid": localStorage.AuthId,
          "user_name": localStorage.user_name,
          "image_str_url": skidkaItem
        })
      };
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/addChangeBugdoyProductsWithoutRegistartion", requestOptions);
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        { 
          await this.fetchDeleteIncoice(this.invoice_list[this.rowIndex].invoice_id)
          const dataInvoice = await response.json()
          this.invoice_list[this.rowIndex].invoice_id = dataInvoice.id;
        
          // this.$refs.message.success('Added_successfully')
          // return true;
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
      if(this.invoice_list[this.rowIndex].group_detail_id > 0){
        const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "tegirmonTortilganBugdoyRoyxatiGroupid": this.id,
            "tegirmonTortilganBugdoyRoyxatiid": this.invoice_list[this.rowIndex].ruyxat_id,
            "tegirmonInvoiceid": this.invoice_list[this.rowIndex].invoice_id,
            "id" : 0,
            "note": this.invoice_list[this.rowIndex].zaxira_qty,
            "reverced_note_id": this.invoice_list[this.rowIndex].zaxira_id,
            "name": this.invoice_list[this.rowIndex].details_name,
            // "name": this.shafyor_name,
            
            // "auth_user_creator_id": localStorage.AuthId,
            // "auth_user_updator_id": localStorage.AuthId,
          })
        };
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonTortilganBugdoyRoyxatiGroupDetails", requestOptions);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const dataGR = await response.json()
            console.log('datagroupDetails')
            console.log(dataGR)
            this.invoice_list[this.rowIndex].group_detail_id = dataGR.id;
            this.$refs.message.success('Added_successfully')
            // return true;
          }
          else{
            const data = await response.text();
            this.modal_info = data;
            this.modal_status = true;
            // return false;  
          }
        }
        catch{
          this.loading = false;
          this.modal_info = this.$i18n.t('network_ne_connect');
          this.modal_status = true;
        }
      }
      await this.updateFull();
    },

    async updateFull(){
      if(this.id>0){
        const res = await fetch(this.$store.state.hostname + '/TegirmonTortilganBugdoyRoyxatiGroupDetails/getPaginationByGroupId?page=0&size=100&group_id=' + this.id);
        const data = await res.json();
        this.fetchInvoice(data.items_list);
      }
    },

    async fetchDeleteIncoice(id){
      const requestOptions = {
        method : "delete",
      };
      const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/" + id, requestOptions);
      const data = await response.text();
      if(response.status == 201 || response.status == 200)
      {
        // this.$refs.message.success('Successfully_removed')
      }
      else{
        this.modal_info = data;
        this.modal_status = true;
      }
    },
    async sendAll(){
      // this.getAllCheck(this.invoice_list)
      this.checkShow = true;
    },
    closeAllGroup(){
      this.checkShow = false;
      // this.fetchAcceptGroup()
    },

    async fetchAcceptGroup(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/acceptTegirmonTortilganBugdoyRoyxatiGroupFromInvoice?id=" + this.id);
        const data = await response.text();
        if(response.status == 201 || response.status == 200)
        {
          this.$refs.message.success('Added_successfully')
          this.$router.push('/today_groups')
        }
        else{
          this.modal_info = data;
          this.modal_status = true;
        }
      }
      catch{
        this.modal_info = data;
        this.modal_status = true;
      }
      
    },
  }
}
</script>

<style lang="scss" scoped>
.myTablegroupList {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; overflow-x:auto
}
.mainShowGroup{
  background: #f7f7f7 !important;
}
.myTablegroupList th{
  font-weight: 500;
  font-size:10px;
}
.myTablegroupList td{
  font-size:11.5px;
  
}
.myTablegroupList td {
  text-align: left;
  padding: 5px 10px;
}
.myTablegroupList th{
  text-align: left;
  padding: 4px 10px;
}

.myTablegroupList tr {
  border-bottom: 0.1px solid rgb(224, 224, 224);
  &:hover{
    background: #afd1fd;
  }
}

.myTablegroupList tr.header, .myTablegroupList tr:hover {
  // background-color: #f1f1f1;
}
// ikkichi pastdagi tabletsa

.myTableDebitorListItemDownAll {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; overflow-x:auto
}
.myTableDebitorListItemDownAll th{
  font-weight: 600;
  font-size:11px;
}
.myTableDebitorListItemDownAll td{
  font-size:11.5px;
  
}
.myTableDebitorListItemDownAll td {
  text-align: left;
  padding: 7px 10px;
}
.myTableDebitorListItemDownAll th{
  text-align: left;
  padding: 8px 10px;
}

.myTableDebitorListItemDownAll tr {
  border-bottom: 1px dashed rgb(240, 240, 240);
  &:hover{
    background: #affddb;
  }
}

.myTableDebitorListItemDownAll tr.header, .myTableDebitorListItemDownAll tr:hover {
  // background-color: #f1f1f1;
}


.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
.editIcon{
  color:#01b348;
  font-size: 13px;
}
.header_shafyor{
  background: #ffffff;
  border-bottom: 1px solid rgb(78, 54, 174);
  margin-bottom: 15px;
}
.for_table{
  height: 250px;
  overflow: hidden;
  overflow-y: scroll;
}
.bg_success{
  background: #a5ffc9;
}
.appleid{
  pointer-events: none;
}
.bg_danger{
  background: #ff7b7b !important;
}
.backColor{
  box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
  background: white;
}
.header_group{
  background-image: radial-gradient( circle farthest-corner at 1.3% 2.8%,  rgba(239,249,249,1) 0%, rgba(182,199,226,1) 100.2% );
}
</style>
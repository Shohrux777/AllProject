<template>
  <div class="add_tarozi_container">
    <div class="add_tarozi_header border-bottom pt-2 px-3" v-show="client_name">
      <div class="row">
        <!-- <div class="col-3">
          <mdb-input class="m-0 p-0 w-100" v-model="search" 
            :placeholder="$t('search_client')" size="sm"
            outline  group type="text" validate error="wrong" success="right"/>
        </div> -->
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
          <mdb-btn v-if="false" class="m-0 py-2 px-4 mr-2" @click="add_invoice_item"
              style="font-size: 8px;" color="primary"
              size="sm"><mdb-icon fas class="mr-1"  icon="plus"></mdb-icon> 
          </mdb-btn>
          <mdb-btn class="m-0 py-2 px-4" @click="add_client_func" v-show="false"
              style="font-size: 8px;" color="success"
              size="sm"><mdb-icon fas class="mr-1"  icon="plus"></mdb-icon> {{$t('add_client')}}
          </mdb-btn>
        </div>
      </div>
    </div>
    <loader v-if="loading" />
    <div  v-else class="add_tarozi_main px-3">
      <div class="add_tarozi_item rounded shadow mt-3 px-2 pt-3 pb-2" v-for="(item, index) in product_list" :key="index">
        <div class="row">
          <div class="col-3">
            <mdb-input class="m-0 p-0 w-100 alert-warning" disabled v-model="item.shafyor_name" 
            :placeholder="$t('shafyor_name')" size="sm"
            outline  group type="text" validate error="wrong" success="right"/>
            <small style="position:absolute; top:-14px; left: 10px; font-size: 10px;" class="input_label_cl font-weight-bold px-2 py-0">{{$t('shafyor_name')}}</small>
          </div>
          <div class="col-3">
            <mdb-input class="m-0 p-0 w-100 alert-warning" disabled v-model="item.car_number"
            :placeholder="$t('car_number')" size="sm"
            outline  group type="text" validate error="wrong" success="right"/>
            <small style="position:absolute; top:-14px; left: 10px; font-size: 10px;" 
            class="input_label_cl font-weight-bold px-2 py-0">{{$t('car_number')}}</small>
          </div>
          <div class="col-3">
            <div style="position:relative;">
              <input  
                class="mt-0 mb-0 rounded input_style alert-warning" disabled 
                @input="changeInput(index)" v-model="item.shafyor_tel" :placeholder="$t('shafyor_tel')" outline/>
              <small style="position:absolute; top:-14px; left: -5px; font-size: 10px;" 
              class="input_label_cl font-weight-bold px-2 py-0">{{$t('shafyor_tel')}}</small>
              </div>
          </div>
          <div class="col-3">
            <mdb-input class="m-0 p-0 w-100 alert-warning" disabled v-model="item.note" 
            :placeholder="$t('note')" size="sm"
            outline  group type="text" validate error="wrong" success="right"/>
            <small style="position:absolute; top:-14px; left: 10px; font-size: 10px;" 
            class="input_label_cl font-weight-bold px-2 py-0">{{$t('note')}}</small>
          </div>
          <div class="col-3 mt-2">
            <erpSelectProduct
                :options="all_product_t.rows"
                @select="selectOptionPro"
                :selected="item.product_name"
                :label="$t('client')"
                :row="index"
                class="alert-warning"
              />
              <small style="position:absolute; top:-14px; left: 10px; font-size: 10px;" 
              class="input_label_cl font-weight-bold px-2 py-0">{{$t('product')}}</small>
              <!-- <small
                v-if="$v.product_name.$dirty && product_id == null"
                class="invalid-text  ml-2"
                style="margin-top: -8px;"
              >
                {{ $t("select_item") }}
              </small> -->
          </div>
          <div class="col-2 mt-2">
            <mdb-input class="m-0 p-0 w-100 alert-warning" disabled v-model="item.all_measure" 
            :placeholder="$t('all_measure')" size="sm"
            outline  group type="number" validate error="wrong" success="right"/>
            <small style="position:absolute; top:-14px; left: 10px; font-size: 10px;" 
            class="input_label_cl font-weight-bold px-2 py-0">{{$t('all_measure')}}</small>
          </div>
          <div class="col-2 mt-2">
            <mdb-input class="m-0 p-0 w-100" v-model="item.car_measure" 
            @blur="blur_nol_car(index)" 
            @click="func_nol_car(index)"
            @input="change_car_measure(index)"
            :placeholder="$t('car_measure')" size="sm"
            outline  group type="text" validate error="wrong" success="right"/>
            <small style="position:absolute; top:-14px; left: 10px; font-size: 10px;" 
            class="input_label_cl font-weight-bold px-2 py-0">{{$t('car_measure')}}</small>
          </div>
          <div class="col-2 mt-2">
            <mdb-input class="m-0 p-0 w-100" v-model="item.discount" 
            @blur="blur_nol_discount(index)"  
            @click="func_nol_discount(index)"
            @input="change_car_measure(index)"
            :placeholder="$t('discount')" size="sm"
            outline  group type="text" validate error="wrong" success="right"/>
            <small style="position:absolute; top:-14px; left: 10px; font-size: 10px;" 
            class="input_label_cl font-weight-bold px-2 py-0">{{$t('discount')}}</small>
          </div>
          <div class="col-3 mt-2">
            <mdb-input class="m-0 p-0 w-100 alert-warning" disabled v-model="item.product_measure"
            :placeholder="$t('product_measure')" size="sm"
            outline  group type="number" validate error="wrong" success="right"/>
            <small style="position:absolute; top:-14px; left: 10px; font-size: 10px;" 
            class="input_label_cl font-weight-bold px-2 py-0">{{$t('product_measure')}}</small>
          </div>
          
          <div class="col-3 mt-2">
            <mdb-input class="m-0 p-0 w-100" v-model="item.product_price"  
            @blur="blur_nol_product_price(index)" 
            @click="func_nol_product_price(index)"
            @input="change_car_measure(index)"
            :placeholder="$t('product_price')" size="sm"
            outline  group type="number" validate error="wrong" success="right"/>
            <small style="position:absolute; top:-14px; left: 10px; font-size: 10px;" 
            class="input_label_cl font-weight-bold px-2 py-0">{{$t('product_price')}}</small>
          </div>
          <div class="col-3 mt-2">
            <mdb-input class="m-0 p-0 w-100" v-model="item.summ" 
            :placeholder="$t('summ')" size="sm"
            outline  group type="number" validate error="wrong" success="right"/>
            <small style="position:absolute; top:-14px; left: 10px; font-size: 10px;" 
            class="input_label_cl font-weight-bold px-2 py-0">{{$t('summ')}}</small>
          </div>
          <div class="col-3 mt-2">
            <mdb-input class="m-0 p-0 w-100" disabled v-model="item.ostatik_measure" 
            :placeholder="$t('ostatik_measure')" size="sm"
            outline  group type="number" validate error="wrong" success="right"/>
            <small style="position:absolute; top:-14px; left: 10px; font-size: 10px;" 
            class="input_label_cl font-weight-bold px-2 py-0">{{$t('ostatik_measure')}}</small>
          </div>
          
          
          <div class="col-3 mt-1">
            <mdb-btn class="m-0 py-1 px-3" @click="getPhoto(index)"
                style="font-size: 12px;" color="primary"
                size="sm"><mdb-icon fas  icon="camera-retro"></mdb-icon>
            </mdb-btn>
            <mdb-btn v-if="product_list.length>1" class="m-0 py-1 px-3 ml-2" @click="delItem(index)"
                style="font-size: 12px;" color="danger"
                size="sm"><mdb-icon fas  icon="trash"></mdb-icon>
            </mdb-btn>
          </div>
        </div>

        <!-- Sotilgan mahsulot -->
        <div class="text-center" v-if="item.payed_summ>0">
          <span style="font-size: 12px;">Sotilgan mahsulot</span>
        </div>
        <div class="" v-if="item.payed_summ>0">
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
                <td> <span >{{(item.payed_measure)}}</span> <span>кг</span></td>
                <td> <span >{{item.payed_summ}}</span> <span>сум</span></td>
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
                  <td> <span >{{row.product.name}}</span> </td>
                  <td> <span >{{row.real_qty}}</span>  <span>кг</span></td>
                  <td> <span >{{row.sum}}</span> </td>
                  <td> <span >{{(row.real_qty / row.real_sum).toFixed(1)}}</span>  <span>кг</span></td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
        
        <!-- Almashtirilgan mahsulot -->

        <!-- Kassaga yuborilgan -->
        
        <div class="text-center" v-if="item.send_group_id>0">
          <span style="font-size: 12px;">Kassaga yuborilgan</span>
        </div>
        <div class="row mx-0" v-if="item.send_group_id>0">
          <loader-table v-if="loading_table"/>
          <div v-else class="col-12 p-0">
            <table class="myTablegroupList ">
              <thead>
                <tr class="header py-3" style="background: #ceffd3;">
                  <th  width="40" class="text-left">№</th>
                  <th>{{$t('product')}}</th>
                  <th>{{$t('measure')}}</th>
                  <th>{{$t('user')}}</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td> <span >{{1}}</span> </td>
                  <td> <span>{{item.product_name}}</span> </td>
                  <td> <span >{{item.group_qty}}</span>  <span>кг</span></td>
                  <td> <span >{{item.auth_second_pull_name}}</span></td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>

        <!-- Kassaga yuborilgan -->

        <div v-if="item.car_measure && !client_name" 
        class="d-flex justify-content-end align-items-center border-top pt-2 pb-2 mb-1 border-bottom">
          <mdb-btn  class="m-0 py-1 px-3 ml-2" v-if="item.product_measure >= item.ostatik_measure - 1 && item.product_measure <= item.ostatik_measure + 1" @click="pay_money(index)"
            style="font-size: 10px;"
            size="sm">Общие платить денги
          </mdb-btn>
          <mdb-btn  class="m-0 py-1 px-3 ml-2" @click="change_product(index)"
            style="font-size: 10px;"
            size="sm">{{ $t('change_product') }}
          </mdb-btn>
          <mdb-btn  class="m-0 py-1 px-3 ml-2" v-if="item.ostatik_measure>1" @click="ostatka_money(index)"
            style="font-size: 10px;"
            size="sm">Отправить касса
          </mdb-btn>
        </div>
        <div class="photo mt-0 d-flex justify-content-start">
          <img :src="hostname + item.image_first_pull" width="120" alt=""  v-show="item.image_first_pull" class="shadow border rounded">
          <img :src="hostname + item.image_second_pull" width="120" alt="" v-show="item.image_second_pull" class="shadow border rounded ml-2">
        </div>
      </div>
    </div>
    <div class="add_container text-right border-top mt-3 mx-3 pt-1">
      <mdb-btn class="m-0 py-2 px-4" @click="save_items"
          style="font-size: 8px;" color="success"
          size="sm"><mdb-icon fas class="mr-1"  icon="check"></mdb-icon> {{$t('save')}}
        </mdb-btn>
    </div>
    <modal-train  :show="add_client_show" headerbackColor="white"  titlecolor="black" :title="$t('show_davernis')" 
      @close="add_client_show = false" width="85%">
        <template v-slot:body>
          <addClientNew @close="add_client_show = false"  @close_data="close_clientAdd" ref="addClientrefs">
          </addClientNew>
        </template>
    </modal-train>
    <modal-train  :show="change_pro" headerbackColor="white"  titlecolor="black" :title="$t('change_product')" 
      @close="change_pro = false" width="80%">
        <template v-slot:body>
          <changeProduct @close="change_pro = false" ref="changer_refs" 
            @addNewData="GetChangeProduct" 
            :row="rowIndex" 
            :product_name="itemForChange.product_name"
            :product_qty="itemForChange.product_measure.toString()" 
            :product_id="itemForChange.product_id" 
            :product_price="itemForChange.product_price"
            :skidka_status="skidka_status">
          </changeProduct>
        </template>
    </modal-train>
    <mdb-modal :show="confirm" @close="confirm = false" size="sm" class="text-center" danger>
      <mdb-modal-header center :close="false">
        <p class="heading">{{$t('Are_you_sure')}}</p>
      </mdb-modal-header>
      <mdb-modal-body>
        <mdb-icon icon="check"  size="4x" class="animated rotateIn text-success d-block"/>
        <small>{{product_list[rowIndex].ostatik_measure}} kg pulga sotib olayabsiz</small>
      </mdb-modal-body>
      <mdb-modal-footer center>
        <mdb-btn outline="danger" @click="promise">{{$t('Yes')}}</mdb-btn>
        <mdb-btn color="danger" @click="confirm = false">{{$t('No')}}</mdb-btn>
      </mdb-modal-footer>
    </mdb-modal>
    <webcam  v-show="showPhoto" @getPhotosub="takePhoto" ref="camera_component"/>
  </div>
</template>

<script>
import {mdbInput, mdbBtn, mdbIcon, mdbModal,mdbModalHeader, mdbModalBody, mdbModalFooter } from 'mdbvue'
import {mapActions,mapGetters} from 'vuex'
import webcam from '../webcam/webcam_Add.vue'
import addClientNew from './new_client_add.vue'
import erpSelect from "../../components/erpSelectFioSearch.vue";
import erpSelectProduct from "../../components/erpSelectProduct.vue";
import InputSearch from '../../components/inputSearch';
import inputSearchYear from '../../components/inputSearchYear';
import changeProduct from '../mainPage/changeComponent.vue'
export default {
  components: {
    mdbInput, mdbBtn, mdbIcon, mdbModal,mdbModalHeader, mdbModalBody, mdbModalFooter,
    erpSelectProduct,
    addClientNew,
    erpSelect,
    InputSearch,
    inputSearchYear,
    webcam,
    changeProduct
  },
  data() {
    return {
      hostname: this.$store.state.server_ip,
      search: '',
      loading: false,
      loading_table: false,
      showPhoto: false,
      add_client_show: false,
      change_pro: false,
      confirm: false,
      rowIndex: 0,
      skidka_status: true,
      itemForChange: {
        product_measure: 0,
      },
      product_list: [
        {
          shafyor_name: '',
          car_number: '',
          shafyor_tel: '',
          note: '',
          product_name: '',
          product_id: null,
          product_price: null,
          all_measure: null,
          car_measure: 0,
          product_measure: 0,
          ostatik_measure: 0,
          discount: 0,
          payed_measure: 0,
          payed_summ: 0,
          summ: 0,
          image_first_pull: '',
          image_second_pull: '',
          date_first_pull: null,
          group_qty: 0,
          send_group_id: 0,
          pull_status: true,
          auth_first_pull_id: localStorage.AuthId,
          auth_first_pull_name: localStorage.user_name,
          changeProduct: [],
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
    await this.fetch_product_t();
    await this.fetchClientLast();
  },
  computed: mapGetters(['all_product_t', 'allClient']),
  methods: {
    ...mapActions(['fetch_product_t', 'fetchClient']),
    async refresh(){
      try{
        this.loading_table = true;
        const res = await fetch(this.$store.state.hostname + '/TegirmonInvoiceNews/getAnyInvoiceNewFullInfoById?invoice_id=' + this.id);
        const data = await res.json();
        this.loading_table = false;
        if(res.status == 200 || res.status == 201){
          console.log('this is by id created')
          console.log(data)
          this.product_list = [];
          let item = {
            id:data.id,
            shafyor_name: data.shafyor_name,
            car_number: data.car_number,
            shafyor_tel: data.shafyor_tel,
            note: data.note,
            product_name: data.product.name,
            product_id: data.tegirmonProductid,
            product_price: data.product_price,
            all_measure: data.all_measure,
            car_measure: data.car_measure,
            product_measure: data.product_measure,
            discount: data.discount,
            payed_measure: data.dolg_summ,
            payed_summ: data.payed_summ,
            summ: data.summ,
            image_first_pull: data.image_first_pull,
            image_second_pull: data.image_second_pull,
            date_first_pull: data.date_first_pull,
            pull_status: data.pull_status,
            rashshod_status: data.rashshod_status,
            finish_status: data.finish_status,
            auth_first_pull_id: data.auth_first_pull_id,
            auth_first_pull_name: data.auth_first_pull_name,
            auth_second_pull_id: data.auth_second_pull_id,
            auth_second_pull_name: data.auth_second_pull_name,
            ostatik_measure: data.credit_sum,
            group_qty: data.qty_real,
            send_group_id: data.send_group_id,
            changeProduct: data.item_list
        }
        console.log('data.TegirmonClientNewid')
        console.log(data.tegirmonClientNewid)
        if(data.tegirmonClientNewid){
          console.log('hiy measure')
          console.log(this.client_id)
          this.client_id = data.tegirmonClientNewid;
          this.client_name = data.text_2;
          this.passport_number = data.client.passport_number;
          this.born_date = data.client.addiotionala_information.slice(8,10) + '-' + data.client.addiotionala_information.slice(5,7) + '-' + data.client.addiotionala_information.slice(0,4);
        }
        this.product_list.unshift(item);
      }
      }
      catch(error){
        console.log(error);
      }
      await this.change_car_measure(0);
    },
    async ostatka_money(i){
      console.log(i);
      this.product_list[i].group_qty = this.product_list[i].ostatik_measure;
      this.rowIndex = i;
      await this.send_group_casher(i);
      await this.create_ruyxati_casher(i);
      // await this.save_item_index_changed(i, this.product_list[i].changeProduct);
    },
    async send_group_casher(index){
      const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
              "shafyor_name": this.product_list[index].shafyor_name,
              "car_number": this.product_list[index].car_number,
              "note": this.product_list[index].note,
              "name": '',
              'qabul_qilgan_user_name': localStorage.user_name,
              "id": 0
            })
          };
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/TegirmonTortilganBugdoyRoyxatiGroups", requestOptions);
            this.loading = false;
            if(response.status == 201 || response.status == 200)
            {
              const group_data = await response.json();
              console.log('group_data')
              console.log(group_data)
              this.product_list[index].send_group_id = group_data.id;
              this.$refs.message.success('Added_successfully');
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
    async create_ruyxati_casher(index){
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "tegirmonProductid": this.product_list[index].product_id,
          "qty": this.product_list[index].ostatik_measure,
          "real_qty": this.product_list[index].ostatik_measure,
          "note": this.product_list[index].note,
          "info": this.product_list[index].note,
          "client_name": this.product_list[index].shafyor_name,
          "id": 0
        })
      };
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonTortilganBugdoyRoyxatis", requestOptions);
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const dataJosn = await response.json()
          this.ruyxat_id = dataJosn.id;
          await this.GetChangeProductEmpty();
          // this.$refs.message.success('Added_successfully')
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
    async GetChangeProductEmpty(){
      let summ_all = 0;
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "qty_real": this.product_list[this.rowIndex].qty,
          "tegirmonProductid": this.product_list[this.rowIndex].product_id,
          "id": 0,
          "summ": summ_all,
          "tegirmonAuthid": localStorage.AuthId,
          // "item_list": item_list_change,
          // "auth_user_creator_id": localStorage.AuthId,
          // "auth_user_updator_id": localStorage.AuthId,
          
        })
      };
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/addChangeBugdoyProductsWithoutRegistartion", requestOptions);
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const dataInvoice = await response.json()
          this.invoice_id = dataInvoice.id;
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


      if(this.product_list[this.rowIndex].send_group_id > 0){
        const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "tegirmonTortilganBugdoyRoyxatiGroupid": this.product_list[this.rowIndex].send_group_id,
            "tegirmonTortilganBugdoyRoyxatiid": this.ruyxat_id,
            "tegirmonInvoiceid": this.invoice_id,
            "id":0,
            "name": '',
            "note": '',
            "reverced_note_id": 0,
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
            console.log(dataGR)
            console.log('dataGR')
            console.log(this.product_list[this.rowIndex].send_group_id)
            console.log(this.ruyxat_id)
            console.log(this.invoice_id)
            await this.save_item_index_changed(this.rowIndex, this.product_list[this.rowIndex].changeProduct);
            this.$refs.message.success('Added_')
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

    },
    async add_client_func(){
      this.add_client_show = !this.add_client_show;
      this.$refs.addClientrefs.MountedFunc();
    },
    async pay_money(i){
      console.log(i);
      this.confirm = true;
      this.rowIndex = i;
    },
    async promise(){
      console.log('da ni bosdi')
      this.product_list[this.rowIndex].payed_measure = this.product_list[this.rowIndex].ostatik_measure;
      this.product_list[this.rowIndex].payed_summ = this.product_list[this.rowIndex].summ;
      this.product_list[this.rowIndex].ostatik_measure = 0;
      let items_list = [];
      await this.save_item_index_changed(this.rowIndex, items_list);
      this.confirm = false;
    },
    async change_product(i){
      console.log(i)
      this.change_pro = true;
      this.rowIndex = i;
      this.itemForChange = this.product_list[i];
      console.log(this.itemForChange)
      this.$refs.changer_refs.updateChanger(
        {
          zaxira_qty: parseFloat(this.product_list[i].group_qty),
          product_id: this.product_list[i].product_id,
          qty: this.product_list[i].product_measure,
          skidka: 0,
        }
      );
    },
    async GetChangeProduct(option){
      console.log(option)
      let change_unit_qty = 0;
      this.change_pro = false;
      // this.invoice_list[option.index].changeProduct = option.data;
      let summ_all = 0;
      let item_list_change = [];
      for(let i = 0; i<option.data.length; i++){
        if(parseFloat(option.data[i].real_qty) > 0 && parseFloat(option.data[i].summ) == 0){
          let item = {
            tegirmonProductid: option.data[i].product_id,
            qty: option.data[i].real_qty,
            real_qty: option.data[i].real_qty,
            sum: option.data[i].summ,
            real_sum: option.data[i].persantage,
            id: option.data[i].id,
          }
          item_list_change.push(item);
        }
        else if(parseFloat(option.data[i].summ)>0){
          change_unit_qty = option.data[i].real_qty;
        }
        summ_all += parseFloat(option.data[i].summ);
      }
      if(this.product_list[option.index].payed_summ == '' || this.product_list[option.index].payed_summ == null){
        this.product_list[option.index].payed_summ = 0;
      }
      this.product_list[option.index].payed_summ = parseFloat(summ_all);
      this.product_list[option.index].payed_measure = parseFloat(change_unit_qty);

      for(let j=0; j<this.product_list[option.index].changeProduct.length; j++){
        if(this.product_list[option.index].changeProduct[j].id>0){
          await this.deleteInvoieItemNews(this.product_list[option.index].changeProduct[j].id);
        }
      }
      await this.save_item_index_changed(option.index, item_list_change);
      this.change_pro = false;
      

    },
    async add_invoice_item(){
      let item = {
          id:0,
          shafyor_name: '',
          car_number: '',
          shafyor_tel: '',
          note: '',
          product_name: '',
          product_id: null,
          product_price: null,
          all_measure: null,
          car_measure: 0,
          product_measure: 0,
          discount: 0,
          payed_measure: 0,
          payed_summ: 0,
          summ: 0,
          group_qty: 0,
          send_group_id: 0,
          image_first_pull: '',
          image_second_pull: '',
          date_first_pull: null,
          pull_status: true,
          auth_first_pull_id: localStorage.AuthId,
          auth_first_pull_name: localStorage.user_name,
          changeProduct: [],
      }
      this.product_list.unshift(item);
      for(let i=0; i<this.all_product_t.rows.length; i++){
        if(this.all_product_t.rows[i].inv_accepted_status){
          this.product_list[0].product_name = this.all_product_t.rows[i].name;
          this.product_list[0].product_id = this.all_product_t.rows[i].id;
          this.product_list[0].product_price = this.all_product_t.rows[i].price;
        }
      }
    },
    async delItem(i){
      this.product_list.splice(i,1);
    },
    async save_items(){
      this.loading = true;
      for(let i=0; i<this.product_list.length; i++){
        await this.save_item_index_changed(i, this.product_list[i].changeProduct);
      }
      this.loading = false;
      this.$router.push('/tarozi_list');
    },
    async save_item_index_changed(index, item_list){
      console.log('this.product_list[index].group_qty')
      console.log(this.product_list[index].group_qty);
      let ostatka_kg = 0;
      ostatka_kg = parseFloat(this.product_list[index].product_measure) - parseFloat(this.product_list[index].payed_measure) - parseFloat(this.product_list[index].group_qty);
      for(let i=0; i<item_list.length; i++){
        ostatka_kg -= parseFloat(item_list[i].real_qty/item_list[i].real_sum);
      }
      console.log('ostatka_kg')
      console.log(ostatka_kg)
      let finish_status = false
      let rashshod_status = false
      if(parseFloat(this.product_list[index].car_measure)>1){
        rashshod_status = true;
      }
      if(ostatka_kg<1){
        finish_status = true;
      }
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "shafyor_name" : this.product_list[index].shafyor_name,
          "car_number" : this.product_list[index].car_number,
          "shafyor_tel" : this.product_list[index].shafyor_tel,
          "note": this.product_list[index].note,
          "tegirmonProductid": this.product_list[index].product_id,
          "product_price": this.product_list[index].product_price,
          "text_1" : this.product_list[index].product_name,
          "image_first_pull": this.product_list[index].image_first_pull,
          "image_second_pull": this.product_list[index].image_second_pull,
          "all_measure": this.product_list[index].all_measure,
          "discount":  this.product_list[index].discount,
          "car_measure":  this.product_list[index].car_measure,
          "product_measure":  this.product_list[index].product_measure,
          "credit_sum": ostatka_kg.toFixed(1),
          "dolg_summ": this.product_list[index].payed_measure,
          "payed_summ": this.product_list[index].payed_summ,
          "tegirmonClientNewid": this.client_id,
          "summ": this.product_list[index].summ,
          "text_2": this.client_name,
          "id" : this.product_list[index].id,
          "pull_status": this.product_list[index].pull_status,
          "rashshod_status": rashshod_status,
          "finish_status":finish_status,
          "auth_first_pull_id": this.product_list[index].auth_first_pull_id,
          "auth_first_pull_name": this.product_list[index].auth_first_pull_name,
          "auth_second_pull_id": localStorage.AuthId,
          "auth_second_pull_name": localStorage.user_name,
          "tegirmonAuthid": localStorage.AuthId,
          "qty_real": this.product_list[index].group_qty, 
          "send_group_id": this.product_list[index].send_group_id,
          "date_first_pull": this.product_list[index].date_first_pull,
          "item_list": item_list
        })
      };
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonInvoiceNews", requestOptions);
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const tryjson = await response.json();
          console.log(tryjson)
          this.id = tryjson.id;
          // this.$router.push('/add_last_tarozi/' + tryjson.id);
          await this.refresh()
          this.$refs.message.success('Added_successfully')
        }
        else{
          const data = await response.text();
          this.modal_info = data;
          this.modal_status = true;
        }
      }
      catch{
        this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
      }
    },
    async deleteInvoieItemNews(id){
      const requestOptions = {
        method : "delete",
      };
      const response = await fetch(this.$store.state.hostname + "/TegirmonInvoiceItemNews/" + id, requestOptions);
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
    changeInput(index){
      var x = this.product_list[index].shafyor_tel.replace(/\D/g, '').match(/(\d{0,2})(\d{0,3})(\d{0,4})/);
      this.product_list[index].shafyor_tel = !x[2] ? x[1] : '(' + x[1] + ') ' + x[2] + (x[3] ? '-' + x[3] : '');
    },
    selectOptionPro(option){
      console.log(option)
      this.product_list[option.index].product_name = option.data.name;
      this.product_list[option.index].product_id = option.data.id;
      this.product_list[option.index].product_price = option.data.price;
    },
    getPhoto(index_photo){
      this.showPhoto = true; 
      this.$refs.camera_component.createCameraElement()
      this.photo_index = index_photo;
    },
    takePhoto(data){
      this.product_list[this.photo_index].image_second_pull = data;
      this.showPhoto = false;
      // this.product_list.push({car_number: 9999});
      // this.product_list.splice(this.product_list.length-1, 1)
    },

    async selectOptionUser(option){
      this.client_info = option;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.passport_number = option.passport_number
      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
    },
    async selectClientPassport(option){
      this.client_info = option;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.passport_number = option.passport_number
      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
    },
    async selectClientBorn(option){
      this.client_info = option;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.passport_number = option.passport_number
      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
    },
    close_clientAdd(option){
      this.add_client_show = false;
      console.log('option')
      console.log(option)
      this.client_info = option;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.passport_number = option.passport_number
      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
    },
     
    // inputlarni 0 bulsa pustoy qilish
    async func_nol_car(i){
      this.change_car_measure(i);
      if(this.product_list[i].car_measure == 0){
        this.product_list[i].car_measure = null;
      }
    },
    async blur_nol_car(i){
      if(this.product_list[i].car_measure == null || this.product_list[i].car_measure == ''){
        this.product_list[i].car_measure = 0;
      }
      this.change_car_measure(i);
    },
    async func_nol_discount(i){
      this.change_car_measure(i);
      if(this.product_list[i].discount == 0){
        this.product_list[i].discount = null;
      }
    },
    async blur_nol_discount(i){
      if(this.product_list[i].discount == null || this.product_list[i].discount == ''){
        this.product_list[i].discount = 0;
      }
      this.change_car_measure(i);
    },
    async func_nol_product_price(i){
      this.change_car_measure(i);
      if(this.product_list[i].product_price == 0){
        this.product_list[i].product_price = null;
      }
    },
    async blur_nol_product_price(i){
      if(this.product_list[i].product_price == null || this.product_list[i].product_price == ''){
        this.product_list[i].product_price = 0;
      }
      this.change_car_measure(i);
    },
    async change_car_measure(i){
      console.log(this.product_list[i].car_measure);
      this.product_list[i].product_measure = parseFloat(this.product_list[i].all_measure) - parseFloat(this.product_list[i].car_measure) - parseFloat(this.product_list[i].discount)
      this.product_list[i].ostatik_measure = parseFloat(this.product_list[i].product_measure) - parseFloat(this.product_list[i].payed_measure) - parseFloat(this.product_list[i].group_qty);
      for(let j=0; j<this.product_list[i].changeProduct.length; j++){
        this.product_list[i].ostatik_measure -= parseFloat(this.product_list[i].changeProduct[j].real_qty / this.product_list[i].changeProduct[j].real_sum).toFixed(0);
      }
      this.product_list[i].summ = parseFloat(this.product_list[i].ostatik_measure) * parseFloat(this.product_list[i].product_price)
    }
  },
}
</script>

<style lang="scss" scoped>
 .input_style{
  width:100%; 
  font-size: 12px !important;  
  height:28px; 
  outline:none; 
  padding: 0 12px; 
  border: 1px solid #DADCE0
 }
 input:focus{
  border: 2px solid #4285F4;
 }
 .input_label_cl{
  color: #677672;
 }
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
</style>
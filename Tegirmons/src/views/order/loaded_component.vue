<template>
  <div class="container-fluid p-3">
    <loader v-if="loading" />
    <div v-else class="row">
      <div style="height: 100% !important;" :class="{'col-6': loaded_status, 'col-12': !loaded_status}">
        <div class="w-100 px-3 card">
          <div class="row">
            <div class="col-12 mt-3">
                <input class="m-0 form-control"
                style="height:32px; font-size: 13px;" v-model="shafyor_name" 
                type="text" validate error="wrong" success="right"/>
              <small
                style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
                class="bg-white px-2 py-0"
                >{{ $t("shafyor_name") }}</small
              >
              <small class="invalid-text mt-0" style="margin-left:5px; "  v-if="$v.shafyor_name.$dirty && !$v.shafyor_name.required" >
                {{$t('name_invalid_text')}}
              </small>
            </div>
            <div class="col-12 mt-3">
                <input class="m-0 form-control" v-mask="'##A###AA'" placeholder="01A123AA"
                style="height:32px; font-size: 13px;" v-model="car_nomer" 
                type="text" validate error="wrong" success="right"/>
              <small
                style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
                class="bg-white px-2 py-0"
                >{{ $t("car_number") }}</small
              >
              <small class="invalid-text mt-0" style="margin-left:5px; "  v-if="$v.car_nomer.$dirty && !$v.car_nomer.required" >
                {{$t('name_invalid_text')}}
              </small>
            </div>
            <div class="col-12 mt-3">
                <input class="m-0 form-control" v-mask="'(##) ###-##-##'" v-model="phone_number" placeholder="(##) ###-##-##"
                style="height:32px; font-size: 13px;"
                type="text" validate error="wrong" success="right"/>
              <small
                style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
                class="bg-white px-2 py-0"
                >{{ $t("phone_number") }}</small
              >
              <small class="invalid-text mt-0" style="margin-left:5px; "  v-if="$v.phone_number.$dirty && !$v.phone_number.required" >
                {{$t('name_invalid_text')}}
              </small>
            </div>
            <div class="col-12 mt-3 mb-3 text-right" v-if="!loaded_status">
                <mdb-btn   @click="addShafyorInfo()" color="success" m="r2" style="font-size: 9.5px"
                    p="r3 l3 t2 b2"> <mdb-icon fas class="mr-1"  icon="camera"></mdb-icon>  {{$t('save')}}
                  </mdb-btn>
            </div>
          </div>
          <div class="row mt-3">
            <div class="col-12">
              <form @submit.prevent="submit">
                <div class="d_table">
                  <mdb-tbl class="fixed-column">
                    <mdb-tbl-head class="m-0 p-0">
                      <tr>
                        <td></td>
                        <!-- product -->
                        <td style="width:300px; font-size: 13px">
                          {{ $t('product') }}
                        </td>
                        <td
                          style=" font-size: 13px"
                        >
                          {{ $t('qty') }}
                        </td>
                      </tr>
                    </mdb-tbl-head>
                    <mdb-tbl-body>
                      <tr
                        scope="row"
                        v-for="(row, rowIndex) in order_car_item"
                        :key="rowIndex"
              
                      >
                        <th>
                          <i
                            class="
                              fas
                              fa-trash
                              delIcon
                              mask
                              waves-effect
                              m-0
                              pl-2
                            "
                            @click="order_car_item.splice(rowIndex,1)"
                          ></i>
                        </th>

                        <th style="position: relative;">
                          <input
                            :value="row.product_name"
                            disabled
                            type="text"
                            class="form-control form-control-sm"
                          />
                        </th>

                        <th>
                          <input
                            v-model="row.qty_str"
                            type="text"
                            class="form-control form-control-sm"
                            @input="changeSumma($event.target.value,rowIndex)"
                            @click="button_qty(rowIndex)"
                            @blur="blur_qty(rowIndex)"
                          />
                          <small
                            class="invalid-text"
                            style="
                              margin: 0;
                              margin-top: 26px;
                              position: relative;
                            "
                            v-if="($v.order_car_item.$each[rowIndex].qty.$dirty &&
                                !$v.order_car_item.$each[rowIndex].qty.required)
                            "
                          >
                            {{ $t("minValue") }}
                          </small>
                        </th>
                      </tr>
                    </mdb-tbl-body>
                  </mdb-tbl>
                  
                </div>
                
                
                
                <div class="blue-gradient">
                  <hr class="mt-1 mb-0"/>
                </div>
                <mdb-row class="mt-1">
                  <mdb-col col="12">
                    <div class="mt-0 text-right">
                      <mdb-btn color="success"  type="submit" style="font-size: 9px"
                        p="r4 l4 t2 b2">
                        <mdb-icon  />{{$t('add')}}
                      </mdb-btn>
                    </div>
                  </mdb-col>
                </mdb-row>
                
              </form>
            </div>
          </div>
        </div>
      </div>
      <div v-if="loaded_status" class="col-6 active_car">
        <div class="pb-3">
          <div class="ortilgan_mashina mb-3 p-2" v-for="(item,i) in car_order_list" :key="i" 
          
          :class="{'alert-info': active_car_item == i}">
            <div class="row p-2" @click="select_car_order(item,i)" >
              <div class="col-4 mt-2 mb-1 px-2">
                  <input class="m-0 form-control"
                  style="height:28px; font-size: 13px;" disabled v-model="item.shafyor_name" 
                  type="text" validate error="wrong" success="right"/>
                <small
                  style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
                  class="bg-white px-2 py-0"
                  >{{ $t("shafyor_name") }}</small
                >
              </div>
              <div class="col-4 mt-2 mb-1 px-2">
                  <input class="m-0 form-control" disabled v-mask="'##A###AA'" placeholder="01A123AA"
                  style="height:28px; font-size: 13px;" v-model="item.car_nomer" 
                  type="text" validate error="wrong" success="right"/>
                <small
                  style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
                  class="bg-white px-2 py-0"
                  >{{ $t("car_number") }}</small
                >
              </div>
              <div class="col-4 mt-2 mb-1 px-2">
                  <input class="m-0 form-control" disabled v-mask="'(##) ###-##-##'" v-model="item.phone_number" placeholder="(##) ###-##-##"
                  style="height:28px; font-size: 13px;"
                  type="text" validate error="wrong" success="right"/>
                <small
                  style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
                  class="bg-white px-2 py-0"
                  >{{ $t("phone_number") }}</small
                >
              </div>
            </div >
            <div v-if="item.item_list.length>0" @click="select_car_order(item,i)" >
              <table class="myTableuserSalaryList">
                  <thead>
                    <tr class="header py-0 info_client_header">
                      <th  width="30" class="text-left">№</th>
                      <th width="250">{{$t('product')}}</th>
                      <th>{{$t('qty')}}</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for="(row,rowIndex) in item.item_list" :key="row.id"
                    :class="{'zero_item': row.stockQty <= 0}">
                      <td> <small >{{rowIndex+1}}</small> </td>
                      <td> <small >{{row.product.name}}</small></td>
                      <td> <small >{{row.qty}}</small> </td>
                    </tr>
                  </tbody>
                </table>
            </div>
            <div class="text-right mt-2">
              <mdb-btn  class="m-0 mb-1 mr-2" v-if="item.is_begin == false" size="sm" outline="success" style="font-size:9px; height:30px;" @click="acceptCarItems(item)">
                <i class="fas fa-truck-loading mr-2" style="font-size: 11px;"></i>  Загружено
              </mdb-btn>
              <mdb-btn  class="m-0 mb-1 "  size="sm" outline="info" style="font-size:9px; height:30px;" @click="printOrderCarItem(item)">
                Печат
              </mdb-btn>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="print_loaded" v-show="print_loaded" id="print_load">
      <div class="">
        <div class="delivery-form">
          <div class="header">
            <!-- <div class="form-no">Form No:</div> -->
            <div>
              <h2>ООО «OQQO'RG'ON TEGIRMON»</h2>
              <div class="company-info">
                <p> 📍 Таш. обл., Аккурганский р-н, Дустлик КФЙ</p>
                <p> 📦 Заказ № {{ order_info.id }}</p>
              </div>
            </div>

            <div 
              class="payment-status" 
              :class="order_info.pay_progress == 100 ? 'paid' : 'unpaid'">
              {{ order_info.pay_progress == 100 ? 'ОПЛАЧЕНО' : 'НЕ ОПЛАЧЕНО' }}
            </div>
          </div>

          <div class="section mt-1">
            <h3>Информация о клиенте</h3>
            <div class="row">
              <div class="col-6">
                <table class="customer-info" v-if="order_info.tegirmonOrderClientid">
                  <tr>
                    <td>Ф.И.О</td><td>:</td><td>{{order_info.client.fio}}</td>
                  </tr>
                  <tr>
                    <td>Телефон</td><td>:</td><td>{{order_info.client.phone_number}}</td>
                  </tr>
                  <tr v-if="order_info.client.phone_number1">
                    <td>Телефон</td><td>:</td><td>{{order_info.client.phone_number1}}</td>
                  </tr>
                </table>
                <table class="customer-info" v-else>
                  <tr>
                    <td>Ф.И.О</td><td>:</td><td>________________________</td>
                  </tr>
                  <tr>
                    <td>Телефон</td><td>:</td><td>________________________</td>
                  </tr>
                </table>
              </div>
              <div class="col-6">
                <table class="customer-info" v-if="select_order_info.shafyor_name">
                  <tr>
                    <td>Имя водителя</td><td>:</td><td>{{select_order_info.shafyor_name}}</td>
                  </tr>
                  <tr>
                    <td>Номер машины</td><td>:</td><td>{{select_order_info.car_nomer}}</td>
                  </tr>
                  <tr>
                    <td>Телефон</td><td>:</td><td>{{select_order_info.phone_number}}</td>
                  </tr>
                </table>
              </div>
            </div>
          </div>

          <div class="section mt-1">
            <h3>Информация о заказе</h3>
            <table class="order-table">
              <thead>
                <tr>
                  <th  width="50" class="text-left">№</th>
                  <th>{{$t('product')}}</th>
                  <th>{{$t('kg_ves')}}</th> 
                  <th>{{$t('qty')}}</th> 
                  <th>{{$t('all')}}</th> 
                </tr>
              </thead>
              <tbody>
                <tr v-for="(item,i) in select_order_info.item_list" :key="i">
                  <td>{{i+1}}</td>
                  <td>{{item.product.name}}</td>
                  <td>{{item.product.real_qty}} кг</td>
                  <td>{{item.qty}} {{ item.product.unitMeasurment.name }}</td>
                  <td>{{(item.qty * item.product.real_qty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} кг</td>
                </tr>
                
              </tbody>
            </table>
          </div>

          <div class="footer">
            <div class="truck">🚚</div>
            <div class="signature">
              Кассир: {{order_info.user_name}} <br>
            </div>
          </div>
        </div>
      </div>
      
    </div>
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>

    <Toast ref="message"></Toast>
  </div>
</template>

<script>
import { required } from 'vuelidate/lib/validators'
import {
  mdbBtn,
  mdbIcon,
  mdbTbl,
  mdbTblHead,
  mdbTblBody,
  mdbCol, mdbRow
} from 'mdbvue'
export default {
    data(){
        return{
          modal_info: '',
          modal_status: false,
          loading: false,
          print_loaded: false,

            shafyor_name: '',
            car_nomer: '',
            phone_number: '',
            note: '',
            order_product_qty: 0,
            car_product_qty: 0,
            car_order_list: [],
            last_order_item: [],
            // last_order_list:[],
            order_car_item: [
              {
                product_name: '',
                product_id: null,
                qty: 0,
                qty_str: '0',
                real_qty: 0,
              }
            ],
            id: 0,
            active_car_item: -1,
            car_order_id: 0,
            order_temp_car_order: [],
            select_order_info: {
              shafyor_name: '',
              car_nomer: '',
              phone_number: ''
            }
        }
    },
    components:{
      mdbBtn, mdbTbl,
      mdbTblHead,
      mdbTblBody,
      mdbIcon,
      mdbCol, mdbRow
    },
    validations: {
      shafyor_name: {  required },
      car_nomer: {  required },
      phone_number: {  required },
      order_car_item: {
        $each: {
          qty: {  required },
        },
      },
    },
props: {
    order_info:
    {
      type: Object,
      default(){
        return {
          fio: '',
          id: 0,
        }
      }
    },
    loaded_status:
    {
        type: Boolean,
        default: false,
    }
  },
  methods:{
    async fetchMounted(order){
      this.car_order_id = 0;
      this.active_car_item = -1;
      this.loading = true;
      this.last_order_item = [];
      for(let m=0; m<order.item_list.length; m++){
        let temp = {
          product_name: order.item_list[m].product.name,
          tegirmonProductid: order.item_list[m].product.id,
          qty: order.item_list[m].qty,
        }
        this.last_order_item.push(temp)
      }
      console.log('this.last_order_item', this.last_order_item)
      // this.shafyor_name = order.shafyor_name;
      // this.car_nomer = order.car_nomer;
      this.order_product_qty = order.item_list.reduce((sum, item) => {
        return sum + (item.qty || 0);
      }, 0);
      console.log(this.order_product_qty)
      await this.fetchOrderCarItem(order.id);
      this.loading = false;
        this.order_car_item = [];
        this.order_temp_car_order = [];
        for(let i=0; i<this.last_order_item.length; i++){
          let temp = {
            product_name: this.last_order_item[i].product_name,
            tegirmonProductid: this.last_order_item[i].tegirmonProductid,
            qty: this.last_order_item[i].qty,
            qty_str: this.last_order_item[i].qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
            qty_old: this.last_order_item[i].qty,
            real_qty: this.last_order_item[i].qty,
          }
          this.order_car_item.push(temp)
        }
    },
    async fetchOrderCarItem(order_id){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonOrderCar/GetTegirmonOrderCarOrderId?order_id=" + order_id);
        const data = await response.json();
        if(response.status == 201 || response.status == 200)
        {
          this.car_order_list = data;
          this.car_product_qty = 0;
          for(let i=0; i<this.car_order_list.length; i++){
            let qty_sum = 0;
            qty_sum = this.car_order_list[i].item_list.reduce((sum, item) => {
              return sum + (item.qty || 0);
            }, 0);
            this.car_product_qty += qty_sum;
            for(let j=0; j<this.last_order_item.length; j++){
              for(let k=0; k<this.car_order_list[i].item_list.length; k++){
                if(this.last_order_item[j].tegirmonProductid == this.car_order_list[i].item_list[k].tegirmonProductid){
                  this.last_order_item[j].qty -= this.car_order_list[i].item_list[k].qty;
                }
              }
            }

          }
          console.log(this.order_product_qty)
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
    async select_car_order(car_order,index){
      for(let k=0; k<this.order_car_item.length; k++){
          this.order_car_item[k].real_qty = this.order_car_item[k].qty_old;
          this.order_car_item[k].qty = this.order_car_item[k].qty_old;
          this.order_car_item[k].qty_str = this.order_car_item[k].qty_old.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      this.shafyor_name = car_order.shafyor_name;
      this.car_nomer = car_order.car_nomer;
      this.phone_number = car_order.phone_number;
      let select_car_qty = 0;
      this.active_car_item = index;
      this.car_order_id = car_order.id;
      for(let i=0; i<car_order.item_list.length; i++){
        select_car_qty += car_order.item_list[i].qty;
        for(let j=0; j<this.order_car_item.length; j++){
          if(this.order_car_item[j].tegirmonProductid == car_order.item_list[i].tegirmonProductid){
            this.order_car_item[j].real_qty += car_order.item_list[i].qty;
            this.order_car_item[j].qty += car_order.item_list[i].qty;
            this.order_car_item[j].qty_str =  this.order_car_item[j].qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
            break;
          }
        }
      }

      this.car_product_qty -= select_car_qty;
    },
    async submit(){
      console.log('error')

      if(this.$v.$invalid )
        {
          this.$v.$touch();
          this.$refs.message.warning('please_fill')
          return false;
        }
        let select_qty_sum = 0;
        select_qty_sum = this.order_car_item.reduce((sum, item) => {
          return sum + (item.qty || 0);
        }, 0);

        this.car_product_qty += select_qty_sum;
        let loaded_progress = 0;
        loaded_progress = (this.car_product_qty * 100)/this.order_product_qty;
        

        const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
              // "tegirmonProductid" : this.product_id,
              "shafyor_name": this.shafyor_name,
              "car_nomer": this.car_nomer,
              "phone_number": this.phone_number,
              "tegirmonOrderid": this.order_info.id,
              "auth_user_creator_id": localStorage.AuthId,
              "auth_user_updator_id": loaded_progress.toFixed(),
              "image_url": this.note,
              "item_list": this.order_car_item,
              "is_begin": false,
              "user_name": localStorage.user_name,
              "TegirmonAuthid": localStorage.AuthId,
              "id": this.car_order_id
            })
          };
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/TegirmonOrderCar", requestOptions);
            const data = await response.json();
            this.loading = false;
            if(response.status == 201 || response.status == 200)
            {
              this.$refs.message.success('Added_successfully');
              this.last_order_item = [];
              this.$emit('close');
              return true;
            }
            else{
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





    async addShafyorInfo(){
      console.log('error')
      if(this.$v.$invalid )
        {
          this.$v.$touch();
          this.$refs.message.warning('please_fill')
          return false;
        }
        const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
              // "tegirmonProductid" : this.product_id,
              "shafyor_name": this.shafyor_name,
              "car_nomer": this.car_nomer,
              "phone_number": this.phone_number,
              "tegirmonOrderid": this.order_info.id,
              "auth_user_creator_id": localStorage.AuthId,
              "image_url": this.note,
            })
          };
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/TegirmonOrderCar", requestOptions);
            const data = await response.json();
            console.log('data')
            console.log(data)
            this.loading = false;
            if(response.status == 201 || response.status == 200)
            {
              this.$refs.message.success('Added_successfully')
              this.$emit('close');
              return true;
            }
            else{
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

    changeSumma(n,i){
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
       }
       if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
        tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9' && tols[tols.length-1] != '.'){
        tols = tols.slice(0,tols.length-1)
       }
       this.order_car_item[i].qty_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');

       var temp = ''
       for(let j=0; j<this.order_car_item[i].qty_str.length; j++){
        if(this.order_car_item[i].qty_str[j] != ' '){
          temp += this.order_car_item[i].qty_str[j];
        }
       }
       if(temp == ''){
        this.order_car_item[i].qty = 0;
       }
       else{
        if(parseFloat(temp) > this.order_car_item[i].real_qty){
          this.order_car_item[i].qty = this.order_car_item[i].real_qty;
          this.order_car_item[i].qty_str = this.order_car_item[i].real_qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        }
        else{
          this.order_car_item[i].qty = parseFloat(temp);
        }
       }
    },
    button_qty(i){
      if( this.order_car_item[i].qty_str == '0'){
         this.order_car_item[i].qty_str = '';
      }
    },
    blur_qty(i){
      if( this.order_car_item[i].qty > this.order_car_item[i].real_qty){
         this.order_car_item[i].qty = this.order_car_item[i].real_qty;
         this.order_car_item[i].qty_str = this.order_car_item[i].real_qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
    },
    async acceptCarItems(order_data){
      console.log(order_data);
    },
    printOrderCarItem(order_data){
      this.select_order_info = order_data;
      console.log(order_data);
      console.log(this.order_info)
      this.$nextTick(() => {
        this.printDiv();
      });
    },
    printDiv() {      
      this.print_loaded = true;
      
        let divContents = document.getElementById("print_load").innerHTML;

        // CSS fayllarini olish
        let styles = "";
        document.querySelectorAll('link[rel="stylesheet"], style').forEach((node) => {
          styles += node.outerHTML;
        });

        // Yangi oynani ochish
        let printWindow = window.open("", "", "height=800,width=1000");

        let html =
          '<html>' +
          '<head>' +
          '<title>Chop etish</title>' +
          styles +
          '<style>' +
          /* A4 format + ichki margin */
          '@page { size: A4; margin: 1mm; }' +

          /* print paytida layoutni cheklash */
          '@media print {' +
          '  html, body { width: 210mm; height: 297mm; margin: 0 0; }' +
          '  #print-container { width: 100%; max-width: 190mm; margin: 0 0; }' +
          '  * { -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; color-adjust: exact !important; }' +
          '}' +
          '</style>' +
          '</head>' +
          '<body>' +
          '<div id="print-container">' +
          divContents +
          '</div>' +
          '<script>window.onafterprint = function() { window.close(); }<\/script>' +
          '</body>' +
          '</html>';

        printWindow.document.open();
        printWindow.document.write(html);
        printWindow.document.close();
        printWindow.focus();
        setTimeout(() => {
          printWindow.print();
        }, 1000);
      this.print_loaded = false;
        
    }
    
  }
}
</script>

<style lang="scss" scoped >
td{
  // margin: 0px !important;
  padding: 4px 15px !important;
}
th{
  // margin: 0px !important;
  padding: 7px 5px !important;
}
.delIcon:hover {
  color: #000;
}
.delIcon {
  padding-top: 8px;
  color: red;
}
input:disabled{
  background: rgb(250, 253, 255);
}
input::placeholder {
  color: rgb(179, 179, 179);       /* rang */
  opacity: 1;       /* brauzer default shaffoflikni olib tashlash */
}
.myTableuserSalaryList {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
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
  padding: 3px 10px !important;

}

.myTableuserSalaryList tr {
  border-bottom: 1px solid rgb(219, 219, 219);
  &:hover{
    background: #afd1fd;
  }
}
.info_client_header{
  background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgb(200, 220, 230) 0%, rgb(196, 223, 228) 90.1% );
}
.info_client_header1{
  background-image: radial-gradient( circle farthest-corner at 10% 20%,  rgb(255, 224, 194) 0%, rgb(253, 205, 187) 90.1% ) !important;

}
.ortilgan_mashina{
  border-radius: 7px;
  box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
  &:hover{
    background: #ebebeb;
    cursor: pointer;
  }
}
.print_loaded{
  position: fixed;
  top:0;
  left: 0;
  bottom: 0;
  right: 0;
  background: white;
}


.delivery-form {
  width: 1000px;
  margin: 0px;
  border: 2px solid #0099cc;
  padding: 20px;
}

.delivery-form .header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  border-bottom: 3px solid #0099cc;
  padding-bottom: 10px;
}

.header h1 {
  font-size: 32px;
  margin: 0;
  color: #000;
}

.form-no {
  font-size: 14px;
  color: #333;
}

.company-info {
  margin-top: 15px;
  margin-left: 5px;
  font-size: 15px;
  line-height: 1;
}

.icon {
  font-size: 48px;
}

.section h3 {
  color: #0099cc;
  border-bottom: 1px solid #0099cc;
  padding-bottom: 5px;
}

.customer-info td {
  padding: 3px 8px;
}

.order-table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 10px;
}

.order-table th {
  background-color: #0099cc;
  color: #fff;
  padding: 8px;
}

.order-table td {
  border: 1px solid #0099cc;
  height: 30px;
}

.footer {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: 10px;
}

.truck {
  font-size: 40px;
  color: #0099cc;
}

.signature {
  text-align: right;
  font-size: 14px;
}

.payment-status {
  display: inline-block;
  padding: 10px 20px;
  font-size: 25px;
  font-weight: bold;
  text-transform: uppercase;
  border: 2px solid;
  border-radius: 8px;
  margin-top: 15px;
  margin-right: 10px;
  // transform: rotate(-5deg);
}

/* Tulangan holat */
.payment-status.paid {
  color: #2e7d32;            /* Yashil matn */
  border-color: #2e7d32;     /* Yashil ramka */
  background-color: #c8e6c9; /* Yashil fon (pastel) */
}

/* Tulamagan holat */
.payment-status.unpaid {
  color: #c62828;            /* Qizil matn */
  border-color: #e24545;     /* Qizil ramka */
  background-color: #ffd4d8; /* Qizil fon (pastel) */
}
</style>
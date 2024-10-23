<template>
  <div class="bg-white">
    <div class="p-4" >
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
        <form @submit.prevent="submit">
          <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
            <div class="title w-75 row align-items-center">
               <div class="col-4">
                <div style="position: relative; margin-top: 25px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
                    {{$t('start_time')}}
                  </small>
                  <mdb-input type="date" disabled size="sm"  v-model="Start_time" outline/>
                </div>
              </div>
              <div class="col-4">
                <div style="position: relative; margin-top: 25px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
                    {{$t('end_time')}}
                  </small>
                  <mdb-input type="date" size="sm" disabled  v-model="End_time" outline/>
                </div>
              </div>
              <div class="col-4">
                <select v-model="selected" @change="selectMenu()" disabled class="form-select selet_item" 
                  aria-label="Default select example">
                  <option value="0">Все</option>
                  <option value="1">Захира продукт</option>
                  <option value="2">Тароз список продукт</option>
                  <option value="3">Продажа продукт</option>
                </select>
              </div>
            </div>
            <div class="plus">
              <mdb-btn @click="apply" v-show="false" color="primary py-2 px-3" style="font-size:9px;">
                <mdb-icon style="font-size:9.5px;" icon="check" class="m-0 p-0 mr-1" />
                {{$t('apply')}}
              </mdb-btn>
            </div>
          </div>
        </form>
        <div class="TablePatientDocId p-0 pt-2 px-1">
          <table class="rasxodTableProductDetails" v-if="selected == 3">
            <thead style="background-color: #C4DEE4;">
              <tr class="header ">
                <th  width="40" class="text-left">№</th>
                <th>CheckId</th>
                <th>{{$t('product')}}</th>
                <th >{{$t('qty')}}</th>
                <th >{{$t('measure')}}</th>
                <th >{{$t('sum')}}</th>
                <th>{{$t('created_date_time')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in get_payment_list" :key="rowIndex" @click="selectPaymentItem(row)">
                <td> <small >{{rowIndex+1}}</small> </td>
                <td> <small >{{row.TegirmonCheckid}}</small> </td>
                <td> <small >{{row.product.name}}</small> </td>
                <td > <small v-if="row.qty">{{row.qty.toFixed(1).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                <td> <small >{{row.product.unitMeasurment.name}}</small> </td>
                <td > <small>{{row.summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                <td> <small v-if="row.created_date_time">{{row.created_date_time.slice(8,10) + '-' + row.created_date_time.slice(5,7) + '-' + row.created_date_time.slice(0,4)}} </small> <small v-if="row.created_date_time" class="ml-2">{{row.created_date_time.slice(11,16)}}</small> </td>
              </tr>
            </tbody>
          </table>
          <table class="rasxodTableProductDetails" v-else>
            <thead style="background-color: #C4DEE4;">
              <tr class="header ">
                <th  width="40" class="text-left">№</th>
                <th>InvoiceId</th>
                <th>{{$t('product')}}</th>
                <th >{{$t('qty')}}</th>
                <th >{{$t('measure')}}</th>
                <!-- <th v-if="selected == 3">{{$t('sum')}}</th> -->
                <th>{{$t('created_date_time')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in get_payment_list" :key="rowIndex" @click="selectInvoiceItem(row)">
                <td> <small >{{rowIndex+1}}</small> </td>
                <td> <small >{{row.TegirmonInvoiceid}}</small> </td>
                <td> <small >{{row.product.name}}</small> </td>
                <td > <small v-if="row.real_qty">{{row.real_qty.toFixed(1).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                <!-- <td v-if="selected == 3"> <small>{{row.total_saled_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td> -->
                <td> <small >{{row.product.unitMeasurment.name}}</small> </td>
                <td> <small v-if="row.created_date_time">{{row.created_date_time.slice(8,10) + '-' + row.created_date_time.slice(5,7) + '-' + row.created_date_time.slice(0,4)}} </small> <small v-if="row.created_date_time" class="ml-2">{{row.created_date_time.slice(11,16)}}</small> </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
    <modal-train  :show="invoice_show" headerbackColor="white"  titlecolor="black" title="Zaxiradan olingan mahsulot" 
      @close="invoice_show = false" width="80%">
        <template v-slot:body>
          <info-invoice @close="invoice_show = false"  v-if="invoice_show"  :option="invoice_list" >
          </info-invoice>
        </template>
    </modal-train>
    <modal-train  :show="invoice_show_get" headerbackColor="white"  titlecolor="black" title="Srazi almashtirib ketilgan" 
      @close="invoice_show_get = false" width="80%">
        <template v-slot:body>
          <!-- updateCreateInvoice -->
          <invoiceGetGroup ref="invoiceGetGroupRuyxat">
          </invoiceGetGroup>
        </template>
    </modal-train>
    <checkshow v-if="checkshow" @close="checkshow = false" :option="check_list"/>
    <!-- @close="checkShow" -->
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>
</template>
<script>
  import checkshow from './saledCheckShow.vue'
  import invoiceGetGroup from './changeInvoiceShow.vue'
  import { mdbBtn, mdbInput, mdbIcon   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  import infoInvoice from '../mainPage/infoInvoice.vue'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      mdbIcon,
      mdbInput,
      infoInvoice,
      checkshow,
      invoiceGetGroup
    },
    data(){
      return{
        loading:false,
        modal_info : '',
        modal_status: false,
        
        selected: 0,
        Start_time: null,
        End_time: null,
        product_id: 0,
        get_payment_list: [],

        invoice_show: false,
        invoice_list: {},
        checkshow: false,
        check_list: {},
        invoice_show_get: false,

      }
    },
    async mounted(){
        // let time1 = new Date();
        this.Start_time = localStorage.getItem('begin_date'); 
        this.End_time = localStorage.getItem('end_date');
        this.selected = localStorage.getItem('menu_item');
        this.product_id = localStorage.getItem('product_id');
        if(this.selected == 1){
          await this.ZaxirListDetails();
        }
        else if(this.selected == 2){
          await this.ChangeListDetails();
        }
        else{
          await this.SaledProductDetails();
        }
    },
   
    computed: mapGetters(['get_zaxira_change_invoice_list']),
    methods: {
      ...mapActions(['fetchZaxira_change_invoice_list']),
      ...mapMutations(['district_row_delete',]),
      async selectMenu(){
        console.log('!')
      },
      async ZaxirListDetails(){
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.End_time + 'T23:59:35.000Z';
        console.log(b_date)
        console.log(e_date)
        console.log(this.product_id)
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonInvoiceItem/getPaginationZaxiraProductIdBetweenDate?page=0&size=10000&begin_date="  + b_date + '&end_date='+ e_date + '&product_id=' + this.product_id);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            console.log(data ,  'sadasd data open')
            this.get_payment_list = data.items_list;
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
      async ChangeListDetails(){
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.End_time + 'T23:59:35.000Z';
        console.log(b_date)
        console.log(e_date)
        console.log(this.product_id)
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonInvoiceItem/getPaginationChangeProductIdBetweenDate?page=0&size=10000&begin_date="  + b_date + '&end_date='+ e_date + '&product_id=' + this.product_id);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            console.log(data ,  'sadasd data open')
            this.get_payment_list = data.items_list;
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
      async SaledProductDetails(){
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.End_time + 'T23:59:35.000Z';
        console.log(b_date)
        console.log(e_date)
        console.log(this.product_id)
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonPayments/getPaginationSaledProductDetails?page=0&size=1000&begin_date="  + b_date + '&end_date='+ e_date + '&product_id=' + this.product_id);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            console.log(data ,  'sadasd data open')
            this.get_payment_list = data.items_list;
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
      async apply(){
        console.log('!')
      },
      async selectInvoiceItem(option){
        console.log(option)
        if(this.selected == 1){
          await this.fetchInvoiceId(option.TegirmonInvoiceid);
          this.invoice_show = true;
        }
        else if(this.selected == 2){
          await this.fetchInvoiceIdGetGroupId(option.TegirmonInvoiceid)
        }
      },

      async selectPaymentItem(option){
        console.log(option)
        await this.fetchCheckFullInfo(option.TegirmonCheckid);
        this.checkshow = true;
      },

      async fetchCheckFullInfo(id){
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonCheck/getcheckFullInfoById?check_id=" + id);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            console.log(data, 'check data')
            this.check_list = data;
            return true;
          }
        }
        catch(error){
          console.log(error);
        }
      },

      async fetchInvoiceId(id){
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/getAnyInvoiceFullInfoById?invoice_id=" + id);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            console.log(data, 'invoice data')
            this.invoice_list = data;
            return true;
          }
        }
        catch(error){
          console.log(error);
        }
      },

      async fetchInvoiceIdGetGroupId(id){
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonTortilganBugdoyRoyxatiGroupDetails/getPaginationByInvoiceId?page=0&size=1&invoice_id=" + id);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            this.$refs.invoiceGetGroupRuyxat.updateCreateInvoice(data.items_list[0].TegirmonTortilganBugdoyRoyxatiGroupid);
            this.invoice_show_get = true;
            // this.$router.push('/showGroupDetail_accept/' + data.items_list[0].TegirmonTortilganBugdoyRoyxatiGroupid)
            // console.log(data, 'invoice data')
            return true;
          }
        }
        catch(error){
          console.log(error);
        }
      }
      
      
    },
  };
</script>

<style lang="scss" scoped>

.rasxodTableProductDetails {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; 
  overflow-x:auto
}
.rasxodTableProductDetails th{
  font-size:12px;
}
.rasxodTableProductDetails td{
  font-size:10px;
}
.rasxodTableProductDetails th, .rasxodTableProductDetails td {
  text-align: left;
  padding: 5px;
  small{
    font-weight: 200 !important;
    font-size: 10px;
  }
}

.rasxodTableProductDetails tr {
  cursor: pointer;
  border-bottom: 1px solid rgb(240, 240, 240);
}
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
.selet_item{
  width:100%;
  border:none;
  border: 1px solid #d5d6d7;
  border-radius: 5px;
  color:#495077;
  font-size: 13px;
  padding: 5.8px 3px;
  outline: none;
  margin-top: 1px;
  cursor: pointer;

}
.selet_item:focus{
  border: 1.7px solid #4285F4;
}
.selet_item option{
  cursor: pointer !important;
  font-size: 12.5px;
}
</style>
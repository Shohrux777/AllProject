<template>
  <div>
    <div class="border-bottom d-flex justify-content-between">
      <router-link to="/saled">
         <h5 class="m-0 ml-4 d-flex" style="padding: 16px 0px">
          <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-chevron-left mr-2" width="25" height="25" viewBox="0 0 24 24" stroke-width="2.5" stroke="#00abfb" fill="none" stroke-linecap="round" stroke-linejoin="round">
            <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
            <polyline points="15 6 9 12 15 18" />
          </svg>
            {{'Назад'}}</h5>
      </router-link>
    </div>
    <div class="mx-3 py-1 p-0 border-bottom">
      <mdb-row>
        <mdb-col class="col-12 col-sm-12 col-md-4 col-lg-2 pt-2">
          <multiselect  v-model="value" :options="options"
            :searchable="false"
            style="font-size: 13px; font-family: Ubuntu, sans-serif;"
            :show-labels="false" placeholder="Entries">
          </multiselect>
        </mdb-col>
        <mdb-col class="col-12 col-sm-12 col-md-4 col-lg-3 pt-2">
          <mdb-input type="text" class="m-0 p-0" size="sm" v-model="search_client" placeholder="Search" outline/>
        </mdb-col>

        <!-- <mdb-col class="col-12 col-sm-12 col-md-4 col-lg-3 pt-2">
          <div style="position:relative;">
            <small class="p-0 m-0 mt-0 px-1 bg-white" style="font-size: 11px; position:absolute; z-index:11; left:7px; top:-8px; color:#757575;">{{$t('date')}}</small>
            <mdb-input type="date" class="m-0 p-0" size="sm" v-model="date" outline/>
          </div>
        </mdb-col>
        <mdb-col class="col-12 col-sm-12 col-md-4 col-lg-3 pt-2">
          <div style="position:relative;">
            <small class="p-0 m-0 mt-0 px-1 bg-white" style="font-size: 11px; position:absolute; z-index:11; left:7px; top:-8px; color:#757575;">{{$t('date')}}</small>
            <mdb-input type="date" class="m-0 p-0" size="sm" v-model="date1" outline/>
          </div>
        </mdb-col> -->

       
        <!-- <mdb-col class="col-12 col-sm-6 col-md-3 col-lg-2 text-right">
          <mdb-btn type="submit" color="primary py-1 px-4 " @click="pdf" style="font-size:15px; margin-top: 9px;"  >
            <mdb-icon icon="print" />
          </mdb-btn>
        </mdb-col> -->
      </mdb-row>


    </div>
    <loaderTable v-if="loading"/>
    <div class="mainChart px-3" id="print">
      <table  class="myTable">
        <thead>
              <tr  class="header ">
              <th style="width: 40px;">№</th>
              <th>{{$t('client')}}</th>
              <th>{{$t('phone_number')}}</th>
              <th>{{$t('online_sum')}}</th>
              <th>{{$t('payed_summ')}}</th>
              <th>{{$t('date')}}</th>
              <th>{{$t('payed_user')}}</th>
              <th width="100" class="text-center mr-2">{{$t('Action')}}</th>
              </tr>
            </thead>
        <tbody>
          <tr v-for="(item,i) in filteredList" :key="i" >
            <!-- @click="showCheckID(item.PosCheckid)" -->
            <td>{{i+1}}</td>
            <td>{{item.client.fio}}</td>
            <td>{{item.client.phone_number}}</td>
            <td>{{item.dolg_summ}}</td>
            <td>{{item.payed_summ}}</td>
            <td>{{item.created_date_time.slice(0,10)}} <span class="ml-2">{{item.created_date_time.slice(11,16)}}</span></td>
            <td>{{item.reserved_2}}</td>
            <td class="text-right mr-2">
              <mdb-btn type="submit" color="primary py-1 px-3 " 
                @click="pay_dolg(item.client.fio, item.client.id, item.dolg_summ, item.id)" 
                style="font-size:9px;">
                {{$t('pay_dolg')}}
              </mdb-btn>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <div class="mx-3 p-0">
      <div class=" m-3 mt-4" >
        <div class="d-inline dataTables_info"  role="status" aria-live="polite"> {{activePage+1}} {{$t('to_')}} {{value}} {{$t('of_')}} {{itemQtyPage}} </div>
        <mdb-pagination class="float-right" style="font-size:12px">
          <div @click="firstPage">
            <mdb-page-item :disabled="elementPageList==0">{{$t('First')}}</mdb-page-item>
          </div>
          
          <div @click="prevPage" :class="{'applied': elementPageList==0}">
            <mdb-page-nav  prev :disabled="elementPageList==0"></mdb-page-nav>
          </div>
          <div v-for="(num,i) in pageList[elementPageList]" :key="i"  @click="paginationClick(num)" >
            <mdb-page-item class="text-white"  :class="{ 'active' : activePage == num }"  >{{num+1}}</mdb-page-item>
          </div>
          <div @click="nextPage" :class="{'applied': elementPageList==pageList.length-1}">
            <mdb-page-nav  next :disabled="elementPageList==pageList.length-1"></mdb-page-nav>
          </div>
          <div @click="lastPage">
            <mdb-page-item :disabled="elementPageList==pageList.length-1">{{$t('Last')}}</mdb-page-item>
          </div>
        </mdb-pagination>
      </div>
    </div>



  

    <mdb-modal size="lg" :show="show_check" @close="show_check = false">
      <mdb-modal-header>
        <mdb-modal-title>Чек</mdb-modal-title>
      </mdb-modal-header>
      <mdb-modal-body>
        <show_checked :options="checkdata"/>
      </mdb-modal-body>
      <mdb-modal-footer>
        <mdb-btn color="secondary" size="sm" @click.native="show_check = false">Close</mdb-btn>
        <!-- <mdb-btn color="primary" size="sm">Save changes</mdb-btn> -->
      </mdb-modal-footer>
    </mdb-modal>


    <!--  -->
    <!-- @print="printChek()" -->
    <pay v-show="dolg_show" @close="closePay" ref="payed_dolg"
    :summaString="client_dolg_str" 
    :summa="client_dolg" />

    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>
      <Toast ref="message"></Toast>
  </div>
</template>

<script>
import {
  mdbIcon,
  mdbBtn, mdbInput,
  mdbRow, mdbCol, mdbPagination, mdbPageItem, mdbPageNav,
  mdbModal,
  mdbModalHeader,
  mdbModalTitle,
  mdbModalBody,
  mdbModalFooter,
} from "mdbvue";
import Multiselect from 'vue-multiselect'
import show_checked from '../sale/checkId.vue'
import erpSelect from "../../components/erpSelect.vue";
import pay from "../aptekaSale/pay_dolg.vue"
import {mapActions, mapGetters, mapMutations} from 'vuex'
export default {
  data(){
    return {
      applyStatus: true,
      modal_info: '',
      modal_status: false,
      loading: false,
      auth_id: localStorage.AuthId,
      pageList: [],
      elementPageList:0,

      options: ['50','100','200','250','500'],
      value: 150,
      pageNum: 0,
      activePage: 0,
      itemQtyPage: 0,


      product_id: 0,
      product_name: '',
      allreports: [],
      date: '',
      date1: '',
      dateString: '',
      date1String: '',
      show_check : false,
      checkdata: {},
      dolg_show: false,
      search_client: '',

      client_name: '',
      client_id: null,
      client_dolg: 0,
      client_dolg_str: '',
    
    }
  },
  watch:{
    'value': function(){
      this.refresh();
    }
  },
  components: {
    mdbIcon,
    mdbBtn, mdbInput,
    mdbRow, mdbCol, mdbPagination, mdbPageItem, mdbPageNav,
    Multiselect, erpSelect,
    mdbModal,
    mdbModalHeader,
    mdbModalTitle,
    mdbModalBody,
    mdbModalFooter,
    show_checked,
    pay
  },
  computed: {
    ...mapGetters(['allProduct']),
    filteredList: function(){
        if(this.search_client)
        {
          return this.allreports.filter((item)=>{
            return this.search_client.toLowerCase().split(' ').every(v => item.reserved_1.toLowerCase().includes(v))
          })
        }else
        {
          return this.allreports;
        }
      }
  },
  methods: {
    ...mapActions(['fetchProduct']),
    ...mapMutations([ 'update_pagination_first']),
    async pay_dolg(client_fio, client_id, dolg, dolg_id){
      console.log('hiy')
      this.dolg_show = true;
      this.client_name = client_fio;
      this.client_id = client_id;
      this.client_dolg = dolg;
      this.client_dolg_str = dolg.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      this.$refs.payed_dolg.changingEnter(1, dolg, client_id, client_fio, dolg_id);

    },  
    async closePay(){
      this.dolg_show = false;
      await this.refresh();
    },
    async dolg_close(){
      this.dolg_show = false;
      await this.refresh();
    },
    async paginationClick(index){
       this.activePage = index;
       await this.refresh();
      console.log(index)
    },
    async showCheckID(id){
      this.show_check = true;
      console.log(id)
      try{
          this.loading = true;
          const res = await fetch(this.$store.state.hostname + '/PosChecks/getCheckFullInfo?id=' + id);
          const res_data = await res.json();
          console.log('res_data');
          console.log(res_data);
          this.checkdata = res_data;
          this.loading = false;
        }
        catch{
           this.loading = false;
            this.modal_info = this.$i18n.t('network_ne_connect'); 
            this.modal_status = true;
        }

    },
    async prevPage(){
      console.log('prev')
      this.elementPageList --;
      this.activePage = this.pageList[this.elementPageList][0];
      await this.refresh();
    },
    async nextPage(){
      console.log('next')
      this.elementPageList ++;
      this.activePage = this.pageList[this.elementPageList][0];
      await this.refresh();
    },
    async lastPage(){
        this.elementPageList = this.pageList.length-1;
        this.activePage = this.pageList[this.elementPageList][this.pageList[this.elementPageList].length-1];
      await this.refresh();
    },
    async firstPage(){
        this.elementPageList = 0;
      this.activePage = this.pageList[this.elementPageList][0];
      await this.refresh();
    },

    async selectOption(option){
      this.product_name = option.name
      this.product_id = option.id
      await this.refresh();
    },
    async all_product(){
      this.date = '';
      await this.refresh();
    },
    // async srok(){
    //   this.dateString = this.date + 'T00:00:00.000Z';
    //   this.date1String = this.date1 + 'T00:00:00.000Z';
    //   await this.refresh();
    //   this.date = this.date.slice(0,10);
    // },

      
      async refresh(){
        try{
          this.loading = true;
          const res = await fetch(this.$store.state.hostname + '/PosClientDolgs/getClientDolgList?page' + this.activePage + '&size=' + this.value);
          // const res = await fetch(this.$store.state.hostname + '/PosChecks');
          const res_data = await res.json();
          console.log('res_data');
          console.log(res_data);
          this.allreports = res_data.items_list;
          // this.allreports = res_data;


          this.pageNum = Math.ceil(res_data.items_count/this.value)
          
          this.pageList = []
          var box = []
          box.push(0);
          // this.elementPageList = 0;
          for(let i=1; i<this.pageNum; i++){
            if(i%5!=0){
              console.log('dasdasd hiy')
              box.push(i)
            }
            else{
              this.pageList.push(box);
              box = []
              box.push(i)
              
            }
          }
          this.pageList.push(box);
          box = [];

          this.itemQtyPage = res_data.items_count
          console.log(this.pageNum)
          
          this.loading = false;
        }
        catch{
           this.loading = false;
            this.modal_info = this.$i18n.t('network_ne_connect'); 
            this.modal_status = true;
        }
      },

  },
  async mounted(){
      let time1 = new Date();
      var mtime = new Date().getMonth();
      mtime = parseInt(mtime)+2;
      if(mtime == 11){
        mtime = '12'
      }
      else if(mtime>0 && mtime<10){
        mtime = '0'+ mtime.toString();
      }
      else{
        mtime = mtime.toString();
      }
      console.log('mtime')
      console.log(mtime)
        this.date = time1.toISOString().slice(0,10); 
        this.date1 = time1.toISOString().slice(0,10);
        this.date1 = this.date1.slice(0,5) + mtime + this.date1.slice(7,10)
        this.dateString = this.date + 'T00:00:00.000Z';
        this.date1String = this.date1 + 'T00:00:00.000Z';
        console.log('this.date1String')
      console.log(this.date1String)
        this.loading = true;
          await this.fetchProduct();
          await this.refresh();
        this.loading = false;
  }
}
</script>

<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
<style lang="scss">
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
  padding: 4px;
}

.myTable tr {
  border-bottom: 1px solid rgb(240, 240, 240);
}

.myTable tr.header, .myTable tr:hover {
  background-color: #f1f1f1;
}
tr:nth-child(even) {
  background-color: #f4fafa
}
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
</style>
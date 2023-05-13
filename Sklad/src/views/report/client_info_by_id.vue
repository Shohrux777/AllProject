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
        <!-- <mdb-col class="col-12 col-sm-12 col-md-4 col-lg-2 pt-2">
          <multiselect  v-model="value" :options="options"
            :searchable="false"
            style="font-size: 13px; font-family: Ubuntu, sans-serif;"
            :show-labels="false" placeholder="Entries">
          </multiselect>
        </mdb-col> -->
        <mdb-col class="col-12 col-sm-12 col-md-5 col-lg-4 pt-2 mb-2">
          <div>
            <lineSelectSearch
                  class="mt-1"
                  :options="allClient.rows"
                  :searchshow="true"
                  @select="selectClient"
                  :selected="client_name"
                  url="/PosClients/getClientListByName?page=0&size=100&name="
                  :label="$t('client')"
              />
            <!-- <small v-if="$v.client_name.$dirty && patient_id == null" class="invalid-text pt-4" >
              {{$t('select_item')}}
          </small> -->
        </div>
        </mdb-col>
      </mdb-row>


    </div>
    <div class="totalInfo container-fluid mt-2">
      <div class="row mt-3 border-bottom pb-3">
          <div class="col-4">
              <div class="qty borderSolder py-2">
                  <span class="ml-3">ВЫДАНО</span>
                  <div class="text-right px-3 mt-1">
                      <p>{{client_total}}</p>
                  </div>
              </div>
          </div>
          <div class="col-4">
              <div class="qty borderSolder py-2">
                  <span class="ml-3">ПОЛУЧЕНО</span>
                  <div class="text-right  px-3 mt-1">
                      <p class="text-success"> + {{client_summa}}</p>
                  </div>
              </div>
          </div>
          <div class="col-4">
              <div class="qty borderSolder py-2">
                  <span class="ml-3">БАЛАНС</span>
                  <div class="text-right px-3 mt-1">
                      <p class="text-danger">- {{client_dolg}}</p>
                  </div>
              </div>
          </div>
        </div>
    </div>
    <loaderTable v-if="loading"/>
    

    <div class="main_client_info_list container-fluid row mt-4">
      <div class="col-7">
        <div class="card px-4 py-2 mb-2" v-for="(item,i) in client_summa_list" :key="i">
          <div class="d-flex justify-content-between border-bottom m-0">
            <p class="m-0 mb-1" style="font-size: 12.5px;"><span class="mr-3">{{ $t('date') }}:</span>{{item.date.slice(0,10)}} 
              <span class="ml-1 font-weight-bold">{{item.date.slice(11,16)}}</span>
            </p>
            <!-- <p class="m-0 mb-1" style="font-size: 12.5px;"><span class="mr-3">Кассир:</span>{{ item.reserved_2 }}</p> -->
          </div>
          <!-- <div class="d-flex justify-content-between  m-0 mt-2">
            <p class="m-0 mb-1" style="font-size: 12.5px;">
              {{ $t('client') }}
            </p>
            <p class="m-0 mb-1" style="font-size: 12.5px;">{{ client_name }}</p>
          </div> -->
          <div class="check_table" v-if="item.paymentsList.length">
            <table style="width:100% !important">
              <thead>
                  <tr style="border-bottom: 0.5px solid #CED4DA;">
                    <th  style="font-size: 12px; font-weight: bold; width: 135px;">{{$t('name')}}</th>
                    <th  style="font-size: 12px; font-weight: bold;  width: 55px; text-align: center">{{$t('soni')}}</th>
                    <th  style="font-size: 12px; font-weight: bold; width: 80px; text-align: right">{{$t('price')}}</th>
                    <th  style="font-size: 12px; font-weight: bold; width: 100px; text-align: right">{{$t('total')}}</th>
                  </tr>
              </thead>
              <tbody>
      
                <tr v-for="(data,index) in item.paymentsList" :key="index" style="border-bottom: 0.5px solid #CED4DA;">
                  <td style="font-size: 12px;">{{data.product_name}}</td>
                  <!-- <td v-if="data.product.contains_number_in_pack>1" style="text-align: center"><span>{{parseInt(parseInt(data.qty_in_pack)/parseInt(data.product.contains_number_in_pack)) }}</span> <span> / </span> <span>{{ parseInt(data.qty_in_pack) - parseInt(parseInt(data.qty_in_pack)/parseInt(data.product.contains_number_in_pack))*data.product.contains_number_in_pack}} </span></td> -->
                  <td  style="text-align: center; font-size: 12px;">{{data.qty}}</td>
                  <td style="text-align: right; font-size: 12px;">{{data.real_sum}}</td>
                  <td style="text-align: right; font-size: 12px;">{{data.summ}}</td>
                </tr>
              </tbody>
            </table>
          </div>
          <div class="d-flex justify-content-between border-bottom m-0 mt-2">
            <p class="m-0 mb-1" style="font-size: 12.5px;"> 
              {{ $t('total') }}
            </p>
            <p class="m-0 mb-1 text-dark" style="font-size: 12.5px;"> {{ item.mobil_sum}}</p>
          </div>
          <div class="d-flex justify-content-between border-bottom m-0 mt-2">
            <p class="m-0 mb-1" style="font-size: 12.5px;"> 
              К оплате
            </p>
            <p class="m-0 mb-1 text-success" style="font-size: 12.5px;"> + {{ item.summ}}</p>
          </div>
          <div class="d-flex justify-content-between border-bottom m-0 mt-2" v-if="item.online_sum>0">
            <p class="m-0 mb-1" style="font-size: 12.5px;"> 
              {{ $t('online_sum') }}
            </p>
            <p class="m-0 mb-1 text-danger" style="font-size: 12.5px;"> - {{ item.online_sum}}</p>
          </div>
          <div class="d-flex justify-content-between border-bottom m-0 mt-2" v-if="item.payme_sum>0">
            <p class="m-0 mb-1" style="font-size: 12.5px;"> 
              {{ $t('pay_dolg_id') }}
            </p>
            <p class="m-0 mb-1 text-secondary" style="font-size: 12.5px;"> + {{ item.payme_sum.toFixed(1)}}</p>
          </div>
        </div>           
      </div>
      <div class="col-5 ">
        <div class="card px-4 py-2 mb-2" v-for="(dolg,i) in client_dolg_list" :key="i" v-show="dolg.dolg_summ >0.1">
          <div class="d-flex justify-content-between border-bottom m-0">
            <p class="m-0 mb-1" style="font-size: 12.5px;"><span class="mr-3">{{ $t('date') }}:</span>{{dolg.created_date_time.slice(0,10)}} 
              <span class="ml-1 font-weight-bold">{{dolg.created_date_time.slice(11,16)}}</span>
            </p>
            <p class="m-0 mb-1" style="font-size: 12.5px;"><span class="mr-3">Кассир:</span>{{ dolg.reserved_2 }}</p>
          </div>
          <div class="d-flex justify-content-between  m-0 mt-2">
            <p class="m-0 mb-1" style="font-size: 12.5px;">
              {{ $t('client') }}
            </p>
            <p class="m-0 mb-1" style="font-size: 12.5px;">{{ dolg.client.fio }}</p>
          </div>
          <div class="d-flex justify-content-between border-bottom m-0 mt-2">
            <p class="m-0 mb-1" style="font-size: 12.5px;"> 
              {{ $t('online_sum') }}
            </p>
            <p class="m-0 mb-1 text-danger font-weight-bold" style="font-size: 12.5px;"> - {{ dolg.dolg_summ.toFixed(1)}}</p>
          </div>
        </div>        
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
import lineSelectSearch from '@/components/lineSelectSearch_client'
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
      client_total: 0,
      client_dolg_str: '',
      client_dolg_list: [],
      client_summa: 0,
      client_summa_str: '',
      client_summa_list: [],

      product_name_list: [],
      product_name_str: '',
    
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
    pay,
    lineSelectSearch
  },
  computed: {
    ...mapGetters(['allProduct', 'allClient']),
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
    ...mapActions(['fetchProduct', 'fetchClient']),
    ...mapMutations([ 'update_pagination_first']),
    async selectClient(option){
      console.log(option)
      this.client_name = option.data.fio;
      this.client_id = option.data.id;
      await this.fetchClientIDDolg(option.data.id)
      await this.fetchClientICheck(option.data.id)
    },

    async fetchClientIDDolg(id){
      try{
        const res = await fetch(this.$store.state.hostname + '/PosClientDolgs/getClientDolgByClientIdList?page=0&size=100&client_id=' + id);
        const res_data = await res.json();
        console.log('res_data');
        console.log(res_data);
        this.client_dolg_list = res_data.items_list;
        if(res.status == 200 || res.status == 200 ){
          console.log('dsdadasdasdasda')
          this.client_dolg = 0
          for(let i=0; i<res_data.items_list.length; i++){
            this.client_dolg += res_data.items_list[i].dolg_summ;
          }
          this.client_dolg_str = this.client_dolg.toFixed(1).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
        }
      }
      catch{
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
      }
    },
    async fetchClientICheck(id){
      try{
        const res = await fetch(this.$store.state.hostname + '/PosChecks/getCheckListByPosClientIdFullInfo?page=0&size=100&pos_client_id=' + id);
        const res_data = await res.json();
        console.log('res_data');
        console.log(res_data);
        this.client_summa_list = res_data.items_list;
        if(res.status == 200 || res.status == 200 ){
          this.client_summa = 0;
          this.client_total = 0;
          for(let i=0; i<res_data.items_list.length; i++){
            this.client_total += res_data.items_list[i].mobil_sum;
            this.client_summa += res_data.items_list[i].summ;
          }
          this.client_summa_str = this.client_summa.toFixed(1).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
        }
      }
      catch{
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
      }
    },

    async funcProductName(id){
      const res = await fetch(this.$store.state.hostname + '/PosProducts/' + id);
      const res_data = await res.json();
      console.log('res_data');
      console.log(res_data.name);
      this.product_name_str = res_data.name;
    },


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
          await this.fetchClient();
          console.log('this.allClient')
          console.log(this.allClient)
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
.check_table{
  padding: 10px 10px 15px 10px; 
}
.borderSolder{
  border: 0.5px dashed #D0D3D8;

  span{
      color:#67676C;
      font-size: 22px;
  }
  p{
      color:#525255;
      font-weight:bold;
      font-size: 24px;
      margin:0;
      padding:0;
  }
}
</style>
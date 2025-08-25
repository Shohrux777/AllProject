<template>
  <div class="getProduct">
      <div class="border-bottom d-flex justify-content-between main_header_bg_shadow">
          <router-link to="/sklad">
              <h5 class="m-0 ml-4 d-flex" style="padding: 10px 0px">
                  {{$t('sklad')}}</h5>
          </router-link>
          <!-- <div>
              <router-link to="/sklad_add/0">
                  <mdb-btn class="main_button_add mt-3" m="r3" size="sm" p="r4 l4 t2 b2" style="font-size: 8px;">
                  <mdb-icon icon="plus" class="mr-2"/>{{$t('add')}}</mdb-btn>
              </router-link>
          </div> -->
      </div>
      <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-8">
                <div class="px-0 card p-3 SkladDashboard">
                    <div class="d-flex justify-content-between align-items-center">
                        <div>
                            <span class="header_name ml-2">{{ sklad_name }} oстатка</span>
                        </div>
                        <div class="d-flex" style="position: relative;">
                            <input class="m-0 form-control mr-2" style="height:30px; width: 250px; font-size: 14px;" v-model="search" @input="searchClick()"  type="text" validate error="wrong" success="right"/>
                            <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575; font-size: 12px;">
                              {{$t('search_products')}}
                            </small>
                            <mdb-btn  class="m-0 mb-1 "  size="sm" outline="info" style="font-size:9px; height:30px;" @click="printDiv">
                                Печат
                            </mdb-btn>
                            <!-- <download-excel

                            class=" rounded px-2 excel_btn m-0 bg_col_blue ml-2"
                            style="margin-top:6px; cursor:pointer; height: 29px; width: 100px;"
                            :data = "user_report_list"
                            :fields = "json_fields"
                            :before-generate = "startDownload"
                            :before-finish   = "finishDownload"
                            :name="`Oylik ( ` + user_report_list[0].fio + ' ).xls'">
                            <small class="text-white ml-0" style="font-size: 12px;">
                                <mdb-icon icon="file-excel" class="m-0 p-0 mr-1"></mdb-icon>
                                Excel
                            </small>
                            </download-excel> -->
                        </div>
                    </div>
                    <div id="sklad_ostatka">
                        <loader-table v-if="loading"/>
                        <table class="myTableSkladDashboard">
                            <thead>
                            <tr class="header py-3 table_header_bg">
                                <th  width="40" class="text-left">№</th>
                                <th width="250">{{$t('product')}}</th>
                                <th>Начальный остаток</th>
                                <th>{{$t('prixod')}}</th>
                                <th>{{$t('rasxod')}}</th>
                                <th>{{$t('ostatka')}}</th>
                                <th>{{$t('measurment')}}</th>
                                <!-- <th >{{$t('lessons_cout')}}</th> -->
                                <!-- <th width="80" class="text-center">{{$t('Action')}}</th> -->
                            </tr>
                            </thead>  
                            <!-- v-show="adminStatus == 0 && row.product.inv_accepted_status == false" -->
                            <tbody>
                                <tr v-for="(row,rowIndex) in searchlist" :key="rowIndex" :style="{background:row.product.shitrix_code}" :class="{'ostatka_not_background': row.currentQty<=0}">
                                    <td> <span >{{rowIndex+1}}</span> </td>
                                    <td> <span >{{row.product.name}}</span> </td>
                                    <td> <span >
                                      {{(row.currentQty - row.kirim + row.chiqim).toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                                    <td> <span >{{row.kirim.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                                    <td> <span >{{row.chiqim.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                                    <td> <span >{{row.currentQty.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                                    <td> <span >{{row.unitM.name}}</span> </td>
                                    <!-- <td> <span >{{row.lessons_cout}}</span> </td> -->
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="col-4 ">
              <loader-table v-if="loading" />
              <div v-else class="p-2 card">
                <div>
                  <span class="header_name ml-2">Топ-5 товаров</span>
                </div>
                <TopProductsPie :products="topProducts"/>
              </div>

              <loader-table v-if="loading" />
              <div v-else class="card p-2 mt-3">
                <weeklySkladChart :stats="weeklyStats" />
              </div>
            </div>
        </div>
        
      </div>
    <block ref="blocked"></block>
  </div>
</template>

<script>
import TopProductsPie from "./topProductPia.vue";
import { mdbInput,  mdbBtn, } from "mdbvue"
import erpSelect from "../../components/erpSelectFio.vue";
import erpSelectAdd from "../../components/erpSelectAdd.vue";
import { required } from 'vuelidate/lib/validators'
import {mapActions,mapGetters} from 'vuex'
import LoaderTable from '../../components/loaderTable.vue';
import weeklySkladChart from './weeklySkladChard.vue'
export default {
data(){
    return{
      modal_info: '',
      modal_status: false,  
      loading: false,
      id: this.$route.params.id,
      comenList: [],
      OstatkaList: [],

      name: '',
      user_name: "",
      user_id: null,
      passport_number: '',
      kg_ves: '',
      search: '',
      searchlist: [],
      sklad_name: 'Sotuv Sklad',
      sklad_id: null,

      adminStatus: localStorage.AccessType,
      topProducts: [],
      weeklyStats:[
        {
        "date": "2025-08-14",
        "kirimSum": 1200000,
        "chiqimSum": 800000
      },
      {
        "date": "2025-08-15",
        "kirimSum": 500000,
        "chiqimSum": 900000
      },
      ],
    }
  },
  components: {
    mdbInput,  mdbBtn, erpSelect, erpSelectAdd,
    LoaderTable, TopProductsPie, weeklySkladChart
  },
  validations: {
      name: {
        required
      },
      user_name:{
        required
      }
    },
    // async created() {
    //   await this.loadData();
    // },
    async mounted() {
      await this.fetchUserAccess(localStorage.user_id);

        console.log('hiy')
      // await this.fetchClient();
      await this.getTop5Sklad();
      
      await this.updateOstatka();
      await this.loadData();
    },
   computed: mapGetters(['all_district_t']),
    
  methods: {
    ...mapActions(['fetch_district_t']),
    async fetchUserAccess(id){
        try{
            const res = await fetch(this.$store.state.hostname + '/TegirmonUserAccess/getTegirmonUserAccessUserId?user_id=' + id);
            const data = await res.json();
            console.log('this is by id')
            if(res.status == 200 || res.status == 201){
                console.log(data)
                if(data.num_2 == 0){
                  console.log('hiiii')
                  this.$refs.blocked.show_block();
                }
            }
        }
        catch(error){
            console.log(error)
        }
      },
    
    async updateOstatka(){
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonSkladHistory/getSkladStatistika?sklad_id=1");
        const data = await response.json();
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          console.log('datwa');
          console.log(data);
          this.OstatkaList = data;
          this.searchlist = data;
          this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
          // const data = await response.text();
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
    async getTop5Sklad(){
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonSkladHistory/getSkladStatistikaTop?sklad_id=1");
        const data = await response.json();
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          console.log(data)
          this.topProducts = data;
          this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
          // const data = await response.text();
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
    async loadData(){
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonSkladHistory/getSkladHaftalikStatistika?sklad_id=1");
        const data = await response.json();
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          console.log(data)
          this.weeklyStats = data;
          this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
          // const data = await response.text();
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
    searchClick(){
    console.log('search_here')
    this.searchlist = this.OstatkaList;
      this.loading = true;
      if(this.search != ''){
        let userSearchList = [];
        for(let i=0; i<this.searchlist.length;i++){
          if(this.searchlist[i].product.name.toLowerCase().includes(this.search.toLowerCase()) || this.searchlist[i].product.price.toString().includes(this.search.toLowerCase()) ){
            userSearchList.push(this.searchlist[i])
          }
        }
        this.searchlist = userSearchList;
      }
      else{
        this.searchlist = this.OstatkaList;
      }
      this.loading = false;
    },
    printDiv() {
        let divContents = document.getElementById("sklad_ostatka").innerHTML;

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
        '@page { size: A4; margin: 5mm; }' +
        '@media print { ' +
        'body { zoom: 1; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; color-adjust: exact !important; }' +
        '}' +
        '</style>' +
        '</head>' +
        '<body>' +
        divContents +
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
  background: #fdfdfd;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #b4c2d6;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #212d3d;
}
  .product_name_price{
    height: 90px;
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
  border: 1px solid rgb(240, 240, 240);
}

.myTable tr.header, .myTable tr:hover {
  // background-color: #f1f1f1;
}
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
.SkladDashboard{
    max-height: calc(100vh - 80px) !important;
    overflow: hidden;
    overflow-y: scroll;
}
.myTableSkladDashboard {
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  font-size: 18px;
  max-height:80px; overflow-x:auto
}
.myTableSkladDashboard th{
  font-weight: 600;
  font-size:11px;
}
.myTableSkladDashboard td{
  font-size:11px;
  
}
.myTableSkladDashboard td {
  text-align: left;
  padding: 5px 10px;
  border: 1px solid rgb(185, 185, 185);

}
.myTableSkladDashboard th{
  text-align: left;
  padding: 5px 10px;
  border: 1px solid #CBDCE6;

}

.myTableSkladDashboard tr {
  &:hover{
    background: #afd1fd;
  }
}

.myTableSkladDashboard tr.header, .myTableSkladDashboard tr:hover {
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
.ostatka_not_background{
  background: rgb(255, 239, 239);
}
.header_name{
    font-size: 14.5px;
    // margin-bottom: 5px;
    color: #484d53;
}
</style>
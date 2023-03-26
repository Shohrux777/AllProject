<template>
  <div class="bg-white">
   <loader v-if="loading"/>
    <div class="py-0">
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
        <form @submit.prevent="submit">
          <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
            <div class="title w-75 row align-items-center">
              <div class="col-4">
                <mdb-input type="text" v-model="search" size="sm" :label="$t('search_here')" outline></mdb-input>
              </div>
              <div class="col-1 m-0 p-0">
                <mdb-btn type="submit" color="primary py-2 px-4"  style="font-size:10px;" >
                {{$t('search')}}
              </mdb-btn>
              </div>
            </div>
            <div class="plus">
              <mdb-btn @click="$router.back()" color="primary py-2 px-4"  style="font-size:10px;" >
                {{$t('back')}}
              </mdb-btn>
            </div>
          </div>
        </form>
        <div class="TablePatientBron mt-2">
            <table class="myTable">
              <thead class="rasxod_table_header">
                <tr class="header">
                  <th width="40">№</th>
                  <th width="270">{{$t('patient')}}</th>
                  <th>{{$t('Room')}}</th>
                  <th width="50">День</th>
                  <th >{{$t('Start_time')}}</th>
                  <th >{{$t('End_time')}}</th>
                  <th >Цена за один день</th>
                  <th >Платная сумма</th>
                  <th >Не оплатет сумма</th>
                  <th class="text-end">{{$t('Action')}}</th>
                </tr>
              </thead>
              <tbody class="rasxod_body">
                <tr v-for="(row,rowIndex) in filteredList" :key="rowIndex" class="hover_bron_item">
                  <td> <span >{{rowIndex+1}}</span> </td>
                  <td> <span >{{row.reserved_name_1}}</span> </td>
                  <td> <span >{{row.reserved_name_2}}</span> </td>  
                  <td> <span class="font-weight-bold">{{row.reserved_number_1}}</span> </td>   
                  <td> <span >{{row.begin_date_bron.slice(0,10)}}</span> </td>   
                  <td> <span >{{row.end_date_bron.slice(0,10)}}</span> </td>   
                  <td> <span >{{row.price_for_one_day.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>   
                  <td> <span class="text-success">{{row.payed_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>   
                  <td> <span class="text-danger"> {{row.need_payed_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>   
                  <td class="text-end"> 
                    <mdb-btn color="success" @click="payed_payment(row)" style="font-size: 9px;" class="px-2 py-1 ml-0">{{$t('pay')}}</mdb-btn> 
                    <mdb-btn color="primary" :disabled="row.need_payed_summ != 0" style="font-size: 9px;" class="px-2 py-1 ml-0">{{$t('finish')}}</mdb-btn> 
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
      </div>
    </div>

     <vue-html2pdf ref='listlar'
        :show-layout="false"
        :float-layout="true"
        :enable-download="false"
        :preview-modal="true"
        :paginate-elements-by-height="1600"
        filename="hee hee"
        :pdf-quality="2"
        :manual-pagination="false"
        pdf-format="a4"
        pdf-orientation="landscape"
        pdf-content-width="100%"
        @hasStartedGeneration="hasStartedGeneration()"
        @hasGenerated="hasGenerated($event)"
      >
      <div slot="pdf-content">
        <!-- <anyTable
          :datasource="m_contragent"
          @page="page"
          @size="size"
          @pay="pay_debit"
          :debit="debit"
          /> -->
      </div>

    </vue-html2pdf>

       <ModalUser  :show="payment_bron_show" classes="bg_dolg" closeColor="white" titlecolor="white" 
        :title="$t('debit')" @close="payment_bron_show = false" width="600px">
        <template v-slot:body>
          <pay_cash :option = "payment_data" @close="payed_close"/>
        </template>
      </ModalUser>
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>
  import VueHtml2pdf from 'vue-html2pdf'
import { required } from 'vuelidate/lib/validators'
import ModalUser from '../../components/modal.vue'
import pay_cash from './pay_cash.vue'
  // import lineSelect from "../../components/lineSelect.vue";
  import { mdbBtn, mdbInput,   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      VueHtml2pdf,
      mdbInput, 
      ModalUser,
      pay_cash
    },
    data(){
      return{

        payment_bron_show: false,
        payment_data: {},
        
        
        patient_id: null,
        summa: 0,
        summaString: '',
        patientName: '',

        search:'',

        show: false,
        snipper: true,
        editData: {},
        modal_info : '',
        modal_status: false,
        contragent_name: '',
        contragent_id: null,
        Start_time: new Date(),
        End_time: new Date(),
        price: null,
        reason: '',
        loading: false,
        bonus_contragent_list: [],
        showen_Auth: false,

        m_contragent :{
          rows: [],
          columns: ['patient_name', 'real_qty', 'created_date_time'],
          col : []
        },
      }
    },
    validations: {
      price: {required},
      contragent_name : {required}
    },
    async mounted(){
      this.refresh();
      if(localStorage.AuthId === 0){
        this.showen_Auth = true
        console.log(localStorage.Auth)
      } 
    },
    computed:{
      ...mapGetters(['get_contragent_list', 'get_report_by_data_time', 'get_report_by_time_card_cash','get_pagination']),
      filteredList: function(){
          if(this.search)
          {
            return this.m_contragent.rows.filter((item)=>{
            console.log(item.reserved_name_1)
            console.log(this.search)
              return this.search.toLowerCase().split(' ').every(v => item.reserved_name_1.toLowerCase().includes(v))
            })
          }else
          {
            return this.m_contragent.rows;
          }
        }
    },
    methods: {
      ...mapActions(['fetch_contragent', 'fetch_report_by_data_time']),
      ...mapMutations(['district_row_delete', 'update_pagination_first']),
      cls_wnd(){
        this.price =  null;
        this.reason =  '';
        this.contragent_name = '';
        this.contragent_id = null;
      },
      payed_payment(item){
        this.payment_bron_show = true;
        this.payment_data = item;
      },
      async payed_close(){
        this.payment_bron_show = false;
        await this.refresh();
        this.$refs.message.success('Added_successfully');
      },
      async refresh(){
        const res = await fetch(this.$store.state.hostname + '/HospitalBronRoomPayments/getPaginationOnlyFinishFalse?page=0&size=400');
        const res_data = await res.json();
        console.log('res_data')
        console.log(res_data)
        this.m_contragent.rows = res_data.items_list;
        this.loading = false;
      },
      async fetch_finish_payment(id){
        const res = await fetch(this.$store.state.hostname + '/HospitalBronRoomPayments/finishPayment?id=' + id);
        const res_data = await res.json();
        console.log('res_data')
        console.log(res_data)
      },

      print(){
        this.$refs.listlar.generatePdf()
      },
    },
  };
</script>

<style lang="scss">


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
.TablePatientDocIds{
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
.TablePatientBron .myTable th, td{
  padding: 7px 4px !important;
}
.TablePatientBron .myTable .rasxod_table_header{
  background-image: radial-gradient( circle farthest-corner at 1.3% 2.8%,  rgb(207, 219, 238) 100.2%, rgb(198, 214, 241) 100.2% );
}
.hover_bron_item:hover{
  background: #e6ebf1;
}
</style>
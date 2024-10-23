<template>
  <div class="bg-white">
    <loader v-if="loading"/>
    <div class="p-4">
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
        <form @submit.prevent="submit">
          <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
            <div class="title w-75 row align-items-center">
               <div class="col-4">
                <div style="position: relative; margin-top: 25px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -12px; color: #757575;">
                    {{$t('start_time')}}
                  </small>
                  <mdb-input type="date" size="sm"  v-model="Start_time" outline/>
                </div>
              </div>
              <div class="col-4">
                <div style="position: relative; margin-top: 25px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -12px; color: #757575;">
                    {{$t('end_time')}}
                  </small>
                  <mdb-input type="date" size="sm"  v-model="End_time" outline/>
                </div>
              </div>
            </div>
            <div class="plus d-flex">
              <mdb-btn type="submit" color="primary py-2 px-4" style="font-size:10px;"  >
                {{$t('apply')}}
              </mdb-btn>
              <mdb-btn @click="print" color="info py-2 px-4"  style="font-size:10px;" >
                {{$t('print')}}
              </mdb-btn>
              <div>
                <download-excel
                  :data = "get_payment_payed_history_excel"
                  :fields = "json_fields"
                  name="Стационар.xls">
                  <mdb-btn color="info py-2 px-4"  style="font-size:10px;" >
                    <mdb-icon icon="file-excel" class="mr-1"></mdb-icon>
                    Excel
                  </mdb-btn>
                </download-excel>
              </div>
            </div>
          </div>
        </form>
        <div class="row mt-3 border-bottom pb-3">
          
          <div class="col-3">
              <div class="qty borderSolder py-2">
                  <span class="ml-3">{{$t('qty')}}</span>
                  <div class="text-right px-3 mt-1">
                      <p>{{qty}}</p>
                  </div>
              </div>
          </div>
          
          <div class="col-3">
              <div class="qty borderSolder py-2">
                  <span class="ml-3">{{$t('cash')}}</span>
                  <div class="text-right px-3 mt-1">
                      <p>{{cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                  </div>
              </div>
          </div>
          <div class="col-3">
              <div class="qty borderSolder py-2">
                  <span class="ml-3">{{$t('card')}}</span>
                  <div class="text-right px-3 mt-1">
                      <p>{{card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                  </div>
              </div>
          </div>
        
          <div class="col-3">
              <div class="qty borderSolder py-2">
                  <span class="ml-3">{{$t('summ_payed')}}</span>
                  <div class="text-right px-3 mt-1">
                      <p>{{(card+cash).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</p>
                  </div>
              </div>
          </div>
        </div>
        <div class="TablePatientDocId p-3">
          <table class="myTable">
            <thead>
              <tr class="header ">
                <th  width="40" class="text-left">№</th>
                <th width="200">{{$t('patient_name')}}</th>
                <th width="200">{{$t('doctor_name')}}</th>
                <th>{{$t('Room')}}</th>
                <th>День</th>
                <th>{{$t('cash')}}</th>
                <th>{{$t('card')}}</th>
                <th>Деньги на еду</th>
                <th>Осталь. денег</th>
                <th>Возврат</th>
                <th>Неоплачено</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in get_payment_list" :key="rowIndex" :class="{'alert-danger' : row.vozvrat_sum}">
                <td> <span >{{rowIndex+1}}</span> </td>
                <td><span>{{ row.hospitalBronRoomPayments.reserved_name_1 }}</span></td>
                <td><span>{{ row.hospitalBronRoomPayments.reserved_name_3 }}</span></td>
                <td><span>{{ row.hospitalBronRoomPayments.reserved_name_2 }}</span></td>
                <td><span>{{ row.hospitalBronRoomPayments.reserved_number_1 }}</span></td>
                <td class="alert-success"><span>{{ row.cash_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span></td>
                <td class="alert-success"><span>{{ row.card_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span></td>
                <td>
                  <span>{{ row.ovqat_puli.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span>
                </td>
                <td>
                  <span>{{ row.qolgan_pul.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span>
                </td>
                <td>
                  <span v-if="row.vozvrat_sum == null">---</span>
                  <span v-else class="font-weight-bold text-secondary">{{ row.vozvrat_sum }}</span>
                </td>
                <td><span class="font-weight-bold text-danger">{{ row.hospitalBronRoomPayments.need_payed_summ }}</span></td>
              </tr>
              <tr >
                <td> <span class="text-primary">Общий</span> </td>
                <td> <span ></span> </td>
                <td> <span ></span> </td>
                <td> <span class="text-primary"></span></td>
                <td> <span class="text-primary">{{(card+cash).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td class="alert-success"> <span class="text-primary font-weight-bold">{{cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td class="alert-success"> <span class="text-primary font-weight-bold">{{card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span class="text-primary">{{get_bron_cash_card.ovqat_puli.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span class="text-primary">{{get_bron_cash_card.qolgan_pul.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span class="text-secondary font-weight-bold">{{get_bron_cash_card.vozvrat_sum.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span class="text-danger font-weight-bold">{{get_bron_cash_card.need_payed_summ.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
              </tr>
            </tbody>
          </table>
        </div>
        <div class="p-3">
          <table>
            <tbody>
              <tr class="py-3 border-bottom">
                <td><span class="text-primary">Общий сумма: </span></td>
                <td><span class="text-primary pl-4">{{(card+cash).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</span></td>
              </tr>
              <tr class="py-3 border-bottom">
                <td><span class="text-primary">Возврат: </span></td>
                <td><span class="text-primary pl-4">{{get_bron_cash_card.vozvrat_sum.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</span></td>
              </tr>
              <tr class="py-3 border-bottom">
                <td><span class="text-primary">Деньги на еду: </span></td>
                <td><span class="text-primary pl-4">{{get_bron_cash_card.ovqat_puli.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</span></td>
              </tr>
              <tr class="py-3 border-bottom">
                <td><span class="text-primary">Осталь. денег: </span></td>
                <td><span class="text-primary pl-4">{{get_bron_cash_card.qolgan_pul.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</span></td>
              </tr>
              <tr class="py-3 border-bottom">
                <td><span class="text-primary">Долг: </span></td>
                <td><span class="text-primary pl-4">{{get_bron_cash_card.need_payed_summ.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</span></td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>

    <!-- <div :class="{'showing':show}">
      <div class="add d-flex justify-content-center align-items-center" >  
        <districtAdd/>
      </div>
    </div> -->
      <!-- <mdb-modal  :show="show"  @close="show = false"  light>
        <mdb-modal-header>
          <mdb-modal-title style="font-weight:  500;">{{$t('Добавить Район')}}</mdb-modal-title>
        </mdb-modal-header>
        <mdb-modal-body>
          <districtAdd :options="editData"/>
        </mdb-modal-body>
      </mdb-modal> -->
      <mdb-modal :show="show" @close="show = false" size="sm" class="text-center" danger>
        <mdb-modal-header center :close="false">
          <p class="heading">{{$t('Are_you_sure')}}</p>
        </mdb-modal-header>
        <mdb-modal-body>
          <mdb-icon icon="times" size="4x" class="animated rotateIn"/>
        </mdb-modal-body>
        <mdb-modal-footer center>
          <mdb-btn outline="danger" @click="promiseService">{{$t('Yes')}}</mdb-btn>
          <mdb-btn color="danger" @click="show = false">{{$t('No')}}</mdb-btn>
        </mdb-modal-footer>
      </mdb-modal>
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>
  // import VueHtml2pdf from 'vue-html2pdf'
  // import DatePicker from 'vue2-datepicker';
  // import districtAdd from "../../../components/new_prog_add/district_add"
  import { mdbBtn, mdbInput, mdbIcon,  mdbModal, mdbModalHeader,  mdbModalBody,mdbModalFooter   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      mdbModal, mdbModalHeader, mdbIcon, mdbModalBody, mdbModalFooter,
      mdbInput,
    },
    data(){
      return{
        show: false,
        admin:false,
        snipper: true,
        loading:false,
        editData: {},
        modal_info : '',
        modal_status: false,
        contragent_name: '',
        contragent_id: 0,
        Start_time: null,
        End_time: null,
        group_name: '',
        group_id: null,
        get_payment_list: [],
        index: null,
        service_id: null,
        day: '',
        qty: 0,
        summ: 0,
        card: 0,
        cash: 0,
        discount_qty: 0,
        json_fields: {
          'Имя пациента': 'hospitalBronRoomPayments.reserved_name_1',
          'Имя врача': 'hospitalBronRoomPayments.reserved_name_3',
          'День': 'hospitalBronRoomPayments.reserved_number_1',
          'Сумма': 'summ',
        },  
      }
    },
    async mounted(){
      if(localStorage.Type == 0){
        this.admin = true;
      }
      {
        let time1 = new Date();
        this.Start_time = time1.toISOString().slice(0,10); 
        this.End_time = time1.toISOString().slice(0,10);
        let a = this.Start_time + 'T00:00:35.000Z' ;
        let b = this.End_time + 'T23:59:50.000Z';
        let c = {
          time1: '',
          time2: '',
        }
        c.time1 = a;
        c.time2 = b;
        this.loading = true;
        await this.fetch_report_by_bron_payed(c);
        await this.fetch_report_by_bron_payed_excel(c);
        this.get_payment_list = this.get_payment_payed_history
        this.qty = this.get_bron_cash_card.qty
        this.summ = this.get_bron_cash_card.cash + this.get_bron_cash_card.card;
        this.card = this.get_bron_cash_card.card;
        this.cash = this.get_bron_cash_card.cash;

        this.loading = false;
      }

        this.fetch_service_group()
    },
    computed: mapGetters(['get_contragent_list', 'get_report_by_data_time', 'get_report_by_time_card_cash',
     'get_payment_payed_history', 'get_bron_cash_card', 'get_payment_payed_history_excel']),
    methods: {
      ...mapActions(['fetch_contragent', 'fetch_report_by_bron_payed', 'fetch_service_group', 'fetch_report_by_bron_payed_excel']),
      ...mapMutations(['district_row_delete']),

      add(){
        this.show =! this.show
        this.editData = {};
      },
      print(){
        window.print();
      },
      deleteRow(data, index){
        console.log(data)
        this.index = index
        this.service_id = data.id
        this.show = true;
      },
      async promiseService(){
        const requestOptions = {
              method : "delete",
            };
            const response = await fetch(this.$store.state.hostname + "/Payments/" + this.service_id, requestOptions);
            const data = await response.json();
            console.log(data)
            if(data.id)
            {
              this.$refs.message.success('Successfully_removed')
              this.get_payment_list.splice(this.index, 1);
              this.show = false;
            }
      },
      async submit(){
        let a = {
          time1: "2021-09-01T09:15:28.886Z",
          time2: new Date(),
        }
        console.log('this.Start_time')
        console.log(this.Start_time)
        console.log(this.End_time)
        a.time1 = this.Start_time + 'T00:00:35.000Z';
        a.time2 = this.End_time + 'T23:59:50.000Z';
        this.loading = true;
        await this.fetch_report_by_bron_payed(a);
        await this.fetch_report_by_bron_payed_excel(a);
        console.log('this.get_payment_payed_history')
        console.log(this.get_payment_payed_history)
        this.get_payment_list = [];
          this.get_payment_list = this.get_payment_payed_history
          this.qty = this.get_bron_cash_card.qty
          this.summ = this.get_bron_cash_card.cash + this.get_bron_cash_card.card;
          this.card = this.get_bron_cash_card.card;
          this.cash = this.get_bron_cash_card.cash;
          this.loading = false;
        console.log(this.get_payment_list)
        
      },

    },
  };
</script>

 <style lang="scss" scoped>
    @import "../../../scss/tableAll.scss";
  </style>
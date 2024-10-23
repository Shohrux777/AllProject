<template>
    <div class="bg-white">
      <loader v-if="loading"/>
      <div class="p-4">
        <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
          <div class="header_title_vrach border-bottom mb-2">
            <span style="font-size: 17px;">{{ $t('doctor_report_salary') }}</span>
          </div>
          <form @submit.prevent="submit">
            <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
              <div class="title w-75 row align-items-center">
                 <div class="col-3">
                  <div style="position: relative; margin-top: 25px;"> 
                    <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -12px; color: #757575;">
                      {{$t('start_time')}}
                    </small>
                    <mdb-input type="date" size="sm" v-model="Start_time" outline/>
                  </div>
                </div>
                <div class="col-3">
                  <div style="position: relative; margin-top: 25px;"> 
                    <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -12px; color: #757575;">
                      {{$t('end_time')}}
                    </small>
                    <mdb-input type="date" size="sm" v-model="End_time" outline/>
                  </div>
                </div>
                
                <div class="col-2 m-0 p-0">
                  <mdb-btn type="submit" color="primary py-2 px-4" style="font-size:9px;">
                    {{$t('apply')}}
                  </mdb-btn>
                </div>
                <!-- <div class="col-4">
                  <RegSelect
                  :label="$t('service_group')"
                  @select="select_service_Group"
                  :options = "get_service_group_list" 
                  :selected="group_name"
                  class="mb-3"
                 />
                </div> -->
              </div>
              
            </div>
          </form>
          
          <div class="summa_header_doc mt-2">
            <span class="summa_header_title pr-3 py-1">{{ $t('report_bron') }}</span>
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
              <tr v-for="(row,rowIndex) in get_payment_payed_history_doc_id" :key="rowIndex" :class="{'alert-danger' : row.vozvrat_sum}">
                <td> <span >{{rowIndex+1}}</span> </td>
                <td><span>{{ row.hospitalBronRoomPayments.reserved_name_1 }}</span></td>
                <td><span>{{ row.hospitalBronRoomPayments.reserved_name_3 }}</span></td>
                <td><span>{{ row.hospitalBronRoomPayments.reserved_name_2 }}</span></td>
                <td><span>{{ row.hospitalBronRoomPayments.reserved_number_1 }}</span></td>
                <td class="alert-success"><span>{{ row.cash_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span></td>
                <td class="alert-success"><span>{{ row.card_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span></td>
                <td><span>{{ row.ovqat_puli.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span></td>
                <td><span>{{ row.qolgan_pul.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span></td>
                <td>
                  <span v-if="row.vozvrat_sum == null">---</span>
                  <span v-else class="font-weight-bold text-secondary">{{ row.vozvrat_sum }}</span>
                </td>
                <td><span class="text-danger font-weight-bold">{{ row.hospitalBronRoomPayments.need_payed_summ }}</span></td>
              </tr>
              <tr >
                <td> <span class="text-primary">Общий</span> </td>
                <td> <span ></span> </td>
                <td> <span ></span> </td>
                <td> <span class="text-primary"></span></td>
                <td> <span class="text-primary">{{(get_bron_card_summa+get_bron_cash_summa).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>

                <td class="alert-success"> <span class="text-primary">{{get_bron_cash_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td class="alert-success"> <span class="text-primary">{{get_bron_card_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span class="text-primary">{{get_bron_dicId_summa.ovqat_puli.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span class="text-primary">{{get_bron_dicId_summa.qolgan_pul.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span class="text-secondary font-weight-bold">{{get_bron_dicId_summa.vozvrat_sum.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span class="text-danger font-weight-bold">{{get_bron_need_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                
              </tr>
            </tbody>
          </table>
        </div>
        <div class="p-3">
          <table>
            <tbody>
              <tr class="py-3 border-bottom">
                <td><span class="text-primary">Общий сумма: </span></td>
                <td><span class="text-primary pl-4">{{(get_bron_card_summa+get_bron_cash_summa).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</span></td>
              </tr>
              <tr class="py-3 border-bottom">
                <td><span class="text-primary">Возврат: </span></td>
                <td><span class="text-primary pl-4">{{get_bron_dicId_summa.vozvrat_sum.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</span></td>
              </tr>
              <tr class="py-3 border-bottom">
                <td><span class="text-primary">Деньги на еду: </span></td>
                <td><span class="text-primary pl-4">{{get_bron_dicId_summa.ovqat_puli.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</span></td>
              </tr>
              <tr class="py-3 border-bottom">
                <td><span class="text-primary">Осталь. денег: </span></td>
                <td><span class="text-primary pl-4">{{get_bron_dicId_summa.qolgan_pul.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</span></td>
              </tr>
              <tr class="py-3 border-bottom">
                <td><span class="text-primary">Долг: </span></td>
                <td><span class="text-primary pl-4">{{get_bron_need_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</span></td>
              </tr>
            </tbody>
          </table>
        </div>
        </div>
      </div>
  
      
        <mdb-modal :show="show" @close="show = false" size="sm" class="text-center" danger>
          <mdb-modal-header center :close="false">
            <p class="heading">{{$t('Are_you_sure')}}</p>
          </mdb-modal-header>
          <mdb-modal-body>
            <mdb-icon icon="times" size="4x" class="animated rotateIn"/>
          </mdb-modal-body>
          <mdb-modal-footer center>
            <mdb-btn outline="danger" >{{$t('Yes')}}</mdb-btn>
            <mdb-btn color="danger" @click="show = false">{{$t('No')}}</mdb-btn>
          </mdb-modal-footer>
        </mdb-modal>
      <Toast ref="message"></Toast>
       <massage_box :hide="modal_status" :detail_info="modal_info"
        :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
    </div>
  
  </template>
  
  <script>
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
          contragent_name: '',
          contragent_id: 0,
          user_id: localStorage.docId,
          auth_id: localStorage.AuthId,
          service_procent: 0,
          service_price: 0,
          service_salary_price: 0,
        }
      },
      async mounted(){

            let time1 = new Date();
            this.Start_time = time1.toISOString().slice(0,10); 
            this.End_time = time1.toISOString().slice(0,10);
            this.user_id = localStorage.docId;
            let a = this.Start_time + 'T00:00:35.000Z';
            let b = this.End_time + 'T23:59:59.000Z';
            let d = {
              time1: '',
              time2: '',
              doc_id: this.user_id
            }
            d.time1 = a;
            d.time2 = b;
            this.loading = true;
            console.log(d)
            await this.fetch_report_by_bron_payed_doc_id(d)
            this.get_payment_list = this.get_report_by_time_card_cash_bydoctorInfo.rows
            this.loading = false;

      },
      computed: mapGetters(['get_report_by_time_card_cash_bydoctorInfo','get_payment_payed_history_doc_id',
       'auth_user_list', 'get_report_by_time_card_cash', 'get_service_group_list',
      'get_bron_cash_summa', 'get_bron_card_summa', 'get_bron_need_summa', 'get_bron_dicId_summa']),
      methods: {
        ...mapActions(['fetch_auth_list','fetch_report_by_bron_payed_doc_id', 'fetch_report_by_data_time_by_doctorinfo', 'fetch_service_group']),
        ...mapMutations(['district_row_delete']),
  
        add(){
          this.show =! this.show
          this.editData = {};
        },
        print(){
          this.$refs.listlar.generatePdf()
        },
        async submit(){
          let d = {
            time1: '',
            time2: '',
            doc_id: this.user_id
          }
          d.time1 = this.Start_time + 'T00:00:35.000Z';
          d.time2 = this.End_time + 'T23:59:59.000Z';
          this.loading = true;
          await this.fetch_report_by_bron_payed_doc_id(d)
          this.get_payment_list = [];
          this.get_payment_list = this.get_report_by_time_card_cash_bydoctorInfo.rows
          this.loading = false;
        },
  
      },
    };
  </script>
  <style lang="scss" scoped>
    @import "../../../scss/tableAll.scss";
  </style>
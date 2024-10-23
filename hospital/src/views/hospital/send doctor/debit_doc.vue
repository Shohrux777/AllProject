<template>
  <div class="bg-white">
   <loader v-if="loading"/>
    <div class="p-4">
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
        <form @submit.prevent="submit">
          <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
            <div class="title w-75 row align-items-center">
              <div class="col-4 m-0 pr-0">
                <mdb-input type="text" v-model="search" size="sm" :label="$t('search_here')" outline></mdb-input>
              </div>
              <div class="col-1 m-0 pl-0">
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
        <div class="newTableDebit">
          <div class="TablePatientDocId p-3">
            <table class="myTabledebit">
              <thead>
                <tr class="header">
                  <th  width="40" class="text-left">№</th>
                  <th width="300">{{$t('patient_name')}}</th>
                  <th >{{$t('service')}}</th>
                  <th>{{$t('debit')}}</th>
                  <th>{{$t('date')}}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,rowIndex) in filteredList" :key="rowIndex">
                  <td> <small style="font-size: 12.5px;">{{rowIndex+1}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.patient_name}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.payments.ServiceName}}</small> </td>
                  <td> <small style="font-size: 12.5px;" class="text-danger">{{row.dolg_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                  <td> <small style="font-size: 12.5px;">{{row.dolgDate.slice(0,10)}}</small> <small style="font-size: 12px;">{{row.dolgDate.slice(11,19)}}</small></td>
                </tr>
                <tr class="alert-primary">
                  <td colspan="2">Общий</td>
                  <td></td>
                  <td colspan="2" class="font-weight-bold text-danger">
                    {{ filteredList.reduce((summa, item) => {return summa += item.dolg_summ}, 0) }}
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>
  import { required } from 'vuelidate/lib/validators'
  import { mdbBtn, mdbInput} from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn, mdbInput
    },
    data(){
      return{
        debit: true,
        debit_show: false,
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
        debit_new_list: [],
        debit_list_patient_id: [],
        pay_debit_show: false,
        debit_pay_cash: 0,
        debit_pay_card: 0,
        dolg_info_patient: {},
      }
    },
    validations: {
      price: {required},
      contragent_name : {required}
    },
    async mounted(){
      await this.newRefresh();
        if(localStorage.AuthId === 0){
          this.showen_Auth = true
          console.log(localStorage.Auth)
        } 
    },
    computed: {
      ...mapGetters(['get_contragent_list', 'get_report_by_data_time', 
        'get_report_by_time_card_cash','get_pagination']),
      filteredList: function () {
      if (this.search) {
        return this.debit_new_list.filter((item) => {
          return this.search.toLowerCase().split(' ').every(v => item.patient_name.toLowerCase().includes(v))
        })
      } else {
        return this.debit_new_list
      }
    }
    },
    methods: {
      ...mapActions(['fetch_contragent', 'fetch_report_by_data_time']),
      ...mapMutations(['district_row_delete', 'update_pagination_first']),
      async newRefresh(){
        try{
          const res = await fetch(this.$store.state.hostname + '/HospitalPatientDolgPaymentInfoes/getPaginationByAuthId?page=0&size=100&auth_id=' + localStorage.AuthId);
          const res_data = await res.json();
          console.log('res_data');
          console.log(res_data.items_list)
          this.debit_new_list = res_data.items_list;
        }
        catch{
          console.log('server error')
        }
      },    
      async refresh(){
        const res = await fetch(this.$store.state.hostname + '/HospitalPatientDolg/getPagination?page=' + this.get_pagination.page + '&size=' + this.get_pagination.size);
        const res_data = await res.json();
        console.log('res_data')
        console.log(res_data)
        this.update_pagination_first({current_item_count: res_data.current_item_count, current_page: res_data.current_page+1, items_count: res_data.items_count});
        this.m_contragent.rows = res_data.items_list;
        this.loading = false;
      },
      async submit(){
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + '/HospitalPatientDolg/getPaginationByPatientFio?page=0&size=100&patient_name=' + this.search)
            const data = await response.json()
            this.loading = false;
            console.log(data)
            if(data.items_count){
              this.m_contragent.rows = data.items_list;
              this.$refs.message.success('Added_successfully')
            }
            else{
              this.$refs.message.error('not_found_patient')
              this.m_contragent.rows = [];
              // this.modal_info = data.detail + "    (" + data.routine + ")";
              // this.modal_status = true;
              return false;
            } 
          }
          catch{
            this.$refs.message.error('not_found_patient')
              this.m_contragent.rows = [];
          }
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
  .myTabledebit {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; overflow-x:auto
}
.myTabledebit th{
  font-weight: 600;
  font-size:12px;
}
.myTabledebit td{
  font-size:13px;
}
.myTabledebit th, .myTabledebit td {
  text-align: left;
  padding: 3px 5px;
}

.myTabledebit tr {
  border-bottom: 1px solid rgb(240, 240, 240);
}

.myTabledebit tr.header, .myTabledebit tr:hover {
  background-color: #f1f1f1;
}
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
</style>
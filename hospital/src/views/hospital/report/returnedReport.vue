<template>
  <div class="bg-white">
    <div class="p-4">
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
        <div class="border-bottom">
          <h5 class="mx-4 pb-3 ">Возвырат деньги</h5>
        </div>
        <form @submit.prevent="submit">
          <div style="height: 100px;" class="d-flex justify-content-between border-bottom align-items-center  ">
            <div class="title w-100 row align-items-center">
              <div class="col-3">
                <div style="position: relative; margin-top: 40px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -14px; color: #757575;">
                    {{$t('start_time')}}
                  </small>
                  <mdb-input type="date" size="sm" v-model="Start_time" outline/>
                </div>
              </div>
              <div class="col-3">
                <div style="position: relative; margin-top: 40px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -14px; color: #757575;">
                    {{$t('end_time')}}
                  </small>
                  <mdb-input type="date" size="sm"  v-model="End_time" outline/>
                </div>
              </div>
              <!-- <div class="col-3">
                <RegSelect
                  class="mt-3"
                  label="Регион"
                  @select="users_func"
                  :options="get_auth_user_limit"
                  :selected="user_name"
                />
                <small v-if="$v.district_name.$dirty && district_id == null" class="text-danger p-0 m-0 pl-1" >
                  {{$t('select_item')}}
                </small>
              </div> -->
              <div class="col-6 pt-2 text-right">
                <mdb-btn @click="print" color="info py-2 px-4"  style="font-size:10px;" >
                  {{$t('print')}}
                </mdb-btn>
                <mdb-btn type="submit" color="primary py-2 px-4" style="font-size:10px;">
                  {{$t('search')}}
                </mdb-btn>
              </div>
              
            </div>
            
          </div>
        </form>
       <div class="TablePatientDocId p-3">
          <table class="myTable">
            <thead>
              <tr class="header ">
                <th width="50">№</th>
                <th >{{$t('patient_name')}}</th>
                <th width="250">{{$t('contragent')}}</th>
                <th >{{$t('serivce_type')}}</th>
                <th>{{$t('service_group')}}</th>
                <th >{{$t('date')}}</th>
                <th >Опл. дата</th>
                <th>{{$t('price')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in bonus_contragent_list" :key="rowIndex">
                <td> <span >{{rowIndex+1}}</span> </td>
                <td> <span >{{row.patientName}}</span> </td>
                <td> <span >{{row.contragentName}}</span> </td>
                <td> <span >{{row.serivceTypeName}}</span> </td>
                <td> <span >{{row.serviceGroupName}}</span> </td>
                <td> <span >{{row.dateTime.slice(0,10) + ' | ' + row.dateTime.slice(11,16)}}</span> </td>
                <td> <span >{{row.payedDate.slice(0,10)}}</span> </td>
                <td> <span >{{row.summa}}</span> </td>
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
        <div class="TablePatientDocId p-3">
          <table class="myTable">
            <thead>
              <tr class="header ">
                <th width="50">№</th>
                <th >{{$t('patient_name')}}</th>
                <th width="250">{{$t('contragent')}}</th>
                <th >{{$t('serivce_type')}}</th>
                <th>{{$t('service_group')}}</th>
                <th >{{$t('date')}}</th>
                <th>{{$t('price')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in bonus_contragent_list" :key="rowIndex">
                <td> <span >{{rowIndex+1}}</span> </td>
                <td> <span >{{row.patientName}}</span> </td>
                <td> <span >{{row.contragentName}}</span> </td>
                <td> <span >{{row.serivceTypeName}}</span> </td>
                <td> <span >{{row.serviceGroupName}}</span> </td>
                <td> <span >{{row.dateTime.slice(0,10) + ' | ' + row.dateTime.slice(11,16)}}</span> </td>
                <td> <span >{{row.summa}}</span> </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>

    </vue-html2pdf>

    <!-- <div :class="{'showing':show}">
      <div class="add d-flex justify-content-center align-items-center" >  
        <districtAdd/>
      </div>
    </div> -->
      <mdb-modal  :show="show"  @close="show = false"  light>
        <mdb-modal-header>
          <mdb-modal-title style="font-weight:  500;">{{$t('Добавить Район')}}</mdb-modal-title>
        </mdb-modal-header>
        <mdb-modal-body>
          <districtAdd :options="editData"/>
        </mdb-modal-body>
      </mdb-modal>
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>
  import VueHtml2pdf from 'vue-html2pdf'
  // import RegSelect from '../../../components/hospital/UserlineSelect.vue'
  // import DatePicker from 'vue2-datepicker';
  import districtAdd from "../../../components/new_prog_add/district_add"
  // import lineSelect from "../../../components/lineSelect.vue";
  import {  mdbInput, mdbBtn,  mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody,   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody,
      districtAdd, mdbInput, mdbBtn,
      VueHtml2pdf
    },
    data(){
      return{
        show: false,
        snipper: true,
        editData: {},
        modal_info : '',
        modal_status: false,
        contragent_name: '',
        contragent_id: 0,
        Start_time: null,
        End_time: null,
        user_name: '',
        user_id: 0,
        bonus_contragent_list: []
      }
    },
    async mounted(){
      console.log('date')
      this.Start_time = new Date().toISOString().slice(0, 10);
      this.End_time = new Date().toISOString().slice(0, 10);
      await this.submit();
    },
    computed: mapGetters(['get_contragent_list', 'get_auth_user_limit', 'get_report_by_data_contragent', 'get_report_qty_summ', 'get_report_by_cont_card_cash']),
    methods: {
      ...mapActions(['fetch_contragent','fetch_auth_list', 'fetch_report_by_data_cont',]),
      ...mapMutations(['district_row_delete']),

      async selectcontragent(option){
        this.contragent_id = option.data.id
        this.contragent_name = option.data.name
      },
      async get_get_(){ 
        const response = await fetch(this.$store.state.hostname + '/ReturnMoneys')
        const data = await response.json()
        this.bonus_contragent_list = data
        console.log(data)
      },
      add(){
        this.show =! this.show
        this.editData = {};
      },
      print(){
        this.$refs.listlar.generatePdf()
      },
      users_func(options){
        console.log(options)
        this.user_name = options.data.fio
        this.user_id = options.data.Authid
      },
      async submit(){
        var time1 = this.Start_time + 'T00:00:35.000Z'
        var time2 = this.End_time + 'T23:59:35.000Z'
        const response = await fetch(this.$store.state.hostname + '/VozvratAlreadyPaidPaymentLists/getVozvratByDateTimeAsList?beginDate=' + time1 + 
        '&endDate=' + time2 )
        const data = await response.json();
        this.bonus_contragent_list = data;
        console.log('test uchun');
        console.log(data);
      },
    }
  }
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
.TablePatientDocId{
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
</style>
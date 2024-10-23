<template>
    <div class="bg-white">
      <loader v-if="loading"/>
      <div class="px-2">
        <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
          <form @submit.prevent="EnterIdAnalis">
            <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
              <div class="title w-75 row align-items-center">
                <div class="col-4">
                  <div style="position: relative; margin-top: 40px;"> 
                    <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -14px; color: #757575;">
                      {{$t('start_time')}}
                    </small>
                    <mdb-input type="date" size="sm" v-model="Start_time" outline/>
                  </div>
                </div>
                <div class="col-4">
                  <div style="position: relative; margin-top: 40px;"> 
                    <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -14px; color: #757575;">
                      {{$t('end_time')}}
                    </small>
                    <mdb-input type="date" size="sm" v-model="End_time" outline/>
                  </div>
                </div>
                <div class="col-4">
                  <div style="position: relative; margin-top: 40px;"> 
                    <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -14px; color: #757575;">
                        Ид анализа
                    </small>
                    <mdb-input type="text" @keyup.enter="EnterIdAnalis" ref="analysis_id" size="sm" v-model="analiz_id" outline/>
                  </div>
                </div>
                
              </div>
              <div class="plus d-flex">
                <mdb-btn @click="print" color="info py-2 px-4" style="font-size:10px;"  >
                  {{$t('print')}}
                </mdb-btn>
                <mdb-btn @click="submit" color="primary py-2 px-4" style="font-size:10px;"  >
                  {{$t('apply')}}
                </mdb-btn>
                <mdb-btn  color="warning" class=" p-0" @click="$router.push('/laboratory_list/0')" style="font-size: 9.8px;"  p="r3 l3 t2 b2">Результат</mdb-btn>
              </div>
            </div>
          </form>
          <div class="TablePatientDocId pt-2">
            <table class="myTableInfoLab">
              <thead>
                <tr class="header ">
                  <th  width="55" class="text-left">№</th> 
                  <th width="150">{{$t('id')}}</th>
                  <th  width="180px">{{$t('fio')}}</th>
                  <th  >{{$t('service_name')}}</th>
                </tr>
              </thead>
              <tbody v-if="one_item">
                <tr v-for="(row,rowIndex) in FullInfoNumberLab" :key="rowIndex"  :class="{'bg-success text-white' : row.finish_all_chekings}">
                  <td style="font-size:13px;"> <span >{{rowIndex+1}}</span></td>
                  <td  style="font-size:13px; "> {{row.id}}</td>
                  <td  style="font-size:13px;"> <span >{{row.note}}</span></td>
                  <td class="py-2 " style="font-size:13px;">
                    <div class="row">
                      <div class="col-3 m-0 mb-1 mt-1 border-bottom border-right border-left" v-for="item in row.serviece_types_fake" :key="item.id"> <span  :class="{'text-white' : row.finish_all_chekings, 'text-primary' : !row.finish_all_chekings}">{{item.name}}</span></div>
                    </div>  </td>
                </tr>
              </tbody>
              <tbody v-else>
                <tr v-for="(row,rowIndex) in FullInfoNumberLab" :key="rowIndex"  :class="{'bg-success text-white' : row.finish_all_chekings}">
                  <td style="font-size:13px;"> <span >{{rowIndex+1}}</span></td>
                  <td  style="font-size:13px; "> {{row.id}}</td>
                  <td  style="font-size:13px;"> <span >{{row.note}}</span></td>
                  <td class="py-2 " style="font-size:13px;" >
                    <div class="row">
                      <div class="col-3 m-0 mb-1 mt-1 border-bottom border-right border-left" v-for="item in row.serviece_types_fake" :key="item.id"> <span  :class="{'text-white' : row.finish_all_chekings, 'text-primary' : !row.finish_all_chekings}">{{item.Name}}</span></div>
                    </div>  </td>
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
            <table class="myTableInfoLab">
              <thead>
                <tr class="header ">
                  <th  width="55" class="text-left">№</th> 
                  <th width="120">{{$t('id')}}</th>
                  <th  width="180px">{{$t('fio')}}</th>
                  <th  >{{$t('service_name')}}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,rowIndex) in FullInfoNumberLab" :key="rowIndex"  :class="{'bg-success text-white' : row.finish_all_chekings}">
                  <td style="font-size:12px;"> <span >{{rowIndex+1}}</span></td>
                  <td  style="font-size:12px; "> {{row.id}}</td>
                  <td  style="font-size:12px;"> <span >{{row.note}}</span></td>
                  <td class="pb-3 " style="font-size:12px;" >
                    <div class="row">
                      <div class="col-4 pb-4 mt-1 border-bottom border-right border-left" v-for="item in row.serviece_types_fake" :key="item.id"> 
                        <span  :class="{'text-white' : row.finish_all_chekings, 'text-primary' : !row.finish_all_chekings}">{{item.Name}}</span>
                      </div>
                    </div>  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
  
      </vue-html2pdf>
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
    import VueHtml2pdf from 'vue-html2pdf'
    // import DatePicker from 'vue2-datepicker';
    // import districtAdd from "../../../components/new_prog_add/district_add"
    import { mdbBtn, mdbIcon, mdbInput,  mdbModal, mdbModalHeader,  mdbModalBody, mdbModalFooter  } from 'mdbvue';
    import {mapActions, mapGetters, mapMutations} from 'vuex'
    // import 'vue2-datepicker/index.css';
    export default {
      components: {
        mdbBtn,
        mdbModal, mdbModalHeader, mdbModalBody, mdbInput,
        mdbModalFooter, mdbIcon,
        VueHtml2pdf
      },
      data(){
        return{
          show: false,
          loading: false,
          snipper: true,
          editData: {},
          modal_info : '',
          modal_status: false,
          contragent_name: '',
          contragent_id: 0,
          Start_time: null,
          End_time: null,
          admin: false,
          group_name: '',
          group_id: null,
          FullInfoNumberLab: [],
          summa: 0,
          index: null,
          service_id: null,
          analiz_id: null,
          one_item: false,
        }
      },
      async mounted(){
        var tempTime  = new Date();
        console.log(tempTime.toISOString().slice(0,10))
        this.Start_time = tempTime.toISOString().slice(0,10);
        this.End_time = tempTime.toISOString().slice(0,10); 
        await this.submit();
        this.$refs.analysis_id.focus();
      },
      computed: mapGetters(['get_contragent_list', 'get_report_by_data', 'get_report_by_time_card_cash', 'get_service_group_list']),
      methods: {
        ...mapActions(['fetch_contragent', 'fetch_report_by_data', 'fetch_service_group']),
        ...mapMutations(['dibet_delite_cont']),
  
        add(){
          this.show =! this.show
          this.editData = {};
        },
        print(){
          this.$refs.listlar.generatePdf()
        },
         
        async promiseService(){
          const requestOptions = {
              method : "delete",
            };
            this.loading =true;
            const response = await fetch(this.$store.state.hostname + "/HospitalContragentDebitPaymentReports/" + this.service_id, requestOptions);
            const data = await response.json();
            this.loading = false;
  
            if(data)
            {
              // this.alert_text = localizeFilter('Successfully_removed');
              this.$refs.message.error('Successfully_removed')
              this.show = false;
              this.FullInfoNumberLab.splice(this.index, 1);
            }
            else{
              this.modal_info = data.detail + "    (" + data.routine + ")";
              this.modal_status = true;
            }
        },
        deleteRow(index, data){
          console.log(data)
          this.index = index
          this.service_id = data.id
          this.show = true;
        },
  
        async submit(){
            this.one_item = false;
          let a = {
            time1: "",
            time2: '',
          }
          a.time1 = this.Start_time + 'T00:00:00.000Z';
          a.time2 = this.End_time + 'T23:59:59.000Z';
          this.loading = true;
          console.log(a.time1)
          try{
            const response = await fetch(this.$store.state.hostname + "/HospitalMarkSeviceTyesWithNumber/getPaginationWithBeginEndTime?page=0&size=1000&begin_date=" + a.time1 + '&end_date=' +  a.time2);
            const data = await response.json();
            console.log(data)
            this.FullInfoNumberLab = data.items_list;
          }
          catch{
            this.$refs.message.error('not_found')
          }
  
          this.loading = false;
          
        },
        async EnterIdAnalis(){
            this.one_item = true;
            try{
                const response = await fetch(this.$store.state.hostname + "/HospitalMarkSeviceTyesWithNumber/getFullInfoById?id=" + this.analiz_id);
                const data = await response.json();
                console.log(data)
                let temp = [];
                temp.push(data)
                this.FullInfoNumberLab = temp;
            }
            catch{
                this.$refs.message.error('not_found')
            }
        }
  
      },
    };
  </script>
  
  <style scoped>
  @import "../../../scss/tableAll.scss";
  
  </style>
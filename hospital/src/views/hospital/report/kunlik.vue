<template>
  <div class="bg-white">
    <loader v-if="loading"/>
    <div class="p-4">
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
        <form @submit.prevent="submit">
          <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
            <div class="title w-75 row align-items-center">
              <div v-if="!admin" class="col-5">
                <div style="position: relative; margin-top: 40px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -8px; color: #757575;">
                    {{$t('start_time')}}
                  </small>
                  <mdb-input type="date"  v-model="Start_time" outline/>
                </div>
              </div>
              <div class="col-5" v-if="!admin">
                <lineSelect
                  class="mt-3 "
                  :options="kassirListForAdmin" 
                  :searchshow="true"
                  @select="selectPatient"
                  :selected="kassir_name"
                  :label="$t('user')"
              />
              </div>
            </div>
            <div class="plus">
              <mdb-btn @click="print" color="info py-2 px-4" style="font-size:10px;"  >
                {{$t('print')}}
              </mdb-btn>
              <mdb-btn type="submit" v-if="!admin"  color="primary py-2 px-4"  style="font-size:10px;" >
                {{$t('apply')}}
              </mdb-btn>
            </div>
          </div>
        </form>
        <div class="TablePatientDocId p-3">
          <table class="myTable">
            <thead>
              <tr class="header ">
                <th >{{$t('service_group')}}</th>
                <th >{{$t('user')}}</th>
                <th >{{$t('date')}}</th>
                <th >{{$t('cash')}}</th>
                <th >{{$t('card')}}</th>
                <th>{{$t('qty')}}</th>
                <td> <span class="text-primary">Общий</span> </td>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in kunlik_report_list" :key="rowIndex"  
              :class="{ 'text-danger': row.hospitalServiceTypeGroupName == 'RETURNED',
               'text-warning': row.hospitalServiceTypeGroupName == 'REJECTED',
               'text-secondary': row.hospitalServiceTypeGroupName == 'VOZVRAT' }">
                <td> 
                  <span v-if = "row.hospitalServiceTypeGroupName == 'RETURNED'" >РACXOДЫ</span> 
                  <span v-else-if = "row.hospitalServiceTypeGroupName == 'REJECTED'" >ОТКАЗ</span> 
                  <span v-else-if = "row.hospitalServiceTypeGroupName == 'VOZVRAT'" >Возвырат</span> 
                  <span v-else >{{row.hospitalServiceTypeGroupName}}</span> 
                </td>
                <td> <span >{{row.authorization.users.fio}}</span> </td>
                <td> <span >{{row.createdDateTime.slice(0,10)}}</span> </td>
                <td> <span >{{row.cashSumm}}</span> </td>
                <td> <span >{{row.cardSumm}}</span> </td>
                <td> <span >{{row.count}}</span> </td>
                <td> <span >{{row.cashSumm + row.cardSumm}}</span> </td>

               
              </tr>
              <tr >
                <td> <span class="text-success">Общий</span> </td>
                <td> <span ></span></td>
                <td> <span ></span></td>
                <td> <span  class="text-success">{{cash}}</span></td>
                <td> <span  class="text-success">{{card}}</span></td>
                <td> <span  class="text-success">{{qtys}}</span></td>
                <td> <span  class="text-success">{{cash + card}}</span></td>
              </tr>
              <tr >
                <td> <span class="text-success"></span>{{$t('cash')}} </td>
                <td> <span  class="text-primary">{{cash}} сум </span></td>
              </tr>
               <tr >
                <td> <span class="text-success"></span>{{$t('card')}} </td>
                <td> <span  class="text-primary">{{card}} сум </span></td>
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
                <th >{{$t('service_group')}}</th>
                <th >{{$t('patient_name')}}</th>
                <th >{{$t('date')}}</th>
                <th >{{$t('cash')}}</th>
                <th >{{$t('card')}}</th>
                <th>{{$t('qty')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in kunlik_report_list" :key="rowIndex">
                <td> 
                  <span v-if = "row.hospitalServiceTypeGroupName == 'RETURNED'" >РACXOДЫ</span> 
                  <span v-else-if = "row.hospitalServiceTypeGroupName == 'REJECTED'" >ОТКАЗ</span> 
                  <span v-else-if = "row.hospitalServiceTypeGroupName == 'VOZVRAT'" >Возвырат</span> 
                  <span v-else >{{row.hospitalServiceTypeGroupName}}</span> 
                </td>
                <td> <span >{{row.authorization.users.fio}}</span> </td>
                <td> <span >{{row.createdDateTime.slice(0,10)}}</span> </td>
                <td> <span >{{row.cashSumm}}</span> </td>
                <td> <span >{{row.cardSumm}}</span> </td>
                <td> <span >{{row.count}}</span> </td>
               
              </tr>
              <tr >
                <td> <span class="text-primary">Общий</span> </td>
                <td> <span ></span> </td>
                
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
  // import DatePicker from 'vue2-datepicker';
  import districtAdd from "../../../components/new_prog_add/district_add"
  import lineSelect from "../../../components/hospital/cashUserSelect.vue";
  import { mdbBtn, mdbInput,  mdbModal, mdbModalHeader,  mdbModalTitle, mdbModalBody,   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, mdbInput,
      districtAdd,
      VueHtml2pdf, lineSelect
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
        admin: true,
        kunlik_report_list: [],
        loading: false,
        cash: 0,
        card: 0,
        qtys: 0,
        allmoney: 0,
        kassirListForAdmin: [],
        kassir_name: '',
        kassir_id: null,
        kassir_athId: null,
      }
    },
    async mounted(){
        await this.fetch_auth_list();
        this.kassirListForAdmin = []
        console.log(this.auth_user_list)
        for(let l = 0; l < this.auth_user_list.length; l++) {
          if(this.auth_user_list[l].userType == 2 || this.auth_user_list[l].userType == 5){
            this.auth_user_list[l].users.authId = this.auth_user_list[l].id;
            this.kassirListForAdmin.push(this.auth_user_list[l].users)
          }
        }
        console.log(this.kassirListForAdmin)
       if(localStorage.Type == 0){
          this.admin = false;
          var f = new Date();
          var l = f.toISOString().slice(0,10);
          console.log(l)
          this.loading =true;
          const response = await fetch(this.$store.state.hostname + "/HospitalManagerReports/getHospitalManagerListByDateForAdmin?dateTimeCur=" + l);
          const data = await response.json();
          console.log(data)
          this.loading = false;
          this.cash = 0;
          this.card = 0;
          this.qtys = 0;
          for(let i=0; i<data.length; i++){
            if(data[i].hospitalServiceTypeGroupName != 'VOZVRAT' && data[i].hospitalServiceTypeGroupName != 'REJECTED'){
              if(data[i].hospitalServiceTypeGroupName == 'RETURNED'){
                this.cash -= data[i].cashSumm
                this.card -= data[i].cardSumm
                // this.qtys -= data[i].count
              }
              else{
                this.cash += data[i].cashSumm
                this.card += data[i].cardSumm
                this.qtys += data[i].count
              }
            }
          }
          this.kunlik_report_list = data;

          // for(let i=0; i<this.kunlik_report_list.length; i++) {
          //   if(this.kunlik_report_list[i].)
          // }
        }
        else{
        var a = new Date();
        var b = a.toISOString().slice(0,10);
        console.log(b)
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/HospitalManagerReports/getHospitalManagerListByAuthIdAndDateForKassir?AuthId=" +
         localStorage.AuthId + "&dateTimeCur=" + b);
          this.loading = false;
        const data = await response.json();
        console.log(data)
        this.cash = 0;
          this.card = 0;
          this.qtys = 0;
        for(let i=0; i<data.length; i++){
            if(data[i].hospitalServiceTypeGroupName != 'VOZVRAT' && data[i].hospitalServiceTypeGroupName != 'REJECTED'){
              if(data[i].hospitalServiceTypeGroupName == 'RETURNED'){
                this.cash -= data[i].cashSumm
                this.card -= data[i].cardSumm
                // this.qtys -= data[i].count
              }
              else{
                this.cash += data[i].cashSumm
                this.card += data[i].cardSumm
                this.qtys += data[i].count
              }
            }
          }
        this.kunlik_report_list = data;

      }



        // await this.fetch_report_by_data(a)
        await this.fetch_contragent()
        // console.log(this.get_report_by_data)
    },
    computed: mapGetters(['get_contragent_list', 'get_report_by_data', 'get_report_by_time_card_cash', 'auth_user_list']),
    methods: {
      ...mapActions(['fetch_contragent', 'fetch_report_by_data', 'fetch_auth_list']),
      ...mapMutations(['dibet_delite_cont']),
      async selectPatient(option){
        console.log(option);
        this.kassir_name = option.data.fio;
        this.kassir_id = option.data.id;
        this.kassir_athId = option.data.authId;
        console.log(this.kassir_id)
        console.log(this.kassir_name)
        console.log(this.kassir_athId)
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/HospitalManagerReports/getHospitalManagerListByAuthIdAndDateForKassir?AuthId=" +
         option.data.authId + "&dateTimeCur=" + this.Start_time);
          this.loading = false;
        const data = await response.json();
        console.log(data)
        this.cash = 0;
          this.card = 0;
          this.qtys = 0;
        for(let i=0; i<data.length; i++){
            if(data[i].hospitalServiceTypeGroupName != 'VOZVRAT' && data[i].hospitalServiceTypeGroupName != 'REJECTED'){
              if(data[i].hospitalServiceTypeGroupName == 'RETURNED'){
                this.cash -= data[i].cashSumm
                this.card -= data[i].cardSumm
                // this.qtys -= data[i].count
              }
              else{
                this.cash += data[i].cashSumm
                this.card += data[i].cardSumm
                this.qtys += data[i].count
              }
            }
          }
        this.kunlik_report_list = data;
      },
      add(){
        this.show =! this.show
        this.editData = {};
      },
      print(){
        this.$refs.listlar.generatePdf()
      },
      async deleteRow(i, del_data){
        const requestOptions = {
            method : "delete",
          };
          this.loading =true;
          const response = await fetch(this.$store.state.hostname + "/HospitalContragentDebitPaymentReports/" + del_data.id, requestOptions);
          const data = await response.json();
          this.loading = false;

          if(data)
          {
            // this.alert_text = localizeFilter('Successfully_removed');
            this.$refs.message.error('Successfully_removed')
            this.dibet_delite_cont(i);
          }
          else{
            this.modal_info = data.detail + "    (" + data.routine + ")";
            this.modal_status = true;
          }
      },
      async submit(){
        console.log(this.Start_time)
          this.loading =true;
        const response = await fetch(this.$store.state.hostname + "/HospitalManagerReports/getHospitalManagerListByDateForAdmin?dateTimeCur=" + this.Start_time);
        const data = await response.json();
          this.loading =false;
          this.cash = 0;
          this.card = 0;
          this.qtys = 0;
          for(let i=0; i<data.length; i++){
            if(data[i].hospitalServiceTypeGroupName != 'VOZVRAT' && data[i].hospitalServiceTypeGroupName != 'REJECTED'){
              if(data[i].hospitalServiceTypeGroupName == 'RETURNED'){
                this.cash -= data[i].cashSumm
                this.card -= data[i].cardSumm
                // this.qtys -= data[i].count
              }
              else{
                this.cash += data[i].cashSumm
                this.card += data[i].cardSumm
                this.qtys += data[i].count
              }
            }
          }
        this.kunlik_report_list = data;

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
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
</style>
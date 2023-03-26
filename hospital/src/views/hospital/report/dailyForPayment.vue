<template>
  <div class="bg-white">
    <loader v-if="loading"/>
    <div class="">
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
        <div class="border-bottom">
          <h5 class="mx-1 pb-0 ">Дневники касса</h5>
        </div>
        <form @submit.prevent="submit">
          <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center ">
            <div class="title w-75 row align-items-center">
              <div  class="col-4">
                <div style="position: relative; margin-top: 30px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -11px; color: #757575;">
                    {{$t('start_time')}}
                  </small>
                  <mdb-input type="date" size="sm" v-model="Start_time" outline/>
                </div>
              </div>
              <div  class="col-4">
                <div style="position: relative; margin-top: 30px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -11px; color: #757575;">
                    {{$t('end_time')}}
                  </small>
                  <mdb-input type="date" size="sm" v-model="End_time" outline/>
                </div>
              </div>
            </div>
            <div class="plus">
              <mdb-btn @click="print" color="info py-2 px-3"  style="font-size:10px;" >
                <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-printer mr-0" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                  <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                  <path d="M17 17h2a2 2 0 0 0 2 -2v-4a2 2 0 0 0 -2 -2h-14a2 2 0 0 0 -2 2v4a2 2 0 0 0 2 2h2" />
                  <path d="M17 9v-4a2 2 0 0 0 -2 -2h-6a2 2 0 0 0 -2 2v4" />
                  <rect x="7" y="13" width="10" height="8" rx="2" />
                </svg>
                {{$t('print')}}
              </mdb-btn>
              <mdb-btn type="submit" color="primary py-2 px-3" style="font-size:10px;"  >
                <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-check" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                  <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                  <path d="M5 12l5 5l10 -10" />
                </svg>
                {{$t('apply')}}
              </mdb-btn>
            </div>
          </div>
        </form>
        <div class="all_price border-bottom pb-2"> 
          <div class="row mt-2">
            <div class="col-3 pr-0">
              <div class="price_all_item card">
                <div class="qty borderSolder py-2">
                    <span class="ml-3">{{$t('cash')}}</span>
                    <div class="text-right px-3 mt-1">
                      <p>{{cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</p>
                    </div>
                  </div>
              </div>
            </div>
            <div class="col-3 pr-0">
              <div class="price_all_item card">
                <div class="qty borderSolder py-2">
                    <span class="ml-3">{{$t('card')}}</span>
                    <div class="text-right px-3 mt-1">
                      <p>{{card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</p>
                    </div>
                  </div>
              </div>
            </div>
            <div class="col-3 pr-0">
              <div class="price_all_item card">
                <div class="qty borderSolder py-2">
                    <span class="ml-3">РACXOДЫ </span>
                    <div class="text-right px-3 mt-1">
                      <p>{{get_money.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</p>
                    </div>
                  </div>
              </div>
            </div>
            <div class="col-3 ">
              <div class="price_all_item card">
                <div class="qty borderSolder py-2">
                    <span class="ml-3">В кассе есть деньги</span>
                    <div class="text-right px-3 mt-1">
                      <p>{{(cash - get_money).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</p>
                    </div>
                  </div>
              </div>
            </div>
          </div>
        </div>
        <div class="TablePatientDocIdset mt-1">
          <table class="myTable">
            <thead class="bg_header">
              <tr class="header text-white ">
                <th >{{$t('service_group')}}</th>
                <!-- <th >{{$t('user')}}</th> -->
                <th >{{$t('date')}}</th>
                <th >{{$t('cash')}}</th>
                <th >{{$t('card')}}</th>
                <th>{{$t('qty')}}</th>
                <td> <span class="text-white">Общий</span> </td>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in get_daily_report_list" :key="rowIndex" >
                <td> <span >{{row.service_group}}</span> </td>
                <td> <span >{{row.date}}</span> </td>
                <td> <span >{{row.cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{row.card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{row.qty}}</span> </td>
                <td> <span >{{(row.cash + row.card).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
              </tr>
              
            </tbody>
            <tbody>
              <tr v-for="(item,itemIndex) in kunlik_report_list" :key="itemIndex"   
              :class="{ 'text-danger': item.hospitalServiceTypeGroupName == 'RETURNED',
               'text-warning': item.hospitalServiceTypeGroupName == 'REJECTED',
               'text-secondary': item.hospitalServiceTypeGroupName == 'VOZVRAT' }"
               v-show="item.hospitalServiceTypeGroupName == 'RETURNED' || item.hospitalServiceTypeGroupName == 'REJECTED'
                || item.hospitalServiceTypeGroupName == 'VOZVRAT'">
                <td>
                  <span v-if = "item.hospitalServiceTypeGroupName == 'RETURNED'" >РACXOДЫ</span> 
                  <span v-else-if = "item.hospitalServiceTypeGroupName == 'REJECTED'" >ОТКАЗ</span> 
                  <span v-else-if = "item.hospitalServiceTypeGroupName == 'VOZVRAT'" >Возвырат</span> 
                  <span v-else >{{item.hospitalServiceTypeGroupName}}</span> 
                </td>
                <!-- <td> <span >{{item.authorization.users.fio}}</span> </td> -->
                <td> <span >{{item.createdDateTime.slice(0,10)}}</span> </td>
                <td> <span >{{item.cashSumm.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{item.cardSumm.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{item.count}}</span> </td>
                <td> <span >{{(item.cashSumm + item.cardSumm).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
              </tr>
              <tr >
                <td> <span class="text-success">Общий</span> </td>
                <td> <span ></span></td>
                <!-- <td> <span ></span></td> -->
                <td> <span  class="text-success">{{(cash - get_money).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span></td>
                <td> <span  class="text-success">{{card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span></td>
                <td> <span  class="text-success">{{qtys}}</span></td>
                <td> <span  class="text-success">{{(cash + card - get_money).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span></td>
              </tr>
              <!-- <tr >
                <td> <span class="text-success"></span>{{$t('cash')}} </td>
                <td> <span  class="text-primary">{{cash}} сум </span></td>
              </tr>
               <tr >
                <td> <span class="text-success"></span>{{$t('card')}} </td>
                <td> <span  class="text-primary">{{card}} сум </span></td>
              </tr>
              <tr >
                <td> <span class="text-success"></span>РACXOДЫ </td>
                <td> <span  class="text-primary">{{get_money}} сум </span></td>
              </tr>
              <tr >
                <td> <span class="text-success"></span>В кассе есть деньги </td>
                <td> <span  class="text-primary">{{cash - get_money}} сум </span></td>
              </tr> -->
            </tbody>
          </table>
        </div>

        <div class="TablePatientDocIdset mt-1">
          <table class="myTable">
            <thead class="bg_header">
              <tr class="header text-white ">
                <th >{{$t('service_group')}}</th>
                <!-- <th >{{$t('user')}}</th> -->
                <th >{{$t('date')}}</th>
                <th >{{$t('cash')}}</th>
                <th >{{$t('card')}}</th>
                <th>{{$t('qty')}}</th>
                <td> <span class="text-white">Общий</span> </td>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td> <span >Стационар</span> </td>
                <td> <span >{{this.Start_time}}</span> </td>
                <td> <span >{{get_bron_cash_card.cash.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{get_bron_cash_card.card.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{get_bron_cash_card.qty}}</span> </td>
                <td> <span >{{(get_bron_cash_card.cash + get_bron_cash_card.card).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
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
         <div class="TablePatientDocIdset p-3">
          <table class="myTable">
            <thead>
              <tr class="header ">
                <th >{{$t('service_group')}}</th>
                <!-- <th >{{$t('user')}}</th> -->
                <th >{{$t('date')}}</th>
                <th >{{$t('cash')}}</th>
                <th >{{$t('card')}}</th>
                <th>{{$t('qty')}}</th>
                <td> <span class="text-primary">Общий</span> </td>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in get_daily_report_list" :key="rowIndex" >
                <td> <span >{{row.service_group}}</span> </td>
                <td> <span >{{row.date}}</span> </td>
                <td> <span >{{row.cash}}</span> </td>
                <td> <span >{{row.card}}</span> </td>
                <td> <span >{{row.qty}}</span> </td>
                <td> <span >{{row.cash + row.card}}</span> </td>
              </tr>
              
            </tbody>
            <tbody>
              <tr v-for="(item,itemIndex) in kunlik_report_list" :key="itemIndex"   
              :class="{ 'text-danger': item.hospitalServiceTypeGroupName == 'RETURNED',
               'text-warning': item.hospitalServiceTypeGroupName == 'REJECTED',
               'text-secondary': item.hospitalServiceTypeGroupName == 'VOZVRAT' }"
               v-show="item.hospitalServiceTypeGroupName == 'RETURNED' || item.hospitalServiceTypeGroupName == 'REJECTED'
                || item.hospitalServiceTypeGroupName == 'VOZVRAT'">
                <td> 
                  <span v-if = "item.hospitalServiceTypeGroupName == 'RETURNED'" >РACXOДЫ</span> 
                  <span v-else-if = "item.hospitalServiceTypeGroupName == 'REJECTED'" >ОТКАЗ</span> 
                  <span v-else-if = "item.hospitalServiceTypeGroupName == 'VOZVRAT'" >Возвырат</span> 
                  <span v-else >{{item.hospitalServiceTypeGroupName}}</span> 
                </td>
                <!-- <td> <span >{{item.authorization.users.fio}}</span> </td> -->
                <td> <span >{{item.createdDateTime.slice(0,10)}}</span> </td>
                <td> <span >{{item.cashSumm}}</span> </td>
                <td> <span >{{item.cardSumm}}</span> </td>
                <td> <span >{{item.count}}</span> </td>
                <td> <span >{{item.cashSumm + item.cardSumm}}</span> </td>
              </tr>
              <tr >
                <td> <span class="text-success">Общий</span> </td>
                <td> <span ></span></td>
                <!-- <td> <span ></span></td> -->
                <td> <span  class="text-success">{{cash - get_money}}</span></td>
                <td> <span  class="text-success">{{card}}</span></td>
                <td> <span  class="text-success">{{qtys}}</span></td>
                <td> <span  class="text-success">{{cash + card - get_money}}</span></td>
              </tr>
              <tr >
                <td> <span class="text-success"></span>{{$t('cash')}} </td>
                <td> <span  class="text-primary">{{cash}} сум </span></td>
              </tr>
               <tr >
                <td> <span class="text-success"></span>{{$t('card')}} </td>
                <td> <span  class="text-primary">{{card}} сум </span></td>
              </tr>
              <tr >
                <td> <span class="text-success"></span>РACXOДЫ </td>
                <td> <span  class="text-primary">{{get_money}} сум </span></td>
              </tr>
              <tr >
                <td> <span class="text-success"></span>В кассе есть деньги </td>
                <td> <span  class="text-primary">{{cash - get_money}} сум </span></td>
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
  import { mdbBtn, mdbInput,  mdbModal, mdbModalHeader,  mdbModalTitle, mdbModalBody,   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, mdbInput,
      districtAdd,
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
        admin: true,
        kunlik_report_list: [],
        loading: false,
        cash: 0,
        card: 0,
        qtys: 0,
        allmoney: 0,
        get_money: 0,

      }
    },
    async mounted(){
      if(localStorage.Type == 0){
        this.admin = false;
      }
      
      {
        let time1 = new Date();
        this.Start_time = time1.toISOString().slice(0,10); 
        this.End_time = time1.toISOString().slice(0,10);
        let a = this.Start_time + 'T00:00:35.000Z' ;
        let b = this.End_time + 'T23:59:59.000Z';
        let c = {
          time1: '',
          time2: '',
          contId: 0
        }
        c.time1 = a;
        c.time2 = b;
        this.loading = true;
        await this.fetch_report_by_data_time(c)
        await this.fetch_report_by_bron_payed(c)
        this.loading = false;
        console.log(this.get_daily_report_list)
        this.cash = 0;
        this.card = 0;
        this.qtys = 0;
        this.get_daily_report_list.forEach((item)=>{
          this.cash += item.cash
          this.card += item.card
          this.qtys += item.qty
        })
      }
      {
        var date = new Date();
          var l = date.toISOString().slice(0,10);
          console.log(l)
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/HospitalManagerReports/getHospitalManagerListByDateForAdmin?dateTimeCur=" + l);
          const dataList = await response.json();
          console.log(dataList)
          this.get_money = 0;
          dataList.forEach((item)=>{
            if(item.hospitalServiceTypeGroupName == 'RETURNED')
            this.get_money += item.cashSumm
          })
          this.kunlik_report_list = dataList;
          this.loading = false;
      }

       
        // console.log(this.get_report_by_data)
    },
    computed: mapGetters(['get_contragent_list', 'get_report_by_data', 'get_report_by_time_card_cash',
     'get_report_by_data_time','get_daily_report_list', 'get_bron_cash_card']),
    methods: {
      ...mapActions(['fetch_contragent', 'fetch_report_by_data', 'fetch_report_by_data_time', 'fetch_report_by_bron_payed']),
      ...mapMutations(['dibet_delite_cont']),

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
          this.loading = true;
          {

          let a = this.Start_time + 'T00:00:00.000Z' ;    
          let b = this.End_time + 'T23:59:59.000Z';
          let c = {
            time1: '',
            time2: '',
            contId: 0
          }
          c.time1 = a;
          c.time2 = b;
          this.loading = true;
          await this.fetch_report_by_data_time(c);
          await this.fetch_report_by_bron_payed(c);
          this.loading = false;
          console.log(this.get_daily_report_list)
          this.cash = 0;
          this.card = 0;
          this.qtys = 0;
          this.get_daily_report_list.forEach((item)=>{
            this.cash += item.cash
            this.card += item.card
            this.qtys += item.qty
        })
        }
        {
            var l = this.Start_time;
            console.log(l)
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/HospitalManagerReports/getHospitalManagerListByDateForAdmin?dateTimeCur=" + l);
            const dataList = await response.json();
            console.log(dataList)
            this.get_money = 0;
            dataList.forEach((item)=>{
              if(item.hospitalServiceTypeGroupName == 'RETURNED')
              this.get_money += item.cashSumm
            })
            this.kunlik_report_list = dataList;
            this.loading = false;
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
.TablePatientDocIdset{
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
.price_all_item .borderSolder{
    // border: 0.5px dashed #D0D3D8;
    background-image: linear-gradient( 65.9deg,  rgba(85,228,224,1) 5.5%, rgba(75,68,224,0.74) 54.2%, rgba(64,198,238,1) 55.2%, rgba(177,36,224,1) 98.4% );

    span{
      color:#000000;
      font-size: 21px;
      font-weight: 450;
    }
    p{
      color:#ffffff;
      font-weight:bold;
      font-size: 23px;
      margin:0;
      padding:0;
    }
  }
.bg_header{
  background-image: linear-gradient( 65.9deg,  rgba(85,228,224,1) 5.5%, rgba(75,68,224,0.74) 54.2%, rgba(64,198,238,1) 55.2%, rgba(177,36,224,1) 98.4% );
}
</style>
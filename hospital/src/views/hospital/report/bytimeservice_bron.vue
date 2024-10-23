<template>
  <div class="bg-white">
    <loader v-if="loading"/>
    <div class="py-2">
      <div class="pt-4 shadow" style="border-radius:5px; position:relative;">
        <form @submit.prevent="submit">
          <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
            <div class="title w-75 row align-items-center">
               <div class="col-4">
                <div style="position: relative; margin-top: 25px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -12px; color: #757575;">
                    {{$t('start_time')}}
                  </small>
                  <mdb-input type="date" size="sm" v-model="Start_time" outline/>
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
            <div class="plus">
              <!-- <mdb-btn @click="allService" color="indigo py-2 px-4"  style="font-size:10px;" >
                {{$t('all_service')}}
              </mdb-btn> -->
              <!-- <mdb-btn @click="print" color="info py-2 px-4"  style="font-size:10px;" >
                {{$t('print')}}
              </mdb-btn> -->
              <mdb-btn type="submit" color="primary py-2 px-4" style="font-size:10px;"  >
                {{$t('apply')}}
              </mdb-btn>
            </div>
          </div>
        </form>
        <div class="TablePatientDocId p-3">
          <table class="kunlik_obshey_hisobot">
            <thead>
              <tr class="header ">
                <th  width="40" class="text-left">№</th>
                <th width="200">Врачы</th>
                <th>Услуга кол-в</th>
                <th>Услуга опл.</th>
                <th >Операцион кол-в</th>
                <th >Операцион опл.</th>
                <th >Стационар кол-в</th>
                <th>Стационар опл.</th>
                <th>Стац. на еду</th>
                <th>Стац. осталь</th>
                
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in all_report_one_list_data" :key="rowIndex">
                <td> <span >{{rowIndex+1}}</span> </td>
                <td> <span >{{row.user_name}}</span> </td>
                <td> <span >{{row.service_qty}}</span></td>
                <td class="alert-danger font-weight-bold"> <span >{{row.service_price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{row.operatsion_qty}}</span> </td>
                <td class="alert-primary font-weight-bold"> <span >{{row.operatsion_price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{row.bron_qty}}</span> </td>
                <td class="alert-success font-weight-bold"> <span >{{row.bron_price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{row.ovqat_puli.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span></td>
                <td> <span >{{row.qolgan_pul.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span></td>
              </tr>
              <tr >
                <td></td>
                <td> <span >Общий</span> </td>
                <td> <span ></span> </td>
                <td class="alert-danger font-weight-bold"> <span>{{summa_usluga.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span></td>
                <td> <span> </span></td>
                <td class="alert-primary font-weight-bold"> <span>{{summa_operatsiya.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> </td>
                <td class="alert-success font-weight-bold"><span >{{summa_bron.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span></td>
                <td> <span> {{ get_bron_cash_card.ovqat_puli.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span></td>
                <td> <span> {{ get_bron_cash_card.qolgan_pul.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</span></td>
              </tr>
            </tbody>
          </table>
        </div>
        <div class="px-5 pb-3">
          <table>
            <tbody>
              <tr class="py-3 border-bottom">
                <td><span class="text-primary">Общий сумма: </span></td>
                <td><span class="text-primary pl-4">{{ (summa_bron + summa_operatsiya + summa_usluga).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</span></td>
              </tr>
              <tr class="py-3 border-bottom">
                <td><span class="text-primary">Прошлые возврат: </span></td>
                <td><span class="text-primary pl-4">{{ get_payed_date_vozvrat_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} сум</span></td>
              </tr>
              <tr class="py-3 border-bottom">
                <td><span class="text-primary">Расходы: </span></td>
                <td><span class="text-primary pl-4"> {{get_money.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</span></td>
              </tr>
              
              <tr class="py-3 border-bottom">
                <td><span class="text-primary">Осталь. денег: </span></td>
                <td><span class="text-primary pl-4">{{(summa_bron + summa_operatsiya + summa_usluga - get_money).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сум</span></td>
              </tr>
              <tr class="py-3 border-bottom">
                <td><span class="text-danger">Долг: </span></td>
                <td><span class="text-danger pl-4">0 сум</span></td>
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
  // import RegSelect from '../../../components/RegSelect.vue'
  // import districtAdd from "../../../components/new_prog_add/district_add"
  import {mdbBtn,  mdbInput, mdbIcon,  mdbModal, mdbModalHeader,   mdbModalBody,mdbModalFooter   } from 'mdbvue';
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
        summa_usluga: 0,
        summa_operatsiya: 0,
        summa_bron: 0,
        discount_qty: 0,
        doc_bron_startsionar_otchot: [],
        all_report_one_list_data: [],
        get_money: 0,
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
        let b = this.End_time + 'T23:59:59.000Z';
        let c = {
          time1: '',
          time2: '',
          contId: 0
        }
        c.time1 = a;
        c.time2 = b;
        this.loading = true;
        await this.fetch_users()
        console.log(this.get_users_list)
        await this.user_need_info_list();

        await this.fetch_report_by_data_time_service_bron(c);
        await this.fetch_report_by_bron_payed(c);
        await this.fetch_payed_date_vozvrat_list(c);
        await this.bronPaymentSort();
        await this.fetchRasxodList();

      }

        this.fetch_service_group()
    },
    computed: mapGetters(['get_contragent_list','get_users_list', 'get_report_by_data_time', 'get_bron_cash_card',
      'get_report_by_time_card_cash', 'get_service_group_list','get_service_bron_data','get_payment_payed_history', 'get_payed_date_vozvrat_summa']),
    methods: {
      ...mapActions(['fetch_contragent','fetch_users','fetch_report_by_bron_payed', 'fetch_report_by_data_time', 
        'fetch_service_group', 'fetch_report_by_data_time_service_bron', 'fetch_payed_date_vozvrat_list']),
      ...mapMutations(['district_row_delete']),
      async user_need_info_list(){
        this.all_report_one_list_data = [];
        for(let i=0; i<this.get_users_list.rows.length;i++){
          let userDate = {
            user_name: '',
            user_type: null,
            user_id : null,
            service_price: 0,
            service_qty: 0,
            operatsion_price: 0,
            operatsion_qty:0,
            authorizationId:null,
            payedDate: null, 
            bron_price: 0,
            bron_qty:0,
            ovqat_puli: 0,
            qolgan_pul: 0,
            position: 0,
          }
          userDate.user_name = this.get_users_list.rows[i].fio;
          userDate.user_id = this.get_users_list.rows[i].id;
          // userDate.position = this.get_users_list.rows[i].position.name;
          if(this.get_users_list.rows[i].authorization != undefined){
            userDate.user_type = this.get_users_list.rows[i].authorization.userType;
            if(this.get_users_list.rows[i].authorization.userType == 1 || this.get_users_list.rows[i].authorization.userType == 3){
              this.all_report_one_list_data.push(userDate);
            }
          }
        }
        console.log('this.all_report_one_list_data')
        console.log(this.all_report_one_list_data)
      },
      add(){
        this.show =! this.show
        this.editData = {};
      },
      print(){
        this.$refs.listlar.generatePdf()
      },
      deleteRow(data, index){
        console.log(data)
        this.index = index;
        this.service_id = data.id;
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
      async bronPaymentSort(){
        this.doc_bron_startsionar_otchot = [];
        this.loading = true;
        console.log('this.get_payment_payed_history')
        console.log(this.get_payment_payed_history)
        for(let i=0; i<this.get_payment_payed_history.length; i++){
          var bron_item_doc_his = {
            user_id: null,
            bron_price: 0,
            bron_qty: 0,
            ovqat_puli: 0,
            qolgan_pul: 0,
          }
          bron_item_doc_his.user_id = this.get_payment_payed_history[i].hospitalBronRoomPayments.UsersId;
          bron_item_doc_his.bron_price = parseInt(this.get_payment_payed_history[i].card_sum + this.get_payment_payed_history[i].cash_sum);
          bron_item_doc_his.bron_qty = parseInt(this.get_payment_payed_history[i].hospitalBronRoomPayments.reserved_number_1);
          bron_item_doc_his.ovqat_puli = parseInt(this.get_payment_payed_history[i].ovqat_puli);
          bron_item_doc_his.qolgan_pul = parseInt(this.get_payment_payed_history[i].qolgan_pul);
          for(let j=i+1; j<this.get_payment_payed_history.length; j++){
            if(this.get_payment_payed_history[i].hospitalBronRoomPayments.UsersId == this.get_payment_payed_history[j].hospitalBronRoomPayments.UsersId){
              bron_item_doc_his.bron_price += parseInt(this.get_payment_payed_history[j].card_sum + this.get_payment_payed_history[j].cash_sum)
              bron_item_doc_his.bron_qty += parseInt(this.get_payment_payed_history[j].hospitalBronRoomPayments.reserved_number_1)
              bron_item_doc_his.ovqat_puli += parseInt(this.get_payment_payed_history[j].ovqat_puli)
              bron_item_doc_his.qolgan_pul += parseInt(this.get_payment_payed_history[j].qolgan_pul)
              this.get_payment_payed_history.splice(j,1);
              j--;
            }
          }
          // console.log(bron_item_doc_his)
          this.doc_bron_startsionar_otchot.push(bron_item_doc_his)
        }
        console.log('this.doc_bron_startsionar_otchot')
        console.log(this.doc_bron_startsionar_otchot)
        await this.all_report_one_list();
      },

      async all_report_one_list(){
        this.loading = true;
        console.log(this.get_service_bron_data)
        console.log(this.doc_bron_startsionar_otchot)
        console.log(this.doc_bron_startsionar_otchot)
        for(let i=0; i<this.get_service_bron_data.length; i++){
          for(let j=0; j<this.all_report_one_list_data.length; j++){
            if(this.all_report_one_list_data[j].user_id == this.get_service_bron_data[i].user_id){
              this.all_report_one_list_data[j].service_price = this.get_service_bron_data[i].service_price;
              this.all_report_one_list_data[j].service_qty = this.get_service_bron_data[i].service_qty;
              this.all_report_one_list_data[j].operatsion_price = this.get_service_bron_data[i].operatsion_price;
              this.all_report_one_list_data[j].operatsion_qty = this.get_service_bron_data[i].operatsion_qty;
              this.all_report_one_list_data[j].authorizationId = this.get_service_bron_data[i].authorizationId;
              this.all_report_one_list_data[j].payedDate = this.get_service_bron_data[i].payedDate;
            }
          }
        }
        for(let i=0; i<this.doc_bron_startsionar_otchot.length; i++){
          for(let j=0; j<this.all_report_one_list_data.length; j++){
            if(this.all_report_one_list_data[j].user_id == this.doc_bron_startsionar_otchot[i].user_id){
              this.all_report_one_list_data[j].bron_price = this.doc_bron_startsionar_otchot[i].bron_price;
              this.all_report_one_list_data[j].bron_qty = this.doc_bron_startsionar_otchot[i].bron_qty;
              this.all_report_one_list_data[j].ovqat_puli = this.doc_bron_startsionar_otchot[i].ovqat_puli;
              this.all_report_one_list_data[j].qolgan_pul = this.doc_bron_startsionar_otchot[i].qolgan_pul;
            }
          }
        }
        this.loading = false;
        this.loading = true;
        this.summa_usluga = 0;
        this.summa_operatsiya = 0;
        this.summa_bron = 0;
        for(let k=0; k<this.all_report_one_list_data.length; k++){
          this.summa_usluga += this.all_report_one_list_data[k].service_price;
          this.summa_operatsiya += this.all_report_one_list_data[k].operatsion_price;
          this.summa_bron += this.all_report_one_list_data[k].bron_price;

        }
        this.loading = false;
        console.log('mana shuni otchoti srochniy kerak')
        console.log(this.all_report_one_list_data)
        
      },
      
      
      async submit(){
        let a = {
          time1: "2021-09-01T09:15:28.886Z",
          time2: new Date(),
          contId: 0
        }
        a.time1 = this.Start_time + 'T00:00:35.000Z';
        a.time2 = this.End_time + 'T23:59:59.000Z';
        this.loading = true;
        await this.user_need_info_list();
        await this.fetch_report_by_data_time_service_bron(a);
        console.log(this.get_service_bron_data);
        await this.fetch_report_by_bron_payed(a);
        await this.fetch_payed_date_vozvrat_list(a);
        console.log('this.get_payment_payed_history')
        console.log(this.get_payment_payed_history)
        await this.bronPaymentSort();

        this.get_payment_list = [];
        this.get_payment_list = this.get_service_bron_data;
        await this.fetchRasxodList();
        this.loading = false;
      },
      async fetchRasxodList(){
        try{
          let timed1 = this.Start_time + 'T00:00:35.000Z'
          let timed2 = this.End_time + 'T23:59:35.000Z'
          const responsed = await fetch(this.$store.state.hostname + '/ReturnMoneys/getReturnMoneyListBeatwenDateTimeAndKassirId?beginDate=' + timed1 + 
          '&endDate=' + timed2 + '&kassirId=0')
          const dataJson = await responsed.json()
          this.get_money = 0;
          for(var i = 0; i < dataJson.length; i++) {
            this.get_money += dataJson[i].price;
          }
        }
        catch(error){
          console.log(error)
        }
      }

    },
  };
</script>

<style lang="scss" scoped>
  @import "../../../scss/tableAll.scss";
</style>

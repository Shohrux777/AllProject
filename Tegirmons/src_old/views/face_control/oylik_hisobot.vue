<template>
  <div class="bg-white p-3">
    
    <div id="printArea">
      <div class="d-flex justify-content-end">
       <mdb-btn  color="info py-2 px-3 m-0" style="font-size:9px;" @click="printDiv">
          Печат
        </mdb-btn>

        <download-excel

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
        </download-excel>
      </div>
      
      <div class="px-2" v-if="user_old_day_report_list.length">
        <small style="font-size: 14px; font-weight:bold; font-style: italic;">Oldingi oylardan tulov qilinmaganlari</small>
      </div>
      <div class="bg-white  mb-3 p-0 shadow"  style="border-radius:5px; position:relative;" v-if="user_old_day_report_list.length">
        <div class="TablePatientDocId p-0 pt-2 px-1">
          
          <table class="kunlik_hisobot_table">
            <thead style="background-color: #C4DEE4;">
              <tr class="header ">
                <th  width="40" class="text-left">№</th>
                <th width="40">ID</th>
                <th >Kun</th>
                <th >FIO</th>
                <th >{{$t('smena')}}</th>
                <th >{{$t('dept')}}</th>
                <th width="100">Kirish</th>
                <th  width="100">Chiqish</th>
                <th >Status</th>
                <th >Vaqt</th>
                <th >Summa</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,index) in user_old_day_report_list" :key="index + 250">
                <td> <small >{{index+1}}</small> </td>
                <td> <small >{{row.userid}}</small> </td>
                <td> <small >{{row.k_date.slice(0,10)}}</small> </td>
                <td> <small v-if="user_report_list.length>0">{{user_report_list[0].fio}}</small> </td>
                <td> <small ></small> </td>
                <td> <small ></small> </td>
                <td>
                  <small v-if="row.k_date" class="bg-success px-2 rounded text-white">{{row.k_date.slice(11,16)}}</small>
                </td>
                <td>
                  <small v-if="row.created_date" class="bg-success px-2 rounded text-white">{{row.created_date.slice(11,16)}}</small>
                </td>
                <td>
                  <small  class="px-2 rounded  text-success">Ishga keldi</small>
                </td>
                <td> <small >{{row.work_time}}</small></td>
                <td> <small v-if="row.sum">{{row.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
              </tr>
              <tr>
                <td colspan="3">
                  Общий
                </td>
                <td colspan="7">
                </td>
                <td>{{oylik_old_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>

      <div class="bg-white  mb-3 p-0 shadow"  style="border-radius:5px; position:relative;">
        <div class="TablePatientDocId p-0 pt-2 px-1">
          <table class="kunlik_hisobot_table">
            <thead style="background-color: #C4DEE4;">
              <tr class="header ">
                <th  width="40" class="text-left">№</th>
                <th width="40">ID</th>
                <th >Kun</th>
                <th >FIO</th>
                <th >{{$t('smena')}}</th>
                <th >{{$t('dept')}}</th>
                <th width="100">Kirish</th>
                <th  width="100">Chiqish</th>
                <th >Status</th>
                <th >Vaqt</th>
                <th >Summa</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in user_report_list" :key="rowIndex" @click="selectInvoiceItem(row)">
                <td> <small >{{rowIndex+1}}</small> </td>
                <td> <small >{{row.userId}}</small> </td>
                <td> <small >{{row.day.slice(0,10)}}</small> </td>
                <td> <small >{{row.fio}}</small> </td>
                <td> <small >{{row.user.group_name}}</small> </td>
                <td> <small >{{row.user.familiya}}</small> </td>
                <td>
                  <small v-if="row.kirish == '--:--'" class="bg-danger px-2 rounded text-white">{{row.kirish}}</small>
                  <small v-else class="bg-success px-2 rounded text-white">{{row.kirish}}</small>
                </td>
                <td>
                  <small v-if="row.chiqish == '--:--'" class="bg-danger px-2 rounded text-white">{{row.chiqish}}</small>
                  <small v-else class="bg-success px-2 rounded text-white">{{row.chiqish}}</small> 
                </td>
                <td>
                  <small v-if="row.isYuqlama" class=" px-2 rounded text-secondary">Javob olgan</small>
                  <small v-else-if="row.isLate" class=" px-2 rounded text-warning">Ishga kech kelgan</small>
                  <small v-else-if="row.kirish == '--:--'" class=" px-2 rounded text-danger">Ishga kelmadi</small>
                  <small v-else class="px-2 rounded  text-success">Ishga keldi</small>
                </td>
                <td> <small >{{row.workedTime}}</small></td>
                <td> <small >{{row.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
              </tr>
              <tr>
                <td colspan="3">
                  Общий
                </td>
                <td colspan="7">
                </td>
                <td>{{oylik_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>

      <div class="px-2" v-if="user_monthly_prixod_list.length">
        <small style="font-size: 14px; font-weight:bold; font-style: italic;">Xodimlar bergan pullar</small>
      </div>
      <div class="bg-white  mb-3 p-0 shadow"  style="border-radius:5px; position:relative;" v-if="user_monthly_prixod_list.length">
        <div class="TablePatientDocId p-0 pt-2 px-1">
          <table class="kunlik_hisobot_table">
            <thead style="background-color: #C4DEE4;">
              <tr class="header ">
                <th  width="40" class="text-left">№</th>
                <th width="40">ID</th>
                <th >Kun</th>
                <th >FIO</th>
                <th >Summa</th>
                <th >Rasm</th>
                <th >Izoh</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,keys) in user_monthly_prixod_list" :key="keys+350">
                <td> <small >{{keys+1}}</small> </td>
                <td> <small >{{row.userid}}</small> </td>
                <td> <small >{{row.k_date.slice(0,10)}}</small>  <small>{{row.k_date.slice(11,16)}}</small></td>
                <td> <small v-if="user_report_list.length>0">{{user_report_list[0].fio}}</small> </td>
                <td> <small v-if="row.sum">{{row.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                <td> <img :src="hostname + row.image_url" alt="" style="height: 70px;"> </td>
                <td> <small > {{row.note}} </small></td>
              </tr>
              <tr>
                <td colspan="3">
                  Общий
                </td>
                <td colspan="3">
                </td>
                <td>{{pay_poluchit.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>

      <div class="px-2" v-if="user_monthly_rasxod_list.length">
        <small style="font-size: 14px; font-weight:bold; font-style: italic;">Xodimga berilgan pullar</small>
      </div>
      <div class="bg-white  mb-3 p-0 shadow"  style="border-radius:5px; position:relative;" v-if="user_monthly_rasxod_list.length">
        <div class="TablePatientDocId p-0 pt-2 px-1">
          
          <table class="kunlik_hisobot_table">
            <thead style="background-color: #C4DEE4;">
              <tr class="header ">
                <th  width="40" class="text-left">№</th>
                <th width="40">ID</th>
                <th >Kun</th>
                <th >FIO</th>
                <th >Summa</th>
                <th >Rasm</th>
                <th >Izoh</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,i) in user_monthly_rasxod_list" :key="i+150">
                <td> <small >{{i+1}}</small> </td>
                <td> <small >{{row.userid}}</small> </td>
                <td> <small >{{row.k_date.slice(0,10)}}</small>  <small>{{row.k_date.slice(11,16)}}</small></td>
                <td> <small v-if="user_report_list.length>0">{{user_report_list[0].fio}}</small> </td>
                <td> <small v-if="row.sum">{{row.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                <td> <img :src="hostname + row.image_url" alt="" style="height: 70px;"> </td>
                <td> <small > {{row.note}} </small></td>
              </tr>
              <tr>
                <td colspan="3">
                  Общий
                </td>
                <td colspan="3">
                </td>
                <td>{{pay_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>


      <div class="bg-white  mb-3 p-0 shadow"  style="border-radius:5px; position:relative;">
        <div class="TablePatientDocId p-0 pt-2 px-1">
          <table class="kunlik_hisobot_table">
            <tbody>
              <tr v-if="oylik_old_summa>0">
                <td> <small > Oldingi oylardan tulov qilinmagan: </small> </td>
                <td> <small > {{ oylik_old_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} </small> </td>
              </tr>
              <tr v-if="oylik_summa>0">
                <td> <small > Hisoblangan ish haqqi: </small> </td>
                <td> <small > {{ oylik_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} </small> </td>
              </tr>
              <tr v-if="pay_poluchit>0">
                <td> <small > Kassaga berilgan pullar: </small> </td>
                <td> <small > {{ pay_poluchit.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} </small> </td>
              </tr>
              <tr v-if="pay_rasxod>0">
                <td> <small > Xodimning kassadan olgan pullari: </small> </td>
                <td> <small > {{ pay_rasxod.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} </small> </td>
              </tr>
              <tr v-if="old_debt>0">
                <td> <small > Oldingi oydan qolgan pul: </small> </td>
                <td> <small > {{ old_debt.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} </small> </td>
              </tr>
              <tr v-if="old_debt<0">
                <td> <small > Oldingi oydan qolgan qarz: </small> </td>
                <td> <small > {{ old_debt.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} </small> </td>
              </tr>
              
              <tr>
                <td> <small style="font-weight: bold !important;"> Beriladigan summa miqdori: </small> </td>
                <td> <small style="font-weight: bold !important;"> {{ (oylik_old_summa + oylik_summa + pay_poluchit - pay_rasxod + old_debt).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} </small> </td>
              </tr>
              <tr ></tr>
              <tr class="alert-success">
                <td v-if="berilgan_sum"> <small > Xodimga berilga ish haqqi: </small> </td>
                <td> <small > {{ berilgan_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} </small> </td>
              </tr>
              <tr class="alert-success" v-if="qolgan_sum">
                <td > <small > Xodimga berilga ish haqqi: </small> </td>
                <td> <small > {{ qolgan_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }} </small> </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
    
    <Toast ref="message"></Toast>
    <block ref="blocked"></block>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>
</template>

<script>
  // import erpSelect from "../../components/erpSelectDynamic.vue";
  import { mdbBtn, mdbInput, mdbIcon   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
import Circle_progress from './circle_progress.vue';

  // import 'vue2-datepicker/index.css';
  export default {
    data(){
      return{
        hostname: this.$store.state.server_ip,
        loading:false,
        modal_info : '',
        modal_status: false,
        user_report_list: [],
        user_old_day_report_list: [],        
        fio: '',
        oylik_name: '',
        dept_name: '',
        oylik_summa: 0,
        oylik_old_summa: 0,
        user_monthly_rasxod_list: [],
        user_monthly_prixod_list: [],
        pay_poluchit: 0,
        pay_rasxod: 0,
        old_debt: 0,

        berilgan_sum: 0,
        qolgan_sum: 0,

        json_fields: {
          'Ish kunlar': {
            callback: (value) => {
              return value.day.slice(0,10);
            }
          },
          'Fio': 'fio',
          'Kirish': 'kirish',
          'Chiqish': 'chiqish',
          'Status':{
            callback: (value) => {
              if(value.isYuqlama){
                return 'Javob olgan';
              }
              else if(value.isLate){
                return `Ishga kech kelgan`;
              }
              else if(value.kirish == '--:--'){
                return '-'
              }
              else if (value.kirish == ''){
                return ''
              }
              else{
                return '+'
              }
            },
          },
          
          'Ishlagan vaqti':'workedTime',
          'Ish haqqi': {
            callback: (value) => {
              if(value.sum){
                return value.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
              }
              else if(value.sum == null){
                return null
              }
              else{
                return 0;
              }
            },
          },
        },
      }
    },
    props:{
    selected_date:{
      type: String,
      default: ''
    },
    user_id:{
      type: Number,
      default: null,
    },
    start_date: {
      type: String,
      default: ''
    },
    end_date: {
      type: String,
      default: ''
    },
    oylik_status: {
      type: Boolean,
      default: false
    }

  },
    components: {
      mdbBtn,
      mdbIcon,
      mdbInput,
      Circle_progress,
      // erpSelect
    },

    async mounted(){
      let time1 = new Date();
      this.selected_date = time1.toISOString().slice(0,10);
      // await this.fetchUserOylikReport();
    },
    computed:  {
      ...mapGetters([]),
    },
    methods: {
      ...mapActions([]),
      ...mapMutations([]),

      printDiv() {
        let divContents = document.getElementById("printArea").innerHTML;

        // Hamma <link> va <style> teglarini olish
        let styles = "";
        document.querySelectorAll('link[rel="stylesheet"], style').forEach((node) => {
          styles += node.outerHTML;
        });

        let printWindow = window.open("", "", "height=800,width=1000");
        printWindow.document.write(`
          <html>
            <head>
              <title>Chop etish</title>
              ${styles} <!-- O'zingizning CSS'lar -->
              <style>
                @page {
                  size: A4;
                  margin: 20mm;
                }
                @media print {
                  body {
                    zoom: 0.6; /* Butun sahifani kichraytiradi */
                  }
                }
              </style>
            </head>
            <body>
              ${divContents}
            </body>
          </html>
        `);
        printWindow.document.close();
        printWindow.focus();
        printWindow.print();
      },

      clw_cw(){
        this.user_report_list = [];
        this.user_old_day_report_list = [];
        this.oylik_summa = 0;
        this.oylik_old_summa = 0;
        this.user_monthly_rasxod_list = [];
        this.user_monthly_prixod_list = [];
        this.pay_poluchit = 0;
        this.pay_rasxod = 0;
        this.old_debt = 0;
        this.berilgan_sum = 0;
        this.qolgan_sum = 0;
      },
    
      async fetchUserOylikReport(invoice){
        this.clw_cw();
        if(invoice.num == -1){
          await this.fetch_user_monthly_report();
          await this.fetch_user_old_worked_day();

          await this.fetch_userRasxod_list();
          await this.fetch_userPrixod_list();
          await this.fetch_User_Last_Salary();
        }
        else{
          console.log(invoice)
          // berilgan oylikni kurishda ochiladigan oyna
          await this.fetch_user_monthly_report_rb(invoice);
          await this.fetch_user_old_worked_day_rb(invoice);
          await this.fetch_userRasxod_list_rb(invoice);
          await this.fetch_userPrixod_list_rb(invoice);
          this.old_debt = invoice.old_qarz;

          this.berilgan_sum = invoice.sum;
          this.qolgan_sum = invoice.old_debt;

        }
      },
      async fetch_user_old_worked_day(){
        try{
            const res = await fetch(this.$store.state.hostname + '/TegirmonUserIshlaganPuli/getUserWorkedDaysSalaryOldDaysForReports?userid=' + this.user_id + '&begin_date=' + this.start_date);
            const data = await res.json();
            console.log('old work days')
            if(res.status == 200 || res.status == 201){
                console.log(data)
                this.user_old_day_report_list = data;
                this.oylik_old_summa = 0;
                this.oylik_old_summa = this.user_old_day_report_list.reduce((sum, item)=>{
                  return sum + item.sum
                },0)
            }
        }
        catch(error){
            console.log(error)
        }
      },
      async fetch_user_monthly_report(){
        try{
            const res = await fetch(this.$store.state.hostname + '/SkudMyCheckinouts/by-date_month?startDate=' + this.start_date + '&endDate=' + this.end_date + '&user_id=' + this.user_id);
            const data = await res.json();
            console.log('data time worked time')
            if(res.status == 200 || res.status == 201){
                console.log(data)
                this.user_report_list = data;
                this.oylik_summa = 0;
                this.oylik_summa = this.user_report_list.reduce((sum, item)=>{
                  return sum + item.sum
                },0)
            }
        }
        catch(error){
            console.log(error)
        }
      },

      async fetch_userRasxod_list(){
        this.user_monthly_rasxod_list = [];
        this.pay_rasxod = 0;
        try{
            const res = await fetch(this.$store.state.hostname + '/TegirmonUserIshlaganPuli/getUserWorkedDaysRasxodEnd?userid=' + this.user_id + '&end_date=' + this.end_date);
            const data = await res.json();
            if(res.status == 200 || res.status == 201){
              console.log(data)
              this.user_monthly_rasxod_list = data;
              this.pay_rasxod = this.user_monthly_rasxod_list.reduce((sum, item) => {
                return sum += item.sum;
              },0)
            }
          }
          catch(error){
              console.log(error)
          }
      },
      async fetch_userPrixod_list(){
        this.user_monthly_prixod_list = [];
        this.pay_poluchit = 0;
        try{
            const res = await fetch(this.$store.state.hostname + '/TegirmonUserIshlaganPuli/getUserWorkedDaysPrixodEnd?userid=' + this.user_id + '&end_date=' + this.end_date);
            const data = await res.json();
            if(res.status == 200 || res.status == 201){
              console.log(data)
              this.user_monthly_prixod_list = data;
              this.pay_poluchit = this.user_monthly_prixod_list.reduce((sum, item) => {
                return sum += item.sum;
              },0)
            }
          }
          catch(error){
              console.log(error)
          }
      },
      async fetch_User_Last_Salary(){
        this.old_debt = 0;
        try{
            const res = await fetch(this.$store.state.hostname + '/TegirmonUserIshlaganPuli/getLastUserWorkedOylikSumma?userid=' + this.user_id);
            const data = await res.json();
            if(res.status == 200 || res.status == 201){
              console.log(data)
              this.old_debt = data.old_debt;
            }
          }
          catch(error){
              console.log(error)
          }
      },

      
      async fetch_user_monthly_report_rb(invoice){
        try{
            const res = await fetch(this.$store.state.hostname + '/SkudMyCheckinouts/by-date_month_payedSalary?startDate=' + invoice.created_date_time.slice(0,10) + '&endDate=' + invoice.updated_date_time.slice(0,10) + '&user_id=' + this.user_id);
            const data = await res.json();
            console.log('data time worked time')
            if(res.status == 200 || res.status == 201){
                console.log(data)
                this.user_report_list = data;
                this.oylik_summa = 0;
                this.oylik_summa = this.user_report_list.reduce((sum, item)=>{
                  return sum + item.sum
                },0)
            }
        }
        catch(error){
            console.log(error)
        }
      },
      async fetch_user_old_worked_day_rb(invoice){
        try{
            const res = await fetch(this.$store.state.hostname + '/TegirmonUserIshlaganPuli/getUserWorkedDaysSalaryOldDaysForReportsPaidSalary?userid=' + this.user_id + '&begin_date=' + invoice.created_date_time.slice(0,10) + '&invoice_id=' + invoice.id);
            const data = await res.json();
            console.log('old work days')
            if(res.status == 200 || res.status == 201){
                console.log(data)
                this.user_old_day_report_list = data;
                this.oylik_old_summa = 0;
                this.oylik_old_summa = this.user_old_day_report_list.reduce((sum, item)=>{
                  return sum + item.sum
                },0)
            }
        }
        catch(error){
            console.log(error)
        }
      },
      async fetch_userRasxod_list_rb(invoice){
        this.user_monthly_rasxod_list = [];
        this.pay_rasxod = 0;
        try{
            const res = await fetch(this.$store.state.hostname + '/TegirmonUserIshlaganPuli/getUserWorkedDaysRasxodEndPaidSalary?userid=' + this.user_id + '&end_date=' + invoice.updated_date_time.slice(0,10) + '&invoice_id=' + invoice.id);
            const data = await res.json();
            if(res.status == 200 || res.status == 201){
              console.log(data)
              this.user_monthly_rasxod_list = data;
              this.pay_rasxod = this.user_monthly_rasxod_list.reduce((sum, item) => {
                return sum += item.sum;
              },0)
            }
          }
          catch(error){
              console.log(error)
          }
      },
      async fetch_userPrixod_list_rb(invoice){
        this.user_monthly_prixod_list = [];
        this.pay_poluchit = 0;
        try{
            const res = await fetch(this.$store.state.hostname + '/TegirmonUserIshlaganPuli/getUserWorkedDaysPrixodEndPaidSalary?userid=' + this.user_id + '&end_date=' + invoice.updated_date_time.slice(0,10) + '&invoice_id=' + invoice.id);
            const data = await res.json();
            if(res.status == 200 || res.status == 201){
              console.log(data)
              this.user_monthly_prixod_list = data;
              this.pay_poluchit = this.user_monthly_prixod_list.reduce((sum, item) => {
                return sum += item.sum;
              },0)
            }
          }
          catch(error){
              console.log(error)
          }
      },




       async startDownload(){
        console.log();
        let temp = {
          day : '',
          fio: '',
          kirish: '',
          chiqish: '',
          isYuqlama: false,
          isLate: false,
          workedTime: '',
          sum: null,
        }
        this.user_report_list.push(temp);

        let temp2 = {
          day : 'Общий',
          fio: '',
          kirish: '',
          chiqish: '',
          isYuqlama: false,
          isLate: false,
          workedTime: '',
          sum: this.oylik_summa,
        }
        this.user_report_list.push(temp2);
       },
       async finishDownload(){
          this.user_report_list.splice(this.user_report_list.length-1,1);
          this.user_report_list.splice(this.user_report_list.length-1,1);
       }

    },
  };
</script>

<style lang="scss" scoped>

.kunlik_hisobot_table {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; 
  overflow-x:auto
}
.kunlik_hisobot_table th{
  font-weight: 500;
  font-size:12.5px;
  
}
.kunlik_hisobot_table td{
  font-size:14px;
  border-left: 1px solid #e6e6e6;
}
.kunlik_hisobot_table th, .kunlik_hisobot_table td {
  text-align: left;
  padding: 5px;
  text-align: center;
  small{
    font-weight: 200 !important;
    font-size: 12.5px;
  }
}

.kunlik_hisobot_table tr {
  cursor: pointer;
  border-bottom: 1px solid rgb(240, 240, 240);
}
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
.selet_item{
  width:100%;
  border:none;
  border: 1px solid #d5d6d7;
  border-radius: 5px;
  color:#495077;
  font-size: 13px;
  padding: 5.8px 3px;
  outline: none;
  margin-top: 1px;
  cursor: pointer;

}
.selet_item:focus{
  border: 1.7px solid #4285F4;
}
.selet_item option{
  cursor: pointer !important;
  font-size: 12.5px;
}
.calendar-controls{
  width: 100%;
  display: flex;
  justify-content: flex-start;
  padding: 5px 0;
}
.calendar-controls select{
  width: 120px;
  height: 30px;
  border: 1px solid rgb(204, 204, 204);
  border-radius: 5px;
  color: rgb(58, 57, 57);
  font-size: 14px;
  padding: 5px;
  margin-left: 5px;
}

.excel_btn{
  display: flex;
  justify-content: center;
  align-items: center;
  height: 30px;
  padding: 0 15px;
  color:#3a4b52;
  border-radius: 4px;
  cursor: pointer;
  margin-right: 7px;
  margin-top: 10px;
  small{
    font-size: 12px;
  }
}
.bg_col_blue{
  border: 1.5px solid #009587;
  background: #009587;   
  color:white;

  &:hover{
    color:white;
    background: #009587;  
  }
}

</style>
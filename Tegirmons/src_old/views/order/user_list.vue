<template>
  <div class="bg-white p-3">
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
    <div id="printArea">
      <div class="bg-white mb-3 p-0"  style="border-radius:5px; position:relative;">
        <div class="TablePatientDocId p-0 pt-2 px-1">
          <table class="kunlik_hisobot_table">
            <thead style="background-color: #C4DEE4;">
              <tr class="header">
                <th  width="40" class="text-left">№</th>
                <th width="40">ID</th>
                <th width="350">FIO</th>
                <th >{{$t('passport_number')}}</th>
                <th >{{$t('addiotionala_information')}}</th>
                <th >{{$t('phone_number')}}</th>
                <th >{{$t('sum')}}</th>
                <th >{{$t('dollor')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in user_report_list" :key="rowIndex" @click="selectInvoiceItem(row)">
                <td> <small >{{rowIndex+1}}</small> </td>
                <td> <small >{{row.id}}</small> </td>
                <td> <small >{{row.fio}}</small> </td>
                <td> <small >{{row.passport_number}}</small></td>
                <td> <small >{{row.addiotionala_information}}</small> </td>
                <td> <small >{{row.phone_number}}</small> </td>
                <td> <small >{{row.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                <td> <small >{{row.dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
              </tr>
              <tr>
                <td colspan="3">
                  Общий
                </td>
                <td colspan="3">
                </td>
                <td>{{sum_all.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</td>
                <td>{{dollor_all.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</td>
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
// import Circle_progress from './circle_progress.vue';

  // import 'vue2-datepicker/index.css';
  export default {
    data(){
      return{
        hostname: this.$store.state.server_ip,
        loading:false,
        modal_info : '',
        modal_status: false,
        user_report_list: [],
        sum_all: 0,
        dollor_all: 0,
        

        json_fields: {
          'Fio': 'fio',
          'Номер паспорта': 'passport_number',
          'Дата рождения': 'addiotionala_information',
          'Телефон': 'phone_number',
          
          'Сумма': {
            callback: (value) => {
              if(value.sum){
                return value.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
              }
            },
          },
          'Доллор': {
            callback: (value) => {
              if(value.dollor){
                return value.dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
              }
            },
          },
        },
      }
    },
    components: {
      mdbBtn,
      mdbIcon,
      mdbInput,
      // Circle_progress,
      // erpSelect
    },

    async mounted(){
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
                  margin: 5mm;
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
    
      async fetchUserOylikReport(){
        this.sum_all = 0;
        this.dollor_all = 0;
        try{
          const response = await fetch(this.$store.state.hostname + "/TegirmonQarzdorlikUser/getAllSortedByDebt");
          const data = await response.json();
          if(response.status == 201 || response.status == 200)
          {
            console.log(data)
            this.user_report_list = data;
            this.sum_all = this.user_report_list.reduce((sum, item)=>{
              return sum += item.sum;
            },0)
            this.dollor_all = this.user_report_list.reduce((sum, item)=>{
              return sum += item.dollor;
            },0)
            return true;
          }
          else{
            this.modal_info = data;
            this.modal_status = true;
            return false;
          }
        }
        catch{
          this.client_list = [];
          this.modal_info = this.$i18n.t('network_ne_connect');
          this.modal_status = true;
        }
      },
     



       async startDownload(){
        let temp2 = {
          fio: 'Общий',
          passport_number: '',
          addiotionala_information: '',
          phone_number: '',
          sum: this.sum_all,
          dollor: this.dollor_all,
        }
        this.user_report_list.push(temp2);
       },
       async finishDownload(){
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
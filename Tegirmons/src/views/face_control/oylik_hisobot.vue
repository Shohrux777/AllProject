<template>
  <div class="bg-white p-3" style="position:relative;">
    <div>
      <div class="d-flex justify-content-end">
        <download-excel

          class=" rounded px-2 excel_btn m-0 bg_col_blue"
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
      
      <div class="bg-white  mb-5 p-0 shadow"  style="border-radius:5px; position:relative;">
        <div class="TablePatientDocId p-0 pt-2 px-1">
          <table class="kunlik_hisobot_table">
            <thead style="background-color: #C4DEE4;">
              <tr class="header ">
                <th  width="40" class="text-left">№</th>
                <th width="40">ID</th>
                <th >Kun</th>
                <th width="400">FIO</th>
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
        loading:false,
        modal_info : '',
        modal_status: false,
        user_report_list: [],
        
        id: this.$route.params.id,
        fio: '',
        oylik_name: '',
        dept_name: '',
        selected_date: null,
        user_id: null,
        oylik_summa: 0,

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
      await this.fetchUserOylikReport();
    },
    computed:  {
      ...mapGetters([]),
    },
    methods: {
      ...mapActions([]),
      ...mapMutations([]),
    
      async fetchUserOylikReport(){
        try{
            const res = await fetch(this.$store.state.hostname + '/SkudMyCheckinouts/by-date_month?month=' + this.selected_date + '&user_id=' + this.user_id);
            const data = await res.json();
            console.log('this is by id')
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
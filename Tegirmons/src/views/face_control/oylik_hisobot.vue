<template>
  <div class="bg-white" style="position:relative;">
    <div  >
      <div class="bg-white  mb-5 pb-4 shadow"  style="border-radius:5px; position:relative;">
        <form @submit.prevent="submit" >
          <div style="height: 45px;" class="d-flex justify-content-between border-bottom align-items-center  ">
            <div class="title w-100 row align-items-center">
              <div class="col-2">
                <div class="calendar-controls">
                  <select v-model="selectedYear" @change="refreshCalendar" >
                      <option v-for="year in years" :key="year" :value="year">{{ year }}</option>
                  </select>
                  <select v-model="selectedMonth" @change="refreshCalendar">
                      <option v-for="month in months" :key="month.value" :value="month.value">
                      {{ month.name }}
                      </option>
                  </select>
                </div>
              </div>
              <div class="col-3">
                <div style="position: relative; margin-top: 25px;">
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">    
                  </small>
                  <mdb-input type="text" size="sm"  v-model="fio" placeholder="FIO" disabled outline/>
                </div>
              </div>
              <div class="col-3">
                <div style="position: relative; margin-top: 25px;">
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">    
                  </small>
                  <mdb-input type="text" size="sm"  v-model="oylik_name" placeholder="Oylik turi" disabled outline/>
                </div>
              </div>
              <div class="col-3">
                <div style="position: relative; margin-top: 25px;">
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">    
                  </small>
                  <mdb-input type="text" size="sm"  v-model="dept_name" placeholder="Отдель" disabled outline/>
                </div>
              </div>
            </div>
          </div>
        </form>
        <div class="container-fluid">
          <div class="row">
            <div class="col-3 py-2 pl-1 pr-2" style="cursor: pointer;" @click="change_user_status(1)">
              <Circle_progress label="Ishga kelgan kunlari" :qty="xodim_soni" :activ_qty="xodim_kelgan" progress_bg="progress_bg_success"/>
            </div>
            <div class="col-3 py-2 px-1 pr-2" style="cursor: pointer;" @click="change_user_status(0)">
              <Circle_progress label="Ishga kelmagan kunlari" :qty="xodim_soni" :activ_qty="xodim_kelmagan" progress_bg="progress_bg_danger"/>
            </div>
            <div class="col-3 py-2 px-1 pr-2" style="cursor: pointer;" @click="change_user_status(2)">
              <Circle_progress label="Ishga kech kelgan kunlari" :qty="xodim_soni" :activ_qty="xodim_kech_kel" progress_bg="progress_bg_warning"/>
            </div>
          </div>
        </div>
        
        <!-- <div class="TablePatientDocId p-0 pt-2 px-1">
          <table class="kunlik_hisobot_table">
            <thead style="background-color: #C4DEE4;">
              <tr class="header ">
                <th  width="40" class="text-left">№</th>
                <th width="40">ID</th>
                <th width="400">FIO</th>
                <th >Tel. nomer</th>
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
              <tr v-for="(row,rowIndex) in select_status_list" :key="rowIndex" @click="selectInvoiceItem(row)">
                <td> <small >{{rowIndex+1}}</small> </td>
                <td> <small >{{row.userId}}</small> </td>
                <td> <small >{{row.fio}}</small> </td>
                <td> <small >{{row.user.phone_number}}</small> </td>
                <td> <small >{{row.user.group_name}}</small> </td>
                <td> <small >{{row.user.familiya}}</small> </td>
                <td>
                  <small v-if="row.kirish" class="bg-success px-2 rounded text-white">{{row.kirish}}</small>
                  <small v-else class="bg-danger px-2 rounded text-white">--:--</small>
                </td>
                <td>
                  <small v-if="row.chiqish" class="bg-success px-2 rounded text-white">{{row.chiqish}}</small> 
                  <small v-else class="bg-danger px-2 rounded text-white">--:--</small> 
                </td>
                <td>
                  <small v-if="row.isLate" class=" px-2 rounded text-warning">Ishga kech kelgan</small>
                  <small v-else-if="row.kirish" class=" px-2 rounded text-success">Ishga keldi</small>
                  <small v-else class="px-2 rounded text-danger">Ishga kelmadi</small>
                </td>
                <td> <small >{{row.workedTime}}</small> </td>
                <td> <small >{{row.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                
              </tr>
            </tbody>
          </table>
        </div> -->
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
    const now = new Date();
      return{
        loading:false,
        modal_info : '',
        modal_status: false,
        selected: 0,
        get_payment_list: [],
        xodim_soni: 0,
        xodim_kelgan: 0,
        xodim_kelmagan: 0,
        xodim_kech_kel:0,
        select_status_list: [],
        selectedMonth: now.getMonth(), // 0-based
        selectedYear: now.getFullYear(), 
        months: [
        { value: 0, name: 'Yanvar' },
        { value: 1, name: 'Fevral' },
        { value: 2, name: 'Mart' },
        { value: 3, name: 'Aprel' },
        { value: 4, name: 'May' },
        { value: 5, name: 'Iyun' },
        { value: 6, name: 'Iyul' },
        { value: 7, name: 'Avgust' },
        { value: 8, name: 'Sentabr' },
        { value: 9, name: 'Oktabr' },
        { value: 10, name: 'Noyabr' },
        { value: 11, name: 'Dekabr' }
        ],
        id: this.$route.params.id,
        fio: '',
        oylik_name: '',
        dept_name: ''
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
      await this.fetch_userId();
    },
   
    computed:  {
      ...mapGetters([]),
     years() {
            const currentYear = new Date().getFullYear();
            const startYear = currentYear - 5;
            return Array.from({ length: 50 }, (_, i) => startYear + i);
        }
    },
    methods: {
      ...mapActions([]),
      ...mapMutations([]),
      async fetch_userId(){
        console.log(this.id);
        try{
          const res = await fetch(this.$store.state.hostname + '/SkudMyUserinfoes/' + this.id);
          const data = await res.json();
          console.log('this is by id')
          if(res.status == 200 || res.status == 201){
            console.log(data)
            this.fio = data.ism;
            this.dept_name = data.group_name;
            if(data.res_badgenumber){
              await this.fetch_oylikId(data.res_badgenumber)
            }
          }
        }
        catch(error){
            console.log(error)
        }
      },
      async fetch_oylikId(oylik_id){
        try{
          const res = await fetch(this.$store.state.hostname + '/SkudOyliks/' + oylik_id);
          const data = await res.json();
          if(res.status == 200 || res.status == 201){
            console.log(data)
            this.oylik_name = data.name;
          }
        }
        catch(error){
            console.log(error)
        }
      },
      async refreshCalendar() {
       
      },

      async fetchUserAccess(id){
        try{
            const res = await fetch(this.$store.state.hostname + '/TegirmonUserAccess/getTegirmonUserAccessUserId?user_id=' + id);
            const data = await res.json();
            console.log('this is by id')
            if(res.status == 200 || res.status == 201){
                console.log(data)
                if(data.status_1 == false){
                  this.$refs.blocked.show_block();
                }
            }
        }
        catch(error){
            console.log(error)
        }
      },

      async selectOption(option){
        this.smena_name = option.smena_nomi;
        this.smena_id = option.id;
        await this.apply();
      },

      async selectOptionDept(option){
        this.dept_name = option.deptname;
        this.dept_id = option.deptid;
        await this.apply();
      },


      async change_user_status(type){
        if(type == 1){
          this.select_status_list = this.get_payment_list.filter(item => item.kirish)
        }
        else if(type == 0){
          this.select_status_list = this.get_payment_list.filter(item => item.kirish == '')
        }
        else if(type == 2){
          this.select_status_list = this.get_payment_list.filter(item => item.isLate)
        }
      },
      async selectInvoiceItem(option){
        console.log(option)
      
      },
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
</style>
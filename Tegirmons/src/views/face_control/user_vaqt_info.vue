<template>
  <div>
    <div class="card user_info_card p-2">
      <div class="user_info_header border-bottom px-2">
        <p class="m-0" style="font-size: 14px;"
          v-if="client_info">{{ client_info.ism }} ({{ choosen_day_date }} ) 
          <span class="text-success">{{ choosen_day.hours }}</span></p>
      </div>
      <div class="border-bottom text-center px-3 alert-success" style="font-style: italic;">
        <p class="m-0" style="font-size: 13px;">
          Pul tushgan vaqtlari
        </p>
      </div>
      <div v-if="user_ishlagan_puli_list.length>0">
        <div class="TablePatientDocId p-0">
          <table class="myTableuserSalaryList">
            <thead class=" rounded" style="background:#C8DDE3">
                <tr class="header">
                  <th width="40">№</th>
                  <th >Kirish</th>
                  <th >Chiqish</th>
                  <th >Vaqt</th>
                  <th >Summa</th>
                  <th  width="80">{{$t('Action')}}</th>
              </tr>
              </thead>
            <tbody>
              <tr v-for="(row, index) in user_ishlagan_puli_list" :key="index">
                  <td >
                    {{ index+1 }}
                  </td>
                  <td>
                    {{row.k_date.slice(0,10)}} <span>{{ row.k_date.slice(11,16) }}</span>
                  </td>
                  <td>
                    {{row.created_date.slice(0,10)}} <span>{{ row.created_date.slice(11,16) }}</span>
                  </td>
                  <td>{{row.work_time}}</td>
                  <td v-if="row.sum">{{row.sum.toFixed(0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</td>
                  <td v-else>{{row.sum}}</td>
                  <td>
                  <i class="fas fa-pen editIcon mask waves-effect  m-0 px-2" v-on:click="Select_work_time(row)"></i>
                  </td>
              </tr>
              </tbody>
          </table>
        </div>
      </div>
      <div v-else class=" text-center px-3" style="font-style: italic;">
        <p class="m-0 text-danger" style="font-size: 13px;">
          Ma'lumot topilmadi!
        </p>
      </div>
      <div class=" text-center px-3 mt-2 alert-success border-bottom"  style="font-style: italic;">
        <p class="m-0 " style="font-size: 13px;">
          Xodimning kirdi chiqdi ma'lumotlari
        </p>
      </div>
      <div v-if="usercheck_in_out.length>0">
        <div class="TablePatientDocId p-0">
          <table class="myTableuserSalaryList">
            <thead class=" rounded" style="background:#C8DDE3">
                <tr class="header">
                  <th width="40">№</th>
                  <th width="400">FIO</th>
                  <th >Vaqt</th>
                  <th >Status</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row, index) in usercheck_in_out" :key="index">
                  <td >
                    {{ index+1 }}
                  </td>
                  <td v-if="row.userinfo">
                    {{ row.userinfo.ism }}
                  </td>
                  <td>
                    {{row.sana.slice(0,10)}} <span>{{ row.checktime }}</span>
                  </td>
                  <td v-if="row.checktype == 'K'" class="text-success">Kirish</td>
                  <td v-else class="text-primary">Chiqish</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      <div v-else class=" text-center px-3" style="font-style: italic;">
        <p class="m-0 text-danger" style="font-size: 13px;">
          Ma'lumot topilmadi!
        </p>
      </div>
    </div>
    <modal-train  :show="show" headerbackColor="#C9DCE4"  titlecolor="black" title="Ishlagan vaqtni uzgartirish" 
      @close="show = false" width="35%">
        <template v-slot:body>
          <div class="m-2">
            <div class=" px-3 bg-success rounded" style="font-style: italic;">
              <p class="m-0 text-white" style="font-size: 14px;">
                Kirish vaqti
              </p>
            </div>
            <div class="d-flex m-0 px-3 mt-2">
              <mdb-input class="m-0 p-0" type="date" size="sm"  v-model="kirish_date" outline/>
              <mdb-input class="m-0 p-0 ml-2" type="time" size="sm"  v-model="kirish_time" outline/>
            </div>

            <div class=" px-3 bg-primary rounded" style="font-style: italic;">
              <p class="m-0 text-white" style="font-size: 14px;">
                Chiqish vaqti
              </p>
            </div>
            <div class="d-flex m-0 px-3 mt-2">
              <mdb-input class="m-0 p-0" type="date" size="sm"  v-model="chiqish_date" outline/>
              <mdb-input class="m-0 p-0 ml-2" type="time" size="sm"  v-model="chiqish_time" outline/>
            </div>
            <hr class="my-1">
            <div class="d-flex justify-content-between w-100">
              <div class="d-flex px-3 mt-1">
                <mdb-input class="m-0 p-0 " type="time" size="sm"  v-model="ishlagan_vaqt" outline/>
                <!-- <p class="m-0 mt-2 ml-4" style="font-size: 14px;">
                  {{ishlagan_puli.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </p> -->
              </div>
              <mdb-btn @click="save_vaqt_change()" color="success py-2 px-3" style="font-size:9px;">
                {{$t('save')}}
              </mdb-btn>
            </div>
          </div>
        </template>
    </modal-train>
  </div>
</template>

<script>
  import { mdbBtn, mdbInput } from 'mdbvue';

export default {
  components: {
      mdbBtn,
      mdbInput,
    },
props:{
    client_info: {
    type:Object,
    default() {
        return {}
    }
    }
},
data(){
    return{
        user_ishlagan_puli_list: [],
        usercheck_in_out: [],
        user_oylik_info: [],
        choosen_day: {},
        choosen_day_date: '',
        show:false,

        kirish_date: null,
        kirish_time: null,
        chiqish_date: null,
        chiqish_time: null,
        ishlagan_vaqt: null,
        ishlagan_puli: 0
    }
},
watch: {
  kirish_date: 'hisoblaIshVaqti',
  kirish_time: 'hisoblaIshVaqti',
  chiqish_date: 'hisoblaIshVaqti',
  chiqish_time: 'hisoblaIshVaqti',
},
methods:{
    async handleChooseDay(data){
      this.choosen_day = data;
      this.usercheck_in_out = [];
      this.user_ishlagan_puli_list = [];
    //   if(!this.client_info.userid){
    //     this.$refs.message.error('not_found_user');
    //     return;
    //   };
    console.log('ishladi kirdi', data)
      const formatted = data.date.getFullYear() + "-" +
      String(data.date.getMonth() + 1).padStart(2, '0') + "-" +
      String(data.date.getDate()).padStart(2, '0');

      this.choosen_day_date = formatted;
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonUserIshlaganPuli/getUserSalaryByDay?userid=" + this.client_info.userid + '&date=' + formatted);
        const data = await response.json();
        console.log('puli',data)
        if(response.status == 200 || response.status == 201){
          this.user_ishlagan_puli_list = data;
        }
        else{
          this.user_ishlagan_puli_list = [];
        }
      }
      catch(error){
        this.user_ishlagan_puli_list = [];
        this.$refs.message.error("Foydalanuvchida " + formatted + " ushbu kun uchun ma'lumot topilmadi");
        console.log(error)
      }

      await this.getCheckInOut(formatted);

      console.log(formatted)
    },

    async Select_work_time(item){
      console.log(item);
      this.kirish_date = item.k_date.slice(0,10);
      this.kirish_time = item.k_date.slice(11,16);
      this.chiqish_date = item.created_date.slice(0,10);
      this.chiqish_time = item.created_date.slice(11,16);
      this.ishlagan_vaqt = item.work_time;
      this.ishlagan_puli = item.sum.toFixed();
      this.show = true;
    },

    hisoblaIshVaqti() {
      if (this.kirish_date && this.kirish_time && this.chiqish_date && this.chiqish_time) {
        const kirish = new Date(`${this.kirish_date}T${this.kirish_time}`);
        const chiqish = new Date(`${this.chiqish_date}T${this.chiqish_time}`);

        if (chiqish > kirish) {
          const diffMs = chiqish - kirish; // milliseconds
          const seconds = Math.floor(diffMs / 1000);
          const hours = Math.floor(seconds / 3600);
          const minutes = Math.floor((seconds % 3600) / 60);
          const secs = seconds % 60;

          this.ishlagan_vaqt = `${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}:${secs.toString().padStart(2, '0')}`;
        } else {
          this.ishlagan_vaqt = "00:00:00";
        }
      }
    },

    async save_vaqt_change(){
      console.log('change')
    },

    async getCheckInOut(date){
      try{
        const response = await fetch(this.$store.state.hostname + "/SkudMyCheckinouts/getPaginationGetByDateTimeByUserID?page=0&size=100&date=" + date + '&user_id=' + this.client_info.userid);
        const data = await response.json();
        console.log('kirdi chiqdi',data)
        if(response.status == 200 || response.status == 201){
          this.usercheck_in_out = data.items_list;
        }
        else{
          this.usercheck_in_out = [];
        }
      }
      catch(error){
        this.usercheck_in_out = [];
        // this.$refs.message.error("Foydalanuvchida " + formatted + " ushbu kun uchun ma'lumot topilmadi");
        console.log(error)
      }
    },
}
}
</script>

<style lang="scss" scoped>
.userSalaryTable{
  height: 300px;
  overflow: hidden;
  overflow-y: scroll;
  // border: 1px solid #757575;
}
.myTableuserSalaryList {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 16px;
  max-height:80px; overflow-x:auto
}
.myTableuserSalaryList th{
  font-weight: 600;
  font-size:11px;
}
.myTableuserSalaryList td{
  font-size:11.5px;
  
}
.myTableuserSalaryList tbody td small{
  font-size:11px;
}
.myTableuserSalaryList td {
  text-align: left;
  padding: 3px 10px;
}
.myTableuserSalaryList th{
  text-align: left;
  padding: 5px 10px;
}

.myTableuserSalaryList tr {
  border-bottom: 1px dashed rgb(240, 240, 240);
  &:hover{
    background: #afd1fd;
  }
}

.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
.editIcon{
  color:#01b348;
  font-size: 13px;
}
.mainWorkChange{
  // border: 1px solid black;
  min-height: calc(100vh - 500px);
}
.hover_info{
  display: none;
}

.border_dashed{
  border-bottom: 1px dashed #ddd;
}
.bg_red_color{
  background: #ff937d !important;
}
.bg_danger{
  background: #fd4925 !important;
}
</style>
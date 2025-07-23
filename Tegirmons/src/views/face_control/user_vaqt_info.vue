<template>
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
                                  <td>{{row.sum.toFixed()}}</td>
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
                    </div>
</template>

<script>
export default {
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
    }
},
methods:{
    async handleChooseDay(data){
      this.choosen_day = data;
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
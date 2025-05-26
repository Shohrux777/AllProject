<template>
    <div class="bytimeCheckInOut">
      <loader v-if="loading"/>
        <div class="navbar_Nav_Edit d-flex justify-content-between"  container>
            <div class="d-flex align-items-center">
                <span to="#"> <span class="text-white">{{ $t('come_in_out') }}</span> </span>
            </div>
            <div >
                <div  class="excel d-flex  align-items-center">
                    <i class="fas fa-filter text-white"></i>
                    <div class="px-2"> 
                        <MDBInput type="text" v-model="search" size="sm" label="Search" style="color:white !important;"/>
                    </div>
                    <div class="px-2 btn_hover"> 
                      <download-excel
                        :data   = "filteredList"
                        :fields = "json_fields"
                        name="Зарплата отчета.xls">
                        <small class="text-white" style="font-size: 12px;">
                            <MDBIcon  icon="file-excel" />
                            EXCEL
                        </small> 
                      </download-excel>
                        
                    </div>
                    <div class="px-2 btn_hover"> 
                        <small class="text-white" style="font-size: 12px;">
                            <MDBIcon  icon="file-pdf"  far/>
                            PDF
                        </small>
                    </div>
                </div>
            </div>
        </div>

        <div class="mainpage ">
          <div class="main_table">
            <div class="date_time_header d-flex">
              <div style="width: 20%;" class="px-2">
                <MDBInput v-model="Start_time" type="date" size="sm" label="e_date" />
              </div>
              <div style="width: 20%;" class="px-2">
                <MDBInput v-model="End_time" type="date" size="sm" label="e_date" />
              </div>
              <div style="width: 20%;" class="px-2" v-show="false">
                <erpSelectDoor
                  :options = "get_door_list.rows"
                  @select="sub_door_select"
                  :selected="door_name"
                  :label="$t('door')"
                  class="m-0 p-0"
                  style="margin-top: -30px !important; "
                />
              </div>
              <div style="width: 40%;" class="px-2">
                <MDBBtn style="font-size: 9px;" @click="submit" color="primary">OK</MDBBtn>
                <MDBBtn style="font-size: 9px;" @click="refresh" color="info">Refresh</MDBBtn>
              </div>
            </div>
            <div class="mt-3">
              <MDBTable class="align-middle mb-0 bg-white">
                  <thead class="bg-light">
                    <tr>
                      <th>№</th>
                        <th>{{$t('FIO')}}
                          <span style="position:relative;">
                              <span @click="sortedArrayAsc('full_name')"><MDBIcon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                              <span @click="sortedArray('full_name')"><MDBIcon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                            </span>
                        </th>
                        <th width="100">User ID
                          <span style="position:relative;">
                            <span @click="sortedArrayAsc('user_id')"><MDBIcon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                            <span @click="sortedArray('user_id')"><MDBIcon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                          </span>
                        </th>
                        <th>{{$t('comeinout')}}</th>
                        <th>{{$t('day')}}</th>
                        <th>{{$t('not_come')}}</th>
                    </tr>
                  </thead>
                  <tbody>
                  <tr v-for="(row, index) in filteredList" :key="index">
                    <td>{{ index + 1 }}</td>
                      <td>
                      <div class="d-flex align-items-center">
                          <div class="ms-3">
                          <p class="fw-bold mb-1" v-if="row.full_name != null">{{ row.full_name }}</p>
                          <!-- <p class="text-muted mb-0" v-if="row.familiya != null">{{ row.familiya }}</p> -->
                          </div>
                      </div>
                      </td>
                      <td style="width:60px;">
                        {{ row.user_id }}
                      </td>
                      <td :class="{'text-success': row.come == 'Пришел' , 'text-danger font-weight-bold': row.come == 'Не пришел'}">{{ row.come }}</td>
                      <td class="text-success ">{{row.working_days_count}}</td>
                      <td class="text-danger font-weight-bold">{{row.not_come}}</td>
                  </tr>
                  </tbody>
                </MDBTable>
            </div>
          </div>
        </div>
    </div>
</template>

<script>
import {
    MDBIcon,MDBInput, MDBBtn, MDBTable, MDBBadge,
  } from 'mdb-vue-ui-kit';
  import { ref } from 'vue';

import erpSelectDoor from '@/components/erpSelectDoor.vue'
import {mapActions, mapGetters} from 'vuex'
import Loader from '@/components/loader.vue';
import downloadExcel from "vue-json-excel3";

export default {
    setup() {
      const exampleModal = ref(false);
      return {
        exampleModal,
      };
    },
    components: {
        MDBIcon,
        MDBInput,
        MDBBtn,
        MDBTable,
        MDBBadge,
        erpSelectDoor,
        Loader,
        downloadExcel
    },
    
    data(){
      return{
        loading: false,
        door_name: '',
        door_id: null,
        Start_time: null,
        End_time: null,
        reportList: [],
        userWorkTime: [],
        search: '',
        json_fields: {
          'ФИО': 'full_name',
          'User ID': 'user_id',
          'Сутствующих': 'come',
          'Дни пришел' : 'working_days_count',
          "Дни не пришел": "not_come"
        }
      }
    },
    computed: {
      ...mapGetters(['get_door_list']),
      filteredList: function(){
        if(this.search)
        {
          return this.reportList.filter((item)=>{
            return this.search.toLowerCase().split(' ').every(v => item.full_name.toLowerCase().includes(v))
          })
        }else
        {
          return this.reportList;
        }
      }
    },
    async mounted(){
        await this.fetch_Door();
        let time1 = new Date();
        this.Start_time = time1.toISOString().slice(0,10); 
        this.End_time = time1.toISOString().slice(0,10);
        let start = this.Start_time + 'T00:00:35.000Z' ;
        let end = this.End_time + 'T00:00:00.000Z';
        await this.submit();
        console.log(start)
        console.log(end)
    },
    methods:{
      ...mapActions([ 'fetch_Door']),
      sub_door_select(option){
        console.log(option)
        this.door_name = option.acc_name;
        this.door_id = option.id;
      },
      async submit(){
        let start = this.Start_time + 'T00:00:35.000Z' ;
        let end = this.End_time + 'T00:00:00.000Z';
        console.log(start)
        console.log(end)
        console.log('submit')

        let fistDays = new Date(this.Start_time);
        let LastDays = new Date(this.End_time);
        var time_difference = LastDays.getTime() - fistDays.getTime();
        var days_difference = time_difference / (1000 * 60 * 60 * 24) + 1;
        
        // let last_date_minuss = LastDays.setDate(LastDays.getDate()+parseInt(this.days));

        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/SkudMyCheckinouts/getPaginationWithAllWorkingDaysInfo?page=0&size=1000&begin_date=" + start + '&end_date=' + end);
          const data = await response.json();
          this.loading = false;
          this.reportList = []
          console.log('data') 
          console.log(data) 
          for(let i=0; i<data.items_list.length; i++){
            console.log(data.items_list[i].working_days_count)
            let a = {
              full_name: data.items_list[i].full_name,
              user_id: data.items_list[i].user_id,
              working_days_count: data.items_list[i].working_days_count,
              come: '',
              not_come: 0,
            }
            a.working_days_count = data.items_list[i].working_days_count;
            if(days_difference>0){
              a.not_come = parseInt(days_difference) - parseInt(data.items_list[i].working_days_count)
            }
            if(data.items_list[i].cur_day_count == 0){
              a.come = 'Не пришел'
            }
            else{
              a.come = 'Пришел'
            }
            this.reportList.push(a);
          }

          // this.reportList = data.items_list;
        }
        catch(error){
          this.loading = false;
          console.log(error)
        }
      },
      timeConvert(n) {
        if(n==0) return '---'
        var hours = (n);
        var rhours = Math.floor(hours);
        var minutes = (hours - rhours) * 60;
        var rminutes = Math.floor(minutes);
        var secund = (minutes - rminutes) * 60;
        var rsecund = Math.round(secund);
        if(rhours<10){
          rhours = '0' + rhours.toString()
        }
        if(rminutes<10){
          rminutes = '0' + rminutes.toString()
        }
        if(rsecund<10){
          rsecund = '0' + rsecund.toString()
        }
        return rhours + ":" + rminutes + ":" + rsecund;
      },
      async refresh(){
        console.log('refresh')
      },
      sortedArrayAsc(key){
          function compare(a, b) {
            if (a[key] < b[key])
              return -1;
            if (a[key] > b[key])
              return 1;
            return 0;
          }
          this.filteredList.sort(compare);
      },
      sortedArray(key){
          function compare(a, b) {
            if (a[key] > b[key])
              return -1;
            if (a[key] < b[key])
              return 1;
            return 0;
          }

          this.filteredList.sort(compare);
      }
    },
    
}
</script>

<style>
.navbar_Nav_Edit{
    background: #475a65;
    padding: 7px 12px;
}
.btn_hover{
    cursor:pointer;
}
.btn_hover small{
    color:#d3d3d3 !important;
}
.btn_hover:hover{
    background: #3a464c;
    border-radius: 2px;
}
.btn_hover:hover small{
    color:#fff !important;
}
.mainpage{
      height: calc(100vh - 90px);
      overflow-y: scroll;
      padding:25px 15px;
  }
  .main_table{
    padding: 15px;
    box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
    border-radius: 5px;
}
.table_header{
      /* position:sticky; 
      top:-25px;
      z-index: 2; */
      background:#e2e2e2;
      
  }
  th,td{
      padding: 5px !important;
      font-size: 12.5px;
  }
</style>
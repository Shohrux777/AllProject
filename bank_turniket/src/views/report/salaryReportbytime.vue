<template>
    <div class="bytimeCheckInOut">
      <loader v-if="loading"/>
        <div class="navbar_Nav_Edit d-flex justify-content-between"  container>
            <div class="d-flex align-items-center">
                <span to="#"> <span class="text-white">Kirish Chiqish</span> </span>
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
                        <th>{{$t('FIO')}}
                          <span style="position:relative;">
                              <span @click="sortedArrayAsc('ism')"><MDBIcon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                              <span @click="sortedArray('ism')"><MDBIcon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                            </span>
                        </th>
                        <th width="100">User ID
                          <span style="position:relative;">
                            <span @click="sortedArrayAsc('userid')"><MDBIcon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                            <span @click="sortedArray('userid')"><MDBIcon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                          </span>
                        </th>
                        <th width="150">{{$t('time_work')}}</th>
                        <th>{{$t('day')}}</th>
                        <th>{{$t('type_salary')}}</th>
                        <th>{{$t('salary')}}</th>
                        <th width="60">{{$t('Action')}}</th>
                    </tr>
                  </thead>
                  <tbody>
                  <tr v-for="(row, index) in filteredList" :key="index">
                      <td>
                      <div class="d-flex align-items-center">
                          <div class="ms-3">
                          <p class="fw-bold mb-1" v-if="row.ism != null">{{ row.ism }}</p>
                          <p class="text-muted mb-0" v-if="row.familiya != null">{{ row.familiya }}</p>
                          </div>
                      </div>
                      </td>
                      <td style="width:60px;">
                        {{ row.userid }}
                      </td>
                      <!-- <td v-if="row.ishlagan_vaqtlar_ls.length>0"><small v-for="(item, i) in row.ishlagan_vaqtlar_ls" :key="i" style="display: block;">{{ item }}</small></td>
                      <td v-else class="text-danger">---</td> -->
                      
                      <td :class="{'text-danger': row.worked_hours_itm == '---'}">{{row.worked_hours_itm}}</td>

                      
                      <td>{{ row.day }}</td>

                      <td>{{row.oylik_nomi}}</td>
                      <td :class="{'text-success': row.ishlagan_puli != 0}" >
                        {{row.ishlagan_puli}}
                      </td>
                      <td class="text-center" @click="show_user_report(row.userid)" style="cursor: pointer;"><i class="fas fa-eye text-success"></i></td>
                  </tr>
                  </tbody>
                </MDBTable>
            </div>
          </div>
        </div>
        <MDBModal
            id="exampleModal"
            tabindex="-1"
            labelledby="exampleModalLabel"
            v-model="exampleModal"
            size="lg"
        >
            <MDBModalHeader style="background: #475a65;" class="text-white px-3 py-2">
                <MDBModalTitle id="exampleModalLabel"> {{$t('door_add')}} </MDBModalTitle>
            </MDBModalHeader>
            <MDBModalBody>
                <div class="userWorkTime">
                  <MDBTable class="align-middle mb-0 bg-white">
                    <thead class="bg-light">
                      <tr>
                          <th>№</th>
                          <th>{{$t('FIO')}}
                          </th>
                          <th width="100">User ID
                          </th>
                          <th width="150">{{$t('time_work')}}</th>
                          <th>{{$t('date')}}</th>
                      </tr>
                    </thead>
                    <tbody>
                    <tr v-for="(row, index) in userWorkTime" :key="index" v-show="row.vaqt">
                        <td >{{index+1}}</td>
                        <td>
                        <div class="d-flex align-items-center">
                            <div class="ms-3">
                            <p class="fw-bold mb-1" v-if="row.ism != null">{{ row.ism }}</p>
                            <p class="text-muted mb-0" v-if="row.familiya != null">{{ row.familiya }}</p>
                          </div>
                        </div>
                        </td>
                        <td style="width:60px;">
                          {{ row.userid }}
                        </td>
                        <!-- <td v-if="row.ishlagan_vaqtlar_ls.length>0"><small v-for="(item, i) in row.ishlagan_vaqtlar_ls" :key="i" style="display: block;">{{ item }}</small></td>
                        <td v-else class="text-danger">---</td> -->
                        
                        <td >{{row.vaqt}}</td>

                        <td  >
                          {{row.kun}}
                        </td>
                    </tr>
                    </tbody>
                  </MDBTable>
                </div>
            </MDBModalBody>
        </MDBModal>
    </div>
</template>

<script>
import {
    MDBIcon,MDBInput, MDBBtn, MDBTable, MDBBadge,
    MDBModal,
    MDBModalHeader,
    MDBModalTitle,
    MDBModalBody,
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
        MDBModal,
        MDBModalHeader,
        MDBModalTitle,
        MDBModalBody,
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
          'ФИО': 'ism',
          'User ID': 'userid',
          'Раб. время': 'ishlagan_vaqtlar_ls',
          "Дни": 'day',
          'Тип зарплаты': 'oylik_nomi',
          'Зарплата' : 'ishlagan_puli'
        }
      }
    },
    computed: {
      ...mapGetters(['get_door_list']),
      filteredList: function(){
        if(this.search)
        {
          return this.reportList.filter((item)=>{
            return this.search.toLowerCase().split(' ').every(v => item.ism.toLowerCase().includes(v))
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
        let end = this.End_time + 'T23:59:59.000Z';
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
      async show_user_report(userId){
        this.exampleModal = true;
        let start = this.Start_time + 'T00:00:35.000Z' ;
        let end = this.End_time + 'T23:59:59.000Z';
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getReportWithoutSmenaPaginationOneUser?page=0&size=1000&begin_date=" + start + '&end_date=' + end + '&user_id=' + userId);
          const data = await response.json();
          this.loading = false;
          this.userWorkTime = []
          console.log('userIdReport') 
          console.log(data.items_list) 
          this.userWorkTime = data.items_list;
        }
        catch(error){
          this.loading = false;
          console.log(error)
        }
      },
      async submit(){
        let start = this.Start_time + 'T00:00:35.000Z' ;
        let end = this.End_time + 'T23:59:59.000Z';
        console.log(start)
        console.log(end)
        console.log('submit')
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getReportWithoutSmenaPaginationByAllUserAndOylik?page=0&size=1000&begin_date=" + start + '&end_date=' + end);
          const data = await response.json();
          this.loading = false;
          this.reportList = []
          console.log(data) 
          for(let i=0; i<data.items_list.length; i++){
            let a = {
              ism: data.items_list[i].ism,
              familiya: data.items_list[i].familiya,
              userid: data.items_list[i].userid,
              ishlagan_vaqtlar_ls: data.items_list[i].ishlagan_vaqtlar_ls,
              day: data.items_list[i].ishlagan_vaqtlar_ls.length,
              kun: data.items_list[i].kun,
              ishlagan_puli: data.items_list[i].ishlagan_puli.toFixed(0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
              oylik_nomi: data.items_list[i].oylik_nomi,
              worked_hours_itm: '',
            }
            a.worked_hours_itm = this.timeConvert(data.items_list[i].worked_hours_itm)
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
<template>
  <div class="bytimeCheckInOut">
    <loader v-if="loading"/>
      <div class="navbar_Nav_Edit d-flex justify-content-between"  container>
          <div class="d-flex align-items-center">
              <span to="#"> <span class="text-white">Ishlagan vaqti</span> </span>
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
          <div class="date_time_header d-flex mt-2 mb-0">
            <div style="width: 20%;" class="px-2">
              <MDBInput v-model="Start_time" type="date" size="sm" label="e_date" />
            </div>
            <div style="width: 20%;" class="px-2">
              <MDBInput v-model="End_time" type="date" size="sm" label="e_date" />
            </div>
            
            <div style="width: 20%;" class="px-2">
              <MDBBtn style="font-size: 9px;" @click="submit" color="primary">OK</MDBBtn>
              <MDBBtn style="font-size: 9px;" @click="refresh" color="info">Refresh</MDBBtn>
            </div>
            <div style="width: 60%;" class="px-0 d-flex" >
              <div style="width:300px;">
                <erpSelectDoor
                  :options = "get_deparment_list.rows"
                  @select="sub_door_select"
                  :selected="dept_name"
                  :label="$t('otdel')"
                  class="m-0 p-0"
                  style="margin-top: -30px !important;"
                />
              </div>
              <MDBSwitch
                label="Почасовая"
                v-model="switch1"
              />
              <MDBSwitch
                label="Дневная"
                v-model="switch2"
              />
              
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
                    <td :class="{'text-success': row.ishlagan_puli != 0}" v-if="row.without_gr_id == 2 && row.oylik_haqqi != 0">
                      {{(row.oylik_haqqi * row.day).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                    </td>
                    <td :class="{'text-success': row.ishlagan_puli != 0}" v-else>
                      {{row.ishlagan_puli}}
                    </td>
                    <td class="text-center" >
                      <span class="mx-2" style="display: inline-block;"><i class="fas fa-arrow-alt-circle-left text-primary " @click="show_user_details_report(row.userid)" style="cursor: pointer;"></i></span>
                      <span class="mx-2" style="display: inline-block;"><i class="fas fa-eye text-success" @click="show_user_report(row.userid)" style="cursor: pointer;"></i></span>
                    </td>
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
                  <tr v-for="(row, index) in userWorkTime" :key="index" >
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
                      
                      <td>{{row.worked_hours_itm_str}}</td>

                      <td>
                        {{row.kun}}
                      </td>
                  </tr>
                  </tbody>
                </MDBTable>
              </div>
          </MDBModalBody>
      </MDBModal>

      <MDBModal
          id="exampleModal"
          tabindex="-1"
          labelledby="exampleModalLabel"
          v-model="exampleModal1"
          size="xl"
      >
          <MDBModalHeader style="background: #475a65;" class="text-white px-3 py-2">
              <MDBModalTitle id="exampleModalLabel"> {{$t('door_add')}} </MDBModalTitle>
          </MDBModalHeader>
          <MDBModalBody>
              <div class="userWorkTime" v-for="(item,i) in list_of_in_out" :key="i">
                <div class="my-2 px-4 alert-danger" >
                  <span>{{ item.kun }}</span>
                  <span class="mx-3"> Ishlagan vaqti: <span class="mx-2 text-success" >{{ userWorkTime[i].worked_hours_itm_str }}</span></span>
                </div>
                <MDBTable class="align-middle mb-0 bg-white">
                  <thead class="bg-light">
                    <tr>
                        <th>№</th>
                        <th>{{$t('FIO')}}
                        </th>
                        <th width="100">User ID
                        </th>
                        <th>{{$t('checktype')}}</th>
                        <th>{{$t('door_name')}}</th>
                        <th>{{$t('date')}}</th>
                        <th>{{$t('time')}}</th>
                    </tr>
                  </thead>
                  <tbody>
                  <tr v-for="(row, index) in item.list" :key="index+1000" >
                      <td >{{index+1}}</td>
                      <td>
                      <div class="d-flex align-items-center">
                          <div class="ms-3">
                          <p class="fw-bold mb-1" v-if="row.fio != null">{{ row.fio }}</p>
                          <p class="text-muted mb-0" v-if="row.userinfo.familiya != null">{{ row.userinfo.familiya }}</p>
                        </div>
                      </div>
                      </td>
                      <td style="width:60px;">
                        {{ row.userid }}
                      </td>
                      <td>
                        <MDBBadge v-if="row.checktype == 'I' || row.checktype == 'K'" badge="success" pill class="d-inline">Входить</MDBBadge>
                        <MDBBadge v-else badge="warning" pill class="d-inline">Выход</MDBBadge>
                      </td>
                      <td>{{row.door_name}}</td>

                      <td>
                        {{row.sana.slice(0,10)}}
                      </td>
                      <td>
                        {{row.checktime}}
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
  MDBSwitch 
} from 'mdb-vue-ui-kit';
import { ref } from 'vue';

import erpSelectDoor from '@/components/erpSelectAdd.vue'
import {mapActions, mapGetters} from 'vuex'
import Loader from '@/components/loader.vue';
import downloadExcel from "vue-json-excel3";

export default {
  setup() {
    const exampleModal = ref(false);
    const exampleModal1 = ref(false);
    const switch1 = ref(false);
    const switch2 = ref(false);
    return {
      exampleModal,
      exampleModal1,
      switch1,
      switch2
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
      downloadExcel,
      MDBSwitch
  },
  
  data(){
    return{
      loading: false,
      dept_name: '',
      dept_id: null,
      Start_time: null,
      End_time: null,
      reportList: [],
      main_reportList: [],
      userWorkTime: [],
      search: '',
      json_fields: {
        'ФИО': 'ism',
        'User ID': 'userid',
        'Раб. время': 'ishlagan_vaqtlar_ls',
        "Дни": 'day',
        'Тип зарплаты': 'oylik_nomi',
        'Зарплата' : 'ishlagan_puli'
      },
      list_of_in_out: [],
    }
  },
  computed: {
    ...mapGetters(['get_door_list', 'get_deparment_list']),
    filteredList: function(){
      let items = this.reportList;
      if(this.search)
      {
        items = items.filter( item => {
          return this.search.toLowerCase().split(' ').every(v => item.ism.toLowerCase().includes(v))
        })
      }
      if(this.dept_id != null){
        items = items.filter( item => {
          return item.departid == this.dept_id;
        })
      }
      if(this.switch1 && !this.switch2){
        let soatbay = 0;
        if(this.switch1){
          soatbay = 1;
        }
        items = items.filter( item => {
          return item.without_gr_id == soatbay;
        })
      }
      else if(this.switch2 && !this.switch1){
        let kunbay = 0;
        if(this.switch2){
          kunbay = 2;
        }
        items = items.filter( item => {
          return item.without_gr_id == kunbay;
        })
      }
      else if(this.switch2 && this.switch1){
        items = items.filter( item => {
          return item.without_gr_id == 1 || item.without_gr_id == 2;
        })
      }
      if(this.search == '' && this.dept_id == null && this.switch1 == false && this.switch2 == false)
      {
        items = this.reportList;
      }
      return items;
    },
  },
  async mounted(){
      await this.fetch_Department();
      let time1 = new Date();
      this.Start_time = time1.toISOString().slice(0,10); 
      this.End_time = time1.toISOString().slice(0,10);
      let start = this.Start_time;
      let end = this.End_time;
      await this.submit();
      console.log(start)
      console.log(end)
  },
  methods:{
    ...mapActions([ 'fetch_Door', 'fetch_Department']),
    sub_door_select(option){
      console.log(option)
      this.dept_name = option.name;
      this.dept_id = option.id;
      console.log(this.reportList);
      // this.reportList = this.main_reportList;
      // let dept_temp_list = [];
      // for(let i=0; i<this.reportList.length; i++){
      //   if(option.id == this.reportList[i].departid){
      //     dept_temp_list.push(this.reportList[i]);
      //   }
      // }
      // this.reportList = dept_temp_list;
    },
    async show_user_details_report(userId){
      // let start = this.Start_time ;
      console.log('hiy')
      this.list_of_in_out = [];
      this.userWorkTime = [];
      let start_date = new Date(this.Start_time)
      let end_date = new Date(this.End_time)
      console.log(start_date.toISOString().slice(0,10))
      // console.log(end_date)
      await this.fetch_user_details_input(userId, start_date.toISOString().slice(0,10))
      await this.fetch_user_work_use(userId, start_date.toISOString().slice(0,10))
      while(start_date < end_date){
        var next_date = start_date.setDate(start_date.getDate() + 1);
        console.log('next_date');
        console.log(next_date);
        start_date = new Date(next_date); 
        console.log(start_date);
        await this.fetch_user_details_input(userId, start_date.toISOString().slice(0,10))
        await this.fetch_user_work_use(userId, start_date.toISOString().slice(0,10))
      }
      // await this.fetch_user_work_use(userId)
      console.log('this.list_of_in_out')
      console.log(this.list_of_in_out)
      console.log(this.userWorkTime)
      this.exampleModal1 = true;

    },
    async fetch_user_details_input(id, date){
      let start = date ;
      // let end = date;
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getPaginationBy0404?page=0&size=1000&date_b=" + start + '&user_id=' + id);
        const data = await response.json();
        this.loading = false;
        console.log('userIdReport');
        console.log(data.items_list);
        if(response.status == 200 || response.status == 201){
          let temp_kun = {
            kun: date,
            list: data.items_list
          }
          this.list_of_in_out.push(temp_kun);
        }
      }
      catch(error){
        this.loading = false;
        console.log(error)
      }
    },

    async show_user_report(userId){
      // let start = this.Start_time ;
      console.log('hiy')
      this.userWorkTime = [];
      let start_date = new Date(this.Start_time)
      let end_date = new Date(this.End_time)
      console.log(start_date.toISOString().slice(0,10))
      // console.log(end_date)
      await this.fetch_user_work_use(userId, start_date.toISOString().slice(0,10))
      while(start_date < end_date){
        var next_date = start_date.setDate(start_date.getDate() + 1);
        console.log('next_date');
        console.log(next_date);
        start_date = new Date(next_date); 
        console.log(start_date);
        await this.fetch_user_work_use(userId, start_date.toISOString().slice(0,10))
      }
      // await this.fetch_user_work_use(userId)
      console.log('this.userWorkTime')
      console.log(this.userWorkTime)
      this.exampleModal = true;

    },
    async fetch_user_work_use(id, date){
      let start = date ;
      let end = date;
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getReportWithoutSmenaPaginationByAllUserAndOylik0404WithUserId?page=0&size=1000&begin_date=" + start + '&end_date=' + end + '&user_id=' + id);
        const data = await response.json();
        this.loading = false;
        console.log('userIdReport');
        console.log(data.items_list);
        if(response.status == 200 || response.status == 201){
          this.userWorkTime.push(data.items_list[0])
        }
        
        
      }
      catch(error){
        this.loading = false;
        console.log(error)
      }
    },
    async submit(){
      this.dept_name = '';
      this.dept_id = null;
      this.switch1 = false;
      this.switch2 = false;
      let start = this.Start_time;
      let end = this.End_time;
      console.log(start)
      console.log(end)
      console.log('submit')
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getReportWithoutSmenaPaginationByAllUserAndOylik0404New?page=0&size=1000&begin_date=" + start + '&end_date=' + end);
        const data = await response.json();
        this.loading = false;
        this.reportList = [];
        console.log('data itemlist') 
        console.log(data) 
        for(let i=0; i<data.items_list.length; i++){
          let b_day = 0;
          for(let j=0; j<data.items_list[i].ishlagan_vaqtlar_ls.length; j++){
            if(data.items_list[i].ishlagan_vaqtlar_ls[j] != '00:00:00'){
              b_day ++;
            }
          }
          let a = {
            ism: data.items_list[i].ism,
            familiya: data.items_list[i].familiya,
            userid: data.items_list[i].userid,
            ishlagan_vaqtlar_ls: data.items_list[i].ishlagan_vaqtlar_ls,
            // day: data.items_list[i].ishlagan_vaqtlar_ls,
            day: b_day,
            kun: data.items_list[i].kun,
            ishlagan_puli: data.items_list[i].ishlagan_puli.toFixed(0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
            oylik_nomi: data.items_list[i].oylik_nomi,
            worked_hours_itm: '',
            departid: data.items_list[i].departid,
            without_gr_id: data.items_list[i].without_gr_id,
            oylik_haqqi: 0
          }
          if(data.items_list[i].oylik_nomi != ''){
            a.oylik_haqqi = data.items_list[i].skudOylik.value;
          }
          if(data.items_list[i].without_gr_id == 2 && data.items_list[i].oylik_haqqi != 0){
            a.ishlagan_puli = a.oylik_haqqi * a.day;
          }
          a.worked_hours_itm = this.timeConvert(data.items_list[i].worked_hours_itm)
          this.reportList.push(a);
        }
        this.main_reportList = this.reportList;
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
      this.dept_name = '';
      this.dept_id = null;
      this.switch1 = false;
      this.switch2 = false;
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
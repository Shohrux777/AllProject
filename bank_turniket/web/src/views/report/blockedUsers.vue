<template>
    <div class="bytimeCheckInOut">
      <loader  v-if="loading"/>
        <div class="navbar_Nav_Edit d-flex justify-content-between"  container>
            <div class="d-flex align-items-center">
                <span to="#"> <span class="text-white">Заблокиров. пользователь</span> </span>
            </div>
            <div >
                <div  class="excel d-flex  align-items-center">
                    <i class="fas fa-filter text-white"></i>
                    <div class="px-2"> 
                        <MDBInput type="text" size="sm" v-model="search" label="Search" style="color:white !important;"/>
                    </div>
                    <div class="px-2 btn_hover">
                      <download-excel
                        :data   = "filteredList"
                        :fields = "json_fields"
                        name="Заблокиров. пользователь.xls">
                        <small class="text-white" style="font-size: 12px;">
                            <MDBIcon  icon="file-excel"/>
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
                    <div class="px-2 btn_hover" @click="unblockAllUser">
                        <small class="text-white" style="font-size: 12px;">
                            <MDBIcon  icon="unlock"  fas/>
                            {{ $t('unlock') }}
                        </small>
                    </div>
                </div>
            </div>
        </div>

        <div class="mainpage ">
          <div class="main_table">
            <div class="date_time_header d-flex" v-if="false">
              <div style="width: 20%;" class="px-2">
                <MDBInput v-model="Start_time" type="date" size="sm" label="e_date" />
              </div>
              <div v-show="false" style="width: 20%;" class="px-2">
                <MDBInput v-model="End_time" type="date" size="sm" label="e_date" />
              </div>
              <div style="width: 20%;" class="px-2">
                <erpSelectAdd
                  :options = "get_deparment_list.rows"
                  @select="sub_door_select"
                  :selected="dept_name"
                  :label="$t('otdel')"
                  class="m-0 p-0"
                  style="margin-top: -30px !important;"
                />
              </div>
              <div style="width: 40%;" class="px-2">
                <MDBBtn style="font-size: 9px;" @click="submit" color="primary">OK</MDBBtn>
                <MDBBtn style="font-size: 9px;" @click="fetchWithoutDept" color="info">Refresh</MDBBtn>
              </div>
            </div>
            <div>
              <MDBTable class="align-middle mb-0 bg-white">
                  <thead class="bg-light">
                    <tr>
                        <th width="40">№</th>
                        <th>{{$t('FIO')}} </th>
                        <!-- <th width="100">User ID</th> -->
                        <!-- <th width="100">{{$t('vaqt')}}</th> -->
                        <th>{{$t('otdel')}}</th>
                        <th width="60">{{$t('Action')}}</th>
                    </tr>
                  </thead>
                  <tbody>
                  <tr v-for="(row, index) in filteredList" :key="index">
                    <td>{{index+1}}</td>
                      <td>
                      <div class="d-flex align-items-center">
                          <div class="ms-3">
                          <p class="fw-bold mb-1" v-if="row.ism != null">{{ row.ism }}</p>
                          <!-- <p class="text-muted mb-0" v-if="row.familiya != null">{{ row.familiya }}</p> -->
                          </div>
                      </div>
                      </td>
                      <!-- <td style="width:60px;">
                        {{ row.userid }}
                      </td> -->
                      <!-- <td>
                      <MDBBadge v-if="row.checktype == 'I' || row.checktype == 'K'" badge="success" pill class="d-inline">Входить</MDBBadge>
                      <MDBBadge v-else badge="warning" pill class="d-inline">Выход</MDBBadge>
                      </td> -->
                      <!-- <td v-if="row.vaqt">{{row.vaqt}}</td> -->
                      <!-- <td v-else class="text-danger">---</td> -->
                      <td>{{row.dept}}</td>
                      <td @click="unblockUser(row)" class="text-center text-success" 
                          style="cursor:pointer;">
                        <i class="fas fa-unlock"></i>
                      </td>
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
    MDBIcon,MDBInput, MDBBtn, MDBTable, MDBBadge
  } from 'mdb-vue-ui-kit';
import erpSelectAdd from '@/components/erpSelectAdd.vue'
import {mapActions, mapGetters} from 'vuex'
import Loader from '@/components/loader.vue';
import downloadExcel from "vue-json-excel3";

export default {
    components: {
        MDBIcon,
        MDBInput,
        MDBBtn,
        MDBTable,
        MDBBadge,
        erpSelectAdd,
        Loader,
        downloadExcel
    },
    data(){
      return{
        dept_name: '',
        loading: false,
        dept_id: null,
        Start_time: null,
        End_time: null,
        reportList: [],
        search: '',
        json_fields: {
          'ФИО': 'ism',
          'Отдел': 'dept'
        }
      }
    },
    computed: {...mapGetters(['get_deparment_list']),
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
        await this.fetch_Department();
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
      ...mapActions([ 'fetch_Department']),
      sub_door_select(option){
        console.log(option)
        this.dept_name = option.name;
        this.dept_id = option.id;
      },
      async fetchWithoutDept(){
        let start = this.Start_time + 'T00:00:35.000Z' ;
        let end = this.End_time + 'T23:59:59.000Z';
        console.log(start)
        console.log(end)
        console.log('submit')
        try{
          const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getReportKemaganlarAllUsers?page=0&size=100&need_date=" + start);
          const data = await response.json();
          console.log(data)
          this.reportList = data.items_list;
        }
        catch(error){
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
          const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getPaginationGrEqualTwo?page=0&size=1000");
          const data = await response.json();
          this.loading = false;
          console.log(data)
          this.reportList = data.items_list;
        }
        catch(error){
          this.loading = false;
          console.log(error)
        }
      },
      async unblockAllUser(){
        this.loading = true;
        for(let i=0; i<this.filteredList.length; i++){
          await this.unblockUser(this.filteredList[i])
        }
        this.loading = false;

      },
      async unblockUser(item){
        console.log(item)
        console.log('unblocked')
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getUnBlokUsersListForAllDoorsWithUpdateGr?user_id_list=" + item.userid);
          const data = await response.json();
          this.loading = false;
          console.log(data)
          if(response.status == 200 || esponse.status == 201){
            await this.submit();
          }
          // this.reportList = data.items_list;
        }
        catch(error){
          this.loading = false;
          console.log(error)
        }
      },
      async refresh(){
        console.log('refresh')
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
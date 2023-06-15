<template>
    <div class="bytimeCheckInOut">
      <loader v-if="loading"/>
        <div class="navbar_Nav_Edit d-flex justify-content-between"  container>
            <div class="d-flex align-items-center">
                <span to="#"> <span class="text-white">Опоздавшие</span> </span>
            </div>
            <div>
                <div  class="excel d-flex  align-items-center">
                    <i class="fas fa-filter text-white"></i>
                    <div class="px-2"> 
                        <MDBInput type="text" size="sm" v-model="search" label="Search" style="color:white !important;"/>
                    </div>
                    <div class="px-2 btn_hover"> 
                      <download-excel
                        :data   = "filteredList"
                        :fields = "json_fields"
                        name="Пришел на работу.xls">
                        <small class="text-white" style="font-size: 12px;">
                            <MDBIcon icon="file-excel"/>
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
              <div style="width: 20%;" class="px-2" v-show="false">
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
                <MDBBtn style="font-size: 9px;" @click="refresh_submit" color="info">Refresh</MDBBtn>
              </div>
            </div>
            <div>
              <MDBTable class="align-middle mb-0 bg-white">
                  <thead class="bg-light">
                    <tr>
                        <th width="40">№</th>
                        <th>{{$t('FIO')}}</th>
                        <th>User ID</th>
                      
                        <th>{{$t('time')}}</th>
                    </tr>
                  </thead>
                  <tbody>
                  <tr v-for="(row, index) in filteredList" :key="index">
                      <td>{{index+1}}</td>
                      <td>
                      <div class="d-flex align-items-center">
                          <div class="ms-3">
                            <p class="fw-bold mb-1" v-if="row.ism != null">{{ row.ism }}</p>
                            <!-- <p class="text-muted mb-0" v-if="row.userinfo != null">{{ row.userinfo.familiya }}</p> -->
                            </div>
                        </div>
                      </td>
                      <td>{{row.userid}}</td>
                      <!-- <td>
                      <MDBBadge badge="danger" pill class="d-inline">Выход</MDBBadge>
                      </td> -->
                      <td>{{row.emp_checktime}}</td>
                      <!-- <td>{{row.sana.slice(0,10)}}</td>
                       -->
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
import erpSelectDoor from '@/components/erpSelectDoor.vue'
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
        erpSelectDoor,
        Loader,
        downloadExcel
    },
    data(){
      return{
        door_name: '',
        door_id: null,
        Start_time: null,
        End_time: null,
        reportList: [],
        search: '',
        loading: false,

        json_fields: {
          'ФИО': 'ism',
          'User ID': 'userid',
          'Время': 'emp_checktime'
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
        let start = this.Start_time;
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
      async refresh_submit(){
        this.door_name = '';
        this.door_id = null;
        await this.submit();
      },
      async submit(){
        let start = this.Start_time;
        let end = this.End_time + 'T23:58:58.000Z';
        console.log(start)
        console.log(end)
        console.log('submit')
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getPaginationVaqtBoyichaVaSanaBoyichaSartirofka?page=0&size=10000&cur_date_str=" + start + '&taqqoslash_vaqti=' + '09:00:00');
          const data = await response.json();
          this.loading = false;
          console.log('datadsd dsds')
          console.log(data)
          
          // this.reportList = data.items_list;
          this.reportList = []
            for(let i=0; i<data.items_list.length; i++){
              if(data.items_list[i].taqqoshlash_status > 0){
                this.reportList.push(data.items_list[i])
              }
            }
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
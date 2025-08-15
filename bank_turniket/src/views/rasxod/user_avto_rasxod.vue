<template>
    <div class="bytimeCheckInOut">
      <loader v-if="loading"/>
        <div class="navbar_Nav_Edit d-flex justify-content-between"  container>
            <div class="d-flex align-items-center">
                <span to="#"> <span class="text-white">{{ $t('user_add_remove_day_list') }}</span> </span>
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
            <div class="mt-3">
              <MDBTable class="align-middle mb-0 bg-white">
                  <thead class="bg-light">
                    <tr>
                      <th>№</th>
                        <th>{{$t('FIO')}}
                          <span style="position:relative;">
                              <span @click="sortedArrayAsc('user_name')"><MDBIcon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                              <span @click="sortedArray('user_name')"><MDBIcon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                            </span>
                        </th>
                        <th width="100">User ID
                          <span style="position:relative;">
                            <span @click="sortedArrayAsc('userid')"><MDBIcon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                            <span @click="sortedArray('userid')"><MDBIcon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                          </span>
                        </th>
                        
                        <th>День</th>
                        <th>Сумма</th>
                        <th>{{$t('note')}}</th>
                        <th>{{$t('date')}}</th>
                        <th  width="100">{{$t('action')}}</th>
                    </tr>
                  </thead>
                  <tbody>
                  <tr v-for="(row, index) in filteredList" :key="index">
                    <td>{{ index + 1 }}</td>
                      <td>
                      <div class="d-flex align-items-center">
                          <div class="ms-3">
                          <p class="fw-bold mb-1" v-if="row.user_name != null">{{ row.user_name }}</p>
                          <!-- <p class="text-muted mb-0" v-if="row.familiya != null">{{ row.familiya }}</p> -->
                          </div>
                      </div>
                      </td>
                      <td style="width:60px;">
                        {{ row.userid }}
                      </td>
                      
                      <td class="text ">{{row.day}}</td>
                      <td class="text ">{{row.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</td>
                      <td class="text ">{{row.note}}</td>
                      <td class="text">{{row.created_date_time.slice(0,10)}}</td>
                      <td class="text ">
                        <!-- <MDBIcon @click="edit_user_day_data(row)" icon="pen" class="text-primary"  style="cursor:pointer; padding: 0 15px !important;"/> -->
                        <MDBIcon  @click="deluser_day_data(row.id, row.user_name)" icon="trash" class="text-danger"  style="cursor:pointer;"/>

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
    MDBIcon,MDBInput, MDBBtn, MDBTable, MDBBadge,
   
    MDBSwitch 
  } from 'mdb-vue-ui-kit';
  import { ref } from 'vue';

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
        Loader,
        downloadExcel,
        MDBSwitch 
    },
    
    data(){
      return{
        show_add_day: false,
        day_time: null,
        day_num: 0,
        day_info: '',
        user_name: '',
        user_id: 0,
        id: 0,

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
            return this.search.toLowerCase().split(' ').every(v => item.user_name.toLowerCase().includes(v))
          })
        }else
        {
          return this.reportList;
        }
      }
    },
    async mounted(){
        // let time1 = new Date();
        // this.Start_time = time1.toISOString().slice(0,10); 
        // this.End_time = time1.toISOString().slice(0,10);
        // let start = this.Start_time + 'T00:00:35.000Z' ;
        // let end = this.End_time + 'T00:00:00.000Z';
        await this.submit();
        // console.log(start)
        // console.log(end)
    },
    methods:{
      ...mapActions([ 'fetch_Door']),
      sub_door_select(option){
        console.log(option)
        this.door_name = option.acc_name;
        this.door_id = option.id;
      },
      func_day_plus(){
        this.day_num = 1;
      },
      func_day_minus(){
        this.day_num = -1;
      },
      async deluser_day_data(del_id, user_name){
        if (confirm("Ты уверен? Удаляет Авто расход" + user_name) == true) {
            console.log(del_id)
            try{
                console.log('deleteFunc')
                const requestOptions = {
                    method : "delete",
                };
                const response = await fetch(this.$store.state.hostname + "/SkudUserAvtoRasxod/" + del_id, requestOptions);
                
                if(response.status == 200 || response.status == 201){
                    await this.submit();
                }
            }
            catch(error){
                console.log(error)
            }
          }
        
      },
      async submit(){
        // let last_date_minuss = LastDays.setDate(LastDays.getDate()+parseInt(this.days));
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/SkudUserAvtoRasxod/getPagination?page=0&size=200");
          const data = await response.json();
          this.loading = false;
          this.reportList = []
          console.log('data') 
          console.log(data) 
          this.reportList = data.items_list;
        }
        catch(error){
          this.loading = false;
          console.log(error)
        }
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

<style lang="scss" stoped>
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
  .add_num_day1{
    background: #fff;
    cursor:pointer;
    width: 50px;
    display: flex;
    justify-content:center;
    &:hover{
      background: rgb(128, 255, 128);
    }
  }
  .add_num_day2{
    background: #fff;
    cursor:pointer;
    width: 50px;
    display: flex;
    justify-content:center;
    &:hover{
      background: rgb(252, 120, 120);
    }
  }
</style>
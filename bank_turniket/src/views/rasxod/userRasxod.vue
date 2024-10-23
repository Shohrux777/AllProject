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
                        <th>Расходь сумма</th>
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
                      <td v-if="row.count == 1" class="text-success fw-bold">{{row.count}}</td>
                      <td v-else class="text-danger fw-bold">{{row.count}}</td>
                      <td class="text ">{{row.note}}</td>
                      <td class="text">{{row.reg_date.slice(0,10)}}</td>
                      <td class="text ">
                        <MDBIcon @click="edit_user_day_data(row)" icon="pen" class="text-primary"  style="cursor:pointer; padding: 0 15px !important;"/>
                        <MDBIcon  @click="deluser_day_data(row.id, row.user_name)" icon="trash" class="text-danger"  style="cursor:pointer;"/>

                      </td>

                  </tr>
                  </tbody>
                </MDBTable>
            </div>
          </div>
        </div>
      <MDBModal
          id="exampleModal2"
          tabindex="2"
          labelledby="exampleModalLabel2"
          v-model="show_add_day"
      > 
          <MDBModalHeader style="background: #475a65;" class="text-white px-3 py-2">
              <MDBModalTitle id="exampleModalLabel"> {{$t('day_add')}} </MDBModalTitle>
          </MDBModalHeader>
          <MDBModalBody>
            <div class="px-3 row">
              <div class="col-12 mt-2 mb-3">
                <MDBInput v-model="day_time" type="date" size="sm" label="Дата" />
              </div>
              <div class="col-12 mb-3">
                <MDBInput v-model="day_num" type="number" size="sm" label="Расходь сумма" />
              </div>
              <div class="col-12 mb-3">
                <MDBInput v-model="day_info" type="text" size="sm" label="Примечания" />
              </div>
              <div class="col-12 mb-3 border-top pt-1">
                <div class="d-flex justify-content-end">
                  <MDBBtn style="font-size: 9px;" @click="submit_day_info" color="success">Сохранить</MDBBtn>
                </div>
              </div>
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
        downloadExcel,
        MDBModal,
        MDBModalHeader,
        MDBModalTitle,
        MDBModalBody,
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
      async edit_user_day_data(data){
        console.log(data)
        this.day_time = data.reg_date;
        this.day_num  = data.count;
        this.day_info = data.note;
        this.show_add_day = true;
        this.user_name = data.user_name;
        this.user_id = data.userid;
        this.id = data.id;
      },
      async submit_day_info(){
        try{
          const requestOptions = {
            method: "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
              "user_name" : this.user_name,
              "userid" : this.user_id,
              "reg_date" : this.day_time,
              "count": this.day_num,
              "note": this.day_info,
              "id": this.id
              })
            };
            const response = await fetch(this.$store.state.hostname + '/SkudUserRasxod', requestOptions);
            const data = await response.json();
            if(response.status == 200 || response.status == 201){
              console.log('malumot qushilyabdi')
              console.log(data)
              this.show_add_day = false;
              await this.submit();
            }
        }
        catch(error){
          console.log(error)
        }


        console.log('day_submit')
      },
      func_day_plus(){
        this.day_num = 1;
      },
      func_day_minus(){
        this.day_num = -1;
      },
      async deluser_day_data(del_id, user_name){
        if (confirm("Ты уверен? Удаляет расходь " + user_name) == true) {
            console.log(del_id)
            try{
                console.log('deleteFunc')
                const requestOptions = {
                    method : "delete",
                };
                const response = await fetch(this.$store.state.hostname + "/SkudUserRasxod/" + del_id, requestOptions);
                
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
        let start = this.Start_time + 'T00:00:35.000Z' ;
        let end = this.End_time + 'T00:00:00.000Z';
        console.log(start)
        console.log(end)
        console.log('submit')
        // let last_date_minuss = LastDays.setDate(LastDays.getDate()+parseInt(this.days));
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/SkudUserRasxod/getPaginationGetByDateTimeUser_add_day?page=0&size=10000&begin_date=" + start + '&end_date=' + end);
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
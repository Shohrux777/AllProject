<template>
    <div class="bytimeCheckInOut">
      <loader v-if="loading"/>
        <div class="navbar_Nav_Edit d-flex justify-content-between"  container>
            <div class="d-flex align-items-center">
                <span to="#"> <span class="text-white page_title">{{ $t('user_add_remove_day_list') }}</span> </span>
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
              <div style="width: 20%;" class="px-2 d-flex align-items-center gap_btns">
                <MDBBtn class="header_btn" @click="submit" color="primary">OK</MDBBtn>
                <MDBBtn class="header_btn" @click="refresh" color="info">Refresh</MDBBtn>
              </div>
              <div>
                <select class="form-select" v-model="select_status" style="height:33px;" @change="select_status_item" aria-label="Default select example">
                  <option selected value="3">Все</option>
                  <option value="0">Расходь сумма</option>
                  <option value="1">Зарплата сумма</option>
                </select>
              </div>
            </div>
            <div class="all_summa px-2">
              <div class="summary_item">
                <small class="summary_label">Общий сумма</small>
                <small class="summary_value">{{all_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small>
              </div>
            </div>
            <div class="mt-3 table_wrap">
              <MDBTable class="align-middle mb-0 bg-white rasxod_table">
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
                        <th>Сумма</th>
                        <th>Статус</th>
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
                      <td v-if="row.count" class="text-success fw-bold">{{row.count.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</td>
                      <td v-else class="text-danger fw-bold">{{row.count}}</td>
                      <td v-if="row.num_1 == 0" class="text "><MDBBadge badge="danger" pill>Расходь сумма</MDBBadge></td>
                      <td v-else class="text "><MDBBadge badge="success" pill>Зарплата сумма</MDBBadge></td>
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
          class="modern_modal"
      > 
          <MDBModalHeader class="modern_modal_header text-white px-3 py-2">
              <MDBModalTitle id="exampleModalLabel" class="modern_modal_title"> {{$t('day_add')}} </MDBModalTitle>
          </MDBModalHeader>
          <MDBModalBody class="modern_modal_body">
            <div class="px-3 row modern_modal_form">
              <div class="col-12 mt-2 mb-3 modern_field">
                <MDBInput v-model="day_time" type="date" size="sm" label="Дата" />
              </div>
              <div class="col-12 mb-3 modern_field">
                <MDBInput v-model="day_num" type="number" size="sm" label="Расходь сумма" />
              </div>
              <div class="col-12 mb-3 modern_field">
                <MDBInput v-model="day_info" type="text" size="sm" label="Примечания" />
              </div>
              <div class="col-12 mb-3 border-top pt-3">
                <div class="d-flex justify-content-end">
                  <MDBBtn class="modern_modal_btn" @click="submit_day_info" color="success">Сохранить</MDBBtn>
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
        all_summa: 0,

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
        },

        select_status: 3,
      }
    },
    computed: {
      ...mapGetters(['get_door_list']),
      filteredList: function(){
        let items = this.reportList;
        this.all_summa = 0;

        if(this.search)
        {
          items = items.filter((item)=>{
            return this.search.toLowerCase().split(' ').every(v => item.user_name.toLowerCase().includes(v))
          })
        }
        if(this.select_status != 3){
          items = items.filter( item => {
            return item.num_1 == this.select_status;
          })
        }
        this.all_summa = items.reduce((accumulator ,item) => {
          return accumulator += parseFloat(item.count);
        }, 0)
        return items;
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
        this.all_summa = 0;
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
          this.all_summa = data.items_list.reduce((accc ,item) => {
            return accc += parseFloat(item.count);
          }, 0)
          console.log('this.all_summa')
          console.log(this.all_summa)
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

<style lang="scss" scoped>
.bytimeCheckInOut{
  min-height: 100vh;
  background: linear-gradient(180deg, #f4f7fb 0%, #eef3f9 100%);
}
.navbar_Nav_Edit{
    background: linear-gradient(135deg, #334750 0%, #465d68 100%);
    padding: 10px 14px;
    border-bottom: 1px solid rgba(255, 255, 255, 0.12);
    box-shadow: 0 8px 20px rgba(27, 42, 52, 0.24);
    position: sticky;
    top: 0;
    z-index: 10;
}
.page_title{
    font-size: 15px;
    font-weight: 600;
    letter-spacing: 0.3px;
}
.btn_hover{
    cursor:pointer;
    border: 1px solid transparent;
    border-radius: 7px;
    transition: all .2s ease;
}
.btn_hover small{
    color:#d3d3d3 !important;
}
.btn_hover:hover{
    background: rgba(255, 255, 255, 0.12);
    border-color: rgba(255, 255, 255, 0.2);
}
.btn_hover:hover small{
    color:#fff !important;
}
.mainpage{
      height: calc(100vh - 82px);
      overflow-y: auto;
      padding:10px 15px;
  }
  .main_table{
    padding: 16px;
    background: #ffffff;
    border: 1px solid #e8eef6;
    box-shadow: 0 10px 30px rgba(35, 64, 96, 0.08);
    border-radius: 14px;
}
.date_time_header{
  background: #f8fbff;
  border: 1px solid #e4edf7;
  border-radius: 10px;
  padding: 6px 8px;
  align-items: center;
}
.gap_btns{
  gap: 8px;
}
.header_btn{
  font-size: 10px;
}
.all_summa{
  margin-top: 8px !important;
  background: #f8fbff;
  border: 1px dashed #d7e4f3;
  border-radius: 10px;
  display: flex;
  align-items: center;
  min-height: 54px;
}
.summary_item{
  min-width: 260px;
  background: #ffffff;
  border: 1px solid #e4edf8;
  border-radius: 8px;
  padding: 6px 10px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.summary_label{
  color: #5c7188;
  font-weight: 600;
}
.summary_value{
  color: #223648;
  font-size: 14px;
  font-weight: 700;
}
.table_wrap{
  border: 1px solid #e4edf7;
  border-radius: 12px;
  overflow: auto;
  max-height: calc(100vh - 280px);
}
.rasxod_table :deep(thead){
  position: sticky;
  top: 0;
  z-index: 1;
}
.rasxod_table :deep(th){
  background: #eef4fb !important;
  color: #2d4052;
  font-weight: 600;
  white-space: nowrap;
}
.rasxod_table :deep(tbody tr:hover){
  background: #f5faff !important;
}
.modern_modal :deep(.modal-content){
  border: none;
  border-radius: 14px;
  overflow: hidden;
  box-shadow: 0 18px 40px rgba(27, 42, 52, 0.25);
}
.modern_modal_header{
  background: linear-gradient(135deg, #3d5663 0%, #2f4551 100%);
  border-bottom: none;
}
.modern_modal_title{
  font-size: 15px;
  font-weight: 600;
}
.modern_modal_body{
  background: linear-gradient(180deg, #f9fcff 0%, #f3f8ff 100%);
}
.modern_modal_form{
  background: #ffffff;
  border: 1px solid #e5edf7;
  border-radius: 10px;
  padding-top: 8px;
  box-shadow: 0 6px 18px rgba(49, 79, 108, 0.08);
}
.modern_field :deep(input){
  border-radius: 8px;
  border-color: #cfdeef;
}
.modern_modal_btn{
  font-size: 11px;
  border-radius: 8px;
  padding: 6px 14px;
  font-weight: 600;
  letter-spacing: 0.2px;
}
th,td{
      padding: 8px 7px !important;
      font-size: 12.5px;
  }
</style>
<template>
  <div class="bytimeCheckInOut">
    <loader v-if="loading"/>
    <div class="navbar_Nav_Edit d-flex justify-content-between" container>
      <div class="d-flex align-items-center">
        <span to="#"> <span class="text-white page_title">Бонус список</span> </span>
      </div>
      <div>
        <div class="excel d-flex align-items-center">
          <i class="fas fa-filter text-white"></i>
          <div class="px-2">
            <MDBInput type="text" v-model="search" size="sm" label="Search" style="color:white !important;"/>
          </div>
          <div class="px-2 btn_hover">
            <download-excel :data="filteredList" :fields="json_fields" name="Бонус список.xls">
              <small class="text-white" style="font-size: 12px;">
                <MDBIcon icon="file-excel" />
                EXCEL
              </small>
            </download-excel>
          </div>
        </div>
      </div>
    </div>

    <div class="mainpage">
      <div class="main_table">
        <div class="date_time_header d-flex">
          <div style="width: 20%;" class="px-2">
            <MDBInput v-model="Start_time" type="date" size="sm" label="e_date" />
          </div>
          <div style="width: 20%;" class="px-2">
            <MDBInput v-model="End_time" type="date" size="sm" label="e_date" />
          </div>
          <div style="width: 20%;" class="px-2 d-flex align-items-center gap_btns">
            <MDBBtn class="header_btn" @click="submit" color="primary">OK</MDBBtn>
            <MDBBtn class="header_btn" @click="refresh" color="info">Refresh</MDBBtn>
          </div>
        </div>
        <div class="all_summa px-2">
          <div class="summary_item">
            <small class="summary_label">Общий сумма</small>
            <small class="summary_value">{{ all_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</small>
          </div>
        </div>
        <div class="mt-3 table_wrap">
          <MDBTable class="align-middle mb-0 bg-white bonus_table">
            <thead class="bg-light">
              <tr>
                <th>№</th>
                <th>{{$t('FIO')}}</th>
                <th width="100">User ID</th>
                <th>Сумма</th>
                <th>{{$t('note')}}</th>
                <th>{{$t('date')}}</th>
                <th width="100">{{$t('action')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row, index) in filteredList" :key="index">
                <td>{{ index + 1 }}</td>
                <td>{{ row.user_name }}</td>
                <td>{{ row.userid }}</td>
                <td class="text-success fw-bold">{{ row.count.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ') }}</td>
                <td>{{ row.note }}</td>
                <td>{{ row.reg_date.slice(0,10) }}</td>
                <td>
                  <MDBIcon @click="edit_bonus_data(row)" icon="pen" class="text-primary" style="cursor:pointer; padding: 0 15px !important;"/>
                  <MDBIcon @click="del_bonus_data(row.id, row.user_name)" icon="trash" class="text-danger" style="cursor:pointer;"/>
                </td>
              </tr>
            </tbody>
          </MDBTable>
        </div>
      </div>
    </div>

    <MDBModal id="exampleModalBonusList" tabindex="2" labelledby="exampleModalLabelBonusList" v-model="show_add_bonus" class="modern_modal">
      <MDBModalHeader class="modern_modal_header text-white px-3 py-2">
        <MDBModalTitle id="exampleModalLabelBonusList" class="modern_modal_title">Бонус qo'shish/tahrirlash</MDBModalTitle>
      </MDBModalHeader>
      <MDBModalBody class="modern_modal_body">
        <div class="px-3 row modern_modal_form">
          <div class="col-12 mt-2 mb-3 modern_field">
            <MDBInput v-model="day_time" type="date" size="sm" label="Дата" />
          </div>
          <div class="col-12 mb-3 modern_field">
            <MDBInput v-model="day_num" type="number" size="sm" label="Бонус сумма" />
          </div>
          <div class="col-12 mb-3 modern_field">
            <MDBInput v-model="day_info" type="text" size="sm" label="Примечания" />
          </div>
          <div class="col-12 mb-3 border-top pt-3">
            <div class="d-flex justify-content-end">
              <MDBBtn class="modern_modal_btn" @click="submit_bonus_info" color="success">Сохранить</MDBBtn>
            </div>
          </div>
        </div>
      </MDBModalBody>
    </MDBModal>
  </div>
</template>

<script>
import {
  MDBIcon, MDBInput, MDBBtn, MDBTable,
  MDBModal, MDBModalHeader, MDBModalTitle, MDBModalBody
} from 'mdb-vue-ui-kit';
import Loader from '@/components/loader.vue';
import downloadExcel from "vue-json-excel3";

export default {
  components: {
    MDBIcon, MDBInput, MDBBtn, MDBTable,
    MDBModal, MDBModalHeader, MDBModalTitle, MDBModalBody,
    Loader, downloadExcel
  },
  data(){
    return{
      show_add_bonus: false,
      day_time: null,
      day_num: 0,
      day_info: '',
      user_name: '',
      user_id: 0,
      id: 0,
      all_summa: 0,
      loading: false,
      Start_time: null,
      End_time: null,
      reportList: [],
      search: '',
      json_fields: {
        'ФИО': 'user_name',
        'User ID': 'userid',
        'Сумма': 'count',
        'Примечания': 'note',
        'Дата': 'reg_date'
      }
    }
  },
  computed: {
    filteredList(){
      let items = this.reportList;
      this.all_summa = 0;
      if(this.search){
        items = items.filter((item) => this.search.toLowerCase().split(' ').every(v => item.user_name.toLowerCase().includes(v)));
      }
      this.all_summa = items.reduce((acc, item) => acc += parseFloat(item.count || 0), 0);
      return items;
    }
  },
  async mounted(){
    const time1 = new Date();
    this.Start_time = time1.toISOString().slice(0,10);
    this.End_time = time1.toISOString().slice(0,10);
    await this.submit();
  },
  methods:{
    async edit_bonus_data(data){
      this.day_time = data.reg_date.slice(0,10);
      this.day_num = data.count;
      this.day_info = data.note;
      this.show_add_bonus = true;
      this.user_name = data.user_name;
      this.user_id = data.userid;
      this.id = data.id;
    },
    async submit_bonus_info(){
      try{
        const requestOptions = {
          method: "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            user_name: this.user_name,
            userid: this.user_id,
            reg_date: this.day_time,
            count: this.day_num,
            note: this.day_info,
            id: this.id
          })
        };
        const response = await fetch(this.$store.state.hostname + '/SkudUserBonus', requestOptions);
        if(response.status == 200 || response.status == 201){
          this.show_add_bonus = false;
          await this.submit();
        }
      } catch(error){
        console.log(error);
      }
    },
    async del_bonus_data(del_id, user_name){
      if (confirm("Ты уверен? Удаляет бонус " + user_name) == true) {
        try{
          const requestOptions = { method : "delete" };
          const response = await fetch(this.$store.state.hostname + "/SkudUserBonus/" + del_id, requestOptions);
          if(response.status == 200 || response.status == 201){
            await this.submit();
          }
        } catch(error){
          console.log(error);
        }
      }
    },
    async submit(){
      this.all_summa = 0;
      const start = this.Start_time + 'T00:00:00.000Z';
      const end = this.End_time + 'T23:59:59.000Z';
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/SkudUserBonus/getPaginationGetByDateTimeUser_bonus?page=0&size=10000&begin_date=" + start + '&end_date=' + end);
        const data = await response.json();
        this.loading = false;
        this.reportList = data.items_list || [];
        this.all_summa = this.reportList.reduce((acc, item) => acc += parseFloat(item.count || 0), 0);
      } catch(error){
        this.loading = false;
        console.log(error);
      }
    },
    async refresh(){
      await this.submit();
    }
  }
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
.btn_hover:hover{
  background: rgba(255, 255, 255, 0.12);
  border-color: rgba(255, 255, 255, 0.2);
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
.bonus_table :deep(thead){
  position: sticky;
  top: 0;
  z-index: 1;
}
.bonus_table :deep(th){
  background: #eef4fb !important;
  color: #2d4052;
  font-weight: 600;
  white-space: nowrap;
}
.bonus_table :deep(tbody tr:hover){
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

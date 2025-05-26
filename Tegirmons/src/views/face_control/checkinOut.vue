<template>
  <div class="bg-white mainDashboard" style="position:absolute;">
    <div class="border-bottom d-flex justify-content-between main_header_bg_shadow">
      <router-link to="/user">
         <h5 class="m-0 ml-3 d-flex" style="padding: 16px 0px">
            Dashboard</h5>
      </router-link>
     
    </div>
    <div class="p-3 shadow rounded" >
      <div class="bg-white p-3 mb-5 pt-0 shadow" style="border-radius:5px; position:relative;">
        <div class="TablePatientDocId p-0">
          <table class="myTable" >
            <thead class=" rounded" style="background:#C8DDE3">
                <tr class="header">
                  <th width="40">№</th>
                  <th width="300">{{$t('fio')}}</th>
                  <th >{{$t('id')}}</th>
                  <th>Статус</th>
                  <th>{{$t('door_name')}}</th>
                  <th>{{$t('date')}}</th>
                  <th>Время</th>
              </tr>
              </thead>
            <tbody>
              <tr v-for="(row, index) in get_dashboard_list.rows" :key="index">
                  <td >
                    {{ index+1 }}
                  </td>
                  <td>
                    <div class="d-flex align-items-center">
                        <img v-show="false" :src="picture_list[row.userid]" alt="" style="width: 45px; height: 45px"
                        class="rounded-circle" />
                        <div class="ms-3">
                        <p class="fw-bold mb-1" v-if="row.userinfo != null">{{ row.userinfo.ism }}</p>
                        <!-- <p class="text-muted mb-0" v-if="row.userinfo != null">{{ row.userinfo.familiya }}</p> -->
                        </div>
                    </div>
                  </td>
                  <td style="width:60px;">
                    {{ row.userid }}
                  </td>
                  <td>
                  <mdb-badge v-if="row.checktype == 'I'  || row.checktype == 'K'" color="success" pill class="d-inline">Входить</mdb-badge>
                  <mdb-badge v-else color="primary" pill class="d-inline">Выход</mdb-badge>
                  </td>
                  <td>{{row.door_name}}</td>
                  <td>{{row.sana.slice(0,10)}}</td>
                  <td>{{row.checktime}}</td>
              </tr>
              </tbody>
          </table>
        </div>
      </div>
    </div>

     
   
    <block ref="blocked"></block>
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>

  import { mdbBtn, mdbBadge, mdbInput, } from 'mdbvue';
  import {mapActions, mapGetters, } from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
       mdbInput, mdbBadge
      
    },
    data(){
      return{
        user_id: 0,
        show_otchert: false,

        show: false,
        admin:false,
        snipper: true,
        loading:false,
        editData: {},
        modal_info : '',
        modal_status: false,
        contragent_name: '',
        contragent_id: 0,
        Start_time: null,
        End_time: null,
        group_name: '',
        group_id: null,
        get_payment_list: [],
        index: null,
        service_id: null,

        rasxod: 0,
        picture_list: ['https://mdbootstrap.com/img/new/avatars/11.jpg', 'https://mdbootstrap.com/img/new/avatars/2.jpg',
          'https://mdbootstrap.com/img/new/avatars/8.jpg', 'https://mdbootstrap.com/img/new/avatars/5.jpg', 
      'https://mdbootstrap.com/img/new/avatars/4.jpg', 'https://mdbootstrap.com/img/new/avatars/3.jpg',
    'https://mdbootstrap.com/img/new/avatars/6.jpg', 'https://mdbootstrap.com/img/new/avatars/1.jpg']
       
      }
    },
    async mounted(){
        setInterval(this.fetch_dashboard_list, 3000);
          await this.fetch_dashboard_list();
          console.log(this.get_dashboard_list)
          console.log('this.get_dashboard_list')
    },
   
    computed: mapGetters(['get_dashboard_list']),
    methods:{
        ...mapActions(['fetch_dashboard_list']),
    },
  };
</script>

<style lang="scss">


.mainDashboard{
  height: 100vh !important;
  overflow: hidden;
  overflow-y: scroll;
}

.addxizmat{
  width: 470px;
  // height: 120px;
  background: #fff;
  position: relative;
  z-index: 5000;
}
.showing{
  display: none;
}
.timePicer{
  position: relative;
  margin-top: -10px;
  .timeLabel{
    position: absolute;
    font-size: 12px;
    background-color: #fff;
    padding: 1px 3px;
    z-index: 1;
    left: 6px;
    top: -1px;
  }
  .dayLabel{
    position: absolute;
    font-size: 12px;
    background-color: #fff;
    padding: 0px 3px;
    z-index: 1;
    left: 6px;
    top: -8px;
  }
}
.TablePatientDocId{
    // height: 400px;
    // overflow: hidden;
    // overflow-y: auto;
    // border: 1px solid #ddd;
  }
  .myTable {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; overflow-x:auto
}
.myTable th{
  font-weight: 600;
  font-size:12px;
}
.myTable td{
  font-size:13px;
}
.myTable th, .myTable td {
  text-align: left;
  padding: 7px;
}

.myTable tr {
  border-bottom: 1px solid rgb(240, 240, 240);
}

.myTable tr.header, .myTable tr:hover {
  // background-color: #f1f1f1;
}
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
</style>
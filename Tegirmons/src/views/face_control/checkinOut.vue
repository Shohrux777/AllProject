<template>
    <div class="deparment_page">
      <div class="mainpage">
          <div class="main_table">
            <table class="w-100  mb-0 bg-white">
              <thead class="bg-light rounded">
                <tr>
                    <th v-for="(column, i) in get_dashboard_list.columns" :key="i">{{$t(column)}}</th>
                </tr>
              </thead>
              <tbody>
              <tr v-for="(row, index) in get_dashboard_list.rows" :key="index">
                  
                  <td>
                  <div class="d-flex align-items-center">
                      <img v-show="false" :src="picture_list[row.userid]" alt="" style="width: 45px; height: 45px"
                      class="rounded-circle" />
                      <div class="ms-3">
                      <p class="fw-bold mb-1" v-if="row.userinfo != null">{{ row.userinfo.ism }}</p>
                      <p class="text-muted mb-0" v-if="row.userinfo != null">{{ row.userinfo.familiya }}</p>
                      </div>
                  </div>
                  </td>
                  <td style="width:60px;">
                    {{ row.userid }}
                  </td>
                  <!-- <td>
                  <p class="fw-normal mb-1">Software engineer</p>
                  <p class="text-muted mb-0">IT department</p>
                  </td> -->
                  <td>
                  <mdbBadge v-if="row.checktype == 'I'  || row.checktype == 'K'" badge="success" pill class="d-inline">Входить</mdbBadge>
                  <mdbBadge v-else badge="warning" pill class="d-inline">Выход</mdbBadge>
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
  </template>
  
  <script>
    import {
      mdbTable,
      mdbBtn,
      mdbBadge,
      mdbIcon,
      } 
      from 'mdbvue'
    import {mapActions, mapGetters} from 'vuex'
    export default {
      components: {
      mdbTable,
      mdbBtn,
      mdbBadge,
      mdbIcon,
      },
      data(){
          return{
              show_dept: false,
              added_status: false,
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
      }
  
    };
  </script>
  
  <style>
  .mainpage{
      height: calc(100vh - 85px);
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
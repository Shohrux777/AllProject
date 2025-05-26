<template>
  <div class="bg-white mainDashboard" style="position:absolute;">
    <div class="border-bottom d-flex justify-content-between main_header_bg_shadow">
      <router-link to="/user">
         <h5 class="m-0 ml-3 d-flex" style="padding: 16px 0px">
            Dashboard</h5>
      </router-link>
     
    </div>
    <div class="p-3 shadow rounded" >
      <form @submit.prevent="submit">
          <div style="height: 60px;" class="d-flex justify-content-between border-bottom align-items-center  ">
            <div class="title w-75 row align-items-center">
               <div class="col-4">
                <div style="position: relative; margin-top: 25px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
                    {{$t('start_time')}}
                  </small>
                  <mdb-input type="date" size="sm"  v-model="Start_time" outline/>
                </div>
              </div>
              <div class="col-4">
                <div style="position: relative; margin-top: 25px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
                    {{$t('end_time')}}
                  </small>
                  <mdb-input type="date" size="sm"   v-model="End_time" outline/>
                </div>
              </div>
            </div>
            <div class="plus">
              <mdb-btn @click="apply" color="primary py-2 px-3" style="font-size:9px;">
                <mdb-icon style="font-size:9.5px;" icon="check" class="m-0 p-0 mr-1" />
                {{$t('apply')}}
              </mdb-btn>
            </div>
          </div>
        </form>
      <div class="bg-white p-3 mb-5 pt-0 shadow" style="border-radius:5px; position:relative;">
        <div class="TablePatientDocId p-0">
          <table class="myTable">
            <thead class=" rounded" style="background:#C8DDE3;">
                <tr>
                      <th>{{$t('FIO')}}
                        <span style="position:relative;">
                            <span @click="sortedArrayAsc('ism')"><mdb-icon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                            <span @click="sortedArray('ism')"><mdb-icon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                          </span>
                      </th>
                      <th width="100">User ID
                        <span style="position:relative;">
                          <span @click="sortedArrayAsc('userid')"><mdb-icon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                          <span @click="sortedArray('userid')"><mdb-icon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                        </span>
                      </th>
                      <th width="150">{{$t('time_work')}}
                        <span style="position:relative;">
                          <span @click="sortedArrayAsc('worked_hours_itm')"><mdb-icon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                          <span @click="sortedArray('worked_hours_itm')"><mdb-icon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                        </span>
                      </th>
                      <th>{{$t('day')}}
                        <span style="position:relative;">
                          <span @click="sortedArrayAsc('day')"><mdb-icon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                          <span @click="sortedArray('day')"><mdb-icon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                        </span>
                      </th>
                      <!-- <th>День
                        <span style="position:relative;">
                          <span @click="sortedArrayAsc('day_count')"><mdb-icon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                          <span @click="sortedArray('day_count')"><mdb-icon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                        </span>
                      </th>
                      <th>Расходь
                        <span style="position:relative;">
                          <span @click="sortedArrayAsc('rasxod')"><mdb-icon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                          <span @click="sortedArray('rasxod')"><mdb-icon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                        </span>
                      </th> -->
                      <th>{{$t('type_salary')}}
                        <span style="position:relative;">
                          <span @click="sortedArrayAsc('oylik_nomi')"><mdb-icon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                          <span @click="sortedArray('oylik_nomi')"><mdb-icon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                        </span>
                      </th>
                      <th>{{$t('salary')}}
                        <span style="position:relative;">
                          <span @click="sortedArrayAsc('ishlagan_puli')"><mdb-icon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                          <span @click="sortedArray('ishlagan_puli')"><mdb-icon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                        </span>
                      </th>
                      <th width="110">{{$t('Action')}}</th>
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
                <td :class="{'text-danger': row.worked_hours_itm == '---'}">{{row.worked_hours_itm}}</td>
                <td>{{ row.day }}</td>
                <td>{{row.oylik_nomi}}</td>
                <td v-if="row.without_gr_id == 2" :class="{'text-success': row.ishlagan_puli != 0, 'text-danger': row.ishlagan_puli<0}" >
                  {{row.ishlagan_puli.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </td>
                <td v-else :class="{'text-success': row.ishlagan_puli != 0, 'text-danger': row.ishlagan_puli<0}" >
                  {{row.ishlagan_puli.toFixed(0).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                </td>
                <td class="text-center">
                  <!-- <span class="mx-1 action_info" style="display: inline-block;" @click="func_show_add_days(row.userid, row.ism, index)">
                    <i class="fa-solid fa-hourglass-start text-info" style="cursor: pointer;"></i>
                    <small>День</small>
                  </span>
                  <span class="mx-1 action_info" style="display: inline-block;" @click="func_show_add_rasxods(row.userid, row.ism, index)">
                    <i class="fa-regular fa-registered text-danger" style="cursor: pointer;"></i>
                    <small>Расходь</small>
                  </span> -->
                  <span class="mx-1" style="display: inline-block;"><i class="fas fa-arrow-alt-circle-left text-primary " @click="show_user_details_report(row.userid)" style="cursor: pointer;"></i></span>
                  <span class="mx-1" style="display: inline-block;"><i class="fas fa-eye text-success" @click="show_user_report(row.userid)" style="cursor: pointer;"></i></span>
                </td>
            </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>

    <modal-train  :show="details_show" headerbackColor="#4A5A64"  titlecolor="white" title="Keldi ketdi buyicha hisobot" 
      @close="details_show = false" width="60%">
        <template v-slot:body >
          <div class="userWorkTime p-2" v-for="(item,i) in list_of_in_out" :key="i">
            <div class="my-2 px-4 alert-danger" >
              <span>{{ item.kun }}</span>
              <span class="mx-3"> Ishlagan vaqti: <span class="mx-2 text-success" >{{ userWorkTime[i].worked_hours_itm_str }}</span></span>
            </div>
            <table class="myTable px-4 py-3">
              <thead style="background: #FBFBFB;">
                <tr>
                    <th>№</th>
                    <th>{{$t('fio')}}
                    </th>
                    <th width="100">ID
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
                      <!-- <p class="text-muted mb-0" v-if="row.userinfo.familiya != null">{{ row.userinfo.familiya }}</p> -->
                    </div>
                  </div>
                  </td>
                  <td style="width:60px;">
                    {{ row.userid }}
                  </td>
                  <td>
                    <mdb-badge v-if="row.checktype == 'I' || row.checktype == 'K'" color="success" pill class="d-inline">Входить</mdb-badge>
                    <mdb-badge v-else color="warning" pill class="d-inline">Выход</mdb-badge>
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
            </table>
          </div>
        </template>
    </modal-train> 
   <!-- <mdb-modal :show="details_show" @close="details_show = false" size="lg" class="text-center" color="info">
        <mdb-modal-header center :close="false">
          <p class="heading">{{$t('Are_you_sure')}}</p>
        </mdb-modal-header>
        <mdb-modal-body>
          <div class="userWorkTime" v-for="(item,i) in list_of_in_out" :key="i">
            <div class="my-2 px-4 alert-danger" >
              <span>{{ item.kun }}</span>
              <span class="mx-3"> Ishlagan vaqti: <span class="mx-2 text-success" >{{ userWorkTime[i].worked_hours_itm_str }}</span></span>
            </div>
            <table class="myTable">
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
                    <mdb-badge v-if="row.checktype == 'I' || row.checktype == 'K'" color="success" pill class="d-inline">Входить</mdb-badge>
                    <mdb-badge v-else color="warning" pill class="d-inline">Выход</mdb-badge>
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
            </table>
          </div>
          
        </mdb-modal-body>
    </mdb-modal> -->
<modal-train  :show="user_show" headerbackColor="#4A5A64"  titlecolor="white" title="Xodimlar qaysi kuni qancha soat ishlagani buyicha hisobot" 
      @close="user_show = false" width="60%">
        <template v-slot:body >
          <div class="TablePatientDocId p-3">
            <table class="myTable">
              <thead style="background: #FBFBFB;">
                <tr style="font-size: 12px;">
                    <th>№</th>
                    <th>{{$t('fio')}}
                    </th>
                    <th width="100">ID
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
                      <!-- <p class="text-muted mb-0" v-if="row.familiya != null">{{ row.familiya }}</p> -->
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
            </table>
          </div>
        </template>
    </modal-train> 
    
    <block ref="blocked"></block>
    <Toast ref="message"></Toast>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>

  import { mdbBtn, mdbBadge, mdbInput, mdbIcon,  mdbModal, mdbModalHeader,   mdbModalBody} from 'mdbvue';
  import {mapActions, mapGetters, } from 'vuex'
  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      mdbInput, mdbBadge, mdbIcon,
      mdbModal, mdbModalHeader,   mdbModalBody,
    },
    data(){
      return{
        show: false,
        details_show: false,
        user_show:false,
        admin:false,
        modal_status: false,
        modal_info: '',
        snipper: true,
        loading:false,
        Start_time: null,
        End_time: null,
        reportList: [],
        main_reportList: [],
        search: '',
        all_summa: 0,
        list_of_in_out : [],
        userWorkTime : [],
        
        picture_list: ['https://mdbootstrap.com/img/new/avatars/11.jpg', 'https://mdbootstrap.com/img/new/avatars/2.jpg',
          'https://mdbootstrap.com/img/new/avatars/8.jpg', 'https://mdbootstrap.com/img/new/avatars/5.jpg', 
      'https://mdbootstrap.com/img/new/avatars/4.jpg', 'https://mdbootstrap.com/img/new/avatars/3.jpg',
    'https://mdbootstrap.com/img/new/avatars/6.jpg', 'https://mdbootstrap.com/img/new/avatars/1.jpg']
       
      }
    },
    async mounted(){
        let time1 = new Date();
        this.Start_time = time1.toISOString().slice(0,10); 
        this.End_time = time1.toISOString().slice(0,10);
        await this.apply();
    },
   
    computed: {
      ...mapGetters(['get_dashboard_list']),
      filteredList: function(){
        let items = this.reportList;
        this.all_summa = 0;
        if(this.search)
        {
          items = items.filter( item => {
            return this.search.toLowerCase().split(' ').every(v => item.ism.toLowerCase().includes(v))
          })
          this.all_summa = items.reduce((accumulator ,item) => {
            return accumulator += parseFloat(item.ishlagan_puli);
          }, 0)
        }
        
        return items;
      },
    },
    methods:{
        ...mapActions(['fetch_dashboard_list']),
      async apply(){
        let start = this.Start_time;
        let end = this.End_time;
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getReportWithoutSmenaPaginationByAllUserAndOylik0404Qorovulxona?page=0&size=1000&begin_date=" + start + '&end_date=' + end);
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
              ishlagan_puli: data.items_list[i].ishlagan_puli,
              oylik_nomi: data.items_list[i].oylik_nomi,
              worked_hours_itm: '',
              departid: data.items_list[i].departid,
              without_gr_id: data.items_list[i].without_gr_id,
              oylik_haqqi: 0,
              rasxod: 0,
              rasxod_list: [],
              day_count: 0,
              day_list: [],
            }
            if(data.items_list[i].oylik_nomi != ''){
              a.oylik_haqqi = data.items_list[i].skudOylik.value;
            }
            if(data.items_list[i].without_gr_id == 2 && data.items_list[i].oylik_haqqi != 0){
              // a.ishlagan_puli = ((parseFloat(a.oylik_haqqi)/parseFloat(this.working_day_count)).toFixed() * a.day);
              a.ishlagan_puli = a.ishlagan_puli.toFixed();
            }
            a.worked_hours_itm = this.timeConvert(data.items_list[i].worked_hours_itm)
            this.reportList.push(a);
          }
          this.main_reportList = this.reportList;
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
        this.details_show = true;

      },
    async fetch_user_details_input(id, date){
      let start = date;
      // let end = date;
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getPaginationBy0404Qarovul?page=0&size=1000&date_b=" + start + '&user_id=' + id);
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
    async fetch_user_work_use(id, date){
      let start = date ;
      let end = date;
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getReportWithoutSmenaPaginationByAllUserAndOylik0404QarovulWithUserId?page=0&size=1000&begin_date=" + start + '&end_date=' + end + '&user_id=' + id);
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
      this.user_show = true;

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
<template>
  <div class="comingList">
    <div class="px-3 py-1 pt-1">
      <div class="d-flex justify-content-start align-items-center">
        <div class="row w-100">
          <!-- <div class="col-4">
            <mdb-input class="m-0 p-0" v-model="search" size="md" @input="searchClick"  outline  group type="text" validate error="wrong" success="right"/>
            <small
              style="position: absolute; top: -7px; left: 20px; font-size: 11px"
              class="bg-white px-2 py-0"
              >{{ $t("search_here") }}</small
            >
          </div> -->
          
          <div class="col-3">
            <div style="position: relative; margin-top:0px;" class="m-0 p-0"> 
              <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
                {{$t('start_time')}}
              </small>
              <mdb-input type="date" size="sm" class="m-0 p-0" v-model="Start_time" outline/>
            </div>
          </div>
          <div class="col-3">
            <div style="position: relative; margin-top: 0px;" class="m-0 p-0"> 
              <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
                {{$t('start_time')}}
              </small>
              <mdb-input type="date" class="m-0 p-0" size="sm"  v-model="End_time" outline/>
            </div>
          </div>
          <div class="col-1 m-0">
            <mdb-btn class="mr-1 py-1 px-2 mb-2 ml-0"  style="font-size: 9px;" color="success" @click="updateList" 
                size="sm"> {{$t('apply')}}
              </mdb-btn>
          </div>
          <div class="col-4">
            <router-link to="/today_groups">
              <mdb-btn class="mr-1 py-1 px-4"  style="font-size: 9px;" color="primary" 
                size="sm"><mdb-icon fas class="mr-1"  icon="users"></mdb-icon> {{$t('groups')}}
              </mdb-btn>
            </router-link>
            <router-link to="/today_comingList">
              <mdb-btn class="mr-1 py-1 px-4"  style="font-size: 9px;" color="info" 
                size="sm"><mdb-icon fas class="mr-1"  icon="user"></mdb-icon> {{$t('client')}}
              </mdb-btn>
            </router-link>
            <router-link to="/groupsAccepted">
              <mdb-btn class="mr-1 py-1 px-4"  style="font-size: 9px;" color="warning" 
                size="sm"><mdb-icon fas class="mr-1"  icon="check"></mdb-icon> {{$t('accepted')}}
              </mdb-btn>
            </router-link>
          </div>
          <div class="col-4 mt-3">
            <div style="position: relative; margin-top:0px;" class="m-0 p-0"> 
              <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
                {{$t('search_here')}}
              </small>
              <mdb-input type="text" size="sm" class="m-0 p-0" @input="searchClick" v-model="search" outline/>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="px-0">
      <loader v-if="loading"/>
      <table v-else class="myTableDebitorList ">
        <thead>
          <tr class="header py-3" style="background: #ffbda9;">
            <th  width="40" class="text-left">№</th>
            <th>{{$t('shafyor_name')}}</th>
            <th>{{$t('car_number')}}</th>
            <th>{{$t('note')}}</th>
            <th>{{$t('date')}}</th>
            <!-- <th >{{$t('lessons_cout')}}</th> -->
            <!-- <th width="380" class="text-center">{{$t('Action')}}</th> -->
          </tr>
        </thead>
        <tbody>
          <tr v-for="(row,rowIndex) in comenList" :key="rowIndex" @click="clickGroup(row)">
            <td> <span >{{rowIndex+1}}</span> </td>
            <td> <span >{{row.shafyor_name}}</span> </td>
            <td> <span >{{row.car_number}}</span> </td>
            <td> <span >{{row.note}}</span> </td>
            <td> <span >{{row.updated_date_time.slice(0,10)}}</span> <span class="ml-2">{{row.updated_date_time.slice(11,16)}}</span> </td>
            <!-- <td> <span >{{row.lessons_cout}}</span> </td> -->
            <!-- <td class="text-center">
              <mdb-btn class="mr-1 py-1 px-3"  style="font-size: 9px;" color="danger"  @click="payDebit(row)" 
                size="sm">{{$t('prixod')}}
              </mdb-btn>
              <mdb-btn class="mr-1 py-1 px-3"  style="font-size: 9px;" color="warning"  @click="$router.push('/changeScore/'+ row.id)" 
                size="sm"><mdb-icon fas class="mr-1"  icon="sync"></mdb-icon> {{$t('changing')}}
              </mdb-btn>
              <mdb-btn class="mr-1 py-1 px-3"  style="font-size: 9px;" color="primary"   
                size="sm">{{$t('paycash')}}
              </mdb-btn>
              <mdb-btn class="mr-1 py-1 px-3"  style="font-size: 9px;" color="success"  @click="$router.push('/getProduct')" 
                size="sm">{{$t('zaxira')}}
              </mdb-btn>
            </td> -->
          </tr>
        </tbody>
      </table>
    </div>
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>

    <Toast ref="message"></Toast>
  </div>
</template>

<script>
import {mdbInput, mdbBtn, mdbIcon} from 'mdbvue'
export default {
  components:{
    mdbInput, mdbBtn, mdbIcon
  },
async mounted() {
  let time1 = new Date();
  this.Start_time = time1.toISOString().slice(0,10); 
  this.End_time = time1.toISOString().slice(0,10);
  await this.updateList();

},
data() {
  return {
    modal_info: '',
    modal_status: false,
    loading: false,
    id: 0,
    search: '',
    comenList: [],
    getListSearch: [],
    Start_time: null,
    End_time: null,
    
  }
},
methods: {
  async clickGroup(row){
    console.log('row')
    console.log(row)
    localStorage.UseIdListInvoice = row.name;
    this.$router.push('/showGroupDetailAccept/'+ row.id)
  },
  async updateList(){ 
    console.log('hirayabmiz')
    let b_date  = this.Start_time + 'T00:00:35.000Z';
    let e_date  = this.End_time + 'T23:59:35.000Z';
    try{
      this.loading = true;
      const res = await fetch(this.$store.state.hostname + '/TegirmonTortilganBugdoyRoyxatiGroups/getPaginationOnlyAcceptedByDate?page=0&size=500&begin_date=' + b_date + '&end_date=' + e_date);
      const data = await res.json();
      this.loading = false;
      console.log(data)
      if(res.status == 200 || res.status == 201){
        console.log('das')
        this.comenList = data.items_list;
        this.getListSearch = data.items_list;
      }
      else{
        this.$refs.message.error('network_ne_connect')
        this.loading = false; 
      }
      this.loading = false;
    }
    catch{
      this.$refs.message.error('network_ne_connect')
      this.loading = false;
    }
  },
  searchClick(){
    console.log('search_here')
    this.comenList = this.getListSearch;
      this.loading = true;
      if(this.search != ''){
        let userSearchList = [];
        for(let i=0; i<this.comenList.length;i++){
          if(this.comenList[i].shafyor_name.toLowerCase().includes(this.search.toLowerCase()) || this.comenList[i].car_number.toLowerCase().includes(this.search.toLowerCase()) || this.comenList[i].note.toLowerCase().includes(this.search.toLowerCase())){
            userSearchList.push(this.comenList[i])
          }
        }
        this.comenList = userSearchList;
      }
      else{
        this.comenList = this.getListSearch;
      }
      this.loading = false;
  }
},
}
</script>

<style lang="scss">


.myTableDebitorList {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; overflow-x:auto
}
.myTableDebitorList th{
  font-weight: 600;
  font-size:11px;
}
.myTableDebitorList td{
  font-size:11.5px;
  
}
.myTableDebitorList td {
  text-align: left;
  padding: 11px 10px;
}
.myTableDebitorList th{
  text-align: left;
  padding: 8px 10px;
}

.myTableDebitorList tr {
  border-bottom: 1px dashed rgb(240, 240, 240);
  &:hover{
    background: #afd1fd;
  }
}

.myTableDebitorList tr.header, .myTableDebitorList tr:hover {
  // background-color: #f1f1f1;
}
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
.editIcon{
  color:#01b348;
  font-size: 13px;
}
</style>
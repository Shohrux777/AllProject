<template>
    <div class="katta_tarozi">
        <div class="katta_tarozi_header row pl-3 pr-2 pt-1 pb-0 border-bottom">
            <div class="search_item col-4" >
                <mdb-input class="m-0 mt-1 p-0 w-100" v-model="search" placeholder="Searching..." size="sm"  
                  outline  group type="text" validate error="wrong" success="right"/>
            </div>
            <div class="col-3">
                <div style="position: relative; margin-top:0px;" class="m-0 p-0"> 
                    <!-- <small class="font-weight-bold" 
                        style="position: absolute; z-index:1; left:10px; top: -15px; color: #414141; font-size: 12px;">
                        {{$t('start_time')}}
                    </small> -->
                    <mdb-input type="date" size="sm" class="m-0 p-0 mt-1" v-model="Start_time" outline/>
                </div>
            </div>
            <div class="col-3">
                <div style="position: relative; margin-top: 0px;" class="m-0 p-0"> 
                    <!-- <small class="font-weight-bold" 
                      style="position: absolute; z-index:1; left:10px; top: -15px; color: #414141; font-size: 12px;">
                      {{$t('end_time')}}
                    </small> -->
                    <mdb-input type="date" class="m-0 p-0 mt-1" size="sm"  v-model="End_time" outline/>
                </div>
            </div>
            <div class="add_btn_item col-2">
                  <mdb-btn class="mr-1 mt-0 py-2 px-4"  @click="updateList"
                      style="font-size: 8px;" color="primary"
                      size="sm"><mdb-icon fas class="mr-1"  icon="check"></mdb-icon> {{$t('apply')}}
                  </mdb-btn>
            </div>
        </div>
        <div class="katta_tarozi_table">
            <loader v-if="loading"/>
            <table v-else class="myTableDebitorList ">
                <thead>
                <tr class="header py-3 table_header_bg">
                    <th  width="40" class="text-left">№</th>
                    <th>{{$t('shafyor_name')}}</th>
                    <th>{{$t('car_number')}}</th>
                    <th>{{$t('phone_number')}}</th>
                    <th>{{$t('product')}}</th>
                    <th>Общий масса</th>
                    <th>{{$t('car_measure')}}</th>
                    <th>{{$t('product_measure')}}</th>
                    <th>{{$t('note')}}</th>
                    <th>{{$t('date')}}</th>
                </tr>
                </thead>
                <tbody>
                <tr v-for="(row, index) in rashshod_list" :key="index" @click="enter_invoice_new(row.id)" v-show="!row.TegirmonClientNewid">
                    <td>  <span >{{index+1}}</span></td>
                    <td><i v-if="row.TegirmonClientNewid" class="fas fa-star text-warning"></i> <span > {{row.shafyor_name}}</span> </td>
                    <td> <span >{{row.car_number}}</span> </td>
                    <td> <span >{{row.shafyor_tel}}</span> </td>
                    <td> <span >{{row.text_1}}</span> </td>
                    <td> <span class="font-weight-bold">{{row.all_measure}} кг</span> </td>
                    <td> <span class="font-weight-bold">{{row.car_measure}} кг</span> </td>
                    <td> <span class="font-weight-bold">{{row.product_measure}} кг</span> </td>
                    <td> <span >{{row.note}}</span> </td>
                    <!-- <td> <span >{{row.date_first_pull}}</span> </td> -->
                    <td> <span >{{row.date_first_pull.slice(8,10) + '-' + row.date_first_pull.slice(5,7) + '-' + row.date_first_pull.slice(0,4)}}</span> <span class="ml-2">{{row.date_first_pull.slice(11,16)}}</span> </td>
                </tr>
                </tbody>
            </table>
        </div>
        
    </div>
</template>

<script>
import {mdbInput, mdbBtn, mdbIcon} from 'mdbvue'
import { mapGetters, mapActions } from 'vuex';
export default {
    components: {
        mdbInput, mdbBtn, mdbIcon,
    },
    data() {
        return {
            search: '',
            loading: false,
            Start_time: null,
            End_time: null,
            rashshod_list: [],
        }
    },
    async mounted () {
        let time1 = new Date();
        this.Start_time = time1.toISOString().slice(0,10);
        this.End_time = time1.toISOString().slice(0,10);
        await this.updateList();
    //   await this.fetchTarozi_list();
    },
    computed: mapGetters(['get_tarozi_tortilgan_list']),
    methods: {
      ...mapActions(['fetchTarozi_list']),
      async enter_invoice_new(invoice_id){
        this.$router.push('add_rashshod_bulgan/' + invoice_id)
      },
      async updateList(){ 
        let b_date  = this.Start_time + 'T00:00:35.000Z';
        let e_date  = this.End_time + 'T23:59:35.000Z';
        try{
        this.loading = true;
        const res = await fetch(this.$store.state.hostname + '/TegirmonInvoiceNews/getPaginationRashshodBulganlarList?page=0&size=1000&b_date=' + b_date + '&e_date=' + e_date);
        const data = await res.json();
        this.loading = false;
        console.log(data)
        if(res.status == 200 || res.status == 201){
            this.rashshod_list = data.items_list;
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
    },
}
</script>

<style lang="scss" scoped>
.katta_tarozi_header{
    display: flex;
    justify-content: space-between;
    align-items: center;
}
.katta_tarozi_table{
    height: calc(100vh - 80px);
    overflow: hidden;
    overflow-y: scroll;
}
.myTableDebitorList {
    table-layout:fixed;
    width: 100%;
    overflow: hidden;
    font-size: 18px;
    max-height:80px; 
    overflow-x:auto;
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
    padding: 6px 10px;
    cursor: pointer;
  }
  .myTableDebitorList th{
    text-align: left;
    padding: 5px 10px;
  }
  
  .myTableDebitorList tr {
    border-bottom: 1px dashed rgb(240, 240, 240);
    &:hover{
      background: #afd1fd;
      cursor:pointer;
    }
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
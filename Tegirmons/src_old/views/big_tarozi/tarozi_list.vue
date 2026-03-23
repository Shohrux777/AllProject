<template>
    <div class="katta_tarozi">
        <div class="katta_tarozi_header pl-3 pr-2 pt-1 pb-0 border-bottom">
            <div class="search_item" style="width:30%;">
                <mdb-input class="m-0 mt-1 p-0 w-100" v-model="search" placeholder="Searching..." size="sm"  
                  outline  group type="text" validate error="wrong" success="right"/>
            </div>
            <div class="add_btn_item">
                <router-link to="/add_tarozi">
                  <mdb-btn class="mr-1 mt-0 py-2 px-4" 
                      style="font-size: 8px;" color="success"
                      size="sm"><mdb-icon fas class="mr-1"  icon="plus"></mdb-icon> {{$t('add')}}
                  </mdb-btn>
                </router-link>
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
                    <th>{{$t('note')}}</th>
                    <th>{{$t('date')}}</th>
                </tr>
                </thead>
                <tbody>
                <tr v-for="(row, index) in get_tarozi_tortilgan_list" :key="index" @click="enter_invoice_new(row.id)">
                    <td>  <small >{{index+1}}</small></td>
                    <td><i v-if="row.TegirmonClientNewid" class="fas fa-star text-warning"></i> <small > {{row.shafyor_name}}</small> </td>
                    <td> <small >{{row.car_number}}</small> </td>
                    <td> <small >{{row.shafyor_tel}}</small> </td>
                    <td> <small >{{row.text_1}}</small> </td>
                    <td> <small class="font-weight-bold">{{row.all_measure}} кг</small> </td>
                    <td> <small >{{row.note}}</small> </td>
                    <!-- <td> <small >{{row.date_first_pull}}</small> </td> -->
                    <td> <small >{{row.date_first_pull.slice(8,10) + '-' + row.date_first_pull.slice(5,7) + '-' + row.date_first_pull.slice(0,4)}}</small> <small class="ml-2">{{row.date_first_pull.slice(11,16)}}</small> </td>
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
        }
    },
    async mounted () {
      await this.fetchTarozi_list();
    },
    computed: mapGetters(['get_tarozi_tortilgan_list']),
    methods: {
      ...mapActions(['fetchTarozi_list']),
      async enter_invoice_new(invoice_id){
        this.$router.push('add_last_tarozi/' + invoice_id)
      }
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
  .myTableDebitorList td, .myTableDebitorList td small{
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
<template>
  <div>
    <div class="main_table ">
        <MDBTable  class="align-middle mb-0 bg-white">
            <thead class="table_header">
                <tr>
                    <th style="font-weight: bold;" v-for="(column, i) in options.columns" :key="i">{{$t(column)}}
                        <span style="position:relative;">
                            <span @click="sortedArrayAsc(column)"><MDBIcon icon="angle-up"  class="px-1 up_down_icon"  style="position:absolute; font-size: 11px; top:-2px; cursor:pointer;"/></span>
                            <span @click="sortedArray(column)"><MDBIcon icon="angle-down"  class="px-1 up_down_icon" style="position:absolute; font-size: 11px; bottom:-4px; cursor:pointer;"/></span>
                        </span>
                    </th>
                </tr>
            </thead>
            <tbody>
            <tr class="tr_hover" :class="{'activetr': activetr == index}" @click="selectItem(index, row)"
                v-for="(row, index) in options.rows" :key="index">
                <td v-for="(column,i) in options.columns" :key="i">
                    <span v-if="column == 'image_url'"><img v-show="row[column]" width="50" height="50" :src="hostname1 + row[column]" alt=""></span>
                    <span v-else-if="column == 'gr'">
                        <MDBBadge v-if="row[column] == 1" badge="primary" pill class="d-inline">{{$t('dont_blocked')}}</MDBBadge>
                        <MDBBadge v-if="row[column] == 2" badge="danger" pill class="d-inline">{{$t('blocked')}}</MDBBadge>
                        <MDBBadge v-if="row[column] == 0" badge="success" pill class="d-inline">{{$t('active')}}</MDBBadge>

                        <!-- <span v-if="row[column] == 1"> {{$t('dont_blocked')}}</span>
                        <span v-if="row[column] == 2"> {{$t('blocked')}}</span> -->
                    </span>

                    <span v-else>{{ row[column]}}</span>
                </td>
            </tr>
            </tbody>
        </MDBTable>
        <div class="pagination d-flex justify-content-end mt-2">
            <MDBPagination circle sm>
                <MDBPageNav prev></MDBPageNav>
                <MDBPageItem href="#">1</MDBPageItem>
                <MDBPageItem active href="#">2</MDBPageItem>
                <MDBPageItem href="#">3</MDBPageItem>
                <MDBPageNav next></MDBPageNav>
            </MDBPagination>
        </div>
    </div>
  </div>
</template>

<script>
import {MDBTable, MDBPagination, MDBPageNav, MDBPageItem, MDBBadge, MDBIcon} from 'mdb-vue-ui-kit'
export default {
components: {
    MDBTable, MDBPagination, MDBPageNav, MDBPageItem, MDBBadge, MDBIcon
},  
data(){
    return{
        activetr: -1,
        hostname1: this.$store.state.hostname1,
    }
},
props:{
    options: {
        type: Object,
        default(){
          return {}
        }
    }
},
methods:{
    selectItem(index, data){
        if(this.activetr == index){
            console.log(index, data)
            this.activetr = -1;
            return;
        }
        this.activetr = index;
        this.$emit('selectData', data)
        console.log(index, data)

    },
    sortedArrayAsc(key){
            function compare(a, b) {
              if (a[key] < b[key])
                return -1;
              if (a[key] > b[key])
                return 1;
              return 0;
            }
            this.options.rows.sort(compare);
        },
        sortedArray(key){
            function compare(a, b) {
              if (a[key] > b[key])
                return -1;
              if (a[key] < b[key])
                return 1;
              return 0;
            }

            this.options.rows.sort(compare);
        }
}
}
</script>

<style>
.main_table{
    padding: 15px;
    box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
    border-radius: 5px;
}

th,td{
    padding: 5px !important;
    font-size: 12.5px;
}
.table_header{
    background-image: linear-gradient(120deg, #fdfbfbce 0%, #f6f6f6ce 100%);
}
.tr_hover:hover{
    background: #E3EBF7;
}
.activetr{
    background: #b5bfcf;
}
.up_down_icon:hover{
    background: #b5bfcf;

}
</style>
<template>
  <div class="getProduct mx-3 mt-4">
    <div class="row px-3">
      <div class="col-4 m-0 p-0">
        <div style="position: relative; margin-top:0px;" class="m-0 p-0"> 
          <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
            {{$t('search_here')}}
          </small>
          <mdb-input type="text" size="sm" class="m-0 p-0" @input="searchClick" v-model="search" outline/>
        </div>
      </div>
    </div>
      <div>
        <div class="px-0">
          <loader v-if="loading"/>
          <table class="myTableOstatkaList">
            <thead>
              <tr class="header py-3 table_header_bg">
                <th  width="40" class="text-left">№</th>
                <th>{{$t('product')}}</th>
                <th>{{$t('price')}}</th>
                <!-- <th>Общие</th> -->
                <th>{{$t('measure_old')}}</th>
                <th>{{$t('measurment')}}</th>
                <th>{{$t('date')}}</th>
                <!-- <th >{{$t('lessons_cout')}}</th> -->
                <!-- <th width="80" class="text-center">{{$t('Action')}}</th> -->
              </tr>
            </thead>  
            <!-- v-show="adminStatus == 0 && row.product.inv_accepted_status == false" -->
            <tbody v-if="adminStatus == 0">
              <tr v-for="(row,rowIndex) in searchlist" :key="rowIndex" :class="{'ostatka_not_background': row.real_qty<=0}">
                <td> <span >{{rowIndex+1}}</span> </td>
                <td> <span >{{row.product.name}}</span> </td>
                <td> <span >{{row.product.price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{row.real_qty.toFixed(1)}}</span></td>
                <td> <span >{{row.measurment.name}}</span> </td>
                <td> <span >{{row.updated_date_time.slice(0,10)}}</span>
                  <span class="ml-2">{{row.updated_date_time.slice(11,16)}}</span> 
                </td>
                <!-- <td> <span >{{row.lessons_cout}}</span> </td> -->
              </tr>
            </tbody>
            <tbody v-else>
              <tr v-for="(row,rowIndex) in searchlist" :key="rowIndex" :class="{'ostatka_not_background': row.real_qty<=0}" v-show="row.product.inv_accepted_status == false">
                <td> <span >{{rowIndex+1}}</span> </td>
                <td> <span >{{row.product.name}}</span> </td>
                <td> <span >{{row.product.price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                <td> <span >{{row.real_qty.toFixed(1)}}</span> </td>
                <td> <span >{{row.measurment.name}}</span> </td>
                <td> <span >{{row.updated_date_time.slice(0,10)}}</span> <span class="ml-2">{{row.updated_date_time.slice(11,16)}}</span> </td>
                <!-- <td> <span >{{row.lessons_cout}}</span> </td> -->  
              </tr>
            </tbody>
          </table>
        </div>
      </div>

  </div>
</template>

<script>
import { mdbInput,  mdbBtn, } from "mdbvue"
import erpSelect from "../../components/erpSelectFio.vue";
import erpSelectAdd from "../../components/erpSelectAdd.vue";
import { required } from 'vuelidate/lib/validators'
import {mapActions,mapGetters} from 'vuex'
export default {
data(){
    return{
      modal_info: '',
      modal_status: false,  
      loading: false,
      id: this.$route.params.id,
      comenList: [],
      OstatkaList: [],

      name: '',
      user_name: "",
      user_id: null,
      passport_number: '',
      kg_ves: '',
      search: '',
      searchlist: [],

      adminStatus: localStorage.AccessType,

    }
  },
  components: {
    mdbInput,  mdbBtn, erpSelect, erpSelectAdd
  },
  validations: {
      name: {
        required
      },
      user_name:{
        required
      }
    },
    async mounted() {
      // await this.fetchClient();
      await this.updateOstatka();
    },
   computed: mapGetters(['all_district_t']),
    
  methods: {
    ...mapActions(['fetch_district_t']),
    async updateOstatka(){
      try{
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonOstatka/getPagination?page=0&size=100");
        const data = await response.json();
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          console.log('datwa');
          console.log(data);
          this.OstatkaList = data.items_list;
          this.searchlist = data.items_list;
          // const tre = response.json()
          // console.log(tre)
          this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
          // const data = await response.text();
          this.modal_info = data;
          this.modal_status = true;
          return false;
        }
      }
      catch{
        this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
      }
    },
    searchClick(){
    console.log('search_here')
    this.searchlist = this.OstatkaList;
      this.loading = true;
      if(this.search != ''){
        let userSearchList = [];
        for(let i=0; i<this.searchlist.length;i++){
          if(this.searchlist[i].product.name.toLowerCase().includes(this.search.toLowerCase()) || this.searchlist[i].product.price.toString().includes(this.search.toLowerCase()) ){
            userSearchList.push(this.searchlist[i])
          }
        }
        this.searchlist = userSearchList;
      }
      else{
        this.searchlist = this.OstatkaList;
      }
      this.loading = false;
  }
  },
}
</script>

<style lang="scss">
  
  .product_name_price{
    height: 90px;
  }
  .pro_name_color{
    color: #757575;
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
  padding: 10px;
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
.myTableOstatkaList {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; overflow-x:auto
}
.myTableOstatkaList th{
  font-weight: 600;
  font-size:11px;
}
.myTableOstatkaList td{
  font-size:11px;
  
}
.myTableOstatkaList td {
  text-align: left;
  padding: 5px 10px;
}
.myTableOstatkaList th{
  text-align: left;
  padding: 5px 10px;
}

.myTableOstatkaList tr {
  border-bottom: 1px dashed rgb(240, 240, 240);
  &:hover{
    background: #afd1fd;
  }
}

.myTableOstatkaList tr.header, .myTableOstatkaList tr:hover {
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
.ostatka_not_background{
  background: rgb(255, 239, 239);
}
</style>
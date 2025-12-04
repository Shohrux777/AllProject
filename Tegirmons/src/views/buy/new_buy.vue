<template>
  <div>
    <div class="border-bottom d-flex justify-content-between main_header_bg_shadow">
      <router-link to="/buy">
         <h5 class="m-0 ml-4 d-flex" style="padding: 15px 0px; font-size: 20px;">
            {{$t('buy')}}</h5>
      </router-link>
      <div class="d-flex">
        <div style="width:250px; margin-top: 16px; position: relative;" class="mr-4"> 
          <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
            {{$t('start_time')}}
          </small>
          <mdb-input type="date" size="sm" @change="changeDate" class="m-0 p-0" v-model="Start_time" outline/>
        </div>
        <div style="width:250px; margin-top: 16px; position: relative;" class="mr-4"> 
          <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
            {{$t('end_time')}}
          </small>
          <mdb-input type="date" size="sm" @change="changeDate" class="m-0 p-0" v-model="End_time" outline/>
        </div>
        <div style="width:250px; margin-top: 14px;" class="mr-5">
          <erpSelectSklad
            :options="allSklad.rows"
            @select="selectOptionSklad"
            :selected="sklad_name"
            :label="$t('sklad')"
            size="sm"
          />
        </div>
        <router-link to="/rasxod_add/0">
            <mdb-btn class=" mt-3" m="r2" color="danger" size="sm" p="r4 l4 t2 b2" style="font-size: 8px;">
              <mdb-icon icon="plus" class="mr-2"/>{{$t('rasxod')}}
            </mdb-btn>
          </router-link>
         <router-link to="/buy_add/0">
            <mdb-btn class="main_button_add mt-3" m="r3" size="sm" p="r4 l4 t2 b2" style="font-size: 8px;">
              <mdb-icon icon="plus" class="mr-2"/>{{$t('prixod')}}</mdb-btn>
          </router-link>
      </div>
    </div>
    <div id="sklad_ostatka" class="mt-3">
      <loader-table v-if="loading"/>
      <table class="myTableSkladDashboard">
          <thead>
          <tr class="header py-3 table_header_bg">
              <th  width="40" class="text-left">№</th>
              <th  width="120" class="text-left">Id</th>
              <th>{{$t('sklad')}}</th>
              <th>{{$t('summ')}}</th>
              <th width="120">{{$t('status_')}}</th>
              <th>{{$t('note')}}</th>
              <th>{{$t('date')}}</th>
              <th>{{$t('user_name')}}</th>
              <th class="text-center">{{$t('accept')}}</th>
              <th width="100">{{$t('Action')}}</th>
          </tr>
          </thead>  
          <!-- v-show="adminStatus == 0 && row.product.inv_accepted_status == false" -->
          <tbody>
              <tr v-for="(row,rowIndex) in m_buy" :key="rowIndex" 
                @click="showItem(row.id)"
                :class="{'alert-danger': row.prixod_status == 2}" 
                style="cursor:pointer; position: relative;">
                  <td> <span >{{rowIndex+1}}</span> </td>
                  <td> <span >{{row.id}}</span> </td>
                  <td> <span v-if="row.TegirmonSkladid">{{ row.sklad.name }} </span> </td>
                  <td> <span >{{row.summ.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                  <td class="text-center">
                    <mdb-badge v-show="row.prixod_status == 2" style="padding: 2px 8px;" pill color="danger">{{$t('rasxod')}}</mdb-badge>
                    <mdb-badge v-show="row.prixod_status == 1" style="padding: 2px 8px;" pill color="success">{{$t('prixod')}}</mdb-badge>
                  </td>
                  <td> <span >{{row.note}}</span> </td>
                  <td> <span >{{row.updated_date_time.slice(8,10)}}-{{row.updated_date_time.slice(5,7)}}-{{row.updated_date_time.slice(0,4)}}  {{row.updated_date_time.slice(11,13)}}:{{row.updated_date_time.slice(14,16)}}</span> </td>
                  <td> <span >{{row.user_name}}</span> </td>
                  <td class="text-center"> 
                    <mdb-btn class="mr-1 py-1 px-2" style="font-size: 7px;" color="success" v-if="!row.inv_accepted_status"  @click="acceptId(row.id, row)" 
                      size="sm">{{$t('apply')}}
                    </mdb-btn>
                    <span v-else class="text-success">
                      Qabul qilindi
                    </span>
                  </td>
                  <td>  
                    <i class="fas fa-pen editIcon mask waves-effect  m-0 ml-2" :class="{'applied': row.inv_accepted_status}" v-on:click="for_edit(row)"></i>
                    <i class="fas fa-trash delIcon mask waves-effect m-0 ml-3" :class="{'applied': row.inv_accepted_status}" v-on:click="for_delete(row)"></i>
                    
                    <div class="TablePatientDocId text-center card p-1" v-show="active_index == row.id">
                      <span class="p-1" style="font-size: 13px;">Berilgan mahsulotlar listi</span>
                      <table class="myTable bg-white w-100">
                        <thead>
                          <tr class="header">
                            <th  width="40" class="text-left">№</th>
                            <th >{{$t('product')}}</th>
                            <th >{{$t('qty')}}</th>
                            <th >{{$t('buy_sum')}}</th>
                            <th >{{$t('summ')}}</th>
                            <th >{{$t('buy_dollor')}}</th>
                            <th >{{$t('all_buy_dollor')}}</th>
                            <th >{{$t('date')}}</th>
                            <th >{{$t('price')}}</th>

                          </tr>
                        </thead>
                        <tbody>
                          <tr v-for="(row,rowIndex) in invoice_item_list" :key="rowIndex">
                            <td> <small style="font-size: 12.5px;">{{rowIndex+1}}</small> </td>
                            <td> <small style="font-size: 12.5px;">{{row.product.name}}</small> </td>
                            <td> <small style="font-size: 12.5px;">{{row.qty}}</small> </td>
                            <td :class="{'alert-success': row.buy_sum}"> <small style="font-size: 12.5px;" v-if="row.buy_sum">{{row.buy_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                            <td :class="{'alert-success': row.buy_sum}"> <small style="font-size: 12.5px;" v-if="row.sum">{{row.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                            <td :class="{'alert-info': row.buy_dollor}"> <small style="font-size: 12.5px;" v-if="row.buy_dollor">{{row.buy_dollor.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small></td>
                            <td :class="{'alert-info': row.buy_dollor}"> <small style="font-size: 12.5px;" v-if="row.all_sum">{{row.all_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                            <td> <span >{{row.updated_date_time.slice(8,10)}}-{{row.updated_date_time.slice(5,7)}}-{{row.updated_date_time.slice(0,4)}}  {{row.updated_date_time.slice(11,13)}}:{{row.updated_date_time.slice(14,16)}}</span> </td> 
                            <td> <small style="font-size: 12.5px;">{{row.product.price}}</small> </td> 
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </td>

                  
                  <!-- <td> <span >{{row.lessons_cout}}</span> </td> -->
              </tr>
          </tbody>
      </table>
    </div>
  </div>
</template>

<script>
import {
  mdbIcon,
  mdbInput,
  mdbBtn,
  mdbBadge
} from "mdbvue";
import erpSelectSklad from "../../components/erpSelect.vue";
import {mapActions,mapGetters} from 'vuex'

export default {
  components: {
    mdbIcon,
    mdbBtn,
    mdbBadge,
    mdbInput,
    erpSelectSklad
  },
  data(){
    return{
      loading:false,
      m_buy: [],
      invoice_item_list: [],
      active_index: -1,
      invoice_data: {},
      sklad_name: '',
      sklad_id: 0,
      Start_time: null,
      End_time: null,

    }
  },
  async mounted(){
    let time1 = new Date();
    this.Start_time = time1.toISOString().slice(0,10);
    this.End_time = time1.toISOString().slice(0,10);
    console.log(this.Start_time);
    await this.fetchSklad();
    await this.refresh();
  },
  computed: mapGetters(['allSklad']),
  methods:{
    ...mapActions([ 'fetchSklad']),
    async selectOptionSklad(option){
       console.log(option);
      this.sklad_name = option.name;
      this.sklad_id = option.id;
      await this.refresh();
    },
    async changeDate(){
      await this.refresh();

    },
    async refresh(){
        const res = await fetch(this.$store.state.hostname + '/TegirmonInvoice/getPaginationBugdoyPrixodQilinganTovarlarListiVaqtsz?page=0&size=1000&date_from=' + this.Start_time + '&date_to=' + this.End_time + '&sklad_id=' + this.sklad_id);
        const res_data = await res.json();
        // await this.update_column();
        console.log(res_data);
        this.m_buy = res_data.items_list;
        this.loading = false;
      },
      async acceptId(id, item){
        console.log(item)
        this.invoice_data = item;
            console.log('fetchContragentRasxod ga keldi')

        if(this.invoice_data.TegirmonContragentid>0){
          if(this.invoice_data.summ>0 || this.invoice_data.pol_all_sum>0){
            console.log('fetchContragentRasxod ga keldi')
            await this.fetchContragentRasxod(this.invoice_data);
          }
          if(this.invoice_data.poluchit_summa>0 || this.invoice_data.pol_dollor_summa>0){
            console.log('fetchContragentPrixod ga keldi')
            await this.fetchContragentPrixod(this.invoice_data)
          }
        }
        try{
          const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/acceptTovarlardiPrixodQilish?id=" + id);
          const data = await response.text();
          if(response.status == 201 || response.status == 200)
          {
            await this.refresh();
            this.$refs.message.success('Successfully_removed')
          }
          else{
            this.modal_info = data;
            this.modal_status = true;
          }
        }
        catch{
          this.modal_info = this.$i18n.t('network_ne_connect'); 
          this.modal_status = true;
        }
      },
      async fetchContragentRasxod(item_data){
            console.log('fetchContragentRasxod ga kirdi')
          const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "worker_name": '',
            "TegirmonContragentId": item_data.TegirmonContragentid,
            "passport_number": '',
            "phone_number": '',
            "auth_user_creator_id": localStorage.AuthId,
            "dollor": item_data.pol_all_sum,
            "dollor_string": item_data.pol_all_sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
            "all_summ": 0,
            "all_summ_string": '',
            "kurs": item_data.pol_kurs_sum,
            "note": item_data.note,
            "addiotionala_information": localStorage.user_name,
            "sum" : item_data.summ,
            "sum_str": item_data.summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
            "status_rasxod": 0,
            "auth_user_updator_id": localStorage.kassa_id,
            "bot_id": item_data.hisob_id,
            "reserve": item_data.hisob_name,
            "invoice_id": item_data.id
          })
        };
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonContragentRasxod", requestOptions);
          // const data = await response.json();
          console.log(response)
          if(response.status == 201 || response.status == 200)
          {
            this.loading = false;
            return true;
          }
          else{
            return false;
          }
        }
        catch{
          return false;
        }
      },
      async fetchContragentPrixod(item_data){
            console.log('fetchContragentPrixod ga kirdi')

          const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "worker_name": '',
            "TegirmonContragentId": item_data.TegirmonContragentid,
            "passport_number":'',
            "phone_number": '',
            "auth_user_creator_id": localStorage.AuthId,
            "dollor": item_data.pol_dollor_summa,
            "dollor_string": item_data.pol_dollor_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
            "all_summ": 0,
            "all_summ_string": '',
            "kurs": item_data.pol_kurs_sum,
            "note": item_data.note,
            "addiotionala_information": localStorage.user_name,
            "sum" : item_data.poluchit_summa,
            "sum_str": item_data.poluchit_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 '),
            "status_rasxod": 1,
            "auth_user_updator_id": localStorage.kassa_id,
            "bot_id": item_data.hisob_id,
            "reserve": item_data.hisob_name,
            "invoice_id": item_data.id
          })
        };
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonContragentRasxod", requestOptions);
          // const data = await response.json();
          console.log(response)
          if(response.status == 201 || response.status == 200)
          {
            return true;
          }
          else{
            return false;
          }
        }
        catch{
          return false;
        }
      },

      for_edit(edit_data)
      {
        if(edit_data.prixod_status == 1){
          this.$router.push("/buy_add/" + edit_data.id);
        }
        else{
          this.$router.push("/rasxod_add/" + edit_data.id);
        }
      },
      async for_delete(del_data,index)
      {
         const requestOptions = {
            method : "delete",
          };
          const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/" + del_data.id, requestOptions);
          const data = await response.text();

          if(response.status == 201 || response.status == 200)
          {
            this.$refs.message.success('Successfully_removed')
            this.alert_success = true;
            this.buy_delete_row(index);
          }
          else{
            this.modal_info = data;
            this.modal_status = true;
          }
      },
    async showItem(id){
      if(this.active_index == id){
        this.active_index = -1;
        return;
      }
      this.active_index = id;
        try{
          const response = await fetch(this.$store.state.hostname + "/TegirmonInvoice/getAnyInvoiceFullInfoById?invoice_id=" + id);
          if(response.status == 201 || response.status == 200)
          {
            const data = await response.json();
            this.invoice_item_list = data.item_list;
            console.log('id 123')
            console.log(data.item_list)
            this.$refs.message.success('Successfully_removed')
          }
          else{
            const data = await response.text();
            this.modal_info = data;
            this.modal_status = true;
          }
        }
        catch{
          this.modal_info = this.$i18n.t('network_ne_connect'); 
          this.modal_status = true;
        }
        this.item_status = true;
      },

  }
}
</script>

<style lang="scss" scoped>
.myTableSkladDashboard {
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  font-size: 18px;
  max-height:80px; overflow-x:auto
}
.myTableSkladDashboard th{
  font-weight: 600;
  font-size:11px;
}
.myTableSkladDashboard td{
  font-size:11px;
  
}
.myTableSkladDashboard td {
  text-align: left;
  padding: 5px 10px;
  border: 1px solid rgb(185, 185, 185);

}
.myTableSkladDashboard th{
  text-align: left;
  padding: 5px 10px;
  border: 1px solid #CBDCE6;

}

.myTableSkladDashboard tr {
  &:hover{
    background: #afd1fd;
  }
}

.myTableSkladDashboard tr.header, .myTableSkladDashboard tr:hover {
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
.TablePatientDocId{
  position: absolute; 
  top: 30px; 
  left: 0; 
  width: 100%;
  max-height: 80vh;
  overflow: hidden;
  overflow-y: scroll;
  background: #fafafa;
  z-index: 1111;
  box-shadow: rgb(38, 57, 77) 0px 20px 30px -10px;
  padding: 5px 7px 20px 7px !important;
}
</style>
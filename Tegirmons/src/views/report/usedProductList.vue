<template>
  <div class="bg-white" style="position:relative;">
    <div class="p-4" >
      <div class="bg-white p-4 mb-5 pt-4 shadow" style="border-radius:5px; position:relative;">
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
              <div class="col-4">
                <select v-model="selected" @change="selectMenu()" class="form-select selet_item" 
                  aria-label="Default select example">
                  <option value="0">Все</option>
                  <option value="1">Захира продукт</option>
                  <option value="2">Тароз список продукт</option>
                  <option value="3">Продажа продукт</option>
                </select>
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
        <div class="TablePatientDocId p-0 pt-2 px-1">
          <table class="rasxodTable">
            <thead style="background-color: #C4DEE4;">
              <tr class="header ">
                <th  width="40" class="text-left">№</th>
                <th>{{$t('product')}}</th>
                <th >{{$t('qty')}}</th>
                <th v-if="selected == 3">{{$t('sum')}}</th>
                <th>{{$t('begin')}}</th>
                <th>{{$t('end')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in get_payment_list" :key="rowIndex" @click="selectInvoiceItem(row)">
                <td> <small >{{rowIndex+1}}</small> </td>
                <td> <small >{{row.product_name}}</small> </td>
                <td > <small v-if="row.total_real_qty">{{row.total_real_qty.toFixed(1).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                <td v-if="selected == 3"> <small v-if="row.total_saled_summa">{{row.total_saled_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                <td> <small >{{Start_time}}</small> </td>
                <td> <small >{{End_time}}</small> </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
    <modal-train  :show="all_invoice_items_show" headerbackColor="white"  titlecolor="black" :title="$t('info_product')" 
      @close="all_invoice_items_show = false" width="60%">
        <template v-slot:body>
          <div class="TablePatientDocId p-0 pb-4 px-2 pt-1">
            <table class="rasxodTable">
              <thead style="background-color: #C4DEE4;">
                <tr class="header ">
                  <th  width="40" class="text-left">№</th>
                  <th>{{$t('product')}}</th>
                  <th>{{$t('status_')}}</th>
                  <th>{{$t('qty')}}</th>
                  <th>{{$t('sum')}}</th>
                  <th>{{$t('begin')}}</th>
                  <th>{{$t('end')}}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,rowInd) in get_product_all_item_one" :key="rowInd" @click="selectInvoiceItemModal(row)">
                  <td> <small >{{rowInd+1}}</small> </td>
                  <td> <small >{{row.product_name}}</small> </td>
                  <td> <small >{{row.status}}</small> </td>
                  <td > <small v-if="row.total_real_qty">{{row.total_real_qty.toFixed(1).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                  <td> 
                    <small v-if="row.status == 'Продажа продукт'">{{row.total_saled_summa.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> 
                    <small v-else>---</small>
                  </td>
                  <td> <small >{{Start_time}}</small> </td>
                  <td> <small >{{End_time}}</small> </td>
                </tr>
              </tbody>
            </table>
          </div>
        </template>
    </modal-train>
    <Toast ref="message"></Toast>
    <block ref="blocked"></block>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>

  import { mdbBtn, mdbInput, mdbIcon   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'

  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      mdbIcon,
      mdbInput,
      
    },
    data(){
      return{
        loading:false,
        modal_info : '',
        modal_status: false,
        selected: 0,
        Start_time: null,
        End_time: null,
        get_payment_list: [],
        all_invoice_items_show: false,
      }
    },
    async mounted(){
      await this.fetchUserAccess(localStorage.user_id);
      if(localStorage.getItem('end_date')){
        this.Start_time = localStorage.getItem('begin_date'); 
        this.End_time = localStorage.getItem('end_date');
        this.selected = localStorage.getItem('menu_item');
      }
      else{
        let time1 = new Date();
        this.Start_time = time1.toISOString().slice(0,10); 
        this.End_time = time1.toISOString().slice(0,10);
        localStorage.setItem('begin_date',this.Start_time)
        localStorage.setItem('end_date',this.End_time)
        localStorage.setItem('menu_item',this.selected)
      } 
      if(this.selected == 0){
        await this.allfetchporucts();
      }
      else{
        await this.selectMenu();
      }
        
    },
   
    computed: mapGetters(['get_zaxira_change_invoice_list', 'get_saled_invoice_list', 'get_zaxira_invoice_list',
     'get_change_invoice_list', 'get_product_all_item_one']),
    methods: {
      ...mapActions(['fetchZaxira_change_invoice_list', 'fetchSaled_invoice_list', 'fetchZaxira_invoice_list', 
      'fetchChange_invoice_list', 'fetchSaled_invoice_list_productId', 'fetchZaxira_invoice_list_productId',
      'fetchChange_invoice_list_productId']),
      ...mapMutations(['district_row_delete',]),


      async fetchUserAccess(id){
        try{
            const res = await fetch(this.$store.state.hostname + '/TegirmonUserAccess/getTegirmonUserAccessUserId?user_id=' + id);
            const data = await res.json();
            console.log('this is by id')
            if(res.status == 200 || res.status == 201){
                console.log(data)
                if(data.status_1 == false){
                  this.$refs.blocked.show_block();
                }
            }
        }
        catch(error){
            console.log(error)
        }
      },



      async selectMenu(){
        localStorage.setItem('begin_date',this.Start_time)
        localStorage.setItem('end_date',this.End_time)
        localStorage.setItem('menu_item',this.selected)
        console.log(this.selected, 'dsdsd')
        if(this.selected == 0){
          await this.allfetchporucts();
        }
        else if(this.selected == 1){
          await this.fetchZaxira_invoice_list({begin:this.Start_time, end:this.End_time});
          this.get_payment_list = this.get_zaxira_invoice_list;
        }
        else if(this.selected == 2){
          await this.fetchChange_invoice_list({begin:this.Start_time, end:this.End_time});
          this.get_payment_list = this.get_change_invoice_list;
        }
        else{
          await this.fetchSaled_invoice_list({begin:this.Start_time, end:this.End_time});
          this.get_payment_list = this.get_saled_invoice_list;
        }
      },

      async allfetchporucts(){
        this.get_payment_list = [];
        await this.fetchZaxira_change_invoice_list({begin:this.Start_time, end:this.End_time});
        await this.fetchSaled_invoice_list({begin:this.Start_time, end:this.End_time});
        for(let i=0; i<this.get_zaxira_change_invoice_list.length; i++){
          let qty_sum = this.get_zaxira_change_invoice_list[i].total_real_qty;
          let item_temp = this.get_zaxira_change_invoice_list[i];
          for(let j=0; j<this.get_saled_invoice_list.length; j++){
            if(this.get_zaxira_change_invoice_list[i].product_id == this.get_saled_invoice_list[j].product_id){
              qty_sum += this.get_saled_invoice_list[j].total_real_qty
              this.get_saled_invoice_list.splice(j,1);
              j--;
            }
          }
          item_temp.total_real_qty = qty_sum;
          if(qty_sum != 0){
            this.get_payment_list.push(item_temp)
          }
        }
        this.get_payment_list = this.get_payment_list.concat(this.get_saled_invoice_list);
        console.log(this.get_saled_invoice_list)
        console.log(this.get_zaxira_change_invoice_list)
        console.log('this.get_payment_list')
        console.log(this.get_payment_list)
      },
      async apply(){
        localStorage.setItem('begin_date',this.Start_time)
        localStorage.setItem('end_date',this.End_time)
        localStorage.setItem('menu_item',this.selected)
        if(this.selected == 0){
          await this.allfetchporucts();
        }
        else if(this.selected == 1){
          await this.fetchZaxira_invoice_list({begin:this.Start_time, end:this.End_time});
          this.get_payment_list = this.get_zaxira_invoice_list;
        }
        else if(this.selected == 2){
          await this.fetchChange_invoice_list({begin:this.Start_time, end:this.End_time});
          this.get_payment_list = this.get_change_invoice_list;
        }
        else{
          await this.fetchSaled_invoice_list({begin:this.Start_time, end:this.End_time});
          this.get_payment_list = this.get_saled_invoice_list;
        }
      },
      async selectInvoiceItem(option){
        console.log(option)
        if(this.selected == 0){
          await this.fetchSaled_invoice_list_productId({begin:this.Start_time, end:this.End_time, product_id: option.product_id});
          await this.fetchZaxira_invoice_list_productId({begin:this.Start_time, end:this.End_time, product_id: option.product_id});
          await this.fetchChange_invoice_list_productId({begin:this.Start_time, end:this.End_time, product_id: option.product_id});
          console.log('this.get_product_all_item_one')
          console.log(this.get_product_all_item_one)
          this.all_invoice_items_show = true;
        }
        else{
          localStorage.setItem('product_id', option.product_id)
          this.$router.push('/usedProductListDetails');
        }

      },
      async selectInvoiceItemModal(option){
        if(option.status == 'Захира продукт'){
          localStorage.setItem('menu_item', 1);
        }
        else if(option.status == 'Тароз список продукт'){
          localStorage.setItem('menu_item', 2);
        }
        else{
          localStorage.setItem('menu_item', 3);
        }

        localStorage.setItem('product_id', option.product_id)
        this.$router.push('/usedProductListDetails');
      }
      
      
    },
  };
</script>

<style lang="scss" scoped>

.rasxodTable {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; 
  overflow-x:auto
}
.rasxodTable th{
  font-weight: 500;
  font-size:12px;
}
.rasxodTable td{
  font-size:10px;
}
.rasxodTable th, .rasxodTable td {
  text-align: left;
  padding: 5px;
  small{
    font-weight: 200 !important;
    font-size: 10px;
  }
}

.rasxodTable tr {
  cursor: pointer;
  border-bottom: 1px solid rgb(240, 240, 240);
}
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
.selet_item{
  width:100%;
  border:none;
  border: 1px solid #d5d6d7;
  border-radius: 5px;
  color:#495077;
  font-size: 13px;
  padding: 5.8px 3px;
  outline: none;
  margin-top: 1px;
  cursor: pointer;

}
.selet_item:focus{
  border: 1.7px solid #4285F4;
}
.selet_item option{
  cursor: pointer !important;
  font-size: 12.5px;
}
</style>
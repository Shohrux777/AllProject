<template>
  <div class="bg-white" style="position:relative;">
    <div class="p-3" >
      <!-- Summa cardlari -->
      <div class="row mb-3">
        <div class="col-6">
          <div class="card p-3 bg-success text-white">
            <div class="d-flex justify-content-between align-items-center">
              <span class="header_name">Kirim </span>
              <span class="font-weight-bold" style="font-size: 18px;">
                {{ formatSum(totalPlusSum) }}
              </span>
            </div>
          </div>
        </div>
        <div class="col-6">
          <div class="card p-3 bg-danger text-white">
            <div class="d-flex justify-content-between align-items-center">
              <span class="header_name">Chiqim </span>
              <span class="font-weight-bold" style="font-size: 18px;">
                {{ formatSum(totalMinusSum) }}
              </span>
            </div>
          </div>
        </div>
      </div>

      <div class="bg-white mb-2 pt-0 " style="border-radius:5px; position:relative;">
        
        <div class="TablePatientDocId p-0 pt-2 px-1">
          <table class="myTableSkladDashboard">
            <thead>
              <tr class="header py-3 table_header_bg">
                <th  width="40" class="text-left">№</th>
                <th>{{$t('product')}}</th>
                <th >{{$t('ostatka')}}</th>
                <th >{{$t('qty')}}</th>
                <th>Farq</th>
                <th>{{$t('summ')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in option" :key="rowIndex" :style="{background:row.product.shitrix_code}" v-show="row.diff !=0">
                <td> <small >{{rowIndex+1}}</small> </td>
                <td> <small >{{row.product_name}}</small> </td>
                <td > <small >{{row.ostatka.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                <td > <small >{{row.qty.toFixed(2).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                <td > <small >{{formatNumber(row.diff).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                <td
                :class="{'bg-white': row.diff == 0, 'bg-success': row.diff >0, 'bg-danger': row.diff < 0,}">
                    <span >{{(row.diff * row.product.price).toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>

              </tr>
            </tbody>
          </table>
        </div>
      </div>

      <div class="d-flex justify-content-end px-4 pb-3" v-if="isToday">
        <mdb-btn
          class="m-0 mb-1 ml-1 mt-2"
          size="sm"
          outline="primary"
          style="font-size:9px; height:30px;"
          @click="$emit('update-diff')">
          {{$t('update')}} diff
        </mdb-btn>
      </div>
    </div>
    
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
    props:{
      option:{
        type: Array,
        default(){
          return []
        }
      },
      // Sklad diff sanasi (parentdan keladi)
      startDate: {
        type: String,
        default: ''
      }
    },
    data(){
      return{
        loading:false,
        modal_info : '',
        modal_status: false,
        
      }
    },
    
   
    computed: {
      ...mapGetters(['get_zaxira_change_invoice_list', 'get_saled_invoice_list', 'get_zaxira_invoice_list',
       'get_change_invoice_list', 'get_product_all_item_one']),
      // Faqat bugungi sana bo'lsa true
      isToday() {
        if (!this.startDate) return false;
        const today = new Date().toISOString().slice(0,10);
        return this.startDate === today;
      },
      // diff > 0 bo'lganlar uchun umumiy summa
      totalPlusSum() {
        return this.option
          .filter(item => item.diff > 0)
          .reduce((sum, item) => {
            const price = item.product?.price || 0;
            return sum + (item.diff * price);
          }, 0);
      },
      // diff < 0 bo'lganlar uchun umumiy summa (addenli)
      totalMinusSum() {
        return this.option
          .filter(item => item.diff < 0)
          .reduce((sum, item) => {
            const price = item.product?.price || 0;
            return sum + Math.abs(item.diff * price);
          }, 0);
      }
    },
    methods: {
      ...mapActions(['fetchZaxira_change_invoice_list', 'fetchSaled_invoice_list', 'fetchZaxira_invoice_list', 
      'fetchChange_invoice_list', 'fetchSaled_invoice_list_productId', 'fetchZaxira_invoice_list_productId',
      'fetchChange_invoice_list_productId']),
      ...mapMutations(['district_row_delete',]),

      formatNumber(value) {
        if (value % 1 === 0) {
          // Butun son bo'lsa, o'zgartirmaymiz
          return value;
        } else {
          // 2 ta onlik raqamgacha kesamiz
          return Math.floor(value * 100) / 100;
        }
      },
      formatSum(value) {
        return value.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      },

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

.myTableSkladDashboard {
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  font-size: 18px;
  max-height:80px; 
  overflow-x:auto
}
.myTableSkladDashboard th{
  font-weight: 600;
  font-size:11px;
}
.myTableSkladDashboard td{
  font-size:11px;
}
.myTableSkladDashboard th, .myTableSkladDashboard td {
  text-align: left;
  padding: 5px 10px;
  border: 1px solid rgb(185, 185, 185);
  small{
    font-weight: 200 !important;
    font-size: 12px;
  }
}

.myTableSkladDashboard th{
  text-align: left;
  padding: 5px 10px;
  border: 1px solid #CBDCE6;
}

.myTableSkladDashboard tr {
  cursor: pointer;
  &:hover{
    background: #afd1fd !important;
  }
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
.header_name{
  font-size: 14.5px;
  color: #ffffff;
}
</style>
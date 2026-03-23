<template>
  <div class="change-without-reg-page">
    <!-- Header -->
    <div class="d-flex justify-content-between align-items-center change-header-card">
      <h5 class="m-0 change-header-title">
        Sotuv bo'limi ishlatilgan mahsulotlar ro'yxati
      </h5>
    </div>

    <!-- Filter Section -->
    <div class="container-fluid mt-2">
      <div class="card change-filter-card">
        <div class="card-body py-0 px-3">
          <form @submit.prevent="apply">
            <div class="d-flex align-items-end flex-wrap change-filter-row">
              <div class="change-date-wrapper mr-3  mb-md-0">
                <div class="change-search-wrapper">
                  <label class="change-search-label">
                    {{$t('start_time')}}
                  </label>
                  <mdb-input
                    type="date"
                    size="sm"
                    v-model="Start_time"
                    outline
                    group
                  />
                </div>
              </div>
              <div class="change-date-wrapper mr-3  mb-md-0">
                <div class="change-search-wrapper">
                  <label class="change-search-label">
                    {{$t('end_time')}}
                  </label>
                  <mdb-input
                    type="date"
                    size="sm"
                    v-model="End_time"
                    outline
                    group
                  />
                </div>
              </div>
              <div class="change-select-wrapper mr-3 mb-4 ">
                <div class="change-search-wrapper">
                  <label class="change-search-label">
                    Turi
                  </label>
                  <select v-model="selected" @change="selectMenu()" class="change-select-item">
                    <option value="0">Все</option>
                    <option value="1">Захира продукт</option>
                    <option value="2">Тароз список продукт</option>
                    <option value="3">Продажа продукт</option>
                  </select>
                </div>
              </div>
              <div class="change-button-wrapper">
                <mdb-btn
                  @click="apply"
                  class="change-apply-btn mb-4"
                  m="r3"
                  size="sm"
                  p="r4 l4 t2 b2"
                >
                  <mdb-icon icon="check" class="mr-1" />
                  {{$t('apply')}}
                </mdb-btn>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>

    <!-- Table Section -->
    <div class="container-fluid mt-2">
      <div class="card change-table-card">
        <div class="card-body p-0">
          <div class="change-table-wrapper">
            <table class="change-table">
              <thead>
                <tr class="change-table-header">
                  <th width="50" class="text-left">№</th>
                  <th>{{$t('product')}}</th>
                  <th>{{$t('qty')}}</th>
                  <th v-if="selected == 3">{{$t('sum')}}</th>
                  <th>{{$t('begin')}}</th>
                  <th>{{$t('end')}}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,rowIndex) in get_payment_list" :key="rowIndex" class="change-table-row" @click="selectInvoiceItem(row)">
                  <td><span class="change-table-cell">{{rowIndex+1}}</span></td>
                  <td><span class="change-table-cell">{{row.product_name}}</span></td>
                  <td><span class="change-table-cell" v-if="row.total_real_qty">{{formatQty(row.total_real_qty)}}</span></td>
                  <td v-if="selected == 3"><span class="change-table-cell" v-if="row.total_saled_summa">{{formatQty(row.total_saled_summa)}}</span></td>
                  <td><span class="change-table-cell">{{Start_time}}</span></td>
                  <td><span class="change-table-cell">{{End_time}}</span></td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
    <modal-train  :show="all_invoice_items_show" headerbackColor="white"  titlecolor="black" :title="$t('info_product')" 
      @close="all_invoice_items_show = false" width="70%">
        <template v-slot:body>
          <div class="modal-table-wrapper">
            <table class="modal-table">
              <thead>
                <tr class="modal-table-header">
                  <th width="50" class="text-left">№</th>
                  <th>{{$t('product')}}</th>
                  <th>{{$t('status_')}}</th>
                  <th>{{$t('qty')}}</th>
                  <th>{{$t('sum')}}</th>
                  <th>{{$t('begin')}}</th>
                  <th>{{$t('end')}}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,rowInd) in get_product_all_item_one" :key="rowInd" class="modal-table-row" @click="selectInvoiceItemModal(row)">
                  <td><span class="modal-table-cell">{{rowInd+1}}</span></td>
                  <td><span class="modal-table-cell">{{row.product_name}}</span></td>
                  <td><span class="modal-table-cell">{{row.status}}</span></td>
                  <td><span class="modal-table-cell" v-if="row.total_real_qty">{{formatQty(row.total_real_qty)}}</span></td>
                  <td> 
                    <span class="modal-table-cell" v-if="row.status == 'Продажа продукт'">{{formatQty(row.total_saled_summa)}}</span> 
                    <span class="modal-table-cell" v-else>---</span>
                  </td>
                  <td><span class="modal-table-cell">{{Start_time}}</span></td>
                  <td><span class="modal-table-cell">{{End_time}}</span></td>
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
      },
      formatQty(value) {
        if (!value && value !== 0) return '';
        
        // Agar butun son bo'lsa, faqat butun sonni qaytaramiz
        if (value % 1 === 0) {
          return value.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1 ');
        }
        
        // Nuqtadan keyin 2 ta raqamgacha formatlash
        const formatted = value.toFixed(2);
        
        // Mingdan keyin space qo'shish
        // Avval butun qism va kasr qismini ajratamiz
        const parts = formatted.split('.');
        const integerPart = parts[0].replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1 ');
        const decimalPart = parts[1];
        
        // Agar kasr qismi "00" bo'lsa, faqat butun sonni qaytaramiz
        if (decimalPart === '00') {
          return integerPart;
        }
        
        // Agar kasr qismi oxirida 0 bo'lsa, uni olib tashlaymiz (masalan, 1.50 -> 1.5)
        const trimmedDecimal = decimalPart.replace(/0+$/, '');
        
        return trimmedDecimal ? `${integerPart}.${trimmedDecimal}` : integerPart;
      }
      
      
    },
  };
</script>

<style lang="scss" scoped>
.change-without-reg-page {
  padding: 8px 10px 14px 10px;
  background: #f5f7fb;
  min-height: 100%;
}

.change-header-card {
  padding: 14px 16px;
  border-radius: 10px;
  background-image: linear-gradient(90deg, #64b0fb 0%, #7fd4ff 50%, #b5e3ff 100%);
  box-shadow: rgba(15, 35, 52, 0.25) 0px 8px 20px -12px;
  color: #fff;
}

.change-header-title {
  font-size: 18px;
  font-weight: 600;
  letter-spacing: 0.3px;
}

.change-filter-card {
  border-radius: 10px;
  border: none;
  box-shadow: rgba(60, 64, 67, 0.15) 0px 1px 2px 0px,
              rgba(60, 64, 67, 0.1) 0px 1px 3px 1px;
  background: #ffffff;
}

.change-filter-row {
  gap: 12px;
}

.change-date-wrapper {
  min-width: 220px;
  max-width: 200px;
}

.change-select-wrapper {
  min-width: 240px;
  max-width: 200px;
}

.change-button-wrapper {
  min-width: 200px;
}

.change-search-wrapper {
  position: relative;
  margin-top: 6px;
}

.change-search-label {
  position: absolute;
  top: -9px;
  left: 6px;
  font-size: 11px;
  font-weight: 600;
  color: #607d8b;
  background: #ffffff;
  padding: 0 4px;
  z-index: 1;
}

.change-select-item {
  width: 100%;
  border: 1px solid #d5d6d7;
  border-radius: 5px;
  color: #495077;
  font-size: 13px;
  padding: 6px 8px;
  outline: none;
  cursor: pointer;
  transition: border-color 0.2s;
  background: #ffffff;
}

.change-select-item:focus {
  border: 1.7px solid #4285F4;
}

.change-select-item option {
  cursor: pointer !important;
  font-size: 12.5px;
}

.change-apply-btn {
  font-size: 10px !important;
  padding: 6px 16px !important;
  border-radius: 20px;
  background-image: linear-gradient(90deg, #4caf50 0%, #66bb6a 100%) !important;
  color: #ffffff !important;
  border: none;
  box-shadow: 0 2px 6px rgba(76, 175, 80, 0.4);
  width: 150px;
}

.change-apply-btn:hover {
  filter: brightness(1.05);
  box-shadow: 0 3px 8px rgba(76, 175, 80, 0.55);
}

.change-table-card {
  border-radius: 10px;
  border: none;
  box-shadow: rgba(60, 64, 67, 0.15) 0px 1px 2px 0px,
              rgba(60, 64, 67, 0.1) 0px 1px 3px 1px;
  background: #ffffff;
}

.change-table-wrapper {
  overflow-x: auto;
  max-height: calc(100vh - 170px);
  overflow-y: auto;
}

.change-table {
  width: 100%;
  border-collapse: collapse;
  font-size: 13px;
}

.change-table-header {
  background-color: #C4DEE4;
  position: sticky;
  top: 0;
  z-index: 10;
}

.change-table-header th {
  font-weight: 600;
  font-size: 12px;
  padding: 7px 12px;
  text-align: left;
  color: #333;
  border-bottom: 2px solid #b0bec5;
}

.change-table-row {
  border-bottom: 1px solid #e0e0e0;
  transition: background-color 0.2s;
}

.change-table-row:hover {
  background-color: #f5f5f5;
}

.change-table-row td {
  padding: 5px 12px;
  cursor: pointer;
}

.change-table-cell {
  font-size: 12px;
  color: #333;
}

.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}

/* Modal Table Styles */
.modal-table-wrapper {
  max-height: 500px;
  overflow-y: auto;
  padding: 10px;
}

.modal-table {
  width: 100%;
  border-collapse: collapse;
  font-size: 13px;
  background-color: white;
}

.modal-table-header {
  background-color: #C4DEE4;
  position: sticky;
  top: 0;
  z-index: 10;
}

.modal-table-header th {
  font-weight: 600;
  font-size: 12px;
  padding: 12px 10px;
  text-align: left;
  color: #333;
  border-bottom: 2px solid #b0bec5;
}

.modal-table-row {
  border-bottom: 1px solid #e0e0e0;
  transition: background-color 0.2s;
}

.modal-table-row:hover {
  background-color: #f5f5f5;
  cursor: pointer;
}

.modal-table-row td {
  padding: 10px;
  font-size: 12px;
  color: #333;
}

.modal-table-cell {
  display: block;
  font-size: 12px;
  color: #333;
}
</style>
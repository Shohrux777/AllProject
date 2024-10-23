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
                    <th>{{$t('client_name')}}</th>
                    <th>{{$t('product_name')}}</th>
                    <th>{{$t('qabul')}}</th>
                    <th>{{$t('tulov')}}</th>
                </tr>
                </thead>
                <tbody>
                <tr v-for="(row, index) in clients_full_info" :key="index">
                  <td>  <span >{{index+1}}</span></td>
                  <td> <span >{{row.client_name}}</span> </td>  
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
            invoice_item_list: [],
            clients_invoice_list: [],
            clients_full_info: []
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
        const res = await fetch(this.$store.state.hostname + '/TegirmonInvoiceNews/getPaginationInvoiceNewClientsReport?page=0&size=10000&b_date=' + b_date + '&e_date=' + e_date);
        const data = await res.json();
        this.loading = false;
        console.log('data')
        if(res.status == 200 || res.status == 201){
            // this.rashshod_list = data.items_list;
            console.log(data)
            this.invoice_item_list = data.items_list;
            this.clients_invoice_list = [];
            for(let i=0; i<this.invoice_item_list.length; i++){
              let client_array = [];
              client_array.push(this.invoice_item_list[i]);
              for(let j=i+1; j<this.invoice_item_list.length; j++){
                if(this.invoice_item_list[i].TegirmonClientNewid == this.invoice_item_list[j].TegirmonClientNewid){
                  client_array.push(this.invoice_item_list[j]);
                  this.invoice_item_list.splice(j,1)
                  j--;
                }
              }
              this.clients_invoice_list.push(client_array);
            }
            console.log('this.clients_invoice_list')
            console.log(this.clients_invoice_list)
            await this.client_update_product()
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
     async client_update_product(){
        this.clients_full_info = [];
        for(let i=0; i<this.clients_invoice_list.length; i++){
          let product_list_invoice = [];
          for(let j=0; j<this.clients_invoice_list[i].length; j++){
            let product = {
              product_id: this.clients_invoice_list[i][j].TegirmonProductid,
              product_name: this.clients_invoice_list[i][j].text_1,
              client_name: this.clients_invoice_list[i][j].text_2,
              qabul: 0,
              tulov: 0,
            }
            if(this.clients_invoice_list[i][j].status_1){
              product.tulov += parseFloat(this.clients_invoice_list[i][j].product_measure)
            }
            else{
              product.qabul += parseFloat(this.clients_invoice_list[i][j].product_measure)
            }
            for(let k=j+1; k<this.clients_invoice_list[i].length; k++){
              if(this.clients_invoice_list[i][j].TegirmonProductid == this.clients_invoice_list[i][k].TegirmonProductid){
                if(this.clients_invoice_list[i][k].status_1){
                  product.tulov += parseFloat(this.clients_invoice_list[i][k].product_measure)
                }
                else{
                  product.qabul += parseFloat(this.clients_invoice_list[i][k].product_measure)
                }
                this.clients_invoice_list[i].splice(k,1);
                k--;
              }
            }
            product_list_invoice.push(product)
          }
          let temp = {
            client_name: this.clients_invoice_list[i][0].text_2,
            client_id: this.clients_invoice_list[i][0].TegirmonClientNewid,
            product_info: product_list_invoice
          }
          this.clients_full_info.push(temp)
        }
        console.log('this.clients_full_info')
        console.log(this.clients_full_info)
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
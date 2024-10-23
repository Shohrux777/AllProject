<template>
  <div>
    <loader v-if="loading"/>
    <div v-else class="pay_component px-4 py-3">
      <div class="pay_info_header">
          <div class="row ">
              <div class="col-3">
                  <div class="pay_info_item">
                      <small>Продукт</small>
                      <div class="d-flex justify-content-end">
                          <span style="display:block;">{{header_info.product_name}}</span>
                      </div>
                  </div>
              </div>
              <div class="col-3">
                  <div class="pay_info_item">
                      <small>Остатка</small>
                      <div class="d-flex justify-content-end">
                          <span style="display:block;">{{header_info.ostatka}} кг</span>
                      </div>
                  </div>
              </div>
              <div class="col-3">
                  <div class="pay_info_item">
                      <small>Продукт цена</small>
                      <div class="d-flex justify-content-end">
                          <span style="display:block;">{{header_info.product_price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сўм</span>
                      </div>
                  </div>
              </div>
              <div class="col-3">
                  <div class="pay_info_item">
                      <small>Сумма</small>
                      <div class="d-flex justify-content-end">
                          <span style="display:block;">{{header_info.summ.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} сўм</span>
                      </div>
                  </div>
              </div>
          </div>
      </div>
      <div class="payed_main_item">
          <div class="row mt-3">
              <div class="col-3 px-2" style="position:relative;">
                  <input type="number" class="pay_input_item" 
                    @input="fetch_measure_run($event.target.value)"
                    @click="fetch_measure_run($event.target.value)"
                      v-model="payed_data.product_measure">
                  <small class="input_title_payed">{{$t('product_measure')}}</small>
              </div>
              <div class="col-3 px-2" style="position:relative;">
                  <input type="text" class="pay_input_item"
                  v-model="payed_data.product_price_string" disabled>
                  <small class="input_title_payed">{{$t('product_price')}}</small>
              </div>
              <div class="col-3 px-2" style="position:relative;">
                  <input type="text" class="pay_input_item"
                      v-model="payed_data.payed_summ_string" 
                      @input="func_summ($event.target.value)"
                      @click="func_summ($event.target.value)">
                  <small class="input_title_payed">{{$t('summ')}}</small>
              </div>
              <div class="col-3 px-2" style="position:relative;">
                <div class="product_btn_item justify-content-end mt-0">
                  <button class="btn_1" @click="getPhoto()">
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-camera" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="#009988" fill="none" stroke-linecap="round" stroke-linejoin="round">
                      <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                      <path d="M5 7h1a2 2 0 0 0 2 -2a1 1 0 0 1 1 -1h6a1 1 0 0 1 1 1a2 2 0 0 0 2 2h1a2 2 0 0 1 2 2v9a2 2 0 0 1 -2 2h-14a2 2 0 0 1 -2 -2v-9a2 2 0 0 1 2 -2" />
                      <path d="M9 13a3 3 0 1 0 6 0a3 3 0 0 0 -6 0" />
                    </svg>
                  </button>
                  <button :disabled="!payed_image" @click="submit_pay">Тулов</button>
                </div>
              </div>
          </div>
          <div class="photo mt-2 d-flex justify-content-start" v-if="payed_image">
            <img :src="hostname + payed_image" width="100" alt="" class="shadow border rounded">
          </div>
      </div>
      <loader-table v-if="loading_table"/>
      <div v-else  class="mt-3">
          <div class="p-2 MainKlientBigTaroziList_main">
            <table class="myTableMainKlientBigTaroziList ">
              <thead>
                <tr class="header py-3 info_client_header">
                  <th  width="40" class="text-left">№</th>
                  <th>{{$t('client_name')}}</th>
                  <th>{{$t('product')}}</th>
                  <th width="120">{{$t('product_price')}}</th>
                  <th>{{$t('product_measure')}}</th>
                  <th>{{$t('ostatka')}}</th>
                  <th>{{$t('summ')}}</th>
                  <th>{{$t('date')}}</th>
                  <th>{{$t('note')}}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(row,index) in invoice_list_history" :key="index" 
                  >
                  <td>  <span >{{index+1}}</span></td>
                  <td><span > {{client_name}}</span></td>
                  <td> <span >{{row.text_1}}</span></td>
                  <td> 
                    <div class="d-flex">
                      <input type="text" class="table_input_style"
                        v-model="product_price_change[index].price">
                      <button class="product_price_update_btn"
                        v-if="parseFloat(product_price_change[index].price) != parseFloat(product_price_change[index].product_price)" 
                        @click="func_product_price_change(index)">
                        <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-check" width="18" height="18" viewBox="0 0 24 24" stroke-width="1.5" stroke="#00b341" fill="none" stroke-linecap="round" stroke-linejoin="round">
                          <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                          <path d="M5 12l5 5l10 -10" />
                        </svg>
                      </button>
                    </div>
                    
                  </td>
                  <td> <span v-if="row.status_1 == false" >{{row.product_measure}} кг 
                  </span> </td>
                  <td> <span class="text-success">{{row.credit_sum.toFixed(1)}} кг</span></td>
  
                  <td> 
                    <small> 
                      {{parseFloat(row.summ).toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                    </small> 
                  </td>
                  <!-- <td> <span >{{row.date_first_pull}}</span> </td> -->
                  <td> <span >{{row.date_first_pull.slice(8,10) + '-' + row.date_first_pull.slice(5,7) + '-' + row.date_first_pull.slice(0,4)}}</span></td> 
                  <td> <span >{{row.note}}</span> </td>
                </tr>
              </tbody>
            </table>
          </div>
      </div>
      
    </div>
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>
    <Toast ref="message"></Toast>
    <webcam  v-show="showPhoto" @getPhotosub="takePhoto" ref="camera_component"/>
  </div>
</template>

<script>
import Loader from '../../components/loader.vue';
import webcam from '../webcam/webcam_Add.vue'

export default {
  components: {
    webcam,
    Loader,
  },
    data() {
        return {
            modal_info: '',
            modal_status: false,
            loading: false,
            loading_table: false,
            invoice_list_history: [],
            invoice_list_selected: [],
            product_price_change: [],

            payed_image: '',
            showPhoto: false,
            hostname: this.$store.state.server_ip,

            header_info: {
                product_name: '',
                ostatka: 0,
                product_price: 0,
                summ: 0,
            },
            client_name: '',
            payed_data: {
                product_measure: 0,
                product_price: 0,
                payed_summ: 0,
                product_price_string: '0',
                payed_summ_string: '0',
            },
            product_id: null,
        }
    },
    methods: {
        async refreshUpdate(item) {
            console.log(item);
            this.product_price_change = [];
            this.payed_image = '';
            this.client_name = item.client_name;
            this.client_id = item.client_id;
            this.product_id = item.product_id;
            try{
              // this.loading = true;
              this.loading_table = true;
              const response = await fetch(this.$store.state.hostname + "/TegirmonInvoiceNews/getPaginationInvoiceNewNotPaidClientIdAndProductID?page=0&size=1000&client_id=" + item.client_id + '&product_id=' + item.product_id);
              const data = await response.json();
              this.loading_table = false;
              if(response.status == 201 || response.status == 200)
              {
                this.header_info = {
                    product_name: '',
                    ostatka: 0,
                    product_price: 0,
                    summ: 0,
                };
                this.invoice_list_history = data.items_list;
                console.log(this.invoice_list_history)
                for(let i=0; i<data.items_list.length; i++){
                    this.header_info.ostatka += parseFloat(data.items_list[i].credit_sum);
                    // this.header_info.summ += parseFloat(parseFloat(data.items_list[i].credit_sum) * parseFloat(data.items_list[i].product_price));
                    this.header_info.summ += parseFloat(data.items_list[i].summ);
                    this.header_info.product_price += parseFloat(data.items_list[i].product_price);
                    this.header_info.product_name = data.items_list[i].text_1;
                    let product_temp_price  = {
                      product_price: data.items_list[i].product_price,
                      price: data.items_list[i].product_price,
                      ostatka: data.items_list[i].credit_sum,
                    }
                    this.product_price_change.push(product_temp_price)
                }
                this.header_info.product_price = (parseFloat(this.header_info.summ)/parseFloat(this.header_info.ostatka)).toFixed();
                this.header_info.ostatka = this.header_info.ostatka.toFixed(1);
                this.payed_data.product_measure = this.header_info.ostatka;
                this.payed_data.product_price = this.header_info.product_price;
                this.payed_data.payed_summ = this.header_info.summ.toFixed();
                this.payed_data.product_price_string = this.header_info.product_price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
                this.payed_data.payed_summ_string = this.header_info.summ.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
                
                console.log('this.invoice_list_history')
                return true;
              }
              else{
                const data_text = await response.text();
                this.modal_info = data_text;
                this.modal_status = true;
                return false;
              }
            }
            catch{
              // this.loading = false;
              this.modal_info = this.$i18n.t('network_ne_connect');
              this.modal_status = true;
            }
        },
        async select_pay_list(data){
          this.payed_data.product_measure = parseFloat(data.credit_sum);
          this.payed_data.product_price = parseFloat(data.product_price);
          this.payed_data.product_price_string = parseFloat(data.product_price).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
          this.payed_data.payed_summ = parseFloat(data.credit_sum) * parseFloat(data.product_price);
          this.payed_data.payed_summ_string = (parseFloat(data.credit_sum) * parseFloat(data.product_price)).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        },
        async func_summ(n){
        console.log(n)
        var tols = ''
        for(let i=0; i<n.length; i++){
          if(n[i] != ' '){
            tols += n[i];
          }
        }

        this.payed_data.payed_summ_string = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        var temp = ''
        for(let i=0; i<this.payed_data.payed_summ_string.length; i++){
          if(this.payed_data.payed_summ_string[i] != ' '){
            temp += this.payed_data.payed_summ_string[i];
          }
        }
        this.payed_data.payed_summ = parseFloat(temp);
        console.log(this.payed_data.payed_summ);
        this.invoice_list_selected = [];
        await this.fetch_summ_run(this.payed_data.payed_summ);
      },
      async fetch_summ_run(pay_summ){
        console.log(pay_summ)
        let temp_summ = pay_summ;
        let temp_measure = 0;
        for(let i=this.invoice_list_history.length-1; i>=0; i--){
          if(temp_summ == 0){
            break;
          }
          else if(temp_summ>=this.invoice_list_history[i].summ){
            temp_summ = temp_summ - parseFloat(this.invoice_list_history[i].summ);
            temp_measure += parseFloat(this.invoice_list_history[i].credit_sum);
            let item_inv = {
              index: i,
              invoice_id: this.invoice_list_history[i].id,
              status: true,
              ostatka: 0,
              payed_summ: this.invoice_list_history[i].summ, 
              payed_measure: parseFloat(this.invoice_list_history[i].credit_sum),
              summ: 0,
            }
            this.invoice_list_selected.push(item_inv);
          }
          else{
            let item_inv = {
              index: i,
              invoice_id: this.invoice_list_history[i].id,
              status: false,
              ostatka: 0,
              payed_summ: this.invoice_list_history[i].summ,
              payed_measure: 0,
              summ: 0,
            }
            item_inv.payed_summ = temp_summ;
            item_inv.summ = parseFloat(this.invoice_list_history[i].summ) - parseFloat(temp_summ);
            let use_product = 0;
            use_product = (parseFloat(this.invoice_list_history[i].credit_sum)*parseFloat(temp_summ))/parseFloat(this.invoice_list_history[i].summ)
            item_inv.ostatka = parseFloat(this.invoice_list_history[i].credit_sum) - parseFloat(use_product);
            temp_measure += parseFloat(use_product);
            item_inv.payed_measure = parseFloat(use_product);
            this.invoice_list_selected.push(item_inv);
            temp_summ = 0;
          }
        }
        this.payed_data.product_measure = temp_measure.toFixed(1);
        console.log('this.invoice_list_selected')
        console.log(this.invoice_list_selected)
      },
      async fetch_measure_run(n){
        this.invoice_list_selected = [];
        let temp_summ = parseFloat(n);
        console.log(temp_summ)
        let temp_measure = 0;
        for(let i=this.invoice_list_history.length-1; i>=0; i--){
          if(temp_summ == 0){
            break;
          }
          else if(temp_summ>=this.invoice_list_history[i].credit_sum){
            temp_summ = temp_summ - parseFloat(this.invoice_list_history[i].credit_sum);
            temp_measure += parseFloat(this.invoice_list_history[i].summ);
            let item_inv = {
              index: i,
              invoice_id: this.invoice_list_history[i].id,
              status: true,
              ostatka: 0,
              payed_measure: parseFloat(this.invoice_list_history[i].credit_sum),
              payed_summ: this.invoice_list_history[i].summ,
              summ: 0, 
            }
            this.invoice_list_selected.push(item_inv);
          }
          else{
            let item_inv = {
              index: i,
              invoice_id: this.invoice_list_history[i].id,
              status: false,
              ostatka: 0,
              payed_summ: 0,
              payed_measure: 0,
              summ: 0,
            }
            item_inv.payed_measure = parseFloat(temp_summ);
            item_inv.ostatka = parseFloat(this.invoice_list_history[i].credit_sum) - parseFloat(temp_summ);
            let use_payed_summ = 0;
            use_payed_summ = parseFloat(this.invoice_list_history[i].product_price)*parseFloat(temp_summ)
            item_inv.payed_summ = parseFloat(use_payed_summ);
            item_inv.summ = parseFloat(this.invoice_list_history[i].summ) - parseFloat(use_payed_summ);
            temp_measure += parseFloat(use_payed_summ);
            this.invoice_list_selected.push(item_inv);
            temp_summ = 0;
          }
        }
        this.payed_data.payed_summ = temp_measure.toFixed(0);
        this.payed_data.payed_summ_string = this.payed_data.payed_summ.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
        console.log('this.invoice_list_selected')
        console.log(this.invoice_list_selected)
      },
      getPhoto(){
        this.showPhoto = true; 
        this.$refs.camera_component.createCameraElement()
      },
      takePhoto(data){
        this.payed_image = data;
        this.showPhoto = false;
      },

      async submit_pay(){
        if(this.payed_data.product_measure == this.header_info.ostatka || this.payed_data.payed_summ == this.header_info.summ){
          console.log('qiymatlar teng keldi')
          for(let i=0; i<this.invoice_list_history.length; i++){
            let temp_summ_inv = this.invoice_list_history[i].summ;
            let temp_ostatks_inv = this.invoice_list_history[i].credit_sum;
            let temp_ostatka_summ_inv = 0;
            this.invoice_list_history[i].status_2 = true;
            this.invoice_list_history[i].payed_summ = temp_summ_inv;
            this.invoice_list_history[i].dolg_summ = temp_ostatks_inv;
            this.invoice_list_history[i].summ = 0;
            this.invoice_list_history[i].credit_sum = 0;
            await this.updateInvoiceItems(i, this.invoice_list_history[i].product_price, temp_ostatka_summ_inv)
           
          }
          this.loading = true;
            await this.createNewInvoice();
          this.loading = false;
        }
        else{
          console.log("this.invoice_list_selected")
          console.log(this.invoice_list_selected)
          for(let i=0; i<this.invoice_list_selected.length; i++){
            this.invoice_list_history[this.invoice_list_selected[i].index].status_2 = this.invoice_list_selected[i].status;
            this.invoice_list_history[this.invoice_list_selected[i].index].credit_sum = this.invoice_list_selected[i].ostatka;
            this.invoice_list_history[this.invoice_list_selected[i].index].payed_summ = this.invoice_list_selected[i].payed_summ;
            this.invoice_list_history[this.invoice_list_selected[i].index].dolg_summ = this.invoice_list_selected[i].payed_measure;
            this.invoice_list_history[this.invoice_list_selected[i].index].summ = this.invoice_list_selected[i].summ;

            await this.updateInvoiceItems(this.invoice_list_selected[i].index, this.invoice_list_history[this.invoice_list_selected[i].index].product_price, this.invoice_list_selected[i].summ)
          }
          this.loading = true;
            await this.createNewInvoice();
          this.loading = false;

        }
        console.log(this.payed_data.product_measure)
        console.log(this.payed_data.payed_summ)
        console.log(this.payed_data.payed_summ_string)
        
      },
      async updateInvoiceItems(index, product_price, summ){
        console.log(this.invoice_list_history[index])
        const requestOptions = {
          method : "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "shafyor_name" : this.invoice_list_history[index].shafyor_name,
            "car_number" : this.invoice_list_history[index].car_number,
            "shafyor_tel" : this.invoice_list_history[index].shafyor_tel,
            "note": this.invoice_list_history[index].note,
            "tegirmonProductid": this.invoice_list_history[index].TegirmonProductid,
            "product_price": product_price,
            "text_1" : this.invoice_list_history[index].text_1,
            "image_first_pull": this.invoice_list_history[index].image_first_pull,
            "image_second_pull": this.invoice_list_history[index].image_second_pull,
            "image_last_pull": this.payed_image,
            "all_measure": this.invoice_list_history[index].all_measure,
            "discount":  this.invoice_list_history[index].discount,
            "car_measure":  this.invoice_list_history[index].car_measure,
            "product_measure":  this.invoice_list_history[index].product_measure,
            "credit_sum": this.invoice_list_history[index].credit_sum,
            "dolg_summ": this.invoice_list_history[index].dolg_summ,
            "payed_summ": this.invoice_list_history[index].payed_summ,
            "tegirmonClientNewid": this.client_id,
            "summ": summ,
            "text_2": this.invoice_list_history[index].text_2,
            "id" : this.invoice_list_history[index].id,
            "pull_status": this.invoice_list_history[index].pull_status,
            "rashshod_status": this.invoice_list_history[index].rashshod_status,
            "finish_status":this.invoice_list_history[index].finish_status,
            "auth_first_pull_id": this.invoice_list_history[index].auth_first_pull_id,
            "auth_first_pull_name": this.invoice_list_history[index].auth_first_pull_name,
            "auth_second_pull_id": this.invoice_list_history[index].auth_second_pull_id,
            "auth_second_pull_name": this.invoice_list_history[index].auth_second_pull_name,
            "auth_last_pull_id": localStorage.AuthId,
            "auth_last_pull_name": localStorage.user_name,
            "tegirmonAuthid": localStorage.AuthId,
            "qty_real": this.invoice_list_history[index].qty_real,
            "send_group_id": this.invoice_list_history[index].send_group_id,
            "date_first_pull": this.invoice_list_history[index].date_first_pull,
            "date_second_pull": this.invoice_list_history[index].date_second_pull,
            "status_2": this.invoice_list_history[index].status_2
          })
        };
        try{
          this.loading = true;
          const response = await fetch(this.$store.state.hostname + "/TegirmonInvoiceNews", requestOptions);
          this.loading = false;
          if(response.status == 201 || response.status == 200)
          {
            let data = response.json();
            console.log('data ichiga kirdu');
            console.log(data);
            this.$refs.message.success('Added_successfully')

            return true;
          }
          else{
            console.log('else bulimida');
            return false;
          }
        }
        catch(error){
          console.log('error catch bulimida else bulimida');
          console.log(error);
          return false;
        }
      },
      async createNewInvoice(){
        const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
              "tegirmonProductid": this.product_id,
              "text_1" : this.invoice_list_history[0].text_1,
              "product_price": this.header_info.product_price,

              "image_first_pull": this.payed_image,
              "image_second_pull": this.payed_image,
              "image_last_pull": this.payed_image,
              "all_measure": parseFloat(this.header_info.ostatka),
              "product_measure":  parseFloat(this.payed_data.product_measure),
              "summ": parseFloat(this.header_info.summ),
              "discount":  0,
              "car_measure":  0,
              "credit_sum": parseFloat(this.header_info.ostatka) - parseFloat(this.payed_data.product_measure), //ostatka
              "dolg_summ": parseFloat(this.payed_data.product_measure), // payed measure
              "payed_summ": parseFloat(this.payed_data.payed_summ),

              "tegirmonClientNewid": this.client_id,
              "text_2": this.client_name,

              "id" : 0,
              "pull_status": true,
              "rashshod_status": true,
              "finish_status":true,
              "auth_first_pull_id": localStorage.AuthId,
              "auth_first_pull_name": localStorage.user_name,
              "auth_second_pull_id": localStorage.AuthId,
              "auth_second_pull_name": localStorage.user_name,
              "auth_last_pull_id": localStorage.AuthId,
              "auth_last_pull_name": localStorage.user_name,
              "tegirmonAuthid": localStorage.AuthId,
              "qty_real": 0, // group qty
              // "send_group_id": this.invoice_list_history[i].send_group_id,
              "status_1": true,
            })
          };
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/TegirmonInvoiceNews", requestOptions);
            this.loading = false;
            if(response.status == 201 || response.status == 200)
            {
              let data = response.json();
              console.log(data);
              if(parseFloat(this.header_info.ostatka) - parseFloat(this.payed_data.product_measure) < 1){
                console.log('kirdi ichiga nimaga ishlamadi')
                await this.createNewInvoiceNol();
              }
              this.$emit('close');
              return true;
            }
            else{
              return false;
            }
          }
          catch(error){
            console.log(error);
            return false;
          }
      },
      async createNewInvoiceNol(){
        const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
              "tegirmonProductid": this.product_id,
              "text_1" : this.invoice_list_history[0].text_1,
              "product_price": this.header_info.product_price,

              "image_first_pull": this.payed_image,
              "image_second_pull": this.payed_image,
              "image_last_pull": this.payed_image,
              "all_measure": 0,
              "product_measure":  0,
              "summ": 0,
              "discount":  0,
              "car_measure":  0,
              "credit_sum": 0, //ostatka
              "dolg_summ": 0, // payed measure
              "payed_summ": 0,

              "tegirmonClientNewid": this.client_id,
              "text_2": this.client_name,

              "id" : 0,
              "pull_status": true,
              "rashshod_status": true,
              "finish_status":true,
              "auth_last_pull_id": localStorage.AuthId,
              "auth_last_pull_name": localStorage.user_name,
              "tegirmonAuthid": localStorage.AuthId,
              "qty_real": 0, // group qty
              // "send_group_id": this.invoice_list_history[i].send_group_id,
              "status_1": true,
              "status_2": true,
            })
          };
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/TegirmonInvoiceNews", requestOptions);
            this.loading = false;
            if(response.status == 201 || response.status == 200)
            {
              let data = response.json();
              console.log(data);
              return true;
            }
            else{
              return false;
            }
          }
          catch(error){
            console.log(error);
            return false;
          }
      },
      async func_product_price_change(i){
        console.log(this.product_price_change)
        console.log(i)
        let inv_summa = 0;
        inv_summa = parseFloat(this.product_price_change[i].ostatka) * this.product_price_change[i].price;
        console.log('inv_summa')
        console.log(inv_summa)
        await this.updateInvoiceItems(i,this.product_price_change[i].price, inv_summa);
        let temp_update = {
          client_name : this.client_name,
          client_id : this.client_id,
          product_id : this.product_id
        }
        await this.refreshUpdate(temp_update);
      }

    },

}
</script>

<style lang="scss" scoped>
.pay_component{
    background: #F1F4F5;
}
.pay_info_item small{
    color: #67748E;
    font-size: 13px;

}
.pay_info_item span{
    color: #324058;
    font-weight: bold;
}
.pay_info_item{
    box-shadow: rgba(9, 30, 66, 0.25) 0px 4px 8px -2px, rgba(9, 30, 66, 0.08) 0px 0px 0px 1px;
    border-radius: 5px;
    padding: 5px 15px;
    background: #fff;

}
.payed_main_item{
    margin-top: 15px;
    min-height: 90px;
    box-shadow: rgba(9, 30, 66, 0.25) 0px 4px 8px -2px, rgba(9, 30, 66, 0.08) 0px 0px 0px 1px;
    border-radius: 5px;
    padding: 5px 15px;
    background: #fff;
}
.pay_input_item{
    outline: none;
    border: 1px solid #c2c4c8;
    border-radius: 5px;
    font-size: 13px;
    color: #324058;
    padding: 3.5px 8px;
    width: 100%;
}
.input_title_payed{
    position: absolute;
    top:-16px;
    left: 15px;
    font-size: 11px;
    color: #8a8f98;
}
.MainKlientBigTaroziList_main{
    height: 290px;
    background: white;
    border-radius: 10px;
    overflow: hidden;
    overflow-y: scroll;
    box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
}
.myTableMainKlientBigTaroziList {
    table-layout:fixed;
    width: 100%;
    overflow: hidden;
    font-size: 16px;
    max-height:80px; overflow-x:auto
  }
  .myTableMainKlientBigTaroziList th{
    font-weight: 600;
    color:#979FAC;
    font-size:11px;
    background: #D5ECF3;

  }
  .myTableMainKlientBigTaroziList td{
    font-size:12px;
    color:#666f82;
    
  }
  .myTableMainKlientBigTaroziList tbody td small{
    font-size:11px;
  }
  .myTableMainKlientBigTaroziList td {
    text-align: left;
    padding: 3px 10px;
  }
  .myTableMainKlientBigTaroziList th{
    text-align: left;
    padding: 5px 10px;
  }
  
  .myTableMainKlientBigTaroziList tr {
    border-bottom: 1px dashed rgb(240, 240, 240);
    cursor:pointer;
    &:hover{
      background: #afd1fd;
    }
  }
  .product_btn_item{
    display:flex;
    align-items: center;

  }
  .product_btn_item button{
    outline: none;
    border: 1.4px solid #2DCEB2;
    border-radius: 6px;
    padding: 3px 12px;
    font-size: 12.5px;
    cursor:pointer;
    background: white;
    margin-left: 7px;
  }
  .product_btn_item button:hover{
    box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
    background: #2DCEB2;
    font-weight: bold;
    color: white;
  }
  .product_btn_item .btn_1{
    outline: none;
    border: 1.4px solid #ffb829;
    border-radius: 6px;
    padding: 3px 12px;
    font-size: 12.5px;
    cursor:pointer;
    background: white;
    margin-left: 7px;
  }
  .product_btn_item .btn_1:hover{
    box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
    background: #ffb829;
    color: white;
  }
  .table_input_style{
    width: 80px;
    border:none;
    outline: none;
  }
  .table_input_style:focus{
    border: 1px solid rgb(169, 169, 169);
    border-radius: 3px;
    padding: 0px 4px;
  }
  .product_price_update_btn{
    outline: none;
    border: 1.4px solid #2DCEB2;
    border-radius: 4px;
    padding: 1px 3px;
    font-size: 12.5px;
    cursor:pointer;
    background: white;
    margin-left: 4px;
    height: 22px;
    display:flex;
    align-items: center;
    justify-content: center;
  }
  .product_price_update_btn:hover{
    background: #83e2d2;
  }
   /* Track */
   ::-webkit-scrollbar-track {
        background: #e8e9ea;
    }
</style>
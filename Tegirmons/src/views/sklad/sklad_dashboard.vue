<template>
  <div class="getProduct">
      <div class="border-bottom d-flex justify-content-between main_header_bg_shadow" v-if="currentLink != '/main_sklad'">
          <router-link to="/sklad">
              <h5 class="m-0 ml-4 d-flex" style="padding: 10px 0px">
                  {{$t('sklad')}}</h5>
          </router-link>
          <!-- <div>
              <router-link to="/sklad_add/0">
                  <mdb-btn class="main_button_add mt-3" m="r3" size="sm" p="r4 l4 t2 b2" style="font-size: 8px;">
                  <mdb-icon icon="plus" class="mr-2"/>{{$t('add')}}</mdb-btn>
              </router-link>
          </div> -->
      </div>
      <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-8">
                <div class="px-0 card p-3 SkladDashboard">
                    <div class="d-flex justify-content-between align-items-center">
                        <div>
                            <span class="header_name ml-2">{{ sklad_name }} oстатка</span>
                        </div>
                        <div class="d-flex mr-2" >
                          <div style="position: relative; margin-top: 1px; width:200px;" class="mr-2"> 
                            <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -11px; color: #757575;">
                              {{$t('date')}}
                            </small>
                            <mdb-input type="date" size="sm" class="m-0 p-0" @change="func_sklad_date" v-model="Start_time" outline/>
                          </div>
                            <div style="width:250px">
                              <erpSelectSklad
                                :options="sklad_list_access"
                                @select="selectOptionSklad"
                                :selected="sklad_name"
                                :label="$t('sklad')"
                                size="sm"
                              />
                            </div>
                            <div style="position: relative;" class="ml-2">
                              <input class="m-0 form-control mr-2" style="height:30px; width: 250px; font-size: 14px;" v-model="search" @input="searchClick()"  type="text" validate error="wrong" success="right"/>
                              <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575; font-size: 12px;">
                                {{$t('search_products')}}
                              </small>
                            </div>
                            
                            <mdb-btn  class="m-0 mb-1 "  size="sm" outline="info" style="font-size:9px; height:30px;" @click="printDiv">
                                Печат
                            </mdb-btn>
                            <mdb-btn v-if="sklad_id" class="m-0 mb-1 ml-1"  size="sm" outline="danger" style="font-size:9px; height:30px;" @click="func_sklad_diff">
                                CNV
                            </mdb-btn>
                            <!-- <download-excel

                            class=" rounded px-2 excel_btn m-0 bg_col_blue ml-2"
                            style="margin-top:6px; cursor:pointer; height: 29px; width: 100px;"
                            :data = "user_report_list"
                            :fields = "json_fields"
                            :before-generate = "startDownload"
                            :before-finish   = "finishDownload"
                            :name="`Oylik ( ` + user_report_list[0].fio + ' ).xls'">
                            <small class="text-white ml-0" style="font-size: 12px;">
                                <mdb-icon icon="file-excel" class="m-0 p-0 mr-1"></mdb-icon>
                                Excel
                            </small>
                            </download-excel> -->
                        </div>
                    </div>
                    <div id="sklad_ostatka">
                        <loader-table v-if="loading"/>
                        <table class="myTableSkladDashboard">
                            <thead>
                            <tr class="header py-3 table_header_bg">
                                <th  width="40" class="text-left">№</th>
                                <th width="250">{{$t('product')}}</th>
                                <th>Начальный остаток</th>
                                <th>{{$t('prixod')}}</th>
                                <th>{{$t('rasxod')}}</th>
                                <th>{{$t('ostatka')}}</th>
                                <th v-if="sklad_diff">{{$t('qty')}}</th>
                                <th v-if="sklad_diff" >{{$t('Farq')}}</th>
                                <th>{{$t('measurment')}}</th>
                                <!-- <th >{{$t('lessons_cout')}}</th> -->
                                <!-- <th width="80" class="text-center">{{$t('Action')}}</th> -->
                            </tr>
                            </thead>
                            <!-- v-show="adminStatus == 0 && row.product.inv_accepted_status == false" -->
                            <tbody>
                                <tr v-for="(row,rowIndex) in searchlist" :key="rowIndex" v-show="row.currentQty && (row.currentQty - row.kirim + row.chiqim) >0"
                                  :style="{background:row.product.shitrix_code}" 
                                  :class="{'ostatka_not_background': row.currentQty<=0}">
                                    <td> <span >{{rowIndex+1}}</span></td>
                                    <td> <span >{{row.product.name}}</span></td>
                                    <td> <span >
                                      <!-- {{(row.currentQty - row.kirim + row.chiqim).toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} -->
                                      {{
                                       ((row.currentQty - row.kirim + row.chiqim) > -1 && (row.currentQty - row.kirim + row.chiqim) < 0)
                                          ? 0
                                          : (row.currentQty - row.kirim + row.chiqim).toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
                                      }}
                                    </span> </td>
                                    <td> <span >
                                      {{
                                        (row.kirim > -1 && row.kirim < 0)
                                          ? 0
                                          : row.kirim.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
                                      }}
                                    </span> </td>
                                    <td> <span >
                                      {{
                                        (row.chiqim > -1 && row.chiqim < 0)
                                          ? 0
                                          : row.chiqim.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
                                      }}
                                      <!-- {{row.chiqim.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} -->
                                    </span> </td>
                                    <td> <span >
                                      {{
                                        (row.currentQty > -1 && row.currentQty < 0)
                                          ? 0
                                          : row.currentQty.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')
                                      }}
                                      <!-- {{row.currentQty.toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}} -->
                                    </span>  <span v-if="sklad_diff">{{row.unitM.name}}</span></td>
                                    <td v-show="sklad_diff" > 
                                      <input type="text" :value="row.new_qty_str" @keyup="funcDiffQty($event.target.value, rowIndex)" 
                                        @click="diff_Qty_click_nol(rowIndex)" @blur="funcAllBlue(rowIndex)"   
                                        class="form-control  border mt-0  pr-2" style="border:none; outline:none; height:24px; font-size: 13px; font-weight: bold;" >  
                                    </td>
                                    <td v-if="sklad_diff" 
                                    :class="{'bg-white': row.diff_qty == 0, 'bg-success': row.diff_qty >0, 'bg-danger': row.diff_qty < 0,}"> 
                                      {{ row.diff_qty_str }}  </td>

                                    <td v-if="sklad_diff" 
                                    :class="{'bg-white': row.diff_qty == 0, 'bg-success': row.diff_qty >0, 'bg-danger': row.diff_qty < 0,}">
                                       <span >{{(row.diff_qty * row.product.price).toFixed().toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</span> </td>
                                    <td v-else> <span >{{row.unitM.name}}</span> </td>
                                    <!-- <td> <span >{{row.lessons_cout}}</span> </td> -->
                                </tr>
                            </tbody>
                        </table>

                        <div class="d-flex justify-content-end">
                          <mdb-btn v-if="sklad_id && sklad_diff && !update_status" 
                            class="m-0 mb-1 ml-1 mt-2"  
                            size="sm" 
                            outline="success" 
                            style="font-size:9px; height:30px;" 
                            @click="func_sklad_diff_add">
                            {{ $t('save') }}  CNV 
                          </mdb-btn>
                        </div>
                        <div class="d-flex justify-content-end">
                          <mdb-btn v-if="sklad_id && update_status" 
                            class="m-0 mb-1 ml-1 mt-2"  
                            size="sm" 
                            outline="primary" 
                            style="font-size:9px; height:30px;" 
                            @click="func_update_diff">
                            {{ $t('update') }}  CNV 
                          </mdb-btn>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-4 ">
              <loader-table v-if="loading" />
              <div v-else class="p-2 card">
                <div>
                  <span class="header_name ml-2">Топ-5 товаров</span>
                </div>
                <TopProductsPie :products="topProducts"/>
              </div>

              <loader-table v-if="loading" />
              <div v-else class="card p-2 mt-3">
                <weeklySkladChart :stats="weeklyStats" />
              </div>
            </div>
        </div>
        
      </div>
      <modal-train  :show="diff_info_show" headerbackColor="cyan"  titlecolor="black" :title="$t('Skladda mahsulot farqi')" 
        @close="diff_info_show = false;  update_status = false;" width="70%">
          <template v-slot:body>
            <sklad_product_diff
              v-if="diff_info_show"
              :option="diff_list"
              :start-date="Start_time"
              @update-diff="updateSkladList"
              @close="diff_info_show = false; update_status = false;"
            ></sklad_product_diff>
          </template>
      </modal-train>
    <block ref="blocked"></block>
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>
      <Toast ref="message"></Toast>
  </div>
</template>

<script>
import TopProductsPie from "./topProductPia.vue";
import { mdbInput,  mdbBtn, } from "mdbvue"
import erpSelect from "../../components/erpSelectFio.vue";
import erpSelectAdd from "../../components/erpSelectAdd.vue";
import { required } from 'vuelidate/lib/validators'
import {mapActions,mapGetters} from 'vuex'
import LoaderTable from '../../components/loaderTable.vue';
import weeklySkladChart from './weeklySkladChard.vue'
import erpSelectSklad from "../../components/erpSelect.vue";
import sklad_product_diff from "./sklad_product_diff.vue"
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
      sklad_name: 'Sotuv Sklad',
      sklad_id: null,
      sklad_name: '',
      sklad_id: 0,
      sklad_list_access: [],

      adminStatus: localStorage.AccessType,
      topProducts: [],
      weeklyStats:[
        
      ],
       currentLink: '',
       Start_time: '',
       sklad_diff: false,
       diff_list: [],
       diff_info_show: false,
       update_status: false,
    }
  },
  created() {
    // Hozirgi sahifa path ni oladi
    this.currentLink = this.$route.path
    console.log('this.currentLink', this.currentLink)
    
    // Agar butun URL kerak bo‘lsa:
    // this.currentLink = window.location.href
  },
  components: {
    mdbInput,  mdbBtn, erpSelect, erpSelectAdd,
    LoaderTable, TopProductsPie, weeklySkladChart,
    erpSelectSklad,sklad_product_diff
  },
  validations: {
      name: {
        required
      },
      user_name:{
        required
      }
    },
    // async created() {
    //   await this.loadData();
    // },
    async mounted() {
      await this.fetchUserAccess(localStorage.user_id);
      await this.fetchUserSkladAccess();

      console.log(this.sklad_list_access.length);
      if(this.sklad_list_access.length>0){
        if(this.sklad_list_access.length >= 3){
          await this.updateOstatka();
          await this.getTop5Sklad(this.sklad_list_access[0].id);
          await this.loadData(this.sklad_list_access[0].id);
        }
        else{
          await this.getTop5Sklad(this.sklad_list_access[0].id);
          await this.loadData(this.sklad_list_access[0].id);
          this.sklad_id = this.sklad_list_access[0].id;
          this.sklad_name = this.sklad_list_access[0].name;
          await this.updateOstatkaSkladId(this.sklad_id);
        }
      }
      let time1 = new Date();
        this.Start_time = time1.toISOString().slice(0,10);

      // await this.fetchSklad();
    },
  computed: mapGetters(['all_district_t', 'allSklad']),
  methods: {
    ...mapActions(['fetch_district_t', 'fetchSklad']),
    
    async func_sklad_diff(){
      this.diff_list = [];
      await this.func_diff_list()
      if(this.diff_list.length == 0){
        // Bugungi sanani tekshirish
        const today = new Date().toISOString().slice(0,10);
        if(this.Start_time !== today){
          return; // Bugun bo'lmasa, chiqib ketadi
        }
        this.sklad_diff = true;
        this.update_status = false;
        this.diff_info_show = false;
        this.searchlist = this.OstatkaList.map(x => ({
          chiqim: x.chiqim,
          currentQty: x.currentQty,
          kirim: x.kirim,
          product: x.product,
          productId: x.productId,
          productName: x.productName,
          unitM: x.unitM,
          new_qty: 0,
          new_qty_str: '0',
          diff_qty: 0,
          diff_qty_str: '0',
        }));
      }
      else{
        this.sklad_diff = false;
        this.diff_info_show = true;
      }
      
    },
    async func_diff_list(){
      try{
            const res = await fetch(
              this.$store.state.hostname +
              '/TegirmonSkladDiffControler/get-by-date?date=' +
              this.Start_time +
              '&sklad_id=' +
              this.sklad_id
            );
            const data = await res.json();
            console.log(data)
            if(res.status == 200 || res.status == 201){
                this.diff_list = data;
            }
        }
        catch(error){
            console.log(error)
        }
    },


    diff_Qty_click_nol(index){
      console.log(index)
      if(this.searchlist[index].new_qty_str == '0'){
        console.log('ichkariga kirayyabdo')
        this.searchlist[index].new_qty_str = '';
        this.searchlist[index].new_qty = 0;
        console.log(this.searchlist[index])
      }
    },

    funcAllBlue(index){
      if(this.searchlist[index].new_qty_str == '' || this.searchlist[index].new_qty_str == null){
        this.searchlist[index].new_qty_str = '0';
        this.searchlist[index].new_qty = 0;
      }
      this.searchlist[index].diff_qty = this.searchlist[index].new_qty - this.searchlist[index].currentQty;
      this.searchlist[index].diff_qty_str = this.formatNumber(this.searchlist[index].diff_qty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      
    },
    funcDiffQty(n,index){
      console.log(n)
      console.log(index)
      var tols = ''
      for(let i=0; i<n.length; i++){
        if(n[i] != ' '){
          tols += n[i];
        }
      }
      if(tols[tols.length-1] != '0' && tols[tols.length-1] != '1' && tols[tols.length-1] != '2' && tols[tols.length-1] != '3' && tols[tols.length-1] != '4' && 
        tols[tols.length-1] != '5' && tols[tols.length-1] != '6' && tols[tols.length-1] != '7' && tols[tols.length-1] != '8' && tols[tols.length-1] != '9'){
        tols = tols.slice(0,tols.length-1)
       }
      this.searchlist[index].new_qty_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      var temp = ''
      for(let i=0; i<this.searchlist[index].new_qty_str.length; i++){
        if(this.searchlist[index].new_qty_str[i] != ' '){
          temp += this.searchlist[index].new_qty_str[i];
        }
      }
      if(temp == ''){
        this.searchlist[index].new_qty = 0;
      }
      else{
        this.searchlist[index].new_qty = parseFloat(temp);
      }
      console.log(this.searchlist[index])
      console.log(this.searchlist)
      this.searchlist[index].diff_qty = this.searchlist[index].new_qty - this.searchlist[index].currentQty;
      this.searchlist[index].diff_qty_str = this.formatNumber(this.searchlist[index].diff_qty).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
    },

    formatNumber(value) {
      if (value % 1 === 0) {
        // Butun son bo'lsa, o'zgartirmaymiz
        return value;
      } else {
        // 2 ta onlik raqamgacha kesamiz
        return Math.floor(value * 100) / 100;
      }
    },

    async func_sklad_diff_add(){
      let temp_list = [];
      temp_list = this.searchlist.map(x => ({
        tegirmonSkladId: this.sklad_id,
        tegirmonProductId: x.productId,
        ostatka: x.currentQty,
        qty: x.new_qty,
        diff: x.diff_qty,
        unit: x.unitM.name,
        product_name: x.productName,
        product_price: x.product.price,
        user_name: localStorage.user_name,
        auth_id: localStorage.AuthId,
        create_date: this.Start_time + 'T20:32:45'
      }));
      console.log(temp_list)
      try {
        const response = await fetch(this.$store.state.hostname + "/TegirmonSkladDiffControler/add-diff-and-prixod", {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(temp_list)
        });

        const result = await response.json();

        if (response.ok) {
          console.log("Success:", result);
          this.sklad_diff = false;
          await this.updateOstatkaSkladId(this.sklad_id);
          this.$refs.message.success('Added_successfully')
        } else {
          console.error("Error:", result);
          this.$refs.message.warning('Xatolik')
        }
      } catch (error) {
        this.modal_info = error;
        this.modal_status = true;
      }
    },

    async updateSkladList(){
      this.sklad_diff = true;
      this.diff_info_show = false;
      this.update_status = true;
      this.searchlist = this.OstatkaList.map(x => ({
          chiqim: x.chiqim,
          currentQty: x.currentQty,
          kirim: x.kirim,
          product: x.product,
          productId: x.productId,
          productName: x.productName,
          unitM: x.unitM,
          new_qty: 0,
          new_qty_str: '0',
          diff_qty: 0,
          diff_qty_str: '0',
        }));

        for(let i=0; i<this.searchlist.length; i++){
          for(let j=0; j<this.diff_list.length; j++){
            if(this.searchlist[i].productId == this.diff_list[j].tegirmonProductId){
              this.searchlist[i].currentQty = this.diff_list[j].ostatka
              this.searchlist[i].new_qty = this.diff_list[j].qty
              this.searchlist[i].new_qty_str = this.diff_list[j].qty.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
              this.searchlist[i].diff_qty = this.diff_list[j].diff;
              this.searchlist[i].diff_qty_str = this.formatNumber(this.diff_list[j].diff).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
              if(this.diff_list[j].diff<=0){
                this.searchlist[i].chiqim = this.searchlist[i].chiqim + this.diff_list[j].diff;
              }
              else{
                this.searchlist[i].kirim = this.searchlist[i].kirim - this.diff_list[j].diff;
              }
            }
          }
        }
    },

    async func_update_diff(){
      let temp_list = [];
      temp_list = this.searchlist.map(x => ({
        tegirmonSkladId: this.sklad_id,
        tegirmonProductId: x.productId,
        ostatka: x.currentQty,
        qty: x.new_qty,
        diff: x.diff_qty,
        unit: x.unitM.name,
        product_name: x.productName,
        product_price: x.product.price,
        user_name: localStorage.user_name,
        auth_id: localStorage.AuthId,
        create_date: this.Start_time + 'T20:32:45'
      }));
      console.log('update diff list', temp_list);
      try {
        const response = await fetch(this.$store.state.hostname + "/TegirmonSkladDiffControler/update-diff-and-prixod", {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(temp_list)
        });

        const result = await response.json();

        if (response.ok) {
          console.log("Update diff Success:", result);
          this.sklad_diff = false;
          this.update_status = false;
          await this.updateOstatkaSkladId(this.sklad_id);
          this.$refs.message.success('Added_successfully')
        } else {
          console.error("Error:", result);
          this.$refs.message.warning('Xatolik')
        }
      } catch (error) {
        this.modal_info = error;
        this.modal_status = true;
      }
    },




    

    async fetchUserSkladAccess(){
        try{
            const res = await fetch(this.$store.state.hostname + '/TegirmonUserSkladAccess/getSkladsByUserId?user_id=' + localStorage.user_id);
            const data = await res.json();
            if(res.status == 200 || res.status == 201){
                console.log('sklad_list_access', data.length);
                this.sklad_list_access = data;
                if(data.length == 0){
                  this.$refs.blocked.show_block();
                }
            }
        }
        catch(error){
            console.log(error)
            // this.$refs.blocked.show_block();
        }
      },

    async selectOptionSklad(option){
      this.sklad_name = option.name;
      this.sklad_id = option.id;
      this.sklad_diff = false;
      await this.updateOstatkaSkladId(this.sklad_id);
      await this.getTop5Sklad(this.sklad_id);
      await this.loadData(this.sklad_id);
    },
    async func_sklad_date(){
      if(this.sklad_id){
        await this.updateOstatkaSkladId(this.sklad_id);
        await this.getTop5Sklad(this.sklad_id);
        await this.loadData(this.sklad_id);
      }
      else{
        await this.updateOstatka();
      }
      
    },
    async fetchUserAccess(id){
        try{
            const res = await fetch(this.$store.state.hostname + '/TegirmonUserAccess/getTegirmonUserAccessUserId?user_id=' + id);
            const data = await res.json();
            console.log('this is by id')
            if(res.status == 200 || res.status == 201){
                console.log(data)
                if(data.num_2 == 0){
                  console.log('hiiii')
                  this.$refs.blocked.show_block();
                }
            }
        }
        catch(error){
          console.log(error)
        }
      },
    
    async updateOstatka(){
      try{
        const today = new Date().toISOString().slice(0,10);
        let url = "";
        // Agar bugungi kun bo'lsa, eski API ishlatamiz
        if (!this.Start_time || this.Start_time === today) {
          url = this.$store.state.hostname + "/TegirmonSkladHistory/getSkladStatistikaNotSkladId";
        } else {
          // Boshqa kunlar uchun yangi API - umumiy (hamma skladlar bo'yicha)
          url = this.$store.state.hostname + "/TegirmonSkladHistory/getSkladStatistikaByDate?date=" + this.Start_time;
        }
        const response = await fetch(url);
        const data = await response.json();
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          console.log('datwa');
          console.log(data);
          this.OstatkaList = data;
          this.searchlist = data;
          // this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
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
    async updateOstatkaSkladId(id){
      try{
        const today = new Date().toISOString().slice(0,10);
        let url = "";
        // Agar bugungi kun bo'lsa, eski API ishlatamiz
        if (!this.Start_time || this.Start_time === today) {
          url = this.$store.state.hostname + "/TegirmonSkladHistory/getSkladStatistika?sklad_id=" + id;
        } else {
          // Boshqa kunlar uchun yangi API - tanlangan sklad bo'yicha
          url = this.$store.state.hostname + "/TegirmonSkladHistory/getSkladStatistikaByDate?date=" + this.Start_time + "&sklad_id=" + id;
        }
        const response = await fetch(url);
        const data = await response.json();
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          console.log('datwa');
          console.log(data);
          this.OstatkaList = data;
          this.searchlist = data;
          this.$refs.message.success('Added_successfully')
          return true;
        }
        else{
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
    async getTop5Sklad(id){
      try{
        this.loading = true;
        let url = this.$store.state.hostname + "/TegirmonSkladHistory/getTopProductsKg";
        const params = [];
        // Sana (Start_time) bo'yicha filter
        if (this.Start_time) {
          params.push("date=" + this.Start_time);
        }
        // id berilgan va 0 dan katta bo'lsa faqat shu sklad bo'yicha, aks holda obshiy
        if (id && id > 0) {
          params.push("sklad_id=" + id);
        }
        if (params.length > 0) {
          url += "?" + params.join("&");
        }
        const response = await fetch(url);
        const data = await response.json();
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          console.log('productsss',data);
          this.topProducts = data;
          // this.$refs.message.success('Added_successfully')
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
    async loadData(id){
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonSkladHistory/getSkladHaftalikStatistika?sklad_id=" + id);
        const data = await response.json();
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          console.log(data)
          this.weeklyStats = data;
          // this.$refs.message.success('Added_successfully')
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
    },
    printDiv() {
        let divContents = document.getElementById("sklad_ostatka").innerHTML;

        // CSS fayllarini olish
        let styles = "";
        document.querySelectorAll('link[rel="stylesheet"], style').forEach((node) => {
        styles += node.outerHTML;
        });

        // Yangi oynani ochish
        let printWindow = window.open("", "", "height=800,width=1000");

        let html =
        '<html>' +
        '<head>' +
        '<title>Chop etish</title>' +
        styles +
        '<style>' +
        '@page { size: A4; margin: 5mm; }' +
        '@media print { ' +
        'body { zoom: 1; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; color-adjust: exact !important; }' +
        '}' +
        '</style>' +
        '</head>' +
        '<body>' +
        divContents +
        '<script>window.onafterprint = function() { window.close(); }<\/script>' +
        '</body>' +
        '</html>';
        printWindow.document.open();
        printWindow.document.write(html);
        printWindow.document.close();
        printWindow.focus();
        setTimeout(() => {
          printWindow.print();
        }, 1000);
        
    }
  },
}
</script>

<style lang="scss" scoped>
  ::-webkit-scrollbar {
  width: 5px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #fdfdfd;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #b4c2d6;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #212d3d;
}
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
  border: 1px solid rgb(240, 240, 240);
}

.myTable tr.header, .myTable tr:hover {
  // background-color: #f1f1f1;
}
.delIcon{
  color: rgb(251, 70, 70);
  font-size: 13px;
}
.SkladDashboard{
    max-height: calc(100vh - 80px) !important;
    overflow: hidden;
    overflow-y: scroll;
}
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
    background: #afd1fd !important;
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
.ostatka_not_background{
  background: rgb(255, 239, 239);
}
.header_name{
    font-size: 14.5px;
    // margin-bottom: 5px;
    color: #484d53;
}
</style>
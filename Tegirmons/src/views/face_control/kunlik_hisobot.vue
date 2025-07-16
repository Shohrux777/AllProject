<template>
  <div class="bg-white" style="position:relative;">
    <div class="p-4" >
      <div class="bg-white px-4 mb-5 pt-1 pb-4 shadow" style="border-radius:5px; position:relative;">
        <form @submit.prevent="submit">
          <div style="height: 40px;" class="d-flex justify-content-between border-bottom align-items-center  ">
            <div class="title w-75 row align-items-center">
               <div class="col-4">
                  <div style="position: relative; margin-top: 25px;">
                    <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
                      
                    </small>
                    <mdb-input type="date" size="sm"  v-model="Start_time" outline/>
                  </div>
                </div>
                <div class="col-4">
                  <div style="position: relative; margin-top: 5px;">
                    <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
                      
                    </small>
                    <div style="position:relative;">
                      <erpSelect
                        :options="get_smena_list.rows"
                        @select="selectOption"
                        :selected="smena_name"
                        searchKey="smena_nomi"
                        :label="$t('smena')"
                        
                      />
                      <!-- <small style="position:absolute; top:-12px; left:3px; font-size: 11.5px;" class="font-weight-bold">{{$t('kassa')}}</small> -->
                    </div>
                  </div>
                </div>
                <div class="col-4">
                  <div style="position: relative; margin-top: 5px;">
                    <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
                      
                    </small>
                    <div style="position:relative;">
                      <erpSelect
                        :options="get_dept_list.rows"
                        @select="selectOptionDept"
                        :selected="dept_name"
                        searchKey="deptname"
                        :label="$t('dept')"
                        
                      />
                      <!-- <small style="position:absolute; top:-12px; left:3px; font-size: 11.5px;" class="font-weight-bold">{{$t('kassa')}}</small> -->
                    </div>
                  </div>
                </div>
              <!-- <div class="col-4">
                <div style="position: relative; margin-top: 25px;"> 
                  <small class="bg-white" style="position: absolute; z-index:1; left:10px; top: -10px; color: #757575;">
                    {{$t('end_time')}}
                  </small>
                  <mdb-input type="date" size="sm"   v-model="End_time" outline/>
                </div>
              </div> -->
              <!-- <div class="col-4">
                <select v-model="selected" @change="selectMenu()" class="form-select selet_item" 
                  aria-label="Default select example">
                  <option value="0">Все</option>
                  <option value="1">Захира продукт</option>
                  <option value="2">Тароз список продукт</option>
                  <option value="3">Продажа продукт</option>
                </select>
              </div> -->
            </div>
            <div class="plus">
              <mdb-btn @click="apply" color="primary py-2 px-3" style="font-size:9px;">
                <mdb-icon style="font-size:9.5px;" icon="check" class="m-0 p-0 mr-1" />
                {{$t('apply')}}
              </mdb-btn>
            </div>
          </div>
        </form>
        <div class="row">
          <div class="col-3 py-2">
            <Circle_progress label="Ishga kelganlar" :qty="xodim_soni" :activ_qty="xodim_kelgan" progress_bg="progress_bg_success"/>
          </div>
          <div class="col-3 py-2">
            <Circle_progress label="Ishga kelmaganlar" :qty="xodim_soni" :activ_qty="xodim_kelmagan" progress_bg="progress_bg_danger"/>
          </div>
        </div>

        <div class="TablePatientDocId p-0 pt-2 px-1">
          <table class="kunlik_hisobot_table">
            <thead style="background-color: #C4DEE4;">
              <tr class="header ">
                <th  width="40" class="text-left">№</th>
                <th width="40">ID</th>
                <th width="400">FIO</th>
                <th >Tel. nomer</th>
                <th width="140">Kirish</th>
                <th  width="140">Chiqish</th>
                <th >Status</th>
                <th >Vaqt</th>
                <th >Summa</th>
                
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row,rowIndex) in get_payment_list" :key="rowIndex" @click="selectInvoiceItem(row)">
                <td> <small >{{rowIndex+1}}</small> </td>
                <td> <small >{{row.userId}}</small> </td>
                <td> <small >{{row.fio}}</small> </td>
                <td> <small >{{row.user.phone_number}}</small> </td>
                <td>
                  <small v-if="row.kirish" class="bg-success px-2 rounded text-white">{{row.kirish}}</small>
                  <small v-else class="bg-danger px-2 rounded text-white">--:--</small>
                </td>
                <td>
                  <small v-if="row.chiqish" class="bg-success px-2 rounded text-white">{{row.chiqish}}</small> 
                  <small v-else class="bg-danger px-2 rounded text-white">--:--</small> 
                </td>
                <td> 
                  <small v-if="row.kirish" class=" px-2 rounded text-success">Ishga keldi</small>
                  <small v-else class="px-2 rounded text-danger">Ishga kelmadi</small>
                </td>
                <td> <small >{{row.workedTime}}</small> </td>
                <td> <small >{{row.sum.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}</small> </td>
                
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
    
    <Toast ref="message"></Toast>
    <block ref="blocked"></block>
     <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
  </div>

</template>

<script>
  import erpSelect from "../../components/erpSelectDynamic.vue";
  import { mdbBtn, mdbInput, mdbIcon   } from 'mdbvue';
  import {mapActions, mapGetters, mapMutations} from 'vuex'
import Circle_progress from './circle_progress.vue';

  // import 'vue2-datepicker/index.css';
  export default {
    components: {
      mdbBtn,
      mdbIcon,
      mdbInput,
      Circle_progress,
      erpSelect
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
        xodim_soni: 0,
        xodim_kelgan: 0,
        xodim_kelmagan: 0,

        dept_name: '',
        dept_id: 0,
        smena_name: '',
        smena_id: 0,
      }
    },
    async mounted(){
      this.fetch_Dept();
      this.fetch_Smena();
      // await this.fetchUserAccess(localStorage.user_id);
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
      await this.apply();
        
    },
   
    computed:  {
      ...mapGetters(['get_zaxira_change_invoice_list', 'get_saled_invoice_list', 'get_zaxira_invoice_list',
     'get_change_invoice_list', 'get_product_all_item_one', 'get_dept_list', 'get_smena_list']),
    },
    methods: {
      ...mapActions(['fetchZaxira_change_invoice_list', 'fetchSaled_invoice_list', 'fetchZaxira_invoice_list', 
      'fetchChange_invoice_list', 'fetchSaled_invoice_list_productId', 'fetchZaxira_invoice_list_productId',
      'fetchChange_invoice_list_productId', 'fetch_Dept', 'fetch_Smena']),
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

      async selectOption(option){
        this.smena_name = option.smena_nomi;
        this.smena_id = option.id;
        await this.apply();
      },

      async selectOptionDept(option){
        this.dept_name = option.deptname;
        this.dept_id = option.deptid;
        await this.apply();
      },

      

      
      async apply(){
        try{
            const res = await fetch(this.$store.state.hostname + '/SkudMyCheckinouts/by-date?sana=' + this.Start_time + '&smena_id=' + this.smena_id + '&dept_id=' + this.dept_id);
            const data = await res.json();
            console.log('this is by id')
            if(res.status == 200 || res.status == 201){
                console.log(data)
              this.get_payment_list = data.users;
              this.xodim_soni = data.totalUsers;
              this.xodim_kelgan = data.checkedInUsers;
              this.xodim_kelmagan = data.totalUsers - data.checkedInUsers;
            }
        }
        catch(error){
            console.log(error)
        }
      },
      async selectInvoiceItem(option){
        console.log(option)
      
      },
      
      
      
    },
  };
</script>

<style lang="scss" scoped>

.kunlik_hisobot_table {
  /* border-collapse: collapse; */
  table-layout:fixed;
  width: 100%;
  overflow: hidden;
  // border: 1px solid #ddd;
  font-size: 18px;
  max-height:80px; 
  overflow-x:auto
}
.kunlik_hisobot_table th{
  font-weight: 500;
  font-size:12.5px;
}
.kunlik_hisobot_table td{
  font-size:14px;
}
.kunlik_hisobot_table th, .kunlik_hisobot_table td {
  text-align: left;
  padding: 5px;
  small{
    font-weight: 200 !important;
    font-size: 12.5px;
  }
}

.kunlik_hisobot_table tr {
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
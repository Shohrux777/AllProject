<template>
  <div class="m-4">
    <main style="border: 0.1px solid white; border-radius: 7px;" class="shadow">
      <form @submit.prevent="submit">
        <div class="container-fluid ">
          <div  class="container-fluid mt-3  shadow bgColorBack p-4" style=" border-radius: 7px;">

            <mdb-row >
              <mdb-col col="4">
                <mdb-input v-model="invoice_name" size="sm" :label="$t('Name')"  type="text" />
                <small class="invalid-text" v-if="$v.invoice_name.$dirty && !$v.invoice_name.required" >
                    {{$t('name_invalid_text')}}
                  </small>
              </mdb-col>
              <mdb-col col="4" >
                <mdb-input v-model="note" type="textarea" style="margin-top: 8px; margin-bottom: 0px; height: 32px;" :label="$t('Descriptions')" icon="pencil-alt" />
              </mdb-col>
            </mdb-row>
          </div>

            <mdb-btn slot="reference" class="mr-1 ml-1 mt-4 px-4 " outline="primary" @click="applied"
                    darkWaves tag="a" floating far icon="paper-plane"  size="sm" >{{$t('apply')}}</mdb-btn>

            <div v-if="sklad_submitted">
              <h4 class=" mb-4 mt-5">{{$t('Invoice_element')}}</h4>

                
                <div :class="{'applied': edit}">
                <div class="d_table " >

                <mdb-tbl class="fixed-column" >
                  <mdb-tbl-head>
                    <tr>
                      <td></td>
                      <!-- product -->
                      <td style="min-width:300px"> {{$t(datasource.columns[0])}} </td>
                      <td style="min-width:100px"> {{$t(datasource.columns[1])}} </td>

                      <!-- ed_izm -->
                    </tr>
                  </mdb-tbl-head>
                  <mdb-tbl-body>
                    <tr  scope="row" v-for="(row,rowIndex) in datasource.rows" :key="rowIndex" :width="row.size">
                      <th><i class="fas fa-trash delIcon mask waves-effect m-0 pl-2"
                      @click="datasource.rows.splice( parseInt(rowIndex), 1)"></i></th>
                      <th>
                        <lineSelectAuth
                            :options="auth_user_list" :row_index="rowIndex"
                            :searchshow="true"
                            @select="selectauth"
                            :selected="row.auth_name"
                            :label="$t('contragent')"
                        />
                      </th>

                      <th>
                        <lineSelect
                            :options="get_service_type.rows" :row_index="rowIndex"
                            :searchshow="true"
                            @select="selectproduct"
                            :selected="row.service_name"
                            :label="$t('product')"
                        />
                      </th>
                    </tr>
                  </mdb-tbl-body>
                </mdb-tbl>

                <!-- <div class="cyrcle_btn ml-5 z-depth-1-half "  v-on:click="addRow" v-mdb-waves="{ duration : 'long' , fixed : true}" >
                    <i class="fas fa-plus white-text d-flex justify-content-center " style="padding-top:11px"></i>
                </div> -->
                <circle-btn  class="gradint mt-5  " @on_click="addRow"/>
                </div>

              <!-- <mdb-btn class="float-right" type="submit" color="success" m="r3"
                p="r4 l4 t2 b2">
                <mdb-icon icon="check" class="mr-2"/>{{'save'|locolize}}</mdb-btn> -->
                <hr class="mt-4 mb-3 gradint"/>
                <div class="text-right pb-3" >
                  <mdb-btn  class="mr-2 py-2 px-3" type="submit" color="success"   style="font-size:10px;" 
                          icon="check"  size="md">{{$t(save_btn)}}</mdb-btn>

                  <!-- <mdb-btn class="mr-1" outline="primary" darkWaves tag="a" floating @click="send_data"
                          icon="paper-plane"  size="sm">{{$t('Send')}}</mdb-btn> -->
                </div>
            </div>
            </div>
          <hr v-if="!sklad_submitted" class="mt-4 mb-4 gradint"/>
        </div>
      </form>

      <div>
        <massage_box :hide="modal_status" :detail_info="modal_info"
         :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
      </div>
      <Toast ref="message"></Toast>

    </main>
  </div>
</template>


<script>
import lineSelect from "../../../components/lineSelect.vue";
import lineSelectAuth from "../../../components/lineSelectAuth.vue";

import {mapActions, mapGetters,mapMutations} from "vuex";
import { required } from 'vuelidate/lib/validators'
// import localizeFilter from '../../filters/localize.filter';
import {
  mdbRow, mdbBtn,mdbInput, mdbCol,
  mdbTbl, mdbTblHead, mdbTblBody,
  } from "mdbvue";
import 'vue2-datepicker/index.css';
export default {
  components : {
      mdbTbl, mdbTblHead, mdbTblBody,
      mdbRow,mdbInput,
      mdbBtn, lineSelect,
      mdbCol, lineSelectAuth
  },
  validations: {
      invoice_name: {required},
    },
    data(){
      return{
        modal_info : '',
        modal_status: false,
        showfor_print : false,
        invoice_name: '',
        save_btn : 'save',
        created_user : 1,
        changed_user : 1,
        id : this.$route.params.id,
        invoiceItems: [],
        time1: new Date(Date.now()),
        note : '',
        company_name : ' ', company_id : null,
        agent_id : null, agent_name : ' ',
        selected_row_index : -1,
        sklad_submitted : false,
        edit : false,
        datasource :
        {
          rows :[],
          // real_time
          columns: ['doctors','service_type'],
        }
      }
    },
    computed:mapGetters(['auth_user_list','get_service_type','sklad_list','CurrencyList','getDepartId','get_sklad',
    'product_fromsklad','get_m_product_list', 'get_filial_list']),
     methods:{
      ...mapActions(['fetch_auth_list','fetch_service_type','fetch_dept_list_id','fetch_sklad_list_id','fetchCurrency',
      'fetch_product_fromsklad','fetch_m_product']),
      ...mapMutations(['get_deparment_id_comp']),
      selectauth(option){
        console.log(option)
        this.datasource.rows[option.row].auth_name = option.data.users.fio;
        this.datasource.rows[option.row].auth_id = option.data.id;
      },
      selectproduct(option)
      {
        console.log(option)
        this.datasource.rows[option.row].service_name = option.data.name;
        this.datasource.rows[option.row].service_id = option.data.id;
        // this.datasource.rows[option.row].summ = this.datasource.rows[option.row].soni * this.datasource.rows[option.row].price;
      },

      applied()
      {
        // if(this.edit) return;
        this.datasource.rows = [];
        if( this.$v.$invalid)
        {
          this.$v.$touch();
          this.$refs.message.warning('please_fill')
          return;
        }   
        this.sklad_submitted = !this.sklad_submitted ;
      },

      addRow() {
       // console.log("test : "  + this.datasource.rows[0].product.name);
            this.datasource.rows.push({
            id : 0,
            auth_id : null,
            auth_name : '',
            service_name : '',
            service_id : null,
          })

            //appData.updated()
        },

      async submit()
      {
        if(this.$v.$invalid)
        {
          this.$v.$touch();
          this.$refs.message.warning('please_fill')
          return;
        }
        this.invoiceItems = [];
        console.log(this.datasource.rows)
        for(var i = 0; i < this.datasource.rows.length; i++){
          var a = {
            serviceTypeId: null,
            authorizationId: null,
            id: 0,
          }
          a.serviceTypeId = this.datasource.rows[i].service_id;
          a.authorizationId = this.datasource.rows[i].auth_id
          this.invoiceItems.push(a);
        }
        console.log('this.invoiceItems')
        console.log(this.invoiceItems)
         const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
              "name": this.invoice_name,
              "note" : this.note,
              "items" : this.invoiceItems,
            })
          };
            // console.log(requestOptions.body)
           const response = await fetch(this.$store.state.hostname + "/HospitalServiceTypePacks", requestOptions);
           const data = await response.json();
           if(data.id)
            {
              console.log(data)
              // this.$refs.message.success('Added_successfully')
              this.$store.state.alert = true;
              this.$router.push("/package");
            }
            else{

              this.modal_info = data.detail + "    (" + data.routine + ")" ;
              this.modal_status = true;

            }

      },
    },

   async mounted(){
      await this.fetch_auth_list();
      await this.fetch_service_type();
    },
}
</script>


<style scoped>
main {
  background-color: #fff;
}
.flexible-navbar {
  transition: padding-left 0.5s;
  padding-left: 270px;
}

@media (max-width: 1199.98px) {

  .flexible-navbar {
    padding-left: 10px;
  }
}

.applied{
  pointer-events: none;
  background:#fcfcfc;
}

.timepiker{
  position: relative;
  width: 100%;
  margin-top:32px;
  cursor: pointer;
}

.fixed-column {
    /* overflow-y: auto; */

    margin-left: 100px;
  }

  .fixed-column th, .fixed-column td {
    white-space: nowrap;
  }

  .fixed-column tr th:first-child, .fixed-column tr td:first-child {
    position: absolute;
    width: 50px;
    top: auto;
    margin-left: -50px;
    margin-top: 0;
  }
  .delIcon:hover{
    color: #000;
    }
  .delIcon{
    color: red;
  }
  .gradint{
    background: #8E2DE2;  /* fallback for old browsers */
    background: -webkit-linear-gradient(to right, #4A00E0, #8E2DE2);  /* Chrome 10-25, Safari 5.1-6 */
    background: linear-gradient(to right, #4A00E0, #8E2DE2); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
  }
</style>


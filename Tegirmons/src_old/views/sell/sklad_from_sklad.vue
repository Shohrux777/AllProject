<template>
  <div>
    <loader v-if="loading"/>

    <div class="changing_add px-3 pt-4 pb-5">
      <div class="row mb-3">
        <div class="col-3">
          <input class="m-0 form-control" style="height:32px; font-size: 13px;" v-model="user_name" size="md"  outline  group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -11px; left: 20px; font-size: 11px;"
            class="bg-white px-2 py-0"
            >{{ $t("user_name") }}</small
          >
        </div>
        <div class="col-3">
          <erpSelectSklad
              :options="allSklad.rows"
              @select="selectOptionSklad"
              :selected="sklad_name"
              :label="$t('sklad')"
              size="sm"
            />
            <small
              class="invalid-text"
              style="margin-left: 10px; margin-top: -5px; font-size: 12px;"
              v-if="$v.sklad_name.$dirty && !$v.sklad_name.required"
            >
              {{ $t("Select_sklad") }}
            </small>
        </div>
        <div class="col-3">
          <erpSelectSklad
              :options="allSklad.rows"
              @select="selectOptionSkladga"
              :selected="skladga_name"
              :label="$t('sklad')"
              size="sm"
            />
            <small
              class="invalid-text"
              style="margin-left: 10px; margin-top: -5px; font-size: 12px;"
              v-if="$v.skladga_name.$dirty && !$v.skladga_name.required"
            >
              {{ $t("Select_sklad") }}
            </small>
        </div>
        
        <div class="col-3">
            <input class="m-0 form-control"  style="height:32px; font-size: 13px;" v-model="note" size="md"  outline  group type="text" validate error="wrong" success="right"/>
          <small
            style="position: absolute; top: -11px; left: 20px; font-size: 11px"
            class="bg-white px-2 py-0"
            >{{ $t("note") }}</small
          >
        </div>
      </div>

      <form @submit.prevent="submit">
        <div class="d-flex w-100">
          <div class=" ">
            <mdb-btn
              class="mr-1"
              style="font-size: 12px"
              outline="orange"
              darkWaves
              tag="a"
              floating
              @click="addRow"
              icon="plus"
              size="sm"
              >{{ $t("Add_product") }}</mdb-btn
            >

            <!-- <mdb-btn-group style="margin-top: -14px">
              <mdb-dropdown>
                <mdb-btn
                  style="font-size: 12px"
                  outline="primary"
                  darkWaves
                  tag="a"
                  floating
                  icon="bars"
                  size="sm"
                  slot="toggle"
                ></mdb-btn>
                <mdb-dropdown-menu>
                  <mdb-dropdown-item
                    style="font-size: 12px"
                    >{{ $t("") }}</mdb-dropdown-item
                  >
                  <mdb-dropdown-item style="font-size:12px"  >{{'Export_to_PDF'|locolize}}</mdb-dropdown-item>
                </mdb-dropdown-menu>
              </mdb-dropdown>
            </mdb-btn-group> -->
          </div>
          
        </div>
        
        <div class="d_table">
          <mdb-tbl class="fixed-column">
            <mdb-tbl-head class="m-0 p-0">
              <tr>
                <td></td>
                <!-- product -->
                <td style="width:570px; font-size: 13px">
                  {{ $t(datasource.columns[0]) }}
                </td>
                <td
                  style=" font-size: 13px"
                >
                  {{ $t(datasource.columns[1]) }}
                </td>
                <td
                  style=" font-size: 13px"
                >
                  {{ $t(datasource.columns[2]) }}
                </td>
                
               
                
                <!-- ed_izm -->
              </tr>
            </mdb-tbl-head>
            <mdb-tbl-body>
              <tr
                scope="row"
                v-for="(row, rowIndex) in datasource.rows"
                :key="rowIndex"
      
              >
                <th>
                  <i
                    class="
                      fas
                      fa-trash
                      delIcon
                      mask
                      waves-effect
                      m-0
                      pl-2
                    "
                    @click="datasource.rows.splice(parseInt(rowIndex), 1)"
                  ></i>
                </th>
                <th>
                  <lineSelect
                    :options="product_list"
                    :row_index="rowIndex"
                    :searchshow="true"
                    @select="selectproduct"
                    :selected="row.product_name"
                  
                    :label="$t('product')"
                  />
                  <small
                    class="invalid-text"
                    style="
                      margin: 0;
                      margin-top: 26px;
                      position: relative;
                    "
                    v-if="
                      $v.datasource.rows.$each[rowIndex].product_name
                        .$dirty &&
                      !$v.datasource.rows.$each[rowIndex].product_name
                        .required
                    "
                  >
                    {{ $t("Select_product") }}
                  </small>
                </th>

                <th>
                  <input
                    v-model="row.qty_str"
                    type="text"
                    class="form-control form-control-sm"
                    @input="changeSumma($event.target.value,rowIndex)"
                  />
                  <small
                    class="invalid-text"
                    style="
                      margin: 0;
                      margin-top: 26px;
                      position: relative;
                    "
                    v-if="($v.datasource.rows.$each[rowIndex].qty.$dirty &&
                        !$v.datasource.rows.$each[rowIndex].qty.required)
                    "
                  >
                    {{ $t("minValue") }}
                  </small>
                </th>
                <th>
                  <input
                    :value="row.ostatka.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')"
                    disabled
                    type="text"
                    class="form-control form-control-sm"
                  />
                </th>
                
              </tr>
            </mdb-tbl-body>
          </mdb-tbl>
        </div>
         
         
        
        <div class="blue-gradient">
          <hr class="mt-1 mb-0"/>
        </div>

        <div class="col-12 px-4 mt-3 mb-2">
          <div class="photoUrlShow">
            <img :src="hostname + photo_url" v-if="photo_url" style="height: 110px;" class="img-fluid img-thumbnail img-responsive shadow" alt="">
          </div>
        </div>

        <mdb-row class="mt-4">
          <mdb-col col="12">
            <div class="mt-0 text-right">
              <mdb-btn   @click="photoRasxod()" color="info"  style="font-size: 10px; padding: 6px 0;" class="m-0 px-3 ml-2 mb-0"
                > <mdb-icon fas class="mr-1"  icon="camera"></mdb-icon>  {{$t('photo')}}
              </mdb-btn>
              <mdb-btn color="success" v-show="photo_url" type="submit" style="font-size: 10px; padding: 6px 0;"
                class="m-0 px-3 ml-2 mb-0">
                <mdb-icon  />
                {{$t('add')}}
              </mdb-btn>
            </div>
          </mdb-col>
        </mdb-row>
        
      </form>
      <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>
      <webcam  v-if="showPhoto" @getPhotosub="takePhoto"/>

      <Toast ref="message"></Toast>
    </div>
  </div>
</template>

<script>
import webcam from '../webcam/webcam_Add.vue'

import { mdbInput, mdbRow, mdbCol, mdbIcon, mdbBtn,  mdbTbl, 
  mdbTblHead,
  mdbTblBody,
  mdbBtnGroup,
  mdbDropdown,
  mdbDropdownItem,
  mdbDropdownMenu,} from "mdbvue"
import { required } from 'vuelidate/lib/validators'
import lineSelect from "../../components/lineSelect.vue";
import {mapActions,mapGetters} from 'vuex'
// import { all } from "core-js/fn/promise";
import erpSelectSklad from "../../components/erpSelect.vue";

export default {
  naem: "changingAdd",
  
  data(){
    return{
      hostname: this.$store.state.server_ip,
      photo_url: '',
      showPhoto: false,

      modal_info: '',
      modal_status: false,
      loading: false,
      id: this.$route.params.id,
      name: '',
      phone_number: '',

      product_name: '',
      product_id: null,
      subproduct_name: '',
      subproduct_id: null,

      user_name: localStorage.user_name,
      all_sum:0,
      note: '',


      producKg: 1,
      subproducKg: 0,
      percanteg: 0,
      datasource: {
        rows: [],
        // real_time
        columns: [
          "product",
          "kg_ves",
          "ostatka",
          "summ"
        ],
      },
      sklad_name: '',
      sklad_id: null,
      skladga_name: '',
      skladga_id: null,
      product_list: [],
    }
  },
  components: {
    mdbInput, mdbRow, mdbCol, mdbIcon, mdbBtn, mdbTbl, mdbBtnGroup,
    mdbDropdown, 
    mdbDropdownItem,
    mdbDropdownMenu,
    mdbTblHead,
    mdbTblBody,
    lineSelect,
    erpSelectSklad,
    webcam
  },
  validations: {
    datasource: {
      rows: {
        $each: {
          qty: {  required },
          product_name: { required },
        },
      },
    },
    sklad_name: {
      required
    },
    skladga_name: {
      required
    }
  },
 
  async mounted() {
    await this.fetchSklad();
  },
  computed: mapGetters([ 'allSklad', 'all_product_t',]),
  methods:{
    ...mapActions(['fetchSklad', 'fetch_product_t']),
    cls_wnd()
    {
      
    },
    takePhoto(img){
      this.photo_url = img;
      this.showPhoto = false;
    },
    async photoRasxod(){
      this.showPhoto = true;
    },
    async selectOptionSklad(option){
      this.sklad_name = option.name;
      this.sklad_id = option.id;
      await this.updateOstatkaSkladId(option.id);
      console.log(this.all_product_t)
    },
    async selectOptionSkladga(option){
      this.skladga_name = option.name;
      this.skladga_id = option.id;
    },

    async updateOstatkaSkladId(id){
      try{
        this.product_list = [];
        // this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonSkladHistory/getSkladStatistika?sklad_id=" + id);
        const data = await response.json();
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          console.log('datwa');
          console.log(data);
          for(let i=0; i<data.length; i++){
            let temp = {
              name: data[i].product.name,
              id: data[i].product.id,
              qty: data[i].currentQty,
            }
            this.product_list.push(temp);
          }
          console.log('datwa');
          console.log(this.product_list);
          // this.product_list = data;
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

    selectproduct(option) {
      console.log(option);
      this.datasource.rows[option.row].product_name = option.data.name;
      this.datasource.rows[option.row].tegirmonProductid = option.data.id;
      this.datasource.rows[option.row].ostatka = option.data.qty.toFixed(1);
      // this.datasource.rows[option.row].price = option.data.price;
    },
     addRow(){
      this.datasource.rows.unshift({
        tegirmonProductid: 0,
        product_name: "",
        qty: 0,
        ostatka: 0,
        qty_str: '',
      });
      console.log(this.datasource)
    },
    changeSumma(n,i){
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
       this.datasource.rows[i].qty_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
       var temp = ''
       for(let j=0; j<this.datasource.rows[i].qty_str.length; j++){
        if(this.datasource.rows[i].qty_str[j] != ' '){
          temp += this.datasource.rows[i].qty_str[j];
        }
      }
      if(parseFloat(temp)>parseFloat(this.datasource.rows[i].ostatka)){
        this.datasource.rows[i].qty = parseFloat(this.datasource.rows[i].ostatka);
        this.datasource.rows[i].qty_str = this.datasource.rows[i].ostatka.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      else{
        this.datasource.rows[i].qty = parseFloat(temp);
      }
      
    },
    save_data :  async function(){        
         if(this.$v.$invalid )
        {
          this.$v.$touch();
          this.$refs.message.warning('please_fill')
          return false;
        }
        const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
              // "tegirmonProductid" : this.product_id,
              "photo_url": this.photo_url,
              "note": this.note,
              "user_name": this.user_name,
              "item_list": this.datasource.rows,
              "from_sklad_id": this.sklad_id,
              "to_sklad_id": this.skladga_id,
              "auth_id": localStorage.AuthId
            })
          };
          try{
            this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/TegirmonSklad/transfer_sklad_to_sklad", requestOptions);
            const data = await response.json();
            console.log('data')
            console.log(data)
            this.loading = false;
            if(response.status == 201 || response.status == 200)
            {
              this.$refs.message.success('Added_successfully')
              this.$emit('close');
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
    async submit()
      {
        if(await this.save_data())
        {
          this.$store.state.alert = true;
          this.$emit('close');
        }

      },
  }
}
</script>

<style lang="scss" scoped >
td{
  // margin: 0px !important;
  padding: 5px 15px !important;
}
.delIcon:hover {
  color: #000;
}
.delIcon {
  padding-top: 8px;
  color: red;
}
</style>

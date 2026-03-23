<template>
  <div class="add_tarozi_container">
    <div class="add_tarozi_header border-bottom pt-2 px-3">
      <div class="row">
        <!-- <div class="col-3">
          <mdb-input class="m-0 p-0 w-100" v-model="search" 
            :placeholder="$t('search_client')" size="sm"
            outline  group type="text" validate error="wrong" success="right"/>
        </div> -->
        <div class="col-3">
          <div>
            <erpSelect
            :options="client_list_last"
            @select="selectOptionUser"
            :selected="client_name"
            :label="$t('client_select_fio')"
            size="sm"
            class="bg-white"
            style="margin-top:0px;"
            url="/TegirmonClientNews/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
            />
            <!-- <small style="position:absolute; top:-8px; left:10px; font-size: 11px;" class="font-weight-bold px-2 py-0">Поиск клиент</small> -->
          </div>
        </div>
        <div class="col-3">
          <input-search  @select="selectClientPassport" :label="$t('')" :selected="passport_number"
            url="/TegirmonClientNews/getPaginationSearchByFioOrPassportSerailNumberOrHomeOrMobilePhoneNumber?page=0&size=100&fio_or_serial_number="
            ref="search_client" placeholder="AA12345678"  style="height:32px;">
          </input-search>
        </div>
        <div class="col-3">
          <input-search-year  @select="selectClientBorn" type="text" :label="$t('')" :selected="born_date" placeholder="дд-мм-гггг"
            url="/TegirmonClientNews/getPaginationByBornDate?page=0&size=100&born_date_str="
            ref="search_client_born_date" style="height:32px;">
          </input-search-year>
        </div>
        <div class="col-3 text-right">
          <mdb-btn v-if="client_id" class="m-0 py-2 px-4 mr-2" @click="add_invoice_item"
              style="font-size: 8px;" color="primary"
              size="sm"><mdb-icon fas class="mr-1"  icon="plus"></mdb-icon> 
          </mdb-btn>
          <mdb-btn class="m-0 py-2 px-4" @click="add_client_func"
              style="font-size: 8px;" color="success"
              size="sm"><mdb-icon fas class="mr-1"  icon="plus"></mdb-icon> {{$t('add_client')}}
          </mdb-btn>
        </div>
      </div>
    </div>
    <loader v-if="loading" />
    <div  v-else class="add_tarozi_main px-3">
      <div class="add_tarozi_item rounded shadow mt-3 px-2 pt-3 pb-2" v-for="(item, index) in product_list" :key="index">
        <div class="row">
          <div class="col-3">
            <mdb-input class="m-0 p-0 w-100" v-model="item.shafyor_name" 
            :placeholder="$t('shafyor_name')" size="sm"
            outline  group type="text" validate error="wrong" success="right"/>
          </div>
          <div class="col-3">
            <mdb-input class="m-0 p-0 w-100" v-model="item.car_number" 
            :placeholder="$t('car_number')" size="sm"
            outline  group type="text" validate error="wrong" success="right"/>
          </div>
          <div class="col-3">
            <div style="position:relative;">
              <input  
                class="mt-0 mb-0 rounded input_style" 
                @input="changeInput(index)" v-model="item.shafyor_tel" :placeholder="$t('shafyor_tel')" outline/>
            </div>
          </div>
          <div class="col-3">
            <mdb-input class="m-0 p-0 w-100" v-model="item.note" 
            :placeholder="$t('note')" size="sm"
            outline  group type="text" validate error="wrong" success="right"/>
          </div>
          <div class="col-3 mt-1">
            <erpSelectProduct
                :options="all_product_t.rows"
                @select="selectOptionPro"
                :selected="item.product_name"
                :label="$t('client')"
                :row="index"
              />
              <!-- <small
                v-if="$v.product_name.$dirty && product_id == null"
                class="invalid-text  ml-2"
                style="margin-top: -8px;"
              >
                {{ $t("select_item") }}
              </small> -->
          </div>
          <div class="col-3 mt-1">
            <mdb-input class="m-0 p-0 w-100" v-model="item.product_price" 
            :placeholder="$t('product_price')" size="sm"
            outline  group type="number" validate error="wrong" success="right"/>
          </div>
          <div class="col-3 mt-1">
            <mdb-input class="m-0 p-0 w-100" v-model="item.all_measure"
            :placeholder="$t('all_measure')" size="sm"
            outline  group type="number" validate error="wrong" success="right"/>
          </div>
          <div class="col-3 mt-1">
            <mdb-btn class="m-0 py-1 px-3" @click="getPhoto(index)"
                style="font-size: 12px;" color="primary"
                size="sm"><mdb-icon fas  icon="camera-retro"></mdb-icon>
            </mdb-btn>
            <mdb-btn v-if="product_list.length>1" class="m-0 py-1 px-3 ml-2" @click="delItem(index)"
                style="font-size: 12px;" color="danger"
                size="sm"><mdb-icon fas  icon="trash"></mdb-icon>
            </mdb-btn>
          </div>
        </div>
        <div class="photo mt-0 d-flex justify-content-start" v-if="item.image_first_pull">
          <img :src="hostname + item.image_first_pull" width="180" alt="" class="shadow border rounded">
        </div>
      </div>
    </div>
    <div class="add_container text-right border-top mt-3 mx-3 pt-1">
      <mdb-btn class="m-0 py-2 px-4" @click="save_items"
          style="font-size: 8px;" color="success"
          size="sm"><mdb-icon fas class="mr-1"  icon="check"></mdb-icon> {{$t('save')}}
        </mdb-btn>
    </div>
    <modal-train  :show="add_client_show" headerbackColor="white"  titlecolor="black" :title="$t('show_davernis')" 
      @close="add_client_show = false" width="85%">
        <template v-slot:body>
          <addClientNew @close="add_client_show = false"  @close_data="close_clientAdd" ref="addClientrefs">
          </addClientNew>
        </template>
    </modal-train>
    <webcam  v-show="showPhoto" @getPhotosub="takePhoto" ref="camera_component"/>
  </div>
</template>

<script>
import {mdbInput, mdbBtn, mdbIcon} from 'mdbvue'
import {mapActions,mapGetters} from 'vuex'
import webcam from '../webcam/webcam_Add.vue'
import addClientNew from './new_client_add.vue'
import erpSelect from "../../components/erpSelectFioSearch.vue";
import erpSelectProduct from "../../components/erpSelectProduct.vue";
import InputSearch from '../../components/inputSearch';
import inputSearchYear from '../../components/inputSearchYear';
export default {
  components: {
    mdbInput, mdbBtn, mdbIcon,
    erpSelectProduct,
    addClientNew,
    erpSelect,
    InputSearch,
    inputSearchYear,
    webcam
  },
  data() {
    return {
      hostname: this.$store.state.server_ip,
      search: '',
      loading: false,
      showPhoto: false,
      add_client_show: false,
      product_list: [
        {
          shafyor_name: '',
          car_number: '',
          shafyor_tel: '',
          note: '',
          product_name: '',
          product_id: null,
          product_price: null,
          all_measure: null,
          car_measure: 0,
          product_measure: 0,
          image_first_pull: '',
          date_first_pull: null,
          pull_status: true,
          auth_first_pull_id: localStorage.AuthId,
          auth_first_pull_name: localStorage.user_name,
        }
      ],
      photo_index: 0,
      client_name: '',
      client_id: null,
      client_info: {},
      passport_number: '',
      born_date: '',
      client_list_last: [],
    }
  },
  async mounted () {
    await this.fetch_product_t();
    for(let i=0; i<this.all_product_t.rows.length; i++){
        if(this.all_product_t.rows[i].inv_accepted_status){
          this.product_list[0].product_name = this.all_product_t.rows[i].name;
          this.product_list[0].product_id = this.all_product_t.rows[i].id;
          this.product_list[0].product_price = this.all_product_t.rows[i].price;
        }
      }
      // await this.fetchClient()
      await this.fetchClientLast()
  },
  computed: mapGetters(['all_product_t', 'allClient']),
  methods: {
    ...mapActions(['fetch_product_t', 'fetchClient']),
    async add_client_func(){
      this.add_client_show = !this.add_client_show;
      this.$refs.addClientrefs.MountedFunc();
    },
    async add_invoice_item(){
      let item = {
        shafyor_name: '',
          car_number: '',
          shafyor_tel: '',
          note: '',
          product_name: '',
          product_id: null,
          product_price: null,
          all_measure: null,
          car_measure: 0,
          product_measure: 0,
          image_first_pull: '',
          date_first_pull: null,
          pull_status: true,
          auth_first_pull_id: localStorage.AuthId,
          auth_first_pull_name: localStorage.user_name,
      }
      this.product_list.unshift(item);
      for(let i=0; i<this.all_product_t.rows.length; i++){
        if(this.all_product_t.rows[i].inv_accepted_status){
          this.product_list[0].product_name = this.all_product_t.rows[i].name;
          this.product_list[0].product_id = this.all_product_t.rows[i].id;
          this.product_list[0].product_price = this.all_product_t.rows[i].price;
        }
      }
    },
    async delItem(i){
      this.product_list.splice(i,1);
    },
    async save_items(){
      this.loading = true;
      for(let i=0; i<this.product_list.length; i++){
        await this.save_item_index(i);
      }
      this.loading = false;
      this.$router.push('tarozi_list')
    },
    async save_item_index(index){
      console.log('this.product_list[index]')
      console.log(this.product_list[index])
      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "shafyor_name" : this.product_list[index].shafyor_name,
          "car_number" : this.product_list[index].car_number,
          "shafyor_tel" : this.product_list[index].shafyor_tel,
          "note": this.product_list[index].note,
          "tegirmonProductid": this.product_list[index].product_id,
          "product_price" : this.product_list[index].product_price,
          "text_1" : this.product_list[index].product_name,
          "image_first_pull": this.product_list[index].image_first_pull,
          "all_measure": this.product_list[index].all_measure,
          "tegirmonClientNewid": this.client_id,
          "text_2": this.client_name,
          "id" : 0,
          "pull_status": this.product_list[index].pull_status,
          "auth_first_pull_id": localStorage.AuthId,
          "auth_first_pull_name": localStorage.user_name,
          "tegirmonAuthid": localStorage.AuthId,
        })
      };
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonInvoiceNews", requestOptions);
        this.loading = false;
        if(response.status == 201 || response.status == 200)
        {
          const tryjson = await response.json();
          console.log(tryjson)
          this.$refs.message.success('Added_successfully')
        }
        else{
          const data = await response.text();
          this.modal_info = data;
          this.modal_status = true;
        }
      }
      catch{
        this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect'); 
        this.modal_status = true;
      }
    },
    async fetchClientLast(){
          try{
            // this.loading = true;
            const response = await fetch(this.$store.state.hostname + "/TegirmonClientNews/getPagination?page=0&size=100");
            const data = await response.json();
            if(response.status == 201 || response.status == 200)
            {
              this.client_list_last = data.items_list;
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
    changeInput(index){
      var x = this.product_list[index].shafyor_tel.replace(/\D/g, '').match(/(\d{0,2})(\d{0,3})(\d{0,4})/);
      this.product_list[index].shafyor_tel = !x[2] ? x[1] : '(' + x[1] + ') ' + x[2] + (x[3] ? '-' + x[3] : '');
    },
    selectOptionPro(option){
      console.log(option)
      this.product_list[option.index].product_name = option.data.name;
      this.product_list[option.index].product_id = option.data.id;
      this.product_list[option.index].product_price = option.data.price;
    },
    getPhoto(index_photo){
      this.showPhoto = true;
      this.$refs.camera_component.createCameraElement();
      this.photo_index = index_photo;
    },
    takePhoto(data){
      this.product_list[this.photo_index].image_first_pull = data;
      this.showPhoto = false;
      // this.product_list.push({car_number: 9999});
      // this.product_list.splice(this.product_list.length-1, 1)
    },
    async selectOptionUser(option){
      this.client_info = option;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.passport_number = option.passport_number
      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
    },
    async selectClientPassport(option){
      this.client_info = option;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.passport_number = option.passport_number
      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
    },
    async selectClientBorn(option){
      this.client_info = option;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.passport_number = option.passport_number
      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }
    },
    close_clientAdd(option){
      this.add_client_show = false;
      console.log('option')
      console.log(option)
      this.client_info = option;
      this.client_name = option.fio;
      this.client_id = option.id;
      this.passport_number = option.passport_number
      if(option.addiotionala_information){
        this.born_date = option.addiotionala_information.slice(8,10) + '-' + option.addiotionala_information.slice(5,7) + '-' + option.addiotionala_information.slice(0,4);
      }

    }
  },
}
</script>

<style lang="scss" scoped>
 .input_style{
  width:100%; 
  font-size: 12px !important;  
  height:28px; 
  outline:none; 
  padding: 0 12px; 
  border: 1px solid #DADCE0
 }
 input:focus{
  border: 2px solid #4285F4;
 }

</style>
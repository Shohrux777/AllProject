<template>
  <div class="product-page">
    <!-- Header -->
    <div class="d-flex justify-content-between align-items-center product-header-card">
      <router-link to="/product" class="d-flex align-items-center product-header-link">
        <h5 class="m-0 product-header-title">
          {{$t('product')}}
        </h5>
      </router-link>
      <div>
        <router-link to="/product_Add/0">
          <mdb-btn
            class="product-add-btn"
            m="r3"
            size="sm"
            p="r4 l4 t2 b2"
          >
            <mdb-icon icon="plus" class="mr-2" />{{$t('add')}}
          </mdb-btn>
        </router-link>
      </div>
    </div>

    <!-- Search -->
    <div class="container-fluid mt-2">
      <div class="row">
        <div class="col-12 col-md-4">
          <div class="product-search-wrapper">
            <label class="product-search-label">
              {{$t('search_here')}}
            </label>
            <mdb-input
              type="text"
              size="md"
              class="m-0"
              @input="searchClick"
              v-model="search"
            />
          </div>
        </div>
      </div>
    </div>

    <!-- Table card (user.vue ga o'xshash) -->
    <div class="product-card-wrapper mt-1">
      <div class="card product-card-shadow">
        <div class="card-body p-2 p-md-3">
          <loaderTable v-if="loading" />
          <anyTable
            v-else
            :datasource="product_list"
            @for_delete="for_delete"
            @for_edit="for_edit"
            :status="status_table"
          />
        </div>
      </div>
    </div>

    <massage_box
      :hide="modal_status"
      :detail_info="modal_info"
      :m_text="$t('Failed_to_add')"
      @to_hide_modal="modal_status= false"
    />
    <Toast ref="message"></Toast>
  </div>
</template>

<script>
import {
  mdbIcon,
  mdbBtn,
  mdbInput
} from "mdbvue";
import {mapActions, mapGetters, mapMutations} from 'vuex'
import anyTable from "../../components/anyTable"
export default {
  data(){
    return {
      modal_info: '',
      search: '',
      modal_status: false,
      loading: false,
      auth_id: localStorage.AuthId,
      product_list: {
        rows: [],
        columns: ['name','price','code','auth_user_updator_id'],
        col: []
      },
      status_table: false,
    }
  },
  components: {
    anyTable, mdbIcon,
    mdbBtn,mdbInput
  },
  computed: mapGetters(['all_product_t']),
  methods: {
    ...mapActions(['fetch_product_t', ]),
    ...mapMutations(['product_t_delete_row']),
      async fetchUserAccess(id){
        this.status_table = false;
        try{
            const res = await fetch(this.$store.state.hostname + '/TegirmonUserAccess/getTegirmonUserAccessUserId?user_id=' + id);
            const data = await res.json();
            console.log('this is by id product')
            if(res.status == 200 || res.status == 201){
                console.log(data)
                this.status_table = data.product;
            }
        }
        catch(error){
            console.log(error)
        }
      },
      for_edit(edit_data)
      {
        this.$router.push("/product_add/" + edit_data.id);
      },
      async for_delete(del_data,index)
      {
         const requestOptions = {
            method : "delete",
          };
          const response = await fetch(this.$store.state.hostname + "/TegirmonProduct/" + del_data.id, requestOptions);
          const data = await response.text();

          if(response.status == 201 || response.status == 200)
          {
            this.$refs.message.success('Successfully_removed')
            this.alert_success = true;
            this.product_t_delete_row(index);
          }
          else{
            this.modal_info = data;
            this.modal_status = true;
          }
      },
      searchClick(){
        this.product_list.rows = this.all_product_t.rows;
          this.loading = true;
          if(this.search != ''){
            let userSearchList = [];
            for(let i=0; i<this.product_list.rows.length;i++){
              console.log(this.product_list.rows[i].name)
              if(this.product_list.rows[i].name.toLowerCase().includes(this.search.toLowerCase()) || this.product_list.rows[i].price.toString().includes(this.search.toLowerCase()) || this.product_list.rows[i].code.toLowerCase().includes(this.search.toLowerCase())){
                userSearchList.push(this.product_list.rows[i])
              }
            }
            this.product_list.rows = userSearchList;
          }
          else{
            this.product_list.rows = this.all_product_t.rows;
          }
          this.loading = false;
      }
  },
  async mounted(){
    this.loading = true;
    await this.fetch_product_t();
    this.product_list.rows = this.all_product_t.rows;
    this.loading = false;
    if(this.$store.state.alert){
      this.$refs.message.success('Added_successfully')
      this.$store.state.alert = false
    }
    await this.fetchUserAccess(localStorage.user_id);
  }
}
</script>

<style scoped>
.product-page {
  padding: 8px 10px 14px 10px;
  background: #ffffff;
  min-height: 100%;
}

.product-header-card {
  padding: 10px 16px;
  border-radius: 10px;
  background-image: linear-gradient(90deg, #64b0fb 0%, #7fd4ff 50%, #b5e3ff 100%);
  box-shadow: rgba(15, 35, 52, 0.25) 0px 8px 20px -12px;
  color: #fff;
}

.product-header-link {
  color: #ffffff;
  text-decoration: none;
}

.product-header-link:hover {
  text-decoration: none;
}

.product-header-title {
  font-size: 18px;
  font-weight: 600;
  letter-spacing: 0.3px;
}

.product-card-wrapper {
  padding: 0 2px;
}

.product-card-shadow {
  border-radius: 10px;
  border: none;
  box-shadow: rgba(60, 64, 67, 0.3) 0px 1px 2px 0px,
              rgba(60, 64, 67, 0.15) 0px 1px 3px 1px;
  background: #ffffff;
}

.product-add-btn {
  font-size: 11px !important;
  padding: 4px 12px !important;
  border-radius: 16px;
  background-image: linear-gradient(90deg, #1e88e5 0%, #42a5f5 100%) !important;
  color: #ffffff !important;
  border: none;
  box-shadow: 0 2px 6px rgba(25, 118, 210, 0.4);
}

.product-add-btn:hover {
  filter: brightness(1.05);
  box-shadow: 0 3px 8px rgba(25, 118, 210, 0.55);
}

.product-search-wrapper {
  position: relative;
  margin-top: 10px;
}

.product-search-label {
  position: absolute;
  top: -9px;
  left: 4px;
  font-size: 11px;
  font-weight: 600;
  color: #607d8b;
  background: #f5f7fb;
  padding: 0 4px;
  z-index: 1;
}
</style>
<template>
  <div>
    <div class="border-bottom d-flex justify-content-between main_header_bg_shadow">
      <router-link to="/changing">
         <h5 class="m-0 ml-4 d-flex" style="padding: 15px 0px; font-size: 20px;">
            {{$t('changing')}}</h5>
      </router-link>
      <div>
         <router-link to="/changing_add/0">
            <mdb-btn class="main_button_add mt-3" m="r3" size="sm" p="r4 l4 t2 b2" style="font-size: 8px;">
              <mdb-icon icon="plus" class="mr-2"/>{{$t('add')}}</mdb-btn>
          </router-link>
      </div>
    </div>
    <loaderTable v-if="loading"/>
    <anyTable v-else
      :datasource="m_toProduct"
      @for_delete="for_delete"
      @for_edit="for_edit"
      :status="status_table"
    />
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>
      <Toast ref="message"></Toast>
  </div>
</template>

<script>
import {
  mdbIcon,
  mdbBtn,
} from "mdbvue";
import {mapActions, mapGetters, mapMutations} from 'vuex'
import anyTable from "../../components/anyTable"
export default {
  data(){
    return {
      modal_info: '',
      modal_status: false,
      loading: false,
      auth_id: localStorage.AuthId,
      status_table: false,
    }
  },
  components: {
    anyTable, mdbIcon,
    mdbBtn,
  },
  computed: mapGetters(['m_toProduct']),
  methods: {
    ...mapActions(['fetchToProduct', ]),
    ...mapMutations(['changing_delete_row']),
    async fetchUserAccess(id){
        this.status_table = false;
        try{
            const res = await fetch(this.$store.state.hostname + '/TegirmonUserAccess/getTegirmonUserAccessUserId?user_id=' + id);
            const data = await res.json();
            console.log('this is by id product')
            if(res.status == 200 || res.status == 201){
                console.log(data)
                this.status_table = data.product_edit;
            }
        }
        catch(error){
            console.log(error)
        }
      },
      for_edit(edit_data)
      {
        this.$router.push("/changing_add/" + edit_data.id);
      },
      async for_delete(del_data,index)
      {
         const requestOptions = {
            method : "delete",
          };
          const response = await fetch(this.$store.state.hostname + "/TegirmonProductToProductPersentage/" + del_data.id, requestOptions);
          const data = await response.text();

          if(response.status == 201 || response.status == 200)
          {
            this.$refs.message.success('Successfully_removed')
            this.alert_success = true;
            this.changing_delete_row(index);
          }
          else{
            this.modal_info = data;
            this.modal_status = true;
          }
      },
  },
  async mounted(){
    this.loading = true;
    await this.fetchToProduct();
    this.loading = false;
    if(this.$store.state.alert){
      this.$refs.message.success('Added_successfully')
      this.$store.state.alert = false
    }
    await this.fetchUserAccess(localStorage.user_id);
  }
}
</script>

<style>

</style>
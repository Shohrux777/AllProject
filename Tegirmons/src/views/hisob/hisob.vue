<template>
  <div>
    <div class="border-bottom d-flex justify-content-between main_header_bg_shadow">
      <router-link to="/hisob">
         <h5 class="m-0 ml-4 d-flex" style="padding: 15px 0px">
            {{$t('hisob')}}</h5>
      </router-link>
      <div>
         <router-link to="/hisob_add/0">
            <mdb-btn class="main_button_add mt-3" m="r3" size="sm" p="r4 l4 t2 b2" style="font-size: 8px;">
              <mdb-icon icon="plus" class="mr-2"/>{{$t('add')}}</mdb-btn>
          </router-link>
      </div>
    </div>
    <loaderTable v-if="loading"/>
    <anyTable v-else
      :datasource="allHisob"
      @for_delete="for_delete"
      @for_edit="for_edit"
      @update_column="checklist"
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
      auth_id: localStorage.AuthId
    }
  },
  components: {
    anyTable, mdbIcon,
    mdbBtn,
  },
  computed: mapGetters(['allHisob']),
  methods: {
    ...mapActions(['fetchHisob', 'fetchHisob_column']),
    ...mapMutations(['hisob_delete_row']),
    async checklist (){
        this.loading = true;
           await this.fetchHisob_column(this.auth_id)
        this.loading = false;
      },
      for_edit(edit_data)
      {
        this.$router.push("/hisob_add/" + edit_data.id);
      },
      async for_delete(del_data,index)
      {
         const requestOptions = {
            method : "delete",
          };
          const response = await fetch(this.$store.state.hostname + "/TegirmonHisoblar/" + del_data.id, requestOptions);
          const data = await response.text();

          if(response.status == 201 || response.status == 200)
          {
            this.$refs.message.success('Successfully_removed')
            this.alert_success = true;
            this.hisob_delete_row(index);
          }
          else{
            this.modal_info = data;
            this.modal_status = true;
          }
      },
  },
  async mounted(){
    this.loading = true;
    await this.fetchHisob();
    // await this.fetchHisob_column(this.auth_id);
    this.loading = false;
    if(this.$store.state.alert){
      this.$refs.message.success('Added_successfully')
      this.$store.state.alert = false
    }
  }
}
</script>

<style>

</style>
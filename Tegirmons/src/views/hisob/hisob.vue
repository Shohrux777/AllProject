<template>
  <div class="hisob-page">
    <!-- Header -->
    <div class="d-flex justify-content-between align-items-center hisob-header-card">
      <router-link to="/hisob" class="d-flex align-items-center hisob-header-link">
        <h5 class="m-0 hisob-header-title">
          {{$t('hisob')}}
        </h5>
      </router-link>
      <div>
        <router-link to="/hisob_add/0">
          <mdb-btn
            class="hisob-add-btn"
            m="r3"
            size="sm"
            p="r4 l4 t2 b2"
          >
            <mdb-icon icon="plus" class="mr-2" />{{$t('add')}}
          </mdb-btn>
        </router-link>
      </div>
    </div>

    <!-- Table card -->
    <div class="hisob-card-wrapper mt-2">
      <div class="card hisob-card-shadow">
        <div class="card-body p-2 p-md-3">
          <loaderTable v-if="loading"/>
          <anyTable v-else
            :datasource="allHisob"
            @for_delete="for_delete"
            @for_edit="for_edit"
            @update_column="checklist"
          />
        </div>
      </div>
    </div>

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

<style scoped>
.hisob-page {
  padding: 8px 10px 14px 10px;
  background: #ffffff;
  min-height: 100%;
}

.hisob-header-card {
  padding: 10px 16px;
  border-radius: 10px;
  background-image: linear-gradient(90deg, #64b0fb 0%, #7fd4ff 50%, #b5e3ff 100%);
  box-shadow: rgba(15, 35, 52, 0.25) 0px 8px 20px -12px;
  color: #fff;
}

.hisob-header-link {
  color: #ffffff;
  text-decoration: none;
}

.hisob-header-link:hover {
  text-decoration: none;
}

.hisob-header-title {
  font-size: 18px;
  font-weight: 600;
  letter-spacing: 0.3px;
}

.hisob-add-btn {
  font-size: 11px !important;
  padding: 4px 12px !important;
  border-radius: 16px;
  background-image: linear-gradient(90deg, #1e88e5 0%, #42a5f5 100%) !important;
  color: #ffffff !important;
  border: none;
  box-shadow: 0 2px 6px rgba(25, 118, 210, 0.4);
}

.hisob-add-btn:hover {
  filter: brightness(1.05);
  box-shadow: 0 3px 8px rgba(25, 118, 210, 0.55);
}

.hisob-card-wrapper {
  padding: 0 2px;
}

.hisob-card-shadow {
  border-radius: 10px;
  border: none;
  box-shadow: rgba(60, 64, 67, 0.3) 0px 1px 2px 0px,
              rgba(60, 64, 67, 0.15) 0px 1px 3px 1px;
  background: #ffffff;
}
</style>
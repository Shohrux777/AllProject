<template>
  <form @submit.prevent="submit_add" class="container">
    <MDBInput
      type="text"
      id="form1"
      class="form-icon-trailing"
      :label="$t('name')"
      v-model="dept_name"
    >
      <!-- <MDBIcon icon="exclamation-circle" class="trailing"></MDBIcon> -->
    </MDBInput>

    <erpSelect
      :options = "get_company_list.rows"
      @select="sub_comp_select"
      :selected="company_name"
      :label="$t('otdel')"
      class="mt-2"
    />

    <div>
      <MDBModalFooter>
        <MDBBtn style="font-size: 11px;" @click="$emit('close')" color="secondary" >Close</MDBBtn>
        <MDBBtn style="font-size: 11px;" type="submit" color="success">Save</MDBBtn>
      </MDBModalFooter>
    </div>
  </form>
</template>

<script>
import { MDBInput, MDBIcon, MDBModalFooter, MDBBtn } from "mdb-vue-ui-kit";
import erpSelect from '@/components/erpSelectAdd.vue'
import {mapActions, mapGetters} from 'vuex'

export default {
  components: {
      MDBInput,
      MDBIcon,
      MDBModalFooter,
      erpSelect,
      MDBBtn
    },
    data(){
      return{
        list: [
          {
            name: 'dadasdasd',
            id: 1
          }
        ],
        dept_name: '',
        id: 0,
        company_name: '',
        company_id: null,
      }
    },
    computed: mapGetters(['get_deparment_list', 'get_company_list']),
    async mounted(){
        await this.fetch_Company();
    },
    methods:{
      ...mapActions(['fetch_Department', 'fetch_Company']),

      sub_comp_select(option){
        console.log(option)
        this.company_name = option.name;
        this.company_id = option.id;
      },
      async submit_add(){
        try{
          const requestOptions = {
          method: "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "deptid": this.id,
            "deptname" : this.dept_name,
            "skudCompanyid": this.company_id,
            })
          };
          const response = await fetch(this.$store.state.hostname + "/SkudMyDepartments", requestOptions);
          const data = await response.json();
          console.log(data)
          if(data.deptid != 0){
            this.$emit('close')
            await this.fetch_Department();
          }
        }
        catch{
          console.log('error')
        }

      }
    }
}
</script>

<style>

</style>
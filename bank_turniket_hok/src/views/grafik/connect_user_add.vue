<template>
  <form @submit.prevent="submit_add" class="container">
    <div class="row mb-3">
      <div class="col-6">
        <erpSelectSmena
          :options = "get_smena_list.rows"
          @select="sub_smena_select"
          :selected="smena_name"
          :label="$t('smena')"
          class="mt-2"
        />
        <MDBInput
          type="date"
          id="form1"
          :label="$t('b_date')"
          v-model="b_date"
        >
        </MDBInput>
        <MDBInput
          type="date"
          id="form2"
          class="form-icon-trailing mt-3"
          :label="$t('e_date')"
          v-model="e_date"
        >
        </MDBInput>
      </div>
      <div class="col-6">
        <erpSelect
          :options = "get_deparment_list.rows"
          @select="sub_dept_select"
          :selected="dept_name"
          :label="$t('otdel')"
          class="mt-2"
        />
      </div>
    </div>
    

    

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
import erpSelectSmena from '@/components/erpSelectSmenaAdd.vue'
import {mapActions, mapGetters} from 'vuex'

export default {
  components: {
      MDBInput,
      MDBIcon,
      MDBModalFooter,
      erpSelect,
      MDBBtn,
      erpSelectSmena
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
        dept_name: '',
        dept_id: null,
        smena_name: '',
        smena_id: null,
        b_date: ' ',
        e_date: ' ',
      }
    },
    computed: mapGetters(['get_deparment_list', 'get_smena_list']),
    async mounted(){
        await this.fetch_Department();
        await this.fetch_Smena();
    },
    methods:{
      ...mapActions(['fetch_Department', 'fetch_Smena']),

      async sub_dept_select(option){
        console.log(option)
        this.dept_name = option.name;
        this.dept_id = option.id;
        await this.fetch_user_dept_id(option.id);
      },
      sub_smena_select(option){
        console.log(option)
        this.smena_name = option.smena_nomi
        this.smena_id = option.id
      },
      async fetch_user_dept_id(id){
        try{
          const response = await fetch(this.$store.state.hostname + "/SkudMyUserinfoes/getPaginationByDepartmentId?page=0&size=11000&dep_id=" + id);
          const data = await response.json();
          console.log('data')
          console.log(data)
        }
        catch(error){
          console.log(error)
        }
      },
      async submit_add(){
        try{
          const requestOptions = {
          method: "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "deptid": this.id,
            "deptname" : this.dept_name,
            "skudCompanyid": this.dept_id,
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
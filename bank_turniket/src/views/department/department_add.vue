<template>
  <form @submit.prevent="submit_add" class="container">
    <MDBInput
      type="text"
      id="form1"
      class="form-icon-trailing"
      :label="$t('name')"
      v-model="dept_name"
    >
      <MDBIcon icon="exclamation-circle" class="trailing"></MDBIcon>
    </MDBInput>

    <erpSelect
      :options = "get_deparment_list.rows" 
      @select="sub_debt_select"
      :selected="subdept_name"
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
        subdept_name: '',
        subdept_id: null,
      }
    },
    computed: mapGetters(['get_deparment_list']),
    async mounted(){
        await this.fetch_Department();
    },
    methods:{
      ...mapActions(['fetch_Department']),

      sub_debt_select(option){
        console.log(option)
        this.subdept_name = option.name;
        this.subdept_id = option.id;
      },
      async submit_add(){
        try{
          const requestOptions = {
          method: "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "deptid": this.id,
            "deptname" : this.dept_name,
            "supdeptid": this.subdept_id,
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
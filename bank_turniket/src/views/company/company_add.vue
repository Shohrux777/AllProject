<template>
  <form @submit.prevent="submit_add" class="container">
    <MDBInput
      type="text"
      id="form1"
      class="form-icon-trailing"
      :label="$t('name')"
      v-model="name"
    >
      <!-- <MDBIcon icon="exclamation-circle" class="trailing"></MDBIcon> -->
    </MDBInput>

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
import {mapActions, mapGetters} from 'vuex'

export default {
  components: {
      MDBInput,
      MDBIcon,
      MDBModalFooter,
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
        name: '',
        id: 0,
        subdept_name: '',
        subdept_id: null,
      }
    },
    computed: mapGetters(['get_deparment_list']),
    async mounted(){
    },
    methods:{
      ...mapActions(['fetch_Company']),

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
            "id": this.id,
            "name" : this.name,
            })
          };
          const response = await fetch(this.$store.state.hostname + "/SkudCompanies", requestOptions);
          const data = await response.json();
          console.log(data)
          if(data.id != 0){
            this.$emit('close')
            await this.fetch_Company();
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
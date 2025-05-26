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

    <!-- <MDBInput
      type="number"
      id="form1"
      class="form-icon-trailing mt-3"
      :label="$t('salary')"
      v-model="money"
    >
    </MDBInput> -->
    <input type="text" v-model="money_str"  @keyup="funcCash($event.target.value)" :placeholder="$t('salary')"  
    class="form-control  mt-2 text-right pr-2" style="border:1px solid #BDBDBD; outline:none;font-size:13px; height:30px;" >
    
    <div class="d-flex my-3">
      <MDBRadio label="Почасовая" value="1" v-model="radio2" inline name="inlineRadioOptions" />
      <MDBRadio label="Дневная" class="ml-5" value="2" v-model="radio2" inline name="inlineRadioOptions" />
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
import {mapActions, mapGetters} from 'vuex'
import { MDBRadio } from 'mdb-vue-ui-kit';
import { ref } from 'vue';
export default {
  setup() {
      const radio2 = ref('1');
      return {
        radio2,
      };
    },
  components: {
      MDBInput,
      MDBIcon,
      MDBModalFooter,
      MDBBtn,
      MDBRadio
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
        money: null,
        money_str: '',
        subdept_name: '',
        subdept_id: null,
        work_type: 0,
      }
    },
    computed: mapGetters(['get_salary_list']),
    async mounted(){
      this.id = this.select_data.id;
      this.name = this.select_data.name;
      this.money = this.select_data.value;
      this.money_str = this.money.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      this.radio2 = this.select_data.reserved_value;
    },
    props:{
        select_data:{
          type: Object,
          default: {}
        },
      },
    methods:{
      ...mapActions(['fetch_Salary']),
      funcCash(n){
        var tols = ''
        for(let i=0; i<n.length; i++){
          if(n[i] != ' '){
            tols += n[i];
          }
        }

        this.money_str = tols.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
        var temp = ''
        for(let i=0; i<this.money_str.length; i++){
          if(this.money_str[i] != ' '){
            temp += this.money_str[i];
          }
        }
        console.log(temp)
        if(temp != ''){
          this.money = parseFloat(temp);
        }
      },
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
            "value": this.money,
            "reserved_value": parseInt(this.radio2),
            })
          };
          const response = await fetch(this.$store.state.hostname + "/SkudOyliks", requestOptions);
          const data = await response.json();
          console.log(data)
          if(data.id != 0){
            this.$emit('close')
            await this.fetch_Salary();
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
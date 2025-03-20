<template>
  <form @submit.prevent="submit_add" class="container">
    <MDBInput
      type="text"
      id="form1"
      class="form-icon-trailing"
      :label="$t('name')"
      v-model="name"
    >
    </MDBInput>
    <div class="row mt-4">
      <div class="col-6">
        <MDBInput
          type="time"
          id="form1"
          class="form-icon-trailing"
          :label="$t('b_time')"
          v-model="b_time"
        >
        </MDBInput>
      </div>
      <div class="col-6">
        <MDBInput
          type="time"
          id="form1"
          class="form-icon-trailing"
          :label="$t('e_time')"
          v-model="e_time"
        >
        </MDBInput>
      </div>
    </div>
    <div class="row mt-4">
      <div class="col-6">
        <MDBInput
          type="time"
          id="form1"
          class="form-icon-trailing"
          :label="$t('b_time_abed')"
          v-model="b_time_abed"
        >
        </MDBInput>
      </div>
      <div class="col-6">
        <MDBInput
          type="time"
          id="form1"
          class="form-icon-trailing"
          :label="$t('e_time_abed')"
          v-model="e_time_abed"
        >
        </MDBInput>
      </div>
    </div>
    <div class="row mt-4">
      <div class="col-6">
        <MDBInput
          type="number"
          id="form1"
          class="form-icon-trailing"
          :label="$t('kech_keldi_min')"
          v-model="kech_keldi"
        >
        </MDBInput>
      </div>
      <div class="col-6">
        <MDBInput
          type="number"
          id="form1"
          class="form-icon-trailing"
          :label="$t('vox_ketdi_min')"
          v-model="vox_ketdi"
        >
        </MDBInput>
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
        b_time: ' ',
        e_time: ' ',
        b_time_abed: ' ',
        e_time_abed: ' ',
        kech_keldi: null,
        vox_ketdi: null,
        subdept_name: '',
        subdept_id: null,
      }
    },
    computed: mapGetters(['get_deparment_list']),
    async mounted(){
    },
    methods:{
      ...mapActions(['fetch_Smena']),

      sub_debt_select(option){
        console.log(option)
        this.subdept_name = option.name;
        this.subdept_id = option.id;
      },
      toHoursAndMinutes(totalMinutes) {
        const hours = Math.floor(totalMinutes / 60);
        const minutes = totalMinutes % 60;

        return `${this.padToTwoDigits(hours)}:${this.padToTwoDigits(minutes)}`;
      },

      padToTwoDigits(num) {
        return num.toString().padStart(2, '0');
      },
      async submit_add(){
      
        try{
          const requestOptions = {
          method: "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "id": this.id,
            "smena_nomi" : this.name,
            "boshlanishi": this.b_time,
            "tugashi": this.e_time,
            "obed_b": this.b_time_abed,
            "obed_t": this.e_time_abed,
            "kech_keldi": this.toHoursAndMinutes(this.kech_keldi),
            "vox_ketdi": this.toHoursAndMinutes(this.vox_ketdi),
            })
          };
          const response = await fetch(this.$store.state.hostname + "/SkudSmenas", requestOptions);
          const data = await response.json();
          console.log(data)
          if(data.id != 0){
            this.$emit('close')
            await this.fetch_sxema_ishGrafik();
            await this.fetch_Smena();
            for(let i=1; i<=7; i++){
              await this.fetch_sxema_resultGrp(i)
            }
          }
        }
        catch{
          console.log('error')
        }

      },
      async fetch_sxema_ishGrafik(){
        try{
          const requestOptions = {
          method: "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "id": 0,
            "nomi" : this.name,
            "sikl_soni": 1,
            "kun_hafta": 'Неделья',
            })
          };
          const response = await fetch(this.$store.state.hostname + "/SkudIshGrafigis", requestOptions);
          const data = await response.json();
          console.log(data)
        }
        catch{
          console.log('error')
        }
      },
      async fetch_sxema_resultGrp(day){
        try{
          const requestOptions = {
          method: "POST",
          headers: { "Content-Type" : "application/json" },
          body: JSON.stringify({
            "ish_b": this.b_time,
            "ish_t": this.e_time,
            "obed_b": this.b_time_abed,
            "obed_t": this.e_time_abed,
            "kech_keldi": this.toHoursAndMinutes(this.kech_keldi),
            "vox_ketdi": this.toHoursAndMinutes(this.vox_ketdi),
            "g_nomi": this.name,
            "kun_nomi": day,
            "id": 0,
            })
          };
          const response = await fetch(this.$store.state.hostname + "/SkudResultGrs", requestOptions);
          const data = await response.json();
          console.log(data)
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
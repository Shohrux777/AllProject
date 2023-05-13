<template>
  <div class="p-3">
    <loader v-if="loading"/>

    <mdb-input :label="$t('fio')" class="p-0 m-0 mt-3 w-100" size="sm" v-model="fio" />
        <small class="invalid-text pt-4" style="margin-left:5px; "  v-if="$v.fio.$dirty && !$v.fio.required" >
            {{$t('name_invalid_text')}}
        </small>
    <mdb-input :label="$t('phone_number')" class="p-0 m-0 mt-3 w-100" size="sm" v-model="phone_number" />
        <small class="invalid-text pt-4" style="margin-left:5px; "  v-if="$v.phone_number.$dirty && !$v.phone_number.required" >
            {{$t('name_invalid_text')}}
        </small>
    <mdb-input :label="$t('passport_number')" class="p-0 m-0 mt-3 w-100" size="sm" v-model="passport_number" />
    <mdb-input :label="$t('address')" class="p-0 m-0 mt-3 w-100" size="sm" v-model="address" />
    <mdb-input :label="$t('note')" class="p-0 m-0 mt-3 w-100" size="sm" v-model="note" />
    <div class=" text-right">
        <mdb-btn style=" font-size: 9px;" @click="$emit('close')" class="px-3 py-2 mt-3" color="danger">{{$t('close')}}</mdb-btn>
        <mdb-btn style=" font-size: 9px;" @click="save_data" class="px-3 py-2 mt-3" color="success">{{$t('client_add')}}</mdb-btn>
    </div>
    <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_add')" @to_hide_modal="modal_status= false"/>

      <Toast ref="message"></Toast>
  </div>
</template>

<script>
import {
  mdbBtn,mdbInput
} from "mdbvue";
import { required } from 'vuelidate/lib/validators'

export default {
    components: {
        mdbBtn,mdbInput,
    },
    data() {
        return {
            modal_info: '',
            modal_status: false,
            loading: false,

            fio: '',
            phone_number: '',
            passport_number: '',
            address: '',
            note: '',
        }
    },
    validations: {
        fio: {
            required
        },
        phone_number: {
            required
        }
    },
    methods:{
        save_data :  async function(){
            if(this.$v.$invalid )
            {
            this.$v.$touch();
            this.$refs.message.warning('please_fill')
            return false;
            }
            const requestOptions = {
                method : "POST",
                headers: { "Content-Type" : "application/json" },
                body: JSON.stringify({
                "fio" : this.fio,
                "phone_number": this.phone_number,
                "passport_number": this.passport_number,
                "address": this.address,
                "note": this.note,
                "card_number": "card_number",
                "real_company_id": localStorage.CompId,
                "id": 0,
                })
            };
            try{
                this.loading = true;
                const response = await fetch(this.$store.state.hostname + "/PosClients", requestOptions);
                const data = await response.text();
                this.loading = false;
                if(response.status == 201 || response.status == 200)
                {
                this.$refs.message.success('Added_successfully')
                this.$emit('close')
                this.fio = '';
                this.phone_number = '';
                this.passport_number = '';
                this.address = '';
                this.note = '';
                return true;
                }
                else{
                this.modal_info = data;
                this.modal_status = true;
                return false;
                }
            }
            catch{
                this.loading = false;
                this.modal_info = this.$i18n.t('network_ne_connect'); 
                this.modal_status = true;
            }
        },
    }
}
</script>

<style>

</style>
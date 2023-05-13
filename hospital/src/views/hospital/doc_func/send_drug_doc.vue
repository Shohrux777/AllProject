<template>
  <div class="container_box_send_pharmacy">
    <loader v-if="loading"/>
    <div v-else class="bg_choose_patient_send_drugs container-fluid">
        <div class="row">
            <div class="col-8">
                <div class="drug_list border-right">
                    <div class="search_drug_into_list">
                        <mdb-input v-model="search" ref="searchDrag" @input="searchDrugDoc" label="Search medicine"/>
                    </div>
                    <div class="drug_pag_list">
                        <table class="myTable">
                            <thead>
                                <tr  class="header">
                                <!-- <th style="width: 40px;">№</th> -->
                                <th style="width: 300px;">{{$t('product')}}</th>
                                <th>{{$t('qty')}}</th>
                                <th>{{$t('price')}}</th>
                                <th>{{$t('srok')}}</th>
                                <th style="width: 250px;">{{$t('manifacturer_name')}}</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(item,i) in allreports" :key="i" 
                                    @click="click_drug(item)"
                                    class="hoverDrug" 
                                    :class="[{'bg_dark ': item.exp_days_count<=0},{'bg_danger ': item.exp_days_count > 0 && item.exp_days_count < 30},{'bg_warning ':  item.exp_days_count > 30 && item.exp_days_count < 60},{'bg_success ': item.exp_days_count > 60 && item.exp_days_count < 90}]">
                                    <!-- <td>{{i+1}}</td> -->
                                    <td>{{item.product_name}}</td>
                                    <td>{{item.real_qty.toFixed(1)}}</td>
                                    <td>{{item.unit_saled_price}}</td>
                                    <td>{{item.exp_date_str}}</td>
                                    <td>{{item.product.manifacturer_name}}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="col-4 m-0 p-0">
                <div class="w-100 py-2 mt-2 borderSolder d-flex justify-content-between">
                    <span class="ml-3">{{$t('summ')}}</span>
                    <div class="text-right px-3 mt-0">
                        <p>{{summa}}</p>
                    </div>
                </div>
                <div class="choosen_drag_list border">
                    <table class="myTable">
                        <thead>
                            <tr  class="header">
                            <!-- <th style="width: 40px;">№</th> -->
                            <th style="width: 200px;">{{$t('product')}}</th>
                            <th>{{$t('qty')}}</th>
                            <th>{{$t('sht')}}</th>
                            <th >Recipe</th>
                            <th >DL</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(item,i) in choosen_product_List" :key="i" 
                                class="hoverDrug" 
                                >
                                <!-- <td>{{i+1}}</td> -->
                                <td>{{item.product_name}}</td>
                                <td>{{item.qty}}</td>
                                <td>{{item.sht}}</td>
                                <td>{{item.recipe}}</td>
                                <td><i class="fas fa-trash delIcon mask waves-effect m-0 pl-2" v-on:click="deleteItemChoosen(item,i)"></i></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="send_pharmasy text-right">
                    <mdb-btn color="primary" @click="send_pharmasy_drug" class="m-0 py-2 mt-1 ml-2"> Send Pharmacy</mdb-btn>
                </div>
            </div>
        </div>


        <div class="addProductQtyAccept" v-if="show_qty_add_doc">
        <div class="acceptBox px-3 py-2">
            <input type="text" v-model="dataAccept.name" disabled class="form-control form-control-sm border mt-2" style="border:none; outline:none;font-weight:bold;" >
            <input type="text" v-model="dataAccept.manifacturer_name" disabled class="form-control form-control-sm border mt-2" style="border:none; outline:none;font-weight:bold;" >
            <!-- <input type="text" v-model="dataAccept.saledPrice" @input="changePriceSaled" class="form-control form-control-sm border mt-2" style="border:none; font-size: 18px; outline:none;font-weight:bold;" > -->
            <div class="d-flex mt-3">
            <div class="skidka " style="width:50%">
                <div style="position:relative;" >
                <input type="text" v-model="dataAccept.info" disabled class="form-control form-control-sm border mt-2 text-left" style="border:none; outline:none;font-weight:bold;" >
                <small style="position:absolute; top:-16px; left:3px;">
                    {{$t('info')}}
                </small>
                </div>
                
            </div>
            <div class=" ml-4"  style="width:50%; position:relative;">
                <div style="position:relative;" >
                <input type="text" v-model="dataAccept.expired_date" disabled class="form-control form-control-sm border mt-2 text-left" style="border:none; outline:none;font-weight:bold;" >
                <small style="position:absolute; top:-16px; left:3px;">
                    {{$t('srok')}}
                </small> 
                </div>
            </div>
            </div>
            <div class="reportproduct py-1 mt-2 border d-flex pl-3 justify-content-between align-items-center"  style="position:relative;">
                <h4 class="m-0 p-0">{{$t('price')}}</h4>
                <h4 class="m-0 p-0 font-weight-bold">{{dataAccept.price}}</h4>
            </div>
            <form v-on:submit.prevent="submit">
                <div class="d-flex align-items-center mt-3" style="position:relative;">    
                    <div class="skidka " style="width:50%">
                        <div style="position:relative;">
                        <input type="number" v-model="qty"  ref="upokovka" class="form-control form-control-md border mt-2 " style="border:none; outline:none;font-weight:bold;" >
                        <small style="position:absolute; top:-16px; left:3px;">
                            {{dataAccept.measurement}}
                        </small>
                        </div>
                    </div>
                    
                    <div class="skidka pl-2" style="width:50%">
                        <div style="position:relative;">
                        <input v-model="sht" :disabled="dataAccept.contains_number_in_pack<=1"
                        type="number"  class="form-control form-control-md border mt-2 " style="border:none; outline:none;font-weight:bold;" >
                        <small style="position:absolute; top:-16px; left:3px;">
                            {{$t('sht')}}
                        </small>
                        </div>
                    </div>
                    </div>
                    <div class="mt-3">
                        <div style="position:relative;">
                            <input v-model="recipe" 
                            type="text"  class="form-control form-control-md border mt-2 " style="border:none; outline:none;font-weight:bold;" >
                            <small style="position:absolute; top:-16px; left:3px;">
                                Recipe
                            </small>
                        </div>
                    </div>
                    <div class="btnClick text-right mt-4">
                        <mdb-btn color="success" type="submit" class="m-0 py-1 mt-1 "> Ok</mdb-btn>
                        <mdb-btn color="danger" @click="cancel_item_drug" class="m-0 py-1 mt-1 ml-2"> Cancel</mdb-btn>
                    </div>
            </form>
        </div>
        </div>
        <Toast ref="message"></Toast>
        <massage_box :hide="modal_status" :detail_info="modal_info"
        :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>
    </div>
  </div>
</template>

<script>
import {  mdbInput, mdbBtn} from 'mdbvue';
export default {
    components: {
        mdbInput,
        mdbBtn
    },
    props:{
        patient: {
            type: Object,
            default() {
                return {}
            }
        },
    },
    data(){
        return{
            loading: false,
            modal_status : false,
            modal_info : '',
            allreports: [],
            search: '',
            summa: 0,
            show_qty_add_doc: false,
            dataAccept: {},
            data_push_Drag: {},

            qty: null,
            sht: null,
            recipe: '',

            choosen_product_List: [],
        }
    },
    async mounted(){
        await this.fetch_drug_list();
    },
    methods:{
        async fetch_drug_list(){
            try{
                const res = await fetch(this.$store.state.hostname + '/PosInvoiceItems/getOstatkaExpiredDateProductsList?page=0&size=300');
                const res_data = await res.json();
                console.log('res_data');
                console.log(res_data);
                this.allreports = res_data.items_list;
            }
            catch(error){
                console.log(error)
            }
        },
        async searchDrugDoc(){
            if(this.search != ''){
                try{
                    this.barcode = '';
                    console.log('hiy')
                    this.Arrowdown = 0;

                    const response = await fetch(this.$store.state.hostname + "/PosInvoiceItems/getProductListForSaleByItemSearchByName?name=" + this.search + '&res_count=100');
                    const data = await response.json();
                    console.log('data')
                    console.log(data)
                    if(data[0].id){
                    this.allreports = data;
                    }
                    else{
                    this.$refs.message.error('not_found');
                    }
                }
                catch(error){
                    // this.modal_info = error;
                    // this.modal_status = true
                    this.allreports = [];
                    this.$refs.message.warning('not_found');
                }
            }
            else{
                await this.fetch_drug_list();
            }
            
        },
        async click_drug(data){
            this.show_qty_add_doc = true;
            console.log('data')
            console.log(data)
            this.data_push_Drag = data;
            this.dataAccept.name = data.product_name;
            this.dataAccept.manifacturer_name = data.product.manifacturer_name;
            this.dataAccept.expired_date = data.exp_date_str;
            this.dataAccept.price = data.unit_saled_price;
            this.dataAccept.contains_number_in_pack = data.product.contains_number_in_pack;
            this.dataAccept.measurement = data.product.publisher_name;
            this.dataAccept.info = '№' + data.product.contains_number_in_pack.toString() + ' ' + data.product.product_type_str;

            this.$nextTick(function () {
                this.$refs.upokovka.focus();
            })
            // this.$refs.upokovka.focus();
        },
        deleteItemChoosen(item,i){
            this.summa -= parseInt(item.summa)
            this.choosen_product_List.splice(i,1)
        },
        async submit(){
            if(this.qty == '' || this.qty == null){
                this.qty = 0;
            }
            if(this.sht == '' || this.sht == null){
                this.sht = 0;
            }
            if(this.sht == 0 && this.qty == 0)return;
            let sum = 0
            sum = (parseFloat(this.dataAccept.price) * parseInt(this.qty)) + ((parseFloat(this.dataAccept.price)/parseInt(this.dataAccept.contains_number_in_pack))* parseFloat(this.sht));
            console.log(sum)
            this.summa +=parseInt(sum);
            let import_item = {
                qty: this.qty,
                recipe: this.recipe,
                sht: this.sht,
                product_name: this.dataAccept.name,
                product_id: this.data_push_Drag.product.id,
                item: this.data_push_Drag,
                manufacture: this.dataAccept.manifacturer_name,
                summa: sum,
            }
            this.choosen_product_List.push(import_item);
            console.log(this.choosen_product_List);
            this.show_qty_add_doc = false;

            this.$nextTick(function () {
                this.$refs.searchDrag.focus();
            })
            this.qty = null;
            this.sht = null;
            this.recipe = '';
            this.dataAccept= {};
            this.data_push_Drag = {};
        },
        cancel_item_drug(){
            this.qty = null;
            this.sht = null;
            this.recipe = '';
            this.dataAccept= {};
            this.data_push_Drag = {};
            this.show_qty_add_doc = false;
            this.$nextTick(function () {
                this.$refs.searchDrag.focus();
            })
        },
        async send_pharmasy_drug(){
            console.log(this.patient.patient_id)
            let tempList = [];
            for(let i=0; i<this.choosen_product_List.length; i++){
                let atp ={
                    qty: parseInt(this.choosen_product_List[i].qty),
                    real_qty: parseInt(this.choosen_product_List[i].sht),
                    get_drugs_status: false,
                    authorizationId: parseInt(localStorage.AuthId),
                    posProductid: this.choosen_product_List[i].product_id,
                    note: this.choosen_product_List[i].recipe,
                    id: 0,
                    patientsId: this.patient.patient_id,
                    doctor_name: localStorage.docName,
                    creator_doctor_name: this.patient.name,

                }
                tempList.push(atp);
            }
            console.log(tempList)
            console.log('hiy send pharmacy')

            try{
                const requestOptions = {
                    method: "POST",
                    headers: { "Content-Type" : "application/json" },
                    body: JSON.stringify(tempList)
                };
                console.log(requestOptions)
                this.loading = true;
                const response = await fetch(this.$store.state.hostname + "/HospitalPatientDrugs/addPostListDrugs", requestOptions);
                const data = await response.json();
                this.loading = false;
                console.log('data payments')
                console.log(data)
                if(data[0].id){
                    this.choosen_product_List = [];
                    this.$emit('close')
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
.bg_choose_patient_send_drugs{
    height: 93vh;
    overflow: hidden;
    background: #fff !important;
}
.drug_list{
    height: 93vh;
    background: #fff;
}
.drug_pag_list{
    height: 93vh;
    overflow-y: scroll;
    background: #fff;
}
.bg_success{
    background: rgb(145, 255, 178);
}
.bg_warning{
    background: rgb(239, 255, 159);
}
.bg_danger{
    background: rgb(255, 171, 160);
}
.bg_dark{
    background: rgb(67, 67, 67);
    color:white;
}
.hoverDrug:hover{
    background: #e2e2e2;
    cursor: pointer;
    color:black;
}
.borderSolder {
    border: 0.5px dashed #D0D3D8;
}
.borderSolder span{
    color:#67676C;
    font-size: 22px;
}
.borderSolder p{
    color:#525255;
    font-weight:bold;
    font-size: 24px;
    margin:0;
    padding:0;
}
.addProductQtyAccept{
    position: fixed;
    display: flex;
    align-items: center;
    justify-content: center;
    top:0;
    left:0;
    width: 100%;
    height: 100vh;
    background: rgba(113, 114, 116, 0.6);
    z-index: 1111;
}
.acceptBox{
    width:500px;
    height: 380px;
    background: snow;
    box-shadow: 3px 2px 5px rgb(129, 129, 129);
    border-radius: 5px;
}
.choosen_drag_list{
    height: 82vh;
    overflow-y: scroll;
}

.container_box_send_pharmacy{
    background: white !important;
}
</style>
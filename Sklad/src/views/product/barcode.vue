<template>
  <div class="barcode" style="width: 200px; margin-top:40px; margin-left: -30px; margin-bottom:0; transform: rotate(90deg)" >
    <div class="d-flex justify-content-between">
      <input type="text" ref="saled" v-on:keyup.13 = "acceptBarcode" v-model="saled_price" @keyup="funcCurrency($event.target.value)" style="text-align:right;  font-size: 26px; width:140px; outline:none; border:none;" >
      <input type="text"  v-model="sum"  style="text-align:left; font-size: 17.5px; width:60px;  outline:none; border:none;" >
    </div>
    <div class="d-flex justify-content-center">
      <barcode height="50" :value="product_id" >
    </barcode>
    </div>
    <Toast ref="message"></Toast>
  </div>
</template>

<script>
import VueBarcode from 'vue-barcode';
export default {
components: {
  'barcode': VueBarcode
},
data() {
  return {
    product_id: 1112455,
    id: this.$route.params.id,
    saled_price: '',
    sum: 'Сўм',


    name: '',
    fullname: '',
    dozirovka: '',
    Fvip: '',
    numberDrag: '1',
    taxNumber: '',
    manufacture: '',
    internationalName: '',
    category_id: null,
    measurment_id: '',
    minqty: '',
    price: '',
    buyed_price: '',
    note: '',
    barcode: '',
    percentage: 0,

    datasource :
      {
        product_info :[
          {
            name: '',
            price: null,
            code: '',
            buyed_price: null,
            percentage: null,
            barcodeList : [{barcode :null, id: 0, product_id: 0}],
            id: 0,
            main_product_id: 0,
          }
        ]
      }
  }
},
  async created(){  
    if(this.id > 0)
    {
      const res = await fetch(this.$store.state.hostname + '/PosProducts/' + this.id);
      const data = await res.json();
      console.log(data);
      this.product_id +=data.id;
      this.saled_price = data.buyed_price.toString().replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, " ");


      this.fullname = data.name;
      this.name = data.sale_name;
      this.dozirovka = data.dozirofka;
      this.Fvip = data.product_type_str;
      this.numberDrag = data.contains_number_in_pack;
      this.taxNumber = data.tax_number;
      this.manufacture = data.manifacturer_name;
      this.internationalName = data.global_name;
      this.measurment_id = data.measurment.id;
      this.category_id = data.category.id;
      this.barcode = this.product_id;
      this.note = data.note; 
      this.buyed_price = data.buyed_price;
      this.price = data.price
      this.percentage = data.percentage;
      this.datasource.product_info[0].barcodeList[0].barcode = this.barcode;
      this.datasource.product_info[0].barcodeList[0].product_id = data.id;
      // this.datasource.product_info[0].name =  data.name;
      this.datasource.product_info[0].price =  data.price;
      this.datasource.product_info[0].buyed_price =  data.buyed_price;
      this.datasource.product_info[0].percentage =  data.percentage;
      this.datasource.product_info[0].main_product_id =  data.id;

    } 
  },
  mounted(){
    this.$refs.saled.focus();
  },
  methods:{
    anything(){
      this.$refs.saled.focus();
    },
    funcCurrency(n){
      console.log(n)
       this.saled_price = n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, " ")
       var temp = ''
       for(let i=0; i<this.saled_price.length; i++){
        if(this.saled_price[i] != ' '){
          temp += this.saled_price[i];
        }
       }
        this.buyed_price = parseInt(temp);
        this.datasource.product_info[0].buyed_price =  parseInt(temp);
        console.log(this.buyed_price)
    },
    async acceptBarcode(){
      const requestOptions = {
            method : "POST",
            headers: { "Content-Type" : "application/json" },
            body: JSON.stringify({
              "id" : this.id,
              "real_company_id" : localStorage.CompId,
              "name": this.fullname,
              "bot_name":  this.fullname,
              "print_name":  this.fullname,
              "sale_name":  this.name,
              "contains_number_in_pack": this.numberDrag,
              "dozirofka": this.dozirovka,
              "product_type_str": this.Fvip,
              "global_name": this.internationalName,
              "manifacturer_name": this.manufacture,
              "tax_number": this.taxNumber,
              "price": this.price,
              "buyed_price": this.buyed_price,
              "category_id": this.category_id,
              "unitmeasurment_id" : this.measurment_id,
              "barcode": this.barcode,
              "info": this.note,
              "note": this.note,
              'percentage': this.percentage,
              "subProductList" : this.datasource.product_info,
            })
          };
            try{
              this.loading = true;
              const response = await fetch(this.$store.state.hostname + "/PosProducts", requestOptions);
              const data = await response.json();
              console.log(data)
              this.loading = false;
              if(response.status == 201 || response.status == 200)
              {
                window.print()
                this.$router.push('/product')
                return true;
              }
              else{
                this.$refs.message.danger('network_ne_connect')
                return false;
              }
            }
            catch{
              this.$refs.message.danger('network_ne_connect')
            }
    }
  }
}
</script>

<style>

</style>
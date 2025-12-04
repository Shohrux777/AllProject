<template>
  <div class="give-product-page">
    <!-- Header -->
    

    <div class="container-fluid pt-3">
      <div class="row">
        <!-- Chap qism: Form -->
        <div class="col-6 pr-3">
          <div class="give-product-card">
            <div class="card-header-custom">
              <h6 class="m-0 my-2">Ma'lumotlar</h6>
            </div>
            <div class="card-body-custom">
              <!-- Name input -->
              <div class="form-group-floating">
                <input 
                  type="text" 
                  v-model="name"
                  class="form-control-floating" 
                  placeholder=" "
                  id="nameInput">
                <label for="nameInput">{{ $t('name') }}</label>
              </div>
              
              <!-- Note textarea -->
              <div class="form-group-floating mt-3">
                <textarea 
                  v-model="note" 
                  class="form-control-floating" 
                  rows="3"
                  placeholder=" "
                  id="noteInput"></textarea>
                <label for="noteInput">{{ $t('note') }}</label>
              </div>

              <!-- Photo section -->
              <div class="photo-section mt-3">
                <div v-if="photo_url" class="photo-preview">
                  <img :src="hostname + photo_url" class="photo-img" alt="Photo">
                  <mdb-btn 
                    @click="photo_url = ''" 
                    color="danger" 
                    size="sm" 
                    class="photo-remove-btn"
                    style="position: absolute; top: 5px; right: 5px; padding: 2px 6px; font-size: 10px;">
                    <mdb-icon icon="times"></mdb-icon>
                  </mdb-btn>
                </div>
                <div v-else class="photo-placeholder">
                  <mdb-icon icon="camera" class="camera-icon"></mdb-icon>
                  <!-- <small class="text-muted">Rasm qo'shilmagan</small> -->
                </div>
              </div>

              <!-- Buttons -->
              <div class="action-buttons mt-4">
                <mdb-btn 
                  @click="photoRasxod()" 
                  color="info" 
                  class="action-btn">
                  <mdb-icon icon="camera" class="mr-1"></mdb-icon> 
                  {{ $t('photo') }}
                </mdb-btn>
                <mdb-btn 
                  v-if="photo_url" 
                  @click="submitSavdoKassa" 
                  color="success" 
                  class="action-btn">
                  <mdb-icon icon="check" class="mr-1"></mdb-icon> 
                  {{ $t('apply') }}
                </mdb-btn>
                <mdb-btn 
                  @click="close" 
                  color="danger" 
                  class="action-btn">
                  <mdb-icon icon="times" class="mr-1"></mdb-icon> 
                  {{ $t('cancel') }}
                </mdb-btn>
              </div>
            </div>
          </div>
        </div>

        <!-- O'ng qism: Bugungi kun uchun berilgan mahsulotlar ro'yxati -->
        <div class="col-6 pl-3">
          <div class="give-product-card">
            <div class="card-header-custom d-flex justify-content-between align-items-center">
              <h6 class="m-0">
                <mdb-icon icon="list" class="mr-2"></mdb-icon>
                Bugungi berilgan mahsulotlar
              </h6>
              <mdb-btn 
                @click="loadTodayGivenProducts" 
                color="info" 
                size="sm" 
                class="refresh-btn">
                <mdb-icon icon="sync" class="mr-1"></mdb-icon> 
                Yangilash
              </mdb-btn>
            </div>
            <div class="card-body-custom">
              <div class="products-list">
                <div v-if="loadingGiven" class="text-center py-5">
                  <mdb-icon icon="spinner" spin class="text-info" style="font-size: 24px;"></mdb-icon>
                  <p class="text-muted mt-2 mb-0">Yuklanmoqda...</p>
                </div>
                <div v-else-if="todayGivenProducts.length === 0" class="text-center py-5">
                  <mdb-icon icon="inbox" class="text-muted" style="font-size: 48px;"></mdb-icon>
                  <p class="text-muted mt-2 mb-0">Bugun hech qanday mahsulot berilmagan</p>
                </div>
                <div v-else>
                  <div 
                    v-for="(item, index) in todayGivenProducts" 
                    :key="index" 
                    class="product-item-card" style="cursor: pointer;">
                    <div class="product-item-header">
                      <div class="product-item-info">
                        <strong class="product-item-name">{{ item.name || "Noma'lum" }}</strong>
                        <small v-if="item.note" class="product-item-note">{{ item.note }}</small>
                        <small v-if="item.user_name" class="product-item-user">
                          <mdb-icon icon="user" class="mr-1"></mdb-icon>
                          {{ item.user_name }}
                        </small>
                      </div>
                      <div class="product-item-time">
                        <mdb-icon icon="clock" class="mr-1"></mdb-icon>
                        {{ formatTime(item.create_date) }}
                      </div>
                    </div>
                    <div class="product-item-products">
                      <div 
                        v-for="(product, pIndex) in item.products" 
                        :key="pIndex"
                        class="product-row">
                        <span class="product-name">{{ product.productName }}</span>
                        <span class="product-qty">
                          <strong>{{ product.totalQty }}</strong>
                          <span v-if="product.unitM && product.unitM.name" class="unit-name">
                            {{ product.unitM.name }}
                          </span>
                        </span>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <webcam v-if="showPhoto" @getPhotosub="takePhoto"/>  
    <Alert ref="alert"></Alert> 
  </div>
</template>

<script>
import webcam from '../webcam/webcam_Add.vue'
import {mdbInput, mdbBtn, mdbIcon } from 'mdbvue'
import {mapActions,mapGetters} from 'vuex'
export default {
  components: {
    mdbInput, mdbBtn,mdbIcon,
    webcam
  }, 
data(){
    return{
      showPhoto: false,
      photo_url: '',
      hostname: this.$store.state.server_ip,

      name: '',
      note: '',
      kassa_name: '',
      kassa_id: null,
      
      todayGivenProducts: [],
      loadingGiven: false,
    }
},
props: {
  item_list: {
    type: Array,
    default() {
      return [];
    }
  }
},
computed: mapGetters(['allKassa','user_kassa_list', 'user_kassa_info']),
mounted(){
  this.loadTodayGivenProducts();
},
methods:{
  ...mapActions(['fetchKassa', 'fetchKassa_userId', 'fetchKassa_info']),
    async refresh(kassa_data){
      console.log(kassa_data)
      this.clw_cl();
    },
    takePhoto(img){
      this.photo_url = img;
      this.showPhoto = false;
    },
    photoRasxod(){
      this.showPhoto = true;
    },
    
    close(){
        this.$emit('close')
    },

    async clw_cl(){
      this.name = '';
      this.note = '';
      this.photo_url = '';
    },
    async loadTodayGivenProducts(){
      try{
        this.loadingGiven = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonSklad/getTodayGivenProducts");
        const data = await response.json();
        this.loadingGiven = false;
        if(response.status == 201 || response.status == 200){
          this.todayGivenProducts = data;
        }
      }
      catch(error){
        this.loadingGiven = false;
        console.error('Error loading today given products:', error);
      }
    },
    formatTime(dateString){
      if(!dateString) return '';
      const date = new Date(dateString);
      const hours = String(date.getHours()).padStart(2, '0');
      const minutes = String(date.getMinutes()).padStart(2, '0');
      return `${hours}:${minutes}`;
    },
    async submitSavdoKassa(){
        if(this.item_list.length == 0){
          this.$refs.alert.warning('Mahsulotlar tanlanmagan !');
          return;
        }
      let list = [];
      for(let i=0; i<this.item_list.length; i++){
        let temp = {
            Product_Name: this.item_list[i].name,
            Qty: this.item_list[i].qty,
            Product_Id: this.item_list[i].product_id,
        }
        list.push(temp)
      }
      console.log(list);

      await this.fetchKassa_userId(localStorage.user_id);
        if(this.user_kassa_list.length){
          localStorage.kassa_id = this.user_kassa_list[0].id;
          localStorage.kassa_num = this.user_kassa_list[0].num_1;
        }
        else{
          this.$refs.alert.error('Bu foydalanuvchi kassaga biriktirilmagan, unda savdo qilish huquqi yuq !');
          localStorage.kassa_id = 0;
          localStorage.kassa_num = 0;
          return;
        }

      const requestOptions = {
        method : "POST",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
          "Name": this.name,
          "User_Name": localStorage.user_name,
          "Note": this.note,
          "Photo": this.photo_url,
          "Auth_Id": localStorage.AuthId,
          "Sklad_Id": 1,
          "Item_List": list
          // "uz_card": 0,     for skidka uchun ishlataman
        })
      };
      console.log('requestOptions')

      console.log(requestOptions.body)
    //   return;
      try{
        this.loading = true;
        const response = await fetch(this.$store.state.hostname + "/TegirmonSklad/transfer_oshxona", requestOptions);
        // const data = await response.json();
        console.log(response)
        if(response.status == 201 || response.status == 200)
        {
          this.clw_cl();
          await this.loadTodayGivenProducts(); // Ro'yxatni yangilash
          this.$emit('close_pay');
          this.loading = false;
          return true;
        }
        else{
          this.modal_info = this.$i18n.t('network_ne_connect');
          this.modal_status = true;
          this.loading = false;
          return false;
        }
      }
      catch{
        this.loading = false;
        this.modal_info = this.$i18n.t('network_ne_connect');
        this.modal_status = true;
        return false;
      }
      
    },
    
}
}
</script>

<style scoped>
.give-product-page {
  background: #f7f8fc;
  min-height: 100%;
  padding-bottom: 20px;
}

/* Header */
.give-product-header {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  padding: 15px 20px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  margin-bottom: 20px;
}

.give-product-header h5 {
  font-weight: 600;
  font-size: 16px;
}

/* Card */
.give-product-card {
  background: white;
  border-radius: 12px;
  box-shadow: 0 2px 12px rgba(0, 0, 0, 0.08);
  overflow: hidden;
  height: 100%;
  display: flex;
  flex-direction: column;
}

.card-header-custom {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  padding: 5px 20px;
  border-bottom: none;
}

.card-header-custom h6 {
  font-weight: 600;
  font-size: 14px;
  margin: 0;
}

.card-body-custom {
  padding: 20px;
  flex: 1;
  overflow-y: auto;
}

/* Form inputs with floating labels */
.form-group-floating {
  position: relative;
}

.form-control-floating {
  width: 100%;
  padding: 8px 15px;
  border: 2px solid #e0e0e0;
  border-radius: 8px;
  font-size: 14px;
  transition: all 0.3s ease;
  background: #fdfcfc;
}

.form-control-floating:focus {
  outline: none;
  border-color: #667eea;
  background: white;
  box-shadow: 0 0 0 3px rgba(102, 126, 234, 0.1);
}

.form-group-floating label {
  position: absolute;
  left: 15px;
  top: 10px;
  font-size: 14px;
  color: #999;
  pointer-events: none;
  transition: all 0.3s ease;
  background: white;
  padding: 0 5px;
}

.form-control-floating:focus + label,
.form-control-floating:not(:placeholder-shown) + label {
  top: -10px;
  left: 10px;
  font-size: 12px;
  color: #667eea;
  font-weight: 600;
}

.form-control-floating::placeholder {
  color: transparent;
}

/* Photo section */
.photo-section {
  border: 2px dashed #e0e0e0;
  border-radius: 8px;
  padding: 15px;
  text-align: center;
  background: #fafafa;
  transition: all 0.3s ease;
}

.photo-section:hover {
  border-color: #667eea;
  background: #f5f7ff;
}

.photo-preview {
  position: relative;
  display: inline-block;
}

.photo-img {
  max-width: 100%;
  max-height: 200px;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.photo-placeholder {
  padding: 30px;
}

.camera-icon {
  font-size: 48px;
  color: #ccc;
  margin-bottom: 10px;
}

/* Action buttons */
.action-buttons {
  display: flex;
  gap: 10px;
  justify-content: flex-end;
}

.action-btn {
  border-radius: 8px;
  padding: 8px 20px;
  font-size: 12px;
  font-weight: 500;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
  transition: all 0.3s ease;
}

.action-btn:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
}

.refresh-btn {
  border-radius: 6px;
  padding: 6px 12px;
  font-size: 10px;
}

/* Products list */
.products-list {
  max-height: 500px;
  overflow-y: auto;
  padding-right: 5px;
}

.products-list::-webkit-scrollbar {
  width: 6px;
}

.products-list::-webkit-scrollbar-track {
  background: #f1f1f1;
  border-radius: 10px;
}

.products-list::-webkit-scrollbar-thumb {
  background: #667eea;
  border-radius: 10px;
}

.products-list::-webkit-scrollbar-thumb:hover {
  background: #764ba2;
}

/* Product item card */
.product-item-card {
  background: linear-gradient(135deg, #f5f7fa 0%, #ffffff 100%);
  border: 1px solid #e8eaf6;
  border-radius: 10px;
  padding: 15px;
  margin-bottom: 12px;
  transition: all 0.3s ease;
  box-shadow: 0 1px 4px rgba(0, 0, 0, 0.05);
}

.product-item-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  border-color: #667eea;
}

.product-item-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 12px;
  padding-bottom: 10px;
  border-bottom: 2px solid #e8eaf6;
}

.product-item-info {
  flex: 1;
}

.product-item-name {
  display: block;
  font-size: 14px;
  color: #333;
  margin-bottom: 4px;
}

.product-item-note {
  display: block;
  font-size: 11px;
  color: #666;
  margin-top: 4px;
}

.product-item-user {
  display: block;
  font-size: 11px;
  color: #667eea;
  margin-top: 6px;
  font-weight: 500;
}

.product-item-time {
  font-size: 11px;
  color: #999;
  display: flex;
  align-items: center;
  white-space: nowrap;
}

.product-item-products {
  margin-top: 8px;
}

.product-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 8px 0;
  border-bottom: 1px solid #f0f0f0;
}

.product-row:last-child {
  border-bottom: none;
}

.product-name {
  font-size: 12px;
  color: #555;
  flex: 1;
}

.product-qty {
  font-size: 12px;
  color: #333;
  display: flex;
  align-items: center;
  gap: 4px;
}

.product-qty strong {
  color: #667eea;
  font-size: 13px;
}

.unit-name {
  color: #999;
  font-size: 11px;
}

/* Responsive */
@media (max-width: 768px) {
  .col-6 {
    margin-bottom: 20px;
  }
  
  .action-buttons {
    flex-direction: column;
  }
  
  .action-btn {
    width: 100%;
  }
}
</style>
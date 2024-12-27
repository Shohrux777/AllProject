<template>
<div>
  <check v-if="check_show" @close="closedChek"  />
  <div v-else class="aptekaSale" >
    <div class="w-100 container-fluid border-bottom d-flex justify-content-between align-items-center"  v-on:click.capture="focusBarcode">
      <div>
        <router-link to="/zaxiraniAlmashtirish">
          <h5 class="m-0 ml-0 d-flex align-items-center" style="padding: 7px 0px; font-size: 15.5px;">
              <mdb-icon icon="shopping-basket" style="font-size: 15px;"  class="mr-2 "/>
              {{$t('sale')}}</h5>
          </router-link>
      </div>
      
      <div class="d-flex">
        <mdb-btn @click="$router.push('/savdo_all_kassa_info')" v-if="kassa_setting_show" color="indigo py-1 px-3" style="font-size:9px;">
          <mdb-icon style="font-size:9px;" icon="calendar" class="m-0 p-0 mr-1" />
          Sotuv kassa
        </mdb-btn>
        <mdb-btn @click="show_kassa_setting()" v-if="kassa_setting_show" color="secondary py-1 px-3" style="font-size:9px;">
          <mdb-icon style="font-size:9px;" icon="wrench" class="m-0 p-0 mr-1" />
          Kassaga xodimlarni ulash
        </mdb-btn>
        <mdb-btn @click="show_send_savdo_kassa()"  color="info py-1 px-3" style="font-size:9px;">
          <mdb-icon style="font-size:9px;" icon="share-square" class="m-0 p-0 mr-1" />
          Asosiy kassaga pul utkazish
        </mdb-btn>
        <mdb-btn @click="show_from_kassa_to_kassa()"  class="bg_kassa_to_kassa py-1 px-3" style="font-size:9px;">
          <mdb-icon style="font-size:9px;" icon="share-square" class="m-0 p-0 mr-1" />
          Boshqa kassaga pul utkazish
        </mdb-btn>
        <div class="" v-if="kassa_info_show">
          <mdb-btn @click="funcKassaInfo"  color="danger py-1 px-3 mr-2" style="font-size:9px;">
            <mdb-icon style="font-size:9px;" icon="calendar" class="m-0 p-0 mr-1" />
            {{$t('closeCash')}}
          </mdb-btn>
        </div>
        
        <div style="position:relative;" class="userLog">
          <h5 class="m-0 px-2 d-flex align-items-center text-primary" 
            style="padding: 10px 0px 0 0; font-size: 11px; font-weight: bold; cursor:pointer;">
            {{kassir}}
          </h5> 
          <div style="position:absolute; top:28px; left:15px; cursor:pointer; font-size: 12px;" class="border w-100 logout "  >
            <p @click="logout" class="p-0 m-0 py-1 ml-2">{{$t('logout')}}</p>
          </div>
        </div>
        <div class="kassa_num_user" :style="{background: colors[kassa_num] || '#ff5733'}">
          <small v-if="kassa_num == 0"> {{kassir[0]}}</small>
          <small v-else> {{kassa_num}}</small>
        </div>
      </div>
    </div>
    <div class="allPage w-100" >
      <div class="tablePageLeft">
        <div class="tableAndBtn" style="position: relative;">
          <div class="myTablesforSaleNew border py-0 px-0" v-on:click.capture="focusBarcode">
            <div class="tableProduct ">
              <table  class="myTablesforSaleNew" >
                <thead>
                  <tr  class="header">
                  <th style="width: 40px;">№</th>
                  <th style="width: 300px;">{{$t('product')}}</th>
                  <th>{{$t('soni')}}</th>
                  <th>{{$t('unit_qty')}}</th>
                  <th>{{$t('price')}}</th>
                  <th>{{$t('expired_date')}}</th>
                  <th class=" pl-3">{{$t('total')}}</th>
                  <th style="width: 40px;">{{$t('')}}</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(item,i) in get_zakaz_product_all_list[get_page_savat]" :key="i" class="backlight searchBarcodeHover">
                    <td>{{i+1}}</td>
                    <td>{{item.name}}</td>
                    <td><input type="number" disabled v-model="item.qty" @input="updateQty(i)" class="form-control form-control-sm" style="border:none; outline:none;font-weight:bold;" ></td>
                    <td><input type="number" disabled v-model="item.qty_in_pack" @input="updateQty(i)"  class="form-control form-control-sm" style="border:none; outline:none; font-weight:bold;" > </td>
                    <td>{{item.real_sumString}}</td>
                    <td>{{item.expired_date}}</td>
                    <td class=" pl-3">{{item.summString}}</td>
                    <td><div @click="deleteProduct(item, i)">
                      <mdb-icon icon="trash" class="text-danger"  />
                    </div>
                      </td>
                  </tr>
                </tbody>
              </table>
            </div>
            <div style="position: absolute; bottom:0px; left:0" class="w-100  bg-white border-bottom">
              <div  class="d-flex flex-wrap align-items-center border-top py-1 " >
                <div v-for="(page,i) in get_zakaz_product_all_list" :key="i" :class="{'bg-warning': savatActive == i,'text-white': savatActive == i, 'bg-white': savatActive != i}"
                    class=" m-0 p-1 border" @click="select_savat(i)" style="width:100px; position:relative; cursor:pointer">
                  <span style="font-size: 13px; padding-left:3px;">{{$t('savat')}} {{i+1}}</span>
                  <div style="position: absolute; top:-6px; right:0; cursor:pointer;" @click="del_savat(i)" class="">
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-x" width="12" height="12" viewBox="0 0 24 24" stroke-width="3.0" stroke="#ff2825" fill="none" stroke-linecap="round" stroke-linejoin="round">
                      <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                      <line x1="18" y1="6" x2="6" y2="18" />
                      <line x1="6" y1="6" x2="18" y2="18" />
                    </svg>
                  </div>
                </div>
                <div @click="add_savat" style="cursor:pointer;" class="ml-1">
                  <mdb-icon icon="plus-circle" size="lg" class="text-success"  />
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="SearchNameBarcode mt-0">
          <div class="row mb-1" v-on:click.self="focusBarcode">
            <div class="col-5 m-0 mx-2">
              <input v-model="searchDrag" @keyup="searchFunc"  size="sm" ref="searchDrag"
              type="text"  id="inputEmail"
              v-on:keyup.38 = "ArrowUp1"
              v-on:keyup.40 = "ArrowDown1"
              v-on:keyup.37 = "ArrowRight1"
              v-on:keyup.39 = "ArrowRight1"
              style="margin-top: 10px;  margin-bottom: 0px; height: 32px; outline:none; font-size:14px;"
              class="w-100  px-2 inputFocusSearch" />
              <label style="position:absolute; top:0px; left:20px; font-size:12px;" class="control-label" for="inputEmail">{{$t('search_here')}}</label>

              <input type="text" 
                style="position:absolute; z-index: -55; outline: none; left: -1400px;"
                ref="hiddenArrow1"
                v-model="hiddenArrow1"
                v-on:keyup.13 = "EnterArrowItem"
                v-on:keyup.38 = "ArrowUp1"
                v-on:keyup.40 = "ArrowDown1"
              >
            </div>

            <div class="col-3 mx-2">
              <input v-model="barcode"
                id="inputEmail1" 
                @keyup.enter="getbarcode()"
                @keyup="getbarcode()"
                @paste="onPaste" 
                @input="hanlePro($event)"
                v-on:keyup.38 = "ArrowUp"
                v-on:keyup.40 = "ArrowDown"
                v-on:keyup.37 = "ArrowRight"
                v-on:keyup.39 = "ArrowRight"
                ref="barcode" size="sm" type="text"  
                style="margin-top: 10px;  margin-bottom: 0px; height: 32px; outline:none; font-size:14px;"
                class="w-100  px-2 inputFocusSearch"/>
              <label style="position:absolute; top: 0px; left:20px; font-size:12px;" class="control-label" for="inputEmail1">Продукт код</label>

            </div>
          </div>
                
          <div class="FoundProductTable border" v-on:click.capture="focusBarcode">
            <input type="text" 
              style="position:absolute; z-index: -55; outline: none; left: -500px;"
              ref="hiddenArrow"
              v-model="hiddenArrow"
              @paste="onPaste" 
              v-on:keyup.13 = "EnterArrowItem"
              v-on:keyup.38 = "ArrowUp"
              v-on:keyup.40 = "ArrowDown"
            >
            <div class=" px-3 py-2">
              <div class="row">
                <div class="col-3 px-2" v-for="(item,i) in productList" :key="i" @click="getProductOstatka(item)" v-show="item.real_qty>0">
                  <div class="border rounded p-2 mt-2" 
                    style="cursor:pointer; box-shadow: rgba(0, 0, 0, 0.15) 1.95px 1.95px 2.6px;">
                    <div style="height:20px; overflow:hidden;">
                      <p class="m-0 mb-1" style="color:#707880; font-size: 14.5px;"> {{item.product.name}}</p>
                    </div>
                    <p class="m-0 mb-0 text-right" style="font-size: 14px;"> 
                      {{item.product.price.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ')}}
                      <small class="ml-0">сўм</small>
                      <!-- <small class="ml-1">{{item.real_qty}}</small> -->
                    </p>
                    <!-- <span>{{item.qty}}</span> -->
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="SummaBtnRight border"  v-on:click.capture="focusBarcode">
        <div class="d-flex">
          <div class="w-50">
            <div class="qty borderSolder py-2">
              <span class="ml-3">{{$t('summ')}}</span>
              <div class="text-right px-3 mt-1">
                <p>{{AllSummString[get_page_savat]}}</p>
              </div>
            </div>
          </div>
          <div class="w-50">
            <div class="qty borderSolder py-2">
              <span class="ml-3">{{$t('discount')}}</span>
              <div class="text-right px-3 mt-1">
                <p>{{get_skidka_summ[get_page_savat]}}</p>
              </div>
            </div>
          </div>
        </div>
        <div class="w-100">
          <div class="qty borderSolderOplate py-2">
            <span class="ml-3">{{$t('Total')}}</span>
            <div class="text-right px-3 mt-1">
              <p>{{AllSummString[get_page_savat]}}</p>
            </div>
          </div>
        </div>
        <div class="d-flex" v-on:click.capture="focusBarcode">
          <div class="w-50">
            <!-- <div class="text-center mt-1 linkMain" style="padding: 9.2px 0;" @click="checkShow">
              <span class="m-0 p-0 " >{{$t('check')}} </span>
            </div> -->
            <div class="text-center mt-1  linkMain" style="padding: 9.2px 0;" @click="paymePay">
              <div>
                <img src="../../assets/payme.svg" style="width:40%;" alt="" >
              </div>
            </div>
            <div class="text-center mt-1  linkMain" style="padding: 9.2px 0;" @click="clickedPay">
              <div>
                <img src="../../assets/click.png" style="width:35%;" alt="">
              </div>
            </div>
            <div class="text-center mt-1  linkMain" style="padding: 9.2px 0;" @click="paynetPay">
              <div>
                <img src="../../assets/paynet.png" style="width:46%;" alt="">
              </div>
            </div>
            <div class="text-center mt-1  linkMain" style="padding: 9.2px 0;" @click="uzumPay">
              <div>
                <img src="../../assets/uzum.png" style="width:38%;" alt="">
              </div>
            </div>
            <div class="text-center linkMain" style="padding: 9.2px 0; margin-top: 2px;">
              <span class="m-0 p-0 " >...</span>
            </div>
            <div class="text-center linkMain" style="padding: 9.2px 0; margin-top: 2px;">
              <span class="m-0 p-0">...</span>
            </div>
            <!-- <div class="text-center mt-1  linkMainDang" style="padding: 9.2px 0;" @click="clearSavat">
              <span class="m-0 p-0 " >{{$t('clear_cash')}}</span>
            </div> -->
            
          </div>
          <div class="w-50">
            <mdb-btn color="success" style="font-size:11px; padding: 12px 0;" @click="cashPay"  class="m-0  mt-1 w-100">{{$t('cash')}}</mdb-btn>
            <!-- <mdb-icon icon="money-bill-alt" class="mr-2 "/> -->
            <mdb-btn color="info" style="font-size:11px; padding: 12px 0;" @click="uzcardPay" class="m-0  mt-1 w-100" > {{'uzcard'}}</mdb-btn>
            <mdb-btn color="mdb-color" style="font-size:11px; padding: 12px 0;" @click="humoPay" class="m-0  mt-1 w-100" > {{'humo'}}</mdb-btn>
            <mdb-btn color="indigo" style="font-size:11px; padding: 12px 0;" @click="onlinePay" class="m-0  mt-1 w-100" > {{'Перечисления'}}</mdb-btn>
            <mdb-btn color="warning" style="font-size:11px; padding: 12px 0;" @click="clickPay" class="m-0  mt-1 w-100"> {{$t('dollor')}}</mdb-btn>
            <mdb-btn style="font-size:11px; padding: 12px 0;" color="deep-orange" @click="plastikPay"  class="m-0  mt-1 w-100"> Пластик</mdb-btn>
          </div>
        </div>
        <div class="d-flex" v-on:click.capture="focusBarcode">
          <div class="w-100">
            <div class="text-center linkMainPrixod" style="padding: 9.2px 0; margin-top: 2px;" @click="pulOlibQolishChiqarNarsa">
              <span class="m-0 p-0 " >{{$t('pul_olish')}}</span>
            </div>
            <div class="text-center linkMainRasxod" style="padding: 9.2px 0; margin-top: 2px;" @click="rasxodClick">
              <span class="m-0 p-0">{{$t('rasxod')}}</span>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="addProductQtyAccept" v-show="showAccept" v-on:click.self="closeChoosenProduct">
      <div class="acceptBox px-3 py-2">
        <input type="text" v-model="dataAccept.name" disabled class="form-control form-control-sm border mt-2" style="border:none; outline:none;font-weight:bold;" >
        <input type="text" v-model="dataAccept.manifacturer_name" disabled class="form-control form-control-sm border mt-2" style="border:none; outline:none;font-weight:bold;" >
        <div class="d-flex mt-3">
          <div class="skidka " style="width:35%">
            <div style="position:relative;">
              <input type="text" v-model="persantage_discount" @input="summaDrag" class="form-control form-control-sm border mt-2 text-right rounded" style="border:none; outline:none;font-weight:bold;" >
              <small style="position:absolute; top:-16px; left:3px; font-size: 12px;">
                {{$t('skidka')}}
              </small>
            </div>
            <div style="position:relative;">
              <input type="text" v-model="dataAccept.expired_date" disabled class="form-control form-control-sm border mt-3 text-right" style="border:none; outline:none;font-weight:bold;" >
              <small style="position:absolute; top:-16px; left:3px; font-size: 12px;">
                Приход дата
              </small> 
            </div>
          </div>
          <div class="price ml-4 mt-2 border rounded d-flex justify-content-end align-items-end"  style="width:65%; position:relative;">
            <h2 class="font-weight-bold">{{unitPrice}}</h2>
            <small style="position:absolute; top:-17px; left:3px; font-size: 13px;">
                {{$t('price')}}
              </small> 
          </div>
        </div>
        <div class="reportproduct  mt-3 border d-flex pl-3"  style="position:relative;">
          <h4 class="font-weight-bold p-0 m-0 my-1">
            <span v-if="adminStatus == 0">{{dataAccept.ostatka_qty}}</span>
            <span v-else><span v-show="dataAccept.inv == false">{{dataAccept.ostatka_qty}}</span></span>
          </h4>
            <small style="position:absolute; top:-16px; left:3px; font-size: 12px;">
              {{$t('reportproduct')}}
            </small> 
        </div>
        <div class="d-flex align-items-center mt-3" style="position:relative;">
          <div class="skidka " style="width:40%">
            <div style="position:relative;">
              <input type="text" v-model="acceptQty" v-on:keyup.39 = "ArrowLeftS"  v-on:keyup.13 = "AddAcceptPage" ref="upokovka" @input="summaDrag" @blur="blurQty" v-on:click.capture="writeNol" class="form-control form-control-md border mt-2 " style="border:none; outline:none;font-weight:bold;" >
              <small style="position:absolute; top:-16px; left:3px; font-size: 12px;">
                {{$t('kg_ves')}}
              </small>
              
            </div>
          </div>
          
          
          <div class="price ml-4 mt-2 border rounded d-flex justify-content-end align-items-end"   style="width:58%; position:relative;">
            <!-- <h2 class="font-weight-bold m-0 my-2">{{UnitSumString}}</h2> -->
            <input type="text" style="height:37px; width:100%; font-size:25px; border:none;" 
              v-model="UnitSumString" class="px-2  form-control" @keyup="funcCurrency($event.target.value)">
            <small style="position:absolute; top:-16px; left:3px; font-size: 12px;">
                Общие сумма
              </small> 
          </div>
          <small class="text-danger" v-if="enoughtDrag" style="position:absolute; bottom:-25px; left:3px;">
            {{$t('enoughtDrag')}}
          </small>
        </div>
        <div class="btnClick text-right mt-4">
          <mdb-btn color="success" @click="AddAcceptPage" :disabled="enoughtDrag"
            style="font-size: 10px;"
            class="m-0 py-2 mt-1 "> {{$t('ok')}}</mdb-btn>
          <mdb-btn color="danger" @click="closeAcceptPage" 
            style="font-size: 10px;"
            class="m-0 py-2 mt-1 ml-2"> {{$t('cancel')}}</mdb-btn>
        </div>
      </div>
    </div>


    
  </div>
  <modal-train  :show="rasxod_show" headerbackColor="#fc4640"  titlecolor="black" :title="$t('rasxod')" 
    @close="rasxod_show = false" width="60%">
      <template v-slot:body>
        <rasxod @close="rasxod_show = false" ref="rasxodWorkerSum">
        </rasxod>
      </template>
  </modal-train>
  <modal-train  :show="pul_olib_qolish" headerbackColor="#33d95f"  titlecolor="black" :title="$t('pul_olish')" 
    @close="pul_olib_qolish = false" width="60%">
      <template v-slot:body>
        <chiqarPulOlish @close="pul_olib_qolish = false" ref="prixodWorkerSum">
        </chiqarPulOlish>
      </template>
  </modal-train>
  <pay v-show="payshow"  @close="closePay" 
    :summaString="AllSummString[get_page_savat]" 
    :summa_default="get_all_summa[get_page_savat]" @print="printChek()"/>

    <kassa_info v-show="closeCash" ref="kassa_info_ref"  @close="closeKassa" />

    <checkshow v-if="checkshow" @close="checkShow"/>

    <modal-train  :show="send_kassa_status" headerbackColor="info"  titlecolor="black" title="Savdo kassaga pul o'tkazish" 
      @close="send_kassa_status = false" width="30%">
        <template v-slot:body>
          <send-main-kassa @close="closeSendKassa" ref="savdo_kassa_send">
          </send-main-kassa>
        </template>
    </modal-train>
    <modal-train  :show="kassa_setting_status" headerbackColor="info"  titlecolor="black" title="Kassaga xodimlarni ulash" 
      @close="kassa_setting_status = false" width="70%">
        <template v-slot:body>
          <kassa_add_user_setting @close="kassa_setting_status = false" ref="kassa_add_user_setting" />
        </template>
    </modal-train>
    <modal-train  :show="from_kassa_to_kassa_sts" headerbackColor="#3b678b"  titlecolor="white" title="Boshqa kassaga pul utkazish" 
      @close="from_kassa_to_kassa_sts = false" width="30%">
        <template v-slot:body>
          <fromKassaToKassa @close="from_kassa_to_kassa_sts = false" ref="fromKassaToKassa_ref" />
        </template>
    </modal-train>
   <massage_box :hide="modal_status" :detail_info="modal_info"
      :m_text="$t('Failed_to_delete')" @to_hide_modal = "modal_status= false"/>

    <Toast ref="message"></Toast>
    <Alert ref="alert"></Alert> 

</div>
    
  
</template>


<script>
import fromKassaToKassa from './fromKassaToKassa.vue'
import sendMainKassa from './sendMainKassa.vue';
import check from './check'
import rasxod from './rasxod'
import chiqarPulOlish from './chiqarPulOlish'
import checkshow from './checkshow'
import {mapActions, mapGetters, mapMutations} from 'vuex'
import {
  mdbIcon,
  mdbBtn,mdbInput
} from "mdbvue";
import Pay from './pay.vue';
import kassa_info from './kassa_info.vue';
import kassa_add_user_setting from './kassa_add_user_setting.vue';
export default {
  components: {
    mdbIcon,
    mdbBtn,mdbInput,
    check,checkshow,
    Pay,
    kassa_info, 
    rasxod,
    chiqarPulOlish,
    sendMainKassa,
    kassa_add_user_setting,
    fromKassaToKassa
  },
  data() {
    return {
      kassa_setting_status: false,
      kassa_num: localStorage.kassa_num,
      colors: ['#ff5733', '#33ff83', '#ac33ff', '#339fff', '#ff3393', '#ffe933', '#9033ff'],
      kassir: localStorage.user_name,
      closeCash: false,
      checkshow: false,
      kassa_setting_show: true,
      from_kassa_to_kassa_sts: false,

      send_kassa_status: false,
      kassa_info_show: false,

      modal_status: false,
      modal_info: '',
      Arrowdown:0,
      hiddenArrow: '',
      hiddenArrow1: '',
      payshow: false,
      unitPrice:'',
      acceptQty: 0,
      acceptUnit: 0,
      number: [1,2,3,4,5,6,7,8,9,0],
      searchBarcodeData: [],
      searchNameData: [],
      dangerSrog: '',
      warningSrog: '',

      enoughtDrag: false,
      UnitSumString: '0',

      check_show: false,
      searchDrag: '',
      showAccept: false,
      dataAccept: {
      },

      timestamp: "",
      totalName: '',
      getSumma: '0',
      zdachi: 0,


      savatActive: 0,
      barcode: '',
      productList: [],
      persantage_discount: 0,
      real_price_unit_product: 0,
      rasxod_show: false,
      pul_olib_qolish: false,
      choosenQty: 0,
      adminStatus: localStorage.AccessType,

    }
  },
 
  watch:{
    get_all_summa(){
      var a = this.get_all_summa[this.get_page_savat].toString()
      a = a.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      this.totalName = a;
    }
  },
  async mounted(){
    await this.fetchUserAccess(localStorage.user_id);

    // var number = 123456789.23
    // var num = new Intl.NumberFormat().format(number)
    // console.log(num)
    var mtime = new Date().getMonth();
    var ytime = new Date().getFullYear();
    if(mtime == 0){
      mtime = '01'
    }
    else if(mtime>0 && mtime<10){
      mtime = '0'+ mtime.toString();
    }
    else{
      mtime = mtime.toString();
    }
    this.dangerSrog = ytime.toString() + '-' + mtime;
    console.log(this.dangerSrog)
    console.log(ytime)

    if(this.get_zakaz_product_all_list.length == 0){ 
      this.update_zakaz_product_all_list()
    }
    console.log('this.get_zakaz_product_all_list.length');
    console.log(this.get_zakaz_product_all_list);
    {
      var a = '50000'
      a = a.replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      this.totalName = a;
    }
    
    this.$nextTick(function () {
      this.$refs.barcode.focus();
      this.barcode = '';
    })
    await this.fetchOstatikProduct();
  },
  computed:{
    ...mapGetters(['allOrderList','get_skidka_summ', 'get_all_summa', 
      'get_m_categoryIdProduct', 'get_zakaz_product_all_list','get_page_savat', 
      'get_product_qty','AllSummString', 'user_kassa_list']),
  },
  methods: {
    ...mapActions([ 'fetchProduct', 'fetchCategoryAllProduct', 'fetchCategoryIdProduct', 
    'fetchProductSearchByName', 'fetchKassa_userId']),
    ...mapMutations(['sklad_delete_row', 'add_product_order', 'minus_product', 'plus_product', 'del_product', 'clear_order', 'input_change', 'changeSumma', 'update_zakaz_product_all_list', 'select_savat_page', 'add_savat_page', 'del_savat_page']),
    async fetchUserAccess(id){
      try{
          const res = await fetch(this.$store.state.hostname + '/TegirmonUserAccess/getTegirmonUserAccessUserId?user_id=' + id);
          const data = await res.json();
          console.log('this is by id')
          if(res.status == 200 || res.status == 201){
            this.kassa_setting_show = data.status_4;
            this.kassa_info_show = data.status_3;
            
          }
      }
      catch(error){
        console.log(error)
      }
    },

    async closeSendKassa(){
      this.send_kassa_status = false;
      this.$refs.message.success('Added_successfully')
      // await this.apply();
    },
    // kassaga xodimlarni ulashni ochish bulimi.
    async show_kassa_setting(){
      this.$refs.kassa_add_user_setting.refresh();
      this.kassa_setting_status = true;
    },

    async show_send_savdo_kassa(){
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
      this.$refs.savdo_kassa_send.refresh(this.user_kassa_list[0]);
      this.send_kassa_status = true;
    },

    async show_from_kassa_to_kassa(){
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
        this.$refs.fromKassaToKassa_ref.refresh(this.user_kassa_list[0]);
      this.from_kassa_to_kassa_sts = true;
    },
    
    
    async fetchOstatikProduct(){
      try{
        const response = await fetch(this.$store.state.hostname + "/TegirmonOstatka/getPagination?page=0&size=100");
        const data = await response.json();
        console.log('data product ostatka list')
        console.log(data)
        if(response.status == 200 || response.status == 201){
          this.productList = data.items_list;
          // this.barcode = '';
        }
        else{
          this.$refs.message.error('not_found');
        }
      }
      catch{
        this.$refs.message.error('not_found')
      
      }
    },
    rasxodClick(){
      this.rasxod_show = true;
      this.$refs.rasxodWorkerSum.clw_rw();
    },
    pulOlibQolishChiqarNarsa(){
      this.pul_olib_qolish = true;
      this.$refs.prixodWorkerSum.clw_rw();

    },
    funcCurrency(n){
      console.log(n)
       this.UnitSumString = n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, " ")
       var temp = ''
       for(let i=0; i<this.UnitSumString.length; i++){
        if(this.UnitSumString[i] != ' '){
          temp += this.UnitSumString[i];
        }
       }
        // this.dataAccept.price = parseFloat((this.acceptQty * this.dataAccept.saledPrice)).toFixed(2)

       this.dataAccept.price = parseFloat(temp);
        this.acceptQty = parseFloat(this.dataAccept.price/this.persantage_discount).toFixed(2)
        this.dataAccept.qty = this.acceptQty;
        this.dataAccept.skidka = (parseFloat(this.real_price_unit_product) - parseFloat(this.persantage_discount))*this.acceptQty;
    },
    getNow: function() {
        const today = new Date();
        const date = today.getFullYear()+'-'+(today.getMonth()+1)+'-'+today.getDate();
        const time = today.getHours() + ":" +  today.getMinutes() + ":" + today.getSeconds();
        const dateTime = date +' '+ time;
        this.timestamp = dateTime;
    },

    focusBarcode(){
      this.$nextTick(function () {
        this.$refs.barcode.focus();
        this.barcode = '';
      })
    },

    async closePay(){
      this.payshow =! this.payshow;
      this.$nextTick(function () {
        this.$refs.barcode.focus();
        this.barcode = '';
      })
      await this.fetchOstatikProduct(); 
    },
    closeKassa(){
      this.closeCash =! this.closeCash
      this.$nextTick(function () {
        this.$refs.barcode.focus();
        this.barcode = '';
      })
    },
    closeChoosenProduct(){
      this.showAccept =! this.showAccept;
      this.$nextTick(function () {
        this.$refs.barcode.focus();
        this.barcode = '';
      })
    },
  


    // <--  Savat qushish start-->
    select_savat(i){
      console.log(i)
      this.savatActive = i
      this.select_savat_page(i)
    },
    add_savat(){
      this.add_savat_page()
      this.savatActive ++;
    },
    del_savat(i){
      this.del_savat_page(i)
    },
    // <--  Savat qushish end-->



    // <--  tekshirish uchun start-->
    onPaste(){
      console.log('hiii ukiii')
    },
    hanlePro(e){
      // console.log(e.data)
      if(e.data == null){
        console.log('barcode')
      }
      else{
        console.log(e.data)
      }
    },
    updateQty(i){
      this.changeSumma(i)
    },
    // <--  tekshirish uchun end-->



    // <--  Topligan productlarni tanlash u-n tepaga pastga start-->
    ArrowDown(){
      if(this.searchBarcodeData.length>0){
        if(this.searchBarcodeData.length != this.Arrowdown){
          this.Arrowdown ++;
          this.$nextTick(function () {
            this.$refs.hiddenArrow.focus();
            this.hiddenArrow = '';
          })
        }
      }
    },
    ArrowUp(){
      if(this.searchBarcodeData.length>0){
        if(this.Arrowdown != 1){
          this.Arrowdown --;
        }
        else{
          this.Arrowdown = 0;
          this.$nextTick(function () {
            this.$refs.barcode.focus();
          })
        }
      }
    },
    ArrowDown1(){
      if(this.searchBarcodeData.length>0){
        if(this.searchBarcodeData.length != this.Arrowdown){
          this.Arrowdown ++;
          this.$nextTick(function () {
            this.$refs.hiddenArrow1.focus();
            this.hiddenArrow1 = '';
          })
        }
      }
    },
    ArrowUp1(){
      if(this.searchBarcodeData.length>0){
        if(this.Arrowdown != 1){
          this.Arrowdown --;
        }
        else{
          this.Arrowdown = 0;
          this.$nextTick(function () {
            this.$refs.searchDrag.focus();
          })
        }
      }
    },
    ArrowRight(){
      this.$nextTick(function () {
        this.$refs.searchDrag.focus();
      })
    },
    ArrowRight1(){
      this.$nextTick(function () {
        this.$refs.barcode.focus();
      })
    },
    EnterArrowItem(){
      if(this.Arrowdown != 0){
        this.getProductOstatka(this.searchBarcodeData[this.Arrowdown-1])
      }
    },
    // <--  Topligan productlarni tanlash u-n tepaga pastga end-->



    // <--  Checkni chiqarish start-->
    printChek(){
      this.check_show = true;
      this.searchBarcodeData = []
    },
    closedChek(){
      this.check_show=false;
      this.ArrowRight1();
    },
    // <--  Checkni chiqarish end-->


    // <--  Productni izlash start-->
    async searchFunc(){
      if(this.searchDrag != ''){
          try{
            this.barcode = '';
            console.log('hiy')
            this.Arrowdown = 0;

            const response = await fetch(this.$store.state.hostname + "/TegirmonOstatka/getPaginationSearchProductByNameOrCodeOrShtrixCode?page=0&size=100&name_o_code_or_shtrix=" + this.searchDrag);
            
            if(response.status == 200 || response.status == 201){
              // this.barcode = '';
              this.$refs.searchDrag.focus();
              const data = await response.json();
              console.log('data')
              console.log(data)
              this.productList = data.items_list;

            }
            else{
              this.$refs.message.error('not_found');
            }
          }
          catch{
            // this.modal_info = this.$i18n.t("not_found");
            // this.modal_status = true
            this.searchBarcodeData = []
            this.$refs.message.error('not_found')
          
          }
        
        }
        else{
          await this.fetchOstatikProduct();
        }
    },


    async getbarcode(){
        if(this.barcode != ''){
          try{
            this.searchDrag=""
            console.log('hiy')
            this.Arrowdown = 0;
            const response = await fetch(this.$store.state.hostname + "/TegirmonOstatka/getPaginationSearchProductByNameOrCodeOrShtrixCode?page=0&size=100&name_o_code_or_shtrix=" + this.barcode);
            console.log('data')
           
            if(response.status == 200 || response.status == 201){
              // this.barcode = '';
              this.$refs.barcode.focus();
              const data = await response.json();
               console.log(data)
              this.productList = data.items_list;
            }
            else{
              this.$refs.message.error('not_found')
            }
          }
          catch{
            // this.modal_info = this.$i18n.t("not_found");
            // this.modal_status = true
            this.$refs.message.error('not_found')
          
          }
        }else{
          await this.fetchOstatikProduct();
        }
      },
    // <--  Productni izlash end-->


    // <--  Productni tanlab sonini yozadigan oyna ochadi start-->
      async getProductOstatka(data){
        try{
          const response = await fetch(this.$store.state.hostname + "/TegirmonOstatka/getPaginationOstatkaProduct_Id?page=0&size=10&product_id=" + data.TegirmonProductid);
          if(response.status == 200 || response.status == 201){
            const data_ostatka = await response.json();
            console.log('data_ostatka', data_ostatka);
            await this.clickBarcodeData(data_ostatka.items_list[0])
          }
        }
        catch{
          await this.clickBarcodeData(data)
        }
      },
      async updateOnlineProductOstatka(product_id){
        try{
          const response = await fetch(this.$store.state.hostname + "/TegirmonOstatka/getPaginationOstatkaProduct_Id?page=0&size=10&product_id=" + product_id);
          if(response.status == 200 || response.status == 201){
            const data_ostatka = await response.json();
            // console.log('data_ostatka product id', data_ostatka);
            this.dataAccept.ostatka_qty = (parseFloat(data_ostatka.items_list[0].real_qty)-parseFloat(this.choosenQty)).toFixed(2);
            if(parseFloat(this.acceptQty) > this.dataAccept.ostatka_qty){
              this.enoughtDrag = true;
              return true;
            }
            else {
              this.enoughtDrag = false;
              return false;
            }
          }
        }
        catch(error){
          console.log(error);
          return false;
        }
      },
      async clickBarcodeData(data){
        // console.log(data)
        // console.log('mansdnasdas dasjhdjashdasdas dajsdh')
        var chooseQty = 0;
        // var chooseUnit = 0;
        console.log(this.get_zakaz_product_all_list[this.get_page_savat])
        for(let i=0; i<this.get_zakaz_product_all_list[this.get_page_savat].length;i++){
          if(this.get_zakaz_product_all_list[this.get_page_savat][i].invoiceItemId == data.id){
            chooseQty = this.get_zakaz_product_all_list[this.get_page_savat][i].qty;
            // chooseUnit = this.get_zakaz_product_all_list[this.get_page_savat][i].qty_in_pack;
          }
        }
        
        this.showAccept = true;
        this.$nextTick(function () {
          this.$refs.upokovka.focus();
           this.acceptQty = null;
        })


        this.dataAccept.inv= data.product.inv_accepted_status;
        this.dataAccept.name = data.product.name;
        this.dataAccept.invoice = data.id;
        this.dataAccept.id = data.product.id;
        this.dataAccept.manifacturer_name = data.product.code;
        this.dataAccept.ostatka_qty = (parseFloat(data.real_qty)-chooseQty).toFixed(2);
        this.dataAccept.skidka = 0;
        this.choosenQty = chooseQty;
        // this.dataAccept.contains_number_in_pack = data.product.contains_number_in_pack;
        this.dataAccept.qty = 0;
        this.dataAccept.expired_date = data.created_date.slice(0,10);
        this.dataAccept.saledPrice = data.product.price;
        this.real_price_unit_product = data.product.price;
        this.dataAccept.price = 0;
        this.persantage_discount = data.product.price;
        this.unitPrice = new Intl.NumberFormat().format(this.dataAccept.saledPrice)
      },

      // oynani yopish
      async closeAcceptPage(){
        this.showAccept = false;
        this.$nextTick(function () {
          this.$refs.barcode.focus();
          this.barcode = '';
          this.Arrowdown = 0;
          this.UnitSumString = '0';
        })

      },
    // <--  Productni tanlab sonini yozadigan oyna ochadi end-->


    // <--  Tanlangan productga son yozishda o'ngga chapga knopka start-->
      ArrowLeftS(){
          this.summaDrag();
        this.$nextTick(function () {
          this.$refs.acceptUnit.focus();
          if(this.acceptUnit == 0){
            this.acceptUnit = '';
          }
          if(this.acceptQty == ''){
            this.acceptQty = 0;
          }
        })
      },
      ArrowRightS(){
        this.summaDrag();
        this.$nextTick(function () {
          this.$refs.upokovka.focus();
          if(this.acceptQty == 0){
            this.acceptQty = '';
          }
          if(this.acceptUnit == ''){
            this.acceptUnit = 0;
          }
        })
      },
    // <--  Tanlangan productga son yozishda o'ngga chapga knopka end-->



    // <--  Tanlangan productni savatga qushadi start-->
      async AddAcceptPage(){
        this.blurUnit();
        this.blurQty();
        if(await this.updateOnlineProductOstatka(this.dataAccept.id)){
          return true;
        }
        if(this.enoughtDrag == false){
          if(this.acceptUnit == 0 && this.acceptQty == 0){
            this.showAccept = false;
          }
          else{
            this.showAccept = false;
            this.add_product_order(this.dataAccept)
            this.$nextTick(function () {
              this.$refs.barcode.focus();
              this.barcode = '';
              this.Arrowdown = 0;
            })
          }
          this.acceptUnit = 0;
          this.acceptQty = 0;
          this.UnitSumString = '0';
        
        }
      },
    // <--  Tanlangan productni savatga qushadi end-->


    // <--  Tanlangan productni sonini summasini hisoblaydi start-->
      summaDrag(){
        this.dataAccept.skidka = (parseFloat(this.real_price_unit_product) - parseFloat(this.persantage_discount))*this.acceptQty;
        this.dataAccept.saledPrice =  this.persantage_discount;
        this.dataAccept.price = parseFloat((this.acceptQty * this.persantage_discount)).toFixed(2)
        this.UnitSumString = new Intl.NumberFormat().format(this.dataAccept.price)
         if(parseFloat(this.acceptQty) > this.dataAccept.ostatka_qty){
           this.enoughtDrag = true;
         }
         else{
           this.enoughtDrag = false;
         }
        this.dataAccept.qty = this.acceptQty;
      },
    // <--  Tanlangan productni sonini summasini hisoblaydi end-->
      


    // <--  Tanlangan productni soni bush bulsa yoki 0 ga teng bulsa yozayotganda uchiradi start-->
      writeNol(){
        if(this.acceptQty == 0){
          this.acceptQty = null;
        }
      },
      writeNolUnit(){
        if(this.acceptUnit == 0){
          this.acceptUnit = null;
        }
      },
      blurQty(){
        if(this.acceptQty == null || this.acceptQty == ''){
          this.acceptQty = 0;
          this.dataAccept.qty = this.acceptQty;
        }
      },
      blurUnit(){
        if(this.acceptUnit == null || this.acceptUnit == ''){
          this.acceptUnit = 0;
          this.dataAccept.unit = this.acceptUnit;
        }
        else if(this.acceptUnit>=this.dataAccept.contains_number_in_pack){
          console.log(typeof(this.dataAccept.contains_number_in_pack))
          var temp = 0;
          temp = this.acceptUnit%this.dataAccept.contains_number_in_pack;
          this.acceptQty = parseInt(this.acceptQty) + parseInt(this.acceptUnit/this.dataAccept.contains_number_in_pack)
          this.acceptUnit = temp;
          this.dataAccept.qty = this.acceptQty;
          this.dataAccept.unit = this.acceptUnit;
        }

      },
    // <--  Tanlangan productni soni bush bulsa yoki 0 ga teng bulsa yozayotganda uchiradi end-->



    // <-- To'lov turlarini tanlash  start-->
      async cashPay(){
        this.payshow = true;
        this.$root.$refs.payed.changingEnter(1);
      },
      async uzcardPay(){
        this.payshow = true;
        this.$root.$refs.payed.changingEnter(2);
      },
      async humoPay(){
        this.payshow = true;
        this.$root.$refs.payed.changingEnter(3);
      },
      
      // 4 chi raqam dolg uchun qolgan
      async onlinePay(){
        this.payshow = true;
        this.$root.$refs.payed.changingEnter(5);
      },
      async clickPay(){
        this.payshow = true;
        this.$root.$refs.payed.changingEnter(6);
      },
      async plastikPay(){
        this.payshow = true;
        this.$root.$refs.payed.changingEnter(4);
      },

      async paymePay(){
        this.payshow = true;
        this.$root.$refs.payed.changingEnter(7);
      },
      async clickedPay(){
        this.payshow = true;
        this.$root.$refs.payed.changingEnter(8);
      },
      async paynetPay(){
        this.payshow = true;
        this.$root.$refs.payed.changingEnter(9);
      },
      async uzumPay(){
        this.payshow = true;
        this.$root.$refs.payed.changingEnter(10);
      },
      async funcKassaInfo(){
        this.closeCash = true;
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
        this.$refs.kassa_info_ref.getAllSumm(this.user_kassa_list[0]);
      },
    // <-- To'lov turlarini tanlash  end-->


    // <-- Pul qaytarish Return mone  start-->
      returnMoney(){
        console.log('return it is')
      },
    // <-- Pul qaytarish Return mone  end-->


    checkShow(){
      this.checkshow = !this.checkshow;
      this.$nextTick(function () {
        this.$refs.barcode.focus();
        this.barcode = '';
      })
    },
    clearSavat(){
      // this.loading = true;
      this.clear_order();
    },
    deleteProduct(data, i){
      this.del_product({data:data, index:i})
    },
      




    // <-- Programmadan chiqish  start-->
      logout(){
        this.$router.push('/')
      }
    // <-- Programmadan chiqish  start-->

  },
    
}
</script>

<style lang="scss">
::-webkit-scrollbar {
  width: 5px;

}

/* Track */
::-webkit-scrollbar-track {
  background: #f1f1f1;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #888;
  border-radius: 2px;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #555;
}

  .allPage{
    width: 100%;
    display: flex;
    height: 95vh;
    .tablePageLeft{
      width: 100%;
      
      .tableAndBtn{
        width:100%;
        display:flex;
        
        // overflow: hidden;
        
        .myTablesforSaleNew{
          width: 100%;
            height: 41vh;
            overflow-y:scroll;
          .tableProducts{
            
            padding-bottom:40px;
             .myTablesforSaleNew { 
                
                /* border-collapse: collapse; */
                table-layout:fixed;
                width: 100%;
                overflow: hidden;
                // border: 1px solid #ddd;
                font-size: 18px;
                max-height:80px; overflow-x:auto
              }
              .myTablesforSaleNew thead{
                background:#0e0d0d; 
              }
              .myTablesforSaleNew th{
                font-weight: 600;
                color:#5a5858; 
                font-size:12px;
              }
              .myTablesforSaleNew td{
                font-size:13px;
              }
              .myTablesforSaleNew th, .myTablesforSaleNew td {
                text-align: left;
                padding: 1px;
              }

              .myTablesforSaleNew tr {
                border-bottom: 1px solid rgb(240, 240, 240);
              }

              .myTablesforSaleNew tr.header, .myTablesforSaleNew tr:hover {
                // background-color: #f1f1f1;
              }
            .delIcon{
              color: rgb(251, 70, 70);
              font-size: 13px;
            }
          }
          
        }

        .myBtn{
          width: 600px;

          .numClick:hover{
            cursor: pointer;
            background: rgb(243, 243, 243);
            // h2{
            //   pointer-events: none;
            // }
          }
        }
      }
      .SearchNameBarcode{

        .FoundProductTable{
          height: 48vh;
          overflow-y: scroll;

          .tableProducts{
            
            .myTablesforSaleNew { 
              
              /* border-collapse: collapse; */
              table-layout:fixed;
              width: 100%;
              overflow: hidden;
              // border: 1px solid #ddd;
              font-size: 18px;
              max-height:80px; overflow-x:auto
            }
            .myTablesforSaleNew th{
              font-weight: 600;
              color:#5a5858; 
              font-size:12px;
            }
            .myTablesforSaleNew td{
              font-size:13px;
            }
            .myTablesforSaleNew th, .myTablesforSaleNew td {
              text-align: left;
              padding: 1px;
            }

            .myTablesforSaleNew tr {
              border-bottom: 1px solid rgb(240, 240, 240);
            }

            .myTablesforSaleNew tr.header, .myTablesforSaleNew tr:hover {
              // background-color: #f1f1f1;
            }
            .myTablesforSaleNew thead{
                background:#c5eeea; 
              }
            .delIcon{
              color: rgb(251, 70, 70);
              font-size: 13px;
            }
          }
        }
      }
    }
    .SummaBtnRight{
      width: 500px;

      .borderSolder{
        border: 0.5px dashed #D0D3D8;

        span{
          color:#67676C;
          font-size: 22px;
        }
        p{
          color:#525255;
          font-weight:bold;
          font-size: 24px;
          margin:0;
          padding:0;
        }
      }
      .borderSolderOplate{
        border: 0.5px dashed #D0D3D8;

        span{
          color:#67676C;
          font-size: 22px;
        }
        p{
          color:#525255;
          font-weight:bold;
          font-size: 28px;
          margin:0;
          padding:0;
        }
      }
    }
  }
  .aptekaSale{
    .oplate{
      min-height:180px;
      .allSumma{
        width: 100%;
        .asosiyTotal{
          max-width: 700px;
          min-width: 690px;
        }
        .barcodeMenu{
          width: 100%;
        }
        .Summa{
          height: 90px;
        }

        .getSumFromClient{
          .zdachi{
              color:#f64a4a;
              font-weight:bold;
              font-size: 25px;
          }
        }
      }

      .payBtn{
        width: 250px;
      }
    }
    
  }
  .tableProduct{
    // overflow-y: scroll;
    overflow-y: hidden;
    max-height: 79vh;
    .myTablesforSaleNew { 
      
      /* border-collapse: collapse; */
      table-layout:fixed;
      width: 100%;
      overflow: hidden;
      // border: 1px solid #ddd;
      font-size: 18px;
      max-height:80px; overflow-x:auto
    }
    .myTablesforSaleNew th{
      font-weight: 600;
      color:#5a5858; 
      font-size:12px;
    }
    .myTablesforSaleNew td{
      font-size:13px;
    }
    .myTablesforSaleNew th, .myTablesforSaleNew td {
      text-align: left;
      padding: 5px;
    }

    .myTablesforSaleNew tr {
      border-bottom: 1px solid rgb(240, 240, 240);
    }

    .myTablesforSaleNew tr.header, .myTablesforSaleNew tr:hover {
      // background-color: #f1f1f1;
    }
    .myTablesforSaleNew thead{
      background:#c5eeea; 
    }
    .delIcon{
      color: rgb(251, 70, 70);
      font-size: 13px;
    }
  }
  .searchBarcodeHover:hover{
    background: seagreen;
    color:seashell;
    cursor: pointer;
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
    background: rgba(2, 2, 2, 0.6);
    z-index: 1111;
  }
  .acceptBox{
    width:500px;
    height: 380px;
    background: snow;
    box-shadow: 3px 2px 5px rgb(129, 129, 129);
    border-radius: 5px;
  }
  .bg_dark{
    background: #202020;
  }
  .bg_danger{
    background: #fb6b6b;
  }
  .bg_warning{
    background: rgb(255, 230, 91);
  }
  .bg_success{
    background: #5fff42;
  }

  .back_dang{
    background: #fb6b6b;
  }
  .back_warning{
    background: #fdcb7a;
  }
  .backlight{
    background: rgb(255, 252, 249);
  }
  .arrow_back_active{
    background: seagreen;
    color:seashell;
  }
  .inputFocusSearch:focus{
    border-bottom: 2px solid rgb(66, 110, 255);
    // background: #f64a4a;
  }
  .inputFocusSearch:focus + .control-label{
    color: rgb(66, 110, 255);
  }
  .inputFocusSearch{
    border: 1px solid rgb(255, 255, 255);
    border-bottom: 1px solid rgb(197, 197, 197);
    // background: #f64a4a;
  }
  .userLog{
    .logout{
      visibility: hidden;
    }
  }
  .userLog:hover{
    .logout{
      visibility: visible;
      background: rgb(255, 255, 255);
    }
    .logout:hover{
      background: rgb(218, 218, 218);
    }

  }

  .linkMain{
    border-bottom: 1px solid rgb(74, 87, 95);
    border-top: 1px solid rgb(74, 87, 95);
    color:rgb(64, 66, 64);
    cursor:pointer;
    font-size: 13.5px;
  }
  .linkMain:hover{
    background: #ffb300;
    color:white;
  }
  .linkMainDang{
    border-bottom: 1px solid rgb(74, 87, 95);
    border-top: 1px solid rgb(74, 87, 95);
    color:rgb(64, 66, 64);
    cursor:pointer;
    font-size: 13.5px;
  }
  .linkMainDang:hover{
    background: #ff5852;
    color:white;
  }

  .linkMainRasxod{
    border-bottom: 1px solid rgb(74, 87, 95);
    border-top: 1px solid rgb(74, 87, 95);
    cursor:pointer;
    font-size: 14.5px;
    height: 70px;
    background: #ea605b; 
    color:white;
    display: flex;
    align-items: center;
    justify-content: center;
    &:hover{
      color:white;
      background: #fc4640;  
    }
  }
  .linkMainPrixod{
    border-bottom: 1px solid rgb(74, 87, 95);
    border-top: 1px solid rgb(74, 87, 95);
    cursor:pointer;
    height: 70px;
    font-size: 14.5px;
    background: #61e176; 
    color:white;
    display: flex;
    align-items: center;
    justify-content: center;
    &:hover{
      color:white;
      background: #33d95f;  
    }
  }
  .bg_kassa_to_kassa{
    background: #3b678b !important; 
  }
</style>
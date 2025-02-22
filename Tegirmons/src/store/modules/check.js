export default {
  state: {
      check_group_list: [],
      check_all_summ: 0,
      zaxiraList: [],
      zaxiraListInvoice: [],
      zaxira_summa: 0,
      user_to_zaxira: {
        client_new_name: '',
      },
      ostatka_check_zaxira: 0,

      check_get_ostatka: [],
      check_get_invoice: {},

  },
  actions: {
      async fetchCheck(ctx) {
          const res = await fetch(ctx.rootState.hostname + '/TegirmonCompanyControler');
          const res_data = await res.json();
          // console.log(ctx.rootState.hostname);
          ctx.commit('updateCompany', res_data);
      },
  },
  mutations: {
    getAllCheck(state, data) {
        console.log('check_group')
        console.log(data)
        state.check_group_list = [];
        state.check_all_summ = 0;
        for(let i=0; i<data.length; i++){
          state.check_all_summ += data[i].summ;
          let itemInvoice = {
            client_name : data[i].client_new_name,
            product_name: data[i].product_name,
            real_qty: data[i].qty,
            group_id: data[i].group_id,
            kassir: data[i].kassir,
            date: data[i].date,
            sell : {
              product_name: data[i].product_name,
              product_price: data[i].product_price,
              sell_price: data[i].summ,
              skidka: data[i].skidka,
              qty: data[i].invoice_note,
            },
            changeProduct: [],
          }
          for(let j=0; j<data[i].changeProduct.length; j++){
            let changeItem = {
              product_name : data[i].changeProduct[j].product_name,
              qty : data[i].changeProduct[j].qty,
              all_qty : data[i].changeProduct[j].persantage,
              // measure: data[i].changeProduct[j].measure,
            }
            itemInvoice.changeProduct.push(changeItem)
          }
          state.check_group_list.push(itemInvoice)
        }

        console.log('state.check_group_list')
        console.log(state.check_group_list)
        
    },
    zaxiraCheckList(state,data){
      console.log('zaxira check')
      console.log('zaxira check123')
      console.log('zaxira check123w')
      console.log(data)
      state.zaxiraList = [];
      state.zaxira_summa = data.buy_sum;

      let zaxiraInvoicce = {
        client_name : data.client_name,
        product_name: data.product_name,
        main_client: data.client.fio,
        client_id: data.client.id,
        client_key: data.client_key,
        id: data.invoice_id,
        real_qty: 0,
        group_id: 0,
        sell : {
          product_name: data.product_name,
          product_price: 0,
          sell_price: data.buy_sum,
          qty: data.buy_qty
        },
        changeProduct: [],
      }
      console.log(data.list)
      console.log('data.list')
      console.log(data.measure_name)
      for(let j=0; j<data.list.length; j++){
        if(data.list[j].real_qty>0){
          let changeItem = {
            product_name : data.list[j].product_name,
            qty : data.list[j].real_qty,
            all_qty : (parseFloat(data.list[j].real_qty)/data.list[j].persantage).toFixed(2),
            measure: data.list[j].measure,
            main_product_measure: data.measure_name,
          }
          if(parseFloat(data.list[j].all_sum)>0){
            changeItem.all_qty = (parseFloat(data.list[j].all_sum)).toFixed(0)
          }
          zaxiraInvoicce.changeProduct.push(changeItem)
        }
        
      }
      state.zaxiraList.push(zaxiraInvoicce)
      console.log('state.zaxiraList')
      console.log(state.zaxiraList)
    },
    check_invoice_zaxira(state,data){
      console.log('zaxiraListInvoice check')
      console.log(data)
      state.zaxiraListInvoice = [];
      let sellMeasure = 0;
      let kassir_name = '';
      if(data.dolg_summ == 0){
        sellMeasure = data.auth_user_creator_id
      }
      else{
        sellMeasure = data.dolg_summ
      }
      // state.zaxira_summa = data.buy_sum;
      if(data.auth){
        kassir_name = data.auth.user.fio
      }
      let zaxiraInvoicce = {  
        client_name : data.client.fio,
        client_dv : data.user_name,
        client_id: data.client.id,
        client_key: data.client.car_number,
        date: data.created_date_time,
        ostatka_old: data.credit_sum,
        product_name: data.product.name,
        real_qty: 0,
        group_id: 0,
        id: data.id,
        check_number: data.check_number,
        kassir: kassir_name,
        sell : {
          product_name: data.product.name,
          product_price: 0,
          sell_price: data.summ,
          qty: sellMeasure
        },
        changeProduct: [],
      }
      for(let j=0; j<data.item_list.length; j++){
        if(data.item_list[j].real_qty>0){
          let changeItem = {
            product_name :data.item_list[j].product.name,
            qty :data.item_list[j].real_qty,
            all_qty : parseFloat(data.item_list[j].real_qty)/data.item_list[j].real_sum,
            // measure:data.item_list[j].measure,
          }
          if(parseFloat(data.item_list[j].all_sum)>0){
            changeItem.all_qty = parseFloat(data.item_list[j].all_sum);
          }
          zaxiraInvoicce.changeProduct.push(changeItem)
        }
        
      }
      state.zaxiraListInvoice.push(zaxiraInvoicce)
      // console.log('state.zaxiraListInvoice')
      // console.log(state.zaxiraListInvoice)
    },

    zaxiragaUtkazishList(state, data){
      state.user_to_zaxira = data
    },
    clearZaxiraList(state){
      state.user_to_zaxira = {
        client_new_name: '',
      }
    },
    get_ostatka_check(state, data){
      console.log('ostatka qty')
      console.log(data)
      state.ostatka_check_zaxira = data;
    },
    get_ostatka_check_for_get(state,data){
      console.log('check uchun')
      console.log(data)
      state.check_get_ostatka = data;
    },
    get_invoice_for_invoice(state,data){
      console.log('check uchun invoice')
      console.log(data)
      state.check_get_invoice = data;
    },
      

  },
  getters: {
    get_all_check(state) {
      return state.check_group_list;
    },
    check_all_summ(state) {
      return state.check_all_summ;
    },
    zaxiraList(state) {
      return state.zaxiraList;
    },
    zaxira_summa(state) {
      return state.zaxira_summa;
    },
    user_to_zaxira(state){
      return state.user_to_zaxira;
    },
    get_ostatka_check_zaxira(state){
      return state.ostatka_check_zaxira
    },
    check_get_ostatka(state){
      return state.check_get_ostatka;
    },
    check_get_invoice(state){
      return state.check_get_invoice;
    },
    zaxiraListInvoice(state){
      return state.zaxiraListInvoice;
    },

  }
}
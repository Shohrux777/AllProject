export default {
    state: {
        zaxira_change_invoice_list: [],
        saled_invoice_list: [],
        zaxira_invoice_list: [],
        change_invoice_list: [],
        Saled_invoice_list_productId: [],
        Zaxira_invoice_list_productId: [],
        Change_invoice_list_productId: [],
    },
    actions: {
        async fetchZaxira_change_invoice_list(ctx, data) {
            try{
                const res = await fetch(ctx.rootState.hostname + '/TegirmonInvoiceItem/getUsedInvoiceProductsQtyBeatweenDate?page=0&size=1000&begin_date=' + data.begin + '&end_date=' + data.end);
                const res_data = await res.json();
                ctx.commit('updateZaxira_change_invoice_list', res_data);
            }
            catch(error){
                console.log(error);
            }
        },
        async fetchSaled_invoice_list(ctx, data) {
            try{
                const res = await fetch(ctx.rootState.hostname + '/TegirmonPayments/getSaledProductsQtyBeatweenDate?page=0&size=1000&begin_date=' + data.begin + '&end_date=' + data.end);
                const res_data = await res.json();
                ctx.commit('updateSaled_invoice_list', res_data);
            }
            catch(error){
                console.log(error);
            }
        },
        async fetchZaxira_invoice_list(ctx, data) {
            try{
                const res = await fetch(ctx.rootState.hostname + '/TegirmonInvoiceItem/getUsedInvoiceProductsQtyZaxiraBeatweenDate?page=0&size=1000&begin_date=' + data.begin + '&end_date=' + data.end);
                const res_data = await res.json();
                ctx.commit('updateZaxira_invoice_list', res_data);
            }
            catch(error){
                console.log(error);
            }
        },
        async fetchChange_invoice_list(ctx, data) {
            try{
                const res = await fetch(ctx.rootState.hostname + '/TegirmonInvoiceItem/getUsedInvoiceProductsQtyFastChangeBeatweenDate?page=0&size=1000&begin_date=' + data.begin + '&end_date=' + data.end);
                const res_data = await res.json();
                ctx.commit('updateChange_invoice_list', res_data);
            }
            catch(error){
                console.log(error);
            }
        },
        async fetchSaled_invoice_list_productId(ctx, data) {
            try{
                const res = await fetch(ctx.rootState.hostname + '/TegirmonPayments/getSaledProductsQtyProductIdBeatweenDate?page=0&size=100&begin_date=' + data.begin + '&end_date=' + data.end + '&product_id=' + data.product_id);
                const res_data = await res.json();
                ctx.commit('updateSaled_invoice_list_productId', res_data);
            }
            catch(error){
                console.log(error);
            }
        },
        async fetchZaxira_invoice_list_productId(ctx, data) {
            try{
                const res = await fetch(ctx.rootState.hostname + '/TegirmonInvoiceItem/getUsedInvoiceProductsQtyZaxiraProductIdBeatweenDate?page=0&size=1000&begin_date=' + data.begin + '&end_date=' + data.end + '&product_id=' + data.product_id);
                const res_data = await res.json();
                ctx.commit('updateZaxira_invoice_list_productId', res_data);
            }
            catch(error){
                console.log(error);
            }
        },
        async fetchChange_invoice_list_productId(ctx, data) {
            try{
                const res = await fetch(ctx.rootState.hostname + '/TegirmonInvoiceItem/getUsedInvoiceProductsQtyFastChangeProductIdBeatweenDate?page=0&size=100&begin_date=' + data.begin + '&end_date=' + data.end + '&product_id=' + data.product_id);
                const res_data = await res.json();
                ctx.commit('updateChange_invoice_list_productId', res_data);
            }
            catch(error){
                console.log(error);
            }
        }

    },
    mutations: {
        // obshiy zaxira bilan srazi almashtirilganni hisoblaydi 
        updateZaxira_change_invoice_list(state, data) {
            state.zaxira_change_invoice_list = data.items_list;
        },

        // hammasini addenli addenli hisoblaydi
        updateSaled_invoice_list(state, data) {
            state.saled_invoice_list = data.items_list;
        },
        updateZaxira_invoice_list(state, data) {
            state.zaxira_invoice_list = data.items_list;
        },
        updateChange_invoice_list(state, data) {
            state.change_invoice_list = data.items_list;
        },

        updateSaled_invoice_list_productId(state, data) {
            console.log(data, 'saled')
            state.Saled_invoice_list_productId = data.items_list;
            if(data.items_list.length>0){
                state.Saled_invoice_list_productId[0].status = 'Продажа продукт'
            }
        },
        updateZaxira_invoice_list_productId(state, data) {
            console.log(data, 'zaxira')
            state.Zaxira_invoice_list_productId = data.items_list;
            if(data.items_list.length>0){
                state.Zaxira_invoice_list_productId[0].status = 'Захира продукт'
            }
        },
        updateChange_invoice_list_productId(state, data) {
            console.log(data, 'change')
            state.Change_invoice_list_productId = data.items_list;
            if(data.items_list.length>0){
                state.Change_invoice_list_productId[0].status = 'Тароз список продукт'
            }
        },
    },
    getters: {
        get_zaxira_change_invoice_list(state) {
            return state.zaxira_change_invoice_list;
        },
        get_saled_invoice_list(state) {
            return state.saled_invoice_list;
        },
        get_zaxira_invoice_list(state) {
            return state.zaxira_invoice_list;
        },
        get_change_invoice_list(state) {
            return state.change_invoice_list;
        },
        get_product_all_item_one(state) {
            return state.Saled_invoice_list_productId.concat(state.Zaxira_invoice_list_productId, state.Change_invoice_list_productId);
        }

    }
}
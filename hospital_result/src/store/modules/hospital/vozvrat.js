export default {
    state: {
        payed_date_vozvrat_list: [],
        payed_date_vozvrat_summa: 0,
        payed_date_vozvrat_list_kassir: [],
        payed_date_vozvrat_summa_kassir: 0,
        payed_date_vozvrat_summa_kassir_obshey: {
            rows: [],
            summ: 0,
        },
        payed_date_vozvrat_list_doc_service: {
            summ: 0,
            list: [],
        },
        payed_date_vozvrat_list_doc_operatsion: {
            summ: 0,
            list: [],
        },
        
    },
    actions: {
        async fetch_payed_date_vozvrat_list(ctx, data) {
            const res = await fetch(ctx.rootState.hostname + '/VozvratAlreadyPaidPaymentLists/getVozvratByDateTimeAsList?beginDate=' + data.time1 + '&endDate=' + data.time2);
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('Updatepayed_date_vozvrat_list', {data: res_data, time:data});
        },
        async fetch_payed_date_vozvrat_list_kassir(ctx, data) {
            const res = await fetch(ctx.rootState.hostname + '/VozvratAlreadyPaidPaymentLists/getVozvratByDateTimeAsList_kassir?beginDate=' + data.time1 + '&endDate=' + data.time2 + '&payed_id=' + data.contId);
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('Updatepayed_date_vozvrat_list_kassir', {data: res_data, time:data});
        },
        async fetch_payed_date_vozvrat_list_doc(ctx, data) {
            const res = await fetch(ctx.rootState.hostname + '/VozvratAlreadyPaidPaymentLists/getVozvratByDateTimeAsList?beginDate=' + data.time1 + '&endDate=' + data.time2);
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('Updatepayed_date_vozvrat_list_doc', {data: res_data, time:data});
        },
    },
    mutations: {
        Updatepayed_date_vozvrat_list(state, data) {
            console.log('temp_list_vozvrat')
            console.log(data.data)
            console.log(data.time)
            state.payed_date_vozvrat_list = [];
            state.payed_date_vozvrat_summa = 0;
            for(let i=0; i<data.data.length; i++){
                if(new Date(data.data[i].payedDate) < new Date(data.time.time1)){
                    state.payed_date_vozvrat_list.push(data.data[i]);
                    state.payed_date_vozvrat_summa += data.data[i].summa;
                }
            }
        },
        Updatepayed_date_vozvrat_list_kassir(state, data) {
            console.log('temp_list_vozvrat')
            console.log(data.data)
            console.log(data.time)
            state.payed_date_vozvrat_list_kassir = [];
            state.payed_date_vozvrat_summa_kassir = 0;
            state.payed_date_vozvrat_summa_kassir_obshey = {
                rows: [],
                summ: 0,
            }
            for(let i=0; i<data.data.length; i++){
                state.payed_date_vozvrat_summa_kassir_obshey.rows.push(data.data[i]);
                state.payed_date_vozvrat_summa_kassir_obshey.summ += data.data[i].summa;
                if(new Date(data.data[i].payedDate) < new Date(data.time.time1)){
                    state.payed_date_vozvrat_list_kassir.push(data.data[i]);
                    state.payed_date_vozvrat_summa_kassir += data.data[i].summa;
                }
            }
        },
        Updatepayed_date_vozvrat_list_doc(state, data) {
            console.log('temp_list_vozvrat_doc')
            console.log(data.data)
            console.log(data.time)
            state.payed_date_vozvrat_list_doc_service.summ = 0;
            state.payed_date_vozvrat_list_doc_service.list = [];
            state.payed_date_vozvrat_list_doc_operatsion.summ = 0;
            state.payed_date_vozvrat_list_doc_operatsion.list = [];
            for(let i=0; i<data.data.length; i++){
                if(new Date(data.data[i].payedDate) < new Date(data.time.time1)){
                    if(data.data[i].authorizationId == data.time.auth_id && data.data[i].authorizationId != null){
                        if(data.data[i].labGroup == 'операция '){
                            state.payed_date_vozvrat_list_doc_operatsion.list.push(data.data[i])
                            state.payed_date_vozvrat_list_doc_operatsion.summ += parseInt(data.data[i].summa)
                        }
                        else{
                            state.payed_date_vozvrat_list_doc_service.list.push(data.data[i])
                            state.payed_date_vozvrat_list_doc_service.summ += parseInt(data.data[i].summa)
                        }
                    }
                    
                }
            }
            console.log('state.payed_date_vozvrat_list_doc_service')
            console.log(state.payed_date_vozvrat_list_doc_service)
            console.log(state.payed_date_vozvrat_list_doc_operatsion)
        },
        
    },
    getters: {
        get_payed_date_vozvrat_list(state) {
            return state.payed_date_vozvrat_list;
        },
        get_payed_date_vozvrat_summa(state) {
            return state.payed_date_vozvrat_summa;
        },
        get_payed_date_vozvrat_list_doc_service(state) {
            return state.payed_date_vozvrat_list_doc_service;
        },
        get_payed_date_vozvrat_list_doc_operatsion(state) {
            return state.payed_date_vozvrat_list_doc_operatsion;
        },
        get_payed_date_vozvrat_list_kassir(state) {
            return state.payed_date_vozvrat_list_kassir;
        },
        get_payed_date_vozvrat_summa_kassir(state) {
            return state.payed_date_vozvrat_summa_kassir;
        },
        get_payed_date_vozvrat_summa_kassir_obshey(state) {
            return state.payed_date_vozvrat_summa_kassir_obshey;
        },
    }
}
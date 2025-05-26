export default {
    state: {
        smena_list: {
            rows: [],
            columns: [ 'id','smena_nomi', 'boshlanishi', 'tugashi', 'obed_b', 'obed_t', 'kech_keldi', 'vox_ketdi'],
            col: []
        },
        ish_grafigi: {
            rows: [],
            columns: [ 'id','nomi', 'sikl_soni', 'kun_hafta'],
            col: []
        },
        result_gr: {
            rows: [],
            columns: [ 'id', 'g_nomi' ,'ish_b', 'ish_t', 'obed_b', 'obed_t', 'kech_keldi', 'vox_ketdi'],
            col: []
        },
    },
    actions: {
        async fetch_Smena(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/SkudSmenas');
            const res_data = await res.json();
            ctx.commit('Updatesmena_list', res_data);
        },
        async fetch_ishGrafigi(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/SkudIshGrafigis');
            const res_data = await res.json();
            ctx.commit('UpdateishGrafigi', res_data);
        },
        async fetch_result_gr(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/SkudResultGrs');
            const res_data = await res.json();
            ctx.commit('Updateresult_gr', res_data);
        }
    },
    mutations: {
        Updatesmena_list(state, data) {
            state.smena_list.rows = data;
        },
        UpdateishGrafigi(state, data) {
            state.ish_grafigi.rows = data;
        },
        Updateresult_gr(state, data) {
            state.result_gr.rows = data;
        },
        district_row_delete(state, index) {
            state.smena_list.rows.splice(parseInt(index), 1);
        }

    },
    getters: {
        get_smena_list(state) {
            return state.smena_list;
        },
        get_ish_grafigi(state) {
            return state.ish_grafigi;
        },
        get_result_gr(state) {
            return state.result_gr;
        },

    }
}
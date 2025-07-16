export default {
    state: {
        smena_list: {
            rows: [],
            columns: [ 'id','smena_nomi', 'boshlanishi', 'tugashi'],
            col: []
        },
    },
    actions: {
        async fetch_Smena(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/SkudSmenas');
            const res_data = await res.json();
            ctx.commit('Updatesmena_list', res_data);
        }
    },
    mutations: {
        Updatesmena_list(state, data) {
            state.smena_list.rows = data;
        },
        smena_row_delete(state, index) {
            state.smena_list.rows.splice(parseInt(index), 1);
        }

    },
    getters: {
        get_smena_list(state) {
            return state.smena_list;
        },

    }
}
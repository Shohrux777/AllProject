export default {
    state: {
        company_list: {
            rows: [],
            columns: [ 'id','name'],
            col: []
        },
    },
    actions: {
        async fetch_Company(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/SkudCompanies');
            const res_data = await res.json();
            ctx.commit('Updatecompany_list', res_data);
        }
    },
    mutations: {
        Updatecompany_list(state, data) {
            state.company_list.rows = data;
        },
        district_row_delete(state, index) {
            state.company_list.rows.splice(parseInt(index), 1);
        }

    },
    getters: {
        get_company_list(state) {
            return state.company_list;
        },

    }
}
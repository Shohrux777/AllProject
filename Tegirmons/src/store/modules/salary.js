export default {
    state: {
        salary_list: {
            rows: [],
            columns: [ 'id','name', 'value', 'reserved_value'],
            col: []
        },
    },
    actions: {
        async fetch_Salary(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/SkudOyliks');
            const res_data = await res.json();
            ctx.commit('Updatesalary_list', res_data);
        }
    },
    mutations: {
        Updatesalary_list(state, data) {
            state.salary_list.rows = data;
        },
        salary_row_delete(state, index) {
            state.salary_list.rows.splice(parseInt(index), 1);
        }

    },
    getters: {
        get_salary_list(state) {
            return state.salary_list;
        },

    }
}
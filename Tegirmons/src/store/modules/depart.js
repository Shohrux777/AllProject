export default {
    state: {
        dept_list: {
            rows: [],
            columns: [ 'id','deptname', 'code'],
            col: []
        },
    },
    actions: {
        async fetch_Dept(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/SkudMyDepartments');
            const res_data = await res.json();
            ctx.commit('Updatedept_list', res_data);
        }
    },
    mutations: {
        Updatedept_list(state, data) {
            state.dept_list.rows = data;
        },
        dept_row_delete(state, index) {
            state.dept_list.rows.splice(parseInt(index), 1);
        }

    },
    getters: {
        get_dept_list(state) {
            return state.dept_list;
        },

    }
}
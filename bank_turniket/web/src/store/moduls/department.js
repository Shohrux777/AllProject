export default {
    state: {
        deparment_list: {
            rows: [],
            columns: [ 'id','name'],
            col: []
        },
    },
    actions: {
        async fetch_Department(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/SkudMyDepartments');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('Updatedeparment_list', res_data);
        }
    },
    mutations: {
        Updatedeparment_list(state, data) {
            state.deparment_list.rows = [];
            for (let i = 0; i < data.length; i++) {
                var a = {
                    name: '',
                    id: null,
                    code: '',
                }
                a.name = data[i].deptname
                a.id = data[i].deptid
                a.code = data[i].code
                state.deparment_list.rows.push(a);
            }
        },
        district_row_delete(state, index) {
            state.deparment_list.rows.splice(parseInt(index), 1);
        }

    },
    getters: {
        get_deparment_list(state) {
            return state.deparment_list;
        },

    }
}
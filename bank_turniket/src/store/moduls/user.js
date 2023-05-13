export default {
    state: {
        user_list: {
            rows: [],
            columns: [ 'userid','ism', 'gr', 'cardno', 'familiya', 'image_url' ],
            col: []
        },
    },
    actions: {
        async fetch_user(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/SkudMyUserinfoes');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('Updateuser_list', res_data);
        }
    },
    mutations: {
        Updateuser_list(state, data) {
            state.user_list.rows = data;
        },
        district_row_delete(state, index) {
            state.user_list.rows.splice(parseInt(index), 1);
        }

    },
    getters: {
        get_user_list(state) {
            return state.user_list;
        },

    }
}
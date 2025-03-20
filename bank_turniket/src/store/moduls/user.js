export default {
    state: {
        user_list: {
            rows: [],
            columns: ['ism', 'cardno', 'image_url' ],
            col: []
        },
        nosalary_user: [],
    },
    actions: {
        async fetch_user(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/SkudMyUserinfoes');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('Updateuser_list', res_data);
        },
        async fetch_nosalary_user(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/SkudOyliks/getPaginationUsersByOylikkaBriktrilmagan?page=0&size=1000');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('Update_nosalary_user', res_data); 
        }
    },
    mutations: {
        Updateuser_list(state, data) {
            state.user_list.rows = data;
        },
        Update_nosalary_user(state, data) {
            state.nosalary_user = data.items_list;
        },
        district_row_delete(state, index) {
            state.user_list.rows.splice(parseInt(index), 1);
        }

    },
    getters: {
        get_user_list(state) {
            return state.user_list;
        },
        get_nosalary_user(state) {
            return state.nosalary_user;
        },

    }
}
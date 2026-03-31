export default {
    state: {
        user_add_note_list: [],
        user_rasxod_list: [],
    },
    actions: {
        async fetch_user_add_note_list(ctx, data) {
            const res = await fetch(ctx.rootState.hostname + '/SkudUserDayAdd/getPaginationGetByDateTimeUser_add_day?page=0&size=10000&begin_date=' + data.start + '&end_date=' + data.end);
            const res_data = await res.json();
            ctx.commit('Updateuser_add_note_list', res_data);
        },
        async fetch_user_rasxod_list(ctx, data) {
            const res = await fetch(ctx.rootState.hostname + '/SkudUserRasxod/getPaginationGetByDateTimeUser_add_day?page=0&size=10000&begin_date=' + data.start + '&end_date=' + data.end);
            const res_data = await res.json();
            ctx.commit('Updateuser_rasxod_list', res_data);
        }
    },
    mutations: {
        Updateuser_add_note_list(state, data) {
            state.user_add_note_list = data.items_list;
        },
        Updateuser_rasxod_list(state, data) {
            state.user_rasxod_list = data.items_list;
        },

    },
    getters: {
        get_user_add_note_list(state) {
            return state.user_add_note_list;
        },
        get_user_rasxod_list(state) {
            return state.user_rasxod_list;
        },

    }
}
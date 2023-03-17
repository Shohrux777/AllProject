export default {
    state: {
        dashboard_list: {
            rows: [],
            columns: [ 'FIO', 'User ID', 'checktype', 'date', 'time'],
            col: []
        },
    },
    actions: {
        async fetch_dashboard_list(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/SkudMyCheckinouts/getPagination?page=0&size=10');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('Updatedashboard_list', res_data);
        }
    },
    mutations: {
        Updatedashboard_list(state, data) {
            console.log('ishlayabdi')
            state.dashboard_list.rows = data.items_list;
        },
        district_row_delete(state, index) {
            state.dashboard_list.rows.splice(parseInt(index), 1);
        }

    },
    getters: {
        get_dashboard_list(state) {
            return state.dashboard_list;
        },

    }
}
export default {
    state: {
        door_list: {
            rows: [],
            columns: [ 'id','acc_name', 'dbname', 'device', 'inout', 'login', 'password'],
            col: []
        },
    },
    actions: {
        async fetch_Door(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/SkudDoors');
            const res_data = await res.json();
            ctx.commit('Updatedoor_list', res_data);
        }
    },
    mutations: {
        Updatedoor_list(state, data) {
            state.door_list.rows = data;
        },
        district_row_delete(state, index) {
            state.door_list.rows.splice(parseInt(index), 1);
        }

    },
    getters: {
        get_door_list(state) {
            return state.door_list;
        },

    }
}
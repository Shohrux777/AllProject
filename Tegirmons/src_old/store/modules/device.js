export default {
    state: {
        device_list: {
            rows: [],
            columns: [ 'id','device_name', 'mac_address', 'ip_address'],
            col: []
        },
    },
    actions: {
        async fetch_Device(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/SkudDevices');
            const res_data = await res.json();
            ctx.commit('Updatedevice_list', res_data);
        }
    },
    mutations: {
        Updatedevice_list(state, data) {
            state.device_list.rows = data;
        },
        device_delete_row(state, index) {
            state.device_list.rows.splice(parseInt(index), 1);
        }

    },
    getters: {
        get_device_list(state) {
            return state.device_list;
        },

    }
}
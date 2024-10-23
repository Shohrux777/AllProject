export default {
    state: {
        service_group_list: [],
        lab_group_list: {
            rows: [],
            columns: ['name_1', 'activeStatus'],
            col: []
        },
    },
    actions: {
        async fetch_service_group(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/HospitalServiceTypeGroups');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('UpdateService_group', res_data);
        },
        async fetch_lab_group(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/AnalizGroupInfoes');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('UpdateLab_group', res_data);
        }
    },
    mutations: {
        UpdateService_group(state, data) {
            state.service_group_list = data;
        },
        UpdateLab_group(state, data) {
            state.lab_group_list.rows = data;
        },
        lab_group_delete(state, i){
            state.lab_group_list.rows.splice(i,1)
        }
    },
    getters: {
        get_lab_group_list(state) {
            return state.lab_group_list;
        },
        get_service_group_list(state) {
            return state.service_group_list;
        }

    }
}
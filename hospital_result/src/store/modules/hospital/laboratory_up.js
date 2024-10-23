export default {
    state: {
        accept_lab_list: [],
    },
    actions: {
        async fetch_accept_lab_list(ctx, id) {
            const res = await fetch(ctx.rootState.hostname + '/HospitalPatientAnalizHistory/getPaginationByPatient?page=0&size=500&patient_id=' + id);
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('Update_accept_lab_list', res_data);
        },
       
    },
    mutations: {
        Update_accept_lab_list(state, data) {
            console.log('data update')
            console.log(data)
            state.accept_lab_list = data.items_list;
        },
       

    },
    getters: {
        get_accept_lab_list(state) {
            return state.accept_lab_list;
        },
        

    }
}
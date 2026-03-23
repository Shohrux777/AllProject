export default {
    state: {
        tarozi_tortilgan_list: []

    },
    actions: {
        async fetchTarozi_list(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/TegirmonInvoiceNews/getPaginationMashinaBilanTortilganlarList?page=0&size=1000');
            const res_data = await res.json();
            ctx.commit('updateTarozi_list', res_data);
        }
    },
    mutations: {
        updateTarozi_list(state, data) {
            console.log(data)
            state.tarozi_tortilgan_list = data.items_list;
        },
        // brend_delete_row(state, index) {
        //     state.m_brend.rows.splice(parseInt(index), 1);
        // },

    },
    getters: {
        get_tarozi_tortilgan_list(state) {
            return state.tarozi_tortilgan_list;
        }

    }
}
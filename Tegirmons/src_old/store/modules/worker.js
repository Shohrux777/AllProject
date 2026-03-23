export default {
    state: {
        m_worker: {
            rows: [],
            columns: ['fio','phone_number','passport_number','addiotionala_information','note'],
            col: []
        },
        auth_list: [],

        worker_controler: {
            rows: [],
            columns: ['name' ],
            col: []
        },
    },
    actions: {
        async fetchWorker(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/TegirmonWorker/getPagination?page=0&size=100');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('updateWorker', res_data);
        },
        // async fetchClient_column(ctx, id) {
        //     const res = await fetch(ctx.rootState.hostname + '/TexColumnConfigs/getColumnListAsJsonObj?tableName=pos_client&auth_id=' + id);
        //     const res_data = await res.json();
        //     ctx.commit('updateClient_column', res_data);
        // },
    },
    mutations: {
        updateWorker(state, data) {
            // console.log(data)
            state.m_worker.rows = data.items_list;
        },
        worker_delete_row(state, index) {
            state.m_worker.rows.splice(parseInt(index), 1);
        },
        updateWorker_column(state, data) {
            state.m_worker.col = data;
            // console.log(data)
                //status true bolgan ustun nomlarini olish uchun
            state.m_worker.columns = data.column_default_obj.filter(obj => { return obj.status === true }).map(({ name }) => name);
        },

       

    },
    getters: {
        allWorker(state) {
            return state.m_worker;
        },

        all_worker_controler(state) {
            return state.worker_controler;
        }

    }
}
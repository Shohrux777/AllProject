export default {
    state: {
        invoice_list: {
            rows: [],
            columns: ['companyId', 'marketAgentId', 'authorizationId', 'createdDateTime', 'note', 'activeStatus'],
            col: []
        },
        package_list: {
            rows: [],
            columns: ['name'],
            col: []
        },
    },
    actions: {
        async fetch_m_invoice(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/MarketInvoices');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('UpdateInvoiceMarket', res_data);
        },
        async fetch_m_package(ctx) {
            const res = await fetch(ctx.rootState.hostname + '/HospitalServiceTypePacks');
            const res_data = await res.json();
            // console.log(ctx.rootState.hostname);
            ctx.commit('UpdatePackage', res_data);
        }
    },
    mutations: {
        UpdateInvoiceMarket(state, data) {
            state.invoice_list.rows = data;
        },
        invoice_delete_row(state, index) {
            state.invoice_list.rows.splice(parseInt(index), 1);
        },

        UpdatePackage(state, data) {
            state.package_list.rows = data;
        },
        package_delete_row(state, index) {
            state.package_list.rows.splice(parseInt(index), 1);
        }

    },
    getters: {
        get_invoice_list(state) {
            return state.invoice_list;
        },
        get_package_list(state) {
            return state.package_list;
        }

    }
}
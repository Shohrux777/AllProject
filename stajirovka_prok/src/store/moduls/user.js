import axios from 'axios';
export default {
    
    state: {
        user_list: {
            rows: [],
            columns: [ 'full_name','name', 'phone', 'mobile_phone', 'description' ],
            col: []
        },
        nosalary_user: [],
    },
    actions: {
        async fetch_user(ctx) {
            try{
                const token = localStorage.getItem('auth_token');

                console.log('Token:', token); // ← bu chiqsin, `Bearer`siz bo'lishi kerak

                const response = await axios.get(
                ctx.rootState.hostname + '/api/admin/users',
                {
                    headers: {
                    Authorization: `Bearer ${token}`,
                    Accept: 'application/json',
                    'ngrok-skip-browser-warning': '69420'
                    }
                }
                );
                console.log('Natija:', response.data);
                ctx.commit('Updateuser_list', response.data);
            }
            catch (error) {
            console.error('Xatolik:', error.response?.data || error.message);
            alert("Xatolik yuz berdi!");
            }
            
            console.log(ctx.rootState.hostname);
        },
    },
    mutations: {
        Updateuser_list(state, data) {
            console.log(data)
            state.user_list.rows = data;
        },
        
        district_row_delete(state, index) {
            state.user_list.rows.splice(parseInt(index), 1);
        }

    },
    getters: {
        get_user_list(state) {
            return state.user_list;
        },
    }
}

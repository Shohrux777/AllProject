import axios from 'axios';
export default {
    
    state: {
        task_list: {
            rows: [],
            columns: ['name','count', 'note',],
            col: []
        },
        user_answer_list: [],
        user_answer_accept_list: [],
        user_answer_pending_list: [],
        user_answer_incorrect_list: [],

        answer_pending_list: [],
    },
    actions: {
        async fetch_task(ctx) {
            try{
                const token = localStorage.getItem('auth_token');

                console.log('Token:', token); // ← bu chiqsin, `Bearer`siz bo'lishi kerak

                const response = await axios.get(
                ctx.rootState.hostname + '/api/tasks',
                {
                    headers: {
                    Authorization: `Bearer ${token}`,
                    Accept: 'application/json',
                    'ngrok-skip-browser-warning': '69420'
                    }
                }
                );
                console.log('Natija:', response.data);
                ctx.commit('Updatetask_list', response.data);
            }
            catch (error) {
            console.error('Xatolik:', error.response?.data || error.message);
            alert("Xatolik yuz berdi!");
            }
            
            // console.log(ctx.rootState.hostname);
        },
        async fetch_user_task_answers(ctx, user_id) {
            try{
                const token = localStorage.getItem('auth_token');

                console.log('Token:', token); // ← bu chiqsin, `Bearer`siz bo'lishi kerak

                const response = await axios.get(
                ctx.rootState.hostname + '/api/users/' + user_id + '/answers',
                {
                    headers: {
                    Authorization: `Bearer ${token}`,
                    Accept: 'application/json',
                    'ngrok-skip-browser-warning': '69420'
                    }
                }
                );
                console.log('Natija:', response.data);
                ctx.commit('Update_userTaskAnswers', response.data);
            }
            catch (error) {
            console.error('Xatolik:', error.response?.data || error.message);
            alert("Xatolik yuz berdi!");
            }
            
            // console.log(ctx.rootState.hostname);
        },

        async fetch_status_answers(ctx, status) {
            try{
                const token = localStorage.getItem('auth_token');

                console.log('Token:', token); // ← bu chiqsin, `Bearer`siz bo'lishi kerak

                const response = await axios.get(
                ctx.rootState.hostname + '/api/users/answers/' + status,
                {
                    headers: {
                    Authorization: `Bearer ${token}`,
                    Accept: 'application/json',
                    'ngrok-skip-browser-warning': '69420'
                    }
                }
                );
                console.log('Natija:', response.data);
                ctx.commit('Updatepending_task', response.data);
            }
            catch (error) {
            console.error('Xatolik:', error.response?.data || error.message);
            alert("Xatolik yuz berdi!");
            }
            
            // console.log(ctx.rootState.hostname);
        },
    },
    mutations: {
        Updatetask_list(state, data) {
            console.log(data)
            state.task_list.rows = data;
        },
        Update_userTaskAnswers(state, data) {
            console.log('usertask',data)
            state.user_answer_list = data;
        },
        Updatepending_task(state, data) {
            console.log('pending_answer',data);
            state.answer_pending_list = data;
            localStorage.setItem('pending_count', data.length);
        },
        task_row_delete(state, index) {
            state.task_list.rows.splice(parseInt(index), 1);
        }

    },
    getters: {
        get_task_list(state) {
            return state.task_list;
        },
        get_user_answer_list(state) {
            return state.user_answer_list;
        },

        get_answer_pending_list(state) {
            return state.answer_pending_list;
        },
    }
}

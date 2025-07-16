import axios from 'axios';
export default {
    
    state: {
        task_list: {
            rows: [],
            columns: ['name','count', 'note',],
            col: []
        },
        user_answer_list: [],
        user_answer_pending_list: [],
        user_answer_accept_list: [],
        user_answer_incorrect_list: [],


        answer_accept_list: [],
        answer_incorrect_list: [],
        answer_pending_list: [],
        answer_user_task_status: [],
        all_task_count: 0,
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
            // alert("Xatolik yuz berdi!");
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
            // alert("Xatolik yuz berdi!");
            }
            
            // console.log(ctx.rootState.hostname);
        },

        async fetch_user_task_answers_status(ctx, config) {
            try{
                const token = localStorage.getItem('auth_token');

                console.log('Token:', token); // ← bu chiqsin, `Bearer`siz bo'lishi kerak
                console.log('config:', config); // ← bu chiqsin, `Bearer`siz bo'lishi kerak

                const response = await axios.post(
                ctx.rootState.hostname + '/api/calculate-results/',
                config,
                {
                    headers: {
                        Authorization: `Bearer ${token}`,
                        Accept: 'application/json',
                    }
                }
                );
                console.log('Natija:', response.data);
                ctx.commit('Update_userTaskAnswersStatus', response.data);
            }
            catch (error) {
            console.error('Xatolik:', error.response?.data || error.message);
            // alert("Xatolik yuz berdi!");
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
                if(status == 0){
                    ctx.commit('Updatepending_task', response.data);
                }
                else if(status == 1){
                    ctx.commit('Updateanswer_task', response.data);

                }
                else{
                    ctx.commit('Updateanswer_ignore_task', response.data);
                }
            }
            catch (error) {
            console.error('Xatolik:', error.response?.data || error.message);
            // alert("Xatolik yuz berdi!");
            }
            
            // console.log(ctx.rootState.hostname);
        },
    },
    mutations: {
        Updatetask_list(state, data) {
            console.log(data)
            state.all_task_count = 0;
            state.task_list.rows = data;
            for(let i=0; i<state.task_list.rows.length; i++){
                if(state.task_list.rows[i].count > 0){
                    state.all_task_count += state.task_list.rows[i].count;
                }
            }
        },
        Update_userTaskAnswers(state, data) {
            console.log('usertask answer',data)
            state.user_answer_pending_list = [];
            state.user_answer_accept_list = [];
            state.user_answer_incorrect_list = [];
            state.user_answer_list = data.answers;
            for(let i=0; i<data.answers.length; i++){
                if(data.answers[i].status == 0){
                    state.user_answer_pending_list.push(data.answers[i]);
                }
                else if(data.answers[i].status == 1){
                    state.user_answer_accept_list.push(data.answers[i]);
                }
                else{
                    state.user_answer_incorrect_list.push(data.answers[i]);
                }
            }
        },

        Updatepending_task(state, data) {
            console.log('pending_answer',data);
            state.answer_pending_list = data;
            localStorage.setItem('pending_count', data.length);
        },
        Updateanswer_task(state, data) {
            console.log('accept_answer',data);
            state.answer_accept_list = data;
            localStorage.setItem('accept_count', data.length);

        },
        Updateanswer_ignore_task(state, data) {
            console.log('ignore_answer',data);
            state.answer_incorrect_list = data;
            localStorage.setItem('ignore_count', data.length);
        },

        task_row_delete(state, index) {
            state.task_list.rows.splice(parseInt(index), 1);
        },
        Update_userTaskAnswersStatus(state, data){
            console.log('data status user task')
            console.log(data)
            state.answer_user_task_status = data;
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
        get_answer_accept_list(state) {
            return state.answer_accept_list;
        },
        get_answer_incorrect_list(state) {
            return state.answer_incorrect_list;
        },
        get_count_user_answer_pending_list(state){
            return state.user_answer_pending_list.length;
        },
        get_count_user_answer_accept_list(state){
            return state.user_answer_accept_list.length;
        },
        get_count_user_answer_incorrect_list(state){
            return state.user_answer_incorrect_list.length;
        },

        get_user_answer_pending_list(state){
            return state.user_answer_pending_list;
        },
        get_user_answer_accept_list(state){
            return state.user_answer_accept_list;
        },
        get_user_answer_incorrect_list(state){
            return state.user_answer_incorrect_list;
        },
        get_all_task_count(state){
            return state.all_task_count
        },

        get_user_answer_pending_list(state){
            return state.user_answer_pending_list;
        },
        get_user_answer_accept_list(state){
            return state.user_answer_accept_list;
        },
        get_user_answer_incorrect_list(state){
            return state.user_answer_incorrect_list;
        },
        get_answer_user_task_status(state){
            return state.answer_user_task_status;
        },
    }
}

import axios from 'axios'

// Server side calls with axios are contained here
export default {
    
    getQuestions() {
        return axios.get('/Question/GetQuestionDetails')
            .then(response => {
                return response.data
            });
    },

    getAnswersByQuestionId(questionId) {
        return axios.get('/Answer/GetAnswerDetailsByQuestionId', { params: { questionId: questionId } })
            .then(response => {
                return response.data
            });
    }
}

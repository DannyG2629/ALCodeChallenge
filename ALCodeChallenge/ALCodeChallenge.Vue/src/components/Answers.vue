<template>
    <div>
        <h5 class="text-center">... and try to click the accepted answer</h5>
        
        <b-container>
            <b-row class="mt-4 border border-dark" align-v="center" v-for="answer in answersResponse" :key="answer.answerId">
                <b-col cols="11" class="clickable">
                    <span v-html="answer.body" v-on:click="answerSelected(answer)"></span>
                </b-col>
                
            </b-row>

            <b-modal v-model="showModal" ref="modal" title="Your selected answer was..." ok-only>
                {{modalText}}
            </b-modal>
        </b-container>
    </div>
</template>

<script>
    import mvcApi from '@/services/MvcApi'

    export default {
        name: 'Answers',
        props: {
            questionId: Number
        },
        data() {
            return {                
                answersResponse: [],
                showModal: false,
                modalText: ''
            }
        },
        methods: {
            getAnswers(questionId) {
                mvcApi.getAnswersByQuestionId(questionId)
                    .then(response => {
                        this.answersResponse = response;
                    })

            },
            answerSelected(answer) {
                this.showModal = false;
                if (answer.isAccepted) {
                    this.modalText = 'Correct! You selected the accepted answer!';
                }
                else {
                    this.modalText = 'Incorrect! You did not select the accepted answer...';
                }
                this.showModal = true;
            }
        },        
        watch: {
            questionId: function (newVal) {
                this.getAnswers(newVal);                
            }
        }

    };
</script>


<style scoped>
    .clickable {
        cursor: pointer;
    }
</style>
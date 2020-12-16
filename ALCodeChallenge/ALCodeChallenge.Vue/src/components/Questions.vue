<template>
    <b-row class="mx-auto">
        <b-col cols="6">
            <h5 class="text-center">Please select a question ...</h5>

            <b-container>
                <b-row class="mt-4" align-v="center" v-for="question in questionsResponse" :key="question.questionId">

                    <b-col cols="8" :class="{ bisque : question.questionId === selectedQuestionId }">
                        <a :href="question.link" target="_blank">{{ question.title }}</a>
                    </b-col>

                    <b-col cols="2">
                        Answers: {{ question.answerCount }}
                    </b-col>

                    <b-col cols="1">
                        <b-button class="mt-2" size="sm" pill variant="primary" v-on:click="questionSelected(question)">Select</b-button>
                    </b-col>
                </b-row>
            </b-container>
        </b-col>

        <b-col cols="6">
            <Answers :questionId="selectedQuestionId" />
        </b-col>
    </b-row>
</template>

<script>
    import mvcApi from '@/services/MvcApi'
    import Answers from '@/components/Answers.vue';

    export default {
        components: {
            Answers
        },
        name: 'Questions',
        data() {
            return {
                questionsResponse: [],
                selectedQuestionId: 0
            }
        },
        methods: {
            getResult() {
                mvcApi.getQuestions()
                    .then(response => {
                        this.questionsResponse = response;
                    })

            },
            questionSelected(question) {                
                this.selectedQuestionId = question.questionId;
            }
        },
        mounted() {
            this.getResult();
        }

    };
</script>


<style scoped>
    .bisque {
        background-color: bisque;
    }
</style>
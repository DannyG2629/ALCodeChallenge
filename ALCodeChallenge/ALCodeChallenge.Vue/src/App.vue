<template>
    <div id="app">
        <h1 class="text-center mb-5">Recent Stack Overflow Questions and Answers</h1>
        <Questions />        
    </div>
</template>

<script>
    import Questions from './components/Questions.vue';    

    export default {
        name: 'app',
        components: {
            Questions
        }
    };
</script>

<style>
</style>


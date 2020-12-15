using ALCodeChallenge.Data.Interfaces;
using ALCodeChallenge.Model;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace ALCodeChallenge.Data.Test
{
    public class AnswerRepositoryTests
    {
        private const string validAnswers = "{\"items\":[{\"is_accepted\":false,\"answer_id\":65280009,\"question_id\":65279852,\"link\":\"https://stackoverflow.com/questions/65279852/javascript-use-cases-of-currying/65280009#65280009\",\"body\":\"<p>Here's a couple of real-world examples where I use this all the time. First, on the front end:</p>\\n<pre><code>debounce(someEventHandler, someTimeout);\\ndebounce(someOtherEventHandler, someTimeout);\\ndebounce(yetAnotherEventHandler, someTimeout);\\n</code></pre>\\n<p>Let's say that I want all my keyboard handlers debounced by 250ms. I can write that over and over, or...</p>\\n<pre><code>const debounceBy250 = curry(flip(debounce))(250);\\n</code></pre>\\n<p>Now the code is more concise and clearer. Secondly, on the backend let's say that you've got code that gets a request (database, REST, TCP, whatever) timeout from an environmental variable. Just like above, you can either add that every place it's used, or you can partially apply it to the functions that take it.</p>\\n<p>For a case where someone was actually using it but didn't know what it was called, <a href=\\\"http://stackoverflow.com/a/37764035/3757232\\\">see my answer here</a> on this not quite a duplicate question.</p>\\n\"},{\"is_accepted\":true,\"answer_id\":65279957,\"question_id\":65279852,\"link\":\"https://stackoverflow.com/questions/65279852/javascript-use-cases-of-currying/65279957#65279957\",\"body\":\"<p>Its usefull when doing partial application or composing. You can do something like this for example</p>\\n<p><div class=\\\"snippet\\\" data-lang=\\\"js\\\" data-hide=\\\"false\\\" data-console=\\\"true\\\" data-babel=\\\"false\\\">\\r\\n<div class=\\\"snippet-code\\\">\\r\\n<pre class=\\\"snippet-code-js lang-js prettyprint-override\\\"><code>const arr = [1,2,3,4]\\n\\nconst curry = (fn, ...a) =&gt; fn.length &gt; a.length ? (...b) =&gt; curry(fn,...a.concat(b)) : fn.apply(null,a)\\n\\nconst add = curry((a,b) =&gt; a+b)\\n\\nconsole.log(arr.map(add(5)))\\n\\nconst compose = (...fns) =&gt; fns.reduce((f,g) =&gt; b =&gt; f(g(b)))\\n\\nconsole.log(arr.map(compose(add(5),add(2))))</code></pre>\\r\\n</div>\\r\\n</div>\\r\\n</p>\\n\"}],\"has_more\":false,\"quota_max\":300,\"quota_remaining\":292}";
        private const string oneValidAnswer = "{\"items\":[{\"is_accepted\":false,\"answer_id\":65280009,\"question_id\":65279852,\"link\":\"https://stackoverflow.com/questions/65279852/javascript-use-cases-of-currying/65280009#65280009\",\"body\":\"<p>Here's a couple of real-world examples where I use this all the time. First, on the front end:</p>\\n<pre><code>debounce(someEventHandler, someTimeout);\\ndebounce(someOtherEventHandler, someTimeout);\\ndebounce(yetAnotherEventHandler, someTimeout);\\n</code></pre>\\n<p>Let's say that I want all my keyboard handlers debounced by 250ms. I can write that over and over, or...</p>\\n<pre><code>const debounceBy250 = curry(flip(debounce))(250);\\n</code></pre>\\n<p>Now the code is more concise and clearer. Secondly, on the backend let's say that you've got code that gets a request (database, REST, TCP, whatever) timeout from an environmental variable. Just like above, you can either add that every place it's used, or you can partially apply it to the functions that take it.</p>\\n<p>For a case where someone was actually using it but didn't know what it was called, <a href=\\\"http://stackoverflow.com/a/37764035/3757232\\\">see my answer here</a> on this not quite a duplicate question.</p>\\n\"}],\"has_more\":false,\"quota_max\":300,\"quota_remaining\":292}";        
        private const int validQuestionId = 65279852;        

        [Fact]
        public void GetAnswerDetailsByQuestionIdAsync_Returns_List_Of_AnswerDetail()
        {
            var mockContext = new Mock<IAnswerDataContext>();
            mockContext.Setup(mc => mc.GetAnswersAsync(It.IsAny<int>()))
                .ReturnsAsync(validAnswers);

            var sut = new AnswerRepository(mockContext.Object);

            var answerDetails = sut.GetAnswerDetailsByQuestionIdAsync(validQuestionId);

            Assert.IsType<List<AnswerDetail>>(answerDetails.Result);
            Assert.NotEmpty(answerDetails.Result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("This is a bad response")]
        [InlineData(null)]
        public void GetAnswerDetailsByQuestionIdAsync_Returns_Empty_List_With_Bad_Response(string jsonValue)
        {
            var mockContext = new Mock<IAnswerDataContext>();
            mockContext.Setup(mc => mc.GetAnswersAsync(It.IsAny<int>()))
                .ReturnsAsync(jsonValue);

            var sut = new AnswerRepository(mockContext.Object);

            var answerDetails = sut.GetAnswerDetailsByQuestionIdAsync(validQuestionId);

            Assert.IsType<List<AnswerDetail>>(answerDetails.Result);
            Assert.Empty(answerDetails.Result);
        }

        [Fact]
        public void GetAnswerDetails_Returns_List_With_Mapped_AnswerDetail()
        {
            var mockContext = new Mock<IAnswerDataContext>();
            mockContext.Setup(mc => mc.GetAnswersAsync(It.IsAny<int>()))
                .ReturnsAsync(oneValidAnswer);

            var sut = new AnswerRepository(mockContext.Object);

            var AnswerDetail = sut.GetAnswerDetailsByQuestionIdAsync(validQuestionId).Result.First();

            Assert.Equal(65280009, AnswerDetail.AnswerId);
            Assert.True(!AnswerDetail.IsAccepted);
            Assert.Equal(65279852, AnswerDetail.QuestionId);
            Assert.Equal("https://stackoverflow.com/questions/65279852/javascript-use-cases-of-currying/65280009#65280009", AnswerDetail.Link);            
        }

    }
}

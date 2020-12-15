using ALCodeChallenge.Data.Interfaces;
using ALCodeChallenge.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace ALCodeChallenge.Data.Test
{
    public class QuestionRepositoryTests
    {
        private const string questionsWithAcceptedAnswers = "{\"items\":[{\"is_answered\":true,\"accepted_answer_id\":65297802,\"answer_count\":2,\"creation_date\":1607986912,\"question_id\":65297761,\"link\":\"https://stackoverflow.com/questions/65297761/variable-reference-into-an-html-text-html-type\",\"title\":\"Variable reference into an HTML text/html type\",\"body\":\"<p>In JS I assign 5 to variable myVar. Then I would like to pass this variable into an HTML text/html type and show it in my demonstration div. I fail since I get 'myVar' instead of 5. Could you please someone help me.</p>\\n<pre><code>&lt;script&gt;\\n    myVar = 5\\n&lt;/script&gt;\\n\\n&lt;script id=&quot;myBlock&quot; type=&quot;text/html&quot;&gt;myVar&lt;/script&gt;\\n\\n&lt;div id=&quot;DemonstrationDiv&quot;&gt;&lt;/div&gt;\\n\\n&lt;script&gt;\\n    vBlock = document.getElementById('myBlock');\\n    document.getElementById('DemonstrationDiv').innerHTML = vBlock.innerHTML\\n&lt;/script&gt;   \\n</code></pre>\\n\"},{\"is_answered\":true,\"accepted_answer_id\":65297840,\"answer_count\":2,\"creation_date\":1607986904,\"question_id\":65297760,\"link\":\"https://stackoverflow.com/questions/65297760/string-initialisation-containing-zero-compiler-bug-or-intended-behavior\",\"title\":\"String initialisation containing zero --&gt; compiler bug or intended behavior?\",\"body\":\"<p>I try to define some hard coded utf sequences.</p>\\n<p>like</p>\\n<pre><code>    static std::string const cUTF_16_BE_BOM = &quot;\\\\xFE\\\\xFFTest&quot;;\\n    static std::string const cUTF_16_LE_BOM = &quot;\\\\xFF\\\\xFETest&quot;;\\n    static std::string const cUTF_8_BOM     = &quot;\\\\xEF\\\\xBB\\\\xBFTest&quot;;\\n    static std::string const cUTF_32_BE_BOM = &quot;\\\\x00\\\\x00\\\\xFE\\\\xFFTest&quot;;\\n    static std::string const cUTF_32_LE_BOM = &quot;\\\\xFF\\\\xFE\\\\x00\\\\x00Test&quot;;\\n    static std::string const cUTF_7_BOM     = &quot;\\\\x2B\\\\x2F\\\\x76\\\\x38\\\\x2DTest&quot;;\\n</code></pre>\\n<p>but cUTF_32_BE_BOM and cUTF_32_LE_BOM result in an empty string in the first case and a string with length two in the second.</p>\\n<p>isn't a c++ string able to handle multiple '\\\\0' chars in it while knowing its real size? I would expect a strlen to return 0 and 2 as length or an output stream only to consume until the first '\\\\0'. But to be not initialized according to the written code is a bit strange in my perception.</p>\\n\"}],\"has_more\":true,\"quota_max\":300,\"quota_remaining\":293}";
        private const string questionWithOneAnswer = "{\"items\":[{\"is_answered\":true,\"accepted_answer_id\":65297802,\"answer_count\":2,\"creation_date\":1607986912,\"question_id\":65297761,\"link\":\"https://stackoverflow.com/questions/65297761/variable-reference-into-an-html-text-html-type\",\"title\":\"Variable reference into an HTML text/html type\",\"body\":\"<p>In JS I assign 5 to variable myVar. Then I would like to pass this variable into an HTML text/html type and show it in my demonstration div. I fail since I get 'myVar' instead of 5. Could you please someone help me.</p>\\n<pre><code>&lt;script&gt;\\n    myVar = 5\\n&lt;/script&gt;\\n\\n&lt;script id=&quot;myBlock&quot; type=&quot;text/html&quot;&gt;myVar&lt;/script&gt;\\n\\n&lt;div id=&quot;DemonstrationDiv&quot;&gt;&lt;/div&gt;\\n\\n&lt;script&gt;\\n    vBlock = document.getElementById('myBlock');\\n    document.getElementById('DemonstrationDiv').innerHTML = vBlock.innerHTML\\n&lt;/script&gt;   \\n</code></pre>\\n\"}],\"has_more\":true,\"quota_max\":300,\"quota_remaining\":293}";        

        [Fact]
        public void GetQuestionDetails_Returns_List_Of_QuestionDetail()
        {
            var mockContext = new Mock<IQuestionDataContext>();
            mockContext.Setup(mc => mc.GetQuestionsAsync(It.IsAny<long>()))
                .ReturnsAsync(questionsWithAcceptedAnswers);

            var sut = new QuestionRepository(mockContext.Object);

            var questionDetails = sut.GetQuestionDetailsAsync();
            
            Assert.IsType<List<QuestionDetail>>(questionDetails.Result);
            Assert.NotEmpty(questionDetails.Result);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("This is a bad response")]
        [InlineData(null)]
        public void GetQuestionDetails_Returns_Empty_List_With_Bad_Response(string jsonValue)
        {
            var mockContext = new Mock<IQuestionDataContext>();
            mockContext.Setup(mc => mc.GetQuestionsAsync(It.IsAny<long>()))
                .ReturnsAsync(jsonValue);

            var sut = new QuestionRepository(mockContext.Object);

            var questionDetails = sut.GetQuestionDetailsAsync();

            Assert.IsType<List<QuestionDetail>>(questionDetails.Result);
            Assert.Empty(questionDetails.Result);
        }

        [Fact]
        public void GetQuestionDetails_Returns_List_With_Mapped_QuestionDetail()
        {
            var mockContext = new Mock<IQuestionDataContext>();
            mockContext.Setup(mc => mc.GetQuestionsAsync(It.IsAny<long>()))
                .ReturnsAsync(questionWithOneAnswer);

            var sut = new QuestionRepository(mockContext.Object);

            var questionDetail = sut.GetQuestionDetailsAsync().Result.First();            

            Assert.Equal(65297802, questionDetail.AcceptedAnswerId);
            Assert.Equal(2, questionDetail.AnswerCount);
            Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(1607986912).DateTime, questionDetail.CreationDate);
            Assert.Equal(65297761, questionDetail.QuestionId);
            Assert.Equal("https://stackoverflow.com/questions/65297761/variable-reference-into-an-html-text-html-type", questionDetail.Link);
            Assert.Equal("Variable reference into an HTML text/html type", questionDetail.Title);
        }

    }
}

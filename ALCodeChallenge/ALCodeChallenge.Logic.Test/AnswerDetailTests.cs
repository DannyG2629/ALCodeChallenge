using ALCodeChallenge.Data.Interfaces;
using ALCodeChallenge.Model;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace ALCodeChallenge.Logic.Test
{
    public class AnswerDetailTests
    {
        private readonly IEnumerable<AnswerDetail> repoDetails = new List<AnswerDetail>
        {
            new AnswerDetail
            {                                
                AnswerId = 1,
                Body = "Test Body 1",
                IsAccepted = true,
                Link = "Test Link 1",
                QuestionId = 1
            },
            new AnswerDetail
            {
                AnswerId = 2,                
                Body = "Test Body 2",
                IsAccepted = false,
                Link = "Test Link 2",
                QuestionId = 2
            },
            new AnswerDetail
            {
                AnswerId = 3,                
                Body = "Test Body 3",
                IsAccepted = true,
                Link = "Test Link 3",
                QuestionId = 3
            },
            new AnswerDetail
            {
                AnswerId = 4,
                Body = "Test Body 4",
                IsAccepted = true,
                Link = "Test Link 4",
                QuestionId = 3
            },
            new AnswerDetail
            {
                AnswerId = 5,
                Body = "Test Body 5",
                IsAccepted = false,
                Link = "Test Link 5",
                QuestionId = 1
            },
            new AnswerDetail
            {
                AnswerId = 6,
                Body = "Test Body 6",
                IsAccepted = true,
                Link = "Test Link 6",
                QuestionId = 6
            }
        };

        [Fact]
        public void GetAnswerDetailsByQuestionIdAsync_Returns_List_Of_Answer_Detail()
        {
            var mockRepo = new Mock<IAnswerRepository>();
            mockRepo.Setup(mc => mc.GetAnswerDetailsByQuestionIdAsync(It.IsAny<int>()))
                .ReturnsAsync(repoDetails);

            var sut = new AnswerLogic(mockRepo.Object);

            var answerDetails = sut.GetAnswerDetailsByQuestionIdAsync(1);

            Assert.IsType<List<AnswerDetail>>(answerDetails.Result);
            Assert.NotEmpty(answerDetails.Result);
        }

        [Fact]
        public void GetAnswerDetailsByQuestionIdAsync_Returns_Only_Answers_With_Matching_QuestionIds()
        {            
            var mockRepo = new Mock<IAnswerRepository>();
            mockRepo.Setup(mc => mc.GetAnswerDetailsByQuestionIdAsync(It.IsAny<int>()))
                .ReturnsAsync(repoDetails);

            var sut = new AnswerLogic(mockRepo.Object);

            var answerDetails = sut.GetAnswerDetailsByQuestionIdAsync(1);

            Assert.IsType<List<AnswerDetail>>(answerDetails.Result);
            Assert.True(answerDetails.Result.ToList().Count() == 2);
        }

        [Fact]
        public void GetAnswerDetailsByQuestionIdAsync_Returns_Empty_List_With_Multiple_Accepted_Answers()
        {
            var mockRepo = new Mock<IAnswerRepository>();
            mockRepo.Setup(mc => mc.GetAnswerDetailsByQuestionIdAsync(It.IsAny<int>()))
                .ReturnsAsync(repoDetails);

            var sut = new AnswerLogic(mockRepo.Object);

            var answerDetails = sut.GetAnswerDetailsByQuestionIdAsync(3);

            Assert.IsType<List<AnswerDetail>>(answerDetails.Result);
            Assert.Empty(answerDetails.Result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(65279851)]
        [InlineData(-65279852)]
        public void GetAnswerDetailsByQuestionIdAsync_Returns_Empty_List_With_Invalid_QuestionIds(int questionId)
        {
            var mockRepo = new Mock<IAnswerRepository>();
            mockRepo.Setup(mc => mc.GetAnswerDetailsByQuestionIdAsync(It.IsAny<int>()))
                .ReturnsAsync(repoDetails);

            var sut = new AnswerLogic(mockRepo.Object);

            var answerDetails = sut.GetAnswerDetailsByQuestionIdAsync(questionId);

            Assert.IsType<List<AnswerDetail>>(answerDetails.Result);
            Assert.Empty(answerDetails.Result);
        }

        [Fact]
        public void GetAnswerDetailsByQuestionIdAsync_Returns_Empty_List_When_All_Answers_Unaccepted()
        {            
            var mockRepo = new Mock<IAnswerRepository>();
            mockRepo.Setup(mc => mc.GetAnswerDetailsByQuestionIdAsync(It.IsAny<int>()))
                .ReturnsAsync(repoDetails);

            var sut = new AnswerLogic(mockRepo.Object);

            var answerDetails = sut.GetAnswerDetailsByQuestionIdAsync(2);

            Assert.IsType<List<AnswerDetail>>(answerDetails.Result);
            Assert.Empty(answerDetails.Result);
        }


        [Fact]
        public void GetAnswerDetailsByQuestionIdAsync_Returns_Empty_List_When_Not_Multiple_Answers()
        {
            var mockRepo = new Mock<IAnswerRepository>();
            mockRepo.Setup(mc => mc.GetAnswerDetailsByQuestionIdAsync(It.IsAny<int>()))
                .ReturnsAsync(repoDetails);

            var sut = new AnswerLogic(mockRepo.Object);

            var answerDetails = sut.GetAnswerDetailsByQuestionIdAsync(6);

            Assert.IsType<List<AnswerDetail>>(answerDetails.Result);
            Assert.Empty(answerDetails.Result);
        }
    }
}

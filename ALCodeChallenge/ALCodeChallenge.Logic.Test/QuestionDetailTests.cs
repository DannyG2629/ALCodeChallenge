using ALCodeChallenge.Data.Interfaces;
using ALCodeChallenge.Model;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;


namespace ALCodeChallenge.Logic.Test
{
    public class QuestionDetailTests
    {
        private readonly IEnumerable<QuestionDetail> repoDetails = new List<QuestionDetail>
        {
            new QuestionDetail
            {                
                AnswerCount = 1,
                AcceptedAnswerId = 1,
                Body = "Test Body 1",
                CreationDate = DateTime.Now.ToShortDateString(),
                Link = "Test Link 1",
                QuestionId = 1,                
                Title = "Test Title 1"
            },
            new QuestionDetail
            {
                AnswerCount = 2,
                AcceptedAnswerId = null,
                Body = "Test Body 2",
                CreationDate = DateTime.Now.ToShortDateString(),
                Link = "Test Link 2",
                QuestionId = 2,
                Title = "Test Title 2"
            },
            new QuestionDetail
            {
                AnswerCount = 5,
                AcceptedAnswerId = 4,
                Body = "Test Body 3",
                CreationDate = DateTime.Now.ToShortDateString(),
                Link = "Test Link 3",
                QuestionId = 3,
                Title = "Test Title 3"
            }
        };

        [Fact]
        public void GetQuestionDetailsAsync_Returns_List_Of_QuestionDetail()
        {
            var mockRepo = new Mock<IQuestionRepository>();
            mockRepo.Setup(mr => mr.GetQuestionDetailsAsync())
                .ReturnsAsync(repoDetails);

            var sut = new QuestionLogic(mockRepo.Object);

            var questionDetails = sut.GetQuestionDetailsAsync();

            Assert.IsType<List<QuestionDetail>>(questionDetails.Result);
            Assert.NotEmpty(questionDetails.Result);
        }

        [Fact]
        public void GetQuestionDetailsAsync_Returns_Only_QuestionDetails_With_Accepted_Answers()
        {
            var mockRepo = new Mock<IQuestionRepository>();
            mockRepo.Setup(mr => mr.GetQuestionDetailsAsync())
                .ReturnsAsync(repoDetails);

            var sut = new QuestionLogic(mockRepo.Object);

            var questionDetails = sut.GetQuestionDetailsAsync();

            Assert.IsType<List<QuestionDetail>>(questionDetails.Result);
            Assert.All(questionDetails.Result, qd => Assert.NotNull(qd.AcceptedAnswerId));
        }

        [Fact]
        public void GetQuestionDetailsAsync_Returns_Only_QuestionDetails_With_Multiple_Answers()
        {
            var mockRepo = new Mock<IQuestionRepository>();
            mockRepo.Setup(mr => mr.GetQuestionDetailsAsync())
                .ReturnsAsync(repoDetails);

            var sut = new QuestionLogic(mockRepo.Object);

            var questionDetails = sut.GetQuestionDetailsAsync();

            Assert.IsType<List<QuestionDetail>>(questionDetails.Result);
            Assert.All(questionDetails.Result, qd => Assert.True(qd.AnswerCount > 1));
        }

        [Fact]
        public void GetQuestionDetails_Returns_Empty_List_When_No_Accepted_Answers()
        {
            var mockRepo = new Mock<IQuestionRepository>();
            mockRepo.Setup(mr => mr.GetQuestionDetailsAsync())
                .ReturnsAsync(new List<QuestionDetail> { new QuestionDetail { AcceptedAnswerId = null } });

            var sut = new QuestionLogic(mockRepo.Object);

            var questionDetails = sut.GetQuestionDetailsAsync();

            Assert.IsType<List<QuestionDetail>>(questionDetails.Result);
            Assert.Empty(questionDetails.Result);
        }

        [Fact]
        public void GetQuestionDetails_Returns_Empty_List_When_No_Multiple_Answers()
        {
            var mockRepo = new Mock<IQuestionRepository>();
            mockRepo.Setup(mr => mr.GetQuestionDetailsAsync())
                .ReturnsAsync(new List<QuestionDetail> { new QuestionDetail { AnswerCount = 1 } });

            var sut = new QuestionLogic(mockRepo.Object);

            var questionDetails = sut.GetQuestionDetailsAsync();

            Assert.IsType<List<QuestionDetail>>(questionDetails.Result);
            Assert.Empty(questionDetails.Result);
        }

    }
}

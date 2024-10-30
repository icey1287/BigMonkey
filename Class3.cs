using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaYuanSouTi
{
    internal class QuestionService
    {
        private readonly QuestionRepository _repository;
        private Random _random;

        public QuestionService(QuestionRepository repository)
        {
            _repository = repository;
            _random = new Random();
        }

        // 获取一个随机未抽取过的题目
        public Question GetRandomQuestion()
        {
            var unansweredQuestions = _repository.Questions.Where(q => !q.IsAnswered).ToList();
            if (unansweredQuestions.Count == 0) return null;  // 如果没有未抽取的题目

            var index = _random.Next(unansweredQuestions.Count);
            var question = unansweredQuestions[index];
            question.IsAnswered = true;  // 标记为已抽取

            _repository.SaveState();  // 保存状态
            return question;
        }

        // 重置所有题目的状态
        public void ResetQuestions()
        {
            _repository.ResetState();
        }
    }
}

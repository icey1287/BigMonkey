using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;


namespace DaYuanSouTi
{
    internal class QuestionRepository
    {
        private readonly string _directoryPath;
        private readonly string _stateFile = "state.json"; // 保存状态的文件路径
        public List<Question> Questions { get; private set; }

        public QuestionRepository(string directoryPath)
        {
            _directoryPath = directoryPath;
            Questions = new List<Question>();
            LoadQuestions();
            LoadState();
        }

        // 从目录加载题库中的所有题目
        private void LoadQuestions()
        {
            if (!Directory.Exists(_directoryPath)) return;

            foreach (var file in Directory.GetFiles(_directoryPath, "*.txt"))
            {
                var lines = File.ReadAllLines(file);
                if (lines.Length >= 3)
                {
                    string subject = lines[0];  // 第一行是科目
                    string content = lines[1];  // 第二行是题目内容
                    string hint = lines[2];     // 第三行是提示信息
                    Questions.Add(new Question(subject, content, hint));
                }
            }
        }

        // 保存当前状态到 JSON 文件
        public void SaveState()
        {
            var state = JsonConvert.SerializeObject(Questions);
            File.WriteAllText(_stateFile, state);
        }

        // 加载保存的状态
        private void LoadState()
        {
            if (File.Exists(_stateFile))
            {
                var state = File.ReadAllText(_stateFile);
                Questions = JsonConvert.DeserializeObject<List<Question>>(state) ?? new List<Question>();
            }
        }

        // 重置所有题目为未抽取状态
        public void ResetState()
        {
            foreach (var question in Questions)
            {
                question.IsAnswered = false;
            }
            SaveState();
        }
    }
}

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

            foreach (var file in Directory.GetFiles(_directoryPath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);  // 读取JSON文件内容
                try
                {
                    // 使用 JsonSerializer 解析 JSON 文件内容为 List<Question>
                    var questions = JsonConvert.DeserializeObject<List<Question>>(jsonContent);
                    if (questions != null)
                    {
                        Questions.AddRange(questions);  // 添加解析出的题目到 Questions 列表
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading JSON from {file}: {ex.Message}");
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

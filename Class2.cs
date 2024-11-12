using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Runtime.ExceptionServices;
using System.Runtime.CompilerServices;


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

                string jsonData = File.ReadAllText(file);
                string title = Path.GetFileNameWithoutExtension(file);
                
                List<dynamic> questions = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);
                try
                {
                    for (int i = 0; i < questions.Count; i++)
                    {
                        string question = questions[i].question;
                        var options = questions[i].options;
                        string answer = questions[i].answer;
                        string explanation = questions[i].explanation;
                        string image = questions[i].image;
                        Console.WriteLine($"问题: {question}");
                        Console.WriteLine("选项:");
                        for (int j = 0; j < options.Count; j++)
                        {
                            Console.WriteLine($"{j + 1}. {options[j]}");
                        }
                        Console.WriteLine($"答案: {answer}");
                        Console.WriteLine($"解释: {explanation}");
                        if (!string.IsNullOrEmpty(image))
                        {
                            Console.WriteLine($"图片: {image}");
                        }
                        Console.WriteLine();
                        Questions.Add(new Question(title, question,question, image,explanation, answer));
                        
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("-----------------加载完毕-------------------");
                
            }
        }

        // 保存当前状态到 JSON 文件
        public void SaveState()
        {
            //以["Content1","Content2","Content3"]的Json格式将List<Question>写入state.json
           
            List<Tuple<String,String>> cont = new List<Tuple<String,String>>();
            foreach(var question in Questions)
            {
                if (question.IsAnswered)
                {
                    cont.Add(new Tuple<String,String>( question.Content,question.Image));
                }
               
            }
            var state = JsonConvert.SerializeObject(cont);
            File.WriteAllText(_stateFile, state);
            //
            //
            //var state = JsonConvert.SerializeObject(Questions);
            //File.WriteAllText(_stateFile, state);
        }

        // 加载保存的状态
        private void LoadState()
        {
            if (File.Exists(_stateFile))
            {
                var state = File.ReadAllText(_stateFile);
                List<Tuple<String, String>> oldlstate = JsonConvert.DeserializeObject<List<Tuple<String, String>>>(state) ?? new List<Tuple<String, String>>();
         foreach(var s in oldlstate)
                {
                    foreach (var q in Questions)
                    {
                        if (s.Item1 == q.Content)
                        {
                            q.IsAnswered = true;
                        }
                    }
                }

            }
            //从Question中去除Questions_Anwsered
           
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

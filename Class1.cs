using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaYuanSouTi
{
    internal class Question
    {
        public string Title { get; set; }     // 题目
        public string Subject { get; set; }   // 科目
        public string Content { get; set; }   // 题目内容
        public string Hint { get; set; }      // 提示信息
        public string Image { get; set; }      // 图片
        public string Explanation { get; set; }     
        public string Answer { get; set; }      
        public bool IsAnswered { get; set; } = false;  // 是否已抽取过

        public Question(string title, string subject,string content, string image,string explanation, string answer)
        {
            Title = title;
            Subject= subject;
            Content = content;
            Image = image;
            Explanation = explanation;
            Answer = answer;    
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaYuanSouTi
{
    internal class Question
    {
        public string Subject { get; set; }   // 科目
        public string Content { get; set; }   // 题目内容
        public string Hint { get; set; }      // 提示信息
        public string Image { get; set; }      // 图片
        public bool IsAnswered { get; set; } = false;  // 是否已抽取过

        public Question(string subject, string image,string content, string hint)
        {
            Subject = subject;
            Content = content;
            Hint = hint;
            Image = image;
        }
    }
}
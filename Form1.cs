using System;
using System.Windows.Forms;

using System.Drawing;
using System.IO;


namespace DaYuanSouTi
{
    public partial class DaYuan出题 : Form
    {
        private QuestionService _questionService;
        private int zoom = 100; // 默认缩放
        public DaYuan出题()
        {
            InitializeComponent();
            string questionDirectory = "../..";  // 指定题库目录路径
            var repository = new QuestionRepository(questionDirectory);
            _questionService = new QuestionService(repository);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var question = _questionService.GetRandomQuestion();
            if (question == null)
            {
                MessageBox.Show("所有题目已抽完！");
                return;
            }

            // 在 textBox1 中显示题目内容
            webBrowser1.DocumentText=$"{question.Subject}\n{question.Content}";
            LoadImageSafely(pictureBox2, question.Image);
        }
        public static bool LoadImageSafely(PictureBox pictureBox, string imagePath)
        {
            Console.WriteLine($"图片:{imagePath}");
            try
            {
                // 1. 首先检查路径是否为空或null
                if(string.IsNullOrEmpty(imagePath))
                {
                    return false;
                }
                imagePath = "../../" + imagePath;
                Console.WriteLine(imagePath);
                // 2. 检查文件是否存在
                if (!File.Exists(imagePath))
                {
           
                      Console.WriteLine("文件不存在！");
                    return false;
                }

                // 3. 检查文件是否被其他进程锁定
                try
                {
                    using (FileStream stream = File.OpenRead(imagePath))
                    {
                        // 如果能打开文件，说明文件没有被锁定
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("文件正被其他进程使用，无法访问！", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // 4. 检查文件是否为有效的图片
                try
                {
                    // 释放之前的图片资源
                    if (pictureBox.Image != null)
                    {
                        pictureBox.Image.Dispose();
                        pictureBox.Image = null;
                    }

                    // 尝试加载新图片
                    using (var image = Image.FromFile(imagePath))
                    {
                        // 创建图片副本以避免文件锁定
                        pictureBox.Image = new Bitmap(image);
                    }
                    Console.WriteLine($"pictureBox:加载成功:{imagePath}");
                    return true;
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("文件不是有效的图片格式！", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载图片时发生错误：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        
    }

    private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.DocumentText="";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _questionService.ResetQuestions();
            MessageBox.Show("题库已重置！");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            zoom +=5;
            //webBrowser1缩放增加
            webBrowser1.Document.Body.Style = $"zoom:{zoom}%";
            webBrowser1.Update();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            zoom = Math.Max(50, zoom -5);  // 最小缩放50%
            webBrowser1.Document.Body.Style = $"zoom:{zoom}%";
            webBrowser1.Update();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}

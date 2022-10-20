using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel;

namespace List
{
    [Serializable]
    [XmlType("Problem")]
    public class XmlData
    {      
        public XmlData() { }
        public XmlData(string question, string trueAnswer, string answer1, string answer2, string answer3, string answer4)
        {
            this.question = question;
            this.trueAnswer = trueAnswer;
            this.answer1 = answer1;
            this.answer2 = answer2;
            this.answer3 = answer3;
            this.answer4 = answer4;
        }
        [DisplayName("Вопрос")]
        public string question { get; set; }
        [DisplayName("Верный ответ")]
        public string trueAnswer { get; set; }
        [DisplayName("1 ответ")]
        public string answer1 { get; set; }
        [DisplayName("2 ответ")]
        public string answer2 { get; set; }
        [DisplayName("3 ответ")]
        public string answer3 { get; set; }
        [DisplayName("4 ответ")]
        public string answer4 { get; set; }
    }
    class LL
    {
        public string listok { get; set; }
        public override string ToString()
        {
            return listok;
        }
    }

    class QuestionsList
    {
        public static List<XmlData> list = new List<XmlData>();
    }
}
   


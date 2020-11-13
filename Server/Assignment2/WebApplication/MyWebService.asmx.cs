using System;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;

namespace WebApplication
{
    /// <summary>
    /// Summary description for MyWebService
    /// 
    /// Hammad Ahmed
    /// EP1850033
    /// Section A
    /// 
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MyWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public string Introduction()
        {
            return "Hammad Ahmed | EP1850033 | Section A";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string Calculate()
        {
            string data = HttpContext.Current.Request["SubjectMarks"];
            SubjectMark[] subjectMarks = JsonConvert.DeserializeObject<SubjectMark[]>(data);

            SubjectMark MinSubjectMarks = subjectMarks.First(x => x.ObtainMarks == subjectMarks.Min(y => y.ObtainMarks));
            SubjectMark MaxSubjectMarks = subjectMarks.First(x => x.ObtainMarks == subjectMarks.Max(y => y.ObtainMarks));

            decimal noOfSubjects = subjectMarks.Count();
            decimal totalMarks = 100 * noOfSubjects;
            decimal totalObtainMarks = subjectMarks.Sum(x => x.ObtainMarks);
            decimal percent = (totalObtainMarks / totalMarks) * 100;

            Result result = new Result()
            {
                MinSubject = MinSubjectMarks,
                MaxSubject = MaxSubjectMarks,
                Percentage = percent
            };

            return JsonConvert.SerializeObject(result);
        }
        public class SubjectMark
        {
            public string Name { get; set; }
            public int ObtainMarks { get; set; }
        }
        public class Result
        {
            public SubjectMark MinSubject { get; set; }
            public SubjectMark MaxSubject { get; set; }
            public decimal Percentage { get; set; }
        }
    }
}
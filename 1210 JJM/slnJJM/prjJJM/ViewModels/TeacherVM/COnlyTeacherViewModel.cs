using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjJJM.ViewModels.TeacherVM
{
    public class COnlyTeacherViewModel

    {
        public tMember memberForOnlyT
        {
            get;
            set;
        }


        public IEnumerable<tMember> memberData
        {
            get;
            set;
        }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public List<tClass> classData
        {
            get;
            set;
        }

        public tTeacher teacherForOnlyT
        {
            get;
            set; 
        }

        public IEnumerable<tClass> classForOnlyT
        {
            get;
            set;
        }


        public List<memberForClass> memForT
        {
            get;set;
        }

        public class memberForClass
        {
            public string m_name { get; set; }
            public int classID { get; set; }
            public string m_gender { get; set; }
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> m_birth { get; set; }
            public string m_phone { get; set; }
            public string m_email { get; set; }
        }
    }
}
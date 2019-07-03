using System;

namespace RDTask2
{
    [Serializable]
    public class Test
    {
        public string StudentName { get; set; }
        public string TestName { get; set; }
        public DateTime TestPassDate { get; set; }
        public double Mark { get; set; }
    }
}

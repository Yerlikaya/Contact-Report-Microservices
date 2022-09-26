using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    //public class Communication
    //{
    //    public string id { get; set; }
    //    public int communicationType { get; set; }
    //    public string address { get; set; }
    //    public string contactId { get; set; }
    //}

    //public class Datum
    //{
    //    public List<Communication> communications { get; set; }
    //    public string id { get; set; }
    //    public string firstName { get; set; }
    //    public string lastName { get; set; }
    //    public string company { get; set; }
    //}

    //public class Root
    //{
    //    public List<Datum> data { get; set; }
    //    public object errors { get; set; }
    //}
    public class ContactReportDataDto
    {
        public List<ContactWithCommunicationsDto> Data { get; set; }
    }
}

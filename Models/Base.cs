using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UserHubAdminPortal.Models
{
    public class Base
    {
        public Guid ID { get; set; }

        public int Autokey { get; set; }

        public DateTime CreatedDate { get; set; }

        public String? CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public String? ModifiedBy { get; set; }

        public Boolean IsActive { get; set; }

        public int RecordStatus { get; set; }
    }
}
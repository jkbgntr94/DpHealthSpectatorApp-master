using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms_EFCore.Models
{
    

        public class Json
        {
            public Header header { get; set; }
            public Body body { get; set; }
            public string id { get; set; }

        }

        public class Header
        {
            public string id { get; set; }
            public DateTime creation_date_time { get; set; }
            public Acquisition_Provenance acquisition_provenance { get; set; }
            public string user_id { get; set; }
            public Schema_Id schema_id { get; set; }
        }

        public class Acquisition_Provenance
        {
            public string source_name { get; set; }
            public DateTime source_creation_date_time { get; set; }
            public string modality { get; set; }
        }

        public class Schema_Id
        {
            public string _namespace { get; set; }
            public string name { get; set; }
            public string version { get; set; }
        }

        public class Body
        {
            public Effective_Time_Frame effective_time_frame { get; set; }
            public Heart_Rate heart_rate { get; set; }
            public Body_Temperature body_temperature { get; set; }
            public string measurement_location { get; set; }
        }

        public class Effective_Time_Frame
        {
            public DateTime date_time { get; set; }
        }

        public class Heart_Rate
        {
            public string unit { get; set; }
            public float value { get; set; }
        }

        public class Body_Temperature
        {
            public string unit { get; set; }
            public float value { get; set; }
        }
    
}

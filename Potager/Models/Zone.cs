//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Potager.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Zone
    {
        public int zone_id { get; set; }
        public string nom { get; set; }
        public Nullable<System.DateTime> date_creation { get; set; }
        public string type { get; set; }
        public string composition { get; set; }
        public string exposition { get; set; }
        public string paillage { get; set; }
        public string emplacement { get; set; }
        public string structures { get; set; }
        public string observations { get; set; }
    
        public virtual Sujet Sujet { get; set; }
        public virtual Zone_Modification Zone_Modification { get; set; }
    }
}
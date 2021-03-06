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
    
    public partial class Sujet
    {
        public int sujet_id { get; set; }
        public int plante_id { get; set; }
        public int zone_id { get; set; }
        public Nullable<System.DateTime> date_semis { get; set; }
        public Nullable<System.DateTime> date_plantation { get; set; }
        public Nullable<System.DateTime> date_debut_floraison { get; set; }
        public Nullable<System.DateTime> date_mort { get; set; }
        public Nullable<float> poids_recolte_total { get; set; }
        public string maladie { get; set; }
        public string observations { get; set; }
    
        public virtual Graine Graine { get; set; }
        public virtual Plante Plante { get; set; }
        public virtual Recolte Recolte { get; set; }
        public virtual Sujet_Entretien Sujet_Entretien { get; set; }
        public virtual Zone Zone { get; set; }
    }
}

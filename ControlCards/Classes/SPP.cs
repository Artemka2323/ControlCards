//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ControlCards.Classes
{
    using System;
    using System.Collections.Generic;
    
    public partial class SPP
    {
        public int IdSecPat { get; set; }
        public Nullable<int> IdPattern { get; set; }
        public Nullable<int> IdSections { get; set; }
        public Nullable<int> IdPoints { get; set; }
    
        public virtual Pattern Pattern { get; set; }
        public virtual Points Points { get; set; }
        public virtual Sections Sections { get; set; }
    }
}

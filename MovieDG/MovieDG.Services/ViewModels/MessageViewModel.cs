using MovieDG.Data.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDG.Core.ViewModels
{
    public class MessageViewModel
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public ApplicationUser User { get; set; }
    }
}

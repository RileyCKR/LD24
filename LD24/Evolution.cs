using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD24
{
    class Evolution
    {
        public int ID { get; private set; }
        public string Description { get; private set; }

        public Evolution(int id, string description)
        {
            this.ID = id;
            this.Description = description;
        }
    }
}
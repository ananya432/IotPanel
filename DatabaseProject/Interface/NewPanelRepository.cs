using DatabaseProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject.Interface
{
    public interface NewPanelRepository { 
    
            Panel AddPanel(Panel panel);

            Panel getRecordById(int id);


    }

    
}

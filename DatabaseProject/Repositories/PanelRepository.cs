using DatabaseProject.DatabaseContext;
using DatabaseProject.Interface;
using DatabaseProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject.Repositories
{
    public class PanelRepository : NewPanelRepository
    {
        private readonly SqlServerContext _SqlServerContext;

        public PanelRepository(SqlServerContext sqlServerContext)
        {
            _SqlServerContext = sqlServerContext;
        }

        public Panel AddPanel(Panel panel)
        {
            _SqlServerContext.Panel.Add(panel);
            _SqlServerContext.SaveChanges();
            return panel;
        }

        public Panel getRecordById(int panelId)
        {
            var record= _SqlServerContext.Panel.Where(x=> x.panelId== panelId).OrderByDescending(x=>x.id).FirstOrDefault();

            return record;

        }
    }

}

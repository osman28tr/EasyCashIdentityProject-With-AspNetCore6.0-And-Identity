using EasyCashIdentityProject.DataAccessLayer.Abstract;
using EasyCashIdentityProject.DataAccessLayer.Concrete;
using EasyCashIdentityProject.DataAccessLayer.Repositories;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.DataAccessLayer.EntityFramework
{
	public class EfCustomerAccountProcessDal : GenericRepository<CustomerAccountProcess>, ICustomerAccountProcessDal
	{
		public List<CustomerAccountProcess> LastProcess(int id)
		{
			using var context = new Context();
			var values = context.CustomerAccountProcesses.Include(y => y.SenderCustomer).ThenInclude(z => z.AppUser).Include(w=>w.ReceiverCustomer).ThenInclude(b=>b.AppUser).Where(x => x.ReceiverId == id || x.SenderId == id).ToList();
			return values;
		}
	}
}

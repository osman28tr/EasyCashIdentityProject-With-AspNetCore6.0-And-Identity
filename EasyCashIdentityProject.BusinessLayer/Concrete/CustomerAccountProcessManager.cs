using EasyCashIdentityProject.DataAccessLayer.Abstract;
using EasyCashIdentityProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.BusinessLayer.Concrete
{
    public class CustomerAccountProcessManager : ICustomerAccountProcessDal
    {
        private readonly ICustomerAccountProcessDal _customerAccountProcessDal;

        public CustomerAccountProcessManager(ICustomerAccountProcessDal customerAccountProcessDal)
        {
            _customerAccountProcessDal = customerAccountProcessDal;
        }

        public void Delete(CustomerAccountProcess t)
        {
            _customerAccountProcessDal.Delete(t);
        }

        public List<CustomerAccountProcess> GetAll()
        {
            return _customerAccountProcessDal.GetAll();
        }

        public CustomerAccountProcess GetById(int id)
        {
            return _customerAccountProcessDal.GetById(id);
        }

        public void Insert(CustomerAccountProcess t)
        {
            _customerAccountProcessDal.Insert(t);
        }

        public void Update(CustomerAccountProcess t)
        {
            _customerAccountProcessDal.Update(t);
        }
    }
}

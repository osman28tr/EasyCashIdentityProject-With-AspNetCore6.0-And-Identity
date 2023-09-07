using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.DTO.Dtos.CustomerAccountProcessDtos
{
	public class SendMoneyDto
	{
		public string ProcessType { get; set; } = "Havale";
		public decimal Amount { get; set; }
		public DateTime ProcessDate { get; set; } = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
		public string ReceiverAccountNumber { get; set; }
        public string Description { get; set; }
    }
}

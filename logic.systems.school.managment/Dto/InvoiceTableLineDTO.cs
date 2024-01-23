namespace logic.systems.school.managment.Dto
{
    public static class InvoiceTableLineDTO
    {
		public static string Line = @"	
                                <tr class=""details"">
				                    <td style=""border-top: 2px solid #eee;"">{desc}</td>
				                    <td>{paymentDate}</td>
				                    <td>{Classe}</td>
				                    <td>{ClasseRoom}</td>
				                    <td>{Year}</td>
				                    <td>{payment}</td>
		                	    </tr>
                              ";

        public static string LineForInvoice = @"	
                                <tr class=""details"">
									<td style=""border-top: 2px solid #eee;"">{desc}</td>
				                    <td style=""border-top: 2px solid #eee;"">{unityPrice}</td>
								    <td>{quantity}</td>
				                    <td>{paymentDate}</td>
				                    <td>{Classe}</td>
				                    <td>{ClasseRoom}</td>
				                    <td>{Year}</td>
				                    <td>{payment}</td>
		                	    </tr>
                              ";
    }
}
